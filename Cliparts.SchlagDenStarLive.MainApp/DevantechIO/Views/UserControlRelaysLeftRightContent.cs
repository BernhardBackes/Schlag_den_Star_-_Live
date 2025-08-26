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
    public partial class UserControlRelaysLeftRightContent : UserControlAllRelaysContent {

        #region Properties

        private Controls.RelaysLeftRight controller = null;

        #endregion

        #region Funktionen

        public UserControlRelaysLeftRightContent() {
            InitializeComponent();

            this.numericUpDownLeftPlayerRelayChannel.Minimum = int.MinValue;
            this.numericUpDownLeftPlayerRelayChannel.Maximum = int.MaxValue;

            this.numericUpDownRightPlayerRelayChannel.Minimum = int.MinValue;
            this.numericUpDownRightPlayerRelayChannel.Maximum = int.MaxValue;
        }

        public void Pose(
            Controls.RelaysLeftRight controller) {

            base.Pose(controller);

            this.controller = controller;
            this.controller.PropertyChanged += this.controller_PropertyChanged;

            Binding bind;

            bind = new Binding("Value", this.controller, "LeftPlayerRelayChannel");
            bind.Format += (s, e) => { e.Value = (byte)e.Value; };
            this.numericUpDownLeftPlayerRelayChannel.DataBindings.Add(bind);

            bind = new Binding("Value", this.controller, "RightPlayerRelayChannel");
            bind.Format += (s, e) => { e.Value = (byte)e.Value; };
            this.numericUpDownRightPlayerRelayChannel.DataBindings.Add(bind);

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

            this.numericUpDownLeftPlayerRelayChannel.DataBindings.Clear();
            this.numericUpDownRightPlayerRelayChannel.DataBindings.Clear();

        }

        #endregion

        #region Events.Incoming

        private void controller_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.InvokeRequired) this.Invoke((MethodInvoker)(() => this.controller_PropertyChanged(sender, e)));
            else {
                //if (e.PropertyName == "RelayNameList") this.fillRelayNameList();
            }
        }

        #endregion

        #region Events.Controls

        protected virtual void numericUpDownLeftPlayerRelayChannel_ValueChanged(object sender, EventArgs e) { this.controller.LeftPlayerRelayChannel = Convert.ToByte(this.numericUpDownLeftPlayerRelayChannel.Value); }
        protected virtual void numericUpDownRightPlayerRelayChannel_ValueChanged(object sender, EventArgs e) { this.controller.RightPlayerRelayChannel = Convert.ToByte(this.numericUpDownRightPlayerRelayChannel.Value); }

        #endregion

    }
}
