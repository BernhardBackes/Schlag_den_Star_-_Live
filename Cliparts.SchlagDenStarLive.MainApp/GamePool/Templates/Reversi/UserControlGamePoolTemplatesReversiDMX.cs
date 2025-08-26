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

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.Reversi {

    public partial class UserControlGamePoolTemplatesReversiDMX : UserControl {

        #region Properties

        private Business business;

        private Field field;

        #endregion


        #region Funktionen

        public UserControlGamePoolTemplatesReversiDMX() {
            InitializeComponent();
            this.labelID.ForeColor = Color.Black;
            this.numericUpDownDMXStartAddress.Minimum = int.MinValue;
            this.numericUpDownDMXStartAddress.Maximum = int.MaxValue;
        }

        public void Pose(
            Business business,
            Field field) {

            this.business = business;

            this.field = field;
            this.field.PropertyChanged += this.field_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.field, "DMXStartAddress");
            bind.Format += (s, e) => { e.Value = (int)e.Value; };
            this.numericUpDownDMXStartAddress.DataBindings.Add(bind);

            this.labelID.Text = this.field.Name;
            this.buttonLeftPlayer.BackColor = Constants.ColorLeftPlayer;
            this.buttonRightPlayer.BackColor = Constants.ColorRightPlayer;

            this.setSelectedPlayer();
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
            this.field.PropertyChanged -= this.field_PropertyChanged;
            this.numericUpDownDMXStartAddress.DataBindings.Clear();
        }

        private void setSelectedPlayer() {
            switch (this.field.SelectedPlayer) {
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.LeftPlayer:
                    this.labelID.BackColor = Constants.ColorLeftPlayer;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.RightPlayer:
                    this.labelID.BackColor = Constants.ColorRightPlayer;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.NotSelected:
                default:
                    this.labelID.BackColor = SystemColors.Control;
                    break;
            }
        }

        #endregion


        #region Events.Incoming

        internal void field_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.field_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "SelectedPlayer") this.setSelectedPlayer();
            }
        }

        #endregion

        #region Events.Controls

        private void numericUpDownDMXStartAddress_ValueChanged(object sender, EventArgs e) { this.field.DMXStartAddress = (int)this.numericUpDownDMXStartAddress.Value; }

        private void buttonNeutral_Click(object sender, EventArgs e) { 
            this.field.SelectedPlayer = Content.Gameboard.PlayerSelection.NotSelected;
            this.business.SetDMXValues();
        }
        private void buttonLeftPlayer_Click(object sender, EventArgs e) { 
            this.field.SelectedPlayer = Content.Gameboard.PlayerSelection.LeftPlayer;
            this.business.SetDMXValues();
        }
        private void buttonRightPlayer_Click(object sender, EventArgs e) { 
            this.field.SelectedPlayer = Content.Gameboard.PlayerSelection.RightPlayer;
            this.business.SetDMXValues();
        }

        #endregion

    }
}
