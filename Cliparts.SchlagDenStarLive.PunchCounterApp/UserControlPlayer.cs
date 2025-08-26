using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.PunchCounterApp {
    public partial class UserControlPlayer : UserControl {

        #region Properties

        private Player player;

        #endregion


        #region Funktionen

        public UserControlPlayer() {
            InitializeComponent();
            this.userControlChannel0.BackColor = this.BackColor;
            this.userControlChannel1.BackColor = this.BackColor;
        }

        public void Pose(
            Player player) {
            this.player = player;
            this.player.PropertyChanged += this.Player_PropertyChanged;

            this.labelID.Text = this.player.ID;

            this.userControlChannel0.Pose(this.player.Channel0);
            this.userControlChannel1.Pose(this.player.Channel1);

            this.setCounter(this.player.Counter);
            this.setCounter(this.player.Enabled);
        }

        private void setCounter(
            int value) {
            this.labelCounter.Text = value.ToString();
        }

        private void setCounter(
            bool value) {
            if (value) this.labelCounter.ForeColor = Color.LawnGreen;
            else this.labelCounter.ForeColor = this.ForeColor;
        }

        #endregion


        #region Events.Incoming

        private void Player_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.Player_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "Counter") this.setCounter(this.player.Counter);
                else if (e.PropertyName == "Enabled") this.setCounter(this.player.Enabled);
            }
        }

        #endregion

        #region Events.Controls
        private void UserControlPlayer_BackColorChanged(object sender, EventArgs e) {
            this.userControlChannel0.BackColor = this.BackColor;
            this.userControlChannel1.BackColor = this.BackColor;
        }

        #endregion

    }
}
