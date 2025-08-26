using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.BoK100 {

    public partial class UserControlGame : _Base.BuzzerScore.UserControlGame {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.numericUpDownTaskCounter.Minimum = int.MinValue;
            this.numericUpDownTaskCounter.Maximum = int.MaxValue;

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            this.business = business;

            for (int i = 0; i < Business.TaskCounterPenaltyCountMax; i++) {
                BoK.UserControlGamePoolTemplatesBokTaskCounterSingleDot userControlGamePoolTemplatesTaskCounterSingleDot;
                Label labelCounter;
                int left;
                if (i == 0) {
                    userControlGamePoolTemplatesTaskCounterSingleDot = this.userControlGamePoolTemplatesTaskCounterSingleDot_00;
                }
                else {
                    left = this.userControlGamePoolTemplatesTaskCounterSingleDot_00.Left + this.userControlGamePoolTemplatesTaskCounterSingleDot_00.Width * i;
                    userControlGamePoolTemplatesTaskCounterSingleDot = new BoK.UserControlGamePoolTemplatesBokTaskCounterSingleDot();
                    userControlGamePoolTemplatesTaskCounterSingleDot.Left = left;
                    userControlGamePoolTemplatesTaskCounterSingleDot.Name = "userControlGamePoolTemplatesTaskCounterSingleDot_" + i.ToString("00");
                    userControlGamePoolTemplatesTaskCounterSingleDot.Top = this.userControlGamePoolTemplatesTaskCounterSingleDot_00.Top;
                    this.Controls.Add(userControlGamePoolTemplatesTaskCounterSingleDot);
                    labelCounter = new Label();
                    labelCounter.BackColor = this.BackColor;
                    labelCounter.Font = this.labelDotCounter_00.Font;
                    labelCounter.Left = left;
                    labelCounter.Name = "labelDotCounter_" + i.ToString("00");
                    labelCounter.Size = this.labelDotCounter_00.Size;
                    labelCounter.TextAlign = this.labelDotCounter_00.TextAlign;
                    labelCounter.Text = ((int)i + 1).ToString();
                    labelCounter.Top = this.labelDotCounter_00.Top;
                    this.Controls.Add(labelCounter);
                }
                userControlGamePoolTemplatesTaskCounterSingleDot.Pose(this.business.TaskCounterPenaltyDots[i]);
            }

            Binding bind;

            bind = new Binding("Value", this.business, "TaskCounter");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTaskCounter.DataBindings.Add(bind);

            this.adjustUserControlDots();

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

            BoK.UserControlGamePoolTemplatesBokTaskCounterSingleDot control;
            string key;
            for (int i = 0; i < Business.TaskCounterPenaltyCountMax; i++) {
                key = "userControlGamePoolTemplatesTaskCounterSingleDot_" + i.ToString("00");
                control = this.Controls[key] as BoK.UserControlGamePoolTemplatesBokTaskCounterSingleDot;
                if (control is BoK.UserControlGamePoolTemplatesBokTaskCounterSingleDot) {
                    control.Dispose();
                }
            }

            this.numericUpDownTaskCounter.DataBindings.Clear();
        }

        private void adjustUserControlDots() {
            Control control;
            string key;
            for (int i = 0; i < Business.TaskCounterPenaltyCountMax; i++) {
                key = "userControlGamePoolTemplatesTaskCounterSingleDot_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.Visible = i < this.business.TaskCounterSize;
                key = "labelDotCounter_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.Visible = i < this.business.TaskCounterSize;
            }
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
            step.AddButton(this.buttonVinsert_TaskCounterIn);
            step.AddButton(this.buttonVstage_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_ReleaseBuzzer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_StopTimeout);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 2);
            step.AddButton(this.buttonVinsert_SetScore);
            step.AddButton(this.buttonVinsert_SetTaskCounter);
            step.AddButton(this.buttonVstage_SetScore);
            step.AddButton(this.buttonGame_Next);
            this.stepList.Add(step);

            index++;
            this.winnerStepIndex = index;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVgraphic_Clear);
            step.AddButton(this.buttonVstage_Clear);
            step.AddButton(this.buttonGame_LockBuzzer);
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

        protected override void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.business_PropertyChanged(sender, e);
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "TaskCounterSize") this.adjustUserControlDots();
            }
        }

        #endregion

        #region Events.Controls

        private void UserControlGame_BackColorChanged(object sender, EventArgs e) {
            Control control;
            string key;
            for (int i = 0; i < Business.TaskCounterPenaltyCountMax; i++) {
                key = "userControlGamePoolTemplatesTaskCounterSingleDot_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.BackColor = this.BackColor;
                key = "labelDotCounter_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.BackColor = this.BackColor;
            }
        }

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void numericUpDownTaskCounter_ValueChanged(object sender, EventArgs e) { this.business.TaskCounter = (int)this.numericUpDownTaskCounter.Value; }

        private void buttonTrue_Click(object sender, EventArgs e) { this.business.True(); }
        private void buttonFalse_Click(object sender, EventArgs e) { this.business.False(); }

        private void buttonVinsert_TaskCounterIn_Click(object sender, EventArgs e) { this.business.Vinsert_TaskCounterIn(); }
        private void buttonVinsert_SetTaskCounter_Click(object sender, EventArgs e) { this.business.Vinsert_SetTaskCounter(); }
        private void buttonVinsert_TaskCounterOut_Click(object sender, EventArgs e) { this.business.Vinsert_TaskCounterOut(); }

        #endregion
    }
}
