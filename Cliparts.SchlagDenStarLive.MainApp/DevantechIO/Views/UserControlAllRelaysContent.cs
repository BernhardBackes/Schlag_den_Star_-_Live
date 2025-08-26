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

using Cliparts.Devantech.Controls.Devices;

//#region Properties
//#endregion

//#region Funktionen
//#endregion

//#region Events.Incoming
//#endregion

//#region Events.Controls
//#endregion

namespace Cliparts.SchlagDenStarLive.MainApp.DevantechIO.Views {

    public partial class UserControlAllRelaysContent : UserControl {

        #region Properties

        private Controls.AllRelays controller = null;

        #endregion

        #region Funktionen

        public UserControlAllRelaysContent() {
            InitializeComponent();
        }

        public void Pose(
            Controls.AllRelays controller) {

            this.controller = controller;
            this.controller.PropertyChanged += this.controller_PropertyChanged;

            Binding bind;

            bind = new Binding("Text", this.controller, "RelayDeviceName");
            bind.Format += (s, e) => { e.Value = (string)e.Value; };
            this.comboBoxRelayNameList.DataBindings.Add(bind);

            bind = new Binding("BackColor", this.controller, "RelayDeviceStatus");
            bind.Format += this.bind_comboBoxRelayNameList;
            this.comboBoxRelayNameList.DataBindings.Add(bind);

            this.fillRelayNameList();

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

            this.controller.PropertyChanged -= this.controller_PropertyChanged;

            this.comboBoxRelayNameList.DataBindings.Clear();

        }

        private void fillRelayNameList() {
            this.comboBoxRelayNameList.BeginUpdate();
            this.comboBoxRelayNameList.Items.Clear();
            this.comboBoxRelayNameList.Items.AddRange(this.controller.RelayNameList);
            this.comboBoxRelayNameList.EndUpdate();
        }

        private void bind_comboBoxRelayNameList(object sender, ConvertEventArgs e) {
            switch ((ConnectionStates)e.Value) {
                case ConnectionStates.Connecting:
                    e.Value = Constants.ColorEnabling;
                    break;
                case ConnectionStates.Connected:
                    e.Value = SystemColors.Control;
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

        private void controller_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.controller_PropertyChanged(sender, e)));
            else {
                if (e.PropertyName == "RelayNameList") this.fillRelayNameList();
            }
        }

        #endregion

        #region Events.Controls

        protected virtual void comboBoxRelayNameList_TextChanged(object sender, EventArgs e) { if (this.controller is Controls.AllRelays) this.controller.RelayDeviceName = this.comboBoxRelayNameList.Text; }

        #endregion
    }
}
