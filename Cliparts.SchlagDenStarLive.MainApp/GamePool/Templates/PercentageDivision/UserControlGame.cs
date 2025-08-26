using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.PercentageDivision {

    public partial class UserControlGame : _Base.Score.UserControlGame {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.numericUpDownLeftPlayerInputA.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerInputA.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerInputA.Minimum = int.MinValue;
            this.numericUpDownRightPlayerInputA.Maximum = int.MaxValue;

            this.numericUpDownVotingA.Minimum = int.MinValue;
            this.numericUpDownVotingA.Maximum = int.MaxValue;

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.business, "LeftPlayerInputA");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerInputA.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "BestPlayer");
            bind.Format += (s, e) => { e.Value = ((Content.Gameboard.PlayerSelection)e.Value) == Content.Gameboard.PlayerSelection.LeftPlayer ? Constants.ColorEnabled : SystemColors.Control ; };
            this.numericUpDownLeftPlayerInputA.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "LeftPlayerInputB");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxLeftPlayerInputB.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "BestPlayer");
            bind.Format += (s, e) => { e.Value = ((Content.Gameboard.PlayerSelection)e.Value) == Content.Gameboard.PlayerSelection.LeftPlayer ? Constants.ColorEnabled : SystemColors.Control; };
            this.textBoxLeftPlayerInputB.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerInputA");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerInputA.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "BestPlayer");
            bind.Format += (s, e) => { e.Value = ((Content.Gameboard.PlayerSelection)e.Value) == Content.Gameboard.PlayerSelection.RightPlayer ? Constants.ColorEnabled : SystemColors.Control; };
            this.numericUpDownRightPlayerInputA.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerInputB");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxRightPlayerInputB.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "BestPlayer");
            bind.Format += (s, e) => { e.Value = ((Content.Gameboard.PlayerSelection)e.Value) == Content.Gameboard.PlayerSelection.RightPlayer ? Constants.ColorEnabled : SystemColors.Control; };
            this.textBoxRightPlayerInputB.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "VotingA");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownVotingA.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "VotingB");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxVotingB.DataBindings.Add(bind);

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

            this.numericUpDownLeftPlayerInputA.DataBindings.Clear();
            this.textBoxLeftPlayerInputB.DataBindings.Clear();
            this.numericUpDownRightPlayerInputA.DataBindings.Clear();
            this.textBoxRightPlayerInputB.DataBindings.Clear();
            this.numericUpDownVotingA.DataBindings.Clear();
            this.textBoxVotingB.DataBindings.Clear();

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
            step.AddButton(this.buttonVfullscreen_ShowGame);
            step.AddButton(this.buttonVstage_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_BorderIn);
            step.AddButton(this.buttonVplayers_EnableInput);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_ContentIn);
            step.AddButton(this.buttonVfullscreen_TextIn);
            step.AddButton(this.buttonVhost_ContentIn);
            step.AddButton(this.buttonVplayers_ContentIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_StartCountDown);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_StopCountDown);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_BorderValuesIn);
            step.AddButton(this.buttonVhost_SetInput);
            step.AddButton(this.buttonVplayers_DisableInput);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_TextOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_VotingIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_Resolve);
            step.AddButton(this.buttonVfullscreen_CountVotingUp);
            step.AddButton(this.buttonVhost_Resolve);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_SetWinner);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_SetScore);
            step.AddButton(this.buttonVstage_SetScore);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_BorderOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ScoreIn);
            step.AddButton(this.buttonVhost_ContentOut);
            step.AddButton(this.buttonVplayers_ContentOut);
            step.AddButton(this.buttonVfullscreen_ContentOut);
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
            step.AddButton(this.buttonVfullscreen_UnloadScene);
            step.AddButton(this.buttonVhost_UnloadScene);
            step.AddButton(this.buttonVleftplayer_UnloadScene);
            step.AddButton(this.buttonVrightplayer_UnloadScene);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
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
                this.pictureBoxDatasetPicture.Enabled = true;
                this.pictureBoxDatasetPicture.Image = this.selectedDataset.Picture;
            }
            else {
                this.pictureBoxDatasetPicture.Enabled = false;
                this.pictureBoxDatasetPicture.Image = null;
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
                if (e.PropertyName == "PictureFilename") this.pictureBoxDatasetPicture.Image = this.selectedDataset.Picture;
            }
        }


        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void numericUpDownLeftPlayerInputA_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerInputA = (int)this.numericUpDownLeftPlayerInputA.Value; }
        private void numericUpDownRightPlayerInputA_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerInputA = (int)this.numericUpDownRightPlayerInputA.Value; }

        private void numericUpDownVotingA_ValueChanged(object sender, EventArgs e) { this.business.VotingA = (int)this.numericUpDownVotingA.Value; }

        private void listBoxDataList_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectDataset(this.listBoxDataList.SelectedIndex); }

        private void buttonVinsert_BorderIn_Click(object sender, EventArgs e) { this.business.Vinsert_BorderIn(); }
        private void buttonVinsert_BorderValuesIn_Click(object sender, EventArgs e) { this.business.Vinsert_BorderValuesIn(); }
        private void buttonVinsert_SetWinner_Click(object sender, EventArgs e) { this.business.Vinsert_SetBestPlayer(); }
        private void buttonVinsert_BorderOut_Click(object sender, EventArgs e) { this.business.Vinsert_BorderOut(); }
        protected override void buttonVinsert_SetScore_Click(object sender, EventArgs e) {
            base.buttonVinsert_SetScore_Click(sender, e);
            this.business.Vinsert_SetBorder();
        }

        private void buttonVfullscreen_ContentIn_Click(object sender, EventArgs e) { this.business.Vfullscreen_ContentIn(); }
        private void buttonVfullscreen_TextIn_Click(object sender, EventArgs e) { this.business.Vfullscreen_TextIn(); }
        private void buttonVfullscreen_TextOut_Click(object sender, EventArgs e) { this.business.Vfullscreen_TextOut(); }
        private void buttonVfullscreen_VotingIn_Click(object sender, EventArgs e) { this.business.Vfullscreen_VotingIn(); }
        private void buttonVfullscreen_CountVotingUp_Click(object sender, EventArgs e) { this.business.Vfullscreen_CountVotingUp(); }
        private void buttonVfullscreen_StartCountDown_Click(object sender, EventArgs e) { this.business.Vfullscreen_StartCountDown(); }
        private void buttonVfullscreen_StopCountDown_Click(object sender, EventArgs e) { this.business.Vfullscreen_StopCountDown(); }
        private void buttonVfullscreen_ContentOut_Click(object sender, EventArgs e) { this.business.Vfullscreen_ContentOut(); }

        private void buttonVhost_ContentIn_Click(object sender, EventArgs e) { this.business.Vhost_In(); }
        private void buttonVhost_SetInput_Click(object sender, EventArgs e) {
            this.business.Vhost_ShowLeftPlayerValue();
            this.business.Vhost_ShowRightPlayerValue();
        }
        private void buttonVhost_Resolve_Click(object sender, EventArgs e) { this.business.Vhost_Resolve(); }
        private void buttonVhost_ContentOut_Click(object sender, EventArgs e) { this.business.Vhost_Out(); }

        private void buttonVplayers_EnableInput_Click(object sender, EventArgs e) { this.business.Vplayers_EnableInput(); }
        private void buttonVplayers_ContentIn_Click(object sender, EventArgs e) { this.business.Vplayers_In(); }
        private void buttonVplayers_DisableInput_Click(object sender, EventArgs e) { this.business.Vplayers_DisableInput(); }
        private void buttonVplayers_ContentOut_Click(object sender, EventArgs e) { this.business.Vplayers_Out(); }

        private void buttonGame_Resolve_Click(object sender, EventArgs e) { this.business.Resolve(); }
        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }

        #endregion
    }
}
