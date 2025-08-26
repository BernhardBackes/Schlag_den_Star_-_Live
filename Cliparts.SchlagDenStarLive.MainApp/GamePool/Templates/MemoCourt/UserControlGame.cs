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

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.MemoCourt {

    public partial class UserControlGame : _Base.UserControlGame {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            this.business = business;
            this.business.TimerAlarm1Fired += this.business_Alarm1Fired;
            this.business.TimerAlarm2Fired += this.business_Alarm2Fired;

            Binding bind;

            bind = new Binding("Text", this.business, "TimerCurrentTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerCurrentTime.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "TimerIsRunning");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorEnabled : this.ForeColor; };
            this.labelTimerCurrentTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerStartTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerStartTimeText.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerStopTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerStopTimeText.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerAlarmTime1");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.userControlRecTriggerTimerAlarmTime1.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerAlarmTime2");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.userControlRecTriggerTimerAlarmTime2.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "SpeedcourtClientName");
            bind.Format += (s, e) => { e.Value = Convert.ToString(e.Value); };
            this.textBoxServer.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "SpeedcourtServerIsRunning");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? "stop" : "start"; };
            this.buttonServerListen.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "SpeedcourtServerIsRunning");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorEnabled : Constants.ColorDisabled; };
            this.buttonServerListen.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "Sequence");
            bind.Format += (s, e) => { e.Value = Convert.ToString(e.Value); };
            this.textBoxSequence.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "SequenceIsStopped");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorDisabled : SystemColors.Control; };
            this.textBoxSequence.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "SequenceProgress");
            bind.Format += (s, e) => { e.Value = Convert.ToInt32(e.Value); };
            this.numericUpDownSequenceProgress.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "ReferenceCounter");
            bind.Format += (s, e) => { e.Value = Convert.ToInt32(e.Value); };
            this.numericUpDownReferenceCounter.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "ReferenceTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.textBoxReferenceTime.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "ShowReference");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxShowReference.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "Counter");
            bind.Format += (s, e) => { e.Value = Convert.ToInt32(e.Value); };
            this.numericUpDownCounter.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "Time");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.textBoxTime.DataBindings.Add(bind);

            this.setSelectedPlayer();

            this.fill_listBoxCourses(this.business.TaskNameList);
            this.listBoxTasks.Text = this.business.SelectedTask;

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

            this.labelTimerCurrentTime.DataBindings.Clear();
            this.labelTimerStartTimeText.DataBindings.Clear();
            this.labelTimerStopTimeText.DataBindings.Clear();
            this.userControlRecTriggerTimerAlarmTime1.DataBindings.Clear();
            this.userControlRecTriggerTimerAlarmTime2.DataBindings.Clear();
            this.textBoxServer.DataBindings.Clear();
            this.buttonServerListen.DataBindings.Clear();
            this.buttonServerListen.DataBindings.Clear();
            this.textBoxSequence.DataBindings.Clear();
            this.numericUpDownSequenceProgress.DataBindings.Clear();
            this.numericUpDownReferenceCounter.DataBindings.Clear();
            this.textBoxReferenceTime.DataBindings.Clear();
            this.checkBoxShowReference.DataBindings.Clear();
            this.numericUpDownCounter.DataBindings.Clear();
            this.textBoxTime.DataBindings.Clear();
        }

        protected override void buildStepList() {

            int index = 0;

            stepAction step;

            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_LoadScene);
            step.AddButton(this.buttonVfullscreen_LoadScene);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVgraphic_Clear);
            step.AddButton(this.buttonVstage_Clear);
            step.AddButton(this.buttonGame_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_ShowGame);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_SequenceOut);
            step.AddButton(this.buttonVfullscreen_SequenceOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_CountdownIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimerIn);
            step.AddButton(this.buttonVinsert_CounterIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimerOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_CounterOut);
            step.AddButton(this.buttonVfullscreen_CountdownOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_SequenceOut_1);
            step.AddButton(this.buttonVfullscreen_SequenceOut_1);
            step.AddButton(this.buttonGame_NextPlayer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_SequenceOut_2);
            step.AddButton(this.buttonVfullscreen_SequenceOut_2);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_CountdownIn_1);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimerIn_1);
            step.AddButton(this.buttonVinsert_CounterIn_1);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimerOut_1);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_CounterOut_1);
            step.AddButton(this.buttonVfullscreen_CountdownOut_1);
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
            step.AddButton(this.buttonVfullscreen_UnloadScene);
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

        private void setSelectedPlayer() {
            switch (this.business.SelectedPlayer) {
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.LeftPlayer:
                    this.buttonLeftPlayerSelected.BackColor = Constants.ColorSelected;
                    this.buttonRightPlayerSelected.UseVisualStyleBackColor = true;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.RightPlayer:
                    this.buttonLeftPlayerSelected.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerSelected.BackColor = Constants.ColorSelected;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.NotSelected:
                default:
                    this.buttonLeftPlayerSelected.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerSelected.UseVisualStyleBackColor = true;
                    break;
            }
        }

        private void fill_listBoxCourses(
            string[] courseNameList) {
            this.listBoxTasks.BeginUpdate();
            this.listBoxTasks.Items.Clear();
            if (courseNameList is string[] &&
                courseNameList.Length > 0) this.listBoxTasks.Items.AddRange(courseNameList);
            this.listBoxTasks.EndUpdate();
        }

        #endregion


        #region Events.Incoming

        protected override void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.business_PropertyChanged(sender, e);
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                base.business_PropertyChanged(sender, e);
                if (e.PropertyName == "SelectedPlayer") this.setSelectedPlayer();
                else if (e.PropertyName == "TaskNameList") this.fill_listBoxCourses(this.business.TaskNameList);
                else if (e.PropertyName == "SelectedTask") this.listBoxTasks.Text = this.business.SelectedTask;

            }
        }

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

        private void buttonLeftPlayerSelected_Click(object sender, EventArgs e) { this.business.SelectedPlayer = Content.Gameboard.PlayerSelection.LeftPlayer; }
        private void buttonRightPlayerSelected_Click(object sender, EventArgs e) { this.business.SelectedPlayer = Content.Gameboard.PlayerSelection.RightPlayer; }

        protected virtual void buttonTimerIn_Click(object sender, EventArgs e) { 
            this.business.Vinsert_TimerIn();
            this.business.Vfullscreen_CountdownIn();
        }
        protected virtual void buttonTimerOut_Click(object sender, EventArgs e) {
            this.business.Vinsert_TimerOut();
            this.business.Vfullscreen_CountdownOut();
        }
        protected virtual void buttonTimerStart_Click(object sender, EventArgs e) {
            this.business.Vfullscreen_StartCountdown();
            this.business.Vinsert_StartTimer(); 
        }
        protected virtual void buttonTimerStop_Click(object sender, EventArgs e) {
            this.business.Vfullscreen_StopCountdown();
            this.business.Vinsert_StopTimer(); 
        }
        protected virtual void buttonTimerContinue_Click(object sender, EventArgs e) {
            this.business.Vfullscreen_ContinueCountdown();
            this.business.Vinsert_ContinueTimer(); 
        }
        protected virtual void buttonTimerReset_Click(object sender, EventArgs e) {
            this.business.Vfullscreen_ResetCountdown();
            this.business.Vinsert_ResetTimer();
        }

        private void buttonServerListen_Click(object sender, EventArgs e) { this.business.ToggleServerIsRunning(); }
        private void listBoxTasks_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectCourse(this.listBoxTasks.Text); }
        private void buttonStartTask_Click(object sender, EventArgs e) { this.business.StartTask(); }
        private void buttonStopTask_Click(object sender, EventArgs e) { this.business.StopTask(); }
        private void buttonResetTask_Click(object sender, EventArgs e) { this.business.ResetTask(); }

        private void textBoxSequence_TextChanged(object sender, EventArgs e) { this.business.Sequence = this.textBoxSequence.Text; }
        private void numericUpDownSequenceProgress_ValueChanged(object sender, EventArgs e) { this.business.SequenceProgress = (int)this.numericUpDownSequenceProgress.Value; }
        private void buttonSequenceIn_Click(object sender, EventArgs e) {
            this.business.Vinsert_SequenceIn();
            this.business.Vfullscreen_SequenceIn();
        }
        private void buttonSequenceOut_Click(object sender, EventArgs e) {
            this.business.Vinsert_SequenceOut();
            this.business.Vfullscreen_SequenceOut();
        }

        private void numericUpDownReferenceCounter_VisibleChanged(object sender, EventArgs e) { this.business.ReferenceCounter = (int)this.numericUpDownReferenceCounter.Value; }
        private void checkBoxShowReference_CheckedChanged(object sender, EventArgs e) { this.business.ShowReference = this.checkBoxShowReference.Checked; }
        private void numericUpDownCounter_ValueChanged(object sender, EventArgs e) { this.business.Counter = (int)this.numericUpDownCounter.Value; }

        private void buttonCorrectSound_Click(object sender, EventArgs e) { this.business.Vinsert_PlayJingleGood(); }
        private void buttonFaultSound_Click(object sender, EventArgs e) { this.business.Vinsert_PlayJingleBad(); }
        private void buttonScoreSound_Click(object sender, EventArgs e) { this.business.Vinsert_PlayJingleScore(); }

        private void buttonVinsert_TimerIn_Click(object sender, EventArgs e) { this.business.Vinsert_TimerIn(); }
        private void buttonVinsert_CounterIn_Click(object sender, EventArgs e) { this.business.Vinsert_CounterIn(); }
        private void buttonVinsert_TimerOut_Click(object sender, EventArgs e) { this.business.Vinsert_TimerOut(); }
        private void buttonVinsert_CounterOut_Click(object sender, EventArgs e) { this.business.Vinsert_CounterOut(); }
        private void buttonVinsert_SequenceIn_Click(object sender, EventArgs e) { this.business.Vinsert_SequenceIn(); }
        private void buttonVinsert_SequenceOut_Click(object sender, EventArgs e) { this.business.Vinsert_SequenceOut(); }

        private void buttonVfullscreen_CountdownIn_Click(object sender, EventArgs e) { this.business.Vfullscreen_CountdownIn(); }
        private void buttonVfullscreen_CountdownOut_Click(object sender, EventArgs e) { this.business.Vfullscreen_CountdownOut(); }
        private void buttonVfullscreen_SequenceIn_Click(object sender, EventArgs e) { this.business.Vfullscreen_SequenceIn(); }
        private void buttonVfullscreen_SequenceOut_Click(object sender, EventArgs e) { this.business.Vfullscreen_SequenceOut(); }

        private void buttonGame_NextPlayer_Click(object sender, EventArgs e) { this.business.NextPlayer(); }

        #endregion

    }

}
