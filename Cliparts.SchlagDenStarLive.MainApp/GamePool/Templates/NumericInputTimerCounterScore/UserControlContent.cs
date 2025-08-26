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

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.NumericInputTimerCounterScore;
using Cliparts.Tools.Base;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.NumericInputTimerCounterScore {

    public partial class UserControlContent : _Base.Score.UserControlContent {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;

        private int selectedDatasetIndex = -1;

        private bool _showCounterInsert = false;
        public bool showCounterInsert {
            get { return this._showCounterInsert; }
            set {
                if (this._showCounterInsert != value) {
                    this._showCounterInsert = value;
                    this.setPreviewData();
                }
            }
        }

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownLeftPlayerBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerBuzzerChannel.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownRightPlayerBuzzerChannel.Maximum = int.MaxValue;

            this.numericUpDownCounterPositionX.Minimum = int.MinValue;
            this.numericUpDownCounterPositionX.Maximum = int.MaxValue;

            this.numericUpDownCounterPositionY.Minimum = int.MinValue;
            this.numericUpDownCounterPositionY.Maximum = int.MaxValue;

            this.comboBoxCounterStyle.BeginUpdate();
            this.comboBoxCounterStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.Score.Styles)));
            this.comboBoxCounterStyle.EndUpdate();

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

        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Text", this.business, "IOUnitName");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.comboBoxIOUnit.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "IOUnitStatus");
            bind.Format += this.bind_comboBoxIOUnitName_BackColor;
            this.comboBoxIOUnit.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerBuzzerChannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerBuzzerChannel.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerBuzzerChannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerBuzzerChannel.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "CounterPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownCounterPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "CounterPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownCounterPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "CounterStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxCounterStyle.DataBindings.Add(bind);

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

            bind = new Binding("Text", this.business, "LeftTeamFemale");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxLeftTeamFemale.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "LeftTeamMale");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxLeftTeamMale.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightTeamFemale");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxRightTeamFemale.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightTeamMale");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxRightTeamMale.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "Filename");
            bind.Format += (s, e) => { e.Value = string.IsNullOrEmpty((string)e.Value) ? "no file loaded" : (string)e.Value; };
            this.labelFilename.DataBindings.Add(bind);

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

            this.comboBoxIOUnit.DataBindings.Clear();
            this.numericUpDownLeftPlayerBuzzerChannel.DataBindings.Clear();
            this.numericUpDownRightPlayerBuzzerChannel.DataBindings.Clear();
            this.numericUpDownCounterPositionX.DataBindings.Clear();
            this.numericUpDownCounterPositionY.DataBindings.Clear();
            this.comboBoxCounterStyle.DataBindings.Clear();
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
            this.textBoxLeftTeamFemale.DataBindings.Clear();
            this.textBoxLeftTeamMale.DataBindings.Clear();
            this.textBoxRightTeamFemale.DataBindings.Clear();
            this.textBoxRightTeamMale.DataBindings.Clear();
            this.labelFilename.DataBindings.Clear();

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
            this.radioButtonSourceScore.Checked = !this.showCounterInsert;
            this.radioButtonSourceCounter.Checked = this.showCounterInsert;
            this.setScorePreview();
            this.setCounterPreview();
            this.setTimerPreview();
        }

        protected override void setScorePreview() {
            if (this.previewSceneIsAvailable) {
                //Preview previewScene = this.previewScene as Preview;
                //this.business.Vinsert_SetScore(previewScene.Insert.Score, 3, 2);
                //if (this.showCounterInsert) previewScene.Insert.Score.SetOut();
                //else previewScene.Insert.Score.SetIn();
            }
        }

        protected void setCounterPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetCounter(previewScene.Insert.Counter, 88, 99);
                if (this.showCounterInsert) previewScene.Insert.Counter.SetIn();
                else previewScene.Insert.Counter.SetOut();
            }
        }

        protected virtual void setTimerPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetTimer(previewScene.Insert.Timer, this.business.TimerStartTime);
                if (this.showCounterInsert) previewScene.Insert.Timer.SetIn();
                else previewScene.Insert.Timer.SetOut();
            }
        }

        private void fillDataList()
        {
            this.listBoxDataList.BeginUpdate();
            this.listBoxDataList.Items.Clear();
            int id = 1;
            foreach (string item in this.business.NameList)
            {
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
            int index)
        {
            if (index < 0) index = 0;
            if (index >= this.business.DatasetsCount) index = this.business.DatasetsCount - 1;
            DatasetContent selectedDataset = this.business.GetDataset(index);
            this.selectedDatasetIndex = this.business.GetDatasetIndex(selectedDataset);

            this.textBoxDatasetName.DataBindings.Clear();
            this.textBoxDatasetHostText.DataBindings.Clear();
            this.textBoxDatasetPlayerText.DataBindings.Clear();

            //Dispose...
            if (this.selectedDataset is DatasetContent)
            {
                this.selectedDataset.PropertyChanged -= this.selectedDataset_PropertyChanged;
            }
            this.selectedDataset = selectedDataset;
            //Pose...
            if (this.selectedDataset is DatasetContent)
            {

                this.selectedDataset.PropertyChanged += this.selectedDataset_PropertyChanged;

                this.groupBoxDataset.Enabled = true;
                this.buttonDataRemoveSet.Enabled = true;

                Binding bind;

                bind = new Binding("Text", this.selectedDataset, "Name");
                bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
                this.textBoxDatasetName.DataBindings.Add(bind);

                bind = new Binding("Text", this.selectedDataset, "HostText");
                bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
                this.textBoxDatasetHostText.DataBindings.Add(bind);

                bind = new Binding("Text", this.selectedDataset, "PlayerText");
                bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
                this.textBoxDatasetPlayerText.DataBindings.Add(bind);

                if (this.listBoxDataList.Items.Count > this.selectedDatasetIndex) this.listBoxDataList.SelectedIndex = this.selectedDatasetIndex;
            }
            else
            {
                this.groupBoxDataset.Enabled = false;
                this.buttonDataRemoveSet.Enabled = false;
                this.textBoxDatasetName.Text = string.Empty;
                this.textBoxDatasetHostText.Text = string.Empty;
                this.textBoxDatasetPlayerText.Text = string.Empty;
            }
            Helper.setControlBackColor(this.buttonDataRemoveSet);

            this.adjustDataMoveSet();

            this.setPreviewData();

        }

        private void adjustDataMoveSet()
        {
            this.buttonDataMoveSetUp.Enabled = this.listBoxDataList.SelectedIndex > 0;
            Helper.setControlBackColor(this.buttonDataMoveSetUp);

            this.buttonDataMoveSetDown.Enabled = this.listBoxDataList.SelectedIndex >= 0 && this.listBoxDataList.SelectedIndex < this.listBoxDataList.Items.Count - 1;
            Helper.setControlBackColor(this.buttonDataMoveSetDown);
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
            }
        }

        private void selectedDataset_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.selectedDataset_PropertyChanged(sender, e)));
            else
            {
                if (e.PropertyName == "HostText")
                {
                    this.textBoxDatasetHostText.Text = this.selectedDataset.HostText;
                }
                else if (e.PropertyName == "Name")
                {
                    this.textBoxDatasetName.Text = this.selectedDataset.Name;
                }
                else if (e.PropertyName == "PlayerText")
                {
                    this.textBoxDatasetPlayerText.Text = this.selectedDataset.PlayerText;
                }
            }
        }


        #endregion

        #region Events.Controls

        protected virtual void comboBoxIOUnit_TextChanged(object sender, EventArgs e) { if (this.business is Business) this.business.IOUnitName = this.comboBoxIOUnit.Text; }
        protected virtual void numericUpDownLeftPlayerBuzzerChannel_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftPlayerBuzzerChannel = (int)this.numericUpDownLeftPlayerBuzzerChannel.Value; }
        protected virtual void numericUpDownRightPlayerBuzzerChannel_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightPlayerBuzzerChannel = (int)this.numericUpDownRightPlayerBuzzerChannel.Value; }

        protected virtual void numericUpDownCounterPositionX_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.CounterPositionX = (int)this.numericUpDownCounterPositionX.Value;
                this.setCounterPreview();
            }
        }
        protected virtual void numericUpDownCounterPositionY_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.CounterPositionY = (int)this.numericUpDownCounterPositionY.Value;
                this.setCounterPreview();
            }
        }
        protected virtual void comboBoxCounterStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.Score.Styles style;
            if (this.business is Business &&
                Enum.TryParse(this.comboBoxCounterStyle.Text, out style)) {
                this.business.CounterStyle = style;
                this.setCounterPreview();
            }
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

        private void textBoxLeftTeamFemale_TextChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftTeamFemale = this.textBoxLeftTeamFemale.Text; }
        private void textBoxLeftTeamMale_TextChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftTeamMale = this.textBoxLeftTeamMale.Text; }
        private void textBoxRightTeamFemale_TextChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightTeamFemale = this.textBoxRightTeamFemale.Text; }
        private void textBoxRightTeamMale_TextChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightTeamMale = this.textBoxRightTeamMale.Text; }

        private void radioButtonSourceScore_CheckedChanged(object sender, EventArgs e) { this.showCounterInsert = !this.radioButtonSourceScore.Checked; }
        private void radioButtonSourceCounter_CheckedChanged(object sender, EventArgs e) { this.showCounterInsert = this.radioButtonSourceCounter.Checked; }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Load Data";
            dialog.InitialDirectory = ApplicationAttributes.ContentPath;
            if (File.Exists(this.business.Filename)) dialog.FileName = this.business.Filename;
            dialog.DefaultExt = "*.xml";
            dialog.Filter = "XML-File (*.xml)|*.xml|all files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.Multiselect = false;
            dialog.RestoreDirectory = true;
            switch (dialog.ShowDialog())
            {
                case DialogResult.Cancel:
                    break;
                case DialogResult.OK:
                    this.business.Load(dialog.FileName);
                    break;
            }
            dialog = null;
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (File.Exists(this.business.Filename)) this.business.Save();
            else buttonSaveAs_Click(sender, e);
        }
        private void buttonSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Save Data As...";
            dialog.InitialDirectory = ApplicationAttributes.ContentPath;
            if (File.Exists(this.business.Filename)) dialog.FileName = this.business.Filename;
            dialog.DefaultExt = "*.xml";
            dialog.Filter = "XML-File (*.xml)|*.xml|all files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;
            switch (dialog.ShowDialog())
            {
                case DialogResult.Cancel:
                    break;
                case DialogResult.OK:
                    this.business.SaveAs(dialog.FileName);
                    break;
            }
            dialog = null;
        }

        private void listBoxDataList_SelectedIndexChanged(object sender, EventArgs e) { this.selectDataset(this.listBoxDataList.SelectedIndex); }
        private void buttonDataMoveSetUp_Click(object sender, EventArgs e)
        {
            if (this.business.TryMoveDatasetUp(this.selectedDatasetIndex)) this.selectDataset(this.selectedDatasetIndex - 1);
        }
        private void buttonDataMoveSetDown_Click(object sender, EventArgs e)
        {
            if (this.business.TryMoveDatasetDown(this.selectedDatasetIndex)) this.selectDataset(this.selectedDatasetIndex + 1);
        }
        private void buttonDataResort_Click(object sender, EventArgs e) { this.business.ResortAllDatasets(); }

        private void buttonDataAddNewSet_Click(object sender, EventArgs e)
        {
            string filename = Helper.selectVideoFile("select movie", string.Empty);
            if (filename != null)
            {
                int listIndex = this.listBoxDataList.SelectedIndex + 1;
                this.business.AddDataset(new DatasetContent(), listIndex);
                this.selectDataset(listIndex);
            }
        }
        private void buttonDataRemoveSet_Click(object sender, EventArgs e)
        {
            if (this.business.TryRemoveDataset(this.selectedDatasetIndex)) this.selectDataset(this.selectedDatasetIndex - 1);
        }
        private void buttonDataRemoveAllSets_Click(object sender, EventArgs e)
        {
            this.business.RemoveAllDatasets();
            this.selectDataset(0);
        }

        private void textBoxDatasetName_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.Name = this.textBoxDatasetName.Text; }
        private void textBoxDatasetHostText_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.HostText = this.textBoxDatasetHostText.Text; }
        private void textBoxDatasetPlayerText_TextChanged(object sender, EventArgs e) { if (this.selectedDataset is DatasetContent) this.selectedDataset.PlayerText = this.textBoxDatasetPlayerText.Text; }

        #endregion
    }
}
