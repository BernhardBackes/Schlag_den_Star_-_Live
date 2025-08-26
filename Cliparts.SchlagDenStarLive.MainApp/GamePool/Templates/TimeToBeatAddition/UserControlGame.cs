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

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimeToBeatAddition {

    public partial class UserControlGame : _Base.UserControlGame {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.numericUpDownLeftPlayerFirstRun.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerFirstRun.Maximum = int.MaxValue;

            this.numericUpDownLeftPlayerSecondRun.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerSecondRun.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerFirstRun.Minimum = int.MinValue;
            this.numericUpDownRightPlayerFirstRun.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerSecondRun.Minimum = int.MinValue;
            this.numericUpDownRightPlayerSecondRun.Maximum = int.MaxValue;

            this.numericUpDownTimeToBeatSentenceTime.Minimum = int.MinValue;
            this.numericUpDownTimeToBeatSentenceTime.Maximum = int.MaxValue;

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "LeftPlayerFirstRun");
            bind.Format += (s, e) => { e.Value = Convert.ToDecimal(e.Value); };
            this.numericUpDownLeftPlayerFirstRun.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerSecondRun");
            bind.Format += (s, e) => { e.Value = Convert.ToDecimal(e.Value); };
            this.numericUpDownLeftPlayerSecondRun.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerFirstRun");
            bind.Format += (s, e) => { e.Value = Convert.ToDecimal(e.Value); };
            this.numericUpDownRightPlayerFirstRun.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerSecondRun");
            bind.Format += (s, e) => { e.Value = Convert.ToDecimal(e.Value); };
            this.numericUpDownRightPlayerSecondRun.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimeToBeatCurrentTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToStopwatchTimeText((double)e.Value, false, true); };
            this.labelTimeToBeatCurrentTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "IOUnitName");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxIOUnitName.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "IOUnitStatus");
            bind.Format += this.bind_textBoxIOUnitName_BackColor;
            this.textBoxIOUnitName.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TimeToBeatSentenceTime");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTimeToBeatSentenceTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimeToBeatSentenceTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimeToBeatSentenceTimeText.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "SentenceTime");
            bind.Format += (s, e) => { e.Value = string.Format("+{0}", Helper.convertDoubleToClockTimeText((int)e.Value, true)); };
            this.buttonAddSentence.DataBindings.Add(bind);

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

            this.numericUpDownLeftPlayerFirstRun.DataBindings.Clear();
            this.numericUpDownLeftPlayerSecondRun.DataBindings.Clear();
            this.numericUpDownRightPlayerFirstRun.DataBindings.Clear();
            this.numericUpDownRightPlayerSecondRun.DataBindings.Clear();

            this.labelTimeToBeatCurrentTime.DataBindings.Clear();
            this.textBoxIOUnitName.DataBindings.Clear();
            this.numericUpDownTimeToBeatSentenceTime.DataBindings.Clear();
            this.labelTimeToBeatSentenceTimeText.DataBindings.Clear();
            this.buttonAddSentence.DataBindings.Clear();

        }

        protected override void buildStepList() {

            int index = 0;

            stepAction step;

            step = new stepAction(index, (stepIndex) => stepIndex + 1);
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
            step.AddButton(this.buttonVinsert_TimeToBeatIn);
            step.AddButton(this.buttonGame_BuzzerOff);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_StartTimeToBeat);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_ReleaseIO);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_StopTimeToBeat);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimeToBeatOut);
            step.AddButton(this.buttonGame_BuzzerBlack);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_NextPlayer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimeToBeatIn_01);
            step.AddButton(this.buttonGame_BuzzerOff_01);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_StartTimeToBeat);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_ReleaseIO_01);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_StopTimeToBeat);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimeToBeatOut);
            step.AddButton(this.buttonGame_BuzzerBlack_01);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 11);
            step.AddButton(this.buttonGame_NextPlayer_01);
            this.stepList.Add(step);

            index++;
            this.winnerStepIndex = index;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVgraphic_Clear);
            step.AddButton(this.buttonVstage_Clear);
            step.AddButton(this.buttonGame_BuzzerBlack_02);
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
                    e.Value = Constants.ColorEnabled;
                    break;
                case BuzzerUnitStates.EventMode:
                    e.Value = Constants.ColorBuzzered;
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
                base.business_PropertyChanged(sender, e);
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
        private void buttonTimeToBeatShow_Click(object sender, EventArgs e) {}
        private void buttonTimeToBeatResetTime_Click(object sender, EventArgs e) {}
        private void buttonTimeToBeatShowOffset_Click(object sender, EventArgs e) { }
        private void buttonTimeToBeatResetOffset_Click(object sender, EventArgs e) {}

        private void buttonReleaseBuzzer_Click(object sender, EventArgs e) { this.business.ReleaseBuzzer(); }
        private void buttonLockBuzzer_Click(object sender, EventArgs e) { this.business.LockBuzzer(); }
        private void buttonBuzzerLeft_Click(object sender, EventArgs e) { this.business.DoBuzzer(this.business.LeftBuzzerChannel); }
        private void buttonBuzzerRight_Click(object sender, EventArgs e) { this.business.DoBuzzer(this.business.RightBuzzerChannel); }

        private void buttonFinish_Click(object sender, EventArgs e) { this.business.PassFinishLine(); }

        private void buttonSetTimeToBeat_Click(object sender, EventArgs e) {}

        private void numericUpDownTimeToBeatSentenceTime_ValueChanged(object sender, EventArgs e) { this.business.TimeToBeatSentenceTime = (int)this.numericUpDownTimeToBeatSentenceTime.Value; }
        private void buttonAddSentence_Click(object sender, EventArgs e) { this.business.AddSentence(); }

        private void buttonVinsert_TimeToBeatIn_Click(object sender, EventArgs e) { this.business.Vinsert_TimeToBeatIn(); }
        private void buttonVinsert_StartTimeToBeat_Click(object sender, EventArgs e) { this.business.Vinsert_StartTimeToBeat(); }
        private void buttonVinsert_StopTimeToBeat_Click(object sender, EventArgs e) { this.business.Vinsert_StopTimeToBeat(); }
        private void buttonVinsert_ShowTimeToBeatTime_Click(object sender, EventArgs e) {}
        private void buttonVinsert_TimeToBeatOut_Click(object sender, EventArgs e) { this.business.Vinsert_TimeToBeatOut(); }

        private void buttonGame_BuzzerOff_Click(object sender, EventArgs e) {
            this.business.SetLeftBuzzerOff(this.business.SelectedPlayer);
            this.business.SetRightBuzzerOff(this.business.SelectedPlayer);
        }
        private void buttonGame_ReleaseIO_Click(object sender, EventArgs e) { this.business.ReleaseBuzzer(); }
        private void buttonGame_NextPlayer_Click(object sender, EventArgs e) { this.business.NextPlayer(); }
        private void buttonGame_BuzzerBlack_Click(object sender, EventArgs e) { this.business.AllLightsBlack(); }

        #endregion

    }

}
