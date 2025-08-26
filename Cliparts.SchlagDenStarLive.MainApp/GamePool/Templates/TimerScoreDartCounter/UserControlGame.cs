using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimerScoreDartCounter {

    public partial class UserControlGame : _Base.TimerScore.UserControlGame {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.numericUpDownCounterValue.Minimum = int.MinValue;
            this.numericUpDownCounterValue.Maximum = int.MaxValue;

            this.numericUpDownToBeatValue.Minimum = int.MinValue;
            this.numericUpDownToBeatValue.Maximum = int.MaxValue;


            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.business, "CounterValue");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownCounterValue.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "ToBeatValue");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownToBeatValue.DataBindings.Add(bind);

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

            this.numericUpDownCounterValue.DataBindings.Clear();
            this.numericUpDownToBeatValue.DataBindings.Clear();

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
            step.AddButton(this.buttonVfullscreen_ContinueTimer);
            step.AddButton(this.buttonVinsert_ContinueTimer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimerOut);
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
            step = new stepAction(index, (stepIndex) => stepIndex - 7);
            step.AddButton(this.buttonVinsert_ScoreOut);
            step.AddButton(this.buttonVfullscreen_ResetTimer); 
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

        private void buttonAddCounterHot_Click(object sender, EventArgs e) {
            int result;
            if (Helper.tryParseIndexFromControl(sender as Control, out result)) this.business.AddCounterHot(result);
            else this.business.AddCounterHot(1);
        }
        private void buttonAddCounterHotX2_01_Click(object sender, EventArgs e) {
            int result;
            if (Helper.tryParseIndexFromControl(sender as Control, out result)) this.business.AddCounterHot(result * 2);

        }
        private void buttonAddCounterHotX3_01_Click(object sender, EventArgs e) {
            int result;
            if (Helper.tryParseIndexFromControl(sender as Control, out result)) this.business.AddCounterHot(result * 3);
        }
        private void numericUpDownCounterValue_ValueChanged(object sender, EventArgs e) { this.business.CounterValue = (int)this.numericUpDownCounterValue.Value; }
        private void numericUpDownToBeatValue_ValueChanged(object sender, EventArgs e) { this.business.ToBeatValue = (int)this.numericUpDownToBeatValue.Value; }
        private void buttonNextCounter_Click(object sender, EventArgs e) { this.business.NextCounter(); }

        private void buttonVinsert_CounterIn_Click(object sender, EventArgs e) { this.business.Vinsert_CounterIn(); }
        private void buttonVinsert_CounterOut_Click(object sender, EventArgs e) { this.business.Vinsert_CounterOut(); }

        private void buttonVinsert_ContinueTimer_Click(object sender, EventArgs e) { this.business.Vinsert_ContinueTimer(); }

        private void buttonVfullscreen_ContinueTimer_Click(object sender, EventArgs e) { this.business.Vfullscreen_ContinueTimer(); }

        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }
        #endregion
    }

}
