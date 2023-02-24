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
            this.File = new System.Windows.Forms.Label();
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
            this.OpenFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenMidiFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenPlaylistMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingSerialportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingDeviceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PlayerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PlayModeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetTestModeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetPlaylistModeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TestPlayMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TestMelodyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TestGuitarMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TestBaseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TestDrumMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ElectricDevicePortLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.GuitarDevicePortLabel = new System.Windows.Forms.Label();
            this.FileLabel = new System.Windows.Forms.Label();
            this.PlaylistLabel = new System.Windows.Forms.Label();
            this.Playlist = new System.Windows.Forms.Label();
            this.ElectricDevicePort = new System.Windows.Forms.Label();
            this.GuitarDevicePort = new System.Windows.Forms.Label();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // File
            // 
            this.File.AutoSize = true;
            this.File.Dock = System.Windows.Forms.DockStyle.Fill;
            this.File.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.File.ForeColor = System.Drawing.Color.Green;
            this.File.Location = new System.Drawing.Point(283, 72);
            this.File.Name = "File";
            this.File.Size = new System.Drawing.Size(274, 36);
            this.File.TabIndex = 1;
            this.File.Text = "fileName";
            this.File.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.tableLayoutPanel4.Location = new System.Drawing.Point(6, 333);
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
            this.NowPlaying.Location = new System.Drawing.Point(12, 204);
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
            this.tableLayoutPanel5.Location = new System.Drawing.Point(170, 278);
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
            this.LogTextBox.Location = new System.Drawing.Point(62, 388);
            this.LogTextBox.Multiline = true;
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.ReadOnly = true;
            this.LogTextBox.Size = new System.Drawing.Size(436, 137);
            this.LogTextBox.TabIndex = 12;
            // 
            // trackBar
            // 
            this.trackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar.Location = new System.Drawing.Point(12, 249);
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
            this.LabelTime.Location = new System.Drawing.Point(444, 281);
            this.LabelTime.Name = "LabelTime";
            this.LabelTime.Size = new System.Drawing.Size(128, 24);
            this.LabelTime.TabIndex = 15;
            this.LabelTime.Text = "00:00/00:00";
            // 
            // OpenFileMenuItem
            // 
            this.OpenFileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenMidiFileMenuItem,
            this.OpenPlaylistMenuItem});
            this.OpenFileMenuItem.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OpenFileMenuItem.Name = "OpenFileMenuItem";
            this.OpenFileMenuItem.Size = new System.Drawing.Size(84, 20);
            this.OpenFileMenuItem.Text = "ファイル（&F）";
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
            this.SettingDeviceMenuItem});
            this.SettingMenuItem.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SettingMenuItem.Name = "SettingMenuItem";
            this.SettingMenuItem.Size = new System.Drawing.Size(74, 20);
            this.SettingMenuItem.Text = "設定（&S）";
            // 
            // SettingSerialportMenuItem
            // 
            this.SettingSerialportMenuItem.Name = "SettingSerialportMenuItem";
            this.SettingSerialportMenuItem.Size = new System.Drawing.Size(180, 22);
            this.SettingSerialportMenuItem.Text = "シリアルポート(&E)...";
            this.SettingSerialportMenuItem.Click += new System.EventHandler(this.SettingSerialportMenuItem_Click);
            // 
            // SettingDeviceMenuItem
            // 
            this.SettingDeviceMenuItem.Name = "SettingDeviceMenuItem";
            this.SettingDeviceMenuItem.Size = new System.Drawing.Size(180, 22);
            this.SettingDeviceMenuItem.Text = "デバイス(&D)...";
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
            this.SetTestModeMenuItem,
            this.SetPlaylistModeMenuItem});
            this.PlayModeMenuItem.Name = "PlayModeMenuItem";
            this.PlayModeMenuItem.Size = new System.Drawing.Size(180, 22);
            this.PlayModeMenuItem.Text = "プレイモード(&M)";
            // 
            // SetTestModeMenuItem
            // 
            this.SetTestModeMenuItem.Name = "SetTestModeMenuItem";
            this.SetTestModeMenuItem.Size = new System.Drawing.Size(180, 22);
            this.SetTestModeMenuItem.Text = "テスト(&D)";
            this.SetTestModeMenuItem.Click += new System.EventHandler(this.checkBoxTest_CheckedChanged);
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
            this.OpenFileMenuItem,
            this.SettingMenuItem,
            this.PlayerMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(584, 24);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ElectricDevicePortLabel
            // 
            this.ElectricDevicePortLabel.AutoSize = true;
            this.ElectricDevicePortLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ElectricDevicePortLabel.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ElectricDevicePortLabel.Location = new System.Drawing.Point(3, 0);
            this.ElectricDevicePortLabel.Name = "ElectricDevicePortLabel";
            this.ElectricDevicePortLabel.Size = new System.Drawing.Size(274, 36);
            this.ElectricDevicePortLabel.TabIndex = 5;
            this.ElectricDevicePortLabel.Text = "ElectricDevicePort";
            this.ElectricDevicePortLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.GuitarDevicePort, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.ElectricDevicePort, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.File, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.Playlist, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.PlaylistLabel, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.FileLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.GuitarDevicePortLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ElectricDevicePortLabel, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 38);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(560, 145);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // GuitarDevicePortLabel
            // 
            this.GuitarDevicePortLabel.AutoSize = true;
            this.GuitarDevicePortLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GuitarDevicePortLabel.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GuitarDevicePortLabel.Location = new System.Drawing.Point(3, 36);
            this.GuitarDevicePortLabel.Name = "GuitarDevicePortLabel";
            this.GuitarDevicePortLabel.Size = new System.Drawing.Size(274, 36);
            this.GuitarDevicePortLabel.TabIndex = 6;
            this.GuitarDevicePortLabel.Text = "GuitarDevicePort";
            this.GuitarDevicePortLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FileLabel
            // 
            this.FileLabel.AutoSize = true;
            this.FileLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileLabel.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FileLabel.Location = new System.Drawing.Point(3, 72);
            this.FileLabel.Name = "FileLabel";
            this.FileLabel.Size = new System.Drawing.Size(274, 36);
            this.FileLabel.TabIndex = 7;
            this.FileLabel.Text = "MidiFile";
            this.FileLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PlaylistLabel
            // 
            this.PlaylistLabel.AutoSize = true;
            this.PlaylistLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlaylistLabel.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PlaylistLabel.Location = new System.Drawing.Point(3, 108);
            this.PlaylistLabel.Name = "PlaylistLabel";
            this.PlaylistLabel.Size = new System.Drawing.Size(274, 37);
            this.PlaylistLabel.TabIndex = 8;
            this.PlaylistLabel.Text = "MidiPlaylist";
            this.PlaylistLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Playlist
            // 
            this.Playlist.AutoSize = true;
            this.Playlist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Playlist.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Playlist.ForeColor = System.Drawing.Color.Green;
            this.Playlist.Location = new System.Drawing.Point(283, 108);
            this.Playlist.Name = "Playlist";
            this.Playlist.Size = new System.Drawing.Size(274, 37);
            this.Playlist.TabIndex = 1;
            this.Playlist.Text = "fileName";
            this.Playlist.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ElectricDevicePort
            // 
            this.ElectricDevicePort.AutoSize = true;
            this.ElectricDevicePort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ElectricDevicePort.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ElectricDevicePort.ForeColor = System.Drawing.SystemColors.Highlight;
            this.ElectricDevicePort.Location = new System.Drawing.Point(283, 0);
            this.ElectricDevicePort.Name = "ElectricDevicePort";
            this.ElectricDevicePort.Size = new System.Drawing.Size(274, 36);
            this.ElectricDevicePort.TabIndex = 9;
            this.ElectricDevicePort.Text = "portNumber";
            this.ElectricDevicePort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GuitarDevicePort
            // 
            this.GuitarDevicePort.AutoSize = true;
            this.GuitarDevicePort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GuitarDevicePort.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GuitarDevicePort.ForeColor = System.Drawing.SystemColors.Highlight;
            this.GuitarDevicePort.Location = new System.Drawing.Point(283, 36);
            this.GuitarDevicePort.Name = "GuitarDevicePort";
            this.GuitarDevicePort.Size = new System.Drawing.Size(274, 36);
            this.GuitarDevicePort.TabIndex = 10;
            this.GuitarDevicePort.Text = "portNumber";
            this.GuitarDevicePort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PlayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(584, 391);
            this.Controls.Add(this.tableLayoutPanel5);
            this.Controls.Add(this.LabelTime);
            this.Controls.Add(this.trackBar);
            this.Controls.Add(this.LogTextBox);
            this.Controls.Add(this.NowPlaying);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(600, 430);
            this.Name = "PlayerForm";
            this.Text = "ElectricMusicPlayer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.IO.Ports.SerialPort serialPort1;
        private System.IO.Ports.SerialPort serialPort2;
        private System.Windows.Forms.Label File;
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
        private System.Windows.Forms.ToolStripMenuItem OpenFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenMidiFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenPlaylistMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SettingMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SettingSerialportMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SettingDeviceMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PlayerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PlayModeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SetTestModeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SetPlaylistModeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TestPlayMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TestMelodyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TestGuitarMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TestBaseMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TestDrumMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Label ElectricDevicePortLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label GuitarDevicePort;
        private System.Windows.Forms.Label ElectricDevicePort;
        private System.Windows.Forms.Label Playlist;
        private System.Windows.Forms.Label PlaylistLabel;
        private System.Windows.Forms.Label FileLabel;
        private System.Windows.Forms.Label GuitarDevicePortLabel;
    }
}

