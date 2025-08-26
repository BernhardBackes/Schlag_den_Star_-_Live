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

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.AddEstimations {

    public partial class UserControlContent : _Base.Score.UserControlContent {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;

        private int selectedDatasetIndex = -1;

        private bool previewSceneIsAvailable { get { return this.previewScene is VentuzScenes.GamePool.AddEstimations.Insert && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownSolution_00.Minimum = int.MinValue;
            this.numericUpDownSolution_00.Maximum = int.MaxValue;

            this.numericUpDownSolution_01.Minimum = int.MinValue;
            this.numericUpDownSolution_01.Maximum = int.MaxValue;

            this.numericUpDownSolution_02.Minimum = int.MinValue;
            this.numericUpDownSolution_02.Maximum = int.MaxValue;

            this.numericUpDownTextInsertPositionX.Minimum = int.MinValue;
            this.numericUpDownTextInsertPositionX.Maximum = int.MaxValue;

            this.numericUpDownTextInsertPositionY.Minimum = int.MinValue;
            this.numericUpDownTextInsertPositionY.Maximum = int.MaxValue;

            this.comboBoxTextInsertStyle.BeginUpdate();
            this.comboBoxTextInsertStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.TextInsert.Styles)));
            this.comboBoxTextInsertStyle.EndUpdate();

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

        public override void SetPreviewPipe(
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            bool selected = ((VentuzScenes.GamePool.Score.Insert)this.previewScene) is VRemote4.HandlerSi.Scene;
            base.SetPreviewPipe(previewPipe);
            if (selected) this.Select();
        }

        public override void Select() {
            if (this.previewPipe is VRemote4.HandlerSi.Client.Pipe.Business &&
                this.previewPipe.Resolution.HasValue &&
                this.previewPipe.ShareHandle.HasValue) {
                base.select(new VentuzScenes.GamePool.Score.Insert(WindowsFormsSynchronizationContext.Current, this.previewPipe));
            }
        }

        protected override void setScorePreview() {
            if (this.previewSceneIsAvailable) {
                VentuzScenes.GamePool.Score.Insert previewScene = this.previewScene as VentuzScenes.GamePool.Score.Insert;
                previewScene.Score.SetPositionX(this.business.ScorePositionX);
                previewScene.Score.SetPositionY(this.business.ScorePositionY);
                previewScene.Score.SetStyle(this.business.ScoreStyle);
                previewScene.Score.SetLeftTopName(this.business.LeftPlayerName);
                previewScene.Score.SetLeftTopScore(99);
                previewScene.Score.SetRightBottomName(this.business.RightPlayerName);
                previewScene.Score.SetRightBottomScore(88);
                previewScene.Score.SetIn();
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

            this.textBoxKeyword_00.DataBindings.Clear();
            this.textBoxHostText_00.DataBindings.Clear();
            this.numericUpDownSolution_00.DataBindings.Clear();

            this.textBoxKeyword_01.DataBindings.Clear();
            this.textBoxHostText_01.DataBindings.Clear();
            this.numericUpDownSolution_01.DataBindings.Clear();

            this.textBoxKeyword_02.DataBindings.Clear();
            this.textBoxHostText_02.DataBindings.Clear();
            this.numericUpDownSolution_02.DataBindings.Clear();

            this.textBoxSolution.DataBindings.Clear();

            //Dispose...
            if (this.selectedDataset is DatasetContent) {

            }
            this.selectedDataset = selectedDataset;
            //Pose...
            if (this.selectedDataset is DatasetContent) {
                Binding bind;

                bind = new Binding("Text", this.selectedDataset.ItemList[0], "Keyword");
                bind.Format += (s, e) => { e.Value = (string)e.Value; };
                this.textBoxKeyword_00.DataBindings.Add(bind);

                bind = new Binding("Text", this.selectedDataset.ItemList[0], "HostText");
                bind.Format += (s, e) => { e.Value = (string)e.Value; };
                this.textBoxHostText_00.DataBindings.Add(bind);

                bind = new Binding("Value", this.selectedDataset.ItemList[0], "Solution");
                bind.Format += (s, e) => { e.Value = Convert.ToInt32(e.Value); };
                this.numericUpDownSolution_00.DataBindings.Add(bind);

                bind = new Binding("Text", this.selectedDataset.ItemList[1], "Keyword");
                bind.Format += (s, e) => { e.Value = (string)e.Value; };
                this.textBoxKeyword_01.DataBindings.Add(bind);

                bind = new Binding("Text", this.selectedDataset.ItemList[1], "HostText");
                bind.Format += (s, e) => { e.Value = (string)e.Value; };
                this.textBoxHostText_01.DataBindings.Add(bind);

                bind = new Binding("Value", this.selectedDataset.ItemList[1], "Solution");
                bind.Format += (s, e) => { e.Value = Convert.ToInt32(e.Value); };
                this.numericUpDownSolution_01.DataBindings.Add(bind);

                bind = new Binding("Text", this.selectedDataset.ItemList[2], "Keyword");
                bind.Format += (s, e) => { e.Value = (string)e.Value; };
                this.textBoxKeyword_02.DataBindings.Add(bind);

                bind = new Binding("Text", this.selectedDataset.ItemList[2], "HostText");
                bind.Format += (s, e) => { e.Value = (string)e.Value; };
                this.textBoxHostText_02.DataBindings.Add(bind);

                bind = new Binding("Value", this.selectedDataset.ItemList[2], "Solution");
                bind.Format += (s, e) => { e.Value = Convert.ToInt32(e.Value); };
                this.numericUpDownSolution_02.DataBindings.Add(bind);

                bind = new Binding("Text", this.selectedDataset, "Solution");
                bind.Format += (s, e) => { e.Value = Convert.ToInt32(e.Value).ToString("#,##0"); };
                this.textBoxSolution.DataBindings.Add(bind);

            }

            if (this.selectedDataset is DatasetContent) {
                this.groupBoxDataset.Enabled = true;
                this.buttonDataRemoveSet.Enabled = true;
                if (this.listBoxDataList.Items.Count > this.selectedDatasetIndex) this.listBoxDataList.SelectedIndex = this.selectedDatasetIndex;
            }
            else {
                this.groupBoxDataset.Enabled = false;
                this.buttonDataRemoveSet.Enabled = false;
                this.textBoxKeyword_00.Text = string.Empty;
                this.textBoxHostText_00.Text = string.Empty;
                this.numericUpDownSolution_00.Value = 0;
                this.textBoxKeyword_01.Text = string.Empty;
                this.textBoxHostText_01.Text = string.Empty;
                this.numericUpDownSolution_01.Value = 0;
                this.textBoxKeyword_02.Text = string.Empty;
                this.textBoxHostText_02.Text = string.Empty;
                this.numericUpDownSolution_02.Value = 0;
                this.textBoxSolution.Text = "-";
            }
            Helper.setControlBackColor(this.buttonDataRemoveSet);

            this.adjustDataMoveSet();

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
            this.setScorePreview();
        }

        void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "NameList") this.fillDataList();
                else if (e.PropertyName == "SampleIncluded") this.fillDataList();
            }
        }

        #endregion

        #region Events.Controls

        protected virtual void numericUpDownTextInsertPositionX_ValueChanged(object sender, EventArgs e) { this.business.TextInsertPositionX = (int)this.numericUpDownTextInsertPositionX.Value; }
        protected virtual void numericUpDownTextInsertPositionY_ValueChanged(object sender, EventArgs e) { this.business.TextInsertPositionY = (int)this.numericUpDownTextInsertPositionY.Value; }
        protected virtual void comboBoxTextInsertStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.TextInsert.Styles style;
            if (Enum.TryParse(this.comboBoxTextInsertStyle.Text, out style)) this.business.TextInsertStyle = style;
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

        #endregion
    }

}
