using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.DoubleRGBIndicators {

    public partial class UserControlGame : _Base.Score.UserControlGame {

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
            this.business.PropertyChanged += this.business_PropertyChanged;

            foreach (Indicator indicator in this.business.Indicators) { 
                UserControlIndicator userControlIndicator;
                Label labelCounter;
                int left;
                int width = this.userControlIndicator_00.Margin.Left + this.userControlIndicator_00.Width + this.userControlIndicator_00.Margin.Right;
                if (indicator.Index == 0) {
                    userControlIndicator = this.userControlIndicator_00;
                    labelCounter = this.labelCounter_00;
                }
                else {
                    left = this.userControlIndicator_00.Left + width * indicator.Index;
                    userControlIndicator = new UserControlIndicator();
                    userControlIndicator.Left = left;
                    userControlIndicator.Name = "userControlIndicator_" + indicator.Index.ToString("00");
                    userControlIndicator.Top = this.userControlIndicator_00.Top;
                    userControlIndicator.BackColor = this.BackColor;
                    userControlIndicator.Size = this.userControlIndicator_00.Size;
                    this.Controls.Add(userControlIndicator);
                    labelCounter = new Label();                    
                    labelCounter.Font = this.labelCounter_00.Font;
                    labelCounter.Left = left;
                    labelCounter.Name = "labelCounter_" + indicator.Index.ToString("00");
                    labelCounter.Size = this.labelCounter_00.Size;
                    labelCounter.TextAlign = this.labelCounter_00.TextAlign;                    
                    labelCounter.Top = this.labelCounter_00.Top;
                    this.Controls.Add(labelCounter);
                }
                userControlIndicator.Pose(indicator);
                labelCounter.Text = string.Format("#{0}", indicator.ID.ToString());
            }

            this.adjustUserControlIndicators();

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

            this.business.PropertyChanged -= this.business_PropertyChanged;
        }

        protected override void buildStepList() {

            int index = 0;
            stepAction step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_LoadScene);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, this.showFullscreenTimer);
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
            step.AddButton(this.buttonVinsert_ScoreIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_SetScore);
            step.AddButton(this.buttonVstage_SetScore);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 2);
            step.AddButton(this.buttonVinsert_ScoreOut);
            step.AddButton(this.buttonGame_Next);
            this.stepList.Add(step);

            index++;
            this.winnerStepIndex = index;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_ShowGameboard);
            step.AddButton(this.buttonVgraphic_Clear);
            step.AddButton(this.buttonVstage_Clear);
            step.AddButton(this.buttonGame_AllLightsBlack);
            step.AddButton(this.buttonVinsert_UnloadScene);
            step.AddButton(this.buttonGame_SetWinner);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_UpdateGameboard);
            this.stepList.Add(step);

            if (index < this.stepsMaxIndex) this.on_Error("buildStepList", "stepList to short");
            if (index > this.stepsMaxIndex) this.on_Error("buildStepList", "stepList to long");
        }
        protected int showFullscreenTimer(
            int stepIndex) {
            if (this.business.ShowFullscreenTimer) return stepIndex + 1;
            else return stepIndex + 2;
        }

        private void adjustUserControlIndicators() {
            Control control;
            string key;
            for (int i = 0; i < Business.IndicatorsCountMax; i++) {
                key = "userControlIndicator_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.Visible = i < this.business.IndicatorsCount;
                key = "labelCounter_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.Visible = i < this.business.IndicatorsCount;
            }
        }

        #endregion


        #region Events.Incoming

        protected override void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.business_PropertyChanged(sender, e);
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "IndicatorsCount") this.adjustUserControlIndicators();
            }
        }

        #endregion

        #region Events.Controls

        private void UserControlGame_BackColorChanged(object sender, EventArgs e) {
            Control control;
            string key;
            for (int i = 0; i < Business.IndicatorsCountMax; i++) {
                key = "userControlIndicator_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.BackColor = this.BackColor;
                key = "labelCounter_" + i.ToString("00");
                control = this.Controls[key] as Control;
                if (control is Control) control.BackColor = this.BackColor;
            }
        }

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void buttonLeftOff_Click(object sender, EventArgs e) { this.business.LeftOff(); }
        private void buttonAllOFF_Click(object sender, EventArgs e) { this.business.AllLightsOff(); }
        private void buttonRightOff_Click(object sender, EventArgs e) { this.business.RightOff();  }

        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }
        private void buttonGame_AllLightsBlack_Click(object sender, EventArgs e) { this.business.AllLightsBlack(); }

        #endregion

    }

}
