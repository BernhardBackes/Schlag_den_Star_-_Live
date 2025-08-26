namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.RGBIndicatorsScore {
    partial class UserControlGamePoolTemplatesRGBIndicatorsScoreIndicator {
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
            this.buttonOff = new System.Windows.Forms.Button();
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
            this.buttonLeftPlayer.Location = new System.Drawing.Point(3, 39);
            this.buttonLeftPlayer.Name = "buttonLeftPlayer";
            this.buttonLeftPlayer.Size = new System.Drawing.Size(62, 30);
            this.buttonLeftPlayer.TabIndex = 6;
            this.buttonLeftPlayer.Text = "LEFT";
            this.buttonLeftPlayer.UseVisualStyleBackColor = false;
            this.buttonLeftPlayer.Click += new System.EventHandler(this.buttonLeft_Click);
            // 
            // buttonOff
            // 
            this.buttonOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOff.ForeColor = System.Drawing.Color.Black;
            this.buttonOff.Location = new System.Drawing.Point(3, 3);
            this.buttonOff.Name = "buttonOff";
            this.buttonOff.Size = new System.Drawing.Size(62, 30);
            this.buttonOff.TabIndex = 5;
            this.buttonOff.Text = "OFF";
            this.buttonOff.UseVisualStyleBackColor = true;
            this.buttonOff.Click += new System.EventHandler(this.buttonIdle_Click);
            // 
            // UserControlGamePoolTemplatesRGBIndicatorsScoreIndicator
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Maroon;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.buttonRightPlayer);
            this.Controls.Add(this.buttonLeftPlayer);
            this.Controls.Add(this.buttonOff);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UserControlGamePoolTemplatesRGBIndicatorsScoreIndicator";
            this.Size = new System.Drawing.Size(70, 115);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonRightPlayer;
        private System.Windows.Forms.Button buttonLeftPlayer;
        private System.Windows.Forms.Button buttonOff;
    }
}
