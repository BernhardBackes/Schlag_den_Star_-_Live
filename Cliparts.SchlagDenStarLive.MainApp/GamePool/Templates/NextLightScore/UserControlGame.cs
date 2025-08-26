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

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.NextLightScore {

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

            bind = new Binding("Text", this.business, "LeftPlayerName");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxLeftPlayerName_1.DataBindings.Add(bind);
            
            bind = new Binding("Value", this.business, "LeftPlayerCounter");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerCounter.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerName");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxRightPlayerName_1.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerCounter");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerCounter.DataBindings.Add(bind);

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

            this.textBoxIOUnitName.DataBindings.Clear();;

            this.textBoxLeftPlayerName_1.DataBindings.Clear();
            this.numericUpDownLeftPlayerCounter.DataBindings.Clear();
            this.textBoxRightPlayerName_1.DataBindings.Clear();
            this.numericUpDownRightPlayerCounter.DataBindings.Clear();
        }

        protected override void buildStepList() {

            int index = 0;
            stepAction step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_LoadScene);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_Init);
            step.AddButton(this.buttonVgraphic_Clear);
            step.AddButton(this.buttonVstage_Clear);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVstage_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_ReleaseBuzzer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_LockBuzzer);
            step.AddButton(this.buttonVinsert_ScoreIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_Resolve);
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
                    e.Value = Constants.ColorBuzzered;
                    break;
                case BuzzerUnitStates.EventMode:
                    e.Value = Constants.ColorEnabled;
                    break;
                default:
                    break;
            }
        }

        protected override void adjustKeyControl(bool isEnabled) {
            base.adjustKeyControl(isEnabled);
            if (isEnabled) {
                this.buttonLeftPlayerAddCounterHot_01.Text = "+1 [W]";
                this.buttonRightPlayerAddCounterHot_01.Text = "+1 [O]";
            }
            else {
                this.buttonLeftPlayerAddCounterHot_01.Text = "+1";
                this.buttonRightPlayerAddCounterHot_01.Text = "+1";
            }
        }

        public override void ParseKey(Keys keycode) {
            base.ParseKey(keycode);
            if (this.keyControl) {
                switch (keycode) {
                    case Keys.W:
                        this.buttonLeftPlayerAddCounterHot_01_Click(this, new EventArgs());
                        break;
                    case Keys.O:
                        this.buttonRightPlayerAddCounterHot_01_Click(this, new EventArgs());
                        break;
                }
            }
        }

        #endregion


        #region Events.Incoming
        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        protected void buttonReleaseBuzzer_Click(object sender, EventArgs e) { if (this.business is Business) this.business.ReleaseBuzzer(); }
        protected void buttonLockBuzzer_Click(object sender, EventArgs e) { if (this.business is Business) this.business.LockBuzzer(); }

        private void buttonPlayJingleStart_Click(object sender, EventArgs e) { this.business.Vinsert_PlayJingleStart(); }

        protected virtual void numericUpDownLeftPlayerCounter_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerCounter = (int)this.numericUpDownLeftPlayerCounter.Value; }
        protected virtual void buttonLeftPlayerAddCounterHot_01_Click(object sender, EventArgs e) { 
            this.business.LeftPlayerCounter++;
            this.business.SetDMXValues();
        }

        protected virtual void numericUpDownRightPlayerCounter_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerCounter = (int)this.numericUpDownRightPlayerCounter.Value; }
        protected virtual void buttonRightPlayerAddCounterHot_01_Click(object sender, EventArgs e) { 
            this.business.RightPlayerCounter++;
            this.business.SetDMXValues();
        }

        private void buttonSetDMXValues_Click(object sender, EventArgs e) { this.business.SetDMXValues(); }

        private void buttonGame_ReleaseBuzzer_Click(object sender, EventArgs e) { this.business.ReleaseBuzzer(); }
        private void buttonGame_LockBuzzer_Click(object sender, EventArgs e) { this.business.LockBuzzer(); }
        protected virtual void buttonGame_Resolve_Click(object sender, EventArgs e) { this.business.Resolve(); }
        protected virtual void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }


        #endregion

    }

}
