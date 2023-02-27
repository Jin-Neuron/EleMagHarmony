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

namespace DEO
{
    public partial class PlayerForm : Form
    {
        //変数宣言部
        private enum part
        {
            Melody,
            Guitar,
            Base,
            Drum
        };
        //プレイリストかテストか
        private enum Status
        {
            PlaylistMode,
            TestMode,
        }
        //ノートの種類
        private enum NoteType
        {
            On,
            Off,
        }
        //テンポを格納する構造体
        private struct TempoData
        {
            public int eventTime;
            public float bpm;
        };
        //データ
        private struct midiData
        {
            public double delay;
            public part playPart;
            public string logTxt;
            public int txtLen;
            public string serialTxt;
        }
        //ヘッダーチャンク解析用
        private struct HeaderChunkData
        {
            public byte[] chunkID;
            public int dataLength;
            public short format;
            public short tracks;
            public short division;
        };
        //トラックチャンク解析用
        private struct TrackChunkData
        {
            public byte[] chunkID;
            public int dataLength;
            public byte[] data;
        };
        private Status status;
        private string filePath = "", title = "";
        private int tempo = 1, timePos = 0, counter = 0, allTime = 0;
        private int[] chCount = new int[20];
        private delegate void LogTextDelegate(string text);
        private delegate void StartButtonDelegate();
        private delegate void StopButtonDelegate();
        private delegate void TrackBarDelegate();
        private delegate void LabelTimeDelegate();
        private bool isDistGuitar = false;
        private List<int> indexes = new List<int>();
        private List<string> files = new List<string>();
        private List<TempoData> tempoList = new List<TempoData>();
        private List<midiData> noteList = new List<midiData>();
        private HeaderChunkData headerChunk = new HeaderChunkData();
        private TimeSpan maxTime, changeTime;
        private System.Threading.Timer trackbar_tim;
        private System.Timers.Timer tim1 = new System.Timers.Timer();
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
            FileterBox.Visible = false;
            LogTextBox.Visible = false;
            this.MinimumSize = new Size(630, 310);
            this.MaximumSize = new Size(800, 310);
            LogTextBox.ScrollBars = ScrollBars.Vertical;
            LogTextBox.HideSelection = false;
            ElectricDevicePortLabel.Enabled = false;
            GuitarDevicePortLabel.Enabled = false;
            UiReset();
        }
        //スレッドタイマーのコールバックメソッド
        private void TimerCallBack(object state)
        {
            Invoke(new TrackBarDelegate(UpDateTrackBar));
            Invoke(new LabelTimeDelegate(UpDateLabelTime));
        }
        //各種シリアルポートの設定
        public void startSerial(string portName, int baundRate, string device)
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
            port.BaudRate = baundRate;
            port.PortName = portName;
            port.Open();
            label.Enabled = true;
            label.Text += portName;
            TestGuitarMenuItem.Enabled = true;
            trackbar_tim.Change(Timeout.Infinite, Timeout.Infinite);
            SetPlaylistModeMenuItem.Enabled = true;
            SetTestModeMenuItem.Enabled = true;
            if (filePath != "" || files.Count > 0)
            {
                StartButton.Enabled = true;
                NowPlaying.Enabled = true;
            }
        }
        //非同期処理によるギターとリレーの同時通信
        private void SendSerial()
        {
            if (serialPort1.IsOpen)
            {
                int i = 0;
                tim1 = new System.Timers.Timer();
                tim1.Interval = 1;
                Int64 lastTime = DateTime.Now.Ticks / 10000;
                tim1.Elapsed += (s, e) =>
                {
                    if ((DateTime.Now.Ticks / 10000 - lastTime) >= (Int64)noteList[i].delay)
                    {
                        tim1.Stop();
                        Invoke(new LogTextDelegate(WriteLogText), noteList[i].playPart.ToString() +  " : " + noteList[i].logTxt);
                        serialPort1.Write(Convert.ToString(noteList[i].txtLen, 2).PadLeft(8, '0') + noteList[i].serialTxt);
                        lastTime += (Int64)noteList[i].delay;
                        i++;
                        if (i < chCount[0])
                        {
                            try
                            {
                                tim1.Start();
                            }
                            catch { }
                        }
                        else
                        {
                            if (status == Status.PlaylistMode)
                            {
                                if (counter < files.Count)
                                {
                                    Invoke(new StartButtonDelegate(Stt));
                                }
                                else
                                {
                                    counter = 0;
                                    Invoke(new StopButtonDelegate(Stp));
                                }
                            }
                            else if (status == Status.TestMode)
                            {
                                counter = 0;
                                Invoke(new StopButtonDelegate(Stp));
                            }
                        }
                    }
                };
                tim1.Start();
            }
        }
        //以下別スレッドからのUI操作用メソッド(Invokeに呼び出される)
        private void WriteLogText(string text)
        {
            if (status == Status.TestMode)
            {
                LogTextBox.Text += text + "\r\n";
                LogTextBox.SelectionStart = LogTextBox.Text.Length;
                LogTextBox.Focus();
                LogTextBox.ScrollToCaret();
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
            Thread.Sleep(1000);
            trackbar_tim.Change(Timeout.Infinite, Timeout.Infinite);
            TaskCancel();
            counter++;
            title = "";
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
            tim1.Stop();
            tim1.Dispose();
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
            SetPlaylistModeMenuItem.Enabled = false;
            SetTestModeMenuItem.Enabled = false;
            RepeatCheck.Enabled = false;
            RandomCheck.Enabled = false;
            PlaylistLabel.Enabled = false;
            FileLabel.Enabled = false;
            NowPlaying.Enabled = false;
            TestMelodyMenuItem.Enabled = false;
            TestGuitarMenuItem.Enabled = false;
            TestBaseMenuItem.Enabled = false;
            TestDrumMenuItem.Enabled = false;
            LabelTime.Enabled = false;
        }
        //チェックボックスのイベント
        private void checkBoxPlaylist_CheckedChanged(object sender, EventArgs e)
        {
            if (!SetPlaylistModeMenuItem.Checked)
            {
                SetPlaylistModeMenuItem.Checked = true;
                SetTestModeMenuItem.Enabled = false;
                PlaylistLabel.Enabled = true;
                OpenPlaylistMenuItem.Enabled = true;
                if(files.Count > 0)
                {
                    status = Status.PlaylistMode;
                    StartButton.Enabled = true;
                    NowPlaying.Enabled = true;
                }
            }
            else
            {
                SetPlaylistModeMenuItem.Checked = false;
                SetTestModeMenuItem.Enabled = true;
                PlaylistLabel.Enabled = false;
                OpenPlaylistMenuItem.Enabled = false;
                StartButton.Enabled = false;
                NextButton.Enabled = false;
                ReturnButton.Enabled = false;
                RandomCheck.Enabled = false;
                RepeatCheck.Enabled = false;
                NowPlaying.Enabled = false;
            }
        }
        private void checkBoxTest_CheckedChanged(object sender, EventArgs e)
        {
            if (!SetTestModeMenuItem.Checked)
            {
                SetTestModeMenuItem.Checked = true;
                SetPlaylistModeMenuItem.Enabled = false;
                FileLabel.Enabled = true;
                OpenMidiFileMenuItem.Enabled = true;
                if (filePath != "")
                {
                    status = Status.TestMode;
                    StartButton.Enabled = true;
                    NowPlaying.Enabled = true;
                }
                if (this.Height < 450)
                {
                    this.Height = 450;
                }
                this.MinimumSize = new Size(630, 450);
                this.MaximumSize = new Size(800, 450);
                FileterBox.Visible = true;
                LogTextBox.Visible = true;
            }
            else
            {
                SetTestModeMenuItem.Checked = false;
                SetPlaylistModeMenuItem.Enabled = true;
                FileLabel.Enabled = false;
                OpenMidiFileMenuItem.Enabled = false;
                StartButton.Enabled = false;
                NextButton.Enabled = false;
                ReturnButton.Enabled = false;
                NowPlaying.Enabled = false;
                this.MinimumSize = new Size(630, 310);
                this.MaximumSize = new Size(800, 310);
                if (this.Height > 310)
                {
                    this.Height = 310;
                }
                FileterBox.Visible = false;
                LogTextBox.Visible = false;
            }
        }
        //ファイルを開くボタンのクリックイベント
        private void OpenPlaylistButton_Click(object sender, EventArgs e)
        {
            string playlistPath = "";
            if (this.Enabled)
            {
                openFileDialog.Filter = "プレイリストファイル(*.m3u) | *.m3u";
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK){
                    playlistPath = openFileDialog.FileName;
                    PlaylistLabel.Text = "MidiPlaylist : " + Path.GetFileNameWithoutExtension(playlistPath);
                    status = Status.PlaylistMode;
                    StartButton.Enabled = true;
                    NextButton.Enabled = true;
                    ReturnButton.Enabled = true;
                    RandomCheck.Enabled = true;
                    RepeatCheck.Enabled = true;
                    NowPlaying.Enabled = true;
                    FileEncoder(playlistPath);
                    if (RandomCheck.Checked)
                    {
                        IndexRandom();
                    }
                }
            }
        }
        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            if (this.Enabled)
            {
                openFileDialog.Filter = "midiファイル(*.mid)|*.mid";
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    FileLabel.Text = "MidiFile : " + Path.GetFileNameWithoutExtension(filePath);
                    status = Status.TestMode;
                    StartButton.Enabled = true;
                    NowPlaying.Enabled = true;
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
                        files.Add(Path.GetDirectoryName(playlist) + "\\" + line);
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
            string path = "";
            tempoList.Clear();
            //パスの取得
            if(status == Status.PlaylistMode)
            {
                if (RandomCheck.Checked)
                {
                    path = files[indexes[counter]];
                }
                else
                {
                    path = files[counter];
                }
            }
            else
            {
                path = filePath;
            }
            //ヘッダチャンクの解析
            using(FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
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
                    MessageBox.Show(Path.GetFileNameWithoutExtension(path) + "は対応していないフォーマットです。");
                    counter++;
                    if(status == Status.TestMode)
                    {
                        StopButton.PerformClick();
                    }
                    return;
                }
                TrackChunkData[] trackChunks = new TrackChunkData[headerChunk.tracks];
                //トラックチャンクの解析
                for(int i = 0; i < headerChunk.tracks; i++)
                {
                    trackChunks[i].chunkID = reader.ReadBytes(4);
                    if (BitConverter.IsLittleEndian)
                    {
                        byte[] byteArray = reader.ReadBytes(4);
                        Array.Reverse(byteArray);
                        trackChunks[i].dataLength = BitConverter.ToInt32(byteArray, 0);
                    }
                    else
                    {
                        trackChunks[i].dataLength = BitConverter.ToInt32(reader.ReadBytes(4), 0);
                    }
                    trackChunks[i].data = reader.ReadBytes(trackChunks[i].dataLength);
                    //各トラックデータについてイベントとデルタタイムの抽出
                    TrackDataAnalaysis(trackChunks[i].data);                    
                }
            }
        }
        //トラックチャンク解析イベント
        private void TrackDataAnalaysis(byte[] data)
        {
            uint currentTime = 0, preTime = 0, eventTime = 0;
            double delay = 0;
            byte statusByte = 0, channel = 0;
            bool[] longFlags = new bool[128];
            int laneIndex = 0;
            int i = 0;
            while(true)
            {
                uint deltaTime = 0;
                NoteType type = new NoteType { };
                while (true)
                {
                    //デルタタイムの抽出
                    byte tmp = data[i++];
                    deltaTime |= tmp & (uint)0x7f;
                    if ((tmp & 0x80) == 0) break;
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

                byte dataByte0, dataByte1;

                if (statusByte >= 0x80 && statusByte <= 0xef)
                {
                    channel = (byte)(statusByte & 0x0f);
                    switch (statusByte & 0xf0)
                    {
                        //ノートオフ
                        case 0x80:
                            //ノート番号
                            dataByte0 = data[i++];
                            //ヴェロシティ
                            dataByte1 = data[i++];
                            eventTime = currentTime - preTime;
                            preTime = currentTime;
                            if (longFlags[dataByte0])
                            {
                                laneIndex = dataByte0;
                                type = NoteType.Off;
                                longFlags[dataByte0] = false;
                            }
                            break;
                        //ノートオン
                        case 0x90:
                            dataByte0 = data[i++];
                            dataByte1 = data[i++];
                            laneIndex = dataByte0;
                            type = NoteType.On;
                            eventTime = currentTime - preTime;
                            preTime = currentTime;
                            longFlags[dataByte0] = true;
                            //ヴェロシティ:0はノーツオフ
                            if (dataByte1 == 0)
                            {
                                if (longFlags[dataByte0])
                                {
                                    type = NoteType.Off;
                                    longFlags[dataByte0] = false;
                                }
                            }
                            break;
                        //これ以降はインクリメント用
                        case 0xa0:
                            i += 2;
                            break;
                        case 0xb0:
                            i += 2;
                            break;
                        //音色設定
                        case 0xc0:
                            i += 1;
                            break;
                        case 0xd0:
                            i += 1;
                            break;
                        case 0xe0:
                            i += 2;
                            break;
                    }
                    if(laneIndex != 0)
                    {
                        //midiデータ格納処理
                        delay += storeNoteData(channel, eventTime, currentTime, laneIndex, type);
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
                            if (title == "")
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
                                title += Encoding.GetEncoding("Shift_JIS").GetString(bytes);
                                NowPlaying.Text = title;
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
                                TempoData tempoData = new TempoData();
                                tempoData.eventTime = (int)currentTime;
                                uint tempo = 0;
                                tempo |= data[i++];
                                tempo <<= 8;
                                tempo |= data[i++];
                                tempo <<= 8;
                                tempo |= data[i++];
                                tempoData.bpm = 60000000 / (float)tempo;
                                tempoData.bpm = (float)(Math.Floor(tempoData.bpm * 10) / 10);
                                tempoList.Add(tempoData);
                            }
                            break;
                    }
                }
                //ループ最後に残っているデータを格納
                if(i >= data.Length)
                {
                    if (laneIndex != 0)
                    {
                        //midiデータ格納処理
                        delay += storeNoteData(channel, eventTime, currentTime, laneIndex, type);
                    }
                    break;
                }
            }
            allTime = (allTime < delay) ? (int)delay : allTime;
        }
        private double storeNoteData(byte channel, uint eventTime, uint currentTime, int laneIndex, NoteType type)
        {
            float bpm = 0;
            int idx = 1, dataLen = 0;
            string text = "", binTxt = "";
            midiData data = new midiData();
            switch (channel)
            {
                case 1:
                    data.playPart = part.Melody; break;
                case 2:
                    data.playPart = part.Guitar; break;
                case 3:
                    data.playPart = part.Base; break;
                case 9:
                    data.playPart = part.Drum; break;
            }
            if (type == NoteType.On)
            {
                dataLen += 13;
                text += idx + ",ON," + laneIndex.ToString() + ",";
                binTxt += Convert.ToString(idx - 1, 2).PadLeft(4, '0');
                binTxt += "1";
                binTxt += Convert.ToString(laneIndex, 2).PadLeft(8, '0');
                idx++;
            }
            else if (type == NoteType.Off)
            {
                dataLen += 5;
                text += idx.ToString() + ",OFF,";
                binTxt += Convert.ToString(idx - 1, 2).PadLeft(4, '0');
                binTxt += "0";
                idx = 1;
            }
            data.logTxt = text;
            data.txtLen = dataLen;
            data.serialTxt = binTxt;
            if (eventTime != 0)
            {
                //bpmの抽出
                for (int j = tempoList.Count - 1; j >= 0; j--)
                {
                    if (currentTime >= tempoList[j].eventTime)
                    {
                        bpm = tempoList[j].bpm;
                        break;
                    }
                }
                data.delay = Math.Round(60000.0 * (double)eventTime / (double)bpm / (double)headerChunk.division);
                chCount[channel]++;
                noteList.Add(data);
                return data.delay;
            }
            else
            {
                data.delay = noteList[chCount[channel]-1].delay;
                data.txtLen += noteList[chCount[channel]-1].txtLen;
                data.logTxt = noteList[chCount[channel]-1].logTxt + data.logTxt;
                data.serialTxt = noteList[chCount[channel]-1].serialTxt + data.serialTxt;
                noteList[chCount[channel]-1] = data;
                return 0;
            }
        }
        //ランダムなインデックス作成（シャッフル時）
        private void IndexRandom()
        {
            indexes.Clear();
            for(int i = 0; i < files.Count; i++)
            {
                indexes.Add(i);
            }
            Random rnd = new Random();
            for(int Pos = 0; Pos < files.Count; Pos++)
            {
                int nextPos = rnd.Next(Pos, indexes.Count);
                int buff = indexes[Pos];
                indexes.Insert(Pos, indexes[nextPos]);
                indexes.RemoveAt(Pos + 1);
                indexes.Insert(nextPos, buff);
                indexes.RemoveAt(nextPos + 1);
            }
        }
        //以下各種音楽再生用ボタンクリックイベント
        private void StartButton_Click(object sender, EventArgs e)
        {
            if (this.Enabled)
            {
                timePos = 0;
                isDistGuitar = false;
                StartButton.Enabled = false;
                FileMenuItem.Enabled = false;
                SettingMenuItem.Enabled = false;
                PlayerMenuItem.Enabled = false;
                StopButton.Enabled = true;
                OpenPlaylistMenuItem.Enabled = false;
                OpenMidiFileMenuItem.Enabled = false;
                SetPlaylistModeMenuItem.Enabled = false;
                SetTestModeMenuItem.Enabled = false;
                FileLabel.Enabled = false;
                PlaylistLabel.Enabled = false;
                RepeatCheck.Enabled = false;
                RandomCheck.Enabled = false;
                LabelTime.Enabled = true;
                trackBar.Enabled = true;
                LogTextBox.Text = "";
                if(status == Status.PlaylistMode)
                {
                    NextButton.Enabled = true;
                    ReturnButton.Enabled = true;
                    if(counter >= files.Count)
                    {
                        counter = 0;
                        if(!RepeatCheck.Checked)
                        {
                            StopButton.PerformClick();
                            return;
                        }
                    }
                }
                try
                {
                    HeaderChunkAnalysis();
                    SendSerial();
                    trackBar.Maximum = (int)allTime;
                    maxTime = TimeSpan.FromMilliseconds((int)allTime);
                    changeTime = new TimeSpan(0, 0, 0);
                    LabelTime.Text = changeTime.ToString(@"mm\:ss") + "/" + maxTime.ToString(@"mm\:ss");
                    trackbar_tim.Change(0, 245);
                }
                catch {//例外を無視
                }
            }
        }
        private void StopButton_Click(object sender, EventArgs e)
        {
            try
            {
                trackbar_tim.Change(Timeout.Infinite, Timeout.Infinite);
                changeTime = new TimeSpan(0, 0, 0);
                TaskCancel();
                string text = "1,OFF,2,OFF,3,OFF,4,OFF,5,OFF,6,OFF,7,OFF,8,OFF,9,OFF,10,OFF,";
                string binTxt = "000110010000000010001000011001000";
                Invoke(new LogTextDelegate(WriteLogText), "piano send: " + text);
                serialPort1.Write(binTxt);
                StartButton.Enabled = true;
                FileMenuItem.Enabled = true;
                SettingMenuItem.Enabled = true;
                PlayerMenuItem.Enabled = true;
                StopButton.Enabled = false;
                LabelTime.Enabled = false;
                trackBar.Enabled = false;
                timePos = 0;
                trackBar.Value = 0;

                if (SetPlaylistModeMenuItem.Checked)
                {
                    OpenPlaylistMenuItem.Enabled = true;
                    PlaylistLabel.Enabled = true;
                    SetPlaylistModeMenuItem.Enabled = true;
                    RepeatCheck.Enabled = true;
                    RandomCheck.Enabled = true;
                }
                if (SetTestModeMenuItem.Checked)
                { 
                    OpenMidiFileMenuItem.Enabled = true;
                    FileLabel.Enabled = true;
                    SetTestModeMenuItem.Enabled = true;
                }
                counter = 0;
                title = "";
                tempoList.Clear();
                noteList.Clear();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void NextButton_Click(object sender, EventArgs e)
        {
            if (NextButton.Enabled)
            {
                if (serialPort1.IsOpen)
                {
                    string binTxt = "000110010000000010001000011001000";
                    serialPort1.Write(binTxt);
                }
                Thread.Sleep(500);
                trackbar_tim.Change(Timeout.Infinite, Timeout.Infinite);
                TaskCancel();
                StartButton.Enabled = true;
                counter++;
                title = "";
                StartButton.PerformClick();
            }
        }
        private void ReturnButton_Click(object sender, EventArgs e)
        {
            if (ReturnButton.Enabled)
            {
                string binTxt = "000110010000000010001000011001000";
                serialPort1.Write(binTxt);
                trackbar_tim.Change(Timeout.Infinite, Timeout.Infinite);
                TaskCancel();
                StartButton.Enabled = true;
                if (counter <= 1)
                {
                    counter = 0;
                }
                else
                {
                    counter--;
                }
                title = "";
                Thread.Sleep(500);
                StartButton.PerformClick();
            }
        }
        private void RandomCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (RandomCheck.Checked)
            {
                IndexRandom();
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

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void SettingSerialportMenuItem_Click(object sender, EventArgs e)
        {
            SettingSerialportForm form = new SettingSerialportForm();
            form.ShowDialog();
            try
            {
                if (!form.IsDisposed)
                {
                    startSerial(form.GetPortName(), form.GetBaundRate(), form.GetDevice());
                    form.Close();
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                SettingSerialportMenuItem.PerformClick();
            }
        }

        private void SettingDeviceMenuItem_Click(object sender, EventArgs e)
        {
            SettingDevicecsForm form = new SettingDevicecsForm();
            form.ShowDialog();
        }
    }
}