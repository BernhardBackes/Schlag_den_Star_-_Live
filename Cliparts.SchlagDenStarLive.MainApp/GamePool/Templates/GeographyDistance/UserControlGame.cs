using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.GeographyDistance
{

    public partial class UserControlGame : _Base.TimerScore.UserControlGame {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;

        private VRemote4.HandlerSi.Business localVentuzHandler;
        private VRemote4.HandlerSi.BusinessForm localVentuzHandlerForm;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business,
            VRemote4.HandlerSi.Business localVentuzHandler) {
            base.Pose(business);

            this.business = business;

            Binding bind;

            bind = new Binding("Text", this.business, "LeftPlayerInput1");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxLeftPlayerInput1.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "LeftPlayerInput2");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxLeftPlayerInput2.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "LeftPlayerDistance");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxLeftPlayerDistance.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerInput1");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxRightPlayerInput1.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerInput2");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxRightPlayerInput2.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerDistance");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxRightPlayerDistance.DataBindings.Add(bind);

            this.localVentuzHandler = localVentuzHandler;

            VRemote4.HandlerSi.Client.Business leftTeamTabletClient;
            if (this.localVentuzHandler.TryGetClient("Left Tablet", out leftTeamTabletClient)) this.userControlClientStateLeftTeamTablet.Pose(leftTeamTabletClient);
            this.userControlClientStateLeftTeamTablet.BackColor = this.BackColor;

            VRemote4.HandlerSi.Client.Business rightTeamTabletClient;
            if (this.localVentuzHandler.TryGetClient("Right Tablet", out rightTeamTabletClient)) this.userControlClientStateRightTeamTablet.Pose(rightTeamTabletClient);
            this.userControlClientStateRightTeamTablet.BackColor = this.BackColor;

            this.setCloserPlayer();

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

            this.textBoxLeftPlayerInput1.DataBindings.Clear();
            this.textBoxLeftPlayerInput2.DataBindings.Clear();
            this.textBoxLeftPlayerDistance.DataBindings.Clear();
            this.textBoxRightPlayerInput1.DataBindings.Clear();
            this.textBoxRightPlayerInput2.DataBindings.Clear();
            this.textBoxRightPlayerDistance.DataBindings.Clear();

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
            step.AddButton(this.buttonVfullscreen_ShowGame);
            step.AddButton(this.buttonVstage_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVstage_MapIn);
            step.AddButton(this.buttonVfullscreen_TextIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TextIn);
            step.AddButton(this.buttonVinsert_TimerIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVPlayer_UnlockTouch);
            step.AddButton(this.buttonVinsert_StartTimer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_StopTimer);
            step.AddButton(this.buttonVHost_SetMap);
            step.AddButton(this.buttonVPlayer_LockTouch);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimerOut);
            step.AddButton(this.buttonVfullscreen_BluePinIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_RedPinIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_SolutionIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_OffsetIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TextOut);
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
            step = new stepAction(index, (stepIndex) => stepIndex - 11);
            step.AddButton(this.buttonVinsert_ScoreOut);
            step.AddButton(this.buttonVfullscreen_Reset);
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
            step.AddButton(this.buttonVinsert_UnloadScene);
            step.AddButton(this.buttonVfullscreen_UnloadScene);
            step.AddButton(this.buttonVhost_UnloadScene);
            step.AddButton(this.buttonVleftplayer_UnloadScene);
            step.AddButton(this.buttonVrightplayer_UnloadScene);
            step.AddButton(this.buttonVfullscreen_ShowGameboard);
            step.AddButton(this.buttonGame_SetWinner);
            step.AddButton(this.buttonGame_ShutTabletsDown);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_UpdateGameboard);
            this.stepList.Add(step);

            if (index < this.stepsMaxIndex) this.on_Error("buildStepList", "stepList to short");
            if (index > this.stepsMaxIndex) this.on_Error("buildStepList", "stepList to long");
        }

        private void setCloserPlayer() {
            switch (this.business.CloserPlayer) {
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.LeftPlayer:
                    this.textBoxLeftPlayerDistance.BackColor = Constants.ColorEnabled;
                    this.textBoxRightPlayerDistance.BackColor = SystemColors.Control;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.RightPlayer:
                    this.textBoxLeftPlayerDistance.BackColor = SystemColors.Control;
                    this.textBoxRightPlayerDistance.BackColor = Constants.ColorEnabled;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.NotSelected:
                default:
                    this.textBoxLeftPlayerDistance.BackColor = SystemColors.Control;
                    this.textBoxRightPlayerDistance.BackColor = SystemColors.Control;
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
                else if (e.PropertyName == "CloserPlayer") this.setCloserPlayer();
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

        private void buttonLeftPlayerSetInput1_Click(object sender, EventArgs e) { this.business.SetLeftPlayerInput1(this.textBoxLeftPlayerInput1.Text); }
        private void buttonLeftPlayerSetInput2_Click(object sender, EventArgs e) { this.business.SetLeftPlayerInput2(this.textBoxLeftPlayerInput2.Text); }
        private void buttonRightPlayerSetInput1_Click(object sender, EventArgs e) { this.business.SetRightPlayerInput1(this.textBoxRightPlayerInput1.Text); }
        private void buttonRightPlayerSetInput2_Click(object sender, EventArgs e) { this.business.SetRightPlayerInput2(this.textBoxRightPlayerInput2.Text); }

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

        private void buttonVinsert_TextIn_Click(object sender, EventArgs e) { this.business.Vinsert_TextInsertIn(); }
        private void buttonVinsert_TextOut_Click(object sender, EventArgs e) { this.business.Vinsert_TextInsertOut(); }

        private void buttonVfullscreen_TextIn_Click(object sender, EventArgs e) { this.business.Vfullscreen_TextIn(); }
        private void buttonVfullscreen_BluePinIn_Click(object sender, EventArgs e) { this.business.Vfullscreen_BluePinsIn(); }
        private void buttonVfullscreen_RedPinIn_Click(object sender, EventArgs e) { this.business.Vfullscreen_RedPinsIn(); }
        private void buttonVfullscreen_SolutionIn_Click(object sender, EventArgs e) { this.business.Vfullscreen_SolutionIn(); }
        private void buttonVfullscreen_OffsetIn_Click(object sender, EventArgs e) { this.business.Vfullscreen_OffsetIn(); }
        private void buttonVfullscreen_Reset_Click(object sender, EventArgs e) { this.business.Vfullscreen_Reset(); }

        private void buttonVstage_MapIn_Click(object sender, EventArgs e) { this.business.Vstage_MapIn(false); }
        private void buttonVstage_MapOut_Click(object sender, EventArgs e) { this.business.Vstage_MapOut(); }
        private void buttonVstage_SampleIn_Click(object sender, EventArgs e) { this.business.Vstage_MapIn(true); }

        private void buttonVHost_SetMap_Click(object sender, EventArgs e) { this.business.Vhost_SetMap(); }

        private void buttonVPlayer_UnlockTouch_Click(object sender, EventArgs e) { this.business.Vplayer_UnlockTouch(); }
        private void buttonVPlayer_LockTouch_Click(object sender, EventArgs e) { this.business.Vplayer_LockTouch(); }

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
