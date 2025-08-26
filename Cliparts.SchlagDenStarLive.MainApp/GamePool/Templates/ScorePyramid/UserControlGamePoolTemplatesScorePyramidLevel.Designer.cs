namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.ScorePyramid {

    partial class UserControlGamePoolTemplatesScorePyramidLevel {
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
            this.numericUpDownLeftPlayerHits = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownRightPlayerHits = new System.Windows.Forms.NumericUpDown();
            this.labelValue = new System.Windows.Forms.Label();
            this.buttonAddLeft = new System.Windows.Forms.Button();
            this.buttonAddRight = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeftPlayerHits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRightPlayerHits)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDownLeftPlayerHits
            // 
            this.numericUpDownLeftPlayerHits.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownLeftPlayerHits.Location = new System.Drawing.Point(53, 3);
            this.numericUpDownLeftPlayerHits.Name = "numericUpDownLeftPlayerHits";
            this.numericUpDownLeftPlayerHits.Size = new System.Drawing.Size(76, 31);
            this.numericUpDownLeftPlayerHits.TabIndex = 0;
            this.numericUpDownLeftPlayerHits.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numericUpDownRightPlayerHits
            // 
            this.numericUpDownRightPlayerHits.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownRightPlayerHits.Location = new System.Drawing.Point(217, 3);
            this.numericUpDownRightPlayerHits.Name = "numericUpDownRightPlayerHits";
            this.numericUpDownRightPlayerHits.Size = new System.Drawing.Size(76, 31);
            this.numericUpDownRightPlayerHits.TabIndex = 1;
            this.numericUpDownRightPlayerHits.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelValue
            // 
            this.labelValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelValue.Location = new System.Drawing.Point(135, 3);
            this.labelValue.Name = "labelValue";
            this.labelValue.Size = new System.Drawing.Size(76, 31);
            this.labelValue.TabIndex = 2;
            this.labelValue.Text = "label1";
            this.labelValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonAddLeft
            // 
            this.buttonAddLeft.ForeColor = System.Drawing.Color.Black;
            this.buttonAddLeft.Location = new System.Drawing.Point(3, 3);
            this.buttonAddLeft.Name = "buttonAddLeft";
            this.buttonAddLeft.Size = new System.Drawing.Size(44, 31);
            this.buttonAddLeft.TabIndex = 3;
            this.buttonAddLeft.Text = "+";
            this.buttonAddLeft.UseVisualStyleBackColor = true;
            this.buttonAddLeft.Click += new System.EventHandler(this.buttonAddLeft_Click);
            // 
            // buttonAddRight
            // 
            this.buttonAddRight.ForeColor = System.Drawing.Color.Black;
            this.buttonAddRight.Location = new System.Drawing.Point(299, 3);
            this.buttonAddRight.Name = "buttonAddRight";
            this.buttonAddRight.Size = new System.Drawing.Size(44, 31);
            this.buttonAddRight.TabIndex = 4;
            this.buttonAddRight.Text = "+";
            this.buttonAddRight.UseVisualStyleBackColor = true;
            this.buttonAddRight.Click += new System.EventHandler(this.buttonAddRight_Click);
            // 
            // UserControlGamePoolTemplatesScorePyramidLevel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.SaddleBrown;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.buttonAddRight);
            this.Controls.Add(this.buttonAddLeft);
            this.Controls.Add(this.labelValue);
            this.Controls.Add(this.numericUpDownRightPlayerHits);
            this.Controls.Add(this.numericUpDownLeftPlayerHits);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UserControlGamePoolTemplatesScorePyramidLevel";
            this.Size = new System.Drawing.Size(347, 37);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeftPlayerHits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRightPlayerHits)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDownLeftPlayerHits;
        private System.Windows.Forms.NumericUpDown numericUpDownRightPlayerHits;
        private System.Windows.Forms.Label labelValue;
        private System.Windows.Forms.Button buttonAddLeft;
        private System.Windows.Forms.Button buttonAddRight;
    }
}
