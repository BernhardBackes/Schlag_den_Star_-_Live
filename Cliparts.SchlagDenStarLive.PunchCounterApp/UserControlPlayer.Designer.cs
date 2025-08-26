
namespace Cliparts.SchlagDenStarLive.PunchCounterApp {
    partial class UserControlPlayer {
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
            this.groupBoxCgannel0 = new System.Windows.Forms.GroupBox();
            this.groupBoxChannel1 = new System.Windows.Forms.GroupBox();
            this.labelID = new System.Windows.Forms.Label();
            this.labelCounter = new System.Windows.Forms.Label();
            this.userControlChannel1 = new Cliparts.SchlagDenStarLive.PunchCounterApp.UserControlChannel();
            this.userControlChannel0 = new Cliparts.SchlagDenStarLive.PunchCounterApp.UserControlChannel();
            this.groupBoxCgannel0.SuspendLayout();
            this.groupBoxChannel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxCgannel0
            // 
            this.groupBoxCgannel0.Controls.Add(this.userControlChannel0);
            this.groupBoxCgannel0.Location = new System.Drawing.Point(3, 34);
            this.groupBoxCgannel0.Name = "groupBoxCgannel0";
            this.groupBoxCgannel0.Size = new System.Drawing.Size(265, 225);
            this.groupBoxCgannel0.TabIndex = 0;
            this.groupBoxCgannel0.TabStop = false;
            // 
            // groupBoxChannel1
            // 
            this.groupBoxChannel1.Controls.Add(this.userControlChannel1);
            this.groupBoxChannel1.Location = new System.Drawing.Point(274, 34);
            this.groupBoxChannel1.Name = "groupBoxChannel1";
            this.groupBoxChannel1.Size = new System.Drawing.Size(265, 225);
            this.groupBoxChannel1.TabIndex = 1;
            this.groupBoxChannel1.TabStop = false;
            // 
            // labelID
            // 
            this.labelID.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelID.Location = new System.Drawing.Point(3, 0);
            this.labelID.Name = "labelID";
            this.labelID.Size = new System.Drawing.Size(536, 36);
            this.labelID.TabIndex = 2;
            this.labelID.Text = "labelID";
            this.labelID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCounter
            // 
            this.labelCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCounter.Location = new System.Drawing.Point(10, 262);
            this.labelCounter.Name = "labelCounter";
            this.labelCounter.Size = new System.Drawing.Size(529, 106);
            this.labelCounter.TabIndex = 3;
            this.labelCounter.Text = "labelCounter";
            this.labelCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // userControlChannel1
            // 
            this.userControlChannel1.BackColor = System.Drawing.Color.Sienna;
            this.userControlChannel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userControlChannel1.ForeColor = System.Drawing.Color.White;
            this.userControlChannel1.Location = new System.Drawing.Point(7, 16);
            this.userControlChannel1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.userControlChannel1.Name = "userControlChannel1";
            this.userControlChannel1.Size = new System.Drawing.Size(250, 200);
            this.userControlChannel1.TabIndex = 0;
            // 
            // userControlChannel0
            // 
            this.userControlChannel0.BackColor = System.Drawing.Color.Sienna;
            this.userControlChannel0.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userControlChannel0.ForeColor = System.Drawing.Color.White;
            this.userControlChannel0.Location = new System.Drawing.Point(7, 16);
            this.userControlChannel0.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.userControlChannel0.Name = "userControlChannel0";
            this.userControlChannel0.Size = new System.Drawing.Size(250, 200);
            this.userControlChannel0.TabIndex = 0;
            // 
            // UserControlPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.Controls.Add(this.labelCounter);
            this.Controls.Add(this.labelID);
            this.Controls.Add(this.groupBoxChannel1);
            this.Controls.Add(this.groupBoxCgannel0);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "UserControlPlayer";
            this.Size = new System.Drawing.Size(545, 381);
            this.BackColorChanged += new System.EventHandler(this.UserControlPlayer_BackColorChanged);
            this.groupBoxCgannel0.ResumeLayout(false);
            this.groupBoxChannel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxCgannel0;
        private UserControlChannel userControlChannel0;
        private System.Windows.Forms.GroupBox groupBoxChannel1;
        private UserControlChannel userControlChannel1;
        private System.Windows.Forms.Label labelID;
        private System.Windows.Forms.Label labelCounter;
    }
}
