namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimerMaxCounterToBeatScore
{
    
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
            this.groupBoxCounter = new System.Windows.Forms.GroupBox();
            this.labelCounterSize = new System.Windows.Forms.Label();
            this.comboBoxCounterSize = new System.Windows.Forms.ComboBox();
            this.labelCounterPositionY = new System.Windows.Forms.Label();
            this.labelCounterPositionX = new System.Windows.Forms.Label();
            this.numericUpDownCounterPositionY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCounterPositionX = new System.Windows.Forms.NumericUpDown();
            this.radioButtonSourceCounter = new System.Windows.Forms.RadioButton();
            this.radioButtonSourceScore = new System.Windows.Forms.RadioButton();
            this.groupBoxScore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionX)).BeginInit();
            this.groupBoxTimer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimerExtraTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimerAlarmTime2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimerStopTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimerAlarmTime1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimerStartTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimerPositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimerPositionX)).BeginInit();
            this.groupBoxBuzzer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimerScaling)).BeginInit();
            this.groupBoxPreview.SuspendLayout();
            this.groupBoxCounter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCounterPositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCounterPositionX)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxScoreStyle
            // 
            this.comboBoxScoreStyle.Size = new System.Drawing.Size(148, 26);
            // 
            // comboBoxTimerStyle
            // 
            this.comboBoxTimerStyle.Size = new System.Drawing.Size(148, 26);
            // 
            // labelRightPlayerBuzzerChannel
            // 
            this.labelRightPlayerBuzzerChannel.Text = "count up ch.:";
            // 
            // labelLeftPlayerBuzzerChannel
            // 
            this.labelLeftPlayerBuzzerChannel.Text = "reset ch.:";
            // 
            // groupBoxPreview
            // 
            this.groupBoxPreview.Controls.Add(this.radioButtonSourceCounter);
            this.groupBoxPreview.Controls.Add(this.radioButtonSourceScore);
            this.groupBoxPreview.Controls.SetChildIndex(this.checkBoxShowSafeArea, 0);
            this.groupBoxPreview.Controls.SetChildIndex(this.radioButtonSourceScore, 0);
            this.groupBoxPreview.Controls.SetChildIndex(this.radioButtonSourceCounter, 0);
            // 
            // groupBoxCounter
            // 
            this.groupBoxCounter.Controls.Add(this.labelCounterSize);
            this.groupBoxCounter.Controls.Add(this.comboBoxCounterSize);
            this.groupBoxCounter.Controls.Add(this.labelCounterPositionY);
            this.groupBoxCounter.Controls.Add(this.labelCounterPositionX);
            this.groupBoxCounter.Controls.Add(this.numericUpDownCounterPositionY);
            this.groupBoxCounter.Controls.Add(this.numericUpDownCounterPositionX);
            this.groupBoxCounter.ForeColor = System.Drawing.Color.White;
            this.groupBoxCounter.Location = new System.Drawing.Point(819, 333);
            this.groupBoxCounter.Name = "groupBoxCounter";
            this.groupBoxCounter.Size = new System.Drawing.Size(534, 50);
            this.groupBoxCounter.TabIndex = 40;
            this.groupBoxCounter.TabStop = false;
            this.groupBoxCounter.Text = "counter";
            // 
            // labelCounterSize
            // 
            this.labelCounterSize.Location = new System.Drawing.Point(318, 17);
            this.labelCounterSize.Name = "labelCounterSize";
            this.labelCounterSize.Size = new System.Drawing.Size(56, 24);
            this.labelCounterSize.TabIndex = 7;
            this.labelCounterSize.Text = "size:";
            this.labelCounterSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxCounterSize
            // 
            this.comboBoxCounterSize.FormattingEnabled = true;
            this.comboBoxCounterSize.Location = new System.Drawing.Point(380, 16);
            this.comboBoxCounterSize.Name = "comboBoxCounterSize";
            this.comboBoxCounterSize.Size = new System.Drawing.Size(148, 26);
            this.comboBoxCounterSize.TabIndex = 6;
            this.comboBoxCounterSize.SelectedIndexChanged += new System.EventHandler(this.comboBoxCounterSize_SelectedIndexChanged);
            // 
            // labelCounterPositionY
            // 
            this.labelCounterPositionY.Location = new System.Drawing.Point(197, 18);
            this.labelCounterPositionY.Name = "labelCounterPositionY";
            this.labelCounterPositionY.Size = new System.Drawing.Size(30, 24);
            this.labelCounterPositionY.TabIndex = 3;
            this.labelCounterPositionY.Text = "y:";
            this.labelCounterPositionY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelCounterPositionX
            // 
            this.labelCounterPositionX.Location = new System.Drawing.Point(6, 18);
            this.labelCounterPositionX.Name = "labelCounterPositionX";
            this.labelCounterPositionX.Size = new System.Drawing.Size(100, 24);
            this.labelCounterPositionX.TabIndex = 2;
            this.labelCounterPositionX.Text = "position.x:";
            this.labelCounterPositionX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownCounterPositionY
            // 
            this.numericUpDownCounterPositionY.Location = new System.Drawing.Point(233, 18);
            this.numericUpDownCounterPositionY.Name = "numericUpDownCounterPositionY";
            this.numericUpDownCounterPositionY.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownCounterPositionY.TabIndex = 1;
            this.numericUpDownCounterPositionY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownCounterPositionY.ValueChanged += new System.EventHandler(this.numericUpDownCounterPositionY_ValueChanged);
            // 
            // numericUpDownCounterPositionX
            // 
            this.numericUpDownCounterPositionX.Location = new System.Drawing.Point(112, 18);
            this.numericUpDownCounterPositionX.Name = "numericUpDownCounterPositionX";
            this.numericUpDownCounterPositionX.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownCounterPositionX.TabIndex = 0;
            this.numericUpDownCounterPositionX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownCounterPositionX.ValueChanged += new System.EventHandler(this.numericUpDownCounterPositionX_ValueChanged);
            // 
            // radioButtonSourceCounter
            // 
            this.radioButtonSourceCounter.AutoSize = true;
            this.radioButtonSourceCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSourceCounter.Location = new System.Drawing.Point(186, 14);
            this.radioButtonSourceCounter.Name = "radioButtonSourceCounter";
            this.radioButtonSourceCounter.Size = new System.Drawing.Size(83, 22);
            this.radioButtonSourceCounter.TabIndex = 22;
            this.radioButtonSourceCounter.TabStop = true;
            this.radioButtonSourceCounter.Text = "counter";
            this.radioButtonSourceCounter.UseVisualStyleBackColor = true;
            this.radioButtonSourceCounter.CheckedChanged += new System.EventHandler(this.radioButtonSourceCounter_CheckedChanged);
            // 
            // radioButtonSourceScore
            // 
            this.radioButtonSourceScore.AutoSize = true;
            this.radioButtonSourceScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSourceScore.Location = new System.Drawing.Point(112, 14);
            this.radioButtonSourceScore.Name = "radioButtonSourceScore";
            this.radioButtonSourceScore.Size = new System.Drawing.Size(69, 22);
            this.radioButtonSourceScore.TabIndex = 21;
            this.radioButtonSourceScore.TabStop = true;
            this.radioButtonSourceScore.Text = "score";
            this.radioButtonSourceScore.UseVisualStyleBackColor = true;
            this.radioButtonSourceScore.CheckedChanged += new System.EventHandler(this.radioButtonSourceScore_CheckedChanged);
            // 
            // UserControlContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxCounter);
            this.Name = "UserControlContent";
            this.Controls.SetChildIndex(this.groupBoxBuzzer, 0);
            this.Controls.SetChildIndex(this.groupBoxPreview, 0);
            this.Controls.SetChildIndex(this.labelGameClass, 0);
            this.Controls.SetChildIndex(this.groupBoxTimer, 0);
            this.Controls.SetChildIndex(this.groupBoxScore, 0);
            this.Controls.SetChildIndex(this.groupBoxCounter, 0);
            this.groupBoxScore.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionX)).EndInit();
            this.groupBoxTimer.ResumeLayout(false);
            this.groupBoxTimer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimerExtraTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimerAlarmTime2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimerStopTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimerAlarmTime1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimerStartTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimerPositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimerPositionX)).EndInit();
            this.groupBoxBuzzer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimerScaling)).EndInit();
            this.groupBoxPreview.ResumeLayout(false);
            this.groupBoxPreview.PerformLayout();
            this.groupBoxCounter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCounterPositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCounterPositionX)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.GroupBox groupBoxCounter;
        protected System.Windows.Forms.Label labelCounterPositionY;
        protected System.Windows.Forms.Label labelCounterPositionX;
        protected System.Windows.Forms.NumericUpDown numericUpDownCounterPositionY;
        protected System.Windows.Forms.NumericUpDown numericUpDownCounterPositionX;
        private System.Windows.Forms.RadioButton radioButtonSourceCounter;
        private System.Windows.Forms.RadioButton radioButtonSourceScore;
        protected System.Windows.Forms.Label labelCounterSize;
        protected System.Windows.Forms.ComboBox comboBoxCounterSize;
    }
}
