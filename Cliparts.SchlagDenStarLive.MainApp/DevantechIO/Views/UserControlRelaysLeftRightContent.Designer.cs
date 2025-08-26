namespace Cliparts.SchlagDenStarLive.MainApp.DevantechIO.Views {
    partial class UserControlRelaysLeftRightContent {
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
            this.labelRightPlayerRelayChannel = new System.Windows.Forms.Label();
            this.numericUpDownRightPlayerRelayChannel = new System.Windows.Forms.NumericUpDown();
            this.labelLeftPlayerRelayChannel = new System.Windows.Forms.Label();
            this.numericUpDownLeftPlayerRelayChannel = new System.Windows.Forms.NumericUpDown();
            this.groupBoxRelay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRightPlayerRelayChannel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeftPlayerRelayChannel)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxRelay
            // 
            this.groupBoxRelay.Controls.Add(this.labelRightPlayerRelayChannel);
            this.groupBoxRelay.Controls.Add(this.numericUpDownRightPlayerRelayChannel);
            this.groupBoxRelay.Controls.Add(this.labelLeftPlayerRelayChannel);
            this.groupBoxRelay.Controls.Add(this.numericUpDownLeftPlayerRelayChannel);
            this.groupBoxRelay.Size = new System.Drawing.Size(534, 85);
            this.groupBoxRelay.Controls.SetChildIndex(this.numericUpDownLeftPlayerRelayChannel, 0);
            this.groupBoxRelay.Controls.SetChildIndex(this.labelLeftPlayerRelayChannel, 0);
            this.groupBoxRelay.Controls.SetChildIndex(this.numericUpDownRightPlayerRelayChannel, 0);
            this.groupBoxRelay.Controls.SetChildIndex(this.labelRightPlayerRelayChannel, 0);
            // 
            // labelRightPlayerRelayChannel
            // 
            this.labelRightPlayerRelayChannel.Location = new System.Drawing.Point(186, 51);
            this.labelRightPlayerRelayChannel.Name = "labelRightPlayerRelayChannel";
            this.labelRightPlayerRelayChannel.Size = new System.Drawing.Size(119, 24);
            this.labelRightPlayerRelayChannel.TabIndex = 11;
            this.labelRightPlayerRelayChannel.Text = "right channel:";
            this.labelRightPlayerRelayChannel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownRightPlayerRelayChannel
            // 
            this.numericUpDownRightPlayerRelayChannel.Location = new System.Drawing.Point(311, 52);
            this.numericUpDownRightPlayerRelayChannel.Name = "numericUpDownRightPlayerRelayChannel";
            this.numericUpDownRightPlayerRelayChannel.Size = new System.Drawing.Size(63, 24);
            this.numericUpDownRightPlayerRelayChannel.TabIndex = 10;
            this.numericUpDownRightPlayerRelayChannel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownRightPlayerRelayChannel.ValueChanged += new System.EventHandler(this.numericUpDownRightPlayerRelayChannel_ValueChanged);
            // 
            // labelLeftPlayerRelayChannel
            // 
            this.labelLeftPlayerRelayChannel.Location = new System.Drawing.Point(6, 50);
            this.labelLeftPlayerRelayChannel.Name = "labelLeftPlayerRelayChannel";
            this.labelLeftPlayerRelayChannel.Size = new System.Drawing.Size(100, 24);
            this.labelLeftPlayerRelayChannel.TabIndex = 9;
            this.labelLeftPlayerRelayChannel.Text = "left channel:";
            this.labelLeftPlayerRelayChannel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownLeftPlayerRelayChannel
            // 
            this.numericUpDownLeftPlayerRelayChannel.Location = new System.Drawing.Point(112, 51);
            this.numericUpDownLeftPlayerRelayChannel.Name = "numericUpDownLeftPlayerRelayChannel";
            this.numericUpDownLeftPlayerRelayChannel.Size = new System.Drawing.Size(63, 24);
            this.numericUpDownLeftPlayerRelayChannel.TabIndex = 8;
            this.numericUpDownLeftPlayerRelayChannel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownLeftPlayerRelayChannel.ValueChanged += new System.EventHandler(this.numericUpDownLeftPlayerRelayChannel_ValueChanged);
            // 
            // UserControlRelaysLeftRightContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UserControlRelaysLeftRightContent";
            this.Size = new System.Drawing.Size(534, 85);
            this.groupBoxRelay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRightPlayerRelayChannel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeftPlayerRelayChannel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelRightPlayerRelayChannel;
        private System.Windows.Forms.NumericUpDown numericUpDownRightPlayerRelayChannel;
        private System.Windows.Forms.Label labelLeftPlayerRelayChannel;
        private System.Windows.Forms.NumericUpDown numericUpDownLeftPlayerRelayChannel;
    }
}
