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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.WindingQuiz;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.WindingQuiz {

    public partial class UserControlContent : _Base.BuzzerScore.UserControlContent {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;
        private int selectedDatasetIndex = -1;

        private DataItem selectedDataItem = null;
        private int selectedDataItemIndex = -1;

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

        private bool _showSolution = true;
        public bool showSolution {
            get { return this._showSolution; }
            set {
                if (this._showSolution != value) {
                    this._showSolution = value;
                    if (this.previewSource == Preview.Sources.Insert) this.setItemPreview();
                }
            }
        }

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownTaskCounterPositionX.Minimum = int.MinValue;
            this.numericUpDownTaskCounterPositionX.Maximum = int.MaxValue;

            this.numericUpDownTaskCounterPositionY.Minimum = int.MinValue;
            this.numericUpDownTaskCounterPositionY.Maximum = int.MaxValue;

            this.numericUpDownTaskCounterSize.Minimum = int.MinValue;
            this.numericUpDownTaskCounterSize.Maximum = int.MaxValue;

            this.numericUpDownItemInsertPositionX.Minimum = int.MinValue;
            this.numericUpDownItemInsertPositionX.Maximum = int.MaxValue;

            this.numericUpDownItemInsertPositionY.Minimum = int.MinValue;
            this.numericUpDownItemInsertPositionY.Maximum = int.MaxValue;

        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.business, "TaskCounterPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTaskCounterPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TaskCounterPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTaskCounterPositionY.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TaskCounterSize");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTaskCounterSize.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "ItemInsertPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownItemInsertPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "ItemInsertPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownItemInsertPositionY.DataBindings.Add(bind);

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

            this.numericUpDownTaskCounterPositionX.DataBindings.Clear();
            this.numericUpDownTaskCounterPositionY.DataBindings.Clear();
            this.numericUpDownTaskCounterSize.DataBindings.Clear();

            this.numericUpDownItemInsertPositionX.DataBindings.Clear();
            this.numericUpDownItemInsertPositionY.DataBindings.Clear();

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

            this.showSolution = true;

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
            }

            if (this.selectedDataset is DatasetContent) {
                this.groupBoxDataset.Enabled = true;
                this.textBoxDataSetName.Text = this.selectedDataset.Name;
                this.textBoxDataSetHostText.Text = this.selectedDataset.HostText;
                this.buttonDataRemoveSet.Enabled = true;
                if (this.listBoxDataList.Items.Count > this.selectedDatasetIndex) this.listBoxDataList.SelectedIndex = this.selectedDatasetIndex;
            }
            else {
                this.groupBoxDataset.Enabled = false;
                this.textBoxDataSetName.Text = string.Empty;
                this.textBoxDataSetHostText.Text = string.Empty;
                this.buttonDataRemoveSet.Enabled = false;
            }
            Helper.setControlBackColor(this.buttonDataRemoveSet);

            this.adjustDataMoveSet();

            this.fillListBoxDataItems();

            this.selectDataItem(0);
        }

        private void adjustDataMoveSet() {
            this.buttonDataMoveSetUp.Enabled = this.listBoxDataList.SelectedIndex > 0;
            Helper.setControlBackColor(this.buttonDataMoveSetUp);

            this.buttonDataMoveSetDown.Enabled = this.listBoxDataList.SelectedIndex >= 0 && this.listBoxDataList.SelectedIndex < this.listBoxDataList.Items.Count - 1;
            Helper.setControlBackColor(this.buttonDataMoveSetDown);
        }

        private void fillListBoxDataItems() {
            this.listBoxDataItems.BeginUpdate();
            this.listBoxDataItems.Items.Clear();
            if (this.selectedDataset is DatasetContent) this.listBoxDataItems.Items.AddRange(this.selectedDataset.ItemList);
            this.listBoxDataItems.Enabled = this.listBoxDataItems.Items.Count > 0;
            Helper.setControlBackColor(this.listBoxDataItems);
            this.listBoxDataItems.EndUpdate();
        }

        private void selectDataItem(
            int index) {
            if (index < 0) index = 0;
            DataItem selectedDataItem = null;
            if (this.selectedDataset is DatasetContent) {
                if (index >= this.selectedDataset.ItemsCount) index = this.selectedDataset.ItemsCount - 1;
                selectedDataItem = this.selectedDataset.GetItem(index);
                this.selectedDataItemIndex = index;
            }

            if (this.selectedDataItem != selectedDataItem) {
                //Dispose...
                if (this.selectedDataItem is DataItem) {
                    this.selectedDataItem.PropertyChanged -= this.selectedDataItem_PropertyChanged;
                }
                this.selectedDataItem = selectedDataItem;
                //Pose...
                if (this.selectedDataItem is DataItem) {
                    this.selectedDataItem.PropertyChanged += this.selectedDataItem_PropertyChanged;
                }
            }

            if (this.selectedDataItem is DataItem) {
                this.groupBoxDataItem.Enabled = true;
                this.textBoxDataItemText.Text = this.selectedDataItem.Text;
                this.textBoxDataItemHostText.Text = this.selectedDataItem.HostText;
                this.buttonDataRemoveItem.Enabled = true;
                if (this.listBoxDataItems.Items.Count > this.selectedDataItemIndex) this.listBoxDataItems.SelectedIndex = this.selectedDataItemIndex;
            }
            else {
                this.groupBoxDataItem.Enabled = false;
                this.textBoxDataItemText.Text = string.Empty;
                this.textBoxDataItemHostText.Text = string.Empty;
                this.buttonDataRemoveItem.Enabled = false;
            }
            Helper.setControlBackColor(this.buttonDataRemoveItem);

            this.adjustDataMoveItem();

            this.setPreviewData();
        }

        private void adjustDataMoveItem() {
            this.buttonDataMoveItemUp.Enabled = this.listBoxDataItems.SelectedIndex > 0;
            Helper.setControlBackColor(this.buttonDataMoveItemUp);

            this.buttonDataMoveItemDown.Enabled = this.listBoxDataItems.SelectedIndex >= 0 && this.listBoxDataItems.SelectedIndex < this.listBoxDataItems.Items.Count - 1;
            Helper.setControlBackColor(this.buttonDataMoveItemDown);
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
                case Preview.Sources.Host:
                    this.setHostPreview();
                    this.radioButtonSourceHost.Checked = true;
                    break;
            }
        }

        protected void setInsertPreview() {
            if (this.previewSceneIsAvailable) {
                this.setScorePreview();
                this.setTimeoutPreview();
                this.setTaskCounterPreview();
                this.setItemPreview();
            }
        }

        protected override void setScorePreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetScore(previewScene.Insert.Score, 2, 3);
                previewScene.Insert.Score.SetIn();
            }
        }

        protected override void setTimeoutPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetTimeout(previewScene.Insert.Timeout);
                previewScene.Insert.Timeout.SetLeftRightToGreen(this.business.TimeoutDuration);
            }
        }

        protected void setTaskCounterPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                SingleDot[] taskCounterPenaltyDots = new SingleDot[] {
                    new SingleDot(VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Blue),
                    new SingleDot(VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Red),
                    new SingleDot(VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Fail),
                    new SingleDot(VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Blue),
                    new SingleDot(VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Red),
                    new SingleDot(VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Fail),
                    new SingleDot(VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Off),
                    new SingleDot(VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Off),
                    new SingleDot(VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Off),
                    new SingleDot(VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Off),
                    new SingleDot(VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Off),
                    new SingleDot(VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Off),
                    new SingleDot(VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Off) };
                this.business.Vinsert_SetTaskCounter(previewScene.Insert.TaskCounter, taskCounterPenaltyDots, 6);
                previewScene.Insert.TaskCounter.SetIn();
            }
        }

        protected void setItemPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetItem(previewScene.Insert, this.selectedDataset, this.selectedDataItem);
                previewScene.Insert.SetIn();
                if (this.showSolution) previewScene.Insert.SetSolutionIn();
                else previewScene.Insert.SetSolutionOut();
            }
        }

        protected void setHostPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vhost_SetItem(previewScene.Host, 6, this.selectedDataset, this.selectedDataItem);
                previewScene.Host.PassNewItemValues();
                previewScene.Host.SetIn();
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
                if (e.PropertyName == "Name") {
                    this.textBoxDataSetName.Text = this.selectedDataset.Name;
                    if (this.previewSource == Preview.Sources.Insert) {
                        this.showSolution = true;
                        this.setItemPreview();
                    }
                }
                else if (e.PropertyName == "HostText") {
                    this.textBoxDataSetHostText.Text = this.selectedDataset.HostText;
                    if (this.previewSource == Preview.Sources.Host) this.setHostPreview();
                }
                else if (e.PropertyName == "NameList") this.fillDataList();
                else if (e.PropertyName == "ItemList") this.fillListBoxDataItems();
            }
        }

        void selectedDataItem_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.selectedDataItem_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "Text") {
                    this.textBoxDataItemText.Text = this.selectedDataItem.Text;
                    if (this.previewSource == Preview.Sources.Insert) {
                        this.showSolution = false;
                        this.setItemPreview();
                    }
                }
                else if (e.PropertyName == "HostText") {
                    this.textBoxDataItemHostText.Text = this.selectedDataItem.HostText;
                    if (this.previewSource == Preview.Sources.Host) this.setHostPreview();
                }
            }
        }

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setPreviewSource(this.previewSource);
        }

        #endregion

        #region Events.Controls

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

        protected virtual void numericUpDownItemInsertPositionX_ValueChanged(object sender, EventArgs e) {
            this.business.ItemInsertPositionX = (int)this.numericUpDownItemInsertPositionX.Value;
            this.setItemPreview();
        }
        protected virtual void numericUpDownItemInsertPositionY_ValueChanged(object sender, EventArgs e) {
            this.business.ItemInsertPositionY = (int)this.numericUpDownItemInsertPositionY.Value;
            this.setItemPreview();
        }

        private void radioButtonSourceInsert_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourceInsert.Checked) this.previewSource = Preview.Sources.Insert; }
        private void radioButtonSourceHost_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourceHost.Checked) this.previewSource = Preview.Sources.Host; }

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

        private void listBoxDataList_Click(object sender, EventArgs e) {
            this.showSolution = true;
            if (this.previewSource == Preview.Sources.Insert) this.setItemPreview();
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

        private void listBoxDataItems_Click(object sender, EventArgs e) {
            this.showSolution = false;
            if (this.previewSource == Preview.Sources.Insert) this.setItemPreview();
        }
        private void listBoxDataItems_SelectedIndexChanged(object sender, EventArgs e) { this.selectDataItem(this.listBoxDataItems.SelectedIndex); }
        private void buttonDataAddNewItem_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                this.selectedDataset.AddItem(new DataItem());
                this.selectDataItem(this.selectedDataset.ItemsCount - 1);
            }
        }
        private void buttonDataMoveItemUp_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent
                && this.selectedDataset.TryMoveItemUp(this.selectedDataItemIndex)) this.selectDataItem(this.selectedDataItemIndex - 1);
        }
        private void buttonDataMoveItemDown_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent
                && this.selectedDataset.TryMoveItemDown(this.selectedDataItemIndex)) this.selectDataItem(this.selectedDataItemIndex + 1);
        }
        private void buttonDataRemoveItem_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent
                && this.selectedDataset.TryRemoveItem(this.selectedDataItemIndex)) this.selectDataItem(this.selectedDataItemIndex);
        }
        private void textBoxDataItemText_TextChanged(object sender, EventArgs e) { if (this.selectedDataItem is DataItem) this.selectedDataItem.Text = this.textBoxDataItemText.Text; }
        private void textBoxDataItemHostText_TextChanged(object sender, EventArgs e) { if (this.selectedDataItem is DataItem) this.selectedDataItem.HostText = this.textBoxDataItemHostText.Text; }

        #endregion

    }
}
