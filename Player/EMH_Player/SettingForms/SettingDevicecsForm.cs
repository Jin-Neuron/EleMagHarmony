using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static EMH_Player.PlayerForm;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace EMH_Player
{
    public partial class SettingDevicecsForm : Form
    {
        private DataClass.PartData[] partData;
        private List<string> checkBoxItem = new List<string>();
        private bool isReset = false;
        public SettingDevicecsForm(DataClass.PartData[] data)
        {
            InitializeComponent();
            this.partData = data;
        }

        private void SettingDevicecs_Load(object sender, EventArgs e)
        {
            TimerIndexListBox.Enabled = false;
            ItemClear();
            PlayPartComboBox.Items.Clear();
            PlayDeviceComboBox.Items.Clear();
            Array partArray = Enum.GetValues(typeof(DataClass.Part));
            Array deviceArray = Enum.GetValues(typeof(DataClass.Device));
            for (int i = 0; i < 4; i++)
            {
                if (i == 1) continue;
                PlayPartComboBox.Items.Add(partArray.GetValue(i).ToString());
                PlayDeviceComboBox.Items.Add(deviceArray.GetValue(i).ToString());
            }
        }
        private void ItemClear()
        {
            TimerIndexListBox.Items.Clear();
            checkBoxItem.Clear();
            for (int i = 0; i < partData.Length; i++)
            {
                if (i == 1) continue;
                for (int j = 0; j < partData[i].timerIndex.Length; j++)
                {
                    string str = "Tim" + partData[i].timerIndex[j] + "(" + partData[i].playDevice + ")"
                        + "[" + partData[i].playPart + "]";
                    TimerIndexListBox.Items.Add(str);
                    checkBoxItem.Add(str);
                }
            }
        }
        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ItemClear();
            string selectItem = ((ComboBox)sender).SelectedItem.ToString();
            int index = 0;
            foreach (string item in checkBoxItem)
            {
                if (item.IndexOf(selectItem) > -1) TimerIndexListBox.SetItemCheckState(index, CheckState.Checked);
                index++;
            }
        }
        private void SetButton_Click(object sender, EventArgs e)
        {
            isReset = false;
            this.Hide();
        }
        public DataClass.PartData[] GetData()
        {
            if (isReset)
            {
                return partData;
            }
            else if (TimerIndexListBox.CheckedItems.Count > 0 && PlayPartComboBox.SelectedIndex >= 0)
            {
                int partIdx = Array.FindIndex(partData, a => a.playPart.ToString() == PlayPartComboBox.SelectedItem.ToString());
                int deviceIdx = Array.FindIndex(partData, a => a.playDevice.ToString() == PlayDeviceComboBox.SelectedItem.ToString());
                DataClass.Part tmpPart = partData[partIdx].playPart;
                partData[partIdx].playPart = partData[deviceIdx].playPart;
                partData[deviceIdx].playPart = tmpPart;
                int tmpChannel = partData[partIdx].channel;
                partData[partIdx].channel = partData[deviceIdx].channel;
                partData[deviceIdx].channel = tmpChannel;
                //入れ替え後、メロディ、ギター、ベース、ドラムの順にソート
                SortPartData(new DataClass.Part[] { DataClass.Part.Melody, DataClass.Part.Guitar, DataClass.Part.Base, DataClass.Part.Drum });
                return partData;
            }
            else
            {
                MessageBox.Show("演奏パートとデバイスを選択してください。");
                return null;
            }
        }
        private void SortPartData(DataClass.Part[] order)
        {
            for(int i = 0; i < partData.Length; i++)
            {
                if (partData[i].playPart == order[i])
                {

                }
                else
                {
                    DataClass.PartData tmp = partData[i];
                    int orderIdx = Array.FindIndex(partData, a => a.playPart == order[i]);
                    partData[i] = partData[orderIdx];
                    partData[orderIdx] = tmp;
                }
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            PlayerForm form = new PlayerForm();
            form.PartReset(partData);
            ItemClear();
            isReset = true;
            this.Hide();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
