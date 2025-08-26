namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.CounterSoloScore {
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
            this.labelCounterStyle = new System.Windows.Forms.Label();
            this.comboBoxCounterStyle = new System.Windows.Forms.ComboBox();
            this.labelCounterPositionY = new System.Windows.Forms.Label();
            this.labelCounterPositionX = new System.Windows.Forms.Label();
            this.numericUpDownCounterPositionY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCounterPositionX = new System.Windows.Forms.NumericUpDown();
            this.radioButtonSourceCounter = new System.Windows.Forms.RadioButton();
            this.radioButtonSourceScore = new System.Windows.Forms.RadioButton();
            this.groupBoxBuzzer = new System.Windows.Forms.GroupBox();
            this.labelRightPlayerBuzzerChannel = new System.Windows.Forms.Label();
            this.numericUpDownRightPlayerBuzzerChannel = new System.Windows.Forms.NumericUpDown();
            this.comboBoxIOUnit = new System.Windows.Forms.ComboBox();
            this.labelLeftPlayerBuzzerChannel = new System.Windows.Forms.Label();
            this.numericUpDownLeftPlayerBuzzerChannel = new System.Windows.Forms.NumericUpDown();
            this.labelIOUnit = new System.Windows.Forms.Label();
            this.groupBoxScore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionX)).BeginInit();
            this.groupBoxPreview.SuspendLayout();
            this.groupBoxCounter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCounterPositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCounterPositionX)).BeginInit();
            this.groupBoxBuzzer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRightPlayerBuzzerChannel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeftPlayerBuzzerChannel)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxScore
            // 
            this.groupBoxScore.Location = new System.Drawing.Point(819, 62);
            // 
            // comboBoxScoreStyle
            // 
            this.comboBoxScoreStyle.Size = new System.Drawing.Size(148, 26);
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
            this.groupBoxCounter.Controls.Add(this.labelCounterStyle);
            this.groupBoxCounter.Controls.Add(this.comboBoxCounterStyle);
            this.groupBoxCounter.Controls.Add(this.labelCounterPositionY);
            this.groupBoxCounter.Controls.Add(this.labelCounterPositionX);
            this.groupBoxCounter.Controls.Add(this.numericUpDownCounterPositionY);
            this.groupBoxCounter.Controls.Add(this.numericUpDownCounterPositionX);
            this.groupBoxCounter.ForeColor = System.Drawing.Color.White;
            this.groupBoxCounter.Location = new System.Drawing.Point(819, 6);
            this.groupBoxCounter.Name = "groupBoxCounter";
            this.groupBoxCounter.Size = new System.Drawing.Size(534, 50);
            this.groupBoxCounter.TabIndex = 18;
            this.groupBoxCounter.TabStop = false;
            this.groupBoxCounter.Text = "counter";
            // 
            // labelCounterStyle
            // 
            this.labelCounterStyle.Location = new System.Drawing.Point(318, 18);
            this.labelCounterStyle.Name = "labelCounterStyle";
            this.labelCounterStyle.Size = new System.Drawing.Size(56, 24);
            this.labelCounterStyle.TabIndex = 5;
            this.labelCounterStyle.Text = "style:";
            this.labelCounterStyle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxCounterStyle
            // 
            this.comboBoxCounterStyle.FormattingEnabled = true;
            this.comboBoxCounterStyle.Location = new System.Drawing.Point(380, 17);
            this.comboBoxCounterStyle.Name = "comboBoxCounterStyle";
            this.comboBoxCounterStyle.Size = new System.Drawing.Size(148, 26);
            this.comboBoxCounterStyle.TabIndex = 4;
            this.comboBoxCounterStyle.SelectedIndexChanged += new System.EventHandler(this.comboBoxCounterStyle_SelectedIndexChanged);
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
            this.radioButtonSourceCounter.TabIndex = 20;
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
            this.radioButtonSourceScore.TabIndex = 19;
            this.radioButtonSourceScore.TabStop = true;
            this.radioButtonSourceScore.Text = "score";
            this.radioButtonSourceScore.UseVisualStyleBackColor = true;
            this.radioButtonSourceScore.CheckedChanged += new System.EventHandler(this.radioButtonSourceScore_CheckedChanged);
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
            this.groupBoxBuzzer.Location = new System.Drawing.Point(819, 118);
            this.groupBoxBuzzer.Name = "groupBoxBuzzer";
            this.groupBoxBuzzer.Size = new System.Drawing.Size(534, 84);
            this.groupBoxBuzzer.TabIndex = 22;
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
            this.comboBoxIOUnit.TextChanged += new System.EventHandler(this.comboBoxIOUnit_TextChanged);
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
            // UserControlContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxBuzzer);
            this.Controls.Add(this.groupBoxCounter);
            this.Name = "UserControlContent";
            this.Controls.SetChildIndex(this.groupBoxPreview, 0);
            this.Controls.SetChildIndex(this.labelGameClass, 0);
            this.Controls.SetChildIndex(this.groupBoxScore, 0);
            this.Controls.SetChildIndex(this.groupBoxCounter, 0);
            this.Controls.SetChildIndex(this.groupBoxBuzzer, 0);
            this.groupBoxScore.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionX)).EndInit();
            this.groupBoxPreview.ResumeLayout(false);
            this.groupBoxPreview.PerformLayout();
            this.groupBoxCounter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCounterPositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCounterPositionX)).EndInit();
            this.groupBoxBuzzer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRightPlayerBuzzerChannel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeftPlayerBuzzerChannel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.GroupBox groupBoxCounter;
        protected System.Windows.Forms.Label labelCounterStyle;
        protected System.Windows.Forms.ComboBox comboBoxCounterStyle;
        protected System.Windows.Forms.Label labelCounterPositionY;
        protected System.Windows.Forms.Label labelCounterPositionX;
        protected System.Windows.Forms.NumericUpDown numericUpDownCounterPositionY;
        protected System.Windows.Forms.NumericUpDown numericUpDownCounterPositionX;
        private System.Windows.Forms.RadioButton radioButtonSourceCounter;
        private System.Windows.Forms.RadioButton radioButtonSourceScore;
        private System.Windows.Forms.GroupBox groupBoxBuzzer;
        private System.Windows.Forms.Label labelRightPlayerBuzzerChannel;
        private System.Windows.Forms.NumericUpDown numericUpDownRightPlayerBuzzerChannel;
        private System.Windows.Forms.ComboBox comboBoxIOUnit;
        private System.Windows.Forms.Label labelLeftPlayerBuzzerChannel;
        private System.Windows.Forms.NumericUpDown numericUpDownLeftPlayerBuzzerChannel;
        private System.Windows.Forms.Label labelIOUnit;
    }
}
