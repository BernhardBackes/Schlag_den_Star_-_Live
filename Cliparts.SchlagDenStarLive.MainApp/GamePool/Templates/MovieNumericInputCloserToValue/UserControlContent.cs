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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MovieNumericInputCloserToValue;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.MovieNumericInputCloserToValue {

    public partial class UserControlContent : _Base.Score.UserControlContent {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;

        private int selectedDatasetIndex = -1;

        private bool _showScore = true;
        private bool showScore {
            get { return this._showScore; }
            set {
                if (this._showScore != value) {
                    this._showScore = value;
                    if (this.previewSource == Preview.Sources.Insert) this.setInsertPreview();
                }
            }
        }

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

            this.numericUpDownInputInsertPositionX.Minimum = int.MinValue;
            this.numericUpDownInputInsertPositionX.Maximum = int.MaxValue;

            this.numericUpDownInputInsertPositionY.Minimum = int.MinValue;
            this.numericUpDownInputInsertPositionY.Maximum = int.MaxValue;

            this.numericUpDownTaskCounterPositionX.Minimum = int.MinValue;
            this.numericUpDownTaskCounterPositionX.Maximum = int.MaxValue;

            this.numericUpDownTaskCounterPositionY.Minimum = int.MinValue;
            this.numericUpDownTaskCounterPositionY.Maximum = int.MaxValue;

            this.numericUpDownTaskCounterSize.Minimum = int.MinValue;
            this.numericUpDownTaskCounterSize.Maximum = int.MaxValue;

            this.numericUpDownDatasetSolution.Minimum = int.MinValue;
            this.numericUpDownDatasetSolution.Maximum = int.MaxValue;

            this.numericUpDownDatasetFirstStop.Minimum = int.MinValue;
            this.numericUpDownDatasetFirstStop.Maximum = int.MaxValue;

            this.numericUpDownDatasetSecondStop.Minimum = int.MinValue;
            this.numericUpDownDatasetSecondStop.Maximum = int.MaxValue;

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

            bind = new Binding("Value", this.business, "InputInsertPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownInputInsertPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "InputInsertPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownInputInsertPositionY.DataBindings.Add(bind);

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

            this.numericUpDownTextInsertPositionX.DataBindings.Clear();
            this.numericUpDownTextInsertPositionY.DataBindings.Clear();
            this.comboBoxTextInsertStyle.DataBindings.Clear();

            this.numericUpDownInputInsertPositionX.DataBindings.Clear();
            this.numericUpDownInputInsertPositionY.DataBindings.Clear();

            this.numericUpDownTaskCounterPositionX.DataBindings.Clear();
            this.numericUpDownTaskCounterPositionY.DataBindings.Clear();
            this.numericUpDownTaskCounterSize.DataBindings.Clear();

            this.labelFilename.DataBindings.Clear();
            this.checkBoxDataSampleIncluded.DataBindings.Clear();
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
                    if (this.showScore) this.radioButtonSourceScore.Checked = true;
                    else this.radioButtonSourceContent.Checked = true;
                    break;
                case Preview.Sources.Fullscreen:
                    this.setFullscreenPreview();
                    this.radioButtonSourceFullscreen.Checked = true;
                    break;
                case Preview.Sources.Host:
                    this.setHostPreview();
                    this.radioButtonSourceHost.Checked = true;
                    break;
                case Preview.Sources.Player:
                    this.setPlayerPreview();
                    this.radioButtonSourcePlayer.Checked = true;
                    break;
            }
        }

        protected void setInsertPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.showScore) {
                    this.setScorePreview();
                    previewScene.Insert.TextInsert.SetOut();
                    previewScene.Insert.Score.SetIn();
                }
                else {
                    this.setContentPreview();
                    previewScene.Insert.Score.SetOut();
                    previewScene.Insert.TextInsert.SetIn();
                    previewScene.Insert.CloserToValue.SetIn();
                    previewScene.Insert.CloserToValue.SetOffsetIn();
                }
                this.setTaskCounterPreview();
            }
        }

        protected override void setScorePreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetScore(previewScene.Insert.Score, 88, 99);
            }
        }

        protected void setContentPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.selectedDataset is DatasetContent) {
                    this.business.Vinsert_SetTextInsert(previewScene.Insert, string.Format("{0}{1}", this.selectedDataset.Solution.ToString("0.0"), this.selectedDataset.Unit));
                    this.business.Vinsert_SetInputInsert(previewScene.Insert, this.selectedDataset.Unit, 111, "+222", true, 888, "-999", false);
                }
            }
        }

        protected void setTaskCounterPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.previewSource == Preview.Sources.Insert) {
                    this.business.Vinsert_SetTaskCounter(previewScene.Insert.TaskCounter, 6);
                    if (this.showScore) previewScene.Insert.TaskCounter.SetOut();
                    else previewScene.Insert.TaskCounter.SetIn();
                }
            }
        }

        protected void setFullscreenPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.selectedDataset is DatasetContent) {
                    this.business.Vfullscreen_SetContent(previewScene.Fullscreen, this.selectedDataset.MovieFilename, this.selectedDataset.FirstStop, this.selectedDataset.SecondStop);
                    previewScene.Fullscreen.SetIn();
                    previewScene.Fullscreen.ToLastFrame();
                }
            }
        }

        protected void setHostPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.selectedDataset is DatasetContent) previewScene.Host.SetText(this.selectedDataset.HostText);
                else previewScene.Host.SetText(string.Empty);
                this.business.Vhost_SetPlayerInput(previewScene.Host, this.selectedDataset.Unit, 111, "+222", true, 888, "-999", false);
                previewScene.Host.SetIn();
            }
        }

        protected void setPlayerPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.selectedDataset is DatasetContent) {
                    previewScene.Player.SetTask(this.selectedDataset.Name);
                    previewScene.Player.UnlockTouch();
                }
                else {
                    previewScene.Player.SetTask(string.Empty);
                    previewScene.Player.LockTouch();
                }
                previewScene.Player.SetIn();
            }
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
            this.numericUpDownDatasetSolution.DataBindings.Clear();
            this.textBoxDatasetUnit.DataBindings.Clear();
            this.textBoxDatasetHostText.DataBindings.Clear();
            this.numericUpDownDatasetFirstStop.DataBindings.Clear();
            this.numericUpDownDatasetSecondStop.DataBindings.Clear();

            //Dispose...
            if (this.selectedDataset is DatasetContent) {
                this.selectedDataset.PropertyChanged -= this.selectedDataset_PropertyChanged;
            }
            this.selectedDataset = selectedDataset;
            //Pose...
            if (this.selectedDataset is DatasetContent) {

                this.selectedDataset.PropertyChanged += this.selectedDataset_PropertyChanged;

                this.groupBoxDataset.Enabled = true;
                this.buttonDataRemoveSet.Enabled = true;

                this.pictureBoxDatasetMovie.Image = this.selectedDataset.Movie;

                Binding bind;

                bind = new Binding("Text", this.selectedDataset, "Name");
                bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
                this.textBoxDatasetName.DataBindings.Add(bind);

                bind = new Binding("Value", this.selectedDataset, "Solution");
                bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
                this.numericUpDownDatasetSolution.DataBindings.Add(bind);

                bind = new Binding("Text", this.selectedDataset, "Unit");
                bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
                this.textBoxDatasetUnit.DataBindings.Add(bind);

                bind = new Binding("Text", this.selectedDataset, "HostText");
                bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
                this.textBoxDatasetHostText.DataBindings.Add(bind);

                bind = new Binding("Value", this.selectedDataset, "FirstStop");
                bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
                this.numericUpDownDatasetFirstStop.DataBindings.Add(bind);

                bind = new Binding("Value", this.selectedDataset, "SecondStop");
                bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
                this.numericUpDownDatasetSecondStop.DataBindings.Add(bind);

                if (this.listBoxDataList.Items.Count > this.selectedDatasetIndex) this.listBoxDataList.SelectedIndex = this.selectedDatasetIndex;
            }
            else {
                this.groupBoxDataset.Enabled = false;
                this.buttonDataRemoveSet.Enabled = false;
                this.pictureBoxDatasetMovie.Image = null;
                this.textBoxDatasetName.Text = string.Empty;
                this.numericUpDownDatasetSolution.Value = 0;
                this.textBoxDatasetUnit.Text = string.Empty;
                this.textBoxDatasetHostText.Text = string.Empty;
                this.numericUpDownDatasetFirstStop.Value = 0;
                this.numericUpDownDatasetSecondStop.Value = 0;
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

        private void selectedDataset_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.selectedDataset_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "MovieFilename") {
                    this.pictureBoxDatasetMovie.Image = this.selectedDataset.Movie;
                    if (this.previewSource == Preview.Sources.Fullscreen) this.setFullscreenPreview();
                }
                else if (e.PropertyName == "HostText") {
                    this.textBoxDatasetHostText.Text = this.selectedDataset.HostText;
                    if (this.previewSource == Preview.Sources.Host) this.setHostPreview();
                }
                else if (e.PropertyName == "Name") {
                    this.textBoxDatasetName.Text = this.selectedDataset.Name;
                    if (this.previewSource == Preview.Sources.Player) this.setPlayerPreview();
                }
                else if (e.PropertyName == "Solution") {
                    this.numericUpDownDatasetSolution.Value = (decimal)this.selectedDataset.Solution;
                    if (this.previewSource == Preview.Sources.Insert) this.setContentPreview();
                }
                else if (e.PropertyName == "Unit") {
                    this.textBoxDatasetUnit.Text = this.selectedDataset.Unit;
                    if (this.previewSource == Preview.Sources.Insert) this.setContentPreview();
                    else if (this.previewSource == Preview.Sources.Host) this.setHostPreview();
                }
            }
        }

        #endregion

        #region Events.Controls

        protected virtual void numericUpDownTextInsertPositionX_ValueChanged(object sender, EventArgs e) { 
            this.business.TextInsertPositionX = (int)this.numericUpDownTextInsertPositionX.Value;
            this.setContentPreview();
        }
        protected virtual void numericUpDownTextInsertPositionY_ValueChanged(object sender, EventArgs e) { 
            this.business.TextInsertPositionY = (int)this.numericUpDownTextInsertPositionY.Value;
            this.setContentPreview();
        }

        protected virtual void comboBoxTextInsertStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.TextInsert.Styles style;
            if (Enum.TryParse(this.comboBoxTextInsertStyle.Text, out style)) this.business.TextInsertStyle = style;
            this.setContentPreview();
        }

        protected virtual void numericUpDownInputInsertPositionX_ValueChanged(object sender, EventArgs e) {
            this.business.InputInsertPositionX = (int)this.numericUpDownInputInsertPositionX.Value;
            this.setContentPreview();
        }
        protected virtual void numericUpDownInputInsertPositionY_ValueChanged(object sender, EventArgs e) {
            this.business.InputInsertPositionY = (int)this.numericUpDownInputInsertPositionY.Value;
            this.setContentPreview();
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

        private void radioButtonSourceInsert_CheckedChanged(object sender, EventArgs e) {
            if (this.radioButtonSourceScore.Checked) {
                this.showScore = true;
                this.previewSource = Preview.Sources.Insert;
            }
        }
        private void radioButtonSourceContent_CheckedChanged(object sender, EventArgs e) {
            if (this.radioButtonSourceContent.Checked) {
                this.showScore = false;
                this.previewSource = Preview.Sources.Insert;
            }
        }
        private void radioButtonSourceFullscreen_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourceFullscreen.Checked) this.previewSource = Preview.Sources.Fullscreen; }
        private void radioButtonSourceHost_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourceHost.Checked) this.previewSource = Preview.Sources.Host; }
        private void radioButtonSourcePlayer_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourcePlayer.Checked) this.previewSource = Preview.Sources.Player; }

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
            string filename = Helper.selectVideoFile("select movie", string.Empty);
            if (filename != null) {
                int listIndex = this.listBoxDataList.SelectedIndex + 1;
                this.business.AddDataset(new DatasetContent(filename), listIndex);
                this.selectDataset(listIndex);
            }
        }
        private void buttonDataRemoveSet_Click(object sender, EventArgs e) {
            if (this.business.TryRemoveDataset(this.selectedDatasetIndex)) this.selectDataset(this.selectedDatasetIndex - 1);
        }
        private void buttonDataRemoveAllSets_Click(object sender, EventArgs e) {
            this.business.RemoveAllDatasets();
            this.selectDataset(0);
        }

        private void pictureBoxDatasetMovie_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                string filename = Helper.selectVideoFile("select movie", this.selectedDataset.MovieFilename);
                if (filename != null) this.selectedDataset.MovieFilename = filename;
            }
        }
        private void textBoxDatasetName_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Name = this.textBoxDatasetName.Text; }
        private void numericUpDownDatasetSolution_ValueChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Solution = (Single)this.numericUpDownDatasetSolution.Value; }
        private void textBoxDatasetUnit_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Unit = this.textBoxDatasetUnit.Text; }
        private void textBoxDatasetHostText_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.HostText = this.textBoxDatasetHostText.Text; }
        private void numericUpDownDatasetFirstStop_ValueChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.FirstStop = (Single)this.numericUpDownDatasetFirstStop.Value; }
        private void numericUpDownDatasetSecondStop_ValueChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.SecondStop = (Single)this.numericUpDownDatasetSecondStop.Value; }

        #endregion

    }

}
