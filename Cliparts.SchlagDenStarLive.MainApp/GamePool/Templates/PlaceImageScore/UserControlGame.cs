using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.PlaceImageScore {

    public partial class UserControlGame : _Base.Score.UserControlGame {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.numericUpDownLeftPlayerPositionX.Minimum = decimal.MinValue;
            this.numericUpDownLeftPlayerPositionX.Maximum = decimal.MaxValue;

            this.numericUpDownLeftPlayerPositionY.Minimum = decimal.MinValue;
            this.numericUpDownLeftPlayerPositionY.Maximum = decimal.MaxValue;

            this.numericUpDownRightPlayerPositionX.Minimum = decimal.MinValue;
            this.numericUpDownRightPlayerPositionX.Maximum = decimal.MaxValue;

            this.numericUpDownRightPlayerPositionY.Minimum = decimal.MinValue;
            this.numericUpDownRightPlayerPositionY.Maximum = decimal.MaxValue;

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

            bind = new Binding("Value", this.business, "LeftPlayerPositionX");
            bind.Format += (s, e) => { e.Value = Convert.ToDecimal(e.Value); };
            this.numericUpDownLeftPlayerPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerPositionY");
            bind.Format += (s, e) => { e.Value = Convert.ToDecimal(e.Value); };
            this.numericUpDownLeftPlayerPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "LeftPlayerDistance");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxLeftPlayerDistance.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerPositionX");
            bind.Format += (s, e) => { e.Value = Convert.ToDecimal(e.Value); };
            this.numericUpDownRightPlayerPositionX.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerPositionY");
            bind.Format += (s, e) => { e.Value = Convert.ToDecimal(e.Value); };
            this.numericUpDownRightPlayerPositionY.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerDistance");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxRightPlayerDistance.DataBindings.Add(bind);

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

            this.numericUpDownLeftPlayerPositionX.DataBindings.Clear();
            this.numericUpDownLeftPlayerPositionY.DataBindings.Clear();
            this.textBoxLeftPlayerDistance.DataBindings.Clear();
            this.numericUpDownRightPlayerPositionX.DataBindings.Clear();
            this.numericUpDownRightPlayerPositionY.DataBindings.Clear();
            this.textBoxRightPlayerDistance.DataBindings.Clear();
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
            step.AddButton(this.buttonVfullscreen_SampleIn);
            step.AddButton(this.buttonVstage_SampleIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_MapIn);
            step.AddButton(this.buttonVstage_MapIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVplayer_UnlockTouch);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_MapIn);
            step.AddButton(this.buttonVplayer_LockTouch);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_BlueIn);
            step.AddButton(this.buttonVfullscreen_BlueIn);
            step.AddButton(this.buttonVhost_BlueIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_RedIn);
            step.AddButton(this.buttonVfullscreen_RedIn);
            step.AddButton(this.buttonVhost_RedIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_SolutionIn);
            step.AddButton(this.buttonVfullscreen_SolutionIn);
            step.AddButton(this.buttonVhost_SolutionIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_MapOut);
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
            step = new stepAction(index, (stepIndex) => stepIndex - 9);
            step.AddButton(this.buttonVinsert_ScoreOut);
            step.AddButton(this.buttonVfullscreen_MapOut);
            step.AddButton(this.buttonVstage_MapOut);
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
            step.AddButton(this.buttonVfullscreen_ShowGameboard);
            step.AddButton(this.buttonVinsert_UnloadScene);
            step.AddButton(this.buttonVfullscreen_UnloadScene);
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
                //Dispose...
                if (this.selectedDataset is DatasetContent) {
                }
                this.selectedDataset = selectedDataset;
                //Pose...
                if (this.selectedDataset is DatasetContent) {
                }
            }

            if (this.selectedDataset is DatasetContent) {
            }
            else {
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

        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void numericUpDownLeftPlayerPositionX_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerPositionX = (double)this.numericUpDownLeftPlayerPositionX.Value; }
        private void numericUpDownLeftPlayerPositionY_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerPositionY = (double)this.numericUpDownLeftPlayerPositionY.Value; }
        private void buttonCalcLeftPlayerDistance_Click(object sender, EventArgs e) { this.business.CalcLeftPlayerDistance(); }
        private void buttonLeftPlayerUnlockTouch_Click(object sender, EventArgs e) { this.business.LeftPlayerUnlockTouch(); }
        private void buttonLeftPlayerLockTouch_Click(object sender, EventArgs e) { this.business.LeftPlayerLockTouch(); }
        private void buttonLeftPlayerResetInput_Click(object sender, EventArgs e) { this.business.LeftPlayerResetInput(); }

        private void numericUpDownRightPlayerPositionX_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerPositionX = (double)this.numericUpDownRightPlayerPositionX.Value; }
        private void numericUpDownRightPlayerPositionY_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerPositionY = (double)this.numericUpDownRightPlayerPositionY.Value; }
        private void buttonCalcRightPlayerDistance_Click(object sender, EventArgs e) { this.business.CalcRightPlayerDistance(); }
        private void buttonRightPlayerUnlockTouch_Click(object sender, EventArgs e) { this.business.RightPlayerUnlockTouch(); }
        private void buttonRightPlayerLockTouch_Click(object sender, EventArgs e) { this.business.RightPlayerLockTouch(); }
        private void buttonRightPlayerResetInput_Click(object sender, EventArgs e) { this.business.RightPlayerResetInput(); }

        private void numericUpDownTaskCounter_ValueChanged(object sender, EventArgs e) { this.business.TaskCounter = (int)this.numericUpDownTaskCounter.Value; }

        private void listBoxDataList_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectDataset(this.listBoxDataList.SelectedIndex); }

        private void buttonVinsert_MapIn_Click(object sender, EventArgs e) { this.business.Vinsert_MapIn(); }
        private void buttonVinsert_BlueIn_Click(object sender, EventArgs e) { this.business.Vinsert_BlueIn(); }
        private void buttonVinsert_RedIn_Click(object sender, EventArgs e) { this.business.Vinsert_RedIn(); }
        private void buttonVinsert_SolutionIn_Click(object sender, EventArgs e) { this.business.Vinsert_SolutionIn(); }
        private void buttonVinsert_MapOut_Click(object sender, EventArgs e) { this.business.Vinsert_MapOut(); }

        private void buttonVfullscreen_SampleIn_Click(object sender, EventArgs e) { this.business.Vfullscreen_MapIn(true); }
        private void buttonVfullscreen_MapIn_Click(object sender, EventArgs e) { this.business.Vfullscreen_MapIn(false); }
        private void buttonVfullscreen_BlueIn_Click(object sender, EventArgs e) { this.business.Vfullscreen_BlueIn(); }
        private void buttonVfullscreen_RedIn_Click(object sender, EventArgs e) { this.business.Vfullscreen_RedIn(); }
        private void buttonVfullscreen_SolutionIn_Click(object sender, EventArgs e) { this.business.Vfullscreen_SolutionIn(); }
        private void buttonVfullscreen_MapOut_Click(object sender, EventArgs e) { this.business.Vfullscreen_MapOut(); }

        private void buttonVstage_SampleIn_Click(object sender, EventArgs e) {
            this.business.Vhost_MapIn(true);
            this.business.Vleftplayer_MapIn(true);
            this.business.Vrightplayer_MapIn(true);
        }
        private void buttonVstage_MapIn_Click(object sender, EventArgs e) {
            this.business.Vhost_MapIn(false);
            this.business.Vleftplayer_MapIn(false);
            this.business.Vrightplayer_MapIn(false);
        }
        private void buttonVstage_MapOut_Click(object sender, EventArgs e) {
            this.business.Vhost_MapOut();
            this.business.Vleftplayer_MapOut();
            this.business.Vrightplayer_MapOut();
        }

        private void buttonVhost_BlueIn_Click(object sender, EventArgs e) { this.business.Vhost_BlueIn(); }
        private void buttonVhost_RedIn_Click(object sender, EventArgs e) { this.business.Vhost_RedIn(); }
        private void buttonVhost_SolutionIn_Click(object sender, EventArgs e) { this.business.Vhost_SolutionIn(); }

        private void buttonVPlayer_UnlockTouch_Click(object sender, EventArgs e) { 
            this.business.Vleftplayer_UnlockTouch();
            this.business.Vrightplayer_UnlockTouch();
        }
        private void buttonVPlayer_LockTouch_Click(object sender, EventArgs e) {
            this.business.Vleftplayer_LockTouch();
            this.business.Vrightplayer_LockTouch();
        }

        private void buttonGame_Resolve_Click(object sender, EventArgs e) { this.business.Resolve(); }
        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }

        #endregion

    }

}
