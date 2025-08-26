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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.Correlation;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.Correlation {

    public partial class UserControlContent : _Base.TimerScore.UserControlContent {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;

        private int selectedDatasetIndex = -1;

        private DatasetContentItem selectedDatasetItem = null;

        private int selectedDatasetItemIndex = -1;

        private List<ListItemDataset> itemList = new List<ListItemDataset>();

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

        private bool _showContentInsert = false;
        public bool showContentInsert {
            get { return this._showContentInsert; }
            set {
                if (this._showContentInsert != value) {
                    this._showContentInsert = value;
                    this.setPreviewData();
                }
            }
        }

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

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
            }

            if (this.selectedDataset is DatasetContent) {
                this.groupBoxDataset.Enabled = true;
                this.textBoxDataSetName.Text = this.selectedDataset.Name;
                this.buttonDataRemoveSet.Enabled = true;
                if (this.listBoxDataList.Items.Count > this.selectedDatasetIndex) this.listBoxDataList.SelectedIndex = this.selectedDatasetIndex;
            }
            else {
                this.groupBoxDataset.Enabled = false;
                this.textBoxDataSetName.Text = string.Empty;
                this.buttonDataRemoveSet.Enabled = false;
            }
            Helper.setControlBackColor(this.buttonDataRemoveSet);

            this.adjustDataMoveSet();

            this.fillListBoxDatasetItems();
            this.selectDatasetItem(0);

            this.fillListBoxDatasetChoices();
            if (this.listBoxDatasetChoices.Enabled) this.listBoxDatasetChoices.SelectedIndex = 0;

            this.fillItemList();

            this.setPreviewData();
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
            if (this.selectedDataset is DatasetContent) {
                int id = 1;
                foreach (string item in this.selectedDataset.NameList) {
                    this.listBoxDatasetItems.Items.Add(string.Format("{0}: {1}", id.ToString("00"), item));
                    id++;
                }
            }
            this.listBoxDatasetItems.Enabled = this.listBoxDatasetItems.Items.Count > 0;
            Helper.setControlBackColor(this.listBoxDatasetItems);
            this.listBoxDatasetItems.EndUpdate();
        }

        private void selectDatasetItem(
            int index) {
            DatasetContentItem item = null;
            if (this.selectedDataset is DatasetContent) {
                if (index < 0) index = 0;
                if (index >= this.selectedDataset.ItemList.Length) index = this.selectedDataset.ItemList.Length - 1;
                item = this.selectedDataset.GetItem(index);
                this.selectedDatasetItemIndex = index;
            }
            else {
                this.selectedDatasetItemIndex = -1;
            }

            if (this.selectedDatasetItem != item) {
                //Dispose...
                if (this.selectedDatasetItem is DatasetContentItem) {
                    this.selectedDatasetItem.PropertyChanged -= this.selectedDatasetItem_PropertyChanged;
                }
                this.selectedDatasetItem = item;
                //Pose...
                if (this.selectedDatasetItem is DatasetContentItem) {
                    this.selectedDatasetItem.PropertyChanged += this.selectedDatasetItem_PropertyChanged;
                }
            }

            if (this.selectedDatasetItem is DatasetContentItem) {
                this.textBoxDatasetItemIssue.Text = this.selectedDatasetItem.Issue;
                this.textBoxDatasetItemSolution.Text = this.selectedDatasetItem.Solution;
                this.textBoxDatasetItemSolutionHostText.Text = this.selectedDatasetItem.SolutionHostText;
                if (this.listBoxDatasetItems.Items.Count > this.selectedDatasetItemIndex) this.listBoxDatasetItems.SelectedIndex = this.selectedDatasetItemIndex;
                this.buttonDataMoveItemUp.Enabled = this.listBoxDatasetItems.SelectedIndex > 0;
                this.buttonDataMoveItemDown.Enabled = this.listBoxDatasetItems.SelectedIndex < this.listBoxDatasetItems.Items.Count - 2;
            }
            else {
                this.textBoxDatasetItemIssue.Text = string.Empty;
                this.textBoxDatasetItemSolution.Text = string.Empty;
                this.textBoxDatasetItemSolutionHostText.Text = string.Empty;
                this.buttonDataMoveItemUp.Enabled = false;
                this.buttonDataMoveItemDown.Enabled = false;
            }

            Helper.setControlBackColor(this.buttonDataMoveItemUp);
            Helper.setControlBackColor(this.buttonDataMoveItemDown);
        }

        private void fillListBoxDatasetChoices() {
            int index = this.listBoxDatasetChoices.SelectedIndex;
            this.listBoxDatasetChoices.BeginUpdate();
            this.listBoxDatasetChoices.Items.Clear();
            if (this.selectedDataset is DatasetContent) this.listBoxDatasetChoices.Items.AddRange(this.selectedDataset.ChoiceList);
            this.listBoxDatasetChoices.Enabled = this.listBoxDatasetChoices.Items.Count > 0;
            Helper.setControlBackColor(this.listBoxDatasetChoices);
            this.listBoxDatasetChoices.EndUpdate();
            if (this.listBoxDatasetChoices.Enabled) {
                if (index < 0) index = 0;
                if (index > this.listBoxDatasetChoices.Items.Count) index = this.listBoxDatasetChoices.Items.Count - 1;
                this.listBoxDatasetChoices.SelectedIndex = index;
            }
            this.adjustDataMoveChoice();
        }

        private void adjustDataMoveChoice() {
            this.buttonDataMoveChoiceUp.Enabled = this.listBoxDatasetChoices.SelectedIndex > 0;
            Helper.setControlBackColor(this.buttonDataMoveChoiceUp);

            this.buttonDataMoveChoiceDown.Enabled = this.listBoxDatasetChoices.SelectedIndex >= 0 && this.listBoxDatasetChoices.SelectedIndex < this.listBoxDatasetChoices.Items.Count - 1;
            Helper.setControlBackColor(this.buttonDataMoveChoiceDown);
        }

        private void fillItemList() {
            this.itemList.Clear();
            if (this.selectedDataset is DatasetContent) {
                int counter = 0;
                foreach (DatasetContentItem item in this.selectedDataset.ItemList) {
                    counter++;
                    if (item is DatasetContentItem &&
                        counter < DatasetContent.ItemsCount) {
                        ListItemDataset newItem = new ListItemDataset(this.itemList.Count, item.Issue, item.Solution, item.SolutionHostText, item.ChoiceIndex);
                        this.itemList.Add(newItem);
                    }
                }
            }
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
                    if (this.showContentInsert) this.radioButtonSourceContent.Checked = true;
                    else this.radioButtonSourceScore.Checked = true;
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
                this.setTimerPreview();
                this.setInsertContentPreview();
            }
        }
        protected override void setScorePreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.previewSource == Preview.Sources.Insert &&
                    !this.showContentInsert) {
                    this.business.Vinsert_SetScore(previewScene.Insert.Score, 2, 3);
                    previewScene.Insert.Score.SetIn();
                }
                else previewScene.Insert.Score.SetOut();
            }
        }
        protected override void setTimerPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.previewSource == Preview.Sources.Insert &&
                    this.showContentInsert) {
                    this.business.Vinsert_SetTimer(previewScene.Insert.Timer);
                    if (this.previewSource == Preview.Sources.Insert) previewScene.Insert.Timer.SetIn();
                }
                else previewScene.Insert.Timer.SetOut();
            }
        }
        protected virtual void setInsertContentPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.previewSource == Preview.Sources.Insert &&
                    this.showContentInsert &&
                    this.selectedDataset is DatasetContent) {
                    this.business.Vinsert_SetContent(previewScene.Insert, this.itemList.ToArray(), this.selectedDataset, false);
                    previewScene.Insert.SetIn();
                    previewScene.Insert.SetTrue();
                }
                else previewScene.Insert.SetOut();
            }
        }

        protected void setStagePreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vstage_SetContent(previewScene.Stage, this.itemList.ToArray(), this.selectedDataset, true, true, true);
                previewScene.Stage.SetIn();
            }
        }

        #endregion


        #region Events.Incoming

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setPreviewSource(this.previewSource);
        }

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
                else if (e.PropertyName == "NameList") {
                    this.fillListBoxDatasetItems();
                    this.selectDatasetItem(this.selectedDatasetItemIndex);
                }
                else if (e.PropertyName == "ChoiceList") {
                    this.fillListBoxDatasetChoices();
                }
                this.fillItemList();
                this.setPreviewData();
            }
        }

        void selectedDatasetItem_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.selectedDatasetItem_PropertyChanged(sender, e)));
            else {
                //if (e.PropertyName == "Issue") {
                //    this.fillItemList();
                //    this.setPreviewData();
                //}
                //else if (e.PropertyName == "Solution") {
                //    this.fillItemList();
                //    this.setPreviewData();
                //}
                //else if (e.PropertyName == "SolutionHostText") {
                //    this.fillItemList();
                //    this.setPreviewData();
                //}
            }
        }

        #endregion

        #region Events.Controls

        private void radioButtonSourceScore_CheckedChanged(object sender, EventArgs e) {
            if (this.radioButtonSourceScore.Checked) {
                this.previewSource = Preview.Sources.Insert;
                this.showContentInsert = false;
            }
        }
        private void radioButtonSourceContent_CheckedChanged(object sender, EventArgs e) {
            if (this.radioButtonSourceContent.Checked) {
                this.previewSource = Preview.Sources.Insert;
                this.showContentInsert = true;
            }
        }
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

        private void listBoxDatasetItems_SelectedIndexChanged(object sender, EventArgs e) { this.selectDatasetItem(this.listBoxDatasetItems.SelectedIndex); }
        private void buttonDataMoveItemUp_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent &&
                this.selectedDataset.TryMoveItemUp(this.selectedDatasetItemIndex)) this.selectDatasetItem(this.selectedDatasetItemIndex - 1);
        }
        private void buttonDataMoveItemDown_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent &&
                this.selectedDataset.TryMoveItemDown(this.selectedDatasetItemIndex)) this.selectDatasetItem(this.selectedDatasetItemIndex + 1);
        }
        private void textBoxDatasetItemIssue_TextChanged(object sender, EventArgs e) { if (this.selectedDatasetItem is DatasetContentItem) this.selectedDatasetItem.Issue = this.textBoxDatasetItemIssue.Text; }
        private void textBoxDatasetItemSolution_TextChanged(object sender, EventArgs e) { if (this.selectedDatasetItem is DatasetContentItem) this.selectedDatasetItem.Solution = this.textBoxDatasetItemSolution.Text; }
        private void textBoxDatasetItemSolutionHostText_TextChanged(object sender, EventArgs e) { if (this.selectedDatasetItem is DatasetContentItem) this.selectedDatasetItem.SolutionHostText = this.textBoxDatasetItemSolutionHostText.Text; }

        private void listBoxDatasetChoices_SelectedIndexChanged(object sender, EventArgs e) { this.adjustDataMoveChoice(); }
        private void buttonDataMoveChoiceUp_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent &&
                this.selectedDataset.TryMoveChoiceUp(this.listBoxDatasetChoices.SelectedIndex)) {
                this.listBoxDatasetChoices.SelectedIndex--;
            }
        }
        private void buttonDataMoveChoiceDown_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent &&
                this.selectedDataset.TryMoveChoiceDown(this.listBoxDatasetChoices.SelectedIndex)) {
                this.listBoxDatasetChoices.SelectedIndex++;
            }
        }
        private void buttonDataChoiceSortAscending_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.SortChoicesAscending(this.checkBoxDataChoiceSortAsName.Checked);
        }
        private void buttonDataChoiceSortDescending_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.SortChoicesDescending(this.checkBoxDataChoiceSortAsName.Checked);
        }

        #endregion
    }
}
