namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.Reversi {

    partial class UserControlGamePoolTemplatesReversiDMX {
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
            this.labelID = new System.Windows.Forms.Label();
            this.buttonNeutral = new System.Windows.Forms.Button();
            this.buttonLeftPlayer = new System.Windows.Forms.Button();
            this.buttonRightPlayer = new System.Windows.Forms.Button();
            this.numericUpDownDMXStartAddress = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDMXStartAddress)).BeginInit();
            this.SuspendLayout();
            // 
            // labelID
            // 
            this.labelID.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelID.Location = new System.Drawing.Point(0, 2);
            this.labelID.Name = "labelID";
            this.labelID.Size = new System.Drawing.Size(86, 25);
            this.labelID.TabIndex = 0;
            this.labelID.Text = "A1";
            this.labelID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonNeutral
            // 
            this.buttonNeutral.Location = new System.Drawing.Point(87, 1);
            this.buttonNeutral.Margin = new System.Windows.Forms.Padding(0);
            this.buttonNeutral.Name = "buttonNeutral";
            this.buttonNeutral.Size = new System.Drawing.Size(30, 18);
            this.buttonNeutral.TabIndex = 1;
            this.buttonNeutral.UseVisualStyleBackColor = true;
            this.buttonNeutral.Click += new System.EventHandler(this.buttonNeutral_Click);
            // 
            // buttonLeftPlayer
            // 
            this.buttonLeftPlayer.Location = new System.Drawing.Point(87, 18);
            this.buttonLeftPlayer.Margin = new System.Windows.Forms.Padding(0);
            this.buttonLeftPlayer.Name = "buttonLeftPlayer";
            this.buttonLeftPlayer.Size = new System.Drawing.Size(30, 18);
            this.buttonLeftPlayer.TabIndex = 2;
            this.buttonLeftPlayer.UseVisualStyleBackColor = true;
            this.buttonLeftPlayer.Click += new System.EventHandler(this.buttonLeftPlayer_Click);
            // 
            // buttonRightPlayer
            // 
            this.buttonRightPlayer.Location = new System.Drawing.Point(87, 35);
            this.buttonRightPlayer.Margin = new System.Windows.Forms.Padding(0);
            this.buttonRightPlayer.Name = "buttonRightPlayer";
            this.buttonRightPlayer.Size = new System.Drawing.Size(30, 18);
            this.buttonRightPlayer.TabIndex = 3;
            this.buttonRightPlayer.UseVisualStyleBackColor = true;
            this.buttonRightPlayer.Click += new System.EventHandler(this.buttonRightPlayer_Click);
            // 
            // numericUpDownDMXStartAddress
            // 
            this.numericUpDownDMXStartAddress.Location = new System.Drawing.Point(0, 28);
            this.numericUpDownDMXStartAddress.Name = "numericUpDownDMXStartAddress";
            this.numericUpDownDMXStartAddress.Size = new System.Drawing.Size(87, 24);
            this.numericUpDownDMXStartAddress.TabIndex = 4;
            this.numericUpDownDMXStartAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownDMXStartAddress.ValueChanged += new System.EventHandler(this.numericUpDownDMXStartAddress_ValueChanged);
            // 
            // UserControlGamePoolTemplatesReversiDMX
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.numericUpDownDMXStartAddress);
            this.Controls.Add(this.buttonRightPlayer);
            this.Controls.Add(this.buttonLeftPlayer);
            this.Controls.Add(this.buttonNeutral);
            this.Controls.Add(this.labelID);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "UserControlGamePoolTemplatesReversiDMX";
            this.Size = new System.Drawing.Size(118, 54);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDMXStartAddress)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelID;
        private System.Windows.Forms.Button buttonNeutral;
        private System.Windows.Forms.Button buttonLeftPlayer;
        private System.Windows.Forms.Button buttonRightPlayer;
        private System.Windows.Forms.NumericUpDown numericUpDownDMXStartAddress;
    }
}
