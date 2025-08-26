
namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.ScrabbleListCounterScoreTimer {
    partial class UserControlGamePoolScrabbleListCounterScoreTimerLetter {
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
            this.labelLetterValue = new System.Windows.Forms.Label();
            this.checkBoxIsIdle = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // labelLetterValue
            // 
            this.labelLetterValue.BackColor = System.Drawing.SystemColors.Control;
            this.labelLetterValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelLetterValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLetterValue.ForeColor = System.Drawing.Color.Black;
            this.labelLetterValue.Location = new System.Drawing.Point(3, 4);
            this.labelLetterValue.Name = "labelLetterValue";
            this.labelLetterValue.Size = new System.Drawing.Size(60, 40);
            this.labelLetterValue.TabIndex = 0;
            this.labelLetterValue.Text = "?";
            this.labelLetterValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelLetterValue.EnabledChanged += new System.EventHandler(this.labelLetterValue_EnabledChanged);
            // 
            // checkBoxIsIdle
            // 
            this.checkBoxIsIdle.AutoSize = true;
            this.checkBoxIsIdle.Location = new System.Drawing.Point(7, 48);
            this.checkBoxIsIdle.Name = "checkBoxIsIdle";
            this.checkBoxIsIdle.Size = new System.Drawing.Size(53, 22);
            this.checkBoxIsIdle.TabIndex = 1;
            this.checkBoxIsIdle.Text = "idle";
            this.checkBoxIsIdle.UseVisualStyleBackColor = true;
            this.checkBoxIsIdle.CheckedChanged += new System.EventHandler(this.checkBoxIsIdle_CheckedChanged);
            // 
            // UserControlGamePoolScrabbleListLetter
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Crimson;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.checkBoxIsIdle);
            this.Controls.Add(this.labelLetterValue);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UserControlGamePoolScrabbleListLetter";
            this.Size = new System.Drawing.Size(68, 72);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelLetterValue;
        private System.Windows.Forms.CheckBox checkBoxIsIdle;
    }
}
