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

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.RotationCounterSoloScoreTimer {

    public partial class UserControlGame : _Base.Score.UserControlGame {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.numericUpDownLeftPlayerCounter.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerCounter.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerCounter.Minimum = int.MinValue;
            this.numericUpDownRightPlayerCounter.Maximum = int.MaxValue;

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;
            this.business.TimerAlarm1Fired += this.business_Alarm1Fired;
            this.business.TimerAlarm2Fired += this.business_Alarm2Fired;

            Binding bind;

            bind = new Binding("Text", this.business, "LeftPlayerIOUnitName");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxLeftPlayerIOUnitName.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "LeftPlayerIOUnitStatus");
            bind.Format += this.bind_textBoxIOUnitName_BackColor;
            this.textBoxLeftPlayerIOUnitName.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerIOUnitName");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxRightPlayerIOUnitName.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "RightPlayerIOUnitStatus");
            bind.Format += this.bind_textBoxIOUnitName_BackColor;
            this.textBoxRightPlayerIOUnitName.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerCurrentTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerCurrentTime.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "TimerIsRunning");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorEnabled : this.ForeColor; };
            this.labelTimerCurrentTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerStartTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerStartTimeText.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerExtraTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerExtraTimeText.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "RunExtraTime");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxTimerRunExtraTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerStopTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerStopTimeText.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerAlarmTime1");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.userControlRecTriggerTimerAlarmTime1.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerAlarmTime2");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.userControlRecTriggerTimerAlarmTime2.DataBindings.Add(bind);

            bind = new Binding("Enabled", this.business, "ShowFullscreenTimer");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.buttonVfullscreen_ShowTimer.DataBindings.Add(bind);

            bind = new Binding("Enabled", this.business, "ShowFullscreenTimer");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.buttonVfullscreen_StartTimer.DataBindings.Add(bind);

            bind = new Binding("Enabled", this.business, "ShowFullscreenTimer");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.buttonVfullscreen_StopTimer.DataBindings.Add(bind);

            bind = new Binding("Enabled", this.business, "ShowFullscreenTimer");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.buttonVfullscreen_ResetTimer.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "LeftPlayerName");
            this.textBoxLeftPlayerName_1.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerName");
            this.textBoxRightPlayerName_1.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerCounter");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerCounter.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerCounter");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerCounter.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "CounterFlipPlayers");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxCounterFlipPlayers.DataBindings.Add(bind);

            this.labelGameClass.Text = this.business.ClassInfo;
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
            this.business.TimerAlarm1Fired -= this.business_Alarm1Fired;
            this.business.TimerAlarm2Fired -= this.business_Alarm2Fired;

            this.textBoxLeftPlayerIOUnitName.DataBindings.Clear();
            this.textBoxRightPlayerIOUnitName.DataBindings.Clear();

            this.labelTimerCurrentTime.DataBindings.Clear();
            this.labelTimerStartTimeText.DataBindings.Clear();
            this.labelTimerExtraTimeText.DataBindings.Clear();
            this.checkBoxTimerRunExtraTime.DataBindings.Clear();
            this.labelTimerStopTimeText.DataBindings.Clear();
            this.userControlRecTriggerTimerAlarmTime1.DataBindings.Clear();
            this.userControlRecTriggerTimerAlarmTime2.DataBindings.Clear();

            this.textBoxLeftPlayerName_1.DataBindings.Clear();
            this.textBoxRightPlayerName_1.DataBindings.Clear();

            this.numericUpDownLeftPlayerCounter.DataBindings.Clear();
            this.numericUpDownRightPlayerCounter.DataBindings.Clear();
            this.checkBoxCounterFlipPlayers.DataBindings.Clear();
        }

        protected override void buildStepList() {

            int index = 0;
            stepAction step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_LoadScene);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVgraphic_Clear);
            step.AddButton(this.buttonVstage_Clear);
            step.AddButton(this.buttonGame_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_ShowTimer);
            step.AddButton(this.buttonVstage_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_CounterIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimerIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_StartTimer);
            step.AddButton(this.buttonVinsert_StartTimer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex);
            step.AddButton(this.buttonVinsert_SetCounter);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_StopTimer);
            step.AddButton(this.buttonVinsert_StopTimer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimerOut);
            step.AddButton(this.buttonVfullscreen_ResetTimer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_CounterOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ScoreIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 8);
            step.AddButton(this.buttonVinsert_ScoreOut);
            step.AddButton(this.buttonGame_Next);
            this.stepList.Add(step);

            index++;
            this.winnerStepIndex = index;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVgraphic_Clear);
            step.AddButton(this.buttonVstage_Clear);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_UnloadScene);
            step.AddButton(this.buttonVfullscreen_ShowGameboard);
            step.AddButton(this.buttonGame_SetWinner);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_UpdateGameboard);
            this.stepList.Add(step);

            if (index < this.stepsMaxIndex) this.on_Error("buildStepList", "stepList to short");
            if (index > this.stepsMaxIndex) this.on_Error("buildStepList", "stepList to long");
        }

        public override void ParseKey(
            Keys keycode) {
            base.ParseKey(keycode);
            if (this.keyControl) {
                switch (keycode) {
                    case Keys.Q:
                        this.business.DoCounter(Content.Gameboard.PlayerSelection.LeftPlayer);
                        break;
                    case Keys.P:
                        this.business.DoCounter(Content.Gameboard.PlayerSelection.RightPlayer);
                        break;
                    case Keys.Left:
                        this.business.DoCounter(Content.Gameboard.PlayerSelection.LeftPlayer);
                        break;
                    case Keys.Right:
                        this.business.DoCounter(Content.Gameboard.PlayerSelection.RightPlayer);
                        break;
                }
            }
        }

        private void bind_textBoxIOUnitName_BackColor(object sender, ConvertEventArgs e) {
            switch ((VeloUnitStates)e.Value) {
                case VeloUnitStates.NotAvailable:
                    e.Value = Constants.ColorMissing;
                    break;
                case VeloUnitStates.Missing:
                    e.Value = Constants.ColorMissing;
                    break;
                case VeloUnitStates.Disconnected:
                    e.Value = Constants.ColorDisabled;
                    break;
                case VeloUnitStates.Connecting:
                    e.Value = Constants.ColorEnabling;
                    break;
                case VeloUnitStates.Connected:
                    e.Value = SystemColors.Control;
                    break;
                case VeloUnitStates.Locked:
                    e.Value = SystemColors.Control;
                    break;
                case VeloUnitStates.DoubleMode:
                    e.Value = Constants.ColorBuzzered;
                    break;
                case VeloUnitStates.SingleMode:
                    e.Value = Constants.ColorEnabled;
                    break;
                default:
                    break;
            }
        }

        #endregion


        #region Events.Incoming

        protected override void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.business_PropertyChanged(sender, e);
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                //if (e.PropertyName == "NameList") this.fillDataList();
                //else if (e.PropertyName == "SampleIncluded") this.fillDataList();
                //else if (e.PropertyName == "SelectedDataset") this.selectDataset(this.business.SelectedDataset);
                //else if (e.PropertyName == "SelectedDatasetIndex") this.selectDataList();
            }
        }

        void business_Alarm1Fired(object sender, EventArgs e) {
            if (this.InvokeRequired)
                this.Invoke((MethodInvoker)(() => this.business_Alarm1Fired(sender, e)));
            else { this.userControlRecTriggerTimerAlarmTime1.StartTrigger(); }
        }

        void business_Alarm2Fired(object sender, EventArgs e) {
            if (this.InvokeRequired)
                this.Invoke((MethodInvoker)(() => this.business_Alarm2Fired(sender, e)));
            else { this.userControlRecTriggerTimerAlarmTime2.StartTrigger(); }
        }

        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        protected virtual void numericUpDownLeftPlayerCounter_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerCounter = (int)this.numericUpDownLeftPlayerCounter.Value; }
        protected virtual void buttonLeftPlayerAddCounterHot_01_Click(object sender, EventArgs e) {
            this.business.LeftPlayerCounter++;
            this.business.Vinsert_SetCounter();
        }

        protected virtual void numericUpDownRightPlayerCounter_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerCounter = (int)this.numericUpDownRightPlayerCounter.Value; }
        protected virtual void buttonRightPlayerAddCounterHot_01_Click(object sender, EventArgs e) {
            this.business.RightPlayerCounter++;
            this.business.Vinsert_SetCounter();
        }

        private void checkBoxCounterFlipPlayers_CheckedChanged(object sender, EventArgs e) { this.business.CounterFlipPlayers = this.checkBoxCounterFlipPlayers.Checked; }

        protected void buttonReleaseBuzzer_Click(object sender, EventArgs e) { if (this.business is Business) this.business.ReleaseBuzzer(); }
        protected void buttonLockBuzzer_Click(object sender, EventArgs e) { if (this.business is Business) this.business.LockBuzzer(); }

        protected virtual void buttonTimerIn_Click(object sender, EventArgs e) { this.business.Vinsert_TimerIn(); }
        protected virtual void buttonTimerOut_Click(object sender, EventArgs e) { this.business.Vinsert_TimerOut(); }
        protected virtual void buttonTimerStart_Click(object sender, EventArgs e) {
            this.business.Vinsert_StartTimer();
            this.business.Vfullscreen_StartTimer();
        }
        protected virtual void buttonTimerStop_Click(object sender, EventArgs e) {
            this.business.Vinsert_StopTimer();
            this.business.Vfullscreen_StopTimer();
        }
        protected virtual void buttonTimerContinue_Click(object sender, EventArgs e) {
            this.business.Vinsert_ContinueTimer();
            this.business.Vfullscreen_ContinueTimer();
        }
        protected virtual void buttonTimerReset_Click(object sender, EventArgs e) {
            this.business.Vinsert_ResetTimer();
            this.business.Vfullscreen_ResetTimer();
        }
        protected virtual void checkBoxTimerRunExtraTime_CheckedChanged(object sender, EventArgs e) { this.business.RunExtraTime = this.checkBoxTimerRunExtraTime.Checked; }

        protected void buttonVinsert_CounterIn_Click(object sender, EventArgs e) { this.business.Vinsert_CounterIn(); }
        protected void buttonVinsert_SetCounter_Click(object sender, EventArgs e) { this.business.Vinsert_SetCounter(); }
        protected void buttonVinsert_CounterOut_Click(object sender, EventArgs e) { this.business.Vinsert_CounterOut(); }
        protected virtual void buttonVinsert_TimerIn_Click(object sender, EventArgs e) { this.business.Vinsert_TimerIn(); }
        protected virtual void buttonVinsert_StartTimer_Click(object sender, EventArgs e) { this.business.Vinsert_StartTimer(); }
        protected virtual void buttonVinsert_StopTimer_Click(object sender, EventArgs e) { this.business.Vinsert_StopTimer(); }
        protected virtual void buttonVinsert_TimerOut_Click(object sender, EventArgs e) { this.business.Vinsert_TimerOut(); }

        protected virtual void buttonVfullscreen_StartTimer_Click(object sender, EventArgs e) { this.business.Vfullscreen_StartTimer(); }
        protected virtual void buttonVfullscreen_StopTimer_Click(object sender, EventArgs e) { this.business.Vfullscreen_StopTimer(); }
        protected virtual void buttonVfullscreen_ResetTimer_Click(object sender, EventArgs e) { this.business.Vfullscreen_ResetTimer(); }

        protected void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }

        #endregion

    }
}
