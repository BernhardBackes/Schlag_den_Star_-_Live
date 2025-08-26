namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.Fullscreen {

    partial class BusinessForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.buttonClear = new System.Windows.Forms.Button();
            this.groupBoxBackloop = new System.Windows.Forms.GroupBox();
            this.buttonFadeBackloopOut = new System.Windows.Forms.Button();
            this.buttonFadeBackloopIn = new System.Windows.Forms.Button();
            this.buttonRestartBackloop = new System.Windows.Forms.Button();
            this.groupBoxGameboard = new System.Windows.Forms.GroupBox();
            this.buttonShowGameboard = new System.Windows.Forms.Button();
            this.groupBoxClock = new System.Windows.Forms.GroupBox();
            this.userControlRecTriggerClockStop = new Cliparts.SchlagDenStarLive.MainApp.UserControlRecTrigger();
            this.labelClockStop = new System.Windows.Forms.Label();
            this.numericUpDownClockStop = new System.Windows.Forms.NumericUpDown();
            this.userControlRecTriggerClockAlarm2 = new Cliparts.SchlagDenStarLive.MainApp.UserControlRecTrigger();
            this.userControlRecTriggerClockAlarm1 = new Cliparts.SchlagDenStarLive.MainApp.UserControlRecTrigger();
            this.labelClockStyle = new System.Windows.Forms.Label();
            this.comboBoxClockStyle = new System.Windows.Forms.ComboBox();
            this.buttonClockReset = new System.Windows.Forms.Button();
            this.buttonClockContinue = new System.Windows.Forms.Button();
            this.buttonClockStop = new System.Windows.Forms.Button();
            this.buttonClockStart = new System.Windows.Forms.Button();
            this.labelClockCurrentTime = new System.Windows.Forms.Label();
            this.labelClockStartValue = new System.Windows.Forms.Label();
            this.labelClockAlarm1 = new System.Windows.Forms.Label();
            this.numericUpDownClockAlarm1 = new System.Windows.Forms.NumericUpDown();
            this.labelClockAlarm2 = new System.Windows.Forms.Label();
            this.numericUpDownClockAlarm2 = new System.Windows.Forms.NumericUpDown();
            this.labelClockStart = new System.Windows.Forms.Label();
            this.numericUpDownClockStart = new System.Windows.Forms.NumericUpDown();
            this.buttonShowClock = new System.Windows.Forms.Button();
            this.groupBoxGame = new System.Windows.Forms.GroupBox();
            this.buttonShowGame = new System.Windows.Forms.Button();
            this.groupBoxMediaPlayer = new System.Windows.Forms.GroupBox();
            this.userControlVentuzScenesMediaPlayer = new Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.UserControlVentuzScenesMediaPlayer();
            this.groupBoxFreetext = new System.Windows.Forms.GroupBox();
            this.textBoxFreetext = new System.Windows.Forms.TextBox();
            this.buttonShowFreetext = new System.Windows.Forms.Button();
            this.groupBoxBackloop.SuspendLayout();
            this.groupBoxGameboard.SuspendLayout();
            this.groupBoxClock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClockStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClockAlarm1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClockAlarm2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClockStart)).BeginInit();
            this.groupBoxGame.SuspendLayout();
            this.groupBoxMediaPlayer.SuspendLayout();
            this.groupBoxFreetext.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClear
            // 
            this.buttonClear.ForeColor = System.Drawing.Color.Black;
            this.buttonClear.Location = new System.Drawing.Point(12, 18);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(84, 45);
            this.buttonClear.TabIndex = 2;
            this.buttonClear.Text = "clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonAllOut_Click);
            // 
            // groupBoxBackloop
            // 
            this.groupBoxBackloop.Controls.Add(this.buttonFadeBackloopOut);
            this.groupBoxBackloop.Controls.Add(this.buttonFadeBackloopIn);
            this.groupBoxBackloop.Controls.Add(this.buttonRestartBackloop);
            this.groupBoxBackloop.ForeColor = System.Drawing.Color.White;
            this.groupBoxBackloop.Location = new System.Drawing.Point(12, 69);
            this.groupBoxBackloop.Name = "groupBoxBackloop";
            this.groupBoxBackloop.Size = new System.Drawing.Size(281, 50);
            this.groupBoxBackloop.TabIndex = 6;
            this.groupBoxBackloop.TabStop = false;
            this.groupBoxBackloop.Text = "backloop";
            // 
            // buttonFadeBackloopOut
            // 
            this.buttonFadeBackloopOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFadeBackloopOut.ForeColor = System.Drawing.Color.Black;
            this.buttonFadeBackloopOut.Location = new System.Drawing.Point(181, 20);
            this.buttonFadeBackloopOut.Name = "buttonFadeBackloopOut";
            this.buttonFadeBackloopOut.Size = new System.Drawing.Size(84, 24);
            this.buttonFadeBackloopOut.TabIndex = 4;
            this.buttonFadeBackloopOut.Text = "fade OUT";
            this.buttonFadeBackloopOut.UseVisualStyleBackColor = true;
            this.buttonFadeBackloopOut.Click += new System.EventHandler(this.buttonFadeBackloopOut_Click);
            // 
            // buttonFadeBackloopIn
            // 
            this.buttonFadeBackloopIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFadeBackloopIn.ForeColor = System.Drawing.Color.Black;
            this.buttonFadeBackloopIn.Location = new System.Drawing.Point(96, 20);
            this.buttonFadeBackloopIn.Name = "buttonFadeBackloopIn";
            this.buttonFadeBackloopIn.Size = new System.Drawing.Size(84, 24);
            this.buttonFadeBackloopIn.TabIndex = 3;
            this.buttonFadeBackloopIn.Text = "fade IN";
            this.buttonFadeBackloopIn.UseVisualStyleBackColor = true;
            this.buttonFadeBackloopIn.Click += new System.EventHandler(this.buttonFadeBackloopIn_Click);
            // 
            // buttonRestartBackloop
            // 
            this.buttonRestartBackloop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRestartBackloop.ForeColor = System.Drawing.Color.Black;
            this.buttonRestartBackloop.Location = new System.Drawing.Point(6, 20);
            this.buttonRestartBackloop.Name = "buttonRestartBackloop";
            this.buttonRestartBackloop.Size = new System.Drawing.Size(84, 24);
            this.buttonRestartBackloop.TabIndex = 2;
            this.buttonRestartBackloop.Text = "restart";
            this.buttonRestartBackloop.UseVisualStyleBackColor = true;
            this.buttonRestartBackloop.Click += new System.EventHandler(this.buttonRestartBackloop_Click);
            // 
            // groupBoxGameboard
            // 
            this.groupBoxGameboard.Controls.Add(this.buttonShowGameboard);
            this.groupBoxGameboard.ForeColor = System.Drawing.Color.White;
            this.groupBoxGameboard.Location = new System.Drawing.Point(12, 125);
            this.groupBoxGameboard.Name = "groupBoxGameboard";
            this.groupBoxGameboard.Size = new System.Drawing.Size(180, 50);
            this.groupBoxGameboard.TabIndex = 7;
            this.groupBoxGameboard.TabStop = false;
            this.groupBoxGameboard.Text = "gameboard";
            // 
            // buttonShowGameboard
            // 
            this.buttonShowGameboard.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonShowGameboard.ForeColor = System.Drawing.Color.Black;
            this.buttonShowGameboard.Location = new System.Drawing.Point(6, 20);
            this.buttonShowGameboard.Name = "buttonShowGameboard";
            this.buttonShowGameboard.Size = new System.Drawing.Size(168, 24);
            this.buttonShowGameboard.TabIndex = 2;
            this.buttonShowGameboard.Text = "show";
            this.buttonShowGameboard.UseVisualStyleBackColor = true;
            this.buttonShowGameboard.Click += new System.EventHandler(this.buttonShowGameboard_Click);
            // 
            // groupBoxClock
            // 
            this.groupBoxClock.Controls.Add(this.userControlRecTriggerClockStop);
            this.groupBoxClock.Controls.Add(this.labelClockStop);
            this.groupBoxClock.Controls.Add(this.numericUpDownClockStop);
            this.groupBoxClock.Controls.Add(this.userControlRecTriggerClockAlarm2);
            this.groupBoxClock.Controls.Add(this.userControlRecTriggerClockAlarm1);
            this.groupBoxClock.Controls.Add(this.labelClockStyle);
            this.groupBoxClock.Controls.Add(this.comboBoxClockStyle);
            this.groupBoxClock.Controls.Add(this.buttonClockReset);
            this.groupBoxClock.Controls.Add(this.buttonClockContinue);
            this.groupBoxClock.Controls.Add(this.buttonClockStop);
            this.groupBoxClock.Controls.Add(this.buttonClockStart);
            this.groupBoxClock.Controls.Add(this.labelClockCurrentTime);
            this.groupBoxClock.Controls.Add(this.labelClockStartValue);
            this.groupBoxClock.Controls.Add(this.labelClockAlarm1);
            this.groupBoxClock.Controls.Add(this.numericUpDownClockAlarm1);
            this.groupBoxClock.Controls.Add(this.labelClockAlarm2);
            this.groupBoxClock.Controls.Add(this.numericUpDownClockAlarm2);
            this.groupBoxClock.Controls.Add(this.labelClockStart);
            this.groupBoxClock.Controls.Add(this.numericUpDownClockStart);
            this.groupBoxClock.Controls.Add(this.buttonShowClock);
            this.groupBoxClock.ForeColor = System.Drawing.Color.White;
            this.groupBoxClock.Location = new System.Drawing.Point(198, 125);
            this.groupBoxClock.Name = "groupBoxClock";
            this.groupBoxClock.Size = new System.Drawing.Size(571, 178);
            this.groupBoxClock.TabIndex = 8;
            this.groupBoxClock.TabStop = false;
            this.groupBoxClock.Text = "clock";
            // 
            // userControlRecTriggerClockStop
            // 
            this.userControlRecTriggerClockStop.Location = new System.Drawing.Point(355, 82);
            this.userControlRecTriggerClockStop.Name = "userControlRecTriggerClockStop";
            this.userControlRecTriggerClockStop.Size = new System.Drawing.Size(60, 24);
            this.userControlRecTriggerClockStop.TabIndex = 217;
            // 
            // labelClockStop
            // 
            this.labelClockStop.Location = new System.Drawing.Point(193, 82);
            this.labelClockStop.Name = "labelClockStop";
            this.labelClockStop.Size = new System.Drawing.Size(87, 24);
            this.labelClockStop.TabIndex = 216;
            this.labelClockStop.Text = "stop:";
            this.labelClockStop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownClockStop
            // 
            this.numericUpDownClockStop.Location = new System.Drawing.Point(286, 82);
            this.numericUpDownClockStop.Name = "numericUpDownClockStop";
            this.numericUpDownClockStop.Size = new System.Drawing.Size(63, 24);
            this.numericUpDownClockStop.TabIndex = 215;
            this.numericUpDownClockStop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownClockStop.ValueChanged += new System.EventHandler(this.numericUpDownClockStop_ValueChanged);
            // 
            // userControlRecTriggerClockAlarm2
            // 
            this.userControlRecTriggerClockAlarm2.Location = new System.Drawing.Point(355, 142);
            this.userControlRecTriggerClockAlarm2.Name = "userControlRecTriggerClockAlarm2";
            this.userControlRecTriggerClockAlarm2.Size = new System.Drawing.Size(60, 24);
            this.userControlRecTriggerClockAlarm2.TabIndex = 214;
            // 
            // userControlRecTriggerClockAlarm1
            // 
            this.userControlRecTriggerClockAlarm1.Location = new System.Drawing.Point(355, 112);
            this.userControlRecTriggerClockAlarm1.Name = "userControlRecTriggerClockAlarm1";
            this.userControlRecTriggerClockAlarm1.Size = new System.Drawing.Size(60, 24);
            this.userControlRecTriggerClockAlarm1.TabIndex = 10;
            // 
            // labelClockStyle
            // 
            this.labelClockStyle.Location = new System.Drawing.Point(193, 22);
            this.labelClockStyle.Name = "labelClockStyle";
            this.labelClockStyle.Size = new System.Drawing.Size(87, 24);
            this.labelClockStyle.TabIndex = 213;
            this.labelClockStyle.Text = "style:";
            this.labelClockStyle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxClockStyle
            // 
            this.comboBoxClockStyle.FormattingEnabled = true;
            this.comboBoxClockStyle.Location = new System.Drawing.Point(286, 20);
            this.comboBoxClockStyle.Name = "comboBoxClockStyle";
            this.comboBoxClockStyle.Size = new System.Drawing.Size(119, 26);
            this.comboBoxClockStyle.TabIndex = 212;
            this.comboBoxClockStyle.SelectedIndexChanged += new System.EventHandler(this.comboBoxClockStyle_SelectedIndexChanged);
            // 
            // buttonClockReset
            // 
            this.buttonClockReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClockReset.ForeColor = System.Drawing.Color.Black;
            this.buttonClockReset.Location = new System.Drawing.Point(428, 113);
            this.buttonClockReset.Name = "buttonClockReset";
            this.buttonClockReset.Size = new System.Drawing.Size(136, 24);
            this.buttonClockReset.TabIndex = 211;
            this.buttonClockReset.Text = "reset";
            this.buttonClockReset.UseVisualStyleBackColor = true;
            this.buttonClockReset.Click += new System.EventHandler(this.buttonClockReset_Click);
            // 
            // buttonClockContinue
            // 
            this.buttonClockContinue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClockContinue.ForeColor = System.Drawing.Color.Black;
            this.buttonClockContinue.Location = new System.Drawing.Point(428, 83);
            this.buttonClockContinue.Name = "buttonClockContinue";
            this.buttonClockContinue.Size = new System.Drawing.Size(136, 24);
            this.buttonClockContinue.TabIndex = 210;
            this.buttonClockContinue.Text = "continue";
            this.buttonClockContinue.UseVisualStyleBackColor = true;
            this.buttonClockContinue.Click += new System.EventHandler(this.buttonClockContinue_Click);
            // 
            // buttonClockStop
            // 
            this.buttonClockStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClockStop.ForeColor = System.Drawing.Color.Black;
            this.buttonClockStop.Location = new System.Drawing.Point(428, 53);
            this.buttonClockStop.Name = "buttonClockStop";
            this.buttonClockStop.Size = new System.Drawing.Size(136, 24);
            this.buttonClockStop.TabIndex = 209;
            this.buttonClockStop.Text = "stop";
            this.buttonClockStop.UseVisualStyleBackColor = true;
            this.buttonClockStop.Click += new System.EventHandler(this.buttonClockStop_Click);
            // 
            // buttonClockStart
            // 
            this.buttonClockStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClockStart.ForeColor = System.Drawing.Color.Black;
            this.buttonClockStart.Location = new System.Drawing.Point(428, 23);
            this.buttonClockStart.Name = "buttonClockStart";
            this.buttonClockStart.Size = new System.Drawing.Size(136, 24);
            this.buttonClockStart.TabIndex = 208;
            this.buttonClockStart.Text = "start";
            this.buttonClockStart.UseVisualStyleBackColor = true;
            this.buttonClockStart.Click += new System.EventHandler(this.buttonClockStart_Click);
            // 
            // labelClockCurrentTime
            // 
            this.labelClockCurrentTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelClockCurrentTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelClockCurrentTime.Location = new System.Drawing.Point(21, 50);
            this.labelClockCurrentTime.Name = "labelClockCurrentTime";
            this.labelClockCurrentTime.Size = new System.Drawing.Size(136, 39);
            this.labelClockCurrentTime.TabIndex = 207;
            this.labelClockCurrentTime.Text = "00:00";
            this.labelClockCurrentTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelClockStartValue
            // 
            this.labelClockStartValue.Location = new System.Drawing.Point(355, 52);
            this.labelClockStartValue.Name = "labelClockStartValue";
            this.labelClockStartValue.Size = new System.Drawing.Size(60, 24);
            this.labelClockStartValue.TabIndex = 21;
            this.labelClockStartValue.Text = "00:00";
            this.labelClockStartValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelClockAlarm1
            // 
            this.labelClockAlarm1.Location = new System.Drawing.Point(193, 112);
            this.labelClockAlarm1.Name = "labelClockAlarm1";
            this.labelClockAlarm1.Size = new System.Drawing.Size(87, 24);
            this.labelClockAlarm1.TabIndex = 20;
            this.labelClockAlarm1.Text = "alarm 1:";
            this.labelClockAlarm1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownClockAlarm1
            // 
            this.numericUpDownClockAlarm1.Location = new System.Drawing.Point(286, 112);
            this.numericUpDownClockAlarm1.Name = "numericUpDownClockAlarm1";
            this.numericUpDownClockAlarm1.Size = new System.Drawing.Size(63, 24);
            this.numericUpDownClockAlarm1.TabIndex = 19;
            this.numericUpDownClockAlarm1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownClockAlarm1.ValueChanged += new System.EventHandler(this.numericUpDownClockAlarm1_ValueChanged);
            // 
            // labelClockAlarm2
            // 
            this.labelClockAlarm2.Location = new System.Drawing.Point(193, 143);
            this.labelClockAlarm2.Name = "labelClockAlarm2";
            this.labelClockAlarm2.Size = new System.Drawing.Size(87, 24);
            this.labelClockAlarm2.TabIndex = 18;
            this.labelClockAlarm2.Text = "alarm 2:";
            this.labelClockAlarm2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownClockAlarm2
            // 
            this.numericUpDownClockAlarm2.Location = new System.Drawing.Point(286, 143);
            this.numericUpDownClockAlarm2.Name = "numericUpDownClockAlarm2";
            this.numericUpDownClockAlarm2.Size = new System.Drawing.Size(63, 24);
            this.numericUpDownClockAlarm2.TabIndex = 17;
            this.numericUpDownClockAlarm2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownClockAlarm2.ValueChanged += new System.EventHandler(this.numericUpDownClockAlarm2_ValueChanged);
            // 
            // labelClockStart
            // 
            this.labelClockStart.Location = new System.Drawing.Point(193, 52);
            this.labelClockStart.Name = "labelClockStart";
            this.labelClockStart.Size = new System.Drawing.Size(87, 24);
            this.labelClockStart.TabIndex = 16;
            this.labelClockStart.Text = "start:";
            this.labelClockStart.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownClockStart
            // 
            this.numericUpDownClockStart.Location = new System.Drawing.Point(286, 52);
            this.numericUpDownClockStart.Name = "numericUpDownClockStart";
            this.numericUpDownClockStart.Size = new System.Drawing.Size(63, 24);
            this.numericUpDownClockStart.TabIndex = 15;
            this.numericUpDownClockStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownClockStart.ValueChanged += new System.EventHandler(this.numericUpDownClockStart_ValueChanged);
            // 
            // buttonShowClock
            // 
            this.buttonShowClock.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonShowClock.ForeColor = System.Drawing.Color.Black;
            this.buttonShowClock.Location = new System.Drawing.Point(6, 20);
            this.buttonShowClock.Name = "buttonShowClock";
            this.buttonShowClock.Size = new System.Drawing.Size(168, 24);
            this.buttonShowClock.TabIndex = 2;
            this.buttonShowClock.Text = "show";
            this.buttonShowClock.UseVisualStyleBackColor = true;
            this.buttonShowClock.Click += new System.EventHandler(this.buttonShowClock_Click);
            // 
            // groupBoxGame
            // 
            this.groupBoxGame.Controls.Add(this.buttonShowGame);
            this.groupBoxGame.ForeColor = System.Drawing.Color.White;
            this.groupBoxGame.Location = new System.Drawing.Point(12, 182);
            this.groupBoxGame.Name = "groupBoxGame";
            this.groupBoxGame.Size = new System.Drawing.Size(180, 50);
            this.groupBoxGame.TabIndex = 9;
            this.groupBoxGame.TabStop = false;
            this.groupBoxGame.Text = "game";
            // 
            // buttonShowGame
            // 
            this.buttonShowGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonShowGame.ForeColor = System.Drawing.Color.Black;
            this.buttonShowGame.Location = new System.Drawing.Point(6, 20);
            this.buttonShowGame.Name = "buttonShowGame";
            this.buttonShowGame.Size = new System.Drawing.Size(168, 24);
            this.buttonShowGame.TabIndex = 2;
            this.buttonShowGame.Text = "show";
            this.buttonShowGame.UseVisualStyleBackColor = true;
            this.buttonShowGame.Click += new System.EventHandler(this.buttonShowGame_Click);
            // 
            // groupBoxMediaPlayer
            // 
            this.groupBoxMediaPlayer.Controls.Add(this.userControlVentuzScenesMediaPlayer);
            this.groupBoxMediaPlayer.ForeColor = System.Drawing.Color.White;
            this.groupBoxMediaPlayer.Location = new System.Drawing.Point(12, 309);
            this.groupBoxMediaPlayer.Name = "groupBoxMediaPlayer";
            this.groupBoxMediaPlayer.Size = new System.Drawing.Size(1000, 235);
            this.groupBoxMediaPlayer.TabIndex = 10;
            this.groupBoxMediaPlayer.TabStop = false;
            this.groupBoxMediaPlayer.Text = "media player";
            // 
            // userControlVentuzScenesMediaPlayer
            // 
            this.userControlVentuzScenesMediaPlayer.BackColor = System.Drawing.Color.DarkSlateGray;
            this.userControlVentuzScenesMediaPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userControlVentuzScenesMediaPlayer.ForeColor = System.Drawing.Color.White;
            this.userControlVentuzScenesMediaPlayer.Location = new System.Drawing.Point(6, 20);
            this.userControlVentuzScenesMediaPlayer.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.userControlVentuzScenesMediaPlayer.Name = "userControlVentuzScenesMediaPlayer";
            this.userControlVentuzScenesMediaPlayer.Size = new System.Drawing.Size(990, 210);
            this.userControlVentuzScenesMediaPlayer.TabIndex = 0;
            // 
            // groupBoxFreetext
            // 
            this.groupBoxFreetext.Controls.Add(this.textBoxFreetext);
            this.groupBoxFreetext.Controls.Add(this.buttonShowFreetext);
            this.groupBoxFreetext.ForeColor = System.Drawing.Color.White;
            this.groupBoxFreetext.Location = new System.Drawing.Point(775, 125);
            this.groupBoxFreetext.Name = "groupBoxFreetext";
            this.groupBoxFreetext.Size = new System.Drawing.Size(237, 126);
            this.groupBoxFreetext.TabIndex = 10;
            this.groupBoxFreetext.TabStop = false;
            this.groupBoxFreetext.Text = "free text";
            // 
            // textBoxFreetext
            // 
            this.textBoxFreetext.Location = new System.Drawing.Point(6, 50);
            this.textBoxFreetext.Multiline = true;
            this.textBoxFreetext.Name = "textBoxFreetext";
            this.textBoxFreetext.Size = new System.Drawing.Size(225, 67);
            this.textBoxFreetext.TabIndex = 3;
            this.textBoxFreetext.Text = "free text";
            this.textBoxFreetext.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxFreetext.TextChanged += new System.EventHandler(this.textBoxFreetext_TextChanged);
            // 
            // buttonShowFreetext
            // 
            this.buttonShowFreetext.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonShowFreetext.ForeColor = System.Drawing.Color.Black;
            this.buttonShowFreetext.Location = new System.Drawing.Point(6, 20);
            this.buttonShowFreetext.Name = "buttonShowFreetext";
            this.buttonShowFreetext.Size = new System.Drawing.Size(225, 24);
            this.buttonShowFreetext.TabIndex = 2;
            this.buttonShowFreetext.Text = "show";
            this.buttonShowFreetext.UseVisualStyleBackColor = true;
            this.buttonShowFreetext.Click += new System.EventHandler(this.buttonShowFreetext_Click);
            // 
            // BusinessForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Crimson;
            this.ClientSize = new System.Drawing.Size(1117, 703);
            this.Controls.Add(this.groupBoxFreetext);
            this.Controls.Add(this.groupBoxMediaPlayer);
            this.Controls.Add(this.groupBoxGame);
            this.Controls.Add(this.groupBoxClock);
            this.Controls.Add(this.groupBoxGameboard);
            this.Controls.Add(this.groupBoxBackloop);
            this.Controls.Add(this.buttonClear);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "BusinessForm";
            this.Text = "BusinessForm";
            this.groupBoxBackloop.ResumeLayout(false);
            this.groupBoxGameboard.ResumeLayout(false);
            this.groupBoxClock.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClockStop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClockAlarm1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClockAlarm2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClockStart)).EndInit();
            this.groupBoxGame.ResumeLayout(false);
            this.groupBoxMediaPlayer.ResumeLayout(false);
            this.groupBoxFreetext.ResumeLayout(false);
            this.groupBoxFreetext.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.GroupBox groupBoxBackloop;
        private System.Windows.Forms.Button buttonRestartBackloop;
        private System.Windows.Forms.Button buttonFadeBackloopOut;
        private System.Windows.Forms.Button buttonFadeBackloopIn;
        private System.Windows.Forms.GroupBox groupBoxGameboard;
        private System.Windows.Forms.Button buttonShowGameboard;
        private System.Windows.Forms.GroupBox groupBoxClock;
        private System.Windows.Forms.Button buttonShowClock;
        private System.Windows.Forms.GroupBox groupBoxGame;
        private System.Windows.Forms.Button buttonShowGame;
        private System.Windows.Forms.Label labelClockStartValue;
        private System.Windows.Forms.Label labelClockAlarm1;
        private System.Windows.Forms.NumericUpDown numericUpDownClockAlarm1;
        private System.Windows.Forms.Label labelClockAlarm2;
        private System.Windows.Forms.NumericUpDown numericUpDownClockAlarm2;
        private System.Windows.Forms.Label labelClockStart;
        private System.Windows.Forms.NumericUpDown numericUpDownClockStart;
        private System.Windows.Forms.Button buttonClockReset;
        private System.Windows.Forms.Button buttonClockContinue;
        private System.Windows.Forms.Button buttonClockStop;
        private System.Windows.Forms.Button buttonClockStart;
        private System.Windows.Forms.Label labelClockCurrentTime;
        private System.Windows.Forms.Label labelClockStyle;
        private System.Windows.Forms.ComboBox comboBoxClockStyle;
        private UserControlRecTrigger userControlRecTriggerClockStop;
        private System.Windows.Forms.Label labelClockStop;
        private System.Windows.Forms.NumericUpDown numericUpDownClockStop;
        private UserControlRecTrigger userControlRecTriggerClockAlarm2;
        private UserControlRecTrigger userControlRecTriggerClockAlarm1;
        private System.Windows.Forms.GroupBox groupBoxMediaPlayer;
        private UserControlVentuzScenesMediaPlayer userControlVentuzScenesMediaPlayer;
        private System.Windows.Forms.GroupBox groupBoxFreetext;
        private System.Windows.Forms.TextBox textBoxFreetext;
        private System.Windows.Forms.Button buttonShowFreetext;
    }
}