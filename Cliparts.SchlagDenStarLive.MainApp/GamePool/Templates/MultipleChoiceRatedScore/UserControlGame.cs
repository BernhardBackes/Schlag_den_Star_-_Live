using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MultipleChoiceRatedScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.MultipleChoiceRatedScore {

    public partial class UserControlGame : _Base.TimerScore.UserControlGame {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.numericUpDownLeftPlayerValueA.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerValueA.Maximum = int.MaxValue;

            this.numericUpDownLeftPlayerValueB.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerValueB.Maximum = int.MaxValue;

            this.numericUpDownLeftPlayerValueC.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerValueC.Maximum = int.MaxValue;

            this.numericUpDownLeftPlayerValueD.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerValueD.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerValueA.Minimum = int.MinValue;
            this.numericUpDownRightPlayerValueA.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerValueB.Minimum = int.MinValue;
            this.numericUpDownRightPlayerValueB.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerValueC.Minimum = int.MinValue;
            this.numericUpDownRightPlayerValueC.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerValueD.Minimum = int.MinValue;
            this.numericUpDownRightPlayerValueD.Maximum = int.MaxValue;

            this.numericUpDownTaskCounter.Minimum = int.MinValue;
            this.numericUpDownTaskCounter.Maximum = int.MaxValue;

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.business, "TaskCounter");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTaskCounter.DataBindings.Add(bind);

            this.labelGameClass.Text = this.business.ClassInfo;

            this.fillDataList();
            this.selectDataset(this.business.SelectedDataset);

            this.setNumericUpDownPlayerValue(this.numericUpDownLeftPlayerValueA, this.business.LeftPlayerValueA);
            this.setNumericUpDownPlayerValue(this.numericUpDownLeftPlayerValueB, this.business.LeftPlayerValueB);
            this.setNumericUpDownPlayerValue(this.numericUpDownLeftPlayerValueC, this.business.LeftPlayerValueC);
            this.setNumericUpDownPlayerValue(this.numericUpDownLeftPlayerValueD, this.business.LeftPlayerValueD);
            this.setNumericUpDownPlayerValue(this.numericUpDownRightPlayerValueA, this.business.RightPlayerValueA);
            this.setNumericUpDownPlayerValue(this.numericUpDownRightPlayerValueB, this.business.RightPlayerValueB);
            this.setNumericUpDownPlayerValue(this.numericUpDownRightPlayerValueC, this.business.RightPlayerValueC);
            this.setNumericUpDownPlayerValue(this.numericUpDownRightPlayerValueD, this.business.RightPlayerValueD);
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

            this.numericUpDownTaskCounter.DataBindings.Clear();
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
            step.AddButton(this.buttonVinsert_ScoreIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ContentTextIn);
            step.AddButton(this.buttonVfullscreen_ContentIn);
            step.AddButton(this.buttonVstage_ContentIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ContentAnswersIn);
            step.AddButton(this.buttonVleftplayer_Unlock);
            step.AddButton(this.buttonVrightplayer_Unlock);
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
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_StopTimer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimerOut);
            step.AddButton(this.buttonVhost_SetContent);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 2);
            step.AddButton(this.buttonVinsert_ContentInputIn);
            step.AddButton(this.buttonVleftplayer_Lock);
            step.AddButton(this.buttonVrightplayer_Lock);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_Start);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ContentResolve);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_Resolve);
            step.AddButton(this.buttonVinsert_SetScore);
            step.AddButton(this.buttonVstage_SetScore);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 10);
            step.AddButton(this.buttonVinsert_ContentOut);
            step.AddButton(this.buttonVfullscreen_ContentOut);
            step.AddButton(this.buttonVstage_ContentOut);
            step.AddButton(this.buttonGame_Next);
            this.stepList.Add(step);

            index++;
            this.winnerStepIndex = index;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVgraphic_Clear);
            step.AddButton(this.buttonVinsert_ScoreOut);
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
                this.numericUpDownLeftPlayerValueA.BackColor = SystemColors.Control;
                this.numericUpDownLeftPlayerValueB.BackColor = SystemColors.Control;
                this.numericUpDownLeftPlayerValueC.BackColor = SystemColors.Control;
                this.numericUpDownLeftPlayerValueD.BackColor = SystemColors.Control;
                this.numericUpDownRightPlayerValueA.BackColor = SystemColors.Control;
                this.numericUpDownRightPlayerValueB.BackColor = SystemColors.Control;
                this.numericUpDownRightPlayerValueC.BackColor = SystemColors.Control;
                this.numericUpDownRightPlayerValueD.BackColor = SystemColors.Control;
                //Dispose...
                if (this.selectedDataset is DatasetContent) {
                    this.selectedDataset.PropertyChanged -= this.selectedDataset_PropertyChanged;
                }
                this.selectedDataset = selectedDataset;
                //Pose...
                if (this.selectedDataset is DatasetContent) {
                    this.selectedDataset.PropertyChanged += this.selectedDataset_PropertyChanged;
                    switch (this.selectedDataset.Solution) {
                        case Game.SolutionItems.AnswerA:
                            this.numericUpDownLeftPlayerValueA.BackColor = Constants.ColorEnabled;
                            this.numericUpDownRightPlayerValueA.BackColor = Constants.ColorEnabled;
                            break;
                        case Game.SolutionItems.AnswerB:
                            this.numericUpDownLeftPlayerValueB.BackColor = Constants.ColorEnabled;
                            this.numericUpDownRightPlayerValueB.BackColor = Constants.ColorEnabled;
                            break;
                        case Game.SolutionItems.AnswerC:
                            this.numericUpDownLeftPlayerValueC.BackColor = Constants.ColorEnabled;
                            this.numericUpDownRightPlayerValueC.BackColor = Constants.ColorEnabled;
                            break;
                        case Game.SolutionItems.AnswerD:
                            this.numericUpDownLeftPlayerValueD.BackColor = Constants.ColorEnabled;
                            this.numericUpDownRightPlayerValueD.BackColor = Constants.ColorEnabled;
                            break;
                    }
                }
            }

            this.selectDataList();
        }

        private void setNumericUpDownPlayerValue(
            NumericUpDown control,
            int? value) {
            if (value.HasValue) control.Value = value.Value;
            else {
                control.Value = 0;
                control.ResetText();
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
                else if (e.PropertyName == "LeftPlayerValueA") this.setNumericUpDownPlayerValue(this.numericUpDownLeftPlayerValueA, this.business.LeftPlayerValueA);
                else if (e.PropertyName == "LeftPlayerValueB") this.setNumericUpDownPlayerValue(this.numericUpDownLeftPlayerValueB, this.business.LeftPlayerValueB);
                else if (e.PropertyName == "LeftPlayerValueC") this.setNumericUpDownPlayerValue(this.numericUpDownLeftPlayerValueC, this.business.LeftPlayerValueC);
                else if (e.PropertyName == "LeftPlayerValueD") this.setNumericUpDownPlayerValue(this.numericUpDownLeftPlayerValueD, this.business.LeftPlayerValueD);
                else if (e.PropertyName == "RightPlayerValueA") this.setNumericUpDownPlayerValue(this.numericUpDownRightPlayerValueA, this.business.RightPlayerValueA);
                else if (e.PropertyName == "RightPlayerValueB") this.setNumericUpDownPlayerValue(this.numericUpDownRightPlayerValueB, this.business.RightPlayerValueB);
                else if (e.PropertyName == "RightPlayerValueC") this.setNumericUpDownPlayerValue(this.numericUpDownRightPlayerValueC, this.business.RightPlayerValueC);
                else if (e.PropertyName == "RightPlayerValueD") this.setNumericUpDownPlayerValue(this.numericUpDownRightPlayerValueD, this.business.RightPlayerValueD);
            }
        }

        void selectedDataset_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.selectedDataset_PropertyChanged(sender, e)));
            else {
                //if (e.PropertyName == "PictureFilename") this.pictureBoxDatasetPicture.Image = this.selectedDataset.TaskMovie;
                //else if (e.PropertyName == "Solution") {
                //    this.textBoxDatasetSolution.Text = this.selectedDataset.SolutionMovie.ToString();
                //}

            }
        }

        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void UserControlGame_BackColorChanged(object sender, EventArgs e) {
        }

        protected override void buttonLeftPlayerAddScoreHot_01_Click(object sender, EventArgs e) {
            base.buttonLeftPlayerAddScoreHot_01_Click(sender, e);
        }
        private void numericUpDownLeftPlayerValueA_ValueChanged(object sender, EventArgs e) { 
            this.business.LeftPlayerValueA = (int)this.numericUpDownLeftPlayerValueA.Value; 
        }
        private void numericUpDownLeftPlayerValueB_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerValueB = (int)this.numericUpDownLeftPlayerValueB.Value; }
        private void numericUpDownLeftPlayerValueC_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerValueC = (int)this.numericUpDownLeftPlayerValueC.Value; }
        private void numericUpDownLeftPlayerValueD_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerValueD = (int)this.numericUpDownLeftPlayerValueD.Value; }
        private void buttonLeftPlayerEnableTouch_Click(object sender, EventArgs e) { this.business.Vleftplayer_EnableTouch(); }
        private void buttonLeftPlayerDisableTouch_Click(object sender, EventArgs e) { this.business.Vleftplayer_DisableTouch(); }

        protected override void buttonRightPlayerAddScoreHot_01_Click(object sender, EventArgs e) {
            base.buttonRightPlayerAddScoreHot_01_Click(sender, e);
        }
        private void numericUpDownRightPlayerValueA_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerValueA = (int)this.numericUpDownRightPlayerValueA.Value; }
        private void numericUpDownRightPlayerValueB_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerValueB = (int)this.numericUpDownRightPlayerValueB.Value; }
        private void numericUpDownRightPlayerValueC_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerValueC = (int)this.numericUpDownRightPlayerValueC.Value; }
        private void numericUpDownRightPlayerValueD_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerValueD = (int)this.numericUpDownRightPlayerValueD.Value; }
        private void buttonRightPlayerEnableTouch_Click(object sender, EventArgs e) { this.business.Vrightplayer_EnableTouch(); }
        private void buttonRightPlayerDisableTouch_Click(object sender, EventArgs e) { this.business.Vrightplayer_DisableTouch(); }

        private void numericUpDownTaskCounter_ValueChanged(object sender, EventArgs e) { this.business.TaskCounter = (int)this.numericUpDownTaskCounter.Value; }

        private void listBoxDataList_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectDataset(this.listBoxDataList.SelectedIndex); }

        private void buttonVinsert_ContentTextIn_Click(object sender, EventArgs e) { this.business.Vinsert_ContentTextIn(); }
        private void buttonVinsert_ContentAnswersIn_Click(object sender, EventArgs e) { this.business.Vinsert_ContentAnswersIn(); }
        private void buttonVinsert_ContentInputIn_Click(object sender, EventArgs e) { this.business.Vinsert_ContentInputIn(); }
        private void buttonVinsert_ContentResolve_Click(object sender, EventArgs e) { this.business.Vinsert_ContentResolve(); }
        private void buttonVinsert_ContentOut_Click(object sender, EventArgs e) { this.business.Vinsert_ContentOut(); }

        private void buttonVfullscreen_ContentIn_Click(object sender, EventArgs e) { this.business.Vfullscreen_ContentIn(); }
        private void buttonVfullscreen_Start_Click(object sender, EventArgs e) { this.business.Vfullscreen_Start(); }
        private void buttonVfullscreen_ContentOut_Click(object sender, EventArgs e) { this.business.Vfullscreen_ContentOut(); }

        private void buttonVstage_ContentIn_Click(object sender, EventArgs e) { this.business.Vstage_ContentIn(); }
        private void buttonVstage_ContentOut_Click(object sender, EventArgs e) { this.business.Vstage_ContentOut(); }

        private void buttonVhost_SetContent_Click(object sender, EventArgs e) { this.business.Vhost_SetContent(); }

        private void buttonVleftPlayer_Unlock_Click(object sender, EventArgs e) { this.business.Vleftplayer_EnableTouch(); }
        private void buttonVleftplayer_Lock_Click(object sender, EventArgs e) { this.business.Vleftplayer_DisableTouch(); }

        private void buttonVrightPlayer_Unlock_Click(object sender, EventArgs e) { this.business.Vrightplayer_EnableTouch(); }
        private void buttonVrightplayer_Lock_Click(object sender, EventArgs e) { this.business.Vrightplayer_DisableTouch(); }

        private void buttonGame_Resolve_Click(object sender, EventArgs e) { this.business.Resolve(); }
        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }

        #endregion

    }
}
