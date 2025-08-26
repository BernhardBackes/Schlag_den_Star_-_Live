using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.WheelOfFortuneNumericInput {

    public partial class UserControlGame : _Base.Score.UserControlGame {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.numericUpDownLeftPlayerInputValue.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerInputValue.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerInputValue.Minimum = int.MinValue;
            this.numericUpDownRightPlayerInputValue.Maximum = int.MaxValue;

            this.numericUpDownWheelValue.Minimum = int.MinValue;
            this.numericUpDownWheelValue.Maximum = int.MaxValue;

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "LeftPlayerInputValue");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerInputValue.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerInputValue");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerInputValue.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "WheelValue");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownWheelValue.DataBindings.Add(bind);

            this.userControlWheelOfFortuneNumericInputLeftPlayerClient.BackColor = this.BackColor;
            this.userControlWheelOfFortuneNumericInputLeftPlayerClient.Pose("left player", business.LeftPlayerClient);

            this.userControlWheelOfFortuneNumericInputRightPlayerClient.BackColor = this.BackColor;
            this.userControlWheelOfFortuneNumericInputRightPlayerClient.Pose("right player", business.RightPlayerClient);

            this.labelGameClass.Text = this.business.ClassInfo;

            this.setBuzzeredPlayer();
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

            this.numericUpDownLeftPlayerInputValue.DataBindings.Clear();
            this.numericUpDownRightPlayerInputValue.DataBindings.Clear();

            this.numericUpDownWheelValue.DataBindings.Clear();
        }

        protected override void buildStepList() {

            int index = 0;
            stepAction step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_LoadScene);
            step.AddButton(this.buttonVfullscreen_LoadScene);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_Init);
            step.AddButton(this.buttonVgraphic_Clear);
            step.AddButton(this.buttonVstage_Clear);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_ShowGame);
            step.AddButton(this.buttonVstage_Init);
            step.AddButton(this.buttonGame_ConnectClients);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ScoreIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_ResetValue);
            step.AddButton(this.buttonGame_UnlockClients);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex);
            step.AddButton(this.buttonGame_AddWheelValue);
            step.AddButton(this.buttonVfullscreen_SetValue);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_Resolve);
            step.AddButton(this.buttonVinsert_SetScore);
            step.AddButton(this.buttonVstage_SetScore);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 3);
            step.AddButton(this.buttonGame_Next);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ScoreOut);
            this.stepList.Add(step);

            index++;
            this.winnerStepIndex = index;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_DisconnectClients);
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

        private void setBuzzeredPlayer() {
            switch (this.business.BuzzeredPlayer) {
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.LeftPlayer:
                    this.buttonLeftPlayerBuzzer.BackColor = Constants.ColorBuzzered;
                    this.buttonRightPlayerBuzzer.UseVisualStyleBackColor = true;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.RightPlayer:
                    this.buttonLeftPlayerBuzzer.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerBuzzer.BackColor = Constants.ColorBuzzered;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.NotSelected:
                default:
                    this.buttonLeftPlayerBuzzer.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerBuzzer.UseVisualStyleBackColor = true;
                    break;
            }
        }

        #endregion


        #region Events.Incoming

        protected override void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.business_PropertyChanged(sender, e);
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "BuzzeredPlayer") this.setBuzzeredPlayer();
            }
        }

        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void UserControlGame_BackColorChanged(object sender, EventArgs e) {
            this.userControlWheelOfFortuneNumericInputLeftPlayerClient.BackColor = this.BackColor;
            this.userControlWheelOfFortuneNumericInputRightPlayerClient.BackColor = this.BackColor;
        }

        private void numericUpDownLeftPlayerInputValue_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftPlayerInputValue = (int)this.numericUpDownLeftPlayerInputValue.Value; }
        private void numericUpDownRightPlayerInputValue_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightPlayerInputValue = (int)this.numericUpDownRightPlayerInputValue.Value; }

        protected void buttonLeftPlayerBuzzer_Click(object sender, EventArgs e) { this.business.DoBuzzer(Content.Gameboard.PlayerSelection.LeftPlayer, this.business.LeftPlayerInputValue); }
        protected void buttonRightPlayerBuzzer_Click(object sender, EventArgs e) { this.business.DoBuzzer(Content.Gameboard.PlayerSelection.RightPlayer, this.business.RightPlayerInputValue); }

        private void numericUpDownWheelValue_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.WheelValue = (int)this.numericUpDownWheelValue.Value; }
        private void buttonWheelAddValueHot_01_Click(object sender, EventArgs e) {
            this.business.AddWheelValue();
            this.business.Vfullscreen_SetValue(this.business.WheelValue);
        }
        private void buttonWheelSetValue_Click(object sender, EventArgs e) { this.business.Vfullscreen_SetValue(this.business.WheelValue); }

        private void buttonWheelStopAudio_Click(object sender, EventArgs e) { this.business.Vfullscreen_StopAudio(); }
        private void buttonWheelGood_Click(object sender, EventArgs e) { this.business.WheelGood(); }
        private void buttonWheelBad_Click(object sender, EventArgs e) { this.business.WheelBad(); }

        private void buttonVfullscreen_ResetValue_Click(object sender, EventArgs e) { this.business.Vfullscreen_ResetValue(); }
        private void buttonVfullscreen_SetValue_Click(object sender, EventArgs e) { this.business.Vfullscreen_SetValue(this.business.WheelValue); }

        private void buttonGame_ConnectClients_Click(object sender, EventArgs e) { this.business.ConnectClients(); }
        private void buttonGame_UnlockClients_Click(object sender, EventArgs e) { this.business.UnlockPlayerClients(); }
        private void buttonGame_AddWheelValue_Click(object sender, EventArgs e) { this.business.AddWheelValue(); }
        private void buttonGame_Resolve_Click(object sender, EventArgs e) { this.business.Resolve(); }
        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }
        private void buttonGame_DisconnectClients_Click(object sender, EventArgs e) { this.business.DisconnectClients(); }

        #endregion

    }

}
