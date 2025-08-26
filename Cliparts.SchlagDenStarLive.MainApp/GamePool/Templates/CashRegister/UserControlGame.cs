using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.CashRegister {

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

            this.numericUpDownLeftPlayerInput.Minimum = decimal.MinValue;
            this.numericUpDownLeftPlayerInput.Maximum = decimal.MaxValue;

            this.numericUpDownRightPlayerInput.Minimum = decimal.MinValue;
            this.numericUpDownRightPlayerInput.Maximum = decimal.MaxValue;

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Business localVentuzHandler) {
            base.Pose(business);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "LeftPlayerInput");
            bind.Format += (s, e) => { e.Value = Convert.ToDouble(e.Value); };
            this.numericUpDownLeftPlayerInput.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerInput");
            bind.Format += (s, e) => { e.Value = Convert.ToDouble(e.Value); };
            this.numericUpDownRightPlayerInput.DataBindings.Add(bind);

            this.localVentuzHandler = localVentuzHandler;

            VRemote4.HandlerSi.Client.Business displaysClient;
            if (this.localVentuzHandler.TryGetClient("Displays", out displaysClient)) this.userControlClientStateDisplays.Pose(displaysClient);
            this.userControlClientStateDisplays.BackColor = this.BackColor;

            VRemote4.HandlerSi.Client.Business contestantLeftTerminal;
            if (this.localVentuzHandler.TryGetClient("Left Terminal", out contestantLeftTerminal)) this.userControlClientStateLeftTerminal.Pose(contestantLeftTerminal);
            this.userControlClientStateLeftTerminal.BackColor = this.BackColor;

            VRemote4.HandlerSi.Client.Business contestantRightTerminal;
            if (this.localVentuzHandler.TryGetClient("Right Terminal", out contestantRightTerminal)) this.userControlClientStateRightTerminal.Pose(contestantRightTerminal);
            this.userControlClientStateRightTerminal.BackColor = this.BackColor;

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

            this.numericUpDownLeftPlayerInput.DataBindings.Clear();
            this.numericUpDownRightPlayerInput.DataBindings.Clear();

            this.userControlClientStateDisplays.Dispose();
            this.userControlClientStateLeftTerminal.Dispose();
            this.userControlClientStateRightTerminal.Dispose();
        }

        protected override void buildStepList() {

            int index = 0;
            stepAction step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_LoadScene);
            step.AddButton(this.buttonVdisplays_Start);
            step.AddButton(this.buttonVleftterminal_Start);
            step.AddButton(this.buttonVrightterminal_Start);
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
            step.AddButton(this.buttonVdisplays_Init);
            step.AddButton(this.buttonVleftterminal_Init);
            step.AddButton(this.buttonVrightterminal_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVdisplays_HideLogo);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVleftterminal_Unlock);
            step.AddButton(this.buttonVrightterminal_Unlock);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ContentIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVdisplays_ShowInput);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ShowSolution);
            step.AddButton(this.buttonVleftterminal_Lock);
            step.AddButton(this.buttonVrightterminal_Lock);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ContentOut);
            step.AddButton(this.buttonVdisplays_ClearInput);
            step.AddButton(this.buttonVleftterminal_Reset);
            step.AddButton(this.buttonVrightterminal_Reset);
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
            step.AddButton(this.buttonVfullscreen_ShowGameboard);
            step.AddButton(this.buttonGame_SetWinner);
            step.AddButton(this.buttonVdisplays_ShutDown);
            step.AddButton(this.buttonVleftterminal_ShutDown);
            step.AddButton(this.buttonVrightterminal_ShutDown);
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

        void selectedDataset_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.selectedDataset_PropertyChanged(sender, e)));
            else {
            }
        }

        private void localVentuzHandlerForm_Disposed(object sender, EventArgs e) {
            if (this.localVentuzHandlerForm is VRemote4.HandlerSi.BusinessForm) {
                this.localVentuzHandlerForm.Disposed -= this.localVentuzHandlerForm_Disposed;
                this.localVentuzHandlerForm = null;
            }
        }

        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void UserControlGame_BackColorChanged(object sender, EventArgs e) {
            this.userControlClientStateDisplays.BackColor = this.BackColor;
            this.userControlClientStateLeftTerminal.BackColor = this.BackColor;
            this.userControlClientStateRightTerminal.BackColor = this.BackColor;
        }

        private void listBoxDataList_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectDataset(this.listBoxDataList.SelectedIndex); }

        private void numericUpDownLeftPlayerInput_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerInput = Convert.ToDouble(this.numericUpDownLeftPlayerInput.Value); }
        private void numericUpDownRightPlayerInput_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerInput = Convert.ToDouble(this.numericUpDownRightPlayerInput.Value); }

        private void buttonShowVentuzHandler_Click(object sender, EventArgs e) {
            if (this.localVentuzHandlerForm == null) {
                this.localVentuzHandlerForm = new VRemote4.HandlerSi.BusinessForm(this.localVentuzHandler, this.BackColor);
                this.localVentuzHandlerForm.Disposed += this.localVentuzHandlerForm_Disposed;
            }
            this.localVentuzHandlerForm.Show();
            this.localVentuzHandlerForm.BringToFront();
        }

        private void buttonVinsert_ContentIn_Click(object sender, EventArgs e) { this.business.Vinsert_ContentIn(); }
        private void buttonVinsert_ShowSolution_Click(object sender, EventArgs e) { this.business.Vinsert_ShowSolution(); }
        private void buttonVinsert_ContentOut_Click(object sender, EventArgs e) { this.business.Vinsert_ContentOut(); }

        private void buttonVdisplays_Start_Click(object sender, EventArgs e) { this.business.Vdisplays_Start(); }
        private void buttonVdisplays_Init_Click(object sender, EventArgs e) { this.business.Vdisplays_Init(); }
        private void buttonVdisplays_HideLogo_Click(object sender, EventArgs e) { this.business.Vdisplays_HideLogo(); }
        private void buttonVdisplays_ShowInput_Click(object sender, EventArgs e) { this.business.Vdisplays_ShowInput(); }
        private void buttonVdisplays_ClearInput_Click(object sender, EventArgs e) { this.business.Vdisplays_ClearInput(); }
        private void buttonVdisplays_ShutDown_Click(object sender, EventArgs e) { this.business.Vdisplays_ShutDown(); }

        private void buttonVleftterminal_Start_Click(object sender, EventArgs e) { this.business.Vleftterminal_Start(); }
        private void buttonVleftterminal_Init_Click(object sender, EventArgs e) { this.business.Vleftterminal_Init(); }
        private void buttonVleftterminal_Unlock_Click(object sender, EventArgs e) { this.business.Vleftterminal_Unlock(); }
        private void buttonVleftterminal_Lock_Click(object sender, EventArgs e) { this.business.Vleftterminal_Lock(); }
        private void buttonVleftterminal_Reset_Click(object sender, EventArgs e) { this.business.Vleftterminal_Reset(); }
        private void buttonVleftterminal_ShutDown_Click(object sender, EventArgs e) { this.business.Vleftterminal_ShutDown(); }

        private void buttonVrightterminal_Start_Click(object sender, EventArgs e) { this.business.Vrightterminal_Start(); }
        private void buttonVrightTerminal_Init_Click(object sender, EventArgs e) { this.business.Vrightterminal_Init(); }
        private void buttonVrightTerminal_Unlock_Click(object sender, EventArgs e) { this.business.Vrightterminal_Unlock(); }
        private void buttonVrightTerminal_Lock_Click(object sender, EventArgs e) { this.business.Vrightterminal_Lock(); }
        private void buttonVrightTerminal_Reset_Click(object sender, EventArgs e) { this.business.Vrightterminal_Reset(); }
        private void buttonVrightterminal_ShutDown_Click(object sender, EventArgs e) { this.business.Vrightterminal_ShutDown(); }

        private void buttonGame_Resolve_Click(object sender, EventArgs e) { this.business.Resolve(); }
        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }

        #endregion

    }

}
