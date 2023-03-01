﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMH_Player
{
    public partial class DataClass
    {
        public enum Part
        {
            Melody,
            Guitar,
            Base,
            Drum
        };
        public enum Device
        {
            MotorSolenoid,
            Guitar,
            FloppyDiscDrive,
            Relay
        }
        //プレイリストかテストか
        public enum Status
        {
            PlaylistMode,
            TestMode,
        }
        //ノートの種類
        public enum NoteType
        {
            On,
            Off,
        }
        //テンポを格納する構造体
        public struct TempoData
        {
            public int eventTime;
            public float bpm;
        };
        public struct FileData
        {
            public string filePath, playlistPath, title;
            public int[] chCnt;
        }
        public struct PartData
        {
            public Part playPart;
            public Device playDevice;
            public int channel;
            public int[] timerIndex;
        }
        //ヘッダーチャンク解析用
        public struct HeaderData
        {
            public byte[] chunkID;
            public int dataLength;
            public short format, tracks, division;
            //トラックチャンク解析用
            public struct TrackData
            {
                public byte[] chunkID, data;
                public int dataLength;
            };
            public TrackData[] trackData;
        };
        //データ
        public struct MidiData
        {
            public double delay;
            public Part playPart;
            public string logTxt, serialTxt;
            public int txtLen;
        }
        public struct DelegateData
        {
            public delegate void LogTextDelegate(string text);
            public delegate void StartButtonDelegate();
            public delegate void StopButtonDelegate();
            public delegate void TrackBarDelegate();
            public delegate void LabelTimeDelegate();
        }
    }
}