using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.DicesCalculation {

    public partial class UserControlGame : _Base.Score.UserControlGame {

        #region Properties

        private Business business;

        private VRemote4.HandlerSi.Business localVentuzHandler;
        private VRemote4.HandlerSi.BusinessForm localVentuzHandlerForm;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Business localVentuzHandler) {
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

            bind = new Binding("Text", this.business, "LeftPlayerInput");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.textBoxLeftPlayerInput.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerInput");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.textBoxRightPlayerInput.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "Solution");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.textBoxSolution.DataBindings.Add(bind);

            this.userControlDicesCalculation_Dice1.Pose(business, 1);
            this.userControlDicesCalculation_Dice2.Pose(business, 2);
            this.userControlDicesCalculation_Dice3.Pose(business, 3);

            this.localVentuzHandler = localVentuzHandler;

            VRemote4.HandlerSi.Client.Business contestantLeftTablet;
            if (this.localVentuzHandler.TryGetClient("Left Tablet", out contestantLeftTablet)) this.userControlClientStateLeftTablet.Pose(contestantLeftTablet);
            this.userControlClientStateLeftTablet.BackColor = this.BackColor;

            VRemote4.HandlerSi.Client.Business contestantRightTablet;
            if (this.localVentuzHandler.TryGetClient("Right Tablet", out contestantRightTablet)) this.userControlClientStateRightTablet.Pose(contestantRightTablet);
            this.userControlClientStateRightTablet.BackColor = this.BackColor;

            this.labelGameClass.Text = this.business.ClassInfo;

            this.setFirstResolved();
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

            this.textBoxLeftPlayerInput.DataBindings.Clear();
            this.textBoxRightPlayerInput.DataBindings.Clear();
            this.textBoxSolution.DataBindings.Clear();

            this.userControlDicesCalculation_Dice1.Dispose();
            this.userControlDicesCalculation_Dice2.Dispose();
            this.userControlDicesCalculation_Dice3.Dispose();

            this.userControlClientStateLeftTablet.Dispose();
            this.userControlClientStateRightTablet.Dispose();
        }

        protected override void buildStepList() {

            int index = 0;
            stepAction step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_LoadScene);
            step.AddButton(this.buttonVlefttablet_StartClient);
            step.AddButton(this.buttonVrighttablet_StartClient);
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
            step.AddButton(this.buttonVlefttablet_Init);
            step.AddButton(this.buttonVrighttablet_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_SortDices);
            step.AddButton(this.buttonVinsert_DicesIn);
            step.AddButton(this.buttonVlefttablet_Start);
            step.AddButton(this.buttonVrighttablet_Start);
            step.AddButton(this.buttonGame_CalcSolution);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_StartTimer);
            step.AddButton(this.buttonVinsert_StartTimer);
            step.AddButton(this.buttonVlefttablet_StartCountdown);
            step.AddButton(this.buttonVrighttablet_StartCountdown);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_StopTimer);
            step.AddButton(this.buttonVinsert_StopTimer);
            step.AddButton(this.buttonVlefttablet_Stop);
            step.AddButton(this.buttonVrighttablet_Stop);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_InputIn);
            step.AddButton(this.buttonVfullscreen_ResetTimer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_DicesOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ShowBorder);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ContentOut);
            step.AddButton(this.buttonVlefttablet_Reset);
            step.AddButton(this.buttonVrighttablet_Reset);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ScoreIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_Resolve);
            step.AddButton(this.buttonVinsert_SetScore);
            step.AddButton(this.buttonVstage_SetScore);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 9);
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
            step.AddButton(this.buttonVlefttablet_ShutDown);
            step.AddButton(this.buttonVrighttablet_ShutDown);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_UpdateGameboard);
            this.stepList.Add(step);

            if (index < this.stepsMaxIndex) this.on_Error("buildStepList", "stepList to short");
            if (index > this.stepsMaxIndex) this.on_Error("buildStepList", "stepList to long");
        }

        private void setFirstResolved() {
            switch (this.business.FirstResolved) {
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.LeftPlayer:
                    this.buttonLeftPlayerFirst.BackColor = Constants.ColorWinner;
                    this.buttonRightPlayerFirst.UseVisualStyleBackColor = true;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.RightPlayer:
                    this.buttonLeftPlayerFirst.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerFirst.BackColor = Constants.ColorWinner;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.NotSelected:
                default:
                    this.buttonLeftPlayerFirst.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerFirst.UseVisualStyleBackColor = true;
                    break;
            }
        }

        public override void ParseKey(
            Keys keycode) {
            base.ParseKey(keycode);
            if (this.keyControl) {
                switch (keycode) {
                    case Keys.NumPad1:
                        this.business.SetNextDice(1);
                        break;
                    case Keys.NumPad2:
                        this.business.SetNextDice(2);
                        break;
                    case Keys.NumPad3:
                        this.business.SetNextDice(3);
                        break;
                    case Keys.NumPad4:
                        this.business.SetNextDice(4);
                        break;
                    case Keys.NumPad5:
                        this.business.SetNextDice(5);
                        break;
                    case Keys.NumPad6:
                        this.business.SetNextDice(6);
                        break;
                }
            }
        }

        #endregion


        #region Events.Incoming

        protected override void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.business_PropertyChanged(sender, e);
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "FirstResolved") this.setFirstResolved();
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

        private void localVentuzHandlerForm_Disposed(object sender, EventArgs e) {
            if (this.localVentuzHandlerForm is VRemote4.HandlerSi.BusinessForm) {
                this.localVentuzHandlerForm.Disposed -= this.localVentuzHandlerForm_Disposed;
                this.localVentuzHandlerForm = null;
            }
        }

        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void UserControlGame_BackColorChanged(object sender, EventArgs e) {
            this.userControlClientStateLeftTablet.BackColor = this.BackColor;
            this.userControlClientStateRightTablet.BackColor = this.BackColor;
        }

        private void buttonLeftPlayerFirst_Click(object sender, EventArgs e) { this.business.FirstResolved = Content.Gameboard.PlayerSelection.LeftPlayer; }
        private void buttonLeftPlayerInputIn_Click(object sender, EventArgs e) { this.business.Vinsert_LeftInputIn(); }
        private void buttonLeftPlayerInputOut_Click(object sender, EventArgs e) { this.business.Vinsert_LeftInputOut(); }

        private void buttonRightPlayerFirst_Click(object sender, EventArgs e) { this.business.FirstResolved = Content.Gameboard.PlayerSelection.RightPlayer; }
        private void buttonRightPlayerInputIn_Click(object sender, EventArgs e) { this.business.Vinsert_RightInputIn(); }
        private void buttonRightPlayerInputOut_Click(object sender, EventArgs e) { this.business.Vinsert_RightInputOut(); }

        private void buttonVinsert_FirstInputIn_Click(object sender, EventArgs e) { this.business.Vinsert_FirstInputIn(); }
        private void buttonVinsert_FirstInputOut_Click(object sender, EventArgs e) { this.business.Vinsert_FirstInputOut(); }

        private void buttonSolutionIn_Click(object sender, EventArgs e) { this.business.Vinsert_SolutionIn(); }
        private void buttonSolutionOut_Click(object sender, EventArgs e) { this.business.Vinsert_SolutionOut(); }

        private void buttonTimerIn_Click(object sender, EventArgs e) { this.business.Vinsert_TimerIn(); }
        private void buttonTimerOut_Click(object sender, EventArgs e) { this.business.Vinsert_TimerOut(); }
        private void buttonTimerStart_Click(object sender, EventArgs e) {
            this.business.Vinsert_StartTimer();
            this.business.Vfullscreen_StartTimer();
        }
        private void buttonTimerStop_Click(object sender, EventArgs e) {
            this.business.Vinsert_StopTimer();
            this.business.Vfullscreen_StopTimer();
        }
        private void buttonTimerContinue_Click(object sender, EventArgs e) {
            this.business.Vinsert_ContinueTimer();
            this.business.Vfullscreen_ContinueTimer();
        }
        private void buttonTimerReset_Click(object sender, EventArgs e) {
            this.business.Vinsert_ResetTimer();
            this.business.Vfullscreen_ResetTimer();
        }
        private void checkBoxTimerRunExtraTime_CheckedChanged(object sender, EventArgs e) { this.business.RunExtraTime = this.checkBoxTimerRunExtraTime.Checked; }

        private void buttonSetLeftPlayerInput_Click(object sender, EventArgs e) { this.business.LeftPlayerInput = this.textBoxLeftPlayerInput.Text; }
        private void buttonSetRightPlayerInput_Click(object sender, EventArgs e) { this.business.RightPlayerInput = this.textBoxRightPlayerInput.Text; }

        private void buttonShowVentuzHandler_Click(object sender, EventArgs e) {
            if (this.localVentuzHandlerForm == null) {
                this.localVentuzHandlerForm = new VRemote4.HandlerSi.BusinessForm(this.localVentuzHandler, this.BackColor);
                this.localVentuzHandlerForm.Disposed += this.localVentuzHandlerForm_Disposed;
            }
            this.localVentuzHandlerForm.Show();
            this.localVentuzHandlerForm.BringToFront();
        }

        private void buttonVinsert_DicesIn_Click(object sender, EventArgs e) { this.business.Vinsert_DicesIn(); }
        private void buttonVinsert_StartTimer_Click(object sender, EventArgs e) {
            this.business.Vinsert_SetTimer();
            this.business.Vinsert_TimerIn();
            this.business.Vinsert_StartTimer();
        }
        private void buttonVinsert_StopTimer_Click(object sender, EventArgs e) {
            this.business.Vinsert_StopTimer();
            this.business.Vinsert_TimerOut();
        }
        private void buttonVinsert_InputIn_Click(object sender, EventArgs e)  {
            this.business.Vinsert_LeftInputIn();
            this.business.Vinsert_RightInputIn();
        }
        private void buttonVinsert_ShowBorder_Click(object sender, EventArgs e) { this.business.Vinsert_ShowBorder(); }
        private void buttonVinsert_DicesOut_Click(object sender, EventArgs e) { this.business.Vinsert_DicesOut(); }
        private void buttonVinsert_ContentOut_Click(object sender, EventArgs e) { this.business.Vinsert_ContentOut(); }

        private void buttonVfullscreen_StartTimer_Click(object sender, EventArgs e) {
            this.business.Vfullscreen_SetTimer();
            this.business.Vfullscreen_StartTimer();
        }
        private void buttonVfullscreen_StopTimer_Click(object sender, EventArgs e) { this.business.Vfullscreen_StopTimer(); }
        private void buttonVfullscreen_ResetTimer_Click(object sender, EventArgs e) { this.business.Vfullscreen_ResetTimer(); }

        private void buttonVlefttablet_StartClient_Click(object sender, EventArgs e) { this.business.Vlefttablet_StartClient(); }
        private void buttonVlefttablet_Init_Click(object sender, EventArgs e) { this.business.Vlefttablet_Init(); }
        private void buttonVlefttablet_Start_Click(object sender, EventArgs e) { this.business.Vlefttablet_Start(); }
        private void buttonVlefttablet_StartCountdown_Click(object sender, EventArgs e) { this.business.Vlefttablet_StartCountdown(); }
        private void buttonVlefttablet_Stop_Click(object sender, EventArgs e) { this.business.Vlefttablet_Stop(); }
        private void buttonVlefttablet_Reset_Click(object sender, EventArgs e) { this.business.Vlefttablet_Reset(); }
        private void buttonVlefttablet_ShutDown_Click(object sender, EventArgs e) { this.business.Vlefttablet_ShutDown(); }

        private void buttonVrighttablet_StartClient_Click(object sender, EventArgs e) { this.business.Vrighttablet_StartClient(); }
        private void buttonVrighttablet_Init_Click(object sender, EventArgs e) { this.business.Vrighttablet_Init(); }
        private void buttonVrighttablet_Start_Click(object sender, EventArgs e) { this.business.Vrighttablet_Start(); }
        private void buttonVrighttablet_StartCountdown_Click(object sender, EventArgs e) { this.business.Vrighttablet_StartCountdown(); }
        private void buttonVrighttablet_Stop_Click(object sender, EventArgs e) { this.business.Vrighttablet_Stop(); }
        private void buttonVrighttablet_Reset_Click(object sender, EventArgs e) { this.business.Vrighttablet_Reset(); }
        private void buttonVrighttablet_ShutDown_Click(object sender, EventArgs e) { this.business.Vrighttablet_ShutDown(); }

        private void buttonGame_SortDices_Click(object sender, EventArgs e) { this.business.SortDices(); }
        private void buttonGame_CalcSolution_Click(object sender, EventArgs e) { this.business.CalculateSolution(); }
        private void buttonGame_Resolve_Click(object sender, EventArgs e) { this.business.Resolve(); }
        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }

        #endregion


    }

}
