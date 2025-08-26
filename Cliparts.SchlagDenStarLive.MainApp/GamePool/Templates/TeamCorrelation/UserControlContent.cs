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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TeamCorrelation;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TeamCorrelation {

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

            this.numericUpDownGamePositionX.Minimum = int.MinValue;
            this.numericUpDownGamePositionX.Maximum = int.MaxValue;

            this.numericUpDownGamePositionY.Minimum = int.MinValue;
            this.numericUpDownGamePositionY.Maximum = int.MaxValue;

            this.numericUpDownGameScaling.Minimum = int.MinValue;
            this.numericUpDownGameScaling.Maximum = int.MaxValue;

            this.numericUpDownTextInsertPositionX.Minimum = int.MinValue;
            this.numericUpDownTextInsertPositionX.Maximum = int.MaxValue;

            this.numericUpDownTextInsertPositionY.Minimum = int.MinValue;
            this.numericUpDownTextInsertPositionY.Maximum = int.MaxValue;

            this.numericUpDownTaskCounterPositionX.Minimum = int.MinValue;
            this.numericUpDownTaskCounterPositionX.Maximum = int.MaxValue;

            this.numericUpDownTaskCounterPositionY.Minimum = int.MinValue;
            this.numericUpDownTaskCounterPositionY.Maximum = int.MaxValue;

            this.numericUpDownTaskCounterSize.Minimum = int.MinValue;
            this.numericUpDownTaskCounterSize.Maximum = int.MaxValue;

        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.business, "GamePositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownGamePositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "GamePositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownGamePositionY.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "GameScaling");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownGameScaling.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TextInsertPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTextInsertPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TextInsertPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTextInsertPositionY.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TaskCounterPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTaskCounterPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TaskCounterPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTaskCounterPositionY.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TaskCounterSize");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTaskCounterSize.DataBindings.Add(bind);

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

            this.numericUpDownGamePositionX.DataBindings.Clear();
            this.numericUpDownGamePositionY.DataBindings.Clear();
            this.numericUpDownGameScaling.DataBindings.Clear();

            this.numericUpDownTextInsertPositionX.DataBindings.Clear();
            this.numericUpDownTextInsertPositionY.DataBindings.Clear();

            this.numericUpDownTaskCounterPositionX.DataBindings.Clear();
            this.numericUpDownTaskCounterPositionY.DataBindings.Clear();
            this.numericUpDownTaskCounterSize.DataBindings.Clear();

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
                this.textBoxDataSetText.Text = this.selectedDataset.Text;
                this.textBoxDataSetHostText.Text = this.selectedDataset.HostText;
                this.pictureBoxDataSetItem1.Image = this.selectedDataset.Item1Image;
                this.textBoxDataSetItem1.Text = this.selectedDataset.Item1Text;
                this.pictureBoxDataSetItem2.Image = this.selectedDataset.Item2Image;
                this.textBoxDataSetItem2.Text = this.selectedDataset.Item2Text;
                this.pictureBoxDataSetItem3.Image = this.selectedDataset.Item3Image;
                this.textBoxDataSetItem3.Text = this.selectedDataset.Item3Text;
                this.pictureBoxDataSetItem4.Image = this.selectedDataset.Item4Image;
                this.textBoxDataSetItem4.Text = this.selectedDataset.Item4Text;
                this.radioButtonDatasetPositionDesk.Checked = this.selectedDataset.Position == PositionValues.Desk;
                this.radioButtonDatasetPositionTablet.Checked = this.selectedDataset.Position == PositionValues.Tablet;
                this.buttonDataRemoveSet.Enabled = true;
                if (this.listBoxDataList.Items.Count > this.selectedDatasetIndex) this.listBoxDataList.SelectedIndex = this.selectedDatasetIndex;
            }
            else {
                this.groupBoxDataset.Enabled = false;
                this.textBoxDataSetText.Text = string.Empty;
                this.textBoxDataSetHostText.Text = string.Empty;
                this.pictureBoxDataSetItem1.Image = null;
                this.textBoxDataSetItem1.Text = string.Empty;
                this.pictureBoxDataSetItem2.Image = null;
                this.textBoxDataSetItem2.Text = string.Empty;
                this.pictureBoxDataSetItem3.Image = null;
                this.textBoxDataSetItem3.Text = string.Empty;
                this.pictureBoxDataSetItem4.Image = null;
                this.textBoxDataSetItem4.Text = string.Empty;
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
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe)
        {
            bool selected = ((Preview)this.previewScene) is VRemote4.HandlerSi.Scene;
            base.SetPreviewPipe(previewPipe);
            if (selected) this.Select();
        }

        public override void Select()
        {
            if (this.previewPipe is VRemote4.HandlerSi.Client.Pipe.Business &&
                this.previewPipe.Resolution.HasValue &&
                this.previewPipe.ShareHandle.HasValue)
            {
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

        protected void setInsertPreview()
        {
            if (this.previewSceneIsAvailable)
            {
                this.setScorePreview();
                this.setGamePreview();
                this.setTaskCounterPreview();
            }
        }

        protected override void setScorePreview()
        {
            if (this.previewSceneIsAvailable)
            {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetScore(previewScene.Insert.Score);
                previewScene.Insert.Score.SetIn();
            }
        }

        protected void setGamePreview()
        {
            if (this.previewSceneIsAvailable)
            {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetGame(previewScene.Insert.Game, this.selectedDataset, "1234", "4321");
                previewScene.Insert.Game.SetIn();
            }
        }

        protected void setTaskCounterPreview()
        {
            if (this.previewSceneIsAvailable)
            {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetTaskCounter(previewScene.Insert.TaskCounter, 6);
                previewScene.Insert.TaskCounter.SetIn();
            }
        }

        protected void setHostPreview()
        {
            if (this.previewSceneIsAvailable)
            {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vhost_SetContent(previewScene.Host, this.selectedDataset, "1234", "1432", "4321", "");
                previewScene.Host.SetIn();
            }
        }

        protected void setPlayerPreview()
        {
            if (this.previewSceneIsAvailable)
            {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vplayer_SetContent(previewScene.Player, this.selectedDataset);
                previewScene.Player.SetIn();
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
                if (e.PropertyName == "Text") this.textBoxDataSetText.Text = this.selectedDataset.Text;
                else if (e.PropertyName == "HostText") this.textBoxDataSetHostText.Text = this.selectedDataset.HostText;
                else if (e.PropertyName == "Position")
                {
                    this.radioButtonDatasetPositionDesk.Checked = this.selectedDataset.Position == PositionValues.Desk;
                    this.radioButtonDatasetPositionTablet.Checked = this.selectedDataset.Position == PositionValues.Tablet;
                }
                else if (e.PropertyName == "Item1Filename") this.pictureBoxDataSetItem1.Image = this.selectedDataset.Item1Image;
                else if (e.PropertyName == "Item1Text") this.textBoxDataSetItem1.Text = this.selectedDataset.Item1Text;
                else if (e.PropertyName == "Item2Filename") this.pictureBoxDataSetItem2.Image = this.selectedDataset.Item2Image;
                else if (e.PropertyName == "Item2Text") this.textBoxDataSetItem2.Text = this.selectedDataset.Item2Text;
                else if (e.PropertyName == "Item3Filename") this.pictureBoxDataSetItem3.Image = this.selectedDataset.Item3Image;
                else if (e.PropertyName == "Item3Text") this.textBoxDataSetItem3.Text = this.selectedDataset.Item3Text;
                else if (e.PropertyName == "Item4Filename") this.pictureBoxDataSetItem4.Image = this.selectedDataset.Item4Image;
                else if (e.PropertyName == "Item4Text") this.textBoxDataSetItem4.Text = this.selectedDataset.Item4Text;
            }
        }

        #endregion

        #region Events.Controls

        protected virtual void numericUpDownTextInsertPositionX_ValueChanged(object sender, EventArgs e) { this.business.TextInsertPositionX = (int)this.numericUpDownTextInsertPositionX.Value; }
        protected virtual void numericUpDownTextInsertPositionY_ValueChanged(object sender, EventArgs e) { this.business.TextInsertPositionY = (int)this.numericUpDownTextInsertPositionY.Value; }

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

        private void numericUpDownGamePositionX_ValueChanged(object sender, EventArgs e)
        {
            this.business.GamePositionX = (int)this.numericUpDownGamePositionX.Value;
            if (this.previewSource == Preview.Sources.Insert) this.setGamePreview();
        }
        private void numericUpDownGamePositionY_ValueChanged(object sender, EventArgs e)
        {
            this.business.GamePositionY = (int)this.numericUpDownGamePositionY.Value;
            if (this.previewSource == Preview.Sources.Insert) this.setGamePreview();
        }
        private void numericUpDownGameScaling_ValueChanged(object sender, EventArgs e)
        {
            this.business.GameScaling = (int)this.numericUpDownGameScaling.Value;
            if (this.previewSource == Preview.Sources.Insert) this.setGamePreview();
        }

        private void textBoxLeftTeamTabletHostname_TextChanged(object sender, EventArgs e) { this.business.LeftTeamTabletHostname = this.textBoxLeftTeamTabletHostname.Text; }
        private void textBoxRightTeamTabletHostname_TextChanged(object sender, EventArgs e) { this.business.RightTeamTabletHostname = this.textBoxRightTeamTabletHostname.Text; }

        private void radioButtonSourceInsert_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourceInsert.Checked) this.previewSource = Preview.Sources.Insert; }
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
            int listIndex = this.listBoxDataList.SelectedIndex + 1;
            this.business.AddDataset(new DatasetContent("?"), listIndex);
            this.selectDataset(listIndex);
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

        private void textBoxDataSetText_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Text = this.textBoxDataSetText.Text; }
        private void textBoxDataSetHostText_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.HostText = this.textBoxDataSetHostText.Text; }
        private void radioButtonDatasetPositionDesk_CheckedChanged(object sender, EventArgs e)
        {
            if (this.selectedDataset is DatasetContent &&
                this.radioButtonDatasetPositionDesk.Checked) this.selectedDataset.Position = PositionValues.Desk;
        }
        private void radioButtonDatasetPositionTablet_CheckedChanged(object sender, EventArgs e)
        {
            if (this.selectedDataset is DatasetContent &&
                this.radioButtonDatasetPositionTablet.Checked) this.selectedDataset.Position = PositionValues.Tablet;
        }
        private void pictureBoxDataSetItem1_Click(object sender, EventArgs e)
        {
            if (this.selectedDataset is DatasetContent)
            {
                string filename = Helper.selectImageFile("select image", this.selectedDataset.Item1Filename);
                if (filename != null) this.selectedDataset.Item1Filename = filename;
            }
        }
        private void textBoxDataSetItem1_TextChanged(object sender, EventArgs e)
        {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.Item1Text = this.textBoxDataSetItem1.Text;
        }
        private void pictureBoxDataSetItem2_Click(object sender, EventArgs e)
        {
            if (this.selectedDataset is DatasetContent)
            {
                string filename = Helper.selectImageFile("select image", this.selectedDataset.Item2Filename);
                if (filename != null) this.selectedDataset.Item2Filename = filename;
            }
        }
        private void textBoxDataSetItem2_TextChanged(object sender, EventArgs e)
        {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.Item2Text = this.textBoxDataSetItem2.Text;
        }
        private void pictureBoxDataSetItem3_Click(object sender, EventArgs e)
        {
            if (this.selectedDataset is DatasetContent)
            {
                string filename = Helper.selectImageFile("select image", this.selectedDataset.Item3Filename);
                if (filename != null) this.selectedDataset.Item3Filename = filename;
            }
        }
        private void textBoxDataSetItem3_TextChanged(object sender, EventArgs e)
        {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.Item3Text = this.textBoxDataSetItem3.Text;
        }
        private void pictureBoxDataSetItem4_Click(object sender, EventArgs e)
        {
            if (this.selectedDataset is DatasetContent)
            {
                string filename = Helper.selectImageFile("select image", this.selectedDataset.Item4Filename);
                if (filename != null) this.selectedDataset.Item4Filename = filename;
            }
        }
        private void textBoxDataSetItem4_TextChanged(object sender, EventArgs e)
        {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.Item4Text = this.textBoxDataSetItem4.Text;
        }

        #endregion

    }
}
