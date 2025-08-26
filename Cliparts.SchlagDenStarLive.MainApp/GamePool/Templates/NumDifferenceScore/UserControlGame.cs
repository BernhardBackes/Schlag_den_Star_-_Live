using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.NumDifferenceScore {

    public partial class UserControlGame : _Base.Score.UserControlGame {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.numericUpDownLeftPlayer1stHalf.Minimum = Decimal.MinValue;
            this.numericUpDownLeftPlayer1stHalf.Maximum = Decimal.MaxValue;

            this.numericUpDownLeftPlayer2ndHalf.Minimum = Decimal.MinValue;
            this.numericUpDownLeftPlayer2ndHalf.Maximum = Decimal.MaxValue;

            this.numericUpDownRightPlayer1stHalf.Minimum = Decimal.MinValue;
            this.numericUpDownRightPlayer1stHalf.Maximum = Decimal.MaxValue;

            this.numericUpDownRightPlayer2ndHalf.Minimum = Decimal.MinValue;
            this.numericUpDownRightPlayer2ndHalf.Maximum = Decimal.MaxValue;

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "LeftPlayer1stHalf");
            bind.Format += (s, e) => { e.Value = Convert.ToDouble(e.Value); };
            this.numericUpDownLeftPlayer1stHalf.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayer2ndHalf");
            bind.Format += (s, e) => { e.Value = Convert.ToDouble(e.Value); };
            this.numericUpDownLeftPlayer2ndHalf.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "LeftPlayerDifference");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxLeftPlayerDifference.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayer1stHalf");
            bind.Format += (s, e) => { e.Value = Convert.ToDouble(e.Value); };
            this.numericUpDownRightPlayer1stHalf.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayer2ndHalf");
            bind.Format += (s, e) => { e.Value = Convert.ToDouble(e.Value); };
            this.numericUpDownRightPlayer2ndHalf.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerDifference");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxRightPlayerDifference.DataBindings.Add(bind);

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

            this.numericUpDownLeftPlayer1stHalf.DataBindings.Clear();
            this.numericUpDownLeftPlayer2ndHalf.DataBindings.Clear();
            this.textBoxLeftPlayerDifference.DataBindings.Clear();

            this.numericUpDownRightPlayer1stHalf.DataBindings.Clear();
            this.numericUpDownRightPlayer2ndHalf.DataBindings.Clear();
            this.textBoxRightPlayerDifference.DataBindings.Clear();

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
            step.AddButton(this.buttonVstage_Init);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_NumericValuesIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex);
            step.AddButton(this.buttonVinsert_SetNumericValues);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_NumericValuesOut);
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
            step = new stepAction(index, (stepIndex) => stepIndex - 5);
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
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVfullscreen_UpdateGameboard);
            this.stepList.Add(step);

            if (index < this.stepsMaxIndex) this.on_Error("buildStepList", "stepList to short");
            if (index > this.stepsMaxIndex) this.on_Error("buildStepList", "stepList to long");
        }

        #endregion


        #region Events.Incoming
        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void numericUpDownLeftPlayer1stHalf_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftPlayer1stHalf = (double)this.numericUpDownLeftPlayer1stHalf.Value; }
        private void numericUpDownLeftPlayer2ndHalf_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.LeftPlayer2ndHalf = (double)this.numericUpDownLeftPlayer2ndHalf.Value; }
        private void buttonLeftPlayerCalc1stHalf_Click(object sender, EventArgs e) { if (this.business is Business) this.business.LeftPlayerCalc1stHalf(); }


        private void numericUpDownRightPlayer1stHalf_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightPlayer1stHalf = (double)this.numericUpDownRightPlayer1stHalf.Value; }
        private void numericUpDownRightPlayer2ndHalf_ValueChanged(object sender, EventArgs e) { if (this.business is Business) this.business.RightPlayer2ndHalf = (double)this.numericUpDownRightPlayer2ndHalf.Value; }
        private void buttonRightPlayerCalc1stHalf_Click(object sender, EventArgs e) { if (this.business is Business) this.business.RightPlayerCalc1stHalf(); }

        private void buttonVinsert_NumericValuesIn_Click(object sender, EventArgs e) { this.business.Vinsert_NumericValuesIn(); }
        private void buttonVinsert_SetNumericValues_Click(object sender, EventArgs e) { this.business.Vinsert_SetNumericValues(); }
        private void buttonVinsert_NumericValuesOut_Click(object sender, EventArgs e) { this.business.Vinsert_NumericValuesOut(); }

        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }

        #endregion
    }

}
