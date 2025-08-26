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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.Select1PictureOf4;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.Select1PictureOf4 {

    public partial class UserControlContent : _Base.BuzzerScore.UserControlContent {

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

        private bool previewSceneIsAvailable { get { return this.previewScene is Insert && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

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
                this.pictureBoxDatasetPicture.Image = this.selectedDataset.Picture;
                this.textBoxDatasetDescription1.Text = this.selectedDataset.Description1;
                this.textBoxDatasetDescription2.Text = this.selectedDataset.Description2;
                this.textBoxDatasetDescription3.Text = this.selectedDataset.Description3;
                this.textBoxDatasetDescription4.Text = this.selectedDataset.Description4;
                this.textBoxDatasetName.Text = this.selectedDataset.Name;
                this.numericUpDownSolutionID.Value = this.selectedDataset.SolutionID;
                this.pictureBoxDatasetSolution.Image = this.selectedDataset.Solution;
                this.textBoxDatasetCredits.Text = this.selectedDataset.Credits;
                this.buttonDataRemoveSet.Enabled = true;
                if (this.listBoxDataList.Items.Count > this.selectedDatasetIndex) this.listBoxDataList.SelectedIndex = this.selectedDatasetIndex;
            }
            else {
                this.groupBoxDataset.Enabled = false;
                this.pictureBoxDatasetPicture.Image = null;
                this.textBoxDatasetDescription1.Text = string.Empty;
                this.textBoxDatasetDescription2.Text = string.Empty;
                this.textBoxDatasetDescription3.Text = string.Empty;
                this.textBoxDatasetDescription4.Text = string.Empty;
                this.textBoxDatasetName.Text = string.Empty;
                this.numericUpDownSolutionID.Value = 0;
                this.pictureBoxDatasetSolution.Image = null;
                this.textBoxDatasetCredits.Text = string.Empty;
                this.buttonDataRemoveSet.Enabled = false;
            }
            Helper.setControlBackColor(this.buttonDataRemoveSet);

            this.setDescriptionColor();

            this.adjustDataMoveSet();

            this.setDatasetPreview();

            this.setTaskCounterPreview();
        }

        private void adjustDataMoveSet() {
            this.buttonDataMoveSetUp.Enabled = this.listBoxDataList.SelectedIndex > 0;
            Helper.setControlBackColor(this.buttonDataMoveSetUp);

            this.buttonDataMoveSetDown.Enabled = this.listBoxDataList.SelectedIndex >= 0 && this.listBoxDataList.SelectedIndex < this.listBoxDataList.Items.Count - 1;
            Helper.setControlBackColor(this.buttonDataMoveSetDown);
        }

        private void setDescriptionColor() {
            if (this.selectedDataset is DatasetContent) {
                if (this.selectedDataset.SolutionID == 1) {
                    this.textBoxDatasetDescription1.ForeColor = Constants.ColorBuzzered;
                    this.textBoxDatasetDescription2.ForeColor = SystemColors.ControlText;
                    this.textBoxDatasetDescription3.ForeColor = SystemColors.ControlText;
                    this.textBoxDatasetDescription4.ForeColor = SystemColors.ControlText;
                }
                else if (this.selectedDataset.SolutionID == 2) {
                    this.textBoxDatasetDescription1.ForeColor = SystemColors.ControlText;
                    this.textBoxDatasetDescription2.ForeColor = Constants.ColorBuzzered;
                    this.textBoxDatasetDescription3.ForeColor = SystemColors.ControlText;
                    this.textBoxDatasetDescription4.ForeColor = SystemColors.ControlText;
                }
                else if (this.selectedDataset.SolutionID == 3) {
                    this.textBoxDatasetDescription1.ForeColor = SystemColors.ControlText;
                    this.textBoxDatasetDescription2.ForeColor = SystemColors.ControlText;
                    this.textBoxDatasetDescription3.ForeColor = Constants.ColorBuzzered;
                    this.textBoxDatasetDescription4.ForeColor = SystemColors.ControlText;
                }
                else if (this.selectedDataset.SolutionID == 4) {
                    this.textBoxDatasetDescription1.ForeColor = SystemColors.ControlText;
                    this.textBoxDatasetDescription2.ForeColor = SystemColors.ControlText;
                    this.textBoxDatasetDescription3.ForeColor = SystemColors.ControlText;
                    this.textBoxDatasetDescription4.ForeColor = Constants.ColorBuzzered;
                }
            }
            else {
                this.textBoxDatasetDescription1.ForeColor = SystemColors.ControlText;
                this.textBoxDatasetDescription2.ForeColor = SystemColors.ControlText;
                this.textBoxDatasetDescription3.ForeColor = SystemColors.ControlText;
                this.textBoxDatasetDescription4.ForeColor = SystemColors.ControlText;
            }
        }

        public override void SetPreviewPipe(
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            bool selected = ((Insert)this.previewScene) is VRemote4.HandlerSi.Scene;
            base.SetPreviewPipe(previewPipe);
            if (selected) this.Select();
        }

        public override void Select() {
            if (this.previewPipe is VRemote4.HandlerSi.Client.Pipe.Business &&
                this.previewPipe.Resolution.HasValue &&
                this.previewPipe.ShareHandle.HasValue) {
                base.select(new Insert(WindowsFormsSynchronizationContext.Current, this.previewPipe));
            }
        }

        protected override void setScorePreview() {
            if (this.previewSceneIsAvailable) {
                Insert previewScene = this.previewScene as Insert;
                previewScene.Score.SetPositionX(this.business.ScorePositionX);
                previewScene.Score.SetPositionY(this.business.ScorePositionY);
                previewScene.Score.SetStyle(this.business.ScoreStyle);
                previewScene.Score.SetLeftTopName(this.business.LeftPlayerName);
                previewScene.Score.SetLeftTopScore(2);
                previewScene.Score.SetRightBottomName(this.business.RightPlayerName);
                previewScene.Score.SetRightBottomScore(3);
                previewScene.Score.SetIn();
            }
        }

        protected override void setTimeoutPreview() {
            if (this.previewSceneIsAvailable) {
                Insert previewScene = this.previewScene as Insert;
                previewScene.Timeout.SetIsVisible(this.business.TimeoutIsVisible);
                previewScene.Timeout.SetPositionX(this.business.TimeoutPositionX);
                previewScene.Timeout.SetPositionY(this.business.TimeoutPositionY);
                previewScene.Timeout.SetLeftRightToGreen(this.business.TimeoutDuration);
                if (this.checkBoxShowContent.Checked) {
                    switch (this.business.ScoreStyle) {
                        case VentuzScenes.GamePool._Modules.Score.Styles.FourDots:
                            previewScene.SetScoreStyle(Insert.Styles.FourDots);
                            break;
                        case VentuzScenes.GamePool._Modules.Score.Styles.FiveDots:
                            previewScene.SetScoreStyle(Insert.Styles.FiveDots);
                            break;
                        case VentuzScenes.GamePool._Modules.Score.Styles.SixDots:
                            previewScene.SetScoreStyle(Insert.Styles.SixDots);
                            break;
                        case VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots:
                        case VentuzScenes.GamePool._Modules.Score.Styles.SevenDots:
                        case VentuzScenes.GamePool._Modules.Score.Styles.Counter:
                        case VentuzScenes.GamePool._Modules.Score.Styles.CounterLeft:
                        case VentuzScenes.GamePool._Modules.Score.Styles.Sport:
                        default:
                            previewScene.SetScoreStyle(Insert.Styles.Counter);
                            break;
                    }
                    previewScene.SetIn();
                }
                else previewScene.SetOut();
            }
        }

        private void setDatasetPreview() {
            if (this.previewSceneIsAvailable) {
                Insert previewScene = this.previewScene as Insert;
                if (this.checkBoxShowContent.Checked &&
                    this.selectedDataset is DatasetContent) {
                    previewScene.SetPictureFilename(this.selectedDataset.PictureFilename);
                    previewScene.SetBorder(this.selectedDataset.SolutionID);
                }
                else {
                    previewScene.SetPictureFilename(string.Empty);
                    previewScene.Deselect();
                }
            }
        }

        protected void setTaskCounterPreview() {
            if (this.previewSceneIsAvailable) {
                Insert previewScene = this.previewScene as Insert; ;
                if (this.checkBoxShowContent.Checked) {
                    this.business.Vinsert_SetTaskCounter(previewScene.TaskCounter, 6);
                    previewScene.TaskCounter.SetIn();
                }
                else previewScene.TaskCounter.SetOut();
            }
        }


        #endregion


        #region Events.Incoming

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setScorePreview();
            this.setTimeoutPreview();
            this.setDatasetPreview();
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
                if (e.PropertyName == "PictureFilename") this.pictureBoxDatasetPicture.Image = this.selectedDataset.Picture;
                else if (e.PropertyName == "Description1") this.textBoxDatasetDescription1.Text = this.selectedDataset.Description1;
                else if (e.PropertyName == "Description2") this.textBoxDatasetDescription2.Text = this.selectedDataset.Description2;
                else if (e.PropertyName == "Description3") this.textBoxDatasetDescription3.Text = this.selectedDataset.Description3;
                else if (e.PropertyName == "Description4") this.textBoxDatasetDescription4.Text = this.selectedDataset.Description4;
                else if (e.PropertyName == "Name") this.textBoxDatasetName.Text = this.selectedDataset.Name;
                else if (e.PropertyName == "SolutionID") {
                    this.numericUpDownSolutionID.Value = this.selectedDataset.SolutionID;
                    this.setDescriptionColor();
                }
                else if (e.PropertyName == "SolutionFilename") this.pictureBoxDatasetSolution.Image = this.selectedDataset.Solution;
                else if (e.PropertyName == "Credits") this.textBoxDatasetCredits.Text = this.selectedDataset.Credits;
            }
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

        protected override void comboBoxScoreStyle_SelectedIndexChanged(object sender, EventArgs e) {
            base.comboBoxScoreStyle_SelectedIndexChanged(sender, e);
            this.setTimeoutPreview();
        }

        private void checkBoxShowContent_CheckedChanged(object sender, EventArgs e) {
            this.setTimeoutPreview();
            this.setDatasetPreview();
            this.setTaskCounterPreview();
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
            string filename = Helper.selectImageFile("select picture", string.Empty);
            if (filename != null) {
                int insertIndex = this.listBoxDataList.SelectedIndex + 1;
                this.business.AddDataset(new DatasetContent(filename), insertIndex);
                this.selectDataset(insertIndex);
            }
        }
        private void buttonDataRemoveSet_Click(object sender, EventArgs e) { 
            if (this.business.TryRemoveDataset(this.selectedDatasetIndex)) this.selectDataset(this.selectedDatasetIndex - 1);
        }
        private void buttonDataRemoveAllSets_Click(object sender, EventArgs e) { 
            this.business.RemoveAllDatasets();
            this.selectDataset(0);
        }

        private void pictureBoxDatasetPicture_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                string filename = Helper.selectImageFile("select picture", this.selectedDataset.PictureFilename);
                if (filename != null) this.selectedDataset.PictureFilename = filename;
            }
        }
        private void textBoxDatasetDescription1_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Description1 = this.textBoxDatasetDescription1.Text; }
        private void textBoxDatasetDescription2_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Description2 = this.textBoxDatasetDescription2.Text; }
        private void textBoxDatasetDescription3_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Description3 = this.textBoxDatasetDescription3.Text; }
        private void textBoxDatasetDescription4_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Description4 = this.textBoxDatasetDescription4.Text; }
        private void textBoxDataSetName_TextChanged(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                this.setDescriptionColor(); 
                this.selectedDataset.Name = this.textBoxDatasetName.Text;
            }
        }
        private void numericUpDownSolutionID_ValueChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.SolutionID = (int)this.numericUpDownSolutionID.Value; }
        private void pictureBoxDatasetSolution_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                string filename = Helper.selectImageFile("select solution", this.selectedDataset.SolutionFilename);
                if (filename != null) this.selectedDataset.SolutionFilename = filename;
            }
        }
        private void buttonDatasetClearSolution_Click(object sender, EventArgs e) { this.selectedDataset.SolutionFilename = string.Empty; }
        private void textBoxDataSetCredits_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Credits = this.textBoxDatasetCredits.Text; }

        #endregion
    }
}
