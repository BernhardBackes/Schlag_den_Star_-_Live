
namespace Cliparts.SchlagDenStarLive.PunchCounterApp {
    partial class FormProgram {
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

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProgram));
            this.groupBoxLeftPlayer = new System.Windows.Forms.GroupBox();
            this.userControlLeftPlayer = new Cliparts.SchlagDenStarLive.PunchCounterApp.UserControlPlayer();
            this.groupBoxRightPlayer = new System.Windows.Forms.GroupBox();
            this.userControlRightPlayer = new Cliparts.SchlagDenStarLive.PunchCounterApp.UserControlPlayer();
            this.labelThreshold = new System.Windows.Forms.Label();
            this.numericUpDownThreshold = new System.Windows.Forms.NumericUpDown();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.groupBoxLeftPlayer.SuspendLayout();
            this.groupBoxRightPlayer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreshold)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxLeftPlayer
            // 
            this.groupBoxLeftPlayer.Controls.Add(this.userControlLeftPlayer);
            this.groupBoxLeftPlayer.Location = new System.Drawing.Point(12, 12);
            this.groupBoxLeftPlayer.Name = "groupBoxLeftPlayer";
            this.groupBoxLeftPlayer.Size = new System.Drawing.Size(558, 408);
            this.groupBoxLeftPlayer.TabIndex = 0;
            this.groupBoxLeftPlayer.TabStop = false;
            // 
            // userControlLeftPlayer
            // 
            this.userControlLeftPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userControlLeftPlayer.Location = new System.Drawing.Point(7, 12);
            this.userControlLeftPlayer.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.userControlLeftPlayer.Name = "userControlLeftPlayer";
            this.userControlLeftPlayer.Size = new System.Drawing.Size(545, 381);
            this.userControlLeftPlayer.TabIndex = 0;
            // 
            // groupBoxRightPlayer
            // 
            this.groupBoxRightPlayer.Controls.Add(this.userControlRightPlayer);
            this.groupBoxRightPlayer.Location = new System.Drawing.Point(576, 12);
            this.groupBoxRightPlayer.Name = "groupBoxRightPlayer";
            this.groupBoxRightPlayer.Size = new System.Drawing.Size(558, 408);
            this.groupBoxRightPlayer.TabIndex = 1;
            this.groupBoxRightPlayer.TabStop = false;
            // 
            // userControlRightPlayer
            // 
            this.userControlRightPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userControlRightPlayer.Location = new System.Drawing.Point(7, 12);
            this.userControlRightPlayer.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.userControlRightPlayer.Name = "userControlRightPlayer";
            this.userControlRightPlayer.Size = new System.Drawing.Size(545, 381);
            this.userControlRightPlayer.TabIndex = 0;
            // 
            // labelThreshold
            // 
            this.labelThreshold.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelThreshold.Location = new System.Drawing.Point(13, 435);
            this.labelThreshold.Name = "labelThreshold";
            this.labelThreshold.Size = new System.Drawing.Size(180, 36);
            this.labelThreshold.TabIndex = 2;
            this.labelThreshold.Text = "threshold:";
            this.labelThreshold.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownThreshold
            // 
            this.numericUpDownThreshold.DecimalPlaces = 1;
            this.numericUpDownThreshold.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownThreshold.Location = new System.Drawing.Point(199, 433);
            this.numericUpDownThreshold.Name = "numericUpDownThreshold";
            this.numericUpDownThreshold.Size = new System.Drawing.Size(100, 40);
            this.numericUpDownThreshold.TabIndex = 3;
            this.numericUpDownThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownThreshold.ValueChanged += new System.EventHandler(this.numericUpDownThreshold_ValueChanged);
            // 
            // buttonStart
            // 
            this.buttonStart.ForeColor = System.Drawing.Color.Black;
            this.buttonStart.Location = new System.Drawing.Point(413, 431);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(157, 40);
            this.buttonStart.TabIndex = 4;
            this.buttonStart.Text = "start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.ForeColor = System.Drawing.Color.Black;
            this.buttonStop.Location = new System.Drawing.Point(576, 431);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(157, 40);
            this.buttonStop.TabIndex = 5;
            this.buttonStop.Text = "stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.ForeColor = System.Drawing.Color.Black;
            this.buttonReset.Location = new System.Drawing.Point(739, 431);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(157, 40);
            this.buttonReset.TabIndex = 6;
            this.buttonReset.Text = "reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // FormProgram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Maroon;
            this.ClientSize = new System.Drawing.Size(1142, 488);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.numericUpDownThreshold);
            this.Controls.Add(this.labelThreshold);
            this.Controls.Add(this.groupBoxRightPlayer);
            this.Controls.Add(this.groupBoxLeftPlayer);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "FormProgram";
            this.Text = "Form1";
            this.groupBoxLeftPlayer.ResumeLayout(false);
            this.groupBoxRightPlayer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreshold)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxLeftPlayer;
        private UserControlPlayer userControlLeftPlayer;
        private System.Windows.Forms.GroupBox groupBoxRightPlayer;
        private UserControlPlayer userControlRightPlayer;
        private System.Windows.Forms.Label labelThreshold;
        private System.Windows.Forms.NumericUpDown numericUpDownThreshold;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonReset;
    }
}

