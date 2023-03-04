using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.IO.Ports;
using System.Security.Cryptography.X509Certificates;
using System.Security;
using System.Runtime.Remoting.Channels;
using System.Reflection;
using System.Xml.Linq;
using System.Collections;
using System.Runtime.InteropServices;

namespace EMH_Player
{
    public partial class PlayerForm : Form
    {
        //変数宣言部
        //アプリでのリアルタイムのmidi確認用
        [DllImport("Winmm.dll")]
        extern static uint midiOutOpen(ref long lphmo, uint uDeviceID, uint dwCallback, uint dwCallbackInstance, uint dwFlags);

        [DllImport("Winmm.dll")]
        extern static uint midiOutClose(long hmo);

        [DllImport("Winmm.dll")]
        extern static uint midiOutShortMsg(long hmo, uint dwMsg);
        private const uint MMSYSERR_NOERROR = 0;
        private const uint MIDI_MAPPER = 0xffffffff;
        private long hMidi;
        /*[System.Runtime.InteropServices.DllImport("winmm.dll",
            CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern int mciSendString(string command,
            System.Text.StringBuilder buffer, int bufferSize, IntPtr hwndCallback);*/
        private int timePos = 0, fileCounter = 0, allTime = 0;
        private List<DataClass.FileData> fileList = new List<DataClass.FileData>();
        private List<DataClass.TempoData> tempoList = new List<DataClass.TempoData>();
        private List<DataClass.MidiData>[] noteList = new List<DataClass.MidiData>[16];
        private DataClass.PartData[] partList = new DataClass.PartData[4];
        private DataClass.HeaderData headerChunk = new DataClass.HeaderData();
        private TimeSpan maxTime, changeTime;
        private System.Threading.Timer trackbar_tim;
        private TickTimer timer;
        Int64[] lastTime;
        Int64 preTime;
        int[] partIndex;
        public PlayerForm()
        {
            InitializeComponent();
            trackbar_tim = new System.Threading.Timer(TimerCallBack, null, Timeout.Infinite, Timeout.Infinite);
            this.FormClosing += (s, e) =>
            {
                trackbar_tim.Change(Timeout.Infinite, Timeout.Infinite);
                trackbar_tim.Dispose();
            };
        }
        //フォーム１の初期設定
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Height = 310;
            this.MinimumSize = new Size(630, 310);
            this.MaximumSize = new Size(800, 310);
            ElectricDevicePortLabel.Enabled = false;
            GuitarDevicePortLabel.Enabled = false;
            PartReset(partList);
            SetPartStatus();
            UiReset();
        }
        private void SetPartStatus() {
            ToolStripStatusLabel[] deviceLabel = new ToolStripStatusLabel[] { MelodyDeviceLabel, BaseDeviceLabel, DrumDeviceLabel };
            string text = "";
            for(int i = 0; i < 4; i++)
            {
                if (i == 1) continue;
                text = partList[i].playPart + " : " + partList[i].playDevice + "[" + partList[i].timerIndex[0].ToString();
                if (partList[i].timerIndex.Length != 1) text += ".." + partList[i].timerIndex[partList[i].timerIndex.Length - 1];
                text += "]";
                deviceLabel[(i == 0) ? i : i - 1].Text = text;
            }

        }
        //partListをデフォルト値に設定する
        public void PartReset(DataClass.PartData[] data)
        {
            Array partArray = Enum.GetValues(typeof(DataClass.Part));
            Array deviceArray = Enum.GetValues(typeof(DataClass.Device));
            foreach (DataClass.Part part in partArray)
            {
                int index = (int)part;
                int[] timerIndex = new int[(index == 1) ? 6 : index + 1];
                data[index].playPart = part;
                data[index].playDevice = (DataClass.Device)deviceArray.GetValue(index);
                data[index].channel = (index == 3) ? 9 : index;
                filterCheckBox.SetItemCheckState(index, CheckState.Checked);
                for (int i = 0; i < timerIndex.Length; i++)
                {
                    int preIndex = (index == 0 || index == 1) ? 1 : ((index == 2) ? 9 : 2);
                    timerIndex[i] = preIndex + i;
                }
                data[index].timerIndex = timerIndex;
            }
        }
        //スレッドタイマーのコールバックメソッド
        private void TimerCallBack(object state)
        {
            Invoke(new DataClass.DelegateData.TrackBarDelegate(UpDateTrackBar));
            Invoke(new DataClass.DelegateData.LabelTimeDelegate(UpDateLabelTime));
        }
        //各種シリアルポートの設定
        public void startSerial(string portName, int baudRate, string device)
        {
            SerialPort port = (device == "ElectricDevice") ? serialPort1 : serialPort2;
            ToolStripStatusLabel label = (device == "ElectricDevice") ? ElectricDevicePortLabel : GuitarDevicePortLabel;

            if (portName == serialPort1.PortName)
            {
                serialPort1.Close();
                ElectricDevicePortLabel.Text = "ElectricDevicePort : ";
                ElectricDevicePortLabel.Enabled = false;
            }else if(portName == serialPort2.PortName)
            {
                serialPort2.Close();
                GuitarDevicePortLabel.Text = "GuitarDevicePort : ";
                GuitarDevicePortLabel.Enabled=false;
            }
            if (port.IsOpen)
            {
                port.Close();
                label.Text = device + "Port : ";
            }
            port.BaudRate = baudRate;
            port.PortName = portName;
            port.Open();
            label.Enabled = true;
            label.Text += portName;
            trackbar_tim.Change(Timeout.Infinite, Timeout.Infinite);
            if (fileList.Count > 0)
            {
                StartButton.Enabled = true;
                NowPlaying.Enabled = true;
            }
        }
        //非同期処理によるギターとリレーの同時通信
        private void SendSerial()
        {
            int cnt = partList.Length;
            partIndex = new int[cnt];
            timer = new TickTimer(SerialSendTask, 1);
            lastTime = new Int64[cnt];
            preTime = (long)Math.Round(DateTime.Now.Ticks / 10000.0);
            for (int i = 0; i < cnt; i++)
            {
                lastTime[i] = (long)Math.Round(DateTime.Now.Ticks / 10000.0);
            }
            timer.Start();
        }
        private void SerialSendTask(object status)
        {
            int cnt = partList.Length;
            string[] serialData = new string[2] { "", "" };
            SerialPort[] port = new SerialPort[] { serialPort1 , serialPort2};
            List<uint> msgs = new List<uint>();
            double currentTime = DateTime.Now.Ticks / 10000.0;
            for (int i = 0; i < cnt; i++)
            {
                int channel = partList[i].channel;
                int pIdx = partIndex[i];
                if (noteList[channel] != null && noteList[channel].Count > 0 && pIdx < noteList[channel].Count)
                {
                    if (currentTime - lastTime[i] >= (Int64)noteList[channel][pIdx].delay)
                    {
                        int idx = (i > 1) ? 0 : i;
                        if (filterCheckBox.GetItemChecked(i) && port[idx].IsOpen)
                        {
                            serialData[idx] += noteList[channel][pIdx].serialTxt;
                            msgs.AddRange(noteList[channel][pIdx].midiMsg);
                        }
                        partIndex[i]++;
                        lastTime[i] = (long)Math.Round(currentTime);
                    }
                }
            }
            if(msgs.Count > 0)
            {
                foreach (uint msg in msgs)
                {
                    midiOutShortMsg(this.hMidi, msg);
                }
            }
            if (serialData[0] != "")
            {
                port[0].Write(Convert.ToString(serialData[0].Length, 2).PadLeft(8, '0') + serialData[0]);
            }
            if (serialData[1] != "")
            {
                port[1].Write(Convert.ToString(serialData[1].Length, 2).PadLeft(8, '0') + serialData[1]);
            }
            if (currentTime - preTime >= allTime)
            {
                timer.Stop();
                Invoke(new DataClass.DelegateData.StopButtonDelegate(Stp));
                if (fileList.Count > 1)
                {
                    Invoke(new DataClass.DelegateData.StartButtonDelegate(Stt));
                }
            }

        }
        private void UpDateTrackBar()
        {
            timePos += 250;
            if (timePos > trackBar.Maximum)
            {
                trackBar.Value = trackBar.Minimum;
            }
            else
            {
                trackBar.Value = timePos;
            }
        }
        private void UpDateLabelTime()
        {
            changeTime += new TimeSpan(0, 0, 0, 0, 250);
            if (changeTime < maxTime)
            {
                LabelTime.Text = changeTime.ToString(@"mm\:ss") + "/" + maxTime.ToString(@"mm\:ss");
            }
            else
            {
                LabelTime.Text = maxTime.ToString(@"mm\:ss") + "/" + maxTime.ToString(@"mm\:ss");
            }
        }
        private void Stt()
        {
            NextButton.Enabled = false;
            ReturnButton.Enabled = false;
            StartButton.Enabled = true;
            StartButton.PerformClick();
        }
        private void Stp()
        {
            StopButton.PerformClick();
        }
        //タスク取り消用メソッド
        private void TaskCancel()
        {
            LabelTime.Enabled = false;
            trackBar.Enabled = false;
        }
        //UIのリセット用メソッド
        private void UiReset() {
            //全てのUIをfalseに設定
            StartButton.Enabled = false;
            StopButton.Enabled = false;
            NextButton.Enabled = false;
            ReturnButton.Enabled = false;
            OpenPlaylistMenuItem.Enabled = false;
            OpenMidiFileMenuItem.Enabled = false;
            RepeatCheck.Enabled = false;
            RandomCheck.Enabled = false;
            PlaylistLabel.Enabled = false;
            FileLabel.Enabled = false;
            NowPlaying.Enabled = false;
            LabelTime.Enabled = false;
        }
        private void OpenFileMenuItem_Click(object sender, EventArgs e)
        {
            fileList.Clear();
            ToolStripMenuItem[] menuItem = new ToolStripMenuItem[] {OpenMidiFileMenuItem, OpenMidiFileMenuItem};
            string ctlName = ((ToolStripMenuItem)sender).Name;
            openFileDialog.Filter = (ctlName == menuItem[0].Name) ? "midiファイル(*.mid)|*.mid" : "プレイリストファイル(*.m3u) | *.m3u";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StartButton.Enabled = true;
                NowPlaying.Enabled = true;
                NextButton.Enabled = true;
                ReturnButton.Enabled = true;
                RandomCheck.Enabled = true;
                RepeatCheck.Enabled = true;
                if (ctlName == menuItem[0].Name)
                {
                    DataClass.FileData data = new DataClass.FileData();
                    data.filePath = openFileDialog.FileName;
                    data.title = "";
                    fileList.Add(data);
                    FileLabel.Text = "MidiFile : " + Path.GetFileNameWithoutExtension(data.filePath);
                }
                else
                {
                    string playlistPath = openFileDialog.FileName;
                    PlaylistLabel.Text = "MidiPlaylist : " + Path.GetFileNameWithoutExtension(playlistPath);
                    FileEncoder(playlistPath);
                    if (RandomCheck.Checked)
                    {
                        IndexRandom();
                    }
                }
            }
        }
        //プレイリストファイルからすべてのパスを取得して格納
        private void FileEncoder(string playlist)
        {
            try
            {
                string line = "";
                using (StreamReader sr = new StreamReader(playlist, Encoding.UTF8))
                {
                    while((line = sr.ReadLine()) != null)
                    {
                        DataClass.FileData data = new DataClass.FileData();
                        data.playlistPath = playlist;
                        data.filePath = Path.GetDirectoryName(playlist) + "\\" + line;
                        data.title = "";
                        fileList.Add(data);
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //ヘッダーチャンク解析イベント
        private void HeaderChunkAnalysis()
        {
            tempoList.Clear();
            //ヘッダチャンクの解析
            using(FileStream stream = new FileStream(fileList[fileCounter].filePath, FileMode.Open, FileAccess.Read))
            using(BinaryReader reader = new BinaryReader(stream))
            {
                headerChunk.chunkID = reader.ReadBytes(4);
                //リトルエンディアンならビットを反転させる
                if (BitConverter.IsLittleEndian)
                {
                    byte[] byteArray = reader.ReadBytes(4);
                    Array.Reverse(byteArray);
                    headerChunk.dataLength = BitConverter.ToInt32(byteArray, 0);

                    byteArray = reader.ReadBytes(2);
                    Array.Reverse(byteArray);
                    headerChunk.format = BitConverter.ToInt16(byteArray, 0);

                    byteArray = reader.ReadBytes(2);
                    Array.Reverse(byteArray);
                    headerChunk.tracks = BitConverter.ToInt16(byteArray, 0);

                    byteArray = reader.ReadBytes(2);
                    Array.Reverse(byteArray);
                    headerChunk.division = BitConverter.ToInt16(byteArray, 0);
                }
                else
                {
                    headerChunk.dataLength = BitConverter.ToInt32(reader.ReadBytes(4), 0);
                    headerChunk.format = BitConverter.ToInt16(reader.ReadBytes(2), 0);
                    headerChunk.tracks = BitConverter.ToInt16(reader.ReadBytes(2), 0);
                    headerChunk.division = BitConverter.ToInt16(reader.ReadBytes(2), 0);
                }
                if(headerChunk.format != 1)
                {
                    MessageBox.Show(Path.GetFileNameWithoutExtension(fileList[fileCounter].filePath) + "は対応していないフォーマットです。");
                    if(fileCounter++ >= fileList.Count)
                    {
                        StopButton.PerformClick();
                    }
                    return;
                }
                headerChunk.trackData = new DataClass.HeaderData.TrackData[headerChunk.tracks];
                //トラックチャンクの解析
                for(int i = 0; i < headerChunk.tracks; i++)
                {
                    headerChunk.trackData[i].chunkID = reader.ReadBytes(4);
                    if (BitConverter.IsLittleEndian)
                    {
                        byte[] byteArray = reader.ReadBytes(4);
                        Array.Reverse(byteArray);
                        headerChunk.trackData[i].dataLength = BitConverter.ToInt32(byteArray, 0);
                    }
                    else
                    {
                        headerChunk.trackData[i].dataLength = BitConverter.ToInt32(reader.ReadBytes(4), 0);
                    }
                    headerChunk.trackData[i].data = reader.ReadBytes(headerChunk.trackData[i].dataLength);
                    //各トラックデータについてイベントとデルタタイムの抽出
                    TrackDataAnalysis(headerChunk.trackData[i].data);                    
                }
            }
        }
        //トラックチャンク解析イベント
        private void TrackDataAnalysis(byte[] data)
        {
            uint currentTime = 0;
            double delay = 0, delayOffset = 0;
            bool[] longFlags = new bool[128];
            int[] partIndex = new int[10];
            byte statusByte = 0;
            int i = 0;
            while (true)
            {
                uint deltaTime = 0;
                byte laneIndex = 0;
                byte channel = 0, velocity = 0;
                DataClass.NoteType type = new DataClass.NoteType { };
                while (true)
                {
                    //デルタタイムの抽出
                    byte temp = data[i++];
                    deltaTime |= temp & (uint)0x7f;
                    if ((temp & 0x80) == 0) break;
                    deltaTime = deltaTime << 7;
                }
                currentTime += deltaTime;
                if (data[i] < 0x80)
                {
                    //ランニングステータス
                }
                else
                {
                    statusByte = data[i++];
                }

                if (statusByte >= 0x80 && statusByte <= 0xef)
                {
                    channel = (byte)(statusByte & 0x0f);
                    if (noteList[channel] == null) noteList[channel] = new List<DataClass.MidiData>();
                    switch (statusByte & 0xf0)
                    {
                        //ノートオフ
                        case 0x80:
                            //ノート番号
                            laneIndex = data[i++];
                            //ヴェロシティ
                            velocity = data[i++];
                            if (longFlags[laneIndex])
                            {
                                type = DataClass.NoteType.Off;
                                longFlags[laneIndex] = false;
                            }
                            break;
                        //ノートオン
                        case 0x90:
                            laneIndex = data[i++];
                            velocity = data[i++];
                            type = DataClass.NoteType.On;
                            longFlags[laneIndex] = true;
                            //ヴェロシティ:0はノーツオフ
                            if (velocity == 0)
                            {
                                if (longFlags[laneIndex])
                                {
                                    type = DataClass.NoteType.Off;
                                    longFlags[laneIndex] = false;
                                }
                            }
                            break;
                        //音色設定
                        case 0xc0:
                            byte harmony = data[i++];
                            uint msg = (uint)((harmony << 8) + statusByte);
                            midiOutShortMsg(this.hMidi, msg);
                            break;
                        //これ以降はインクリメント用
                        case 0xa0:
                            i += 2;
                            break;
                        case 0xb0:
                            i += 2;
                            break;
                        case 0xd0:
                            i += 1;
                            break;
                        case 0xe0:
                            i += 2;
                            break;
                    }
                }
                //SysExイベント用、インクリメントオンリー
                else if (statusByte == 0x70 || statusByte == 0x7f)
                {
                    byte dataLength = data[i++];
                    i += dataLength;
                }
                //メタイベント用
                else if (statusByte == 0xff)
                {
                    byte metaEventID = data[i++];
                    byte dataLength = data[i++];
                    switch (metaEventID)
                    {
                        //曲名を格納
                        case 0x03:
                            if (fileList[fileCounter].title == "")
                            {
                                NowPlaying.Text = "";
                                byte[] bytes = new byte[100];
                                for (int j = 0; j < dataLength; j++)
                                {
                                    byte temp = data[i++];
                                    bytes[j] = temp;
                                    if ((temp & 0xf0) >= 0x80)
                                    {
                                        bytes[++j] |= data[i++];
                                    }
                                }
                                DataClass.FileData fileData = new DataClass.FileData();
                                fileData.title += Encoding.GetEncoding("Shift_JIS").GetString(bytes);
                                fileData.filePath = fileList[fileCounter].filePath;
                                fileList[fileCounter] = fileData;
                                NowPlaying.Text = fileData.title;
                            }
                            else
                            {
                                i += dataLength;
                            }
                            break;
                        case 0x00:
                        case 0x01:
                        case 0x02:
                        case 0x04:
                        case 0x05:
                        case 0x06:
                        case 0x07:
                        case 0x20:
                        case 0x21:
                        case 0x2f:
                        case 0x54:
                        case 0x58:
                        case 0x59:
                        case 0x7f:
                            i += dataLength;
                            break;
                        //テンポ情報を格納
                        case 0x51:
                            {
                                DataClass.TempoData tempoData = new DataClass.TempoData();
                                tempoData.eventTime = (int)currentTime;
                                uint tempo = 0;
                                tempo |= data[i++];
                                tempo <<= 8;
                                tempo |= data[i++];
                                tempo <<= 8;
                                tempo |= data[i++];
                                tempoData.bpm = 60000000 / (float)tempo;
                                tempoList.Add(tempoData);
                            }
                            break;
                    }
                }
                float bpm = 0;
                //bpmの抽出
                for (int j = tempoList.Count - 1; j >= 0; j--)
                {
                    if (currentTime > tempoList[j].eventTime)
                    {
                        bpm = tempoList[j].bpm;
                        break;
                    }
                }
                if(bpm != 0)
                {
                    double tmp = 60000.0 * deltaTime / bpm / headerChunk.division;
                    delayOffset += tmp;
                    delay += tmp;
                }
                if (laneIndex != 0)
                {
                    double pos = storeNoteData(partIndex, channel, delayOffset, laneIndex, velocity, type);
                    if(pos >= 0)
                        delayOffset = pos;
                }
                //ループ最後に残っているデータを格納
                if (i >= data.Length)
                {
                    break;
                }
            }
            allTime = (allTime < delay) ? (int)delay : allTime;
        }
        private double storeNoteData(int[] partIdx, byte channel, double delay,
            byte laneIndex, byte velocity, DataClass.NoteType type)
        {
            int idx = 0, listIdx = Array.FindIndex(partList, a => a.channel == channel), partMax = 0, bin = 0;
            string text = "", binTxt = "";
            DataClass.MidiData data = new DataClass.MidiData();
            if (listIdx < 0) return -1;
            data.playPart = partList[listIdx].playPart;
            partMax = partList[listIdx].timerIndex.Length;
            if (type == DataClass.NoteType.On)
            {
                for (int i = 0; i < partMax; i++)
                {
                    if (partIdx[i] == 0)
                    {
                        partIdx[i] = laneIndex;
                        idx = i + 1;
                        break;
                    }
                }
                if (idx == 0) return -1;
                int timerOffset = partList[listIdx].timerIndex[idx - 1] - 1;
                timerOffset = (timerOffset < 5) ? timerOffset : timerOffset - 3;
                text = (timerOffset + 1).ToString() + ",ON," + laneIndex.ToString() + ",";
                binTxt = Convert.ToString(timerOffset, 2).PadLeft(4, '0') + "1" + Convert.ToString(laneIndex, 2).PadLeft(8, '0');
                bin <<= 4;
                bin += timerOffset;
                bin <<= 1;
                bin += 1;
                bin <<= 8;
                bin += laneIndex;
                data.midiMsg = new List<uint>() { (uint)((velocity << 16) + (laneIndex << 8) + 0x90 + channel) };
            }
            else if (type == DataClass.NoteType.Off)
            {
                for (int i = 0; i < partMax; i++)
                {
                    if (partIdx[i] == laneIndex)
                    {
                        partIdx[i] = 0;
                        idx = i + 1;
                        break;
                    }
                }
                if (idx == 0) return -1;
                if (partList[listIdx].playPart == DataClass.Part.Drum)
                {
                    //ドラムのノーツオフはスルーする
                    return delay;
                }
                int timerOffset = partList[listIdx].timerIndex[idx - 1] - 1;
                timerOffset = (timerOffset < 5) ? timerOffset : timerOffset - 3;
                text += (timerOffset + 1).ToString() + ",OFF,";
                binTxt = Convert.ToString(timerOffset, 2).PadLeft(4, '0') + "0" + Convert.ToString(laneIndex, 2).PadLeft(8, '0');
                bin <<= 4;
                bin += timerOffset;
                bin <<= 1;
                bin += 0;
                data.midiMsg = new List<uint>() { (uint)((velocity << 16) + (laneIndex << 8) + 0x80 + channel) };
            }
            data.logTxt = text;
            data.serialTxt = binTxt;
            if (delay > 0)
            {
                data.delay = delay;
                //新しく要素を作成
                noteList[channel].Add(data);
                return 0;
            }
            else
            {
                //イベントタイムが0の時はリストの前要素を編集
                data.delay = noteList[channel][noteList[channel].Count-1].delay;
                data.logTxt = noteList[channel][noteList[channel].Count - 1].logTxt + data.logTxt;
                data.serialTxt = noteList[channel][noteList[channel].Count - 1].serialTxt + data.serialTxt;
                data.midiMsg.AddRange(noteList[channel][noteList[channel].Count - 1].midiMsg);
                data.midiMsg.Sort();
                noteList[channel][noteList[channel].Count - 1] = data;
                return delay;
            }
        }
        //ランダムなインデックス作成（シャッフル時）
        private void IndexRandom()
        {
            Random rnd = new Random();
            for(int i = 0; i < fileList.Count; i++)
            {
                int random = rnd.Next(i, fileList.Count);
                DataClass.FileData tmp = fileList[i];
                fileList[i] = fileList[random];
                fileList[random] = tmp;
            }
        }
        //以下各種音楽再生用ボタンクリックイベント
        private void StartButton_Click(object sender, EventArgs e)
        {
            timePos = 0;
            StartButton.Enabled = false;
            menuStrip1.Enabled = false;
            if (fileCounter >= fileList.Count)
            {
                fileCounter = 0;
                if (!RepeatCheck.Checked)
                {
                    NextButton.Enabled = true;
                    ReturnButton.Enabled = true;
                    StopButton.Enabled = true;
                    StopButton.PerformClick();
                    return;
                }
            }
            if( midiOutOpen( ref this.hMidi, MIDI_MAPPER, 0, 0, 0) != MMSYSERR_NOERROR)
            {
                MessageBox.Show("midiOutOpen error");
            }
            StopButton.Enabled = true;
            RepeatCheck.Enabled = false;
            RandomCheck.Enabled = false;
            LabelTime.Enabled = true;
            trackBar.Enabled = true;
            NextButton.Enabled = true;
            ReturnButton.Enabled = true;
            HeaderChunkAnalysis();
            SendSerial();
            trackBar.Maximum = (int)allTime;
            maxTime = TimeSpan.FromMilliseconds((int)allTime);
            changeTime = new TimeSpan(0, 0, 0);
            LabelTime.Text = changeTime.ToString(@"mm\:ss") + "/" + maxTime.ToString(@"mm\:ss");
            trackbar_tim.Change(0, 245);
        }
        private void StopButton_Click(object sender, EventArgs e)
        {
            StopButton.Enabled = false;
            menuStrip1.Enabled = true;
            allTime = 0;
            trackbar_tim.Change(Timeout.Infinite, Timeout.Infinite);
            changeTime = new TimeSpan(0, 0, 0);
            TaskCancel();
            //midiデバイスをクローズ
            midiOutClose(this.hMidi);
            //string text = "1,OFF,2,OFF,3,OFF,4,OFF,5,OFF,6,OFF,7,OFF,8,OFF,9,OFF,10,OFF,";
            string binTxt = "000110010000000010001000011001000";
            //if (serialPort1.IsOpen) serialPort1.Write(binTxt);
            //if(serialPort2.IsOpen) serialPort2.Write(binTxt);
            StartButton.Enabled = true;
            LabelTime.Enabled = false;
            trackBar.Enabled = false;
            timePos = 0;
            trackBar.Value = 0;
            RepeatCheck.Enabled = true;
            RandomCheck.Enabled = true;
            //データをリセット
            tempoList.Clear();
            for(int i = 0; i < noteList.Length; i++)
            {
                if (noteList[i] == null) continue;
                noteList[i].Clear();
            }
            for(int i = 0; i < fileList.Count; i++)
            {
                DataClass.FileData data = new DataClass.FileData();
                data.playlistPath = fileList[i].playlistPath;
                data.filePath = fileList[i].filePath;
                data.title = "";
                fileList[i] = data;
            }
        }
        private void NextButton_Click(object sender, EventArgs e)
        {
            StopButton.PerformClick();
            NextButton.Enabled = false;
            ReturnButton.Enabled = false;
            fileCounter++;
            StartButton.PerformClick();
        }
        private void ReturnButton_Click(object sender, EventArgs e)
        {
            StopButton.PerformClick();
            NextButton.Enabled = false;
            ReturnButton.Enabled = false;
            if (fileCounter <= 1)
            {
                fileCounter = 0;
            }
            else
            {
                fileCounter--;
            }
            StartButton.PerformClick();
        }

        private void HelpMenuItem_Click(object sender, EventArgs e)
        {
            string helpStr = "再生を開始するにはまず、\"設定->シリアルポート\"より各デバイスのシリアルポートを設定してください。\n" +
                "その後、\"プレイヤー->プレイモード\"よりファイル単体の再生かプレイリスト(m3uファイル)による再生かを選択します。\n" +
                "最後に\"ファイル->ファイル（プレイリスト）を開く\"を選択して再生したいファイルを選択してください。\n\n" +
                "パートごとのMidiチャネルやデバイスの詳細を設定したい場合は、設定メニューを参照してください。";
            MessageBox.Show(helpStr);
        }

        private void RandomCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (RandomCheck.Checked)
            {
                IndexRandom();
            }
            else
            {
                string playlist = fileList[fileCounter].playlistPath;
                fileList.Clear();
                FileEncoder(playlist);
            }
        }
        //トラックバーのスクロールの変更はNG
        private void trackBar_Scroll(object sender, EventArgs e)
        {
            if (trackBar.Value >= trackBar.Maximum)
            {
                trackBar.Value = trackBar.Minimum;
            }
            else
            {
                trackBar.Value = timePos;
            }
        }

        private void SettingSerialportMenuItem_Click(object sender, EventArgs e)
        {
            SettingSerialportForm form = new SettingSerialportForm();
            form.ShowDialog();
            try
            {
                if (!form.IsDisposed)
                {
                    startSerial(form.GetPortName(), form.GetBaudRate(), form.GetDevice());
                    form.Close();
                    OpenPlaylistMenuItem.Enabled = true;
                    OpenMidiFileMenuItem.Enabled = true;
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                SettingSerialportMenuItem.PerformClick();
            }
        }

        private void SettingDeviceMenuItem_Click(object sender, EventArgs e)
        {
            SettingDevicecsForm form = new SettingDevicecsForm(partList);
            form.ShowDialog();
            try
            {
                if (!form.IsDisposed)
                {
                    DataClass.PartData[] data = form.GetData();
                    if (data != null) {
                        partList = data;
                        SetPartStatus();
                        MessageBox.Show("デバイスと演奏パートを設定しました。");
                    }
                    SettingDeviceMenuItem.PerformClick();
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SettingChannelMenuItem_Click(object sender, EventArgs e)
        {
            SettingMidiChannelForm form = new SettingMidiChannelForm(partList);
            form.ShowDialog();
            try
            {
                if (!form.IsDisposed)
                {
                    DataClass.PartData[] data = form.GetData();
                    if (data != null)
                    {
                        partList = data;
                        SetPartStatus();
                        MessageBox.Show("チャネルを設定しました。");
                    }
                    SettingChannelMenuItem.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}