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

namespace DEO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] PortList = SerialPort.GetPortNames();

            PortSelectRelay.Items.Clear();
            PortSelectGuitar.Items.Clear();
            foreach (string PortName in PortList)
            {
                PortSelectRelay.Items.Add(PortName);
                PortSelectGuitar.Items.Add(PortName);
            }
            if (PortSelectRelay.Items.Count > 0)
            {
                PortSelectRelay.SelectedIndex = 0;
            }
            if(PortSelectGuitar.Items.Count > 0)
            {
                PortSelectGuitar.SelectedIndex = 0;
            }
            StartButton.Enabled = false;
            StopButton.Enabled = false;
            NextButton.Enabled = false;
            ReturnButton.Enabled = false;
            OpenPlaylistButton.Enabled = false;
            OpenFileButton.Enabled = false;
            checkBoxPlaylist.Enabled = false;
            checkBoxTest.Enabled = false;
            RepeatCheck.Enabled = false;
            RandomCheck.Enabled = false;
        }

        private void RelayPortSelectButton_Click(object sender, EventArgs e)
        {

        }

        private void GuitarPortSelectButton2_Click(object sender, EventArgs e)
        {

        }
    }
}
