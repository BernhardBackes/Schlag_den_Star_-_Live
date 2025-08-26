using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TurnNameTrueFalseCountdownScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TurnNameTrueFalseCountdownScore {

    public partial class UserControlGame : _Base.Score.UserControlGame {

        #region Properties

        private Business business;

        private DatasetContent selectedDataset = null;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.comboBoxLeftPlayerSelection.Items.AddRange(Enum.GetNames(typeof(SelectionValues)));
            this.comboBoxRightPlayerSelection.Items.AddRange(Enum.GetNames(typeof(SelectionValues)));

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Text", this.business, "LeftPlayerSelection");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxLeftPlayerSelection.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "LeftPlayerSelection");
            bind.Format += this.bind_comboBoxSelection_BackColor;
            this.comboBoxLeftPlayerSelection.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerSelection");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxRightPlayerSelection.DataBindings.Add(bind);
            bind = new Binding("BackColor", this.business, "RightPlayerSelection");
            bind.Format += this.bind_comboBoxSelection_BackColor;
            this.comboBoxRightPlayerSelection.DataBindings.Add(bind);

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
            step.AddButton(this.buttonVinsert_ScoreIn);
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
            step.AddButton(this.buttonVinsert_StartTimeout);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_StopTimeout); 
            step.AddButton(this.buttonVplayers_DisableInput);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVhost_SetInput);
            step.AddButton(this.buttonVplayers_ShowInput);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, nameIsFalse);
            step.AddButton(this.buttonVinsert_Resolve);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TurnName);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_ContentOut);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonGame_Resolve);
            step.AddButton(this.buttonVinsert_SetScore);
            step.AddButton(this.buttonVstage_SetScore);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 8);
            step.AddButton(this.buttonVstage_ContentOut);
            step.AddButton(this.buttonGame_Next);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
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
            step = new stepAction(index, (stepIndex) => stepIndex);
            step.AddButton(this.buttonVfullscreen_UpdateGameboard);
            this.stepList.Add(step);

            if (index < this.stepsMaxIndex) this.on_Error("buildStepList", "stepList to short");
            if (index > this.stepsMaxIndex) this.on_Error("buildStepList", "stepList to long");
        }

        private int nameIsFalse(
            int index) {
            if (this.selectedDataset is DatasetContent &&
                !this.selectedDataset.TaskIsTrue) return index + 1;
            else return index + 2;
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
            this.setSelectedDataset();
            this.selectDataList();
        }
        private void setSelectedDataset() {
            if (this.selectedDataset is DatasetContent) {
                if (this.selectedDataset.TaskIsTrue) {
                    this.textBoxDataSetSolution.Text = "TRUE";
                    this.textBoxDataSetSolution.BackColor = Constants.ColorEnabled;
                    this.buttonVinsert_TurnName.Enabled = false;
                }
                else {
                    this.textBoxDataSetSolution.Text = "FALSE";
                    this.textBoxDataSetSolution.BackColor = Constants.ColorDisabled;
                    this.buttonVinsert_TurnName.Enabled = true;
                }
            }
            else {
                this.textBoxDataSetSolution.Text = string.Empty;
                this.textBoxDataSetSolution.BackColor = SystemColors.Control;
                this.buttonVinsert_TurnName.Enabled = false;
            }
            Helper.setControlBackColor(this.buttonVinsert_TurnName);
        }

        private void bind_comboBoxSelection_BackColor(object sender, ConvertEventArgs e) {
            switch ((SelectionValues)e.Value) {
                case SelectionValues.True:
                    e.Value = Constants.ColorEnabled;
                    break;
                case SelectionValues.False:
                    e.Value = Constants.ColorDisabled;
                    break;
                case SelectionValues.NotAvailable:
                default:
                    e.Value = SystemColors.Control;
                    break;
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
                this.setSelectedDataset();
            }
        }

        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void comboBoxLeftPlayerSelection_SelectedIndexChanged(object sender, EventArgs e) {
            SelectionValues value;
            if (Enum.TryParse(comboBoxLeftPlayerSelection.Text, out value)) this.business.LeftPlayerSelection = value;
        }
        private void buttonLeftPlayerEnableInput_Click(object sender, EventArgs e) { this.business.Vleftplayer_EnableInput(); }
        private void buttonLeftPlayerDisableInput_Click(object sender, EventArgs e) { this.business.Vleftplayer_DisableInput(); }

        private void comboBoxRightPlayerSelection_SelectedIndexChanged(object sender, EventArgs e) {
            SelectionValues value;
            if (Enum.TryParse(comboBoxRightPlayerSelection.Text, out value)) this.business.RightPlayerSelection = value;
        }
        private void buttonRightPlayerEnableInput_Click(object sender, EventArgs e) { this.business.Vrightplayer_EnableInput(); }
        private void buttonRightPlayerDisableInput_Click(object sender, EventArgs e) { this.business.Vrightplayer_DisableInput(); }

        private void listBoxDataList_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectDataset(this.listBoxDataList.SelectedIndex); }

        private void buttonVinsert_TaskIn_Click(object sender, EventArgs e) { this.business.Vinsert_ContentIn(); }
        private void buttonVinsert_StartTimeout_Click(object sender, EventArgs e) { this.business.Vinsert_StartTimeout(); }
        private void buttonVinsert_StopTimeout_Click(object sender, EventArgs e) { this.business.Vinsert_StopTimeout(); }
        private void buttonVinsert_Resolve_Click(object sender, EventArgs e) { this.business.Vinsert_Resolve(); }
        private void buttonVinsert_TurnName_Click(object sender, EventArgs e) { this.business.Vinsert_TurnName(); }
        private void buttonVinsert_ContentOut_Click(object sender, EventArgs e) { this.business.Vinsert_ContentOut(); }

        private void buttonVstage_ContentIn_Click(object sender, EventArgs e) { this.business.Vstage_ContentIn(); }
        private void buttonVstage_ContentOut_Click(object sender, EventArgs e) { this.business.Vstage_ContentOut(); }

        private void buttonVhost_SetInput_Click(object sender, EventArgs e) { this.business.Vhost_SetContent(); }

        private void buttonVplayers_EnableInput_Click(object sender, EventArgs e) { this.business.Vplayers_EnableInput(); }
        private void buttonVplayers_DisableInput_Click(object sender, EventArgs e) { this.business.Vplayers_DisableInput(); }
        private void buttonVplayers_ShowInput_Click(object sender, EventArgs e) { this.business.Vplayers_ShowInput(); }

        private void buttonGame_Resolve_Click(object sender, EventArgs e) { this.business.Resolve(); }
        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }

        #endregion

    }
}
