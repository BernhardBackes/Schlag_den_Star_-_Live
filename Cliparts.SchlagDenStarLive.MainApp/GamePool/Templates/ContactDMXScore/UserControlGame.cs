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

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.ContactDMXScore {

    public partial class UserControlGame : _Base.Score.UserControlGame {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.stepsMaxIndex = this.patchStepButtons();
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            this.business = business;
            this.business.PropertyChanged += this.business_PropertyChanged;

            Binding bind;

            bind = new Binding("Text", this.business, "IOUnitName");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxIOUnitName.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "IOUnitStatus");
            bind.Format += this.bind_textBoxIOUnitName_BackColor;
            this.textBoxIOUnitName.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "LeftPlayerChannelState");
            bind.Format += (s, e) => { e.Value = (IOnet.IOUnit.IONbuz.InputChannelStates)e.Value == IOnet.IOUnit.IONbuz.InputChannelStates.DOWN ? Constants.ColorBuzzered : SystemColors.ButtonFace; };
            this.panelLeftPlayerChannelState.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "LeftPlayerDMXChannelClosed");
            bind.Format += (s, e) => { e.Value = ((bool)e.Value) ? Constants.ColorBuzzered : SystemColors.ButtonFace; };
            this.panelLeftPlayerDMXChannelClosed.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "RightPlayerChannelState");
            bind.Format += (s, e) => { e.Value = (IOnet.IOUnit.IONbuz.InputChannelStates)e.Value == IOnet.IOUnit.IONbuz.InputChannelStates.DOWN ? Constants.ColorBuzzered : SystemColors.ButtonFace; };
            this.panelRightPlayerChannelState.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "RightPlayerDMXChannelClosed");
            bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorBuzzered : SystemColors.ButtonFace; };
            this.panelRightPlayerDMXChannelClosed.DataBindings.Add(bind);

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

            this.textBoxIOUnitName.DataBindings.Clear();
            this.textBoxIOUnitName.DataBindings.Clear();
            this.panelLeftPlayerChannelState.DataBindings.Clear();
            this.panelLeftPlayerDMXChannelClosed.DataBindings.Clear();
            this.panelRightPlayerChannelState.DataBindings.Clear();
            this.panelRightPlayerDMXChannelClosed.DataBindings.Clear();
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
            step.AddButton(this.buttonVinsert_MaskIn);
            this.stepList.Add(step);

            index++;
            step = new stepAction(index, (stepIndex) => stepIndex + 1);
            step.AddButton(this.buttonVinsert_MaskOut);
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
            step = new stepAction(index, (stepIndex) => stepIndex - 4);
            step.AddButton(this.buttonVinsert_ScoreOut);
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

        private void bind_textBoxIOUnitName_BackColor(object sender, ConvertEventArgs e) {
            switch ((BuzzerUnitStates)e.Value) {
                case BuzzerUnitStates.NotAvailable:
                    e.Value = Constants.ColorMissing;
                    break;
                case BuzzerUnitStates.Missing:
                    e.Value = Constants.ColorMissing;
                    break;
                case BuzzerUnitStates.Disconnected:
                    e.Value = Constants.ColorDisabled;
                    break;
                case BuzzerUnitStates.Connecting:
                    e.Value = Constants.ColorEnabling;
                    break;
                case BuzzerUnitStates.Connected:
                    e.Value = SystemColors.Control;
                    break;
                case BuzzerUnitStates.Locked:
                    e.Value = SystemColors.Control;
                    break;
                case BuzzerUnitStates.BuzzerMode:
                    e.Value = Constants.ColorEnabled;
                    break;
                case BuzzerUnitStates.EventMode:
                    e.Value = Constants.ColorBuzzered;
                    break;
                default:
                    break;
            }
        }

        private void set_panelLeftPlayerDMXChannelClosed(
            bool closed) {
            }

        #endregion


        #region Events.Incoming
        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void buttonCloseLeftPlayerDMXChannel_Click(object sender, EventArgs e) { if (this.business is Business) this.business.LeftPlayerDMXChannelClosed = true; }
        private void buttonOpenLeftPlayerDMXChannel_Click(object sender, EventArgs e) { if (this.business is Business) this.business.LeftPlayerDMXChannelClosed = false; }

        private void buttonCloseRightPlayerDMXChannel_Click(object sender, EventArgs e) { if (this.business is Business) this.business.RightPlayerDMXChannelClosed = true; }
        private void buttonOpenRightPlayerDMXChannel_Click(object sender, EventArgs e) { if (this.business is Business) this.business.RightPlayerDMXChannelClosed = false; }

        private void buttonReleaseBuzzer_Click(object sender, EventArgs e) { if (this.business is Business) this.business.ReleaseBuzzer(); }
        private void buttonLockBuzzer_Click(object sender, EventArgs e) { if (this.business is Business) this.business.LockBuzzer(); }

        private void buttonVinsert_MaskIn_Click(object sender, EventArgs e) { if (this.business is Business) this.business.Vinsert_MaskIn(); }
        private void buttonVinsert_MaskOut_Click(object sender, EventArgs e) { if (this.business is Business) this.business.Vinsert_MaskOut(); }


        #endregion

    }

}
