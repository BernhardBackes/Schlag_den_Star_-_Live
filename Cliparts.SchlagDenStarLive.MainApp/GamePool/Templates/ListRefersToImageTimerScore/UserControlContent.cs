using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.Tools.Base;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.ListRefersToImageTimerScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.ListRefersToImageTimerScore {

    public partial class UserControlContent : _Base.TimerScore.UserControlContent {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;
        private int selectedDatasetIndex = -1;

        private DatasetItem selectedDatasetItem = null;
        private int selectedDatasetItemIndex = -1;

        private Preview.Sources _previewSource = Preview.Sources.Insert;
        protected Preview.Sources previewSource {
            get { return this._previewSource; }
            set {
                if (this._previewSource != value) {
                    this._previewSource = value;
                    this.setPreviewSource(value);
                }
            }
        }

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownBorderPositionX.Minimum = int.MinValue;
            this.numericUpDownBorderPositionX.Maximum = int.MaxValue;

            this.numericUpDownBorderPositionY.Minimum = int.MinValue;
            this.numericUpDownBorderPositionY.Maximum = int.MaxValue;

            this.numericUpDownBorderScaling.Minimum = int.MinValue;
            this.numericUpDownBorderScaling.Maximum = int.MaxValue;

            this.comboBoxBorderStyle.BeginUpdate();
            this.comboBoxBorderStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.Border.Styles)));
            this.comboBoxBorderStyle.EndUpdate();

            this.numericUpDownItemInsertPositionX.Minimum = int.MinValue;
            this.numericUpDownItemInsertPositionX.Maximum = int.MaxValue;

            this.numericUpDownItemInsertPositionY.Minimum = int.MinValue;
            this.numericUpDownItemInsertPositionY.Maximum = int.MaxValue;

            this.numericUpDownTaskCounterPositionX.Minimum = int.MinValue;
            this.numericUpDownTaskCounterPositionX.Maximum = int.MaxValue;

            this.numericUpDownTaskCounterPositionY.Minimum = int.MinValue;
            this.numericUpDownTaskCounterPositionY.Maximum = int.MaxValue;

            this.numericUpDownTaskCounterSize.Minimum = int.MinValue;
            this.numericUpDownTaskCounterSize.Maximum = int.MaxValue;

            this.comboBoxDatasetHostStyle.BeginUpdate();
            this.comboBoxDatasetHostStyle.Items.AddRange(Enum.GetNames(typeof(Stage.HostStyles)));
            this.comboBoxDatasetHostStyle.EndUpdate();

            this.comboBoxDatasetInsertStyle.BeginUpdate();
            this.comboBoxDatasetInsertStyle.Items.AddRange(Enum.GetNames(typeof(InsertList.Styles)));
            this.comboBoxDatasetInsertStyle.EndUpdate();

            this.numericUpDownDatasetXCorrection.Minimum = int.MinValue;
            this.numericUpDownDatasetXCorrection.Maximum = int.MaxValue;

            this.numericUpDownDatasetYCorrection.Minimum = int.MinValue;
            this.numericUpDownDatasetYCorrection.Maximum = int.MaxValue;

        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.business, "BorderPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownBorderPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "BorderPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownBorderPositionY.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "BorderScaling");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownBorderScaling.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "BorderStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxBorderStyle.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "ItemInsertPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownItemInsertPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "ItemInsertPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownItemInsertPositionY.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TaskCounterPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTaskCounterPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TaskCounterPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTaskCounterPositionY.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TaskCounterSize");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTaskCounterSize.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "Filename");
            bind.Format += (s, e) => { e.Value = string.IsNullOrEmpty((string)e.Value) ? "no file loaded" : (string)e.Value; };
            this.labelFilename.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "SampleIncluded");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxDataSampleIncluded.DataBindings.Add(bind);

            this.labelGameClass.Text = this.business.ClassInfo;

            this.fillDataList();

            this.selectDataset(this.business.SelectedDatasetIndex);
        }

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);

            this.business.PropertyChanged -= this.business_PropertyChanged;

            this.numericUpDownBorderPositionX.DataBindings.Clear();
            this.numericUpDownBorderPositionY.DataBindings.Clear();
            this.comboBoxBorderStyle.DataBindings.Clear();

            this.numericUpDownItemInsertPositionX.DataBindings.Clear();
            this.numericUpDownItemInsertPositionY.DataBindings.Clear();

            this.numericUpDownTaskCounterPositionX.DataBindings.Clear();
            this.numericUpDownTaskCounterPositionY.DataBindings.Clear();
            this.numericUpDownTaskCounterSize.DataBindings.Clear();

            this.labelFilename.DataBindings.Clear();
            this.checkBoxDataSampleIncluded.DataBindings.Clear();
        }

        private void fillDataList() {
            this.listBoxDataList.BeginUpdate();
            this.listBoxDataList.Items.Clear();
            int id = 1;
            if (this.business.SampleIncluded) id = 0;
            foreach (string item in this.business.NameList) {
                this.listBoxDataList.Items.Add(string.Format("{0}: {1}", id.ToString("00"), item));
                id++;
            }
            this.listBoxDataList.EndUpdate();

            this.listBoxDataList.Enabled = this.listBoxDataList.Items.Count > 0;
            Helper.setControlBackColor(this.listBoxDataList);

            this.buttonDataRemoveAllSets.Enabled = this.listBoxDataList.Items.Count > 0;
            Helper.setControlBackColor(this.buttonDataRemoveAllSets);

            this.selectDataset(this.selectedDatasetIndex);
        }

        private void selectDataset(
            int index) {
            if (index < 0) index = 0;
            if (index >= this.business.DatasetsCount) index = this.business.DatasetsCount - 1;
            DatasetContent selectedDataset = this.business.GetDataset(index);
            this.selectedDatasetIndex = this.business.GetDatasetIndex(selectedDataset);

            if (this.selectedDataset != selectedDataset) {
                //Dispose...
                if (this.selectedDataset is DatasetContent) {
                    this.selectedDataset.PropertyChanged -= this.selectedDataset_PropertyChanged;
                }
                this.selectedDataset = selectedDataset;
                //Pose...
                if (this.selectedDataset is DatasetContent) {
                    this.selectedDataset.PropertyChanged += this.selectedDataset_PropertyChanged;
                }
                this.selectedDatasetItemIndex = 0;
            }

            if (this.selectedDataset is DatasetContent) {
                this.groupBoxDataset.Enabled = true;
                this.textBoxDataSetName.Text = this.selectedDataset.Name;
                this.textBoxDataSetHostText.Text = this.selectedDataset.HostText;
                this.pictureBoxDatasetPicture.Image = this.selectedDataset.Picture;
                this.comboBoxDatasetHostStyle.Text = this.selectedDataset.ItemHostStyle.ToString();
                this.comboBoxDatasetInsertStyle.Text = this.selectedDataset.ItemInsertStyle.ToString();
                this.numericUpDownDatasetXCorrection.Value = this.selectedDataset.XCorrection;
                this.numericUpDownDatasetYCorrection.Value = this.selectedDataset.YCorrection;
                this.textBoxDatasetCredits.Text = this.selectedDataset.Credits;
                this.buttonDataRemoveSet.Enabled = true;
                if (this.listBoxDataList.Items.Count > this.selectedDatasetIndex) this.listBoxDataList.SelectedIndex = this.selectedDatasetIndex;
            }
            else {
                this.groupBoxDataset.Enabled = false;
                this.textBoxDataSetName.Text = string.Empty;
                this.textBoxDataSetHostText.Text = string.Empty;
                this.pictureBoxDatasetPicture.Image = null;
                this.numericUpDownDatasetYCorrection.Value = 0;
                this.textBoxDatasetCredits.Text = string.Empty;
                this.buttonDataRemoveSet.Enabled = false;
            }
            Helper.setControlBackColor(this.buttonDataRemoveSet);

            this.adjustDataMoveSet();

            this.fillListBoxDatasetItems();

            if (this.previewSource == Preview.Sources.Fullscreen) this.setItemPreview();
            else if (this.previewSource == Preview.Sources.Stage) this.setStagePreview();
        }

        private void adjustDataMoveSet() {
            this.buttonDataMoveSetUp.Enabled = this.listBoxDataList.SelectedIndex > 0;
            Helper.setControlBackColor(this.buttonDataMoveSetUp);

            this.buttonDataMoveSetDown.Enabled = this.listBoxDataList.SelectedIndex >= 0 && this.listBoxDataList.SelectedIndex < this.listBoxDataList.Items.Count - 1;
            Helper.setControlBackColor(this.buttonDataMoveSetDown);
        }

        private void fillListBoxDatasetItems() {
            this.listBoxDatasetItems.BeginUpdate();
            this.listBoxDatasetItems.Items.Clear();
            if (this.selectedDataset is DatasetContent) this.listBoxDatasetItems.Items.AddRange(this.selectedDataset.ItemList);
            this.listBoxDatasetItems.Enabled = this.listBoxDatasetItems.Items.Count > 0;
            Helper.setControlBackColor(this.listBoxDatasetItems);
            this.listBoxDatasetItems.EndUpdate();

            this.selectDatasetItem(this.selectedDatasetItemIndex);
        }

        private void selectDatasetItem(
            int index) {
            if (index < 0) index = 0;
            DatasetItem selectedDatasetItem = null;
            if (this.selectedDataset is DatasetContent) {
                if (index >= this.selectedDataset.ItemsCount) index = this.selectedDataset.ItemsCount - 1;
                selectedDatasetItem = this.selectedDataset.GetItem(index);
                this.selectedDatasetItemIndex = index;
            }

            if (this.selectedDatasetItem != selectedDatasetItem) {
                //Dispose...
                if (this.selectedDatasetItem is DatasetItem) {
                    this.selectedDatasetItem.PropertyChanged -= this.selectedDatasetItem_PropertyChanged;
                }
                this.selectedDatasetItem = selectedDatasetItem;
                //Pose...
                if (this.selectedDatasetItem is DatasetItem) {
                    this.selectedDatasetItem.PropertyChanged += this.selectedDatasetItem_PropertyChanged;
                }
            }

            if (this.selectedDatasetItem is DatasetItem) {
                this.groupBoxDataItem.Enabled = true;
                this.textBoxDatasetItemText.Text = this.selectedDatasetItem.Text;
                this.textBoxDatasetItemHostText.Text = this.selectedDatasetItem.HostText;
                this.buttonDataRemoveItem.Enabled = true;
                if (this.listBoxDatasetItems.Items.Count > this.selectedDatasetItemIndex) this.listBoxDatasetItems.SelectedIndex = this.selectedDatasetItemIndex;
            }
            else {
                this.groupBoxDataItem.Enabled = false;
                this.textBoxDatasetItemText.Text = string.Empty;
                this.textBoxDatasetItemHostText.Text = string.Empty;
                this.buttonDataRemoveItem.Enabled = false;
            }
            Helper.setControlBackColor(this.buttonDataRemoveItem);

        }

        public override void SetPreviewPipe(
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            bool selected = ((Preview)this.previewScene) is VRemote4.HandlerSi.Scene;
            base.SetPreviewPipe(previewPipe);
            if (selected) this.Select();
        }

        public override void Select() {
            if (this.previewPipe is VRemote4.HandlerSi.Client.Pipe.Business &&
                this.previewPipe.Resolution.HasValue &&
                this.previewPipe.ShareHandle.HasValue) {
                base.select(new Preview(WindowsFormsSynchronizationContext.Current, this.previewPipe));
            }
        }

        protected void setPreviewSource(
            Preview.Sources source) {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                previewScene.SetSource(source);
                this.setPreviewData();
            }
        }

        private void setPreviewData() {
            switch (this.previewSource) {
                case Preview.Sources.Insert:
                    this.setInsertPreview();
                    this.radioButtonSourceInsert.Checked = true;
                    break;
                case Preview.Sources.Fullscreen:
                    this.setFullscreenPreview();
                    this.radioButtonSourceFullscreen.Checked = true;
                    break;
                case Preview.Sources.Stage:
                    this.setStagePreview();
                    this.radioButtonSourceStage.Checked = true;
                    break;
            }
        }

        protected void setInsertPreview() {
            if (this.previewSceneIsAvailable) {
                this.setScorePreview();
                this.setTaskCounterPreview();
            }
        }

        protected override void setScorePreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetScore(previewScene.Insert.Score, 2, 3);
                switch (this.previewSource) {
                    case Preview.Sources.Insert:
                        previewScene.Insert.Score.SetIn();
                        previewScene.Insert.Border.SetOut();
                        break;
                    case Preview.Sources.Fullscreen:
                        previewScene.Insert.Border.SetIn();
                        previewScene.Insert.Score.SetOut();
                        break;
                    case Preview.Sources.Stage:
                        previewScene.Insert.Border.SetOut();
                        previewScene.Insert.Score.SetOut();
                        break;
                }
            }
        }

        protected void setFullscreenPreview() {
            if (this.previewSceneIsAvailable) {
                this.setBorderPreview();
                this.setItemPreview();
                this.setTaskCounterPreview();
            }
        }
        protected virtual void setBorderPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetBorder(previewScene.Insert.Border, 2, 3);

                if (this.previewSource == Preview.Sources.Fullscreen) {
                    previewScene.Insert.Score.SetOut();
                    previewScene.Insert.Border.SetIn();
                }
            }
        }
        protected void setItemPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.selectedDataset is DatasetContent) {
                    previewScene.Fullscreen.SetPictureFilename(this.selectedDataset.PictureFilename);
                    this.business.Vfullscreen_SetItemList(previewScene.Fullscreen.InsertList, this.selectedDataset, true);
                    previewScene.Fullscreen.SetIn();
                    previewScene.Fullscreen.InsertList.SetIn();
                }
                else previewScene.Fullscreen.SetOut();
            }
        }
        protected void setTaskCounterPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.previewSource == Preview.Sources.Fullscreen) {
                    this.business.Vinsert_SetTaskCounter(previewScene.Insert.TaskCounter, 6);
                    previewScene.Insert.TaskCounter.SetIn();
                }
                else previewScene.Insert.TaskCounter.SetOut();
            }
        }

        protected void setStagePreview() {
            if (this.previewSceneIsAvailable) {
                this.setScorePreview();
                Preview previewScene = this.previewScene as Preview;
                if (this.selectedDataset is DatasetContent) {
                    this.business.Vhost_SetContent(previewScene.Stage, this.selectedDataset);
                    previewScene.Stage.SetIn();
                }
                else previewScene.Stage.SetOut();
            }
        }

        #endregion


        #region Events.Incoming

        void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "NameList") this.fillDataList();
                else if (e.PropertyName == "SampleIncluded") this.fillDataList();
            }
        }

        void selectedDataset_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.selectedDataset_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "Name") this.textBoxDataSetName.Text = this.selectedDataset.Name;
                else if (e.PropertyName == "HostText") this.textBoxDataSetHostText.Text = this.selectedDataset.HostText;
                else if (e.PropertyName == "PictureFilename") this.pictureBoxDatasetPicture.Image = this.selectedDataset.Picture;
                else if (e.PropertyName == "ItemHostStyle") this.comboBoxDatasetHostStyle.Text = this.selectedDataset.ItemHostStyle.ToString();
                else if (e.PropertyName == "ItemInsertStyle") this.comboBoxDatasetInsertStyle.Text = this.selectedDataset.ItemInsertStyle.ToString();
                else if (e.PropertyName == "XCorrection") this.numericUpDownDatasetXCorrection.Value = this.selectedDataset.XCorrection;
                else if (e.PropertyName == "YCorrection") this.numericUpDownDatasetYCorrection.Value = this.selectedDataset.YCorrection;
                else if (e.PropertyName == "Credits") this.textBoxDatasetCredits.Text = this.selectedDataset.Credits;
                else if (e.PropertyName == "NameList") this.fillDataList();
                else if (e.PropertyName == "ItemList") this.fillListBoxDatasetItems();
                if (this.previewSource == Preview.Sources.Fullscreen) this.setItemPreview();
                else if (this.previewSource == Preview.Sources.Stage) this.setStagePreview();
            }
        }

        void selectedDatasetItem_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.selectedDatasetItem_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "Text") this.textBoxDatasetItemText.Text = this.selectedDatasetItem.Text;
                else if (e.PropertyName == "HostText") this.textBoxDatasetItemHostText.Text = this.selectedDatasetItem.HostText;
            }
        }

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setPreviewSource(this.previewSource);
        }


        #endregion

        #region Events.Controls

        private void numericUpDownBorderPositionX_ValueChanged(object sender, EventArgs e) { 
            this.business.BorderPositionX = (int)this.numericUpDownBorderPositionX.Value;
            if (this.previewSource == Preview.Sources.Fullscreen) this.setBorderPreview();
        }
        private void numericUpDownBorderPositionY_ValueChanged(object sender, EventArgs e) { 
            this.business.BorderPositionY = (int)this.numericUpDownBorderPositionY.Value;
            if (this.previewSource == Preview.Sources.Fullscreen) this.setBorderPreview();
        }
        private void numericUpDownBorderScaling_ValueChanged(object sender, EventArgs e) { 
            this.business.BorderScaling = (int)this.numericUpDownBorderScaling.Value;
            if (this.previewSource == Preview.Sources.Fullscreen) this.setBorderPreview();
        }
        private void comboBoxBorderStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.Border.Styles style;
            if (Enum.TryParse(this.comboBoxBorderStyle.Text, out style)) this.business.BorderStyle = style;
            if (this.previewSource == Preview.Sources.Fullscreen) this.setBorderPreview();
        }

        protected virtual void numericUpDownItemInsertPositionX_ValueChanged(object sender, EventArgs e) { 
            this.business.ItemInsertPositionX = (int)this.numericUpDownItemInsertPositionX.Value; 
        }
        protected virtual void numericUpDownItemInsertPositionY_ValueChanged(object sender, EventArgs e) { 
            this.business.ItemInsertPositionY = (int)this.numericUpDownItemInsertPositionY.Value; 
        }

        private void numericUpDownTaskCounterPositionX_ValueChanged(object sender, EventArgs e) {
            this.business.TaskCounterPositionX = (int)this.numericUpDownTaskCounterPositionX.Value;
            this.setTaskCounterPreview();
        }
        private void numericUpDownTaskCounterPositionY_ValueChanged(object sender, EventArgs e) {
            this.business.TaskCounterPositionY = (int)this.numericUpDownTaskCounterPositionY.Value;
            this.setTaskCounterPreview();
        }
        private void numericUpDownTaskCounterSize_ValueChanged(object sender, EventArgs e) {
            this.business.TaskCounterSize = (int)this.numericUpDownTaskCounterSize.Value;
            this.setTaskCounterPreview();
        }

        private void radioButtonSourceInsert_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourceInsert.Checked) this.previewSource = Preview.Sources.Insert; }
        private void radioButtonSourceFullscreen_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourceFullscreen.Checked) this.previewSource = Preview.Sources.Fullscreen; }
        private void radioButtonSourceStage_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourceStage.Checked) this.previewSource = Preview.Sources.Stage; }

        private void buttonLoad_Click(object sender, EventArgs e) {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Load Data";
            dialog.InitialDirectory = ApplicationAttributes.ContentPath;
            if (File.Exists(this.business.Filename)) dialog.FileName = this.business.Filename;
            dialog.DefaultExt = "*.xml";
            dialog.Filter = "XML-File (*.xml)|*.xml|all files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.Multiselect = false;
            dialog.RestoreDirectory = true;
            switch (dialog.ShowDialog()) {
                case DialogResult.Cancel:
                    break;
                case DialogResult.OK:
                    this.business.Load(dialog.FileName);
                    break;
            }
            dialog = null;
        }
        private void buttonSave_Click(object sender, EventArgs e) {
            if (File.Exists(this.business.Filename)) this.business.Save();
            else buttonSaveAs_Click(sender, e);
        }
        private void buttonSaveAs_Click(object sender, EventArgs e) {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Save Data As...";
            dialog.InitialDirectory = ApplicationAttributes.ContentPath;
            if (File.Exists(this.business.Filename)) dialog.FileName = this.business.Filename;
            dialog.DefaultExt = "*.xml";
            dialog.Filter = "XML-File (*.xml)|*.xml|all files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;
            switch (dialog.ShowDialog()) {
                case DialogResult.Cancel:
                    break;
                case DialogResult.OK:
                    this.business.SaveAs(dialog.FileName);
                    break;
            }
            dialog = null;
        }

        private void listBoxDataList_SelectedIndexChanged(object sender, EventArgs e) { this.selectDataset(this.listBoxDataList.SelectedIndex); }
        private void buttonDataMoveSetUp_Click(object sender, EventArgs e) {
            if (this.business.TryMoveDatasetUp(this.selectedDatasetIndex)) this.selectDataset(this.selectedDatasetIndex - 1);
        }
        private void buttonDataMoveSetDown_Click(object sender, EventArgs e) { 
            if (this.business.TryMoveDatasetDown(this.selectedDatasetIndex)) this.selectDataset(this.selectedDatasetIndex + 1);
        }
        private void buttonDataResort_Click(object sender, EventArgs e) { this.business.ResortAllDatasets(); }

        private void checkBoxSampleIncluded_CheckedChanged(object sender, EventArgs e) { this.business.SampleIncluded = this.checkBoxDataSampleIncluded.Checked; }
        private void buttonDataAddNewSet_Click(object sender, EventArgs e) {
            int insertIndex = this.listBoxDataList.SelectedIndex + 1;
            this.business.AddDataset(new DatasetContent(), insertIndex);
            this.selectDataset(insertIndex);
        }
        private void buttonDataImportSets_Click(object sender, EventArgs e) {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Import Data";
            dialog.InitialDirectory = ApplicationAttributes.ContentPath;
            if (File.Exists(this.business.Filename)) dialog.FileName = this.business.Filename;
            dialog.DefaultExt = "*.txt";
            dialog.Filter = "Text-File (*.txt)|*.txt|all files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.Multiselect = false;
            dialog.RestoreDirectory = true;
            switch (dialog.ShowDialog()) {
                case DialogResult.Cancel:
                    break;
                case DialogResult.OK:
                    this.business.Import(dialog.FileName);
                    break;
            }
            dialog = null;
        }
        private void buttonDataRemoveSet_Click(object sender, EventArgs e) { 
            if (this.business.TryRemoveDataset(this.selectedDatasetIndex)) this.selectDataset(this.selectedDatasetIndex - 1);
        }
        private void buttonDataRemoveAllSets_Click(object sender, EventArgs e) { 
            this.business.RemoveAllDatasets();
            this.selectDataset(0);
        }
        private void textBoxDataSetName_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Name = this.textBoxDataSetName.Text; }
        private void textBoxDataSetHostText_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.HostText = this.textBoxDataSetHostText.Text; }
        private void pictureBoxDatasetPicture_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                string filename = Helper.selectImageFile("select picture", this.selectedDataset.PictureFilename);
                if (filename != null) this.selectedDataset.PictureFilename = filename;
            }
        }
        private void buttonDatasetRemovePicture_Click(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.PictureFilename = string.Empty; }
        private void numericUpDownDatasetXCorrection_ValueChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.XCorrection = (int)this.numericUpDownDatasetXCorrection.Value; }
        private void numericUpDownDatasetYCorrection_ValueChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.YCorrection = (int)this.numericUpDownDatasetYCorrection.Value; }
        private void comboBoxDatasetInsertStyle_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                InsertList.Styles style;
                if (Enum.TryParse(this.comboBoxDatasetInsertStyle.Text, out style)) this.selectedDataset.ItemInsertStyle = style;
            }
        }
        private void comboBoxDatasetHostStyle_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                Stage.HostStyles style;
                if (Enum.TryParse(this.comboBoxDatasetHostStyle.Text, out style)) this.selectedDataset.ItemHostStyle = style;
            }
        }
        private void textBoxDataSetCredits_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Credits = this.textBoxDatasetCredits.Text; }

        private void listBoxDatasetItems_SelectedIndexChanged(object sender, EventArgs e) { this.selectDatasetItem(this.listBoxDatasetItems.SelectedIndex); }
        private void buttonDataAddNewItem_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                this.selectedDataset.AddItem(new DatasetItem());
                this.selectDatasetItem(this.selectedDataset.ItemsCount - 1);
            }
        }
        private void buttonDataRemoveItem_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent
                && this.selectedDataset.TryRemoveItem(this.selectedDatasetItemIndex)) this.selectDatasetItem(this.selectedDatasetItemIndex);
        }
        private void textBoxDatasetItemText_TextChanged(object sender, EventArgs e) { if (this.selectedDatasetItem is DatasetItem) this.selectedDatasetItem.Text = this.textBoxDatasetItemText.Text; }
        private void textBoxDatasetItemHostText_TextChanged(object sender, EventArgs e) { if (this.selectedDatasetItem is DatasetItem) this.selectedDatasetItem.HostText = this.textBoxDatasetItemHostText.Text; }

        #endregion

    }
}
