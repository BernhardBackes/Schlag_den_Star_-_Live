namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.DoubleRGBIndicators {
    partial class UserControlIndicator {
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
            this.buttonRightPlayer = new System.Windows.Forms.Button();
            this.buttonLeftPlayer = new System.Windows.Forms.Button();
            this.buttonBothPlayers = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonRightPlayer
            // 
            this.buttonRightPlayer.BackColor = System.Drawing.Color.LightSkyBlue;
            this.buttonRightPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRightPlayer.ForeColor = System.Drawing.Color.Black;
            this.buttonRightPlayer.Location = new System.Drawing.Point(3, 75);
            this.buttonRightPlayer.Name = "buttonRightPlayer";
            this.buttonRightPlayer.Size = new System.Drawing.Size(62, 30);
            this.buttonRightPlayer.TabIndex = 7;
            this.buttonRightPlayer.Text = "RIGHT";
            this.buttonRightPlayer.UseVisualStyleBackColor = false;
            this.buttonRightPlayer.Click += new System.EventHandler(this.buttonRight_Click);
            // 
            // buttonLeftPlayer
            // 
            this.buttonLeftPlayer.BackColor = System.Drawing.Color.LightSalmon;
            this.buttonLeftPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLeftPlayer.ForeColor = System.Drawing.Color.Black;
            this.buttonLeftPlayer.Location = new System.Drawing.Point(3, 3);
            this.buttonLeftPlayer.Name = "buttonLeftPlayer";
            this.buttonLeftPlayer.Size = new System.Drawing.Size(62, 30);
            this.buttonLeftPlayer.TabIndex = 6;
            this.buttonLeftPlayer.Text = "LEFT";
            this.buttonLeftPlayer.UseVisualStyleBackColor = false;
            this.buttonLeftPlayer.Click += new System.EventHandler(this.buttonLeft_Click);
            // 
            // buttonBothPlayers
            // 
            this.buttonBothPlayers.BackColor = System.Drawing.SystemColors.Control;
            this.buttonBothPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBothPlayers.ForeColor = System.Drawing.Color.Black;
            this.buttonBothPlayers.Location = new System.Drawing.Point(3, 39);
            this.buttonBothPlayers.Name = "buttonBothPlayers";
            this.buttonBothPlayers.Size = new System.Drawing.Size(62, 30);
            this.buttonBothPlayers.TabIndex = 8;
            this.buttonBothPlayers.Text = "BOTH";
            this.buttonBothPlayers.UseVisualStyleBackColor = false;
            this.buttonBothPlayers.Click += new System.EventHandler(this.buttonBothPlayers_Click);
            // 
            // UserControlGamePoolTemplatesDoubleRGBIndicatorsIndicator
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Maroon;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.buttonBothPlayers);
            this.Controls.Add(this.buttonRightPlayer);
            this.Controls.Add(this.buttonLeftPlayer);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UserControlGamePoolTemplatesDoubleRGBIndicatorsIndicator";
            this.Size = new System.Drawing.Size(70, 115);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonRightPlayer;
        private System.Windows.Forms.Button buttonLeftPlayer;
        private System.Windows.Forms.Button buttonBothPlayers;
    }
}
