namespace Cliparts.SchlagDenStarLive.MainApp.BuzzerIO {
    partial class UserControlBuzzer {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent() {
            this.listBoxUnits = new System.Windows.Forms.ListBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonRelease = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxUnits
            // 
            this.listBoxUnits.FormattingEnabled = true;
            this.listBoxUnits.ItemHeight = 18;
            this.listBoxUnits.Items.AddRange(new object[] {
            "listBoxUnits",
            "unit2",
            "unit3",
            "unit4"});
            this.listBoxUnits.Location = new System.Drawing.Point(3, 9);
            this.listBoxUnits.Name = "listBoxUnits";
            this.listBoxUnits.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBoxUnits.Size = new System.Drawing.Size(225, 76);
            this.listBoxUnits.TabIndex = 0;
            // 
            // buttonConnect
            // 
            this.buttonConnect.ForeColor = System.Drawing.Color.Black;
            this.buttonConnect.Location = new System.Drawing.Point(3, 91);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(100, 25);
            this.buttonConnect.TabIndex = 1;
            this.buttonConnect.Text = "connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonLock_Click);
            // 
            // buttonRelease
            // 
            this.buttonRelease.ForeColor = System.Drawing.Color.Black;
            this.buttonRelease.Location = new System.Drawing.Point(128, 91);
            this.buttonRelease.Name = "buttonRelease";
            this.buttonRelease.Size = new System.Drawing.Size(100, 25);
            this.buttonRelease.TabIndex = 2;
            this.buttonRelease.Text = "release";
            this.buttonRelease.UseVisualStyleBackColor = true;
            this.buttonRelease.Click += new System.EventHandler(this.buttonRelease_Click);
            // 
            // UserControlBuzzer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.Controls.Add(this.buttonRelease);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.listBoxUnits);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "UserControlBuzzer";
            this.Size = new System.Drawing.Size(231, 124);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxUnits;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonRelease;
    }
}
