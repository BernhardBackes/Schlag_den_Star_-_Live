namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.PictureInsertAndFullscreenScore {
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
            this.buttonSaveAs = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.labelFilename = new System.Windows.Forms.Label();
            this.checkBoxDataSampleIncluded = new System.Windows.Forms.CheckBox();
            this.panelData = new System.Windows.Forms.Panel();
            this.buttonDataResort = new System.Windows.Forms.Button();
            this.groupBoxDataset = new System.Windows.Forms.GroupBox();
            this.pictureBoxDatasetMovie = new System.Windows.Forms.PictureBox();
            this.textBoxDataSetName = new System.Windows.Forms.TextBox();
            this.buttonDataRemoveAllSets = new System.Windows.Forms.Button();
            this.buttonDataRemoveSet = new System.Windows.Forms.Button();
            this.buttonDataAddNewSet = new System.Windows.Forms.Button();
            this.buttonDataMoveSetDown = new System.Windows.Forms.Button();
            this.buttonDataMoveSetUp = new System.Windows.Forms.Button();
            this.listBoxDataList = new System.Windows.Forms.ListBox();
            this.groupBoxFullscreen = new System.Windows.Forms.GroupBox();
            this.labelFullscreenScaling = new System.Windows.Forms.Label();
            this.numericUpDownFullscreenScaling = new System.Windows.Forms.NumericUpDown();
            this.labelFullscreenPositionY = new System.Windows.Forms.Label();
            this.labelFullscreenPositionX = new System.Windows.Forms.Label();
            this.numericUpDownFullscreenPositionY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownFullscreenPositionX = new System.Windows.Forms.NumericUpDown();
            this.radioButtonSourceFullscreen = new System.Windows.Forms.RadioButton();
            this.radioButtonSourceInsert = new System.Windows.Forms.RadioButton();
            this.groupBoxInsert = new System.Windows.Forms.GroupBox();
            this.labelInsertScaling = new System.Windows.Forms.Label();
            this.numericUpDownInsertScaling = new System.Windows.Forms.NumericUpDown();
            this.labelInsertPositionY = new System.Windows.Forms.Label();
            this.labelInsertPositionX = new System.Windows.Forms.Label();
            this.numericUpDownInsertPositionY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownInsertPositionX = new System.Windows.Forms.NumericUpDown();
            this.groupBoxScore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionX)).BeginInit();
            this.groupBoxPreview.SuspendLayout();
            this.panelData.SuspendLayout();
            this.groupBoxDataset.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDatasetMovie)).BeginInit();
            this.groupBoxFullscreen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFullscreenScaling)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFullscreenPositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFullscreenPositionX)).BeginInit();
            this.groupBoxInsert.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInsertScaling)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInsertPositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInsertPositionX)).BeginInit();
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
            this.groupBoxDataset.Controls.Add(this.pictureBoxDatasetMovie);
            this.groupBoxDataset.Controls.Add(this.textBoxDataSetName);
            this.groupBoxDataset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxDataset.ForeColor = System.Drawing.Color.White;
            this.groupBoxDataset.Location = new System.Drawing.Point(3, 3);
            this.groupBoxDataset.Name = "groupBoxDataset";
            this.groupBoxDataset.Size = new System.Drawing.Size(913, 373);
            this.groupBoxDataset.TabIndex = 24;
            this.groupBoxDataset.TabStop = false;
            // 
            // pictureBoxDatasetMovie
            // 
            this.pictureBoxDatasetMovie.BackColor = System.Drawing.Color.LightGray;
            this.pictureBoxDatasetMovie.BackgroundImage = global::Cliparts.SchlagDenStarLive.MainApp.Properties.Resources.preview_desk;
            this.pictureBoxDatasetMovie.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxDatasetMovie.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxDatasetMovie.Location = new System.Drawing.Point(12, 18);
            this.pictureBoxDatasetMovie.Name = "pictureBoxDatasetMovie";
            this.pictureBoxDatasetMovie.Size = new System.Drawing.Size(613, 345);
            this.pictureBoxDatasetMovie.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxDatasetMovie.TabIndex = 22;
            this.pictureBoxDatasetMovie.TabStop = false;
            this.pictureBoxDatasetMovie.Click += new System.EventHandler(this.pictureBoxDatasetMovie_Click);
            // 
            // textBoxDataSetName
            // 
            this.textBoxDataSetName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDataSetName.Location = new System.Drawing.Point(631, 20);
            this.textBoxDataSetName.Name = "textBoxDataSetName";
            this.textBoxDataSetName.Size = new System.Drawing.Size(276, 24);
            this.textBoxDataSetName.TabIndex = 18;
            this.textBoxDataSetName.Text = "textBoxDataSetName";
            this.textBoxDataSetName.TextChanged += new System.EventHandler(this.textBoxDataSetName_TextChanged);
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
            // groupBoxFullscreen
            // 
            this.groupBoxFullscreen.Controls.Add(this.labelFullscreenScaling);
            this.groupBoxFullscreen.Controls.Add(this.numericUpDownFullscreenScaling);
            this.groupBoxFullscreen.Controls.Add(this.labelFullscreenPositionY);
            this.groupBoxFullscreen.Controls.Add(this.labelFullscreenPositionX);
            this.groupBoxFullscreen.Controls.Add(this.numericUpDownFullscreenPositionY);
            this.groupBoxFullscreen.Controls.Add(this.numericUpDownFullscreenPositionX);
            this.groupBoxFullscreen.ForeColor = System.Drawing.Color.White;
            this.groupBoxFullscreen.Location = new System.Drawing.Point(819, 118);
            this.groupBoxFullscreen.Name = "groupBoxFullscreen";
            this.groupBoxFullscreen.Size = new System.Drawing.Size(534, 50);
            this.groupBoxFullscreen.TabIndex = 38;
            this.groupBoxFullscreen.TabStop = false;
            this.groupBoxFullscreen.Text = "fullscreen";
            // 
            // labelFullscreenScaling
            // 
            this.labelFullscreenScaling.Location = new System.Drawing.Point(318, 18);
            this.labelFullscreenScaling.Name = "labelFullscreenScaling";
            this.labelFullscreenScaling.Size = new System.Drawing.Size(83, 24);
            this.labelFullscreenScaling.TabIndex = 5;
            this.labelFullscreenScaling.Text = "scaling:";
            this.labelFullscreenScaling.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownFullscreenScaling
            // 
            this.numericUpDownFullscreenScaling.DecimalPlaces = 1;
            this.numericUpDownFullscreenScaling.Location = new System.Drawing.Point(407, 18);
            this.numericUpDownFullscreenScaling.Name = "numericUpDownFullscreenScaling";
            this.numericUpDownFullscreenScaling.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownFullscreenScaling.TabIndex = 4;
            this.numericUpDownFullscreenScaling.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownFullscreenScaling.ValueChanged += new System.EventHandler(this.numericUpDownFullscreenScaling_ValueChanged);
            // 
            // labelFullscreenPositionY
            // 
            this.labelFullscreenPositionY.Location = new System.Drawing.Point(197, 18);
            this.labelFullscreenPositionY.Name = "labelFullscreenPositionY";
            this.labelFullscreenPositionY.Size = new System.Drawing.Size(30, 24);
            this.labelFullscreenPositionY.TabIndex = 3;
            this.labelFullscreenPositionY.Text = "y:";
            this.labelFullscreenPositionY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelFullscreenPositionX
            // 
            this.labelFullscreenPositionX.Location = new System.Drawing.Point(6, 18);
            this.labelFullscreenPositionX.Name = "labelFullscreenPositionX";
            this.labelFullscreenPositionX.Size = new System.Drawing.Size(100, 24);
            this.labelFullscreenPositionX.TabIndex = 2;
            this.labelFullscreenPositionX.Text = "position.x:";
            this.labelFullscreenPositionX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownFullscreenPositionY
            // 
            this.numericUpDownFullscreenPositionY.Location = new System.Drawing.Point(233, 18);
            this.numericUpDownFullscreenPositionY.Name = "numericUpDownFullscreenPositionY";
            this.numericUpDownFullscreenPositionY.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownFullscreenPositionY.TabIndex = 1;
            this.numericUpDownFullscreenPositionY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownFullscreenPositionY.ValueChanged += new System.EventHandler(this.numericUpDownFullscreenPositionY_ValueChanged);
            // 
            // numericUpDownFullscreenPositionX
            // 
            this.numericUpDownFullscreenPositionX.Location = new System.Drawing.Point(112, 18);
            this.numericUpDownFullscreenPositionX.Name = "numericUpDownFullscreenPositionX";
            this.numericUpDownFullscreenPositionX.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownFullscreenPositionX.TabIndex = 0;
            this.numericUpDownFullscreenPositionX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownFullscreenPositionX.ValueChanged += new System.EventHandler(this.numericUpDownFullscreenPositionX_ValueChanged);
            // 
            // radioButtonSourceFullscreen
            // 
            this.radioButtonSourceFullscreen.AutoSize = true;
            this.radioButtonSourceFullscreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSourceFullscreen.Location = new System.Drawing.Point(186, 14);
            this.radioButtonSourceFullscreen.Name = "radioButtonSourceFullscreen";
            this.radioButtonSourceFullscreen.Size = new System.Drawing.Size(99, 22);
            this.radioButtonSourceFullscreen.TabIndex = 15;
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
            this.radioButtonSourceInsert.TabIndex = 14;
            this.radioButtonSourceInsert.TabStop = true;
            this.radioButtonSourceInsert.Text = "insert";
            this.radioButtonSourceInsert.UseVisualStyleBackColor = true;
            this.radioButtonSourceInsert.CheckedChanged += new System.EventHandler(this.radioButtonSourceInsert_CheckedChanged);
            // 
            // groupBoxInsert
            // 
            this.groupBoxInsert.Controls.Add(this.labelInsertScaling);
            this.groupBoxInsert.Controls.Add(this.numericUpDownInsertScaling);
            this.groupBoxInsert.Controls.Add(this.labelInsertPositionY);
            this.groupBoxInsert.Controls.Add(this.labelInsertPositionX);
            this.groupBoxInsert.Controls.Add(this.numericUpDownInsertPositionY);
            this.groupBoxInsert.Controls.Add(this.numericUpDownInsertPositionX);
            this.groupBoxInsert.ForeColor = System.Drawing.Color.White;
            this.groupBoxInsert.Location = new System.Drawing.Point(819, 62);
            this.groupBoxInsert.Name = "groupBoxInsert";
            this.groupBoxInsert.Size = new System.Drawing.Size(534, 50);
            this.groupBoxInsert.TabIndex = 39;
            this.groupBoxInsert.TabStop = false;
            this.groupBoxInsert.Text = "insert";
            // 
            // labelInsertScaling
            // 
            this.labelInsertScaling.Location = new System.Drawing.Point(318, 18);
            this.labelInsertScaling.Name = "labelInsertScaling";
            this.labelInsertScaling.Size = new System.Drawing.Size(83, 24);
            this.labelInsertScaling.TabIndex = 5;
            this.labelInsertScaling.Text = "scaling:";
            this.labelInsertScaling.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownInsertScaling
            // 
            this.numericUpDownInsertScaling.DecimalPlaces = 1;
            this.numericUpDownInsertScaling.Location = new System.Drawing.Point(407, 18);
            this.numericUpDownInsertScaling.Name = "numericUpDownInsertScaling";
            this.numericUpDownInsertScaling.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownInsertScaling.TabIndex = 4;
            this.numericUpDownInsertScaling.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownInsertScaling.ValueChanged += new System.EventHandler(this.numericUpDownInsertScaling_ValueChanged);
            // 
            // labelInsertPositionY
            // 
            this.labelInsertPositionY.Location = new System.Drawing.Point(197, 18);
            this.labelInsertPositionY.Name = "labelInsertPositionY";
            this.labelInsertPositionY.Size = new System.Drawing.Size(30, 24);
            this.labelInsertPositionY.TabIndex = 3;
            this.labelInsertPositionY.Text = "y:";
            this.labelInsertPositionY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelInsertPositionX
            // 
            this.labelInsertPositionX.Location = new System.Drawing.Point(6, 18);
            this.labelInsertPositionX.Name = "labelInsertPositionX";
            this.labelInsertPositionX.Size = new System.Drawing.Size(100, 24);
            this.labelInsertPositionX.TabIndex = 2;
            this.labelInsertPositionX.Text = "position.x:";
            this.labelInsertPositionX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownInsertPositionY
            // 
            this.numericUpDownInsertPositionY.Location = new System.Drawing.Point(233, 18);
            this.numericUpDownInsertPositionY.Name = "numericUpDownInsertPositionY";
            this.numericUpDownInsertPositionY.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownInsertPositionY.TabIndex = 1;
            this.numericUpDownInsertPositionY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownInsertPositionY.ValueChanged += new System.EventHandler(this.numericUpDownInsertPositionY_ValueChanged);
            // 
            // numericUpDownInsertPositionX
            // 
            this.numericUpDownInsertPositionX.Location = new System.Drawing.Point(112, 18);
            this.numericUpDownInsertPositionX.Name = "numericUpDownInsertPositionX";
            this.numericUpDownInsertPositionX.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownInsertPositionX.TabIndex = 0;
            this.numericUpDownInsertPositionX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownInsertPositionX.ValueChanged += new System.EventHandler(this.numericUpDownInsertPositionX_ValueChanged);
            // 
            // UserControlContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxInsert);
            this.Controls.Add(this.groupBoxFullscreen);
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
            this.Controls.SetChildIndex(this.groupBoxFullscreen, 0);
            this.Controls.SetChildIndex(this.groupBoxInsert, 0);
            this.groupBoxScore.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionX)).EndInit();
            this.groupBoxPreview.ResumeLayout(false);
            this.groupBoxPreview.PerformLayout();
            this.panelData.ResumeLayout(false);
            this.panelData.PerformLayout();
            this.groupBoxDataset.ResumeLayout(false);
            this.groupBoxDataset.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDatasetMovie)).EndInit();
            this.groupBoxFullscreen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFullscreenScaling)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFullscreenPositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFullscreenPositionX)).EndInit();
            this.groupBoxInsert.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInsertScaling)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInsertPositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInsertPositionX)).EndInit();
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
        private System.Windows.Forms.PictureBox pictureBoxDatasetMovie;
        private System.Windows.Forms.Button buttonDataRemoveAllSets;
        private System.Windows.Forms.Button buttonDataRemoveSet;
        private System.Windows.Forms.Button buttonDataAddNewSet;
        private System.Windows.Forms.TextBox textBoxDataSetName;
        private System.Windows.Forms.Button buttonDataMoveSetDown;
        private System.Windows.Forms.Button buttonDataMoveSetUp;
        private System.Windows.Forms.ListBox listBoxDataList;
        private System.Windows.Forms.Button buttonDataResort;
        protected System.Windows.Forms.GroupBox groupBoxFullscreen;
        protected System.Windows.Forms.Label labelFullscreenPositionY;
        protected System.Windows.Forms.Label labelFullscreenPositionX;
        protected System.Windows.Forms.NumericUpDown numericUpDownFullscreenPositionY;
        protected System.Windows.Forms.NumericUpDown numericUpDownFullscreenPositionX;
        private System.Windows.Forms.RadioButton radioButtonSourceFullscreen;
        private System.Windows.Forms.RadioButton radioButtonSourceInsert;
        protected System.Windows.Forms.Label labelFullscreenScaling;
        protected System.Windows.Forms.NumericUpDown numericUpDownFullscreenScaling;
        protected System.Windows.Forms.GroupBox groupBoxInsert;
        protected System.Windows.Forms.Label labelInsertScaling;
        protected System.Windows.Forms.NumericUpDown numericUpDownInsertScaling;
        protected System.Windows.Forms.Label labelInsertPositionY;
        protected System.Windows.Forms.Label labelInsertPositionX;
        protected System.Windows.Forms.NumericUpDown numericUpDownInsertPositionY;
        protected System.Windows.Forms.NumericUpDown numericUpDownInsertPositionX;
    }
}
