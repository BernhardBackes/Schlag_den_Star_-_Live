using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.Devantech.Controls.Devices;

namespace Cliparts.SchlagDenStarLive.MainApp.DevantechIO.Views {
    public partial class UserControlAllRelaysGame : UserControl {

        #region Properties

        protected Controls.AllRelays controller = null;

        #endregion

        #region Funktionen

        public UserControlAllRelaysGame() {
            InitializeComponent();
        }

        public void Pose(
            Controls.AllRelays controller) {

            this.controller = controller;

            Binding bind;

            bind = new Binding("Text", this.controller, "RelayDeviceName");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.textBoxDevice.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.controller, "RelayDeviceStatus");
            bind.Format += this.bind_textBoxDevice;
            this.textBoxDevice.DataBindings.Add(bind);

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

        private void bind_textBoxDevice(object sender, ConvertEventArgs e) {
            switch ((ConnectionStates)e.Value) {
                case ConnectionStates.Connecting:
                    e.Value = Constants.ColorEnabling;
                    break;
                case ConnectionStates.Connected:
                    e.Value = Constants.ColorEnabled;
                    break;
                case ConnectionStates.Disconnecting:
                    e.Value = Constants.ColorDisabling;
                    break;
                case ConnectionStates.Disconnected:
                    e.Value = Constants.ColorDisabled;
                    break;
                case ConnectionStates.Idle:
                default:
                    e.Value = Constants.ColorMissing;
                    break;
            }
        }

        #endregion

        #region Events.Incoming
        #endregion

        #region Events.Controls

        private void buttonCloseAllRelays_Click(object sender, EventArgs e) { this.controller.CloseAllRelays(); }

        private void buttonOpenAllRelays_Click(object sender, EventArgs e) { this.controller.OpenAllRelays(); }

        #endregion

    }
}
