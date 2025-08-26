using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.NumericInputCloserToValueTimerCounter {

    public partial class UserControlGame : _Base.Score.UserControlGame {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.numericUpDownLeftPlayerValue.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerValue.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerValue.Minimum = int.MinValue;
            this.numericUpDownRightPlayerValue.Maximum = int.MaxValue;

            this.numericUpDownSolution.Minimum = int.MinValue;
            this.numericUpDownSolution.Maximum = int.MaxValue;

            this.numericUpDownCounter.Minimum = int.MinValue;
            this.numericUpDownCounter.Maximum = int.MaxValue;

            this.numericUpDownCounterToAdd.Minimum = int.MinValue;
            this.numericUpDownCounterToAdd.Maximum = int.MaxValue;

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            this.userControlGamePoolTemplates_ModulesTimerGame.Pose(this.business.Vinsert_Timer);

            Binding bind;

            bind = new Binding("Value", this.business, "LeftPlayerValue");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerValue.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "LeftPlayerOffset");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxLeftPlayerOffset.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "LeftPlayerIsCloser");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorEnabled : SystemColors.Control; };
            this.textBoxLeftPlayerOffset.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerValue");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerValue.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerOffset");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxRightPlayerOffset.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "RightPlayerIsCloser");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorEnabled : SystemColors.Control; };
            this.textBoxRightPlayerOffset.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "Counter");
            bind.Format += (s, e) => { e.Value = (decimal)e.Value; };
            this.numericUpDownCounter.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "CounterToAdd");
            bind.Format += (s, e) => { e.Value = (decimal)e.Value; };
            this.numericUpDownCounterToAdd.DataBindings.Add(bind);

            this.fillDataList();
            this.selectDataset(this.business.SelectedDataset);
            this.setFirstLogin();
            this.setDecimals();

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

            this.numericUpDownLeftPlayerValue.DataBindings.Clear();
            this.textBoxLeftPlayerOffset.DataBindings.Clear();
            this.numericUpDownRightPlayerValue.DataBindings.Clear();
            this.textBoxRightPlayerOffset.DataBindings.Clear();
            this.numericUpDownCounter.DataBindings.Clear();
            this.numericUpDownCounterToAdd.DataBindings.Clear();

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
            step.AddButton(this.buttonVinsert_TextInsertIn);
            step.AddButton(this.buttonVstage_ContentIn);
            step.AddButton(this.buttonVleftplayer_Unlock);
            step.AddButton(this.buttonVrightplayer_Unlock);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_InputIn);
            step.AddButton(this.buttonVhost_SetInput);
            step.AddButton(this.buttonVleftplayer_Lock);
            step.AddButton(this.buttonVrightplayer_Lock);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ResolveInputInsert);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TextInsertOut);
            step.AddButton(this.buttonVinsert_InputInsertOut);
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
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ScoreOut);
            step.AddButton(this.buttonVstage_ContentOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 7);
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
            this.setDecimals();
        }

        private void setDatasetItems() {
            if (this.selectedDataset is DatasetContent) {
                this.numericUpDownSolution.Value = this.selectedDataset.Solution;
                this.labelSolutionText.Text = this.selectedDataset.SolutionText;
            }
            else {
                this.numericUpDownSolution.Value = 0;
                this.labelSolutionText.Text = string.Empty;
            }
            this.labelLeftPlayerValueText.Text = this.business.LeftPlayerValueText;
            this.labelRightPlayerValueText.Text = this.business.RightPlayerValueText;
        }

        private void setFirstLogin() {
            switch (this.business.FirstLogin) {
                case Content.Gameboard.PlayerSelection.LeftPlayer:
                    this.buttonLeftPlayerFirst.BackColor = Constants.ColorWinner;
                    this.buttonRightPlayerFirst.UseVisualStyleBackColor = true;
                    break;
                case Content.Gameboard.PlayerSelection.RightPlayer:
                    this.buttonLeftPlayerFirst.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerFirst.BackColor = Constants.ColorWinner;
                    break;
                case Content.Gameboard.PlayerSelection.NotSelected:
                default:
                    this.buttonLeftPlayerFirst.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerFirst.UseVisualStyleBackColor = true;
                    break;
            }
        }

        private void setDecimals() {
            this.checkBoxShowDecimal.Checked = this.business.ShowDecimal;
            if (this.business.ShowDecimal) {
                this.numericUpDownCounter.DecimalPlaces = 1;
                this.numericUpDownCounterToAdd.DecimalPlaces = 1;
            }
            else {
                this.numericUpDownCounter.DecimalPlaces = 0;
                this.numericUpDownCounterToAdd.DecimalPlaces = 0;
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
                else if (e.PropertyName == "FirstLogin") this.setFirstLogin();
                else if (e.PropertyName == "ShowDecimal") this.setDecimals();
                else if (e.PropertyName == "LeftPlayerValue") this.labelLeftPlayerValueText.Text = this.business.LeftPlayerValueText;
                else if (e.PropertyName == "RightPlayerValue") this.labelRightPlayerValueText.Text = this.business.RightPlayerValueText;
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

        private void UserControlGame_BackColorChanged(object sender, EventArgs e) {
            this.userControlGamePoolTemplates_ModulesTimerGame.BackColor = this.BackColor;
        }

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void numericUpDownLeftPlayerValue_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerValue = (int)this.numericUpDownLeftPlayerValue.Value; }
        private void buttonLeftPlayerFirst_Click(object sender, EventArgs e) {
            if (this.business.FirstLogin == Content.Gameboard.PlayerSelection.LeftPlayer) this.business.FirstLogin = Content.Gameboard.PlayerSelection.NotSelected;
            else this.business.FirstLogin = Content.Gameboard.PlayerSelection.LeftPlayer;
        }

        private void numericUpDownRightPlayerValue_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerValue = (int)this.numericUpDownRightPlayerValue.Value; }
        private void buttonRightPlayerFirst_Click(object sender, EventArgs e) {
            if (this.business.FirstLogin == Content.Gameboard.PlayerSelection.RightPlayer) this.business.FirstLogin = Content.Gameboard.PlayerSelection.NotSelected;
            else this.business.FirstLogin = Content.Gameboard.PlayerSelection.RightPlayer;
        }

        private void numericUpDownSolution_ValueChanged(object sender, EventArgs e) {
            if (this.business.SelectedDataset is DatasetContent) this.business.SelectedDataset.Solution = this.numericUpDownSolution.Value;
        }
        private void listBoxDataList_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectDataset(this.listBoxDataList.SelectedIndex); }

        private void buttonTimerIn_Click(object sender, EventArgs e) { this.business.Vinsert_Timer.In(); }
        private void buttonStartTimer_Click(object sender, EventArgs e) { this.business.Vinsert_Timer.Start(); }
        private void buttonStopTimer_Click(object sender, EventArgs e) { this.business.Vinsert_Timer.Stop(); }
        private void buttonTimerOut_Click(object sender, EventArgs e) { this.business.Vinsert_Timer.Out(); }

        private void checkBoxShowDecimal_CheckedChanged(object sender, EventArgs e) { this.business.ShowDecimal = this.checkBoxShowDecimal.Checked;    }
        private void numericUpDownCounter_ValueChanged(object sender, EventArgs e) { this.business.Counter = this.numericUpDownCounter.Value; }
        private void numericUpDownCounterToAdd_ValueChanged(object sender, EventArgs e) { this.business.CounterToAdd = this.numericUpDownCounterToAdd.Value; }
        private void buttonCounterIn_Click(object sender, EventArgs e) { this.business.Vinsert_CounterIn(); }
        private void buttonCounterAddHot_1_Click(object sender, EventArgs e) { this.business.AddCounterHot(1); }
        private void buttonCounterAddHot_X_Click(object sender, EventArgs e) { this.business.AddCounterHot(this.business.CounterToAdd); }
        private void buttonCounterOut_Click(object sender, EventArgs e) { this.business.Vinsert_CounterOut(); }

        private void buttonTimeout_Click(object sender, EventArgs e) { this.business.Timeout(); }

        private void buttonVinsert_TextInsertIn_Click(object sender, EventArgs e) { this.business.Vinsert_TextInsertIn(); }
        private void buttonVinsert_InputIn_Click(object sender, EventArgs e) { this.business.Vinsert_InputInsertIn(); }
        private void buttonVinsert_ResolveInputInsert_Click(object sender, EventArgs e) { this.business.Vinsert_ResolveInputInsert(); }
        private void buttonVinsert_TextInsertOut_Click(object sender, EventArgs e) { this.business.Vinsert_TextInsertOut(); }
        private void buttonVinsert_InputInsertOut_Click(object sender, EventArgs e) { this.business.Vinsert_InputInsertOut(); }

        private void buttonVstage_ContentIn_Click(object sender, EventArgs e) { this.business.Vstage_ContentIn(); }
        private void buttonVstage_ContentOut_Click(object sender, EventArgs e) { this.business.Vstage_ContentOut(); }

        private void buttonVhost_SetInput_Click(object sender, EventArgs e) {
            this.business.Vhost_SetPlayerInput();
            this.business.Vstages_SetPlayerInput();
        }

        private void buttonVleftplayer_Unlock_Click(object sender, EventArgs e) { this.business.Vleftplayer_UnlockInput(); }
        private void buttonVleftplayer_Lock_Click(object sender, EventArgs e) { this.business.Vleftplayer_LockInput(); }

        private void buttonVrightplayer_Unlock_Click(object sender, EventArgs e) { this.business.Vrightplayer_UnlockInput(); }
        private void buttonVrightplayer_Lock_Click(object sender, EventArgs e) { this.business.Vrightplayer_LockInput(); }

        private void buttonGame_Resolve_Click(object sender, EventArgs e) { this.business.Resolve(); }
        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }

        #endregion

    }

}
