namespace Cliparts.SchlagDenStarLive.MainApp.Content.GameList {
    partial class UserControlGameList {
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
            this.buttonNotepadFont = new System.Windows.Forms.Button();
            this.richTextBoxNotepad = new System.Windows.Forms.RichTextBox();
            this.labelNotepad = new System.Windows.Forms.Label();
            this.listBoxGames = new System.Windows.Forms.ListBox();
            this.labelNoGame = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonNotepadFont
            // 
            this.buttonNotepadFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNotepadFont.ForeColor = System.Drawing.Color.Black;
            this.buttonNotepadFont.Location = new System.Drawing.Point(181, 546);
            this.buttonNotepadFont.Name = "buttonNotepadFont";
            this.buttonNotepadFont.Size = new System.Drawing.Size(53, 22);
            this.buttonNotepadFont.TabIndex = 22;
            this.buttonNotepadFont.Text = "font";
            this.buttonNotepadFont.UseVisualStyleBackColor = true;
            this.buttonNotepadFont.Click += new System.EventHandler(this.buttonNotepadFont_Click);
            // 
            // richTextBoxNotepad
            // 
            this.richTextBoxNotepad.AcceptsTab = true;
            this.richTextBoxNotepad.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxNotepad.Location = new System.Drawing.Point(3, 569);
            this.richTextBoxNotepad.Name = "richTextBoxNotepad";
            this.richTextBoxNotepad.Size = new System.Drawing.Size(231, 318);
            this.richTextBoxNotepad.TabIndex = 21;
            this.richTextBoxNotepad.Text = "";
            this.richTextBoxNotepad.Enter += new System.EventHandler(this.richTextBoxNotepad_Enter);
            this.richTextBoxNotepad.Leave += new System.EventHandler(this.richTextBoxNotepad_Leave);
            // 
            // labelNotepad
            // 
            this.labelNotepad.Location = new System.Drawing.Point(3, 545);
            this.labelNotepad.Name = "labelNotepad";
            this.labelNotepad.Size = new System.Drawing.Size(231, 22);
            this.labelNotepad.TabIndex = 20;
            this.labelNotepad.Text = "Notepad";
            this.labelNotepad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listBoxGames
            // 
            this.listBoxGames.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxGames.FormattingEnabled = true;
            this.listBoxGames.HorizontalScrollbar = true;
            this.listBoxGames.ItemHeight = 24;
            this.listBoxGames.Items.AddRange(new object[] {
            "- Voting",
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
            this.listBoxGames.Size = new System.Drawing.Size(231, 532);
            this.listBoxGames.TabIndex = 18;
            this.listBoxGames.SelectedIndexChanged += new System.EventHandler(this.listBoxGames_SelectedIndexChanged);
            // 
            // labelNoGame
            // 
            this.labelNoGame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelNoGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNoGame.ForeColor = System.Drawing.Color.CadetBlue;
            this.labelNoGame.Location = new System.Drawing.Point(240, 3);
            this.labelNoGame.Name = "labelNoGame";
            this.labelNoGame.Size = new System.Drawing.Size(973, 884);
            this.labelNoGame.TabIndex = 23;
            this.labelNoGame.Text = "no game loaded";
            this.labelNoGame.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UserControlGameList
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Purple;
            this.Controls.Add(this.labelNoGame);
            this.Controls.Add(this.buttonNotepadFont);
            this.Controls.Add(this.richTextBoxNotepad);
            this.Controls.Add(this.labelNotepad);
            this.Controls.Add(this.listBoxGames);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "UserControlGameList";
            this.Size = new System.Drawing.Size(1216, 890);
            this.BackColorChanged += new System.EventHandler(this.UserControlGameList_BackColorChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonNotepadFont;
        private System.Windows.Forms.RichTextBox richTextBoxNotepad;
        private System.Windows.Forms.Label labelNotepad;
        private System.Windows.Forms.ListBox listBoxGames;
        private System.Windows.Forms.Label labelNoGame;
    }
}
