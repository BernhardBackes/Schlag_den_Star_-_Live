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

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimerTimeToBeat {

    public partial class UserControlGame : _Base.Timer.UserControlGame {

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

            bind = new Binding("Text", this.business, "IOUnitName");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxIOUnitName.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "IOUnitStatus");
            bind.Format += this.bind_textBoxIOUnitName_BackColor;
            this.textBoxIOUnitName.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimeToBeat");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxTimeToBeat.DataBindings.Add(bind);

            this.userControlRelaysLeftRightGame.Pose(this.business.DevantechIO);
            this.userControlRelaysLeftRightGame.BackColor = this.BackColor;

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
            this.textBoxIOUnitName.DataBindings.Clear();
            this.textBoxTimeToBeat.DataBindings.Clear();

            this.userControlRelaysLeftRightGame.Dispose();
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
            step.AddButton(this.buttonVfullscreen_ResetTimer_1);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimeToBeatIn);
            step.AddButton(this.buttonGame_CloseRelay);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_OpenRelay);
            step.AddButton(this.buttonVinsert_StartTimeToBeat);
            step.AddButton(this.buttonGame_ReleaseIO);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_StopTimeToBeat);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_NextPlayer);
            step.AddButton(this.buttonGame_CloseRelay_1);
            step.AddButton(this.buttonVinsert_ShowTimeToBeatTime);
            step.AddButton(this.buttonVinsert_ResetTimeToBeat);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_OpenRelay_1);
            step.AddButton(this.buttonVinsert_StartTimeToBeat_1);
            step.AddButton(this.buttonGame_ReleaseIO_1);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_StopTimeToBeat);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimeToBeatOut);
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

        private void UserControlGame_BackColorChanged(object sender, EventArgs e) {
            this.userControlRelaysLeftRightGame.BackColor = this.BackColor;
        }

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

        private void buttonReleaseBuzzer_Click(object sender, EventArgs e) { this.business.ReleaseBuzzer(); }
        private void buttonLockBuzzer_Click(object sender, EventArgs e) { this.business.LockBuzzer(); }
        private void buttonBuzzer_Click(object sender, EventArgs e) { this.business.PassFinishLine(); }

        private void buttonSetTimeToBeat_Click(object sender, EventArgs e) { this.business.TimeToBeat = this.textBoxTimeToBeat.Text.Replace('.', ','); }

        private void buttonVinsert_TimeToBeatIn_Click(object sender, EventArgs e) { this.business.Vinsert_TimeToBeatIn(); }
        private void buttonVinsert_StartTimeToBeat_Click(object sender, EventArgs e) { this.business.Vinsert_StartTimeToBeat(); }
        private void buttonVinsert_StopTimeToBeat_Click(object sender, EventArgs e) { this.business.Vinsert_StopTimeToBeat(); }
        private void buttonVinsert_ShowTimeToBeatTime_Click(object sender, EventArgs e) { this.business.Vinsert_ShowTimeToBeatTime(); }
        private void buttonVinsert_ResetTimeToBeat_Click(object sender, EventArgs e) { this.business.Vinsert_ResetTimeToBeat(); }
        private void buttonVinsert_TimeToBeatOut_Click(object sender, EventArgs e) { this.business.Vinsert_TimeToBeatOut(); }

        private void buttonGame_CloseRelay_Click(object sender, EventArgs e) { this.business.CloseRelais(); }
        private void buttonGame_OpenRelay_Click(object sender, EventArgs e) { this.business.OpenRelais(); }
        private void buttonGame_ReleaseIO_Click(object sender, EventArgs e) { this.business.ReleaseBuzzer(); }
        private void buttonGame_NextPlayer_Click(object sender, EventArgs e) { this.business.NextPlayer(); }

        #endregion

    }

}
