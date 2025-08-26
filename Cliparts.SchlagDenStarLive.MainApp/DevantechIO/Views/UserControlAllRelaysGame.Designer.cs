namespace Cliparts.SchlagDenStarLive.MainApp.DevantechIO.Views {
    partial class UserControlAllRelaysGame {
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
            this.groupBoxRelay = new System.Windows.Forms.GroupBox();
            this.labelAllRelays = new System.Windows.Forms.Label();
            this.textBoxDevice = new System.Windows.Forms.TextBox();
            this.labelDevice = new System.Windows.Forms.Label();
            this.buttonOpenAllRelays = new System.Windows.Forms.Button();
            this.buttonCloseAllRelays = new System.Windows.Forms.Button();
            this.groupBoxRelay.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxRelay
            // 
            this.groupBoxRelay.Controls.Add(this.labelAllRelays);
            this.groupBoxRelay.Controls.Add(this.textBoxDevice);
            this.groupBoxRelay.Controls.Add(this.labelDevice);
            this.groupBoxRelay.Controls.Add(this.buttonOpenAllRelays);
            this.groupBoxRelay.Controls.Add(this.buttonCloseAllRelays);
            this.groupBoxRelay.ForeColor = System.Drawing.Color.White;
            this.groupBoxRelay.Location = new System.Drawing.Point(0, 0);
            this.groupBoxRelay.Name = "groupBoxRelay";
            this.groupBoxRelay.Size = new System.Drawing.Size(200, 97);
            this.groupBoxRelay.TabIndex = 397;
            this.groupBoxRelay.TabStop = false;
            this.groupBoxRelay.Text = "relay";
            // 
            // labelAllRelays
            // 
            this.labelAllRelays.Location = new System.Drawing.Point(82, 65);
            this.labelAllRelays.Name = "labelAllRelays";
            this.labelAllRelays.Size = new System.Drawing.Size(36, 24);
            this.labelAllRelays.TabIndex = 16;
            this.labelAllRelays.Text = "all";
            this.labelAllRelays.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxDevice
            // 
            this.textBoxDevice.Location = new System.Drawing.Point(6, 38);
            this.textBoxDevice.Name = "textBoxDevice";
            this.textBoxDevice.ReadOnly = true;
            this.textBoxDevice.Size = new System.Drawing.Size(188, 24);
            this.textBoxDevice.TabIndex = 15;
            this.textBoxDevice.Text = "textBoxDevice";
            this.textBoxDevice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelDevice
            // 
            this.labelDevice.Location = new System.Drawing.Point(71, 11);
            this.labelDevice.Name = "labelDevice";
            this.labelDevice.Size = new System.Drawing.Size(59, 24);
            this.labelDevice.TabIndex = 14;
            this.labelDevice.Text = "device";
            this.labelDevice.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // buttonOpenAllRelays
            // 
            this.buttonOpenAllRelays.ForeColor = System.Drawing.Color.Black;
            this.buttonOpenAllRelays.Location = new System.Drawing.Point(119, 64);
            this.buttonOpenAllRelays.Name = "buttonOpenAllRelays";
            this.buttonOpenAllRelays.Size = new System.Drawing.Size(75, 26);
            this.buttonOpenAllRelays.TabIndex = 5;
            this.buttonOpenAllRelays.Text = "open";
            this.buttonOpenAllRelays.UseVisualStyleBackColor = true;
            this.buttonOpenAllRelays.Click += new System.EventHandler(this.buttonOpenAllRelays_Click);
            // 
            // buttonCloseAllRelays
            // 
            this.buttonCloseAllRelays.ForeColor = System.Drawing.Color.Black;
            this.buttonCloseAllRelays.Location = new System.Drawing.Point(6, 64);
            this.buttonCloseAllRelays.Name = "buttonCloseAllRelays";
            this.buttonCloseAllRelays.Size = new System.Drawing.Size(75, 26);
            this.buttonCloseAllRelays.TabIndex = 4;
            this.buttonCloseAllRelays.Text = "close";
            this.buttonCloseAllRelays.UseVisualStyleBackColor = true;
            this.buttonCloseAllRelays.Click += new System.EventHandler(this.buttonCloseAllRelays_Click);
            // 
            // UserControlAllRelaysGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Sienna;
            this.Controls.Add(this.groupBoxRelay);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "UserControlAllRelaysGame";
            this.Size = new System.Drawing.Size(200, 97);
            this.groupBoxRelay.ResumeLayout(false);
            this.groupBoxRelay.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxDevice;
        private System.Windows.Forms.Label labelDevice;
        private System.Windows.Forms.Button buttonOpenAllRelays;
        private System.Windows.Forms.Button buttonCloseAllRelays;
        private System.Windows.Forms.Label labelAllRelays;
        protected System.Windows.Forms.GroupBox groupBoxRelay;
    }
}
