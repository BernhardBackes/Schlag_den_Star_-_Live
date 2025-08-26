using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.SelectDifferences4x4 {

    public partial class UserControlGame : _Base.Score.UserControlGame {

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

            this.setBuzzeredPlayer();

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
            step.AddButton(this.buttonVinsert_ContentIn);
            step.AddButton(this.buttonVfullscreen_ContentIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVplayers_ContentIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ShowSelection);
            step.AddButton(this.buttonVfullscreen_ShowSelection);
            step.AddButton(this.buttonVplayers_ShowSelection);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_Resolve);
            step.AddButton(this.buttonVfullscreen_Resolve);
            step.AddButton(this.buttonVplayers_Resolve);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ShowMissed);
            step.AddButton(this.buttonVfullscreen_ShowMissed);
            step.AddButton(this.buttonVplayers_ShowMissed);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ContentOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ScoreIn);
            step.AddButton(this.buttonVstage_SetScore);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 7);
            step.AddButton(this.buttonVinsert_ScoreOut);
            step.AddButton(this.buttonVplayers_ContentOut);
            step.AddButton(this.buttonVfullscreen_ContentOut);
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

            this.setSolutionFramesList();

            this.selectDataList();
        }

        private void setSolutionFramesList() {
            string key;
            PictureBox control;
            bool[] frameIsIdleList = this.business.FrameIsIdleList;
            for (int i = 0; i < DatasetContent.FramesCount; i++) {
                key = string.Format("pictureBoxDatasetSolution_{0}", i.ToString("00"));
                control = this.groupBoxDatasetSolution.Controls[key] as PictureBox;
                if (control is PictureBox) {
                    if (this.selectedDataset is DatasetContent) {
                        if (this.selectedDataset.SolutionFrameList.Contains(i)) control.BackColor = Constants.ColorEnabled;
                        else control.BackColor = SystemColors.Control;
                        if (frameIsIdleList.Length > i) {
                            if (frameIsIdleList[i]) control.Image = null;
                            else control.Image = Properties.Resources.roter_Rahmen;
                        }
                        else control.Image = null;
                    }
                    else {
                        control.Image = null;
                        control.BackColor = SystemColors.ControlDark;
                    }
                }
            }
            this.groupBoxDatasetSolution.Enabled = this.selectedDataset is DatasetContent;
        }

        private void setBuzzeredPlayer() {
            switch (this.business.BuzzeredPlayer) {
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.LeftPlayer:
                    this.buttonLeftPlayerBuzzer.BackColor = Constants.ColorBuzzered;
                    this.buttonRightPlayerBuzzer.UseVisualStyleBackColor = true;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.RightPlayer:
                    this.buttonLeftPlayerBuzzer.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerBuzzer.BackColor = Constants.ColorBuzzered;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.NotSelected:
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
                if (e.PropertyName == "NameList") this.fillDataList();
                else if (e.PropertyName == "SampleIncluded") this.fillDataList();
                else if (e.PropertyName == "SelectedDataset") this.selectDataset(this.business.SelectedDataset);
                else if (e.PropertyName == "SelectedDatasetIndex") this.selectDataList();
                else if (e.PropertyName == "BuzzeredPlayer") {
                    this.setBuzzeredPlayer();
                    Helper.invokeActionAfterDelay(this.setSolutionFramesList, 2000, WindowsFormsSynchronizationContext.Current);
                }
            }
        }

        void selectedDataset_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.selectedDataset_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "SolutionFilename") this.pictureBoxDatasetPicture.Image = this.selectedDataset.Picture;
                else if (e.PropertyName == "SolutionFrameList") this.setSolutionFramesList();
            }
        }

        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        protected override void buttonLeftPlayerAddScoreHot_01_Click(object sender, EventArgs e) {
            base.buttonLeftPlayerAddScoreHot_01_Click(sender, e);
            this.business.Vinsert_SetContent();
        }
        protected override void buttonRightPlayerAddScoreHot_01_Click(object sender, EventArgs e) {
            base.buttonRightPlayerAddScoreHot_01_Click(sender, e);
            this.business.Vinsert_SetContent();
        }

        private void buttonLeftPlayerBuzzer_Click(object sender, EventArgs e) { this.business.DoBuzzer(Content.Gameboard.PlayerSelection.LeftPlayer); }
        private void buttonRightPlayerBuzzer_Click(object sender, EventArgs e) { this.business.DoBuzzer(Content.Gameboard.PlayerSelection.RightPlayer); }

        private void listBoxDataList_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectDataset(this.listBoxDataList.SelectedIndex); }

        private void buttonTrue_Click(object sender, EventArgs e) { this.business.True(); }
        private void buttonFalse_Click(object sender, EventArgs e) { this.business.False(); }

        private void buttonVinsert_ContentIn_Click(object sender, EventArgs e) { this.business.Vinsert_ContentIn(); }
        private void buttonVinsert_ShowSelection_Click(object sender, EventArgs e) { this.business.Vinsert_ShowSelection(); }
        private void buttonVinsert_Resolve_Click(object sender, EventArgs e) { this.business.Vinsert_Resolve(); }
        private void buttonVinsert_ShowMissed_Click(object sender, EventArgs e) { this.business.Vinsert_ShowMissed(); }
        private void buttonVinsert_ContentOut_Click(object sender, EventArgs e) { this.business.Vinsert_ContentOut(); }

        private void buttonVfullscreen_ContentIn_Click(object sender, EventArgs e) { this.business.Vfullscreen_ContentIn(); }
        private void buttonVfullscreen_ShowSelection_Click(object sender, EventArgs e) { this.business.Vfullscreen_ShowSelection(); }
        private void buttonVfullscreen_Resolve_Click(object sender, EventArgs e) { this.business.Vfullscreen_Resolve(); }
        private void buttonVfullscreen_ShowMissed_Click(object sender, EventArgs e) { this.business.Vfullscreen_ShowMissed(); }
        private void buttonVfullscreen_ContentOut_Click(object sender, EventArgs e) { this.business.Vfullscreen_ContentOut(); }

        private void buttonVplayers_ContentIn_Click(object sender, EventArgs e) { this.business.Vplayers_ContentIn(); }
        private void buttonVplayers_ShowSelection_Click(object sender, EventArgs e) { this.business.Vplayers_ShowSelection(); }
        private void buttonVplayers_Resolve_Click(object sender, EventArgs e) { this.business.Vplayers_Resolve(); }
        private void buttonVplayers_ShowMissed_Click(object sender, EventArgs e) { this.business.Vplayers_ShowMissed(); }
        private void buttonVplayers_ContentOut_Click(object sender, EventArgs e) { this.business.Vplayers_ContentOut(); }

        private void buttonVleftplayer_UnlockTouch_Click(object sender, EventArgs e) { this.business.Vleftplayer_UnlockTouch(); }
        private void buttonVleftplayer_LockTouch_Click(object sender, EventArgs e) { this.business.Vleftplayer_LockTouch(); }

        private void buttonVrightplayer_UnlockTouch_Click(object sender, EventArgs e) { this.business.Vrightplayer_UnlockTouch(); }
        private void buttonVrightplayer_LockTouch_Click(object sender, EventArgs e) { this.business.Vrightplayer_LockTouch(); }

        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }

        #endregion

    }
}
