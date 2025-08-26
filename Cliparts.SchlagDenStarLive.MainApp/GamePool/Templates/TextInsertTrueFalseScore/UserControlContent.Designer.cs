namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TextInsertTrueFalseScore {
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
            this.buttonDataImportSets = new System.Windows.Forms.Button();
            this.buttonDataResort = new System.Windows.Forms.Button();
            this.groupBoxDataset = new System.Windows.Forms.GroupBox();
            this.checkBoxDataSetIsTrue = new System.Windows.Forms.CheckBox();
            this.labelDataSetPlayerText = new System.Windows.Forms.Label();
            this.textBoxDataSetPlayerText = new System.Windows.Forms.TextBox();
            this.labelDataSetHostText = new System.Windows.Forms.Label();
            this.labelDataSetText = new System.Windows.Forms.Label();
            this.textBoxDataSetHostText = new System.Windows.Forms.TextBox();
            this.textBoxDataSetText = new System.Windows.Forms.TextBox();
            this.buttonDataRemoveAllSets = new System.Windows.Forms.Button();
            this.buttonDataRemoveSet = new System.Windows.Forms.Button();
            this.buttonDataAddNewSet = new System.Windows.Forms.Button();
            this.buttonDataMoveSetDown = new System.Windows.Forms.Button();
            this.buttonDataMoveSetUp = new System.Windows.Forms.Button();
            this.listBoxDataList = new System.Windows.Forms.ListBox();
            this.groupBoxTextInsert = new System.Windows.Forms.GroupBox();
            this.labelTextInsertPositionY = new System.Windows.Forms.Label();
            this.labelTextInsertPositionX = new System.Windows.Forms.Label();
            this.numericUpDownTextInsertPositionY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownTextInsertPositionX = new System.Windows.Forms.NumericUpDown();
            this.radioButtonSourceHost = new System.Windows.Forms.RadioButton();
            this.radioButtonSourceInsert = new System.Windows.Forms.RadioButton();
            this.radioButtonSourcePlayer = new System.Windows.Forms.RadioButton();
            this.groupBoxTimeout = new System.Windows.Forms.GroupBox();
            this.checkBoxTimeoutIsVisible = new System.Windows.Forms.CheckBox();
            this.labelTimeoutPositionY = new System.Windows.Forms.Label();
            this.labelTimeoutPositionX = new System.Windows.Forms.Label();
            this.numericUpDownTimeoutPositionY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownTimeoutPositionX = new System.Windows.Forms.NumericUpDown();
            this.groupBoxScore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionX)).BeginInit();
            this.groupBoxPreview.SuspendLayout();
            this.panelData.SuspendLayout();
            this.groupBoxDataset.SuspendLayout();
            this.groupBoxTextInsert.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTextInsertPositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTextInsertPositionX)).BeginInit();
            this.groupBoxTimeout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeoutPositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeoutPositionX)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxScoreStyle
            // 
            this.comboBoxScoreStyle.Size = new System.Drawing.Size(148, 26);
            // 
            // groupBoxPreview
            // 
            this.groupBoxPreview.Controls.Add(this.radioButtonSourcePlayer);
            this.groupBoxPreview.Controls.Add(this.radioButtonSourceHost);
            this.groupBoxPreview.Controls.Add(this.radioButtonSourceInsert);
            this.groupBoxPreview.Controls.SetChildIndex(this.checkBoxShowSafeArea, 0);
            this.groupBoxPreview.Controls.SetChildIndex(this.radioButtonSourceInsert, 0);
            this.groupBoxPreview.Controls.SetChildIndex(this.radioButtonSourceHost, 0);
            this.groupBoxPreview.Controls.SetChildIndex(this.radioButtonSourcePlayer, 0);
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
            this.panelData.Controls.Add(this.buttonDataImportSets);
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
            // buttonDataImportSets
            // 
            this.buttonDataImportSets.ForeColor = System.Drawing.Color.Black;
            this.buttonDataImportSets.Location = new System.Drawing.Point(1168, 63);
            this.buttonDataImportSets.Name = "buttonDataImportSets";
            this.buttonDataImportSets.Size = new System.Drawing.Size(170, 26);
            this.buttonDataImportSets.TabIndex = 33;
            this.buttonDataImportSets.Text = "import datasets";
            this.buttonDataImportSets.UseVisualStyleBackColor = true;
            this.buttonDataImportSets.Click += new System.EventHandler(this.buttonDataImportSets_Click);
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
            this.groupBoxDataset.Controls.Add(this.checkBoxDataSetIsTrue);
            this.groupBoxDataset.Controls.Add(this.labelDataSetPlayerText);
            this.groupBoxDataset.Controls.Add(this.textBoxDataSetPlayerText);
            this.groupBoxDataset.Controls.Add(this.labelDataSetHostText);
            this.groupBoxDataset.Controls.Add(this.labelDataSetText);
            this.groupBoxDataset.Controls.Add(this.textBoxDataSetHostText);
            this.groupBoxDataset.Controls.Add(this.textBoxDataSetText);
            this.groupBoxDataset.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxDataset.ForeColor = System.Drawing.Color.White;
            this.groupBoxDataset.Location = new System.Drawing.Point(3, 3);
            this.groupBoxDataset.Name = "groupBoxDataset";
            this.groupBoxDataset.Size = new System.Drawing.Size(913, 373);
            this.groupBoxDataset.TabIndex = 24;
            this.groupBoxDataset.TabStop = false;
            // 
            // checkBoxDataSetIsTrue
            // 
            this.checkBoxDataSetIsTrue.AutoSize = true;
            this.checkBoxDataSetIsTrue.Location = new System.Drawing.Point(311, 171);
            this.checkBoxDataSetIsTrue.Name = "checkBoxDataSetIsTrue";
            this.checkBoxDataSetIsTrue.Size = new System.Drawing.Size(74, 22);
            this.checkBoxDataSetIsTrue.TabIndex = 23;
            this.checkBoxDataSetIsTrue.Text = "is true";
            this.checkBoxDataSetIsTrue.UseVisualStyleBackColor = true;
            this.checkBoxDataSetIsTrue.CheckedChanged += new System.EventHandler(this.checkBoxDataSetIsTrue_CheckedChanged);
            // 
            // labelDataSetPlayerText
            // 
            this.labelDataSetPlayerText.Location = new System.Drawing.Point(205, 121);
            this.labelDataSetPlayerText.Name = "labelDataSetPlayerText";
            this.labelDataSetPlayerText.Size = new System.Drawing.Size(100, 24);
            this.labelDataSetPlayerText.TabIndex = 22;
            this.labelDataSetPlayerText.Text = "player:";
            this.labelDataSetPlayerText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxDataSetPlayerText
            // 
            this.textBoxDataSetPlayerText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDataSetPlayerText.Location = new System.Drawing.Point(311, 121);
            this.textBoxDataSetPlayerText.Multiline = true;
            this.textBoxDataSetPlayerText.Name = "textBoxDataSetPlayerText";
            this.textBoxDataSetPlayerText.Size = new System.Drawing.Size(314, 44);
            this.textBoxDataSetPlayerText.TabIndex = 21;
            this.textBoxDataSetPlayerText.Text = "textBoxDataSetPlayerText\r\n2. Zeile";
            this.textBoxDataSetPlayerText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxDataSetPlayerText.TextChanged += new System.EventHandler(this.textBoxDataSetPlayerText_TextChanged);
            // 
            // labelDataSetHostText
            // 
            this.labelDataSetHostText.Location = new System.Drawing.Point(205, 50);
            this.labelDataSetHostText.Name = "labelDataSetHostText";
            this.labelDataSetHostText.Size = new System.Drawing.Size(100, 24);
            this.labelDataSetHostText.TabIndex = 20;
            this.labelDataSetHostText.Text = "host:";
            this.labelDataSetHostText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelDataSetText
            // 
            this.labelDataSetText.Location = new System.Drawing.Point(205, 20);
            this.labelDataSetText.Name = "labelDataSetText";
            this.labelDataSetText.Size = new System.Drawing.Size(100, 24);
            this.labelDataSetText.TabIndex = 4;
            this.labelDataSetText.Text = "text:";
            this.labelDataSetText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxDataSetHostText
            // 
            this.textBoxDataSetHostText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDataSetHostText.Location = new System.Drawing.Point(311, 50);
            this.textBoxDataSetHostText.Multiline = true;
            this.textBoxDataSetHostText.Name = "textBoxDataSetHostText";
            this.textBoxDataSetHostText.Size = new System.Drawing.Size(314, 65);
            this.textBoxDataSetHostText.TabIndex = 19;
            this.textBoxDataSetHostText.Text = "textBoxDataSetHostText\r\n2. Zeile\r\n3. Zeile";
            this.textBoxDataSetHostText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxDataSetHostText.TextChanged += new System.EventHandler(this.textBoxDataSetHostText_TextChanged);
            // 
            // textBoxDataSetText
            // 
            this.textBoxDataSetText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDataSetText.Location = new System.Drawing.Point(311, 20);
            this.textBoxDataSetText.Name = "textBoxDataSetText";
            this.textBoxDataSetText.Size = new System.Drawing.Size(314, 24);
            this.textBoxDataSetText.TabIndex = 18;
            this.textBoxDataSetText.Text = "textBoxDataSetText\r";
            this.textBoxDataSetText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxDataSetText.TextChanged += new System.EventHandler(this.textBoxDataSetText_TextChanged);
            // 
            // buttonDataRemoveAllSets
            // 
            this.buttonDataRemoveAllSets.ForeColor = System.Drawing.Color.Black;
            this.buttonDataRemoveAllSets.Location = new System.Drawing.Point(1168, 127);
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
            this.buttonDataRemoveSet.Location = new System.Drawing.Point(1168, 95);
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
            // groupBoxTextInsert
            // 
            this.groupBoxTextInsert.Controls.Add(this.labelTextInsertPositionY);
            this.groupBoxTextInsert.Controls.Add(this.labelTextInsertPositionX);
            this.groupBoxTextInsert.Controls.Add(this.numericUpDownTextInsertPositionY);
            this.groupBoxTextInsert.Controls.Add(this.numericUpDownTextInsertPositionX);
            this.groupBoxTextInsert.ForeColor = System.Drawing.Color.White;
            this.groupBoxTextInsert.Location = new System.Drawing.Point(819, 118);
            this.groupBoxTextInsert.Name = "groupBoxTextInsert";
            this.groupBoxTextInsert.Size = new System.Drawing.Size(534, 50);
            this.groupBoxTextInsert.TabIndex = 38;
            this.groupBoxTextInsert.TabStop = false;
            this.groupBoxTextInsert.Text = "text insert";
            // 
            // labelTextInsertPositionY
            // 
            this.labelTextInsertPositionY.Location = new System.Drawing.Point(197, 18);
            this.labelTextInsertPositionY.Name = "labelTextInsertPositionY";
            this.labelTextInsertPositionY.Size = new System.Drawing.Size(30, 24);
            this.labelTextInsertPositionY.TabIndex = 3;
            this.labelTextInsertPositionY.Text = "y:";
            this.labelTextInsertPositionY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelTextInsertPositionX
            // 
            this.labelTextInsertPositionX.Location = new System.Drawing.Point(6, 18);
            this.labelTextInsertPositionX.Name = "labelTextInsertPositionX";
            this.labelTextInsertPositionX.Size = new System.Drawing.Size(100, 24);
            this.labelTextInsertPositionX.TabIndex = 2;
            this.labelTextInsertPositionX.Text = "position.x:";
            this.labelTextInsertPositionX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownTextInsertPositionY
            // 
            this.numericUpDownTextInsertPositionY.Location = new System.Drawing.Point(233, 18);
            this.numericUpDownTextInsertPositionY.Name = "numericUpDownTextInsertPositionY";
            this.numericUpDownTextInsertPositionY.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownTextInsertPositionY.TabIndex = 1;
            this.numericUpDownTextInsertPositionY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownTextInsertPositionY.ValueChanged += new System.EventHandler(this.numericUpDownTextInsertPositionY_ValueChanged);
            // 
            // numericUpDownTextInsertPositionX
            // 
            this.numericUpDownTextInsertPositionX.Location = new System.Drawing.Point(112, 18);
            this.numericUpDownTextInsertPositionX.Name = "numericUpDownTextInsertPositionX";
            this.numericUpDownTextInsertPositionX.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownTextInsertPositionX.TabIndex = 0;
            this.numericUpDownTextInsertPositionX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownTextInsertPositionX.ValueChanged += new System.EventHandler(this.numericUpDownTextInsertPositionX_ValueChanged);
            // 
            // radioButtonSourceHost
            // 
            this.radioButtonSourceHost.AutoSize = true;
            this.radioButtonSourceHost.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSourceHost.Location = new System.Drawing.Point(186, 14);
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
            this.radioButtonSourceInsert.Location = new System.Drawing.Point(112, 14);
            this.radioButtonSourceInsert.Name = "radioButtonSourceInsert";
            this.radioButtonSourceInsert.Size = new System.Drawing.Size(68, 22);
            this.radioButtonSourceInsert.TabIndex = 11;
            this.radioButtonSourceInsert.TabStop = true;
            this.radioButtonSourceInsert.Text = "insert";
            this.radioButtonSourceInsert.UseVisualStyleBackColor = true;
            this.radioButtonSourceInsert.CheckedChanged += new System.EventHandler(this.radioButtonSourceInsert_CheckedChanged);
            // 
            // radioButtonSourcePlayer
            // 
            this.radioButtonSourcePlayer.AutoSize = true;
            this.radioButtonSourcePlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSourcePlayer.Location = new System.Drawing.Point(250, 14);
            this.radioButtonSourcePlayer.Name = "radioButtonSourcePlayer";
            this.radioButtonSourcePlayer.Size = new System.Drawing.Size(71, 22);
            this.radioButtonSourcePlayer.TabIndex = 13;
            this.radioButtonSourcePlayer.TabStop = true;
            this.radioButtonSourcePlayer.Text = "player";
            this.radioButtonSourcePlayer.UseVisualStyleBackColor = true;
            this.radioButtonSourcePlayer.CheckedChanged += new System.EventHandler(this.radioButtonSourcePlayer_CheckedChanged);
            // 
            // groupBoxTimeout
            // 
            this.groupBoxTimeout.Controls.Add(this.checkBoxTimeoutIsVisible);
            this.groupBoxTimeout.Controls.Add(this.labelTimeoutPositionY);
            this.groupBoxTimeout.Controls.Add(this.labelTimeoutPositionX);
            this.groupBoxTimeout.Controls.Add(this.numericUpDownTimeoutPositionY);
            this.groupBoxTimeout.Controls.Add(this.numericUpDownTimeoutPositionX);
            this.groupBoxTimeout.ForeColor = System.Drawing.Color.White;
            this.groupBoxTimeout.Location = new System.Drawing.Point(819, 62);
            this.groupBoxTimeout.Name = "groupBoxTimeout";
            this.groupBoxTimeout.Size = new System.Drawing.Size(534, 50);
            this.groupBoxTimeout.TabIndex = 39;
            this.groupBoxTimeout.TabStop = false;
            this.groupBoxTimeout.Text = "timeout";
            // 
            // checkBoxTimeoutIsVisible
            // 
            this.checkBoxTimeoutIsVisible.AutoSize = true;
            this.checkBoxTimeoutIsVisible.Location = new System.Drawing.Point(360, 19);
            this.checkBoxTimeoutIsVisible.Name = "checkBoxTimeoutIsVisible";
            this.checkBoxTimeoutIsVisible.Size = new System.Drawing.Size(92, 22);
            this.checkBoxTimeoutIsVisible.TabIndex = 4;
            this.checkBoxTimeoutIsVisible.Text = "is visible";
            this.checkBoxTimeoutIsVisible.UseVisualStyleBackColor = true;
            this.checkBoxTimeoutIsVisible.Click += new System.EventHandler(this.checkBoxTimeoutIsVisible_Click);
            // 
            // labelTimeoutPositionY
            // 
            this.labelTimeoutPositionY.Location = new System.Drawing.Point(197, 18);
            this.labelTimeoutPositionY.Name = "labelTimeoutPositionY";
            this.labelTimeoutPositionY.Size = new System.Drawing.Size(30, 24);
            this.labelTimeoutPositionY.TabIndex = 3;
            this.labelTimeoutPositionY.Text = "y:";
            this.labelTimeoutPositionY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelTimeoutPositionX
            // 
            this.labelTimeoutPositionX.Location = new System.Drawing.Point(6, 18);
            this.labelTimeoutPositionX.Name = "labelTimeoutPositionX";
            this.labelTimeoutPositionX.Size = new System.Drawing.Size(100, 24);
            this.labelTimeoutPositionX.TabIndex = 2;
            this.labelTimeoutPositionX.Text = "position.x:";
            this.labelTimeoutPositionX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownTimeoutPositionY
            // 
            this.numericUpDownTimeoutPositionY.Location = new System.Drawing.Point(233, 18);
            this.numericUpDownTimeoutPositionY.Name = "numericUpDownTimeoutPositionY";
            this.numericUpDownTimeoutPositionY.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownTimeoutPositionY.TabIndex = 1;
            this.numericUpDownTimeoutPositionY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownTimeoutPositionY.ValueChanged += new System.EventHandler(this.numericUpDownTimeoutPositionY_ValueChanged);
            // 
            // numericUpDownTimeoutPositionX
            // 
            this.numericUpDownTimeoutPositionX.Location = new System.Drawing.Point(112, 18);
            this.numericUpDownTimeoutPositionX.Name = "numericUpDownTimeoutPositionX";
            this.numericUpDownTimeoutPositionX.Size = new System.Drawing.Size(79, 24);
            this.numericUpDownTimeoutPositionX.TabIndex = 0;
            this.numericUpDownTimeoutPositionX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownTimeoutPositionX.ValueChanged += new System.EventHandler(this.numericUpDownTimeoutPositionX_ValueChanged);
            // 
            // UserControlContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxTimeout);
            this.Controls.Add(this.groupBoxTextInsert);
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
            this.Controls.SetChildIndex(this.groupBoxTextInsert, 0);
            this.Controls.SetChildIndex(this.groupBoxTimeout, 0);
            this.groupBoxScore.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionX)).EndInit();
            this.groupBoxPreview.ResumeLayout(false);
            this.groupBoxPreview.PerformLayout();
            this.panelData.ResumeLayout(false);
            this.panelData.PerformLayout();
            this.groupBoxDataset.ResumeLayout(false);
            this.groupBoxDataset.PerformLayout();
            this.groupBoxTextInsert.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTextInsertPositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTextInsertPositionX)).EndInit();
            this.groupBoxTimeout.ResumeLayout(false);
            this.groupBoxTimeout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeoutPositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeoutPositionX)).EndInit();
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
        private System.Windows.Forms.Button buttonDataRemoveAllSets;
        private System.Windows.Forms.Button buttonDataRemoveSet;
        private System.Windows.Forms.Button buttonDataAddNewSet;
        private System.Windows.Forms.TextBox textBoxDataSetText;
        private System.Windows.Forms.Button buttonDataMoveSetDown;
        private System.Windows.Forms.Button buttonDataMoveSetUp;
        private System.Windows.Forms.ListBox listBoxDataList;
        private System.Windows.Forms.Button buttonDataResort;
        protected System.Windows.Forms.GroupBox groupBoxTextInsert;
        protected System.Windows.Forms.Label labelTextInsertPositionY;
        protected System.Windows.Forms.Label labelTextInsertPositionX;
        protected System.Windows.Forms.NumericUpDown numericUpDownTextInsertPositionY;
        protected System.Windows.Forms.NumericUpDown numericUpDownTextInsertPositionX;
        protected System.Windows.Forms.Label labelDataSetHostText;
        protected System.Windows.Forms.Label labelDataSetText;
        private System.Windows.Forms.TextBox textBoxDataSetHostText;
        private System.Windows.Forms.Button buttonDataImportSets;
        protected System.Windows.Forms.Label labelDataSetPlayerText;
        private System.Windows.Forms.TextBox textBoxDataSetPlayerText;
        private System.Windows.Forms.CheckBox checkBoxDataSetIsTrue;
        private System.Windows.Forms.RadioButton radioButtonSourcePlayer;
        private System.Windows.Forms.RadioButton radioButtonSourceHost;
        private System.Windows.Forms.RadioButton radioButtonSourceInsert;
        private System.Windows.Forms.GroupBox groupBoxTimeout;
        private System.Windows.Forms.CheckBox checkBoxTimeoutIsVisible;
        private System.Windows.Forms.Label labelTimeoutPositionY;
        private System.Windows.Forms.Label labelTimeoutPositionX;
        private System.Windows.Forms.NumericUpDown numericUpDownTimeoutPositionY;
        private System.Windows.Forms.NumericUpDown numericUpDownTimeoutPositionX;
    }
}
