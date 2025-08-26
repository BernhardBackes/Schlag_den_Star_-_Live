namespace Cliparts.SchlagDenStarLive.MainApp {

    partial class UserControlRecTrigger {
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
            this.labelValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelValue
            // 
            this.labelValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelValue.Location = new System.Drawing.Point(0, 0);
            this.labelValue.Name = "labelValue";
            this.labelValue.Size = new System.Drawing.Size(55, 24);
            this.labelValue.TabIndex = 0;
            this.labelValue.Text = "labelValue";
            this.labelValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UserControlRecTrigger
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.labelValue);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UserControlRecTrigger";
            this.Size = new System.Drawing.Size(55, 24);
            this.BackColorChanged += new System.EventHandler(this.UserControlRecTrigger_BackColorChanged);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label labelValue;


    }
}
