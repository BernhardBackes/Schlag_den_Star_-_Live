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


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.Fullscreen {

    public class Clock : VRemote4.HandlerSi.Scene, INotifyPropertyChanged {

        //	[Path]=.Style
        //	[Path]=.StartTime
        //	[Path]=.StopTime
        //	[Path]=.AlarmTime1
        //	[Path]=.AlarmTime2
        //	[Path]=.Start
        //	[Path]=.Stop
        //	[Path]=.Continue
        //	[Path]=.Reset
        //	[Path]=.CurrentTime
        //	[Path]=.OnTimeChanged
        //	[Path]=.OnStop
        //	[Path]=.OnAlarm2
        //	[Path]=.OnAlarm1

        public enum Styles { Sec, MinSec }

        #region Properties

        private const string sceneID = "project/fullscreen/clock";

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

        #endregion


        #region Funktionen

        public Clock(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode = Modes.Static, string scene = sceneID)
            : base(syncContext, port, scene, mode) {
        }

        public Clock(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe, string scene = sceneID)
            : base(syncContext, pipe, scene) {
        }

        public void SetStyle(Styles value) { this.SetDataItemValue(".Style", value); }

        public void SetStartTime(int value) { this.SetDataItemValue(".StartTime", value); }
        public void SetAlarmTime1(int value) { this.SetDataItemValue(".AlarmTime1", value); }
        public void SetAlarmTime2(int value) { this.SetDataItemValue(".AlarmTime2", value); }
        public void SetStopTime(int value) { this.SetDataItemValue(".StopTime", value); }

        public void StartTimer() { this.SetDataItemTrigger(".Start"); }
        public void StopTimer() { this.SetDataItemTrigger(".Stop"); }
        public void ContinueTimer() { this.SetDataItemTrigger(".Continue"); }
        public void ResetTimer() { this.SetDataItemTrigger(".Reset"); }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".OnTimeChanged") this.CurrentTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".OnAlarm1") this.on_Alarm1Fired(this, new EventArgs());
                else if (e.Path == ".OnAlarm2") this.on_Alarm2Fired(this, new EventArgs());
                else if (e.Path == ".OnStop") this.on_StopFired(this, new EventArgs());
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_ValueChanged(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs &&
                e.Value != null) {
                if (e.Path == ".StartTime") this.StartTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".AlarmTime1") this.AlarmTime1 = Convert.ToInt32(e.Value);
                else if (e.Path == ".AlarmTime2") this.AlarmTime2 = Convert.ToInt32(e.Value);
                else if (e.Path == ".StopTime") this.StopTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".CurrentTime") this.CurrentTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".Style") {
                    Styles style;
                    if (Enum.TryParse(e.Value.ToString(), out style)) this.Style = style;
                }
            }
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
