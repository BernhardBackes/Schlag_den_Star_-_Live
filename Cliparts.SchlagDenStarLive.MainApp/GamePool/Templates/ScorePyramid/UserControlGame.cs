using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.ScorePyramid {

    public partial class UserControlGame : _Base.UserControlGame {

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

            bind = new Binding("Text", this.business, "LeftPlayerScore");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxLeftPlayerScore.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerScore");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxRightPlayerScore.DataBindings.Add(bind);

            DatasetLevel level;
            string key;
            UserControlGamePoolTemplatesScorePyramidLevel control;
            for (int i = 0; i < Business.LevelsCount; i++) {
                key = "userControlGamePoolTemplatesScorePyramidLevel_" + i.ToString("00");
                if (this.Controls.ContainsKey(key) &&
                    this.business.TryGetLevel(i, out level)) {
                    control = this.Controls[key] as UserControlGamePoolTemplatesScorePyramidLevel;
                    if (control is UserControlGamePoolTemplatesScorePyramidLevel) {
                        control.Pose(level);
                        control.BackColor = this.BackColor;
                    }
                }
            }

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

            this.textBoxLeftPlayerScore.DataBindings.Clear();
            this.textBoxRightPlayerScore.DataBindings.Clear();

            string key;
            UserControlGamePoolTemplatesScorePyramidLevel control;
            for (int i = 0; i < Business.LevelsCount; i++) {
                key = "userControlGamePoolTemplatesScorePyramidLevel_" + i.ToString("00");
                if (this.Controls.ContainsKey(key)) {
                    control = this.Controls[key] as UserControlGamePoolTemplatesScorePyramidLevel;
                    if (control is UserControlGamePoolTemplatesScorePyramidLevel) control.Dispose();
                }
            }

        }

        protected override void buildStepList() {

            int index = 0;

            stepAction step;

            index = 0;
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
            step.AddButton(this.buttonVstage_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_GameIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex);
            step.AddButton(this.buttonVinsert_SetGame);
            step.AddButton(this.buttonVstage_SetScore);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_GameOut);
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

        #endregion


        #region Events.Incoming
        #endregion

        #region Events.Controls

        private void userControlGame_BackColorChanged(object sender, EventArgs e) {
            string key;
            UserControlGamePoolTemplatesScorePyramidLevel control;
            for (int i = 0; i < Business.LevelsCount; i++) {
                key = "userControlGamePoolTemplatesScorePyramidLevel_" + i.ToString("00");
                if (this.Controls.ContainsKey(key)) {
                    control = this.Controls[key] as UserControlGamePoolTemplatesScorePyramidLevel;
                    if (control is UserControlGamePoolTemplatesScorePyramidLevel) control.BackColor = this.BackColor;
                }
            }
        }

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }

        private void buttonVinsert_GameIn_Click(object sender, EventArgs e) { this.business.Vinsert_GameIn(); }
        private void buttonVinsert_SetGame_Click(object sender, EventArgs e) { this.business.Vinsert_SetGame(); }
        private void buttonVinsert_GameOut_Click(object sender, EventArgs e) { this.business.Vinsert_GameOut(); }

        #endregion

    }

}
