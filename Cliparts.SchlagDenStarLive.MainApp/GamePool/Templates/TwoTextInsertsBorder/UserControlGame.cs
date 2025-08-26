using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TwoTextInsertsBorder {

    public partial class UserControlGame : _Base.Score.UserControlGame {

        #region Properties

        private Business business;

        private DatasetContent selectedLeftPlayerDataset = null;
        private DatasetContent selectedRightPlayerDataset = null;

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

            this.labelGameClass.Text = this.business.ClassInfo;

            this.fillDataList();
            this.selectLeftPlayerDataset(this.business.LeftPlayerSelectedDataset);
            this.selectRightPlayerDataset(this.business.RightPlayerSelectedDataset);
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
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVgraphic_Clear);
            step.AddButton(this.buttonVstage_Clear);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_Init);
            step.AddButton(this.buttonVstage_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ScoreIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ContentIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex);
            step.AddButton(this.buttonVinsert_SetScore);
            step.AddButton(this.buttonVstage_SetScore);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ContentOut);
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
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_ShowGameboard);
            step.AddButton(this.buttonGame_SetWinner);
            step.AddButton(this.buttonVinsert_UnloadScene);
            step.AddButton(this.buttonVfullscreen_UnloadScene);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex);
            step.AddButton(this.buttonVfullscreen_UpdateGameboard);
            this.stepList.Add(step);

            if (index < this.stepsMaxIndex) this.on_Error("buildStepList", "stepList to short");
            if (index > this.stepsMaxIndex) this.on_Error("buildStepList", "stepList to long");
        }

        private void fillDataList() {
            this.listBoxDataListLeftPlayer.BeginUpdate();
            this.listBoxDataListLeftPlayer.Items.Clear();
            this.listBoxDataListRightPlayer.BeginUpdate();
            this.listBoxDataListRightPlayer.Items.Clear();
            int id = 1;
            foreach (string item in this.business.NameList) {
                this.listBoxDataListLeftPlayer.Items.Add(string.Format("{0}: {1}", id.ToString("00"), item));
                this.listBoxDataListRightPlayer.Items.Add(string.Format("{0}: {1}", id.ToString("00"), item));
                id++;
            }
            this.listBoxDataListLeftPlayer.EndUpdate();
            this.listBoxDataListLeftPlayer.Enabled = this.listBoxDataListLeftPlayer.Items.Count > 0;
            Helper.setControlBackColor(this.listBoxDataListRightPlayer);

            this.listBoxDataListRightPlayer.EndUpdate();
            this.listBoxDataListRightPlayer.Enabled = this.listBoxDataListRightPlayer.Items.Count > 0;
            Helper.setControlBackColor(this.listBoxDataListLeftPlayer);

            this.selectLeftPlayerDataList();
            this.selectRightPlayerDataList();
        }

        private void selectLeftPlayerDataList() {
            int index = this.business.GetDatasetIndex(this.selectedLeftPlayerDataset);
            if (index >= 0 &&
                index < this.listBoxDataListLeftPlayer.Items.Count) this.listBoxDataListLeftPlayer.SelectedIndex = index;
        }

        private void selectRightPlayerDataList() {
            int index = this.business.GetDatasetIndex(this.selectedRightPlayerDataset);
            if (index >= 0 &&
                index < this.listBoxDataListRightPlayer.Items.Count) this.listBoxDataListRightPlayer.SelectedIndex = index;
        }

        private void selectLeftPlayerDataset(
            DatasetContent selectedDataset) {
            if (this.selectedLeftPlayerDataset != selectedDataset) {
                //Dispose...
                if (this.selectedLeftPlayerDataset is DatasetContent) {
                    this.selectedLeftPlayerDataset.PropertyChanged -= this.selectedDataset_PropertyChanged;
                }
                this.selectedLeftPlayerDataset = selectedDataset;
                //Pose...
                if (this.selectedLeftPlayerDataset is DatasetContent) {
                    this.selectedLeftPlayerDataset.PropertyChanged += this.selectedDataset_PropertyChanged;
                }
            }
            this.selectLeftPlayerDataList();
        }

        private void selectRightPlayerDataset(
            DatasetContent selectedDataset) {
            if (this.selectedRightPlayerDataset != selectedDataset) {
                //Dispose...
                if (this.selectedRightPlayerDataset is DatasetContent) {
                    this.selectedRightPlayerDataset.PropertyChanged -= this.selectedDataset_PropertyChanged;
                }
                this.selectedRightPlayerDataset = selectedDataset;
                //Pose...
                if (this.selectedRightPlayerDataset is DatasetContent) {
                    this.selectedRightPlayerDataset.PropertyChanged += this.selectedDataset_PropertyChanged;
                }
            }
            this.selectRightPlayerDataList();
        }

        #endregion


        #region Events.Incoming

        protected override void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.business_PropertyChanged(sender, e);
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "NameList") this.fillDataList();
                else if (e.PropertyName == "SampleIncluded") this.fillDataList();
                else if (e.PropertyName == "LeftPlayerSelectedDataset") this.selectLeftPlayerDataset(this.business.LeftPlayerSelectedDataset);
                else if (e.PropertyName == "LeftPlayerSelectedDatasetIndex") this.selectLeftPlayerDataList();
                else if (e.PropertyName == "RightPlayerSelectedDataset") this.selectRightPlayerDataset(this.business.RightPlayerSelectedDataset);
                else if (e.PropertyName == "RightPlayerSelectedDatasetIndex") this.selectRightPlayerDataList();
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

        private void listBoxLeftDataList_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectLeftPlayerDataset(this.listBoxDataListLeftPlayer.SelectedIndex); }
        private void listBoxRightDataList_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectRightPlayerDataset(this.listBoxDataListRightPlayer.SelectedIndex); }

        protected override void buttonVinsert_ScoreIn_Click(object sender, EventArgs e) {
            base.buttonVinsert_ScoreIn_Click(sender, e);
            this.business.Vinsert_BorderIn();
        }
        protected override void buttonVinsert_SetScore_Click(object sender, EventArgs e) {
            base.buttonVinsert_SetScore_Click(sender, e);
            this.business.Vinsert_SetBorder();
        }
        protected override void buttonVinsert_ScoreOut_Click(object sender, EventArgs e) {
            base.buttonVinsert_ScoreOut_Click(sender, e);
            this.business.Vinsert_BorderOut();
        }

        private void buttonVinsert_ContentIn_Click(object sender, EventArgs e) { this.business.Vinsert_ContentIn(); }
        private void buttonVinsert_ContentOut_Click(object sender, EventArgs e) { this.business.Vinsert_ContentOut(); }

        private void buttonLeftPlayerNext_Click(object sender, EventArgs e) { this.business.LeftPlayerNext(); }
        private void buttonRightPlayerNext_Click(object sender, EventArgs e) { this.business.RightPlayerNext(); }

        #endregion

    }
}
