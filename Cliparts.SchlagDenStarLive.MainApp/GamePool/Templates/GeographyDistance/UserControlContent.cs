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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.GeographyDistance;
using SlimDX;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.GeographyDistance {

    public partial class UserControlContent : _Base.TimerScore.UserControlContent {

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

            this.comboBoxDatasetMapLayout.BeginUpdate();
            this.comboBoxDatasetMapLayout.Items.AddRange(Enum.GetNames(typeof(Fullscreen.MapLayoutElements)));
            this.comboBoxDatasetMapLayout.EndUpdate();

            this.numericUpDownDatasetDistance.Minimum = int.MinValue;
            this.numericUpDownDatasetDistance.Maximum = int.MaxValue;

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

            bind = new Binding("Text", this.business, "LeftTeamTabletHostname");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.textBoxLeftTeamTabletHostname.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightTeamTabletHostname");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.textBoxRightTeamTabletHostname.DataBindings.Add(bind);

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

            this.textBoxLeftTeamTabletHostname.DataBindings.Clear();
            this.textBoxRightTeamTabletHostname.DataBindings.Clear();

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
            this.comboBoxDatasetMapLayout.DataBindings.Clear();
            this.numericUpDownDatasetDistance.DataBindings.Clear();
            this.textBoxDatasetFirstLocationName.DataBindings.Clear();
            this.textBoxDatasetFirstLocationLongitude.DataBindings.Clear();
            this.textBoxDatasetFirstLocationLatitude.DataBindings.Clear();
            this.textBoxDatasetSecondLocationName.DataBindings.Clear();
            this.textBoxDatasetSecondLocationLongitude.DataBindings.Clear();
            this.textBoxDatasetSecondLocationLatitude.DataBindings.Clear();
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
                this.comboBoxDatasetMapLayout.Text = this.selectedDataset.MapLayout.ToString();
                this.numericUpDownDatasetDistance.Value = this.selectedDataset.Distance;
                this.textBoxDatasetFirstLocationName.Text = this.selectedDataset.FirstLocationName;
                this.textBoxDatasetFirstLocationLongitude.Text = this.selectedDataset.FirstLocationCoordinates.Longitude.Text;
                this.textBoxDatasetFirstLocationLatitude.Text = this.selectedDataset.FirstLocationCoordinates.Latitude.Text;
                this.buttonDatasetFirstLocationSetLatitude.Enabled = true;
                this.buttonDatasetFirstLocationSetLongitude.Enabled = true;
                this.textBoxDatasetSecondLocationName.Text = this.selectedDataset.SecondLocationName;
                this.textBoxDatasetSecondLocationLongitude.Text = this.selectedDataset.SecondLocationCoordinates.Longitude.Text;
                this.textBoxDatasetSecondLocationLatitude.Text = this.selectedDataset.SecondLocationCoordinates.Latitude.Text;
                this.buttonDatasetSecondLocationSetLatitude.Enabled = true;
                this.buttonDatasetSecondLocationSetLongitude.Enabled = true;
                this.buttonDataRemoveSet.Enabled = true;
                if (this.listBoxDataList.Items.Count > this.selectedDatasetIndex) this.listBoxDataList.SelectedIndex = this.selectedDatasetIndex;
            }
            else {
                this.groupBoxDataset.Enabled = false;
                this.textBoxDatasetName.Text = string.Empty;
                this.comboBoxDatasetMapLayout.Text = string.Empty;
                this.numericUpDownDatasetDistance.Value = 0;
                this.textBoxDatasetFirstLocationName.Text = string.Empty;
                this.textBoxDatasetFirstLocationLongitude.Text = string.Empty;
                this.textBoxDatasetFirstLocationLatitude.Text = string.Empty;
                this.buttonDatasetFirstLocationSetLatitude.Enabled = false;
                this.buttonDatasetFirstLocationSetLongitude.Enabled = false;
                this.textBoxDatasetSecondLocationName.Text = string.Empty;
                this.textBoxDatasetSecondLocationLongitude.Text = string.Empty;
                this.textBoxDatasetSecondLocationLatitude.Text = string.Empty;
                this.buttonDatasetSecondLocationSetLatitude.Enabled = false;
                this.buttonDatasetSecondLocationSetLongitude.Enabled = false;
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
                    this.business.Vinsert_SetTextInsert(previewScene.Insert.TextInsert, this.selectedDataset.FirstLocationName);
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
                    //previewScene.Fullscreen.SetTaskText(this.selectedDataset.Name);
                    //previewScene.Fullscreen.ShowTask();
                    previewScene.Fullscreen.SetResultsSolution(this.selectedDataset.Name);
                    previewScene.Fullscreen.SetResultsSolutionDistance(this.selectedDataset.Distance);
                    previewScene.Fullscreen.SetResultsBlueName(this.business.RightPlayerName);
                    previewScene.Fullscreen.SetResultsBlueDistance(1234);
                    previewScene.Fullscreen.SetResultsRedName(this.business.LeftPlayerName);
                    previewScene.Fullscreen.SetResultsRedDistance(5678);
                    previewScene.Fullscreen.SetResultsIn();
                    previewScene.Fullscreen.SetSolutionDistanceIn(); 
                    previewScene.Fullscreen.SetYellowPin1Position(this.selectedDataset.FirstLocationCoordinates.HD_PositionX, this.selectedDataset.FirstLocationCoordinates.HD_PositionY);
                    previewScene.Fullscreen.SetYellowPin2Position(this.selectedDataset.SecondLocationCoordinates.HD_PositionX, this.selectedDataset.SecondLocationCoordinates.HD_PositionY);
                    previewScene.Fullscreen.SetYellowPins();
                }
                else {
                    previewScene.Fullscreen.ResetTask();
                    previewScene.Fullscreen.ResetResults();
                    previewScene.Fullscreen.ResetSolutionDistance();
                    previewScene.Fullscreen.ResetYellowPins();
                }
            }
        }

        protected void setStagePreview() {
            if (this.previewSceneIsAvailable &&
                this.previewSource == Preview.Sources.Stage) {
                Preview previewScene = this.previewScene as Preview;
                if (this.selectedDataset is DatasetContent) {
                    previewScene.Stage.SetMapLayout(this.selectedDataset.MapLayout);
                    //previewScene.Stage.SetFullText(this.selectedDataset.HostText);
                    previewScene.Stage.SetSolutionName(this.selectedDataset.Name);
                    previewScene.Stage.SetSolutionDistance(this.selectedDataset.Distance);
                    previewScene.Stage.SetBlueName(this.business.RightPlayerName);
                    previewScene.Stage.SetRedName(this.business.LeftPlayerName);
                    previewScene.Stage.SetSolutionPin1Position(this.selectedDataset.FirstLocationCoordinates.HD_PositionX, this.selectedDataset.FirstLocationCoordinates.HD_PositionY);
                    previewScene.Stage.SetSolutionPin2Position(this.selectedDataset.SecondLocationCoordinates.HD_PositionX, this.selectedDataset.SecondLocationCoordinates.HD_PositionY);
                    previewScene.Stage.ShowSolutionDistance();
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
                else if (e.PropertyName == "MapLayout") this.comboBoxDatasetMapLayout.Text = this.selectedDataset.MapLayout.ToString();
                else if (e.PropertyName == "Distance") this.numericUpDownDatasetDistance.Value = this.selectedDataset.Distance;
                else if (e.PropertyName == "FirstLocationName") this.textBoxDatasetFirstLocationName.Text = this.selectedDataset.FirstLocationName;
                else if (e.PropertyName == "SecondLocationName") this.textBoxDatasetSecondLocationName.Text = this.selectedDataset.SecondLocationName;
                else if (e.PropertyName == "Latitude")
                {
                    this.textBoxDatasetFirstLocationLatitude.Text = this.selectedDataset.FirstLocationCoordinates.Latitude.Text;
                    this.textBoxDatasetSecondLocationLatitude.Text = this.selectedDataset.SecondLocationCoordinates.Latitude.Text;
                }
                else if (e.PropertyName == "Longitude")
                {
                    this.textBoxDatasetFirstLocationLongitude.Text = this.selectedDataset.FirstLocationCoordinates.Longitude.Text;
                    this.textBoxDatasetSecondLocationLongitude.Text = this.selectedDataset.SecondLocationCoordinates.Longitude.Text;
                }
                this.setInsertTextPreview();
                this.setFullscreenPreview();
                this.setStagePreview();
            }
        }

        #endregion

        #region Events.Controls

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

        private void textBoxLeftTeamTabletHostname_TextChanged(object sender, EventArgs e) { this.business.LeftTeamTabletHostname = this.textBoxLeftTeamTabletHostname.Text; }
        private void textBoxRightTeamTabletHostname_TextChanged(object sender, EventArgs e) { this.business.RightTeamTabletHostname = this.textBoxRightTeamTabletHostname.Text; }

        private void radioButtonSourceInsert_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourceInsert.Checked) this.previewSource = Preview.Sources.Insert; }
        private void radioButtonSourceFullscreen_CheckedChanged(object sender, EventArgs e) { this.previewSource = Preview.Sources.Fullscreen; }
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
            Fullscreen.MapLayoutElements mapLayout = Fullscreen.MapLayoutElements.Europe;
            if (this.selectedDataset is DatasetContent) mapLayout = this.selectedDataset.MapLayout;
            int insertIndex = this.listBoxDataList.SelectedIndex + 1;
            this.business.AddDataset(new DatasetContent(mapLayout), insertIndex);
            this.selectDataset(insertIndex);
        }
        private void buttonDataRemoveSet_Click(object sender, EventArgs e) {
            if (this.business.TryRemoveDataset(this.selectedDatasetIndex)) this.selectDataset(this.selectedDatasetIndex);
        }
        private void buttonDataRemoveAllSets_Click(object sender, EventArgs e) {
            this.business.RemoveAllDatasets();
            this.selectDataset(0);
        }

        private void comboBoxMapLayout_TextChanged(object sender, EventArgs e) {
            Fullscreen.MapLayoutElements result;
            if (Enum.TryParse(this.comboBoxDatasetMapLayout.Text, out result) &&
                this.selectedDataset is DatasetContent) this.selectedDataset.MapLayout = result;
        }
        private void numericUpDownDatasetDistance_ValueChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Distance = (int)numericUpDownDatasetDistance.Value; }
        private void textBoxDatasetFirstLocationName_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.FirstLocationName = this.textBoxDatasetFirstLocationName.Text; }
        private void buttonDatasetFirstLocationSetLatitude_Click(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.FirstLocationCoordinates.Latitude.Text = this.textBoxDatasetFirstLocationLatitude.Text; }
        private void buttonDatasetFirstLocationSetLongitude_Click(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.FirstLocationCoordinates.Longitude.Text = this.textBoxDatasetFirstLocationLongitude.Text; }
        private void textBoxDatasetSecondLocationName_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.SecondLocationName = this.textBoxDatasetSecondLocationName.Text; }
        private void buttonDatasetSecondLocationSetLatitude_Click(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.SecondLocationCoordinates.Latitude.Text = this.textBoxDatasetSecondLocationLatitude.Text; }
        private void buttonDatasetSecondLocationSetLongitude_Click(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.SecondLocationCoordinates.Longitude.Text = this.textBoxDatasetSecondLocationLongitude.Text; }

        #endregion

    }

}
