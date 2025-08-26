using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SelectTwoFields.Game;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.SelectTwoFields
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

            this.comboBoxLeftTeamInputDesk.Items.AddRange(Enum.GetNames(typeof(FieldIDElements)));
            this.comboBoxLeftTeamInputTablet.Items.AddRange(Enum.GetNames(typeof(FieldIDElements)));
            this.comboBoxRightTeamInputDesk.Items.AddRange(Enum.GetNames(typeof(FieldIDElements)));
            this.comboBoxRightTeamInputTablet.Items.AddRange(Enum.GetNames(typeof(FieldIDElements)));

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

            bind = new Binding("Text", this.business, "LeftTeamInputDesk");
            bind.Format += (s, e) => { e.Value = e.Value is FieldIDElements ? e.Value.ToString() : string.Empty; };
            this.comboBoxLeftTeamInputDesk.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "LeftTeamInputTablet");
            bind.Format += (s, e) => { e.Value = e.Value is FieldIDElements ? e.Value.ToString() : string.Empty; };
            this.comboBoxLeftTeamInputTablet.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightTeamInputDesk");
            bind.Format += (s, e) => { e.Value = e.Value is FieldIDElements ? e.Value.ToString() : string.Empty; };
            this.comboBoxRightTeamInputDesk.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightTeamInputTablet");
            bind.Format += (s, e) => { e.Value = e.Value is FieldIDElements ? e.Value.ToString() : string.Empty; };
            this.comboBoxRightTeamInputTablet.DataBindings.Add(bind);

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

            this.setSelectedTeam();

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

            this.comboBoxLeftTeamInputDesk.DataBindings.Clear();
            this.comboBoxLeftTeamInputTablet.DataBindings.Clear();
            this.comboBoxRightTeamInputDesk.DataBindings.Clear();
            this.comboBoxRightTeamInputTablet.DataBindings.Clear();

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
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_ShowGame);
            step.AddButton(this.buttonVstage_Init);
            step.AddButton(this.buttonGame_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_BorderIn);
            step.AddButton(this.buttonVfullscreen_ContentIn);
            step.AddButton(this.buttonVhost_ContentIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVplayers_ContentIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_SetBorder);
            step.AddButton(this.buttonVhost_SetInput);
            step.AddButton(this.buttonVplayers_LockTouch);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_SetInput);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_Resolve);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_BorderOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ScoreIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_SetScore);
            step.AddButton(this.buttonVstage_SetScore);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 8);
            step.AddButton(this.buttonVinsert_ScoreOut);
            step.AddButton(this.buttonVfullscreen_ContentOut);
            step.AddButton(this.buttonVstage_ContentOut);
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
            step.AddButton(this.buttonVrightplayer_UnloadScene);
            step.AddButton(this.buttonGame_ShutTabletsDown);
            step.AddButton(this.buttonGame_SetWinner);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_UpdateGameboard);
            this.stepList.Add(step);

            if (index < this.stepsMaxIndex) this.on_Error("buildStepList", "stepList to short");
            if (index > this.stepsMaxIndex) this.on_Error("buildStepList", "stepList to long");
        }

        private void setSelectedTeam()
        {
            switch (this.business.SelectedTeam)
            {
                case Content.Gameboard.PlayerSelection.LeftPlayer:
                    this.buttonLeftTeamSelected.BackColor = Constants.ColorSelected;
                    this.buttonRightTeamSelected.UseVisualStyleBackColor = true;
                    break;
                case Content.Gameboard.PlayerSelection.RightPlayer:
                    this.buttonLeftTeamSelected.UseVisualStyleBackColor = true;
                    this.buttonRightTeamSelected.BackColor = Constants.ColorSelected;
                    break;
                case Content.Gameboard.PlayerSelection.NotSelected:
                default:
                    this.buttonLeftTeamSelected.UseVisualStyleBackColor = true;
                    this.buttonRightTeamSelected.UseVisualStyleBackColor = true;
                    break;
            }
        }

        private void fillDataList()
        {
            this.listBoxDataList.BeginUpdate();
            this.listBoxDataList.Items.Clear();
            int id = 1;
            if (this.business.SampleIncluded) id = 0;
            foreach (string item in this.business.NameList)
            {
                this.listBoxDataList.Items.Add(string.Format("{0}: {1}", id.ToString("00"), item));
                id++;
            }
            this.listBoxDataList.EndUpdate();

            this.listBoxDataList.Enabled = this.listBoxDataList.Items.Count > 0;
            Helper.setControlBackColor(this.listBoxDataList);

            this.selectDataList();
        }

        private void selectDataList()
        {
            int index = this.business.GetDatasetIndex(this.selectedDataset);
            if (index >= 0 &&
                index < this.listBoxDataList.Items.Count) this.listBoxDataList.SelectedIndex = index;
        }

        private void selectDataset(
            DatasetContent selectedDataset)
        {
            if (this.selectedDataset != selectedDataset)
            {
                //Dispose...
                if (this.selectedDataset is DatasetContent)
                {
                    this.selectedDataset.PropertyChanged -= this.selectedDataset_PropertyChanged;
                }
                this.selectedDataset = selectedDataset;
                //Pose...
                if (this.selectedDataset is DatasetContent)
                {
                    this.selectedDataset.PropertyChanged += this.selectedDataset_PropertyChanged;
                }
            }

            this.selectDataList();
        }

        #endregion


        #region Events.Incoming

        protected override void business_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.business_PropertyChanged(sender, e);
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else
            {
                if (e.PropertyName == "NameList") this.fillDataList();
                else if (e.PropertyName == "SampleIncluded") this.fillDataList();
                else if (e.PropertyName == "SelectedDataset") this.selectDataset(this.business.SelectedDataset);
                else if (e.PropertyName == "SelectedDatasetIndex") this.selectDataList();
                else if (e.PropertyName == "SelectedTeam") this.setSelectedTeam();
            }
        }

        void selectedDataset_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.selectedDataset_PropertyChanged(sender, e)));
            else
            {
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
        private void comboBoxLeftTeamInputTablet_SelectedIndexChanged(object sender, EventArgs e)
        {
            FieldIDElements id;
            if (Enum.TryParse(this.comboBoxLeftTeamInputTablet.Text, out id)) this.business.LeftTeamInputTablet = id;
        }
        private void comboBoxLeftTeamInputDesk_SelectedIndexChanged(object sender, EventArgs e)
        {
            FieldIDElements id;
            if (Enum.TryParse(this.comboBoxLeftTeamInputDesk.Text, out id)) this.business.LeftTeamInputDesk = id;
        }
        private void buttonLeftTeamSelected_Click(object sender, EventArgs e) { this.business.SelectedTeam = Content.Gameboard.PlayerSelection.LeftPlayer; }

        private void comboBoxRightTeamInputDesk_SelectedIndexChanged(object sender, EventArgs e)
        {
            FieldIDElements id;
            if (Enum.TryParse(this.comboBoxRightTeamInputDesk.Text, out id)) this.business.RightTeamInputDesk = id;
        }
        private void comboBoxRightTeamInputTablet_SelectedIndexChanged(object sender, EventArgs e)
        {
            FieldIDElements id;
            if (Enum.TryParse(this.comboBoxRightTeamInputTablet.Text, out id)) this.business.RightTeamInputTablet = id;
        }
        private void buttonRightTeamSelected_Click(object sender, EventArgs e) { this.business.SelectedTeam = Content.Gameboard.PlayerSelection.RightPlayer; }

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

        private void buttonVinsert_BorderIn_Click(object sender, EventArgs e) { this.business.Vinsert_BorderIn(); }
        private void buttonVinsert_SetBorder_Click(object sender, EventArgs e) { this.business.Vinsert_SetBorder(); }
        private void buttonVinsert_BorderOut_Click(object sender, EventArgs e) { this.business.Vinsert_BorderOut(); }

        private void buttonVfullscreen_ContentIn_Click(object sender, EventArgs e) { this.business.Vfullscreen_ContentIn(); }
        private void buttonVfullscreen_SetInput_Click(object sender, EventArgs e) { this.business.Vfullscreen_SetInput(); }
        private void buttonVfullscreen_Resolve_Click(object sender, EventArgs e) { this.business.Vfullscreen_Resolve(); }
        private void buttonVfullscreen_ContentOut_Click(object sender, EventArgs e) { this.business.Vfullscreen_ContentOut(); }

        private void buttonVhost_ContentIn_Click(object sender, EventArgs e) { this.business.Vhost_ContentIn(); }
        private void buttonVhost_SetInput_Click(object sender, EventArgs e) { this.business.Vhost_SetInput(); }

        private void buttonVplayers_ContentIn_Click(object sender, EventArgs e) { this.business.Vplayers_ContentIn(); }
        private void buttonVplayers_LockTouch_Click(object sender, EventArgs e) { this.business.Vplayers_DisableInput(); }

        private void buttonVstage_ContentOut_Click(object sender, EventArgs e) { this.business.Vstage_ContentOut(); }

        private void buttonGame_StartTablets_Click(object sender, EventArgs e)
        {
            this.business.Vlefttablet_Start();
            this.business.Vrighttablet_Start();
        }
        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }
        private void buttonGame_ShutTabletsDown_Click(object sender, EventArgs e)
        {
            this.business.Vlefttablet_ShutDown();
            this.business.Vrighttablet_ShutDown();
        }

        #endregion

    }

}
