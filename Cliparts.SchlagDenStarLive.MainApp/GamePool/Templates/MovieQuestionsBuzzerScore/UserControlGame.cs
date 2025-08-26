using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.MovieQuestionsBuzzerScore {

    public partial class UserControlGame : _Base.BuzzerScore.UserControlGame {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.textBoxLeftPlayerName_1.BackColor = this.textBoxLeftPlayerName.BackColor;
            this.textBoxRightPlayerName_1.BackColor = this.textBoxRightPlayerName.BackColor;

            this.numericUpDownLeftPlayerCounter.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerCounter.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerCounter.Minimum = int.MinValue;
            this.numericUpDownRightPlayerCounter.Maximum = int.MaxValue;

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

            bind = new Binding("Value", this.business, "TaskCounter");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTaskCounter.DataBindings.Add(bind);

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

            this.textBoxLeftPlayerName_1.DataBindings.Clear();
            this.textBoxRightPlayerName_1.DataBindings.Clear();

            this.numericUpDownLeftPlayerCounter.DataBindings.Clear();
            this.numericUpDownRightPlayerCounter.DataBindings.Clear();

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
            step.AddButton(this.buttonVstage_Init);
            step.AddButton(this.buttonVfullscreen_ShowGame);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_ContentIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_StartContent);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_CounterIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_ReleaseBuzzer);
            step.AddButton(this.buttonVhost_ContentIn);
            step.AddButton(this.buttonVinsert_TextInsertIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_StopTimeout);
            step.AddButton(this.buttonGame_LockBuzzer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TextInsertSolutionIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, isLastQuestion);
            step.AddButton(this.buttonVinsert_TextInsertOut);
            step.AddButton(this.buttonVhost_ContentOut);
            step.AddButton(this.buttonGame_NextQuestion);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_CounterOut);
            step.AddButton(this.buttonVfullscreen_ContentOut);
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
            step = new stepAction(index, (stepIndex) => stepIndex - 10);
            step.AddButton(this.buttonVinsert_ScoreOut);
            step.AddButton(this.buttonGame_Next);
            this.stepList.Add(step);

            index++;
            this.winnerStepIndex = index;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVgraphic_Clear);
            step.AddButton(this.buttonVstage_Clear);
            step.AddButton(this.buttonVinsert_UnloadScene);
            step.AddButton(this.buttonVfullscreen_UnloadScene);
            step.AddButton(this.buttonVhost_UnloadScene);
            step.AddButton(this.buttonVleftplayer_UnloadScene);
            step.AddButton(this.buttonVrightplayer_UnloadScene);
            step.AddButton(this.buttonVfullscreen_ShowGameboard);
            step.AddButton(this.buttonGame_SetWinner);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex);
            step.AddButton(this.buttonVfullscreen_UpdateGameboard);
            this.stepList.Add(step);

            if (index < this.stepsMaxIndex) this.on_Error("buildStepList", "stepList to short");
            if (index > this.stepsMaxIndex) this.on_Error("buildStepList", "stepList to long");
        }
        private int isLastQuestion(
            int index) {
            if (this.business.LastQuestion) return index + 1;
            else return index - 3;
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
                this.pictureBoxDatasetPicture.Image = this.selectedDataset.Movie;
            }
            else {
                this.pictureBoxDatasetPicture.Enabled = false;
                this.pictureBoxDatasetPicture.Image = null;
            }

            this.selectDataList();

            this.fillDataQuestionList();
        }

        private void fillDataQuestionList() {
            this.listBoxDataQuestionList.BeginUpdate();
            this.listBoxDataQuestionList.Items.Clear();

            int id = 1;
            if (this.selectedDataset is DatasetContent) {
                this.listBoxDataQuestionList.Enabled = true;
                foreach (DatasetQuestion item in this.selectedDataset.QuestionList) {
                    this.listBoxDataQuestionList.Items.Add(string.Format("{0}: {1}", id.ToString("00"), item));
                    id++;
                }
                this.selectQuestion(this.selectedDataset.SelectedQuestionIndex);
            }
            else {
                this.listBoxDataQuestionList.Enabled = false;
                this.selectQuestion(0);
            }
            this.listBoxDataQuestionList.EndUpdate();
        }

        private void selectQuestion(
            int index) {
            DatasetQuestion selectedQuestion = null;
            int selectedIndex = -1;
            if (this.selectedDataset is DatasetContent) {
                this.selectedDataset.SelectQuestion(index);
                selectedQuestion = this.selectedDataset.SelectedQuestion;
                selectedIndex = this.selectedDataset.SelectedQuestionIndex;
            }
            if (selectedIndex >= 0 &&
                selectedIndex < this.listBoxDataQuestionList.Items.Count) this.listBoxDataQuestionList.SelectedIndex = selectedIndex;
            if (selectedQuestion is DatasetQuestion) {
                this.textBoxDataQuestionAnswer.Text = selectedQuestion.Answer;
                this.textBoxDataQuestionAnswer.Enabled = true;
            }
            else {
                this.textBoxDataQuestionAnswer.Text = string.Empty;
                this.textBoxDataQuestionAnswer.Enabled = false;
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
                if (e.PropertyName == "PictureFilename") this.pictureBoxDatasetPicture.Image = this.selectedDataset.Movie;
                else if (e.PropertyName == "SelectedQuestionIndex") this.selectQuestion(this.selectedDataset.SelectedQuestionIndex);
            }
        }

        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        protected virtual void numericUpDownLeftPlayerCounter_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerCounter = (int)this.numericUpDownLeftPlayerCounter.Value; }
        protected virtual void buttonLeftPlayerAddCounterHot_01_Click(object sender, EventArgs e) {
            this.business.LeftPlayerCounter++;
            this.business.Vinsert_SetCounter();
        }
        private void buttonLeftPlayerSubtractCounterHot_01_Click(object sender, EventArgs e) {
            this.business.LeftPlayerCounter--;
            this.business.Vinsert_SetCounter();
        }

        protected virtual void numericUpDownRightPlayerCounter_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerCounter = (int)this.numericUpDownRightPlayerCounter.Value; }
        protected virtual void buttonRightPlayerAddCounterHot_01_Click(object sender, EventArgs e) {
            this.business.RightPlayerCounter++;
            this.business.Vinsert_SetCounter();
        }
        private void buttonRightPlayerSubtractCounterHot_01_Click(object sender, EventArgs e) {
            this.business.RightPlayerCounter--;
            this.business.Vinsert_SetCounter();
        }

        private void numericUpDownTaskCounter_ValueChanged(object sender, EventArgs e) { this.business.TaskCounter = (int)this.numericUpDownTaskCounter.Value; }

        private void buttonTrue_Click(object sender, EventArgs e) { this.business.True(); }
        private void buttonFalse_Click(object sender, EventArgs e) { this.business.False(); }

        private void listBoxDataList_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectDataset(this.listBoxDataList.SelectedIndex); }

        private void listBoxDataQuestionList_SelectedIndexChanged(object sender, EventArgs e) { this.selectQuestion(this.listBoxDataQuestionList.SelectedIndex); }

        private void buttonVinsert_CounterIn_Click(object sender, EventArgs e) { this.business.Vinsert_CounterIn(); }
        private void buttonVinsert_TextInsertIn_Click(object sender, EventArgs e) { this.business.Vinsert_TextInsertIn(); }
        private void buttonVinsert_TextInsertSolutionIn_Click(object sender, EventArgs e) { this.business.Vinsert_TextInsertSolutionIn(); }
        private void buttonVinsert_TextInsertOut_Click(object sender, EventArgs e) { this.business.Vinsert_TextInsertOut(); }
        private void buttonVinsert_CounterOut_Click(object sender, EventArgs e) { this.business.Vinsert_CounterOut(); }

        private void buttonVfullscreen_ContentIn_Click(object sender, EventArgs e) { this.business.Vfullscreen_ContentIn(); }
        private void buttonVfullscreen_StartContent_Click(object sender, EventArgs e) { this.business.Vfullscreen_StartContent(); }
        private void buttonVfullscreen_ContentOut_Click(object sender, EventArgs e) { this.business.Vfullscreen_ContentOut(); }

        private void buttonVhost_ContentIn_Click(object sender, EventArgs e) { this.business.Vhost_In(); }
        private void buttonVhost_ContentOut_Click(object sender, EventArgs e) { this.business.Vhost_Out(); }

        private void buttonVplayers_TextInsertIn_Click(object sender, EventArgs e) {
            this.business.Vleftplayer_TextInsertIn();
            this.business.Vrightplayer_TextInsertIn();
        }
        private void buttonVplayers_TextInsertOut_Click(object sender, EventArgs e) {
            this.business.Vleftplayer_TextInsertOut();
            this.business.Vrightplayer_TextInsertOut();
        }

        private void buttonGame_NextQuestion_Click(object sender, EventArgs e) { this.business.NextQuestion(); }
        private void buttonGame_Resolve_Click(object sender, EventArgs e) { this.business.Resolve(); }

        #endregion
    }
}
