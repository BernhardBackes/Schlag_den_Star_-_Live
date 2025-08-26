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

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimerForTwoSoloScoreCounter {

    public partial class UserControlGame : _Base.Score.UserControlGame {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.numericUpDownTimerOffset.Minimum = int.MinValue;
            this.numericUpDownTimerOffset.Maximum = int.MaxValue;

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            this.business = business;

            for (int i = 0; i < Business.TaskCounterPenaltyCountMax; i++) {
                UserControlGamePoolTemplatesTimerForTwoSoloScoreCounTCSD userControlGamePoolTemplatesTaskCounterSingleDot;
                Label labelCounter;
                int left;
                if (i == 0) {
                    userControlGamePoolTemplatesTaskCounterSingleDot = this.userControlGamePoolTemplatesTimerForTwoSoloScoreCounterTCSD_00;
                }
                else {
                    left = this.userControlGamePoolTemplatesTimerForTwoSoloScoreCounterTCSD_00.Left + this.userControlGamePoolTemplatesTimerForTwoSoloScoreCounterTCSD_00.Width * i;
                    userControlGamePoolTemplatesTaskCounterSingleDot = new UserControlGamePoolTemplatesTimerForTwoSoloScoreCounTCSD();
                    userControlGamePoolTemplatesTaskCounterSingleDot.Left = left;
                    userControlGamePoolTemplatesTaskCounterSingleDot.Name = "userControlGamePoolTemplatesTimerForTwoSoloScoreCounterTCSD_" + i.ToString("00");
                    userControlGamePoolTemplatesTaskCounterSingleDot.Top = this.userControlGamePoolTemplatesTimerForTwoSoloScoreCounterTCSD_00.Top;
                    this.Controls.Add(userControlGamePoolTemplatesTaskCounterSingleDot);
                    labelCounter = new Label();
                    labelCounter.BackColor = this.BackColor;
                    labelCounter.Font = this.labelDotCounter_00.Font;
                    labelCounter.Left = left;
                    labelCounter.Name = "labelDotCounter_" + i.ToString("00");
                    labelCounter.Size = this.labelDotCounter_00.Size;
                    labelCounter.TextAlign = this.labelDotCounter_00.TextAlign;
                    labelCounter.Text = ((int)i + 1).ToString();
                    labelCounter.Top = this.labelDotCounter_00.Top;
                    this.Controls.Add(labelCounter);
                }
                userControlGamePoolTemplatesTaskCounterSingleDot.Pose(this.business.TaskCounterPenaltyDots[i]);
            }

            Binding bind;

            bind = new Binding("BackColor", this.business, "SwapTracks");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorMissing : SystemColors.Control; };
            this.checkBoxSwapTracks.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimelineName");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.textBoxTimelineName.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "TimelineStatus");
            bind.Format += this.bind_textBoxTimelineName_BackColor;
            this.textBoxTimelineName.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerCurrentTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToStopwatchTimeText((double)e.Value, false, true); };
            this.labelTimerCurrentTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "IOUnitName");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxIOUnitName.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "IOUnitStatus");
            bind.Format += this.bind_textBoxIOUnitName_BackColor;
            this.textBoxIOUnitName.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "LeftFinishReached");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorSelected : Color.Transparent; };
            this.labelLeftFinishReached.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "RightFinishReached");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorSelected : Color.Transparent; };
            this.labelRightFinishReached.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimeToBeat");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxTimeToBeat.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TaskCounter");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTaskCounter.DataBindings.Add(bind);

            this.adjustUserControlDots();

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

            UserControlGamePoolTemplatesTimerForTwoSoloScoreCounTCSD control;
            string key;
            for (int i = 0; i < Business.TaskCounterPenaltyCountMax; i++) {
                key = "userControlGamePoolTemplatesTimerForTwoSoloScoreCounterTCSD_" + i.ToString("00");
                control = this.Controls[key] as UserControlGamePoolTemplatesTimerForTwoSoloScoreCounTCSD;
                if (control is UserControlGamePoolTemplatesTimerForTwoSoloScoreCounTCSD) {
                    control.Dispose();
                }
            }

            this.checkBoxSwapTracks.DataBindings.Clear();

            this.textBoxTimelineName.DataBindings.Clear();

            this.labelTimerCurrentTime.DataBindings.Clear();

            this.textBoxIOUnitName.DataBindings.Clear();

            this.labelLeftFinishReached.DataBindings.Clear();
            this.labelRightFinishReached.DataBindings.Clear();

            this.textBoxTimeToBeat.DataBindings.Clear();
        }

        private void adjustUserControlDots() {
            Control control;
            string key;
            for (int i = 0; i < Business.TaskCounterPenaltyCountMax; i++) {
                key = "userControlGamePoolTemplatesTimerForTwoSoloScoreCounterTCSD_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.Visible = i < this.business.TaskCounterSize;
                key = "labelDotCounter_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.Visible = i < this.business.TaskCounterSize;
            }
        }


        protected override void buildStepList() {

            int index = 0;
            stepAction step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_LoadScene);
            step.AddButton(this.buttonVstage_Init);
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
            step.AddButton(this.buttonGame_ReleaseIO);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_GetPreciseTime);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_StopTimer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimerOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ScoreIn);
            step.AddButton(this.buttonVinsert_TaskCounterIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_Resolve);
            step.AddButton(this.buttonVinsert_SetScore);
            step.AddButton(this.buttonVinsert_SetTaskCounter);
            step.AddButton(this.buttonVstage_SetScore);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 8);
            step.AddButton(this.buttonVinsert_ScoreOut);
            step.AddButton(this.buttonVinsert_TaskCounterOut);
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

        private void setFirstFinisher() {
            switch (this.business.FirstFinisher) {
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
                else if (e.PropertyName == "TaskCounterSize") this.adjustUserControlDots();
            }
        }

        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void UserControlGame_BackColorChanged(object sender, EventArgs e) {
            Control control;
            string key;
            for (int i = 0; i < Business.TaskCounterPenaltyCountMax; i++) {
                key = "userControlGamePoolTemplatesTimerForTwoSoloScoreCounterTCSD_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.BackColor = this.BackColor;
                key = "labelDotCounter_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.BackColor = this.BackColor;
            }
        }

        protected override void buttonLeftPlayerAddScoreHot_01_Click(object sender, EventArgs e) {
            base.buttonLeftPlayerAddScoreHot_01_Click(sender, e);
            this.business.Vinsert_SetTimer();
        }
        private void buttonLeftPlayerFinish_Click(object sender, EventArgs e) { this.business.PassFinishLine(Content.Gameboard.PlayerSelection.LeftPlayer); }
        protected override void buttonRightPlayerAddScoreHot_01_Click(object sender, EventArgs e) {
            base.buttonRightPlayerAddScoreHot_01_Click(sender, e);
            this.business.Vinsert_SetTimer();
        }
        private void buttonRightPlayerFinish_Click(object sender, EventArgs e) { this.business.PassFinishLine(Content.Gameboard.PlayerSelection.RightPlayer); }

        private void checkBoxSwapTracks_CheckedChanged(object sender, EventArgs e) { this.business.SwapTracks = !this.business.SwapTracks; }

        private void buttonTimelineRelease_Click(object sender, EventArgs e) { this.business.ReleaseTimeline(); }
        private void buttonTimelineLock_Click(object sender, EventArgs e) { this.business.LockTimeline(); }

        private void buttonTimerIn_Click(object sender, EventArgs e) { this.business.Vinsert_TimerIn(); }
        private void buttonTimerOut_Click(object sender, EventArgs e) { this.business.Vinsert_TimerOut(); }
        private void buttonTimerStart_Click(object sender, EventArgs e) { this.business.Vinsert_StartTimer(); }
        private void buttonTimerStop_Click(object sender, EventArgs e) { this.business.Vinsert_StopTimer(); }
        private void buttonTimerContinue_Click(object sender, EventArgs e) { this.business.Vinsert_ContinueTimer(); }
        private void buttonTimerReset_Click(object sender, EventArgs e) { this.business.Vinsert_ResetTimer(); }
        private void buttonTimerShowOffsetTop_Click(object sender, EventArgs e) { this.business.Vinsert_ShowOffsetTimeTop(Convert.ToSingle(this.numericUpDownTimerOffset.Value)); }
        private void buttonTimerShowOffsetBottom_Click(object sender, EventArgs e) { this.business.Vinsert_ShowOffsetTimeBottom(Convert.ToSingle(this.numericUpDownTimerOffset.Value)); }
        private void buttonTimerResetOffset_Click(object sender, EventArgs e) { this.business.Vinsert_SetShowOffsetOut(); }

        private void buttonReleaseBuzzer_Click(object sender, EventArgs e) { this.business.ReleaseBuzzer(); }
        private void buttonLockBuzzer_Click(object sender, EventArgs e) { this.business.LockBuzzer(); }
        private void buttonBuzzerReleaseLeft_Click(object sender, EventArgs e) { this.business.ReleaseBuzzer(Content.Gameboard.PlayerSelection.LeftPlayer); }
        private void buttonBuzzerReleaseRight_Click(object sender, EventArgs e) { this.business.ReleaseBuzzer(Content.Gameboard.PlayerSelection.RightPlayer); }
        private void buttonBuzzerLeft_Click(object sender, EventArgs e) { this.business.PassFinishLine(Content.Gameboard.PlayerSelection.LeftPlayer); }
        private void buttonBuzzerRight_Click(object sender, EventArgs e) { this.business.PassFinishLine(Content.Gameboard.PlayerSelection.RightPlayer); }

        private void buttonVinsert_TimerIn_Click(object sender, EventArgs e) { this.business.Vinsert_TimerIn(); }
        private void buttonVinsert_StartTimer_Click(object sender, EventArgs e) { this.business.Vinsert_StartTimer(); }
        private void buttonVinsert_GetPreciseTime_Click(object sender, EventArgs e) { this.business.Vinsert_GetPreciseTime(); }
        private void buttonVinsert_StopTimer_Click(object sender, EventArgs e) { this.business.Vinsert_StopTimer(); }
        private void buttonVinsert_ResetTimer_Click(object sender, EventArgs e) {
            this.business.Vinsert_SetShowOffsetOut(); 
            this.business.Vinsert_ResetTimer(); 
        }
        private void buttonVinsert_TimerOut_Click(object sender, EventArgs e) { this.business.Vinsert_TimerOut(); }

        private void buttonGame_ReleaseIO_Click(object sender, EventArgs e) {
            this.business.ReleaseTimeline();
            this.business.ReleaseBuzzer();
        }
        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }

        private void buttonVinsert_TaskCounterIn_Click(object sender, EventArgs e) { this.business.Vinsert_TaskCounterIn(); }
        private void buttonVinsert_SetTaskCounter_Click(object sender, EventArgs e) { this.business.Vinsert_SetTaskCounter(); }
        private void buttonVinsert_TaskCounterOut_Click(object sender, EventArgs e) { this.business.Vinsert_TaskCounterOut(); }

        private void buttonGame_Resolve_Click(object sender, EventArgs e) { this.business.Resolve(); }

        #endregion

    }
}
