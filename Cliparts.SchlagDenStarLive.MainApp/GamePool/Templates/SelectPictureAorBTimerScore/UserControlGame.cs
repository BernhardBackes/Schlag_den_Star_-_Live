using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SelectPictureAorBTimerScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.SelectPictureAorBTimerScore {

    public partial class UserControlGame : _Base.Score.UserControlGame {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.comboBoxLeftPlayerInput.BeginUpdate();
            this.comboBoxLeftPlayerInput.Items.AddRange(Enum.GetNames(typeof(Fullscreen.SelectionElements)));
            this.comboBoxLeftPlayerInput.EndUpdate();

            this.comboBoxRightPlayerInput.BeginUpdate();
            this.comboBoxRightPlayerInput.Items.AddRange(Enum.GetNames(typeof(Fullscreen.SelectionElements)));
            this.comboBoxRightPlayerInput.EndUpdate();

            this.numericUpDownTaskCounter.Minimum = int.MinValue;
            this.numericUpDownTaskCounter.Maximum = int.MaxValue;

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            this.userControlGamePoolTemplates_ModulesTimerGame.Pose(this.business.Vinsert_Timer);
            this.userControlGamePoolTemplates_ModulesTimerGame.BackColor = this.BackColor;

            Binding bind;

            bind = new Binding("Text", this.business, "LeftPlayerInput");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxLeftPlayerInput.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "LeftPlayerCorrect");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorWinner : SystemColors.Control; };
            this.comboBoxLeftPlayerInput.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerInput");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxRightPlayerInput.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "RightPlayerCorrect");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorWinner : SystemColors.Control; };
            this.comboBoxRightPlayerInput.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TaskCounter");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTaskCounter.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "BuzzerMode");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorSelected : SystemColors.Control; };
            this.checkBoxBuzzerMode.DataBindings.Add(bind);

            this.labelGameClass.Text = this.business.ClassInfo;

            this.setUseTimer();
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

            this.userControlGamePoolTemplates_ModulesTimerGame.Dispose();

            this.comboBoxLeftPlayerInput.DataBindings.Clear();
            this.comboBoxRightPlayerInput.DataBindings.Clear();

            this.numericUpDownTaskCounter.DataBindings.Clear();

            this.checkBoxUseTimer.DataBindings.Clear();

            this.checkBoxBuzzerMode.DataBindings.Clear();
        }

        protected override void buildStepList() {

            int index = 0;
            stepAction step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_LoadScene);
            step.AddButton(this.buttonVfullscreen_LoadScene);
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
            step.AddButton(this.buttonVfullscreen_ShowGame);
            step.AddButton(this.buttonVstage_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_BorderIn);
            step.AddButton(this.buttonVfullscreen_ContentIn);
            step.AddButton(this.buttonVhost_ContentIn);
            step.AddButton(this.buttonVplayers_ContentIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVplayers_ReleaseInput);
            step.AddButton(this.buttonVinsert_TimerIn);
            step.AddButton(this.buttonVinsert_StartTimer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVplayers_LockInput);
            step.AddButton(this.buttonVhost_SetInput);
            step.AddButton(this.buttonVfullscreen_ShowInput);
            step.AddButton(this.buttonVinsert_StopTimer);
            step.AddButton(this.buttonVinsert_TimerOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_Resolve);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_BorderOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ScoreIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_Resolve);
            step.AddButton(this.buttonVinsert_SetScore);
            step.AddButton(this.buttonVstage_SetScore);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 7);
            step.AddButton(this.buttonVinsert_ScoreOut);
            step.AddButton(this.buttonVfullscreen_ContentOut);
            step.AddButton(this.buttonVhost_ContentOut);
            step.AddButton(this.buttonVplayers_ContentOut);
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
            step.AddButton(this.buttonVfullscreen_UnloadScene);
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

            if (this.selectedDataset is DatasetContent) {
                this.pictureBoxDatasetPicture.Enabled = true;
                this.pictureBoxDatasetPicture.Image = this.selectedDataset.Solution;
            }
            else {
                this.pictureBoxDatasetPicture.Enabled = false;
                this.pictureBoxDatasetPicture.Image = null;
            }

            this.selectDataList();
        }

        private void setUseTimer() {
            this.buttonVinsert_TimerIn.Enabled = this.business.UseTimer;
            this.buttonVinsert_StartTimer.Enabled = this.business.UseTimer;
            this.buttonVinsert_StopTimer.Enabled = this.business.UseTimer;
            this.buttonVinsert_TimerOut.Enabled = this.business.UseTimer;
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
                else if (e.PropertyName == "UseTimer") this.setUseTimer();
            }
        }

        void selectedDataset_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.selectedDataset_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "SolutionFilename") this.pictureBoxDatasetPicture.Image = this.selectedDataset.Solution;
            }
        }

        #endregion

        #region Events.Controls

        private void UserControlGame_BackColorChanged(object sender, EventArgs e) {
            this.userControlGamePoolTemplates_ModulesTimerGame.BackColor = this.BackColor;
        }

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void comboBoxLeftPlayerInput_SelectedIndexChanged(object sender, EventArgs e) {
            Fullscreen.SelectionElements result;
            if (this.business is Business && 
                Enum.TryParse(this.comboBoxLeftPlayerInput.Text, out result)) this.business.LeftPlayerInput = result;
        }
        private void buttonLeftPlayerReleaseInput_Click(object sender, EventArgs e) { this.business.Vleftplayer_ReleaseInput(); }
        private void buttonLeftPlayerLockInput_Click(object sender, EventArgs e) { this.business.Vleftplayer_LockInput(); }

        private void comboBoxRightPlayerInput_SelectedIndexChanged(object sender, EventArgs e) {
            Fullscreen.SelectionElements result;
            if (this.business is Business && 
                Enum.TryParse(this.comboBoxRightPlayerInput.Text, out result)) this.business.RightPlayerInput = result;
        }
        private void buttonRightPlayerReleaseInput_Click(object sender, EventArgs e) { this.business.Vrightplayer_ReleaseInput(); }
        private void buttonRightPlayerLockInput_Click(object sender, EventArgs e) { this.business.Vrightplayer_LockInput(); }

        private void checkBoxUseTimer_CheckedChanged(object sender, EventArgs e) { this.business.UseTimer = this.checkBoxUseTimer.Checked; }

        private void numericUpDownTaskCounter_ValueChanged(object sender, EventArgs e) { this.business.TaskCounter = (int)this.numericUpDownTaskCounter.Value; }

        private void listBoxDataList_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectDataset(this.listBoxDataList.SelectedIndex); }

        private void checkBoxBuzzerMode_CheckedChanged(object sender, EventArgs e) { this.business.BuzzerMode = this.checkBoxBuzzerMode.Checked; }

        private void buttonVinsert_BorderIn_Click(object sender, EventArgs e) { this.business.Vinsert_BorderIn(); }
        private void buttonVinsert_BorderOut_Click(object sender, EventArgs e) { this.business.Vinsert_BorderOut(); }
        protected virtual void buttonVinsert_TimerIn_Click(object sender, EventArgs e) { this.business.Vinsert_Timer.In(); }
        private void buttonVinsert_TimerIn_EnabledChanged(object sender, EventArgs e) { Helper.setControlBackColor(sender as Control); }
        protected virtual void buttonVinsert_StartTimer_Click(object sender, EventArgs e) { this.business.Vinsert_Timer.Start(); }
        private void buttonVinsert_StartTimer_EnabledChanged(object sender, EventArgs e) { Helper.setControlBackColor(sender as Control); }
        protected virtual void buttonVinsert_StopTimer_Click(object sender, EventArgs e) { this.business.Vinsert_Timer.Stop(); }
        private void buttonVinsert_StopTimer_EnabledChanged(object sender, EventArgs e) { Helper.setControlBackColor(sender as Control); }
        protected virtual void buttonVinsert_TimerOut_Click(object sender, EventArgs e) { this.business.Vinsert_Timer.Out(); }
        private void buttonVinsert_TimerOut_EnabledChanged(object sender, EventArgs e) { Helper.setControlBackColor(sender as Control); }

        private void buttonVfullscreen_ContentIn_Click(object sender, EventArgs e) { this.business.Vfullscreen_ContentIn(); }
        private void buttonVfullscreen_Set_Click(object sender, EventArgs e) { this.business.Vfullscreen_ShowInput(); }
        private void buttonVfullscreen_Resolve_Click(object sender, EventArgs e) { this.business.Vfullscreen_Resolve(); }
        private void buttonVfullscreen_ContentOut_Click(object sender, EventArgs e) { this.business.Vfullscreen_ContentOut(); }

        private void buttonVhost_ContentIn_Click(object sender, EventArgs e) { this.business.Vhost_In(); }
        private void buttonVhost_SetInput_Click(object sender, EventArgs e) { this.business.Vhost_Set(); }
        private void buttonVhost_ContentOut_Click(object sender, EventArgs e) { this.business.Vhost_Out(); }

        private void buttonVplayers_ContentIn_Click(object sender, EventArgs e) {
            this.business.Vleftplayer_ContentIn();
            this.business.Vrightplayer_ContentIn();
        }
        private void buttonVplayers_ReleaseInput_Click(object sender, EventArgs e) {
            this.business.Vleftplayer_ReleaseInput();
            this.business.Vrightplayer_ReleaseInput();
        }
        private void buttonVplayers_LockInput_Click(object sender, EventArgs e) {
            this.business.Vleftplayer_LockInput();
            this.business.Vrightplayer_LockInput();
        }
        private void buttonVplayers_ContentOut_Click(object sender, EventArgs e) {
            this.business.Vleftplayer_ContentOut();
            this.business.Vrightplayer_ContentOut();
        }

        private void buttonGame_Resolve_Click(object sender, EventArgs e) { this.business.Resolve(); }

        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }

        #endregion
    }

}
