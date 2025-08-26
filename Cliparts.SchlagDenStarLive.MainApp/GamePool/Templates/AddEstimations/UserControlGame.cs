using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.AddEstimations {

    public partial class UserControlGame : _Base.Score.UserControlGame {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.numericUpDownLeftPlayerAnswer_00.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerAnswer_00.Maximum = int.MaxValue;

            this.numericUpDownLeftPlayerAnswer_01.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerAnswer_01.Maximum = int.MaxValue;

            this.numericUpDownLeftPlayerAnswer_02.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerAnswer_02.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerAnswer_00.Minimum = int.MinValue;
            this.numericUpDownRightPlayerAnswer_00.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerAnswer_01.Minimum = int.MinValue;
            this.numericUpDownRightPlayerAnswer_01.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerAnswer_02.Minimum = int.MinValue;
            this.numericUpDownRightPlayerAnswer_02.Maximum = int.MaxValue;

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.business, "LeftPlayerEstimation_1");
            bind.Format += (s, e) => { e.Value = Convert.ToInt32(e.Value); };
            this.numericUpDownLeftPlayerAnswer_00.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerEstimation_2");
            bind.Format += (s, e) => { e.Value = Convert.ToInt32(e.Value); };
            this.numericUpDownLeftPlayerAnswer_01.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerEstimation_3");
            bind.Format += (s, e) => { e.Value = Convert.ToInt32(e.Value); };
            this.numericUpDownLeftPlayerAnswer_02.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "LeftPlayerSum");
            bind.Format += (s, e) => { e.Value = Convert.ToInt32(e.Value).ToString("#,##0"); };
            this.textBoxLeftPlayerSum.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "LeftPlayerOffset");
            bind.Format += (s, e) => { e.Value = Convert.ToInt32(e.Value).ToString("#,##0"); };
            this.textBoxLeftPlayerOffset.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerEstimation_1");
            bind.Format += (s, e) => { e.Value = Convert.ToInt32(e.Value); };
            this.numericUpDownRightPlayerAnswer_00.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerEstimation_2");
            bind.Format += (s, e) => { e.Value = Convert.ToInt32(e.Value); };
            this.numericUpDownRightPlayerAnswer_01.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerEstimation_3");
            bind.Format += (s, e) => { e.Value = Convert.ToInt32(e.Value); };
            this.numericUpDownRightPlayerAnswer_02.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerSum");
            bind.Format += (s, e) => { e.Value = Convert.ToInt32(e.Value).ToString("#,##0"); };
            this.textBoxRightPlayerSum.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerOffset");
            bind.Format += (s, e) => { e.Value = Convert.ToInt32(e.Value).ToString("#,##0"); };
            this.textBoxRightPlayerOffset.DataBindings.Add(bind);

            this.fillDataList();
            this.selectDataset(this.business.SelectedDataset);

            this.setClosestPlayer();

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

            this.numericUpDownLeftPlayerAnswer_00.DataBindings.Clear();
            this.numericUpDownLeftPlayerAnswer_01.DataBindings.Clear();
            this.numericUpDownLeftPlayerAnswer_02.DataBindings.Clear();
            this.textBoxLeftPlayerSum.DataBindings.Clear();
            this.textBoxLeftPlayerOffset.DataBindings.Clear();

            this.numericUpDownRightPlayerAnswer_00.DataBindings.Clear();
            this.numericUpDownRightPlayerAnswer_01.DataBindings.Clear();
            this.numericUpDownRightPlayerAnswer_02.DataBindings.Clear();
            this.textBoxRightPlayerSum.DataBindings.Clear();
            this.textBoxRightPlayerOffset.DataBindings.Clear();
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
            step.AddButton(this.buttonVstage_ContentIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVplayers_Start);
            step.AddButton(this.buttonVinsert_TextInsertIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVhost_SetInput);
            step.AddButton(this.buttonVplayers_Lock);
            step.AddButton(this.buttonVinsert_TextInsertOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ContentIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ShowSolutions);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ShowSolution_1);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ShowSolution_2);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ShowSolution_3);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_Resolve);
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
            step.AddButton(this.buttonGame_Resolve);
            step.AddButton(this.buttonVinsert_SetScore);
            step.AddButton(this.buttonVstage_SetScore);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 12);
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
            step.AddButton(this.buttonVhost_UnloadScene);
            step.AddButton(this.buttonVleftplayer_UnloadScene);
            step.AddButton(this.buttonVrightplayer_UnloadScene);
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

            this.setDatasetItems();

            this.selectDataList();
        }

        private void setDatasetItems() {
            if (this.selectedDataset is DatasetContent) {
                this.textBoxQuestion_00.Text = this.selectedDataset.ItemList[0].Keyword;
                this.textBoxQuestion_01.Text = this.selectedDataset.ItemList[1].Keyword;
                this.textBoxQuestion_02.Text = this.selectedDataset.ItemList[2].Keyword;
                this.textBoxSolution.Text = this.selectedDataset.Solution.ToString();
            }
            else {
                this.textBoxQuestion_00.Text = string.Empty;
                this.textBoxQuestion_01.Text = string.Empty;
                this.textBoxQuestion_02.Text = string.Empty;
                this.textBoxSolution.Text = string.Empty;
            }
        }

        private void setClosestPlayer() {
            switch (this.business.ClosestPlayer) {
                case Business.PlayerSelection.NotSelected:
                    this.textBoxLeftPlayerOffset.BackColor = SystemColors.Control;
                    this.textBoxRightPlayerOffset.BackColor = SystemColors.Control;
                    break;
                case Business.PlayerSelection.LeftPlayer:
                    this.textBoxLeftPlayerOffset.BackColor = Constants.ColorWinner;
                    this.textBoxRightPlayerOffset.BackColor = SystemColors.Control;
                    break;
                case Business.PlayerSelection.RightPlayer:
                    this.textBoxLeftPlayerOffset.BackColor = SystemColors.Control;
                    this.textBoxRightPlayerOffset.BackColor = Constants.ColorWinner;
                    break;
                case Business.PlayerSelection.BothPlayers:
                    this.textBoxLeftPlayerOffset.BackColor = Constants.ColorWinner;
                    this.textBoxRightPlayerOffset.BackColor = Constants.ColorWinner;
                    break;
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
                else if (e.PropertyName == "ClosestPlayer") this.setClosestPlayer();
            }
        }

        void selectedDataset_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.selectedDataset_PropertyChanged(sender, e)));
            else {
                this.setDatasetItems();
            }
        }

        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void buttonLeftPlayerContentIn_Click(object sender, EventArgs e) { this.business.Vleftplayer_ContentIn(); }
        private void buttonLeftPlayerContentOut_Click(object sender, EventArgs e) { this.business.Vleftplayer_ContentOut(); }
        private void buttonLeftPlayerResetContent_Click(object sender, EventArgs e) { this.business.Vleftplayer_ResetInput(); }
        private void buttonLeftPlayerStartContent_Click(object sender, EventArgs e) { this.business.Vleftplayer_StartInput(); }
        private void buttonLeftPlayerReleaseContent_Click(object sender, EventArgs e) { this.business.Vleftplayer_ReleaseInput(); }
        private void buttonLeftPlayerLockContent_Click(object sender, EventArgs e) { this.business.Vleftplayer_LockInput(); }

        private void buttonRightPlayerContentIn_Click(object sender, EventArgs e) { this.business.Vrightplayer_ContentIn(); }
        private void buttonRightPlayerContentOut_Click(object sender, EventArgs e) { this.business.Vrightplayer_ContentOut(); }
        private void buttonRightPlayerResetContent_Click(object sender, EventArgs e) { this.business.Vrightplayer_ResetInput(); }
        private void buttonRightPlayerStartContent_Click(object sender, EventArgs e) { this.business.Vrightplayer_StartInput(); }
        private void buttonRightPlayerReleaseContent_Click(object sender, EventArgs e) { this.business.Vrightplayer_ReleaseInput(); }
        private void buttonRightPlayerLockContent_Click(object sender, EventArgs e) { this.business.Vrightplayer_LockInput(); }

        private void listBoxDataList_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectDataset(this.listBoxDataList.SelectedIndex); }

        private void buttonVinsert_TextInsertIn_Click(object sender, EventArgs e) { this.business.Vinsert_TextContentIn(); }
        private void buttonVinsert_TextInsertOut_Click(object sender, EventArgs e) { this.business.Vinsert_TextContentOut(); }
        private void buttonVinsert_ContentIn_Click(object sender, EventArgs e) { this.business.Vinsert_ContentIn(); }
        private void buttonVinsert_ShowSolutions_Click(object sender, EventArgs e) { this.business.Vinsert_ShowSolutions(); }
        private void buttonVinsert_ShowSolution_1_Click(object sender, EventArgs e) { this.business.Vinsert_ShowSolution(1); }
        private void buttonVinsert_ShowSolution_2_Click(object sender, EventArgs e) { this.business.Vinsert_ShowSolution(2); }
        private void buttonVinsert_ShowSolution_3_Click(object sender, EventArgs e) { this.business.Vinsert_ShowSolution(3); }
        private void buttonVinsert_Resolve_Click(object sender, EventArgs e) { this.business.Vinsert_Resolve(); }
        private void buttonVinsert_ContentOut_Click(object sender, EventArgs e) { this.business.Vinsert_ContentOut(); }

        private void buttonVstage_ContentIn_Click(object sender, EventArgs e) { this.business.Vstage_ContentIn(); }
        private void buttonVstage_ContentOut_Click(object sender, EventArgs e) { this.business.Vstage_ContentOut(); }

        private void buttonVhost_SetInput_Click(object sender, EventArgs e) {
            this.business.Vhost_SetLeftPlayerInput();
            this.business.Vhost_SetRightPlayerInput();
        }

        private void buttonVplayers_Start_Click(object sender, EventArgs e) {
            this.business.Vleftplayer_StartInput();
            this.business.Vrightplayer_StartInput();
        }
        private void buttonVplayers_Lock_Click(object sender, EventArgs e) {
            this.business.Vleftplayer_LockInput();
            this.business.Vrightplayer_LockInput();
        }

        private void buttonGame_Resolve_Click(object sender, EventArgs e) { this.business.Resolve(); }
        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }

        #endregion

    }

}
