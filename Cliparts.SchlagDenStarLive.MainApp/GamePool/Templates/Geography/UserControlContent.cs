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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.Geography;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.Geography {

    public partial class UserControlContent : _Base.Score.UserControlContent {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;

        private int selectedDatasetIndex = -1;

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

            this.numericUpDownTextInsertPositionX.Minimum = int.MinValue;
            this.numericUpDownTextInsertPositionX.Maximum = int.MaxValue;

            this.numericUpDownTextInsertPositionY.Minimum = int.MinValue;
            this.numericUpDownTextInsertPositionY.Maximum = int.MaxValue;

            this.comboBoxTextInsertStyle.BeginUpdate();
            this.comboBoxTextInsertStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.TextInsert.Styles)));
            this.comboBoxTextInsertStyle.EndUpdate();

            this.comboBoxMapLayout.BeginUpdate();
            this.comboBoxMapLayout.Items.AddRange(Enum.GetNames(typeof(Fullscreen.MapLayoutElements)));
            this.comboBoxMapLayout.EndUpdate();

        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.business, "TextInsertPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTextInsertPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TextInsertPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTextInsertPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TextInsertStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxTextInsertStyle.DataBindings.Add(bind);

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

            this.numericUpDownTextInsertPositionX.DataBindings.Clear();
            this.numericUpDownTextInsertPositionY.DataBindings.Clear();
            this.comboBoxTextInsertStyle.DataBindings.Clear();

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

            this.textBoxDatasetName.DataBindings.Clear();
            this.comboBoxMapLayout.DataBindings.Clear();
            this.textBoxDatasetLongitude.DataBindings.Clear();
            this.textBoxDatasetLatitude.DataBindings.Clear();
            this.textBoxDatasetInsertText.DataBindings.Clear();
            this.textBoxDatasetFullText.DataBindings.Clear();
            this.textBoxDatasetHostText.DataBindings.Clear();
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
                this.textBoxDatasetName.Text = this.selectedDataset.Name;
                this.comboBoxMapLayout.Text = this.selectedDataset.MapLayout.ToString();
                this.textBoxDatasetLongitude.Text = this.selectedDataset.Coordinates.Longitude.Text;
                this.textBoxDatasetLatitude.Text = this.selectedDataset.Coordinates.Latitude.Text;
                this.textBoxDatasetInsertText.Text = this.selectedDataset.InsertText;
                this.textBoxDatasetFullText.Text = this.selectedDataset.FullText;
                this.textBoxDatasetHostText.Text = this.selectedDataset.HostText;
                this.buttonDatasetSetLatitude.Enabled = true;
                this.buttonDatasetSetLongitude.Enabled = true;
                this.buttonDataRemoveSet.Enabled = true;
                if (this.listBoxDataList.Items.Count > this.selectedDatasetIndex) this.listBoxDataList.SelectedIndex = this.selectedDatasetIndex;
            }
            else {
                this.groupBoxDataset.Enabled = false;
                this.textBoxDatasetName.Text = string.Empty;
                this.comboBoxMapLayout.Text = string.Empty;
                this.textBoxDatasetLongitude.Text = string.Empty;
                this.textBoxDatasetLatitude.Text = string.Empty;
                this.textBoxDatasetInsertText.Text = string.Empty;
                this.textBoxDatasetFullText.Text = string.Empty;
                this.textBoxDatasetHostText.Text = string.Empty;
                this.buttonDatasetSetLatitude.Enabled = false;
                this.buttonDatasetSetLongitude.Enabled = false;
                this.buttonDataRemoveSet.Enabled = true;
                this.buttonDataRemoveSet.Enabled = false;
            }
            Helper.setControlBackColor(this.buttonDataRemoveSet);

            this.adjustDataMoveSet();

            this.setPreviewData();
        }

        private void adjustDataMoveSet() {
            this.buttonDataMoveSetUp.Enabled = this.listBoxDataList.SelectedIndex > 0;
            Helper.setControlBackColor(this.buttonDataMoveSetUp);

            this.buttonDataMoveSetDown.Enabled = this.listBoxDataList.SelectedIndex >= 0 && this.listBoxDataList.SelectedIndex < this.listBoxDataList.Items.Count - 1;
            Helper.setControlBackColor(this.buttonDataMoveSetDown);
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
                    this.radioButtonSourceHost.Checked = true;
                    break;
            }
        }

        protected void setInsertPreview() {
            if (this.previewSceneIsAvailable) {
                this.setScorePreview();
                this.setInsertTextPreview();
            }
        }

        protected override void setScorePreview() {
            if (this.previewSceneIsAvailable &&
                this.previewSource == Preview.Sources.Insert) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetScore(previewScene.Insert.Score, 2, 3);
                previewScene.Insert.Score.SetIn();
            }
        }

        protected void setInsertTextPreview() {
            if (this.previewSceneIsAvailable &&
                this.previewSource == Preview.Sources.Insert) {
                Preview previewScene = this.previewScene as Preview;
                if (this.selectedDataset is DatasetContent) {
                    this.business.Vinsert_SetTextInsert(previewScene.Insert.TextInsert, this.selectedDataset.InsertText);
                    previewScene.Insert.TextInsert.SetIn();
                }
            }
        }

        protected void setFullscreenPreview() {
            if (this.previewSceneIsAvailable &&
                this.previewSource == Preview.Sources.Fullscreen) {
                Preview previewScene = this.previewScene as Preview;
                if (this.selectedDataset is DatasetContent) {
                    previewScene.Fullscreen.SetMapLayout(this.selectedDataset.MapLayout);
                    previewScene.Fullscreen.SetTaskText(this.selectedDataset.FullText);
                    previewScene.Fullscreen.ShowTask();
                    previewScene.Fullscreen.SetResultsSolution(this.selectedDataset.Name);
                    previewScene.Fullscreen.SetResultsBlueName(this.business.RightPlayerName);
                    previewScene.Fullscreen.SetResultsBlueDistance(1234);
                    previewScene.Fullscreen.SetResultsRedName(this.business.LeftPlayerName);
                    previewScene.Fullscreen.SetResultsRedDistance(5678);
                    previewScene.Fullscreen.ShowResults();
                    previewScene.Fullscreen.SetYellowPinPosition(this.selectedDataset.Coordinates.HD_PositionX, this.selectedDataset.Coordinates.HD_PositionY);
                    previewScene.Fullscreen.SetYellowPin();
                }
                else {
                    previewScene.Fullscreen.ResetTask();
                    previewScene.Fullscreen.ResetResults();
                    previewScene.Fullscreen.ResetYellowPin();
                }
            }
        }

        protected void setStagePreview() {
            if (this.previewSceneIsAvailable &&
                this.previewSource == Preview.Sources.Stage) {
                Preview previewScene = this.previewScene as Preview;
                if (this.selectedDataset is DatasetContent) {
                    previewScene.Stage.SetMapLayout(this.selectedDataset.MapLayout);
                    previewScene.Stage.SetFullText(this.selectedDataset.HostText);
                    previewScene.Stage.SetSolutionName(this.selectedDataset.Name);
                    previewScene.Stage.SetBlueName(this.business.RightPlayerName);
                    previewScene.Stage.SetRedName(this.business.LeftPlayerName);
                    previewScene.Stage.SetSolutionPosition(this.selectedDataset.Coordinates.SXGA_PositionX, this.selectedDataset.Coordinates.SXGA_PositionY);
                    previewScene.Stage.SelectPlayer(Stage.PlayerSelection.NotSelected);
                    previewScene.Stage.ShowSolution();
                }
                else {
                    previewScene.Stage.HideSolution();
                }
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
                if (e.PropertyName == "Name") this.textBoxDatasetName.Text = this.selectedDataset.Name;
                else if (e.PropertyName == "MapLayout") this.comboBoxMapLayout.Text = this.selectedDataset.MapLayout.ToString();
                else if (e.PropertyName == "Latitude") this.textBoxDatasetLatitude.Text = this.selectedDataset.Coordinates.Latitude.Text;
                else if (e.PropertyName == "Longitude") this.textBoxDatasetLongitude.Text = this.selectedDataset.Coordinates.Longitude.Text;
                else if (e.PropertyName == "InsertText") this.textBoxDatasetInsertText.Text = this.selectedDataset.InsertText;
                else if (e.PropertyName == "FullText") this.textBoxDatasetFullText.Text = this.selectedDataset.FullText;
                else if (e.PropertyName == "HostText") this.textBoxDatasetHostText.Text = this.selectedDataset.HostText;
                this.setInsertTextPreview();
                this.setFullscreenPreview();
                this.setStagePreview();
            }
        }

        #endregion

        #region Events.Controls

        //protected override void numericUpDownScorePositionX_ValueChanged(object sender, EventArgs e) {
        //    base.numericUpDownScorePositionX_ValueChanged(sender, e);
        //    this.setScorePreview();
        //}

        //protected override void numericUpDownScorePositionY_ValueChanged(object sender, EventArgs e) {
        //    base.numericUpDownScorePositionY_ValueChanged(sender, e);
        //    this.setInsertScorePreview();
        //}

        //protected override void comboBoxScoreStyle_SelectedIndexChanged(object sender, EventArgs e) {
        //    base.comboBoxScoreStyle_SelectedIndexChanged(sender, e);
        //    this.setInsertScorePreview();
        //}

        protected virtual void numericUpDownTextInsertPositionX_ValueChanged(object sender, EventArgs e) { 
            this.business.TextInsertPositionX = (int)this.numericUpDownTextInsertPositionX.Value;
            this.setInsertTextPreview();
        }
        protected virtual void numericUpDownTextInsertPositionY_ValueChanged(object sender, EventArgs e) { 
            this.business.TextInsertPositionY = (int)this.numericUpDownTextInsertPositionY.Value;
            this.setInsertTextPreview();
        }
        protected virtual void comboBoxTextInsertStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.TextInsert.Styles style;
            if (Enum.TryParse(this.comboBoxTextInsertStyle.Text, out style)) this.business.TextInsertStyle = style;
            this.setInsertTextPreview();
        }

        private void radioButtonSourceInsert_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourceInsert.Checked) this.previewSource = Preview.Sources.Insert; }
        private void radioButtonSourceFullscreen_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourceFullscreen.Checked) this.previewSource = Preview.Sources.Fullscreen; }
        private void radioButtonSourceStage_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourceHost.Checked) this.previewSource = Preview.Sources.Stage; }

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
            this.business.AddDataset(new DatasetContent(Fullscreen.MapLayoutElements.Europe), insertIndex);
            this.selectDataset(insertIndex);
        }
        private void buttonDataRemoveSet_Click(object sender, EventArgs e) {
            if (this.business.TryRemoveDataset(this.selectedDatasetIndex)) this.selectDataset(this.selectedDatasetIndex);
        }
        private void buttonDataRemoveAllSets_Click(object sender, EventArgs e) {
            this.business.RemoveAllDatasets();
            this.selectDataset(0);
        }

        private void textBoxDatasetName_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Name = this.textBoxDatasetName.Text; }
        private void comboBoxMapLayout_TextChanged(object sender, EventArgs e) {
            Fullscreen.MapLayoutElements result;
            if (Enum.TryParse(this.comboBoxMapLayout.Text, out result) &&
                this.selectedDataset is DatasetContent) this.selectedDataset.MapLayout = result;
        }
        private void buttonDatasetSetLongitude_Click(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Coordinates.Longitude.Text = this.textBoxDatasetLongitude.Text; }
        private void buttonDatasetSetLatitude_Click(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Coordinates.Latitude.Text = this.textBoxDatasetLatitude.Text; }
        private void textBoxDatasetSetInsertText_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.InsertText = this.textBoxDatasetInsertText.Text; }
        private void textBoxDatasetSetFullText_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.FullText = this.textBoxDatasetFullText.Text; }
        private void textBoxDatasetHostText_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.HostText = this.textBoxDatasetHostText.Text; }

        #endregion
    }

}
