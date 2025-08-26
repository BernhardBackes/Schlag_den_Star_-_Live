using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.DoubleRGBIndicators {

    public partial class UserControlIndicatorSettings : UserControl {

        #region Properties

        private Business business;
        private Indicator indicator;

        #endregion


        #region Funktionen

        public UserControlIndicatorSettings() {
            InitializeComponent();

            this.numericUpDownLeftPlayerStartAddress.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerStartAddress.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerStartAddress.Minimum = int.MinValue;
            this.numericUpDownRightPlayerStartAddress.Maximum = int.MaxValue;

        }

        public void Pose(
            Business business,
            Indicator indicator) {

            this.business = business;
            this.indicator = indicator;

            Binding bind;

            bind = new Binding("Value", this.indicator, "LeftPlayerStartAddress");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownLeftPlayerStartAddress.DataBindings.Add(bind);

            bind = new Binding("Value", this.indicator, "RightPlayerStartAddress");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownRightPlayerStartAddress.DataBindings.Add(bind);

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

            this.numericUpDownLeftPlayerStartAddress.DataBindings.Clear();
            this.numericUpDownRightPlayerStartAddress.DataBindings.Clear();
        }


        #endregion

        #region Events.Incoming

        #endregion

        #region Events.Controls

        private void numericUpDownLeftPlayerStartAddress_ValueChanged(object sender, EventArgs e) { this.indicator.LeftPlayerStartAddress = (int)this.numericUpDownLeftPlayerStartAddress.Value; }
        private void buttonLeftPlayerOn_Click(object sender, EventArgs e) { this.business.SetLeftColor(this.indicator.LeftPlayerStartAddress); }
        private void buttonLeftPlayerOff_Click(object sender, EventArgs e) { this.business.SetOffColor(this.indicator.LeftPlayerStartAddress); }
        private void buttonLeftPlayerBlack_Click(object sender, EventArgs e) { this.business.SetBlack(this.indicator.LeftPlayerStartAddress); }

        private void numericUpDownRightPlayerStartAddress_ValueChanged(object sender, EventArgs e) { this.indicator.RightPlayerStartAddress = (int)this.numericUpDownRightPlayerStartAddress.Value; }
        private void buttonRightPlayerOn_Click(object sender, EventArgs e) { this.business.SetRightColor(this.indicator.RightPlayerStartAddress); }
        private void buttonRightPlayerOff_Click(object sender, EventArgs e) { this.business.SetOffColor(this.indicator.RightPlayerStartAddress); }
        private void buttonRightPlayerBlack_Click(object sender, EventArgs e) { this.business.SetBlack(this.indicator.RightPlayerStartAddress); }

        private void buttonOn_Click(object sender, EventArgs e) {
            this.business.SetLeftColor(this.indicator.LeftPlayerStartAddress);
            this.business.SetRightColor(this.indicator.RightPlayerStartAddress);
        }
        private void buttonOff_Click(object sender, EventArgs e) {
            this.business.SetOffColor(this.indicator.LeftPlayerStartAddress);
            this.business.SetOffColor(this.indicator.RightPlayerStartAddress);
        }
        private void buttonBlack_Click(object sender, EventArgs e) {
            this.business.SetBlack(this.indicator.LeftPlayerStartAddress);
            this.business.SetBlack(this.indicator.RightPlayerStartAddress);
        }

        #endregion

    }
}
