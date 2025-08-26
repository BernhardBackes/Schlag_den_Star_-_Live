namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.ShootingTimerForTwoSoloScore {

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
            this.labelRightPlayerBuzzerChannel = new System.Windows.Forms.Label();
            this.numericUpDownRightPlayerBuzzerChannel = new System.Windows.Forms.NumericUpDown();
            this.comboBoxIOUnit = new System.Windows.Forms.ComboBox();
            this.labelLeftPlayerBuzzerChannel = new System.Windows.Forms.Label();
            this.numericUpDownLeftPlayerBuzzerChannel = new System.Windows.Forms.NumericUpDown();
            this.labelIOUnit = new System.Windows.Forms.Label();
            this.radioButtonSourceTimer = new System.Windows.Forms.RadioButton();
            this.radioButtonSourceScore = new System.Windows.Forms.RadioButton();
            this.groupBoxTimeline = new System.Windows.Forms.GroupBox();
            this.textBoxRightPlayerTransponderCode = new System.Windows.Forms.TextBox();
            this.textBoxLeftPlayerTransponderCode = new System.Windows.Forms.TextBox();
            this.textBoxTimelineName = new System.Windows.Forms.TextBox();
            this.labelRightPlayerTransponderCode = new System.Windows.Forms.Label();
            this.labelLeftPlayerTransponderCode = new System.Windows.Forms.Label();
            this.labelTimelineName = new System.Windows.Forms.Label();
            this.groupBoxFinishMode = new System.Windows.Forms.GroupBox();
            this.radioButtonFinishModeCrossing = new System.Windows.Forms.RadioButton();
            this.radioButtonFinishModeReaching = new System.Windows.Forms.RadioButton();
            this.groupBoxShooting = new System.Windows.Forms.GroupBox();
            this.labelShootingHitsCount = new System.Windows.Forms.Label();
            this.numericUpDownShootingHitsCount = new System.Windows.Forms.NumericUpDown();
            this.labelShootingStyle = new System.Windows.Forms.Label();
            this.comboBoxShootingStyle = new System.Windows.Forms.ComboBox();
            this.labelShootingPositionY = new System.Windows.Forms.Label();
            this.labelShootingPositionX = new System.Windows.Forms.Label();
            this.numericUpDownShootingPositionY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownShootingPositionX = new System.Windows.Forms.NumericUpDown();
            this.groupBoxScore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionX)).BeginInit();
            this.groupBoxPreview.SuspendLayout();
            this.groupBoxBuzzer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRightPlayerBuzzerChannel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeftPlayerBuzzerChannel)).BeginInit();
            this.groupBoxTimeline.SuspendLayout();
            this.groupBoxFinishMode.SuspendLayout();
            this.groupBoxShooting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownShootingHitsCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownShootingPositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownShootingPositionX)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxScore
            // 
            this.groupBoxScore.Location = new System.Drawing.Point(819, 307);
            // 
            // comboBoxScoreStyle
            // 
            this.comboBoxScoreStyle.Size = new System.Drawing.Size(148, 26);
            // 
            // groupBoxPreview
            // 
            this.groupBoxPreview.Controls.Add(this.radioButtonSourceScore);
            this.groupBoxPreview.Controls.Add(this.radioButtonSourceTimer);
            this.groupBoxPreview.Location = new System.Drawing.Point(819, 419);
            this.groupBoxPreview.Controls.SetChildIndex(this.checkBoxShowSafeArea, 0);
            this.groupBoxPreview.Controls.SetChildIndex(this.radioButtonSourceTimer, 0);
            this.groupBoxPreview.Controls.SetChildIndex(this.radioButtonSourceScore, 0);
            // 
            // groupBoxBuzzer
            // 
            this.groupBoxBuzzer.Controls.Add(this.labelRightPlayerBuzzerChannel);
            this.groupBoxBuzzer.Controls.Add(this.numericUpDownRightPlayerBuzzerChannel);
            this.groupBoxBuzzer.Controls.Add(this.comboBoxIOUnit);
            this.groupBoxBuzzer.Controls.Add(this.labelLeftPlayerBuzzerChannel);
            this.groupBoxBuzzer.Controls.Add(this.numericUpDownLeftPlayerBuzzerChannel);
            this.groupBoxBuzzer.Controls.Add(this.labelIOUnit);
            this.groupBoxBuzzer.ForeColor = System.Drawing.Color.White;
            this.groupBoxBuzzer.Location = new System.Drawing.Point(819, 125);
            this.groupBoxBuzzer.Name = "groupBoxBuzzer";
            this.groupBoxBuzzer.Size = new System.Drawing.Size(534, 84);
            this.groupBoxBuzzer.TabIndex = 20;
            this.groupBoxBuzzer.TabStop = false;
            this.groupBoxBuzzer.Text = "buzzer";
            // 
            // labelRightPlayerBuzzerChannel
            // 
            this.labelRightPlayerBuzzerChannel.Location = new System.Drawing.Point(186, 51);
            this.labelRightPlayerBuzzerChannel.Name = "labelRightPlayerBuzzerChannel";
            this.labelRightPlayerBuzzerChannel.Size = new System.Drawing.Size(119, 24);
            this.labelRightPlayerBuzzerChannel.TabIndex = 7;
            this.labelRightPlayerBuzzerChannel.Text = "right channel:";
            this.labelRightPlayerBuzzerChannel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownRightPlayerBuzzerChannel
            // 
            this.numericUpDownRightPlayerBuzzerChannel.Location = new System.Drawing.Point(311, 52);
            this.numericUpDownRightPlayerBuzzerChannel.Name = "numericUpDownRightPlayerBuzzerChannel";
            this.numericUpDownRightPlayerBuzzerChannel.Size = new System.Drawing.Size(63, 24);
            this.numericUpDownRightPlayerBuzzerChannel.TabIndex = 6;
            this.numericUpDownRightPlayerBuzzerChannel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownRightPlayerBuzzerChannel.ValueChanged += new System.EventHandler(this.numericUpDownRightPlayerBuzzerChannel_ValueChanged);
            // 
            // comboBoxIOUnit
            // 
            this.comboBoxIOUnit.FormattingEnabled = true;
            this.comboBoxIOUnit.Location = new System.Drawing.Point(112, 19);
            this.comboBoxIOUnit.Name = "comboBoxIOUnit";
            this.comboBoxIOUnit.Size = new System.Drawing.Size(262, 26);
            this.comboBoxIOUnit.TabIndex = 5;
            this.comboBoxIOUnit.SelectedIndexChanged += new System.EventHandler(this.comboBoxIOUnit_TextChanged);
            // 
            // labelLeftPlayerBuzzerChannel
            // 
            this.labelLeftPlayerBuzzerChannel.Location = new System.Drawing.Point(6, 50);
            this.labelLeftPlayerBuzzerChannel.Name = "labelLeftPlayerBuzzerChannel";
            this.labelLeftPlayerBuzzerChannel.Size = new System.Drawing.Size(100, 24);
            this.labelLeftPlayerBuzzerChannel.TabIndex = 4;
            this.labelLeftPlayerBuzzerChannel.Text = "left channel:";
            this.labelLeftPlayerBuzzerChannel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownLeftPlayerBuzzerChannel
            // 
            this.numericUpDownLeftPlayerBuzzerChannel.Location = new System.Drawing.Point(112, 51);
            this.numericUpDownLeftPlayerBuzzerChannel.Name = "numericUpDownLeftPlayerBuzzerChannel";
            this.numericUpDownLeftPlayerBuzzerChannel.Size = new System.Drawing.Size(63, 24);
            this.numericUpDownLeftPlayerBuzzerChannel.TabIndex = 3;
            this.numericUpDownLeftPlayerBuzzerChannel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownLeftPlayerBuzzerChannel.ValueChanged += new System.EventHandler(this.numericUpDownLeftPlayerBuzzerChannel_ValueChanged);
            // 
            // labelIOUnit
            // 
            this.labelIOUnit.Location = new System.Drawing.Point(6, 20);
            this.labelIOUnit.Name = "labelIOUnit";
            this.labelIOUnit.Size = new System.Drawing.Size(100, 24);
            this.labelIOUnit.TabIndex = 2;
            this.labelIOUnit.Text = "IONet unit:";
            this.labelIOUnit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // radioButtonSourceTimer
            // 
            this.radioButtonSourceTimer.AutoSize = true;
            this.radioButtonSourceTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSourceTimer.Location = new System.Drawing.Point(122, 12);
            this.radioButtonSourceTimer.Name = "radioButtonSourceTimer";
            this.radioButtonSourceTimer.Size = new System.Drawing.Size(64, 22);
            this.radioButtonSourceTimer.TabIndex = 22;
            this.radioButtonSourceTimer.TabStop = true;
            this.radioButtonSourceTimer.Text = "timer";
            this.radioButtonSourceTimer.UseVisualStyleBackColor = true;
            this.radioButtonSourceTimer.CheckedChanged += new System.EventHandler(this.radioButtonSourceTimer_CheckedChanged);
            // 
            // radioButtonSourceScore
            // 
            this.radioButtonSourceScore.AutoSize = true;
            this.radioButtonSourceScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSourceScore.Location = new System.Drawing.Point(192, 12);
            this.radioButtonSourceScore.Name = "radioButtonSourceScore";
            this.radioButtonSourceScore.Size = new System.Drawing.Size(69, 22);
            this.radioButtonSourceScore.TabIndex = 21;
            this.radioButtonSourceScore.TabStop = true;
            this.radioButtonSourceScore.Text = "score";
            this.radioButtonSourceScore.UseVisualStyleBackColor = true;
            this.radioButtonSourceScore.CheckedChanged += new System.EventHandler(this.radioButtonSourceScore_CheckedChanged);
            // 
            // groupBoxTimeline
            // 
            this.groupBoxTimeline.Controls.Add(this.textBoxRightPlayerTransponderCode);
            this.groupBoxTimeline.Controls.Add(this.textBoxLeftPlayerTransponderCode);
            this.groupBoxTimeline.Controls.Add(this.textBoxTimelineName);
            this.groupBoxTimeline.Controls.Add(this.labelRightPlayerTransponderCode);
            this.groupBoxTimeline.Controls.Add(this.labelLeftPlayerTransponderCode);
            this.groupBoxTimeline.Controls.Add(this.labelTimelineName);
            this.groupBoxTimeline.ForeColor = System.Drawing.Color.White;
            this.groupBoxTimeline.Location = new System.Drawing.Point(819, 6);
            this.groupBoxTimeline.Name = "groupBoxTimeline";
            this.groupBoxTimeline.Size = new System.Drawing.Size(534, 113);
            this.groupBoxTimeline.TabIndex = 21;
            this.groupBoxTimeline.TabStop = false;
            this.groupBoxTimeline.Text = "timeline";
            // 
            // textBoxRightPlayerTransponderCode
            // 
            this.textBoxRightPlayerTransponderCode.Location = new System.Drawing.Point(157, 80);
            this.textBoxRightPlayerTransponderCode.Name = "textBoxRightPlayerTransponderCode";
            this.textBoxRightPlayerTransponderCode.Size = new System.Drawing.Size(225, 24);
            this.textBoxRightPlayerTransponderCode.TabIndex = 17;
            this.textBoxRightPlayerTransponderCode.TextChanged += new System.EventHandler(this.textBoxRightPlayerTransponderCode_TextChanged);
            // 
            // textBoxLeftPlayerTransponderCode
            // 
            this.textBoxLeftPlayerTransponderCode.Location = new System.Drawing.Point(157, 50);
            this.textBoxLeftPlayerTransponderCode.Name = "textBoxLeftPlayerTransponderCode";
            this.textBoxLeftPlayerTransponderCode.Size = new System.Drawing.Size(225, 24);
            this.textBoxLeftPlayerTransponderCode.TabIndex = 16;
            this.textBoxLeftPlayerTransponderCode.TextChanged += new System.EventHandler(this.textBoxLeftPlayerTransponderCode_TextChanged);
            // 
            // textBoxTimelineName
            // 
            this.textBoxTimelineName.Location = new System.Drawing.Point(157, 20);
            this.textBoxTimelineName.Name = "textBoxTimelineName";
            this.textBoxTimelineName.Size = new System.Drawing.Size(225, 24);
            this.textBoxTimelineName.TabIndex = 15;
            this.textBoxTimelineName.TextChanged += new System.EventHandler(this.textBoxTimelineName_TextChanged);
            // 
            // labelRightPlayerTransponderCode
            // 
            this.labelRightPlayerTransponderCode.Location = new System.Drawing.Point(6, 80);
            this.labelRightPlayerTransponderCode.Name = "labelRightPlayerTransponderCode";
            this.labelRightPlayerTransponderCode.Size = new System.Drawing.Size(145, 24);
            this.labelRightPlayerTransponderCode.TabIndex = 7;
            this.labelRightPlayerTransponderCode.Text = "right transponder:";
            this.labelRightPlayerTransponderCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelLeftPlayerTransponderCode
            // 
            this.labelLeftPlayerTransponderCode.Location = new System.Drawing.Point(6, 50);
            this.labelLeftPlayerTransponderCode.Name = "labelLeftPlayerTransponderCode";
            this.labelLeftPlayerTransponderCode.Size = new System.Drawing.Size(145, 24);
            this.labelLeftPlayerTransponderCode.TabIndex = 4;
            this.labelLeftPlayerTransponderCode.Text = "left transponder:";
            this.labelLeftPlayerTransponderCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelTimelineName
            // 
            this.labelTimelineName.Location = new System.Drawing.Point(6, 20);
            this.labelTimelineName.Name = "labelTimelineName";
            this.labelTimelineName.Size = new System.Drawing.Size(145, 24);
            this.labelTimelineName.TabIndex = 2;
            this.labelTimelineName.Text = "name:";
            this.labelTimelineName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBoxFinishMode
            // 
            this.groupBoxFinishMode.Controls.Add(this.radioButtonFinishModeCrossing);
            this.groupBoxFinishMode.Controls.Add(this.radioButtonFinishModeReaching);
            this.groupBoxFinishMode.ForeColor = System.Drawing.Color.White;
            this.groupBoxFinishMode.Location = new System.Drawing.Point(819, 363);
            this.groupBoxFinishMode.Name = "groupBoxFinishMode";
            this.groupBoxFinishMode.Size = new System.Drawing.Size(534, 50);
            this.groupBoxFinishMode.TabIndex = 20;
            this.groupBoxFinishMode.TabStop = false;
            this.groupBoxFinishMode.Text = "finish mode";
            // 
            // radioButtonFinishModeCrossing
            // 
            this.radioButtonFinishModeCrossing.AutoSize = true;
            this.radioButtonFinishModeCrossing.Location = new System.Drawing.Point(122, 19);
            this.radioButtonFinishModeCrossing.Name = "radioButtonFinishModeCrossing";
            this.radioButtonFinishModeCrossing.Size = new System.Drawing.Size(91, 22);
            this.radioButtonFinishModeCrossing.TabIndex = 1;
            this.radioButtonFinishModeCrossing.TabStop = true;
            this.radioButtonFinishModeCrossing.Text = "crossing";
            this.radioButtonFinishModeCrossing.UseVisualStyleBackColor = true;
            this.radioButtonFinishModeCrossing.CheckedChanged += new System.EventHandler(this.radioButtonFinishModeCrossing_CheckedChanged);
            // 
            // radioButtonFinishModeReaching
            // 
            this.radioButtonFinishModeReaching.AutoSize = true;
            this.radioButtonFinishModeReaching.Location = new System.Drawing.Point(11, 19);
            this.radioButtonFinishModeReaching.Name = "radioButtonFinishModeReaching";
            this.radioButtonFinishModeReaching.Size = new System.Drawing.Size(90, 22);
            this.radioButtonFinishModeReaching.TabIndex = 0;
            this.radioButtonFinishModeReaching.TabStop = true;
            this.radioButtonFinishModeReaching.Text = "reaching";
            this.radioButtonFinishModeReaching.UseVisualStyleBackColor = true;
            this.radioButtonFinishModeReaching.CheckedChanged += new System.EventHandler(this.radioButtonFinishModeReaching_CheckedChanged);
            // 
            // groupBoxShooting
            // 
            this.groupBoxShooting.Controls.Add(this.labelShootingHitsCount);
            this.groupBoxShooting.Controls.Add(this.numericUpDownShootingHitsCount);
            this.groupBoxShooting.Controls.Add(this.labelShootingStyle);
            this.groupBoxShooting.Controls.Add(this.comboBoxShootingStyle);
            this.groupBoxShooting.Controls.Add(this.labelShootingPositionY);
            this.groupBoxShooting.Controls.Add(this.labelShootingPositionX);
            this.groupBoxShooting.Controls.Add(this.numericUpDownShootingPositionY);
            this.groupBoxShooting.Controls.Add(this.numericUpDownShootingPositionX);
            this.groupBoxShooting.ForeColor = System.Drawing.Color.White;
            this.groupBoxShooting.Location = new System.Drawing.Point(819, 215);
            this.groupBoxShooting.Name = "groupBoxShooting";
            this.groupBoxShooting.Size = new System.Drawing.Size(534, 86);
            this.groupBoxShooting.TabIndex = 49;
            this.groupBoxShooting.TabStop = false;
            this.groupBoxShooting.Text = "shooting";
            // 
            // labelShootingHitsCount
            // 
            this.labelShootingHitsCount.Location = new System.Drawing.Point(274, 49);
            this.labelShootingHitsCount.Name = "labelShootingHitsCount";
            this.labelShootingHitsCount.Size = new System.Drawing.Size(100, 24);
            this.labelShootingHitsCount.TabIndex = 7;
            this.labelShootingHitsCount.Text = "hits count:";
            this.labelShootingHitsCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownShootingHitsCount
            // 
            this.numericUpDownShootingHitsCount.Location = new System.Drawing.Point(380, 49);
            this.numericUpDownShootingHitsCount.Name = "numericUpDownShootingHitsCount";
            this.numericUpDownShootingHitsCount.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownShootingHitsCount.TabIndex = 6;
            this.numericUpDownShootingHitsCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownShootingHitsCount.ValueChanged += new System.EventHandler(this.numericUpDownShootingHitsCount_ValueChanged);
            // 
            // labelShootingStyle
            // 
            this.labelShootingStyle.Location = new System.Drawing.Point(318, 18);
            this.labelShootingStyle.Name = "labelShootingStyle";
            this.labelShootingStyle.Size = new System.Drawing.Size(56, 24);
            this.labelShootingStyle.TabIndex = 5;
            this.labelShootingStyle.Text = "style:";
            this.labelShootingStyle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxShootingStyle
            // 
            this.comboBoxShootingStyle.FormattingEnabled = true;
            this.comboBoxShootingStyle.Location = new System.Drawing.Point(380, 17);
            this.comboBoxShootingStyle.Name = "comboBoxShootingStyle";
            this.comboBoxShootingStyle.Size = new System.Drawing.Size(148, 26);
            this.comboBoxShootingStyle.TabIndex = 4;
            this.comboBoxShootingStyle.SelectedIndexChanged += new System.EventHandler(this.comboBoxShootingStyle_SelectedIndexChanged);
            // 
            // labelShootingPositionY
            // 
            this.labelShootingPositionY.Location = new System.Drawing.Point(197, 18);
            this.labelShootingPositionY.Name = "labelShootingPositionY";
            this.labelShootingPositionY.Size = new System.Drawing.Size(30, 24);
            this.labelShootingPositionY.TabIndex = 3;
            this.labelShootingPositionY.Text = "y:";
            this.labelShootingPositionY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelShootingPositionX
            // 
            this.labelShootingPositionX.Location = new System.Drawing.Point(6, 18);
            this.labelShootingPositionX.Name = "labelShootingPositionX";
            this.labelShootingPositionX.Size = new System.Drawing.Size(100, 24);
            this.labelShootingPositionX.TabIndex = 2;
            this.labelShootingPositionX.Text = "position.x:";
            this.labelShootingPositionX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownShootingPositionY
            // 
            this.numericUpDownShootingPositionY.Location = new System.Drawing.Point(233, 18);
            this.numericUpDownShootingPositionY.Name = "numericUpDownShootingPositionY";
            this.numericUpDownShootingPositionY.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownShootingPositionY.TabIndex = 1;
            this.numericUpDownShootingPositionY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownShootingPositionY.ValueChanged += new System.EventHandler(this.numericUpDownShootingPositionY_ValueChanged);
            // 
            // numericUpDownShootingPositionX
            // 
            this.numericUpDownShootingPositionX.Location = new System.Drawing.Point(112, 18);
            this.numericUpDownShootingPositionX.Name = "numericUpDownShootingPositionX";
            this.numericUpDownShootingPositionX.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownShootingPositionX.TabIndex = 0;
            this.numericUpDownShootingPositionX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownShootingPositionX.ValueChanged += new System.EventHandler(this.numericUpDownShootingPositionX_ValueChanged);
            // 
            // UserControlContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxShooting);
            this.Controls.Add(this.groupBoxFinishMode);
            this.Controls.Add(this.groupBoxTimeline);
            this.Controls.Add(this.groupBoxBuzzer);
            this.Name = "UserControlContent";
            this.Controls.SetChildIndex(this.groupBoxPreview, 0);
            this.Controls.SetChildIndex(this.labelGameClass, 0);
            this.Controls.SetChildIndex(this.groupBoxScore, 0);
            this.Controls.SetChildIndex(this.groupBoxBuzzer, 0);
            this.Controls.SetChildIndex(this.groupBoxTimeline, 0);
            this.Controls.SetChildIndex(this.groupBoxFinishMode, 0);
            this.Controls.SetChildIndex(this.groupBoxShooting, 0);
            this.groupBoxScore.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionX)).EndInit();
            this.groupBoxPreview.ResumeLayout(false);
            this.groupBoxPreview.PerformLayout();
            this.groupBoxBuzzer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRightPlayerBuzzerChannel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeftPlayerBuzzerChannel)).EndInit();
            this.groupBoxTimeline.ResumeLayout(false);
            this.groupBoxTimeline.PerformLayout();
            this.groupBoxFinishMode.ResumeLayout(false);
            this.groupBoxFinishMode.PerformLayout();
            this.groupBoxShooting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownShootingHitsCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownShootingPositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownShootingPositionX)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxBuzzer;
        private System.Windows.Forms.Label labelRightPlayerBuzzerChannel;
        private System.Windows.Forms.NumericUpDown numericUpDownRightPlayerBuzzerChannel;
        private System.Windows.Forms.ComboBox comboBoxIOUnit;
        private System.Windows.Forms.Label labelLeftPlayerBuzzerChannel;
        private System.Windows.Forms.NumericUpDown numericUpDownLeftPlayerBuzzerChannel;
        private System.Windows.Forms.Label labelIOUnit;
        private System.Windows.Forms.RadioButton radioButtonSourceScore;
        private System.Windows.Forms.RadioButton radioButtonSourceTimer;
        private System.Windows.Forms.GroupBox groupBoxTimeline;
        private System.Windows.Forms.TextBox textBoxRightPlayerTransponderCode;
        private System.Windows.Forms.TextBox textBoxLeftPlayerTransponderCode;
        private System.Windows.Forms.TextBox textBoxTimelineName;
        private System.Windows.Forms.Label labelRightPlayerTransponderCode;
        private System.Windows.Forms.Label labelLeftPlayerTransponderCode;
        private System.Windows.Forms.Label labelTimelineName;
        protected System.Windows.Forms.GroupBox groupBoxFinishMode;
        private System.Windows.Forms.RadioButton radioButtonFinishModeCrossing;
        private System.Windows.Forms.RadioButton radioButtonFinishModeReaching;
        protected System.Windows.Forms.GroupBox groupBoxShooting;
        protected System.Windows.Forms.Label labelShootingHitsCount;
        protected System.Windows.Forms.NumericUpDown numericUpDownShootingHitsCount;
        protected System.Windows.Forms.Label labelShootingStyle;
        protected System.Windows.Forms.ComboBox comboBoxShootingStyle;
        protected System.Windows.Forms.Label labelShootingPositionY;
        protected System.Windows.Forms.Label labelShootingPositionX;
        protected System.Windows.Forms.NumericUpDown numericUpDownShootingPositionY;
        protected System.Windows.Forms.NumericUpDown numericUpDownShootingPositionX;
    }
}
