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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TrueOrFalseMultiple;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TrueOrFalseMultiple {

    public partial class UserControlGame : _Base.Score.UserControlGame {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;

        private DatasetItem selectedDatasetItem = null;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.comboBoxLeftPlayerInput.BeginUpdate();
            this.comboBoxLeftPlayerInput.Items.AddRange(Enum.GetNames(typeof(SelectionValues)));
            this.comboBoxLeftPlayerInput.EndUpdate();

            this.comboBoxRightPlayerInput.BeginUpdate();
            this.comboBoxRightPlayerInput.Items.AddRange(Enum.GetNames(typeof(SelectionValues)));
            this.comboBoxRightPlayerInput.EndUpdate();

            this.textBoxLeftPlayerName_1.BackColor = this.textBoxLeftPlayerName.BackColor;
            this.textBoxRightPlayerName_1.BackColor = this.textBoxRightPlayerName.BackColor;

            this.numericUpDownLeftPlayerCounter.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerCounter.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerCounter.Minimum = int.MinValue;
            this.numericUpDownRightPlayerCounter.Maximum = int.MaxValue;

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            this.business = business;

            Binding bind;

            bind = new Binding("Text", this.business, "LeftPlayerInput");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxLeftPlayerInput.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "LeftPlayerCorrectInput");
            bind.Format += (s, e) => { 
                if (e.Value is null) e.Value = SystemColors.Control; 
                else if (e.Value.Equals(true)) e.Value = Constants.ColorTrue;
                else e.Value = Constants.ColorFalse;
            };
            this.comboBoxLeftPlayerInput.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerInput");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxRightPlayerInput.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "RightPlayerCorrectInput");
            bind.Format += (s, e) => {
                if (e.Value is null) e.Value = SystemColors.Control;
                else if (e.Value.Equals(true)) e.Value = Constants.ColorTrue;
                else e.Value = Constants.ColorFalse;
            };
            this.comboBoxRightPlayerInput.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "LeftPlayerName");
            this.textBoxLeftPlayerName_1.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerName");
            this.textBoxRightPlayerName_1.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerCounter");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerCounter.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerCounter");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerCounter.DataBindings.Add(bind);

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

            this.comboBoxLeftPlayerInput.DataBindings.Clear();
            this.comboBoxRightPlayerInput.DataBindings.Clear();

            this.textBoxLeftPlayerName_1.DataBindings.Clear();
            this.textBoxRightPlayerName_1.DataBindings.Clear();

            this.numericUpDownLeftPlayerCounter.DataBindings.Clear();
            this.numericUpDownRightPlayerCounter.DataBindings.Clear();
        }

        protected override void buildStepList() {

            int index = 0;
            stepAction step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_LoadScene);
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
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ScoreIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_GameIn);
            step.AddButton(this.buttonVstage_GameIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ItemIn);
            step.AddButton(this.buttonVplayers_ItemIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_StartCountDown);
            step.AddButton(this.buttonVplayers_StartCountDown);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => 
            {
                if (this.business.IsLastItem) return stepIndex + 2; 
                else return stepIndex + 1;
            });
            step.AddButton(this.buttonVplayers_LockInput);
            step.AddButton(this.buttonVinsert_SetInput);
            step.AddButton(this.buttonVhost_SetInput);
            step.AddButton(this.buttonVplayersSetInput);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 3);
            step.AddButton(this.buttonGame_NextItem);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_InitResolve);
            step.AddButton(this.buttonVinsert_CounterIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_SolutionIn);
            step.AddButton(this.buttonVstage_SolutionIn);
            step.AddButton(this.buttonGame_ResolveItem);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) =>
            {
                if (this.business.IsLastItem) return stepIndex + 2;
                else return stepIndex + 1;
            });
            step.AddButton(this.buttonVinsert_SetCounter);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 2);
            step.AddButton(this.buttonGame_NextItemResolving);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_ResolveSet);
            step.AddButton(this.buttonVinsert_SetScore);
            step.AddButton(this.buttonVstage_SetScore);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 10);
            step.AddButton(this.buttonVinsert_CounterOut);
            step.AddButton(this.buttonVinsert_GameOut);
            step.AddButton(this.buttonVstage_GameOut);
            step.AddButton(this.buttonGame_Next);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
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

        private void fillDataList()
        {
            this.listBoxDatasetList.BeginUpdate();
            this.listBoxDatasetList.Items.Clear();
            int id = 1;
            if (this.business.SampleIncluded) id = 0;
            foreach (string item in this.business.NameList)
            {
                this.listBoxDatasetList.Items.Add(string.Format("{0}: {1}", id.ToString("00"), item));
                id++;
            }
            this.listBoxDatasetList.EndUpdate();

            this.listBoxDatasetList.Enabled = this.listBoxDatasetList.Items.Count > 0;
            Helper.setControlBackColor(this.listBoxDatasetList);

            this.selectDataList();
        }

        private void selectDataList()
        {
            int index = this.business.GetDatasetIndex(this.selectedDataset);
            if (index >= 0 &&
                index < this.listBoxDatasetList.Items.Count) this.listBoxDatasetList.SelectedIndex = index;
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

            this.fillListBoxDatasetItems();
        }

        private void fillListBoxDatasetItems()
        {
            this.listBoxDatasetItemList.BeginUpdate();
            this.listBoxDatasetItemList.Items.Clear();
            if (this.selectedDataset is DatasetContent) this.listBoxDatasetItemList.Items.AddRange(this.selectedDataset.ItemList);
            this.listBoxDatasetItemList.Enabled = this.listBoxDatasetItemList.Items.Count > 0;
            Helper.setControlBackColor(this.listBoxDatasetItemList);
            this.listBoxDatasetItemList.EndUpdate();

            this.selectDatasetItem(0);
        }

        private void selectDatasetItem(
            int index)
        {
            if (index < 0) index = 0;
            this.selectedDatasetItem = null;
            if (this.selectedDataset is DatasetContent)
            {
                if (index >= this.selectedDataset.ItemsCount) index = this.selectedDataset.ItemsCount - 1;
                this.selectedDatasetItem = this.selectedDataset.GetItem(index);
            }

            if (this.selectedDatasetItem is DatasetItem)
            {
                if (this.listBoxDatasetItemList.Items.Count > index) this.listBoxDatasetItemList.SelectedIndex = index;
                this.textBoxDatasetItemSolution.Text = this.selectedDatasetItem.Solution.ToString();
            }
            else
            {
                this.textBoxDatasetItemSolution.Text = string.Empty;
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
                else if (e.PropertyName == "SelectedDatasetItem") this.selectDatasetItem(this.business.SelectedDatasetItemIndex);

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

        private void comboBoxLeftPlayerInput_SelectedIndexChanged(object sender, EventArgs e) { this.business.LeftPlayerInput = (SelectionValues)this.comboBoxLeftPlayerInput.SelectedIndex; }
        private void buttonLeftPlayerLockInput_Click(object sender, EventArgs e) { this.business.Vleftplayer_LockInput(); }

        private void comboBoxRightPlayerInput_SelectedIndexChanged(object sender, EventArgs e) { this.business.RightPlayerInput = (SelectionValues)this.comboBoxRightPlayerInput.SelectedIndex; }
        private void buttonRightPlayerLockInput_Click(object sender, EventArgs e) { this.business.Vrightplayer_LockInput(); }

        protected virtual void numericUpDownLeftPlayerCounter_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerCounter = (int)this.numericUpDownLeftPlayerCounter.Value; }
        protected virtual void buttonLeftPlayerAddCounterHot_01_Click(object sender, EventArgs e) {
            this.business.LeftPlayerCounter++;
            this.business.Vinsert_SetCounter();
        }
        private void buttonLeftPlayerSubtractCounterHot_01_Click(object sender, EventArgs e) {
            this.business.LeftPlayerCounter--;
            this.business.Vinsert_SetCounter();
        }

        protected virtual void numericUpDownRightPlayerCounter_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerCounter = (int)this.numericUpDownRightPlayerCounter.Value; }
        protected virtual void buttonRightPlayerAddCounterHot_01_Click(object sender, EventArgs e) {
            this.business.RightPlayerCounter++;
            this.business.Vinsert_SetCounter();
        }
        private void buttonRightPlayerSubtractCounterHot_01_Click(object sender, EventArgs e) {
            this.business.RightPlayerCounter--;
            this.business.Vinsert_SetCounter();
        }

        private void listBoxDatasetList_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectDataset(this.listBoxDatasetList.SelectedIndex); }
        private void listBoxDatasetItemList_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectDatasetItem(this.listBoxDatasetItemList.SelectedIndex); }

        private void buttonVinsert_CounterIn_Click(object sender, EventArgs e) { this.business.Vinsert_CounterIn(); }
        private void buttonVinsert_GameIn_Click(object sender, EventArgs e) { this.business.Vinsert_GameIn(); }
        private void buttonVinsert_ItemIn_Click(object sender, EventArgs e) { this.business.Vinsert_ItemIn(); }
        private void buttonVinsert_StartCountdown_Click(object sender, EventArgs e) { this.business.Vinsert_StartCountdown(); }
        private void buttonVinsert_SetInput_Click(object sender, EventArgs e) { this.business.Vinsert_SetInput(); }
        private void buttonVinsert_SolutionIn_Click(object sender, EventArgs e) { this.business.Vinsert_SolutionIn(); }
        private void buttonVinsert_SetCounter_Click(object sender, EventArgs e) { this.business.Vinsert_SetCounter(); }
        private void buttonVinsert_CounterOut_Click(object sender, EventArgs e) { this.business.Vinsert_CounterOut(); }
        private void buttonVinsert_GameOut_Click(object sender, EventArgs e) { this.business.Vinsert_GameOut(); }

        private void buttonVstage_GameIn_Click(object sender, EventArgs e) 
        {
            this.business.Vhost_GameIn();
            this.business.Vleftplayer_GameIn();
            this.business.Vrightplayer_GameIn();
        }
        private void buttonVstage_SolutionIn_Click(object sender, EventArgs e) 
        {
            this.business.Vhost_SolutionIn();
            this.business.Vleftplayer_SolutionIn();
            this.business.Vrightplayer_SolutionIn();
        }
        private void buttonVstage_GameOut_Click(object sender, EventArgs e)
        {
            this.business.Vhost_GameOut();
            this.business.Vleftplayer_GameOut();
            this.business.Vrightplayer_GameOut();
        }

        private void buttonVhost_SetInput_Click(object sender, EventArgs e) { this.business.Vhost_SetInput(); }

        private void buttonVplayers_ItemIn_Click(object sender, EventArgs e)
        {
            this.business.Vleftplayer_ItemIn();
            this.business.Vrightplayer_ItemIn();
        }
        private void buttonVplayers_StartCountDown_Click(object sender, EventArgs e)
        {
            this.business.VleftPlayer_StartCountDown();
            this.business.Vrightplayer_StartCountDown();
        }
        private void buttonVplayers_LockInput_Click(object sender, EventArgs e) 
        {
            this.business.Vleftplayer_LockInput();
            this.business.Vrightplayer_LockInput();
        }
        private void buttonVplayersSetInput_Click(object sender, EventArgs e)
        {
            this.business.Vleftplayer_SetInput();
            this.business.Vrightplayer_SetInput();
        }

        private void buttonGame_NextItem_Click(object sender, EventArgs e) { this.business.NextItem(false); }
        private void buttonGame_InitResolve_Click(object sender, EventArgs e) { this.business.InitResolve(); }
        private void buttonGame_ResolveItem_Click(object sender, EventArgs e) { this.business.ResolveItem(); }
        private void buttonGame_NextItemResolving_Click(object sender, EventArgs e) { this.business.NextItem(true); }
        private void buttonGame_ResolveSet_Click(object sender, EventArgs e) { this.business.ResolveSet(); }
        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }

        #endregion

    }
}
