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
using NAudio.Midi;
using System.Net.Configuration;

namespace EMH_Player
{
    public partial class PlayerForm : Form
    {
        //変数宣言部
        private static int timePos = 0, fileCounter = 0, timIdx = 0;
        private static List<DataClass.FileData> fileList = new List<DataClass.FileData>();
        private static List<DataClass.TempoData> tempoList = new List<DataClass.TempoData>();
        private static List<DataClass.MidiData> playData = new List<DataClass.MidiData>();
        private static DataClass.PartData[] partList = new DataClass.PartData[4];
        private static DataClass.HeaderData headerChunk = new DataClass.HeaderData();
        private static TimeSpan maxTime, changeTime;
        private static System.Threading.Timer trackbar_tim;
        private static TickTimer timer;
        private static double lastTime, allTime = 0, preTime;
        private static MidiOut midi;
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
                data[index].port = (index == 1) ? serialPort2 : serialPort1;
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
            timer = new TickTimer(SerialSendTask, 1);
            timIdx = 0;
            lastTime = DateTime.Now.Ticks / 10000.0;
            preTime = DateTime.Now.Ticks / 10000.0;
            timer.Start();
        }
        private void SerialSendTask(object status)
        {
            string[] serialData = new string[2] { "", "" };
            List<uint> msgs = new List<uint>();
            double currentTime = Math.Round(DateTime.Now.Ticks / 10000.0);
            if (playData == null || playData.Count < 1) return;
            if (timIdx < playData.Count)
            {
                if (currentTime - lastTime >= Math.Floor(playData[timIdx].delay))
                {
                    for(int i = 0; i < playData[timIdx].midiMsg.Count; i++)
                    {
                        midi.Send(playData[timIdx].midiMsg[i].RawData);
                        int idx = (int)playData[timIdx].playPart[i];
                        if (filterCheckBox.GetItemChecked(idx))
                        {
                            //チェックオンの時は音を鳴らし、midi送信データに値を格納する
                            idx = (idx != 1) ? 0 : 1;
                            serialData[idx] += playData[timIdx].serialTxt[idx];
                            midi.Send(playData[timIdx].midiMsg[i].RawData);
                        }
                        else if (!filterCheckBox.GetItemChecked(idx))
                        {
                            //チェックオフ時はノーツオフのデータ(restMessage)を格納
                            idx = (idx != 1) ? 0 : 1;
                            serialData[idx] += playData[timIdx].serialRstTxt;
                            midi.Send(playData[timIdx].midiRstMsg[i].RawData);
                        }
                    }
                    if (playData[timIdx].serialTxt[0] != null && playData[timIdx].serialTxt[0] != "" && serialPort1.IsOpen)
                    {
                        serialPort1.Write(Convert.ToString(serialData[0].Length, 2).PadLeft(8, '0') + serialData[0]);
                    }
                    if (playData[timIdx].serialTxt[1] != null && playData[timIdx].serialTxt[1] != "" && serialPort2.IsOpen)
                    {
                        serialPort1.Write(Convert.ToString(serialData[1].Length, 2).PadLeft(8, '0') + serialData[1]);
                    }
                    timIdx++;
                    lastTime = currentTime;
                }
            }
            else
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
                if(headerChunk.format == 2)
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
            uint currentTime = 0, currentTempoDelay = 0, tempoEvent = 0;
            double delayOffset = 0, pos = (playData.Count > 0) ? playData[0].delay : 0, currentDelay = 0;
            bool[] longFlags = new bool[128];
            int[] partIndex = new int[10];
            byte channel = 0, statusByte = 0;
            int i = 0, currentTempoIndex = 0, delayIndex = 0;
            while (true)
            {
                uint deltaTime = 0;
                byte laneIndex = 0;
                byte velocity = 0;
                DataClass.NoteType type = new DataClass.NoteType { };
                while (true)
                {
                    //デルタタイムの抽出
                    byte temp = data[i++];
                    deltaTime |= temp & (uint)0x7f;
                    if ((temp & 0x80) == 0) break;
                    deltaTime = deltaTime << 7;
                }
                if (data[i] < 0x80)
                {
                    //ランニングステータス
                }
                else
                {
                    statusByte = data[i++];
                }
                double tmp = 0;
                //bpmの抽出
                for (int j = 0; j < tempoList.Count; j++)
                {
                    int cnt = 0;
                    if (currentTime >= currentTempoDelay
                        && currentTime + deltaTime <= tempoList.Sum(a => {
                            if (cnt++ <= j) return a.eventTime;
                            return 0;}))
                    {
                        for (int k = currentTempoIndex; ++k <= j;)
                        {
                            float bpm = tempoList[currentTempoIndex].bpm;
                            if (currentTime + deltaTime >= currentTempoDelay + tempoList[k].eventTime)
                            {
                                tmp += 60000.0 * 
                                    ((currentTime > currentTempoDelay) ? currentTempoDelay + tempoList[k].eventTime - currentTime
                                    : tempoList[k].eventTime) / bpm / headerChunk.division;
                                currentTempoDelay += (uint)tempoList[k].eventTime;
                                currentTempoIndex = k;
                            }
                            else{
                                tmp += 60000.0 * 
                                    ((currentTime < currentTempoDelay) ? currentTime + deltaTime - currentTempoDelay : deltaTime) 
                                    / bpm / headerChunk.division;
                                j = tempoList.Count;
                                break;
                            }
                        }
                    }
                    else if (currentTime + deltaTime > tempoList.Sum(a => a.eventTime))
                    {
                        tmp += 60000.0 * deltaTime / tempoList[tempoList.Count - 1].bpm / headerChunk.division;
                        break;
                    }
                }
                currentTime += deltaTime;
                tempoEvent += deltaTime;
                delayOffset += tmp;

                if (statusByte >= 0x80 && statusByte <= 0xef)
                {
                    channel = (byte)(statusByte & 0x0f);
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
                            midi.Send(MidiMessage.ChangePatch(harmony, channel+1).RawData);
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
                        case 0x54:
                        case 0x58:
                        case 0x59:
                        case 0x7f:
                            i += dataLength;
                            break;
                        //トラックの終了
                        case 0x2f:
                            return;
                        //テンポ情報を格納
                        case 0x51:
                            {
                                DataClass.TempoData tempoData = new DataClass.TempoData();
                                tempoData.eventTime = tempoEvent;
                                tempoEvent = 0;
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
                if (laneIndex != 0)
                {
                    storeNoteData(partIndex, channel, ref delayOffset, ref currentDelay, ref pos, ref delayIndex, laneIndex, velocity, type);
                }
            }
        }
        private void storeNoteData(int[] partIdx, byte channel, ref double delay, ref double currentDelay, ref double pos, ref int index,
            byte laneIndex, byte velocity, DataClass.NoteType type)
        {
            int idx = 0, listIdx = Array.FindIndex(partList, a => a.channel == channel), partMax = 0;
            if (listIdx < 0) return;

            DataClass.MidiData data = new DataClass.MidiData();
            data.playPart = new List<DataClass.Part>();
            data.serialTxt = new string[2];
            data.serialRstTxt = new string[2];
            data.playPart.Add(partList[listIdx].playPart);
            int dataIdx = 0;
            if (partList[listIdx].playPart == DataClass.Part.Guitar) dataIdx = 1;
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
                if (idx == 0) return;
                int timerOffset = partList[listIdx].timerIndex[idx - 1] - 1;
                timerOffset = (timerOffset < 5) ? timerOffset : timerOffset - 3;
                data.logTxt = (timerOffset + 1).ToString() + ",ON," + laneIndex.ToString() + ",";
                data.serialTxt[dataIdx] = Convert.ToString(timerOffset, 2).PadLeft(4, '0') + "1" + Convert.ToString(laneIndex, 2).PadLeft(8, '0');
                data.serialRstTxt[dataIdx] = Convert.ToString(timerOffset, 2).PadLeft(4, '0') + "0";
                data.midiMsg = new List<MidiMessage>() { MidiMessage.StartNote(laneIndex, velocity, channel+1) };
                data.midiRstMsg = new List<MidiMessage>() { MidiMessage.StopNote(laneIndex, velocity, channel+1) };
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
                if (idx == 0) return;
                //ドラムのノーツオフはスルーする
                if (partList[listIdx].playPart == DataClass.Part.Drum) return;

                int timerOffset = partList[listIdx].timerIndex[idx - 1] - 1;
                timerOffset = (timerOffset < 5) ? timerOffset : timerOffset - 3;
                data.logTxt += (timerOffset + 1).ToString() + ",OFF,";
                data.serialTxt[dataIdx] = Convert.ToString(timerOffset, 2).PadLeft(4, '0') + "0";
                data.serialRstTxt[dataIdx] = Convert.ToString(timerOffset, 2).PadLeft(4, '0') + "0";
                data.midiMsg = new List<MidiMessage>() { MidiMessage.StopNote(laneIndex, velocity, channel + 1) };
                data.midiRstMsg = new List<MidiMessage>() { MidiMessage.StopNote(laneIndex, velocity, channel + 1) };
            }
            while (true)
            {
                if (playData.Count < 1)
                {
                    data.delay = delay;
                    currentDelay = delay;
                    pos = currentDelay;
                    playData.Add(data);
                    break;
                }
                else if (currentDelay + delay > pos)
                {
                    if (index + 1 >= playData.Count || currentDelay + delay < pos + playData[index + 1].delay)
                    {
                        currentDelay += delay;
                        data.delay = currentDelay - pos;
                        pos = currentDelay;
                        playData.Insert(++index, data);
                        if (index < playData.Count - 1)
                        {
                            data = playData[index + 1];
                            data.delay -= playData[index].delay;
                            playData[index + 1] = data;
                        }
                        break;
                    }
                    else
                    {
                        pos += playData[++index].delay;
                    }
                }
                else if (currentDelay + delay < pos)
                {
                    pos = delay;
                    currentDelay = delay;
                    //最初のデータを格納
                    data.delay = delay;
                    playData.Insert(index, data);
                    data = playData[index + 1];
                    data.delay -= playData[index].delay;
                    playData[index + 1] = data;
                    break;
                }
                else if (currentDelay + delay == pos)
                {
                    currentDelay = pos;
                    //delayが0の時はリストの前要素を編集
                    data.delay = playData[index].delay;
                    data.logTxt = playData[index].logTxt + data.logTxt;
                    data.serialTxt[dataIdx] = playData[index].serialTxt[dataIdx] + data.serialTxt[dataIdx];
                    data.serialRstTxt[dataIdx] = playData[index].serialRstTxt[dataIdx] + data.serialRstTxt[dataIdx];
                    data.playPart.InsertRange(0, playData[index].playPart);
                    data.midiMsg.InsertRange(0, playData[index].midiMsg);
                    data.midiRstMsg.InsertRange(0, playData[index].midiRstMsg);
                    playData[index] = data;
                    break;
                }
            }
            delay = 0;
            return;
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
            midi = new MidiOut(0);
            StopButton.Enabled = true;
            RepeatCheck.Enabled = false;
            RandomCheck.Enabled = false;
            LabelTime.Enabled = true;
            trackBar.Enabled = true;
            NextButton.Enabled = true;
            ReturnButton.Enabled = true;
            HeaderChunkAnalysis();
            SendSerial();
            allTime = playData.Sum(a => a.delay);
            trackBar.Maximum = (int)allTime;
            maxTime = TimeSpan.FromMilliseconds(allTime);
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
            midi.Close();
            midi.Dispose();
            string binTxt = "0000000010001000011001000010100110001110";
            if (serialPort1.IsOpen) serialPort1.Write(Convert.ToString(binTxt.Length, 2).PadLeft(8, '0') + binTxt);
            if(serialPort2.IsOpen) serialPort2.Write(Convert.ToString(binTxt.Length, 2).PadLeft(8, '0') + binTxt);
            StartButton.Enabled = true;
            LabelTime.Enabled = false;
            trackBar.Enabled = false;
            timePos = 0;
            trackBar.Value = 0;
            RepeatCheck.Enabled = true;
            RandomCheck.Enabled = true;
            //データをリセット
            tempoList.Clear();
            playData.Clear();
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
                "次に\"ファイル->ファイル（プレイリスト）を開く\"を選択して再生したいファイルを選択してください。\n\n" +
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