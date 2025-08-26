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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SpeedBuzzerCounterSoloScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.SpeedBuzzerCounterSoloScore {

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

            this.numericUpDownCounterPositionX.Minimum = int.MinValue;
            this.numericUpDownCounterPositionX.Maximum = int.MaxValue;

            this.numericUpDownCounterPositionY.Minimum = int.MinValue;
            this.numericUpDownCounterPositionY.Maximum = int.MaxValue;

            this.comboBoxCounterStyle.BeginUpdate();
            this.comboBoxCounterStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.Score.Styles)));
            this.comboBoxCounterStyle.EndUpdate();

            this.numericUpDownLeftPlayerBuzzerChannel_1.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerBuzzerChannel_1.Maximum = int.MaxValue;

            this.numericUpDownLeftPlayerBuzzerChannel_2.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerBuzzerChannel_2.Maximum = int.MaxValue;

            this.numericUpDownLeftPlayerBuzzerChannel_3.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerBuzzerChannel_3.Maximum = int.MaxValue;

            this.numericUpDownLeftPlayerBuzzerChannel_4.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerBuzzerChannel_4.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerBuzzerChannel_1.Minimum = int.MinValue;
            this.numericUpDownRightPlayerBuzzerChannel_1.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerBuzzerChannel_2.Minimum = int.MinValue;
            this.numericUpDownRightPlayerBuzzerChannel_2.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerBuzzerChannel_3.Minimum = int.MinValue;
            this.numericUpDownRightPlayerBuzzerChannel_3.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerBuzzerChannel_4.Minimum = int.MinValue;
            this.numericUpDownRightPlayerBuzzerChannel_4.Maximum = int.MaxValue;

            this.numericUpDownDMXStartchannel_1.Minimum = int.MinValue;
            this.numericUpDownDMXStartchannel_1.Maximum = int.MaxValue;

            this.numericUpDownDMXStartchannel_2.Minimum = int.MinValue;
            this.numericUpDownDMXStartchannel_2.Maximum = int.MaxValue;

            this.numericUpDownDMXStartchannel_3.Minimum = int.MinValue;
            this.numericUpDownDMXStartchannel_3.Maximum = int.MaxValue;

            this.numericUpDownDMXStartchannel_4.Minimum = int.MinValue;
            this.numericUpDownDMXStartchannel_4.Maximum = int.MaxValue;

            this.numericUpDataSetScore.Minimum = int.MinValue;
            this.numericUpDataSetScore.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.business, "CounterPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownCounterPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "CounterPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownCounterPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "CounterStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxCounterStyle.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "IOUnitName");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.comboBoxIOUnit.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "IOUnitStatus");
            bind.Format += this.bind_comboBoxIOUnitName_BackColor;
            this.comboBoxIOUnit.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerBuzzerChannel_1");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerBuzzerChannel_1.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerBuzzerChannel_2");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerBuzzerChannel_2.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerBuzzerChannel_3");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerBuzzerChannel_3.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerBuzzerChannel_4");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerBuzzerChannel_4.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerBuzzerChannel_1");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerBuzzerChannel_1.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerBuzzerChannel_2");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerBuzzerChannel_2.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerBuzzerChannel_3");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerBuzzerChannel_3.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerBuzzerChannel_4");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerBuzzerChannel_4.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "DMXStartchannel_1");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownDMXStartchannel_1.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "DMXStartchannel_2");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownDMXStartchannel_2.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "DMXStartchannel_3");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownDMXStartchannel_3.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "DMXStartchannel_4");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownDMXStartchannel_4.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "RightColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelRightColor.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "LeftColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelRightColor.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "LeftColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelLeftColor.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "RightColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelLeftColor.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "OnColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelOnColor.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "OffColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelOnColor.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "OffColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelOffColor.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "OnColor");
            bind.Format += (s, e) => { e.Value = (Color)e.Value; };
            this.labelOffColor.DataBindings.Add(bind);

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
            this.numericUpDownLeftPlayerBuzzerChannel_1.DataBindings.Clear();
            this.numericUpDownLeftPlayerBuzzerChannel_2.DataBindings.Clear();
            this.numericUpDownLeftPlayerBuzzerChannel_3.DataBindings.Clear();
            this.numericUpDownLeftPlayerBuzzerChannel_4.DataBindings.Clear();
            this.numericUpDownRightPlayerBuzzerChannel_1.DataBindings.Clear();
            this.numericUpDownRightPlayerBuzzerChannel_2.DataBindings.Clear();
            this.numericUpDownRightPlayerBuzzerChannel_3.DataBindings.Clear();
            this.numericUpDownRightPlayerBuzzerChannel_4.DataBindings.Clear();

            this.numericUpDownDMXStartchannel_1.DataBindings.Clear();
            this.numericUpDownDMXStartchannel_2.DataBindings.Clear();
            this.numericUpDownDMXStartchannel_3.DataBindings.Clear();
            this.numericUpDownDMXStartchannel_4.DataBindings.Clear();

            this.numericUpDataSetScore.DataBindings.Clear();

            this.labelRightColor.DataBindings.Clear();
            this.labelLeftColor.DataBindings.Clear();
            this.labelOnColor.DataBindings.Clear();

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
                this.numericUpDataSetScore.Value = this.selectedDataset.Score;
                this.buttonDataRemoveSet.Enabled = true;
                if (this.listBoxDataList.Items.Count > this.selectedDatasetIndex) this.listBoxDataList.SelectedIndex = this.selectedDatasetIndex;
            }
            else {
                this.groupBoxDataset.Enabled = false;
                this.textBoxDataSetName.Text = string.Empty;
                this.numericUpDataSetScore.Value = 0;
                this.buttonDataRemoveSet.Enabled = false;
            }
            Helper.setControlBackColor(this.buttonDataRemoveSet);

            this.fillStepList();
        }

        private void setSelectedStep() {
            this.labelPosition_1.BackColor = Color.PaleTurquoise;
            this.labelPosition_2.BackColor = Color.PaleTurquoise;
            this.labelPosition_3.BackColor = Color.PaleTurquoise;
            this.labelPosition_4.BackColor = Color.PaleTurquoise;
            if (this.listBoxCourseSteps.SelectedIndex < 0) {
                this.groupBoxStep.Text = string.Empty;
            }
            else {
                int id = this.listBoxCourseSteps.SelectedIndex + 1;
                this.groupBoxStep.Text = string.Format("step {0} of {1}", id.ToString(), this.listBoxCourseSteps.Items.Count);
                StepPositions item = (StepPositions)this.listBoxCourseSteps.SelectedItem;
                switch (item) {
                    case StepPositions.Position_1:
                        this.labelPosition_1.BackColor = Color.Gold;
                        break;
                    case StepPositions.Position_2:
                        this.labelPosition_2.BackColor = Color.Gold;
                        break;
                    case StepPositions.Position_3:
                        this.labelPosition_3.BackColor = Color.Gold;
                        break;
                    case StepPositions.Position_4:
                        this.labelPosition_4.BackColor = Color.Gold;
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

        private void setPreviewData() {
            this.radioButtonSourceScore.Checked = !this.showCounterInsert;
            this.radioButtonSourceCounter.Checked = this.showCounterInsert;
            this.setScorePreview();
            this.setCounterPreview();
        }

        protected override void setScorePreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetScore(previewScene.Insert.Score, 3, 2);
                if (this.showCounterInsert) previewScene.Insert.Score.SetOut();
                else previewScene.Insert.Score.SetIn();
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

        #endregion


        #region Events.Incoming

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setPreviewData();
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
                else if (e.PropertyName == "Score") this.numericUpDataSetScore.Value = this.selectedDataset.Score;
            }
        }

        #endregion

        #region Events.Controls

        protected virtual void comboBoxIOUnit_TextChanged(object sender, EventArgs e) { if (this.business is Business) this.business.IOUnitName = this.comboBoxIOUnit.Text; }
        private void numericUpDownLeftPlayerBuzzerChannel_1_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftPlayerBuzzerChannel_1 = (int)this.numericUpDownLeftPlayerBuzzerChannel_1.Value; }
        private void numericUpDownLeftPlayerBuzzerChannel_2_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftPlayerBuzzerChannel_2 = (int)this.numericUpDownLeftPlayerBuzzerChannel_2.Value; }
        private void numericUpDownLeftPlayerBuzzerChannel_3_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftPlayerBuzzerChannel_3 = (int)this.numericUpDownLeftPlayerBuzzerChannel_3.Value; }
        private void numericUpDownLeftPlayerBuzzerChannel_4_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftPlayerBuzzerChannel_4 = (int)this.numericUpDownLeftPlayerBuzzerChannel_4.Value; }
        private void numericUpDownRightPlayerBuzzerChannel_1_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightPlayerBuzzerChannel_1 = (int)this.numericUpDownRightPlayerBuzzerChannel_1.Value; }
        private void numericUpDownRightPlayerBuzzerChannel_2_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightPlayerBuzzerChannel_2 = (int)this.numericUpDownRightPlayerBuzzerChannel_2.Value; }
        private void numericUpDownRightPlayerBuzzerChannel_3_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightPlayerBuzzerChannel_3 = (int)this.numericUpDownRightPlayerBuzzerChannel_3.Value; }
        private void numericUpDownRightPlayerBuzzerChannel_4_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightPlayerBuzzerChannel_4 = (int)this.numericUpDownRightPlayerBuzzerChannel_4.Value; }

        private void numericUpDownDMXStartchannel_1_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.DMXStartchannel_1 = (int)this.numericUpDownDMXStartchannel_1.Value; }
        private void buttonPosition_1_On_Click(object sender, EventArgs e) { if (this.business is Business) this.business.PanelOn(StepPositions.Position_1); }
        private void buttonPosition_1_Left_Click(object sender, EventArgs e) { if (this.business is Business) this.business.PanelLeft(StepPositions.Position_1); }
        private void buttonPosition_1_Right_Click(object sender, EventArgs e) { if (this.business is Business) this.business.PanelRight(StepPositions.Position_1); }
        private void buttonPosition_1_Off_Click(object sender, EventArgs e) { if (this.business is Business) this.business.PanelOff(StepPositions.Position_1); }

        private void numericUpDownDMXStartchannel_2_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.DMXStartchannel_2 = (int)this.numericUpDownDMXStartchannel_2.Value; }
        private void buttonPosition_2_On_Click(object sender, EventArgs e) { if (this.business is Business) this.business.PanelOn(StepPositions.Position_2); }
        private void buttonPosition_2_Left_Click(object sender, EventArgs e) { if (this.business is Business) this.business.PanelLeft(StepPositions.Position_2); }
        private void buttonPosition_2_Right_Click(object sender, EventArgs e) { if (this.business is Business) this.business.PanelRight(StepPositions.Position_2); }
        private void buttonPosition_2_Off_Click(object sender, EventArgs e) { if (this.business is Business) this.business.PanelOff(StepPositions.Position_2); }

        private void numericUpDownDMXStartchannel_3_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.DMXStartchannel_3 = (int)this.numericUpDownDMXStartchannel_3.Value; }
        private void buttonPosition_3_On_Click(object sender, EventArgs e) { if (this.business is Business) this.business.PanelOn(StepPositions.Position_3); }
        private void buttonPosition_3_Left_Click(object sender, EventArgs e) { if (this.business is Business) this.business.PanelLeft(StepPositions.Position_3); }
        private void buttonPosition_3_Right_Click(object sender, EventArgs e) { if (this.business is Business) this.business.PanelRight(StepPositions.Position_3); }
        private void buttonPosition_3_Off_Click(object sender, EventArgs e) { if (this.business is Business) this.business.PanelOff(StepPositions.Position_3); }

        private void numericUpDownDMXStartchannel_4_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.DMXStartchannel_4 = (int)this.numericUpDownDMXStartchannel_4.Value; }
        private void buttonPosition_4_On_Click(object sender, EventArgs e) { if (this.business is Business) this.business.PanelOn(StepPositions.Position_4); }
        private void buttonPosition_4_Left_Click(object sender, EventArgs e) { if (this.business is Business) this.business.PanelLeft(StepPositions.Position_4); }
        private void buttonPosition_4_Right_Click(object sender, EventArgs e) { if (this.business is Business) this.business.PanelRight(StepPositions.Position_4); }
        private void buttonPosition_4_Off_Click(object sender, EventArgs e) { if (this.business is Business) this.business.PanelOff(StepPositions.Position_4); }


        private void labelOffColor_Click(object sender, EventArgs e) {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.OffColor;
            switch (dialog.ShowDialog()) {
                case DialogResult.OK:
                    this.business.OffColor = dialog.Color;
                    break;
            }
        }

        private void labelLeftColor_Click(object sender, EventArgs e) {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.LeftColor;
            switch (dialog.ShowDialog()) {
                case DialogResult.OK:
                    this.business.LeftColor = dialog.Color;
                    break;
            }

        }

        private void labelRightColor_Click(object sender, EventArgs e) {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.business.RightColor;
            switch (dialog.ShowDialog()) {
                case DialogResult.OK:
                    this.business.RightColor = dialog.Color;
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

        private void numericUpDataSetScore_ValueChanged(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.Score = (int)this.numericUpDataSetScore.Value;
        }

        private void buttonAddStepsRandom_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.AddStepsRandom((int)this.numericUpDownAddStepsRandom.Value);
        }
        private void buttonClearSteps_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.Clear();
        }

        private void buttonInsertPosition_1_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                int index = this.listBoxCourseSteps.SelectedIndex + 1;
                this.selectedDataset.InsertStep(index, StepPositions.Position_1);
                this.listBoxCourseSteps.SelectedIndex = index;
            }
        }

        private void buttonSetPosition_1_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.SetStep(this.listBoxCourseSteps.SelectedIndex, StepPositions.Position_1);
        }

        private void buttonInsertPosition_2_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                int index = this.listBoxCourseSteps.SelectedIndex + 1;
                this.selectedDataset.InsertStep(index, StepPositions.Position_2);
                this.listBoxCourseSteps.SelectedIndex = index;
            }
        }

        private void buttonSetPosition_2_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.SetStep(this.listBoxCourseSteps.SelectedIndex, StepPositions.Position_2);
        }

        private void buttonInsertPosition_3_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                int index = this.listBoxCourseSteps.SelectedIndex + 1;
                this.selectedDataset.InsertStep(index, StepPositions.Position_3);
                this.listBoxCourseSteps.SelectedIndex = index;
            }
        }

        private void buttonSetPosition_3_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.SetStep(this.listBoxCourseSteps.SelectedIndex, StepPositions.Position_3);
        }

        private void buttonInsertPosition_4_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) {
                int index = this.listBoxCourseSteps.SelectedIndex + 1;
                this.selectedDataset.InsertStep(index, StepPositions.Position_4);
                this.listBoxCourseSteps.SelectedIndex = index;
            }
        }

        private void buttonSetPosition_4_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.SetStep(this.listBoxCourseSteps.SelectedIndex, StepPositions.Position_4);
        }


        private void buttonRemoveStep_Click(object sender, EventArgs e) {
            if (this.selectedDataset is DatasetContent) this.selectedDataset.RemoveStep(this.listBoxCourseSteps.SelectedIndex);
        }

        private void listBoxCourseSteps_SelectedIndexChanged(object sender, EventArgs e) {
            this.setSelectedStep();
        }

        private void radioButtonSourceScore_CheckedChanged(object sender, EventArgs e) { this.showCounterInsert = !this.radioButtonSourceScore.Checked; }
        private void radioButtonSourceCounter_CheckedChanged(object sender, EventArgs e) { this.showCounterInsert = this.radioButtonSourceCounter.Checked; }

        #endregion
    }
}
