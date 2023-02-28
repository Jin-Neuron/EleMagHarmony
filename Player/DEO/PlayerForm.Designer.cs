namespace DEO
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
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.NextButton = new System.Windows.Forms.Button();
            this.StartButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.ReturnButton = new System.Windows.Forms.Button();
            this.NowPlaying = new System.Windows.Forms.TextBox();
            this.RepeatCheck = new System.Windows.Forms.CheckBox();
            this.RandomCheck = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.LogTextBox = new System.Windows.Forms.TextBox();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.LabelTime = new System.Windows.Forms.Label();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenMidiFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenPlaylistMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingSerialportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.midiチャネルCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingDeviceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PlayerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PlayModeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetFileModeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetPlaylistModeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TestPlayMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TestMelodyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TestGuitarMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TestBaseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TestDrumMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.HelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PartStatus = new System.Windows.Forms.StatusStrip();
            this.MelodyDeviceLabel = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.ShowLogCheckBox1 = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.PartStatus.SuspendLayout();
            this.PortFileStatus.SuspendLayout();
            this.FileterBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.Controls.Add(this.NextButton, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.StartButton, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.StopButton, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.ReturnButton, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(6, 169);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(566, 49);
            this.tableLayoutPanel4.TabIndex = 6;
            // 
            // NextButton
            // 
            this.NextButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NextButton.Font = new System.Drawing.Font("MS UI Gothic", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.NextButton.ForeColor = System.Drawing.Color.Blue;
            this.NextButton.Location = new System.Drawing.Point(426, 3);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(137, 43);
            this.NextButton.TabIndex = 3;
            this.NextButton.Text = "▶▶";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // StartButton
            // 
            this.StartButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StartButton.Font = new System.Drawing.Font("MS UI Gothic", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.StartButton.ForeColor = System.Drawing.Color.Green;
            this.StartButton.Location = new System.Drawing.Point(285, 3);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(135, 43);
            this.StartButton.TabIndex = 2;
            this.StartButton.Text = "▶";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StopButton.Font = new System.Drawing.Font("MS UI Gothic", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.StopButton.ForeColor = System.Drawing.Color.Red;
            this.StopButton.Location = new System.Drawing.Point(144, 3);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(135, 43);
            this.StopButton.TabIndex = 1;
            this.StopButton.Text = "■";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // ReturnButton
            // 
            this.ReturnButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReturnButton.Font = new System.Drawing.Font("MS UI Gothic", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ReturnButton.ForeColor = System.Drawing.Color.Blue;
            this.ReturnButton.Location = new System.Drawing.Point(3, 3);
            this.ReturnButton.Name = "ReturnButton";
            this.ReturnButton.Size = new System.Drawing.Size(135, 43);
            this.ReturnButton.TabIndex = 0;
            this.ReturnButton.Text = "◀◀";
            this.ReturnButton.UseVisualStyleBackColor = true;
            this.ReturnButton.Click += new System.EventHandler(this.ReturnButton_Click);
            // 
            // NowPlaying
            // 
            this.NowPlaying.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NowPlaying.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.NowPlaying.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.NowPlaying.ForeColor = System.Drawing.Color.SteelBlue;
            this.NowPlaying.Location = new System.Drawing.Point(12, 40);
            this.NowPlaying.Name = "NowPlaying";
            this.NowPlaying.ReadOnly = true;
            this.NowPlaying.Size = new System.Drawing.Size(560, 39);
            this.NowPlaying.TabIndex = 7;
            this.NowPlaying.Text = "NowPlaying";
            this.NowPlaying.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // RepeatCheck
            // 
            this.RepeatCheck.AutoSize = true;
            this.RepeatCheck.Dock = System.Windows.Forms.DockStyle.Right;
            this.RepeatCheck.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.RepeatCheck.ForeColor = System.Drawing.Color.Fuchsia;
            this.RepeatCheck.Location = new System.Drawing.Point(48, 3);
            this.RepeatCheck.Name = "RepeatCheck";
            this.RepeatCheck.Size = new System.Drawing.Size(67, 43);
            this.RepeatCheck.TabIndex = 8;
            this.RepeatCheck.Text = "🔁";
            this.RepeatCheck.UseVisualStyleBackColor = true;
            // 
            // RandomCheck
            // 
            this.RandomCheck.AutoSize = true;
            this.RandomCheck.Dock = System.Windows.Forms.DockStyle.Left;
            this.RandomCheck.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.RandomCheck.ForeColor = System.Drawing.Color.RoyalBlue;
            this.RandomCheck.Location = new System.Drawing.Point(121, 3);
            this.RandomCheck.Name = "RandomCheck";
            this.RandomCheck.Size = new System.Drawing.Size(67, 43);
            this.RandomCheck.TabIndex = 9;
            this.RandomCheck.Text = "🔀";
            this.RandomCheck.UseVisualStyleBackColor = true;
            this.RandomCheck.CheckedChanged += new System.EventHandler(this.RandomCheck_CheckedChanged);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.RepeatCheck, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.RandomCheck, 1, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(176, 114);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(237, 49);
            this.tableLayoutPanel5.TabIndex = 10;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "midiファイル(*.mid)|*.mid";
            // 
            // LogTextBox
            // 
            this.LogTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.LogTextBox.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LogTextBox.Location = new System.Drawing.Point(89, 221);
            this.LogTextBox.Multiline = true;
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.ReadOnly = true;
            this.LogTextBox.Size = new System.Drawing.Size(404, 137);
            this.LogTextBox.TabIndex = 12;
            // 
            // trackBar
            // 
            this.trackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar.Location = new System.Drawing.Point(12, 85);
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(560, 45);
            this.trackBar.TabIndex = 13;
            this.trackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar.Scroll += new System.EventHandler(this.trackBar_Scroll);
            // 
            // LabelTime
            // 
            this.LabelTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelTime.AutoSize = true;
            this.LabelTime.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LabelTime.Location = new System.Drawing.Point(444, 114);
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
            this.FileMenuItem.Name = "FileMenuItem";
            this.FileMenuItem.Size = new System.Drawing.Size(84, 20);
            this.FileMenuItem.Text = "ファイル（&F）";
            // 
            // OpenMidiFileMenuItem
            // 
            this.OpenMidiFileMenuItem.Name = "OpenMidiFileMenuItem";
            this.OpenMidiFileMenuItem.Size = new System.Drawing.Size(193, 22);
            this.OpenMidiFileMenuItem.Text = "midiファイルを開く(&M)";
            this.OpenMidiFileMenuItem.Click += new System.EventHandler(this.OpenFileButton_Click);
            // 
            // OpenPlaylistMenuItem
            // 
            this.OpenPlaylistMenuItem.Name = "OpenPlaylistMenuItem";
            this.OpenPlaylistMenuItem.Size = new System.Drawing.Size(193, 22);
            this.OpenPlaylistMenuItem.Text = "midiプレイリストを開く(&L)";
            this.OpenPlaylistMenuItem.Click += new System.EventHandler(this.OpenPlaylistButton_Click);
            // 
            // SettingMenuItem
            // 
            this.SettingMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SettingSerialportMenuItem,
            this.midiチャネルCToolStripMenuItem,
            this.SettingDeviceMenuItem});
            this.SettingMenuItem.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SettingMenuItem.Name = "SettingMenuItem";
            this.SettingMenuItem.Size = new System.Drawing.Size(74, 20);
            this.SettingMenuItem.Text = "設定（&S）";
            // 
            // SettingSerialportMenuItem
            // 
            this.SettingSerialportMenuItem.Name = "SettingSerialportMenuItem";
            this.SettingSerialportMenuItem.Size = new System.Drawing.Size(162, 22);
            this.SettingSerialportMenuItem.Text = "シリアルポート(&E)...";
            this.SettingSerialportMenuItem.Click += new System.EventHandler(this.SettingSerialportMenuItem_Click);
            // 
            // midiチャネルCToolStripMenuItem
            // 
            this.midiチャネルCToolStripMenuItem.Name = "midiチャネルCToolStripMenuItem";
            this.midiチャネルCToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.midiチャネルCToolStripMenuItem.Text = "チャネル(&C)...";
            // 
            // SettingDeviceMenuItem
            // 
            this.SettingDeviceMenuItem.Name = "SettingDeviceMenuItem";
            this.SettingDeviceMenuItem.Size = new System.Drawing.Size(162, 22);
            this.SettingDeviceMenuItem.Text = "デバイス(&D)...";
            this.SettingDeviceMenuItem.Click += new System.EventHandler(this.SettingDeviceMenuItem_Click);
            // 
            // PlayerMenuItem
            // 
            this.PlayerMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PlayModeMenuItem,
            this.TestPlayMenuItem});
            this.PlayerMenuItem.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PlayerMenuItem.Name = "PlayerMenuItem";
            this.PlayerMenuItem.Size = new System.Drawing.Size(79, 20);
            this.PlayerMenuItem.Text = "プレイヤー(&P)";
            // 
            // PlayModeMenuItem
            // 
            this.PlayModeMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SetFileModeMenuItem,
            this.SetPlaylistModeMenuItem});
            this.PlayModeMenuItem.Name = "PlayModeMenuItem";
            this.PlayModeMenuItem.Size = new System.Drawing.Size(180, 22);
            this.PlayModeMenuItem.Text = "プレイモード(&M)";
            // 
            // SetFileModeMenuItem
            // 
            this.SetFileModeMenuItem.Name = "SetFileModeMenuItem";
            this.SetFileModeMenuItem.Size = new System.Drawing.Size(180, 22);
            this.SetFileModeMenuItem.Text = "ファイル(&F)";
            this.SetFileModeMenuItem.Click += new System.EventHandler(this.checkBoxTest_CheckedChanged);
            // 
            // SetPlaylistModeMenuItem
            // 
            this.SetPlaylistModeMenuItem.Name = "SetPlaylistModeMenuItem";
            this.SetPlaylistModeMenuItem.Size = new System.Drawing.Size(180, 22);
            this.SetPlaylistModeMenuItem.Text = "プレイリスト(&L)";
            this.SetPlaylistModeMenuItem.Click += new System.EventHandler(this.checkBoxPlaylist_CheckedChanged);
            // 
            // TestPlayMenuItem
            // 
            this.TestPlayMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TestMelodyMenuItem,
            this.TestGuitarMenuItem,
            this.TestBaseMenuItem,
            this.TestDrumMenuItem});
            this.TestPlayMenuItem.Name = "TestPlayMenuItem";
            this.TestPlayMenuItem.Size = new System.Drawing.Size(180, 22);
            this.TestPlayMenuItem.Text = "テストプレイ(&T)";
            // 
            // TestMelodyMenuItem
            // 
            this.TestMelodyMenuItem.Name = "TestMelodyMenuItem";
            this.TestMelodyMenuItem.Size = new System.Drawing.Size(180, 22);
            this.TestMelodyMenuItem.Text = "メロディー(&V)";
            // 
            // TestGuitarMenuItem
            // 
            this.TestGuitarMenuItem.Name = "TestGuitarMenuItem";
            this.TestGuitarMenuItem.Size = new System.Drawing.Size(180, 22);
            this.TestGuitarMenuItem.Text = "ギター(&G)";
            // 
            // TestBaseMenuItem
            // 
            this.TestBaseMenuItem.Name = "TestBaseMenuItem";
            this.TestBaseMenuItem.Size = new System.Drawing.Size(180, 22);
            this.TestBaseMenuItem.Text = "ベース(&B)";
            // 
            // TestDrumMenuItem
            // 
            this.TestDrumMenuItem.Name = "TestDrumMenuItem";
            this.TestDrumMenuItem.Size = new System.Drawing.Size(180, 22);
            this.TestDrumMenuItem.Text = "ドラム(&D)";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.SettingMenuItem,
            this.PlayerMenuItem,
            this.HelpMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(584, 24);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // HelpMenuItem
            // 
            this.HelpMenuItem.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HelpMenuItem.Name = "HelpMenuItem";
            this.HelpMenuItem.Size = new System.Drawing.Size(65, 20);
            this.HelpMenuItem.Text = "ヘルプ(&H)";
            // 
            // PartStatus
            // 
            this.PartStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.PartStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MelodyDeviceLabel,
            this.toolStripStatusLabel2,
            this.BaseDeviceLabel,
            this.DrumDeviceLabel});
            this.PartStatus.Location = new System.Drawing.Point(0, 389);
            this.PartStatus.Name = "PartStatus";
            this.PartStatus.Size = new System.Drawing.Size(584, 22);
            this.PartStatus.TabIndex = 17;
            this.PartStatus.Text = "statusStrip1";
            // 
            // MelodyDeviceLabel
            // 
            this.MelodyDeviceLabel.Name = "MelodyDeviceLabel";
            this.MelodyDeviceLabel.Size = new System.Drawing.Size(189, 17);
            this.MelodyDeviceLabel.Spring = true;
            this.MelodyDeviceLabel.Text = "Melody : Motor & Solenoid [1]";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // BaseDeviceLabel
            // 
            this.BaseDeviceLabel.Name = "BaseDeviceLabel";
            this.BaseDeviceLabel.Size = new System.Drawing.Size(189, 17);
            this.BaseDeviceLabel.Spring = true;
            this.BaseDeviceLabel.Text = "Base : FloppyDrive[2..4]";
            // 
            // DrumDeviceLabel
            // 
            this.DrumDeviceLabel.Name = "DrumDeviceLabel";
            this.DrumDeviceLabel.Size = new System.Drawing.Size(189, 17);
            this.DrumDeviceLabel.Spring = true;
            this.DrumDeviceLabel.Text = "Drum : Relay[5..8]";
            // 
            // PortFileStatus
            // 
            this.PortFileStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.PortFileStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ElectricDevicePortLabel,
            this.GuitarDevicePortLabel,
            this.FileLabel,
            this.PlaylistLabel});
            this.PortFileStatus.Location = new System.Drawing.Point(0, 365);
            this.PortFileStatus.Name = "PortFileStatus";
            this.PortFileStatus.Size = new System.Drawing.Size(584, 24);
            this.PortFileStatus.SizingGrip = false;
            this.PortFileStatus.TabIndex = 18;
            this.PortFileStatus.Text = "statusStrip2";
            // 
            // ElectricDevicePortLabel
            // 
            this.ElectricDevicePortLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.ElectricDevicePortLabel.Name = "ElectricDevicePortLabel";
            this.ElectricDevicePortLabel.Size = new System.Drawing.Size(142, 19);
            this.ElectricDevicePortLabel.Spring = true;
            this.ElectricDevicePortLabel.Text = "ElectricDevicePort : ";
            // 
            // GuitarDevicePortLabel
            // 
            this.GuitarDevicePortLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.GuitarDevicePortLabel.Name = "GuitarDevicePortLabel";
            this.GuitarDevicePortLabel.Size = new System.Drawing.Size(142, 19);
            this.GuitarDevicePortLabel.Spring = true;
            this.GuitarDevicePortLabel.Text = "GuitarDevicePort : ";
            // 
            // FileLabel
            // 
            this.FileLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.FileLabel.Name = "FileLabel";
            this.FileLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.FileLabel.Size = new System.Drawing.Size(142, 19);
            this.FileLabel.Spring = true;
            this.FileLabel.Text = "MidiFile : ";
            // 
            // PlaylistLabel
            // 
            this.PlaylistLabel.Name = "PlaylistLabel";
            this.PlaylistLabel.Size = new System.Drawing.Size(142, 19);
            this.PlaylistLabel.Spring = true;
            this.PlaylistLabel.Text = "MidiPlaylist : ";
            // 
            // FileterBox
            // 
            this.FileterBox.Controls.Add(this.filterCheckBox);
            this.FileterBox.Location = new System.Drawing.Point(6, 241);
            this.FileterBox.Name = "FileterBox";
            this.FileterBox.Size = new System.Drawing.Size(77, 88);
            this.FileterBox.TabIndex = 19;
            this.FileterBox.TabStop = false;
            this.FileterBox.Text = "LogFileter";
            // 
            // filterCheckBox
            // 
            this.filterCheckBox.FormattingEnabled = true;
            this.filterCheckBox.Items.AddRange(new object[] {
            "Melody",
            "Guitar",
            "Base",
            "Drum"});
            this.filterCheckBox.Location = new System.Drawing.Point(3, 19);
            this.filterCheckBox.Name = "filterCheckBox";
            this.filterCheckBox.Size = new System.Drawing.Size(68, 60);
            this.filterCheckBox.TabIndex = 0;
            // 
            // ShowLogCheckBox1
            // 
            this.ShowLogCheckBox1.AutoSize = true;
            this.ShowLogCheckBox1.Location = new System.Drawing.Point(46, 144);
            this.ShowLogCheckBox1.Name = "ShowLogCheckBox1";
            this.ShowLogCheckBox1.Size = new System.Drawing.Size(75, 16);
            this.ShowLogCheckBox1.TabIndex = 20;
            this.ShowLogCheckBox1.Text = "ログを表示";
            this.ShowLogCheckBox1.UseVisualStyleBackColor = true;
            this.ShowLogCheckBox1.CheckedChanged += new System.EventHandler(this.ShowLogCheckBox1_CheckedChanged);
            // 
            // PlayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(584, 411);
            this.Controls.Add(this.ShowLogCheckBox1);
            this.Controls.Add(this.FileterBox);
            this.Controls.Add(this.PortFileStatus);
            this.Controls.Add(this.PartStatus);
            this.Controls.Add(this.tableLayoutPanel5);
            this.Controls.Add(this.LabelTime);
            this.Controls.Add(this.trackBar);
            this.Controls.Add(this.LogTextBox);
            this.Controls.Add(this.NowPlaying);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(800, 510);
            this.MinimumSize = new System.Drawing.Size(600, 310);
            this.Name = "PlayerForm";
            this.Text = "ElectricMusicPlayer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.PartStatus.ResumeLayout(false);
            this.PartStatus.PerformLayout();
            this.PortFileStatus.ResumeLayout(false);
            this.PortFileStatus.PerformLayout();
            this.FileterBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.IO.Ports.SerialPort serialPort1;
        private System.IO.Ports.SerialPort serialPort2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button ReturnButton;
        private System.Windows.Forms.TextBox NowPlaying;
        private System.Windows.Forms.CheckBox RepeatCheck;
        private System.Windows.Forms.CheckBox RandomCheck;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox LogTextBox;
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.Label LabelTime;
        private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenMidiFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenPlaylistMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SettingMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SettingSerialportMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SettingDeviceMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PlayerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PlayModeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SetFileModeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SetPlaylistModeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TestPlayMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TestMelodyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TestGuitarMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TestBaseMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TestDrumMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip PartStatus;
        private System.Windows.Forms.ToolStripStatusLabel MelodyDeviceLabel;
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
        private System.Windows.Forms.ToolStripMenuItem midiチャネルCToolStripMenuItem;
        private System.Windows.Forms.CheckBox ShowLogCheckBox1;
    }
}

