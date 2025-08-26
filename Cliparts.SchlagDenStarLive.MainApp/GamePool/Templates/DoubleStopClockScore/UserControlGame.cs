using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.DoubleStopClockScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.DoubleStopClockScore {

    public partial class UserControlGame : _Base.Score.UserControlGame {

        #region Properties

        private Business business;

        private VRemote4.HandlerSi.Business localVentuzHandler;
        private VRemote4.HandlerSi.BusinessForm localVentuzHandlerForm;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Business localVentuzHandler,
            DoubleStopClock doubleStopClockScene) {
            base.Pose(business);

            this.business = business;

            this.localVentuzHandler = localVentuzHandler;

            Binding bind;

            bind = new Binding("BackColor", doubleStopClockScene, "LeftClockLeftBuzzerIsDown");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorBuzzered : SystemColors.Control; };
            this.panelLeftClockLeftBuzzer.DataBindings.Add(bind);

            bind = new Binding("BackColor", doubleStopClockScene, "LeftClockRightBuzzerIsDown");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorBuzzered : SystemColors.Control; };
            this.panelLeftClockRightBuzzer.DataBindings.Add(bind);

            bind = new Binding("Visible", doubleStopClockScene, "LeftClockIsWinner");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.labelLeftClockIsWinner.DataBindings.Add(bind);

            bind = new Binding("BackColor", doubleStopClockScene, "RightClockLeftBuzzerIsDown");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorBuzzered : SystemColors.Control; };
            this.panelRightClockLeftBuzzer.DataBindings.Add(bind);

            bind = new Binding("BackColor", doubleStopClockScene, "RightClockRightBuzzerIsDown");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorBuzzered : SystemColors.Control; };
            this.panelRightClockRightBuzzer.DataBindings.Add(bind);

            bind = new Binding("Visible", doubleStopClockScene, "RightClockIsWinner");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.labelRightClockIsWinner.DataBindings.Add(bind);

            VRemote4.HandlerSi.Client.Business displaysClient;
            if (this.localVentuzHandler.TryGetClient("DoubleStopClock", out displaysClient)) this.userControlClientStateDoubleStopClock.Pose(displaysClient);
            this.userControlClientStateDoubleStopClock.BackColor = this.BackColor;

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

            this.panelLeftClockLeftBuzzer.DataBindings.Clear();
            this.panelLeftClockRightBuzzer.DataBindings.Clear();
            this.labelLeftClockIsWinner.DataBindings.Clear();
            this.panelRightClockLeftBuzzer.DataBindings.Clear();
            this.panelRightClockRightBuzzer.DataBindings.Clear();
            this.labelRightClockIsWinner.DataBindings.Clear();

            this.userControlClientStateDoubleStopClock.Dispose();
        }

        protected override void buildStepList() {

            int index = 0;
            stepAction step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_LoadScene);
            step.AddButton(this.buttonVdoubleStopClock_Start);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVgraphic_Clear);
            step.AddButton(this.buttonVstage_Clear);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVstage_Init);
            step.AddButton(this.buttonGame_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVdoubleStopClock_Reset);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVdoubleStopClock_Stop);
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
            step = new stepAction(index, (stepIndex) => stepIndex - 4);
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
            step.AddButton(this.buttonVdoubleStopClock_ShutDown);
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

        private void localVentuzHandlerForm_Disposed(object sender, EventArgs e) {
            if (this.localVentuzHandlerForm is VRemote4.HandlerSi.BusinessForm) {
                this.localVentuzHandlerForm.Disposed -= this.localVentuzHandlerForm_Disposed;
                this.localVentuzHandlerForm = null;
            }
        }

        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void UserControlGame_BackColorChanged(object sender, EventArgs e) {
            this.userControlClientStateDoubleStopClock.BackColor = this.BackColor;
        }

        private void buttonPlayJingleStart_Click(object sender, EventArgs e) { this.business.Vinsert_PlayJingleStart(); }

        private void buttonResetLeftClock_Click(object sender, EventArgs e) { this.business.VdoubleStopClock_ResetLeftClock(); }
        private void buttonEnableLeftClock_Click(object sender, EventArgs e) { this.business.VdoubleStopClock_EnableLeftClock(); }
        private void buttonStopLeftClock_Click(object sender, EventArgs e) { this.business.VdoubleStopClock_StopLeftClock(); }
        private void buttonResetRightClock_Click(object sender, EventArgs e) { this.business.VdoubleStopClock_ResetRightClock(); }
        private void buttonEnableRightClock_Click(object sender, EventArgs e) { this.business.VdoubleStopClock_EnableRightClock(); }
        private void buttonStopRightClock_Click(object sender, EventArgs e) { this.business.VdoubleStopClock_StopRightClock_(); }
        private void buttonResetStopClock_Click(object sender, EventArgs e) { this.business.VdoubleStopClock_Reset(); }
        private void buttonStopStopClock_Click(object sender, EventArgs e) { this.business.VdoubleStopClock_Stop(); }

        private void buttonShowVentuzHandler_Click(object sender, EventArgs e) {
            if (this.localVentuzHandlerForm == null) {
                this.localVentuzHandlerForm = new VRemote4.HandlerSi.BusinessForm(this.localVentuzHandler, this.BackColor);
                this.localVentuzHandlerForm.Disposed += this.localVentuzHandlerForm_Disposed;
            }
            this.localVentuzHandlerForm.Show();
            this.localVentuzHandlerForm.BringToFront();
        }

        private void buttonVdoubleStopClock_Start_Click(object sender, EventArgs e) { this.business.VdoubleStopClock_Start(); }
        private void buttonVdoubleStopClock_Reset_Click(object sender, EventArgs e) { this.business.VdoubleStopClock_Reset(); }
        private void buttonVdoubleStopClock_Stop_Click(object sender, EventArgs e) { this.business.VdoubleStopClock_Stop(); }
        private void buttonVdoubleStopClock_ShutDown_Click(object sender, EventArgs e) { this.business.VdoubleStopClock_ShutDown(); }

        #endregion

    }

}
