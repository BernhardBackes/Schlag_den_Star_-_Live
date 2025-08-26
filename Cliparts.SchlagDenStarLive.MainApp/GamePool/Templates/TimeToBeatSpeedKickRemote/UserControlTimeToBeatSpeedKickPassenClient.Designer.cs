namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimeToBeatSpeedKickRemote {
    partial class UserControlTimeToBeatSpeedKickRemotePassenClient {
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
            this.labelStatus = new System.Windows.Forms.Label();
            this.textBoxHostName = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.buttonStartCourse = new System.Windows.Forms.Button();
            this.buttonStopCourse = new System.Windows.Forms.Button();
            this.buttonResetCourse = new System.Windows.Forms.Button();
            this.groupBoxCourses = new System.Windows.Forms.GroupBox();
            this.listBoxCourses = new System.Windows.Forms.ListBox();
            this.labelProgress = new System.Windows.Forms.Label();
            this.groupBoxCourses.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelStatus
            // 
            this.labelStatus.Location = new System.Drawing.Point(4, 3);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(193, 24);
            this.labelStatus.TabIndex = 1;
            this.labelStatus.Text = "host";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // textBoxHostName
            // 
            this.textBoxHostName.Location = new System.Drawing.Point(4, 30);
            this.textBoxHostName.Name = "textBoxHostName";
            this.textBoxHostName.ReadOnly = true;
            this.textBoxHostName.Size = new System.Drawing.Size(193, 24);
            this.textBoxHostName.TabIndex = 2;
            this.textBoxHostName.Text = "textBoxHostname";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonConnect.ForeColor = System.Drawing.Color.Black;
            this.buttonConnect.Location = new System.Drawing.Point(4, 58);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(95, 24);
            this.buttonConnect.TabIndex = 3;
            this.buttonConnect.Text = "connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDisconnect.ForeColor = System.Drawing.Color.Black;
            this.buttonDisconnect.Location = new System.Drawing.Point(102, 58);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(95, 24);
            this.buttonDisconnect.TabIndex = 4;
            this.buttonDisconnect.Text = "disconnect";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // buttonStartCourse
            // 
            this.buttonStartCourse.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStartCourse.ForeColor = System.Drawing.Color.Black;
            this.buttonStartCourse.Location = new System.Drawing.Point(6, 195);
            this.buttonStartCourse.Name = "buttonStartCourse";
            this.buttonStartCourse.Size = new System.Drawing.Size(182, 26);
            this.buttonStartCourse.TabIndex = 5;
            this.buttonStartCourse.Text = "start";
            this.buttonStartCourse.UseVisualStyleBackColor = true;
            this.buttonStartCourse.Click += new System.EventHandler(this.buttonStartCourse_Click);
            // 
            // buttonStopCourse
            // 
            this.buttonStopCourse.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStopCourse.ForeColor = System.Drawing.Color.Black;
            this.buttonStopCourse.Location = new System.Drawing.Point(6, 225);
            this.buttonStopCourse.Name = "buttonStopCourse";
            this.buttonStopCourse.Size = new System.Drawing.Size(182, 26);
            this.buttonStopCourse.TabIndex = 6;
            this.buttonStopCourse.Text = "stop";
            this.buttonStopCourse.UseVisualStyleBackColor = true;
            this.buttonStopCourse.Click += new System.EventHandler(this.buttonStopCourse_Click);
            // 
            // buttonResetCourse
            // 
            this.buttonResetCourse.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonResetCourse.ForeColor = System.Drawing.Color.Black;
            this.buttonResetCourse.Location = new System.Drawing.Point(6, 255);
            this.buttonResetCourse.Name = "buttonResetCourse";
            this.buttonResetCourse.Size = new System.Drawing.Size(182, 26);
            this.buttonResetCourse.TabIndex = 7;
            this.buttonResetCourse.Text = "reset";
            this.buttonResetCourse.UseVisualStyleBackColor = true;
            this.buttonResetCourse.Click += new System.EventHandler(this.buttonResetCourse_Click);
            // 
            // groupBoxCourses
            // 
            this.groupBoxCourses.Controls.Add(this.labelProgress);
            this.groupBoxCourses.Controls.Add(this.listBoxCourses);
            this.groupBoxCourses.Controls.Add(this.buttonStopCourse);
            this.groupBoxCourses.Controls.Add(this.buttonResetCourse);
            this.groupBoxCourses.Controls.Add(this.buttonStartCourse);
            this.groupBoxCourses.ForeColor = System.Drawing.Color.White;
            this.groupBoxCourses.Location = new System.Drawing.Point(4, 88);
            this.groupBoxCourses.Name = "groupBoxCourses";
            this.groupBoxCourses.Size = new System.Drawing.Size(193, 287);
            this.groupBoxCourses.TabIndex = 8;
            this.groupBoxCourses.TabStop = false;
            this.groupBoxCourses.Text = "courses";
            // 
            // listBoxCourses
            // 
            this.listBoxCourses.FormattingEnabled = true;
            this.listBoxCourses.ItemHeight = 18;
            this.listBoxCourses.Location = new System.Drawing.Point(6, 23);
            this.listBoxCourses.Name = "listBoxCourses";
            this.listBoxCourses.Size = new System.Drawing.Size(182, 130);
            this.listBoxCourses.TabIndex = 8;
            this.listBoxCourses.SelectedIndexChanged += new System.EventHandler(this.listBoxCourses_SelectedIndexChanged);
            // 
            // labelProgress
            // 
            this.labelProgress.Location = new System.Drawing.Point(6, 163);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(181, 23);
            this.labelProgress.TabIndex = 9;
            this.labelProgress.Text = "labelProgress";
            this.labelProgress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UserControlTimeToBeatSpeedKickRemotePassenClient
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Indigo;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.groupBoxCourses);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.textBoxHostName);
            this.Controls.Add(this.labelStatus);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "UserControlTimeToBeatSpeedKickRemotePassenClient";
            this.Size = new System.Drawing.Size(200, 378);
            this.groupBoxCourses.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.TextBox textBoxHostName;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.Button buttonStartCourse;
        private System.Windows.Forms.Button buttonStopCourse;
        private System.Windows.Forms.Button buttonResetCourse;
        private System.Windows.Forms.GroupBox groupBoxCourses;
        private System.Windows.Forms.ListBox listBoxCourses;
        private System.Windows.Forms.Label labelProgress;
    }
}
