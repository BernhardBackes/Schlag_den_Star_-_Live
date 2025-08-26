using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliparts.SchlagDenStarLive.MainApp.DevantechIO.Views {

    public partial class UserControlRelaysLeftRightGame : UserControlAllRelaysGame {

        #region Properties

        private Controls.RelaysLeftRight controller = null;

        #endregion

        #region Funktionen

        public UserControlRelaysLeftRightGame() {
            InitializeComponent();
        }

        public void Pose(
            Controls.RelaysLeftRight controller) {

            base.Pose(controller);

            this.controller = controller;

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

        }

        #endregion

        #region Events.Incoming

        #endregion

        #region Events.Controls

        private void buttonCloseLeftRelay_Click(object sender, EventArgs e) { this.controller.CloseLeftRelay(); }
        private void buttonOpenLeftRelay_Click(object sender, EventArgs e) { this.controller.OpenLeftRelay(); }

        private void buttonCloseRightRelay_Click(object sender, EventArgs e) { this.controller.CloseRightRelay(); }
        private void buttonOpenRightRelay_Click(object sender, EventArgs e) { this.controller.OpenRightRelay(); }

        #endregion

    }
}
