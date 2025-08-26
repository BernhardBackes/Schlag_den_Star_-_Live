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

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.ScorePointer {

    public partial class UserControlGame : _Base.UserControlGame {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.numericUpDownLeftPlayerScore.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerScore.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerScore.Minimum = int.MinValue;
            this.numericUpDownRightPlayerScore.Maximum = int.MaxValue;

            this.numericUpDownPointer.Minimum = int.MinValue;
            this.numericUpDownPointer.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "LeftPlayerScore");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerScore.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerScore");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerScore.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "FlipPlayers");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxFlipPlayers.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "Pointer");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownPointer.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "IOUnitName");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxIOUnitName.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.business, "IOUnitStatus");
            bind.Format += this.bind_textBoxIOUnitName_BackColor;
            this.textBoxIOUnitName.DataBindings.Add(bind);

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

            this.numericUpDownLeftPlayerScore.DataBindings.Clear();
            this.numericUpDownRightPlayerScore.DataBindings.Clear();
            this.checkBoxFlipPlayers.DataBindings.Clear();
            this.numericUpDownPointer.DataBindings.Clear();
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

        #endregion


        #region Events.Incoming
        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        protected virtual void numericUpDownLeftPlayerScore_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerScore = (int)this.numericUpDownLeftPlayerScore.Value; }
        protected virtual void buttonLeftPlayerAddScoreHot_01_Click(object sender, EventArgs e) {
            this.business.LeftPlayerScore++;
            this.business.Vinsert_SetScorePointer();
            this.business.Vstage_SetScore();
        }

        protected virtual void numericUpDownRightPlayerScore_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerScore = (int)this.numericUpDownRightPlayerScore.Value; }
        protected virtual void buttonRightPlayerAddScoreHot_01_Click(object sender, EventArgs e) {
            this.business.RightPlayerScore++;
            this.business.Vinsert_SetScorePointer();
            this.business.Vstage_SetScore();
        }

        private void checkBoxFlipPlayers_CheckedChanged(object sender, EventArgs e) { this.business.FlipPlayers = this.checkBoxFlipPlayers.Checked; }

        protected virtual void numericUpDownPointer_ValueChanged(object sender, EventArgs e) { this.business.Pointer = (int)this.numericUpDownPointer.Value; }
        protected virtual void buttonAddPointerHot_01_Click(object sender, EventArgs e) {
            this.business.Pointer++;
            this.business.Vinsert_SetScorePointer();
        }

        protected void buttonReleaseBuzzer_Click(object sender, EventArgs e) { if (this.business is Business) this.business.ReleaseBuzzer(); }
        protected void buttonLockBuzzer_Click(object sender, EventArgs e) { if (this.business is Business) this.business.LockBuzzer(); }

        protected virtual void buttonVinsert_ScoreIn_Click(object sender, EventArgs e) { this.business.Vinsert_ScorePointerIn(); }
        protected virtual void buttonVinsert_SetScore_Click(object sender, EventArgs e) { this.business.Vinsert_SetScorePointer(); }
        protected virtual void buttonVinsert_ScoreOut_Click(object sender, EventArgs e) { this.business.Vinsert_ScorePointerOut(); }

        protected virtual void buttonVstage_Init_Click(object sender, EventArgs e) { this.business.Vstage_Init(); }
        protected virtual void buttonVstage_SetScore_Click(object sender, EventArgs e) { this.business.Vstage_SetScore(); }

        #endregion

    }

}
