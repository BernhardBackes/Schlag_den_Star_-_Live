using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.ScrabbleList {

    public partial class UserControlGame : _Base.Sets.UserControlGame {

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
            this.business.TimerAlarm1Fired += this.business_Alarm1Fired;
            this.business.TimerAlarm2Fired += this.business_Alarm2Fired;

            this.userControlLetterLeft_00.Pose(this.business.LeftPlayerContent.Letters[0]);
            this.userControlLetterLeft_01.Pose(this.business.LeftPlayerContent.Letters[1]);
            this.userControlLetterLeft_02.Pose(this.business.LeftPlayerContent.Letters[2]);
            this.userControlLetterLeft_03.Pose(this.business.LeftPlayerContent.Letters[3]);
            this.userControlLetterLeft_04.Pose(this.business.LeftPlayerContent.Letters[4]);
            this.userControlLetterLeft_05.Pose(this.business.LeftPlayerContent.Letters[5]);
            this.userControlLetterLeft_06.Pose(this.business.LeftPlayerContent.Letters[6]);
            this.userControlLetterLeft_07.Pose(this.business.LeftPlayerContent.Letters[7]);
            this.userControlLetterLeft_08.Pose(this.business.LeftPlayerContent.Letters[8]);

            this.userControlLetterRight_00.Pose(this.business.RightPlayerContent.Letters[0]);
            this.userControlLetterRight_01.Pose(this.business.RightPlayerContent.Letters[1]);
            this.userControlLetterRight_02.Pose(this.business.RightPlayerContent.Letters[2]);
            this.userControlLetterRight_03.Pose(this.business.RightPlayerContent.Letters[3]);
            this.userControlLetterRight_04.Pose(this.business.RightPlayerContent.Letters[4]);
            this.userControlLetterRight_05.Pose(this.business.RightPlayerContent.Letters[5]);
            this.userControlLetterRight_06.Pose(this.business.RightPlayerContent.Letters[6]);
            this.userControlLetterRight_07.Pose(this.business.RightPlayerContent.Letters[7]);
            this.userControlLetterRight_08.Pose(this.business.RightPlayerContent.Letters[8]);

            Binding bind;

            bind = new Binding("Text", this.business, "TimerCurrentTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerCurrentTime.DataBindings.Add(bind);

            bind = new Binding("ForeColor", this.business, "TimerIsRunning");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorEnabled : this.ForeColor; };
            this.labelTimerCurrentTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerStartTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerStartTimeText.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerExtraTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerExtraTimeText.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "RunExtraTime");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxTimerRunExtraTime.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerStopTime");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.labelTimerStopTimeText.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerAlarmTime1");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.userControlRecTriggerTimerAlarmTime1.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "TimerAlarmTime2");
            bind.Format += (s, e) => { e.Value = Helper.convertDoubleToClockTimeText((int)e.Value, true); };
            this.userControlRecTriggerTimerAlarmTime2.DataBindings.Add(bind);

            bind = new Binding("Enabled", this.business, "ShowFullscreenTimer");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.buttonVfullscreen_ShowTimer.DataBindings.Add(bind);

            bind = new Binding("Enabled", this.business, "ShowFullscreenTimer");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.buttonVfullscreen_StartTimer.DataBindings.Add(bind);

            bind = new Binding("Enabled", this.business, "ShowFullscreenTimer");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.buttonVfullscreen_StopTimer.DataBindings.Add(bind);

            bind = new Binding("Enabled", this.business, "ShowFullscreenTimer");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.buttonVfullscreen_ResetTimer.DataBindings.Add(bind);

            bind = new Binding("Enabled", this.business, "ShowFullscreenTimer");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.buttonVfullscreen_ResetTimer_1.DataBindings.Add(bind);

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

            if (this.business is Business) {
                this.business.Dispose();
                this.business.TimerAlarm1Fired -= this.business_Alarm1Fired;
                this.business.TimerAlarm2Fired -= this.business_Alarm2Fired;
            }

            this.labelTimerCurrentTime.DataBindings.Clear();
            this.labelTimerStartTimeText.DataBindings.Clear();
            this.labelTimerExtraTimeText.DataBindings.Clear();
            this.checkBoxTimerRunExtraTime.DataBindings.Clear();
            this.labelTimerStopTimeText.DataBindings.Clear();
            this.userControlRecTriggerTimerAlarmTime1.DataBindings.Clear();
            this.userControlRecTriggerTimerAlarmTime2.DataBindings.Clear();

            this.buttonVfullscreen_ShowTimer.DataBindings.Clear();
            this.buttonVfullscreen_StartTimer.DataBindings.Clear();
            this.buttonVfullscreen_StopTimer.DataBindings.Clear();
            this.buttonVfullscreen_ResetTimer.DataBindings.Clear();
            this.buttonVfullscreen_ResetTimer_1.DataBindings.Clear();

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
            step.AddButton(this.buttonVinsert_SetsIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_GameIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimerIn);
            step.AddButton(this.buttonVfullscreen_ResetTimer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_StartTimer);
            step.AddButton(this.buttonVinsert_StartTimer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_StopTimer);
            step.AddButton(this.buttonVinsert_StopTimer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimerOut);
            step.AddButton(this.buttonVfullscreen_ResetTimer_1);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_UpdateGame);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 5);
            step.AddButton(this.buttonVinsert_SetSets);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_GameOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 8);
            step.AddButton(this.buttonGame_Next);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex +1);
            step.AddButton(this.buttonVinsert_SetsOut);
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

        void business_Alarm1Fired(object sender, EventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_Alarm1Fired(sender, e)));
            else { this.userControlRecTriggerTimerAlarmTime1.StartTrigger(); }
        }

        void business_Alarm2Fired(object sender, EventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_Alarm2Fired(sender, e)));
            else { this.userControlRecTriggerTimerAlarmTime2.StartTrigger(); }
        }

        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        protected override void UserControlGame_BackColorChanged(object sender, EventArgs e) {
            base.UserControlGame_BackColorChanged(sender, e);
            foreach (Control control in this.Controls) {
                if (control is UserControlGamePoolScrabbleListLetter) control.BackColor = this.BackColor;
            }
        }

        private void listBoxDataList_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectDataset(this.listBoxDataList.SelectedIndex); }

        protected virtual void buttonTimerIn_Click(object sender, EventArgs e) { this.business.Vinsert_TimerIn(); }
        protected virtual void buttonTimerOut_Click(object sender, EventArgs e) { this.business.Vinsert_TimerOut(); }
        protected virtual void buttonTimerStart_Click(object sender, EventArgs e) { this.business.Vinsert_StartTimer(); }
        protected virtual void buttonTimerStop_Click(object sender, EventArgs e) { this.business.Vinsert_StopTimer(); }
        protected virtual void buttonTimerContinue_Click(object sender, EventArgs e) { this.business.Vinsert_ContinueTimer(); }
        protected virtual void buttonTimerReset_Click(object sender, EventArgs e) { this.business.Vinsert_ResetTimer(); }
        protected virtual void checkBoxTimerRunExtraTime_CheckedChanged(object sender, EventArgs e) { this.business.RunExtraTime = this.checkBoxTimerRunExtraTime.Checked; }

        private void buttonVinsert_GameIn_Click(object sender, EventArgs e) { this.business.Vinsert_GameIn(); }
        protected virtual void buttonVinsert_TimerIn_Click(object sender, EventArgs e) { this.business.Vinsert_TimerIn(); }
        protected virtual void buttonVinsert_StartTimer_Click(object sender, EventArgs e) { this.business.Vinsert_StartTimer(); }
        protected virtual void buttonVinsert_StopTimer_Click(object sender, EventArgs e) { this.business.Vinsert_StopTimer(); }
        protected virtual void buttonVinsert_TimerOut_Click(object sender, EventArgs e) { this.business.Vinsert_TimerOut(); }
        private void buttonVinsert_UpdateGame_Click(object sender, EventArgs e) { this.business.Vinsert_UpdateGame(); }
        private void buttonVinsert_SetSets_Click(object sender, EventArgs e) { this.business.Vinsert_SetSets(); }
        private void buttonVinsert_GameOut_Click(object sender, EventArgs e) { this.business.Vinsert_GameOut(); }
        private void buttonVinsert_SetsOut_Click(object sender, EventArgs e) { this.business.Vinsert_SetsOut(); }

        protected virtual void buttonVfullscreen_StartTimer_Click(object sender, EventArgs e) { this.business.Vfullscreen_StartTimer(); }
        protected virtual void buttonVfullscreen_StopTimer_Click(object sender, EventArgs e) { this.business.Vfullscreen_StopTimer(); }
        protected virtual void buttonVfullscreen_ResetTimer_Click(object sender, EventArgs e) { this.business.Vfullscreen_ResetTimer(); }

        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }

        #endregion

    }

}
