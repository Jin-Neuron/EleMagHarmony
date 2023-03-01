namespace EMH_Player
{
    partial class SettingDevicecsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingDevicecsForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.PlayDeviceComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ResetButton = new System.Windows.Forms.Button();
            this.SetButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CloseButton = new System.Windows.Forms.Button();
            this.PlayPartComboBox = new System.Windows.Forms.ComboBox();
            this.TimerIndexListBox = new System.Windows.Forms.CheckedListBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.PlayDeviceComboBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ResetButton, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.SetButton, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.CloseButton, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.PlayPartComboBox, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.TimerIndexListBox, 2, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(390, 227);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // PlayDeviceComboBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.PlayDeviceComboBox, 2);
            this.PlayDeviceComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlayDeviceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PlayDeviceComboBox.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PlayDeviceComboBox.FormattingEnabled = true;
            this.PlayDeviceComboBox.Items.AddRange(new object[] {
            "メロディ",
            "ベース",
            "ドラム"});
            this.PlayDeviceComboBox.Location = new System.Drawing.Point(197, 37);
            this.PlayDeviceComboBox.Name = "PlayDeviceComboBox";
            this.PlayDeviceComboBox.Size = new System.Drawing.Size(190, 27);
            this.PlayDeviceComboBox.TabIndex = 16;
            this.PlayDeviceComboBox.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label2, 2);
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(3, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 34);
            this.label2.TabIndex = 15;
            this.label2.Text = "デバイス";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.UseMnemonic = false;
            // 
            // ResetButton
            // 
            this.ResetButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResetButton.Location = new System.Drawing.Point(197, 195);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(91, 29);
            this.ResetButton.TabIndex = 14;
            this.ResetButton.Text = "デフォルトに戻す";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // SetButton
            // 
            this.SetButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SetButton.Location = new System.Drawing.Point(294, 195);
            this.SetButton.Name = "SetButton";
            this.SetButton.Size = new System.Drawing.Size(93, 29);
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
            this.label3.Location = new System.Drawing.Point(3, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(188, 124);
            this.label3.TabIndex = 10;
            this.label3.Text = "タイマー";
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
            this.label1.Size = new System.Drawing.Size(188, 34);
            this.label1.TabIndex = 5;
            this.label1.Text = "演奏パート";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.UseMnemonic = false;
            // 
            // CloseButton
            // 
            this.CloseButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CloseButton.Location = new System.Drawing.Point(3, 195);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(91, 29);
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
            this.PlayPartComboBox.Location = new System.Drawing.Point(197, 3);
            this.PlayPartComboBox.Name = "PlayPartComboBox";
            this.PlayPartComboBox.Size = new System.Drawing.Size(190, 27);
            this.PlayPartComboBox.TabIndex = 7;
            // 
            // TimerIndexListBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.TimerIndexListBox, 2);
            this.TimerIndexListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TimerIndexListBox.FormattingEnabled = true;
            this.TimerIndexListBox.Items.AddRange(new object[] {
            "Tim1(Motor&Solenoid)[Melody]",
            "Tim2(Relay)[Drum]",
            "Tim3(Relay)[Drum]",
            "Tim4(Relay)[Drum]",
            "Tim5(Relay)[Drum]",
            "Tim9(Floppy)[Base]",
            "Tim10(Floppy)[Base]",
            "Tim11(Floppy)[Base]"});
            this.TimerIndexListBox.Location = new System.Drawing.Point(197, 71);
            this.TimerIndexListBox.Name = "TimerIndexListBox";
            this.TimerIndexListBox.Size = new System.Drawing.Size(190, 118);
            this.TimerIndexListBox.TabIndex = 13;
            // 
            // SettingDevicecsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 251);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(430, 290);
            this.MinimumSize = new System.Drawing.Size(430, 290);
            this.Name = "SettingDevicecsForm";
            this.Text = "SettingDevicecs";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CloseButton_Click);
            this.Load += new System.EventHandler(this.SettingDevicecs_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox PlayPartComboBox;
        private System.Windows.Forms.CheckedListBox TimerIndexListBox;
        private System.Windows.Forms.Button SetButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.ComboBox PlayDeviceComboBox;
        private System.Windows.Forms.Label label2;
    }
}