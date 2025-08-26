using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.AnswerListTimerScore {

    public partial class UserControlGame : _Base.TimerScore.UserControlGame {

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

            this.labelGameClass.Text = this.business.ClassInfo;

            this.fillDataList();
            this.selectDataset(this.business.SelectedDataset);

            this.fillListBoxIdleItems();
            this.fillListBoxUsedChoices();
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
        }

        protected override void buildStepList() {

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
            step.AddButton(this.buttonVstage_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_Reset);
            step.AddButton(this.buttonVinsert_ContentIn);
            step.AddButton(this.buttonVstage_ContentIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_StartTimer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 1);
            step.AddButton(this.buttonGame_Select);
            step.AddButton(this.buttonVinsert_TimerOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ResolveAll);
            step.AddButton(this.buttonVstage_ShowResolved);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ContentOut);
            step.AddButton(this.buttonVstage_ContentOut);
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
            }
            else {
            }

            this.selectDataList();
        }

        private void fillListBoxIdleItems() {
            this.listBoxIdleItems.BeginUpdate();
            this.listBoxIdleItems.Items.Clear();
            this.listBoxIdleItems.Items.AddRange(this.business.IdleItems);
            this.listBoxIdleItems.EndUpdate();
            this.listBoxIdleItems.Enabled = this.listBoxIdleItems.Items.Count > 0;
            Helper.setControlBackColor(this.listBoxDataList);
        }

        private void fillListBoxUsedChoices() {
            this.listBoxUsedChoices.BeginUpdate();
            this.listBoxUsedChoices.Items.Clear();
            this.listBoxUsedChoices.Items.AddRange(this.business.SelectedItems);
            this.listBoxUsedChoices.EndUpdate();
            this.listBoxUsedChoices.Enabled = this.listBoxUsedChoices.Items.Count > 0;
            if (this.listBoxUsedChoices.Enabled) this.listBoxUsedChoices.SelectedIndex = this.listBoxUsedChoices.Items.Count - 1;
            Helper.setControlBackColor(this.listBoxDataList);
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
                else if (e.PropertyName == "IdleItems") this.fillListBoxIdleItems();
                else if (e.PropertyName == "SelectedItems") this.fillListBoxUsedChoices();
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

        private void listBoxDataList_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectDataset(this.listBoxDataList.SelectedIndex); }

        private void buttonWrong_Click(object sender, EventArgs e) {
            this.business.Wrong();
            this.nextStepIndex = 6;
        }

        private void buttonUsedChoiceBack_Click(object sender, EventArgs e) { this.business.TakeSelectionBack(this.listBoxUsedChoices.Text); }

        private void buttonVinsert_ContentIn_Click(object sender, EventArgs e) { this.business.Vinsert_ContentIn(); }
        protected override void buttonVinsert_StartTimer_Click(object sender, EventArgs e) {
            this.business.Vinsert_TimerIn();
            base.buttonVinsert_StartTimer_Click(sender, e);
        }
        private void buttonVinsert_ResolveAll_Click(object sender, EventArgs e) { this.business.Vinsert_ResolveAll(); }
        private void buttonVinsert_ContentOut_Click(object sender, EventArgs e) { this.business.Vinsert_ContentOut(); }

        private void buttonVstage_ContentIn_Click(object sender, EventArgs e) { this.business.Vstage_ContentIn(); }
        private void buttonVstage_ShowResolved_Click(object sender, EventArgs e) { this.business.Vstage_ShowResolved(); }
        private void buttonVstage_ContentOut_Click(object sender, EventArgs e) { this.business.Vstage_ContentOut(); }

        private void buttonGame_Reset_Click(object sender, EventArgs e) { this.business.ResetSelectedDataset(); }
        private void buttonGame_Select_Click(object sender, EventArgs e) { this.business.ExecuteSelection(this.listBoxIdleItems.Text); }
        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }

        #endregion

    }

}
