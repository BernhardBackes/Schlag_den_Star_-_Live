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

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.SpeedTypingCounterToBeat
{

    public partial class UserControlGame : _Base.Timer.UserControlGame {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.numericUpDownCounterValue.Minimum = int.MinValue;
            this.numericUpDownCounterValue.Maximum = int.MaxValue;

            this.numericUpDownToBeatValue.Minimum = int.MinValue;
            this.numericUpDownToBeatValue.Maximum = int.MaxValue;

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Text", this.business, "IOUnitName");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxIOUnitName.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "IOUnitStatus");
            bind.Format += this.bind_textBoxIOUnitName_BackColor;
            this.textBoxIOUnitName.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "CounterValue");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownCounterValue.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "Text");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxText.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "CounterTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelCounterTime.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "ToBeatValue");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownToBeatValue.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "ToBeatTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelToBeatTime.DataBindings.Add(bind);

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

            this.textBoxIOUnitName.DataBindings.Clear();

            this.textBoxText.DataBindings.Clear();

            this.numericUpDownCounterValue.DataBindings.Clear();
            this.labelCounterTime.DataBindings.Clear();
            this.numericUpDownToBeatValue.DataBindings.Clear();
            this.labelToBeatTime.DataBindings.Clear();
        }

        protected override void buildStepList() {

            int index = 0;
            stepAction step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_LoadScene);
            step.AddButton(this.buttonVfullscreen_LoadScene);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, this.showFullscreenTimer);
            step.AddButton(this.buttonVgraphic_Clear);
            step.AddButton(this.buttonVstage_Clear);
            step.AddButton(this.buttonGame_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_ShowGame);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_CounterIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimerIn);
            step.AddButton(this.buttonVfullscreen_ResetTimer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_ReleaseBuzzer);
            step.AddButton(this.buttonVinsert_StartTimer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ContentIn);
            step.AddButton(this.buttonVfullscreen_ContentIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex);
            step.AddButton(this.buttonGame_NextItem);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_StopTimer);
            step.AddButton(this.buttonGame_LockBuzzer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimerOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ContentOut);
            step.AddButton(this.buttonVfullscreen_ContentOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 7);
            step.AddButton(this.buttonGame_NextPlayer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_CounterOut);
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
            step.AddButton(this.buttonVfullscreen_UnloadScene);
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

        private void bind_textBoxIOUnitName_BackColor(object sender, ConvertEventArgs e)
        {
            switch ((BuzzerUnitStates)e.Value)
            {
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

        private void fillDataList() {
            this.listBoxDataList.BeginUpdate();
            this.listBoxDataList.Items.Clear();
            int id = 1;
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
                index < this.listBoxDataList.Items.Count)
                this.listBoxDataList.SelectedIndex = index;
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
            if (this.InvokeRequired)
                this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "NameList")
                    this.fillDataList();
                else if (e.PropertyName == "SampleIncluded")
                    this.fillDataList();
                else if (e.PropertyName == "SelectedDataset")
                    this.selectDataset(this.business.SelectedDataset);
                else if (e.PropertyName == "SelectedDatasetIndex")
                    this.selectDataList();
            }
        }

        void selectedDataset_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired)
                this.Invoke((MethodInvoker)(() => this.selectedDataset_PropertyChanged(sender, e)));
            else {
            }
        }

        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void buttonReleaseBuzzer_Click(object sender, EventArgs e) { if (this.business is Business) this.business.ReleaseBuzzer(); }
        private void buttonLockBuzzer_Click(object sender, EventArgs e) { if (this.business is Business) this.business.LockBuzzer(); }

        private void textBoxText_TextChanged(object sender, EventArgs e) { if (this.business is Business) this.business.Text = this.textBoxText.Text; }

        private void numericUpDownCounterValue_ValueChanged(object sender, EventArgs e) { this.business.CounterValue = (int)this.numericUpDownCounterValue.Value; }
        private void buttonLeftPlayerAddCounterValueHot_01_Click(object sender, EventArgs e) {
            this.business.CounterValue++;
            this.business.Vinsert_SetCounter();
        }

        private void numericUpDownToBeatValue_ValueChanged(object sender, EventArgs e) { this.business.ToBeatValue = (int)this.numericUpDownToBeatValue.Value; }

        private void listBoxDataList_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectDataset(this.listBoxDataList.SelectedIndex); }

        private void buttonPlayGoodSound_Click(object sender, EventArgs e) { this.business.PlayGoodSound(); }
        private void buttonPlayBadSound_Click(object sender, EventArgs e) { this.business.PlayBadSound(); }

        private void buttonVinsert_CounterIn_Click(object sender, EventArgs e) { if (this.business is Business) this.business.Vinsert_CounterIn(); }
        private void buttonVinsert_CounterOut_Click(object sender, EventArgs e) { if (this.business is Business) this.business.Vinsert_CounterOut(); }
        private void buttonVinsert_ContentIn_Click(object sender, EventArgs e) { if (this.business is Business) this.business.Vinsert_ContentIn(); }
        private void buttonVinsert_ContentOut_Click(object sender, EventArgs e) { if (this.business is Business) this.business.Vinsert_ContentOut(); }

        private void buttonVfullscreen_ContentIn_Click(object sender, EventArgs e) { if (this.business is Business) this.business.Vfullscreen_ContentIn(); }
        private void buttonVfullscreen_ContentOut_Click(object sender, EventArgs e) { if (this.business is Business) this.business.Vfullscreen_ContentOut(); }

        private void buttonGame_ReleaseBuzzer_Click(object sender, EventArgs e) { if (this.business is Business) this.business.ReleaseBuzzer(); }
        private void buttonGame_NextItem_Click(object sender, EventArgs e) { if (this.business is Business) this.business.Next(); }
        private void buttonGame_LockBuzzer_Click(object sender, EventArgs e) { if (this.business is Business) this.business.LockBuzzer(); }
        private void buttonGame_NextPlayer_Click(object sender, EventArgs e) { if (this.business is Business) this.business.NextPlayer(); }

        #endregion

    }

}
