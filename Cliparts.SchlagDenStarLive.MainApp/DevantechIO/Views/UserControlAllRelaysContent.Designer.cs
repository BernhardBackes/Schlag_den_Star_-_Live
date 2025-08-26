namespace Cliparts.SchlagDenStarLive.MainApp.DevantechIO.Views {
    partial class UserControlAllRelaysContent {
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
            this.comboBoxRelayNameList = new System.Windows.Forms.ComboBox();
            this.labelDevice = new System.Windows.Forms.Label();
            this.groupBoxRelay.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxRelay
            // 
            this.groupBoxRelay.Controls.Add(this.comboBoxRelayNameList);
            this.groupBoxRelay.Controls.Add(this.labelDevice);
            this.groupBoxRelay.ForeColor = System.Drawing.Color.White;
            this.groupBoxRelay.Location = new System.Drawing.Point(0, 0);
            this.groupBoxRelay.Name = "groupBoxRelay";
            this.groupBoxRelay.Size = new System.Drawing.Size(534, 55);
            this.groupBoxRelay.TabIndex = 25;
            this.groupBoxRelay.TabStop = false;
            this.groupBoxRelay.Text = "relay";
            // 
            // comboBoxRelayNameList
            // 
            this.comboBoxRelayNameList.FormattingEnabled = true;
            this.comboBoxRelayNameList.Location = new System.Drawing.Point(112, 19);
            this.comboBoxRelayNameList.Name = "comboBoxRelayNameList";
            this.comboBoxRelayNameList.Size = new System.Drawing.Size(262, 26);
            this.comboBoxRelayNameList.TabIndex = 5;
            this.comboBoxRelayNameList.TextChanged += new System.EventHandler(this.comboBoxRelayNameList_TextChanged);
            // 
            // labelDevice
            // 
            this.labelDevice.Location = new System.Drawing.Point(6, 20);
            this.labelDevice.Name = "labelDevice";
            this.labelDevice.Size = new System.Drawing.Size(100, 24);
            this.labelDevice.TabIndex = 2;
            this.labelDevice.Text = "device:";
            this.labelDevice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UserControlAllRelaysContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkRed;
            this.Controls.Add(this.groupBoxRelay);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "UserControlAllRelaysContent";
            this.Size = new System.Drawing.Size(534, 55);
            this.groupBoxRelay.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBoxRelayNameList;
        private System.Windows.Forms.Label labelDevice;
        protected System.Windows.Forms.GroupBox groupBoxRelay;
    }
}
