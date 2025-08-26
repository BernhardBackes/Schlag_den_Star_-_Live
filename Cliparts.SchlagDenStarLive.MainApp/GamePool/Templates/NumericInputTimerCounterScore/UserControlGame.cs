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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.NumericInputTimerCounterScore
{

    public partial class UserControlGame : _Base.Score.UserControlGame
    {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;

        #endregion


        #region Funktionen

        public UserControlGame()
        {
            InitializeComponent();

            this.numericUpDownLeftPlayerValue.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerValue.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerValue.Minimum = int.MinValue;
            this.numericUpDownRightPlayerValue.Maximum = int.MaxValue;

            this.numericUpDownLeftPlayerCounter.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerCounter.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerCounter.Minimum = int.MinValue;
            this.numericUpDownRightPlayerCounter.Maximum = int.MaxValue;

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business)
        {
            base.Pose(business);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;
            this.business.TimerAlarm1Fired += this.business_Alarm1Fired;
            this.business.TimerAlarm2Fired += this.business_Alarm2Fired;

            Binding bind;

            bind = new Binding("Value", this.business, "LeftPlayerValue");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerValue.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "LeftPlayerBestEstimation");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxLeftPlayerBestEstimation.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "LeftPlayerHighestCounter");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxLeftPlayerHighestCounter.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerValue");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerValue.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "RightPlayerBestEstimation");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxRightPlayerBestEstimation.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "RightPlayerHighestCounter");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxRightPlayerHighestCounter.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "IOUnitName");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxIOUnitName.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "IOUnitStatus");
            bind.Format += this.bind_textBoxIOUnitName_BackColor;
            this.textBoxIOUnitName.DataBindings.Add(bind);

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

            bind = new Binding("Text", this.business, "LeftPlayerName");
            this.textBoxLeftPlayerName_1.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerName");
            this.textBoxRightPlayerName_1.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerCounter");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerCounter.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerCounter");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerCounter.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "FemalesSelected");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorSelected : Constants.ColorDropped; };
            this.pictureBoxFemaleSelected.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "FemalesSelected");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorDropped : Constants.ColorSelected; };
            this.pictureBoxMaleSelected.DataBindings.Add(bind);

            this.setSelectedPlayer();

            this.fillDataList();
            this.selectDataset(this.business.SelectedDataset);

            this.fillItemLists();

            this.labelGameClass.Text = this.business.ClassInfo;
        }

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);

            this.business.PropertyChanged -= this.business_PropertyChanged;
            this.business.TimerAlarm1Fired -= this.business_Alarm1Fired;
            this.business.TimerAlarm2Fired -= this.business_Alarm2Fired;

            this.numericUpDownLeftPlayerValue.DataBindings.Clear();
            this.checkBoxLeftPlayerBestEstimation.DataBindings.Clear();
            this.checkBoxLeftPlayerHighestCounter.DataBindings.Clear();
            this.numericUpDownRightPlayerValue.DataBindings.Clear();
            this.checkBoxLeftPlayerBestEstimation.DataBindings.Clear();
            this.checkBoxLeftPlayerHighestCounter.DataBindings.Clear();

            this.textBoxIOUnitName.DataBindings.Clear();

            this.labelTimerCurrentTime.DataBindings.Clear();
            this.labelTimerStartTimeText.DataBindings.Clear();
            this.labelTimerExtraTimeText.DataBindings.Clear();
            this.checkBoxTimerRunExtraTime.DataBindings.Clear();
            this.labelTimerStopTimeText.DataBindings.Clear();
            this.userControlRecTriggerTimerAlarmTime1.DataBindings.Clear();
            this.userControlRecTriggerTimerAlarmTime2.DataBindings.Clear();

            this.textBoxLeftPlayerName_1.DataBindings.Clear();
            this.textBoxRightPlayerName_1.DataBindings.Clear();

            this.numericUpDownLeftPlayerCounter.DataBindings.Clear();
            this.numericUpDownRightPlayerCounter.DataBindings.Clear();

            this.pictureBoxFemaleSelected.DataBindings.Clear();
            this.pictureBoxMaleSelected.DataBindings.Clear();

        }

        protected override void buildStepList()
        {

            int index = 0;
            stepAction step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_LoadScene);
            step.AddButton(this.buttonVhost_LoadScene);
            step.AddButton(this.buttonVleftplayer_LoadScene);
            step.AddButton(this.buttonVrightplayer_LoadScene);
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
            step.AddButton(this.buttonVstage_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVstage_ContentIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVleftplayer_Unlock);
            step.AddButton(this.buttonVrightplayer_Unlock);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVhost_SetInput);
            step.AddButton(this.buttonVplayers_ContentOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_CounterIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimerIn);
            step.AddButton(this.buttonGame_ReleaseBuzzer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_StartTimer);
            step.AddButton(this.buttonVinsert_StartTimer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex);
            step.AddButton(this.buttonVinsert_SetCounter);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_StopTimer);
            step.AddButton(this.buttonVinsert_StopTimer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_LockBuzzer);
            step.AddButton(this.buttonVinsert_TimerOut);
            step.AddButton(this.buttonVfullscreen_ResetTimer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_SetResults);
            step.AddButton(this.buttonVinsert_ScoreIn);
            step.AddButton(this.buttonVhost_ShowResults);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_ResolveHighestCounter);
            step.AddButton(this.buttonVinsert_SetScore);
            step.AddButton(this.buttonVstage_SetScore);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVplayers_ShowInput);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_ResolveBestEstimation);
            step.AddButton(this.buttonVinsert_SetScore_01);
            step.AddButton(this.buttonVstage_SetScore_01);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_CounterOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 13);
            step.AddButton(this.buttonVinsert_ScoreOut);
            step.AddButton(this.buttonVstage_ContentOut);
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
            step.AddButton(this.buttonVhost_UnloadScene);
            step.AddButton(this.buttonVleftplayer_UnloadScene);
            step.AddButton(this.buttonVrightplayer_UnloadScene);
            step.AddButton(this.buttonGame_SetWinner);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_UpdateGameboard);
            this.stepList.Add(step);

            if (index < this.stepsMaxIndex) this.on_Error("buildStepList", "stepList to short");
            if (index > this.stepsMaxIndex) this.on_Error("buildStepList", "stepList to long");
        }

        public override void ParseKey(
            Keys keycode)
        {
            base.ParseKey(keycode);
            if (this.keyControl)
            {
                switch (keycode)
                {
                    case Keys.Q:
                        this.business.DoCounter(Content.Gameboard.PlayerSelection.LeftPlayer);
                        break;
                    case Keys.P:
                        this.business.DoCounter(Content.Gameboard.PlayerSelection.RightPlayer);
                        break;
                    case Keys.Left:
                        this.business.DoCounter(Content.Gameboard.PlayerSelection.LeftPlayer);
                        break;
                    case Keys.Right:
                        this.business.DoCounter(Content.Gameboard.PlayerSelection.RightPlayer);
                        break;
                }
            }
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
                    e.Value = Constants.ColorBuzzered;
                    break;
                case BuzzerUnitStates.EventMode:
                    e.Value = Constants.ColorEnabled;
                    break;
                default:
                    break;
            }
        }

        private void setSelectedPlayer()
        {
            switch (this.business.SelectedPlayer)
            {
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.LeftPlayer:
                    this.buttonLeftPlayerSelected.BackColor = Constants.ColorSelected;
                    this.buttonRightPlayerSelected.UseVisualStyleBackColor = true;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.RightPlayer:
                    this.buttonLeftPlayerSelected.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerSelected.BackColor = Constants.ColorSelected;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.NotSelected:
                default:
                    this.buttonLeftPlayerSelected.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerSelected.UseVisualStyleBackColor = true;
                    break;
            }
        }

        private void fillDataList()
        {
            this.listBoxDataList.BeginUpdate();
            this.listBoxDataList.Items.Clear();
            int id = 1;
            foreach (string item in this.business.NameList)
            {
                this.listBoxDataList.Items.Add(string.Format("{0}: {1}", id.ToString("00"), item));
                id++;
            }
            this.listBoxDataList.EndUpdate();

            this.listBoxDataList.Enabled = this.listBoxDataList.Items.Count > 0;
            Helper.setControlBackColor(this.listBoxDataList);

            this.selectDataList();
        }

        private void selectDataList()
        {
            int index = this.business.GetDatasetIndex(this.selectedDataset);
            if (index >= 0 &&
                index < this.listBoxDataList.Items.Count) this.listBoxDataList.SelectedIndex = index;
        }

        private void selectDataset(
            DatasetContent selectedDataset)
        {
            if (this.selectedDataset != selectedDataset)
            {
                //Dispose...
                if (this.selectedDataset is DatasetContent)
                {
                    //this.selectedDataset.PropertyChanged -= this.selectedDataset_PropertyChanged;
                }
                this.selectedDataset = selectedDataset;
                //Pose...
                if (this.selectedDataset is DatasetContent)
                {
                    //this.selectedDataset.PropertyChanged += this.selectedDataset_PropertyChanged;
                }
            }

            this.selectDataList();
        }

        private void fillItemLists()
        {
            this.listBoxAvailableItems.BeginUpdate();
            this.listBoxAvailableItems.Items.Clear();
            this.listBoxAvailableItems.Items.AddRange(this.business.AvailableItems.ToArray());
            this.listBoxAvailableItems.EndUpdate();
            this.listBoxSelectedItems.BeginUpdate();
            this.listBoxSelectedItems.Items.Clear();
            this.listBoxSelectedItems.Items.AddRange(this.business.SelectedItems.ToArray());
            this.listBoxSelectedItems.EndUpdate();
            this.labelSelectedItemsCount.Text = this.business.SelectedItems.Count.ToString();
        }

        #endregion


        #region Events.Incoming

        protected override void business_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.business_PropertyChanged(sender, e);
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else
            {
                if (e.PropertyName == "NameList") this.fillDataList();
                else if (e.PropertyName == "SelectedDataset") this.selectDataset(this.business.SelectedDataset);
                else if (e.PropertyName == "SelectedDatasetIndex") this.selectDataList();
                else if (e.PropertyName == "Items") this.fillItemLists();
                else if (e.PropertyName == "SelectedPlayer") this.setSelectedPlayer();

            }
        }

        void business_Alarm1Fired(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke((MethodInvoker)(() => this.business_Alarm1Fired(sender, e)));
            else { this.userControlRecTriggerTimerAlarmTime1.StartTrigger(); }
        }

        void business_Alarm2Fired(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke((MethodInvoker)(() => this.business_Alarm2Fired(sender, e)));
            else { this.userControlRecTriggerTimerAlarmTime2.StartTrigger(); }
        }

        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void numericUpDownLeftPlayerValue_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerValue = (int)this.numericUpDownLeftPlayerValue.Value; }
        private void checkBoxLeftPlayerBestEstimation_CheckedChanged(object sender, EventArgs e) { this.business.LeftPlayerBestEstimation = this.checkBoxLeftPlayerBestEstimation.Checked; }
        private void checkBoxLeftPlayerHighestCounter_CheckedChanged(object sender, EventArgs e) { this.business.LeftPlayerHighestCounter = this.checkBoxLeftPlayerHighestCounter.Checked; }
        protected virtual void numericUpDownLeftPlayerCounter_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerCounter = (int)this.numericUpDownLeftPlayerCounter.Value; }
        protected virtual void buttonLeftPlayerAddCounterHot_01_Click(object sender, EventArgs e)
        {
            this.business.LeftPlayerCounter++;
            this.business.Vinsert_SetCounter();
        }

        private void numericUpDownRightPlayerValue_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerValue = (int)this.numericUpDownRightPlayerValue.Value; }
        private void checkBoxRightPlayerBestEstimation_CheckedChanged(object sender, EventArgs e) { this.business.RightPlayerBestEstimation = this.checkBoxRightPlayerBestEstimation.Checked; }
        private void checkBoxRightPlayerHighestCounter_CheckedChanged(object sender, EventArgs e) { this.business.RightPlayerHighestCounter = this.checkBoxRightPlayerHighestCounter.Checked; }
        protected virtual void numericUpDownRightPlayerCounter_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerCounter = (int)this.numericUpDownRightPlayerCounter.Value; }
        protected virtual void buttonRightPlayerAddCounterHot_01_Click(object sender, EventArgs e)
        {
            this.business.RightPlayerCounter++;
            this.business.Vinsert_SetCounter();
        }

        private void pictureBoxFemaleSelected_Click(object sender, EventArgs e) { this.business.FemalesSelected = true; }
        private void pictureBoxMaleSelected_Click(object sender, EventArgs e) { this.business.FemalesSelected = false; }

        private void buttonLeftPlayerSelected_Click(object sender, EventArgs e) { this.business.SelectedPlayer = Content.Gameboard.PlayerSelection.LeftPlayer; }
        private void buttonRightPlayerSelected_Click(object sender, EventArgs e) { this.business.SelectedPlayer = Content.Gameboard.PlayerSelection.RightPlayer; }

        protected void buttonReleaseBuzzer_Click(object sender, EventArgs e) { if (this.business is Business) this.business.ReleaseBuzzer(); }
        protected void buttonLockBuzzer_Click(object sender, EventArgs e) { if (this.business is Business) this.business.LockBuzzer(); }

        protected virtual void buttonTimerIn_Click(object sender, EventArgs e) { this.business.Vinsert_TimerIn(); }
        protected virtual void buttonTimerOut_Click(object sender, EventArgs e) { this.business.Vinsert_TimerOut(); }
        protected virtual void buttonTimerStart_Click(object sender, EventArgs e)
        {
            this.business.Vinsert_StartTimer();
            this.business.Vfullscreen_StartTimer();
        }
        protected virtual void buttonTimerStop_Click(object sender, EventArgs e)
        {
            this.business.Vinsert_StopTimer();
            this.business.Vfullscreen_StopTimer();
        }
        protected virtual void buttonTimerContinue_Click(object sender, EventArgs e)
        {
            this.business.Vinsert_ContinueTimer();
            this.business.Vfullscreen_ContinueTimer();
        }
        protected virtual void buttonTimerReset_Click(object sender, EventArgs e)
        {
            this.business.Vinsert_ResetTimer();
            this.business.Vfullscreen_ResetTimer();
        }
        protected virtual void checkBoxTimerRunExtraTime_CheckedChanged(object sender, EventArgs e) { this.business.RunExtraTime = this.checkBoxTimerRunExtraTime.Checked; }
        private void buttonTimer_30_Click(object sender, EventArgs e) { this.business.TimerStartTime = 30; }
        private void buttonTimer_45_Click(object sender, EventArgs e) { this.business.TimerStartTime = 45; }
        private void buttonTimer_60_Click(object sender, EventArgs e) { this.business.TimerStartTime = 60; }

        private void listBoxDataList_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectDataset(this.listBoxDataList.SelectedIndex); }

        private void buttonListIn_Click(object sender, EventArgs e) { this.business.Vinsert_ListIn(); }
        private void buttonListOut_Click(object sender, EventArgs e) { this.business.Vinsert_ListOut(); }
        private void buttonFillList_Click(object sender, EventArgs e) { this.business.Vinsert_FillList(); }
        private void listBoxAvailableItems_Click(object sender, EventArgs e) { this.business.SelectItem(this.listBoxAvailableItems.SelectedItem as ListItem); }
        private void listBoxSelectedItems_Click(object sender, EventArgs e) { this.business.DeselectItem(this.listBoxSelectedItems.SelectedItem as ListItem); }

        protected void buttonVinsert_CounterIn_Click(object sender, EventArgs e) { this.business.Vinsert_CounterIn(); }
        protected void buttonVinsert_SetCounter_Click(object sender, EventArgs e) { this.business.Vinsert_SetCounter(); }
        protected void buttonVinsert_CounterOut_Click(object sender, EventArgs e) { this.business.Vinsert_CounterOut(); }
        protected virtual void buttonVinsert_TimerIn_Click(object sender, EventArgs e) { this.business.Vinsert_TimerIn(); }
        protected virtual void buttonVinsert_StartTimer_Click(object sender, EventArgs e) { this.business.Vinsert_StartTimer(); }
        protected virtual void buttonVinsert_StopTimer_Click(object sender, EventArgs e) { this.business.Vinsert_StopTimer(); }
        protected virtual void buttonVinsert_TimerOut_Click(object sender, EventArgs e) { this.business.Vinsert_TimerOut(); }
        private void buttonVinsert_SetScore_01_Click(object sender, EventArgs e) { this.business.Vinsert_SetScore(); }

        protected virtual void buttonVfullscreen_StartTimer_Click(object sender, EventArgs e) { this.business.Vfullscreen_StartTimer(); }
        protected virtual void buttonVfullscreen_StopTimer_Click(object sender, EventArgs e) { this.business.Vfullscreen_StopTimer(); }
        protected virtual void buttonVfullscreen_ResetTimer_Click(object sender, EventArgs e) { this.business.Vfullscreen_ResetTimer(); }

        private void buttonVstage_ContentIn_Click(object sender, EventArgs e) { this.business.Vstage_ContentIn(); }
        private void buttonVstage_ContentOut_Click(object sender, EventArgs e) { this.business.Vstage_ContentOut(); }
        private void buttonVstage_SetScore_01_Click(object sender, EventArgs e) { this.business.Vstage_SetScore(); }

        private void buttonVplayers_ContentOut_Click(object sender, EventArgs e) { this.business.Vplayers_ContentOut(); }
        private void buttonVplayers_ShowInput_Click(object sender, EventArgs e) { this.business.Vplayers_ShowInput(); }

        private void buttonVhost_SetInput_Click(object sender, EventArgs e) { this.business.Vhost_SetPlayerInput(); }
        private void buttonVhost_ShowResults_Click(object sender, EventArgs e) { this.business.Vhost_ShowResults(); }

        private void buttonVleftplayer_Unlock_Click(object sender, EventArgs e) { this.business.Vleftplayer_UnlockInput(); }
        private void buttonVleftplayer_Lock_Click(object sender, EventArgs e) { this.business.Vleftplayer_LockInput(); }

        private void buttonVrightplayer_Unlock_Click(object sender, EventArgs e) { this.business.Vrightplayer_UnlockInput(); }
        private void buttonVrightplayer_Lock_Click(object sender, EventArgs e) { this.business.Vrightplayer_LockInput(); }

        private void buttonGame_ReleaseBuzzer_Click(object sender, EventArgs e) { this.business.ReleaseBuzzer(); }
        private void buttonGame_LockBuzzer_Click(object sender, EventArgs e) { this.business.LockBuzzer(); }
        private void buttonGame_SetResults_Click(object sender, EventArgs e) { this.business.SetResults(); }
        private void buttonGame_ResolveHighestCounter_Click(object sender, EventArgs e) { this.business.ResolveHighestCounter(); }
        private void buttonGame_ResolveBestEstimation_Click(object sender, EventArgs e) { this.business.ResolveBestEstimation(); }
        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }

        #endregion

    }
}