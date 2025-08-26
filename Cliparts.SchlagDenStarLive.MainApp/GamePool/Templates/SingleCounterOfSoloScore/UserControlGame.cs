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

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.SingleCounterOfSoloScore {

    public partial class UserControlGame : _Base.Score.UserControlGame {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.numericUpDownCounter.Minimum = int.MinValue;
            this.numericUpDownCounter.Maximum = int.MaxValue;

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            this.business = business;
            this.textBoxIOUnitName.DataBindings.Clear();

            Binding bind;

            bind = new Binding("Text", this.business, "IOUnitName");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxIOUnitName.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "IOUnitStatus");
            bind.Format += this.bind_textBoxIOUnitName_BackColor;
            this.textBoxIOUnitName.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "Counter");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownCounter.DataBindings.Add(bind);

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

            this.textBoxIOUnitName.DataBindings.Clear();

            this.numericUpDownCounter.DataBindings.Clear();
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
            step.AddButton(this.buttonVinsert_CounterIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex);
            step.AddButton(this.buttonVinsert_SetCounter);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_CounterOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ScoreIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 4);
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

        #endregion


        #region Events.Incoming

        protected override void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.business_PropertyChanged(sender, e);
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                //if (e.PropertyName == "NameList") this.fillDataList();
                //else if (e.PropertyName == "SampleIncluded") this.fillDataList();
                //else if (e.PropertyName == "SelectedDataset") this.selectDataset(this.business.SelectedDataset);
                //else if (e.PropertyName == "SelectedDatasetIndex") this.selectDataList();
            }
        }

        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        protected virtual void numericUpDownCounter_ValueChanged(object sender, EventArgs e) { this.business.Counter = (int)this.numericUpDownCounter.Value; }
        protected virtual void buttonAddCounterHot_01_Click(object sender, EventArgs e) {
            this.business.Counter++;
            this.business.Vinsert_SetCounter();
        }
        private void buttonSubtractCounterHot_01_Click(object sender, EventArgs e) {
            this.business.Counter--;
            this.business.Vinsert_SetCounter();
        }

        protected void buttonReleaseBuzzer_Click(object sender, EventArgs e) { if (this.business is Business) this.business.ReleaseBuzzer(); }
        protected void buttonLockBuzzer_Click(object sender, EventArgs e) { if (this.business is Business) this.business.LockBuzzer(); }

        private void buttonVinsert_CounterIn_Click(object sender, EventArgs e) { this.business.Vinsert_CounterIn(); }
        private void buttonVinsert_SetCounter_Click(object sender, EventArgs e) { this.business.Vinsert_SetCounter(); }
        private void buttonVinsert_CounterOut_Click(object sender, EventArgs e) { this.business.Vinsert_CounterOut(); }

        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }


        #endregion

    }
}
