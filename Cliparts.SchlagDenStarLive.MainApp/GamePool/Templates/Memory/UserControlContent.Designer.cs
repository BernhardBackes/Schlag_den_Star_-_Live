namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.Memory {
    partial class UserControlContent {
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
            this.radioButtonSourceFullscreen = new System.Windows.Forms.RadioButton();
            this.radioButtonSourceInsert = new System.Windows.Forms.RadioButton();
            this.groupBoxLocalVentuzServer = new System.Windows.Forms.GroupBox();
            this.labelLocalVentuzServerGameboard = new System.Windows.Forms.Label();
            this.textBoxLocalVentuzServerGameboard = new System.Windows.Forms.TextBox();
            this.buttonSaveAs = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.labelFilename = new System.Windows.Forms.Label();
            this.panelData = new System.Windows.Forms.Panel();
            this.buttonShowAllCards = new System.Windows.Forms.Button();
            this.buttonDataRandomize = new System.Windows.Forms.Button();
            this.labelDataUnusedCardIDs = new System.Windows.Forms.Label();
            this.listBoxDataUnusedCardIDs = new System.Windows.Forms.ListBox();
            this.comboBoxDataCardID_00_01 = new System.Windows.Forms.ComboBox();
            this.comboBoxDataCardID_00_00 = new System.Windows.Forms.ComboBox();
            this.textBoxDataPictureFilename_00 = new System.Windows.Forms.TextBox();
            this.groupBoxBorder = new System.Windows.Forms.GroupBox();
            this.labelBorderScalingUnit = new System.Windows.Forms.Label();
            this.labelBorderScaling = new System.Windows.Forms.Label();
            this.numericUpDownBorderScaling = new System.Windows.Forms.NumericUpDown();
            this.labelBorderStyle = new System.Windows.Forms.Label();
            this.labelBorderPositionY = new System.Windows.Forms.Label();
            this.comboBoxBorderStyle = new System.Windows.Forms.ComboBox();
            this.labelBorderPositionX = new System.Windows.Forms.Label();
            this.numericUpDownBorderPositionY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownBorderPositionX = new System.Windows.Forms.NumericUpDown();
            this.checkBoxGameboardDuellMode = new System.Windows.Forms.CheckBox();
            this.groupBoxScore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionX)).BeginInit();
            this.groupBoxPreview.SuspendLayout();
            this.groupBoxLocalVentuzServer.SuspendLayout();
            this.panelData.SuspendLayout();
            this.groupBoxBorder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBorderScaling)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBorderPositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBorderPositionX)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxScoreStyle
            // 
            this.comboBoxScoreStyle.Size = new System.Drawing.Size(148, 26);
            // 
            // groupBoxPreview
            // 
            this.groupBoxPreview.Controls.Add(this.radioButtonSourceFullscreen);
            this.groupBoxPreview.Controls.Add(this.radioButtonSourceInsert);
            this.groupBoxPreview.Controls.SetChildIndex(this.checkBoxShowSafeArea, 0);
            this.groupBoxPreview.Controls.SetChildIndex(this.radioButtonSourceInsert, 0);
            this.groupBoxPreview.Controls.SetChildIndex(this.radioButtonSourceFullscreen, 0);
            // 
            // radioButtonSourceFullscreen
            // 
            this.radioButtonSourceFullscreen.AutoSize = true;
            this.radioButtonSourceFullscreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSourceFullscreen.Location = new System.Drawing.Point(186, 14);
            this.radioButtonSourceFullscreen.Name = "radioButtonSourceFullscreen";
            this.radioButtonSourceFullscreen.Size = new System.Drawing.Size(99, 22);
            this.radioButtonSourceFullscreen.TabIndex = 18;
            this.radioButtonSourceFullscreen.TabStop = true;
            this.radioButtonSourceFullscreen.Text = "fullscreen";
            this.radioButtonSourceFullscreen.UseVisualStyleBackColor = true;
            this.radioButtonSourceFullscreen.CheckedChanged += new System.EventHandler(this.radioButtonSourceFullscreen_CheckedChanged);
            // 
            // radioButtonSourceInsert
            // 
            this.radioButtonSourceInsert.AutoSize = true;
            this.radioButtonSourceInsert.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSourceInsert.Location = new System.Drawing.Point(112, 14);
            this.radioButtonSourceInsert.Name = "radioButtonSourceInsert";
            this.radioButtonSourceInsert.Size = new System.Drawing.Size(68, 22);
            this.radioButtonSourceInsert.TabIndex = 17;
            this.radioButtonSourceInsert.TabStop = true;
            this.radioButtonSourceInsert.Text = "insert";
            this.radioButtonSourceInsert.UseVisualStyleBackColor = true;
            this.radioButtonSourceInsert.CheckedChanged += new System.EventHandler(this.radioButtonSourceInsert_CheckedChanged);
            // 
            // groupBoxLocalVentuzServer
            // 
            this.groupBoxLocalVentuzServer.Controls.Add(this.checkBoxGameboardDuellMode);
            this.groupBoxLocalVentuzServer.Controls.Add(this.labelLocalVentuzServerGameboard);
            this.groupBoxLocalVentuzServer.Controls.Add(this.textBoxLocalVentuzServerGameboard);
            this.groupBoxLocalVentuzServer.ForeColor = System.Drawing.Color.White;
            this.groupBoxLocalVentuzServer.Location = new System.Drawing.Point(819, 149);
            this.groupBoxLocalVentuzServer.Name = "groupBoxLocalVentuzServer";
            this.groupBoxLocalVentuzServer.Size = new System.Drawing.Size(534, 75);
            this.groupBoxLocalVentuzServer.TabIndex = 18;
            this.groupBoxLocalVentuzServer.TabStop = false;
            this.groupBoxLocalVentuzServer.Text = "[V] local server";
            // 
            // labelLocalVentuzServerGameboard
            // 
            this.labelLocalVentuzServerGameboard.Location = new System.Drawing.Point(6, 18);
            this.labelLocalVentuzServerGameboard.Name = "labelLocalVentuzServerGameboard";
            this.labelLocalVentuzServerGameboard.Size = new System.Drawing.Size(100, 24);
            this.labelLocalVentuzServerGameboard.TabIndex = 4;
            this.labelLocalVentuzServerGameboard.Text = "gameboard:";
            this.labelLocalVentuzServerGameboard.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxLocalVentuzServerGameboard
            // 
            this.textBoxLocalVentuzServerGameboard.Location = new System.Drawing.Point(112, 18);
            this.textBoxLocalVentuzServerGameboard.Name = "textBoxLocalVentuzServerGameboard";
            this.textBoxLocalVentuzServerGameboard.Size = new System.Drawing.Size(416, 24);
            this.textBoxLocalVentuzServerGameboard.TabIndex = 0;
            this.textBoxLocalVentuzServerGameboard.Text = "textBoxLocalVentuzServerGameboard";
            this.textBoxLocalVentuzServerGameboard.TextChanged += new System.EventHandler(this.textBoxLocalVentuzServerGameboard_TextChanged);
            // 
            // buttonSaveAs
            // 
            this.buttonSaveAs.ForeColor = System.Drawing.Color.Black;
            this.buttonSaveAs.Location = new System.Drawing.Point(1272, 461);
            this.buttonSaveAs.Name = "buttonSaveAs";
            this.buttonSaveAs.Size = new System.Drawing.Size(75, 26);
            this.buttonSaveAs.TabIndex = 38;
            this.buttonSaveAs.Text = "save as";
            this.buttonSaveAs.UseVisualStyleBackColor = true;
            this.buttonSaveAs.Click += new System.EventHandler(this.buttonSaveAs_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.ForeColor = System.Drawing.Color.Black;
            this.buttonSave.Location = new System.Drawing.Point(1191, 461);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 26);
            this.buttonSave.TabIndex = 37;
            this.buttonSave.Text = "save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.ForeColor = System.Drawing.Color.Black;
            this.buttonLoad.Location = new System.Drawing.Point(1110, 461);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(75, 26);
            this.buttonLoad.TabIndex = 36;
            this.buttonLoad.Text = "load";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // labelFilename
            // 
            this.labelFilename.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelFilename.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFilename.Location = new System.Drawing.Point(8, 462);
            this.labelFilename.Name = "labelFilename";
            this.labelFilename.Size = new System.Drawing.Size(1096, 24);
            this.labelFilename.TabIndex = 35;
            this.labelFilename.Text = "labelFilename";
            this.labelFilename.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelData
            // 
            this.panelData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelData.Controls.Add(this.buttonShowAllCards);
            this.panelData.Controls.Add(this.buttonDataRandomize);
            this.panelData.Controls.Add(this.labelDataUnusedCardIDs);
            this.panelData.Controls.Add(this.listBoxDataUnusedCardIDs);
            this.panelData.Controls.Add(this.comboBoxDataCardID_00_01);
            this.panelData.Controls.Add(this.comboBoxDataCardID_00_00);
            this.panelData.Controls.Add(this.textBoxDataPictureFilename_00);
            this.panelData.Location = new System.Drawing.Point(8, 493);
            this.panelData.Name = "panelData";
            this.panelData.Size = new System.Drawing.Size(1345, 381);
            this.panelData.TabIndex = 39;
            // 
            // buttonShowAllCards
            // 
            this.buttonShowAllCards.ForeColor = System.Drawing.Color.Black;
            this.buttonShowAllCards.Location = new System.Drawing.Point(1191, 31);
            this.buttonShowAllCards.Name = "buttonShowAllCards";
            this.buttonShowAllCards.Size = new System.Drawing.Size(147, 42);
            this.buttonShowAllCards.TabIndex = 68;
            this.buttonShowAllCards.Text = "show all";
            this.buttonShowAllCards.UseVisualStyleBackColor = true;
            this.buttonShowAllCards.Click += new System.EventHandler(this.buttonShowAllCards_Click);
            // 
            // buttonDataRandomize
            // 
            this.buttonDataRandomize.ForeColor = System.Drawing.Color.Black;
            this.buttonDataRandomize.Location = new System.Drawing.Point(984, 329);
            this.buttonDataRandomize.Name = "buttonDataRandomize";
            this.buttonDataRandomize.Size = new System.Drawing.Size(200, 26);
            this.buttonDataRandomize.TabIndex = 67;
            this.buttonDataRandomize.Text = "randomize";
            this.buttonDataRandomize.UseVisualStyleBackColor = true;
            this.buttonDataRandomize.Click += new System.EventHandler(this.buttonDataRandomize_Click);
            // 
            // labelDataUnusedCardIDs
            // 
            this.labelDataUnusedCardIDs.Location = new System.Drawing.Point(981, 3);
            this.labelDataUnusedCardIDs.Name = "labelDataUnusedCardIDs";
            this.labelDataUnusedCardIDs.Size = new System.Drawing.Size(203, 25);
            this.labelDataUnusedCardIDs.TabIndex = 66;
            this.labelDataUnusedCardIDs.Text = "unused card ids";
            this.labelDataUnusedCardIDs.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // listBoxDataUnusedCardIDs
            // 
            this.listBoxDataUnusedCardIDs.ColumnWidth = 75;
            this.listBoxDataUnusedCardIDs.FormattingEnabled = true;
            this.listBoxDataUnusedCardIDs.ItemHeight = 18;
            this.listBoxDataUnusedCardIDs.Location = new System.Drawing.Point(984, 31);
            this.listBoxDataUnusedCardIDs.MultiColumn = true;
            this.listBoxDataUnusedCardIDs.Name = "listBoxDataUnusedCardIDs";
            this.listBoxDataUnusedCardIDs.Size = new System.Drawing.Size(200, 292);
            this.listBoxDataUnusedCardIDs.TabIndex = 65;
            // 
            // comboBoxDataCardID_00_01
            // 
            this.comboBoxDataCardID_00_01.FormattingEnabled = true;
            this.comboBoxDataCardID_00_01.Location = new System.Drawing.Point(389, 5);
            this.comboBoxDataCardID_00_01.Name = "comboBoxDataCardID_00_01";
            this.comboBoxDataCardID_00_01.Size = new System.Drawing.Size(60, 26);
            this.comboBoxDataCardID_00_01.TabIndex = 34;
            this.comboBoxDataCardID_00_01.SelectedIndexChanged += new System.EventHandler(this.comboBoxDataCardID_SelectedIndexChanged);
            // 
            // comboBoxDataCardID_00_00
            // 
            this.comboBoxDataCardID_00_00.FormattingEnabled = true;
            this.comboBoxDataCardID_00_00.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42"});
            this.comboBoxDataCardID_00_00.Location = new System.Drawing.Point(323, 5);
            this.comboBoxDataCardID_00_00.Name = "comboBoxDataCardID_00_00";
            this.comboBoxDataCardID_00_00.Size = new System.Drawing.Size(60, 26);
            this.comboBoxDataCardID_00_00.TabIndex = 33;
            this.comboBoxDataCardID_00_00.SelectedIndexChanged += new System.EventHandler(this.comboBoxDataCardID_SelectedIndexChanged);
            // 
            // textBoxDataPictureFilename_00
            // 
            this.textBoxDataPictureFilename_00.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDataPictureFilename_00.Location = new System.Drawing.Point(17, 6);
            this.textBoxDataPictureFilename_00.Name = "textBoxDataPictureFilename_00";
            this.textBoxDataPictureFilename_00.ReadOnly = true;
            this.textBoxDataPictureFilename_00.Size = new System.Drawing.Size(300, 24);
            this.textBoxDataPictureFilename_00.TabIndex = 32;
            this.textBoxDataPictureFilename_00.Text = "textBoxDataPictureFilename_00";
            this.textBoxDataPictureFilename_00.Click += new System.EventHandler(this.textBoxDataSongFilename_Click);
            // 
            // groupBoxBorder
            // 
            this.groupBoxBorder.Controls.Add(this.labelBorderScalingUnit);
            this.groupBoxBorder.Controls.Add(this.labelBorderScaling);
            this.groupBoxBorder.Controls.Add(this.numericUpDownBorderScaling);
            this.groupBoxBorder.Controls.Add(this.labelBorderStyle);
            this.groupBoxBorder.Controls.Add(this.labelBorderPositionY);
            this.groupBoxBorder.Controls.Add(this.comboBoxBorderStyle);
            this.groupBoxBorder.Controls.Add(this.labelBorderPositionX);
            this.groupBoxBorder.Controls.Add(this.numericUpDownBorderPositionY);
            this.groupBoxBorder.Controls.Add(this.numericUpDownBorderPositionX);
            this.groupBoxBorder.ForeColor = System.Drawing.Color.White;
            this.groupBoxBorder.Location = new System.Drawing.Point(819, 62);
            this.groupBoxBorder.Name = "groupBoxBorder";
            this.groupBoxBorder.Size = new System.Drawing.Size(534, 81);
            this.groupBoxBorder.TabIndex = 40;
            this.groupBoxBorder.TabStop = false;
            this.groupBoxBorder.Text = "border";
            // 
            // labelBorderScalingUnit
            // 
            this.labelBorderScalingUnit.Location = new System.Drawing.Point(197, 49);
            this.labelBorderScalingUnit.Name = "labelBorderScalingUnit";
            this.labelBorderScalingUnit.Size = new System.Drawing.Size(30, 24);
            this.labelBorderScalingUnit.TabIndex = 27;
            this.labelBorderScalingUnit.Text = "%";
            this.labelBorderScalingUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelBorderScaling
            // 
            this.labelBorderScaling.Location = new System.Drawing.Point(6, 48);
            this.labelBorderScaling.Name = "labelBorderScaling";
            this.labelBorderScaling.Size = new System.Drawing.Size(100, 24);
            this.labelBorderScaling.TabIndex = 26;
            this.labelBorderScaling.Text = "scaling:";
            this.labelBorderScaling.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownBorderScaling
            // 
            this.numericUpDownBorderScaling.Location = new System.Drawing.Point(112, 49);
            this.numericUpDownBorderScaling.Name = "numericUpDownBorderScaling";
            this.numericUpDownBorderScaling.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownBorderScaling.TabIndex = 25;
            this.numericUpDownBorderScaling.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownBorderScaling.ValueChanged += new System.EventHandler(this.numericUpDownBorderScaling_ValueChanged);
            // 
            // labelBorderStyle
            // 
            this.labelBorderStyle.Location = new System.Drawing.Point(318, 17);
            this.labelBorderStyle.Name = "labelBorderStyle";
            this.labelBorderStyle.Size = new System.Drawing.Size(56, 24);
            this.labelBorderStyle.TabIndex = 21;
            this.labelBorderStyle.Text = "style:";
            this.labelBorderStyle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelBorderPositionY
            // 
            this.labelBorderPositionY.Location = new System.Drawing.Point(197, 19);
            this.labelBorderPositionY.Name = "labelBorderPositionY";
            this.labelBorderPositionY.Size = new System.Drawing.Size(30, 24);
            this.labelBorderPositionY.TabIndex = 3;
            this.labelBorderPositionY.Text = "y:";
            this.labelBorderPositionY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxBorderStyle
            // 
            this.comboBoxBorderStyle.FormattingEnabled = true;
            this.comboBoxBorderStyle.Location = new System.Drawing.Point(380, 16);
            this.comboBoxBorderStyle.Name = "comboBoxBorderStyle";
            this.comboBoxBorderStyle.Size = new System.Drawing.Size(148, 26);
            this.comboBoxBorderStyle.TabIndex = 20;
            this.comboBoxBorderStyle.SelectedIndexChanged += new System.EventHandler(this.comboBoxBorderStyle_SelectedIndexChanged);
            // 
            // labelBorderPositionX
            // 
            this.labelBorderPositionX.Location = new System.Drawing.Point(6, 18);
            this.labelBorderPositionX.Name = "labelBorderPositionX";
            this.labelBorderPositionX.Size = new System.Drawing.Size(100, 24);
            this.labelBorderPositionX.TabIndex = 2;
            this.labelBorderPositionX.Text = "position.x:";
            this.labelBorderPositionX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownBorderPositionY
            // 
            this.numericUpDownBorderPositionY.Location = new System.Drawing.Point(233, 19);
            this.numericUpDownBorderPositionY.Name = "numericUpDownBorderPositionY";
            this.numericUpDownBorderPositionY.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownBorderPositionY.TabIndex = 1;
            this.numericUpDownBorderPositionY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownBorderPositionY.ValueChanged += new System.EventHandler(this.numericUpDownBorderPositionY_ValueChanged);
            // 
            // numericUpDownBorderPositionX
            // 
            this.numericUpDownBorderPositionX.Location = new System.Drawing.Point(112, 19);
            this.numericUpDownBorderPositionX.Name = "numericUpDownBorderPositionX";
            this.numericUpDownBorderPositionX.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownBorderPositionX.TabIndex = 0;
            this.numericUpDownBorderPositionX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownBorderPositionX.ValueChanged += new System.EventHandler(this.numericUpDownBorderPositionX_ValueChanged);
            // 
            // checkBoxGameboardDuellMode
            // 
            this.checkBoxGameboardDuellMode.AutoSize = true;
            this.checkBoxGameboardDuellMode.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxGameboardDuellMode.Location = new System.Drawing.Point(9, 48);
            this.checkBoxGameboardDuellMode.Name = "checkBoxGameboardDuellMode";
            this.checkBoxGameboardDuellMode.Size = new System.Drawing.Size(115, 22);
            this.checkBoxGameboardDuellMode.TabIndex = 5;
            this.checkBoxGameboardDuellMode.Text = "duell-mode:";
            this.checkBoxGameboardDuellMode.UseVisualStyleBackColor = true;
            this.checkBoxGameboardDuellMode.CheckedChanged += new System.EventHandler(this.checkBoxGameboardDuellMode_CheckedChanged);
            // 
            // UserControlContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxBorder);
            this.Controls.Add(this.panelData);
            this.Controls.Add(this.buttonSaveAs);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.labelFilename);
            this.Controls.Add(this.groupBoxLocalVentuzServer);
            this.Name = "UserControlContent";
            this.Controls.SetChildIndex(this.groupBoxLocalVentuzServer, 0);
            this.Controls.SetChildIndex(this.groupBoxPreview, 0);
            this.Controls.SetChildIndex(this.labelGameClass, 0);
            this.Controls.SetChildIndex(this.groupBoxScore, 0);
            this.Controls.SetChildIndex(this.labelFilename, 0);
            this.Controls.SetChildIndex(this.buttonLoad, 0);
            this.Controls.SetChildIndex(this.buttonSave, 0);
            this.Controls.SetChildIndex(this.buttonSaveAs, 0);
            this.Controls.SetChildIndex(this.panelData, 0);
            this.Controls.SetChildIndex(this.groupBoxBorder, 0);
            this.groupBoxScore.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionX)).EndInit();
            this.groupBoxPreview.ResumeLayout(false);
            this.groupBoxPreview.PerformLayout();
            this.groupBoxLocalVentuzServer.ResumeLayout(false);
            this.groupBoxLocalVentuzServer.PerformLayout();
            this.panelData.ResumeLayout(false);
            this.panelData.PerformLayout();
            this.groupBoxBorder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBorderScaling)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBorderPositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBorderPositionX)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonSourceFullscreen;
        private System.Windows.Forms.RadioButton radioButtonSourceInsert;
        private System.Windows.Forms.GroupBox groupBoxLocalVentuzServer;
        private System.Windows.Forms.Label labelLocalVentuzServerGameboard;
        private System.Windows.Forms.TextBox textBoxLocalVentuzServerGameboard;
        private System.Windows.Forms.Button buttonSaveAs;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Label labelFilename;
        private System.Windows.Forms.Panel panelData;
        private System.Windows.Forms.ComboBox comboBoxDataCardID_00_01;
        private System.Windows.Forms.ComboBox comboBoxDataCardID_00_00;
        private System.Windows.Forms.TextBox textBoxDataPictureFilename_00;
        private System.Windows.Forms.Label labelDataUnusedCardIDs;
        private System.Windows.Forms.ListBox listBoxDataUnusedCardIDs;
        private System.Windows.Forms.Button buttonDataRandomize;
        private System.Windows.Forms.GroupBox groupBoxBorder;
        protected System.Windows.Forms.Label labelBorderStyle;
        private System.Windows.Forms.Label labelBorderPositionY;
        protected System.Windows.Forms.ComboBox comboBoxBorderStyle;
        private System.Windows.Forms.Label labelBorderPositionX;
        private System.Windows.Forms.NumericUpDown numericUpDownBorderPositionY;
        private System.Windows.Forms.NumericUpDown numericUpDownBorderPositionX;
        private System.Windows.Forms.Label labelBorderScalingUnit;
        private System.Windows.Forms.Label labelBorderScaling;
        private System.Windows.Forms.NumericUpDown numericUpDownBorderScaling;
        private System.Windows.Forms.Button buttonShowAllCards;
        private System.Windows.Forms.CheckBox checkBoxGameboardDuellMode;
    }
}
