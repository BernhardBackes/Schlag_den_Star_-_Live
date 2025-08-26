using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SortItemsScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.SortItemsScore {

    public partial class UserControlGame : _Base.Score.UserControlGame {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.numericUpDownTaskCounter.Minimum = int.MinValue;
            this.numericUpDownTaskCounter.Maximum = int.MaxValue;

            this.numericUpDownSelectedItemResult.Minimum = int.MinValue;
            this.numericUpDownSelectedItemResult.Maximum = int.MaxValue;

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            this.setComboBoxPlayerInputItems();

            Binding bind;

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

            this.business.PropertyChanged -= this.business_PropertyChanged;
            this.numericUpDownTaskCounter.DataBindings.Clear();
        }

        protected override void buildStepList() {

            int index = 0;
            stepAction step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_LoadScene);
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
            step.AddButton(this.buttonVplayers_ContentIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVplayers_ReleaseInput);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVplayers_LockInput);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ContentIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_Resolve);
            step.AddButton(this.buttonGame_Resolve);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_SetScore);
            step.AddButton(this.buttonVstage_SetScore);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ContentOut);
            step.AddButton(this.buttonVplayers_ContentOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ScoreIn);
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
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_UnloadScene);
            step.AddButton(this.buttonVfullscreen_ShowGameboard);
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

        private void setComboBoxPlayerInputItems() {
            this.comboBoxLeftPlayerInput_00.Items.Clear();
            this.comboBoxLeftPlayerInput_01.Items.Clear();
            this.comboBoxLeftPlayerInput_02.Items.Clear();
            this.comboBoxLeftPlayerInput_03.Items.Clear();
            this.comboBoxLeftPlayerInput_04.Items.Clear();
            this.comboBoxRightPlayerInput_00.Items.Clear();
            this.comboBoxRightPlayerInput_01.Items.Clear();
            this.comboBoxRightPlayerInput_02.Items.Clear();
            this.comboBoxRightPlayerInput_03.Items.Clear();
            this.comboBoxRightPlayerInput_04.Items.Clear();
            if (this.selectedDataset is DatasetContent) {
                this.comboBoxLeftPlayerInput_00.Items.AddRange(this.selectedDataset.Items);
                this.comboBoxLeftPlayerInput_00.Enabled = true;
                this.comboBoxLeftPlayerInput_01.Items.AddRange(this.selectedDataset.Items);
                this.comboBoxLeftPlayerInput_01.Enabled = true;
                this.comboBoxLeftPlayerInput_02.Items.AddRange(this.selectedDataset.Items);
                this.comboBoxLeftPlayerInput_02.Enabled = true;
                this.comboBoxLeftPlayerInput_03.Items.AddRange(this.selectedDataset.Items);
                this.comboBoxLeftPlayerInput_03.Enabled = true;
                this.comboBoxLeftPlayerInput_04.Items.AddRange(this.selectedDataset.Items);
                this.comboBoxLeftPlayerInput_04.Enabled = true;
                this.comboBoxRightPlayerInput_00.Items.AddRange(this.selectedDataset.Items);
                this.comboBoxRightPlayerInput_00.Enabled = true;
                this.comboBoxRightPlayerInput_01.Items.AddRange(this.selectedDataset.Items);
                this.comboBoxRightPlayerInput_01.Enabled = true;
                this.comboBoxRightPlayerInput_02.Items.AddRange(this.selectedDataset.Items);
                this.comboBoxRightPlayerInput_02.Enabled = true;
                this.comboBoxRightPlayerInput_03.Items.AddRange(this.selectedDataset.Items);
                this.comboBoxRightPlayerInput_03.Enabled = true;
                this.comboBoxRightPlayerInput_04.Items.AddRange(this.selectedDataset.Items);
                this.comboBoxRightPlayerInput_04.Enabled = true;
            }
            else {
                this.comboBoxLeftPlayerInput_00.Enabled = false;
                this.comboBoxLeftPlayerInput_01.Enabled = false;
                this.comboBoxLeftPlayerInput_02.Enabled = false;
                this.comboBoxLeftPlayerInput_03.Enabled = false;
                this.comboBoxLeftPlayerInput_04.Enabled = false;
                this.comboBoxRightPlayerInput_00.Enabled = false;
                this.comboBoxRightPlayerInput_01.Enabled = false;
                this.comboBoxRightPlayerInput_02.Enabled = false;
                this.comboBoxRightPlayerInput_03.Enabled = false;
                this.comboBoxRightPlayerInput_04.Enabled = false;
            }
            Helper.setControlBackColor(this.comboBoxLeftPlayerInput_00);
            Helper.setControlBackColor(this.comboBoxLeftPlayerInput_01);
            Helper.setControlBackColor(this.comboBoxLeftPlayerInput_02);
            Helper.setControlBackColor(this.comboBoxLeftPlayerInput_03);
            Helper.setControlBackColor(this.comboBoxLeftPlayerInput_04);
            Helper.setControlBackColor(this.comboBoxRightPlayerInput_00);
            Helper.setControlBackColor(this.comboBoxRightPlayerInput_01);
            Helper.setControlBackColor(this.comboBoxRightPlayerInput_02);
            Helper.setControlBackColor(this.comboBoxRightPlayerInput_03);
            Helper.setControlBackColor(this.comboBoxRightPlayerInput_04);

            this.setComboBoxLeftPlayerInputText();
            this.setComboBoxRightPlayerInputText();
        }

        private void setComboBoxLeftPlayerInputText() {
            for (int i = 0; i < DatasetContent.DataItemsCount; i++) {
                string key = string.Format("comboBoxLeftPlayerInput_{0}", i.ToString("00"));
                ComboBox comboBoxPlayerInput = this.Controls[key] as ComboBox;
                if (comboBoxPlayerInput is ComboBox) {
                    if (this.business.LeftPlayerInput[i] is DatasetItem) comboBoxPlayerInput.Text = this.business.LeftPlayerInput[i].Text;
                    else comboBoxPlayerInput.Text = "n.a.";
                }
            }
        }

        private void setComboBoxRightPlayerInputText() {
            for (int i = 0; i < DatasetContent.DataItemsCount; i++) {
                string key = string.Format("comboBoxRightPlayerInput_{0}", i.ToString("00"));
                ComboBox comboBoxPlayerInput = this.Controls[key] as ComboBox;
                if (comboBoxPlayerInput is ComboBox) {
                    if (this.business.RightPlayerInput[i] is DatasetItem) comboBoxPlayerInput.Text = this.business.RightPlayerInput[i].Text;
                    else comboBoxPlayerInput.Text = "n.a.";
                }
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
            this.setSortedItems();

            this.selectDataList();
            this.setComboBoxPlayerInputItems();
        }

        private void setSortedItems() {
            this.listBoxSortedItems.Items.Clear();
            if (this.selectedDataset is DatasetContent) {
                this.listBoxSortedItems.Items.AddRange(this.selectedDataset.SortedItems);
                this.listBoxSortedItems.Enabled = true;
            }
            else this.listBoxSortedItems.Enabled = false;
            Helper.setControlBackColor(this.listBoxSortedItems);
            this.setSelectedDatasetItem();
        }
        private void setSelectedDatasetItem() {
            if (this.business.SelectedDatasetItem is DatasetItem) {
                this.listBoxSortedItems.Text = this.business.SelectedDatasetItem.Text;
                this.labelSelectedItemResult.Enabled = true;
                this.numericUpDownSelectedItemResult.Value = this.business.SelectedDatasetItem.Result;
                this.numericUpDownSelectedItemResult.Enabled = true;
                this.buttonSelectedItemIn.Enabled = true;
                this.buttonSelectedItemOut.Enabled = true;
            }
            else {
                this.labelSelectedItemResult.Enabled = false;
                this.numericUpDownSelectedItemResult.Value = 0;
                this.numericUpDownSelectedItemResult.Enabled = false;
                this.buttonSelectedItemIn.Enabled = false;
                this.buttonSelectedItemOut.Enabled = false;
            }
            Helper.setControlBackColor(this.numericUpDownSelectedItemResult);
            Helper.setControlBackColor(this.buttonSelectedItemIn);
            Helper.setControlBackColor(this.buttonSelectedItemOut);
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
                else if (e.PropertyName == "SelectedDatasetItem") this.setSelectedDatasetItem();
                else if (e.PropertyName == "LeftPlayerInput") this.setComboBoxLeftPlayerInputText();
                else if (e.PropertyName == "RightPlayerInput") this.setComboBoxRightPlayerInputText();
            }
        }

        void selectedDataset_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.selectedDataset_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "SortedItems") this.setSortedItems();
            }
        }

        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        protected override void buttonLeftPlayerAddScoreHot_01_Click(object sender, EventArgs e) { base.buttonLeftPlayerAddScoreHot_01_Click(sender, e); }
        private void comboBoxLeftPlayerInput_SelectedIndexChanged(object sender, EventArgs e) {
            int index;
            ComboBox control;
            control = sender as ComboBox;
            if (control is ComboBox &&
                Helper.tryParseIndexFromControl(sender as Control, out index)) this.business.SetLeftPlayerInput(index, control.SelectedIndex + 1);
        }
        private void buttonLeftPlayerContentIn_Click(object sender, EventArgs e) { this.business.Vleftplayer_ContentIn(); }
        private void buttonLeftPlayerReleaseInput_Click(object sender, EventArgs e) { this.business.Vleftplayer_ReleaseInput(); }
        private void buttonLeftPlayerLockInput_Click(object sender, EventArgs e) { this.business.Vleftplayer_LockInput(); }

        protected override void buttonRightPlayerAddScoreHot_01_Click(object sender, EventArgs e) { base.buttonRightPlayerAddScoreHot_01_Click(sender, e); }
        private void comboBoxRightPlayerInput_SelectedIndexChanged(object sender, EventArgs e) {
            int index;
            ComboBox control;
            control = sender as ComboBox;
            if (control is ComboBox &&
                Helper.tryParseIndexFromControl(sender as Control, out index)) this.business.SetRightPlayerInput(index, control.SelectedIndex + 1);
        }

        private void buttonRightPlayerContentIn_Click(object sender, EventArgs e) { this.business.Vrightplayer_ContentIn(); }
        private void buttonRightPlayerReleaseInput_Click(object sender, EventArgs e) { this.business.Vrightplayer_ReleaseInput(); }
        private void buttonRightPlayerLockInput_Click(object sender, EventArgs e) { this.business.Vrightplayer_LockInput(); }

        private void numericUpDownTaskCounter_ValueChanged(object sender, EventArgs e) { this.business.TaskCounter = (int)this.numericUpDownTaskCounter.Value; }

        private void listBoxDataList_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectDataset(this.listBoxDataList.SelectedIndex); }
        private void listBoxSortedItems_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectDatasetItem(this.listBoxSortedItems.SelectedIndex); }
        private void numericUpDownSelectedItemResult_ValueChanged(object sender, EventArgs e) { if (this.business.SelectedDatasetItem is DatasetItem) this.business.SelectedDatasetItem.Result = (int)this.numericUpDownSelectedItemResult.Value; }
        private void buttonSelectedItemIn_Click(object sender, EventArgs e) { this.business.Vinsert_SelectedItemIn(); }
        private void buttonSelectedItemOut_Click(object sender, EventArgs e) { this.business.Vinsert_SelectedItemOut(); }

        private void buttonVinsert_ContentIn_Click(object sender, EventArgs e) { this.business.Vinsert_GameIn(); }
        private void buttonVinsert_Resolve_Click(object sender, EventArgs e) { this.business.Vinsert_GameResolve(); }
        private void buttonVinsert_ContentOut_Click(object sender, EventArgs e) { this.business.Vinsert_GameOut();    }

        private void buttonVplayers_ContentIn_Click(object sender, EventArgs e) {
            this.business.Vleftplayer_ContentIn();
            this.business.Vrightplayer_ContentIn();
        }
        private void buttonVplayers_ReleaseInput_Click(object sender, EventArgs e) {
            this.business.Vleftplayer_ReleaseInput();
            this.business.Vrightplayer_ReleaseInput();
        }
        private void buttonVplayers_LockInput_Click(object sender, EventArgs e) {
            this.business.Vleftplayer_LockInput();
            this.business.Vrightplayer_LockInput();
        }
        private void buttonVplayers_ContentOut_Click(object sender, EventArgs e) {
            this.business.Vleftplayer_ContentOut();
            this.business.Vrightplayer_ContentOut();
        }

        private void buttonGame_Resolve_Click(object sender, EventArgs e) { this.business.Resolve(); }
        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }

        #endregion
    }
}
