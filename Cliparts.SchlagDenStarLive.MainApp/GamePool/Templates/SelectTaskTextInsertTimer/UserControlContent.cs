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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SelectTaskTextInsertTimer;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.SelectTaskTextInsertTimer {

    public partial class UserControlContent : _Base.TimerScore.UserControlContent {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;

        private int selectedDatasetIndex = -1;

        private Preview.Sources _previewSource = Preview.Sources.Insert;
        protected Preview.Sources previewSource
        {
            get { return this._previewSource; }
            set
            {
                if (this._previewSource != value)
                {
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

            this.numericUpDownTaskInsertPositionX.Minimum = int.MinValue;
            this.numericUpDownTaskInsertPositionX.Maximum = int.MaxValue;

            this.numericUpDownTaskInsertPositionY.Minimum = int.MinValue;
            this.numericUpDownTaskInsertPositionY.Maximum = int.MaxValue;

            this.numericUpDownTextInsertPositionX.Minimum = int.MinValue;
            this.numericUpDownTextInsertPositionX.Maximum = int.MaxValue;

            this.numericUpDownTextInsertPositionY.Minimum = int.MinValue;
            this.numericUpDownTextInsertPositionY.Maximum = int.MaxValue;

            this.comboBoxTextInsertStyle.BeginUpdate();
            this.comboBoxTextInsertStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.TextInsert.Styles)));
            this.comboBoxTextInsertStyle.EndUpdate();

            this.numericUpDownTimeoutPositionX.Minimum = int.MinValue;
            this.numericUpDownTimeoutPositionX.Maximum = int.MaxValue;

            this.numericUpDownTimeoutPositionY.Minimum = int.MinValue;
            this.numericUpDownTimeoutPositionY.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.business, nameof(this.business.TaskInsertPositionX));
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTaskInsertPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, nameof(this.business.TaskInsertPositionY));
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTaskInsertPositionY.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, nameof(this.business.TextInsertPositionX));
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTextInsertPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, nameof(this.business.TextInsertPositionY));
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTextInsertPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, nameof(this.business.TextInsertStyle));
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxTextInsertStyle.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, nameof(this.business.Filename));
            bind.Format += (s, e) => { e.Value = string.IsNullOrEmpty((string)e.Value) ? "no file loaded" : (string)e.Value; };
            this.labelFilename.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, nameof(this.business.TimeoutPositionX));
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimeoutPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, nameof(this.business.TimeoutPositionY));
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimeoutPositionY.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, nameof(this.business.TimeoutIsVisible));
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxTimeoutIsVisible.DataBindings.Add(bind);
            bind = new Binding("ForeColor", this.business, nameof(this.business.TimeoutIsVisible));
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? this.ForeColor : Constants.ColorMissing; };
            this.checkBoxTimeoutIsVisible.DataBindings.Add(bind);

            this.labelGameClass.Text = this.business.ClassInfo;

            this.fillDataList();

            this.selectDataset(this.business.SelectedDatasetIndex);

            this.setPreviewData();

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

            this.numericUpDownTaskInsertPositionX.DataBindings.Clear();
            this.numericUpDownTaskInsertPositionY.DataBindings.Clear();

            this.numericUpDownTextInsertPositionX.DataBindings.Clear();
            this.numericUpDownTextInsertPositionY.DataBindings.Clear();
            this.comboBoxTextInsertStyle.DataBindings.Clear();

            this.labelFilename.DataBindings.Clear();
        }

        private void fillDataList() {
            this.listBoxDataList.BeginUpdate();
            this.listBoxDataList.Items.Clear();
            int id = 1;
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

            this.textBoxDatasetText.DataBindings.Clear();
            this.textBoxDatasetSolution.DataBindings.Clear();
            this.textBoxDatasetHostText.DataBindings.Clear();

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

                Binding bind;

                bind = new Binding("Text", this.selectedDataset, "Text");
                bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
                this.textBoxDatasetText.DataBindings.Add(bind);

                bind = new Binding("Text", this.selectedDataset, "Solution");
                bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
                this.textBoxDatasetSolution.DataBindings.Add(bind);

                bind = new Binding("Text", this.selectedDataset, "HostText");
                bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
                this.textBoxDatasetHostText.DataBindings.Add(bind);

                if (this.listBoxDataList.Items.Count > this.selectedDatasetIndex) this.listBoxDataList.SelectedIndex = this.selectedDatasetIndex;

            }
            else
            {
                this.groupBoxDataset.Enabled = false;
                this.buttonDataRemoveSet.Enabled = false;
                this.textBoxDatasetText.Text = string.Empty;
                this.textBoxDatasetHostText.Text = string.Empty;
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
            Preview.Sources source)
        {
            if (this.previewSceneIsAvailable)
            {
                Preview previewScene = this.previewScene as Preview;
                previewScene.SetSource(source);
                this.setPreviewData();
            }
        }

        private void setPreviewData()
        {
            switch (this.previewSource)
            {
                case Preview.Sources.Insert:
                    this.setInsertPreview();
                    this.radioButtonSourceInsert.Checked = true;
                    break;
                case Preview.Sources.Stage:
                    this.setStagePreview();
                    this.radioButtonSourceStage.Checked = true;
                    break;
            }
        }

        protected void setInsertPreview()
        {
            if (this.previewSceneIsAvailable)
            {
                this.setTasksPreview();
                this.setScorePreview();
                this.setSolutionPreview();
                this.setTimerPreview();
                this.setTimeoutPreview();
            }
        }

        protected void setTasksPreview()
        {
            if (this.previewSceneIsAvailable)
            {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetTasks(previewScene.Insert.Game, new List<DatasetContent>(this.business.DataList), new List<DatasetContent>());
                previewScene.Insert.Game.SetIn();
            }
        }

        protected override void setScorePreview()
        {
            if (this.previewSceneIsAvailable)
            {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetScore(previewScene.Insert.Score, 2, 3);
                previewScene.Insert.Score.SetIn();
            }
        }

        protected void setSolutionPreview()
        {
            if (this.previewSceneIsAvailable)
            {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetSolution(previewScene.Insert.TextInsert, this.selectedDataset);
                previewScene.Insert.TextInsert.SetIn();
            }
        }

        protected override void setTimerPreview()
        {
            if (this.previewSceneIsAvailable)
            {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetTimer(previewScene.Insert.Timer);
                previewScene.Insert.Timer.SetIn();
            }
        }

        protected void setTimeoutPreview()
        {
            if (this.previewSceneIsAvailable)
            {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetTimeout(previewScene.Insert.Timeout);
                previewScene.Insert.Timeout.SetLeftRightToGreen(this.business.TimeoutDuration);
            }
        }

        protected void setStagePreview()
        {
            if (this.previewSceneIsAvailable)
            {
                Preview previewScene = this.previewScene as Preview;
                if (this.selectedDataset is DatasetContent)
                {
                    previewScene.Stage.SetHeadline(this.selectedDataset.HostText);
                    string text = string.Empty;
                    foreach (DatasetContent datasetContent in this.business.DataList) 
                    {
                        if (!string.IsNullOrEmpty(text)) text += "\r\n";
                        text += datasetContent.Text;
                    }
                    previewScene.Stage.SetText(text);
                }
                previewScene.Stage.SetIn();
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
                //if (e.PropertyName == "Text") this.textBoxDatasetText.Text = this.selectedDataset.Text;
            }
        }

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setPreviewData();
        }

        #endregion

        #region Events.Controls

        private void UserControlContent_BackColorChanged(object sender, EventArgs e)
        {
            foreach (TabPage item in this.tabControl.TabPages) item.BackColor = this.BackColor;
        }

        private void numericUpDownTaskInsertPositionX_ValueChanged(object sender, EventArgs e)
        {
            this.business.TaskInsertPositionX = (int)this.numericUpDownTaskInsertPositionX.Value;
            this.setTasksPreview();
        }
        private void numericUpDownTaskInsertPositionY_ValueChanged(object sender, EventArgs e)
        {
            this.business.TaskInsertPositionY = (int)this.numericUpDownTaskInsertPositionY.Value;
            this.setTasksPreview();
        }

        protected virtual void numericUpDownTextInsertPositionX_ValueChanged(object sender, EventArgs e) 
        { 
            this.business.TextInsertPositionX = (int)this.numericUpDownTextInsertPositionX.Value;
            this.setSolutionPreview();
        }
        protected virtual void numericUpDownTextInsertPositionY_ValueChanged(object sender, EventArgs e) 
        { 
            this.business.TextInsertPositionY = (int)this.numericUpDownTextInsertPositionY.Value;
            this.setSolutionPreview();
        }
        protected virtual void comboBoxTextInsertStyle_SelectedIndexChanged(object sender, EventArgs e) 
        {
            VentuzScenes.GamePool._Modules.TextInsert.Styles style;
            if (Enum.TryParse(this.comboBoxTextInsertStyle.Text, out style)) this.business.TextInsertStyle = style;
            this.setSolutionPreview();
        }

        protected virtual void numericUpDownTimeoutPositionX_ValueChanged(object sender, EventArgs e)
        {
            if (this.business is Business)
            {
                this.business.TimeoutPositionX = (int)this.numericUpDownTimeoutPositionX.Value;
                this.setTimeoutPreview();
            }
        }
        protected virtual void numericUpDownTimeoutPositionY_ValueChanged(object sender, EventArgs e)
        {
            if (this.business is Business)
            {
                this.business.TimeoutPositionY = (int)this.numericUpDownTimeoutPositionY.Value;
                this.setTimeoutPreview();
            }
        }
        protected virtual void checkBoxTimeoutIsVisible_Click(object sender, EventArgs e)
        {
            if (this.business is Business)
            {
                this.business.TimeoutIsVisible = this.checkBoxTimeoutIsVisible.Checked;
                this.setTimeoutPreview();
            }
        }

        private void radioButtonSourceInsert_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourceInsert.Checked) this.previewSource = Preview.Sources.Insert; }
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

        private void buttonDataAddNewSet_Click(object sender, EventArgs e) {
            int listIndex = this.listBoxDataList.SelectedIndex + 1;
            this.business.AddDataset(new DatasetContent("?"), listIndex);
            this.selectDataset(listIndex);
        }
        private void buttonDataRemoveSet_Click(object sender, EventArgs e) { 
            if (this.business.TryRemoveDataset(this.selectedDatasetIndex)) this.selectDataset(this.selectedDatasetIndex - 1);
        }
        private void buttonDataRemoveAllSets_Click(object sender, EventArgs e) { 
            this.business.RemoveAllDatasets();
            this.selectDataset(0);
        }

        private void textBoxDataSetText_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Text = this.textBoxDatasetText.Text; }
        private void textBoxDatasetSolution_TextChanged(object sender, EventArgs e) 
        { 
            if (this.selectedDataset is DatasetContent) this.selectedDataset.Solution = this.textBoxDatasetSolution.Text;
            this.setInsertPreview();
        }
        private void textBoxDatasetHostText_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.HostText = this.textBoxDatasetHostText.Text; }

        #endregion

    }
}
