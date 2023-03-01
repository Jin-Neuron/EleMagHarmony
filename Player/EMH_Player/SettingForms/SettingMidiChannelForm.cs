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

namespace EMH_Player
{
    public partial class SettingMidiChannelForm : Form
    {
        private DataClass.PartData[] partData;
        private List<string> checkBoxItem = new List<string>();
        private bool isReset = false;
        private int lastCheckedIndex = -1;
        public SettingMidiChannelForm(DataClass.PartData[] data)
        {
            InitializeComponent();
            this.partData = data;
        }

        private void SettingMidiChannelForm_Load(object sender, EventArgs e)
        {
            PlayPartComboBox.Items.Clear();
            ItemClear();
            Array partArray = Enum.GetValues(typeof(DataClass.Part));
            for (int i = 0; i < 4; i++)
            {
                PlayPartComboBox.Items.Add(partArray.GetValue(i).ToString());
            }
        }
        private void ItemClear()
        {
            ChannelIndexListBox.Items.Clear();
            checkBoxItem.Clear();
            for (int i = 0; i < 16; i++)
            {
                string str = "channel" + (i + 1).ToString();
                int partIdx = Array.FindIndex(partData, a => a.channel == i);
                if (partIdx > -1) str += "(" + partData[partIdx].playDevice + ")[" + partData[partIdx].playPart + "]";
                ChannelIndexListBox.Items.Add(str);
                checkBoxItem.Add(str);
            }
        }
        private void PlayPartComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectItem = ((ComboBox)sender).SelectedItem.ToString();
            int index = 0;
            foreach (string item in checkBoxItem)
            {
                if (item.IndexOf(selectItem) > -1) ChannelIndexListBox.SetItemCheckState(index, CheckState.Checked);
                index++;
            }
        }
        private void ChannelIndexListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.Index != lastCheckedIndex)
            {
                if (lastCheckedIndex != -1)
                    ((CheckedListBox)sender).SetItemCheckState(lastCheckedIndex, CheckState.Unchecked);
                lastCheckedIndex = e.Index;
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
            else if (ChannelIndexListBox.CheckedItems.Count > 0 && PlayPartComboBox.SelectedIndex >= 0)
            {
                int partIdx = Array.FindIndex(partData, a => a.playPart.ToString() == PlayPartComboBox.SelectedItem.ToString());
                int channelIdx = Array.FindIndex(partData, a => a.channel == lastCheckedIndex);
                if(channelIdx > -1)
                {
                    int tmp = partData[partIdx].channel;
                    partData[partIdx].channel = partData[channelIdx].channel;
                    partData[channelIdx].channel = tmp;
                }
                else
                {
                    partData[partIdx].channel = lastCheckedIndex;
                }
                return partData;
            }
            else
            {
                MessageBox.Show("演奏パートとチャネルを選択してください。");
                return null;
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            PlayerForm form = new PlayerForm();
            form.PartReset(partData);
            ItemClear();
            isReset = true;
            this.Hide();
        }
    }
}
