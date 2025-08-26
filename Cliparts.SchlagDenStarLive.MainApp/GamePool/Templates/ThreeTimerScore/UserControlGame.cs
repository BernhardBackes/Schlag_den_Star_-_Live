using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.ThreeTimerScore {

    public partial class UserControlGame : _Base.TimerScore.UserControlGame {

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

            Binding bind;

            bind = new Binding("Text", this.business, "LeftTimerCurrentTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelLeftTimerCurrentTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "LeftTimerStartTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelLeftTimerStartTimeText.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "LeftTimerStopTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelLeftTimerStopTimeText.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightTimerCurrentTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelRightTimerCurrentTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightTimerStartTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelRightTimerStartTimeText.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightTimerStopTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelRightTimerStopTimeText.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "LeftPlayerName");
            this.textBoxLeftPlayerName_1.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerName");
            this.textBoxRightPlayerName_1.DataBindings.Add(bind);

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

            this.labelLeftTimerCurrentTime.DataBindings.Clear();
            this.labelLeftTimerStartTimeText.DataBindings.Clear();
            this.labelLeftTimerStopTimeText.DataBindings.Clear();
            this.labelRightTimerCurrentTime.DataBindings.Clear();
            this.labelRightTimerStartTimeText.DataBindings.Clear();
            this.labelRightTimerStopTimeText.DataBindings.Clear();

            this.textBoxLeftPlayerName_1.DataBindings.Clear();
            this.textBoxRightPlayerName_1.DataBindings.Clear();
        }

        protected override void buildStepList() {

            int index = 0;
            stepAction step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_LoadScene);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, this.showFullscreenTimer);
            step.AddButton(this.buttonVgraphic_Clear);
            step.AddButton(this.buttonVstage_Clear);
            step.AddButton(this.buttonGame_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ResetLeftRightTimer);
            step.AddButton(this.buttonVfullscreen_ShowTimer);
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
            step.AddButton(this.buttonVinsert_ScoreIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_SetScore);
            step.AddButton(this.buttonVstage_SetScore);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 6);
            step.AddButton(this.buttonVinsert_ScoreOut);
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
        protected int showFullscreenTimer(
            int stepIndex) {
            if (this.business.ShowFullscreenTimer) return stepIndex + 1;
            else return stepIndex + 2; 
        }

        #endregion


        #region Events.Incoming
        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        protected virtual void buttonLeftTimerIn_Click(object sender, EventArgs e) { this.business.Vinsert_LeftTimerIn(); }
        protected virtual void buttonLeftTimerOut_Click(object sender, EventArgs e) { this.business.Vinsert_LeftTimerOut(); }
        protected virtual void buttonLeftTimerStart_Click(object sender, EventArgs e) { this.business.Vinsert_StartLeftTimer(); }
        protected virtual void buttonLeftTimerStop_Click(object sender, EventArgs e) { this.business.Vinsert_StopLeftTimer(); }
        protected virtual void buttonLeftTimerContinue_Click(object sender, EventArgs e) { this.business.Vinsert_ContinueLeftTimer(); }
        protected virtual void buttonLeftTimerReset_Click(object sender, EventArgs e) { this.business.Vinsert_ResetLeftTimer(); }

        protected virtual void buttonRightTimerIn_Click(object sender, EventArgs e) { this.business.Vinsert_RightTimerIn(); }
        protected virtual void buttonRightTimerOut_Click(object sender, EventArgs e) { this.business.Vinsert_RightTimerOut(); }
        protected virtual void buttonRightTimerStart_Click(object sender, EventArgs e) { this.business.Vinsert_StartRightTimer(); }
        protected virtual void buttonRightTimerStop_Click(object sender, EventArgs e) { this.business.Vinsert_StopRightTimer(); }
        protected virtual void buttonRightTimerContinue_Click(object sender, EventArgs e) { this.business.Vinsert_ContinueRightTimer(); }
        protected virtual void buttonRightTimerReset_Click(object sender, EventArgs e) { this.business.Vinsert_ResetRightTimer(); }

        private void buttonLeftRightTimerIn_Click(object sender, EventArgs e) {
            this.business.Vinsert_LeftTimerIn();
            this.business.Vinsert_RightTimerIn();
        }
        private void buttonLeftRightTimerOut_Click(object sender, EventArgs e) {
            this.business.Vinsert_LeftTimerOut();
            this.business.Vinsert_RightTimerOut();
        }

        private void buttonVinsert_ResetLeftRightTimer_Click(object sender, EventArgs e) {
            this.business.Vinsert_ResetLeftTimer();
            this.business.Vinsert_ResetRightTimer();
        }

        #endregion

    }

}
