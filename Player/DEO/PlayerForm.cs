﻿using System;
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

namespace DEO
{
    public partial class PlayerForm : Form
    {
        //変数宣言部
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
        //ノート情報を格納する構造体
        private struct NoteData
        {
            public int eventTime;
            public int laneIndex;
            public NoteType type;
        };
        //テンポを格納する構造体
        private struct TempoData
        {
            public int eventTime;
            public float bpm;
        };
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
        private int harmony = 0;
        private int counter = 0;
        private Status status;
        private string filePath = "";
        private string title = "";
        private int tempo = 1;
        private int timePos = 0;
        private delegate void LogTextDelegate(string text);
        private delegate void StartButtonDelegate();
        private delegate void StopButtonDelegate();
        private delegate void TrackBarDelegate();
        private delegate void LabelTimeDelegate();
        private bool isDistGuitar = false;
        private List<int> indexes = new List<int>();
        private List<string> files = new List<string>();
        private List<NoteData> PianoNoteList = new List<NoteData>();
        private List<NoteData> GuitarNoteList = new List<NoteData>();
        private List<TempoData> tempoList = new List<TempoData>();
        private List<List<string>> texts = new List<List<string>>();
        private List<List<double>> delays = new List<List<double>>();
        private List<double> temp_delay = new List<double>();
        private List<List<string>> bins = new List<List<string>>();
        private HeaderChunkData headerChunk = new HeaderChunkData();
        private TimeSpan maxTime;
        private TimeSpan changeTime;
        private System.Threading.Timer trackbar_tim;
        private System.Timers.Timer tim1 = new System.Timers.Timer();
        private System.Timers.Timer tim2 = new System.Timers.Timer();
        private System.Timers.Timer tim3 = new System.Timers.Timer();
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
            LogTextBox.Visible = false;
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
            //配列を初期化
            temp_delay.Clear();
            delays.Clear();
            texts.Clear();
            bins.Clear();
            TempoControll();
            tempo = (int)tempoList[0].bpm;
            if (temp_delay.Count > 0)
            {
                int i = 0;
                tim1 = new System.Timers.Timer();
                tim1.Interval = 1;
                Int64 lastTime = DateTime.Now.Ticks / 10000;
                tim1.Elapsed += (s, e) =>
                {
                    if ((DateTime.Now.Ticks / 10000 - lastTime) >= (Int64)tempoList[i].eventTime)
                    {
                        tim1.Stop();
                        lastTime += (Int64)tempoList[i].bpm;
                        tempo = (int)tempoList[i].bpm;
                        Invoke(new LogTextDelegate(WriteLogText), "tempo = " + ((int)tempoList[i].bpm).ToString() + ",");
                        i++;
                        if (i < tempoList.Count)
                        {
                            tim1.Start();
                        }
                    }
                };
                tim1.Start();
            }
            if (serialPort1.IsOpen)
            {
                int i = 0;
                tim2 = new System.Timers.Timer();
                tim2.Interval = 1;
                StoreNoteData(serialPort1, PianoNoteList, "piano");
                Int64 lastTime = DateTime.Now.Ticks / 10000;
                tim2.Elapsed += (s, e) =>
                {
                    if ((DateTime.Now.Ticks / 10000 - lastTime) >= (Int64)delays[0][i])
                    {
                        tim2.Stop();
                        Invoke(new LogTextDelegate(WriteLogText), "piano send: " + texts[0][i]);
                        serialPort1.Write(bins[0][i]);
                        lastTime += (Int64)delays[0][i];
                        i++;
                        if (i < delays[0].Count)
                        {
                            try
                            {
                                tim2.Start();
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
                tim2.Start();
            }
        }
        //テンポ制御用メソッド
        private void TempoControll()
        {
            foreach(TempoData data in tempoList)
            {
                if (data.eventTime != 0)
                {
                    double delay = Math.Round(60000.0 / data.bpm * (double)data.eventTime / (double)headerChunk.division);
                    temp_delay.Add(delay);
                }
            }
        }
        //ノート情報制御用メソッド
        private void StoreNoteData(SerialPort serial, List<NoteData> notes, string str)
        {
            string text = "";
            string binTxt = "";
            int i = 0, j = 1;
            double delay = 0;
            int[] parts = new int[128];
            int tempoidx = 0;
            int tmpd = tempoList[tempoidx + 1].eventTime;
            int delay_all = 0;
            int dataLen = 0;
            int idx = (str == "guitar") ? 1 : 0;
            delays.Add(new List<double>());
            bins.Add(new List<string>());
            texts.Add(new List<string>());
            if (str == "guitar")
            {
                if (isDistGuitar)
                {
                    //serial.Write("1,");
                    Invoke(new LogTextDelegate(WriteLogText), "IsDistGuitar");
                }
                else
                {
                    //serial.Write("0,");
                }
            }
            foreach (NoteData data in notes)
            {
                //鳴らすパートを指定し、マイコンに送信
                if (parts[data.laneIndex] == 0)
                {
                    parts[data.laneIndex] = j;
                }
                if (data.type == NoteType.On)
                {
                    dataLen += 13;
                    text += parts[data.laneIndex].ToString() + ",ON," + data.laneIndex.ToString() + ",";
                    binTxt += Convert.ToString(parts[data.laneIndex] - 1, 2).PadLeft(4, '0');
                    binTxt += "1";
                    binTxt += Convert.ToString(data.laneIndex, 2).PadLeft(8,'0');
                }
                else if (data.type == NoteType.Off)
                {
                    dataLen += 5;
                    text += parts[data.laneIndex].ToString() + ",OFF,";
                    binTxt += Convert.ToString(parts[data.laneIndex] - 1, 2).PadLeft(4, '0');
                    binTxt += "0";
                    parts[data.laneIndex] = 0;
                }
                if (tempoidx < temp_delay.Count)
                {
                    if (delay_all >= tmpd)
                    {
                        tempoidx++;
                    }
                }
                if (data.eventTime != 0)
                {
                    delay = Math.Round(60000.0 * (double)data.eventTime / (double)tempoList[tempoidx].bpm / (double)headerChunk.division);
                }
                if (i < notes.Count - 1)
                {
                    NoteData nextData = notes[i + 1];
                    delay_all += nextData.eventTime;

                    if (nextData.type == NoteType.On)
                    {
                        List<int> partsNum = new List<int>();
                        for(int k = 0; k < 128; k++)
                        {
                            if(parts[k] > 0)
                            {
                                partsNum.Add(parts[k]);
                            }
                        }
                        partsNum.Sort();
                        if(partsNum.Count > 0)
                        {
                            if(partsNum.Count < partsNum.Max())
                            {
                                for(int k = 0; k < partsNum.Count - 1; k++)
                                {
                                    if (partsNum[k] + 1 != partsNum[k + 1])
                                    {
                                        j = k + 1;
                                        break;
                                    }
                                }
                            }else
                            {
                                j = partsNum.Max() + 1;
                            }
                        }
                        else
                        {
                            j = 1;
                        }
                    }else if(nextData.type == NoteType.Off)
                    {
                        j = parts[nextData.laneIndex];
                    }
                    if(nextData.eventTime != 0)
                    {/*
                        Invoke(new LogTextDelegate(WriteLogText), str + " send: " + text);
                        serial.Write(text);*/
                        delays[idx].Add(delay);
                        texts[idx].Add(text);
                        String dataLenStr = Convert.ToString(dataLen, 2).PadLeft(8, '0');
                        binTxt = dataLenStr + binTxt;
                        bins[idx].Add(binTxt);
                        text = "";
                        binTxt = "";
                        dataLen = 0;
                    }
                }
                else
                {
                    /*Invoke(new LogTextDelegate(WriteLogText), str + " send: " + text);
                    serial.Write(text);*/
                    delays[idx].Add(delay);
                    texts[idx].Add(text);
                    String dataLenStr = Convert.ToString(dataLen, 2).PadLeft(8, '0');
                    binTxt = dataLenStr + binTxt;
                    bins[idx].Add(binTxt);
                    text = "";
                    binTxt = "";
                }
                i += 1;
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
            tim2.Stop();
            tim2.Dispose();
            tim3.Stop();
            tim3.Dispose();
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
            PianoNoteList.Clear();
            GuitarNoteList.Clear();
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
            uint currentTime = 0;
            byte statusByte = 0;
            bool[] longFlags = new bool[128];

            for (int i = 0; i < data.Length;)
            {
                uint deltaTime = 0;
                while (true)
                {
                    //デルタタイムの抽出
                    byte tmp = data[i++];
                    deltaTime |= tmp & (uint)0x7f;
                    if ((tmp & 0x80) == 0) break;
                    deltaTime = deltaTime << 7;
                }
                currentTime = deltaTime;
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
                    switch (statusByte & 0xf0)
                    {
                        //ノートオフ
                        case 0x80:
                            dataByte0 = data[i++];
                            dataByte1 = data[i++];
                            if (longFlags[dataByte0])
                            {
                                NoteData note = new NoteData();
                                note.eventTime = (int)currentTime;
                                note.laneIndex = (int)dataByte0;
                                note.type = NoteType.Off;

                                if (harmony == 0x00)
                                {
                                    PianoNoteList.Add(note);
                                }else if(harmony == 0x1D || harmony == 0x1E){
                                    GuitarNoteList.Add(note);
                                }
                                longFlags[note.laneIndex] = false;
                            }
                            break;
                        //ノートオン
                        case 0x90:
                            dataByte0 = data[i++];
                            dataByte1 = data[i++];
                            {
                                NoteData note = new NoteData();
                                note.eventTime = (int)currentTime;
                                note.laneIndex = (int)dataByte0;
                                note.type = NoteType.On;
                                longFlags[note.laneIndex] = true;
                                if (dataByte1 == 0)
                                {
                                    if (longFlags[note.laneIndex])
                                    {
                                        note.type = NoteType.Off;
                                        longFlags[note.laneIndex] = false;
                                    }
                                }
                                switch (harmony)
                                {
                                    case 0x00:
                                        PianoNoteList.Add(note);
                                        break;
                                    case 0x1E:
                                        isDistGuitar = true;
                                        goto case 0x1D;
                                    case 0x1D:
                                        GuitarNoteList.Add(note);
                                        break;
                                }
                            }
                            break;
                        //これ以降はインクリメント用
                        case 0xa0:
                            i += 2;
                            break;
                        case 0xb0:
                            dataByte0 = data[i++];
                            dataByte1 = data[i++];
                            if (dataByte0 < 0x78)
                            {

                            }
                            else
                            {
                                switch (dataByte0)
                                {
                                    case 0x78:
                                    case 0x7a:
                                    case 0x7b:
                                    case 0x7c:
                                    case 0x7d:
                                    case 0x7e:
                                    case 0x7f:
                                        break;
                                }
                            }
                            break;
                        //音色設定
                        case 0xc0:
                            dataByte0 = data[i++];
                            harmony = dataByte0;
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
                    int musicLength = (int)delays[0].Sum();
                    trackBar.Maximum = musicLength;
                    maxTime = TimeSpan.FromMilliseconds(musicLength);
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
                PianoNoteList.Clear();
                tempoList.Clear();
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