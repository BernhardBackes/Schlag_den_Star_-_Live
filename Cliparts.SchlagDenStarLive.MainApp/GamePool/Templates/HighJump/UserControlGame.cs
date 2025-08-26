using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.HighJump {

    public partial class UserControlGame : _Base.UserControlGame {

        #region Properties

        private Business business;

        private MarkerSet selectedMarker = null;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.numericUpDownMarkerHeight.Minimum = int.MinValue;
            this.numericUpDownMarkerHeight.Maximum = int.MaxValue;

            this.numericUpDownMarkerExpanse.Minimum = int.MinValue;
            this.numericUpDownMarkerExpanse.Maximum = int.MaxValue;

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            Binding bind;

            this.buildStepList();

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            bind = new Binding("Text", this.business, "LeftPlayerFaultsCount");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.labelLeftPlayerFaultsCount.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerFaultsCount");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.labelRightPlayerFaultsCount.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "MarkerBeginning");
            bind.Format += (s, e) => { e.Value = ((double)(e.Value)).ToString("0.00") + "m"; };
            this.labelMarkerBeginningValue.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "MarkerExpanse");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownMarkerExpanse.DataBindings.Add(bind);

            this.labelGameClass.Text = this.business.ClassInfo;

            this.fillMarkerList();

            this.selectMarkerSet(this.business.SelectedMarker);
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

            this.labelLeftPlayerFaultsCount.DataBindings.Clear();
            this.labelRightPlayerFaultsCount.DataBindings.Clear();

            this.labelMarkerBeginningValue.DataBindings.Clear();
            this.numericUpDownMarkerExpanse.DataBindings.Clear();

            this.selectMarkerSet(null);

        }

        protected override void buildStepList() {

            int index = 0;
            stepAction step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_LoadScene);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVgraphic_Clear);
            step.AddButton(this.buttonVstage_Clear);
            step.AddButton(this.buttonGame_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex);
            step.AddButton(this.buttonVinsert_MarkerSetsIn);
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
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_UpdateGameboard);
            this.stepList.Add(step);

            if (index < this.stepsMaxIndex) this.on_Error("buildStepList", "stepList to short");
            if (index > this.stepsMaxIndex) this.on_Error("buildStepList", "stepList to long");
        }

        private void fillMarkerList() {
            this.listBoxMarkers.BeginUpdate();
            this.listBoxMarkers.Items.Clear();
            this.listBoxMarkers.EndUpdate();
            this.listBoxMarkers.Items.AddRange(this.business.MarkerSetList);
            this.listBoxMarkers.Enabled = this.listBoxMarkers.Items.Count > 0;
            Helper.setControlBackColor(this.listBoxMarkers);

            this.buttonRemoveSelectedMarkerSet.Enabled = this.listBoxMarkers.Items.Count > 1;
            Helper.setControlBackColor(this.buttonRemoveSelectedMarkerSet);

            this.selectMarker();
        }

        private void selectMarker() {
            int index = this.business.SelectedMarkerIndex;
            if (index >= 0 &&
                index < this.listBoxMarkers.Items.Count) this.listBoxMarkers.SelectedIndex = index;
        }

        private void selectMarkerSet(
            MarkerSet selectedMarker) {

            this.numericUpDownMarkerHeight.DataBindings.Clear();
            this.numericUpDownLeftPlayerFaults.DataBindings.Clear();
            this.checkBoxLeftPlayerPassed.DataBindings.Clear();
            this.numericUpDownRightPlayerFaults.DataBindings.Clear();
            this.checkBoxRightPlayerPassed.DataBindings.Clear();

            if (this.selectedMarker != selectedMarker) {
                //Dispose...
                if (this.selectedMarker is MarkerSet) {
                    //this.selectedMarker.PropertyChanged -= this.selectedMarker_PropertyChanged;
                }
                this.selectedMarker = selectedMarker;
                //Pose...
                if (this.selectedMarker is MarkerSet) {
                    //this.selectedMarker.PropertyChanged += this.selectedMarker_PropertyChanged;
                    Binding bind;

                    bind = new Binding("Value", this.selectedMarker, "Height");
                    bind.Format += (s, e) => { e.Value = (double)e.Value; };
                    this.numericUpDownMarkerHeight.DataBindings.Add(bind);

                    bind = new Binding("Value", this.selectedMarker, "LeftPlayerFaults");
                    bind.Format += (s, e) => { e.Value = (int)e.Value; };
                    this.numericUpDownLeftPlayerFaults.DataBindings.Add(bind);

                    bind = new Binding("Checked", this.selectedMarker, "LeftPlayerPassed");
                    bind.Format += (s, e) => { e.Value = (bool)e.Value; };
                    this.checkBoxLeftPlayerPassed.DataBindings.Add(bind);

                    bind = new Binding("Value", this.selectedMarker, "RightPlayerFaults");
                    bind.Format += (s, e) => { e.Value = (int)e.Value; };
                    this.numericUpDownRightPlayerFaults.DataBindings.Add(bind);

                    bind = new Binding("Checked", this.selectedMarker, "LeftPlayerPassed");
                    bind.Format += (s, e) => { e.Value = (bool)e.Value; };
                    this.checkBoxRightPlayerPassed.DataBindings.Add(bind);

                    this.numericUpDownMarkerHeight.Value = (decimal)this.selectedMarker.Height;
                    this.numericUpDownLeftPlayerFaults.Value = this.selectedMarker.LeftPlayerFaults;
                    this.checkBoxLeftPlayerPassed.Checked = this.selectedMarker.LeftPlayerPassed;
                    this.numericUpDownRightPlayerFaults.Value = this.selectedMarker.RightPlayerFaults;
                    this.checkBoxRightPlayerPassed.Checked = this.selectedMarker.LeftPlayerPassed;
                }
                else {
                }
            }

            this.selectMarker();
        }

        #endregion


        #region Events.Incoming

        protected override void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.business_PropertyChanged(sender, e);
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "NameList") this.fillMarkerList();
                else if (e.PropertyName == "SelectedMarker") this.selectMarkerSet(this.business.SelectedMarker);
                else if (e.PropertyName == "SelectedMarkerIndex") this.selectMarker();
            }
        }

        #endregion

        #region Events.Controls

        private void UserControlGame_BackColorChanged(object sender, EventArgs e) {
            Control control;
            string key;
            //for (int i = 0; i < Business.DecimalSetsCountMax; i++) {
            //    key = "userControlSetLeftPlayer_" + i.ToString("00");
            //    control = this.Controls[key] as Control;
            //    if (control is Control) control.BackColor = this.BackColor;
            //    key = "userControlSetRightPlayer_" + i.ToString("00");
            //    control = this.Controls[key] as Control;
            //    if (control is Control) control.BackColor = this.BackColor;
            //    key = "labelCounter_" + i.ToString("00");
            //    control = this.Controls[key] as Control;
            //    if (control is Control) control.BackColor = this.BackColor;
            //}
        }

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }

        private void numericUpDownMarkerHeight_ValueChanged(object sender, EventArgs e) {
            if (this.selectedMarker is MarkerSet) this.selectedMarker.Height = (double)this.numericUpDownMarkerHeight.Value;
        }

        private void numericUpDownLeftPlayerFaults_ValueChanged(object sender, EventArgs e) {
            if (this.selectedMarker is MarkerSet) this.selectedMarker.LeftPlayerFaults = (int)this.numericUpDownLeftPlayerFaults.Value;
        }

        private void checkBoxLeftPlayerPassed_CheckedChanged(object sender, EventArgs e) {
            if (this.selectedMarker is MarkerSet) this.selectedMarker.LeftPlayerPassed = this.checkBoxLeftPlayerPassed.Checked;
        }

        private void numericUpDownRightPlayerFaults_ValueChanged(object sender, EventArgs e) {
            if (this.selectedMarker is MarkerSet) this.selectedMarker.RightPlayerFaults = (int)this.numericUpDownRightPlayerFaults.Value;
        }

        private void checkBoxRightPlayerPassed_CheckedChanged(object sender, EventArgs e) {
            if (this.selectedMarker is MarkerSet) this.selectedMarker.RightPlayerPassed = this.checkBoxRightPlayerPassed.Checked;
        }

        private void numericUpDownMarkerExpanse_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) this.business.MarkerExpanse = (int)this.numericUpDownMarkerExpanse.Value;
        }

        private void listBoxMarkers_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectMarker(this.listBoxMarkers.SelectedIndex);  }

        private void buttonAddNewMarkerSet_Click(object sender, EventArgs e) { this.business.AddNewMarkerSet(); }
        private void buttonRemoveSelectedMarkerSet_Click(object sender, EventArgs e) { this.business.TryRemoveMarkerSet(this.business.SelectedMarkerIndex); }

        private void buttonVinsert_MarkerSetsIn_Click(object sender, EventArgs e) { this.business.Vinsert_MarkerSetsIn(); }

        #endregion

    }

}
