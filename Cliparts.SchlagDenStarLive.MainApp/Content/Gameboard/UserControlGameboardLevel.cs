using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard {

    public partial class UserControlGameboardLevel : UserControl {

        #region Properties

        private Level level;

        #endregion


        #region Funktionen

        public UserControlGameboardLevel() { 
            InitializeComponent();
        }

        public void Pose(
            Level level) {

            this.BackColor = this.Parent.BackColor;

            this.level = level;

            if (this.level is Level) {

                this.labelID.Text = this.level.ID.ToString();

                Binding bind;

                bind = new Binding("BackColor", this.level, "Selected");
                bind.Format += (s, e) => { e.Value = (bool)e.Value ? Constants.ColorSelected : this.Parent.BackColor; };
                this.DataBindings.Add(bind);

                this.level.PropertyChanged += this.level_PropertyChanged;

                this.setWinner();
            }

        }

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);

            this.checkBoxEnabled.DataBindings.Clear();
            this.DataBindings.Clear();

            this.level.PropertyChanged -= this.level_PropertyChanged;
        }

        private void setWinner() {
            switch (this.level.Winner) {
                case PlayerSelection.LeftPlayer:
                    this.buttonLeftPlayerWinner.BackColor = Constants.ColorWinner;
                    this.buttonRightPlayerWinner.UseVisualStyleBackColor = true;
                    break;
                case PlayerSelection.RightPlayer:
                    this.buttonLeftPlayerWinner.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerWinner.BackColor = Constants.ColorWinner;
                    break;
                case PlayerSelection.NotSelected:
                default:
                    this.buttonLeftPlayerWinner.UseVisualStyleBackColor = true;
                    this.buttonRightPlayerWinner.UseVisualStyleBackColor = true;
                    break;
            }
        }

        #endregion


        #region Events.Incoming

        void level_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.level_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "Winner") this.setWinner();
            }
        }

        #endregion

        #region Events.Controls

        private void UserControlGameboardLevel_Click(object sender, EventArgs e) { this.level.Selected = true; }

        private void labelID_Click(object sender, EventArgs e) { this.level.Selected = true; }

        private void checkBoxBlocked_CheckedChanged(object sender, EventArgs e) { this.level.Enabled = this.checkBoxEnabled.Checked; }

        private void buttonLeftPlayerWinner_Click(object sender, EventArgs e) { this.level.ToggleWinner(PlayerSelection.LeftPlayer); }

        private void buttonRightPlayerWinner_Click(object sender, EventArgs e) { this.level.ToggleWinner(PlayerSelection.RightPlayer); }

        #endregion

    }
}
