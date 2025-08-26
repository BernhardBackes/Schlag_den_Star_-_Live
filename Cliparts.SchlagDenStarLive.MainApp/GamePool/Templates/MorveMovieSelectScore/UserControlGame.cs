using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MorveMovieSelectScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.MorveMovieSelectScore {

    public partial class UserControlGame : _Base.Score.UserControlGame {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.comboBoxLeftPlayerSelection.BeginUpdate();
            this.comboBoxLeftPlayerSelection.Items.Clear();
            this.comboBoxLeftPlayerSelection.Items.AddRange(Enum.GetNames(typeof(Player.SelectionValues)));
            this.comboBoxLeftPlayerSelection.EndUpdate();

            this.comboBoxRightPlayerSelection.BeginUpdate();
            this.comboBoxRightPlayerSelection.Items.Clear();
            this.comboBoxRightPlayerSelection.Items.AddRange(Enum.GetNames(typeof(Player.SelectionValues)));
            this.comboBoxRightPlayerSelection.EndUpdate();

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

            bind = new Binding("BackColor", this.business, "BuzzerMode");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorSelected : SystemColors.Control; };
            this.checkBoxBuzzerMode.DataBindings.Add(bind);

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

            this.comboBoxLeftPlayerSelection.DataBindings.Clear();
            this.comboBoxRightPlayerSelection.DataBindings.Clear();

            this.numericUpDownTaskCounter.DataBindings.Clear();

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
            step.AddButton(this.buttonVstage_ContentIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_Start);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_BorderSelectionIn);
            step.AddButton(this.buttonVstage_SetContent);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_ShowSolution);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ResolveBorderSelection);
            step.AddButton(this.buttonVfullscreen_Resolve);
            step.AddButton(this.buttonGame_Resolve);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_BorderOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ScoreIn);
            step.AddButton(this.buttonVstage_SetScore);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ScoreOut);
            step.AddButton(this.buttonVstage_ContentOut);
            step.AddButton(this.buttonVfullscreen_ContentOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 8);
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
                this.pictureBoxDatasetPicture.Image = this.selectedDataset.Movie;
                this.textBoxDatasetSolution.Enabled = true;
                this.textBoxDatasetSolution.Text = this.selectedDataset.Solution.ToString();
            }
            else {
                this.pictureBoxDatasetPicture.Enabled = false;
                this.pictureBoxDatasetPicture.Image = null;
                this.textBoxDatasetSolution.Enabled = false;
                this.textBoxDatasetSolution.Text = string.Empty;
            }

            this.selectDataList();

            this.setComboBoxPlayerSelection(this.comboBoxLeftPlayerSelection, this.business.SelectedDataset, this.business.LeftPlayerSelection);
            this.setComboBoxPlayerSelection(this.comboBoxRightPlayerSelection, this.business.SelectedDataset, this.business.RightPlayerSelection);
        }

        private void setComboBoxPlayerSelection(
            ComboBox control,
            DatasetContent selectedDataset,
            Player.SelectionValues playerSelection) {
            if (control is ComboBox) {
                if (selectedDataset is DatasetContent) {
                    control.Enabled = true;
                    if (playerSelection == Player.SelectionValues.NotSelected) control.BackColor = SystemColors.Control;
                    else if (playerSelection == selectedDataset.Solution) control.BackColor = Constants.ColorEnabled;
                    else control.BackColor = Constants.ColorDisabled;
                }
                else {
                    control.Enabled = false;
                    control.BackColor = SystemColors.Control;
                }
                control.Text = playerSelection.ToString();
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
                else if (e.PropertyName == "LeftPlayerSelection") this.setComboBoxPlayerSelection(this.comboBoxLeftPlayerSelection, this.business.SelectedDataset, this.business.LeftPlayerSelection);
                else if (e.PropertyName == "RightPlayerSelection") this.setComboBoxPlayerSelection(this.comboBoxRightPlayerSelection, this.business.SelectedDataset, this.business.RightPlayerSelection);
            }
        }

        void selectedDataset_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.selectedDataset_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "PictureFilename") this.pictureBoxDatasetPicture.Image = this.selectedDataset.Movie;
                else if (e.PropertyName == "Solution") {
                    this.textBoxDatasetSolution.Text = this.selectedDataset.Solution.ToString();
                    this.setComboBoxPlayerSelection(this.comboBoxLeftPlayerSelection, this.business.SelectedDataset, this.business.LeftPlayerSelection);
                    this.setComboBoxPlayerSelection(this.comboBoxRightPlayerSelection, this.business.SelectedDataset, this.business.RightPlayerSelection);
                }

            }
        }

        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        protected override void buttonLeftPlayerAddScoreHot_01_Click(object sender, EventArgs e) {
            base.buttonLeftPlayerAddScoreHot_01_Click(sender, e);
            this.business.Vinsert_SetBorder();
        }
        private void comboBoxLeftPlayerSelection_SelectedIndexChanged(object sender, EventArgs e) {
            Player.SelectionValues selection;
            if (Enum.TryParse(this.comboBoxLeftPlayerSelection.Text, out selection)) this.business.LeftPlayerSelection = selection;
        }
        private void buttonLeftPlayerEnableTouch_Click(object sender, EventArgs e) { this.business.VleftPlayer_EnableTouch(); }
        private void buttonLeftPlayerDisableTouch_Click(object sender, EventArgs e) { this.business.VleftPlayer_DisableTouch(); }

        protected override void buttonRightPlayerAddScoreHot_01_Click(object sender, EventArgs e) {
            base.buttonRightPlayerAddScoreHot_01_Click(sender, e);
            this.business.Vinsert_SetBorder();
        }
        private void comboBoxRightPlayerSelection_SelectedIndexChanged(object sender, EventArgs e) {
            Player.SelectionValues selection;
            if (Enum.TryParse(this.comboBoxRightPlayerSelection.Text, out selection)) this.business.RightPlayerSelection = selection;
        }
        private void buttonRightPlayerEnableTouch_Click(object sender, EventArgs e) { this.business.VrightPlayer_EnableTouch(); }
        private void buttonRightPlayerDisableTouch_Click(object sender, EventArgs e) { this.business.VrightPlayer_DisableTouch(); }

        private void checkBoxBuzzerMode_CheckedChanged(object sender, EventArgs e) { this.business.BuzzerMode = this.checkBoxBuzzerMode.Checked; }

        private void numericUpDownTaskCounter_ValueChanged(object sender, EventArgs e) { this.business.TaskCounter = (int)this.numericUpDownTaskCounter.Value; }

        private void listBoxDataList_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectDataset(this.listBoxDataList.SelectedIndex); }

        private void buttonVinsert_BorderIn_Click(object sender, EventArgs e) { this.business.Vinsert_BorderIn(); }
        private void buttonVinsert_BorderSelectionIn_Click(object sender, EventArgs e) { this.business.Vinsert_BorderSelectionIn(); }
        private void buttonVinsert_ResolveBorderSelection_Click(object sender, EventArgs e) { this.business.Vinsert_ResolveBorderSelection(); }
        private void buttonVinsert_BorderOut_Click(object sender, EventArgs e) { this.business.Vinsert_BorderOut(); }

        private void buttonVfullscreen_ContentIn_Click(object sender, EventArgs e) { this.business.Vfullscreen_ContentIn(); }
        private void buttonVfullscreen_Start_Click(object sender, EventArgs e) { this.business.Vfullscreen_Start(); }
        private void buttonVfullscreen_ShowSolution_Click(object sender, EventArgs e) { this.business.Vfullscreen_ShowSolution(); }
        private void buttonVfullscreen_Resolve_Click(object sender, EventArgs e) { this.business.Vfullscreen_Resolve(); }
        private void buttonVfullscreen_ContentOut_Click(object sender, EventArgs e) { this.business.Vfullscreen_ContentOut(); }

        private void buttonVstage_ContentIn_Click(object sender, EventArgs e) { this.business.Vstage_ContentIn(); }
        private void buttonVstage_SetContent_Click(object sender, EventArgs e) { this.business.Vstage_SetContent(); }
        private void buttonVstage_ContentOut_Click(object sender, EventArgs e) { this.business.Vstage_ContentOut(); }
        private void buttonVstage_InitBuzzer_Click(object sender, EventArgs e) { this.business.Vstage_InitBuzzer(); }

        private void buttonGame_Resolve_Click(object sender, EventArgs e) { this.business.Resolve(); }
        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }

        #endregion

    }
}
