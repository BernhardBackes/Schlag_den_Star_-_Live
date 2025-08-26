namespace Cliparts.SchlagDenStarLive.MainApp.Content {
    partial class BusinessForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BusinessForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuStripFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripFileLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusFilename = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBoxGameList = new System.Windows.Forms.GroupBox();
            this.userControlGameListContent = new Cliparts.SchlagDenStarLive.MainApp.Content.GameList.UserControlGameListContent();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.groupBoxGameList.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStripFile});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1734, 24);
            this.menuStrip.TabIndex = 6;
            this.menuStrip.Text = "menuStrip1";
            // 
            // menuStripFile
            // 
            this.menuStripFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStripFileNew,
            this.menuStripFileLoad,
            this.menuStripFileSave,
            this.menuStripFileSaveAs});
            this.menuStripFile.Name = "menuStripFile";
            this.menuStripFile.Size = new System.Drawing.Size(37, 20);
            this.menuStripFile.Text = "File";
            // 
            // menuStripFileNew
            // 
            this.menuStripFileNew.Name = "menuStripFileNew";
            this.menuStripFileNew.Size = new System.Drawing.Size(123, 22);
            this.menuStripFileNew.Text = "New";
            this.menuStripFileNew.Click += new System.EventHandler(this.menuStripFileNew_Click);
            // 
            // menuStripFileLoad
            // 
            this.menuStripFileLoad.Name = "menuStripFileLoad";
            this.menuStripFileLoad.Size = new System.Drawing.Size(123, 22);
            this.menuStripFileLoad.Text = "Load";
            this.menuStripFileLoad.Click += new System.EventHandler(this.menuStripFileLoad_Click);
            // 
            // menuStripFileSave
            // 
            this.menuStripFileSave.Name = "menuStripFileSave";
            this.menuStripFileSave.Size = new System.Drawing.Size(123, 22);
            this.menuStripFileSave.Text = "Save";
            this.menuStripFileSave.Click += new System.EventHandler(this.menuStripFileSave_Click);
            // 
            // menuStripFileSaveAs
            // 
            this.menuStripFileSaveAs.Name = "menuStripFileSaveAs";
            this.menuStripFileSaveAs.Size = new System.Drawing.Size(123, 22);
            this.menuStripFileSaveAs.Text = "Save As...";
            this.menuStripFileSaveAs.Click += new System.EventHandler(this.menuStripFileSaveAs_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusFilename});
            this.statusStrip.Location = new System.Drawing.Point(0, 959);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1734, 22);
            this.statusStrip.TabIndex = 5;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusFilename
            // 
            this.toolStripStatusFilename.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusFilename.ForeColor = System.Drawing.Color.Black;
            this.toolStripStatusFilename.Name = "toolStripStatusFilename";
            this.toolStripStatusFilename.Size = new System.Drawing.Size(132, 17);
            this.toolStripStatusFilename.Text = "toolStripStatusFilename";
            // 
            // groupBoxGameList
            // 
            this.groupBoxGameList.Controls.Add(this.userControlGameListContent);
            this.groupBoxGameList.ForeColor = System.Drawing.Color.White;
            this.groupBoxGameList.Location = new System.Drawing.Point(13, 28);
            this.groupBoxGameList.Name = "groupBoxGameList";
            this.groupBoxGameList.Size = new System.Drawing.Size(1710, 925);
            this.groupBoxGameList.TabIndex = 7;
            this.groupBoxGameList.TabStop = false;
            this.groupBoxGameList.Text = "games";
            // 
            // userControlGameListContent
            // 
            this.userControlGameListContent.BackColor = System.Drawing.Color.MidnightBlue;
            this.userControlGameListContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userControlGameListContent.ForeColor = System.Drawing.Color.White;
            this.userControlGameListContent.Location = new System.Drawing.Point(7, 21);
            this.userControlGameListContent.Name = "userControlGameListContent";
            this.userControlGameListContent.Size = new System.Drawing.Size(1700, 900);
            this.userControlGameListContent.TabIndex = 0;
            // 
            // BusinessForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.DarkMagenta;
            this.ClientSize = new System.Drawing.Size(1734, 981);
            this.Controls.Add(this.groupBoxGameList);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.statusStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BusinessForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BusinessForm";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.groupBoxGameList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuStripFile;
        private System.Windows.Forms.ToolStripMenuItem menuStripFileNew;
        private System.Windows.Forms.ToolStripMenuItem menuStripFileLoad;
        private System.Windows.Forms.ToolStripMenuItem menuStripFileSave;
        private System.Windows.Forms.ToolStripMenuItem menuStripFileSaveAs;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusFilename;
        private System.Windows.Forms.GroupBox groupBoxGameList;
        private GameList.UserControlGameListContent userControlGameListContent;
    }
}