using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.SortListTimerScore {

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

            Binding bind;

            bind = new Binding("BackColor", this.business, "TargetPosition");
            bind.Format += (s, e) => { e.Value = (int)e.Value == 1 ? Constants.ColorSelected : SystemColors.ButtonFace; };
            this.buttonTargetPosition_01.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "TargetPosition");
            bind.Format += (s, e) => { e.Value = (int)e.Value == 2 ? Constants.ColorSelected : SystemColors.ButtonFace; };
            this.buttonTargetPosition_02.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "TargetPosition");
            bind.Format += (s, e) => { e.Value = (int)e.Value == 3 ? Constants.ColorSelected : SystemColors.ButtonFace; };
            this.buttonTargetPosition_03.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "TargetPosition");
            bind.Format += (s, e) => { e.Value = (int)e.Value == 4 ? Constants.ColorSelected : SystemColors.ButtonFace; };
            this.buttonTargetPosition_04.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "TargetPosition");
            bind.Format += (s, e) => { e.Value = (int)e.Value == 5 ? Constants.ColorSelected : SystemColors.ButtonFace; };
            this.buttonTargetPosition_05.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "TargetPosition");
            bind.Format += (s, e) => { e.Value = (int)e.Value == 6 ? Constants.ColorSelected : SystemColors.ButtonFace; };
            this.buttonTargetPosition_06.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "TargetPosition");
            bind.Format += (s, e) => { e.Value = (int)e.Value == 7 ? Constants.ColorSelected : SystemColors.ButtonFace; };
            this.buttonTargetPosition_07.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "TargetPosition");
            bind.Format += (s, e) => { e.Value = (int)e.Value == 8 ? Constants.ColorSelected : SystemColors.ButtonFace; };
            this.buttonTargetPosition_08.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "TargetPosition");
            bind.Format += (s, e) => { e.Value = (int)e.Value == 9 ? Constants.ColorSelected : SystemColors.ButtonFace; };
            this.buttonTargetPosition_09.DataBindings.Add(bind);

            this.fillDataList();
            this.selectDataset(this.business.SelectedDataset);

            this.fillListBoxesChoices();
            this.setSelectionStatus();

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

            this.buttonTargetPosition_01.DataBindings.Clear();
            this.buttonTargetPosition_02.DataBindings.Clear();
            this.buttonTargetPosition_03.DataBindings.Clear();
            this.buttonTargetPosition_04.DataBindings.Clear();
            this.buttonTargetPosition_05.DataBindings.Clear();
            this.buttonTargetPosition_06.DataBindings.Clear();
            this.buttonTargetPosition_07.DataBindings.Clear();
            this.buttonTargetPosition_08.DataBindings.Clear();
            this.buttonTargetPosition_09.DataBindings.Clear();
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
            step.AddButton(this.buttonVinsert_TargetIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_StartTimer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_Select);
            step.AddButton(this.buttonVstage_Select);
            step.AddButton(this.buttonVinsert_TimerOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, this.checkTrueOrFalse);
            step.AddButton(this.buttonGame_Resolve);
            step.AddButton(this.buttonVstage_Set);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_ShowSolution);
            step.AddButton(this.buttonVstage_ShowSolution);
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
            step = new stepAction(index, (stepIndex) => stepIndex - 10);
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
        protected int checkTrueOrFalse(
            int stepIndex) {
            if (this.business.SelectionStatus == SelectionStates.Idle ||
                this.business.SelectionStatus == SelectionStates.True) return stepIndex - 2;
            else return stepIndex + 1;
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

        private void setSelectionStatus() {
            if (this.listBoxIdleChoices.Enabled) {
                switch (this.business.SelectionStatus) {
                    case SelectionStates.True:
                        this.listBoxIdleChoices.BackColor = Constants.ColorEnabled;
                        break;
                    case SelectionStates.False:
                        this.listBoxIdleChoices.BackColor = Constants.ColorDisabled;
                        break;
                    case SelectionStates.Idle:
                    default:
                        this.listBoxIdleChoices.BackColor = SystemColors.Control;
                        break;
                }
            }
        }

        private void fillListBoxesChoices() {
            this.listBoxIdleChoices.BeginUpdate();
            this.listBoxIdleChoices.Items.Clear();
            if (this.business.SelectedDataset is DatasetContent) {
                foreach (DatasetContentItem item in this.business.SelectedDataset.ChoiceList) {
                    if (item.Status != DatasetContentItem.StatusElements.Busy) this.listBoxIdleChoices.Items.Add(item);
                }
            }
            this.listBoxIdleChoices.EndUpdate();
            this.listBoxIdleChoices.Enabled = this.listBoxIdleChoices.Items.Count > 0;
            Helper.setControlBackColor(this.listBoxIdleChoices);

            this.listBoxUsedChoices.BeginUpdate();
            this.listBoxUsedChoices.Items.Clear();
            if (this.business.SelectedDataset is DatasetContent) {
                foreach (DatasetContentItem item in this.business.SelectedDataset.ItemList) {
                    if (item.Status == DatasetContentItem.StatusElements.Busy) this.listBoxUsedChoices.Items.Add(item);
                }
            }
            this.listBoxUsedChoices.EndUpdate();
            this.listBoxUsedChoices.Enabled = this.listBoxUsedChoices.Items.Count > 0;
            Helper.setControlBackColor(this.listBoxUsedChoices);

            int counter = 0;
            if (this.listBoxUsedChoices.Items.Count > 0) counter = this.listBoxUsedChoices.Items.Count + 1;
            this.buttonTargetPosition_01.Visible = counter >= 1;
            this.buttonTargetPosition_02.Visible = counter >= 2;
            this.buttonTargetPosition_03.Visible = counter >= 3;
            this.buttonTargetPosition_04.Visible = counter >= 4;
            this.buttonTargetPosition_05.Visible = counter >= 5;
            this.buttonTargetPosition_06.Visible = counter >= 6;
            this.buttonTargetPosition_07.Visible = counter >= 7;
            this.buttonTargetPosition_08.Visible = counter >= 8;
            this.buttonTargetPosition_09.Visible = counter >= 9;
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
                else if (e.PropertyName == "SelectionStatus") this.setSelectionStatus();
                else if (e.PropertyName == "Choices") {
                    this.fillListBoxesChoices();
                    this.setSelectionStatus();
                }
            }
        }

        void selectedDataset_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.selectedDataset_PropertyChanged(sender, e)));
            else {
                //if (e.PropertyName == "PictureFilename") this.pictureBoxDatasetPicture.Image = this.selectedDataset.Movie;
            }
        }

        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void listBoxDataList_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectDataset(this.listBoxDataList.SelectedIndex); }

        private void listBoxIdleChoices_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectChoice(this.listBoxIdleChoices.SelectedItem as DatasetContentItem); }
        private void buttonTargetPosition_Click(object sender, EventArgs e) {
            int result = 0;
            if (Helper.tryParseIndexFromControl(sender as Control, out result)) this.business.SetTargetPosition(result);
        }

        private void buttonVinsert_ContentIn_Click(object sender, EventArgs e) { this.business.Vinsert_ContentIn(); }
        private void buttonVinsert_TargetIn_Click(object sender, EventArgs e) { this.business.Vinsert_TargetIn(); }
        private void buttonVinsert_ResolveAll_Click(object sender, EventArgs e) { this.business.Vinsert_ResolveAll(); }
        private void buttonVinsert_ContentOut_Click(object sender, EventArgs e) { this.business.Vinsert_ContentOut(); }

        private void buttonVstage_ContentIn_Click(object sender, EventArgs e) { this.business.Vstage_ContentIn(); }
        private void buttonVstage_Select_Click(object sender, EventArgs e) { this.business.Vstage_Select(); }
        private void buttonVstage_Set_Click(object sender, EventArgs e) { this.business.Vstage_SetContent(); }
        private void buttonVstage_ShowSolution_Click(object sender, EventArgs e) { this.business.Vstage_ShowSolution(); }
        private void buttonVstage_ShowResolved_Click(object sender, EventArgs e) { this.business.Vstage_ShowResolved(); }
        private void buttonVstage_ContentOut_Click(object sender, EventArgs e) { this.business.Vstage_ContentOut(); }

        private void buttonGame_Reset_Click(object sender, EventArgs e) { this.business.ResetSelectedDataset(); }
        private void buttonGame_Select_Click(object sender, EventArgs e) { this.business.ExecuteSelection(); }
        private void buttonGame_Resolve_Click(object sender, EventArgs e) { this.business.Resolve(); }
        private void buttonGame_ShowSolution_Click(object sender, EventArgs e) { this.business.ShowSolution(); }
        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }

        #endregion

    }

}
