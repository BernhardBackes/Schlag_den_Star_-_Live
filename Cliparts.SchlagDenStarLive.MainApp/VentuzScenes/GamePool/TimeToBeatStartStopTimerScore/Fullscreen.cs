using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimeToBeatStartStopTimerScore {

    public class Fullscreen : _Base {

        //	[Path]=.IsHorizontal
        //	[Path]=.Timer.StartTime
        //	[Path]=.Timer.StopTime
        //	[Path]=.Timer.AlarmTime1
        //	[Path]=.Timer.AlarmTime2
        //	[Path]=.Timer.Start
        //	[Path]=.Timer.Stop
        //	[Path]=.Timer.Continue
        //	[Path]=.Timer.Reset
        //	[Path]=.Timer.Running
        //	[Path]=.Timer.CurrentTime
        //	[Path]=.Timer.OnRunningChanged
        //	[Path]=.Timer.OnTimeChanged
        //	[Path]=.Timer.OnStop
        //	[Path]=.Timer.OnAlarm1
        //	[Path]=.Timer.OnAlarm2
        //	[Path]=.Stopwatch.Start
        //	[Path]=.Stopwatch.Stop
        //	[Path]=.Stopwatch.Reset
        //	[Path]=.Stopwatch.OnStop
        //	[Path]=.Stopwatch.GetPreciseTime
        //	[Path]=.Stopwatch.OnGetPreciseTime
        //	[Path]=.TimeToBeat.SetOut
        //	[Path]=.TimeToBeat.ToOut
        //	[Path]=.TimeToBeat.SetIn
        //	[Path]=.TimeToBeat.ToIn
        //	[Path]=.TimeToBeat.Time


        /*
        [Path]= .TimeToBeat.ToIn (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Invoke (the display label of the DataItem)
        [Mode]= W (read/write mode of the DataItem)
        [Name]= ToIn (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Trigger (type of the current instance)
         */
        #region Properties

        private const string sceneID = "project/gamepool/timetobeatstartstoptimerscore/fullscreen";

        private int? timerStartTime = null;
        public int TimerStartTime {
            get {
                if (this.timerStartTime.HasValue) return this.timerStartTime.Value;
                else return 0;
            }
            private set {
                if (!this.timerStartTime.HasValue ||
                    this.timerStartTime != value) {
                    this.timerStartTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? timerAlarmTime1 = null;
        public int TimerAlarmTime1 {
            get {
                if (this.timerAlarmTime1.HasValue) return this.timerAlarmTime1.Value;
                else return 0;
            }
            private set {
                if (!this.timerAlarmTime1.HasValue ||
                    this.timerAlarmTime1 != value) {
                    this.timerAlarmTime1 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? timerAlarmTime2 = null;
        public int TimerAlarmTime2 {
            get {
                if (this.timerAlarmTime2.HasValue) return this.timerAlarmTime2.Value;
                else return 0;
            }
            private set {
                if (!this.timerAlarmTime2.HasValue ||
                    this.timerAlarmTime2 != value) {
                    this.timerAlarmTime2 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? timerStopTime = null;
        public int TimerStopTime {
            get {
                if (this.timerStopTime.HasValue) return this.timerStopTime.Value;
                else return 0;
            }
            private set {
                if (!this.timerStopTime.HasValue ||
                    this.timerStopTime != value) {
                    this.timerStopTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? timerCurrentTime = null;
        public int TimerCurrentTime {
            get {
                if (this.timerCurrentTime.HasValue) return this.timerCurrentTime.Value;
                else return 0;
            }
            private set {
                if (!this.timerCurrentTime.HasValue ||
                    this.timerCurrentTime.Value != value) {
                    this.timerCurrentTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool? timerIsRunning = null;
        public bool TimerIsRunning {
            get {
                if (this.timerIsRunning.HasValue) return this.timerIsRunning.Value;
                else return false;
            }
            private set {
                if (!this.timerIsRunning.HasValue ||
                    this.timerIsRunning.Value != value) {
                    this.timerIsRunning = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private float? stopwatchCurrentTime = null;
        public float StopwatchCurrentTime {
            get {
                if (this.stopwatchCurrentTime.HasValue) return this.stopwatchCurrentTime.Value;
                else return 0;
            }
            private set {
                if (!this.stopwatchCurrentTime.HasValue ||
                    this.stopwatchCurrentTime.Value != value) {
                    this.stopwatchCurrentTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool? stopwatchIsRunning = null;
        public bool StopwatchIsRunning {
            get {
                if (this.stopwatchIsRunning.HasValue) return this.stopwatchIsRunning.Value;
                else return false;
            }
            private set {
                if (!this.stopwatchIsRunning.HasValue ||
                    this.stopwatchIsRunning.Value != value) {
                    this.stopwatchIsRunning = value;
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

        public void SetBackgroundIsValid(bool value) { this.SetDataItemValue(".IsHorizontal", value); }

        public void SetStartTime(int value) { this.SetDataItemValue(".Timer.StartTime", value); }
        public void SetAlarmTime1(int value) { this.SetDataItemValue(".Timer.AlarmTime1", value); }
        public void SetAlarmTime2(int value) { this.SetDataItemValue(".Timer.AlarmTime2", value); }
        public void SetStopTime(int value) { this.SetDataItemValue(".Timer.StopTime", value); }

        public void StartTimer() { this.SetDataItemTrigger(".Timer.Start"); }
        public void StopTimer() { this.SetDataItemTrigger(".Timer.Stop"); }
        public void ContinueTimer() { this.SetDataItemTrigger(".Timer.Continue"); }
        public void ResetTimer() { this.SetDataItemTrigger(".Timer.Reset"); }

        public void StartStopwatch() { this.SetDataItemTrigger(".Stopwatch.Start"); }
        public void StopStopwatch() { this.SetDataItemTrigger(".Stopwatch.Stop"); }
        public void ResetStopwatch() { this.SetDataItemTrigger(".Stopwatch.Reset"); }
        public void GetStopwatchPreciseTime() { this.SetDataItemTrigger(".Stopwatch.GetPreciseTime"); }

        public void SetTimeToBeatTime(string value) { this.SetDataItemValue(".TimeToBeat.Time", value); }
        public void SetTimeToBeatOut() { this.SetDataItemTrigger(".TimeToBeat.SetOut"); }
        public void TimeToBeatToOut() { this.SetDataItemTrigger(".TimeToBeat.ToOut"); }
        public void SetTimeToBeatIn() { this.SetDataItemTrigger(".TimeToBeat.SetIn"); }
        public void TimeToBeatToIn() { this.SetDataItemTrigger(".TimeToBeat.ToIn"); }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Timer.OnTimeChanged") this.TimerCurrentTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".Timer.OnRunningChanged") this.TimerIsRunning = Convert.ToBoolean(e.Value);
                else if (e.Path == ".Timer.OnAlarm1") this.on_TimerAlarm1Fired(this, new EventArgs());
                else if (e.Path == ".Timer.OnAlarm2") this.on_TimerAlarm2Fired(this, new EventArgs());
                else if (e.Path == ".Timer.OnStop") this.on_TimerStopFired(this, new EventArgs());
                else if (e.Path == ".Stopwatch.OnTimeChanged") this.StopwatchCurrentTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".Stopwatch.OnRunningChanged") this.StopwatchIsRunning = Convert.ToBoolean(e.Value);
                else if (e.Path == ".Stopwatch.OnStop") {
                    float time = Convert.ToSingle(e.Value);
                    time = time / 100;
                    this.StopwatchCurrentTime = time;
                    this.on_StopwatchStopFired(this, new EventArgs());
                }
                else if (e.Path == ".Stopwatch.OnGetPreciseTime") {
                    float time = Convert.ToSingle(e.Value);
                    time = time / 100;
                    this.StopwatchCurrentTime = time;
                    this.on_StopwatchPreciseTimeReceived(this, new EventArgs());
                }
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_ValueChanged(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs &&
                e.Value != null) {
                if (e.Path == ".Timer.StartTime") this.TimerStartTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".Timer.AlarmTime1") this.TimerAlarmTime1 = Convert.ToInt32(e.Value);
                else if (e.Path == ".Timer.AlarmTime2") this.TimerAlarmTime2 = Convert.ToInt32(e.Value);
                else if (e.Path == ".Timer.StopTime") this.TimerStopTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".Timer.CurrentTime") this.TimerCurrentTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".Timer.Running") this.TimerIsRunning = Convert.ToBoolean(e.Value);
                else if (e.Path == ".Stopwatch.CurrentTime") this.StopwatchCurrentTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".Stopwatch.Running") this.StopwatchIsRunning = Convert.ToBoolean(e.Value);
            }
        }

        public override void Dispose() {
            base.Dispose();
        }

        public override void Clear() {
            base.Clear();
        }

        #endregion


        #region Events.Outgoing

        public event EventHandler TimerAlarm1Fired;
        private void on_TimerAlarm1Fired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.TimerAlarm1Fired, e); }

        public event EventHandler TimerAlarm2Fired;
        private void on_TimerAlarm2Fired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.TimerAlarm2Fired, e); }

        public event EventHandler TimerStopFired;
        private void on_TimerStopFired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.TimerStopFired, e); }

        public event EventHandler StopwatchStopFired;
        private void on_StopwatchStopFired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.StopwatchStopFired, e); }

        public event EventHandler StopwatchPreciseTimeReceived;
        private void on_StopwatchPreciseTimeReceived(object sender, EventArgs e) { Helper.raiseEvent(sender, this.StopwatchPreciseTimeReceived, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }

}
