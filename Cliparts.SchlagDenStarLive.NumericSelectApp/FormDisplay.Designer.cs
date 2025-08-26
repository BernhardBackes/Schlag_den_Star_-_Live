namespace Cliparts.SchlagDenStarLive.NumericSelectApp {
    partial class FormDisplay {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDisplay));
            this.pictureBoxBuzzer = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBuzzer)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxBuzzer
            // 
            this.pictureBoxBuzzer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxBuzzer.Image = global::Cliparts.SchlagDenStarLive.NumericSelectApp.Properties.Resources.BG_NEUTRAL;
            this.pictureBoxBuzzer.Location = new System.Drawing.Point(194, 176);
            this.pictureBoxBuzzer.Name = "pictureBoxBuzzer";
            this.pictureBoxBuzzer.Size = new System.Drawing.Size(100, 50);
            this.pictureBoxBuzzer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxBuzzer.TabIndex = 0;
            this.pictureBoxBuzzer.TabStop = false;
            // 
            // FormDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBoxBuzzer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormDisplay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormDisplay";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBuzzer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxBuzzer;
    }
}