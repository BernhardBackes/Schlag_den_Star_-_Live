using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.ShootingScore {

    public partial class UserControlGame : _Base.UserControlGame {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.numericUpDownLeftPlayerScore.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerScore.Maximum = int.MaxValue;

            this.numericUpDownLeftPlayerHeats.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerHeats.Maximum = int.MaxValue;

            this.numericUpDownLeftPlayerHits.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerHits.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerScore.Minimum = int.MinValue;
            this.numericUpDownRightPlayerScore.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerHeats.Minimum = int.MinValue;
            this.numericUpDownRightPlayerHeats.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerHits.Minimum = int.MinValue;
            this.numericUpDownRightPlayerHits.Maximum = int.MaxValue;

        }

        public void Pose(
            Business business) {
            base.Pose(business);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "LeftPlayerScore");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerScore.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerHeats");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerHeats.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerHits");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerHits.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerScore");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerScore.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerHeats");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerHeats.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerHits");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerHits.DataBindings.Add(bind);

            bind = new Binding("Checked", this.business, "FlipPlayers");
            bind.Format += (s, e) => { e.Value = (bool)e.Value; };
            this.checkBoxFlipPlayers.DataBindings.Add(bind);

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
            this.numericUpDownLeftPlayerHeats.DataBindings.Clear();
            this.numericUpDownLeftPlayerHits.DataBindings.Clear();
            this.numericUpDownRightPlayerScore.DataBindings.Clear();
            this.numericUpDownRightPlayerHeats.DataBindings.Clear();
            this.numericUpDownRightPlayerHits.DataBindings.Clear();
            this.checkBoxFlipPlayers.DataBindings.Clear();
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
            this.business.Vstage_SetScore();
        }

        protected virtual void numericUpDownRightPlayerScore_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerScore = (int)this.numericUpDownRightPlayerScore.Value; }
        protected virtual void buttonRightPlayerAddScoreHot_01_Click(object sender, EventArgs e) {
            this.business.RightPlayerScore++;
            this.business.Vinsert_SetScore();
            this.business.Vstage_SetScore();
        }

        private void checkBoxFlipPlayers_CheckedChanged(object sender, EventArgs e) { this.business.FlipPlayers = this.checkBoxFlipPlayers.Checked; }

        private void numericUpDownLeftPlayerHeats_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerHeats = (int)this.numericUpDownLeftPlayerHeats.Value; }
        private void buttonLeftPlayerAddHeatHot_Click(object sender, EventArgs e) {
            this.business.LeftPlayerHeats++;
            this.business.Vinsert_SetShooting();
            this.business.Vstage_SetScore();
        }
        private void buttonLeftPlayerNextHeat_Click(object sender, EventArgs e) { this.business.LeftPlayerNextHeat(); }
        private void numericUpDownLeftPlayerHits_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerHits = (int)this.numericUpDownLeftPlayerHits.Value; }
        private void buttonLeftPlayerAddHitHot_Click(object sender, EventArgs e) {
            this.business.LeftPlayerHits++;
            this.business.Vinsert_SetShooting();
        }
        private void buttonLeftPlayerHitsIn_Click(object sender, EventArgs e) { this.business.Vinsert_ShootingLeftPlayerHitsIn();  }
        private void buttonLeftPlayerHitsOut_Click(object sender, EventArgs e) { this.business.Vinsert_ShootingLeftPlayerHitsOut(); }
        private void buttonLeftPlayerHitMissed_Click(object sender, EventArgs e) { this.business.Vinsert_ShootingLeftPlayerHitMiss(); }

        private void numericUpDownRightPlayerHeats_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerHeats = (int)this.numericUpDownRightPlayerHeats.Value; }
        private void buttonRightPlayerAddHeatHot_Click(object sender, EventArgs e) {
            this.business.RightPlayerHeats++;
            this.business.Vinsert_SetShooting();
            this.business.Vstage_SetScore();
        }
        private void buttonRightPlayerNextHeat_Click(object sender, EventArgs e) { this.business.RightPlayerNextHeat(); }
        private void numericUpDownRightPlayerHits_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerHits = (int)this.numericUpDownRightPlayerHits.Value; }
        private void buttonRightPlayerAddHitHot_Click(object sender, EventArgs e) {
            this.business.RightPlayerHits++;
            this.business.Vinsert_SetShooting();
        }
        private void buttonRightPlayerHitsIn_Click(object sender, EventArgs e) { this.business.Vinsert_ShootingRightPlayerHitsIn(); }
        private void buttonRightPlayerHitsOut_Click(object sender, EventArgs e) { this.business.Vinsert_ShootingRightPlayerHitsOut(); }
        private void buttonRightPlayerHitMissed_Click(object sender, EventArgs e) { this.business.Vinsert_ShootingRightPlayerHitMiss(); }

        private void buttonVinsert_ShootingIn_Click(object sender, EventArgs e) { this.business.Vinsert_ShootingIn(); }
        private void buttonVinsert_SetShooting_Click(object sender, EventArgs e) { this.business.Vinsert_SetShooting(); }
        private void buttonVinsert_ShootingOut_Click(object sender, EventArgs e) { this.business.Vinsert_ShootingOut(); }

        protected virtual void buttonVinsert_ScoreIn_Click(object sender, EventArgs e) { this.business.Vinsert_ScoreIn(); }
        protected virtual void buttonVinsert_SetScore_Click(object sender, EventArgs e) { this.business.Vinsert_SetScore(); }
        protected virtual void buttonVinsert_ScoreOut_Click(object sender, EventArgs e) { this.business.Vinsert_ScoreOut(); }

        protected virtual void buttonVstage_Init_Click(object sender, EventArgs e) { this.business.Vstage_Init(); }
        protected virtual void buttonVstage_SetScore_Click(object sender, EventArgs e) { this.business.Vstage_SetScore(); }

        private void buttonGame_Resolve_Click(object sender, EventArgs e) { this.business.Resolve(); }
        private void buttonGame_Next_Click(object sender, EventArgs e) { this.business.Next(); }

        #endregion

    }

}
