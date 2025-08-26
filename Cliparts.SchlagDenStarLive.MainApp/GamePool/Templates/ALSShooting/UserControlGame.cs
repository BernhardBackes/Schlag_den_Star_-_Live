using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.ALSShooting;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.ALSShooting {

    public partial class UserControlGame : _Base.ShootingScore.UserControlGame {

        #region Properties

        private Business business;

        private Cliparts.ALSShooting.Views.WinForms.FormClient alsForm;

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

            bind = new Binding("BackColor", this.business, "SwapTracks");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorMissing : SystemColors.Control; };
            this.buttonSwapTracks.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "ALSHost");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxALSHost.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "ALSStatus");
            bind.Format += (s, e) => { e.Value = (ConnectionStates)e.Value == ConnectionStates.Connected ? Constants.ColorEnabled : SystemColors.Control; };
            this.textBoxALSHost.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "LockTargets");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorMissing : SystemColors.Control; };
            this.buttonALSLockTargets.DataBindings.Add(bind);

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

            this.buttonSwapTracks.DataBindings.Clear();
            this.textBoxALSHost.DataBindings.Clear();
            this.buttonALSLockTargets.DataBindings.Clear();
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
            step.AddButton(this.buttonVstage_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ShootingIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex);
            step.AddButton(this.buttonVinsert_SetShooting);
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
            step.AddButton(this.buttonGame_Resolve);
            step.AddButton(this.buttonVinsert_SetScore);
            step.AddButton(this.buttonVstage_SetScore);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 5);
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

        #endregion


        #region Events.Incoming

        private void alsForm_Disposed(object sender, EventArgs e) {
            if (this.alsForm is Cliparts.ALSShooting.Views.WinForms.FormClient) {
                this.alsForm.Disposed -= this.alsForm_Disposed;
                this.alsForm = null;
            }
        }

        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void buttonSwapTracks_Click(object sender, EventArgs e) { this.business.SwapTracks = !this.business.SwapTracks; }

        private void buttonALSConnect_Click(object sender, EventArgs e) { this.business.ConnectALS(); }

        private void buttonALSDisconnect_Click(object sender, EventArgs e) { this.business.DisconnectALS(); }

        private void buttonALSLockTargets_Click(object sender, EventArgs e) { this.business.LockTargets = !this.business.LockTargets; }

        private void buttonALSShowForm_Click(object sender, EventArgs e) {
            if (this.alsForm == null) {
                this.alsForm = new Cliparts.ALSShooting.Views.WinForms.FormClient(this.business.alsClient);
                this.alsForm.BackColor = this.BackColor;
                this.alsForm.Disposed += this.alsForm_Disposed;
            }
            this.alsForm.Show();
            this.alsForm.BringToFront();
        }

        #endregion
    }

}
