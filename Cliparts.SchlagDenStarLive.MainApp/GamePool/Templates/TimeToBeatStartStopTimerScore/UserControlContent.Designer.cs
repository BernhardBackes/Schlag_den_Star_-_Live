namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimeToBeatStartStopTimerScore {

    partial class UserControlContent {
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
            this.groupBoxBuzzer = new System.Windows.Forms.GroupBox();
            this.comboBoxIOUnit = new System.Windows.Forms.ComboBox();
            this.labelIOUnit = new System.Windows.Forms.Label();
            this.labelBuzzerChannel = new System.Windows.Forms.Label();
            this.numericUpDownBuzzerChannel = new System.Windows.Forms.NumericUpDown();
            this.radioButtonSourceFullscreen = new System.Windows.Forms.RadioButton();
            this.radioButtonSourceInsert = new System.Windows.Forms.RadioButton();
            this.groupBoxTimer = new System.Windows.Forms.GroupBox();
            this.labelTimerAlarmTime2Text = new System.Windows.Forms.Label();
            this.labelTimerStopTimeText = new System.Windows.Forms.Label();
            this.labelTimerAlarmTime1Text = new System.Windows.Forms.Label();
            this.labelTimerStartTimeText = new System.Windows.Forms.Label();
            this.labelTimerAlarmTime2 = new System.Windows.Forms.Label();
            this.numericUpDownTimerAlarmTime2 = new System.Windows.Forms.NumericUpDown();
            this.labelTimerStopTime = new System.Windows.Forms.Label();
            this.numericUpDownTimerStopTime = new System.Windows.Forms.NumericUpDown();
            this.labelTimerAlarmTime1 = new System.Windows.Forms.Label();
            this.numericUpDownTimerAlarmTime1 = new System.Windows.Forms.NumericUpDown();
            this.labelTimerStartTime = new System.Windows.Forms.Label();
            this.numericUpDownTimerStartTime = new System.Windows.Forms.NumericUpDown();
            this.comboBoxContactMode = new System.Windows.Forms.ComboBox();
            this.labelContactMode = new System.Windows.Forms.Label();
            this.labelContactModeDescription = new System.Windows.Forms.Label();
            this.groupBoxScore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionX)).BeginInit();
            this.groupBoxPreview.SuspendLayout();
            this.groupBoxBuzzer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBuzzerChannel)).BeginInit();
            this.groupBoxTimer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimerAlarmTime2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimerStopTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimerAlarmTime1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimerStartTime)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxScore
            // 
            this.groupBoxScore.Location = new System.Drawing.Point(819, 93);
            // 
            // comboBoxScoreStyle
            // 
            this.comboBoxScoreStyle.Size = new System.Drawing.Size(148, 26);
            // 
            // groupBoxPreview
            // 
            this.groupBoxPreview.Controls.Add(this.radioButtonSourceFullscreen);
            this.groupBoxPreview.Controls.Add(this.radioButtonSourceInsert);
            this.groupBoxPreview.Controls.SetChildIndex(this.checkBoxShowSafeArea, 0);
            this.groupBoxPreview.Controls.SetChildIndex(this.radioButtonSourceInsert, 0);
            this.groupBoxPreview.Controls.SetChildIndex(this.radioButtonSourceFullscreen, 0);
            // 
            // groupBoxBuzzer
            // 
            this.groupBoxBuzzer.Controls.Add(this.comboBoxIOUnit);
            this.groupBoxBuzzer.Controls.Add(this.labelIOUnit);
            this.groupBoxBuzzer.Controls.Add(this.labelBuzzerChannel);
            this.groupBoxBuzzer.Controls.Add(this.numericUpDownBuzzerChannel);
            this.groupBoxBuzzer.ForeColor = System.Drawing.Color.White;
            this.groupBoxBuzzer.Location = new System.Drawing.Point(819, 149);
            this.groupBoxBuzzer.Name = "groupBoxBuzzer";
            this.groupBoxBuzzer.Size = new System.Drawing.Size(534, 50);
            this.groupBoxBuzzer.TabIndex = 20;
            this.groupBoxBuzzer.TabStop = false;
            this.groupBoxBuzzer.Text = "buzzer";
            // 
            // comboBoxIOUnit
            // 
            this.comboBoxIOUnit.FormattingEnabled = true;
            this.comboBoxIOUnit.Location = new System.Drawing.Point(112, 17);
            this.comboBoxIOUnit.Name = "comboBoxIOUnit";
            this.comboBoxIOUnit.Size = new System.Drawing.Size(262, 26);
            this.comboBoxIOUnit.TabIndex = 7;
            this.comboBoxIOUnit.SelectedIndexChanged += new System.EventHandler(this.comboBoxIOUnit_TextChanged);
            // 
            // labelIOUnit
            // 
            this.labelIOUnit.Location = new System.Drawing.Point(6, 18);
            this.labelIOUnit.Name = "labelIOUnit";
            this.labelIOUnit.Size = new System.Drawing.Size(100, 24);
            this.labelIOUnit.TabIndex = 6;
            this.labelIOUnit.Text = "IONet unit:";
            this.labelIOUnit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelBuzzerChannel
            // 
            this.labelBuzzerChannel.Location = new System.Drawing.Point(359, 18);
            this.labelBuzzerChannel.Name = "labelBuzzerChannel";
            this.labelBuzzerChannel.Size = new System.Drawing.Size(100, 24);
            this.labelBuzzerChannel.TabIndex = 4;
            this.labelBuzzerChannel.Text = "channel:";
            this.labelBuzzerChannel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownBuzzerChannel
            // 
            this.numericUpDownBuzzerChannel.Location = new System.Drawing.Point(465, 18);
            this.numericUpDownBuzzerChannel.Name = "numericUpDownBuzzerChannel";
            this.numericUpDownBuzzerChannel.Size = new System.Drawing.Size(63, 24);
            this.numericUpDownBuzzerChannel.TabIndex = 3;
            this.numericUpDownBuzzerChannel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownBuzzerChannel.ValueChanged += new System.EventHandler(this.numericUpDownBuzzerChannel_ValueChanged);
            // 
            // radioButtonSourceFullscreen
            // 
            this.radioButtonSourceFullscreen.AutoSize = true;
            this.radioButtonSourceFullscreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSourceFullscreen.Location = new System.Drawing.Point(189, 14);
            this.radioButtonSourceFullscreen.Name = "radioButtonSourceFullscreen";
            this.radioButtonSourceFullscreen.Size = new System.Drawing.Size(99, 22);
            this.radioButtonSourceFullscreen.TabIndex = 23;
            this.radioButtonSourceFullscreen.TabStop = true;
            this.radioButtonSourceFullscreen.Text = "fullscreen";
            this.radioButtonSourceFullscreen.UseVisualStyleBackColor = true;
            this.radioButtonSourceFullscreen.CheckedChanged += new System.EventHandler(this.radioButtonSourceFullscreen_CheckedChanged);
            // 
            // radioButtonSourceInsert
            // 
            this.radioButtonSourceInsert.AutoSize = true;
            this.radioButtonSourceInsert.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSourceInsert.Location = new System.Drawing.Point(114, 14);
            this.radioButtonSourceInsert.Name = "radioButtonSourceInsert";
            this.radioButtonSourceInsert.Size = new System.Drawing.Size(68, 22);
            this.radioButtonSourceInsert.TabIndex = 22;
            this.radioButtonSourceInsert.TabStop = true;
            this.radioButtonSourceInsert.Text = "insert";
            this.radioButtonSourceInsert.UseVisualStyleBackColor = true;
            this.radioButtonSourceInsert.CheckedChanged += new System.EventHandler(this.radioButtonSourceInsert_CheckedChanged);
            // 
            // groupBoxTimer
            // 
            this.groupBoxTimer.Controls.Add(this.labelTimerAlarmTime2Text);
            this.groupBoxTimer.Controls.Add(this.labelTimerStopTimeText);
            this.groupBoxTimer.Controls.Add(this.labelTimerAlarmTime1Text);
            this.groupBoxTimer.Controls.Add(this.labelTimerStartTimeText);
            this.groupBoxTimer.Controls.Add(this.labelTimerAlarmTime2);
            this.groupBoxTimer.Controls.Add(this.numericUpDownTimerAlarmTime2);
            this.groupBoxTimer.Controls.Add(this.labelTimerStopTime);
            this.groupBoxTimer.Controls.Add(this.numericUpDownTimerStopTime);
            this.groupBoxTimer.Controls.Add(this.labelTimerAlarmTime1);
            this.groupBoxTimer.Controls.Add(this.numericUpDownTimerAlarmTime1);
            this.groupBoxTimer.Controls.Add(this.labelTimerStartTime);
            this.groupBoxTimer.Controls.Add(this.numericUpDownTimerStartTime);
            this.groupBoxTimer.ForeColor = System.Drawing.Color.White;
            this.groupBoxTimer.Location = new System.Drawing.Point(819, 6);
            this.groupBoxTimer.Name = "groupBoxTimer";
            this.groupBoxTimer.Size = new System.Drawing.Size(534, 81);
            this.groupBoxTimer.TabIndex = 23;
            this.groupBoxTimer.TabStop = false;
            this.groupBoxTimer.Text = "timer";
            // 
            // labelTimerAlarmTime2Text
            // 
            this.labelTimerAlarmTime2Text.Location = new System.Drawing.Point(427, 48);
            this.labelTimerAlarmTime2Text.Name = "labelTimerAlarmTime2Text";
            this.labelTimerAlarmTime2Text.Size = new System.Drawing.Size(56, 24);
            this.labelTimerAlarmTime2Text.TabIndex = 17;
            this.labelTimerAlarmTime2Text.Text = "00:00";
            this.labelTimerAlarmTime2Text.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelTimerStopTimeText
            // 
            this.labelTimerStopTimeText.Location = new System.Drawing.Point(425, 18);
            this.labelTimerStopTimeText.Name = "labelTimerStopTimeText";
            this.labelTimerStopTimeText.Size = new System.Drawing.Size(56, 24);
            this.labelTimerStopTimeText.TabIndex = 16;
            this.labelTimerStopTimeText.Text = "00:00";
            this.labelTimerStopTimeText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelTimerAlarmTime1Text
            // 
            this.labelTimerAlarmTime1Text.Location = new System.Drawing.Point(197, 48);
            this.labelTimerAlarmTime1Text.Name = "labelTimerAlarmTime1Text";
            this.labelTimerAlarmTime1Text.Size = new System.Drawing.Size(56, 24);
            this.labelTimerAlarmTime1Text.TabIndex = 15;
            this.labelTimerAlarmTime1Text.Text = "00:00";
            this.labelTimerAlarmTime1Text.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelTimerStartTimeText
            // 
            this.labelTimerStartTimeText.Location = new System.Drawing.Point(195, 18);
            this.labelTimerStartTimeText.Name = "labelTimerStartTimeText";
            this.labelTimerStartTimeText.Size = new System.Drawing.Size(56, 24);
            this.labelTimerStartTimeText.TabIndex = 14;
            this.labelTimerStartTimeText.Text = "00:00";
            this.labelTimerStartTimeText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelTimerAlarmTime2
            // 
            this.labelTimerAlarmTime2.Location = new System.Drawing.Point(256, 48);
            this.labelTimerAlarmTime2.Name = "labelTimerAlarmTime2";
            this.labelTimerAlarmTime2.Size = new System.Drawing.Size(80, 24);
            this.labelTimerAlarmTime2.TabIndex = 13;
            this.labelTimerAlarmTime2.Text = "alarm 2:";
            this.labelTimerAlarmTime2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownTimerAlarmTime2
            // 
            this.numericUpDownTimerAlarmTime2.Location = new System.Drawing.Point(342, 48);
            this.numericUpDownTimerAlarmTime2.Name = "numericUpDownTimerAlarmTime2";
            this.numericUpDownTimerAlarmTime2.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownTimerAlarmTime2.TabIndex = 12;
            this.numericUpDownTimerAlarmTime2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownTimerAlarmTime2.ValueChanged += new System.EventHandler(this.numericUpDownTimerAlarmTime2_ValueChanged);
            // 
            // labelTimerStopTime
            // 
            this.labelTimerStopTime.Location = new System.Drawing.Point(256, 18);
            this.labelTimerStopTime.Name = "labelTimerStopTime";
            this.labelTimerStopTime.Size = new System.Drawing.Size(80, 24);
            this.labelTimerStopTime.TabIndex = 11;
            this.labelTimerStopTime.Text = "stop:";
            this.labelTimerStopTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownTimerStopTime
            // 
            this.numericUpDownTimerStopTime.Location = new System.Drawing.Point(341, 18);
            this.numericUpDownTimerStopTime.Name = "numericUpDownTimerStopTime";
            this.numericUpDownTimerStopTime.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownTimerStopTime.TabIndex = 10;
            this.numericUpDownTimerStopTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownTimerStopTime.ValueChanged += new System.EventHandler(this.numericUpDownTimerStopTime_ValueChanged);
            // 
            // labelTimerAlarmTime1
            // 
            this.labelTimerAlarmTime1.Location = new System.Drawing.Point(6, 48);
            this.labelTimerAlarmTime1.Name = "labelTimerAlarmTime1";
            this.labelTimerAlarmTime1.Size = new System.Drawing.Size(100, 24);
            this.labelTimerAlarmTime1.TabIndex = 9;
            this.labelTimerAlarmTime1.Text = "alarm 1:";
            this.labelTimerAlarmTime1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownTimerAlarmTime1
            // 
            this.numericUpDownTimerAlarmTime1.Location = new System.Drawing.Point(112, 48);
            this.numericUpDownTimerAlarmTime1.Name = "numericUpDownTimerAlarmTime1";
            this.numericUpDownTimerAlarmTime1.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownTimerAlarmTime1.TabIndex = 8;
            this.numericUpDownTimerAlarmTime1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownTimerAlarmTime1.ValueChanged += new System.EventHandler(this.numericUpDownTimerAlarmTime1_ValueChanged);
            // 
            // labelTimerStartTime
            // 
            this.labelTimerStartTime.Location = new System.Drawing.Point(6, 18);
            this.labelTimerStartTime.Name = "labelTimerStartTime";
            this.labelTimerStartTime.Size = new System.Drawing.Size(100, 24);
            this.labelTimerStartTime.TabIndex = 7;
            this.labelTimerStartTime.Text = "start:";
            this.labelTimerStartTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownTimerStartTime
            // 
            this.numericUpDownTimerStartTime.Location = new System.Drawing.Point(111, 18);
            this.numericUpDownTimerStartTime.Name = "numericUpDownTimerStartTime";
            this.numericUpDownTimerStartTime.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownTimerStartTime.TabIndex = 6;
            this.numericUpDownTimerStartTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownTimerStartTime.ValueChanged += new System.EventHandler(this.numericUpDownTimerStartTime_ValueChanged);
            // 
            // comboBoxContactMode
            // 
            this.comboBoxContactMode.FormattingEnabled = true;
            this.comboBoxContactMode.Location = new System.Drawing.Point(930, 314);
            this.comboBoxContactMode.Name = "comboBoxContactMode";
            this.comboBoxContactMode.Size = new System.Drawing.Size(174, 26);
            this.comboBoxContactMode.TabIndex = 24;
            this.comboBoxContactMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxContactMode_SelectedIndexChanged);
            // 
            // labelContactMode
            // 
            this.labelContactMode.Location = new System.Drawing.Point(813, 315);
            this.labelContactMode.Name = "labelContactMode";
            this.labelContactMode.Size = new System.Drawing.Size(116, 24);
            this.labelContactMode.TabIndex = 8;
            this.labelContactMode.Text = "contact mode:";
            this.labelContactMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelContactModeDescription
            // 
            this.labelContactModeDescription.AutoSize = true;
            this.labelContactModeDescription.Location = new System.Drawing.Point(930, 343);
            this.labelContactModeDescription.Name = "labelContactModeDescription";
            this.labelContactModeDescription.Size = new System.Drawing.Size(253, 36);
            this.labelContactModeDescription.TabIndex = 25;
            this.labelContactModeDescription.Text = "Open: contact is valid if open\r\nClosed: contact is valid if closed";
            // 
            // UserControlContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelContactModeDescription);
            this.Controls.Add(this.labelContactMode);
            this.Controls.Add(this.comboBoxContactMode);
            this.Controls.Add(this.groupBoxTimer);
            this.Controls.Add(this.groupBoxBuzzer);
            this.Name = "UserControlContent";
            this.Controls.SetChildIndex(this.groupBoxPreview, 0);
            this.Controls.SetChildIndex(this.labelGameClass, 0);
            this.Controls.SetChildIndex(this.groupBoxScore, 0);
            this.Controls.SetChildIndex(this.groupBoxBuzzer, 0);
            this.Controls.SetChildIndex(this.groupBoxTimer, 0);
            this.Controls.SetChildIndex(this.comboBoxContactMode, 0);
            this.Controls.SetChildIndex(this.labelContactMode, 0);
            this.Controls.SetChildIndex(this.labelContactModeDescription, 0);
            this.groupBoxScore.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionX)).EndInit();
            this.groupBoxPreview.ResumeLayout(false);
            this.groupBoxPreview.PerformLayout();
            this.groupBoxBuzzer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBuzzerChannel)).EndInit();
            this.groupBoxTimer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimerAlarmTime2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimerStopTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimerAlarmTime1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimerStartTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxBuzzer;
        private System.Windows.Forms.Label labelBuzzerChannel;
        private System.Windows.Forms.NumericUpDown numericUpDownBuzzerChannel;
        private System.Windows.Forms.ComboBox comboBoxIOUnit;
        private System.Windows.Forms.Label labelIOUnit;
        private System.Windows.Forms.RadioButton radioButtonSourceFullscreen;
        private System.Windows.Forms.RadioButton radioButtonSourceInsert;
        protected System.Windows.Forms.GroupBox groupBoxTimer;
        protected System.Windows.Forms.Label labelTimerAlarmTime2Text;
        protected System.Windows.Forms.Label labelTimerStopTimeText;
        protected System.Windows.Forms.Label labelTimerAlarmTime1Text;
        protected System.Windows.Forms.Label labelTimerStartTimeText;
        protected System.Windows.Forms.Label labelTimerAlarmTime2;
        protected System.Windows.Forms.NumericUpDown numericUpDownTimerAlarmTime2;
        protected System.Windows.Forms.Label labelTimerStopTime;
        protected System.Windows.Forms.NumericUpDown numericUpDownTimerStopTime;
        protected System.Windows.Forms.Label labelTimerAlarmTime1;
        protected System.Windows.Forms.NumericUpDown numericUpDownTimerAlarmTime1;
        protected System.Windows.Forms.Label labelTimerStartTime;
        protected System.Windows.Forms.NumericUpDown numericUpDownTimerStartTime;
        private System.Windows.Forms.ComboBox comboBoxContactMode;
        private System.Windows.Forms.Label labelContactMode;
        private System.Windows.Forms.Label labelContactModeDescription;
    }
}
