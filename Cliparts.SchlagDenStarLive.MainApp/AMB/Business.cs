using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cliparts.Tools.AMB;

namespace Cliparts.SchlagDenStarLive.MainApp.AMB {

    public enum TimelineStates {
        Offline,
        Locked,
        Unlocked
    }

    public class FirstContactArgs : EventArgs {
        public string Timeline { get; private set; }
        public FirstContactData Data { get; private set; }
        public FirstContactArgs(
            string timeline,
            FirstContactData data) {
            this.Timeline = timeline;
            this.Data = data;
        }
    }

    public class PassingArgs : EventArgs {
        public string Timeline{ get; private set; }
        public PassingData Data { get; private set; }
        public PassingArgs(
            string timeline,
            PassingData data) {
            this.Timeline = timeline;
            this.Data = data;
        }
    }

    public class Business : Cliparts.Messaging.Message, INotifyPropertyChanged {

        #region Properties

        private SynchronizationContext syncContext;

        private AMBControl ambControl;

        private Settings.Business settings;

        private Dictionary<string, string> decoderList = new Dictionary<string, string>();
        public string[] DecoderList { get { return this.decoderList.Values.ToArray(); } }      

        #endregion


        #region Funktionen

        public Business(
            SynchronizationContext syncContext,
            Settings.Business settings) {

            this.syncContext = syncContext;

            this.ambControl = new AMBControl();
            this.ambControl.DecoderChanged += this.ambControl_DecoderChanged;
            this.ambControl.DecoderConnected += this.ambControl_DecoderConnected;
            this.ambControl.DecoderDisconnected += this.ambControl_DecoderDisconnected;
            this.ambControl.Error += this.ambControl_Error;
            this.ambControl.FirstContacted += this.ambControl_FirstContacted;
            this.ambControl.Passed += this.ambControl_Passed;
            this.ambControl.TimeReceived += this.ambControl_TimeReceived;

            this.settings = settings;

        }

        public void Dispose() {
            this.ambControl.DecoderChanged -= this.ambControl_DecoderChanged;
            this.ambControl.DecoderConnected -= this.ambControl_DecoderConnected;
            this.ambControl.DecoderDisconnected -= this.ambControl_DecoderDisconnected;
            this.ambControl.Error -= this.ambControl_Error;
            this.ambControl.FirstContacted -= this.ambControl_FirstContacted;
            this.ambControl.Passed -= this.ambControl_Passed;
            this.ambControl.TimeReceived -= this.ambControl_TimeReceived;
        }

        public void ShowForm() {
            if (this.ambControl != null) {
                this.ambControl.ShowForm();
            }
        }

        #endregion


        #region Events.Outgoing

        public event PropertyChangedEventHandler PropertyChanged;
        protected void on_PropertyChanged([CallerMemberName]string callerName = "") { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }

        public event EventHandler<FirstContactArgs> FirstContact;
        private void on_FirstContact(object sender, FirstContactArgs e) { Helper.raiseEvent(sender, this.FirstContact, e); }

        public event EventHandler<PassingArgs> Passed;
        private void on_Passed(object sender, PassingArgs e) { Helper.raiseEvent(sender, this.Passed, e); }

        #endregion


        #region Events.Incoming

        void ambControl_DecoderChanged(DecoderBase decoder) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ambControl_DecoderChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, decoder);
        }
        private void sync_ambControl_DecoderChanged(object content) {
            DecoderBase decoder = content as DecoderBase;
            if (decoder is DecoderBase) {
                if (this.decoderList.ContainsKey(decoder.DecoderID) &&
                    this.decoderList[decoder.DecoderID] != decoder.TimelineName) {
                    this.decoderList[decoder.DecoderID] = decoder.TimelineName;
                    this.on_PropertyChanged("DecoderList");
                }
            }
        }

        void ambControl_DecoderConnected(DecoderBase decoder) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ambControl_DecoderConnected);
            if (this.syncContext != null) this.syncContext.Post(callback, decoder);
        }
        private void sync_ambControl_DecoderConnected(object content) {
            DecoderBase decoder = content as DecoderBase;
            if (decoder is DecoderBase) {
                if (!string.IsNullOrEmpty(decoder.DecoderID) &&
                    !this.decoderList.ContainsKey(decoder.DecoderID)) {
                    this.decoderList.Add(decoder.DecoderID, decoder.TimelineName);
                    this.on_PropertyChanged("DecoderList");
                }
            }
        }

        void ambControl_DecoderDisconnected(DecoderBase decoder) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ambControl_DecoderDisconnected);
            if (this.syncContext != null) this.syncContext.Post(callback, decoder);
        }
        private void sync_ambControl_DecoderDisconnected(object content) {
            DecoderBase decoder = content as DecoderBase;
            if (decoder is DecoderBase) {
                if (this.decoderList.ContainsKey(decoder.DecoderID)) {
                    this.decoderList.Remove(decoder.DecoderID);
                    this.on_PropertyChanged("DecoderList");
                }
            }
        }

        void ambControl_Error(object sender, Tools.Base.StringEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ambControl_Error);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ambControl_Error(object content) {
            Tools.Base.StringEventArgs e = content as Tools.Base.StringEventArgs;
            if (e is Tools.Base.StringEventArgs) { }
        }

        void ambControl_FirstContacted(FirstContactData firstContact) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ambControl_FirstContacted);
            if (this.syncContext != null) this.syncContext.Post(callback, firstContact);
        }
        private void sync_ambControl_FirstContacted(object content) {
            FirstContactData firstContact = content as FirstContactData;
            if (firstContact is FirstContactData) {
                if (this.decoderList.ContainsKey(firstContact.DecoderID)) this.on_FirstContact(this, new FirstContactArgs(this.decoderList[firstContact.DecoderID], firstContact));
            }
        }

        void ambControl_Passed(PassingData passing) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ambControl_Passed);
            if (this.syncContext != null) this.syncContext.Post(callback, passing);
        }
        private void sync_ambControl_Passed(object content) {
            PassingData passing = content as PassingData;
            if (passing is PassingData) {
                if (this.decoderList.ContainsKey(passing.DecoderID)) this.on_Passed(this, new PassingArgs(this.decoderList[passing.DecoderID], passing));
            }
        }

        void ambControl_TimeReceived(TimeData time) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ambControl_TimeReceived);
            if (this.syncContext != null) this.syncContext.Post(callback, time);
        }
        private void sync_ambControl_TimeReceived(object content) {
            TimeData time = content as TimeData;
            if (time is TimeData) { }
        }

        #endregion

    }
}
