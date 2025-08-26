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

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.SelectTaskTextInsertTimer
{

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

            Binding bind;

            this.labelGameClass.Text = this.business.ClassInfo;

            this.fillAvailableTasks();
            this.fillUsedTasks();
            this.selectDataset(this.business.SelectedDataset);

            this.setBuzzeredPlayer();
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
            step.AddButton(this.buttonVinsert_ScoreIn);
            step.AddButton(this.buttonVinsert_TasksIn);
            step.AddButton(this.buttonVstage_TasksIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_SelectTask);
            step.AddButton(this.buttonVstage_SelectTask);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimerIn);
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
            step.AddButton(this.buttonVfullscreen_ResetTimer);
            step.AddButton(this.buttonVinsert_TimerOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_SolutionIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_SolutionOut);
            step.AddButton(this.buttonGame_Next);
            step.AddButton(this.buttonVinsert_SetTasks);
            step.AddButton(this.buttonVstage_SetTasks);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 6);
            step.AddButton(this.buttonVinsert_SetScore);
            step.AddButton(this.buttonVstage_SetScore);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ScoreOut);
            step.AddButton(this.buttonVinsert_TasksOut);
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

        private void fillAvailableTasks()
        {
            this.listBoxAvailableTasks.BeginUpdate();
            this.listBoxAvailableTasks.Items.Clear();
            this.listBoxAvailableTasks.Items.AddRange(this.business.AvailableTasks.ToArray());
            this.listBoxAvailableTasks.EndUpdate();

            this.listBoxAvailableTasks.Enabled = this.listBoxAvailableTasks.Items.Count > 0;
            Helper.setControlBackColor(this.listBoxAvailableTasks);

            this.selectDataList();
        }

        private void fillUsedTasks()
        {
            this.listBoxUsedTasks.BeginUpdate();
            this.listBoxUsedTasks.Items.Clear();
            this.listBoxUsedTasks.Items.AddRange(this.business.UsedTasks.ToArray());
            this.listBoxUsedTasks.EndUpdate();

            this.listBoxUsedTasks.Enabled = this.listBoxUsedTasks.Items.Count > 0;
            Helper.setControlBackColor(this.listBoxUsedTasks);
        }

        private void selectDataList() {
            int index = this.business.GetDatasetIndex(this.selectedDataset);
            if (this.selectedDataset is DatasetContent &&
                this.listBoxAvailableTasks.Items.Contains(this.selectedDataset)) this.listBoxAvailableTasks.SelectedItem = this.selectedDataset;
            else this.listBoxAvailableTasks.SelectedIndex = -1;
        }

        private void selectDataset(
            DatasetContent selectedDataset) {
            this.textBoxSolution.Text = string.Empty;
            if (this.selectedDataset is DatasetContent) {
                this.selectedDataset.PropertyChanged -= this.selectedDataset_PropertyChanged;
            }
            this.selectedDataset = selectedDataset;
            //Pose...
            if (this.selectedDataset is DatasetContent)
            {
                this.selectedDataset.PropertyChanged += this.selectedDataset_PropertyChanged;
                this.textBoxSolution.Text = selectedDataset.Solution;
            }
            this.selectDataList();
        }

        private void setBuzzeredPlayer()
        {
            switch (this.business.BuzzeredPlayer)
            {
                case Content.Gameboard.PlayerSelection.LeftPlayer:
                    this.buttonLeftPlayerBuzzer.BackColor = Constants.ColorBuzzered;
                    this.buttonRightPlayerBuzzer.UseVisualStyleBackColor = true;
                    break;
                case Content.Gameboard.PlayerSelection.RightPlayer:
                    this.buttonLeftPlayerBuzzer.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerBuzzer.BackColor = Constants.ColorBuzzered;
                    break;
                case Content.Gameboard.PlayerSelection.NotSelected:
                default:
                    this.buttonLeftPlayerBuzzer.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerBuzzer.UseVisualStyleBackColor = true;
                    break;
            }
        }

        #endregion


        #region Events.Incoming

        protected override void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.business_PropertyChanged(sender, e);
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "AvailableTasks") this.fillAvailableTasks();
                else if (e.PropertyName == "UsedTasks") this.fillUsedTasks();
                else if (e.PropertyName == "SelectedDataset") this.selectDataset(this.business.SelectedDataset);
                else if (e.PropertyName == "SelectedDatasetIndex") this.selectDataList();
                else if (e.PropertyName == "BuzzeredPlayer") this.setBuzzeredPlayer();
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

        protected void buttonLeftPlayerBuzzer_Click(object sender, EventArgs e) { this.business.DoBuzzer(Content.Gameboard.PlayerSelection.LeftPlayer); }
        protected void buttonRightPlayerBuzzer_Click(object sender, EventArgs e) { this.business.DoBuzzer(Content.Gameboard.PlayerSelection.RightPlayer); }

        private void listBoxDataList_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectDataset(this.listBoxAvailableTasks.SelectedIndex); }
        private void listBoxAvailableTasks_Click(object sender, EventArgs e)
        {
            int index = this.business.GetDatasetIndex(this.listBoxAvailableTasks.SelectedItem as DatasetContent);
            this.business.SelectDataset(index);
        }
        private void buttonTaskOut_Click(object sender, EventArgs e) { this.business.TaskOut(this.listBoxAvailableTasks.SelectedItem as DatasetContent); }
        private void buttonTaskIn_Click(object sender, EventArgs e) { this.business.TaskIn(this.listBoxUsedTasks.SelectedItem as DatasetContent); }

        private void buttonTrue_Click(object sender, EventArgs e) { this.business.True(); }
        private void buttonFalse_Click(object sender, EventArgs e) { this.business.False(); }

        private void buttonVinsert_TasksIn_Click(object sender, EventArgs e) { this.business.Vinsert_TasksIn(); }
        private void buttonVinsert_SelectTask_Click(object sender, EventArgs e) { this.business.Vinsert_SelectTask(); }
        private void buttonVinsert_SolutionIn_Click(object sender, EventArgs e) { this.business.Vinsert_SolutionIn(); }
        private void buttonVinsert_SolutionOut_Click(object sender, EventArgs e) { this.business.Vinsert_SolutionOut(); }
        private void buttonVinsert_SetTasks_Click(object sender, EventArgs e) { this.business.Vinsert_SetTasks(); }
        private void buttonVinsert_TasksOut_Click(object sender, EventArgs e) { this.business.Vinsert_TasksOut(); }

        private void buttonVstage_TasksIn_Click(object sender, EventArgs e) { this.business.Vstage_TasksIn(); }
        private void buttonVstage_SelectTask_Click(object sender, EventArgs e) { this.business.Vstage_SelectTask(false); }
        private void buttonVstage_SetTasks_Click(object sender, EventArgs e) { this.business.Vstage_SetTasks(); }

        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }

        #endregion

    }
}
