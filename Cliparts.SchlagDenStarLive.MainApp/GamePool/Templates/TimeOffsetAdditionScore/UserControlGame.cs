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

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimeOffsetAdditionScore {

    public partial class UserControlGame : _Base.Score.UserControlGame {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;

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

            Binding bind;

            bind = new Binding("Text", this.business, "TimerCurrentTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToStopwatchTimeText((double)e.Value, false, true); };
            this.labelTimerCurrentTime.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "OffsetSum");
            bind.Format += (s, e) => { e.Value = (double)e.Value; };
            this.numericUpDownOffsetSum.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "IOUnitName");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxIOUnitName.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "IOUnitStatus");
            bind.Format += this.bind_textBoxIOUnitName_BackColor;
            this.textBoxIOUnitName.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerTimeToBeat");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxTimeToBeat.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "PoleCounter");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownPoleCounter.DataBindings.Add(bind);

            this.labelGameClass.Text = this.business.ClassInfo;

            this.fillDataList();
            this.selectDataset(this.business.SelectedDataset);
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

            this.labelTimerCurrentTime.DataBindings.Clear();
            this.numericUpDownOffsetSum.DataBindings.Clear();
            this.textBoxIOUnitName.DataBindings.Clear();
            this.textBoxTimeToBeat.DataBindings.Clear();
            this.numericUpDownPoleCounter.DataBindings.Clear();
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
            step.AddButton(this.buttonVfullscreen_ShowFreetext);
            step.AddButton(this.buttonVstage_Init);
            step.AddButton(this.buttonGame_SetDMX);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimerIn);
            step.AddButton(this.buttonGame_ReleaseBuzzer);
            step.AddButton(this.buttonGame_Start);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_ClearText);
            step.AddButton(this.buttonGame_Stop);
            this.stepList.Add(step);
            
            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_NextPlayer);
            step.AddButton(this.buttonVinsert_ResetTimer);
            step.AddButton(this.buttonVinsert_ToBeatIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_ReleaseBuzzer_1);
            step.AddButton(this.buttonGame_Start_1);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_Stop_1);
            step.AddButton(this.buttonVinsert_TimerOut);
            step.AddButton(this.buttonVfullscreen_ClearText_1);
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
            step = new stepAction(index, (stepIndex) => stepIndex - 7);
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

        private void fillDataList() {
            this.listBoxDataList.BeginUpdate();
            this.listBoxDataList.Items.Clear();
            int id = 1;
            if (this.business.SampleIncluded) id = 0;
            foreach (string item in this.business.NameList) {
                this.listBoxDataList.Items.Add(string.Format("{0}: {1}", id.ToString("00"), item));
                id++;
            }
            this.listBoxDataList.EndUpdate();

            this.listBoxDataList.Enabled = this.listBoxDataList.Items.Count > 0;
            Helper.setControlBackColor(this.listBoxDataList);

            this.selectDataList();
        }

        private void selectDataList() {
            int index = this.business.GetDatasetIndex(this.selectedDataset);
            if (index >= 0 &&
                index < this.listBoxDataList.Items.Count) this.listBoxDataList.SelectedIndex = index;
        }

        private void selectDataset(
            DatasetContent selectedDataset) {
            if (this.selectedDataset != selectedDataset) {
                //Dispose...
                if (this.selectedDataset is DatasetContent) {
                    this.selectedDataset.PropertyChanged -= this.selectedDataset_PropertyChanged;
                }
                this.selectedDataset = selectedDataset;
                //Pose...
                if (this.selectedDataset is DatasetContent) {
                    this.selectedDataset.PropertyChanged += this.selectedDataset_PropertyChanged;
                }
            }

            this.selectDataList();
        }

        #endregion


        #region Events.Incoming

        protected override void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.business_PropertyChanged(sender, e);
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "NameList") this.fillDataList();
                else if (e.PropertyName == "SampleIncluded") this.fillDataList();
                else if (e.PropertyName == "SelectedDataset") this.selectDataset(this.business.SelectedDataset);
                else if (e.PropertyName == "SelectedDatasetIndex") this.selectDataList();
            }
        }

        void selectedDataset_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.selectedDataset_PropertyChanged(sender, e)));
            else {
            }
        }
        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void buttonTimerIn_Click(object sender, EventArgs e) { this.business.Vinsert_TimerIn(); }
        private void buttonTimerOut_Click(object sender, EventArgs e) { this.business.Vinsert_TimerOut(); }
        private void buttonTimerStart_Click(object sender, EventArgs e) { this.business.Vinsert_StartTimer(); }
        private void buttonTimerStop_Click(object sender, EventArgs e) { this.business.Vinsert_StopTimer(); }
        private void buttonTimerReset_Click(object sender, EventArgs e) { this.business.Vinsert_ResetTimer(); }
        private void buttonTimerShowOffset_Click(object sender, EventArgs e) { this.business.Vinsert_ShowOffset(Convert.ToSingle(this.numericUpDownTimerOffset.Value)); }
        private void numericUpDownOffsetSum_ValueChanged(object sender, EventArgs e) { this.business.OffsetSum = Convert.ToDouble(this.numericUpDownOffsetSum.Value); }

        private void buttonReleaseBuzzer_Click(object sender, EventArgs e) { this.business.ReleaseBuzzer(); }
        private void buttonLockBuzzer_Click(object sender, EventArgs e) { this.business.LockBuzzer(); }
        private void buttonBuzzer_Click(object sender, EventArgs e) { this.business.DoBuzzer(); }

        private void numericUpDownPoleCounter_ValueChanged(object sender, EventArgs e) { this.business.PoleCounter = (int)this.numericUpDownPoleCounter.Value; }

        private void buttonSetTimeToBeat_Click(object sender, EventArgs e) { this.business.SetTimeToBeat(this.textBoxTimeToBeat.Text); }

        private void listBoxDataList_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectDataset(this.listBoxDataList.SelectedIndex); }

        private void buttonVinsert_TimerIn_Click(object sender, EventArgs e) { this.business.Vinsert_TimerIn(); }
        private void buttonVinsert_StartTimer_Click(object sender, EventArgs e) { this.business.Vinsert_StartTimer(); }
        private void buttonVinsert_ToBeatIn_Click(object sender, EventArgs e) { this.business.Vinsert_TimeToBeatIn(); }
        private void buttonVinsert_ResetTimer_Click(object sender, EventArgs e) { this.business.Vinsert_ResetTimer(); }
        private void buttonVinsert_StartTimer_1_Click(object sender, EventArgs e) { this.business.Vinsert_StartTimer(); }
        private void buttonVinsert_TimerOut_Click(object sender, EventArgs e) { this.business.Vinsert_TimerOut(); }

        private void buttonVfullscreen_ClearText_Click(object sender, EventArgs e) { this.business.Vfullscreen_ClearText(); }
        private void buttonVfullscreen_ClearText_1_Click(object sender, EventArgs e) { this.business.Vfullscreen_ClearText(); }

        private void buttonGame_SetDMX_Click(object sender, EventArgs e) { this.business.SetDMXValues(); }
        private void buttonGame_ReleaseBuzzer_Click(object sender, EventArgs e) { this.business.ReleaseBuzzer(); }
        private void buttonGame_Start_Click(object sender, EventArgs e) { this.business.StartGame(); }
        private void buttonGame_Stop_Click(object sender, EventArgs e) { this.business.StopGame(); }
        private void buttonGame_NextPlayer_Click(object sender, EventArgs e) { this.business.NextPlayer(); }
        private void buttonGame_ReleaseBuzzer_1_Click(object sender, EventArgs e) { this.business.ReleaseBuzzer(); }
        private void buttonGame_Start_1_Click(object sender, EventArgs e) { this.business.StartGame(); }
        private void buttonGame_Stop_1_Click(object sender, EventArgs e) { this.business.StopGame(); }
        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }

        #endregion
    }

}
