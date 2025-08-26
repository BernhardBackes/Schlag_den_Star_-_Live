namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TwoTimersStartStopContinue {
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
            this.numericUpDownTimerPositionX = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownTimerPositionY = new System.Windows.Forms.NumericUpDown();
            this.labelTimerPositionX = new System.Windows.Forms.Label();
            this.labelTimerPositionY = new System.Windows.Forms.Label();
            this.groupBoxTimer = new System.Windows.Forms.GroupBox();
            this.labelTimelineName = new System.Windows.Forms.Label();
            this.labelLeftPlayerTransponderCode = new System.Windows.Forms.Label();
            this.labelRightPlayerTransponderCode = new System.Windows.Forms.Label();
            this.groupBoxTimeline = new System.Windows.Forms.GroupBox();
            this.textBoxRightPlayerTransponderCode = new System.Windows.Forms.TextBox();
            this.textBoxLeftPlayerTransponderCode = new System.Windows.Forms.TextBox();
            this.textBoxTimelineName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimerPositionX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimerPositionY)).BeginInit();
            this.groupBoxTimer.SuspendLayout();
            this.groupBoxTimeline.SuspendLayout();
            this.SuspendLayout();
            // 
            // numericUpDownTimerPositionX
            // 
            this.numericUpDownTimerPositionX.Location = new System.Drawing.Point(112, 18);
            this.numericUpDownTimerPositionX.Name = "numericUpDownTimerPositionX";
            this.numericUpDownTimerPositionX.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownTimerPositionX.TabIndex = 0;
            this.numericUpDownTimerPositionX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownTimerPositionX.ValueChanged += new System.EventHandler(this.numericUpDownTimerPositionX_ValueChanged);
            // 
            // numericUpDownTimerPositionY
            // 
            this.numericUpDownTimerPositionY.Location = new System.Drawing.Point(233, 18);
            this.numericUpDownTimerPositionY.Name = "numericUpDownTimerPositionY";
            this.numericUpDownTimerPositionY.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownTimerPositionY.TabIndex = 1;
            this.numericUpDownTimerPositionY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownTimerPositionY.ValueChanged += new System.EventHandler(this.numericUpDownTimerPositionY_ValueChanged);
            // 
            // labelTimerPositionX
            // 
            this.labelTimerPositionX.Location = new System.Drawing.Point(6, 18);
            this.labelTimerPositionX.Name = "labelTimerPositionX";
            this.labelTimerPositionX.Size = new System.Drawing.Size(100, 24);
            this.labelTimerPositionX.TabIndex = 2;
            this.labelTimerPositionX.Text = "position.x:";
            this.labelTimerPositionX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelTimerPositionY
            // 
            this.labelTimerPositionY.Location = new System.Drawing.Point(197, 18);
            this.labelTimerPositionY.Name = "labelTimerPositionY";
            this.labelTimerPositionY.Size = new System.Drawing.Size(30, 24);
            this.labelTimerPositionY.TabIndex = 3;
            this.labelTimerPositionY.Text = "y:";
            this.labelTimerPositionY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBoxTimer
            // 
            this.groupBoxTimer.Controls.Add(this.labelTimerPositionY);
            this.groupBoxTimer.Controls.Add(this.labelTimerPositionX);
            this.groupBoxTimer.Controls.Add(this.numericUpDownTimerPositionY);
            this.groupBoxTimer.Controls.Add(this.numericUpDownTimerPositionX);
            this.groupBoxTimer.ForeColor = System.Drawing.Color.White;
            this.groupBoxTimer.Location = new System.Drawing.Point(819, 125);
            this.groupBoxTimer.Name = "groupBoxTimer";
            this.groupBoxTimer.Size = new System.Drawing.Size(534, 50);
            this.groupBoxTimer.TabIndex = 14;
            this.groupBoxTimer.TabStop = false;
            this.groupBoxTimer.Text = "timer";
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
            // labelLeftPlayerTransponderCode
            // 
            this.labelLeftPlayerTransponderCode.Location = new System.Drawing.Point(6, 50);
            this.labelLeftPlayerTransponderCode.Name = "labelLeftPlayerTransponderCode";
            this.labelLeftPlayerTransponderCode.Size = new System.Drawing.Size(145, 24);
            this.labelLeftPlayerTransponderCode.TabIndex = 4;
            this.labelLeftPlayerTransponderCode.Text = "left transponder:";
            this.labelLeftPlayerTransponderCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.groupBoxTimeline.TabIndex = 13;
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
            // UserControlContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxTimer);
            this.Controls.Add(this.groupBoxTimeline);
            this.Name = "UserControlContent";
            this.Controls.SetChildIndex(this.groupBoxTimeline, 0);
            this.Controls.SetChildIndex(this.groupBoxTimer, 0);
            this.Controls.SetChildIndex(this.labelGameClass, 0);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimerPositionX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimerPositionY)).EndInit();
            this.groupBoxTimer.ResumeLayout(false);
            this.groupBoxTimeline.ResumeLayout(false);
            this.groupBoxTimeline.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDownTimerPositionX;
        private System.Windows.Forms.NumericUpDown numericUpDownTimerPositionY;
        private System.Windows.Forms.Label labelTimerPositionX;
        private System.Windows.Forms.Label labelTimerPositionY;
        private System.Windows.Forms.GroupBox groupBoxTimer;
        private System.Windows.Forms.Label labelTimelineName;
        private System.Windows.Forms.Label labelLeftPlayerTransponderCode;
        private System.Windows.Forms.Label labelRightPlayerTransponderCode;
        private System.Windows.Forms.GroupBox groupBoxTimeline;
        private System.Windows.Forms.TextBox textBoxRightPlayerTransponderCode;
        private System.Windows.Forms.TextBox textBoxLeftPlayerTransponderCode;
        private System.Windows.Forms.TextBox textBoxTimelineName;
    }
}
