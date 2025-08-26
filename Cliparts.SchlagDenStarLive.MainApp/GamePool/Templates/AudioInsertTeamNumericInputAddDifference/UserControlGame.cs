using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.AudioInsertTeamNumericInputAddDifference
{

    public partial class UserControlGame : _Base.Score.UserControlGame {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;

        private VRemote4.HandlerSi.Business localVentuzHandler;
        private VRemote4.HandlerSi.BusinessForm localVentuzHandlerForm;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.numericUpDownLeftPlayerTopValue.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerTopValue.Maximum = int.MaxValue;

            this.numericUpDownLeftPlayerBottomValue.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerBottomValue.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerTopValue.Minimum = int.MinValue;
            this.numericUpDownRightPlayerTopValue.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerBottomValue.Minimum = int.MinValue;
            this.numericUpDownRightPlayerBottomValue.Maximum = int.MaxValue;

            this.numericUpDownTaskCounter.Minimum = int.MinValue;
            this.numericUpDownTaskCounter.Maximum = int.MaxValue;

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Business localVentuzHandler) {
            base.Pose(business);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.business, "LeftPlayerTopValue");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerTopValue.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "LeftPlayerTopDifference");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxLeftPlayerTopDifference.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerBottomValue");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerBottomValue.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "LeftPlayerBottomDifference");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxLeftPlayerBottomDifference.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerTopValue");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerTopValue.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerTopDifference");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxRightPlayerTopDifference.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerBottomValue");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerBottomValue.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerBottomDifference");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxRightPlayerBottomDifference.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "TaskCounter");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownTaskCounter.DataBindings.Add(bind);

            this.localVentuzHandler = localVentuzHandler;

            VRemote4.HandlerSi.Client.Business leftTeamTabletClient;
            if (this.localVentuzHandler.TryGetClient("Left Tablet", out leftTeamTabletClient)) this.userControlClientStateLeftTeamTablet.Pose(leftTeamTabletClient);
            this.userControlClientStateLeftTeamTablet.BackColor = this.BackColor;

            VRemote4.HandlerSi.Client.Business rightTeamTabletClient;
            if (this.localVentuzHandler.TryGetClient("Right Tablet", out rightTeamTabletClient)) this.userControlClientStateRightTeamTablet.Pose(rightTeamTabletClient);
            this.userControlClientStateRightTeamTablet.BackColor = this.BackColor;

            this.fillDataList();
            this.selectDataset(this.business.SelectedDataset);

            this.labelGameClass.Text = this.business.ClassInfo;
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

            this.numericUpDownLeftPlayerTopValue.DataBindings.Clear();
            this.textBoxLeftPlayerTopDifference.DataBindings.Clear();
            this.numericUpDownLeftPlayerBottomValue.DataBindings.Clear();
            this.textBoxLeftPlayerBottomDifference.DataBindings.Clear();
            this.numericUpDownRightPlayerTopValue.DataBindings.Clear();
            this.textBoxRightPlayerTopDifference.DataBindings.Clear();
            this.numericUpDownRightPlayerBottomValue.DataBindings.Clear();
            this.textBoxRightPlayerBottomDifference.DataBindings.Clear();

            this.numericUpDownTaskCounter.DataBindings.Clear();

        }

        protected override void buildStepList() {

            int index = 0;
            stepAction step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_LoadScene);
            step.AddButton(this.buttonVhost_LoadScene);
            step.AddButton(this.buttonVleftplayer_LoadScene);
            step.AddButton(this.buttonVrightplayer_LoadScene);
            step.AddButton(this.buttonGame_StartTablets);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVgraphic_Clear);
            step.AddButton(this.buttonVstage_Clear);
            step.AddButton(this.buttonGame_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_GameIn);
            step.AddButton(this.buttonVstage_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_PlayAudio);
            step.AddButton(this.buttonVinsert_TaskIn);
            step.AddButton(this.buttonVstage_ContentIn);
            step.AddButton(this.buttonVplayers_EnableInput);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_InputIn);
            step.AddButton(this.buttonVhost_SetInput);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_SolutionIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_Resolve);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_Resolve);
            step.AddButton(this.buttonVinsert_SetScore);
            step.AddButton(this.buttonVstage_SetScore);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ContentOut);
            step.AddButton(this.buttonVstage_ContentOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 6);
            step.AddButton(this.buttonGame_Next);
            this.stepList.Add(step);

            index++;
            this.winnerStepIndex = index;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVgraphic_Clear);
            step.AddButton(this.buttonVstage_Clear);
            step.AddButton(this.buttonGame_ShutTabletsDown);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_UnloadScene);
            step.AddButton(this.buttonVfullscreen_UnloadScene);
            step.AddButton(this.buttonVhost_UnloadScene);
            step.AddButton(this.buttonVleftplayer_UnloadScene);
            step.AddButton(this.buttonVrightplayer_UnloadScene);
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

            this.setDatasetItems();

            this.selectDataList();
        }

        private void setDatasetItems() {
            if (this.selectedDataset is DatasetContent) {
                this.textBoxSolution.Text = this.selectedDataset.Solution.ToString();
            }
            else {
                this.textBoxSolution.Text = string.Empty;
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
                this.setDatasetItems();
            }
        }

        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void UserControlGame_BackColorChanged(object sender, EventArgs e)
        {
            this.userControlClientStateLeftTeamTablet.BackColor = this.BackColor;
            this.userControlClientStateRightTeamTablet.BackColor = this.BackColor;
        }

        private void numericUpDownLeftPlayerTopValue_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerTopValue = (int)this.numericUpDownLeftPlayerTopValue.Value; }
        private void buttonLeftTeamTopEnableInput_Click(object sender, EventArgs e) { this.business.Vleftplayer_UnlockInput(); }
        private void buttonLeftTeamTopDisableInput_Click(object sender, EventArgs e) { this.business.Vleftplayer_LockInput(); }
        private void buttonLeftTeamTopClearInput_Click(object sender, EventArgs e) { this.business.Vleftplayer_ResetInput(); }

        private void numericUpDownLeftPlayerBottomValue_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerBottomValue = (int)this.numericUpDownLeftPlayerBottomValue.Value; }
        private void buttonLeftTeamBottomEnableInput_Click(object sender, EventArgs e) { this.business.Vlefttablet_EnableInput(); }
        private void buttonLeftTeamBottomDisableInput_Click(object sender, EventArgs e) { this.business.Vlefttablet_DisableInput(); }
        private void buttonLeftTeamBottomClearInput_Click(object sender, EventArgs e) { this.business.Vlefttablet_ClearInput(); }

        private void numericUpDownRightPlayerTopValue_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerTopValue = (int)this.numericUpDownRightPlayerTopValue.Value; }
        private void buttonRightTeamTopEnableInput_Click(object sender, EventArgs e) { this.business.Vrightplayer_UnlockInput(); }
        private void buttonRightTeamTopDisableInput_Click(object sender, EventArgs e) { this.business.Vrightplayer_LockInput(); }
        private void buttonRightTeamTopClearInput_Click(object sender, EventArgs e) { this.business.Vrightplayer_ResetInput(); }

        private void numericUpDownRightPlayerBottomValue_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerBottomValue = (int)this.numericUpDownRightPlayerBottomValue.Value; }
        private void buttonRightTeamBottomEnableInput_Click(object sender, EventArgs e) { this.business.Vrighttablet_EnableInput(); }
        private void buttonRightTeamBottomDisableInput_Click(object sender, EventArgs e) { this.business.Vrighttablet_DisableInput(); }
        private void buttonRightTeamBottomClearInput_Click(object sender, EventArgs e) { this.business.Vrighttablet_ClearInput(); }

        private void numericUpDownTaskCounter_ValueChanged(object sender, EventArgs e) { this.business.TaskCounter = (int)this.numericUpDownTaskCounter.Value; }

        private void listBoxDataList_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectDataset(this.listBoxDataList.SelectedIndex); }

        private void buttonShowVentuzHandler_Click(object sender, EventArgs e)
        {
            if (this.localVentuzHandlerForm == null)
            {
                this.localVentuzHandlerForm = new VRemote4.HandlerSi.BusinessForm(this.localVentuzHandler, this.BackColor);
                this.localVentuzHandlerForm.Disposed += this.localVentuzHandlerForm_Disposed;
            }
            this.localVentuzHandlerForm.Show();
            this.localVentuzHandlerForm.BringToFront();
        }
        private void localVentuzHandlerForm_Disposed(object sender, EventArgs e)
        {
            if (this.localVentuzHandlerForm is VRemote4.HandlerSi.BusinessForm)
            {
                this.localVentuzHandlerForm.Disposed -= this.localVentuzHandlerForm_Disposed;
                this.localVentuzHandlerForm = null;
            }
        }

        private void buttonVinsert_GameIn_Click(object sender, EventArgs e) { this.business.Vinsert_GameIn(); }
        private void buttonVinsert_PlayAudio_Click(object sender, EventArgs e) { this.business.Vinsert_PlayAudio(); }
        private void buttonVinsert_TaskIn_Click(object sender, EventArgs e) { this.business.Vinsert_TaskIn(); }
        private void buttonVinsert_InputIn_Click(object sender, EventArgs e) { this.business.Vinsert_InputIn(); }
        private void buttonVinsert_SolutionIn_Click(object sender, EventArgs e) { this.business.Vinsert_SolutionIn(); }
        private void buttonVinsert_Resolve_Click(object sender, EventArgs e) { this.business.Vinsert_DifferencesIn();  }
        private void buttonVinsert_ContentOut_Click(object sender, EventArgs e) {
            this.business.Vinsert_InputOut();
            this.business.Vinsert_TaskOut();
        }

        private void buttonVstage_ContentIn_Click(object sender, EventArgs e) { this.business.Vstage_ContentIn(); }
        private void buttonVstage_ContentOut_Click(object sender, EventArgs e) { this.business.Vstage_ContentOut(); }

        private void buttonVhost_SetInput_Click(object sender, EventArgs e) {
            this.business.Vhost_SetPlayerInput();
        }

        private void buttonVplayers_EnableInput_Click(object sender, EventArgs e) { this.business.Vplayers_EnableInput(); }

        private void buttonVleftplayer_Unlock_Click(object sender, EventArgs e) { this.business.Vleftplayer_UnlockInput(); }
        private void buttonVleftplayer_Lock_Click(object sender, EventArgs e) { this.business.Vleftplayer_LockInput(); }

        private void buttonVrightplayer_Unlock_Click(object sender, EventArgs e) { this.business.Vrightplayer_UnlockInput(); }
        private void buttonVrightplayer_Lock_Click(object sender, EventArgs e) { this.business.Vrightplayer_LockInput(); }

        private void buttonGame_StartTablets_Click(object sender, EventArgs e)
        {
            this.business.Vlefttablet_Start();
            this.business.Vrighttablet_Start();
        }
        private void buttonGame_Resolve_Click(object sender, EventArgs e) { this.business.Resolve(); }
        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }
        private void buttonGame_ShutTabletsDown_Click(object sender, EventArgs e)
        {
            this.business.Vlefttablet_ShutDown();
            this.business.Vrighttablet_ShutDown();
        }
        #endregion

    }

}
