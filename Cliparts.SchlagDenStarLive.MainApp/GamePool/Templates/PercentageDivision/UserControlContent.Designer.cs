namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.PercentageDivision {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlContent));
            this.buttonSaveAs = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.labelFilename = new System.Windows.Forms.Label();
            this.checkBoxDataSampleIncluded = new System.Windows.Forms.CheckBox();
            this.panelData = new System.Windows.Forms.Panel();
            this.buttonDataResort = new System.Windows.Forms.Button();
            this.groupBoxDataset = new System.Windows.Forms.GroupBox();
            this.labelDatasetHostText = new System.Windows.Forms.Label();
            this.textBoxDatasetHostText = new System.Windows.Forms.TextBox();
            this.textBoxDataSetName = new System.Windows.Forms.TextBox();
            this.pictureBoxDatasetPicture = new System.Windows.Forms.PictureBox();
            this.buttonDataRemoveAllSets = new System.Windows.Forms.Button();
            this.buttonDataRemoveSet = new System.Windows.Forms.Button();
            this.buttonDataAddNewSet = new System.Windows.Forms.Button();
            this.buttonDataMoveSetDown = new System.Windows.Forms.Button();
            this.buttonDataMoveSetUp = new System.Windows.Forms.Button();
            this.listBoxDataList = new System.Windows.Forms.ListBox();
            this.groupBoxBorder = new System.Windows.Forms.GroupBox();
            this.labelBorderStyle = new System.Windows.Forms.Label();
            this.comboBoxBorderStyle = new System.Windows.Forms.ComboBox();
            this.labelBorderPositionY = new System.Windows.Forms.Label();
            this.labelBorderPositionX = new System.Windows.Forms.Label();
            this.numericUpDownBorderPositionY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownBorderPositionX = new System.Windows.Forms.NumericUpDown();
            this.groupBoxScore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionX)).BeginInit();
            this.groupBoxPreview.SuspendLayout();
            this.panelData.SuspendLayout();
            this.groupBoxDataset.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDatasetPicture)).BeginInit();
            this.groupBoxBorder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBorderPositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBorderPositionX)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxScoreStyle
            // 
            this.comboBoxScoreStyle.Size = new System.Drawing.Size(148, 26);
            // 
            // buttonSaveAs
            // 
            this.buttonSaveAs.ForeColor = System.Drawing.Color.Black;
            this.buttonSaveAs.Location = new System.Drawing.Point(1272, 461);
            this.buttonSaveAs.Name = "buttonSaveAs";
            this.buttonSaveAs.Size = new System.Drawing.Size(75, 26);
            this.buttonSaveAs.TabIndex = 19;
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
            this.buttonSave.TabIndex = 18;
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
            this.buttonLoad.TabIndex = 17;
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
            this.labelFilename.TabIndex = 16;
            this.labelFilename.Text = "labelFilename";
            this.labelFilename.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBoxDataSampleIncluded
            // 
            this.checkBoxDataSampleIncluded.AutoSize = true;
            this.checkBoxDataSampleIncluded.Location = new System.Drawing.Point(925, 7);
            this.checkBoxDataSampleIncluded.Name = "checkBoxDataSampleIncluded";
            this.checkBoxDataSampleIncluded.Size = new System.Drawing.Size(193, 22);
            this.checkBoxDataSampleIncluded.TabIndex = 29;
            this.checkBoxDataSampleIncluded.Text = "first dataset is sample";
            this.checkBoxDataSampleIncluded.UseVisualStyleBackColor = true;
            this.checkBoxDataSampleIncluded.CheckedChanged += new System.EventHandler(this.checkBoxSampleIncluded_CheckedChanged);
            // 
            // panelData
            // 
            this.panelData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelData.Controls.Add(this.buttonDataResort);
            this.panelData.Controls.Add(this.groupBoxDataset);
            this.panelData.Controls.Add(this.checkBoxDataSampleIncluded);
            this.panelData.Controls.Add(this.buttonDataRemoveAllSets);
            this.panelData.Controls.Add(this.buttonDataRemoveSet);
            this.panelData.Controls.Add(this.buttonDataAddNewSet);
            this.panelData.Controls.Add(this.buttonDataMoveSetDown);
            this.panelData.Controls.Add(this.buttonDataMoveSetUp);
            this.panelData.Controls.Add(this.listBoxDataList);
            this.panelData.Location = new System.Drawing.Point(8, 493);
            this.panelData.Name = "panelData";
            this.panelData.Size = new System.Drawing.Size(1345, 381);
            this.panelData.TabIndex = 30;
            // 
            // buttonDataResort
            // 
            this.buttonDataResort.ForeColor = System.Drawing.Color.Black;
            this.buttonDataResort.Location = new System.Drawing.Point(1168, 345);
            this.buttonDataResort.Name = "buttonDataResort";
            this.buttonDataResort.Size = new System.Drawing.Size(118, 26);
            this.buttonDataResort.TabIndex = 31;
            this.buttonDataResort.Text = "resort";
            this.buttonDataResort.UseVisualStyleBackColor = true;
            this.buttonDataResort.Click += new System.EventHandler(this.buttonDataResort_Click);
            // 
            // groupBoxDataset
            // 
            this.groupBoxDataset.Controls.Add(this.labelDatasetHostText);
            this.groupBoxDataset.Controls.Add(this.textBoxDatasetHostText);
            this.groupBoxDataset.Controls.Add(this.textBoxDataSetName);
            this.groupBoxDataset.Controls.Add(this.pictureBoxDatasetPicture);
            this.groupBoxDataset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxDataset.ForeColor = System.Drawing.Color.White;
            this.groupBoxDataset.Location = new System.Drawing.Point(3, 3);
            this.groupBoxDataset.Name = "groupBoxDataset";
            this.groupBoxDataset.Size = new System.Drawing.Size(913, 373);
            this.groupBoxDataset.TabIndex = 24;
            this.groupBoxDataset.TabStop = false;
            // 
            // labelDatasetHostText
            // 
            this.labelDatasetHostText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDatasetHostText.Location = new System.Drawing.Point(179, 270);
            this.labelDatasetHostText.Name = "labelDatasetHostText";
            this.labelDatasetHostText.Size = new System.Drawing.Size(276, 24);
            this.labelDatasetHostText.TabIndex = 31;
            this.labelDatasetHostText.Text = "host text";
            this.labelDatasetHostText.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // textBoxDatasetHostText
            // 
            this.textBoxDatasetHostText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDatasetHostText.Location = new System.Drawing.Point(21, 297);
            this.textBoxDatasetHostText.Multiline = true;
            this.textBoxDatasetHostText.Name = "textBoxDatasetHostText";
            this.textBoxDatasetHostText.Size = new System.Drawing.Size(593, 44);
            this.textBoxDatasetHostText.TabIndex = 30;
            this.textBoxDatasetHostText.Text = "textBoxDatasetHostText";
            this.textBoxDatasetHostText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxDatasetHostText.WordWrap = false;
            this.textBoxDatasetHostText.TextChanged += new System.EventHandler(this.textBoxDatasetHostText_TextChanged);
            // 
            // textBoxDataSetName
            // 
            this.textBoxDataSetName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDataSetName.Location = new System.Drawing.Point(631, 18);
            this.textBoxDataSetName.Name = "textBoxDataSetName";
            this.textBoxDataSetName.Size = new System.Drawing.Size(276, 24);
            this.textBoxDataSetName.TabIndex = 23;
            this.textBoxDataSetName.Text = "textBoxDataSetName";
            this.textBoxDataSetName.TextChanged += new System.EventHandler(this.textBoxDataSetName_TextChanged);
            // 
            // pictureBoxDatasetPicture
            // 
            this.pictureBoxDatasetPicture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxDatasetPicture.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxDatasetPicture.Image")));
            this.pictureBoxDatasetPicture.Location = new System.Drawing.Point(12, 18);
            this.pictureBoxDatasetPicture.Name = "pictureBoxDatasetPicture";
            this.pictureBoxDatasetPicture.Size = new System.Drawing.Size(613, 345);
            this.pictureBoxDatasetPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxDatasetPicture.TabIndex = 22;
            this.pictureBoxDatasetPicture.TabStop = false;
            this.pictureBoxDatasetPicture.Click += new System.EventHandler(this.pictureBoxDatasetTask_Click);
            // 
            // buttonDataRemoveAllSets
            // 
            this.buttonDataRemoveAllSets.ForeColor = System.Drawing.Color.Black;
            this.buttonDataRemoveAllSets.Location = new System.Drawing.Point(1168, 95);
            this.buttonDataRemoveAllSets.Name = "buttonDataRemoveAllSets";
            this.buttonDataRemoveAllSets.Size = new System.Drawing.Size(170, 26);
            this.buttonDataRemoveAllSets.TabIndex = 21;
            this.buttonDataRemoveAllSets.Text = "remove all datasets";
            this.buttonDataRemoveAllSets.UseVisualStyleBackColor = true;
            this.buttonDataRemoveAllSets.Click += new System.EventHandler(this.buttonDataRemoveAllSets_Click);
            // 
            // buttonDataRemoveSet
            // 
            this.buttonDataRemoveSet.ForeColor = System.Drawing.Color.Black;
            this.buttonDataRemoveSet.Location = new System.Drawing.Point(1168, 63);
            this.buttonDataRemoveSet.Name = "buttonDataRemoveSet";
            this.buttonDataRemoveSet.Size = new System.Drawing.Size(170, 26);
            this.buttonDataRemoveSet.TabIndex = 20;
            this.buttonDataRemoveSet.Text = "remove dataset";
            this.buttonDataRemoveSet.UseVisualStyleBackColor = true;
            this.buttonDataRemoveSet.Click += new System.EventHandler(this.buttonDataRemoveSet_Click);
            // 
            // buttonDataAddNewSet
            // 
            this.buttonDataAddNewSet.ForeColor = System.Drawing.Color.Black;
            this.buttonDataAddNewSet.Location = new System.Drawing.Point(1168, 31);
            this.buttonDataAddNewSet.Name = "buttonDataAddNewSet";
            this.buttonDataAddNewSet.Size = new System.Drawing.Size(170, 26);
            this.buttonDataAddNewSet.TabIndex = 19;
            this.buttonDataAddNewSet.Text = "add new dataset";
            this.buttonDataAddNewSet.UseVisualStyleBackColor = true;
            this.buttonDataAddNewSet.Click += new System.EventHandler(this.buttonDataAddNewSet_Click);
            // 
            // buttonDataMoveSetDown
            // 
            this.buttonDataMoveSetDown.ForeColor = System.Drawing.Color.Black;
            this.buttonDataMoveSetDown.Location = new System.Drawing.Point(1044, 345);
            this.buttonDataMoveSetDown.Name = "buttonDataMoveSetDown";
            this.buttonDataMoveSetDown.Size = new System.Drawing.Size(118, 26);
            this.buttonDataMoveSetDown.TabIndex = 17;
            this.buttonDataMoveSetDown.Text = "move down";
            this.buttonDataMoveSetDown.UseVisualStyleBackColor = true;
            this.buttonDataMoveSetDown.Click += new System.EventHandler(this.buttonDataMoveSetDown_Click);
            // 
            // buttonDataMoveSetUp
            // 
            this.buttonDataMoveSetUp.ForeColor = System.Drawing.Color.Black;
            this.buttonDataMoveSetUp.Location = new System.Drawing.Point(925, 345);
            this.buttonDataMoveSetUp.Name = "buttonDataMoveSetUp";
            this.buttonDataMoveSetUp.Size = new System.Drawing.Size(118, 26);
            this.buttonDataMoveSetUp.TabIndex = 16;
            this.buttonDataMoveSetUp.Text = "move up";
            this.buttonDataMoveSetUp.UseVisualStyleBackColor = true;
            this.buttonDataMoveSetUp.Click += new System.EventHandler(this.buttonDataMoveSetUp_Click);
            // 
            // listBoxDataList
            // 
            this.listBoxDataList.FormattingEnabled = true;
            this.listBoxDataList.ItemHeight = 18;
            this.listBoxDataList.Location = new System.Drawing.Point(925, 31);
            this.listBoxDataList.Name = "listBoxDataList";
            this.listBoxDataList.Size = new System.Drawing.Size(237, 310);
            this.listBoxDataList.TabIndex = 0;
            this.listBoxDataList.SelectedIndexChanged += new System.EventHandler(this.listBoxDataList_SelectedIndexChanged);
            // 
            // groupBoxBorder
            // 
            this.groupBoxBorder.Controls.Add(this.labelBorderStyle);
            this.groupBoxBorder.Controls.Add(this.comboBoxBorderStyle);
            this.groupBoxBorder.Controls.Add(this.labelBorderPositionY);
            this.groupBoxBorder.Controls.Add(this.labelBorderPositionX);
            this.groupBoxBorder.Controls.Add(this.numericUpDownBorderPositionY);
            this.groupBoxBorder.Controls.Add(this.numericUpDownBorderPositionX);
            this.groupBoxBorder.ForeColor = System.Drawing.Color.White;
            this.groupBoxBorder.Location = new System.Drawing.Point(819, 62);
            this.groupBoxBorder.Name = "groupBoxBorder";
            this.groupBoxBorder.Size = new System.Drawing.Size(534, 50);
            this.groupBoxBorder.TabIndex = 31;
            this.groupBoxBorder.TabStop = false;
            this.groupBoxBorder.Text = "border";
            // 
            // labelBorderStyle
            // 
            this.labelBorderStyle.Location = new System.Drawing.Point(318, 18);
            this.labelBorderStyle.Name = "labelBorderStyle";
            this.labelBorderStyle.Size = new System.Drawing.Size(56, 24);
            this.labelBorderStyle.TabIndex = 5;
            this.labelBorderStyle.Text = "style:";
            this.labelBorderStyle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxBorderStyle
            // 
            this.comboBoxBorderStyle.FormattingEnabled = true;
            this.comboBoxBorderStyle.Location = new System.Drawing.Point(380, 17);
            this.comboBoxBorderStyle.Name = "comboBoxBorderStyle";
            this.comboBoxBorderStyle.Size = new System.Drawing.Size(148, 26);
            this.comboBoxBorderStyle.TabIndex = 4;
            this.comboBoxBorderStyle.SelectedIndexChanged += new System.EventHandler(this.comboBoxBorderStyle_SelectedIndexChanged);
            // 
            // labelBorderPositionY
            // 
            this.labelBorderPositionY.Location = new System.Drawing.Point(197, 18);
            this.labelBorderPositionY.Name = "labelBorderPositionY";
            this.labelBorderPositionY.Size = new System.Drawing.Size(30, 24);
            this.labelBorderPositionY.TabIndex = 3;
            this.labelBorderPositionY.Text = "y:";
            this.labelBorderPositionY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.numericUpDownBorderPositionY.Location = new System.Drawing.Point(233, 18);
            this.numericUpDownBorderPositionY.Name = "numericUpDownBorderPositionY";
            this.numericUpDownBorderPositionY.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownBorderPositionY.TabIndex = 1;
            this.numericUpDownBorderPositionY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownBorderPositionY.ValueChanged += new System.EventHandler(this.numericUpDownBorderPositionY_ValueChanged);
            // 
            // numericUpDownBorderPositionX
            // 
            this.numericUpDownBorderPositionX.Location = new System.Drawing.Point(112, 18);
            this.numericUpDownBorderPositionX.Name = "numericUpDownBorderPositionX";
            this.numericUpDownBorderPositionX.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownBorderPositionX.TabIndex = 0;
            this.numericUpDownBorderPositionX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownBorderPositionX.ValueChanged += new System.EventHandler(this.numericUpDownBorderPositionX_ValueChanged);
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
            this.Name = "UserControlContent";
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
            this.panelData.ResumeLayout(false);
            this.panelData.PerformLayout();
            this.groupBoxDataset.ResumeLayout(false);
            this.groupBoxDataset.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDatasetPicture)).EndInit();
            this.groupBoxBorder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBorderPositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBorderPositionX)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSaveAs;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Label labelFilename;
        private System.Windows.Forms.CheckBox checkBoxDataSampleIncluded;
        private System.Windows.Forms.Panel panelData;
        private System.Windows.Forms.GroupBox groupBoxDataset;
        private System.Windows.Forms.PictureBox pictureBoxDatasetPicture;
        private System.Windows.Forms.Button buttonDataRemoveAllSets;
        private System.Windows.Forms.Button buttonDataRemoveSet;
        private System.Windows.Forms.Button buttonDataAddNewSet;
        private System.Windows.Forms.Button buttonDataMoveSetDown;
        private System.Windows.Forms.Button buttonDataMoveSetUp;
        private System.Windows.Forms.ListBox listBoxDataList;
        private System.Windows.Forms.Button buttonDataResort;
        private System.Windows.Forms.TextBox textBoxDataSetName;
        private System.Windows.Forms.Label labelDatasetHostText;
        private System.Windows.Forms.TextBox textBoxDatasetHostText;
        protected System.Windows.Forms.GroupBox groupBoxBorder;
        protected System.Windows.Forms.Label labelBorderStyle;
        protected System.Windows.Forms.ComboBox comboBoxBorderStyle;
        protected System.Windows.Forms.Label labelBorderPositionY;
        protected System.Windows.Forms.Label labelBorderPositionX;
        protected System.Windows.Forms.NumericUpDown numericUpDownBorderPositionY;
        protected System.Windows.Forms.NumericUpDown numericUpDownBorderPositionX;
    }
}
