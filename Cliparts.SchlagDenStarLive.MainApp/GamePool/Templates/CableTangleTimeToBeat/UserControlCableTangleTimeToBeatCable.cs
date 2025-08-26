using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.CableTangleTimeToBeat {

    public partial class UserControlCableTangleTimeToBeatCable : UserControl {

        #region Properties

        private Cable tile;

        #endregion


        #region Funktionen

        public UserControlCableTangleTimeToBeatCable() {
            InitializeComponent();

            this.comboBoxSelectedIOUnit.BeginUpdate();
            this.comboBoxSelectedIOUnit.Items.AddRange(Enum.GetNames(typeof(AvailableIOUnits)));
            this.comboBoxSelectedIOUnit.EndUpdate();

            this.numericUpDownBuzzerChannel.Minimum = int.MinValue;
            this.numericUpDownBuzzerChannel.Maximum = int.MaxValue;

            this.numericUpDownDMXStartAddress.Minimum = int.MinValue;
            this.numericUpDownDMXStartAddress.Maximum = int.MaxValue;

        }

        public void Pose(
            Cable tile) {

            this.tile = tile;

            this.labelID.Text = this.tile.ID.ToString();

            Binding bind;

            bind = new Binding("Text", this.tile, "SelectedIOUnit");
            bind.Format += (s, e) => { e.Value = e.Value.ToString(); };
            this.comboBoxSelectedIOUnit.DataBindings.Add(bind);
            this.comboBoxSelectedIOUnit.Text = this.tile.SelectedIOUnit.ToString();

            bind = new Binding("Value", this.tile, "BuzzerChannel");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownBuzzerChannel.DataBindings.Add(bind);
            this.numericUpDownBuzzerChannel.Value = this.tile.BuzzerChannel;

            bind = new Binding("Value", this.tile, "DMXStartAddress");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownDMXStartAddress.DataBindings.Add(bind);
            this.numericUpDownDMXStartAddress.Value = this.tile.DMXStartAddress;
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

            this.comboBoxSelectedIOUnit.DataBindings.Clear();
            this.numericUpDownBuzzerChannel.DataBindings.Clear();
            this.numericUpDownDMXStartAddress.DataBindings.Clear();
        }

        #endregion

        #region Events.Controls

        private void comboBoxSelectedIOUnit_SelectedIndexChanged(object sender, EventArgs e) {
            AvailableIOUnits unit;
            if (Enum.TryParse(this.comboBoxSelectedIOUnit.Text, out unit)) this.tile.SelectedIOUnit = unit;
        }
        private void numericUpDownBuzzerChannel_ValueChanged(object sender, EventArgs e) { this.tile.BuzzerChannel = (int)this.numericUpDownBuzzerChannel.Value; }
        private void numericUpDownDMXStartAddress_ValueChanged(object sender, EventArgs e) { this.tile.DMXStartAddress = (int)this.numericUpDownDMXStartAddress.Value; }
        private void buttonSetOn_Click(object sender, EventArgs e) { this.tile.SetLightOn(false); }
        private void buttonSetOff_Click(object sender, EventArgs e) { this.tile.SetLightOff(false); }

        #endregion

    }
}
