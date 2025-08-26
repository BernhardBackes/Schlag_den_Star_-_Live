using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cliparts.Devantech;
using Cliparts.Devantech.Controls.Devices;

namespace Cliparts.SchlagDenStarLive.MainApp.DevantechIO.Controls {

    public class RelaysLeftRight : AllRelays {

        #region Properties

        private byte leftPlayerRelayChannel = 1;
        public byte LeftPlayerRelayChannel {
            get { return this.leftPlayerRelayChannel; }
            set {
                if (this.leftPlayerRelayChannel != value) {
                    if (value < 1) value = 1;
                    if (value > 8) value = 8;
                    this.leftPlayerRelayChannel = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private byte rightPlayerRelayChannel = 2;
        public byte RightPlayerRelayChannel {
            get { return this.rightPlayerRelayChannel; }
            set {
                if (this.rightPlayerRelayChannel != value) {
                    if (value < 1) value = 1;
                    if (value > 8) value = 8;
                    this.rightPlayerRelayChannel = value;
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion

        #region Funktionen

        public RelaysLeftRight() {}

        public override void Pose(
            SynchronizationContext syncContext,
            Controller devantechHandler) {

            base.Pose(syncContext, devantechHandler);
        }

        public override void Dispose() {
            base.Dispose();
        }

        public void CloseLeftRelay() { this.devantechHandler.CloseRelay(this.RelayDeviceName, this.LeftPlayerRelayChannel); }
        public void OpenLeftRelay() { this.devantechHandler.OpenRelay(this.RelayDeviceName, this.LeftPlayerRelayChannel); }

        public void CloseRightRelay() { this.devantechHandler.CloseRelay(this.RelayDeviceName, this.RightPlayerRelayChannel); }
        public void OpenRightRelay() { this.devantechHandler.OpenRelay(this.RelayDeviceName, this.RightPlayerRelayChannel); }

        #endregion

        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
