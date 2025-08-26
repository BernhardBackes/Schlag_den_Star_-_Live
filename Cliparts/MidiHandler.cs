using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Cliparts.Tools.MidiMaster;

namespace Cliparts.MidiHandler {

    public class DalyedPlayer {
        private SynchronizationContext syncContext;
        public string EventName { get; private set; }
        private System.Timers.Timer delayTimer;
        public DalyedPlayer(
            SynchronizationContext syncContext,
            string eventName,
            int delay) {
            this.syncContext = syncContext;
            this.EventName = eventName;
            delayTimer = new System.Timers.Timer(delay);
            delayTimer.AutoReset = false;
            delayTimer.Elapsed += this.delayTimer_Elapsed;
        }

        public void Start() { this.delayTimer.Start(); }

        public event EventHandler Elapsed;
        private void on_Elapsed(object sender, EventArgs e) { if (this.Elapsed != null) this.Elapsed(sender, e); }

        void delayTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_delayTimer_Elapsed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_delayTimer_Elapsed(object content) { this.on_Elapsed(this, new EventArgs()); }
    }

    public class EventListArgs : EventArgs {
        public string[] NameList { get; private set; }
        public EventListArgs(string[] nameList) { this.NameList = nameList; }
    }

    public class StatusChangedArgs : EventArgs {
        public bool IsOpen { get; private set; }
        public bool IsEnabled { get; private set; }
        public StatusChangedArgs(
            bool isOpen,
            bool isEnabled) {
            this.IsOpen = isOpen;
            this.IsEnabled = isEnabled;
        }
    }

    public class SendMidiEventArgs : EventArgs {
        public string Name { get; private set; }
        public SendMidiEventArgs(string name) { this.Name = name; }
    }

    public class Business : Cliparts.Messaging.Message {

        #region Properties

        private SynchronizationContext syncContext;

        private MidiMaster midiMaster;
        private bool in_IsOpen = false;
        private bool in_IsEnabled = false;
        private bool out_IsOpen = false;
        private bool out_IsEnabled = false;

        public string Filename {
            get {
                if (this.midiMaster == null) return string.Empty;
                else return this.midiMaster.FileName;
            }
        }

        #endregion


        #region Funktionen

        public Business(
            SynchronizationContext syncContext,
            string filename) {

            this.syncContext = syncContext;

            this.midiMaster = new MidiMaster();
            this.midiMaster.MidiInStatusChanged += new EventHandler<MidiStatusChangedEventArgs>(this.midiMaster_MidiInStatusChanged);
            this.midiMaster.MidiOutStatusChanged += new EventHandler<MidiStatusChangedEventArgs>(this.midiMaster_MidiOutStatusChanged);
            this.midiMaster.MidiReceived += new EventHandler<MidiReceivedEventArgs>(this.midiMaster_MidiReceived);
            this.loadFile(filename);
        }

        public void Dispose() {
            this.midiMaster.MidiInStatusChanged -= this.midiMaster_MidiInStatusChanged;
            this.midiMaster.MidiOutStatusChanged -= this.midiMaster_MidiOutStatusChanged;
            this.midiMaster.MidiReceived -= this.midiMaster_MidiReceived;
            this.midiMaster.Save();
            this.midiMaster.CloseMidiInDevice();
            this.midiMaster.CloseMidiOutDevice();
        }

        private void loadFile(string filename) {
            if (this.midiMaster != null &&
                filename != null) {
                this.midiMaster.Load(filename);
            }
        }

        public void ShowForm() {
            if (this.midiMaster != null) {
                this.midiMaster.ShowForm();
                this.on_EventListChanged(this, new EventListArgs(this.midiMaster.EventList.Keys.ToArray()));
            }
        }

        public void UpdateEventList() {
            if (this.midiMaster != null) {
                this.on_EventListChanged(this, new EventListArgs(this.midiMaster.EventList.Keys.ToArray()));
            }
        }

        public void ToggleOutStatus() {
            if (this.midiMaster != null) {
                this.midiMaster.EnabledOut = !this.midiMaster.EnabledOut;
                this.checkStates();
            }
        }

        private void checkStates() {
            if (this.midiMaster != null) {
                int inOpened = this.midiMaster.CheckMidiInOpened();
                bool inStatusChanged = this.in_IsOpen != inOpened >= 0;
                inStatusChanged = inStatusChanged || this.in_IsEnabled != this.midiMaster.EnabledIn;
                this.in_IsOpen = inOpened >= 0;
                this.in_IsEnabled = this.midiMaster.EnabledIn;
                if (inStatusChanged) this.on_InStatusChanged(this, new StatusChangedArgs(this.in_IsOpen, this.in_IsEnabled));

                int outOpened = this.midiMaster.CheckMidiOutOpened();
                bool outStatusChanged = this.out_IsOpen != outOpened >= 0;
                outStatusChanged = outStatusChanged || this.out_IsEnabled != this.midiMaster.EnabledOut;
                this.out_IsOpen = outOpened >= 0;
                this.out_IsEnabled = this.midiMaster.EnabledOut;
                if (outStatusChanged) this.on_OutStatusChanged(this, new StatusChangedArgs(this.out_IsOpen, this.out_IsEnabled));
            }
        }

        public void SendEvent(string eventName) {
            this.midiMaster.SendEvent(eventName);
        }

        private Dictionary<string, DateTime> lastSent = new Dictionary<string, DateTime>();
        private TimeSpan safety = TimeSpan.FromMilliseconds(250);
        public void PlaySafeMidi(string name) {
            var now = DateTime.Now;
            if (!lastSent.ContainsKey(name)) {
                SendEvent(name);
                lastSent[name] = now;
            }
            else {
                if (now - lastSent[name] < safety) {
                    return;
                }
                else {
                    SendEvent(name);
                    lastSent[name] = now;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="delay">Delay in ms</param>
        public void SendEvent(
            string eventName,
            int delay) {
            if (delay > 100) {
                DalyedPlayer delayed = new DalyedPlayer(this.syncContext, eventName, delay);
                delayed.Elapsed += this.delayed_Elapsed;
                delayed.Start();
            }
            else this.midiMaster.SendEvent(eventName);
        }

        public void tryParseFromString(string clipboard) {
            var newDictionary = new Dictionary<string, MidiNote>();

            //Name, Note, Velocity, Channel
            var lines = clipboard.Split('\n');


            foreach (var line in lines) {
                var tabs = line.Split('\t');
                try {
                    var name = tabs[0];
                    if (String.IsNullOrWhiteSpace(name))
                        continue;

                    var note = new MidiNote();
                    note.Note = (byte)Int32.Parse(tabs[1]);
                    note.Velocity = (byte)Int32.Parse(tabs[2]);
                    note.Channel = (byte)Int32.Parse(tabs[3]);
                    note.Status = true;

                    newDictionary.Add(name, note);
                }
                catch {
                    Console.WriteLine(line);
                }
            }

            midiMaster.EventList.Clear();
            foreach (var pair in newDictionary) {
                midiMaster.EventList.Add(pair.Key, pair.Value);
            }
        }

        #endregion


        #region Events.Outgoing

        public event EventHandler<EventListArgs> EventListChanged;
        private void on_EventListChanged(object sender, EventListArgs e) {
            if (this.EventListChanged != null) { this.EventListChanged(sender, e); }
        }

        public event EventHandler<StatusChangedArgs> In_StatusChanged;
        private void on_InStatusChanged(object sender, StatusChangedArgs e) {
            if (this.In_StatusChanged != null) { this.In_StatusChanged(sender, e); }
        }

        public event EventHandler<StatusChangedArgs> Out_StatusChanged;
        private void on_OutStatusChanged(object sender, StatusChangedArgs e) {
            if (this.Out_StatusChanged != null) { this.Out_StatusChanged(sender, e); }
        }

        public event EventHandler<MidiReceivedEventArgs> MidiReceived;
        private void on_MidiReceived(object sender, MidiReceivedEventArgs e) {
            if (this.MidiReceived != null) { this.MidiReceived(sender, e); }
        }

        #endregion

        #region Events.Incoming

        private void midiMaster_MidiInStatusChanged(object sender, MidiStatusChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_midiMaster_MidiInStatusChanged);
            try {
                if (this.syncContext != null) this.syncContext.Post(callback, sender);
            }
            catch { }
        }
        private void sync_midiMaster_MidiInStatusChanged(object content) {
            MidiMaster sender = content as MidiMaster;
            if (sender == this.midiMaster && this.midiMaster != null) this.checkStates();
        }

        private void midiMaster_MidiOutStatusChanged(object sender, MidiStatusChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_midiMaster_MidiOutStatusChanged);
            try {
                if (this.syncContext != null) this.syncContext.Post(callback, sender);
            }
            catch { }
        }
        private void sync_midiMaster_MidiOutStatusChanged(object content) {
            MidiMaster sender = content as MidiMaster;
            if (sender == this.midiMaster && this.midiMaster != null) this.checkStates();
        }

        private void midiMaster_MidiReceived(object sender, MidiReceivedEventArgs e) {
            this.on_MidiReceived(sender, e);
        }

        void delayed_Elapsed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_delayed_Elapsed);
            if (this.syncContext != null) this.syncContext.Post(callback, sender);
        }
        private void sync_delayed_Elapsed(object content) {
            DalyedPlayer delayed = content as DalyedPlayer;
            if (delayed is DalyedPlayer) {
                this.SendEvent(delayed.EventName);
                delayed.Elapsed -= this.delayed_Elapsed;
            }
        }

        #endregion

    }
}
