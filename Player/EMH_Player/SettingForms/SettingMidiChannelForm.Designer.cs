namespace EMH_Player
{
    partial class SettingMidiChannelForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingMidiChannelForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ResetButton = new System.Windows.Forms.Button();
            this.SetButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CloseButton = new System.Windows.Forms.Button();
            this.PlayPartComboBox = new System.Windows.Forms.ComboBox();
            this.ChannelIndexListBox = new System.Windows.Forms.CheckedListBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Controls.Add(this.ResetButton, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.SetButton, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.CloseButton, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.PlayPartComboBox, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.ChannelIndexListBox, 2, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(390, 187);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // ResetButton
            // 
            this.ResetButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResetButton.Location = new System.Drawing.Point(159, 152);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(111, 32);
            this.ResetButton.TabIndex = 14;
            this.ResetButton.Text = "デフォルトに戻す";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // SetButton
            // 
            this.SetButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SetButton.Location = new System.Drawing.Point(276, 152);
            this.SetButton.Name = "SetButton";
            this.SetButton.Size = new System.Drawing.Size(111, 32);
            this.SetButton.TabIndex = 12;
            this.SetButton.Text = "設定";
            this.SetButton.UseVisualStyleBackColor = true;
            this.SetButton.Click += new System.EventHandler(this.SetButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label3, 2);
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(3, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 112);
            this.label3.TabIndex = 10;
            this.label3.Text = "Midiチャネル";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 2);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 37);
            this.label1.TabIndex = 5;
            this.label1.Text = "演奏パート";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.UseMnemonic = false;
            // 
            // CloseButton
            // 
            this.CloseButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CloseButton.Location = new System.Drawing.Point(3, 152);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(72, 32);
            this.CloseButton.TabIndex = 6;
            this.CloseButton.Text = "閉じる";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // PlayPartComboBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.PlayPartComboBox, 2);
            this.PlayPartComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlayPartComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PlayPartComboBox.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PlayPartComboBox.FormattingEnabled = true;
            this.PlayPartComboBox.Items.AddRange(new object[] {
            "メロディ",
            "ベース",
            "ドラム"});
            this.PlayPartComboBox.Location = new System.Drawing.Point(159, 3);
            this.PlayPartComboBox.Name = "PlayPartComboBox";
            this.PlayPartComboBox.Size = new System.Drawing.Size(228, 27);
            this.PlayPartComboBox.TabIndex = 7;
            this.PlayPartComboBox.SelectedIndexChanged += new System.EventHandler(this.PlayPartComboBox_SelectedIndexChanged);
            // 
            // ChannelIndexListBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.ChannelIndexListBox, 2);
            this.ChannelIndexListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChannelIndexListBox.FormattingEnabled = true;
            this.ChannelIndexListBox.Items.AddRange(new object[] {
            "Channel1(MotorSolenoid)[Melody]",
            "Channel2(Guitar)[Guitar]",
            "Channel3(FloppyDiscDrive)[Base]",
            "Channel4",
            "Channel5",
            "Channel6",
            "Channel7",
            "Channel8",
            "Channel9(Relay)[Drum]",
            "Channel10",
            "Channel11",
            "Channel12",
            "Channel13",
            "Channel14",
            "Channel15",
            "Channel16"});
            this.ChannelIndexListBox.Location = new System.Drawing.Point(159, 40);
            this.ChannelIndexListBox.Name = "ChannelIndexListBox";
            this.ChannelIndexListBox.Size = new System.Drawing.Size(228, 106);
            this.ChannelIndexListBox.TabIndex = 13;
            this.ChannelIndexListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ChannelIndexListBox_ItemCheck);
            // 
            // SettingMidiChannelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 211);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(430, 250);
            this.MinimumSize = new System.Drawing.Size(430, 250);
            this.Name = "SettingMidiChannelForm";
            this.Text = "SettingMidiChannelForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CloseButton_Click);
            this.Load += new System.EventHandler(this.SettingMidiChannelForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Button SetButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.ComboBox PlayPartComboBox;
        private System.Windows.Forms.CheckedListBox ChannelIndexListBox;
    }
}