using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.Jakkolo {

    public partial class UserControlGame : _Base.UserControlGame {

        #region Properties

        private Business business;

        #endregion


        #region Funktionen

        public UserControlGame() {
            InitializeComponent();

            this.numericUpDownLeftPlayerCoin1.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerCoin1.Maximum = int.MaxValue;

            this.numericUpDownLeftPlayerCoin2.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerCoin2.Maximum = int.MaxValue;

            this.numericUpDownLeftPlayerCoin3.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerCoin3.Maximum = int.MaxValue;

            this.numericUpDownLeftPlayerCoin4.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerCoin4.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerCoin1.Minimum = int.MinValue;
            this.numericUpDownRightPlayerCoin1.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerCoin2.Minimum = int.MinValue;
            this.numericUpDownRightPlayerCoin2.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerCoin3.Minimum = int.MinValue;
            this.numericUpDownRightPlayerCoin3.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerCoin4.Minimum = int.MinValue;
            this.numericUpDownRightPlayerCoin4.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business) {
            base.Pose(business);

            this.business = business;

            Binding bind;

            bind = new Binding("Value", this.business, "LeftPlayerCoin1");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerCoin1.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerCoin2");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerCoin2.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerCoin3");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerCoin3.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "LeftPlayerCoin4");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerCoin4.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "LeftPlayerResult");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxLeftPlayerResult.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerCoin1");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerCoin1.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerCoin2");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerCoin2.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerCoin3");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerCoin3.DataBindings.Add(bind);

            bind = new Binding("Value", this.business, "RightPlayerCoin4");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerCoin4.DataBindings.Add(bind);

            bind = new Binding("Text", this.business, "RightPlayerResult");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.textBoxRightPlayerResult.DataBindings.Add(bind);
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

            this.numericUpDownLeftPlayerCoin1.DataBindings.Clear();
            this.numericUpDownLeftPlayerCoin2.DataBindings.Clear();
            this.numericUpDownLeftPlayerCoin3.DataBindings.Clear();
            this.numericUpDownLeftPlayerCoin4.DataBindings.Clear();
            this.textBoxLeftPlayerResult.DataBindings.Clear();

            this.numericUpDownRightPlayerCoin1.DataBindings.Clear();
            this.numericUpDownRightPlayerCoin2.DataBindings.Clear();
            this.numericUpDownRightPlayerCoin3.DataBindings.Clear();
            this.numericUpDownRightPlayerCoin4.DataBindings.Clear();
            this.textBoxRightPlayerResult.DataBindings.Clear();
        }

        #endregion


        #region Events.Incoming
        #endregion

        #region Events.Controls

        private new void control_Enter(object sender, EventArgs e) { base.control_Enter(sender, e); }
        private new void control_Leave(object sender, EventArgs e) { base.control_Leave(sender, e); }
        private new void control_EnabledChanged(object sender, EventArgs e) { base.control_EnabledChanged(sender, e); }

        private void numericUpDownLeftPlayerCoin1_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerCoin1 = (int)this.numericUpDownLeftPlayerCoin1.Value; }
        private void numericUpDownLeftPlayerCoin2_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerCoin2 = (int)this.numericUpDownLeftPlayerCoin2.Value; }
        private void numericUpDownLeftPlayerCoin3_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerCoin3 = (int)this.numericUpDownLeftPlayerCoin3.Value; }
        private void numericUpDownLeftPlayerCoin4_ValueChanged(object sender, EventArgs e) { this.business.LeftPlayerCoin4 = (int)this.numericUpDownLeftPlayerCoin4.Value; }
        private void buttonLeftPlayerCoin1AddHot_Click(object sender, EventArgs e) { this.business.LeftPlayerCoin1AddHot(); }
        private void buttonLeftPlayerCoin2AddHot_Click(object sender, EventArgs e) { this.business.LeftPlayerCoin2AddHot(); }
        private void buttonLeftPlayerCoin3AddHot_Click(object sender, EventArgs e) { this.business.LeftPlayerCoin3AddHot(); }
        private void buttonLeftPlayerCoin4AddHot_Click(object sender, EventArgs e) { this.business.LeftPlayerCoin4AddHot(); }

        private void numericUpDownRightPlayerCoin1_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerCoin1 = (int)this.numericUpDownRightPlayerCoin1.Value; }
        private void numericUpDownRightPlayerCoin2_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerCoin2 = (int)this.numericUpDownRightPlayerCoin2.Value; }
        private void numericUpDownRightPlayerCoin3_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerCoin3 = (int)this.numericUpDownRightPlayerCoin3.Value; }
        private void numericUpDownRightPlayerCoin4_ValueChanged(object sender, EventArgs e) { this.business.RightPlayerCoin4 = (int)this.numericUpDownRightPlayerCoin4.Value; }
        private void buttonRightPlayerCoin1AddHot_Click(object sender, EventArgs e) { this.business.RightPlayerCoin1AddHot(); }
        private void buttonRightPlayerCoin2AddHot_Click(object sender, EventArgs e) { this.business.RightPlayerCoin2AddHot(); }
        private void buttonRightPlayerCoin3AddHot_Click(object sender, EventArgs e) { this.business.RightPlayerCoin3AddHot(); }
        private void buttonRightPlayerCoin4AddHot_Click(object sender, EventArgs e) { this.business.RightPlayerCoin4AddHot(); }

        protected virtual void buttonVinsert_JakkoloIn_Click(object sender, EventArgs e) { this.business.Vinsert_JakkoloIn(); }
        protected virtual void buttonVinsert_SetJakkolo_Click(object sender, EventArgs e) { this.business.Vinsert_SetJakkolo(); }
        protected virtual void buttonVinsert_JakkoloOut_Click(object sender, EventArgs e) { this.business.Vinsert_JakkoloOut(); }

        protected virtual void buttonVstage_Init_Click(object sender, EventArgs e) { this.business.Vstage_Init(); }
        protected virtual void buttonVstage_SetScore_Click(object sender, EventArgs e) { this.business.Vstage_SetScore(); }

        #endregion

    }

}
