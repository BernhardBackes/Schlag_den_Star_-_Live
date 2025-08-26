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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimerForTwoStopoverAddition {

    public partial class UserControlGame : _Base.Buzzer.UserControlGame {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.numericUpDownLeftPlayerStopTime.Minimum = decimal.MinValue;
            this.numericUpDownLeftPlayerStopTime.Maximum = decimal.MaxValue;

            this.numericUpDownLeftPlayerAdditionTime.Minimum = decimal.MinValue;
            this.numericUpDownLeftPlayerAdditionTime.Maximum = decimal.MaxValue;

            this.numericUpDownRightPlayerStopTime.Minimum = decimal.MinValue;
            this.numericUpDownRightPlayerStopTime.Maximum = decimal.MaxValue;

            this.numericUpDownRightPlayerAdditionTime.Minimum = decimal.MinValue;
            this.numericUpDownRightPlayerAdditionTime.Maximum = decimal.MaxValue;

            this.numericUpDownRoundCounter.Minimum = int.MinValue;
            this.numericUpDownRoundCounter.Maximum = int.MaxValue;

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "LeftPlayerStopTime");
            bind.Format += (s, e) => { e.Value = Convert.ToDecimal(e.Value); };
            this.numericUpDownLeftPlayerStopTime.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerAdditionTime");
            bind.Format += (s, e) => { e.Value = Convert.ToDecimal(e.Value); };
            this.numericUpDownLeftPlayerAdditionTime.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerStopTime");
            bind.Format += (s, e) => { e.Value = Convert.ToDecimal(e.Value); };
            this.numericUpDownRightPlayerStopTime.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerAdditionTime");
            bind.Format += (s, e) => { e.Value = Convert.ToDecimal(e.Value); };
            this.numericUpDownRightPlayerAdditionTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerCurrentTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToStopwatchTimeText((double)e.Value, false, true); };
            this.labelTimerCurrentTime.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "LeftStopoverReached");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorSelected : Color.Transparent; };
            this.labelLeftStopoverReached.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "RightStopoverReached");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorSelected : Color.Transparent; };
            this.labelRightStopoverReached.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "LeftFinishReached");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorSelected : Color.Transparent; };
            this.labelLeftFinishReached.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "RightFinishReached");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorSelected : Color.Transparent; };
            this.labelRightFinishReached.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RoundCounter");
            bind.Format += (s, e) => { e.Value = Convert.ToInt32(e.Value); };
            this.numericUpDownRoundCounter.DataBindings.Add(bind);

            this.setFirstFinisher();

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

            this.numericUpDownLeftPlayerStopTime.DataBindings.Clear();
            this.numericUpDownLeftPlayerAdditionTime.DataBindings.Clear();

            this.numericUpDownRightPlayerStopTime.DataBindings.Clear();
            this.numericUpDownRightPlayerAdditionTime.DataBindings.Clear();

            this.labelTimerCurrentTime.DataBindings.Clear();

            this.numericUpDownRoundCounter.DataBindings.Clear();

            this.labelLeftStopoverReached.DataBindings.Clear();
            this.labelRightStopoverReached.DataBindings.Clear();

            this.labelLeftFinishReached.DataBindings.Clear();
            this.labelRightFinishReached.DataBindings.Clear();
        }

        protected override void buildStepList() {

            int index = 0;
            stepAction step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_LoadScene);
            //step.AddButton(this.buttonVstage_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVgraphic_Clear);
            step.AddButton(this.buttonVstage_Clear);
            step.AddButton(this.buttonGame_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimerIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_StartTimer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_ReleaseBuzzer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_StopTimer);
            step.AddButton(this.buttonGame_LockBuzzer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_AddTime);
            step.AddButton(this.buttonVinsert_SetTimer);
            step.AddButton(this.buttonVinsert_TimerAdditionIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 4);
            step.AddButton(this.buttonGame_Next);
            step.AddButton(this.buttonVinsert_ResetTimer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimerOut);
            this.stepList.Add(step);

            index++;
            this.winnerStepIndex = index;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVgraphic_Clear);
            step.AddButton(this.buttonVstage_Clear);
            step.AddButton(this.buttonGame_AllLightsBlack);
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

        private void setFirstFinisher() {
            switch (this.business.BuzzeredPlayer) {
                case Content.Gameboard.PlayerSelection.LeftPlayer:
                    this.buttonLeftPlayerFinish.BackColor = Constants.ColorSelected;
                    this.buttonRightPlayerFinish.UseVisualStyleBackColor = true;
                    break;
                case Content.Gameboard.PlayerSelection.RightPlayer:
                    this.buttonLeftPlayerFinish.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerFinish.BackColor = Constants.ColorSelected;
                    break;
                case Content.Gameboard.PlayerSelection.NotSelected:
                default:
                    this.buttonLeftPlayerFinish.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerFinish.UseVisualStyleBackColor = true;
                    break;
            }
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

        private void bind_textBoxIOUnitName_BackColor(object sender, ConvertEventArgs e) {
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

        #endregion


        #region Events.Incoming

        protected override void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.business_PropertyChanged(sender, e);
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "FirstFinisher") this.setFirstFinisher();
            }
        }

        #endregion

        #region Events.Controls

        private void numericUpDownLeftPlayerStopTime_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerStopTime = (double)this.numericUpDownLeftPlayerStopTime.Value; }
        private void numericUpDownLeftPlayerAdditionTime_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerAdditionTime = (double)this.numericUpDownLeftPlayerAdditionTime.Value; }

        private void numericUpDownRightPlayerStopTime_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerStopTime = (double)this.numericUpDownRightPlayerStopTime.Value; }
        private void numericUpDownRightPlayerAdditionTime_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerAdditionTime = (double)this.numericUpDownRightPlayerAdditionTime.Value; }

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void buttonLeftPlayerStopover_Click(object sender, EventArgs e) { this.business.SetStopover(Content.Gameboard.PlayerSelection.LeftPlayer); }
        private void buttonLeftPlayerFinish_Click(object sender, EventArgs e) { this.business.SetFinish(Content.Gameboard.PlayerSelection.LeftPlayer); }
        private void labelLeftStopoverReached_Click(object sender, EventArgs e) { this.business.LeftStopoverReached = !this.business.LeftStopoverReached; }

        private void buttonRightPlayerStopover_Click(object sender, EventArgs e) { this.business.SetStopover(Content.Gameboard.PlayerSelection.RightPlayer); }
        private void buttonRightPlayerFinish_Click(object sender, EventArgs e) { this.business.SetFinish(Content.Gameboard.PlayerSelection.RightPlayer); }
        private void labelRightStopoverReached_Click(object sender, EventArgs e) { this.business.RightStopoverReached = !this.business.RightStopoverReached; }

        private void numericUpDownRoundCounter_ValueChanged(object sender, EventArgs e) { this.business.RoundCounter = (int)this.numericUpDownRoundCounter.Value; }

        private void buttonTimerIn_Click(object sender, EventArgs e) { this.business.Vinsert_TimerIn(); }
        private void buttonTimerOut_Click(object sender, EventArgs e) { this.business.Vinsert_TimerOut(); }
        private void buttonTimerStart_Click(object sender, EventArgs e) { this.business.Vinsert_StartTimer(); }
        private void buttonTimerStop_Click(object sender, EventArgs e) { this.business.Vinsert_StopTimer(); }
        private void buttonTimerContinue_Click(object sender, EventArgs e) { this.business.Vinsert_ContinueTimer(); }
        private void buttonTimerReset_Click(object sender, EventArgs e) { this.business.Vinsert_ResetTimer(); }
        private void buttonTimerAdditionIn_Click(object sender, EventArgs e) { this.business.Vinsert_TimerAdditionIn(); }
        private void buttonTimerAdditionOut_Click(object sender, EventArgs e) { this.business.Vinsert_TimerAdditionOut(); }

        private void buttonBuzzerReleaseLeft_Click(object sender, EventArgs e) { this.business.ReleaseBuzzer(Content.Gameboard.PlayerSelection.LeftPlayer); }
        private void buttonBuzzerReleaseRight_Click(object sender, EventArgs e) { this.business.ReleaseBuzzer(Content.Gameboard.PlayerSelection.RightPlayer); }
        private void buttonStopoverLeft_Click(object sender, EventArgs e) { this.business.PassStopover(Content.Gameboard.PlayerSelection.LeftPlayer); }
        private void buttonStopoverRight_Click(object sender, EventArgs e) { this.business.PassStopover(Content.Gameboard.PlayerSelection.RightPlayer); }
        private void buttonBuzzerLeft_Click(object sender, EventArgs e) { this.business.PassFinishLine(Content.Gameboard.PlayerSelection.LeftPlayer); }
        private void buttonBuzzerRight_Click(object sender, EventArgs e) { this.business.PassFinishLine(Content.Gameboard.PlayerSelection.RightPlayer); }

        private void buttonVinsert_TimerIn_Click(object sender, EventArgs e) { this.business.Vinsert_TimerIn(); }
        private void buttonVinsert_StartTimer_Click(object sender, EventArgs e) { this.business.Vinsert_StartTimer(); }
        private void buttonVinsert_StopTimer_Click(object sender, EventArgs e) { this.business.Vinsert_StopTimer(); }
        private void buttonVinsert_SetTimer_Click(object sender, EventArgs e) { this.business.Vinsert_SetTimer(); }
        private void buttonVinsert_TimerAdditionIn_Click(object sender, EventArgs e) { this.business.Vinsert_TimerAdditionIn(); }
        private void buttonVinsert_ResetTimer_Click(object sender, EventArgs e) {
            this.business.Vinsert_SetTimer();
            this.business.Vinsert_ResetTimer(); 
        }
        private void buttonVinsert_TimerOut_Click(object sender, EventArgs e) { this.business.Vinsert_TimerOut(); }

        private void buttonGame_LockBuzzer_Click(object sender, EventArgs e) { this.business.LockBuzzer(); }
        private void buttonGame_AddTime_Click(object sender, EventArgs e) { this.business.AddTime(); }
        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }

        #endregion

    }
}
