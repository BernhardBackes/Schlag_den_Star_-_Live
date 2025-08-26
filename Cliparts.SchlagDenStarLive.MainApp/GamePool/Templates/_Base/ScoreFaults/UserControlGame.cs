using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.ScoreFaults {

    public partial class UserControlGame : _Base.UserControlGame {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.numericUpDownLeftPlayerScore.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerScore.Maximum = int.MaxValue;

            this.numericUpDownLeftPlayerFaults.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerFaults.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerScore.Minimum = int.MinValue;
            this.numericUpDownRightPlayerScore.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerFaults.Minimum = int.MinValue;
            this.numericUpDownRightPlayerFaults.Maximum = int.MaxValue;
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

            bind = new Binding("Value", this.business, "LeftPlayerFaults");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerFaults.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerFaults");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerFaults.DataBindings.Add(bind);

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
            this.business.Vinsert_SetScore();
            this.business.Vfullscreen_SetScore();
            this.business.Vstage_SetScore();
        }

        protected virtual void numericUpDownRightPlayerScore_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerScore = (int)this.numericUpDownRightPlayerScore.Value; }
        protected virtual void buttonRightPlayerAddScoreHot_01_Click(object sender, EventArgs e) {
            this.business.RightPlayerScore++;
            this.business.Vinsert_SetScore();
            this.business.Vfullscreen_SetScore();
            this.business.Vstage_SetScore();
        }

        protected virtual void numericUpDownLeftPlayerFaults_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerFaults = (int)this.numericUpDownLeftPlayerFaults.Value; }
        protected virtual void buttonLeftPlayerAddFaultsHot_01_Click(object sender, EventArgs e) {
            this.business.LeftPlayerFaults++;
            this.business.Vinsert_SetFaults();
        }
        private void buttonLeftPlayerFaultsIn_Click(object sender, EventArgs e) { this.business.Vinsert_LeftFaultsIn(); }
        private void buttonLeftPlayerFaultsOut_Click(object sender, EventArgs e) { this.business.Vinsert_LeftFaultsOut(); }

        protected virtual void numericUpDownRightPlayerFaults_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerFaults = (int)this.numericUpDownRightPlayerFaults.Value; }
        protected virtual void buttonRightPlayerAddFaultsHot_01_Click(object sender, EventArgs e) {
            this.business.RightPlayerFaults++;
            this.business.Vinsert_SetFaults();
        }
        private void buttonRightPlayerFaultsIn_Click(object sender, EventArgs e) { this.business.Vinsert_RightFaultsIn(); }
        private void buttonRightPlayerFaultsOut_Click(object sender, EventArgs e) { this.business.Vinsert_RightFaultsOut(); }

        protected virtual void buttonVinsert_ScoreIn_Click(object sender, EventArgs e) { this.business.Vinsert_ScoreIn(); }
        protected virtual void buttonVinsert_SetScore_Click(object sender, EventArgs e) { this.business.Vinsert_SetScore(); }
        protected virtual void buttonVinsert_ScoreOut_Click(object sender, EventArgs e) { this.business.Vinsert_ScoreOut(); }

        private void buttonVfullscreen_SetScore_Click(object sender, EventArgs e) { this.business.Vfullscreen_SetScore(); }

        protected virtual void buttonVstage_Init_Click(object sender, EventArgs e) { this.business.Vstage_Init(); }
        protected virtual void buttonVstage_SetScore_Click(object sender, EventArgs e) { this.business.Vstage_SetScore(); }

        #endregion

    }

}
