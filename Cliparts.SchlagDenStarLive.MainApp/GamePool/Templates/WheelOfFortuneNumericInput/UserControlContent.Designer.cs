namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.WheelOfFortuneNumericInput {
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
            this.groupBoxPlayerClients = new System.Windows.Forms.GroupBox();
            this.textBoxRightPlayerClientHostname = new System.Windows.Forms.TextBox();
            this.labelRightPlayerClientHostname = new System.Windows.Forms.Label();
            this.textBoxLeftPlayerClientHostname = new System.Windows.Forms.TextBox();
            this.labelLeftPlayerClientHostname = new System.Windows.Forms.Label();
            this.groupBoxScore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionX)).BeginInit();
            this.groupBoxPreview.SuspendLayout();
            this.groupBoxPlayerClients.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxScoreStyle
            // 
            this.comboBoxScoreStyle.Size = new System.Drawing.Size(148, 26);
            // 
            // groupBoxPlayerClients
            // 
            this.groupBoxPlayerClients.Controls.Add(this.textBoxRightPlayerClientHostname);
            this.groupBoxPlayerClients.Controls.Add(this.labelRightPlayerClientHostname);
            this.groupBoxPlayerClients.Controls.Add(this.textBoxLeftPlayerClientHostname);
            this.groupBoxPlayerClients.Controls.Add(this.labelLeftPlayerClientHostname);
            this.groupBoxPlayerClients.ForeColor = System.Drawing.Color.White;
            this.groupBoxPlayerClients.Location = new System.Drawing.Point(819, 63);
            this.groupBoxPlayerClients.Name = "groupBoxPlayerClients";
            this.groupBoxPlayerClients.Size = new System.Drawing.Size(534, 86);
            this.groupBoxPlayerClients.TabIndex = 17;
            this.groupBoxPlayerClients.TabStop = false;
            this.groupBoxPlayerClients.Text = "player clients";
            // 
            // textBoxRightPlayerClientHostname
            // 
            this.textBoxRightPlayerClientHostname.Location = new System.Drawing.Point(112, 53);
            this.textBoxRightPlayerClientHostname.Name = "textBoxRightPlayerClientHostname";
            this.textBoxRightPlayerClientHostname.Size = new System.Drawing.Size(200, 24);
            this.textBoxRightPlayerClientHostname.TabIndex = 3;
            this.textBoxRightPlayerClientHostname.Text = "textBoxRightPlayerClientHostname";
            this.textBoxRightPlayerClientHostname.TextChanged += new System.EventHandler(this.textBoxRightPlayerClientHostname_TextChanged);
            // 
            // labelRightPlayerClientHostname
            // 
            this.labelRightPlayerClientHostname.Location = new System.Drawing.Point(9, 53);
            this.labelRightPlayerClientHostname.Name = "labelRightPlayerClientHostname";
            this.labelRightPlayerClientHostname.Size = new System.Drawing.Size(97, 24);
            this.labelRightPlayerClientHostname.TabIndex = 2;
            this.labelRightPlayerClientHostname.Text = "right:";
            this.labelRightPlayerClientHostname.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxLeftPlayerClientHostname
            // 
            this.textBoxLeftPlayerClientHostname.Location = new System.Drawing.Point(112, 23);
            this.textBoxLeftPlayerClientHostname.Name = "textBoxLeftPlayerClientHostname";
            this.textBoxLeftPlayerClientHostname.Size = new System.Drawing.Size(200, 24);
            this.textBoxLeftPlayerClientHostname.TabIndex = 1;
            this.textBoxLeftPlayerClientHostname.Text = "textBoxLeftPlayerClientHostname";
            this.textBoxLeftPlayerClientHostname.TextChanged += new System.EventHandler(this.textBoxLeftPlayerClientHostname_TextChanged);
            // 
            // labelLeftPlayerClientHostname
            // 
            this.labelLeftPlayerClientHostname.Location = new System.Drawing.Point(9, 23);
            this.labelLeftPlayerClientHostname.Name = "labelLeftPlayerClientHostname";
            this.labelLeftPlayerClientHostname.Size = new System.Drawing.Size(97, 24);
            this.labelLeftPlayerClientHostname.TabIndex = 0;
            this.labelLeftPlayerClientHostname.Text = "left:";
            this.labelLeftPlayerClientHostname.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UserControlContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxPlayerClients);
            this.Name = "UserControlContent";
            this.Controls.SetChildIndex(this.groupBoxPreview, 0);
            this.Controls.SetChildIndex(this.labelGameClass, 0);
            this.Controls.SetChildIndex(this.groupBoxScore, 0);
            this.Controls.SetChildIndex(this.groupBoxPlayerClients, 0);
            this.groupBoxScore.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionX)).EndInit();
            this.groupBoxPreview.ResumeLayout(false);
            this.groupBoxPreview.PerformLayout();
            this.groupBoxPlayerClients.ResumeLayout(false);
            this.groupBoxPlayerClients.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxPlayerClients;
        private System.Windows.Forms.Label labelLeftPlayerClientHostname;
        private System.Windows.Forms.TextBox textBoxRightPlayerClientHostname;
        private System.Windows.Forms.Label labelRightPlayerClientHostname;
        private System.Windows.Forms.TextBox textBoxLeftPlayerClientHostname;
    }
}
