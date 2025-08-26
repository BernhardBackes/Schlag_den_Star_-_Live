namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.InsertAndFullscreenStillBuzzerScore {

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
            this.pictureBoxDatasetSolution = new System.Windows.Forms.PictureBox();
            this.labelDatasetHostText = new System.Windows.Forms.Label();
            this.textBoxDatasetHostText = new System.Windows.Forms.TextBox();
            this.pictureBoxDatasetTask = new System.Windows.Forms.PictureBox();
            this.textBoxDataSetName = new System.Windows.Forms.TextBox();
            this.buttonDataRemoveAllSets = new System.Windows.Forms.Button();
            this.buttonDataRemoveSet = new System.Windows.Forms.Button();
            this.buttonDataAddNewSet = new System.Windows.Forms.Button();
            this.buttonDataMoveSetDown = new System.Windows.Forms.Button();
            this.buttonDataMoveSetUp = new System.Windows.Forms.Button();
            this.listBoxDataList = new System.Windows.Forms.ListBox();
            this.groupBoxContent = new System.Windows.Forms.GroupBox();
            this.labelContentPositionY = new System.Windows.Forms.Label();
            this.labelContentPositionX = new System.Windows.Forms.Label();
            this.numericUpDownContentPositionY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownContentPositionX = new System.Windows.Forms.NumericUpDown();
            this.radioButtonSourceHost = new System.Windows.Forms.RadioButton();
            this.radioButtonSourceInsert = new System.Windows.Forms.RadioButton();
            this.groupBoxTaskCounter = new System.Windows.Forms.GroupBox();
            this.labelTaskCounterSize = new System.Windows.Forms.Label();
            this.numericUpDownTaskCounterSize = new System.Windows.Forms.NumericUpDown();
            this.labelTaskCounterPositionY = new System.Windows.Forms.Label();
            this.labelTaskCounterPositionX = new System.Windows.Forms.Label();
            this.numericUpDownTaskCounterPositionY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownTaskCounterPositionX = new System.Windows.Forms.NumericUpDown();
            this.radioButtonSourceFullscreen = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionX)).BeginInit();
            this.groupBoxPreview.SuspendLayout();
            this.panelData.SuspendLayout();
            this.groupBoxDataset.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDatasetSolution)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDatasetTask)).BeginInit();
            this.groupBoxContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownContentPositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownContentPositionX)).BeginInit();
            this.groupBoxTaskCounter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTaskCounterSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTaskCounterPositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTaskCounterPositionX)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxScoreStyle
            // 
            this.comboBoxScoreStyle.Size = new System.Drawing.Size(148, 26);
            // 
            // groupBoxPreview
            // 
            this.groupBoxPreview.Controls.Add(this.radioButtonSourceFullscreen);
            this.groupBoxPreview.Controls.Add(this.radioButtonSourceHost);
            this.groupBoxPreview.Controls.Add(this.radioButtonSourceInsert);
            this.groupBoxPreview.Controls.SetChildIndex(this.checkBoxShowSafeArea, 0);
            this.groupBoxPreview.Controls.SetChildIndex(this.radioButtonSourceInsert, 0);
            this.groupBoxPreview.Controls.SetChildIndex(this.radioButtonSourceHost, 0);
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
            this.groupBoxDataset.Controls.Add(this.pictureBoxDatasetSolution);
            this.groupBoxDataset.Controls.Add(this.labelDatasetHostText);
            this.groupBoxDataset.Controls.Add(this.textBoxDatasetHostText);
            this.groupBoxDataset.Controls.Add(this.pictureBoxDatasetTask);
            this.groupBoxDataset.Controls.Add(this.textBoxDataSetName);
            this.groupBoxDataset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxDataset.ForeColor = System.Drawing.Color.White;
            this.groupBoxDataset.Location = new System.Drawing.Point(3, 3);
            this.groupBoxDataset.Name = "groupBoxDataset";
            this.groupBoxDataset.Size = new System.Drawing.Size(913, 373);
            this.groupBoxDataset.TabIndex = 24;
            this.groupBoxDataset.TabStop = false;
            // 
            // pictureBoxDatasetSolution
            // 
            this.pictureBoxDatasetSolution.BackgroundImage = global::Cliparts.SchlagDenStarLive.MainApp.Properties.Resources.SdS_HG_Loop;
            this.pictureBoxDatasetSolution.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxDatasetSolution.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxDatasetSolution.Location = new System.Drawing.Point(631, 189);
            this.pictureBoxDatasetSolution.Name = "pictureBoxDatasetSolution";
            this.pictureBoxDatasetSolution.Size = new System.Drawing.Size(276, 174);
            this.pictureBoxDatasetSolution.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxDatasetSolution.TabIndex = 32;
            this.pictureBoxDatasetSolution.TabStop = false;
            this.pictureBoxDatasetSolution.Click += new System.EventHandler(this.pictureBoxDatasetSolution_Click);
            // 
            // labelDatasetHostText
            // 
            this.labelDatasetHostText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDatasetHostText.Location = new System.Drawing.Point(631, 47);
            this.labelDatasetHostText.Name = "labelDatasetHostText";
            this.labelDatasetHostText.Size = new System.Drawing.Size(276, 24);
            this.labelDatasetHostText.TabIndex = 31;
            this.labelDatasetHostText.Text = "host text";
            this.labelDatasetHostText.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // textBoxDatasetHostText
            // 
            this.textBoxDatasetHostText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDatasetHostText.Location = new System.Drawing.Point(631, 74);
            this.textBoxDatasetHostText.Multiline = true;
            this.textBoxDatasetHostText.Name = "textBoxDatasetHostText";
            this.textBoxDatasetHostText.Size = new System.Drawing.Size(276, 68);
            this.textBoxDatasetHostText.TabIndex = 30;
            this.textBoxDatasetHostText.Text = "textBoxDatasetHostText";
            this.textBoxDatasetHostText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxDatasetHostText.TextChanged += new System.EventHandler(this.textBoxDatasetHostText_TextChanged);
            // 
            // pictureBoxDatasetTask
            // 
            this.pictureBoxDatasetTask.BackColor = System.Drawing.Color.LightGray;
            this.pictureBoxDatasetTask.BackgroundImage = global::Cliparts.SchlagDenStarLive.MainApp.Properties.Resources.SdS_HG_Loop;
            this.pictureBoxDatasetTask.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxDatasetTask.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxDatasetTask.Location = new System.Drawing.Point(12, 18);
            this.pictureBoxDatasetTask.Name = "pictureBoxDatasetTask";
            this.pictureBoxDatasetTask.Size = new System.Drawing.Size(613, 345);
            this.pictureBoxDatasetTask.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxDatasetTask.TabIndex = 22;
            this.pictureBoxDatasetTask.TabStop = false;
            this.pictureBoxDatasetTask.Click += new System.EventHandler(this.pictureBoxDatasetTask_Click);
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
            // groupBoxContent
            // 
            this.groupBoxContent.Controls.Add(this.labelContentPositionY);
            this.groupBoxContent.Controls.Add(this.labelContentPositionX);
            this.groupBoxContent.Controls.Add(this.numericUpDownContentPositionY);
            this.groupBoxContent.Controls.Add(this.numericUpDownContentPositionX);
            this.groupBoxContent.ForeColor = System.Drawing.Color.White;
            this.groupBoxContent.Location = new System.Drawing.Point(819, 208);
            this.groupBoxContent.Name = "groupBoxContent";
            this.groupBoxContent.Size = new System.Drawing.Size(534, 50);
            this.groupBoxContent.TabIndex = 38;
            this.groupBoxContent.TabStop = false;
            this.groupBoxContent.Text = "content";
            // 
            // labelContentPositionY
            // 
            this.labelContentPositionY.Location = new System.Drawing.Point(197, 18);
            this.labelContentPositionY.Name = "labelContentPositionY";
            this.labelContentPositionY.Size = new System.Drawing.Size(30, 24);
            this.labelContentPositionY.TabIndex = 3;
            this.labelContentPositionY.Text = "y:";
            this.labelContentPositionY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelContentPositionX
            // 
            this.labelContentPositionX.Location = new System.Drawing.Point(6, 18);
            this.labelContentPositionX.Name = "labelContentPositionX";
            this.labelContentPositionX.Size = new System.Drawing.Size(100, 24);
            this.labelContentPositionX.TabIndex = 2;
            this.labelContentPositionX.Text = "position.x:";
            this.labelContentPositionX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownContentPositionY
            // 
            this.numericUpDownContentPositionY.Location = new System.Drawing.Point(233, 18);
            this.numericUpDownContentPositionY.Name = "numericUpDownContentPositionY";
            this.numericUpDownContentPositionY.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownContentPositionY.TabIndex = 1;
            this.numericUpDownContentPositionY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownContentPositionY.ValueChanged += new System.EventHandler(this.numericUpDownTextInsertPositionY_ValueChanged);
            // 
            // numericUpDownContentPositionX
            // 
            this.numericUpDownContentPositionX.Location = new System.Drawing.Point(112, 18);
            this.numericUpDownContentPositionX.Name = "numericUpDownContentPositionX";
            this.numericUpDownContentPositionX.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownContentPositionX.TabIndex = 0;
            this.numericUpDownContentPositionX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownContentPositionX.ValueChanged += new System.EventHandler(this.numericUpDownTextInsertPositionX_ValueChanged);
            // 
            // radioButtonSourceHost
            // 
            this.radioButtonSourceHost.AutoSize = true;
            this.radioButtonSourceHost.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSourceHost.Location = new System.Drawing.Point(301, 14);
            this.radioButtonSourceHost.Name = "radioButtonSourceHost";
            this.radioButtonSourceHost.Size = new System.Drawing.Size(59, 22);
            this.radioButtonSourceHost.TabIndex = 12;
            this.radioButtonSourceHost.TabStop = true;
            this.radioButtonSourceHost.Text = "host";
            this.radioButtonSourceHost.UseVisualStyleBackColor = true;
            this.radioButtonSourceHost.CheckedChanged += new System.EventHandler(this.radioButtonSourceHost_CheckedChanged);
            // 
            // radioButtonSourceInsert
            // 
            this.radioButtonSourceInsert.AutoSize = true;
            this.radioButtonSourceInsert.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSourceInsert.Location = new System.Drawing.Point(122, 14);
            this.radioButtonSourceInsert.Name = "radioButtonSourceInsert";
            this.radioButtonSourceInsert.Size = new System.Drawing.Size(68, 22);
            this.radioButtonSourceInsert.TabIndex = 11;
            this.radioButtonSourceInsert.TabStop = true;
            this.radioButtonSourceInsert.Text = "insert";
            this.radioButtonSourceInsert.UseVisualStyleBackColor = true;
            this.radioButtonSourceInsert.CheckedChanged += new System.EventHandler(this.radioButtonSourceInsert_CheckedChanged);
            // 
            // groupBoxTaskCounter
            // 
            this.groupBoxTaskCounter.Controls.Add(this.labelTaskCounterSize);
            this.groupBoxTaskCounter.Controls.Add(this.numericUpDownTaskCounterSize);
            this.groupBoxTaskCounter.Controls.Add(this.labelTaskCounterPositionY);
            this.groupBoxTaskCounter.Controls.Add(this.labelTaskCounterPositionX);
            this.groupBoxTaskCounter.Controls.Add(this.numericUpDownTaskCounterPositionY);
            this.groupBoxTaskCounter.Controls.Add(this.numericUpDownTaskCounterPositionX);
            this.groupBoxTaskCounter.ForeColor = System.Drawing.Color.White;
            this.groupBoxTaskCounter.Location = new System.Drawing.Point(819, 264);
            this.groupBoxTaskCounter.Name = "groupBoxTaskCounter";
            this.groupBoxTaskCounter.Size = new System.Drawing.Size(534, 50);
            this.groupBoxTaskCounter.TabIndex = 41;
            this.groupBoxTaskCounter.TabStop = false;
            this.groupBoxTaskCounter.Text = "task counter";
            // 
            // labelTaskCounterSize
            // 
            this.labelTaskCounterSize.Location = new System.Drawing.Point(318, 18);
            this.labelTaskCounterSize.Name = "labelTaskCounterSize";
            this.labelTaskCounterSize.Size = new System.Drawing.Size(56, 24);
            this.labelTaskCounterSize.TabIndex = 7;
            this.labelTaskCounterSize.Text = "size:";
            this.labelTaskCounterSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownTaskCounterSize
            // 
            this.numericUpDownTaskCounterSize.Location = new System.Drawing.Point(380, 18);
            this.numericUpDownTaskCounterSize.Name = "numericUpDownTaskCounterSize";
            this.numericUpDownTaskCounterSize.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownTaskCounterSize.TabIndex = 6;
            this.numericUpDownTaskCounterSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownTaskCounterSize.ValueChanged += new System.EventHandler(this.numericUpDownTaskCounterSize_ValueChanged);
            // 
            // labelTaskCounterPositionY
            // 
            this.labelTaskCounterPositionY.Location = new System.Drawing.Point(197, 18);
            this.labelTaskCounterPositionY.Name = "labelTaskCounterPositionY";
            this.labelTaskCounterPositionY.Size = new System.Drawing.Size(30, 24);
            this.labelTaskCounterPositionY.TabIndex = 3;
            this.labelTaskCounterPositionY.Text = "y:";
            this.labelTaskCounterPositionY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelTaskCounterPositionX
            // 
            this.labelTaskCounterPositionX.Location = new System.Drawing.Point(6, 18);
            this.labelTaskCounterPositionX.Name = "labelTaskCounterPositionX";
            this.labelTaskCounterPositionX.Size = new System.Drawing.Size(100, 24);
            this.labelTaskCounterPositionX.TabIndex = 2;
            this.labelTaskCounterPositionX.Text = "position.x:";
            this.labelTaskCounterPositionX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownTaskCounterPositionY
            // 
            this.numericUpDownTaskCounterPositionY.Location = new System.Drawing.Point(233, 18);
            this.numericUpDownTaskCounterPositionY.Name = "numericUpDownTaskCounterPositionY";
            this.numericUpDownTaskCounterPositionY.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownTaskCounterPositionY.TabIndex = 1;
            this.numericUpDownTaskCounterPositionY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownTaskCounterPositionY.ValueChanged += new System.EventHandler(this.numericUpDownTaskCounterPositionY_ValueChanged);
            // 
            // numericUpDownTaskCounterPositionX
            // 
            this.numericUpDownTaskCounterPositionX.Location = new System.Drawing.Point(112, 18);
            this.numericUpDownTaskCounterPositionX.Name = "numericUpDownTaskCounterPositionX";
            this.numericUpDownTaskCounterPositionX.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownTaskCounterPositionX.TabIndex = 0;
            this.numericUpDownTaskCounterPositionX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownTaskCounterPositionX.ValueChanged += new System.EventHandler(this.numericUpDownTaskCounterPositionX_ValueChanged);
            // 
            // radioButtonSourceFullscreen
            // 
            this.radioButtonSourceFullscreen.AutoSize = true;
            this.radioButtonSourceFullscreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSourceFullscreen.Location = new System.Drawing.Point(196, 14);
            this.radioButtonSourceFullscreen.Name = "radioButtonSourceFullscreen";
            this.radioButtonSourceFullscreen.Size = new System.Drawing.Size(99, 22);
            this.radioButtonSourceFullscreen.TabIndex = 17;
            this.radioButtonSourceFullscreen.TabStop = true;
            this.radioButtonSourceFullscreen.Text = "fullscreen";
            this.radioButtonSourceFullscreen.UseVisualStyleBackColor = true;
            this.radioButtonSourceFullscreen.CheckedChanged += new System.EventHandler(this.radioButtonSourceFullscreen_CheckedChanged);
            // 
            // UserControlContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxTaskCounter);
            this.Controls.Add(this.groupBoxContent);
            this.Controls.Add(this.panelData);
            this.Controls.Add(this.buttonSaveAs);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.labelFilename);
            this.Name = "UserControlContent";
            this.Controls.SetChildIndex(this.groupBoxPreview, 0);
            this.Controls.SetChildIndex(this.labelGameClass, 0);
            this.Controls.SetChildIndex(this.labelFilename, 0);
            this.Controls.SetChildIndex(this.buttonLoad, 0);
            this.Controls.SetChildIndex(this.buttonSave, 0);
            this.Controls.SetChildIndex(this.buttonSaveAs, 0);
            this.Controls.SetChildIndex(this.panelData, 0);
            this.Controls.SetChildIndex(this.groupBoxContent, 0);
            this.Controls.SetChildIndex(this.groupBoxTaskCounter, 0);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionX)).EndInit();
            this.groupBoxPreview.ResumeLayout(false);
            this.groupBoxPreview.PerformLayout();
            this.panelData.ResumeLayout(false);
            this.panelData.PerformLayout();
            this.groupBoxDataset.ResumeLayout(false);
            this.groupBoxDataset.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDatasetSolution)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDatasetTask)).EndInit();
            this.groupBoxContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownContentPositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownContentPositionX)).EndInit();
            this.groupBoxTaskCounter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTaskCounterSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTaskCounterPositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTaskCounterPositionX)).EndInit();
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
        private System.Windows.Forms.PictureBox pictureBoxDatasetTask;
        private System.Windows.Forms.Button buttonDataRemoveAllSets;
        private System.Windows.Forms.Button buttonDataRemoveSet;
        private System.Windows.Forms.Button buttonDataAddNewSet;
        private System.Windows.Forms.TextBox textBoxDataSetName;
        private System.Windows.Forms.Button buttonDataMoveSetDown;
        private System.Windows.Forms.Button buttonDataMoveSetUp;
        private System.Windows.Forms.ListBox listBoxDataList;
        private System.Windows.Forms.Button buttonDataResort;
        protected System.Windows.Forms.GroupBox groupBoxContent;
        protected System.Windows.Forms.Label labelContentPositionY;
        protected System.Windows.Forms.Label labelContentPositionX;
        protected System.Windows.Forms.NumericUpDown numericUpDownContentPositionY;
        protected System.Windows.Forms.NumericUpDown numericUpDownContentPositionX;
        private System.Windows.Forms.RadioButton radioButtonSourceHost;
        private System.Windows.Forms.RadioButton radioButtonSourceInsert;
        protected System.Windows.Forms.GroupBox groupBoxTaskCounter;
        protected System.Windows.Forms.Label labelTaskCounterSize;
        protected System.Windows.Forms.NumericUpDown numericUpDownTaskCounterSize;
        protected System.Windows.Forms.Label labelTaskCounterPositionY;
        protected System.Windows.Forms.Label labelTaskCounterPositionX;
        protected System.Windows.Forms.NumericUpDown numericUpDownTaskCounterPositionY;
        protected System.Windows.Forms.NumericUpDown numericUpDownTaskCounterPositionX;
        private System.Windows.Forms.Label labelDatasetHostText;
        private System.Windows.Forms.TextBox textBoxDatasetHostText;
        private System.Windows.Forms.PictureBox pictureBoxDatasetSolution;
        private System.Windows.Forms.RadioButton radioButtonSourceFullscreen;
    }
}
