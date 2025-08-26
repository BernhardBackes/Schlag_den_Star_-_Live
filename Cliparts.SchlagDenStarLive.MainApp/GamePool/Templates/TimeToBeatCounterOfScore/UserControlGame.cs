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

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimeToBeatCounterOfScore
{

    public partial class UserControlGame : _Base.Score.UserControlGame {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.numericUpDownTimeToBeatTime.Minimum = int.MinValue;
            this.numericUpDownTimeToBeatTime.Maximum = int.MaxValue;

            this.numericUpDownTimeToBeatOffset.Minimum = int.MinValue;
            this.numericUpDownTimeToBeatOffset.Maximum = int.MaxValue;

            this.numericUpDownCounterOfTarget.Minimum = int.MinValue;
            this.numericUpDownCounterOfTarget.Maximum = int.MaxValue;

            this.numericUpDownCounterOfValue.Minimum = int.MinValue;
            this.numericUpDownCounterOfValue.Maximum = int.MaxValue;

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            this.business = business;

            Binding bind;

            bind = new Binding("Text", this.business, "TimeToBeatCurrentTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToStopwatchTimeText((double)e.Value, false, true); };
            this.labelTimeToBeatCurrentTime.DataBindings.Add(bind);
            
            bind = new Binding("Text", this.business, "TimeToBeat");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxTimeToBeat.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "Target");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownCounterOfTarget.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "Counter");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownCounterOfValue.DataBindings.Add(bind);

            this.setSelectedPlayer();

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

            this.labelTimeToBeatCurrentTime.DataBindings.Clear();
            this.textBoxTimeToBeat.DataBindings.Clear();
            this.numericUpDownCounterOfTarget.DataBindings.Clear();
            this.numericUpDownCounterOfValue.DataBindings.Clear();
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
            step.AddButton(this.buttonVfullscreen_ShowFreetext);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimeToBeatIn);
            step.AddButton(this.buttonVinsert_CounterIn);
            step.AddButton(this.buttonVfullscreen_SetCounter);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_StartTimeToBeat);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_StopTimeToBeat);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_NextPlayer);
            step.AddButton(this.buttonVinsert_ResetTimeToBeat);
            step.AddButton(this.buttonVinsert_SetCounter);
            step.AddButton(this.buttonVfullscreen_SetCounter_1);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ShowTimeToBeatTime);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_StartTimeToBeat);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_StopTimeToBeat);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimeToBeatOut);
            step.AddButton(this.buttonVinsert_CounterOut);
            step.AddButton(this.buttonVfullscreen_SetCounter_2);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ScoreIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_SetScore);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 10);
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

        #endregion


        #region Events.Incoming

        protected override void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.business_PropertyChanged(sender, e);
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "SelectedPlayer") this.setSelectedPlayer();
            }
        }

        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void buttonLeftPlayerSelected_Click(object sender, EventArgs e) { this.business.SelectedPlayer = Content.Gameboard.PlayerSelection.LeftPlayer; }
        private void buttonRightPlayerSelected_Click(object sender, EventArgs e) { this.business.SelectedPlayer = Content.Gameboard.PlayerSelection.RightPlayer; }

        protected virtual void buttonTimeToBeatIn_Click(object sender, EventArgs e) { this.business.Vinsert_TimeToBeatIn(); }
        protected virtual void buttonTimeToBeatOut_Click(object sender, EventArgs e) { this.business.Vinsert_TimeToBeatOut(); }
        protected virtual void buttonTimeToBeatStart_Click(object sender, EventArgs e) { this.business.Vinsert_StartTimeToBeat(); }
        protected virtual void buttonTimeToBeatStop_Click(object sender, EventArgs e) { this.business.Vinsert_StopTimeToBeat(); }
        protected virtual void buttonTimeToBeatContinue_Click(object sender, EventArgs e) { this.business.Vinsert_ContinueTimeToBeat(); }
        protected virtual void buttonTimeToBeatReset_Click(object sender, EventArgs e) { this.business.Vinsert_ResetTimeToBeat(); }
        private void buttonTimeToBeatShow_Click(object sender, EventArgs e) { this.business.Vinsert_ShowTimeToBeatTime(Convert.ToSingle(this.numericUpDownTimeToBeatTime.Value)); }
        private void buttonTimeToBeatResetTime_Click(object sender, EventArgs e) { this.business.Vinsert_ResetTimeToBeatTime(); }
        private void buttonTimeToBeatShowOffset_Click(object sender, EventArgs e) { this.business.Vinsert_ShowOffsetTime(Convert.ToSingle(this.numericUpDownTimeToBeatOffset.Value)); }
        private void buttonTimeToBeatResetOffset_Click(object sender, EventArgs e) { this.business.Vinsert_ResetOffsetTime(); }

        private void buttonSetTimeToBeat_Click(object sender, EventArgs e) { this.business.TimeToBeat = this.textBoxTimeToBeat.Text.Replace('.', ','); }

        private void numericUpDownCounterOfTarget_ValueChanged(object sender, EventArgs e) { this.business.Target = (int)numericUpDownCounterOfTarget.Value; }
        private void numericUpDownCounterOfValue_ValueChanged(object sender, EventArgs e) { this.business.Counter = (int)numericUpDownCounterOfValue.Value; }
        private void buttonCounterOfValueAdd1_Click(object sender, EventArgs e) { this.business.AddCounter(1); }
        private void CounterOfValueReset_Click(object sender, EventArgs e) { this.business.ResetCounter(); }

        private void buttonVinsert_TimeToBeatIn_Click(object sender, EventArgs e) { this.business.Vinsert_TimeToBeatIn(); }
        private void buttonVinsert_StartTimeToBeat_Click(object sender, EventArgs e) { this.business.Vinsert_StartTimeToBeat(); }
        private void buttonVinsert_StopTimeToBeat_Click(object sender, EventArgs e) { this.business.Vinsert_StopTimeToBeat(); }
        private void buttonVinsert_ResetTimeToBeat_Click(object sender, EventArgs e) { this.business.Vinsert_ResetTimeToBeat(); }
        private void buttonVinsert_ShowTimeToBeatTime_Click(object sender, EventArgs e) { this.business.Vinsert_ShowTimeToBeatTime(); }
        private void buttonVinsert_TimeToBeatOut_Click(object sender, EventArgs e) { this.business.Vinsert_TimeToBeatOut(); }
        private void buttonVinsert_CounterIn_Click(object sender, EventArgs e) { this.business.Vinsert_CounterIn(); }
        private void buttonVinsert_SetCounter_Click(object sender, EventArgs e) { this.business.Vinsert_SetCounter(); }
        private void buttonVinsert_CounterOut_Click(object sender, EventArgs e) { this.business.Vinsert_CounterOut(); }

        private void buttonVfullscreen_SetCounter_Click(object sender, EventArgs e) { this.business.Vfullscreen_SetCounter(); }

        private void buttonGame_NextPlayer_Click(object sender, EventArgs e) { this.business.NextPlayer(); }
        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }

        #endregion

    }
}
