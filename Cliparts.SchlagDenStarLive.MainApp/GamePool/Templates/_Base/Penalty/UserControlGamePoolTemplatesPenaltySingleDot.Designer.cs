namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.Penalty {
    partial class UserControlGamePoolTemplatesPenaltySingleDot {
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
            this.buttonRed = new System.Windows.Forms.Button();
            this.buttonGreen = new System.Windows.Forms.Button();
            this.buttonOff = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonRed
            // 
            this.buttonRed.BackColor = System.Drawing.Color.LightSalmon;
            this.buttonRed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRed.ForeColor = System.Drawing.Color.Black;
            this.buttonRed.Location = new System.Drawing.Point(3, 75);
            this.buttonRed.Name = "buttonRed";
            this.buttonRed.Size = new System.Drawing.Size(62, 30);
            this.buttonRed.TabIndex = 7;
            this.buttonRed.Text = "RED";
            this.buttonRed.UseVisualStyleBackColor = false;
            this.buttonRed.Click += new System.EventHandler(this.buttonRed_Click);
            // 
            // buttonGreen
            // 
            this.buttonGreen.BackColor = System.Drawing.Color.PaleGreen;
            this.buttonGreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGreen.ForeColor = System.Drawing.Color.Black;
            this.buttonGreen.Location = new System.Drawing.Point(3, 39);
            this.buttonGreen.Name = "buttonGreen";
            this.buttonGreen.Size = new System.Drawing.Size(62, 30);
            this.buttonGreen.TabIndex = 6;
            this.buttonGreen.Text = "GREEN";
            this.buttonGreen.UseVisualStyleBackColor = false;
            this.buttonGreen.Click += new System.EventHandler(this.buttonGreen_Click);
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
            // UserControlGamePoolTemplatesPenaltySingleDot
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Maroon;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.buttonRed);
            this.Controls.Add(this.buttonGreen);
            this.Controls.Add(this.buttonOff);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UserControlGamePoolTemplatesPenaltySingleDot";
            this.Size = new System.Drawing.Size(70, 115);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonRed;
        private System.Windows.Forms.Button buttonGreen;
        private System.Windows.Forms.Button buttonOff;
    }
}
