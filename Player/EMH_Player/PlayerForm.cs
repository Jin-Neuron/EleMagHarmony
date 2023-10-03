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
using MetroFramework.Controls;
using System.Windows.Media;
using System.Windows.Controls;

namespace EMH_Player
{
    public partial class PlayerForm : Form
    {
        //変数宣言部
        private static int fileCounter = 0, timIdx = 0;
        private static bool isRepeat = false, isShuffle = false;
        private static List<DataClass.FileData> fileList = new List<DataClass.FileData>();
        private static List<DataClass.TempoData> tempoList = new List<DataClass.TempoData>();
        private static List<DataClass.MidiData> playData = new List<DataClass.MidiData>();
        private static DataClass.PartData[] partList = new DataClass.PartData[4];
        private static DataClass.HeaderData headerChunk = new DataClass.HeaderData();
        private static TimeSpan maxTime, changeTime;
        private static DataClass.PlayStatus status = new DataClass.PlayStatus();
        private static TickTimer timer;
        private static double lastTime = 0, timeOffset = 0, startTime = 0, allTime = 0, timePos = 0;
        private static MidiOut midi;
        public PlayerForm()
        {
            InitializeComponent();
            ToolStripMenuItem checkMarginOnly = new ToolStripMenuItem("CheckMargin");
            (OpenMidiFileMenuItem.DropDown as ToolStripDropDownMenu).ShowCheckMargin = false;
            menuStrip1.Renderer = new MyRenderer();
            this.FormClosing += (s, e) =>
            {
                if (timer != null)
                    timer.Stop();
                if (midi != null)
                {
                    midi.Close();
                    midi.Dispose();
                }
            };
        }
        private class MyRenderer : ToolStripProfessionalRenderer
        {
            public MyRenderer() : base(new MyColors()) { }
        }

        private class MyColors : ProfessionalColorTable
        {
            //境界線関係
            public override System.Drawing.Color MenuBorder
            { get { return System.Drawing.Color.FromArgb(51, 54, 49); } }
            public override System.Drawing.Color MenuItemBorder
            { get { return System.Drawing.Color.FromArgb(51, 54, 49); } }
            public override System.Drawing.Color ToolStripBorder
            { get { return System.Drawing.Color.FromArgb(51, 54, 49); } }
            public override System.Drawing.Color ToolStripDropDownBackground
            { get { return System.Drawing.Color.FromArgb(71, 75, 66); } }

            //メニューアイテム選択時のbackground
            public override System.Drawing.Color MenuItemSelected
            { get { return System.Drawing.Color.FromArgb(71, 75, 66); } }
            public override System.Drawing.Color MenuItemSelectedGradientBegin
            { get { return System.Drawing.Color.FromArgb(91, 99, 86); } }
            public override System.Drawing.Color MenuItemSelectedGradientEnd
            { get { return System.Drawing.Color.FromArgb(91, 99, 86); } }
            public override System.Drawing.Color MenuItemPressedGradientBegin
            { get { return System.Drawing.Color.FromArgb(71, 75, 66); } }
            public override System.Drawing.Color MenuItemPressedGradientEnd
            { get { return System.Drawing.Color.FromArgb(71, 75, 66); } }

        }
        //フォーム１の初期設定
        private void Form1_Load(object sender, EventArgs e)
        {
            status = DataClass.PlayStatus.Stopping;
            PartReset(partList);
        }
        //partListをデフォルト値に設定する
        public void PartReset(DataClass.PartData[] data)
        {
            Array partArray = Enum.GetValues(typeof(DataClass.Part));
            Array deviceArray = Enum.GetValues(typeof(DataClass.Device));
            int[] timerNum = new int[4] { 1, 6, 1, 4 };
            int preIndex = 0;
            foreach (DataClass.Part part in partArray)
            {
                int index = (int)part;
                int[] timerIndex = new int[timerNum[index]];
                data[index].playPart = part;
                data[index].playDevice = (DataClass.Device)deviceArray.GetValue(index);
                data[index].channel = (index == 3) ? 9 : (index == 1) ? 3 : index;
                filterCheckBox.SetItemCheckState(index, CheckState.Checked);
                for (int i = 0; i < timerIndex.Length; i++)
                {
                    timerIndex[i] = preIndex + i;
                }
                preIndex += timerNum[index];
                data[index].timerIndex = timerIndex;
            }
        }
        //各種シリアルポートの設定
        public void StartSerial(string portName, int baudRate)
        {

            if (portName == serialPort1.PortName)
            {
                serialPort1.Close();
                EleMagPortLabel.Text = "EleMag Port : ";
            }
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                EleMagPortLabel.Text = "EleMag Port : ";
            }
            serialPort1.BaudRate = baudRate;
            serialPort1.PortName = portName;
            serialPort1.Open();
            EleMagPortLabel.Text += portName;
        }
        private void SendSerial(object status)
        {
            string[] serialData = new string[2] { "", "" };
            List<uint> msgs = new List<uint>();
            double currentTime = Math.Round(DateTime.Now.Ticks / 10000.0);
            if (playData == null || playData.Count < 1) return;
            if (timIdx < playData.Count)
            {
                timePos = currentTime - startTime + timeOffset;
                if (timePos - lastTime >= Math.Floor(playData[timIdx].delay))
                {
                    for(int i = 0; i < playData[timIdx].midiMsg.Count; i++)
                    {
                        int idx = (int)playData[timIdx].playPart[i];
                        if (filterCheckBox.GetItemChecked(idx))
                        {
                            if (serialPort1.IsOpen)
                                serialPort1.Write(playData[timIdx].serialData.ToArray(), 
                                    0, playData[timIdx].serialData.Count());
                            if (!muteCheckBox.Checked)
                                midi.Send(playData[timIdx].midiMsg[i].RawData);
                        }
                        else if (!filterCheckBox.GetItemChecked(idx))
                        {
                            if (serialPort1.IsOpen)
                                serialPort1.Write(playData[timIdx].serialRstData.ToArray(),
                                    0, playData[timIdx].serialRstData.Count());
                            if(muteCheckBox.Checked)
                                midi.Send(playData[timIdx].midiRstMsg[i].RawData);
                        }
                    }
                    timIdx++;
                    lastTime = timePos;
                }
                Invoke(new DataClass.DelegateData.TrackBarDelegate(UpDateTrackBar));
            }
            else
            {
                timer.Stop();
                Invoke(new DataClass.DelegateData.PlayerButtonDelegate(PlayCtl), NextButton);
            }

        }
        //Invoke用メソッド
        private void UpDateTrackBar()
        {
            changeTime = new TimeSpan(0, 0, 0, 0, (int)(timePos));
            if (timePos > trackBar.Maximum || timePos < trackBar.Minimum)
            {
                trackBar.Value = trackBar.Minimum;
                LabelTime.Text = maxTime.ToString(@"mm\:ss") + "/" + maxTime.ToString(@"mm\:ss");
            }
            else
            {
                trackBar.Value = (int)timePos;
                LabelTime.Text = changeTime.ToString(@"mm\:ss") + "/" + maxTime.ToString(@"mm\:ss");
            }
        }
        private void PlayCtl(object sender)
        {
            PlayerButton_Click(sender, EventArgs.Empty);
        }
        //ファイルメニューのクリックイベント
        private void OpenFileMenuItem_Click(object sender, EventArgs e)
        {
            fileList.Clear();
            ToolStripMenuItem[] menuItem = new ToolStripMenuItem[] {OpenMidiFileMenuItem, OpenMidiFileMenuItem};
            string ctlName = ((ToolStripMenuItem)sender).Name;
            openFileDialog.Filter = (ctlName == menuItem[0].Name) ? "midiファイル(*.mid)|*.mid" : "プレイリストファイル(*.m3u) | *.m3u";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
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
                    if (isShuffle)
                    {
                        IndexRandom();
                    }
                }
                playData.Clear();
                tempoList.Clear();
                HeaderChunkAnalysis();
                allTime = playData.Sum(a => a.delay);
                trackBar.Maximum = (int)Math.Round(allTime);
                maxTime = TimeSpan.FromMilliseconds(allTime);
                LabelTime.Text = changeTime.ToString(@"mm\:ss") + "/" + maxTime.ToString(@"mm\:ss");
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
            using( FileStream stream = new FileStream(fileList[fileCounter].filePath, FileMode.Open, FileAccess.Read))
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
                        PlayerButton_Click(StopButton, EventArgs.Empty);
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
            int[] partIndex = new int[12];
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
                    long tempoSum = tempoList.Sum(a => {
                        if (cnt++ <= j) return a.eventTime;
                        return 0;
                    });
                    long allSum = tempoList.Sum(a => a.eventTime);
                    if (currentTime >= currentTempoDelay
                        && currentTime + deltaTime <= tempoSum)
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
                            else
                            {
                                tmp += 60000.0 *
                                    ((currentTime < currentTempoDelay) ? currentTime + deltaTime - currentTempoDelay : deltaTime)
                                    / bpm / headerChunk.division;
                                j = tempoList.Count;
                                break;
                            }
                        }
                    }
                    else if (currentTime + deltaTime > allSum)
                    {
                        if (currentTime < allSum)
                        {
                            uint preSum = 0;
                            cnt = 0;
                            foreach(DataClass.TempoData t in tempoList)
                            {
                                if(preSum <= currentTime)
                                {
                                    cnt++;
                                    preSum += t.eventTime;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            if (preSum > currentTime)
                            {
                                tmp += 60000.0 * (preSum - currentTime) / tempoList[cnt - 2].bpm / headerChunk.division;
                            }
                            for (int k = cnt; k < tempoList.Count; k++)
                            {
                                tmp += 60000.0 * (tempoList[k].eventTime) / tempoList[k - 1].bpm / headerChunk.division;
                                preSum += tempoList[k].eventTime;
                            }
                            tmp += 60000.0 * (deltaTime + currentTime - allSum) / tempoList[tempoList.Count - 1].bpm / headerChunk.division;
                        }
                        else
                        {
                            tmp += 60000.0 * deltaTime / tempoList[tempoList.Count - 1].bpm / headerChunk.division;
                        }
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
                            int listIdx = Array.FindIndex(partList, a => a.channel == channel);
                            if (listIdx >= 0)
                                partList[listIdx].harmony = data[i++];
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
            data.serialData = new List<byte>();
            data.serialRstData = new List<byte>();
            data.playPart.Add(partList[listIdx].playPart);
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
                int timerOffset = partList[listIdx].timerIndex[idx - 1];
                data.logTxt = timerOffset.ToString() + ",ON," + laneIndex.ToString() + ",";
                data.serialData = new List<byte>() { (byte)(0x10 + timerOffset), laneIndex };
                data.serialRstData = new List<byte>() { (byte)(0x00  + timerOffset) };
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
                int timerOffset = partList[listIdx].timerIndex[idx - 1];
                data.logTxt += timerOffset.ToString() + ",OFF,";
                data.serialData = new List<byte>() { (byte)(0x00 + timerOffset) };
                data.serialRstData = new List<byte>() { (byte)(0x00 + timerOffset) };
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
                    data.playPart.InsertRange(0, playData[index].playPart);
                    if (playData[index].serialData != null)
                    {
                        if (data.serialData != null)
                        {
                            data.serialData.InsertRange(0, playData[index].serialData);
                            data.serialRstData.InsertRange(0, playData[index].serialRstData);
                        }
                        else
                        {
                            data.serialData = playData[index].serialData;
                            data.serialRstData = playData[index].serialRstData;
                        }
                    }
                    data.midiMsg.InsertRange(0, playData[index].midiMsg);
                    data.midiRstMsg.InsertRange(0, playData[index].midiRstMsg);
                    playData[index] = data;
                    break;
                }
            }
            delay = 0;
            return;
        }
        //timePosの位置からtimIdxを検索する
        private void FindTimeIdx()
        {
            double del = allTime;
            if (playData.Count > 0)
            {
                for (timIdx = playData.Count - 1; timIdx >= 1; timIdx--)
                {
                    del -= playData[timIdx].delay;
                    if (timePos >= del)
                    {
                        timIdx++;
                        break;
                    }
                }
                lastTime = (del > playData[0].delay) ? del : 0;
            }
            else
            {
                timIdx = 0;
            }
        }
        //midiとタイマーをスタートさせる
        private void StartTimer()
        {
            midi = new MidiOut(0);
            for (int i = 0; i < partList.Count(); i++)
                midi.Send(MidiMessage.ChangePatch(partList[i].harmony, partList[i].channel + 1).RawData);
            timer = new TickTimer(SendSerial, 1);
            startTime = Math.Round(DateTime.Now.Ticks / 10000.0);
            timer.Start();
        }
        //midiとタイマーをストップする
        private void StopTimer()
        {
            if (timer != null)
                timer.Stop();
            if (midi != null)
            {
                midi.Close();
                midi.Dispose();
            }
            timeOffset = timePos;
            byte[] rstBin = new byte[12] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0a, 0x0b };
            if (serialPort1.IsOpen) serialPort1.Write(rstBin, 0, 8);
        }
        //midiのデータと時間のラベル再セットする
        private void ResetData()
        {
            playData.Clear();
            tempoList.Clear();
            HeaderChunkAnalysis();
            allTime = playData.Sum(a => a.delay);
            trackBar.Maximum = (int)Math.Round(allTime);
            maxTime = TimeSpan.FromMilliseconds(allTime);
            LabelTime.Text = changeTime.ToString(@"mm\:ss") + "/" + maxTime.ToString(@"mm\:ss");
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
        private void PlayerButton_Click(object sender, EventArgs e)
        {
            sender = (PictureBox)sender;
            if(sender != PlayButton)
            {
                PlayButton.Image = System.Drawing.Image.FromFile(@".\IMG\play_icon.png");
                menuStrip1.Enabled = true;
                changeTime = new TimeSpan(0, 0, 0);
                StopTimer();
                //データをリセット
                timePos = 0;
                timIdx = 0;
                trackBar.Value = 0;
                startTime = 0;
                timeOffset = 0;
                lastTime = 0;
                for (int i = 0; i < fileList.Count; i++)
                {
                    DataClass.FileData data = new DataClass.FileData();
                    data.playlistPath = fileList[i].playlistPath;
                    data.filePath = fileList[i].filePath;
                    data.title = "";
                    fileList[i] = data;
                }
                changeTime = new TimeSpan(0, 0, 0);
                status = DataClass.PlayStatus.Stopping;
            }
            if(sender != StopButton)
            {
                if (sender != PlayButton)
                {
                    fileCounter = (sender == NextButton) ? (fileCounter + 1 >= fileList.Count) ? 0 : fileCounter + 1 
                                                            : (fileCounter <= 1) ? 0 : fileCounter - 1;
                    ResetData();
                    if (fileCounter == 0 && sender == NextButton && !isRepeat)
                        return;
                }
                if (status != DataClass.PlayStatus.Playing)
                {
                    PlayButton.Image = System.Drawing.Image.FromFile(@".\IMG\pause_icon.png");
                    menuStrip1.Enabled = false;
                    StartTimer();
                    status = DataClass.PlayStatus.Playing;
                }
                else if(status == DataClass.PlayStatus.Playing)
                {
                    PlayButton.Image = System.Drawing.Image.FromFile(@".\IMG\play_icon.png");
                    menuStrip1.Enabled = true;
                    StopTimer();
                    status = DataClass.PlayStatus.Pausing;
                }
            }
        }

        private void muteCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (muteCheckBox.Checked)
            {
                midi.Close();
                midi.Dispose();
                midi = new MidiOut(0);
                for (int i = 0; i < partList.Count(); i++)
                    midi.Send(MidiMessage.ChangePatch(partList[i].harmony, partList[i].channel + 1).RawData);
            }
        }

        private void PlayControlButtonClick(object sender, EventArgs e)
        {
            if((PictureBox)sender == RepeatButton)
            {
                isRepeat = !isRepeat;
                if (isRepeat)
                    RepeatButton.Image = System.Drawing.Image.FromFile(@".\IMG\repeat_icon_enable.png");
                else
                    RepeatButton.Image = System.Drawing.Image.FromFile(@".\IMG\repeat_icon_disable.png");
            }
            if((PictureBox)sender == ShuffleButton)
            {
                isShuffle = !isShuffle;
                if (isShuffle)
                {
                    ShuffleButton.Image = System.Drawing.Image.FromFile(@".\IMG\random_icon_enable.png");
                    IndexRandom();
                }
                else
                {
                    if (fileList.Count > 0 && fileList[0].playlistPath != null)
                    {
                        string playlist = fileList[0].playlistPath;
                        fileList.Clear();
                        FileEncoder(playlist);
                    }
                    ShuffleButton.Image = System.Drawing.Image.FromFile(@".\IMG\random_icon_disable.png");
                }
            }
        }

        private void HelpMenuItem_Click(object sender, EventArgs e)
        {
            string helpStr = "再生を開始するにはまず、\"設定->シリアルポート\"より各デバイスのシリアルポートを設定してください。\n" +
                "次に\"ファイル->ファイル（プレイリスト）を開く\"を選択して再生したいファイルを選択してください。\n\n" +
                "パートごとのMidiチャネルやデバイスの詳細を設定したい場合は、設定メニューを参照してください。";
            MessageBox.Show(helpStr);
        }
        private void trackBar_MouseDown(object sender, MouseEventArgs e)
        {
            StopTimer();
        }

        private void trackBar_MouseUp(object sender, MouseEventArgs e)
        {
            FindTimeIdx();
            if (status == DataClass.PlayStatus.Playing)
            {
                StartTimer();
            }
        }
        private void trackBar_Scroll(object sender, EventArgs e)
        {
            timePos -= timeOffset;
            timeOffset = trackBar.Value - timePos;
            timePos += timeOffset;
            changeTime = new TimeSpan(0, 0, 0, 0, (int)timePos);
            if (changeTime < maxTime)
            {
                LabelTime.Text = changeTime.ToString(@"mm\:ss") + "/" + maxTime.ToString(@"mm\:ss");
            }
            else
            {
                LabelTime.Text = maxTime.ToString(@"mm\:ss") + "/" + maxTime.ToString(@"mm\:ss");
            }
        }
        private void SettingSerialPortMenuItem_Click(object sender, EventArgs e)
        {
            SettingSerialportForm form = new SettingSerialportForm();
            form.ShowDialog();
            try
            {
                if (!form.IsDisposed)
                {
                    StartSerial(form.GetPortName(), form.GetBaudRate());
                    form.Close();
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                SettingSerialportMenuItem.PerformClick();
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
                        MessageBox.Show("チャネルを設定しました。");
                        if(playData.Count() > 0)
                        {
                            ResetData();
                            FindTimeIdx();
                        }
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