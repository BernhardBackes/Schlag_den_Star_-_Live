using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.Moelkky {

    public partial class UserControlGame : _Base.ScoreFaults.UserControlGame {

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

            this.textBoxLeftPlayerName_1.DataBindings.Clear();
            this.textBoxRightPlayerName_1.DataBindings.Clear();
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
            step.AddButton(this.buttonVfullscreen_ShowScore);
            step.AddButton(this.buttonVstage_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ScoreIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex);
            step.AddButton(this.buttonVinsert_SetScore);
            step.AddButton(this.buttonVfullscreen_SetScore);
            step.AddButton(this.buttonVstage_SetScore);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
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

        private void leftPlayerScored() {
            this.business.Vinsert_SetScore();
            this.business.Vstage_SetScore();
            this.business.LeftPlayerFaults = 0;
            this.business.Vinsert_LeftFaultsOut();
        }
        private void rightPlayerScored() {
            this.business.Vinsert_SetScore();
            this.business.Vstage_SetScore();
            this.business.RightPlayerFaults = 0;
            this.business.Vinsert_RightFaultsOut();
        }

        #endregion


        #region Events.Incoming
        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        protected void buttonLeftPlayerAddScoreHot_Click(object sender, EventArgs e) {
            int index;
            if (Helper.tryParseIndexFromControl(sender as Control, out index)) {
                if (index > 1) this.business.LeftPlayerScore += index;
                this.leftPlayerScored();
            }
            this.business.Vinsert_SetScore();
            this.business.Vfullscreen_SetScore();
            this.business.Vstage_SetScore();
        }
        private void buttonLeftPlayerSubtractScoreHot_15_Click(object sender, EventArgs e) {
            this.business.LeftPlayerScore -= 15;
            this.leftPlayerScored();
            this.business.Vinsert_SetScore();
            this.business.Vfullscreen_SetScore();
            this.business.Vstage_SetScore();
        }
        private void buttonLeftPlayerFaultHot_Click(object sender, EventArgs e) {
            this.business.LeftPlayerFaults++;
            this.business.Vinsert_LeftFaultsIn();
        }

        private void buttonRightPlayerAddScoreHot_Click(object sender, EventArgs e) {
            int index;
            if (Helper.tryParseIndexFromControl(sender as Control, out index)) {
                if (index > 1) this.business.RightPlayerScore += index;
                this.rightPlayerScored();
            }
            this.business.Vinsert_SetScore();
            this.business.Vfullscreen_SetScore();
            this.business.Vstage_SetScore();
        }
        private void buttonRightPlayerSubtractScoreHot_15_Click(object sender, EventArgs e) {
            this.business.RightPlayerScore -= 15;
            this.rightPlayerScored();
            this.business.Vinsert_SetScore();
            this.business.Vfullscreen_SetScore();
            this.business.Vstage_SetScore();
        }
        private void buttonRightPlayerFaultHot_Click(object sender, EventArgs e) {
            this.business.RightPlayerFaults++;
            this.business.Vinsert_RightFaultsIn();
        }
        
        #endregion

    }

}
