using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TextInsertTeamInputScore {

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

            bind = new Binding("Text", this.business, "LeftTeamInputTop");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxLeftTeamInputTop.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "LeftTeamIsTrue");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorEnabled : SystemColors.Control; };
            this.textBoxLeftTeamInputTop.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "LeftTeamInputBottom");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxLeftTeamInputBottom.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "LeftTeamIsTrue");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorEnabled : SystemColors.Control; };
            this.textBoxLeftTeamInputBottom.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "LeftTeamIsTrue");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxLeftTeamIsTrue.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightTeamInputTop");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxRightTeamInputTop.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "RightTeamIsTrue");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorEnabled : SystemColors.Control; };
            this.textBoxRightTeamInputTop.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightTeamInputBottom");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxRightTeamInputBottom.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "RightTeamIsTrue");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorEnabled : SystemColors.Control; };
            this.textBoxRightTeamInputBottom.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "RightTeamIsTrue");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxRightTeamIsTrue.DataBindings.Add(bind);

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

            this.textBoxLeftTeamInputTop.DataBindings.Clear();
            this.textBoxLeftTeamInputBottom.DataBindings.Clear();
            this.checkBoxLeftTeamIsTrue.DataBindings.Clear();
            this.textBoxRightTeamInputTop.DataBindings.Clear();
            this.textBoxRightTeamInputBottom.DataBindings.Clear();
            this.checkBoxRightTeamIsTrue.DataBindings.Clear();

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
            step.AddButton(this.buttonVstage_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TaskIn);
            step.AddButton(this.buttonVstage_ContentIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVplayers_EnableInput);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVhost_SetInput);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_InputIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_Resolve);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ContentOut);
            step.AddButton(this.buttonVstage_ContentOut);
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
            step = new stepAction(index, (stepIndex) => stepIndex - 8);
            step.AddButton(this.buttonVinsert_ScoreOut);
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
            step.AddButton(this.buttonVfullscreen_ShowGameboard);
            step.AddButton(this.buttonVhost_UnloadScene);
            step.AddButton(this.buttonVleftplayer_UnloadScene);
            step.AddButton(this.buttonVrightplayer_UnloadScene);
            step.AddButton(this.buttonGame_SetWinner);
            this.stepList.Add(step);

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

        private void textBoxLeftTeamInputTop_TextChanged(object sender, EventArgs e) { this.business.LeftTeamInputTop = this.textBoxLeftTeamInputTop.Text; }
        private void buttonLeftTeamTopEnableInput_Click(object sender, EventArgs e) { this.business.Vleftplayer_EnableInput(); }
        private void buttonLeftTeamTopDisableInput_Click(object sender, EventArgs e) { this.business.Vleftplayer_DisableInput(); }
        private void buttonLeftTeamTopClearInput_Click(object sender, EventArgs e) { this.business.Vleftplayer_ClearInput(); }
        private void checkBoxLeftTeamIsTrue_CheckedChanged(object sender, EventArgs e) { this.business.LeftTeamIsTrue = this.checkBoxLeftTeamIsTrue.Checked; }
        private void textBoxLeftTeamInputBottom_TextChanged(object sender, EventArgs e) { this.business.LeftTeamInputBottom = this.textBoxLeftTeamInputBottom.Text; }
        private void buttonLeftTeamBottomEnableInput_Click(object sender, EventArgs e) { this.business.Vlefttablet_EnableInput(); }
        private void buttonLeftTeamBottomDisableInput_Click(object sender, EventArgs e) { this.business.Vlefttablet_DisableInput(); }
        private void buttonLeftTeamBottomClearInput_Click(object sender, EventArgs e) { this.business.Vlefttablet_ClearInput(); }
        private void buttonLeftTeamInputTopIn_Click(object sender, EventArgs e) { this.business.Vinsert_InputIn_LeftTop(); }
        private void buttonLeftTeamInputBottomIn_Click(object sender, EventArgs e) { this.business.Vinsert_InputIn_LeftBottom(); }
        private void buttonLeftTeamInputIn_Click(object sender, EventArgs e) { this.business.Vinsert_InputIn_Left(false); }
        private void buttonLeftTeamResolve_Click(object sender, EventArgs e) { this.business.Vinsert_InputIn_Left(true); }

        private void textBoxRightTeamInputTop_TextChanged(object sender, EventArgs e) { this.business.RightTeamInputTop = this.textBoxRightTeamInputTop.Text; }
        private void buttonRightTeamTopEnableInput_Click(object sender, EventArgs e) { this.business.Vrightplayer_EnableInput(); }
        private void buttonRightTeamTopDisableInput_Click(object sender, EventArgs e) { this.business.Vrightplayer_DisableInput(); }
        private void buttonRightTeamTopClearInput_Click(object sender, EventArgs e) { this.business.Vrightplayer_ClearInput(); }
        private void checkBoxRightTeamIsTrue_CheckedChanged(object sender, EventArgs e) { this.business.RightTeamIsTrue = this.checkBoxRightTeamIsTrue.Checked; }
        private void textBoxRightTeamInputBottom_TextChanged(object sender, EventArgs e) { this.business.RightTeamInputBottom = this.textBoxRightTeamInputBottom.Text; }
        private void buttonRightTeamBottomEnableInput_Click(object sender, EventArgs e) { this.business.Vrighttablet_EnableInput(); }
        private void buttonRightTeamBottomDisableInput_Click(object sender, EventArgs e) { this.business.Vrighttablet_DisableInput(); }
        private void buttonRightTeamBottomClearInput_Click(object sender, EventArgs e) { this.business.Vrighttablet_ClearInput(); }
        private void buttonRightTeamInputTopIn_Click(object sender, EventArgs e) { this.business.Vinsert_InputIn_RightTop(); }
        private void buttonRightTeamInputBottomIn_Click(object sender, EventArgs e) { this.business.Vinsert_InputIn_RightBottom(); }
        private void buttonRightTeamInputIn_Click(object sender, EventArgs e) { this.business.Vinsert_InputIn_Right(false); }
        private void buttonRightTeamResolve_Click(object sender, EventArgs e) { this.business.Vinsert_InputIn_Right(true); }

        private void numericUpDownTaskCounter_ValueChanged(object sender, EventArgs e) { this.business.TaskCounter = (int)this.numericUpDownTaskCounter.Value; }

        private void listBoxDataList_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectDataset(this.listBoxDataList.SelectedIndex); }

        private void buttonShowVentuzHandler_Click(object sender, EventArgs e) {
            if (this.localVentuzHandlerForm == null) {
                this.localVentuzHandlerForm = new VRemote4.HandlerSi.BusinessForm(this.localVentuzHandler, this.BackColor);
                this.localVentuzHandlerForm.Disposed += this.localVentuzHandlerForm_Disposed;
            }
            this.localVentuzHandlerForm.Show();
            this.localVentuzHandlerForm.BringToFront();
        }
        private void localVentuzHandlerForm_Disposed(object sender, EventArgs e) {
            if (this.localVentuzHandlerForm is VRemote4.HandlerSi.BusinessForm) {
                this.localVentuzHandlerForm.Disposed -= this.localVentuzHandlerForm_Disposed;
                this.localVentuzHandlerForm = null;
            }
        }

        private void buttonVinsert_TaskIn_Click(object sender, EventArgs e) { this.business.Vinsert_TaskIn(); }
        private void buttonVinsert_InputIn_Click(object sender, EventArgs e) { this.business.Vinsert_InputIn(); }
        private void buttonVinsert_Resolve_Click(object sender, EventArgs e) { this.business.Vinsert_SetContent(true); }
        private void buttonVinsert_ContentOut_Click(object sender, EventArgs e) { this.business.Vinsert_ContentOut(); }

        private void buttonVstage_ContentIn_Click(object sender, EventArgs e) { this.business.Vstage_ContentIn(); }
        private void buttonVstage_ContentOut_Click(object sender, EventArgs e) { this.business.Vstage_ContentOut(); }

        private void buttonVhost_SetInput_Click(object sender, EventArgs e) { this.business.Vhost_SetContent(); }

        private void buttonVplayers_EnableInput_Click(object sender, EventArgs e) { this.business.Vplayers_EnableInput(); }

        private void buttonGame_StartTablets_Click(object sender, EventArgs e) {
            this.business.Vlefttablet_Start();
            this.business.Vrighttablet_Start();
        }
        private void buttonGame_Resolve_Click(object sender, EventArgs e) { this.business.Resolve(); }
        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }
        private void buttonGame_ShutTabletsDown_Click(object sender, EventArgs e) {
            this.business.Vlefttablet_ShutDown();
            this.business.Vrighttablet_ShutDown();
        }

        #endregion

    }
}
