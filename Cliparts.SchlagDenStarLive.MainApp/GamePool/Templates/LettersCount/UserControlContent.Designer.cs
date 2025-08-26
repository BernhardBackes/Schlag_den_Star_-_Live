namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.LettersCount {

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
            this.labelDataSetLetters = new System.Windows.Forms.Label();
            this.labelDataSetLettersCount = new System.Windows.Forms.Label();
            this.labelDataSetText = new System.Windows.Forms.Label();
            this.textBoxDataSetLetters = new System.Windows.Forms.TextBox();
            this.textBoxDataSetLettersCount = new System.Windows.Forms.TextBox();
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
            this.radioButtonSourceContent = new System.Windows.Forms.RadioButton();
            this.radioButtonSourceHost = new System.Windows.Forms.RadioButton();
            this.radioButtonSourceScore = new System.Windows.Forms.RadioButton();
            this.radioButtonSourcePlayer = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScorePositionX)).BeginInit();
            this.groupBoxPreview.SuspendLayout();
            this.panelData.SuspendLayout();
            this.groupBoxDataset.SuspendLayout();
            this.groupBoxTextInsert.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTextInsertPositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTextInsertPositionX)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxScoreStyle
            // 
            this.comboBoxScoreStyle.Size = new System.Drawing.Size(148, 26);
            // 
            // groupBoxPreview
            // 
            this.groupBoxPreview.Controls.Add(this.radioButtonSourcePlayer);
            this.groupBoxPreview.Controls.Add(this.radioButtonSourceContent);
            this.groupBoxPreview.Controls.Add(this.radioButtonSourceHost);
            this.groupBoxPreview.Controls.Add(this.radioButtonSourceScore);
            this.groupBoxPreview.Controls.SetChildIndex(this.checkBoxShowSafeArea, 0);
            this.groupBoxPreview.Controls.SetChildIndex(this.radioButtonSourceScore, 0);
            this.groupBoxPreview.Controls.SetChildIndex(this.radioButtonSourceHost, 0);
            this.groupBoxPreview.Controls.SetChildIndex(this.radioButtonSourceContent, 0);
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
            this.groupBoxDataset.Controls.Add(this.labelDataSetLetters);
            this.groupBoxDataset.Controls.Add(this.labelDataSetLettersCount);
            this.groupBoxDataset.Controls.Add(this.labelDataSetText);
            this.groupBoxDataset.Controls.Add(this.textBoxDataSetLetters);
            this.groupBoxDataset.Controls.Add(this.textBoxDataSetLettersCount);
            this.groupBoxDataset.Controls.Add(this.textBoxDataSetText);
            this.groupBoxDataset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxDataset.ForeColor = System.Drawing.Color.White;
            this.groupBoxDataset.Location = new System.Drawing.Point(3, 3);
            this.groupBoxDataset.Name = "groupBoxDataset";
            this.groupBoxDataset.Size = new System.Drawing.Size(913, 373);
            this.groupBoxDataset.TabIndex = 24;
            this.groupBoxDataset.TabStop = false;
            // 
            // labelDataSetLetters
            // 
            this.labelDataSetLetters.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDataSetLetters.Location = new System.Drawing.Point(6, 77);
            this.labelDataSetLetters.Name = "labelDataSetLetters";
            this.labelDataSetLetters.Size = new System.Drawing.Size(222, 24);
            this.labelDataSetLetters.TabIndex = 23;
            this.labelDataSetLetters.Text = "letters:";
            this.labelDataSetLetters.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelDataSetLettersCount
            // 
            this.labelDataSetLettersCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDataSetLettersCount.Location = new System.Drawing.Point(6, 47);
            this.labelDataSetLettersCount.Name = "labelDataSetLettersCount";
            this.labelDataSetLettersCount.Size = new System.Drawing.Size(222, 24);
            this.labelDataSetLettersCount.TabIndex = 22;
            this.labelDataSetLettersCount.Text = "letters count:";
            this.labelDataSetLettersCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelDataSetText
            // 
            this.labelDataSetText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDataSetText.Location = new System.Drawing.Point(6, 17);
            this.labelDataSetText.Name = "labelDataSetText";
            this.labelDataSetText.Size = new System.Drawing.Size(222, 24);
            this.labelDataSetText.TabIndex = 21;
            this.labelDataSetText.Text = "text:";
            this.labelDataSetText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxDataSetLetters
            // 
            this.textBoxDataSetLetters.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDataSetLetters.Location = new System.Drawing.Point(234, 77);
            this.textBoxDataSetLetters.Name = "textBoxDataSetLetters";
            this.textBoxDataSetLetters.ReadOnly = true;
            this.textBoxDataSetLetters.Size = new System.Drawing.Size(407, 24);
            this.textBoxDataSetLetters.TabIndex = 20;
            this.textBoxDataSetLetters.Text = "textBoxDataSetLetters";
            // 
            // textBoxDataSetLettersCount
            // 
            this.textBoxDataSetLettersCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDataSetLettersCount.Location = new System.Drawing.Point(234, 47);
            this.textBoxDataSetLettersCount.Name = "textBoxDataSetLettersCount";
            this.textBoxDataSetLettersCount.ReadOnly = true;
            this.textBoxDataSetLettersCount.Size = new System.Drawing.Size(79, 24);
            this.textBoxDataSetLettersCount.TabIndex = 19;
            this.textBoxDataSetLettersCount.Text = "textBoxDataSetLettersCount";
            this.textBoxDataSetLettersCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxDataSetText
            // 
            this.textBoxDataSetText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDataSetText.Location = new System.Drawing.Point(234, 17);
            this.textBoxDataSetText.Name = "textBoxDataSetText";
            this.textBoxDataSetText.Size = new System.Drawing.Size(608, 24);
            this.textBoxDataSetText.TabIndex = 18;
            this.textBoxDataSetText.Text = "textBoxDataSetText";
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
            this.groupBoxTextInsert.Location = new System.Drawing.Point(819, 208);
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
            // radioButtonSourceContent
            // 
            this.radioButtonSourceContent.AutoSize = true;
            this.radioButtonSourceContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSourceContent.Location = new System.Drawing.Point(187, 14);
            this.radioButtonSourceContent.Name = "radioButtonSourceContent";
            this.radioButtonSourceContent.Size = new System.Drawing.Size(82, 22);
            this.radioButtonSourceContent.TabIndex = 24;
            this.radioButtonSourceContent.TabStop = true;
            this.radioButtonSourceContent.Text = "content";
            this.radioButtonSourceContent.UseVisualStyleBackColor = true;
            this.radioButtonSourceContent.CheckedChanged += new System.EventHandler(this.radioButtonSourceContent_CheckedChanged);
            // 
            // radioButtonSourceHost
            // 
            this.radioButtonSourceHost.AutoSize = true;
            this.radioButtonSourceHost.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSourceHost.Location = new System.Drawing.Point(275, 14);
            this.radioButtonSourceHost.Name = "radioButtonSourceHost";
            this.radioButtonSourceHost.Size = new System.Drawing.Size(59, 22);
            this.radioButtonSourceHost.TabIndex = 23;
            this.radioButtonSourceHost.TabStop = true;
            this.radioButtonSourceHost.Text = "host";
            this.radioButtonSourceHost.UseVisualStyleBackColor = true;
            this.radioButtonSourceHost.CheckedChanged += new System.EventHandler(this.radioButtonSourceHost_CheckedChanged);
            // 
            // radioButtonSourceScore
            // 
            this.radioButtonSourceScore.AutoSize = true;
            this.radioButtonSourceScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSourceScore.Location = new System.Drawing.Point(112, 14);
            this.radioButtonSourceScore.Name = "radioButtonSourceScore";
            this.radioButtonSourceScore.Size = new System.Drawing.Size(69, 22);
            this.radioButtonSourceScore.TabIndex = 22;
            this.radioButtonSourceScore.TabStop = true;
            this.radioButtonSourceScore.Text = "score";
            this.radioButtonSourceScore.UseVisualStyleBackColor = true;
            this.radioButtonSourceScore.CheckedChanged += new System.EventHandler(this.radioButtonSourceScore_CheckedChanged);
            // 
            // radioButtonSourcePlayer
            // 
            this.radioButtonSourcePlayer.AutoSize = true;
            this.radioButtonSourcePlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSourcePlayer.Location = new System.Drawing.Point(340, 14);
            this.radioButtonSourcePlayer.Name = "radioButtonSourcePlayer";
            this.radioButtonSourcePlayer.Size = new System.Drawing.Size(71, 22);
            this.radioButtonSourcePlayer.TabIndex = 25;
            this.radioButtonSourcePlayer.TabStop = true;
            this.radioButtonSourcePlayer.Text = "player";
            this.radioButtonSourcePlayer.UseVisualStyleBackColor = true;
            this.radioButtonSourcePlayer.CheckedChanged += new System.EventHandler(this.radioButtonSourcePlayer_CheckedChanged);
            // 
            // UserControlContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxTextInsert);
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
            this.Controls.SetChildIndex(this.groupBoxTextInsert, 0);
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
        private System.Windows.Forms.Button buttonDataImportSets;
        private System.Windows.Forms.Label labelDataSetLetters;
        private System.Windows.Forms.Label labelDataSetLettersCount;
        private System.Windows.Forms.Label labelDataSetText;
        private System.Windows.Forms.TextBox textBoxDataSetLetters;
        private System.Windows.Forms.TextBox textBoxDataSetLettersCount;
        private System.Windows.Forms.RadioButton radioButtonSourcePlayer;
        private System.Windows.Forms.RadioButton radioButtonSourceContent;
        private System.Windows.Forms.RadioButton radioButtonSourceHost;
        private System.Windows.Forms.RadioButton radioButtonSourceScore;
    }
}
