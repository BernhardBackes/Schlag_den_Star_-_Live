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

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.Buzzer {

    public partial class UserControlGame : _Base.UserControlGame {

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

            this.setBuzzeredPlayer();

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

        private void setBuzzeredPlayer() {
            switch (this.business.BuzzeredPlayer) {
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.LeftPlayer:
                    this.buttonLeftPlayerBuzzer.BackColor = Constants.ColorBuzzered;
                    this.buttonRightPlayerBuzzer.UseVisualStyleBackColor = true;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.RightPlayer:
                    this.buttonLeftPlayerBuzzer.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerBuzzer.BackColor = Constants.ColorBuzzered;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.NotSelected:
                default:
                    this.buttonLeftPlayerBuzzer.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerBuzzer.UseVisualStyleBackColor = true;
                    break;
            }
        }

        #endregion


        #region Events.Incoming

        protected override void business_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.business_PropertyChanged(sender, e);
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.business_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "BuzzeredPlayer") this.setBuzzeredPlayer();
            }

        }

        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void buttonLeftPlayerBuzzer_Click(object sender, EventArgs e) { this.business.DoBuzzer(Content.Gameboard.PlayerSelection.LeftPlayer); }
        private void buttonRightPlayerBuzzer_Click(object sender, EventArgs e) { this.business.DoBuzzer(Content.Gameboard.PlayerSelection.RightPlayer); }

        private void buttonReleaseBuzzer_Click(object sender, EventArgs e) { if (this.business is Business) this.business.ReleaseBuzzer(); }
        private void buttonLockBuzzer_Click(object sender, EventArgs e) { if (this.business is Business) this.business.LockBuzzer(); }

        private void buttonGame_ReleaseBuzzer_Click(object sender, EventArgs e) { if (this.business is Business) this.business.ReleaseBuzzer(); }
        private void buttonGame_AllLightsBlack_Click(object sender, EventArgs e) { this.business.AllLightsBlack(); }
        #endregion

    }

}
