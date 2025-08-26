using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.WeightLifting {

    public partial class UserControlGame : _Base.Timer.UserControlGame {

        #region Properties

        private Business business;

        private MarkerSet selectedMarker = null;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.numericUpDownMarkerWeight.Minimum = int.MinValue;
            this.numericUpDownMarkerWeight.Maximum = int.MaxValue;

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

            bind = new Binding("Text", this.business, "MarkerBeginning");
            bind.Format += (s, e) => { e.Value = ((int)(e.Value)).ToString("0") + " kg"; };
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
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_MarkerSetsIn);
            step.AddButton(this.buttonVfullscreen_ShowTimer);
            step.AddButton(this.buttonVfullscreen_ResetTimer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_TimerIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_StartTimer);
            step.AddButton(this.buttonVinsert_StartTimer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_StopTimer);
            step.AddButton(this.buttonVinsert_StopTimer);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex - 3);
            step.AddButton(this.buttonVinsert_TimerOut);
            step.AddButton(this.buttonVfullscreen_ResetTimer_1);
            step.AddButton(this.buttonVinsert_SetMarkerSets);
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

            this.numericUpDownMarkerWeight.DataBindings.Clear();

            if (this.selectedMarker != selectedMarker) {
                //Dispose...
                if (this.selectedMarker is MarkerSet) {
                    this.selectedMarker.PropertyChanged -= this.selectedMarker_PropertyChanged;
                }
                this.selectedMarker = selectedMarker;
                //Pose...
                if (this.selectedMarker is MarkerSet) {
                    this.selectedMarker.PropertyChanged += this.selectedMarker_PropertyChanged;
                    Binding bind;

                    bind = new Binding("Value", this.selectedMarker, "Weight");
                    bind.Format += (s, e) => { e.Value = (int)e.Value; };
                    this.numericUpDownMarkerWeight.DataBindings.Add(bind);

                    this.numericUpDownMarkerWeight.Enabled = true;
                    this.numericUpDownMarkerWeight.Value = (decimal)this.selectedMarker.Weight;
                }
                else {
                    this.numericUpDownMarkerWeight.Enabled = false;
                }
            }

            Helper.setControlBackColor(this.numericUpDownMarkerWeight);

            this.setMarkerButtons();

            this.selectMarker();
        }

        private void setMarkerButtons() {

            if (this.selectedMarker is MarkerSet) {
                this.buttonLeftPlayerOff.Enabled = true;
                this.buttonLeftPlayerGreen.Enabled = true;
                this.buttonLeftPlayerRed.Enabled = true;
                switch (this.selectedMarker.LeftPlayerStatus) {
                    case VentuzScenes.GamePool.WeightLifting.Insert.DotStates.Green:
                        this.buttonLeftPlayerOff.UseVisualStyleBackColor = true;
                        this.buttonLeftPlayerGreen.BackColor = Color.LimeGreen;
                        this.buttonLeftPlayerRed.BackColor = Color.LightSalmon;
                        break;
                    case VentuzScenes.GamePool.WeightLifting.Insert.DotStates.Red:
                        this.buttonLeftPlayerOff.UseVisualStyleBackColor = true;
                        this.buttonLeftPlayerGreen.BackColor = Color.PaleGreen;
                        this.buttonLeftPlayerRed.BackColor = Color.Red;
                        break;
                    case VentuzScenes.GamePool.WeightLifting.Insert.DotStates.Off:
                    default:
                        this.buttonLeftPlayerOff.UseVisualStyleBackColor = true;
                        this.buttonLeftPlayerGreen.BackColor = Color.PaleGreen;
                        this.buttonLeftPlayerRed.BackColor = Color.LightSalmon;
                        break;
                }
                this.buttonRightPlayerOff.Enabled = true;
                this.buttonRightPlayerGreen.Enabled = true;
                this.buttonRightPlayerRed.Enabled = true;
                switch (this.selectedMarker.RightPlayerStatus) {
                    case VentuzScenes.GamePool.WeightLifting.Insert.DotStates.Green:
                        this.buttonRightPlayerOff.UseVisualStyleBackColor = true;
                        this.buttonRightPlayerGreen.BackColor = Color.LimeGreen;
                        this.buttonRightPlayerRed.BackColor = Color.LightSalmon;
                        break;
                    case VentuzScenes.GamePool.WeightLifting.Insert.DotStates.Red:
                        this.buttonRightPlayerOff.UseVisualStyleBackColor = true;
                        this.buttonRightPlayerGreen.BackColor = Color.PaleGreen;
                        this.buttonRightPlayerRed.BackColor = Color.Red;
                        break;
                    case VentuzScenes.GamePool.WeightLifting.Insert.DotStates.Off:
                    default:
                        this.buttonRightPlayerOff.UseVisualStyleBackColor = true;
                        this.buttonRightPlayerGreen.BackColor = Color.PaleGreen;
                        this.buttonRightPlayerRed.BackColor = Color.LightSalmon;
                        break;
                }
            }
            else {
                this.buttonLeftPlayerOff.Enabled = false;
                this.buttonLeftPlayerGreen.Enabled = false;
                this.buttonLeftPlayerRed.Enabled = false;
                Helper.setControlBackColor(this.buttonLeftPlayerOff);
                Helper.setControlBackColor(this.buttonLeftPlayerGreen);
                Helper.setControlBackColor(this.buttonLeftPlayerRed);
                this.buttonRightPlayerOff.Enabled = false;
                this.buttonRightPlayerGreen.Enabled = false;
                this.buttonRightPlayerRed.Enabled = false;
                Helper.setControlBackColor(this.buttonRightPlayerOff);
                Helper.setControlBackColor(this.buttonRightPlayerGreen);
                Helper.setControlBackColor(this.buttonRightPlayerRed);
            }
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

        void selectedMarker_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else { this.setMarkerButtons(); }
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
            if (this.selectedMarker is MarkerSet) this.selectedMarker.Weight = (int)this.numericUpDownMarkerWeight.Value;
        }

        private void buttonLeftPlayerOff_Click(object sender, EventArgs e) {
            if (this.selectedMarker is MarkerSet) this.selectedMarker.LeftPlayerStatus = VentuzScenes.GamePool.WeightLifting.Insert.DotStates.Off;
        }
        private void buttonLeftPlayerGreen_Click(object sender, EventArgs e) {
            if (this.selectedMarker is MarkerSet) this.selectedMarker.LeftPlayerStatus = VentuzScenes.GamePool.WeightLifting.Insert.DotStates.Green;
        }
        private void buttonLeftPlayerRed_Click(object sender, EventArgs e) {
            if (this.selectedMarker is MarkerSet) this.selectedMarker.LeftPlayerStatus = VentuzScenes.GamePool.WeightLifting.Insert.DotStates.Red;
        }

        private void buttonRightPlayerOff_Click(object sender, EventArgs e) {
            if (this.selectedMarker is MarkerSet) this.selectedMarker.RightPlayerStatus = VentuzScenes.GamePool.WeightLifting.Insert.DotStates.Off;
        }
        private void buttonRightPlayerGreen_Click(object sender, EventArgs e) {
            if (this.selectedMarker is MarkerSet) this.selectedMarker.RightPlayerStatus = VentuzScenes.GamePool.WeightLifting.Insert.DotStates.Green;
        }
        private void buttonRightPlayerRed_Click(object sender, EventArgs e) {
            if (this.selectedMarker is MarkerSet) this.selectedMarker.RightPlayerStatus = VentuzScenes.GamePool.WeightLifting.Insert.DotStates.Red;
        }

        private void numericUpDownMarkerExpanse_ValueChanged(object sender, EventArgs e) {
            if (this.business is Business) this.business.MarkerExpanse = (int)this.numericUpDownMarkerExpanse.Value;
        }

        private void listBoxMarkers_SelectedIndexChanged(object sender, EventArgs e) { this.business.SelectMarker(this.listBoxMarkers.SelectedIndex);  }

        private void buttonAddNewMarkerSet_Click(object sender, EventArgs e) { this.business.AddNewMarkerSet(); }
        private void buttonRemoveSelectedMarkerSet_Click(object sender, EventArgs e) { this.business.TryRemoveMarkerSet(this.business.SelectedMarkerIndex); }

        private void buttonVinsert_MarkerSetsIn_Click(object sender, EventArgs e) { this.business.Vinsert_MarkerSetsIn(); }
        private void buttonVinsert_SetMarkerSets_Click(object sender, EventArgs e) { this.business.Vinsert_SetMarkerSets(); }

        #endregion

    }

}
