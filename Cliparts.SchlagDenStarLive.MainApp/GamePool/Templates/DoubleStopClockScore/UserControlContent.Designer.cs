namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.DoubleStopClockScore {
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
            this.groupBoxLocalVentuzServer = new System.Windows.Forms.GroupBox();
            this.labelLocalVentuzServerDoubleStopClock = new System.Windows.Forms.Label();
            this.textBoxLocalVentuzServerDoubleStopClock = new System.Windows.Forms.TextBox();
            this.groupBoxScore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionX)).BeginInit();
            this.groupBoxPreview.SuspendLayout();
            this.groupBoxLocalVentuzServer.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxScoreStyle
            // 
            this.comboBoxScoreStyle.Size = new System.Drawing.Size(148, 26);
            // 
            // groupBoxLocalVentuzServer
            // 
            this.groupBoxLocalVentuzServer.Controls.Add(this.labelLocalVentuzServerDoubleStopClock);
            this.groupBoxLocalVentuzServer.Controls.Add(this.textBoxLocalVentuzServerDoubleStopClock);
            this.groupBoxLocalVentuzServer.ForeColor = System.Drawing.Color.White;
            this.groupBoxLocalVentuzServer.Location = new System.Drawing.Point(819, 62);
            this.groupBoxLocalVentuzServer.Name = "groupBoxLocalVentuzServer";
            this.groupBoxLocalVentuzServer.Size = new System.Drawing.Size(534, 50);
            this.groupBoxLocalVentuzServer.TabIndex = 18;
            this.groupBoxLocalVentuzServer.TabStop = false;
            this.groupBoxLocalVentuzServer.Text = "[V] local server";
            // 
            // labelLocalVentuzServerDoubleStopClock
            // 
            this.labelLocalVentuzServerDoubleStopClock.Location = new System.Drawing.Point(6, 18);
            this.labelLocalVentuzServerDoubleStopClock.Name = "labelLocalVentuzServerDoubleStopClock";
            this.labelLocalVentuzServerDoubleStopClock.Size = new System.Drawing.Size(146, 24);
            this.labelLocalVentuzServerDoubleStopClock.TabIndex = 4;
            this.labelLocalVentuzServerDoubleStopClock.Text = "double stopclock:";
            this.labelLocalVentuzServerDoubleStopClock.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxLocalVentuzServerDoubleStopClock
            // 
            this.textBoxLocalVentuzServerDoubleStopClock.Location = new System.Drawing.Point(158, 18);
            this.textBoxLocalVentuzServerDoubleStopClock.Name = "textBoxLocalVentuzServerDoubleStopClock";
            this.textBoxLocalVentuzServerDoubleStopClock.Size = new System.Drawing.Size(370, 24);
            this.textBoxLocalVentuzServerDoubleStopClock.TabIndex = 0;
            this.textBoxLocalVentuzServerDoubleStopClock.TextChanged += new System.EventHandler(this.textBoxLocalVentuzServerDoubleStopClock_TextChanged);
            // 
            // UserControlContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxLocalVentuzServer);
            this.Name = "UserControlContent";
            this.Controls.SetChildIndex(this.groupBoxPreview, 0);
            this.Controls.SetChildIndex(this.labelGameClass, 0);
            this.Controls.SetChildIndex(this.groupBoxScore, 0);
            this.Controls.SetChildIndex(this.groupBoxLocalVentuzServer, 0);
            this.groupBoxScore.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionX)).EndInit();
            this.groupBoxPreview.ResumeLayout(false);
            this.groupBoxPreview.PerformLayout();
            this.groupBoxLocalVentuzServer.ResumeLayout(false);
            this.groupBoxLocalVentuzServer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxLocalVentuzServer;
        private System.Windows.Forms.Label labelLocalVentuzServerDoubleStopClock;
        private System.Windows.Forms.TextBox textBoxLocalVentuzServerDoubleStopClock;
    }
}
