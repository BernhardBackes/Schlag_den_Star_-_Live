using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cliparts.Devantech;
using Cliparts.Devantech.Controls.Devices;

namespace Cliparts.SchlagDenStarLive.MainApp.DevantechIO.Controls {

    public class AllRelays : Messaging.Message, INotifyPropertyChanged {

        #region Properties

        protected SynchronizationContext syncContext;

        protected Controller devantechHandler = null;

        public string[] RelayNameList {
            get {
                if (this.devantechHandler is Controller) return this.devantechHandler.LoadedNames;
                else return new string[0];
            }
        }

        private string relayDeviceName = string.Empty;
        public string RelayDeviceName {
            get { return this.relayDeviceName; }
            set {
                if (this.relayDeviceName != value) {
                    if (string.IsNullOrEmpty(value)) this.relayDeviceName = string.Empty;
                    else this.relayDeviceName = value;
                    this.on_PropertyChanged();
                    this.getDeviceStatus(this.relayDeviceName);
                }
            }
        }

        private ConnectionStates relayDeviceStatus = ConnectionStates.Idle;
        public ConnectionStates RelayDeviceStatus {
            get { return this.relayDeviceStatus; }
            private set {
                if (this.relayDeviceStatus != value) {
                    this.relayDeviceStatus = value;
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion


        #region Funktionen

        public AllRelays() {}

        public virtual void  Pose(
            SynchronizationContext syncContext,
            Controller devantechHandler) {

            this.syncContext = syncContext;

            this.devantechHandler = devantechHandler;
            this.devantechHandler.PropertyChanged += this.devantechHandler_PropertyChanged;
            this.devantechHandler.RelayClosed += this.devantechHandler_RelayClosed;
            this.devantechHandler.RelayOpened += this.devantechHandler_RelayOpened;
            this.getDeviceStatus(this.RelayDeviceName);

        }

        public virtual void Dispose() {
        }

        private void getDeviceStatus(
            string name) {
            if (string.IsNullOrEmpty(name) ||
                !this.RelayDeviceName.Contains(name) ||
                this.devantechHandler == null) this.RelayDeviceStatus = ConnectionStates.Idle;
            else this.RelayDeviceStatus = this.devantechHandler.GetDeviceStatus(name);
        }

        public void CloseAllRelays() {
            this.devantechHandler.CloseAllRelays(this.RelayDeviceName);
        }

        public void OpenAllRelays() {
            this.devantechHandler.OpenAllRelays(this.RelayDeviceName);
        }

        #endregion


        #region Events.Outgoing

        public event PropertyChangedEventHandler PropertyChanged;
        protected void on_PropertyChanged([CallerMemberName] string callerName = "") { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }
        protected void on_PropertyChanged(PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, e); }
        protected void on_PropertyChanged(object sender, PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(sender, this.PropertyChanged, e); }

        #endregion

        #region Events.Incoming

        private void devantechHandler_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            PropertyChangedEventSyncArgs pcesa = new PropertyChangedEventSyncArgs(this, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_devantechHandler_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, pcesa);
        }
        protected virtual void sync_devantechHandler_PropertyChanged(object content) {
            PropertyChangedEventSyncArgs pcesa = content as PropertyChangedEventSyncArgs;
            if (pcesa is PropertyChangedEventSyncArgs) {
                if (pcesa.EventArgs is PropertyChangedEventArgs) {
                    if (pcesa.EventArgs.PropertyName == "LoadedNames") this.on_PropertyChanged("RelayNameList");
                    else if (pcesa.EventArgs.PropertyName == "Status") this.getDeviceStatus(this.RelayDeviceName);
                }
            }
        }
        protected virtual void devantechHandler_RelayClosed(object sender, int e) {}
        protected virtual void devantechHandler_RelayOpened(object sender, int e) {}

        #endregion

    }

}
