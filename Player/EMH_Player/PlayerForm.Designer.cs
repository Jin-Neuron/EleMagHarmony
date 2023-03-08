namespace EMH_Player
{
    partial class PlayerForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerForm));
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.serialPort2 = new System.IO.Ports.SerialPort(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.LabelTime = new System.Windows.Forms.Label();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenMidiFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenPlaylistMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingSerialportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingDeviceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingChannelMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.HelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PartStatus = new System.Windows.Forms.StatusStrip();
            this.ElectricDeviceLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.BaseDeviceLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.DrumDeviceLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.PortFileStatus = new System.Windows.Forms.StatusStrip();
            this.ElectricDevicePortLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.GuitarDevicePortLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.FileLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.PlaylistLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.FileterBox = new System.Windows.Forms.GroupBox();
            this.filterCheckBox = new System.Windows.Forms.CheckedListBox();
            this.NowPlaying = new MetroFramework.Controls.MetroTextBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.NextButton = new System.Windows.Forms.PictureBox();
            this.PlayButton = new System.Windows.Forms.PictureBox();
            this.StopButton = new System.Windows.Forms.PictureBox();
            this.ReturnButton = new System.Windows.Forms.PictureBox();
            this.trackBar = new MetroFramework.Controls.MetroTrackBar();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ShuffleButton = new System.Windows.Forms.PictureBox();
            this.RepeatButton = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.PartStatus.SuspendLayout();
            this.PortFileStatus.SuspendLayout();
            this.FileterBox.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NextButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StopButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReturnButton)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ShuffleButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepeatButton)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "midiファイル(*.mid)|*.mid";
            // 
            // LabelTime
            // 
            this.LabelTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelTime.AutoSize = true;
            this.LabelTime.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LabelTime.ForeColor = System.Drawing.Color.White;
            this.LabelTime.Location = new System.Drawing.Point(457, 125);
            this.LabelTime.Name = "LabelTime";
            this.LabelTime.Size = new System.Drawing.Size(128, 24);
            this.LabelTime.TabIndex = 15;
            this.LabelTime.Text = "00:00/00:00";
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenMidiFileMenuItem,
            this.OpenPlaylistMenuItem});
            this.FileMenuItem.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FileMenuItem.ForeColor = System.Drawing.Color.White;
            this.FileMenuItem.Name = "FileMenuItem";
            this.FileMenuItem.Size = new System.Drawing.Size(84, 20);
            this.FileMenuItem.Text = "ファイル（&F）";
            // 
            // OpenMidiFileMenuItem
            // 
            this.OpenMidiFileMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(54)))), ((int)(((byte)(49)))));
            this.OpenMidiFileMenuItem.ForeColor = System.Drawing.Color.White;
            this.OpenMidiFileMenuItem.Name = "OpenMidiFileMenuItem";
            this.OpenMidiFileMenuItem.Size = new System.Drawing.Size(193, 22);
            this.OpenMidiFileMenuItem.Text = "midiファイルを開く(&M)";
            this.OpenMidiFileMenuItem.Click += new System.EventHandler(this.OpenFileMenuItem_Click);
            // 
            // OpenPlaylistMenuItem
            // 
            this.OpenPlaylistMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(54)))), ((int)(((byte)(49)))));
            this.OpenPlaylistMenuItem.ForeColor = System.Drawing.Color.White;
            this.OpenPlaylistMenuItem.Name = "OpenPlaylistMenuItem";
            this.OpenPlaylistMenuItem.Size = new System.Drawing.Size(193, 22);
            this.OpenPlaylistMenuItem.Text = "midiプレイリストを開く(&L)";
            this.OpenPlaylistMenuItem.Click += new System.EventHandler(this.OpenFileMenuItem_Click);
            // 
            // SettingMenuItem
            // 
            this.SettingMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SettingSerialportMenuItem,
            this.SettingDeviceMenuItem,
            this.SettingChannelMenuItem});
            this.SettingMenuItem.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SettingMenuItem.ForeColor = System.Drawing.Color.White;
            this.SettingMenuItem.Name = "SettingMenuItem";
            this.SettingMenuItem.Size = new System.Drawing.Size(74, 20);
            this.SettingMenuItem.Text = "設定（&S）";
            // 
            // SettingSerialportMenuItem
            // 
            this.SettingSerialportMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(54)))), ((int)(((byte)(49)))));
            this.SettingSerialportMenuItem.ForeColor = System.Drawing.Color.White;
            this.SettingSerialportMenuItem.Name = "SettingSerialportMenuItem";
            this.SettingSerialportMenuItem.Size = new System.Drawing.Size(171, 22);
            this.SettingSerialportMenuItem.Text = "シリアルポート(&E)...";
            this.SettingSerialportMenuItem.Click += new System.EventHandler(this.SettingSerialPortMenuItem_Click);
            // 
            // SettingDeviceMenuItem
            // 
            this.SettingDeviceMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(54)))), ((int)(((byte)(49)))));
            this.SettingDeviceMenuItem.ForeColor = System.Drawing.Color.White;
            this.SettingDeviceMenuItem.Name = "SettingDeviceMenuItem";
            this.SettingDeviceMenuItem.Size = new System.Drawing.Size(171, 22);
            this.SettingDeviceMenuItem.Text = "パートとデバイス(&P)...";
            this.SettingDeviceMenuItem.Click += new System.EventHandler(this.SettingDeviceMenuItem_Click);
            // 
            // SettingChannelMenuItem
            // 
            this.SettingChannelMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(54)))), ((int)(((byte)(49)))));
            this.SettingChannelMenuItem.ForeColor = System.Drawing.Color.White;
            this.SettingChannelMenuItem.Name = "SettingChannelMenuItem";
            this.SettingChannelMenuItem.Size = new System.Drawing.Size(171, 22);
            this.SettingChannelMenuItem.Text = "Midiチャネル(&C)...";
            this.SettingChannelMenuItem.Click += new System.EventHandler(this.SettingChannelMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(54)))), ((int)(((byte)(49)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.SettingMenuItem,
            this.HelpMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(600, 24);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // HelpMenuItem
            // 
            this.HelpMenuItem.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HelpMenuItem.ForeColor = System.Drawing.Color.White;
            this.HelpMenuItem.Name = "HelpMenuItem";
            this.HelpMenuItem.Size = new System.Drawing.Size(65, 20);
            this.HelpMenuItem.Text = "ヘルプ(&H)";
            this.HelpMenuItem.Click += new System.EventHandler(this.HelpMenuItem_Click);
            // 
            // PartStatus
            // 
            this.PartStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(54)))), ((int)(((byte)(49)))));
            this.PartStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ElectricDeviceLabel,
            this.toolStripStatusLabel2,
            this.BaseDeviceLabel,
            this.DrumDeviceLabel});
            this.PartStatus.Location = new System.Drawing.Point(0, 249);
            this.PartStatus.Name = "PartStatus";
            this.PartStatus.Size = new System.Drawing.Size(600, 22);
            this.PartStatus.TabIndex = 17;
            this.PartStatus.Text = "statusStrip1";
            // 
            // ElectricDeviceLabel
            // 
            this.ElectricDeviceLabel.ForeColor = System.Drawing.Color.White;
            this.ElectricDeviceLabel.Name = "ElectricDeviceLabel";
            this.ElectricDeviceLabel.Size = new System.Drawing.Size(195, 17);
            this.ElectricDeviceLabel.Spring = true;
            this.ElectricDeviceLabel.Text = "Melody : Motor &&Solenoid [1]";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // BaseDeviceLabel
            // 
            this.BaseDeviceLabel.ForeColor = System.Drawing.Color.White;
            this.BaseDeviceLabel.Name = "BaseDeviceLabel";
            this.BaseDeviceLabel.Size = new System.Drawing.Size(195, 17);
            this.BaseDeviceLabel.Spring = true;
            this.BaseDeviceLabel.Text = "Base : FloppyDrive[2..4]";
            // 
            // DrumDeviceLabel
            // 
            this.DrumDeviceLabel.ForeColor = System.Drawing.Color.White;
            this.DrumDeviceLabel.Name = "DrumDeviceLabel";
            this.DrumDeviceLabel.Size = new System.Drawing.Size(195, 17);
            this.DrumDeviceLabel.Spring = true;
            this.DrumDeviceLabel.Text = "Drum : Relay[5..8]";
            // 
            // PortFileStatus
            // 
            this.PortFileStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(75)))), ((int)(((byte)(66)))));
            this.PortFileStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ElectricDevicePortLabel,
            this.GuitarDevicePortLabel,
            this.FileLabel,
            this.PlaylistLabel});
            this.PortFileStatus.Location = new System.Drawing.Point(0, 225);
            this.PortFileStatus.Name = "PortFileStatus";
            this.PortFileStatus.Size = new System.Drawing.Size(600, 24);
            this.PortFileStatus.SizingGrip = false;
            this.PortFileStatus.TabIndex = 18;
            this.PortFileStatus.Text = "statusStrip2";
            // 
            // ElectricDevicePortLabel
            // 
            this.ElectricDevicePortLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.ElectricDevicePortLabel.ForeColor = System.Drawing.Color.White;
            this.ElectricDevicePortLabel.Name = "ElectricDevicePortLabel";
            this.ElectricDevicePortLabel.Size = new System.Drawing.Size(146, 19);
            this.ElectricDevicePortLabel.Spring = true;
            this.ElectricDevicePortLabel.Text = "ElectricDevicePort : ";
            // 
            // GuitarDevicePortLabel
            // 
            this.GuitarDevicePortLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.GuitarDevicePortLabel.ForeColor = System.Drawing.Color.White;
            this.GuitarDevicePortLabel.Name = "GuitarDevicePortLabel";
            this.GuitarDevicePortLabel.Size = new System.Drawing.Size(146, 19);
            this.GuitarDevicePortLabel.Spring = true;
            this.GuitarDevicePortLabel.Text = "GuitarDevicePort : ";
            // 
            // FileLabel
            // 
            this.FileLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.FileLabel.ForeColor = System.Drawing.Color.White;
            this.FileLabel.Name = "FileLabel";
            this.FileLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.FileLabel.Size = new System.Drawing.Size(146, 19);
            this.FileLabel.Spring = true;
            this.FileLabel.Text = "MidiFile : ";
            // 
            // PlaylistLabel
            // 
            this.PlaylistLabel.ForeColor = System.Drawing.Color.White;
            this.PlaylistLabel.Name = "PlaylistLabel";
            this.PlaylistLabel.Size = new System.Drawing.Size(146, 19);
            this.PlaylistLabel.Spring = true;
            this.PlaylistLabel.Text = "MidiPlaylist : ";
            // 
            // FileterBox
            // 
            this.FileterBox.Controls.Add(this.filterCheckBox);
            this.FileterBox.ForeColor = System.Drawing.Color.White;
            this.FileterBox.Location = new System.Drawing.Point(23, 125);
            this.FileterBox.Name = "FileterBox";
            this.FileterBox.Padding = new System.Windows.Forms.Padding(0);
            this.FileterBox.Size = new System.Drawing.Size(74, 86);
            this.FileterBox.TabIndex = 19;
            this.FileterBox.TabStop = false;
            this.FileterBox.Text = "Fileter";
            // 
            // filterCheckBox
            // 
            this.filterCheckBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(49)))), ((int)(((byte)(52)))));
            this.filterCheckBox.ForeColor = System.Drawing.Color.White;
            this.filterCheckBox.FormattingEnabled = true;
            this.filterCheckBox.Items.AddRange(new object[] {
            "Melody",
            "Guitar",
            "Base",
            "Drum"});
            this.filterCheckBox.Location = new System.Drawing.Point(3, 23);
            this.filterCheckBox.Name = "filterCheckBox";
            this.filterCheckBox.Size = new System.Drawing.Size(68, 60);
            this.filterCheckBox.TabIndex = 0;
            // 
            // NowPlaying
            // 
            this.NowPlaying.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.NowPlaying.CustomButton.Image = null;
            this.NowPlaying.CustomButton.Location = new System.Drawing.Point(541, 2);
            this.NowPlaying.CustomButton.Name = "";
            this.NowPlaying.CustomButton.Size = new System.Drawing.Size(29, 29);
            this.NowPlaying.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.NowPlaying.CustomButton.TabIndex = 1;
            this.NowPlaying.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.NowPlaying.CustomButton.UseSelectable = true;
            this.NowPlaying.CustomButton.Visible = false;
            this.NowPlaying.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.NowPlaying.ForeColor = System.Drawing.Color.Aquamarine;
            this.NowPlaying.Lines = new string[] {
        "NowPlaying"};
            this.NowPlaying.Location = new System.Drawing.Point(12, 40);
            this.NowPlaying.MaxLength = 32767;
            this.NowPlaying.Name = "NowPlaying";
            this.NowPlaying.PasswordChar = '\0';
            this.NowPlaying.ReadOnly = true;
            this.NowPlaying.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.NowPlaying.SelectedText = "";
            this.NowPlaying.SelectionLength = 0;
            this.NowPlaying.SelectionStart = 0;
            this.NowPlaying.ShortcutsEnabled = true;
            this.NowPlaying.Size = new System.Drawing.Size(573, 34);
            this.NowPlaying.Style = MetroFramework.MetroColorStyle.Teal;
            this.NowPlaying.TabIndex = 21;
            this.NowPlaying.Text = "NowPlaying";
            this.NowPlaying.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NowPlaying.UseCustomBackColor = true;
            this.NowPlaying.UseCustomForeColor = true;
            this.NowPlaying.UseSelectable = true;
            this.NowPlaying.UseStyleColors = true;
            this.NowPlaying.WaterMarkColor = System.Drawing.Color.Transparent;
            this.NowPlaying.WaterMarkFont = new System.Drawing.Font("MS UI Gothic", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.ColumnCount = 7;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.31606F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.067358F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.95337F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.326425F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.95337F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.067358F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.31606F));
            this.tableLayoutPanel4.Controls.Add(this.NextButton, 6, 0);
            this.tableLayoutPanel4.Controls.Add(this.PlayButton, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.StopButton, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.ReturnButton, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(130, 172);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(331, 46);
            this.tableLayoutPanel4.TabIndex = 6;
            // 
            // NextButton
            // 
            this.NextButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.NextButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NextButton.Image = ((System.Drawing.Image)(resources.GetObject("NextButton.Image")));
            this.NextButton.Location = new System.Drawing.Point(254, 3);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(74, 40);
            this.NextButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.NextButton.TabIndex = 29;
            this.NextButton.TabStop = false;
            this.NextButton.Click += new System.EventHandler(this.PlayerButton_Click);
            // 
            // PlayButton
            // 
            this.PlayButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PlayButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlayButton.Image = ((System.Drawing.Image)(resources.GetObject("PlayButton.Image")));
            this.PlayButton.Location = new System.Drawing.Point(182, 3);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(36, 40);
            this.PlayButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PlayButton.TabIndex = 28;
            this.PlayButton.TabStop = false;
            this.PlayButton.Click += new System.EventHandler(this.PlayerButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.StopButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StopButton.Image = ((System.Drawing.Image)(resources.GetObject("StopButton.Image")));
            this.StopButton.Location = new System.Drawing.Point(110, 3);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(36, 40);
            this.StopButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.StopButton.TabIndex = 27;
            this.StopButton.TabStop = false;
            this.StopButton.Click += new System.EventHandler(this.PlayerButton_Click);
            // 
            // ReturnButton
            // 
            this.ReturnButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ReturnButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReturnButton.Image = ((System.Drawing.Image)(resources.GetObject("ReturnButton.Image")));
            this.ReturnButton.Location = new System.Drawing.Point(3, 3);
            this.ReturnButton.Name = "ReturnButton";
            this.ReturnButton.Size = new System.Drawing.Size(71, 40);
            this.ReturnButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ReturnButton.TabIndex = 26;
            this.ReturnButton.TabStop = false;
            this.ReturnButton.Click += new System.EventHandler(this.PlayerButton_Click);
            // 
            // trackBar
            // 
            this.trackBar.BackColor = System.Drawing.Color.Transparent;
            this.trackBar.Location = new System.Drawing.Point(12, 80);
            this.trackBar.Margin = new System.Windows.Forms.Padding(0);
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(573, 45);
            this.trackBar.TabIndex = 25;
            this.trackBar.Text = "metroTrackBar1";
            this.trackBar.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.trackBar.UseCustomBackColor = true;
            this.trackBar.Value = 0;
            this.trackBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.trackBar_Scroll);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.Controls.Add(this.ShuffleButton, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.RepeatButton, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(240, 125);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(108, 41);
            this.tableLayoutPanel1.TabIndex = 24;
            // 
            // ShuffleButton
            // 
            this.ShuffleButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ShuffleButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ShuffleButton.Image = ((System.Drawing.Image)(resources.GetObject("ShuffleButton.Image")));
            this.ShuffleButton.Location = new System.Drawing.Point(61, 3);
            this.ShuffleButton.Name = "ShuffleButton";
            this.ShuffleButton.Size = new System.Drawing.Size(44, 35);
            this.ShuffleButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ShuffleButton.TabIndex = 29;
            this.ShuffleButton.TabStop = false;
            this.ShuffleButton.Click += new System.EventHandler(this.PlayControlButtonClick);
            // 
            // RepeatButton
            // 
            this.RepeatButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RepeatButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RepeatButton.Image = ((System.Drawing.Image)(resources.GetObject("RepeatButton.Image")));
            this.RepeatButton.Location = new System.Drawing.Point(3, 3);
            this.RepeatButton.Name = "RepeatButton";
            this.RepeatButton.Size = new System.Drawing.Size(42, 35);
            this.RepeatButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.RepeatButton.TabIndex = 28;
            this.RepeatButton.TabStop = false;
            this.RepeatButton.Click += new System.EventHandler(this.PlayControlButtonClick);
            // 
            // PlayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(14)))));
            this.ClientSize = new System.Drawing.Size(600, 271);
            this.Controls.Add(this.LabelTime);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.NowPlaying);
            this.Controls.Add(this.FileterBox);
            this.Controls.Add(this.PortFileStatus);
            this.Controls.Add(this.PartStatus);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.trackBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(800, 310);
            this.MinimumSize = new System.Drawing.Size(600, 310);
            this.Name = "PlayerForm";
            this.Text = "Ele Mag Player";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.PartStatus.ResumeLayout(false);
            this.PartStatus.PerformLayout();
            this.PortFileStatus.ResumeLayout(false);
            this.PortFileStatus.PerformLayout();
            this.FileterBox.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NextButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StopButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReturnButton)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ShuffleButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepeatButton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.IO.Ports.SerialPort serialPort1;
        private System.IO.Ports.SerialPort serialPort2;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label LabelTime;
        private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenMidiFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenPlaylistMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SettingMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SettingSerialportMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SettingDeviceMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip PartStatus;
        private System.Windows.Forms.ToolStripStatusLabel ElectricDeviceLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel BaseDeviceLabel;
        private System.Windows.Forms.ToolStripStatusLabel DrumDeviceLabel;
        private System.Windows.Forms.StatusStrip PortFileStatus;
        private System.Windows.Forms.ToolStripStatusLabel ElectricDevicePortLabel;
        private System.Windows.Forms.ToolStripStatusLabel GuitarDevicePortLabel;
        private System.Windows.Forms.ToolStripStatusLabel FileLabel;
        private System.Windows.Forms.ToolStripStatusLabel PlaylistLabel;
        private System.Windows.Forms.ToolStripMenuItem HelpMenuItem;
        private System.Windows.Forms.GroupBox FileterBox;
        private System.Windows.Forms.CheckedListBox filterCheckBox;
        private System.Windows.Forms.ToolStripMenuItem SettingChannelMenuItem;
        private MetroFramework.Controls.MetroTextBox NowPlaying;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private MetroFramework.Controls.MetroTrackBar trackBar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox ReturnButton;
        private System.Windows.Forms.PictureBox PlayButton;
        private System.Windows.Forms.PictureBox StopButton;
        private System.Windows.Forms.PictureBox NextButton;
        private System.Windows.Forms.PictureBox ShuffleButton;
        private System.Windows.Forms.PictureBox RepeatButton;
    }
}

