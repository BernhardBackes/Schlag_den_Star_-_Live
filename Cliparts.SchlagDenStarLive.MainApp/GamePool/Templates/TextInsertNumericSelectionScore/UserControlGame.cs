using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TextInsertNumericSelectionScore {

    public partial class UserControlGame : _Base.Score.UserControlGame {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.numericUpDownLeftPlayerValue.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerValue.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerValue.Minimum = int.MinValue;
            this.numericUpDownRightPlayerValue.Maximum = int.MaxValue;

            this.numericUpDownTaskCounter.Minimum = int.MinValue;
            this.numericUpDownTaskCounter.Maximum = int.MaxValue;

            this.numericUpDownSolution.Minimum = int.MinValue;
            this.numericUpDownSolution.Maximum = int.MaxValue;

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.business, "LeftPlayerValue");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerValue.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "LeftPlayerValueMatch");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorDisabled : SystemColors.Control; };
            this.numericUpDownLeftPlayerValue.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "LeftPlayerHasValue");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorEnabled : SystemColors.Control; };
            this.buttonLeftPlayerHasValue.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "LeftPlayerOffset");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxLeftPlayerOffset.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "LeftPlayerIsCloser");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorEnabled : SystemColors.Control; };
            this.textBoxLeftPlayerOffset.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerValue");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerValue.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "RightPlayerValueMatch");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorDisabled : SystemColors.Control; };
            this.numericUpDownRightPlayerValue.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "RightPlayerHasValue");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorEnabled : SystemColors.Control; };
            this.buttonRightPlayerHasValue.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerOffset");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxRightPlayerOffset.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "RightPlayerIsCloser");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorEnabled : SystemColors.Control; };
            this.textBoxRightPlayerOffset.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TaskCounter");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTaskCounter.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "Solution");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownSolution.DataBindings.Add(bind);

            this.userControlTextInsertNumericSelectionScoreLeftPlayerClient.BackColor = this.BackColor;
            this.userControlTextInsertNumericSelectionScoreLeftPlayerClient.Pose("left player", business.LeftPlayerClient);

            this.userControlTextInsertNumericSelectionScoreRightPlayerClient.BackColor = this.BackColor;
            this.userControlTextInsertNumericSelectionScoreRightPlayerClient.Pose("right player", business.RightPlayerClient);

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

            this.numericUpDownLeftPlayerValue.DataBindings.Clear();
            this.buttonLeftPlayerHasValue.DataBindings.Clear();
            this.textBoxLeftPlayerOffset.DataBindings.Clear();
            this.numericUpDownRightPlayerValue.DataBindings.Clear();
            this.textBoxRightPlayerOffset.DataBindings.Clear();
            this.buttonRightPlayerHasValue.DataBindings.Clear();

            this.numericUpDownTaskCounter.DataBindings.Clear();

            this.numericUpDownSolution.DataBindings.Clear();

            this.userControlTextInsertNumericSelectionScoreLeftPlayerClient.Dispose();
            this.userControlTextInsertNumericSelectionScoreRightPlayerClient.Dispose();

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
            step.AddButton(this.buttonGame_ConnectClients);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ScoreIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ContentIn);
            step.AddButton(this.buttonGame_UnlockClients);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_ShowInput);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ShowSolution);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_Resolve);
            step.AddButton(this.buttonVinsert_SetScore);
            step.AddButton(this.buttonVstage_SetScore);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 4);
            step.AddButton(this.buttonVinsert_ContentOut);
            step.AddButton(this.buttonGame_Next);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ScoreOut);
            this.stepList.Add(step);

            index++;
            this.winnerStepIndex = index;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVgraphic_Clear);
            step.AddButton(this.buttonVstage_Clear);
            step.AddButton(this.buttonGame_DisconnectClients);
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

        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void UserControlGame_BackColorChanged(object sender, EventArgs e) {
            this.userControlTextInsertNumericSelectionScoreLeftPlayerClient.BackColor = this.BackColor;
            this.userControlTextInsertNumericSelectionScoreRightPlayerClient.BackColor = this.BackColor;
        }

        private void numericUpDownLeftPlayerValue_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerValue = (int)this.numericUpDownLeftPlayerValue.Value; }
        private void buttonLeftPlayerHasValue_Click(object sender, EventArgs e) { this.business.LeftPlayerHasValue = !this.business.LeftPlayerHasValue;  }

        private void numericUpDownRightPlayerValue_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerValue = (int)this.numericUpDownRightPlayerValue.Value; }
        private void buttonRightPlayerHasValue_Click(object sender, EventArgs e) { this.business.RightPlayerHasValue = !this.business.RightPlayerHasValue; }

        private void numericUpDownTaskCounter_ValueChanged(object sender, EventArgs e) { this.business.TaskCounter = (int)this.numericUpDownTaskCounter.Value; }

        private void numericUpDownSolution_ValueChanged(object sender, EventArgs e) { this.business.Solution = (int)this.numericUpDownSolution.Value; }

        private void listBoxDataList_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectDataset(this.listBoxDataList.SelectedIndex); }

        private void buttonVinsert_ContentIn_Click(object sender, EventArgs e) { this.business.Vinsert_ContentIn(); }
        private void buttonVinsert_ShowSolution_Click(object sender, EventArgs e) { this.business.Vinsert_ShowSolution(); }
        private void buttonVinsert_ContentOut_Click(object sender, EventArgs e) { this.business.Vinsert_ContentOut(); }

        private void buttonGame_ConnectClients_Click(object sender, EventArgs e) { this.business.ConnectClients(); }
        private void buttonGame_UnlockClients_Click(object sender, EventArgs e) { this.business.UnlockPlayerClients(); }
        private void buttonGame_ShowInput_Click(object sender, EventArgs e) { this.business.ShowInput(); }
        private void buttonGame_Resolve_Click(object sender, EventArgs e) { this.business.Resolve(); }
        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }
        private void buttonGame_DisconnectClients_Click(object sender, EventArgs e) { this.business.DisconnectClients(); }

        #endregion
    }
}
