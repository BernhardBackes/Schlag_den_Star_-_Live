using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MemoCourt {

    public class Fullscreen : _Base {

        //	[Path]=.Countdown.SetOut
        //	[Path]=.Countdown.ToOut
        //	[Path]=.Countdown.SetIn
        //	[Path]=.Countdown.ToIn
        //	[Path]=.Countdown.Timer.StartTime
        //	[Path]=.Countdown.Timer.AlarmTime
        //	[Path]=.Countdown.Timer.Start
        //	[Path]=.Countdown.Timer.Stop
        //	[Path]=.Countdown.Timer.Continue
        //	[Path]=.Countdown.Timer.Reset
        //	[Path]=.Sequence.SetOut
        //	[Path]=.Sequence.ToOut
        //	[Path]=.Sequence.SetIn
        //	[Path]=.Sequence.ToIn
        //	[Path]=.Sequence.Value

        #region Properties     
   
        private const string sceneID = "project/gamepool/memocourt/fullscreen";

        private int? countdownStartTime = null;
        public int CountdownStartTime {
            get {
                if (this.countdownStartTime.HasValue) return this.countdownStartTime.Value;
                else return 0;
            }
            private set {
                if (!this.countdownStartTime.HasValue ||
                    this.countdownStartTime != value) {
                    this.countdownStartTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? countdownAlarmTime = null;
        public int CountdownAlarmTime {
            get {
                if (this.countdownAlarmTime.HasValue) return this.countdownAlarmTime.Value;
                else return 0;
            }
            private set {
                if (!this.countdownAlarmTime.HasValue ||
                    this.countdownAlarmTime != value) {
                    this.countdownAlarmTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion


        #region Funktionen

        public Fullscreen(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        public Fullscreen(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
            this.init();
        }

        private void init() {
        }

        public void SetCountdownStartTime(int value) { this.SetDataItemValue(".Countdown.Timer.StartTime", value); }
        public void SetCountdownAlarmTime(int value) { this.SetDataItemValue(".Countdown.Timer.AlarmTime", value); }

        public void SetCountdownOut() { this.SetDataItemTrigger(".Countdown.SetOut"); }
        public void CountdownToOut() { this.SetDataItemTrigger(".Countdown.ToOut"); }
        public void SetCountdownIn() { this.SetDataItemTrigger(".Countdown.SetIn"); }
        public void CountdownToIn() { this.SetDataItemTrigger(".Countdown.ToIn"); }
        public void StartCountdown() { this.SetDataItemTrigger(".Countdown.Timer.Start"); }
        public void StopCountdown() { this.SetDataItemTrigger(".Countdown.Timer.Stop"); }
        public void ContinueCountdown() { this.SetDataItemTrigger(".Countdown.Timer.Continue"); }
        public void ResetCountdown() { this.SetDataItemTrigger(".Countdown.Timer.Reset"); }

        public void SetSequenceValue(string value) { this.SetDataItemValue(".Sequence.Value", value); }

        public void SetSequenceOut() { this.SetDataItemTrigger(".Sequence.SetOut"); }
        public void SequenceToOut() { this.SetDataItemTrigger(".Sequence.ToOut"); }
        public void SetSequenceIn() { this.SetDataItemTrigger(".Sequence.SetIn"); }
        public void SequenceToIn() { this.SetDataItemTrigger(".Sequence.ToIn"); }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Countdown.Timer.OnAlarm") this.on_CountdownAlarmFired(this, new EventArgs());
                else if (e.Path == ".Countdown.Timer.OnStop") this.on_CountdownStopFired(this, new EventArgs());
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_ValueChanged(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs &&
                e.Value != null) {
                if (e.Path == ".Countdown.Timer.StartTime") this.CountdownStartTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".Countdown.Timer.AlarmTime") this.CountdownAlarmTime = Convert.ToInt32(e.Value);
            }
        }

        public override void Dispose() {
            base.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.CountdownToOut();
            this.SequenceToOut();
        }

        #endregion


        #region Events.Outgoing

        public event EventHandler CountdownAlarmFired;
        private void on_CountdownAlarmFired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.CountdownAlarmFired, e); }

        public event EventHandler CountdownStopFired;
        private void on_CountdownStopFired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.CountdownStopFired, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }
}
