namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.AudienceVoting {
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
            this.groupBoxContent = new System.Windows.Forms.GroupBox();
            this.labelContentPositionY = new System.Windows.Forms.Label();
            this.labelContentPositionX = new System.Windows.Forms.Label();
            this.numericUpDownContentPositionY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownContentPositionX = new System.Windows.Forms.NumericUpDown();
            this.groupBoxPreview.SuspendLayout();
            this.groupBoxContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownContentPositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownContentPositionX)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxContent
            // 
            this.groupBoxContent.Controls.Add(this.labelContentPositionY);
            this.groupBoxContent.Controls.Add(this.labelContentPositionX);
            this.groupBoxContent.Controls.Add(this.numericUpDownContentPositionY);
            this.groupBoxContent.Controls.Add(this.numericUpDownContentPositionX);
            this.groupBoxContent.ForeColor = System.Drawing.Color.White;
            this.groupBoxContent.Location = new System.Drawing.Point(819, 187);
            this.groupBoxContent.Name = "groupBoxContent";
            this.groupBoxContent.Size = new System.Drawing.Size(534, 50);
            this.groupBoxContent.TabIndex = 39;
            this.groupBoxContent.TabStop = false;
            this.groupBoxContent.Text = "content";
            // 
            // labelContentPositionY
            // 
            this.labelContentPositionY.Location = new System.Drawing.Point(197, 18);
            this.labelContentPositionY.Name = "labelContentPositionY";
            this.labelContentPositionY.Size = new System.Drawing.Size(30, 24);
            this.labelContentPositionY.TabIndex = 3;
            this.labelContentPositionY.Text = "y:";
            this.labelContentPositionY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelContentPositionX
            // 
            this.labelContentPositionX.Location = new System.Drawing.Point(6, 18);
            this.labelContentPositionX.Name = "labelContentPositionX";
            this.labelContentPositionX.Size = new System.Drawing.Size(100, 24);
            this.labelContentPositionX.TabIndex = 2;
            this.labelContentPositionX.Text = "position.x:";
            this.labelContentPositionX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownContentPositionY
            // 
            this.numericUpDownContentPositionY.Location = new System.Drawing.Point(233, 18);
            this.numericUpDownContentPositionY.Name = "numericUpDownContentPositionY";
            this.numericUpDownContentPositionY.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownContentPositionY.TabIndex = 1;
            this.numericUpDownContentPositionY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownContentPositionY.ValueChanged += new System.EventHandler(this.numericUpDownContentPositionY_ValueChanged);
            // 
            // numericUpDownContentPositionX
            // 
            this.numericUpDownContentPositionX.Location = new System.Drawing.Point(112, 18);
            this.numericUpDownContentPositionX.Name = "numericUpDownContentPositionX";
            this.numericUpDownContentPositionX.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownContentPositionX.TabIndex = 0;
            this.numericUpDownContentPositionX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownContentPositionX.ValueChanged += new System.EventHandler(this.numericUpDownContentPositionX_ValueChanged);
            // 
            // UserControlContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxContent);
            this.Name = "UserControlContent";
            this.Controls.SetChildIndex(this.labelGameClass, 0);
            this.Controls.SetChildIndex(this.groupBoxPreview, 0);
            this.Controls.SetChildIndex(this.groupBoxContent, 0);
            this.groupBoxPreview.ResumeLayout(false);
            this.groupBoxPreview.PerformLayout();
            this.groupBoxContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownContentPositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownContentPositionX)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.GroupBox groupBoxContent;
        protected System.Windows.Forms.Label labelContentPositionY;
        protected System.Windows.Forms.Label labelContentPositionX;
        protected System.Windows.Forms.NumericUpDown numericUpDownContentPositionY;
        protected System.Windows.Forms.NumericUpDown numericUpDownContentPositionX;
    }
}
