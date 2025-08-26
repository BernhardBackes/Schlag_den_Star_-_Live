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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.ShootingTimerForTwoSoloScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.ShootingTimerForTwoSoloScore {

    public partial class UserControlContent : _Base.Score.UserControlContent {

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

            this.numericUpDownShootingPositionX.Minimum = int.MinValue;
            this.numericUpDownShootingPositionX.Maximum = int.MaxValue;

            this.numericUpDownShootingPositionY.Minimum = int.MinValue;
            this.numericUpDownShootingPositionY.Maximum = int.MaxValue;

            this.comboBoxShootingStyle.BeginUpdate();
            this.comboBoxShootingStyle.Items.AddRange(Enum.GetNames(typeof(VentuzScenes.GamePool._Modules.ShootingTimerForTwo.Styles)));
            this.comboBoxShootingStyle.EndUpdate();

            this.numericUpDownShootingHitsCount.Minimum = int.MinValue;
            this.numericUpDownShootingHitsCount.Maximum = int.MaxValue;

            this.numericUpDownLeftPlayerBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerBuzzerChannel.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownRightPlayerBuzzerChannel.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            base.Pose(business, previewPipe);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.business, "ShootingPositionX");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownShootingPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "ShootingPositionY");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownShootingPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "ShootingStyle");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxShootingStyle.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "HitsCount");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownShootingHitsCount.DataBindings.Add(bind);

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

            bind = new Binding("Checked", this.business, "FinishMode");
            bind.Format += (s, e) => { e.Value = (Business.FinishModes)e.Value == Business.FinishModes.Reaching; };
            this.radioButtonFinishModeReaching.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "FinishMode");
            bind.Format += (s, e) => { e.Value = (Business.FinishModes)e.Value == Business.FinishModes.Crossing; };
            this.radioButtonFinishModeCrossing.DataBindings.Add(bind);

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

            this.comboBoxIOUnit.DataBindings.Clear();
            this.numericUpDownLeftPlayerBuzzerChannel.DataBindings.Clear();
            this.numericUpDownRightPlayerBuzzerChannel.DataBindings.Clear();

            this.numericUpDownShootingPositionX.DataBindings.Clear();
            this.numericUpDownShootingPositionY.DataBindings.Clear();
            this.comboBoxShootingStyle.DataBindings.Clear();
            this.numericUpDownShootingHitsCount.DataBindings.Clear();

            this.radioButtonFinishModeReaching.DataBindings.Clear();
            this.radioButtonFinishModeCrossing.DataBindings.Clear();
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
                    this.setShootingPreview();
                    previewScene.Insert.Score.SetOut();
                    previewScene.Insert.ShootingTimerForTwo.SetIn();
                }
                else {
                    this.setScorePreview();
                    previewScene.Insert.ShootingTimerForTwo.SetOut();
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

        protected void setShootingPreview() {
            if (this.previewSceneIsAvailable) {
                Preview previewScene = this.previewScene as Preview;
                this.business.Vinsert_SetShooting(previewScene.Insert.ShootingTimerForTwo);
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

        protected virtual void numericUpDownShootingPositionX_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.ShootingPositionX = (int)this.numericUpDownShootingPositionX.Value;
                this.setShootingPreview();
            }
        }
        protected virtual void numericUpDownShootingPositionY_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.ShootingPositionY = (int)this.numericUpDownShootingPositionY.Value;
                this.setShootingPreview();
            }
        }
        protected virtual void comboBoxShootingStyle_SelectedIndexChanged(object sender, EventArgs e) {
            VentuzScenes.GamePool._Modules.ShootingTimerForTwo.Styles style;
            if (this.business is Business &&
                Enum.TryParse(this.comboBoxShootingStyle.Text, out style)) {
                this.business.ShootingStyle = style;
                this.setShootingPreview();
            }
        }
        private void numericUpDownShootingHitsCount_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) {
                this.business.HitsCount = (int)this.numericUpDownShootingHitsCount.Value;
                this.setShootingPreview();
            }
        }

        protected virtual void comboBoxIOUnit_TextChanged(object sender, EventArgs e) { if (this.business is Business) this.business.IOUnitName = this.comboBoxIOUnit.Text; }
        protected virtual void numericUpDownLeftPlayerBuzzerChannel_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerBuzzerChannel = (int)this.numericUpDownLeftPlayerBuzzerChannel.Value; }
        protected virtual void numericUpDownRightPlayerBuzzerChannel_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerBuzzerChannel = (int)this.numericUpDownRightPlayerBuzzerChannel.Value; }

        private void radioButtonFinishModeReaching_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonFinishModeReaching.Checked) this.business.FinishMode = Business.FinishModes.Reaching; }
        private void radioButtonFinishModeCrossing_CheckedChanged(object sender, EventArgs e) { if (this.radioButtonFinishModeCrossing.Checked) this.business.FinishMode = Business.FinishModes.Crossing; }

        private void radioButtonSourceTimer_CheckedChanged(object sender, EventArgs e) { this.showTimerInsert = this.radioButtonSourceTimer.Checked; }
        private void radioButtonSourceScore_CheckedChanged(object sender, EventArgs e) { this.showTimerInsert = !this.radioButtonSourceScore.Checked; }

        #endregion
    }

}
