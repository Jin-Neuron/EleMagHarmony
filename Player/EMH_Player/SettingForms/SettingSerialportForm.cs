using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection;

namespace EMH_Player
{
    public partial class SettingSerialportForm : Form
    {
        public SettingSerialportForm()
        {
            InitializeComponent();
        }

        private void PortComboBox_DropDown(object sender, EventArgs e)
        {
            string[] PortList = SerialPort.GetPortNames();
            PortComboBox.Items.Clear();
            foreach (string Port in PortList)
            {
                PortComboBox.Items.Add(Port);
            }
            if (PortComboBox.Items.Count > 0)
            {
                PortComboBox.SelectedIndex = 0;
            }
        }

        private void SpeedComboBox_DropDown(object sender, EventArgs e)
        {
            // ボーレートを毎回取得して表示するために表示の度にリストをクリアする
            SpeedComboBox.Items.Clear();

            // ボーレートを出力する
            SpeedComboBox.Items.Add("115200"); //デフォルトなのでこれを最初にもってくる

            SpeedComboBox.Items.Add("9600");
            SpeedComboBox.Items.Add("14400");
            SpeedComboBox.Items.Add("19200");
            SpeedComboBox.Items.Add("28800");
            SpeedComboBox.Items.Add("38400");
            SpeedComboBox.Items.Add("57600");
            SpeedComboBox.Items.Add("76800");
            SpeedComboBox.Items.Add("153600");
            SpeedComboBox.Items.Add("230400");
            SpeedComboBox.Items.Add("460800");
        }

        private void DeviceComboBox_DropDown(object sender, EventArgs e)
        {
            DeviceComboBox.Items.Clear();
            DeviceComboBox.Items.Add("ElectricDevice");
            DeviceComboBox.Items.Add("GuitarDevice");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        public string GetPortName()
        {
            return PortComboBox.SelectedItem.ToString();
        }
        public int GetBaundRate()
        {
            return int.Parse(SpeedComboBox.SelectedItem.ToString());
        }
        public string GetDevice()
        {
            return DeviceComboBox.SelectedItem.ToString();
        }

        private void SettingSerialportForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}
