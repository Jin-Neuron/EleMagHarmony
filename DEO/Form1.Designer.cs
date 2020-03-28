namespace DEO
{
    partial class Form1
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
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.serialPort2 = new System.IO.Ports.SerialPort(this.components);
            this.checkBoxPlaylist = new System.Windows.Forms.CheckBox();
            this.checkBoxTest = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.GuitarPortSelectButton2 = new System.Windows.Forms.Button();
            this.PortSelectGuitar = new System.Windows.Forms.ComboBox();
            this.PortSelectRelay = new System.Windows.Forms.ComboBox();
            this.RelayPortSelectButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.OpenPlaylistButton = new System.Windows.Forms.Button();
            this.Playlist1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.OpenFileButton = new System.Windows.Forms.Button();
            this.File1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.NextButton = new System.Windows.Forms.Button();
            this.StartButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.ReturnButton = new System.Windows.Forms.Button();
            this.NowPlaying = new System.Windows.Forms.TextBox();
            this.RepeatCheck = new System.Windows.Forms.CheckBox();
            this.RandomCheck = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackBar
            // 
            this.trackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar.Location = new System.Drawing.Point(71, 358);
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(677, 45);
            this.trackBar.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // checkBoxPlaylist
            // 
            this.checkBoxPlaylist.AutoSize = true;
            this.checkBoxPlaylist.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBoxPlaylist.Location = new System.Drawing.Point(71, 126);
            this.checkBoxPlaylist.Name = "checkBoxPlaylist";
            this.checkBoxPlaylist.Size = new System.Drawing.Size(153, 23);
            this.checkBoxPlaylist.TabIndex = 1;
            this.checkBoxPlaylist.Text = "プレイリストモード";
            this.checkBoxPlaylist.UseVisualStyleBackColor = true;
            // 
            // checkBoxTest
            // 
            this.checkBoxTest.AutoSize = true;
            this.checkBoxTest.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBoxTest.Location = new System.Drawing.Point(71, 204);
            this.checkBoxTest.Name = "checkBoxTest";
            this.checkBoxTest.Size = new System.Drawing.Size(115, 23);
            this.checkBoxTest.TabIndex = 2;
            this.checkBoxTest.Text = "テストモード";
            this.checkBoxTest.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.GuitarPortSelectButton2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.PortSelectGuitar, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.PortSelectRelay, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.RelayPortSelectButton, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(71, 26);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(677, 94);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // GuitarPortSelectButton2
            // 
            this.GuitarPortSelectButton2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GuitarPortSelectButton2.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GuitarPortSelectButton2.Location = new System.Drawing.Point(341, 58);
            this.GuitarPortSelectButton2.Name = "GuitarPortSelectButton2";
            this.GuitarPortSelectButton2.Size = new System.Drawing.Size(333, 33);
            this.GuitarPortSelectButton2.TabIndex = 7;
            this.GuitarPortSelectButton2.Text = "GuitarConnect";
            this.GuitarPortSelectButton2.UseVisualStyleBackColor = true;
            this.GuitarPortSelectButton2.Click += new System.EventHandler(this.GuitarPortSelectButton2_Click);
            // 
            // PortSelectGuitar
            // 
            this.PortSelectGuitar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PortSelectGuitar.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PortSelectGuitar.FormattingEnabled = true;
            this.PortSelectGuitar.Location = new System.Drawing.Point(3, 58);
            this.PortSelectGuitar.Name = "PortSelectGuitar";
            this.PortSelectGuitar.Size = new System.Drawing.Size(332, 27);
            this.PortSelectGuitar.TabIndex = 4;
            // 
            // PortSelectRelay
            // 
            this.PortSelectRelay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PortSelectRelay.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PortSelectRelay.FormattingEnabled = true;
            this.PortSelectRelay.Location = new System.Drawing.Point(3, 21);
            this.PortSelectRelay.Name = "PortSelectRelay";
            this.PortSelectRelay.Size = new System.Drawing.Size(332, 27);
            this.PortSelectRelay.TabIndex = 0;
            // 
            // RelayPortSelectButton
            // 
            this.RelayPortSelectButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RelayPortSelectButton.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.RelayPortSelectButton.Location = new System.Drawing.Point(341, 21);
            this.RelayPortSelectButton.Name = "RelayPortSelectButton";
            this.RelayPortSelectButton.Size = new System.Drawing.Size(333, 31);
            this.RelayPortSelectButton.TabIndex = 6;
            this.RelayPortSelectButton.Text = "RelayConnect";
            this.RelayPortSelectButton.UseVisualStyleBackColor = true;
            this.RelayPortSelectButton.Click += new System.EventHandler(this.RelayPortSelectButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(332, 18);
            this.label1.TabIndex = 5;
            this.label1.Text = "SelectComPort";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.OpenPlaylistButton, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.Playlist1, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(71, 155);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(674, 43);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // OpenPlaylistButton
            // 
            this.OpenPlaylistButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OpenPlaylistButton.Font = new System.Drawing.Font("MS UI Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OpenPlaylistButton.Location = new System.Drawing.Point(340, 3);
            this.OpenPlaylistButton.Name = "OpenPlaylistButton";
            this.OpenPlaylistButton.Size = new System.Drawing.Size(331, 37);
            this.OpenPlaylistButton.TabIndex = 0;
            this.OpenPlaylistButton.Text = "プレイリストを開く";
            this.OpenPlaylistButton.UseVisualStyleBackColor = true;
            // 
            // Playlist1
            // 
            this.Playlist1.AutoSize = true;
            this.Playlist1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Playlist1.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Playlist1.Location = new System.Drawing.Point(3, 0);
            this.Playlist1.Name = "Playlist1";
            this.Playlist1.Size = new System.Drawing.Size(331, 43);
            this.Playlist1.TabIndex = 1;
            this.Playlist1.Text = "fileName";
            this.Playlist1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.OpenFileButton, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.File1, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(71, 233);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(674, 43);
            this.tableLayoutPanel3.TabIndex = 5;
            // 
            // OpenFileButton
            // 
            this.OpenFileButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OpenFileButton.Font = new System.Drawing.Font("MS UI Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OpenFileButton.Location = new System.Drawing.Point(340, 3);
            this.OpenFileButton.Name = "OpenFileButton";
            this.OpenFileButton.Size = new System.Drawing.Size(331, 37);
            this.OpenFileButton.TabIndex = 0;
            this.OpenFileButton.Text = "MIDIファイルを開く";
            this.OpenFileButton.UseVisualStyleBackColor = true;
            // 
            // File1
            // 
            this.File1.AutoSize = true;
            this.File1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.File1.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.File1.Location = new System.Drawing.Point(3, 0);
            this.File1.Name = "File1";
            this.File1.Size = new System.Drawing.Size(331, 43);
            this.File1.TabIndex = 1;
            this.File1.Text = "fileName";
            this.File1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.tableLayoutPanel4.Location = new System.Drawing.Point(71, 452);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(677, 49);
            this.tableLayoutPanel4.TabIndex = 6;
            // 
            // NextButton
            // 
            this.NextButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NextButton.Font = new System.Drawing.Font("MS UI Gothic", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.NextButton.ForeColor = System.Drawing.Color.Blue;
            this.NextButton.Location = new System.Drawing.Point(510, 3);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(164, 43);
            this.NextButton.TabIndex = 3;
            this.NextButton.Text = "▶▶";
            this.NextButton.UseVisualStyleBackColor = true;
            // 
            // StartButton
            // 
            this.StartButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StartButton.Font = new System.Drawing.Font("MS UI Gothic", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.StartButton.ForeColor = System.Drawing.Color.Green;
            this.StartButton.Location = new System.Drawing.Point(341, 3);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(163, 43);
            this.StartButton.TabIndex = 2;
            this.StartButton.Text = "▶";
            this.StartButton.UseVisualStyleBackColor = true;
            // 
            // StopButton
            // 
            this.StopButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StopButton.Font = new System.Drawing.Font("MS UI Gothic", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.StopButton.ForeColor = System.Drawing.Color.Red;
            this.StopButton.Location = new System.Drawing.Point(172, 3);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(163, 43);
            this.StopButton.TabIndex = 1;
            this.StopButton.Text = "■";
            this.StopButton.UseVisualStyleBackColor = true;
            // 
            // ReturnButton
            // 
            this.ReturnButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReturnButton.Font = new System.Drawing.Font("MS UI Gothic", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ReturnButton.ForeColor = System.Drawing.Color.Blue;
            this.ReturnButton.Location = new System.Drawing.Point(3, 3);
            this.ReturnButton.Name = "ReturnButton";
            this.ReturnButton.Size = new System.Drawing.Size(163, 43);
            this.ReturnButton.TabIndex = 0;
            this.ReturnButton.Text = "◀◀";
            this.ReturnButton.UseVisualStyleBackColor = true;
            // 
            // NowPlaying
            // 
            this.NowPlaying.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NowPlaying.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.NowPlaying.ForeColor = System.Drawing.Color.SteelBlue;
            this.NowPlaying.Location = new System.Drawing.Point(71, 302);
            this.NowPlaying.Name = "NowPlaying";
            this.NowPlaying.ReadOnly = true;
            this.NowPlaying.Size = new System.Drawing.Size(677, 39);
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
            this.RepeatCheck.Location = new System.Drawing.Point(20, 3);
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
            this.RandomCheck.Location = new System.Drawing.Point(93, 3);
            this.RandomCheck.Name = "RandomCheck";
            this.RandomCheck.Size = new System.Drawing.Size(67, 43);
            this.RandomCheck.TabIndex = 9;
            this.RandomCheck.Text = "🔀";
            this.RandomCheck.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.RepeatCheck, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.RandomCheck, 1, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(320, 397);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(181, 49);
            this.tableLayoutPanel5.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 519);
            this.Controls.Add(this.tableLayoutPanel5);
            this.Controls.Add(this.NowPlaying);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.checkBoxTest);
            this.Controls.Add(this.checkBoxPlaylist);
            this.Controls.Add(this.trackBar);
            this.MinimumSize = new System.Drawing.Size(838, 558);
            this.Name = "Form1";
            this.Text = "DreamElectricOrchestra";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.IO.Ports.SerialPort serialPort2;
        private System.Windows.Forms.CheckBox checkBoxPlaylist;
        private System.Windows.Forms.CheckBox checkBoxTest;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox PortSelectGuitar;
        private System.Windows.Forms.ComboBox PortSelectRelay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button RelayPortSelectButton;
        private System.Windows.Forms.Button GuitarPortSelectButton2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button OpenPlaylistButton;
        private System.Windows.Forms.Label Playlist1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button OpenFileButton;
        private System.Windows.Forms.Label File1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button ReturnButton;
        private System.Windows.Forms.TextBox NowPlaying;
        private System.Windows.Forms.CheckBox RepeatCheck;
        private System.Windows.Forms.CheckBox RandomCheck;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
    }
}

