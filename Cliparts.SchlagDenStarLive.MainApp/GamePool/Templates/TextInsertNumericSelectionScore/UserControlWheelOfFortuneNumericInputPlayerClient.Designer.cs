namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TextInsertNumericSelectionScore {
    partial class UserControlTextInsertNumericSelectionScorePlayerClient {
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
            this.labelName = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.textBoxHostName = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.buttonSetIdle = new System.Windows.Forms.Button();
            this.buttonSetActive = new System.Windows.Forms.Button();
            this.buttonSetLocked = new System.Windows.Forms.Button();
            this.buttonSetUnlocked = new System.Windows.Forms.Button();
            this.labelPlayerValue = new System.Windows.Forms.Label();
            this.textBoxPlayerValue = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.Location = new System.Drawing.Point(3, 3);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(272, 23);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "labelName";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStatus
            // 
            this.labelStatus.Location = new System.Drawing.Point(7, 30);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(60, 52);
            this.labelStatus.TabIndex = 1;
            this.labelStatus.Text = "host:";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxHostName
            // 
            this.textBoxHostName.Location = new System.Drawing.Point(69, 30);
            this.textBoxHostName.Name = "textBoxHostName";
            this.textBoxHostName.ReadOnly = true;
            this.textBoxHostName.Size = new System.Drawing.Size(206, 24);
            this.textBoxHostName.TabIndex = 2;
            this.textBoxHostName.Text = "textBoxHostname";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonConnect.ForeColor = System.Drawing.Color.Black;
            this.buttonConnect.Location = new System.Drawing.Point(69, 58);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(100, 24);
            this.buttonConnect.TabIndex = 3;
            this.buttonConnect.Text = "connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDisconnect.ForeColor = System.Drawing.Color.Black;
            this.buttonDisconnect.Location = new System.Drawing.Point(175, 58);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(100, 24);
            this.buttonDisconnect.TabIndex = 4;
            this.buttonDisconnect.Text = "disconnect";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // buttonSetIdle
            // 
            this.buttonSetIdle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSetIdle.ForeColor = System.Drawing.Color.Black;
            this.buttonSetIdle.Location = new System.Drawing.Point(6, 88);
            this.buttonSetIdle.Name = "buttonSetIdle";
            this.buttonSetIdle.Size = new System.Drawing.Size(62, 24);
            this.buttonSetIdle.TabIndex = 5;
            this.buttonSetIdle.Text = "idle";
            this.buttonSetIdle.UseVisualStyleBackColor = true;
            this.buttonSetIdle.Click += new System.EventHandler(this.buttonSetIdle_Click);
            // 
            // buttonSetActive
            // 
            this.buttonSetActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSetActive.ForeColor = System.Drawing.Color.Black;
            this.buttonSetActive.Location = new System.Drawing.Point(75, 88);
            this.buttonSetActive.Name = "buttonSetActive";
            this.buttonSetActive.Size = new System.Drawing.Size(62, 24);
            this.buttonSetActive.TabIndex = 6;
            this.buttonSetActive.Text = "active";
            this.buttonSetActive.UseVisualStyleBackColor = true;
            this.buttonSetActive.Click += new System.EventHandler(this.buttonSetActive_Click);
            // 
            // buttonSetLocked
            // 
            this.buttonSetLocked.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSetLocked.ForeColor = System.Drawing.Color.Black;
            this.buttonSetLocked.Location = new System.Drawing.Point(144, 88);
            this.buttonSetLocked.Name = "buttonSetLocked";
            this.buttonSetLocked.Size = new System.Drawing.Size(62, 24);
            this.buttonSetLocked.TabIndex = 7;
            this.buttonSetLocked.Text = "lock";
            this.buttonSetLocked.UseVisualStyleBackColor = true;
            this.buttonSetLocked.Click += new System.EventHandler(this.buttonSetLocked_Click);
            // 
            // buttonSetUnlocked
            // 
            this.buttonSetUnlocked.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSetUnlocked.ForeColor = System.Drawing.Color.Black;
            this.buttonSetUnlocked.Location = new System.Drawing.Point(213, 88);
            this.buttonSetUnlocked.Name = "buttonSetUnlocked";
            this.buttonSetUnlocked.Size = new System.Drawing.Size(62, 24);
            this.buttonSetUnlocked.TabIndex = 8;
            this.buttonSetUnlocked.Text = "unlock";
            this.buttonSetUnlocked.UseVisualStyleBackColor = true;
            this.buttonSetUnlocked.Click += new System.EventHandler(this.buttonSetUnlocked_Click);
            // 
            // labelPlayerValue
            // 
            this.labelPlayerValue.Location = new System.Drawing.Point(7, 115);
            this.labelPlayerValue.Name = "labelPlayerValue";
            this.labelPlayerValue.Size = new System.Drawing.Size(60, 31);
            this.labelPlayerValue.TabIndex = 9;
            this.labelPlayerValue.Text = "value:";
            this.labelPlayerValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxPlayerValue
            // 
            this.textBoxPlayerValue.Location = new System.Drawing.Point(69, 118);
            this.textBoxPlayerValue.Name = "textBoxPlayerValue";
            this.textBoxPlayerValue.ReadOnly = true;
            this.textBoxPlayerValue.Size = new System.Drawing.Size(100, 24);
            this.textBoxPlayerValue.TabIndex = 10;
            this.textBoxPlayerValue.Text = "textBoxPlayerValue";
            this.textBoxPlayerValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // UserControlTCPComPlayerClient
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Indigo;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.textBoxPlayerValue);
            this.Controls.Add(this.labelPlayerValue);
            this.Controls.Add(this.buttonSetUnlocked);
            this.Controls.Add(this.buttonSetLocked);
            this.Controls.Add(this.buttonSetActive);
            this.Controls.Add(this.buttonSetIdle);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.textBoxHostName);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.labelName);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "UserControlTCPComPlayerClient";
            this.Size = new System.Drawing.Size(278, 150);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.TextBox textBoxHostName;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.Button buttonSetIdle;
        private System.Windows.Forms.Button buttonSetActive;
        private System.Windows.Forms.Button buttonSetLocked;
        private System.Windows.Forms.Button buttonSetUnlocked;
        private System.Windows.Forms.Label labelPlayerValue;
        private System.Windows.Forms.TextBox textBoxPlayerValue;
    }
}
