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

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TwoTimersStartStopContinue {

    public partial class UserControlGame : _Base.UserControlGame {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.numericUpDownLAPCounter.Minimum = int.MinValue;
            this.numericUpDownLAPCounter.Maximum = int.MaxValue;

            this.numericUpDownLeftPlayerTime.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerTime.Maximum = int.MaxValue;

            this.numericUpDownLeftPlayerPreviousTime.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerPreviousTime.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerTime.Minimum = int.MinValue;
            this.numericUpDownRightPlayerTime.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerPreviousTime.Minimum = int.MinValue;
            this.numericUpDownRightPlayerPreviousTime.Maximum = int.MaxValue;

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Text", this.business, "TimelineName");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.textBoxTimelineName.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "TimelineIsConnected");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? SystemColors.Control : Constants.ColorDisabled; };
            this.textBoxTimelineName.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LAPCounter");
            bind.Format += (s, e) => { e.Value = Convert.ToDecimal(e.Value); };
            this.numericUpDownLAPCounter.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerTime");
            bind.Format += (s, e) => { e.Value = Convert.ToDecimal((double)e.Value); };
            this.numericUpDownLeftPlayerTime.DataBindings.Add(bind);
            bind = new Binding("Text", this.business, "LeftPlayerTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToStopwatchTimeText((double)e.Value, true, true); };
            this.labelLeftPlayerTime.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerPreviousTime");
            bind.Format += (s, e) => { e.Value = Convert.ToDecimal((double)e.Value); };
            this.numericUpDownLeftPlayerPreviousTime.DataBindings.Add(bind);
            bind = new Binding("Text", this.business, "LeftPlayerPreviousTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToStopwatchTimeText((double)e.Value, true, true); };
            this.labelLeftPlayerPreviousTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "LeftPlayerLAP");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToStopwatchTimeText((double)e.Value, true, true); };
            this.labelLeftPlayerLAP.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerTime");
            bind.Format += (s, e) => { e.Value = Convert.ToDecimal((double)e.Value); };
            this.numericUpDownRightPlayerTime.DataBindings.Add(bind);
            bind = new Binding("Text", this.business, "RightPlayerTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToStopwatchTimeText((double)e.Value, true, true); };
            this.labelRightPlayerTime.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerPreviousTime");
            bind.Format += (s, e) => { e.Value = Convert.ToDecimal((double)e.Value); };
            this.numericUpDownRightPlayerPreviousTime.DataBindings.Add(bind);
            bind = new Binding("Text", this.business, "RightPlayerPreviousTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToStopwatchTimeText((double)e.Value, true, true); };
            this.labelRightPlayerPreviousTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerLAP");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToStopwatchTimeText((double)e.Value, true, true); };
            this.labelRightPlayerLAP.DataBindings.Add(bind);

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

            this.textBoxTimelineName.DataBindings.Clear();

            this.numericUpDownLAPCounter.DataBindings.Clear();

            this.numericUpDownLeftPlayerTime.DataBindings.Clear();
            this.labelLeftPlayerTime.DataBindings.Clear();
            this.numericUpDownLeftPlayerPreviousTime.DataBindings.Clear();
            this.labelLeftPlayerPreviousTime.DataBindings.Clear();
            this.labelLeftPlayerLAP.DataBindings.Clear();

            this.numericUpDownRightPlayerTime.DataBindings.Clear();
            this.labelRightPlayerTime.DataBindings.Clear();
            this.numericUpDownRightPlayerPreviousTime.DataBindings.Clear();
            this.labelRightPlayerPreviousTime.DataBindings.Clear();
            this.labelRightPlayerLAP.DataBindings.Clear();
        }

        protected override void buildStepList() {

            int index = 0;
            stepAction step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_LoadScene);
            step.AddButton(this.buttonVfullscreen_LoadScene);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVgraphic_Clear);
            step.AddButton(this.buttonVstage_Clear);
            step.AddButton(this.buttonGame_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimerIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_StartBothTimer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_LAPsOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 2);
            step.AddButton(this.buttonGame_Next);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex -1);
            step.AddButton(this.buttonVinsert_TimerOut);
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
                    e.Value = Constants.ColorEnabled;
                    break;
                case BuzzerUnitStates.EventMode:
                    e.Value = Constants.ColorBuzzered;
                    break;
                default:
                    break;
            }
        }

        #endregion


        #region Events.Incoming

        protected override void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.business_PropertyChanged(sender, e);
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
            }

        }

        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void numericUpDownLeftPlayerTime_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerTime = (double)this.numericUpDownLeftPlayerTime.Value; }
        private void buttonLeftPlayerSetTimer_Click(object sender, EventArgs e) { this.business.Vinsert_SetLeftPlayerTimer(); }
        private void buttonLeftPlayerStartTimer_Click(object sender, EventArgs e) { this.business.Vinsert_StartLeftPlayerTimer(); }
        private void buttonLeftPlayerStopTimer_Click(object sender, EventArgs e) { this.business.Vinsert_StopLeftPlayerTimer(); }
        private void numericUpDownLeftPlayerPreviousTime_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerPreviousTime = (double)this.numericUpDownLeftPlayerPreviousTime.Value; }
        private void buttonLeftPlayerLAPIn_Click(object sender, EventArgs e) { this.business.Vinsert_LeftPlayerTimerLAPIn(); }
        private void buttonLeftPlayerLAPOut_Click(object sender, EventArgs e) { this.business.Vinsert_LeftPlayerTimerLAPOut(); }
        private void buttonLeftPlayerPassFinishline_Click(object sender, EventArgs e) { this.business.PassFinishLine(Content.Gameboard.PlayerSelection.LeftPlayer); }

        private void numericUpDownRightPlayerTime_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerTime = (double)this.numericUpDownRightPlayerTime.Value; }
        private void buttonRightPlayerSetTimer_Click(object sender, EventArgs e) { this.business.Vinsert_SetRightPlayerTimer(); }
        private void buttonRightPlayerStartTimer_Click(object sender, EventArgs e) { this.business.Vinsert_StartRightPlayerTimer(); }
        private void buttonRightPlayerStopTimer_Click(object sender, EventArgs e) { this.business.Vinsert_StopRightPlayerTimer(); }
        private void numericUpDownRightPlayerPreviousTime_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerPreviousTime = (double)this.numericUpDownRightPlayerPreviousTime.Value; }
        private void buttonRightPlayerLAPIn_Click(object sender, EventArgs e) { this.business.Vinsert_RightPlayerTimerLAPIn(); }
        private void buttonRightPlayerLAPOut_Click(object sender, EventArgs e) { this.business.Vinsert_RightPlayerTimerLAPOut(); }
        private void buttonRightPlayerPassFinishline_Click(object sender, EventArgs e) { this.business.PassFinishLine(Content.Gameboard.PlayerSelection.RightPlayer); }

        private void numericUpDownLAPCounter_ValueChanged(object sender, EventArgs e) { this.business.LAPCounter = (int)this.numericUpDownLAPCounter.Value; }

        private void buttonVinsert_TimerIn_Click(object sender, EventArgs e) { this.business.Vinsert_TimerIn(); }
        private void buttonVinsert_StartBothTimer_Click(object sender, EventArgs e) { this.business.Vinsert_StartBothTimer(); }
        private void buttonVinsert_LAPsOut_Click(object sender, EventArgs e) {
            this.business.Vinsert_LeftPlayerTimerLAPOut();
            this.business.Vinsert_RightPlayerTimerLAPOut();
        }
        private void buttonVinsert_TimerOut_Click(object sender, EventArgs e) { this.business.Vinsert_TimerOut(); }

        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }

        #endregion

    }
}
