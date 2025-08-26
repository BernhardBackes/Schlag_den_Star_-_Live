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

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.ShootingTimerForTwoSoloScore {

    public partial class UserControlGame : _Base.Score.UserControlGame {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.numericUpDownLeftPlayerHeats.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerHeats.Maximum = int.MaxValue;

            this.numericUpDownLeftPlayerHits.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerHits.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerHeats.Minimum = int.MinValue;
            this.numericUpDownRightPlayerHeats.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerHits.Minimum = int.MinValue;
            this.numericUpDownRightPlayerHits.Maximum = int.MaxValue;

            this.numericUpDownTimerOffset.Minimum = int.MinValue;
            this.numericUpDownTimerOffset.Maximum = int.MaxValue;

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "LeftPlayerHeats");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerHeats.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerHits");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerHits.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerHeats");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerHeats.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerHits");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerHits.DataBindings.Add(bind);

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

            this.numericUpDownLeftPlayerHeats.DataBindings.Clear();
            this.numericUpDownLeftPlayerHits.DataBindings.Clear();
            this.numericUpDownRightPlayerHeats.DataBindings.Clear();
            this.numericUpDownRightPlayerHits.DataBindings.Clear();

            this.checkBoxSwapTracks.DataBindings.Clear();

            this.textBoxTimelineName.DataBindings.Clear();

            this.labelTimerCurrentTime.DataBindings.Clear();

            this.textBoxIOUnitName.DataBindings.Clear();

            this.labelLeftFinishReached.DataBindings.Clear();
            this.labelRightFinishReached.DataBindings.Clear();

            this.textBoxTimeToBeat.DataBindings.Clear();
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
            step.AddButton(this.buttonVinsert_ShootingIn);
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
            step.AddButton(this.buttonVinsert_ShootingOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ScoreIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_SetScore);
            step.AddButton(this.buttonVstage_SetScore);
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

        private void setFirstFinisher() {
            switch (this.business.FirstFinisher) {
                case Content.Gameboard.PlayerSelection.LeftPlayer:
                    this.buttonLeftPlayerPassFinishLine.BackColor = Constants.ColorSelected;
                    this.buttonRightPlayerPassFinishLine.UseVisualStyleBackColor = true;
                    break;
                case Content.Gameboard.PlayerSelection.RightPlayer:
                    this.buttonLeftPlayerPassFinishLine.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerPassFinishLine.BackColor = Constants.ColorSelected;
                    break;
                case Content.Gameboard.PlayerSelection.NotSelected:
                default:
                    this.buttonLeftPlayerPassFinishLine.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerPassFinishLine.UseVisualStyleBackColor = true;
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

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        protected override void buttonLeftPlayerAddScoreHot_01_Click(object sender, EventArgs e) {
            base.buttonLeftPlayerAddScoreHot_01_Click(sender, e);
            this.business.Vinsert_SetShooting();
        }
        private void buttonLeftPlayerPassFinishLine_Click(object sender, EventArgs e) { this.business.PassFinishLine(Content.Gameboard.PlayerSelection.LeftPlayer); }

        private void numericUpDownLeftPlayerHeats_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerHeats = (int)this.numericUpDownLeftPlayerHeats.Value; }
        private void buttonLeftPlayerAddHeatHot_Click(object sender, EventArgs e) {
            this.business.LeftPlayerHeats++;
            this.business.Vinsert_SetShooting();
            this.business.Vstage_SetScore();
        }
        private void buttonLeftPlayerNextHeat_Click(object sender, EventArgs e) { this.business.LeftPlayerNextHeat(); }
        private void numericUpDownLeftPlayerHits_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerHits = (int)this.numericUpDownLeftPlayerHits.Value; }
        private void buttonLeftPlayerAddHitHot_Click(object sender, EventArgs e) {
            this.business.LeftPlayerHits++;
            this.business.Vinsert_SetShooting();
        }
        private void buttonLeftPlayerHitsIn_Click(object sender, EventArgs e) { this.business.Vinsert_ShootingLeftPlayerHitsIn(); }
        private void buttonLeftPlayerHitsOut_Click(object sender, EventArgs e) { this.business.Vinsert_ShootingLeftPlayerHitsOut(); }
        private void buttonLeftPlayerHitMissed_Click(object sender, EventArgs e) { this.business.Vinsert_ShootingLeftPlayerHitMiss(); }

        protected override void buttonRightPlayerAddScoreHot_01_Click(object sender, EventArgs e) {
            base.buttonRightPlayerAddScoreHot_01_Click(sender, e);
            this.business.Vinsert_SetShooting();
        }
        private void buttonRightPlayerPassFinishLine_Click(object sender, EventArgs e) { this.business.PassFinishLine(Content.Gameboard.PlayerSelection.RightPlayer); }
        private void numericUpDownRightPlayerHeats_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerHeats = (int)this.numericUpDownRightPlayerHeats.Value; }
        private void buttonRightPlayerAddHeatHot_Click(object sender, EventArgs e) {
            this.business.RightPlayerHeats++;
            this.business.Vinsert_SetShooting();
            this.business.Vstage_SetScore();
        }
        private void buttonRightPlayerNextHeat_Click(object sender, EventArgs e) { this.business.RightPlayerNextHeat(); }
        private void numericUpDownRightPlayerHits_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerHits = (int)this.numericUpDownRightPlayerHits.Value; }
        private void buttonRightPlayerAddHitHot_Click(object sender, EventArgs e) {
            this.business.RightPlayerHits++;
            this.business.Vinsert_SetShooting();
        }
        private void buttonRightPlayerHitsIn_Click(object sender, EventArgs e) { this.business.Vinsert_ShootingRightPlayerHitsIn(); }
        private void buttonRightPlayerHitsOut_Click(object sender, EventArgs e) { this.business.Vinsert_ShootingRightPlayerHitsOut(); }
        private void buttonRightPlayerHitMissed_Click(object sender, EventArgs e) { this.business.Vinsert_ShootingRightPlayerHitMiss(); }

        private void checkBoxSwapTracks_CheckedChanged(object sender, EventArgs e) { this.business.SwapTracks = !this.business.SwapTracks; }

        private void buttonTimelineRelease_Click(object sender, EventArgs e) { this.business.ReleaseTimeline(); }
        private void buttonTimelineLock_Click(object sender, EventArgs e) { this.business.LockTimeline(); }

        private void buttonTimerIn_Click(object sender, EventArgs e) { this.business.Vinsert_ShootingIn(); }
        private void buttonTimerOut_Click(object sender, EventArgs e) { this.business.Vinsert_ShootingOut(); }
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

        private void buttonVinsert_ShootingIn_Click(object sender, EventArgs e) { this.business.Vinsert_ShootingIn(); }
        private void buttonVinsert_SetShooting_Click(object sender, EventArgs e) { this.business.Vinsert_SetShooting(); }
        private void buttonVinsert_StartTimer_Click(object sender, EventArgs e) { this.business.Vinsert_StartTimer(); }
        private void buttonVinsert_GetPreciseTime_Click(object sender, EventArgs e) { this.business.Vinsert_GetPreciseTime(); }
        private void buttonVinsert_StopTimer_Click(object sender, EventArgs e) { this.business.Vinsert_StopTimer(); }
        private void buttonVinsert_ResetTimer_Click(object sender, EventArgs e) {
            this.business.Vinsert_SetShowOffsetOut(); 
            this.business.Vinsert_ResetTimer(); 
        }
        private void buttonVinsert_ShootingOut_Click(object sender, EventArgs e) { this.business.Vinsert_ShootingOut(); }

        private void buttonGame_ReleaseIO_Click(object sender, EventArgs e) {
            this.business.ReleaseTimeline();
            this.business.ReleaseBuzzer();
        }
        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }

        #endregion

    }
}
