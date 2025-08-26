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

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimerRandomBuzzerScore {

    public partial class UserControlGame : _Base.Timer.UserControlGame {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.numericUpDownLeftPlayerScore.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerScore.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerScore.Minimum = int.MinValue;
            this.numericUpDownRightPlayerScore.Maximum = int.MaxValue;

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "LeftPlayerScore");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerScore.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerScore");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerScore.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "FlipPlayers");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxFlipPlayers.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "IOUnitName");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxIOUnitName.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "IOUnitStatus");
            bind.Format += this.bind_textBoxIOUnitName_BackColor;
            this.textBoxIOUnitName.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "ActiveBuzzer");
            bind.Format += (s, e) => { e.Value = (ActiveBuzzerStates)e.Value == ActiveBuzzerStates.Top ? this.business.OnColor : this.business.OffColor; };
            this.buttonSetBuzzerTop.DataBindings.Add(bind);
            bind = new Binding("ForeColor", this.business, "ActiveBuzzer");
            bind.Format += (s, e) => { e.Value = (ActiveBuzzerStates)e.Value == ActiveBuzzerStates.Top ? this.business.OffColor : this.business.OnColor; };
            this.buttonSetBuzzerTop.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "ActiveBuzzer");
            bind.Format += (s, e) => { e.Value = (ActiveBuzzerStates)e.Value == ActiveBuzzerStates.Left ? this.business.OnColor : this.business.OffColor; };
            this.buttonSetBuzzerLeft.DataBindings.Add(bind);
            bind = new Binding("ForeColor", this.business, "ActiveBuzzer");
            bind.Format += (s, e) => { e.Value = (ActiveBuzzerStates)e.Value == ActiveBuzzerStates.Left ? this.business.OffColor : this.business.OnColor; };
            this.buttonSetBuzzerLeft.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "ActiveBuzzer");
            bind.Format += (s, e) => { e.Value = (ActiveBuzzerStates)e.Value == ActiveBuzzerStates.Right ? this.business.OnColor : this.business.OffColor; };
            this.buttonSetBuzzerRight.DataBindings.Add(bind);
            bind = new Binding("ForeColor", this.business, "ActiveBuzzer");
            bind.Format += (s, e) => { e.Value = (ActiveBuzzerStates)e.Value == ActiveBuzzerStates.Right ? this.business.OffColor : this.business.OnColor; };
            this.buttonSetBuzzerRight.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "ActiveBuzzer");
            bind.Format += (s, e) => { e.Value = (ActiveBuzzerStates)e.Value == ActiveBuzzerStates.Bottom ? this.business.OnColor : this.business.OffColor; };
            this.buttonSetBuzzerBottom.DataBindings.Add(bind);
            bind = new Binding("ForeColor", this.business, "ActiveBuzzer");
            bind.Format += (s, e) => { e.Value = (ActiveBuzzerStates)e.Value == ActiveBuzzerStates.Bottom ? this.business.OffColor : this.business.OnColor; };
            this.buttonSetBuzzerBottom.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "StartBuzzer");
            bind.Format += (s, e) => { e.Value = (ActiveBuzzerStates)e.Value == ActiveBuzzerStates.Top; };
            this.checkBoxStartBuzzerTop.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "StartBuzzer");
            bind.Format += (s, e) => { e.Value = (ActiveBuzzerStates)e.Value == ActiveBuzzerStates.Left; };
            this.checkBoxStartBuzzerLeft.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "StartBuzzer");
            bind.Format += (s, e) => { e.Value = (ActiveBuzzerStates)e.Value == ActiveBuzzerStates.Right; };
            this.checkBoxStartBuzzerRight.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "StartBuzzer");
            bind.Format += (s, e) => { e.Value = (ActiveBuzzerStates)e.Value == ActiveBuzzerStates.Bottom; };
            this.checkBoxStartBuzzerBottom.DataBindings.Add(bind);


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

            this.numericUpDownLeftPlayerScore.DataBindings.Clear();
            this.numericUpDownRightPlayerScore.DataBindings.Clear();
            this.checkBoxFlipPlayers.DataBindings.Clear();
            this.textBoxIOUnitName.DataBindings.Clear();
            this.buttonSetBuzzerTop.DataBindings.Clear();
            this.buttonSetBuzzerLeft.DataBindings.Clear();
            this.buttonSetBuzzerRight.DataBindings.Clear();
            this.buttonSetBuzzerBottom.DataBindings.Clear();
            this.checkBoxStartBuzzerTop.DataBindings.Clear();
            this.checkBoxStartBuzzerLeft.DataBindings.Clear();
            this.checkBoxStartBuzzerRight.DataBindings.Clear();
            this.checkBoxStartBuzzerBottom.DataBindings.Clear();
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
            step.AddButton(this.buttonGame_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_ShowTimer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ScoreIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimerIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_StartTimer);
            step.AddButton(this.buttonVinsert_StartTimer);
            step.AddButton(this.buttonGame_Start);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex);
            step.AddButton(this.buttonVinsert_SetScore);
            step.AddButton(this.buttonVstage_SetScore);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_StopTimer);
            step.AddButton(this.buttonVinsert_StopTimer);
            step.AddButton(this.buttonGame_Stop);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimerOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 6);
            step.AddButton(this.buttonVinsert_ScoreOut);
            step.AddButton(this.buttonVfullscreen_ResetTimer);
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

        protected virtual void numericUpDownLeftPlayerScore_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerScore = (int)this.numericUpDownLeftPlayerScore.Value; }
        protected virtual void buttonLeftPlayerAddScoreHot_01_Click(object sender, EventArgs e) {
            this.business.LeftPlayerScore++;
            this.business.Vinsert_SetScore();
            this.business.Vstage_SetScore();
        }

        protected virtual void numericUpDownRightPlayerScore_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerScore = (int)this.numericUpDownRightPlayerScore.Value; }
        protected virtual void buttonRightPlayerAddScoreHot_01_Click(object sender, EventArgs e) {
            this.business.RightPlayerScore++;
            this.business.Vinsert_SetScore();
            this.business.Vstage_SetScore();
        }

        private void checkBoxFlipPlayers_CheckedChanged(object sender, EventArgs e) { this.business.FlipPlayers = this.checkBoxFlipPlayers.Checked; }

        private void buttonReleaseBuzzer_Click(object sender, EventArgs e) { this.business.ReleaseBuzzer(); }
        private void buttonLockBuzzer_Click(object sender, EventArgs e) { this.business.LockBuzzer(); }

        private void buttonSetBuzzerTop_Click(object sender, EventArgs e) { this.business.SetActiveBuzzer(ActiveBuzzerStates.Top); }
        private void buttonSetBuzzerLeft_Click(object sender, EventArgs e) { this.business.SetActiveBuzzer(ActiveBuzzerStates.Left); }
        private void buttonSetBuzzerRight_Click(object sender, EventArgs e) { this.business.SetActiveBuzzer(ActiveBuzzerStates.Right); }
        private void buttonSetBuzzerBottom_Click(object sender, EventArgs e) { this.business.SetActiveBuzzer(ActiveBuzzerStates.Bottom); }
        private void buttonNextBuzzer_Click(object sender, EventArgs e) { this.business.NextBuzzer(); }

        protected virtual void buttonVinsert_ScoreIn_Click(object sender, EventArgs e) { this.business.Vinsert_ScoreIn(); }
        protected virtual void buttonVinsert_SetScore_Click(object sender, EventArgs e) { this.business.Vinsert_SetScore(); }
        protected virtual void buttonVinsert_ScoreOut_Click(object sender, EventArgs e) { this.business.Vinsert_ScoreOut(); }

        protected virtual void buttonVstage_Init_Click(object sender, EventArgs e) { this.business.Vstage_Init(); }
        protected virtual void buttonVstage_SetScore_Click(object sender, EventArgs e) { this.business.Vstage_SetScore(); }

        private void buttonGame_Start_Click(object sender, EventArgs e) { this.business.StartGame(); }
        private void buttonGame_Stop_Click(object sender, EventArgs e) { this.business.StopGame(); }

        private void checkBoxStartBuzzerTop_CheckedChanged(object sender, EventArgs e) { if (this.checkBoxStartBuzzerTop.Checked) this.business.StartBuzzer = ActiveBuzzerStates.Top; }
        private void checkBoxStartBuzzerLeft_CheckedChanged(object sender, EventArgs e) { if (this.checkBoxStartBuzzerLeft.Checked) this.business.StartBuzzer = ActiveBuzzerStates.Left; }
        private void checkBoxStartBuzzerRight_CheckedChanged(object sender, EventArgs e) { if (this.checkBoxStartBuzzerRight.Checked) this.business.StartBuzzer = ActiveBuzzerStates.Right; }
        private void checkBoxStartBuzzerBottom_CheckedChanged(object sender, EventArgs e) { if (this.checkBoxStartBuzzerBottom.Checked) this.business.StartBuzzer = ActiveBuzzerStates.Bottom; }

        #endregion

    }

}
