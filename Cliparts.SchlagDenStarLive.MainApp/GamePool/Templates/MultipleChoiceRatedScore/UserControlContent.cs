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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MultipleChoiceRatedScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.MultipleChoiceRatedScore {

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

            this.numericUpDownTaskCounterPositionX.Minimum = int.MinValue;
            this.numericUpDownTaskCounterPositionX.Maximum = int.MaxValue;

            this.numericUpDownTaskCounterPositionY.Minimum = int.MinValue;
            this.numericUpDownTaskCounterPositionY.Maximum = int.MaxValue;

            this.numericUpDownTaskCounterSize.Minimum = int.MinValue;
            this.numericUpDownTaskCounterSize.Maximum = int.MaxValue;

            this.numericUpDownInsertPositionX.Minimum = int.MinValue;
            this.numericUpDownInsertPositionX.Maximum = int.MaxValue;

            this.numericUpDownInsertPositionY.Minimum = int.MinValue;
            this.numericUpDownInsertPositionY.Maximum = int.MaxValue;

            this.comboBoxDataSetSolution.BeginUpdate();
            this.comboBoxDataSetSolution.Items.AddRange(Enum.GetNames(typeof(Game.SolutionItems)));
            this.comboBoxDataSetSolution.EndUpdate();
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

            bind = new Binding("Value", this.business, "InsertPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownInsertPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "InsertPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownInsertPositionY.DataBindings.Add(bind);

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

            this.numericUpDownInsertPositionX.DataBindings.Clear();
            this.numericUpDownInsertPositionY.DataBindings.Clear();

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
                this.textBoxDataSetHostText.Text = this.selectedDataset.HostText;
                this.textBoxDataSetAnswerA.Text = this.selectedDataset.AnswerA;
                this.textBoxDataSetAnswerB.Text = this.selectedDataset.AnswerB;
                this.textBoxDataSetAnswerC.Text = this.selectedDataset.AnswerC;
                this.textBoxDataSetAnswerD.Text = this.selectedDataset.AnswerD;
                this.textBoxDataSetAnswerAHost.Text = this.selectedDataset.AnswerAHost;
                this.textBoxDataSetAnswerBHost.Text = this.selectedDataset.AnswerBHost;
                this.textBoxDataSetAnswerCHost.Text = this.selectedDataset.AnswerCHost;
                this.textBoxDataSetAnswerDHost.Text = this.selectedDataset.AnswerDHost;

                this.pictureBoxDataSetSolutionMovie.Image = this.selectedDataset.SolutionMovie;
                this.buttonDataRemoveSet.Enabled = true;
                if (this.listBoxDataList.Items.Count > this.selectedDatasetIndex) this.listBoxDataList.SelectedIndex = this.selectedDatasetIndex;
            }
            else {
                this.groupBoxDataset.Enabled = false;

                this.textBoxDataSetName.Text = string.Empty;
                this.textBoxDataSetHostText.Text = string.Empty;
                this.textBoxDataSetAnswerA.Text = string.Empty;
                this.textBoxDataSetAnswerB.Text = string.Empty;
                this.textBoxDataSetAnswerC.Text = string.Empty;
                this.textBoxDataSetAnswerD.Text = string.Empty;
                this.textBoxDataSetAnswerAHost.Text = string.Empty;
                this.textBoxDataSetAnswerBHost.Text = string.Empty;
                this.textBoxDataSetAnswerCHost.Text = string.Empty;
                this.textBoxDataSetAnswerDHost.Text = string.Empty;

                this.pictureBoxDataSetSolutionMovie.Image = null;

                this.buttonDataRemoveSet.Enabled = false;
            }
            Helper.setControlBackColor(this.buttonDataRemoveSet);

            this.setSolution();

            this.adjustDataMoveSet();

            this.setPreviewData();
        }

        private void setSolution() {
            if (this.selectedDataset is DatasetContent) {
                switch (this.selectedDataset.Solution) {
                    case Game.SolutionItems.AnswerA:
                        this.textBoxDataSetAnswerA.BackColor = Constants.ColorEnabled;
                        this.textBoxDataSetAnswerB.BackColor = SystemColors.Control;
                        this.textBoxDataSetAnswerC.BackColor = SystemColors.Control;
                        this.textBoxDataSetAnswerD.BackColor = SystemColors.Control;
                        break;
                    case Game.SolutionItems.AnswerB:
                        this.textBoxDataSetAnswerA.BackColor = SystemColors.Control;
                        this.textBoxDataSetAnswerB.BackColor = Constants.ColorEnabled;
                        this.textBoxDataSetAnswerC.BackColor = SystemColors.Control;
                        this.textBoxDataSetAnswerD.BackColor = SystemColors.Control;
                        break;
                    case Game.SolutionItems.AnswerC:
                        this.textBoxDataSetAnswerA.BackColor = SystemColors.Control;
                        this.textBoxDataSetAnswerB.BackColor = SystemColors.Control;
                        this.textBoxDataSetAnswerC.BackColor = Constants.ColorEnabled;
                        this.textBoxDataSetAnswerD.BackColor = SystemColors.Control;
                        break;
                    case Game.SolutionItems.AnswerD:
                    default:
                        this.textBoxDataSetAnswerA.BackColor = SystemColors.Control;
                        this.textBoxDataSetAnswerB.BackColor = SystemColors.Control;
                        this.textBoxDataSetAnswerC.BackColor = SystemColors.Control;
                        this.textBoxDataSetAnswerD.BackColor = Constants.ColorEnabled;
                        break;
                }
                this.comboBoxDataSetSolution.Text = this.selectedDataset.Solution.ToString();
            }
            else {
                this.textBoxDataSetAnswerA.BackColor = SystemColors.Control;
                this.textBoxDataSetAnswerB.BackColor = SystemColors.Control;
                this.textBoxDataSetAnswerC.BackColor = SystemColors.Control;
                this.textBoxDataSetAnswerD.BackColor = SystemColors.Control;
            }
            this.textBoxDataSetAnswerAHost.BackColor = this.textBoxDataSetAnswerA.BackColor;
            this.textBoxDataSetAnswerBHost.BackColor = this.textBoxDataSetAnswerB.BackColor;
            this.textBoxDataSetAnswerCHost.BackColor = this.textBoxDataSetAnswerC.BackColor;
            this.textBoxDataSetAnswerDHost.BackColor = this.textBoxDataSetAnswerD.BackColor;
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
                this.setTimerPreview();
                this.setTaskCounterPreview();
                this.setContentPreview();
            }
        }

        protected override void setScorePreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetScore(previewScene.Insert.Score, 2, 3);
                previewScene.Insert.Score.SetIn();
            }
        }

        protected override void setTimerPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetTimer(previewScene.Insert.Timer, this.business.TimerStartTime);
                previewScene.Insert.Timer.SetIn();
            }
        }

        protected void setTaskCounterPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetTaskCounter(previewScene.Insert.TaskCounter, 6);
                previewScene.Insert.TaskCounter.SetIn();
            }
        }

        protected void setContentPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetContent(
                    previewScene.Insert.Game, 
                    this.selectedDataset,
                    1,2,3,4,
                    4,3,2,1);
                previewScene.Insert.Game.SetIn();
                previewScene.Insert.Game.SetAnswersSolutionIn();
            }
        }

        protected void setFullscreenPreview() {
            if (this.previewSceneIsAvailable) {
                //this.setBorderPreview();
                //this.setTimerPreview();
                //this.setPicturePreview();
                //this.setTaskCounterPreview();
            }
        }

        protected void setHostPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vhost_SetContent(previewScene.Host, this.selectedDataset, 1, 2, 3, 4, 4, 3, 2, 1);
                previewScene.Host.GameToIn();
            }
        }

        protected void setPlayerPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vplayer_SetContent(previewScene.Player, this.selectedDataset);
                previewScene.Player.GameToIn();
                previewScene.Player.UnlockInput();
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
                else if (e.PropertyName == "AnswerA") this.textBoxDataSetAnswerA.Text = this.selectedDataset.AnswerA;
                else if (e.PropertyName == "AnswerB") this.textBoxDataSetAnswerB.Text = this.selectedDataset.AnswerB;
                else if (e.PropertyName == "AnswerC") this.textBoxDataSetAnswerC.Text = this.selectedDataset.AnswerC;
                else if (e.PropertyName == "AnswerD") this.textBoxDataSetAnswerD.Text = this.selectedDataset.AnswerD;
                else if (e.PropertyName == "AnswerAHost") this.textBoxDataSetAnswerAHost.Text = this.selectedDataset.AnswerAHost;
                else if (e.PropertyName == "AnswerBHost") this.textBoxDataSetAnswerBHost.Text = this.selectedDataset.AnswerBHost;
                else if (e.PropertyName == "AnswerCHost") this.textBoxDataSetAnswerCHost.Text = this.selectedDataset.AnswerCHost;
                else if (e.PropertyName == "AnswerDHost") this.textBoxDataSetAnswerDHost.Text = this.selectedDataset.AnswerDHost;
                else if (e.PropertyName == "SolutionFilename") this.pictureBoxDataSetSolutionMovie.Image = this.selectedDataset.SolutionMovie;
                else if (e.PropertyName == "Solution") this.setSolution();
                this.setPreviewData();
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

        protected virtual void numericUpDownInsertPositionX_ValueChanged(object sender, EventArgs e) { 
            this.business.InsertPositionX = (int)this.numericUpDownInsertPositionX.Value;
            this.setContentPreview();
        }
        protected virtual void numericUpDownInsertPositionY_ValueChanged(object sender, EventArgs e) { 
            this.business.InsertPositionY = (int)this.numericUpDownInsertPositionY.Value;
            this.setContentPreview();
        }

        private void radioButtonSourceInsert_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourceInsert.Checked) this.previewSource = Preview.Sources.Insert; }
        private void radioButtonSourceFullscreen_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourceFullscreen.Checked) this.previewSource = Preview.Sources.Fullscreen; }
        private void radioButtonSourceHost_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourceHost.Checked) this.previewSource = Preview.Sources.Host; }
        private void radioButtonSourceHostPlayer_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonSourcePlayer.Checked) this.previewSource = Preview.Sources.Player; }


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

        private void textBoxDataSetName_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Name = this.textBoxDataSetName.Text; }
        private void textBoxDataSetHostText_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.HostText = this.textBoxDataSetHostText.Text; }
        private void textBoxDataSetAnswerA_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.AnswerA = this.textBoxDataSetAnswerA.Text; }
        private void textBoxDataSetAnswerB_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.AnswerB = this.textBoxDataSetAnswerB.Text; }
        private void textBoxDataSetAnswerC_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.AnswerC = this.textBoxDataSetAnswerC.Text; }
        private void textBoxDataSetAnswerD_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.AnswerD = this.textBoxDataSetAnswerD.Text; }
        private void textBoxDataSetAnswerAHost_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.AnswerAHost = this.textBoxDataSetAnswerAHost.Text; }
        private void textBoxDataSetAnswerBHost_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.AnswerBHost = this.textBoxDataSetAnswerBHost.Text; }
        private void textBoxDataSetAnswerCHost_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.AnswerCHost = this.textBoxDataSetAnswerCHost.Text; }
        private void textBoxDataSetAnswerDHost_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.AnswerDHost = this.textBoxDataSetAnswerDHost.Text; }

        private void pictureBoxDataSetSolutionMovie_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                string filename = Helper.selectVideoFile("select movie", this.selectedDataset.SolutionFilename);
                if (filename != null) this.selectedDataset.SolutionFilename = filename;
            }
        }

        private void comboBoxDataSetSolution_SelectedIndexChanged(object sender, EventArgs e) {
            Game.SolutionItems result;
            if (this.selectedDataset is DatasetContent &&
                Enum.TryParse(this.comboBoxDataSetSolution.Text, out result)) this.selectedDataset.Solution = result;
        }

        #endregion

    }
}
