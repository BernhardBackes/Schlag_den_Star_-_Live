using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.Tools.Base;

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimeOffsetAdditionScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimeOffsetAdditionScore {

    public partial class UserControlContent : _Base.Score.UserControlContent {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;

        private int selectedDatasetIndex = -1;

        private bool _showGameInsert = false;
        public bool showGameInsert {
            get { return this._showGameInsert; }
            set {
                if (this._showGameInsert != value) {
                    this._showGameInsert = value;
                    this.setPreviewData();
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

            this.numericUpDownPole1BuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownPole1BuzzerChannel.Maximum = int.MaxValue;

            this.numericUpDownPole2BuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownPole2BuzzerChannel.Maximum = int.MaxValue;
            
            this.numericUpDownPole3BuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownPole3BuzzerChannel.Maximum = int.MaxValue;

            this.numericUpDownPole1DMXStart.Minimum = int.MinValue;
            this.numericUpDownPole1DMXStart.Maximum = int.MaxValue;

            this.numericUpDownPole2DMXStart.Minimum = int.MinValue;
            this.numericUpDownPole2DMXStart.Maximum = int.MaxValue;

            this.numericUpDownPole3DMXStart.Minimum = int.MinValue;
            this.numericUpDownPole3DMXStart.Maximum = int.MaxValue;

            this.numericUpDownDatasetDuration.Minimum = int.MinValue;
            this.numericUpDownDatasetDuration.Maximum = int.MaxValue;
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

            bind = new Binding("Text", this.business, "IOUnitName");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.comboBoxIOUnit.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "IOUnitStatus");
            bind.Format += this.bind_comboBoxIOUnitName_BackColor;
            this.comboBoxIOUnit.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "Pole1BuzzerChannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownPole1BuzzerChannel.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "Pole2BuzzerChannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownPole2BuzzerChannel.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "Pole3BuzzerChannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownPole3BuzzerChannel.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "Pole1DMXStart");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownPole1DMXStart.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "Pole2DMXStart");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownPole2DMXStart.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "Pole3DMXStart");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownPole3DMXStart.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "OffColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelOffColor.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "OnColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelOffColor.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "OnColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelOnColor.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "OffColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelOnColor.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "Filename");
            bind.Format += (s, e) => { e.Value = string.IsNullOrEmpty((string)e.Value) ? "no file loaded" : (string)e.Value; };
            this.labelFilename.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "SampleIncluded");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxDataSampleIncluded.DataBindings.Add(bind);

            this.labelGameClass.Text = this.business.ClassInfo;

            this.fillIOUnitList();

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

            this.comboBoxIOUnit.DataBindings.Clear();
            this.numericUpDownPole1BuzzerChannel.DataBindings.Clear();
            this.numericUpDownPole2BuzzerChannel.DataBindings.Clear();
            this.numericUpDownPole3BuzzerChannel.DataBindings.Clear();

            this.labelFilename.DataBindings.Clear();
            this.checkBoxDataSampleIncluded.DataBindings.Clear();
        }

        private void fillIOUnitList() {
            this.comboBoxIOUnit.BeginUpdate();
            this.comboBoxIOUnit.Items.Clear();
            this.comboBoxIOUnit.Items.AddRange(this.business.IOUnitNameList);
            this.comboBoxIOUnit.EndUpdate();
        }

        private void bind_comboBoxIOUnitName_BackColor(object sender, ConvertEventArgs e) {
            switch ((BuzzerUnitStates)e.Value) {
                case BuzzerUnitStates.NotAvailable:
                    e.Value = Constants.ColorMissing;
                    break;
                case BuzzerUnitStates.Missing:
                    e.Value = Constants.ColorMissing;
                    break;
                case BuzzerUnitStates.Disconnected:
                    e.Value = Constants.ColorDisabled;
                    break;
                case BuzzerUnitStates.Connecting:
                    e.Value = Constants.ColorEnabling;
                    break;
                case BuzzerUnitStates.Connected:
                    e.Value = SystemColors.Control;
                    break;
                case BuzzerUnitStates.Locked:
                    e.Value = SystemColors.Control;
                    break;
                case BuzzerUnitStates.BuzzerMode:
                    e.Value = Constants.ColorBuzzered;
                    break;
                case BuzzerUnitStates.EventMode:
                    e.Value = Constants.ColorEnabled;
                    break;
                default:
                    break;
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
                this.numericUpDownDatasetDuration.Value = this.selectedDataset.Duration;
                this.buttonDataRemoveSet.Enabled = true;
                if (this.listBoxDataList.Items.Count > this.selectedDatasetIndex) this.listBoxDataList.SelectedIndex = this.selectedDatasetIndex;
            }
            else {
                this.groupBoxDataset.Enabled = false;
                this.numericUpDownDatasetDuration.Value = 0;
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

        private void setPreviewData() {
            this.radioButtonSourceScore.Checked = !this.showGameInsert;
            this.radioButtonSourceGame.Checked = this.showGameInsert;
            this.setScorePreview();
            this.setGamePreview();
        }

        protected override void setScorePreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.showGameInsert) previewScene.Insert.Score.SetOut();
                else {
                    this.business.Vinsert_SetScore(previewScene.Insert.Score, 2, 3);
                    previewScene.Insert.Score.SetIn();
                }
            }
        }

        protected void setGamePreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                if (this.showGameInsert) {
                    this.business.Vinsert_SetTimer(previewScene.Insert.Game, 6, 3.21);
                    previewScene.Insert.Game.SetIn();
                }
                else previewScene.Insert.Game.SetOut();
            }
        }

        #endregion


        #region Events.Incoming

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setPreviewData();
        }

        void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "IOUnitList") this.fillIOUnitList();
                else if (e.PropertyName == "NameList") this.fillDataList();
                else if (e.PropertyName == "SampleIncluded") this.fillDataList();
            }
        }

        void selectedDataset_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.selectedDataset_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "Duration") {
                    this.numericUpDownDatasetDuration.Value = this.selectedDataset.Duration;
                }
            }
        }

        #endregion

        #region Events.Controls

        private void numericUpDownGamePositionX_ValueChanged(object sender, EventArgs e) {
            this.business.GamePositionX = (int)this.numericUpDownGamePositionX.Value;
            this.setGamePreview();
        }

        private void numericUpDownGamePositionY_ValueChanged(object sender, EventArgs e) {
            this.business.GamePositionY = (int)this.numericUpDownGamePositionY.Value;
            this.setGamePreview();
        }

        protected virtual void comboBoxIOUnit_TextChanged(object sender, EventArgs e) { if (this.business is Business) this.business.IOUnitName = this.comboBoxIOUnit.Text; }
        protected virtual void numericUpDownPole1BuzzerChannel_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.Pole1BuzzerChannel = (int)this.numericUpDownPole1BuzzerChannel.Value; }
        protected virtual void numericUpDownPole2BuzzerChannel_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.Pole2BuzzerChannel = (int)this.numericUpDownPole2BuzzerChannel.Value; }
        protected virtual void numericUpDownPole3BuzzerChannel_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.Pole3BuzzerChannel = (int)this.numericUpDownPole3BuzzerChannel.Value; }

        private void numericUpDownPole1DMXStart_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.Pole1DMXStart = (int)this.numericUpDownPole1DMXStart.Value; }
        private void buttonPole1On_Click(object sender, EventArgs e) { if (this.business is Business) this.business.Pole1On(true); }
        private void buttonPole1Off_Click(object sender, EventArgs e) { if (this.business is Business) this.business.Pole1On(false); }

        private void numericUpDownPole2DMXStart_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.Pole2DMXStart = (int)this.numericUpDownPole2DMXStart.Value; }
        private void buttonPole2On_Click(object sender, EventArgs e) { if (this.business is Business) this.business.Pole2On(true); }
        private void buttonPole2Off_Click(object sender, EventArgs e) { if (this.business is Business) this.business.Pole2On(false); }

        private void numericUpDownPole3DMXStart_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.Pole3DMXStart = (int)this.numericUpDownPole3DMXStart.Value; }
        private void buttonPole3On_Click(object sender, EventArgs e) { if (this.business is Business) this.business.Pole3On(true); }
        private void buttonPole3Off_Click(object sender, EventArgs e) { if (this.business is Business) this.business.Pole3On(false); }

        private void labelOffColor_Click(object sender, EventArgs e) {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.OffColor;
            switch (dialog.ShowDialog()) {
                case DialogResult.OK:
                    this.business.OffColor = dialog.Color;
                    break;
            }
        }

        private void labelOnColor_Click(object sender, EventArgs e) {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.OnColor;
            switch (dialog.ShowDialog()) {
                case DialogResult.OK:
                    this.business.OnColor = dialog.Color;
                    break;
            }
        }

        private void buttonBlack_Click(object sender, EventArgs e) { this.business.AllPolesBlack(); }

        private void radioButtonSourceScore_CheckedChanged(object sender, EventArgs e) { this.showGameInsert = !this.radioButtonSourceScore.Checked; }
        private void radioButtonSourceGame_CheckedChanged(object sender, EventArgs e) { this.showGameInsert = this.radioButtonSourceGame.Checked; }

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

        private void checkBoxSampleIncluded_CheckedChanged(object sender, EventArgs e) { this.business.SampleIncluded = this.checkBoxDataSampleIncluded.Checked; }
        private void buttonDataAddNewSet_Click(object sender, EventArgs e) {
            int listIndex = this.listBoxDataList.SelectedIndex + 1;
            this.business.AddDataset(new DatasetContent(), listIndex);
            this.selectDataset(listIndex);
        }
        private void buttonDataRemoveSet_Click(object sender, EventArgs e) {
            if (this.business.TryRemoveDataset(this.selectedDatasetIndex)) this.selectDataset(this.selectedDatasetIndex - 1);
        }
        private void buttonDataRemoveAllSets_Click(object sender, EventArgs e) {
            this.business.RemoveAllDatasets();
            this.selectDataset(0);
        }

        private void numericUpDownDatasetDuration_ValueChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Duration = (int)this.numericUpDownDatasetDuration.Value; }

        #endregion

    }

}
