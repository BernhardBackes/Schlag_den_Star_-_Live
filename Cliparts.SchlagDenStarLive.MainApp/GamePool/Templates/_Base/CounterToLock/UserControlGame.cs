using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.CounterToLock {

    public partial class UserControlGame : _Base.UserControlGame {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.textBoxLeftPlayerName_1.BackColor = this.textBoxLeftPlayerName.BackColor;
            this.textBoxRightPlayerName_1.BackColor = this.textBoxRightPlayerName.BackColor;

            this.numericUpDownLeftPlayerCounter.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerCounter.Maximum = int.MaxValue;

            this.numericUpDownLeftPlayerLocked.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerLocked.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerCounter.Minimum = int.MinValue;
            this.numericUpDownRightPlayerCounter.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerLocked.Minimum = int.MinValue;
            this.numericUpDownRightPlayerLocked.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            this.business = business;

            Binding bind;

            bind = new Binding("Text", this.business, "LeftPlayerName");
            this.textBoxLeftPlayerName_1.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerName");
            this.textBoxRightPlayerName_1.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerLocked");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerLocked.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerCounter");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerCounter.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerLocked");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerLocked.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerCounter");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerCounter.DataBindings.Add(bind);

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

            this.textBoxLeftPlayerName_1.DataBindings.Clear();
            this.numericUpDownLeftPlayerLocked.DataBindings.Clear();
            this.numericUpDownLeftPlayerCounter.DataBindings.Clear();

            this.textBoxRightPlayerName_1.DataBindings.Clear();
            this.numericUpDownRightPlayerLocked.DataBindings.Clear();
            this.numericUpDownRightPlayerCounter.DataBindings.Clear();
        }

        #endregion


        #region Events.Incoming
        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        protected virtual void numericUpDownLeftPlayerLocked_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerLocked = (int)this.numericUpDownLeftPlayerLocked.Value; }
        private void numericUpDownLeftPlayerCounter_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerCounter = (int)this.numericUpDownLeftPlayerCounter.Value; }
        protected virtual void buttonLeftPlayerAddCounterHot_01_Click(object sender, EventArgs e) {
            this.business.LeftPlayerCounter++;
            this.business.Vinsert_SetCounterToLock();
            this.business.Vstage_SetScore();
        }

        protected virtual void numericUpDownRightPlayerLocked_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerLocked = (int)this.numericUpDownRightPlayerLocked.Value; }
        private void numericUpDownRightPlayerCounter_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerCounter = (int)this.numericUpDownRightPlayerCounter.Value; }
        protected virtual void buttonRightPlayerAddCounterHot_01_Click(object sender, EventArgs e) {
            this.business.RightPlayerCounter++;
            this.business.Vinsert_SetCounterToLock();
            this.business.Vstage_SetScore();
        }

        protected virtual void buttonVinsert_CounterToLockIn_Click(object sender, EventArgs e) { this.business.Vinsert_CounterToLockIn(); }
        protected virtual void buttonVinsert_SetCounterToLock_Click(object sender, EventArgs e) { this.business.Vinsert_SetCounterToLock(); }
        protected virtual void buttonVinsert_CounterToLockOut_Click(object sender, EventArgs e) { this.business.Vinsert_CounterToLockOut(); }

        protected virtual void buttonVstage_Init_Click(object sender, EventArgs e) { this.business.Vstage_Init(); }
        protected virtual void buttonVstage_SetScore_Click(object sender, EventArgs e) { this.business.Vstage_SetScore(); }

        private void buttonGame_FailHot_Click(object sender, EventArgs e) { this.business.FailHot(); }
        private void buttonGame_LockHot_Click(object sender, EventArgs e) { this.business.LockHot(); }

        #endregion
    }

}
