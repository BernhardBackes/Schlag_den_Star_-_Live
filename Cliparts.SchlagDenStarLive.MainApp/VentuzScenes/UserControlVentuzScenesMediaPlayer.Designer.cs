namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes {

    partial class UserControlVentuzScenesMediaPlayer {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent() {
            this.checkBoxLoop = new System.Windows.Forms.CheckBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonSet = new System.Windows.Forms.Button();
            this.buttonContinue = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.buttonSelectFile = new System.Windows.Forms.Button();
            this.textBoxFilename = new System.Windows.Forms.TextBox();
            this.trackBarVolume = new System.Windows.Forms.TrackBar();
            this.pictureBoxOnAir = new System.Windows.Forms.PictureBox();
            this.labelVolume = new System.Windows.Forms.Label();
            this.numericUpDownFaderDuration = new System.Windows.Forms.NumericUpDown();
            this.labelFaderDuration = new System.Windows.Forms.Label();
            this.labelFaderDurationUnit = new System.Windows.Forms.Label();
            this.buttonReset = new System.Windows.Forms.Button();
            this.labelPositionY = new System.Windows.Forms.Label();
            this.numericUpDownPositionX = new System.Windows.Forms.NumericUpDown();
            this.labelPositionX = new System.Windows.Forms.Label();
            this.numericUpDownPositionY = new System.Windows.Forms.NumericUpDown();
            this.labelScaling = new System.Windows.Forms.Label();
            this.numericUpDownScaling = new System.Windows.Forms.NumericUpDown();
            this.labelScalingUnit = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOnAir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFaderDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPositionX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScaling)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBoxLoop
            // 
            this.checkBoxLoop.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxLoop.FlatAppearance.CheckedBackColor = System.Drawing.Color.Lime;
            this.checkBoxLoop.ForeColor = System.Drawing.Color.Black;
            this.checkBoxLoop.Location = new System.Drawing.Point(342, 131);
            this.checkBoxLoop.Name = "checkBoxLoop";
            this.checkBoxLoop.Size = new System.Drawing.Size(84, 32);
            this.checkBoxLoop.TabIndex = 19;
            this.checkBoxLoop.Text = "loop";
            this.checkBoxLoop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxLoop.UseVisualStyleBackColor = true;
            this.checkBoxLoop.CheckedChanged += new System.EventHandler(this.checkBoxMediaPlayerLoop_CheckedChanged);
            // 
            // buttonClear
            // 
            this.buttonClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClear.ForeColor = System.Drawing.Color.Black;
            this.buttonClear.Location = new System.Drawing.Point(8, 83);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(206, 32);
            this.buttonClear.TabIndex = 16;
            this.buttonClear.Text = "clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonSet
            // 
            this.buttonSet.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSet.ForeColor = System.Drawing.Color.Black;
            this.buttonSet.Location = new System.Drawing.Point(8, 48);
            this.buttonSet.Name = "buttonSet";
            this.buttonSet.Size = new System.Drawing.Size(206, 32);
            this.buttonSet.TabIndex = 15;
            this.buttonSet.Text = "set";
            this.buttonSet.UseVisualStyleBackColor = true;
            this.buttonSet.Click += new System.EventHandler(this.buttonSet_Click);
            // 
            // buttonContinue
            // 
            this.buttonContinue.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonContinue.ForeColor = System.Drawing.Color.Black;
            this.buttonContinue.Location = new System.Drawing.Point(220, 131);
            this.buttonContinue.Name = "buttonContinue";
            this.buttonContinue.Size = new System.Drawing.Size(100, 32);
            this.buttonContinue.TabIndex = 14;
            this.buttonContinue.Text = "continue";
            this.buttonContinue.UseVisualStyleBackColor = true;
            this.buttonContinue.Click += new System.EventHandler(this.buttonContinue_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStop.ForeColor = System.Drawing.Color.Black;
            this.buttonStop.Location = new System.Drawing.Point(114, 131);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(100, 32);
            this.buttonStop.TabIndex = 13;
            this.buttonStop.Text = "stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonPlay
            // 
            this.buttonPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPlay.ForeColor = System.Drawing.Color.Black;
            this.buttonPlay.Location = new System.Drawing.Point(8, 131);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(100, 32);
            this.buttonPlay.TabIndex = 12;
            this.buttonPlay.Text = "play";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // buttonSelectFile
            // 
            this.buttonSelectFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSelectFile.ForeColor = System.Drawing.Color.Black;
            this.buttonSelectFile.Location = new System.Drawing.Point(929, 8);
            this.buttonSelectFile.Name = "buttonSelectFile";
            this.buttonSelectFile.Size = new System.Drawing.Size(53, 24);
            this.buttonSelectFile.TabIndex = 11;
            this.buttonSelectFile.Text = "···";
            this.buttonSelectFile.UseVisualStyleBackColor = true;
            this.buttonSelectFile.Click += new System.EventHandler(this.buttonSelectFile_Click);
            // 
            // textBoxFilename
            // 
            this.textBoxFilename.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFilename.Location = new System.Drawing.Point(8, 8);
            this.textBoxFilename.Name = "textBoxFilename";
            this.textBoxFilename.ReadOnly = true;
            this.textBoxFilename.Size = new System.Drawing.Size(920, 24);
            this.textBoxFilename.TabIndex = 10;
            this.textBoxFilename.Text = "textBoxFilename";
            // 
            // trackBarVolume
            // 
            this.trackBarVolume.Location = new System.Drawing.Point(432, 125);
            this.trackBarVolume.Name = "trackBarVolume";
            this.trackBarVolume.Size = new System.Drawing.Size(550, 45);
            this.trackBarVolume.TabIndex = 20;
            this.trackBarVolume.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarVolume.Scroll += new System.EventHandler(this.trackBarVolume_Scroll);
            // 
            // pictureBoxOnAir
            // 
            this.pictureBoxOnAir.Image = global::Cliparts.SchlagDenStarLive.MainApp.Properties.Resources.onair;
            this.pictureBoxOnAir.Location = new System.Drawing.Point(342, 48);
            this.pictureBoxOnAir.Name = "pictureBoxOnAir";
            this.pictureBoxOnAir.Size = new System.Drawing.Size(100, 67);
            this.pictureBoxOnAir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxOnAir.TabIndex = 21;
            this.pictureBoxOnAir.TabStop = false;
            // 
            // labelVolume
            // 
            this.labelVolume.Location = new System.Drawing.Point(429, 86);
            this.labelVolume.Name = "labelVolume";
            this.labelVolume.Size = new System.Drawing.Size(553, 36);
            this.labelVolume.TabIndex = 18;
            this.labelVolume.Text = "volume";
            this.labelVolume.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // numericUpDownFaderDuration
            // 
            this.numericUpDownFaderDuration.DecimalPlaces = 1;
            this.numericUpDownFaderDuration.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownFaderDuration.Location = new System.Drawing.Point(220, 86);
            this.numericUpDownFaderDuration.Name = "numericUpDownFaderDuration";
            this.numericUpDownFaderDuration.Size = new System.Drawing.Size(100, 29);
            this.numericUpDownFaderDuration.TabIndex = 22;
            this.numericUpDownFaderDuration.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownFaderDuration.ValueChanged += new System.EventHandler(this.numericUpDownFaderDuration_ValueChanged);
            // 
            // labelFaderDuration
            // 
            this.labelFaderDuration.Location = new System.Drawing.Point(220, 35);
            this.labelFaderDuration.Name = "labelFaderDuration";
            this.labelFaderDuration.Size = new System.Drawing.Size(100, 48);
            this.labelFaderDuration.TabIndex = 23;
            this.labelFaderDuration.Text = "fader\r\nduration";
            this.labelFaderDuration.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // labelFaderDurationUnit
            // 
            this.labelFaderDurationUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFaderDurationUnit.Location = new System.Drawing.Point(316, 86);
            this.labelFaderDurationUnit.Name = "labelFaderDurationUnit";
            this.labelFaderDurationUnit.Size = new System.Drawing.Size(20, 29);
            this.labelFaderDurationUnit.TabIndex = 24;
            this.labelFaderDurationUnit.Text = "s";
            this.labelFaderDurationUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonReset
            // 
            this.buttonReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReset.ForeColor = System.Drawing.Color.Black;
            this.buttonReset.Location = new System.Drawing.Point(8, 169);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(206, 32);
            this.buttonReset.TabIndex = 25;
            this.buttonReset.Text = "reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // labelPositionY
            // 
            this.labelPositionY.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPositionY.Location = new System.Drawing.Point(637, 48);
            this.labelPositionY.Name = "labelPositionY";
            this.labelPositionY.Size = new System.Drawing.Size(29, 29);
            this.labelPositionY.TabIndex = 27;
            this.labelPositionY.Text = "Y:";
            this.labelPositionY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownPositionX
            // 
            this.numericUpDownPositionX.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownPositionX.Location = new System.Drawing.Point(547, 48);
            this.numericUpDownPositionX.Name = "numericUpDownPositionX";
            this.numericUpDownPositionX.Size = new System.Drawing.Size(84, 29);
            this.numericUpDownPositionX.TabIndex = 26;
            this.numericUpDownPositionX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownPositionX.ValueChanged += new System.EventHandler(this.numericUpDownPositionX_ValueChanged);
            // 
            // labelPositionX
            // 
            this.labelPositionX.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPositionX.Location = new System.Drawing.Point(448, 48);
            this.labelPositionX.Name = "labelPositionX";
            this.labelPositionX.Size = new System.Drawing.Size(98, 29);
            this.labelPositionX.TabIndex = 28;
            this.labelPositionX.Text = "position X:";
            this.labelPositionX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownPositionY
            // 
            this.numericUpDownPositionY.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownPositionY.Location = new System.Drawing.Point(667, 48);
            this.numericUpDownPositionY.Name = "numericUpDownPositionY";
            this.numericUpDownPositionY.Size = new System.Drawing.Size(84, 29);
            this.numericUpDownPositionY.TabIndex = 29;
            this.numericUpDownPositionY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownPositionY.ValueChanged += new System.EventHandler(this.numericUpDownPositionY_ValueChanged);
            // 
            // labelScaling
            // 
            this.labelScaling.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelScaling.Location = new System.Drawing.Point(790, 48);
            this.labelScaling.Name = "labelScaling";
            this.labelScaling.Size = new System.Drawing.Size(77, 29);
            this.labelScaling.TabIndex = 31;
            this.labelScaling.Text = "scaling:";
            this.labelScaling.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownScaling
            // 
            this.numericUpDownScaling.DecimalPlaces = 1;
            this.numericUpDownScaling.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownScaling.Location = new System.Drawing.Point(868, 48);
            this.numericUpDownScaling.Name = "numericUpDownScaling";
            this.numericUpDownScaling.Size = new System.Drawing.Size(84, 29);
            this.numericUpDownScaling.TabIndex = 30;
            this.numericUpDownScaling.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownScaling.ValueChanged += new System.EventHandler(this.numericUpDownScaling_ValueChanged);
            // 
            // labelScalingUnit
            // 
            this.labelScalingUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelScalingUnit.Location = new System.Drawing.Point(953, 48);
            this.labelScalingUnit.Name = "labelScalingUnit";
            this.labelScalingUnit.Size = new System.Drawing.Size(29, 29);
            this.labelScalingUnit.TabIndex = 32;
            this.labelScalingUnit.Text = "%";
            this.labelScalingUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UserControlVentuzScenesMediaPlayer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.Controls.Add(this.labelScalingUnit);
            this.Controls.Add(this.labelScaling);
            this.Controls.Add(this.numericUpDownScaling);
            this.Controls.Add(this.numericUpDownPositionY);
            this.Controls.Add(this.labelPositionX);
            this.Controls.Add(this.labelPositionY);
            this.Controls.Add(this.numericUpDownPositionX);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.labelFaderDurationUnit);
            this.Controls.Add(this.labelFaderDuration);
            this.Controls.Add(this.numericUpDownFaderDuration);
            this.Controls.Add(this.pictureBoxOnAir);
            this.Controls.Add(this.trackBarVolume);
            this.Controls.Add(this.checkBoxLoop);
            this.Controls.Add(this.labelVolume);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonSet);
            this.Controls.Add(this.buttonContinue);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.buttonSelectFile);
            this.Controls.Add(this.textBoxFilename);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "UserControlVentuzScenesMediaPlayer";
            this.Size = new System.Drawing.Size(990, 210);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOnAir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFaderDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPositionX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScaling)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxLoop;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonSet;
        private System.Windows.Forms.Button buttonContinue;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Button buttonSelectFile;
        private System.Windows.Forms.TextBox textBoxFilename;
        private System.Windows.Forms.TrackBar trackBarVolume;
        private System.Windows.Forms.PictureBox pictureBoxOnAir;
        private System.Windows.Forms.Label labelVolume;
        private System.Windows.Forms.NumericUpDown numericUpDownFaderDuration;
        private System.Windows.Forms.Label labelFaderDuration;
        private System.Windows.Forms.Label labelFaderDurationUnit;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Label labelPositionY;
        private System.Windows.Forms.NumericUpDown numericUpDownPositionX;
        private System.Windows.Forms.Label labelPositionX;
        private System.Windows.Forms.NumericUpDown numericUpDownPositionY;
        private System.Windows.Forms.Label labelScaling;
        private System.Windows.Forms.NumericUpDown numericUpDownScaling;
        private System.Windows.Forms.Label labelScalingUnit;
    }
}
