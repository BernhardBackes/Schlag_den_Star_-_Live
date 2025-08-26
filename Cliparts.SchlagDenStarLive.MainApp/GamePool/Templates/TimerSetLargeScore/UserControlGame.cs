using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimerSetLargeScore {

    public partial class UserControlGame : _Base.SetLarge.UserControlGame {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.numericUpDownLeftPlayerScore.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerScore.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerScore.Minimum = int.MinValue;
            this.numericUpDownRightPlayerScore.Maximum = int.MaxValue;

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            this.business = business;
            this.business.TimerAlarm1Fired += this.business_Alarm1Fired;
            this.business.TimerAlarm2Fired += this.business_Alarm2Fired;

            Binding bind;

            bind = new Binding("Value", this.business, "LeftPlayerScore");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerScore.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerScore");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerScore.DataBindings.Add(bind);

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

            this.business.TimerAlarm1Fired -= this.business_Alarm1Fired;
            this.business.TimerAlarm2Fired -= this.business_Alarm2Fired;

            this.numericUpDownLeftPlayerScore.DataBindings.Clear();
            this.numericUpDownRightPlayerScore.DataBindings.Clear();

            this.labelTimerCurrentTime.DataBindings.Clear();
            this.labelTimerStartTimeText.DataBindings.Clear();
            this.labelTimerExtraTimeText.DataBindings.Clear();
            this.checkBoxTimerRunExtraTime.DataBindings.Clear();
            this.labelTimerStopTimeText.DataBindings.Clear();
            this.userControlRecTriggerTimerAlarmTime1.DataBindings.Clear();
            this.userControlRecTriggerTimerAlarmTime2.DataBindings.Clear();

            this.buttonVfullscreen_ShowTimer.DataBindings.Clear();
            this.buttonVfullscreen_StartTimer.DataBindings.Clear();
            this.buttonVfullscreen_StopTimer.DataBindings.Clear();
            this.buttonVfullscreen_ResetTimer.DataBindings.Clear();

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
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimerIn);
            step.AddButton(this.buttonVfullscreen_ResetTimer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_StartTimer);
            step.AddButton(this.buttonVinsert_StartTimer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_StopTimer);
            step.AddButton(this.buttonVinsert_StopTimer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimerOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_SetLargeIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, this.checkForSetStates);
            step.AddButton(this.buttonVinsert_SetLargeOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ScoreIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 7);
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
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_SetWinner);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_UpdateGameboard);
            this.stepList.Add(step);

            if (index < this.stepsMaxIndex) this.on_Error("buildStepList", "stepList to short");
            if (index > this.stepsMaxIndex) this.on_Error("buildStepList", "stepList to long");
        }
        protected int checkForSetStates(
            int stepIndex) {
            if (this.business.LeftPlayerSetStatus == VentuzScenes.GamePool._Modules.SetLarge.ValueStates.Idle ||
                this.business.RightPlayerSetStatus == VentuzScenes.GamePool._Modules.SetLarge.ValueStates.Idle) {
                // es fehlt mindestens ein Eintrag
                return stepIndex - 5;
            }
            else return stepIndex + 1;
        }


        #endregion


        #region Events.Incoming

        void business_Alarm1Fired(object sender, EventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_Alarm1Fired(sender, e)));
            else { this.userControlRecTriggerTimerAlarmTime1.StartTrigger(); }
        }

        void business_Alarm2Fired(object sender, EventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_Alarm2Fired(sender, e)));
            else { this.userControlRecTriggerTimerAlarmTime2.StartTrigger(); }
        }

        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        protected virtual void numericUpDownLeftPlayerScore_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerScore = (int)this.numericUpDownLeftPlayerScore.Value; }
        protected virtual void buttonLeftPlayerAddScoreHot_01_Click(object sender, EventArgs e) {
            this.business.LeftPlayerScore++;
            this.business.Vinsert_SetScore();
        }

        protected virtual void numericUpDownRightPlayerScore_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerScore = (int)this.numericUpDownRightPlayerScore.Value; }
        protected virtual void buttonRightPlayerAddScoreHot_01_Click(object sender, EventArgs e) {
            this.business.RightPlayerScore++;
            this.business.Vinsert_SetScore();
        }

        protected virtual void buttonTimerIn_Click(object sender, EventArgs e) { this.business.Vinsert_TimerIn(); }
        protected virtual void buttonTimerOut_Click(object sender, EventArgs e) { this.business.Vinsert_TimerOut(); }
        protected virtual void buttonTimerStart_Click(object sender, EventArgs e) { this.business.Vinsert_StartTimer(); }
        protected virtual void buttonTimerStop_Click(object sender, EventArgs e) { this.business.Vinsert_StopTimer(); }
        protected virtual void buttonTimerContinue_Click(object sender, EventArgs e) { this.business.Vinsert_ContinueTimer(); }
        protected virtual void buttonTimerReset_Click(object sender, EventArgs e) { this.business.Vinsert_ResetTimer(); }
        protected virtual void checkBoxTimerRunExtraTime_CheckedChanged(object sender, EventArgs e) { this.business.RunExtraTime = this.checkBoxTimerRunExtraTime.Checked; }

        protected virtual void buttonVinsert_TimerIn_Click(object sender, EventArgs e) { this.business.Vinsert_TimerIn(); }
        protected virtual void buttonVinsert_StartTimer_Click(object sender, EventArgs e) { this.business.Vinsert_StartTimer(); }
        protected virtual void buttonVinsert_StopTimer_Click(object sender, EventArgs e) { this.business.Vinsert_StopTimer(); }
        protected virtual void buttonVinsert_TimerOut_Click(object sender, EventArgs e) { this.business.Vinsert_TimerOut(); }
        private void buttonVinsert_SetLargeOut_Click(object sender, EventArgs e) { this.business.Vinsert_SetLargeOut(); }
        private void buttonVinsert_ScoreIn_Click(object sender, EventArgs e) { this.business.Vinsert_ScoreIn(); }
        private void buttonVinsert_ScoreOut_Click(object sender, EventArgs e) { this.business.Vinsert_ScoreOut(); }

        protected virtual void buttonVfullscreen_StartTimer_Click(object sender, EventArgs e) { this.business.Vfullscreen_StartTimer(); }
        protected virtual void buttonVfullscreen_StopTimer_Click(object sender, EventArgs e) { this.business.Vfullscreen_StopTimer(); }
        protected virtual void buttonVfullscreen_ResetTimer_Click(object sender, EventArgs e) { this.business.Vfullscreen_ResetTimer(); }

        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }

        #endregion

    }
}
