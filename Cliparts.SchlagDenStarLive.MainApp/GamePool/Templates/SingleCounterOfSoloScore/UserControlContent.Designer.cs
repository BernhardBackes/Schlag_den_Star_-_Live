namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.SingleCounterOfSoloScore {
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
            this.textBoxCounterTitle = new System.Windows.Forms.TextBox();
            this.labelCounterTitle = new System.Windows.Forms.Label();
            this.numericUpDownCounterTarget = new System.Windows.Forms.NumericUpDown();
            this.labelCounterTarget = new System.Windows.Forms.Label();
            this.labelCounterPositionY = new System.Windows.Forms.Label();
            this.labelCounterPositionX = new System.Windows.Forms.Label();
            this.numericUpDownCounterPositionY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCounterPositionX = new System.Windows.Forms.NumericUpDown();
            this.radioButtonSourceCounter = new System.Windows.Forms.RadioButton();
            this.radioButtonSourceScore = new System.Windows.Forms.RadioButton();
            this.groupBoxBuzzer = new System.Windows.Forms.GroupBox();
            this.comboBoxIOUnit = new System.Windows.Forms.ComboBox();
            this.labelBuzzerChannel = new System.Windows.Forms.Label();
            this.numericUpDownBuzzerChannel = new System.Windows.Forms.NumericUpDown();
            this.labelIOUnit = new System.Windows.Forms.Label();
            this.groupBoxScore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionX)).BeginInit();
            this.groupBoxPreview.SuspendLayout();
            this.groupBoxCounter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCounterTarget)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCounterPositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCounterPositionX)).BeginInit();
            this.groupBoxBuzzer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBuzzerChannel)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxScore
            // 
            this.groupBoxScore.Location = new System.Drawing.Point(819, 94);
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
            this.groupBoxCounter.Controls.Add(this.textBoxCounterTitle);
            this.groupBoxCounter.Controls.Add(this.labelCounterTitle);
            this.groupBoxCounter.Controls.Add(this.numericUpDownCounterTarget);
            this.groupBoxCounter.Controls.Add(this.labelCounterTarget);
            this.groupBoxCounter.Controls.Add(this.labelCounterPositionY);
            this.groupBoxCounter.Controls.Add(this.labelCounterPositionX);
            this.groupBoxCounter.Controls.Add(this.numericUpDownCounterPositionY);
            this.groupBoxCounter.Controls.Add(this.numericUpDownCounterPositionX);
            this.groupBoxCounter.ForeColor = System.Drawing.Color.White;
            this.groupBoxCounter.Location = new System.Drawing.Point(819, 6);
            this.groupBoxCounter.Name = "groupBoxCounter";
            this.groupBoxCounter.Size = new System.Drawing.Size(534, 82);
            this.groupBoxCounter.TabIndex = 18;
            this.groupBoxCounter.TabStop = false;
            this.groupBoxCounter.Text = "counter";
            // 
            // textBoxCounterTitle
            // 
            this.textBoxCounterTitle.Location = new System.Drawing.Point(112, 48);
            this.textBoxCounterTitle.Name = "textBoxCounterTitle";
            this.textBoxCounterTitle.Size = new System.Drawing.Size(200, 24);
            this.textBoxCounterTitle.TabIndex = 8;
            this.textBoxCounterTitle.TextChanged += new System.EventHandler(this.textBoxCounterTitle_TextChanged);
            // 
            // labelCounterTitle
            // 
            this.labelCounterTitle.Location = new System.Drawing.Point(8, 48);
            this.labelCounterTitle.Name = "labelCounterTitle";
            this.labelCounterTitle.Size = new System.Drawing.Size(100, 24);
            this.labelCounterTitle.TabIndex = 7;
            this.labelCounterTitle.Text = "title:";
            this.labelCounterTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownCounterTarget
            // 
            this.numericUpDownCounterTarget.Location = new System.Drawing.Point(380, 48);
            this.numericUpDownCounterTarget.Name = "numericUpDownCounterTarget";
            this.numericUpDownCounterTarget.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownCounterTarget.TabIndex = 6;
            this.numericUpDownCounterTarget.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownCounterTarget.ValueChanged += new System.EventHandler(this.numericUpDownCounterTarget_ValueChanged);
            // 
            // labelCounterTarget
            // 
            this.labelCounterTarget.Location = new System.Drawing.Point(318, 48);
            this.labelCounterTarget.Name = "labelCounterTarget";
            this.labelCounterTarget.Size = new System.Drawing.Size(56, 24);
            this.labelCounterTarget.TabIndex = 5;
            this.labelCounterTarget.Text = "target:";
            this.labelCounterTarget.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.groupBoxBuzzer.Controls.Add(this.comboBoxIOUnit);
            this.groupBoxBuzzer.Controls.Add(this.labelBuzzerChannel);
            this.groupBoxBuzzer.Controls.Add(this.numericUpDownBuzzerChannel);
            this.groupBoxBuzzer.Controls.Add(this.labelIOUnit);
            this.groupBoxBuzzer.ForeColor = System.Drawing.Color.White;
            this.groupBoxBuzzer.Location = new System.Drawing.Point(819, 150);
            this.groupBoxBuzzer.Name = "groupBoxBuzzer";
            this.groupBoxBuzzer.Size = new System.Drawing.Size(534, 84);
            this.groupBoxBuzzer.TabIndex = 22;
            this.groupBoxBuzzer.TabStop = false;
            this.groupBoxBuzzer.Text = "buzzer";
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
            // labelBuzzerChannel
            // 
            this.labelBuzzerChannel.Location = new System.Drawing.Point(6, 50);
            this.labelBuzzerChannel.Name = "labelBuzzerChannel";
            this.labelBuzzerChannel.Size = new System.Drawing.Size(100, 24);
            this.labelBuzzerChannel.TabIndex = 4;
            this.labelBuzzerChannel.Text = "channel:";
            this.labelBuzzerChannel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownBuzzerChannel
            // 
            this.numericUpDownBuzzerChannel.Location = new System.Drawing.Point(112, 51);
            this.numericUpDownBuzzerChannel.Name = "numericUpDownBuzzerChannel";
            this.numericUpDownBuzzerChannel.Size = new System.Drawing.Size(63, 24);
            this.numericUpDownBuzzerChannel.TabIndex = 3;
            this.numericUpDownBuzzerChannel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownBuzzerChannel.ValueChanged += new System.EventHandler(this.numericUpDownBuzzerChannel_ValueChanged);
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
            this.groupBoxCounter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCounterTarget)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCounterPositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCounterPositionX)).EndInit();
            this.groupBoxBuzzer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBuzzerChannel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.GroupBox groupBoxCounter;
        protected System.Windows.Forms.Label labelCounterTarget;
        protected System.Windows.Forms.Label labelCounterPositionY;
        protected System.Windows.Forms.Label labelCounterPositionX;
        protected System.Windows.Forms.NumericUpDown numericUpDownCounterPositionY;
        protected System.Windows.Forms.NumericUpDown numericUpDownCounterPositionX;
        private System.Windows.Forms.RadioButton radioButtonSourceCounter;
        private System.Windows.Forms.RadioButton radioButtonSourceScore;
        private System.Windows.Forms.GroupBox groupBoxBuzzer;
        private System.Windows.Forms.ComboBox comboBoxIOUnit;
        private System.Windows.Forms.Label labelBuzzerChannel;
        private System.Windows.Forms.NumericUpDown numericUpDownBuzzerChannel;
        private System.Windows.Forms.Label labelIOUnit;
        private System.Windows.Forms.TextBox textBoxCounterTitle;
        protected System.Windows.Forms.Label labelCounterTitle;
        protected System.Windows.Forms.NumericUpDown numericUpDownCounterTarget;
    }
}
