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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.ScrabbleList;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.ScrabbleList {

    public partial class UserControlContent : _Base.Sets.UserControlContent {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;

        private int selectedDatasetIndex = -1;

        private PlayerContent leftPlayerContent { get; set; }

        private PlayerContent rightPlayerContent { get; set; }


        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownTimerPositionX.Minimum = int.MinValue;
            this.numericUpDownTimerPositionX.Maximum = int.MaxValue;

            this.numericUpDownTimerPositionY.Minimum = int.MinValue;
            this.numericUpDownTimerPositionY.Maximum = int.MaxValue;

            this.comboBoxTimerStyle.BeginUpdate();
            this.comboBoxTimerStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.Timer.Styles)));
            this.comboBoxTimerStyle.EndUpdate();

            this.numericUpDownTimerStartTime.Minimum = int.MinValue;
            this.numericUpDownTimerStartTime.Maximum = int.MaxValue;

            this.numericUpDownTimerExtraTime.Minimum = int.MinValue;
            this.numericUpDownTimerExtraTime.Maximum = int.MaxValue;

            this.numericUpDownTimerStopTime.Minimum = int.MinValue;
            this.numericUpDownTimerStopTime.Maximum = int.MaxValue;

            this.numericUpDownTimerAlarmTime1.Minimum = int.MinValue;
            this.numericUpDownTimerAlarmTime1.Maximum = int.MaxValue;

            this.numericUpDownTimerAlarmTime2.Minimum = int.MinValue;
            this.numericUpDownTimerAlarmTime2.Maximum = int.MaxValue;

            this.numericUpDownGamePositionX.Minimum = int.MinValue;
            this.numericUpDownGamePositionX.Maximum = int.MaxValue;

            this.numericUpDownGamePositionY.Minimum = int.MinValue;
            this.numericUpDownGamePositionY.Maximum = int.MaxValue;

            this.numericUpDownGameScaling.Minimum = int.MinValue;
            this.numericUpDownGameScaling.Maximum = int.MaxValue;

        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            this.leftPlayerContent = new PlayerContent();
            this.leftPlayerContent.PropertyChanged += this.playerContent_PropertyChanged;

            this.rightPlayerContent = new PlayerContent();
            this.rightPlayerContent.PropertyChanged += this.playerContent_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.business, "TimerPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimerPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxTimerStyle.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimerStartTime");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerStartTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerStartTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerStartTimeText.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimerExtraTime");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerExtraTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerExtraTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerExtraTimeText.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimerStopTime");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerStopTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerStopTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerStopTimeText.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimerAlarmTime1");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerAlarmTime1.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerAlarmTime1");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerAlarmTime1Text.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimerAlarmTime2");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerAlarmTime2.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerAlarmTime2");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerAlarmTime2Text.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "ShowFullscreenTimer");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxTimerShowFullscreenTimer.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "GamePositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownGamePositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "GamePositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownGamePositionY.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "GameScaling");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownGameScaling.DataBindings.Add(bind);

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

            this.leftPlayerContent.PropertyChanged -= this.playerContent_PropertyChanged;
            this.rightPlayerContent.PropertyChanged -= this.playerContent_PropertyChanged;

            this.numericUpDownTimerPositionX.DataBindings.Clear();
            this.numericUpDownTimerPositionY.DataBindings.Clear();
            this.comboBoxTimerStyle.DataBindings.Clear();
            this.numericUpDownTimerStartTime.DataBindings.Clear();
            this.labelTimerStartTimeText.DataBindings.Clear();
            this.numericUpDownTimerExtraTime.DataBindings.Clear();
            this.labelTimerExtraTimeText.DataBindings.Clear();
            this.numericUpDownTimerStopTime.DataBindings.Clear();
            this.labelTimerStopTimeText.DataBindings.Clear();
            this.numericUpDownTimerAlarmTime1.DataBindings.Clear();
            this.labelTimerAlarmTime1Text.DataBindings.Clear();
            this.numericUpDownTimerAlarmTime2.DataBindings.Clear();
            this.labelTimerAlarmTime2Text.DataBindings.Clear();
            this.checkBoxTimerShowFullscreenTimer.DataBindings.Clear();
            this.numericUpDownGamePositionX.DataBindings.Clear();
            this.numericUpDownGamePositionY.DataBindings.Clear();
            this.numericUpDownGameScaling.DataBindings.Clear();
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
                this.buttonDataRemoveSet.Enabled = true;
                if (this.listBoxDataList.Items.Count > this.selectedDatasetIndex) this.listBoxDataList.SelectedIndex = this.selectedDatasetIndex;
                this.leftPlayerContent.SetDatasetText(this.selectedDataset.Text);
                this.rightPlayerContent.SetDatasetText(this.selectedDataset.Text);
            }
            else {
                this.groupBoxDataset.Enabled = false;
                this.textBoxDataSetText.Text = string.Empty;
                this.buttonDataRemoveSet.Enabled = false;
                this.leftPlayerContent.SetDatasetText(string.Empty);
                this.rightPlayerContent.SetDatasetText(string.Empty);
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

        protected void setPreviewData() {
            if (this.previewSceneIsAvailable) {
                this.setSetsPreview();
                this.setTimerPreview();
                this.setItemPreview();
            }
        }

        protected override void setSetsPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                List<_Base.Sets.SingleSet> leftPlayerSets = new List<_Base.Sets.SingleSet>();
                leftPlayerSets.Add(new _Base.Sets.SingleSet(11, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid));
                leftPlayerSets.Add(new _Base.Sets.SingleSet(12, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid));
                leftPlayerSets.Add(new _Base.Sets.SingleSet(13, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid));
                leftPlayerSets.Add(new _Base.Sets.SingleSet(14, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid));
                leftPlayerSets.Add(new _Base.Sets.SingleSet(15, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid));
                leftPlayerSets.Add(new _Base.Sets.SingleSet());
                leftPlayerSets.Add(new _Base.Sets.SingleSet());
                leftPlayerSets.Add(new _Base.Sets.SingleSet());
                leftPlayerSets.Add(new _Base.Sets.SingleSet());
                leftPlayerSets.Add(new _Base.Sets.SingleSet());
                List<_Base.Sets.SingleSet> rightPlayerSets = new List<_Base.Sets.SingleSet>();
                rightPlayerSets.Add(new _Base.Sets.SingleSet(21, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid));
                rightPlayerSets.Add(new _Base.Sets.SingleSet(22, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid));
                rightPlayerSets.Add(new _Base.Sets.SingleSet(23, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid));
                rightPlayerSets.Add(new _Base.Sets.SingleSet(24, VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid));
                rightPlayerSets.Add(new _Base.Sets.SingleSet());
                rightPlayerSets.Add(new _Base.Sets.SingleSet());
                rightPlayerSets.Add(new _Base.Sets.SingleSet());
                rightPlayerSets.Add(new _Base.Sets.SingleSet());
                rightPlayerSets.Add(new _Base.Sets.SingleSet());
                rightPlayerSets.Add(new _Base.Sets.SingleSet());
                this.business.Vinsert_SetSets(previewScene.Insert.Sets, 0, 0, leftPlayerSets.ToArray(), rightPlayerSets.ToArray());
                previewScene.Insert.Sets.SetIn();
            }
        }

        protected void setTimerPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetTimer(previewScene.Insert.Timer);
                previewScene.Insert.Timer.SetIn();
            }
        }

        protected void setItemPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetGame(previewScene.Insert.Game, this.rightPlayerContent.Letters, this.rightPlayerContent.Letters);
                previewScene.Insert.Game.SetIn();
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
                if (e.PropertyName == "Name") this.textBoxDataSetText.Text = this.selectedDataset.Text;
                else if (e.PropertyName == "Text") {
                    this.leftPlayerContent.SetDatasetText(this.selectedDataset.Text);
                    this.rightPlayerContent.SetDatasetText(this.selectedDataset.Text);
                    this.setItemPreview();
                }
            }
        }

        private void playerContent_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.selectedDataset_PropertyChanged(sender, e)));
            else {
                //if (e.PropertyName == "Letter") this.setItemPreview();
            }
        }

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setPreviewData();
        }

        #endregion

        #region Events.Controls

        protected override void numericUpDownSetsPositionX_ValueChanged(object sender, EventArgs e) {
            base.numericUpDownSetsPositionX_ValueChanged(sender, e);
            this.setSetsPreview();
        }
        protected override void numericUpDownSetsPositionY_ValueChanged(object sender, EventArgs e) {
            base.numericUpDownSetsPositionY_ValueChanged(sender, e);
            this.setSetsPreview();
        }
        protected override void comboBoxSetsStyle_SelectedIndexChanged(object sender, EventArgs e) {
            base.comboBoxSetsStyle_SelectedIndexChanged(sender, e);
            this.setSetsPreview();
        }

        protected virtual void numericUpDownTimerPositionX_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.TimerPositionX = (int)this.numericUpDownTimerPositionX.Value;
                this.setTimerPreview();
            }
        }
        protected virtual void numericUpDownTimerPositionY_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.TimerPositionY = (int)this.numericUpDownTimerPositionY.Value;
                this.setTimerPreview();
            }
        }
        protected virtual void comboBoxTimerStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.Timer.Styles style;
            if (this.business is Business &&
                Enum.TryParse(this.comboBoxTimerStyle.Text, out style)) {
                this.business.TimerStyle = style;
                this.setTimerPreview();
            }
        }
        protected virtual void numericUpDownTimerStartTime_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.TimerStartTime = (int)this.numericUpDownTimerStartTime.Value;
                this.setTimerPreview();
            }
        }
        protected virtual void numericUpDownTimerExtraTime_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.TimerExtraTime = (int)this.numericUpDownTimerExtraTime.Value; }
        protected virtual void numericUpDownTimerStopTime_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.TimerStopTime = (int)this.numericUpDownTimerStopTime.Value; }
        protected virtual void numericUpDownTimerAlarmTime1_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.TimerAlarmTime1 = (int)this.numericUpDownTimerAlarmTime1.Value; }
        protected virtual void numericUpDownTimerAlarmTime2_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.TimerAlarmTime2 = (int)this.numericUpDownTimerAlarmTime2.Value; }
        protected virtual void checkBoxTimerShowFullscreenTimer_CheckedChanged(object sender, EventArgs e) { if (this.business is Business) this.business.ShowFullscreenTimer = this.checkBoxTimerShowFullscreenTimer.Checked; }

        protected virtual void numericUpDownGamePositionX_ValueChanged(object sender, EventArgs e) { this.business.GamePositionX = (int)this.numericUpDownGamePositionX.Value; }
        protected virtual void numericUpDownGamePositionY_ValueChanged(object sender, EventArgs e) { this.business.GamePositionY = (int)this.numericUpDownGamePositionY.Value; }
        protected virtual void numericUpDownGameScaling_ValueChanged(object sender, EventArgs e) { this.business.GameScaling = (int)this.numericUpDownGameScaling.Value; }

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
        private void buttonDataRemoveSet_Click(object sender, EventArgs e) {
            if (this.business.TryRemoveDataset(this.selectedDatasetIndex)) this.selectDataset(this.selectedDatasetIndex - 1);
        }
        private void buttonDataRemoveAllSets_Click(object sender, EventArgs e) {
            this.business.RemoveAllDatasets();
            this.selectDataset(0);
        }

        private void textBoxDataSetText_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Text = this.textBoxDataSetText.Text; }

        #endregion
    }

}
