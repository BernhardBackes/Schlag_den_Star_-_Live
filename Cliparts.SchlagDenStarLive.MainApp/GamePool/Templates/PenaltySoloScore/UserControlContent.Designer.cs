namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.PenaltySoloScore {
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
            this.groupBoxScore = new System.Windows.Forms.GroupBox();
            this.labelScoreStyle = new System.Windows.Forms.Label();
            this.comboBoxScoreStyle = new System.Windows.Forms.ComboBox();
            this.labelScorePositionY = new System.Windows.Forms.Label();
            this.labelScorePositionX = new System.Windows.Forms.Label();
            this.numericUpDownScorePositionY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownScorePositionX = new System.Windows.Forms.NumericUpDown();
            this.groupBoxScore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionX)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxScore
            // 
            this.groupBoxScore.Controls.Add(this.labelScoreStyle);
            this.groupBoxScore.Controls.Add(this.comboBoxScoreStyle);
            this.groupBoxScore.Controls.Add(this.labelScorePositionY);
            this.groupBoxScore.Controls.Add(this.labelScorePositionX);
            this.groupBoxScore.Controls.Add(this.numericUpDownScorePositionY);
            this.groupBoxScore.Controls.Add(this.numericUpDownScorePositionX);
            this.groupBoxScore.ForeColor = System.Drawing.Color.White;
            this.groupBoxScore.Location = new System.Drawing.Point(819, 62);
            this.groupBoxScore.Name = "groupBoxScore";
            this.groupBoxScore.Size = new System.Drawing.Size(534, 50);
            this.groupBoxScore.TabIndex = 17;
            this.groupBoxScore.TabStop = false;
            this.groupBoxScore.Text = "score";
            // 
            // labelScoreStyle
            // 
            this.labelScoreStyle.Location = new System.Drawing.Point(318, 18);
            this.labelScoreStyle.Name = "labelScoreStyle";
            this.labelScoreStyle.Size = new System.Drawing.Size(56, 24);
            this.labelScoreStyle.TabIndex = 5;
            this.labelScoreStyle.Text = "style:";
            this.labelScoreStyle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxScoreStyle
            // 
            this.comboBoxScoreStyle.FormattingEnabled = true;
            this.comboBoxScoreStyle.Location = new System.Drawing.Point(380, 17);
            this.comboBoxScoreStyle.Name = "comboBoxScoreStyle";
            this.comboBoxScoreStyle.Size = new System.Drawing.Size(148, 26);
            this.comboBoxScoreStyle.TabIndex = 4;
            this.comboBoxScoreStyle.SelectedIndexChanged += new System.EventHandler(this.comboBoxScoreStyle_SelectedIndexChanged);
            // 
            // labelScorePositionY
            // 
            this.labelScorePositionY.Location = new System.Drawing.Point(197, 18);
            this.labelScorePositionY.Name = "labelScorePositionY";
            this.labelScorePositionY.Size = new System.Drawing.Size(30, 24);
            this.labelScorePositionY.TabIndex = 3;
            this.labelScorePositionY.Text = "y:";
            this.labelScorePositionY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelScorePositionX
            // 
            this.labelScorePositionX.Location = new System.Drawing.Point(6, 18);
            this.labelScorePositionX.Name = "labelScorePositionX";
            this.labelScorePositionX.Size = new System.Drawing.Size(100, 24);
            this.labelScorePositionX.TabIndex = 2;
            this.labelScorePositionX.Text = "position.x:";
            this.labelScorePositionX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownScorePositionY
            // 
            this.numericUpDownScorePositionY.Location = new System.Drawing.Point(233, 18);
            this.numericUpDownScorePositionY.Name = "numericUpDownScorePositionY";
            this.numericUpDownScorePositionY.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownScorePositionY.TabIndex = 1;
            this.numericUpDownScorePositionY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownScorePositionY.ValueChanged += new System.EventHandler(this.numericUpDownScorePositionY_ValueChanged);
            // 
            // numericUpDownScorePositionX
            // 
            this.numericUpDownScorePositionX.Location = new System.Drawing.Point(112, 18);
            this.numericUpDownScorePositionX.Name = "numericUpDownScorePositionX";
            this.numericUpDownScorePositionX.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownScorePositionX.TabIndex = 0;
            this.numericUpDownScorePositionX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownScorePositionX.ValueChanged += new System.EventHandler(this.numericUpDownScorePositionX_ValueChanged);
            // 
            // UserControlContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxScore);
            this.Name = "UserControlContent";
            this.Controls.SetChildIndex(this.labelGameClass, 0);
            this.Controls.SetChildIndex(this.groupBoxScore, 0);
            this.groupBoxScore.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionX)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.GroupBox groupBoxScore;
        protected System.Windows.Forms.Label labelScoreStyle;
        protected System.Windows.Forms.ComboBox comboBoxScoreStyle;
        protected System.Windows.Forms.Label labelScorePositionY;
        protected System.Windows.Forms.Label labelScorePositionX;
        protected System.Windows.Forms.NumericUpDown numericUpDownScorePositionY;
        protected System.Windows.Forms.NumericUpDown numericUpDownScorePositionX;
    }
}
