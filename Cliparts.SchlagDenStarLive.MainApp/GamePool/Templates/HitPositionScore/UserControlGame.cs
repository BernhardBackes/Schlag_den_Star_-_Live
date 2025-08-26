using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.HitPositionScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.HitPositionScore {

    public partial class UserControlGame : _Base.TimerScore.UserControlGame {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.numericUpDownLeftPlayerSelectionX.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerSelectionX.Maximum = int.MaxValue;

            this.numericUpDownLeftPlayerSelectionY.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerSelectionY.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerSelectionX.Minimum = int.MinValue;
            this.numericUpDownRightPlayerSelectionX.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerSelectionY.Minimum = int.MinValue;
            this.numericUpDownRightPlayerSelectionY.Maximum = int.MaxValue;

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

            bind = new Binding("Value", this.business, "LeftPlayerSelection");
            bind.Format += (s, e) => { e.Value = ((Point)e.Value).X; };
            this.numericUpDownLeftPlayerSelectionX.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "LeftPlayerSelectionIsValid");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorEnabled : SystemColors.Control; };
            this.numericUpDownLeftPlayerSelectionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerSelection");
            bind.Format += (s, e) => { e.Value = ((Point)e.Value).Y; };
            this.numericUpDownLeftPlayerSelectionY.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "LeftPlayerSelectionIsValid");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorEnabled : SystemColors.Control; };
            this.numericUpDownLeftPlayerSelectionY.DataBindings.Add(bind);

            bind = new Binding("Enabled", this.business, "LeftPlayerSelectionIsValid");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.buttonLeftPlayerShowMarker.DataBindings.Add(bind);

            bind = new Binding("Enabled", this.business, "LeftPlayerSelectionIsValid");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.buttonLeftPlayerPlayMarker.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "LeftPlayerSelectionHits");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxLeftPlayerSelectionHits.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "LeftPlayerSelectionHits");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorEnabled : Constants.ColorDisabled; };
            this.checkBoxLeftPlayerSelectionHits.DataBindings.Add(bind);


            bind = new Binding("Value", this.business, "RightPlayerSelection");
            bind.Format += (s, e) => { e.Value = ((Point)e.Value).X; };
            this.numericUpDownRightPlayerSelectionX.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "RightPlayerSelectionIsValid");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorEnabled : SystemColors.Control; };
            this.numericUpDownRightPlayerSelectionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerSelection");
            bind.Format += (s, e) => { e.Value = ((Point)e.Value).Y; };
            this.numericUpDownRightPlayerSelectionY.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "RightPlayerSelectionIsValid");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorEnabled : SystemColors.Control; };
            this.numericUpDownRightPlayerSelectionY.DataBindings.Add(bind);

            bind = new Binding("Enabled", this.business, "RightPlayerSelectionIsValid");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.buttonRightPlayerShowMarker.DataBindings.Add(bind);

            bind = new Binding("Enabled", this.business, "RightPlayerSelectionIsValid");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.buttonRightPlayerPlayMarker.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "RightPlayerSelectionHits");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxRightPlayerSelectionHits.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "RightPlayerSelectionHits");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorEnabled : Constants.ColorDisabled; };
            this.checkBoxRightPlayerSelectionHits.DataBindings.Add(bind);

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

            this.numericUpDownLeftPlayerSelectionX.DataBindings.Clear();
            this.numericUpDownLeftPlayerSelectionY.DataBindings.Clear();
            this.buttonLeftPlayerShowMarker.DataBindings.Clear();
            this.buttonLeftPlayerPlayMarker.DataBindings.Clear();
            this.checkBoxLeftPlayerSelectionHits.DataBindings.Clear();

            this.numericUpDownRightPlayerSelectionX.DataBindings.Clear();
            this.numericUpDownRightPlayerSelectionY.DataBindings.Clear();
            this.buttonRightPlayerShowMarker.DataBindings.Clear();
            this.buttonRightPlayerPlayMarker.DataBindings.Clear();
            this.checkBoxRightPlayerSelectionHits.DataBindings.Clear();

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
            step.AddButton(this.buttonVinsert_TimerIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_StartTimer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_StopTimer);
            step.AddButton(this.buttonVplayer_EnableTouch);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimerOut);
            step.AddButton(this.buttonVfullscreen_ContentOut);
            step.AddButton(this.buttonVplayer_ContentOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_ShowSelection);
            step.AddButton(this.buttonVhost_ShowSelection);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_ContentIn_1);
            step.AddButton(this.buttonVplayer_ContentIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_BorderOut);
            step.AddButton(this.buttonVfullscreen_ResetContent);
            step.AddButton(this.buttonVstage_ResetContent); 
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ScoreIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_Resolve);
            step.AddButton(this.buttonVstage_SetScore);
            step.AddButton(this.buttonVinsert_SetScore);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex -10);
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
            step.AddButton(this.buttonVfullscreen_ShowGameboard);
            step.AddButton(this.buttonGame_SetWinner); this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex);
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
                this.pictureBoxDatasetPicture.Image = this.selectedDataset.Content;
            }
            else {
                this.pictureBoxDatasetPicture.Enabled = false;
                this.pictureBoxDatasetPicture.Image = null;
            }

            this.selectDataList();

            //this.setComboBoxPlayerSelection(this.comboBoxLeftPlayerSelection, this.business.SelectedDataset, this.business.LeftPlayerSelection);
            //this.setComboBoxPlayerSelection(this.comboBoxRightPlayerSelection, this.business.SelectedDataset, this.business.RightPlayerSelection);
        }

        private void setComboBoxPlayerSelection(
            ComboBox control,
            DatasetContent selectedDataset,
            Point? playerSelection) {
            /*if (control is ComboBox) {
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
            }*/
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
                if (e.PropertyName == "ContentFilename") this.pictureBoxDatasetPicture.Image = this.selectedDataset.Content;
            }
        }

        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void buttonGameResetZoom_Click(object sender, EventArgs e) { this.business.Vfullscreen_ResetZoom(); }

        protected override void buttonLeftPlayerAddScoreHot_01_Click(object sender, EventArgs e) {
            base.buttonLeftPlayerAddScoreHot_01_Click(sender, e);
            this.business.Vinsert_SetBorder();
        }
        private void numericUpDownLeftPlayerSelectionX_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerSelection = new Point((int)this.numericUpDownLeftPlayerSelectionX.Value, this.business.LeftPlayerSelection.Y); }           
        private void numericUpDownLeftPlayerSelectionY_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerSelection = new Point(this.business.LeftPlayerSelection.X, (int)this.numericUpDownLeftPlayerSelectionY.Value); }
        private void buttonLeftPlayerShowMarker_Click(object sender, EventArgs e) { this.business.Vfullscreen_ShowLeftPlayerMarker(); }
        private void buttonLeftPlayerPlayMarker_Click(object sender, EventArgs e) { this.business.Vfullscreen_PlayLeftPlayerMarker(); }
        private void buttonLeftPlayerZoomMarker_Click(object sender, EventArgs e) { this.business.Vfullscreen_ZoomToLeftPlayerMarker(); }
        private void buttonLeftPlayerHideMarker_Click(object sender, EventArgs e) { this.business.Vfullscreen_HideLeftPlayerMarker(); }
        private void buttonLeftPlayerEnableTouch_Click(object sender, EventArgs e) { this.business.VleftPlayer_EnableTouch(); }
        private void buttonLeftPlayerDisableTouch_Click(object sender, EventArgs e) { this.business.VleftPlayer_DisableTouch(); }
        private void checkBoxLeftPlayerSelectionHits_CheckedChanged(object sender, EventArgs e) { this.business.LeftPlayerSelectionHits = this.checkBoxLeftPlayerSelectionHits.Checked; }

        protected override void buttonRightPlayerAddScoreHot_01_Click(object sender, EventArgs e) {
            base.buttonRightPlayerAddScoreHot_01_Click(sender, e);
            this.business.Vinsert_SetBorder();
        }
        private void numericUpDownRightPlayerSelectionX_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerSelection = new Point((int)this.numericUpDownRightPlayerSelectionX.Value, this.business.RightPlayerSelection.Y); }
        private void numericUpDownRightPlayerSelectionY_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerSelection = new Point(this.business.RightPlayerSelection.X, (int)this.numericUpDownRightPlayerSelectionY.Value); }
        private void buttonRightPlayerShowMarker_Click(object sender, EventArgs e) { this.business.Vfullscreen_ShowRightPlayerMarker(); }
        private void buttonRightPlayerPlayMarker_Click(object sender, EventArgs e) { this.business.Vfullscreen_PlayRightPlayerMarker(); }
        private void buttonRightPlayerZoomMarker_Click(object sender, EventArgs e) { this.business.Vfullscreen_ZoomToRightPlayerMarker(); }
        private void buttonRightPlayerHideMarker_Click(object sender, EventArgs e) { this.business.Vfullscreen_HideRightPlayerMarker(); }
        private void buttonRightPlayerEnableTouch_Click(object sender, EventArgs e) { this.business.VrightPlayer_EnableTouch(); }
        private void buttonRightPlayerDisableTouch_Click(object sender, EventArgs e) { this.business.VrightPlayer_DisableTouch(); }
        private void checkBoxRightPlayerSelectionHits_CheckedChanged(object sender, EventArgs e) { this.business.RightPlayerSelectionHits = this.checkBoxRightPlayerSelectionHits.Checked; }

        private void checkBoxBuzzerMode_CheckedChanged(object sender, EventArgs e) { this.business.BuzzerMode = this.checkBoxBuzzerMode.Checked; }

        private void numericUpDownTaskCounter_ValueChanged(object sender, EventArgs e) { this.business.TaskCounter = (int)this.numericUpDownTaskCounter.Value; }

        private void listBoxDataList_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectDataset(this.listBoxDataList.SelectedIndex); }

        private void buttonVinsert_BorderIn_Click(object sender, EventArgs e) { this.business.Vinsert_BorderIn(); }
        private void buttonVinsert_BorderOut_Click(object sender, EventArgs e) { this.business.Vinsert_BorderOut(); }

        private void buttonVfullscreen_ContentIn_Click(object sender, EventArgs e) { this.business.Vfullscreen_ContentIn(); }
        private void buttonVfullscreen_ContentOut_Click(object sender, EventArgs e) { this.business.Vfullscreen_ContentOut(); }
        private void buttonVfullscreen_ShowSelection_Click(object sender, EventArgs e) { this.business.Vfullscreen_ShowSelection(); }
        private void buttonVfullscreen_ContentIn_1_Click(object sender, EventArgs e) { this.business.Vfullscreen_ContentIn(); }
        private void buttonVfullscreen_ResetContent_Click(object sender, EventArgs e) { this.business.Vfullscreen_ResetContent(); }

        private void buttonVstage_ContentIn_Click(object sender, EventArgs e) { this.business.Vstage_ContentIn(); }
        private void buttonVstage_ResetContent_Click(object sender, EventArgs e) { this.business.Vstage_ResetContent(); }

        private void buttonVhost_ShowSelection_Click(object sender, EventArgs e) { this.business.Vhost_ShowSelection(); }


        private void buttonVplayer_EnableTouch_Click(object sender, EventArgs e) {
            this.business.VleftPlayer_EnableTouch();
            this.business.VrightPlayer_EnableTouch();
        }
        private void buttonVplayer_ContentOut_Click(object sender, EventArgs e) {
            this.business.Vplayer_ContentOut();
        }
        private void buttonVplayer_ContentIn_Click(object sender, EventArgs e) {
            this.business.Vplayer_ContentIn();
        }

        private void buttonGame_Resolve_Click(object sender, EventArgs e) { this.business.Resolve(); }
        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }


        #endregion

    }
}
