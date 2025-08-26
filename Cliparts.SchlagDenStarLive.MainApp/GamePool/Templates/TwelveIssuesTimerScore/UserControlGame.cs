using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TwelveIssuesTimerScore {

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
            this.business.PropertyChanged += this.business_PropertyChanged;

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
        }

        protected override void buildStepList() {

            int index = 0;
            stepAction step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_LoadScene);
            step.AddButton(this.buttonVfullscreen_LoadScene);
            step.AddButton(this.buttonVhost_LoadScene);
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
            step.AddButton(this.buttonVfullscreen_ShowGame);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_ResetData);
            step.AddButton(this.buttonVinsert_BorderIn);
            step.AddButton(this.buttonVfullscreen_ContentIn);
            step.AddButton(this.buttonVhost_ContentIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimerIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_StartTimer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex);
            step.AddButton(this.buttonVinsert_StopTimer);
            step.AddButton(this.buttonVfullscreen_SetContent);
            step.AddButton(this.buttonVhost_SetContent);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimerOut);
            step.AddButton(this.buttonVfullscreen_ResolveContent);
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
            step.AddButton(this.buttonVinsert_SetScore);
            step.AddButton(this.buttonVstage_SetScore);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 8);
            step.AddButton(this.buttonVinsert_ScoreOut);
            step.AddButton(this.buttonVfullscreen_ContentOut);
            step.AddButton(this.buttonVhost_ContentOut);
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
            step.AddButton(this.buttonVhost_UnloadScene);
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

            if (this.selectedDataset is DatasetContent) {
                for (int i = 0; i < Business.ITEMS_COUNT; i++) this.setDataItem(i, this.selectedDataset.GetItem(i));
            }
            else {
                for (int i = 0; i < Business.ITEMS_COUNT; i++) this.setDataItem(i, null);
            }

            this.selectDataList();
        }

        private void setDataItem(
            int index,
            DataItem item) {
            if (index >= 0 &&
                index < Business.ITEMS_COUNT) {
                string buttoKey = string.Format("buttonDataItem_{0}", index.ToString("00"));
                Button button = this.groupBoxDataItems.Controls[buttoKey] as Button;
                if (button is Button) {
                    if (item is DataItem &&
                        item.FileExists) {
                        button.Text = string.Format("{0}\n{1}", item.ID.ToString(), item.Name);
                        if (item.IsIdle) button.BackColor = Constants.ColorEnabled;
                        else button.BackColor = SystemColors.ButtonFace;
                        button.Visible = true;
                    }
                    else {
                        button.Text = string.Empty;
                        button.Visible = false;
                    }
                }
                buttoKey = string.Format("buttonWrong_{0}", index.ToString("00"));
                button = this.groupBoxDataItems.Controls[buttoKey] as Button;
                if (button is Button) {
                    if (item is DataItem &&
                        item.FileExists) {
                        button.Enabled = item.IsIdle;
                        Helper.setControlBackColor(button, true, Constants.ColorDisabled);
                        button.Visible = true;
                    }
                    else {
                        button.Visible = false;
                    }

                }
            }
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
                for (int i = 0; i < Business.ITEMS_COUNT; i++) this.setDataItem(i, this.selectedDataset.GetItem(i));
            }
        }

        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void listBoxDataList_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectDataset(this.listBoxDataList.SelectedIndex); }

        private void buttonDataItem_MouseDown(object sender, MouseEventArgs e) {
            int index;
            if (Helper.tryParseIndexFromControl(sender as Control, out index)) {
                if (e.Button == MouseButtons.Left) {
                    this.business.ItemOut(index, false);
                    this.business.Vinsert_StopTimer();
                    this.nextStepIndex = 5;
                }
                else if (e.Button == MouseButtons.Right) this.business.ToggleItem(index);
            }
        }

        private void buttonWrong_Click(object sender, EventArgs e) {
            int index;
            if (Helper.tryParseIndexFromControl(sender as Control, out index)) {
                this.business.ItemOut(index, true);
                this.business.Vinsert_StopTimer();
                this.nextStepIndex = 7;
            }
        }

        private void buttonVinsert_BorderIn_Click(object sender, EventArgs e) { this.business.Vinsert_BorderIn(); }
        private void buttonVinsert_BorderOut_Click(object sender, EventArgs e) { this.business.Vinsert_BorderOut(); }


        private void buttonVfullscreen_ContentIn_Click(object sender, EventArgs e) { this.business.Vfullscreen_ContentIn(); }
        private void buttonVfullscreen_SetContent_Click(object sender, EventArgs e) { this.business.Vfullscreen_SetContent(); }
        private void buttonVfullscreen_ResolveContent_Click(object sender, EventArgs e) { this.business.Vfullscreen_ResolveContent(); }
        private void buttonVfullscreen_ContentOut_Click(object sender, EventArgs e) { this.business.Vfullscreen_ContentOut(); }

        private void buttonVhost_ContentIn_Click(object sender, EventArgs e) { this.business.Vhost_ContentIn(); }
        private void buttonVhost_SetContent_Click(object sender, EventArgs e) { this.business.Vhost_SetContent(); }
        private void buttonVhost_ContentOut_Click(object sender, EventArgs e) { this.business.Vhost_ContentOut(); }

        private void buttonGame_ResetData_Click(object sender, EventArgs e) { this.business.ResetDataset(); }
        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }


        #endregion
    }

}
