namespace Cliparts.SchlagDenStarLive.MainApp.Content.Playlist {
    partial class UserControlPlaylist {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlPlaylist));
            this.listBoxDataList = new System.Windows.Forms.ListBox();
            this.radioButtonFullscreen = new System.Windows.Forms.RadioButton();
            this.radioButtonInsert = new System.Windows.Forms.RadioButton();
            this.buttonEditList = new System.Windows.Forms.Button();
            this.buttonPause = new System.Windows.Forms.Button();
            this.buttonEject = new System.Windows.Forms.Button();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxDataList
            // 
            this.listBoxDataList.FormattingEnabled = true;
            this.listBoxDataList.ItemHeight = 18;
            this.listBoxDataList.Location = new System.Drawing.Point(0, 0);
            this.listBoxDataList.Name = "listBoxDataList";
            this.listBoxDataList.Size = new System.Drawing.Size(330, 94);
            this.listBoxDataList.TabIndex = 0;
            this.listBoxDataList.SelectedIndexChanged += new System.EventHandler(this.listBoxDataList_SelectedIndexChanged);
            // 
            // radioButtonFullscreen
            // 
            this.radioButtonFullscreen.AutoSize = true;
            this.radioButtonFullscreen.Location = new System.Drawing.Point(3, 150);
            this.radioButtonFullscreen.Name = "radioButtonFullscreen";
            this.radioButtonFullscreen.Size = new System.Drawing.Size(99, 22);
            this.radioButtonFullscreen.TabIndex = 1;
            this.radioButtonFullscreen.TabStop = true;
            this.radioButtonFullscreen.Text = "fullscreen";
            this.radioButtonFullscreen.UseVisualStyleBackColor = true;
            this.radioButtonFullscreen.CheckedChanged += new System.EventHandler(this.radioButtonFullscreen_CheckedChanged);
            // 
            // radioButtonInsert
            // 
            this.radioButtonInsert.AutoSize = true;
            this.radioButtonInsert.Location = new System.Drawing.Point(3, 125);
            this.radioButtonInsert.Name = "radioButtonInsert";
            this.radioButtonInsert.Size = new System.Drawing.Size(68, 22);
            this.radioButtonInsert.TabIndex = 2;
            this.radioButtonInsert.TabStop = true;
            this.radioButtonInsert.Text = "insert";
            this.radioButtonInsert.UseVisualStyleBackColor = true;
            this.radioButtonInsert.CheckedChanged += new System.EventHandler(this.radioButtonInsert_CheckedChanged);
            // 
            // buttonEditList
            // 
            this.buttonEditList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEditList.ForeColor = System.Drawing.Color.Black;
            this.buttonEditList.Location = new System.Drawing.Point(0, 98);
            this.buttonEditList.Name = "buttonEditList";
            this.buttonEditList.Size = new System.Drawing.Size(102, 24);
            this.buttonEditList.TabIndex = 3;
            this.buttonEditList.Text = "edit list";
            this.buttonEditList.UseVisualStyleBackColor = true;
            this.buttonEditList.Click += new System.EventHandler(this.buttonEditList_Click);
            // 
            // buttonPause
            // 
            this.buttonPause.BackColor = System.Drawing.Color.White;
            this.buttonPause.Image = global::Cliparts.SchlagDenStarLive.MainApp.Properties.Resources.pause_button;
            this.buttonPause.Location = new System.Drawing.Point(180, 98);
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size(75, 75);
            this.buttonPause.TabIndex = 6;
            this.buttonPause.UseVisualStyleBackColor = false;
            this.buttonPause.Click += new System.EventHandler(this.buttonPause_Click);
            // 
            // buttonEject
            // 
            this.buttonEject.BackColor = System.Drawing.Color.White;
            this.buttonEject.Image = global::Cliparts.SchlagDenStarLive.MainApp.Properties.Resources.eject_button;
            this.buttonEject.Location = new System.Drawing.Point(255, 98);
            this.buttonEject.Name = "buttonEject";
            this.buttonEject.Size = new System.Drawing.Size(75, 75);
            this.buttonEject.TabIndex = 5;
            this.buttonEject.UseVisualStyleBackColor = false;
            this.buttonEject.Click += new System.EventHandler(this.buttonEject_Click);
            // 
            // buttonPlay
            // 
            this.buttonPlay.BackColor = System.Drawing.Color.White;
            this.buttonPlay.Image = ((System.Drawing.Image)(resources.GetObject("buttonPlay.Image")));
            this.buttonPlay.Location = new System.Drawing.Point(105, 98);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(75, 75);
            this.buttonPlay.TabIndex = 4;
            this.buttonPlay.UseVisualStyleBackColor = false;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // UserControlPlaylist
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.DarkRed;
            this.Controls.Add(this.buttonPause);
            this.Controls.Add(this.buttonEject);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.buttonEditList);
            this.Controls.Add(this.radioButtonInsert);
            this.Controls.Add(this.radioButtonFullscreen);
            this.Controls.Add(this.listBoxDataList);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "UserControlPlaylist";
            this.Size = new System.Drawing.Size(330, 175);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxDataList;
        private System.Windows.Forms.RadioButton radioButtonFullscreen;
        private System.Windows.Forms.RadioButton radioButtonInsert;
        private System.Windows.Forms.Button buttonEditList;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Button buttonEject;
        private System.Windows.Forms.Button buttonPause;
    }
}
