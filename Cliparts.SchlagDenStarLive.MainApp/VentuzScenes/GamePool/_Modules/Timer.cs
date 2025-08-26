using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules {

    public class Timer : _InsertBase {

        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        //	[Path]=.Style
        //	[Path]=.Scaling
        //	[Path]=.Timer.StartTime
        //	[Path]=.Timer.StopTime
        //	[Path]=.Timer.AlarmTime1
        //	[Path]=.Timer.AlarmTime2
        //	[Path]=.Timer.Start
        //	[Path]=.Timer.Stop
        //	[Path]=.Timer.Continue
        //	[Path]=.Timer.Reset
        //	[Path]=.Timer.CurrentTime
        //	[Path]=.Timer.Running
        //	[Path]=.Timer.OnTimeChanged
        //	[Path]=.Timer.OnRunningChanged
        //	[Path]=.Timer.OnStop
        //	[Path]=.Timer.OnAlarm1
        //	[Path]=.Timer.OnAlarm2

        public enum Styles { Sec, MinSec }

        #region Properties     
   
        private const string sceneID = "project/gamepool/_modules/timer";

        private Styles? style = null;
        public Styles Style {
            get {
                if (this.style.HasValue) return this.style.Value;
                else return Styles.MinSec;
            }
            private set {
                if (!this.style.HasValue ||
                    this.style != value) {
                    this.style = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private double? scaling = null;
        public double Scaling {
            get {
                if (this.scaling.HasValue) return this.scaling.Value;
                else return 0;
            }
            private set {
                if (!this.scaling.HasValue ||
                    this.scaling != value) {
                    this.scaling = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? startTime = null;
        public int StartTime {
            get {
                if (this.startTime.HasValue) return this.startTime.Value;
                else return 0;
            }
            private set {
                if (!this.startTime.HasValue ||
                    this.startTime != value) {
                    this.startTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? alarmTime1 = null;
        public int AlarmTime1 {
            get {
                if (this.alarmTime1.HasValue) return this.alarmTime1.Value;
                else return 0;
            }
            private set {
                if (!this.alarmTime1.HasValue ||
                    this.alarmTime1 != value) {
                    this.alarmTime1 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? alarmTime2 = null;
        public int AlarmTime2 {
            get {
                if (this.alarmTime2.HasValue) return this.alarmTime2.Value;
                else return 0;
            }
            private set {
                if (!this.alarmTime2.HasValue ||
                    this.alarmTime2 != value) {
                    this.alarmTime2 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? stopTime = null;
        public int StopTime {
            get {
                if (this.stopTime.HasValue) return this.stopTime.Value;
                else return 0;
            }
            private set {
                if (!this.stopTime.HasValue ||
                    this.stopTime != value) {
                    this.stopTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? currentTime = null;
        public int CurrentTime {
            get {
                if (this.currentTime.HasValue) return this.currentTime.Value;
                else return 0;
            }
            private set {
                if (!this.currentTime.HasValue ||
                    this.currentTime.Value != value) {
                    this.currentTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool? isRunning = null;
        public bool IsRunning {
            get {
                if (this.isRunning.HasValue) return this.isRunning.Value;
                else return false;
            }
            private set {
                if (!this.isRunning.HasValue ||
                    this.isRunning.Value != value) {
                    this.isRunning = value;
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion


        #region Funktionen

        public Timer(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public Timer(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode = Modes.Static,
            string scene = sceneID)
            : base(syncContext, port, scene, mode) {
        }

        public Timer(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetStyle(Styles value) { this.SetDataItemValue(".Style", value); }
        public void SetScaling(double value) { this.SetDataItemValue(".Scaling", value); }

        public void SetStartTime(int value) { this.SetDataItemValue(".Timer.StartTime", value); }
        public void SetAlarmTime1(int value) { this.SetDataItemValue(".Timer.AlarmTime1", value); }
        public void SetAlarmTime2(int value) { this.SetDataItemValue(".Timer.AlarmTime2", value); }
        public void SetStopTime(int value) { this.SetDataItemValue(".Timer.StopTime", value); }

        public void StartTimer() { this.SetDataItemTrigger(".Timer.Start"); }
        public void StopTimer() { this.SetDataItemTrigger(".Timer.Stop"); }
        public void ContinueTimer() { this.SetDataItemTrigger(".Timer.Continue"); }
        public void ResetTimer() { this.SetDataItemTrigger(".Timer.Reset"); }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Timer.OnTimeChanged") this.CurrentTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".Timer.OnRunningChanged") this.IsRunning = Convert.ToBoolean(e.Value);
                else if (e.Path == ".Timer.OnAlarm1") this.on_Alarm1Fired(this, new EventArgs());
                else if (e.Path == ".Timer.OnAlarm2") this.on_Alarm2Fired(this, new EventArgs());
                else if (e.Path == ".Timer.OnStop") this.on_StopFired(this, new EventArgs());
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_ValueChanged(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs &&
                e.Value != null) {
                if (e.Path == ".Timer.StartTime") this.StartTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".Timer.AlarmTime1") this.AlarmTime1 = Convert.ToInt32(e.Value);
                else if (e.Path == ".Timer.AlarmTime2") this.AlarmTime2 = Convert.ToInt32(e.Value);
                else if (e.Path == ".Timer.StopTime") this.StopTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".Timer.CurrentTime") this.CurrentTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".Timer.Running") this.IsRunning = Convert.ToBoolean(e.Value);
                else if (e.Path == ".Style") {
                    Styles style;
                    if (Enum.TryParse(e.Value.ToString(), out style)) this.Style = style;
                }
                else if (e.Path == ".Scaling") this.Scaling = Convert.ToDouble(e.Value);
            }
        }

        public override void Dispose() {
            base.Dispose();
            this.Reset();
        }

        public override void Clear() {
            base.Clear();
            this.ToOut();
        }

        #endregion


        #region Events.Outgoing

        public event EventHandler Alarm1Fired;
        private void on_Alarm1Fired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.Alarm1Fired, e); }

        public event EventHandler Alarm2Fired;
        private void on_Alarm2Fired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.Alarm2Fired, e); }

        public event EventHandler StopFired;
        private void on_StopFired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.StopFired, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }
}
