using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.Remembory {

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
            step.AddButton(this.buttonVhost_ContentIn);
            step.AddButton(this.buttonVplayers_LiveVideoIn);
            step.AddButton(this.buttonGame_ReleaseBuzzer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_BorderOut);
            step.AddButton(this.buttonVplayers_LiveVideoOut);
            step.AddButton(this.buttonVinsert_TimerIn);
            step.AddButton(this.buttonVinsert_ContentIn);
            step.AddButton(this.buttonVplayers_TimerIn);
            step.AddButton(this.buttonVplayers_ContentIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVplayers_StartTimer);
            step.AddButton(this.buttonVinsert_StartTimer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimerOut);
            step.AddButton(this.buttonVplayers_TimerOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ContentOut);
            step.AddButton(this.buttonVfullscreen_ContentOut);
            step.AddButton(this.buttonVhost_ContentOut);
            step.AddButton(this.buttonVplayers_ContentOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ScoreIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_CalcScore);
            step.AddButton(this.buttonVinsert_SetScore);
            step.AddButton(this.buttonVstage_SetScore);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 7);
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

        private void bind_textBoxIOUnitName_BackColor(object sender, ConvertEventArgs e) {
            switch ((BuzzerUnitStates)e.Value) {
                case BuzzerUnitStates.NotAvailable:
                    e.Value = Constants.ColorMissing;
                    break;
                case BuzzerUnitStates.Missing:
                    e.Value = Constants.ColorMissing;
                    break;
                case BuzzerUnitStates.Disconnected:
                    e.Value = Constants.ColorDisabled;
                    break;
                case BuzzerUnitStates.Connecting:
                    e.Value = Constants.ColorEnabling;
                    break;
                case BuzzerUnitStates.Connected:
                    e.Value = SystemColors.Control;
                    break;
                case BuzzerUnitStates.Locked:
                    e.Value = SystemColors.Control;
                    break;
                case BuzzerUnitStates.BuzzerMode:
                    e.Value = Constants.ColorEnabled;
                    break;
                case BuzzerUnitStates.EventMode:
                    e.Value = Constants.ColorBuzzered;
                    break;
                default:
                    break;
            }
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
                this.pictureBoxImage_01.Image = this.selectedDataset.Image1;
                this.pictureBoxImage_02.Image = this.selectedDataset.Image2;
                this.pictureBoxImage_03.Image = this.selectedDataset.Image3;
                this.pictureBoxImage_04.Image = this.selectedDataset.Image4;
                this.pictureBoxImage_05.Image = this.selectedDataset.Image5;
                this.pictureBoxImage_06.Image = this.selectedDataset.Image6;
                this.pictureBoxImage_07.Image = this.selectedDataset.Image7;
                this.pictureBoxImage_08.Image = this.selectedDataset.Image8;
                this.panelSelectedDataset.Enabled = true;
            }
            else {
                this.pictureBoxImage_01.Image = null;
                this.pictureBoxImage_02.Image = null;
                this.pictureBoxImage_03.Image = null;
                this.pictureBoxImage_04.Image = null;
                this.pictureBoxImage_05.Image = null;
                this.pictureBoxImage_06.Image = null;
                this.pictureBoxImage_07.Image = null;
                this.pictureBoxImage_08.Image = null;
                this.panelSelectedDataset.Enabled = false;
            }

            this.selectDataList();

            this.setFoundImages();
        }

        private void setFoundImages() {
            if (this.business.FoundImages.Count > 0 && this.business.FoundImages[0]) this.pictureBoxImage_01.BackColor = Constants.ColorGotten;
            else this.pictureBoxImage_01.BackColor = this.BackColor;
            if (this.business.FoundImages.Count > 0 && this.business.FoundImages[1]) this.pictureBoxImage_02.BackColor = Constants.ColorGotten;
            else this.pictureBoxImage_02.BackColor = this.BackColor;
            if (this.business.FoundImages.Count > 0 && this.business.FoundImages[2]) this.pictureBoxImage_03.BackColor = Constants.ColorGotten;
            else this.pictureBoxImage_03.BackColor = this.BackColor;
            if (this.business.FoundImages.Count > 0 && this.business.FoundImages[3]) this.pictureBoxImage_04.BackColor = Constants.ColorGotten;
            else this.pictureBoxImage_04.BackColor = this.BackColor;
            if (this.business.FoundImages.Count > 0 && this.business.FoundImages[4]) this.pictureBoxImage_05.BackColor = Constants.ColorGotten;
            else this.pictureBoxImage_05.BackColor = this.BackColor;
            if (this.business.FoundImages.Count > 0 && this.business.FoundImages[5]) this.pictureBoxImage_06.BackColor = Constants.ColorGotten;
            else this.pictureBoxImage_06.BackColor = this.BackColor;
            if (this.business.FoundImages.Count > 0 && this.business.FoundImages[6]) this.pictureBoxImage_07.BackColor = Constants.ColorGotten;
            else this.pictureBoxImage_07.BackColor = this.BackColor;
            if (this.business.FoundImages.Count > 0 && this.business.FoundImages[7]) this.pictureBoxImage_08.BackColor = Constants.ColorGotten;
            else this.pictureBoxImage_08.BackColor = this.BackColor;
        }

        #endregion


        #region Events.Incoming

        protected override void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.business_PropertyChanged(sender, e);
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "BuzzeredPlayer") this.setBuzzeredPlayer();
                else if (e.PropertyName == "NameList") this.fillDataList();
                else if (e.PropertyName == "SampleIncluded") this.fillDataList();
                else if (e.PropertyName == "SelectedDataset") this.selectDataset(this.business.SelectedDataset);
                else if (e.PropertyName == "SelectedDatasetIndex") this.selectDataList();
                else if (e.PropertyName == "FoundImages") this.setFoundImages();
            }
        }

        void selectedDataset_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.selectedDataset_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "Image1Filename") this.pictureBoxImage_01.Image = this.selectedDataset.Image1;
                else if (e.PropertyName == "Image2Filename") this.pictureBoxImage_02.Image = this.selectedDataset.Image2;
                else if (e.PropertyName == "Image3Filename") this.pictureBoxImage_03.Image = this.selectedDataset.Image3;
                else if (e.PropertyName == "Image4Filename") this.pictureBoxImage_04.Image = this.selectedDataset.Image4;
                else if (e.PropertyName == "Image5Filename") this.pictureBoxImage_05.Image = this.selectedDataset.Image5;
                else if (e.PropertyName == "Image6Filename") this.pictureBoxImage_06.Image = this.selectedDataset.Image6;
                else if (e.PropertyName == "Image7Filename") this.pictureBoxImage_07.Image = this.selectedDataset.Image7;
                else if (e.PropertyName == "Image8Filename") this.pictureBoxImage_08.Image = this.selectedDataset.Image8;
            }
        }


        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void UserControlGame_BackColorChanged(object sender, EventArgs e) { this.setFoundImages(); }

        private void buttonLeftPlayerBuzzer_Click(object sender, EventArgs e) { if (this.business is Business) this.business.DoBuzzer(Content.Gameboard.PlayerSelection.LeftPlayer); }
        private void buttonRightPlayerBuzzer_Click(object sender, EventArgs e) { if (this.business is Business) this.business.DoBuzzer(Content.Gameboard.PlayerSelection.RightPlayer); }

        protected override void buttonTimerIn_Click(object sender, EventArgs e) {
            base.buttonTimerIn_Click(sender, e);
            if (this.business is Business) this.business.Vplayers_TimerIn();
        }
        protected override void buttonTimerOut_Click(object sender, EventArgs e) {
            base.buttonTimerOut_Click(sender, e);
            if (this.business is Business) this.business.Vplayers_TimerOut();
        }
        protected override void buttonTimerStart_Click(object sender, EventArgs e) {
            base.buttonTimerStart_Click(sender, e);
            if (this.business is Business) this.business.Vplayers_StartTimer();
        }
        protected override void buttonTimerStop_Click(object sender, EventArgs e) {
            base.buttonTimerStop_Click(sender, e);
            if (this.business is Business) this.business.Vplayers_StopTimer();
        }
        protected override void buttonTimerContinue_Click(object sender, EventArgs e) {
            base.buttonTimerContinue_Click(sender, e);
            if (this.business is Business) this.business.Vplayers_ContinueTimer();
        }
        protected override void buttonTimerReset_Click(object sender, EventArgs e) {
            base.buttonTimerReset_Click(sender, e);
            if (this.business is Business) this.business.Vplayers_ResetTimer();
        }

        private void listBoxDataList_SelectedIndexChanged(object sender, EventArgs e) { if (this.business is Business) this.business.SelectDataset(this.listBoxDataList.SelectedIndex); }

        private void buttonRemoveImage_Click(object sender, EventArgs e) {
            int index = 0;
            if (this.business is Business &&
                Helper.tryParseIndexFromControl(sender as Control, out index)) this.business.RemoveFoundImage(index);
        }
        private void pictureBoxImage_Click(object sender, EventArgs e) {
            int index = 0;
            if (this.business is Business &&
                Helper.tryParseIndexFromControl(sender as Control, out index)) this.business.AddFoundImage(index);
        }
        private void buttonFault_Click(object sender, EventArgs e) { if (this.business is Business) this.business.Fault(); }

        private void buttonVinsert_BorderIn_Click(object sender, EventArgs e) { if (this.business is Business) this.business.Vinsert_BorderIn(); }
        private void buttonVinsert_BorderOut_Click(object sender, EventArgs e) { if (this.business is Business) this.business.Vinsert_BorderOut(); }
        private void buttonVinsert_ContentIn_Click(object sender, EventArgs e) { if (this.business is Business) this.business.Vinsert_ContentIn(); }
        private void buttonVinsert_ContentOut_Click(object sender, EventArgs e) { if (this.business is Business) this.business.Vinsert_ContentOut(); }

        private void buttonVfullscreen_ContentIn_Click(object sender, EventArgs e) { if (this.business is Business) this.business.Vfullscreen_ContentIn(); }
        private void buttonVfullscreen_ContentOut_Click(object sender, EventArgs e) { if (this.business is Business) this.business.Vfullscreen_ContentOut(); }

        private void buttonVhost_ContentIn_Click(object sender, EventArgs e) { if (this.business is Business) this.business.Vhost_In(); }
        private void buttonVhost_ContentOut_Click(object sender, EventArgs e) { if (this.business is Business) this.business.Vhost_Out(); }

        private void buttonVplayers_LiveVideoIn_Click(object sender, EventArgs e) { if (this.business is Business) this.business.Vplayers_LiveVideoIn(); }
        private void buttonVplayers_LiveVideoOut_Click(object sender, EventArgs e) { if (this.business is Business) this.business.Vplayers_LiveVideoOut(); }
        private void buttonVplayers_TimertIn_Click(object sender, EventArgs e) { if (this.business is Business) this.business.Vplayers_TimerIn(); }
        private void buttonVplayers_ContentIn_Click(object sender, EventArgs e) { if (this.business is Business) this.business.Vplayers_ContentIn(); }
        private void buttonVplayers_StartTimer_Click(object sender, EventArgs e) { if (this.business is Business) this.business.Vplayers_StartTimer(); }
        private void buttonVplayers_TimerOut_Click(object sender, EventArgs e) { if (this.business is Business) this.business.Vplayers_TimerOut(); }
        private void buttonVplayers_ContentOut_Click(object sender, EventArgs e) { if (this.business is Business) this.business.Vplayers_ContentOut(); }

        private void buttonGame_ReleaseBuzzer_Click(object sender, EventArgs e) { if (this.business is Business) this.business.ReleaseBuzzer(); }
        private void buttonGame_LockBuzzer_Click(object sender, EventArgs e) { if (this.business is Business) this.business.LockBuzzer(); }
        private void buttonGame_CalcScore_Click(object sender, EventArgs e) { if (this.business is Business) this.business.CalcScore(); }
        private void buttonGame_Next_Click(object sender, EventArgs e) { if (this.business is Business) this.business.Next(); }

        #endregion
    }
}
