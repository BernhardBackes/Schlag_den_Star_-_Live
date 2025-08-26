using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TripleBuzzerScore {

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

            Binding bind;

            bind = new Binding("Text", this.business, "IOUnitName");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxIOUnitName.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "IOUnitStatus");
            bind.Format += this.bind_textBoxIOUnitName_BackColor;
            this.textBoxIOUnitName.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "LeftPlayerBuzzerIdle1");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? SystemColors.Control : Constants.ColorBuzzered; };
            this.buttonLeftPlayerBuzzerIdle1.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "LeftPlayerBuzzerIdle2");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? SystemColors.Control : Constants.ColorBuzzered; };
            this.buttonLeftPlayerBuzzerIdle2.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "LeftPlayerBuzzerIdle3");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? SystemColors.Control : Constants.ColorBuzzered; };
            this.buttonLeftPlayerBuzzerIdle3.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "RightPlayerBuzzerIdle1");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? SystemColors.Control : Constants.ColorBuzzered; };
            this.buttonRightPlayerBuzzerIdle1.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "RightPlayerBuzzerIdle2");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? SystemColors.Control : Constants.ColorBuzzered; };
            this.buttonRightPlayerBuzzerIdle2.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "RightPlayerBuzzerIdle3");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? SystemColors.Control : Constants.ColorBuzzered; };
            this.buttonRightPlayerBuzzerIdle3.DataBindings.Add(bind);

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

            this.textBoxIOUnitName.DataBindings.Clear();
            this.buttonLeftPlayerBuzzerIdle1.DataBindings.Clear();
            this.buttonLeftPlayerBuzzerIdle2.DataBindings.Clear();
            this.buttonLeftPlayerBuzzerIdle3.DataBindings.Clear();
            this.buttonRightPlayerBuzzerIdle1.DataBindings.Clear();
            this.buttonRightPlayerBuzzerIdle2.DataBindings.Clear();
            this.buttonRightPlayerBuzzerIdle3.DataBindings.Clear();
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

        protected override void buildStepList() {

            int index = 0;
            stepAction step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_LoadScene);
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
            step.AddButton(this.buttonGame_ReleaseBuzzer);
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
            step = new stepAction(index, (stepIndex) => stepIndex - 3);
            step.AddButton(this.buttonVinsert_ScoreOut);
            step.AddButton(this.buttonGame_Next);
            this.stepList.Add(step);

            index++;
            this.winnerStepIndex = index;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVgraphic_Clear);
            step.AddButton(this.buttonVstage_Clear);
            step.AddButton(this.buttonGame_AllLightsBlack);
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

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void buttonReleaseBuzzer_Click(object sender, EventArgs e) { this.business.ReleaseBuzzer(); }
        private void buttonLockBuzzer_Click(object sender, EventArgs e) { this.business.LockBuzzer(); }


        private void buttonLeftPlayerBuzzerIdle1_Click(object sender, EventArgs e) { this.business.LeftPlayerBuzzerIdle1 = !this.business.LeftPlayerBuzzerIdle1; }
        private void buttonLeftPlayerBuzzerIdle2_Click(object sender, EventArgs e) { this.business.LeftPlayerBuzzerIdle2 = !this.business.LeftPlayerBuzzerIdle2; }
        private void buttonLeftPlayerBuzzerIdle3_Click(object sender, EventArgs e) { this.business.LeftPlayerBuzzerIdle3 = !this.business.LeftPlayerBuzzerIdle3; }

        private void buttonRightPlayerBuzzerIdle1_Click(object sender, EventArgs e) { this.business.RightPlayerBuzzerIdle1 = !this.business.RightPlayerBuzzerIdle1; }
        private void buttonRightPlayerBuzzerIdle2_Click(object sender, EventArgs e) { this.business.RightPlayerBuzzerIdle2 = !this.business.RightPlayerBuzzerIdle2; }
        private void buttonRightPlayerBuzzerIdle3_Click(object sender, EventArgs e) { this.business.RightPlayerBuzzerIdle3 = !this.business.RightPlayerBuzzerIdle3; }

        private void buttonGame_ReleaseBuzzer_Click(object sender, EventArgs e) { if (this.business is Business) this.business.ReleaseBuzzer(); }
        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }
        private void buttonGame_AllLightsBlack_Click(object sender, EventArgs e) { this.business.AllLightsBlack(); }

        #endregion

    }

}
