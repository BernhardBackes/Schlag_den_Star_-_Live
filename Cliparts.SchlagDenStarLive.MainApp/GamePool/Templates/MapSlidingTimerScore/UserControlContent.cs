using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.Tools.Base;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MapSlidingTimerScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.MapSlidingTimerScore {

    public partial class UserControlContent : _Base.BuzzerTimerScore.UserControlContent {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;

        private int selectedDatasetIndex = -1;

        private int taskCounter {
            get {
                if (this.business.SampleIncluded) return this.selectedDatasetIndex;
                else return this.selectedDatasetIndex + 1;
            }
        }

        private bool _showScore = true;
        protected bool showScore {
            get { return this._showScore; }
            set {
                if (this._showScore != value) {
                    this._showScore = value;
                    this.setPreviewData();
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

            this.numericUpDownTaskCounterPositionX.Minimum = int.MinValue;
            this.numericUpDownTaskCounterPositionX.Maximum = int.MaxValue;

            this.numericUpDownTaskCounterPositionY.Minimum = int.MinValue;
            this.numericUpDownTaskCounterPositionY.Maximum = int.MaxValue;

            this.numericUpDownTaskCounterSize.Minimum = int.MinValue;
            this.numericUpDownTaskCounterSize.Maximum = int.MaxValue;

            this.numericUpDownBorderPositionX.Minimum = int.MinValue;
            this.numericUpDownBorderPositionX.Maximum = int.MaxValue;

            this.numericUpDownBorderPositionY.Minimum = int.MinValue;
            this.numericUpDownBorderPositionY.Maximum = int.MaxValue;

            this.numericUpDownBorderScaling.Minimum = int.MinValue;
            this.numericUpDownBorderScaling.Maximum = int.MaxValue;

            this.comboBoxBorderStyle.BeginUpdate();
            this.comboBoxBorderStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.Border.Styles)));
            this.comboBoxBorderStyle.EndUpdate();

            this.numericUpDownDatasetZoomFactor.Minimum = int.MinValue;
            this.numericUpDownDatasetZoomFactor.Maximum = int.MaxValue;

            this.numericUpDownDatasetScaling.Minimum = int.MinValue;
            this.numericUpDownDatasetScaling.Maximum = int.MaxValue;

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

            bind = new Binding("Value", this.business, "TextInsertPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTextInsertPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TextInsertPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTextInsertPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TextInsertStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxTextInsertStyle.DataBindings.Add(bind);

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

            this.numericUpDownBorderPositionX.DataBindings.Clear();
            this.numericUpDownBorderPositionY.DataBindings.Clear();
            this.numericUpDownBorderScaling.DataBindings.Clear();
            this.comboBoxBorderStyle.DataBindings.Clear();

            this.numericUpDownTextInsertPositionX.DataBindings.Clear();
            this.numericUpDownTextInsertPositionY.DataBindings.Clear();
            this.comboBoxTextInsertStyle.DataBindings.Clear();

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

            this.textBoxDatasetName.DataBindings.Clear();
            this.textBoxDatasetLongitude.DataBindings.Clear();
            this.textBoxDatasetLatitude.DataBindings.Clear();
            this.numericUpDownDatasetZoomFactor.DataBindings.Clear();
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
                this.textBoxDatasetLongitude.Text = this.selectedDataset.Coordinates.Longitude.Text;
                this.textBoxDatasetLatitude.Text = this.selectedDataset.Coordinates.Latitude.Text;
                this.numericUpDownDatasetZoomFactor.Value = this.selectedDataset.ZoomFactor;
                this.numericUpDownDatasetScaling.Value = this.selectedDataset.Scaling;
                this.textBoxDatasetHostText.Text = this.selectedDataset.HostText;
                this.textBoxDatasetURL.Text = this.selectedDataset.URL;
                this.buttonDatasetSetLatitude.Enabled = true;
                this.buttonDatasetSetLongitude.Enabled = true;
                this.buttonDataRemoveSet.Enabled = true;
                if (this.listBoxDataList.Items.Count > this.selectedDatasetIndex) this.listBoxDataList.SelectedIndex = this.selectedDatasetIndex;
            }
            else {
                this.groupBoxDataset.Enabled = false;
                this.textBoxDatasetName.Text = string.Empty;
                this.textBoxDatasetLongitude.Text = string.Empty;
                this.textBoxDatasetLatitude.Text = string.Empty;
                this.textBoxDatasetHostText.Text = string.Empty;
                this.textBoxDatasetURL.Text = string.Empty;
                this.buttonDatasetSetLatitude.Enabled = false;
                this.buttonDatasetSetLongitude.Enabled = false;
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
                    if (this.showScore) this.radioButtonSourceScore.Checked = true;
                    else this.radioButtonSourceInsert.Checked = true;
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
                this.setScorePreview();
                this.setBorderPreview();
                this.setTaskCounterPreview();
                this.setTextPreview();
                this.setTimerPreview();
            }
        }

        protected override void setScorePreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.previewSource == Preview.Sources.Insert) {
                    this.business.Vinsert_SetScore(previewScene.Insert.Score, 2, 3);
                    if (this.showScore) previewScene.Insert.Score.SetIn();
                    else previewScene.Insert.Score.SetOut();
                }
            }
        }

        protected virtual void setBorderPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetBorder(previewScene.Insert.Border, 2, 3);
                if (this.previewSource == Preview.Sources.Insert) {
                    if (this.showScore) previewScene.Insert.Border.SetOut();
                    else previewScene.Insert.Border.SetIn();
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

        protected void setTextPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.previewSource == Preview.Sources.Insert &&
                    this.selectedDataset is DatasetContent) {
                    this.business.Vinsert_SetTextInsert(previewScene.Insert.TextInsert, this.selectedDataset.Name);
                    if (this.showScore) previewScene.Insert.TextInsert.SetOut();
                    else previewScene.Insert.TextInsert.SetIn();
                }
            }
        }

        protected override void setTimerPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.previewSource == Preview.Sources.Insert) {
                    this.business.Vinsert_SetTimer(previewScene.Insert.Timer, this.business.TimerStartTime);
                    if (this.showScore) previewScene.Insert.Timer.SetOut();
                    else previewScene.Insert.Timer.SetIn();
                }
            }
        }

        protected void setHostPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.selectedDataset is DatasetContent) {
                    this.business.Vhost_Set(previewScene.Host, this.taskCounter, this.selectedDataset.HostText);
                    previewScene.Host.SetIn();
                }
                else {
                    previewScene.Host.SetOut();
                    this.business.Vhost_Set(previewScene.Host, -1, string.Empty);
                }
            }
        }

        protected void setPlayerPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.selectedDataset is DatasetContent) {
                    this.business.Vplayer_Set(previewScene.Player, this.selectedDataset.Name, this.selectedDataset.URL, this.selectedDataset.Scaling);
                    previewScene.Player.LoadURL();
                    previewScene.Player.SetIn();
                }
                else {
                    previewScene.Player.SetOut();
                    this.business.Vplayer_Set(previewScene.Player, string.Empty, string.Empty, 100);
                }
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
                else if (e.PropertyName == "Latitude") this.textBoxDatasetLatitude.Text = this.selectedDataset.Coordinates.Latitude.Text;
                else if (e.PropertyName == "Longitude") this.textBoxDatasetLongitude.Text = this.selectedDataset.Coordinates.Longitude.Text;
                else if (e.PropertyName == "ZoomFactor") this.numericUpDownDatasetZoomFactor.Value = this.selectedDataset.ZoomFactor;
                else if (e.PropertyName == "Scaling") this.numericUpDownDatasetScaling.Value = this.selectedDataset.Scaling;
                else if (e.PropertyName == "HostText") this.textBoxDatasetHostText.Text = this.selectedDataset.HostText;
                else if (e.PropertyName == "URL") this.textBoxDatasetURL.Text = this.selectedDataset.URL;
                if (this.previewSource == Preview.Sources.Insert) this.setInsertPreview();
                if (this.previewSource == Preview.Sources.Host) this.setHostPreview();
                if (this.previewSource == Preview.Sources.Player) this.setPlayerPreview();
            }
        }

        #endregion

        #region Events.Controls

        private void UserControlContent_BackColorChanged(object sender, EventArgs e) {
            this.tabPageBorder.BackColor = this.BackColor;
            this.tabPageTaskCounter.BackColor = this.BackColor;
            this.tabPageTextInsert.BackColor = this.BackColor;
        }

        private void numericUpDownBorderPositionX_ValueChanged(object sender, EventArgs e) {
            this.business.BorderPositionX = (int)this.numericUpDownBorderPositionX.Value;
            if (this.previewSource == Preview.Sources.Insert) this.setBorderPreview();
        }
        private void numericUpDownBorderPositionY_ValueChanged(object sender, EventArgs e) {
            this.business.BorderPositionY = (int)this.numericUpDownBorderPositionY.Value;
            if (this.previewSource == Preview.Sources.Insert) this.setBorderPreview();
        }
        private void numericUpDownBorderScaling_ValueChanged(object sender, EventArgs e) {
            this.business.BorderScaling = (int)this.numericUpDownBorderScaling.Value;
            if (this.previewSource == Preview.Sources.Insert) this.setBorderPreview();
        }
        private void comboBoxBorderStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.Border.Styles style;
            if (Enum.TryParse(this.comboBoxBorderStyle.Text, out style)) this.business.BorderStyle = style;
            if (this.previewSource == Preview.Sources.Insert) this.setBorderPreview();
        }

        protected virtual void numericUpDownTextInsertPositionX_ValueChanged(object sender, EventArgs e) {
            this.business.TextInsertPositionX = (int)this.numericUpDownTextInsertPositionX.Value;
            if (this.previewSource == Preview.Sources.Insert) this.setTextPreview();
        }
        protected virtual void numericUpDownTextInsertPositionY_ValueChanged(object sender, EventArgs e) {
            this.business.TextInsertPositionY = (int)this.numericUpDownTextInsertPositionY.Value;
            if (this.previewSource == Preview.Sources.Insert) this.setTextPreview();
        }
        protected virtual void comboBoxTextInsertStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.TextInsert.Styles style;
            if (Enum.TryParse(this.comboBoxTextInsertStyle.Text, out style)) this.business.TextInsertStyle = style;
            if (this.previewSource == Preview.Sources.Insert) this.setTextPreview();
        }

        private void numericUpDownTaskCounterPositionX_ValueChanged(object sender, EventArgs e) {
            this.business.TaskCounterPositionX = (int)this.numericUpDownTaskCounterPositionX.Value;
            if (this.previewSource == Preview.Sources.Insert) this.setTaskCounterPreview();
        }
        private void numericUpDownTaskCounterPositionY_ValueChanged(object sender, EventArgs e) {
            this.business.TaskCounterPositionY = (int)this.numericUpDownTaskCounterPositionY.Value;
            if (this.previewSource == Preview.Sources.Insert) this.setTaskCounterPreview();
        }
        private void numericUpDownTaskCounterSize_ValueChanged(object sender, EventArgs e) {
            this.business.TaskCounterSize = (int)this.numericUpDownTaskCounterSize.Value;
            if (this.previewSource == Preview.Sources.Insert) this.setTaskCounterPreview();
        }

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
        private void buttonDataRemoveSet_Click(object sender, EventArgs e) {
            if (this.business.TryRemoveDataset(this.selectedDatasetIndex)) this.selectDataset(this.selectedDatasetIndex - 1);
        }
        private void buttonDataRemoveAllSets_Click(object sender, EventArgs e) {
            this.business.RemoveAllDatasets();
            this.selectDataset(0);
        }

        private void radioButtonSourceScore_CheckedChanged(object sender, EventArgs e) {
            if (this.radioButtonSourceScore.Checked) {
                this.previewSource = Preview.Sources.Insert;
                this.showScore = true;
            }

        }
        private void radioButtonSourceInsert_CheckedChanged(object sender, EventArgs e) {
            if (this.radioButtonSourceInsert.Checked) {
                this.previewSource = Preview.Sources.Insert;
                this.showScore = false;
            }
        }
        private void radioButtonSourceHost_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourceHost.Checked) this.previewSource = Preview.Sources.Host; }
        private void radioButtonSourcePlayer_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourcePlayer.Checked) this.previewSource = Preview.Sources.Player; }

        private void textBoxDatasetName_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Name = this.textBoxDatasetName.Text; }
        private void buttonDatasetSetLongitude_Click(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Coordinates.Longitude.Text = this.textBoxDatasetLongitude.Text; }
        private void buttonDatasetSetLatitude_Click(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Coordinates.Latitude.Text = this.textBoxDatasetLatitude.Text; }
        private void numericUpDownDatasetZoomFactor_ValueChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.ZoomFactor = (ushort)this.numericUpDownDatasetZoomFactor.Value; }
        private void numericUpDownDatasetScaling_ValueChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Scaling = (ushort)this.numericUpDownDatasetScaling.Value; }
        private void textBoxDatasetHostText_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.HostText = this.textBoxDatasetHostText.Text; }

        #endregion

    }
}
