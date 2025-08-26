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

    public partial class UserControlGamePoolTemplatesReversiField : UserControl {

        #region Properties

        private Field field;

        #endregion


        #region Funktionen

        public UserControlGamePoolTemplatesReversiField() {
            InitializeComponent();
            this.labelID.ForeColor = Color.Black;
        }

        public void Pose(
            Field field) {

            this.field = field;
            this.field.PropertyChanged += this.field_PropertyChanged;
            
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

        private void buttonNeutral_Click(object sender, EventArgs e) { this.field.SelectedPlayer = Content.Gameboard.PlayerSelection.NotSelected; }
        private void buttonLeftPlayer_Click(object sender, EventArgs e) { this.field.SelectedPlayer = Content.Gameboard.PlayerSelection.LeftPlayer; }
        private void buttonRightPlayer_Click(object sender, EventArgs e) { this.field.SelectedPlayer = Content.Gameboard.PlayerSelection.RightPlayer; }
        private void labelID_Click(object sender, EventArgs e) { this.field.SelectField(); }

        #endregion

    }
}
