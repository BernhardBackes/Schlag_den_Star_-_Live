using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TeamBuzzerScore
{

    public partial class UserControlGame : _Base.BuzzerScore.UserControlGame {

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

            bind = new Binding("BackColor", this.business, "LeftPlayer1stBuzzered");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorBuzzered : SystemColors.ButtonFace; };
            this.buttonLeftPlayer1stBuzzer.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "LeftPlayer2ndBuzzered");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorBuzzered : SystemColors.ButtonFace; };
            this.buttonLeftPlayer2ndBuzzer.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "RightPlayer1stBuzzered");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorBuzzered : SystemColors.ButtonFace; };
            this.buttonRightPlayer1stBuzzer.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "RightPlayer2ndBuzzered");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorBuzzered : SystemColors.ButtonFace; };
            this.buttonRightPlayer2ndBuzzer.DataBindings.Add(bind);

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

            this.buttonLeftPlayer1stBuzzer.DataBindings.Clear();
            this.buttonLeftPlayer2ndBuzzer.DataBindings.Clear();
            this.buttonRightPlayer1stBuzzer.DataBindings.Clear();
            this.buttonRightPlayer2ndBuzzer.DataBindings.Clear();

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
            step.AddButton(this.buttonVinsert_ScoreIn);
            step.AddButton(this.buttonVstage_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_ReleaseBuzzer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_SetScore);
            step.AddButton(this.buttonVstage_SetScore);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 2);
            step.AddButton(this.buttonGame_Next);
            this.stepList.Add(step);

            index++;
            this.winnerStepIndex = index;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVgraphic_Clear);
            step.AddButton(this.buttonVstage_Clear);
            step.AddButton(this.buttonGame_LockBuzzer);
            step.AddButton(this.buttonVinsert_UnloadScene);
            step.AddButton(this.buttonVfullscreen_ShowGameboard);
            step.AddButton(this.buttonGame_SetWinner);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_UpdateGameboard);
            step.AddButton(this.buttonGame_AllToBlack);
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

        private void buttonLeftPlayer1stBuzzer_Click(object sender, EventArgs e) { this.business.DoBuzzer(Content.Gameboard.PlayerSelection.LeftPlayer, false); }
        private void buttonLeftPlayer2ndBuzzer_Click(object sender, EventArgs e) { this.business.DoBuzzer(Content.Gameboard.PlayerSelection.LeftPlayer, true); }

        private void buttonRightPlayer1stBuzzer_Click(object sender, EventArgs e) { this.business.DoBuzzer(Content.Gameboard.PlayerSelection.RightPlayer, false); }
        private void buttonRightPlayer2ndBuzzer_Click(object sender, EventArgs e) { this.business.DoBuzzer(Content.Gameboard.PlayerSelection.RightPlayer, true); }

        private void buttonGame_AllToBlack_Click(object sender, EventArgs e) { this.business.AllLightsBlack(); }

        #endregion

    }

}
