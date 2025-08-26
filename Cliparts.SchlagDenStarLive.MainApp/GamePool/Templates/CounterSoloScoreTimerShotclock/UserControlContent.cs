using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.CounterSoloScoreTimerShotclock {

    public partial class UserControlContent : _Base.Score.UserControlContent {

        #region Properties

        private Business business;

        private bool previewSceneIsAvailable { get { return this.previewScene is VentuzScenes.GamePool.CounterSoloScoreTimer.Insert && this.previewScene.Status == VRemote4.HandlerSi.Scene.States.Available; } }

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

            this.numericUpDownShotclockStartTime.Minimum = int.MinValue;
            this.numericUpDownShotclockStartTime.Maximum = int.MaxValue;

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

            bind = new Binding("Value", this.business, "ShotclockStartTime");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownShotclockStartTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "ShotclockStartTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.numericUpDownShotclockStartTime.DataBindings.Add(bind);

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
            this.numericUpDownShotclockStartTime.DataBindings.Clear();
            this.numericUpDownShotclockStartTime.DataBindings.Clear();
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
            bool selected = ((VentuzScenes.GamePool.CounterSoloScoreTimer.Insert)this.previewScene) is VRemote4.HandlerSi.Scene;
            base.SetPreviewPipe(previewPipe);
            if (selected) this.Select();
        }

        public override void Select() {
            if (this.previewPipe is VRemote4.HandlerSi.Client.Pipe.Business &&
                this.previewPipe.Resolution.HasValue &&
                this.previewPipe.ShareHandle.HasValue) {
                base.select(new VentuzScenes.GamePool.CounterSoloScoreTimer.Insert(WindowsFormsSynchronizationContext.Current, this.previewPipe));
            }
        }

        protected override void setScorePreview() {
            if (this.previewSceneIsAvailable) {
                VentuzScenes.GamePool.CounterSoloScoreTimer.Insert previewScene = this.previewScene as VentuzScenes.GamePool.CounterSoloScoreTimer.Insert;
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

        protected void setCounterPreview() {
            if (this.previewSceneIsAvailable) {
                VentuzScenes.GamePool.CounterSoloScoreTimer.Insert previewScene = this.previewScene as VentuzScenes.GamePool.CounterSoloScoreTimer.Insert;
                previewScene.Counter.SetPositionX(this.business.CounterPositionX);
                previewScene.Counter.SetPositionY(this.business.CounterPositionY);
                previewScene.Counter.SetStyle(this.business.CounterStyle);
                previewScene.Counter.SetLeftTopName(this.business.LeftPlayerName);
                previewScene.Counter.SetLeftTopScore(99);
                previewScene.Counter.SetRightBottomName(this.business.RightPlayerName);
                previewScene.Counter.SetRightBottomScore(88);
                previewScene.Counter.SetIn();
            }
        }

        protected virtual void setTimerPreview() {
            if (this.previewSceneIsAvailable) {
                var previewScene = this.previewScene as VentuzScenes.GamePool.CounterSoloScoreTimer.Insert;
                previewScene.Timer.SetPositionX(this.business.TimerPositionX);
                previewScene.Timer.SetPositionY(this.business.TimerPositionY);
                previewScene.Timer.SetStyle(this.business.TimerStyle);
                previewScene.Timer.SetScaling(100);
                previewScene.Timer.SetIn();
            }
        }

        #endregion


        #region Events.Incoming

        protected override void previewScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            base.previewScene_StatusChanged(sender, e);
            this.setScorePreview();
            this.setCounterPreview();
            this.setTimerPreview();
        }


        void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "IOUnitList") this.fillIOUnitList();
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
        
        private void numericUpDownShotclockStartTime_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.ShotclockStartTime = (int)this.numericUpDownShotclockStartTime.Value; }

        #endregion

    }
}
