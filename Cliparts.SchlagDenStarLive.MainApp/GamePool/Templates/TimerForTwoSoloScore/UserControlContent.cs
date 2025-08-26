using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.AMB;
using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimerForTwoSoloScore;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimerForTwoSoloScore {

    public partial class UserControlContent : _Base.BuzzerScore.UserControlContent {

        #region Properties

        private Business business;

        private bool _showTimerInsert = true;
        public bool showTimerInsert {
            get { return this._showTimerInsert; }
            set {
                if (this._showTimerInsert != value) {
                    this._showTimerInsert = value;
                    this.setPreviewData();
                }
            }
        }

        private bool previewSceneIsAvailable { get { return this.previewScene is Preview && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

        #endregion


        #region Funktionen

        public UserControlContent() {
            InitializeComponent();

            this.numericUpDownTimerPositionX.Minimum = int.MinValue;
            this.numericUpDownTimerPositionX.Maximum = int.MaxValue;

            this.numericUpDownTimerPositionY.Minimum = int.MinValue;
            this.numericUpDownTimerPositionY.Maximum = int.MaxValue;

            this.numericUpDownLeftPlayerBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerBuzzerChannel.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownRightPlayerBuzzerChannel.Maximum = int.MaxValue;

            this.numericUpDownTimerPositionX.Minimum = int.MinValue;
            this.numericUpDownTimerPositionX.Maximum = int.MaxValue;

            this.numericUpDownTimerPositionY.Minimum = int.MinValue;
            this.numericUpDownTimerPositionY.Maximum = int.MaxValue;

            this.numericUpDownLeftPoleStartChannel1.Minimum = int.MinValue;
            this.numericUpDownLeftPoleStartChannel1.Maximum = int.MaxValue;

            this.numericUpDownLeftPoleStartChannel2.Minimum = int.MinValue;
            this.numericUpDownLeftPoleStartChannel2.Maximum = int.MaxValue;

            this.numericUpDownLeftPoleStartChannel3.Minimum = int.MinValue;
            this.numericUpDownLeftPoleStartChannel3.Maximum = int.MaxValue;

            this.numericUpDownLeftPoleStartChannel4.Minimum = int.MinValue;
            this.numericUpDownLeftPoleStartChannel4.Maximum = int.MaxValue;

            this.numericUpDownRightPoleStartChannel1.Minimum = int.MinValue;
            this.numericUpDownRightPoleStartChannel1.Maximum = int.MaxValue;

            this.numericUpDownRightPoleStartChannel2.Minimum = int.MinValue;
            this.numericUpDownRightPoleStartChannel2.Maximum = int.MaxValue;

            this.numericUpDownRightPoleStartChannel3.Minimum = int.MinValue;
            this.numericUpDownRightPoleStartChannel3.Maximum = int.MaxValue;

            this.numericUpDownRightPoleStartChannel4.Minimum = int.MinValue;
            this.numericUpDownRightPoleStartChannel4.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Text", this.business, "TimelineName");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.textBoxTimelineName.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "TimelineStatus");
            bind.Format += this.bind_textBoxTimelineName_BackColor;
            this.textBoxTimelineName.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "LeftPlayerTransponderCode");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.textBoxLeftPlayerTransponderCode.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerTransponderCode");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.textBoxRightPlayerTransponderCode.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimerPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimerPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimerPositionY.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "FinishMode");
            bind.Format += (s, e) => { e.Value = (Business.FinishModes)e.Value == Business.FinishModes.Reaching; };
            this.radioButtonFinishModeReaching.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "FinishMode");
            bind.Format += (s, e) => { e.Value = (Business.FinishModes)e.Value == Business.FinishModes.Crossing; };
            this.radioButtonFinishModeCrossing.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "BuzzerMode");
            bind.Format += (s, e) => { e.Value = Convert.ToBoolean(e.Value); };
            this.checkBoxBuzzerMode.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPoleStartChannel1");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPoleStartChannel1.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPoleStartChannel2");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPoleStartChannel2.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPoleStartChannel3");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPoleStartChannel3.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPoleStartChannel4");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPoleStartChannel4.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPoleStartChannel1");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPoleStartChannel1.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPoleStartChannel2");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPoleStartChannel2.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPoleStartChannel3");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPoleStartChannel3.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPoleStartChannel4");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPoleStartChannel4.DataBindings.Add(bind);

            this.labelGameClass.Text = this.business.ClassInfo;

            this.fillIOUnitList();
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

            this.textBoxTimelineName.DataBindings.Clear();
            this.textBoxLeftPlayerTransponderCode.DataBindings.Clear();
            this.textBoxRightPlayerTransponderCode.DataBindings.Clear();

            this.numericUpDownTimerPositionX.DataBindings.Clear();
            this.numericUpDownTimerPositionY.DataBindings.Clear();

            this.radioButtonFinishModeReaching.DataBindings.Clear();
            this.radioButtonFinishModeCrossing.DataBindings.Clear();

            this.checkBoxBuzzerMode.DataBindings.Clear();

            this.numericUpDownLeftPoleStartChannel1.DataBindings.Clear();
            this.numericUpDownLeftPoleStartChannel2.DataBindings.Clear();
            this.numericUpDownLeftPoleStartChannel3.DataBindings.Clear();
            this.numericUpDownLeftPoleStartChannel4.DataBindings.Clear();

            this.numericUpDownRightPoleStartChannel1.DataBindings.Clear();
            this.numericUpDownRightPoleStartChannel2.DataBindings.Clear();
            this.numericUpDownRightPoleStartChannel3.DataBindings.Clear();
            this.numericUpDownRightPoleStartChannel4.DataBindings.Clear();
        }

        private void bind_textBoxTimelineName_BackColor(object sender, ConvertEventArgs e) {
            switch ((TimelineStates)e.Value) {
                case TimelineStates.Offline:
                    e.Value = Constants.ColorMissing;
                    break;
                case TimelineStates.Locked:
                    e.Value = SystemColors.Control;
                    break;
                case TimelineStates.Unlocked:
                    e.Value = Constants.ColorEnabled;
                    break;
            }
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
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.radioButtonSourceScore.Checked = !this.showTimerInsert;
                this.radioButtonSourceTimer.Checked = this.showTimerInsert;
                if (this.showTimerInsert) {
                    this.setTimerPreview();
                    previewScene.Insert.Score.SetOut();
                    previewScene.Insert.SetIn();
                }
                else {
                    this.setScorePreview();
                    previewScene.Insert.SetOut();
                    previewScene.Insert.Score.SetIn();
                }
            }
        }

        protected override void setScorePreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetScore(previewScene.Insert.Score, 2, 3);
            }
        }

        protected void setTimerPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetTimer(previewScene.Insert);
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
                if (e.PropertyName == "IOUnitNameList") this.fillIOUnitList();
            }
        }

        #endregion

        #region Events.Controls

        private void textBoxTimelineName_TextChanged(object sender, EventArgs e) { if (this.business is Business) this.business.TimelineName = this.textBoxTimelineName.Text; }
        private void textBoxLeftPlayerTransponderCode_TextChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftPlayerTransponderCode = this.textBoxLeftPlayerTransponderCode.Text; }
        private void textBoxRightPlayerTransponderCode_TextChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightPlayerTransponderCode = this.textBoxRightPlayerTransponderCode.Text; }

        protected virtual void numericUpDownTimerPositionX_ValueChanged(object sender, EventArgs e) {
            this.business.TimerPositionX = (int)this.numericUpDownTimerPositionX.Value;
            this.setTimerPreview();
        }
        protected virtual void numericUpDownTimerPositionY_ValueChanged(object sender, EventArgs e) {
            this.business.TimerPositionY = (int)this.numericUpDownTimerPositionY.Value;
            this.setTimerPreview();
        }

        private void radioButtonFinishModeReaching_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonFinishModeReaching.Checked) this.business.FinishMode = Business.FinishModes.Reaching; }
        private void radioButtonFinishModeCrossing_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonFinishModeCrossing.Checked) this.business.FinishMode = Business.FinishModes.Crossing; }
        private void checkBoxBuzzerMode_CheckedChanged(object sender, EventArgs e) { this.business.BuzzerMode = this.checkBoxBuzzerMode.Checked; }

        private void numericUpDownLeftPlayerDMXStartchannel_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftPlayerDMXStartchannel = (int)this.numericUpDownLeftPlayerDMXStartchannel.Value; }
        private void buttonLeftPlayerOn_Click(object sender, EventArgs e) { this.business.SetLeftPlayerOn(); }
        private void buttonLeftPlayerOff_Click(object sender, EventArgs e) { this.business.SetLeftPlayerOff(); }

        private void numericUpDownRightPlayerDMXStartchannel_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightPlayerDMXStartchannel = (int)this.numericUpDownRightPlayerDMXStartchannel.Value; }
        private void buttonRightPlayerOn_Click(object sender, EventArgs e) { this.business.SetRightPlayerOn(); }
        private void buttonRightPlayerOff_Click(object sender, EventArgs e) { this.business.SetRightPlayerOff(); }

        private void numericUpDownLeftPoleStartChannel1_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftPoleStartChannel1 = (int)this.numericUpDownLeftPoleStartChannel1.Value; }
        private void numericUpDownLeftPoleStartChannel2_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftPoleStartChannel2 = (int)this.numericUpDownLeftPoleStartChannel2.Value; }
        private void numericUpDownLeftPoleStartChannel3_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftPoleStartChannel3 = (int)this.numericUpDownLeftPoleStartChannel3.Value; }
        private void numericUpDownLeftPoleStartChannel4_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftPoleStartChannel4 = (int)this.numericUpDownLeftPoleStartChannel4.Value; }

        private void numericUpDownRightPoleStartChannel1_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightPoleStartChannel1 = (int)this.numericUpDownRightPoleStartChannel1.Value; }
        private void numericUpDownRightPoleStartChannel2_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightPoleStartChannel2 = (int)this.numericUpDownRightPoleStartChannel2.Value; }
        private void numericUpDownRightPoleStartChannel3_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightPoleStartChannel3 = (int)this.numericUpDownRightPoleStartChannel3.Value; }
        private void numericUpDownRightPoleStartChannel4_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightPoleStartChannel4 = (int)this.numericUpDownRightPoleStartChannel4.Value; }

        private void buttonBlack_Click(object sender, EventArgs e) { this.business.AllLightsBlack(); }

        private void radioButtonSourceTimer_CheckedChanged(object sender, EventArgs e) { this.showTimerInsert = this.radioButtonSourceTimer.Checked; }
        private void radioButtonSourceScore_CheckedChanged(object sender, EventArgs e) { this.showTimerInsert = !this.radioButtonSourceScore.Checked; }

        #endregion

    }

}
