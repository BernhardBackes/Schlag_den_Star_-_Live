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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MathematicalFormulaScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.MathematicalFormulaScore {

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

            this.numericUpDownGameInsertPositionX.Minimum = int.MinValue;
            this.numericUpDownGameInsertPositionX.Maximum = int.MaxValue;

            this.numericUpDownTaskCounterPositionX.Minimum = int.MinValue;
            this.numericUpDownTaskCounterPositionX.Maximum = int.MaxValue;

            this.numericUpDownTaskCounterPositionY.Minimum = int.MinValue;
            this.numericUpDownTaskCounterPositionY.Maximum = int.MaxValue;

            this.numericUpDownTaskCounterSize.Minimum = int.MinValue;
            this.numericUpDownTaskCounterSize.Maximum = int.MaxValue;

            this.numericUpDownGameInsertPositionY.Minimum = int.MinValue;
            this.numericUpDownGameInsertPositionY.Maximum = int.MaxValue;

            this.comboBoxDatasetSize.Items.AddRange(Enum.GetNames(typeof(Game.Sizes)));

            this.numericUpDownDatasetNumber_01.Minimum = int.MinValue;
            this.numericUpDownDatasetNumber_01.Maximum = int.MaxValue;

            this.numericUpDownDatasetNumber_02.Minimum = int.MinValue;
            this.numericUpDownDatasetNumber_02.Maximum = int.MaxValue;

            this.numericUpDownDatasetNumber_03.Minimum = int.MinValue;
            this.numericUpDownDatasetNumber_03.Maximum = int.MaxValue;

            this.numericUpDownDatasetNumber_04.Minimum = int.MinValue;
            this.numericUpDownDatasetNumber_04.Maximum = int.MaxValue;

            this.comboBoxDatasetOperation_01.Items.AddRange(Enum.GetNames(typeof(Game.Operations)));

            this.comboBoxDatasetOperation_02.Items.AddRange(Enum.GetNames(typeof(Game.Operations)));

            this.comboBoxDatasetOperation_03.Items.AddRange(Enum.GetNames(typeof(Game.Operations)));
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

            bind = new Binding("Value", this.business, "GameInsertPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownGameInsertPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "GameInsertPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownGameInsertPositionY.DataBindings.Add(bind);

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

            this.numericUpDownGameInsertPositionX.DataBindings.Clear();
            this.numericUpDownGameInsertPositionY.DataBindings.Clear();

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

        protected void setInsertPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.setContentPreview();
                previewScene.Insert.Game.SetIn();
                previewScene.Insert.Game.SetToBorderIn();
                this.setScorePreview();
                this.setTaskCounterPreview();
                previewScene.Insert.Score.SetIn();
            }
        }

        protected override void setScorePreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetScore(previewScene.Insert.Score, 88, 99);
            }
        }

        protected void setTaskCounterPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.previewSource == Preview.Sources.Insert) {
                    this.business.Vinsert_SetTaskCounter(previewScene.Insert.TaskCounter, 6);
                    previewScene.Insert.TaskCounter.SetIn();
                }
            }
        }

        protected void setContentPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.selectedDataset is DatasetContent) {
                    this.business.Vinsert_SetGameInsert(
                        previewScene.Insert.Game, 
                        this.selectedDataset,
                        null,
                        null,
                        null,
                        this.selectedDataset.Solution,
                        true);
                }
            }
        }

        protected void setHostPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vhost_Set(previewScene.Host, this.selectedDataset, "10 x 10 : 10 = 10", true);
                previewScene.Host.SetIn();
            }
        }

        protected void setPlayerPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vplayers_Set(previewScene.Player, this.selectedDataset);
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

            this.comboBoxDatasetSize.DataBindings.Clear();
            this.numericUpDownDatasetNumber_01.DataBindings.Clear();
            this.numericUpDownDatasetNumber_02.DataBindings.Clear();
            this.numericUpDownDatasetNumber_03.DataBindings.Clear();
            this.numericUpDownDatasetNumber_04.DataBindings.Clear();
            this.comboBoxDatasetOperation_01.DataBindings.Clear();
            this.comboBoxDatasetOperation_02.DataBindings.Clear();
            this.comboBoxDatasetOperation_03.DataBindings.Clear();
            this.textBoxDatasetSolution.DataBindings.Clear();

            //Dispose...
            if (this.selectedDataset is DatasetContent) {
                this.selectedDataset.PropertyChanged -= this.selectedDataset_PropertyChanged;
            }
            this.selectedDataset = selectedDataset;
            //Pose...
            if (this.selectedDataset is DatasetContent) {

                this.selectedDataset.PropertyChanged += this.selectedDataset_PropertyChanged;

                Binding bind;

                bind = new Binding("Text", this.selectedDataset, "Size");
                bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
                this.comboBoxDatasetSize.DataBindings.Add(bind);

                bind = new Binding("Value", this.selectedDataset, "Number_01");
                bind.Format += (s, e) => { e.Value = Convert.ToInt32(e.Value); };
                this.numericUpDownDatasetNumber_01.DataBindings.Add(bind);

                bind = new Binding("Value", this.selectedDataset, "Number_02");
                bind.Format += (s, e) => { e.Value = Convert.ToInt32(e.Value); };
                this.numericUpDownDatasetNumber_02.DataBindings.Add(bind);

                bind = new Binding("Value", this.selectedDataset, "Number_03");
                bind.Format += (s, e) => { e.Value = Convert.ToInt32(e.Value); };
                this.numericUpDownDatasetNumber_03.DataBindings.Add(bind);

                bind = new Binding("Value", this.selectedDataset, "Number_04");
                bind.Format += (s, e) => { e.Value = Convert.ToInt32(e.Value); };
                this.numericUpDownDatasetNumber_04.DataBindings.Add(bind);

                bind = new Binding("Text", this.selectedDataset, "Operation_01");
                bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
                this.comboBoxDatasetOperation_01.DataBindings.Add(bind);

                bind = new Binding("Text", this.selectedDataset, "Operation_02");
                bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
                this.comboBoxDatasetOperation_02.DataBindings.Add(bind);

                bind = new Binding("Text", this.selectedDataset, "Operation_03");
                bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
                this.comboBoxDatasetOperation_03.DataBindings.Add(bind);

                bind = new Binding("Text", this.selectedDataset, "Solution");
                bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
                this.textBoxDatasetSolution.DataBindings.Add(bind);

                this.groupBoxDataset.Enabled = true;
                this.buttonDataRemoveSet.Enabled = true;
                if (this.listBoxDataList.Items.Count > this.selectedDatasetIndex) this.listBoxDataList.SelectedIndex = this.selectedDatasetIndex;

                this.textBoxDatasetFormula.Text = this.selectedDataset.ToString();

                this.adjustSize(this.selectedDataset.Size);
            }
            else {
                this.groupBoxDataset.Enabled = false;
                this.buttonDataRemoveSet.Enabled = false;

                this.numericUpDownDatasetNumber_01.Value = 0;
                this.numericUpDownDatasetNumber_02.Value = 0;
                this.numericUpDownDatasetNumber_03.Value = 0;
                this.numericUpDownDatasetNumber_04.Value = 0;

                this.comboBoxDatasetOperation_01.SelectedIndex = 0;
                this.comboBoxDatasetOperation_02.SelectedIndex = 0;
                this.comboBoxDatasetOperation_03.SelectedIndex = 0;

                this.textBoxDatasetSolution.Text = string.Empty;

                this.adjustSize(Game.Sizes.TwoOperations);
            }
            Helper.setControlBackColor(this.buttonDataRemoveSet);

            this.adjustDataMoveSet();

            this.setPreviewData();

        }

        private void adjustSize(
            Game.Sizes value) {
            this.comboBoxDatasetOperation_03.Enabled = value == Game.Sizes.ThreeOperations;
            this.numericUpDownDatasetNumber_04.Enabled = value == Game.Sizes.ThreeOperations;

            Helper.setControlBackColor(this.comboBoxDatasetOperation_03);
            Helper.setControlBackColor(this.numericUpDownDatasetNumber_04);
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
                if (e.PropertyName == "Formula") this.textBoxDatasetFormula.Text = this.selectedDataset.ToString();
                else if (e.PropertyName == "Size") this.adjustSize(this.selectedDataset.Size);
                this.setPreviewData(); 
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

        protected virtual void numericUpDownGameInsertPositionX_ValueChanged(object sender, EventArgs e) { 
            this.business.GameInsertPositionX = (int)this.numericUpDownGameInsertPositionX.Value;
            this.setContentPreview();
        }
        protected virtual void numericUpDownGameInsertPositionY_ValueChanged(object sender, EventArgs e) { 
            this.business.GameInsertPositionY = (int)this.numericUpDownGameInsertPositionY.Value;
            this.setContentPreview();
        }

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

        private void comboBoxDatasetSize_SelectedIndexChanged(object sender, EventArgs e) {
            Game.Sizes result;
            if (Enum.TryParse(this.comboBoxDatasetSize.Text, out result) && this.selectedDataset is DatasetContent) this.selectedDataset.Size = result;
        }

        private void numericUpDownDatasetNumber_01_ValueChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Number_01 = (int)this.numericUpDownDatasetNumber_01.Value; }
        private void numericUpDownDatasetNumber_02_ValueChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Number_02 = (int)this.numericUpDownDatasetNumber_02.Value; }
        private void numericUpDownDatasetNumber_03_ValueChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Number_03 = (int)this.numericUpDownDatasetNumber_03.Value; }
        private void numericUpDownDatasetNumber_04_ValueChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Number_04 = (int)this.numericUpDownDatasetNumber_04.Value; }

        private void comboBoxDatasetOperation_01_SelectedIndexChanged(object sender, EventArgs e) {
            Game.Operations result;
            if (Enum.TryParse(this.comboBoxDatasetOperation_01.Text, out result) && this.selectedDataset is DatasetContent) this.selectedDataset.Operation_01 = result;
        }
        private void comboBoxDatasetOperation_02_SelectedIndexChanged(object sender, EventArgs e) {
            Game.Operations result;
            if (Enum.TryParse(this.comboBoxDatasetOperation_02.Text, out result) && this.selectedDataset is DatasetContent) this.selectedDataset.Operation_02 = result;
        }
        private void comboBoxDatasetOperation_03_SelectedIndexChanged(object sender, EventArgs e) {
            Game.Operations result;
            if (Enum.TryParse(this.comboBoxDatasetOperation_03.Text, out result) && this.selectedDataset is DatasetContent) this.selectedDataset.Operation_03 = result;
        }

        #endregion

    }

}
