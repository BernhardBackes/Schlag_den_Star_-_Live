namespace Cliparts.SchlagDenStarLive.MainApp.Content.GameList {
    partial class UserControlGameListContent {
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
            this.labelNoGame = new System.Windows.Forms.Label();
            this.listBoxGames = new System.Windows.Forms.ListBox();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonRename = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonMoveDown = new System.Windows.Forms.Button();
            this.buttonMoveUp = new System.Windows.Forms.Button();
            this.buttonExportSettings = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelNoGame
            // 
            this.labelNoGame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelNoGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNoGame.ForeColor = System.Drawing.Color.CadetBlue;
            this.labelNoGame.Location = new System.Drawing.Point(340, 3);
            this.labelNoGame.Name = "labelNoGame";
            this.labelNoGame.Size = new System.Drawing.Size(1360, 895);
            this.labelNoGame.TabIndex = 21;
            this.labelNoGame.Text = "no game loaded";
            this.labelNoGame.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listBoxGames
            // 
            this.listBoxGames.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxGames.FormattingEnabled = true;
            this.listBoxGames.HorizontalScrollbar = true;
            this.listBoxGames.ItemHeight = 24;
            this.listBoxGames.Items.AddRange(new object[] {
            "- Spiel 1",
            "- Spiel 2",
            "- Spiel 3",
            "- Spiel 4",
            "- Spiel 5",
            "- Spiel 6",
            "- Spiel 7",
            "- Spiel 8",
            "- Spiel 9",
            "- Spiel 10",
            "- Spiel 11",
            "- Spiel 12",
            "- Spiel 13",
            "- Spiel 14",
            "- Spiel 15",
            "- Stechen",
            "- Ersatz 1",
            "- Ersatz 2",
            "- Ersatz 3",
            "- Ersatz 4",
            "- Ersatz 5"});
            this.listBoxGames.Location = new System.Drawing.Point(3, 3);
            this.listBoxGames.Name = "listBoxGames";
            this.listBoxGames.Size = new System.Drawing.Size(331, 532);
            this.listBoxGames.TabIndex = 20;
            this.listBoxGames.SelectedIndexChanged += new System.EventHandler(this.listBoxGames_SelectedIndexChanged);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRemove.ForeColor = System.Drawing.Color.Black;
            this.buttonRemove.Location = new System.Drawing.Point(3, 649);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(231, 39);
            this.buttonRemove.TabIndex = 27;
            this.buttonRemove.Text = "remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonRename
            // 
            this.buttonRename.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRename.ForeColor = System.Drawing.Color.Black;
            this.buttonRename.Location = new System.Drawing.Point(3, 609);
            this.buttonRename.Name = "buttonRename";
            this.buttonRename.Size = new System.Drawing.Size(231, 39);
            this.buttonRename.TabIndex = 26;
            this.buttonRename.Text = "rename";
            this.buttonRename.UseVisualStyleBackColor = true;
            this.buttonRename.Click += new System.EventHandler(this.buttonRename_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAdd.ForeColor = System.Drawing.Color.Black;
            this.buttonAdd.Location = new System.Drawing.Point(3, 569);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(231, 39);
            this.buttonAdd.TabIndex = 25;
            this.buttonAdd.Text = "add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonMoveDown
            // 
            this.buttonMoveDown.ForeColor = System.Drawing.Color.Black;
            this.buttonMoveDown.Location = new System.Drawing.Point(3, 536);
            this.buttonMoveDown.Name = "buttonMoveDown";
            this.buttonMoveDown.Size = new System.Drawing.Size(115, 27);
            this.buttonMoveDown.TabIndex = 24;
            this.buttonMoveDown.Text = "move down";
            this.buttonMoveDown.UseVisualStyleBackColor = true;
            this.buttonMoveDown.Click += new System.EventHandler(this.buttonMoveDown_Click);
            // 
            // buttonMoveUp
            // 
            this.buttonMoveUp.ForeColor = System.Drawing.Color.Black;
            this.buttonMoveUp.Location = new System.Drawing.Point(119, 536);
            this.buttonMoveUp.Name = "buttonMoveUp";
            this.buttonMoveUp.Size = new System.Drawing.Size(115, 27);
            this.buttonMoveUp.TabIndex = 23;
            this.buttonMoveUp.Text = "move up";
            this.buttonMoveUp.UseVisualStyleBackColor = true;
            this.buttonMoveUp.Click += new System.EventHandler(this.buttonMoveUp_Click);
            // 
            // buttonExportSettings
            // 
            this.buttonExportSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExportSettings.ForeColor = System.Drawing.Color.Black;
            this.buttonExportSettings.Location = new System.Drawing.Point(3, 694);
            this.buttonExportSettings.Name = "buttonExportSettings";
            this.buttonExportSettings.Size = new System.Drawing.Size(231, 39);
            this.buttonExportSettings.TabIndex = 28;
            this.buttonExportSettings.Text = "export settings";
            this.buttonExportSettings.UseVisualStyleBackColor = true;
            this.buttonExportSettings.Click += new System.EventHandler(this.buttonExportSettings_Click);
            // 
            // UserControlGameListContent
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.Controls.Add(this.buttonExportSettings);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonRename);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonMoveDown);
            this.Controls.Add(this.buttonMoveUp);
            this.Controls.Add(this.labelNoGame);
            this.Controls.Add(this.listBoxGames);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "UserControlGameListContent";
            this.Size = new System.Drawing.Size(1700, 900);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelNoGame;
        private System.Windows.Forms.ListBox listBoxGames;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonRename;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonMoveDown;
        private System.Windows.Forms.Button buttonMoveUp;
        private System.Windows.Forms.Button buttonExportSettings;
    }
}
