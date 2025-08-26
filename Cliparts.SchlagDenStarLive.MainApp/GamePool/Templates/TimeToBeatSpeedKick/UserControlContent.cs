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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimeToBeatSpeedKick;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimeToBeatSpeedKick {

    public partial class UserControlContent : _Base.Score.UserControlContent {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;

        private int selectedDatasetIndex = -1;

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownTimeToBeatPositionX.Minimum = int.MinValue;
            this.numericUpDownTimeToBeatPositionX.Maximum = int.MaxValue;

            this.numericUpDownTimeToBeatPositionY.Minimum = int.MinValue;
            this.numericUpDownTimeToBeatPositionY.Maximum = int.MaxValue;

            this.comboBoxTimeToBeatStyle.BeginUpdate();
            this.comboBoxTimeToBeatStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.TimeToBeat.Styles)));
            this.comboBoxTimeToBeatStyle.EndUpdate();

            this.numericUpDownTimeToBeatStopTime.Minimum = int.MinValue;
            this.numericUpDownTimeToBeatStopTime.Maximum = int.MaxValue;

            this.numericUpDownTopBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownTopBuzzerChannel.Maximum = int.MaxValue;

            this.numericUpDownLeftBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownLeftBuzzerChannel.Maximum = int.MaxValue;

            this.numericUpDownRightBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownRightBuzzerChannel.Maximum = int.MaxValue;

            this.numericUpDownBottomBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownBottomBuzzerChannel.Maximum = int.MaxValue;

            this.numericUpDownTopPanelDMXStart.Minimum = int.MinValue;
            this.numericUpDownTopPanelDMXStart.Maximum = int.MaxValue;

            this.numericUpDownLeftPanelDMXStart.Minimum = int.MinValue;
            this.numericUpDownLeftPanelDMXStart.Maximum = int.MaxValue;

            this.numericUpDownRightPanelDMXStart.Minimum = int.MinValue;
            this.numericUpDownRightPanelDMXStart.Maximum = int.MaxValue;

            this.numericUpDownBottomPanelDMXStart.Minimum = int.MinValue;
            this.numericUpDownBottomPanelDMXStart.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.business, "TimeToBeatPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimeToBeatPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimeToBeatPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimeToBeatPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimeToBeatStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxTimeToBeatStyle.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimeToBeatStopTime");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimeToBeatStopTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimeToBeatStopTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimeToBeatStopTimeText.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "IOUnitName");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.comboBoxIOUnit.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "IOUnitStatus");
            bind.Format += this.bind_comboBoxIOUnitName_BackColor;
            this.comboBoxIOUnit.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TopBuzzerChannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTopBuzzerChannel.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftBuzzerChannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftBuzzerChannel.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightBuzzerChannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightBuzzerChannel.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "BottomBuzzerChannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownBottomBuzzerChannel.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TopPanelDMXStart");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTopPanelDMXStart.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPanelDMXStart");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPanelDMXStart.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPanelDMXStart");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPanelDMXStart.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "BottomPanelDMXStart");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownBottomPanelDMXStart.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "OffColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelOffColor.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "OnColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelOffColor.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "NextColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelNextColor.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "OffColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelNextColor.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "ShowNextPanel");
            bind.Format += (s, e) => { Convert.ToBoolean(e.Value); };
            this.checkBoxShowNextPanel.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "OnColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelOnColor.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "OffColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelOnColor.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "HitColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelHitColor.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "OffColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelHitColor.DataBindings.Add(bind);

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

            this.numericUpDownTimeToBeatPositionX.DataBindings.Clear();
            this.numericUpDownTimeToBeatPositionY.DataBindings.Clear();
            this.comboBoxTimeToBeatStyle.DataBindings.Clear();
            this.numericUpDownTimeToBeatStopTime.DataBindings.Clear();
            this.labelTimeToBeatStopTimeText.DataBindings.Clear();

            this.comboBoxIOUnit.DataBindings.Clear();
            this.numericUpDownTopBuzzerChannel.DataBindings.Clear();
            this.numericUpDownLeftBuzzerChannel.DataBindings.Clear();
            this.numericUpDownRightBuzzerChannel.DataBindings.Clear();
            this.numericUpDownBottomBuzzerChannel.DataBindings.Clear();

            this.numericUpDownTopPanelDMXStart.DataBindings.Clear();
            this.numericUpDownLeftPanelDMXStart.DataBindings.Clear();
            this.numericUpDownRightPanelDMXStart.DataBindings.Clear();
            this.numericUpDownBottomPanelDMXStart.DataBindings.Clear();

            this.labelOffColor.DataBindings.Clear();
            this.labelNextColor.DataBindings.Clear();
            this.checkBoxShowNextPanel.DataBindings.Clear();
            this.labelOnColor.DataBindings.Clear(); 
            this.labelHitColor.DataBindings.Clear();

            this.business.PropertyChanged -= this.business_PropertyChanged;
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
                    e.Value = Constants.ColorEnabled;
                    break;
                case BuzzerUnitStates.EventMode:
                    e.Value = Constants.ColorBuzzered;
                    break;
                default:
                    break;
            }
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

        private void fillStepList() {

            int index = this.listBoxCourseSteps.SelectedIndex;

            this.listBoxCourseSteps.BeginUpdate();
            this.listBoxCourseSteps.Items.Clear();

            if (this.selectedDataset is DatasetContent) {
                foreach (StepPositions item in this.selectedDataset.StepList) {
                    this.listBoxCourseSteps.Items.Add(item);
                }
            }
            this.listBoxCourseSteps.EndUpdate();

            this.listBoxCourseSteps.Enabled = this.listBoxCourseSteps.Items.Count > 0;

            if (this.listBoxCourseSteps.Enabled) {
                if (index < 0) index = 0;
                if (index >= this.listBoxCourseSteps.Items.Count) index = this.listBoxCourseSteps.Items.Count - 1;
                this.listBoxCourseSteps.SelectedIndex = index;
            }

            Helper.setControlBackColor(this.listBoxCourseSteps);

            this.setSelectedStep();
        }

        private void selectDataset(
            int index) {
            if (index < 0)
                index = 0;
            if (index >= this.business.DatasetsCount)
                index = this.business.DatasetsCount - 1;
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
                this.buttonDataRemoveSet.Enabled = true;
                if (this.listBoxDataList.Items.Count > this.selectedDatasetIndex) this.listBoxDataList.SelectedIndex = this.selectedDatasetIndex;
            }
            else {
                this.groupBoxDataset.Enabled = false;
                this.textBoxDataSetName.Text = string.Empty;
                this.buttonDataRemoveSet.Enabled = false;
            }
            Helper.setControlBackColor(this.buttonDataRemoveSet);

            this.fillStepList();
        }

        private void setSelectedStep() {
            this.labelTopStep.BackColor = Color.PaleTurquoise;
            this.labelLeftStep.BackColor = Color.PaleTurquoise;
            this.labelRightStep.BackColor = Color.PaleTurquoise;
            this.labelBottomStep.BackColor = Color.PaleTurquoise;
            if (this.listBoxCourseSteps.SelectedIndex < 0) {
                this.groupBoxStep.Text = string.Empty;
            }
            else {
                int id = this.listBoxCourseSteps.SelectedIndex + 1;
                this.groupBoxStep.Text = string.Format("step {0} of {1}", id.ToString(), this.listBoxCourseSteps.Items.Count);
                StepPositions item = (StepPositions)this.listBoxCourseSteps.SelectedItem;
                switch (item) {
                    case StepPositions.Top:
                        this.labelTopStep.BackColor = Color.Gold;
                        break;
                    case StepPositions.Left:
                        this.labelLeftStep.BackColor = Color.Gold;
                        break;
                    case StepPositions.Right:
                        this.labelRightStep.BackColor = Color.Gold;
                        break;
                    case StepPositions.Bottom:
                        this.labelBottomStep.BackColor = Color.Gold;
                        break;
                }
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

        protected void setTimerPreview() {
            //base.setTimerPreview();
            //if (this.previewSceneIsAvailable) {
            //    Preview previewScene = this.previewScene as Preview;
            //    this.business.Vinsert_Timer.Set(previewScene.Insert.Timer);
            //    previewScene.Insert.Timer.SetIn();
            //}
        }

        protected void setScorePreview() {
            //if (this.previewSceneIsAvailable) {
            //    Preview previewScene = this.previewScene as Preview;
            //    this.business.Vinsert_SetScore(previewScene.Insert.Score, 2, 3);
            //    previewScene.Insert.Score.SetIn();
            //}
        }

        #endregion


        #region Events.Incoming

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setTimerPreview();
            this.setScorePreview();
        }

        protected void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "IOUnitList") this.fillIOUnitList();
                else if (e.PropertyName == "NameList") this.fillDataList();
            }
        }

        void selectedDataset_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired)
                this.Invoke((MethodInvoker)(() => this.selectedDataset_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "Name") {
                    this.textBoxDataSetName.Text = this.selectedDataset.Name;
                    this.fillDataList();
                }
                else if (e.PropertyName == "StepList") {
                    this.fillStepList();
                }
            }
        }

        #endregion

        #region Events.Controls

        protected virtual void comboBoxIOUnit_TextChanged(object sender, EventArgs e) { if (this.business is Business) this.business.IOUnitName = this.comboBoxIOUnit.Text; }
        private void numericUpDownTopBuzzerChannel_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.TopBuzzerChannel = (int)this.numericUpDownTopBuzzerChannel.Value; }
        protected virtual void numericUpDownLeftBuzzerChannel_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftBuzzerChannel = (int)this.numericUpDownLeftBuzzerChannel.Value; }
        protected virtual void numericUpDownRightBuzzerChannel_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightBuzzerChannel = (int)this.numericUpDownRightBuzzerChannel.Value; }
        private void numericUpDownBottomBuzzerChannel_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.BottomBuzzerChannel = (int)this.numericUpDownBottomBuzzerChannel.Value; }

        private void numericUpDownTopPanelDMXStart_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.TopPanelDMXStart = (int)this.numericUpDownTopPanelDMXStart.Value; }
        private void buttonTopPanelOn_Click(object sender, EventArgs e) { if (this.business is Business) this.business.PanelOn(StepPositions.Top); }
        private void buttonTopPanelHit_Click(object sender, EventArgs e) { if (this.business is Business) this.business.PanelHit(StepPositions.Top); }
        private void buttonTopPanelNext_Click(object sender, EventArgs e) { if (this.business is Business) this.business.PanelNext(StepPositions.Top); }
        private void buttonTopPanelOff_Click(object sender, EventArgs e) { if (this.business is Business) this.business.PanelOff(StepPositions.Top); }

        private void numericUpDownLeftPanelDMXStart_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftPanelDMXStart = (int)this.numericUpDownLeftPanelDMXStart.Value; }
        private void buttonLeftPanelOn_Click(object sender, EventArgs e) { if (this.business is Business) this.business.PanelOn(StepPositions.Left); }
        private void buttonLeftPanelHit_Click(object sender, EventArgs e) { if (this.business is Business) this.business.PanelHit(StepPositions.Left); }
        private void buttonLeftPanelNext_Click(object sender, EventArgs e) { if (this.business is Business) this.business.PanelNext(StepPositions.Left); }
        private void buttonLeftPanelOff_Click(object sender, EventArgs e) { if (this.business is Business) this.business.PanelOff(StepPositions.Left); }

        private void numericUpDownRightPanelDMXStart_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightPanelDMXStart = (int)this.numericUpDownRightPanelDMXStart.Value; }
        private void buttonRightPanelOn_Click(object sender, EventArgs e) { if (this.business is Business) this.business.PanelOn(StepPositions.Right); }
        private void buttonRightPanelHit_Click(object sender, EventArgs e) { if (this.business is Business) this.business.PanelHit(StepPositions.Right); }
        private void buttonRightPanelNext_Click(object sender, EventArgs e) { if (this.business is Business) this.business.PanelNext(StepPositions.Right); }
        private void buttonRightPanelOff_Click(object sender, EventArgs e) { if (this.business is Business) this.business.PanelOff(StepPositions.Right); }

        private void numericUpDownBottomPanelDMXStart_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.BottomPanelDMXStart = (int)this.numericUpDownBottomPanelDMXStart.Value; }
        private void buttonBottomPanelOn_Click(object sender, EventArgs e) { if (this.business is Business) this.business.PanelOn(StepPositions.Bottom); }
        private void buttonBottomPanelHit_Click(object sender, EventArgs e) { if (this.business is Business) this.business.PanelHit(StepPositions.Bottom); }
        private void buttonBottomPanelNext_Click(object sender, EventArgs e) { if (this.business is Business) this.business.PanelNext(StepPositions.Bottom); }
        private void buttonBottomPanelOff_Click(object sender, EventArgs e) { if (this.business is Business) this.business.PanelOff(StepPositions.Bottom); }

        private void labelOffColor_Click(object sender, EventArgs e) {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.OffColor;
            switch (dialog.ShowDialog()) {
                case DialogResult.OK:
                    this.business.OffColor = dialog.Color;
                    break;
            }
        }

        private void labelNextColor_Click(object sender, EventArgs e) {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.NextColor;
            switch (dialog.ShowDialog()) {
                case DialogResult.OK:
                    this.business.NextColor = dialog.Color;
                    break;
            }

        }

        private void checkBoxShowNext_CheckedChanged(object sender, EventArgs e) {
            this.business.ShowNextPanel = this.checkBoxShowNextPanel.Checked;
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

        private void labelHitColor_Click(object sender, EventArgs e) {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.HitColor;
            switch (dialog.ShowDialog()) {
                case DialogResult.OK:
                    this.business.HitColor = dialog.Color;
                    break;
            }
        }

        private void buttonLoad_Click(object sender, EventArgs e) {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Load Data";
            dialog.InitialDirectory = ApplicationAttributes.ContentPath;
            if (File.Exists(this.business.Filename))
                dialog.FileName = this.business.Filename;
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
            if (File.Exists(this.business.Filename))
                this.business.Save();
            else
                buttonSaveAs_Click(sender, e);
        }
        private void buttonSaveAs_Click(object sender, EventArgs e) {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Save Data As...";
            dialog.InitialDirectory = ApplicationAttributes.ContentPath;
            if (File.Exists(this.business.Filename))
                dialog.FileName = this.business.Filename;
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
        private void buttonDataAddNewSet_Click(object sender, EventArgs e) {
            int listIndex = this.listBoxDataList.SelectedIndex + 1;
            this.business.AddDataset(new DatasetContent(), listIndex);
            this.selectDataset(listIndex);
        }
        private void buttonDataRemoveSet_Click(object sender, EventArgs e) {
            if (this.business.TryRemoveDataset(this.selectedDatasetIndex))
                this.selectDataset(this.selectedDatasetIndex - 1);
        }
        private void buttonDataRemoveAllSets_Click(object sender, EventArgs e) {
            this.business.RemoveAllDatasets();
            this.selectDataset(0);
        }
        private void textBoxDataSetName_TextChanged(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.Name = this.textBoxDataSetName.Text;
        }

        private void buttonAddStepsRandom_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.AddStepsRandom((int)this.numericUpDownAddStepsRandom.Value);
        }
        private void buttonClearSteps_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.Clear();
        }

        private void buttonInsertTopStep_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                int index = this.listBoxCourseSteps.SelectedIndex + 1;
                this.selectedDataset.InsertStep(index, StepPositions.Top);
                this.listBoxCourseSteps.SelectedIndex = index;
            }
        }

        private void buttonSetTopStep_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.SetStep(this.listBoxCourseSteps.SelectedIndex, StepPositions.Top);
        }

        private void buttonInsertLeftStep_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                int index = this.listBoxCourseSteps.SelectedIndex + 1;
                this.selectedDataset.InsertStep(index, StepPositions.Left);
                this.listBoxCourseSteps.SelectedIndex = index;
            }
        }

        private void buttonSetLeftStep_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.SetStep(this.listBoxCourseSteps.SelectedIndex, StepPositions.Left);
        }

        private void buttonInsertRightStep_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                int index = this.listBoxCourseSteps.SelectedIndex + 1;
                this.selectedDataset.InsertStep(index, StepPositions.Right);
                this.listBoxCourseSteps.SelectedIndex = index;
            }
        }

        private void buttonSetRightStep_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.SetStep(this.listBoxCourseSteps.SelectedIndex, StepPositions.Right);
        }

        private void buttonInsertBottomStep_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                int index = this.listBoxCourseSteps.SelectedIndex + 1;
                this.selectedDataset.InsertStep(index, StepPositions.Bottom);
                this.listBoxCourseSteps.SelectedIndex = index;
            }
        }

        private void buttonSetBottomStep_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.SetStep(this.listBoxCourseSteps.SelectedIndex, StepPositions.Bottom);
        }

        private void buttonRemoveStep_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.RemoveStep(this.listBoxCourseSteps.SelectedIndex);
        }

        private void listBoxCourseSteps_SelectedIndexChanged(object sender, EventArgs e) {
            this.setSelectedStep();
        }

        #endregion
    }
}
