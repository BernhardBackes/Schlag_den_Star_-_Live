using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules {

    public class TwoTimersScores : _InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.Top.Name
        //	[Path]=.Top.Score
        //	[Path]=.Top.Timer.Style
        //	[Path]=.Top.Timer.StartTime
        //	[Path]=.Top.Timer.StopTime
        //	[Path]=.Top.Timer.AlarmTime1
        //	[Path]=.Top.Timer.AlarmTime2
        //	[Path]=.Top.Timer.Start
        //	[Path]=.Top.Timer.Stop
        //	[Path]=.Top.Timer.Continue
        //	[Path]=.Top.Timer.Reset
        //	[Path]=.Top.Timer.Running
        //	[Path]=.Top.Timer.RunningChanged
        //	[Path]=.Top.Timer.CurrentTime
        //	[Path]=.Top.Timer.OnTimeChanged
        //	[Path]=.Top.Timer.OnAlarm1
        //	[Path]=.Top.Timer.OnAlarm2
        //	[Path]=.Top.Timer.OnStop
        //	[Path]=.Bottom.Name
        //	[Path]=.Bottom.Score
        //	[Path]=.Bottom.Timer.Style
        //	[Path]=.Bottom.Timer.StartTime
        //	[Path]=.Bottom.Timer.StopTime
        //	[Path]=.Bottom.Timer.AlarmTime1
        //	[Path]=.Bottom.Timer.AlarmTime2
        //	[Path]=.Bottom.Timer.Start
        //	[Path]=.Bottom.Timer.Stop
        //	[Path]=.Bottom.Timer.Continue
        //	[Path]=.Bottom.Timer.Reset
        //	[Path]=.Bottom.Timer.Running
        //	[Path]=.Bottom.Timer.RunningChanged
        //	[Path]=.Bottom.Timer.CurrentTime
        //	[Path]=.Bottom.Timer.OnTimeChanged
        //	[Path]=.Bottom.Timer.OnAlarm1
        //	[Path]=.Bottom.Timer.OnAlarm2
        //	[Path]=.Bottom.Timer.OnStop

        public enum Styles { Sec, MinSec }

        #region Properties

        private const string sceneID = "project/gamepool/_modules/twotimersscores";

        private Styles? topTimerStyle = null;
        public Styles TopTimerStyle {
            get {
                if (this.topTimerStyle.HasValue) return this.topTimerStyle.Value;
                else return Styles.MinSec;
            }
            private set {
                if (!this.topTimerStyle.HasValue ||
                    this.topTimerStyle != value) {
                    this.topTimerStyle = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? topTimerStartTime = null;
        public int TopTimerStartTime {
            get {
                if (this.topTimerStartTime.HasValue) return this.topTimerStartTime.Value;
                else return 0;
            }
            private set {
                if (!this.topTimerStartTime.HasValue ||
                    this.topTimerStartTime != value) {
                    this.topTimerStartTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? topTimerAlarmTime1 = null;
        public int TopTimerAlarmTime1 {
            get {
                if (this.topTimerAlarmTime1.HasValue) return this.topTimerAlarmTime1.Value;
                else return 0;
            }
            private set {
                if (!this.topTimerAlarmTime1.HasValue ||
                    this.topTimerAlarmTime1 != value) {
                    this.topTimerAlarmTime1 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? topTimerAlarmTime2 = null;
        public int TopTimerAlarmTime2 {
            get {
                if (this.topTimerAlarmTime2.HasValue) return this.topTimerAlarmTime2.Value;
                else return 0;
            }
            private set {
                if (!this.topTimerAlarmTime2.HasValue ||
                    this.topTimerAlarmTime2 != value) {
                    this.topTimerAlarmTime2 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? topTimerStopTime = null;
        public int TopTimerStopTime {
            get {
                if (this.topTimerStopTime.HasValue) return this.topTimerStopTime.Value;
                else return 0;
            }
            private set {
                if (!this.topTimerStopTime.HasValue ||
                    this.topTimerStopTime != value) {
                    this.topTimerStopTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? topTimerCurrentTime = null;
        public int TopTimerCurrentTime {
            get {
                if (this.topTimerCurrentTime.HasValue) return this.topTimerCurrentTime.Value;
                else return 0;
            }
            private set {
                if (!this.topTimerCurrentTime.HasValue ||
                    this.topTimerCurrentTime.Value != value) {
                    this.topTimerCurrentTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool? topTimerIsRunning = null;
        public bool TopTimerIsRunning {
            get {
                if (this.topTimerIsRunning.HasValue) return this.topTimerIsRunning.Value;
                else return false;
            }
            private set {
                if (!this.topTimerIsRunning.HasValue ||
                    this.topTimerIsRunning.Value != value) {
                    this.topTimerIsRunning = value;
                    this.on_PropertyChanged();
                }
            }
        }


        private Styles? bottomTimerStyle = null;
        public Styles BottomTimerStyle {
            get {
                if (this.bottomTimerStyle.HasValue) return this.bottomTimerStyle.Value;
                else return Styles.MinSec;
            }
            private set {
                if (!this.bottomTimerStyle.HasValue ||
                    this.bottomTimerStyle != value) {
                    this.bottomTimerStyle = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? bottomTimerStartTime = null;
        public int BottomTimerStartTime {
            get {
                if (this.bottomTimerStartTime.HasValue) return this.bottomTimerStartTime.Value;
                else return 0;
            }
            private set {
                if (!this.bottomTimerStartTime.HasValue ||
                    this.bottomTimerStartTime != value) {
                    this.bottomTimerStartTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? bottomTimerAlarmTime1 = null;
        public int BottomTimerAlarmTime1 {
            get {
                if (this.bottomTimerAlarmTime1.HasValue) return this.bottomTimerAlarmTime1.Value;
                else return 0;
            }
            private set {
                if (!this.bottomTimerAlarmTime1.HasValue ||
                    this.bottomTimerAlarmTime1 != value) {
                    this.bottomTimerAlarmTime1 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? bottomTimerAlarmTime2 = null;
        public int BottomTimerAlarmTime2 {
            get {
                if (this.bottomTimerAlarmTime2.HasValue) return this.bottomTimerAlarmTime2.Value;
                else return 0;
            }
            private set {
                if (!this.bottomTimerAlarmTime2.HasValue ||
                    this.bottomTimerAlarmTime2 != value) {
                    this.bottomTimerAlarmTime2 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? bottomTimerStopTime = null;
        public int BottomTimerStopTime {
            get {
                if (this.bottomTimerStopTime.HasValue) return this.bottomTimerStopTime.Value;
                else return 0;
            }
            private set {
                if (!this.bottomTimerStopTime.HasValue ||
                    this.bottomTimerStopTime != value) {
                    this.bottomTimerStopTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? bottomTimerCurrentTime = null;
        public int BottomTimerCurrentTime {
            get {
                if (this.bottomTimerCurrentTime.HasValue) return this.bottomTimerCurrentTime.Value;
                else return 0;
            }
            private set {
                if (!this.bottomTimerCurrentTime.HasValue ||
                    this.bottomTimerCurrentTime.Value != value) {
                    this.bottomTimerCurrentTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool? bottomTimerIsRunning = null;
        public bool BottomTimerIsRunning {
            get {
                if (this.bottomTimerIsRunning.HasValue) return this.bottomTimerIsRunning.Value;
                else return false;
            }
            private set {
                if (!this.bottomTimerIsRunning.HasValue ||
                    this.bottomTimerIsRunning.Value != value) {
                    this.bottomTimerIsRunning = value;
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion


        #region Funktionen

        public TwoTimersScores(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public TwoTimersScores(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode = Modes.Static,
            string scene = sceneID)
            : base(syncContext, port, scene, mode) {
        }

        public TwoTimersScores(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetTopName(string value) { this.SetDataItemValue(".Top.Name", value); }
        public void SetTopScore(int value) { this.SetDataItemValue(".Top.Score", value); }

        public void SetTopTimerStyle(Styles value) { this.SetDataItemValue(".Top.Timer.Style", value); }

        public void SetTopTimerStartTime(int value) { this.SetDataItemValue(".Top.Timer.StartTime", value); }
        public void SetTopTimerAlarmTime1(int value) { this.SetDataItemValue(".Top.Timer.AlarmTime1", value); }
        public void SetTopTimerAlarmTime2(int value) { this.SetDataItemValue(".Top.Timer.AlarmTime2", value); }
        public void SetTopTimerStopTime(int value) { this.SetDataItemValue(".Top.Timer.StopTime", value); }

        public void StartTopTimer() { this.SetDataItemTrigger(".Top.Timer.Start"); }
        public void StopTopTimer() { this.SetDataItemTrigger(".Top.Timer.Stop"); }
        public void ContinueTopTimer() { this.SetDataItemTrigger(".Top.Timer.Continue"); }
        public void ResetTopTimer() { this.SetDataItemTrigger(".Top.Timer.Reset"); }

        public void SetBottomName(string value) { this.SetDataItemValue(".Bottom.Name", value); }
        public void SetBottomScore(int value) { this.SetDataItemValue(".Bottom.Score", value); }

        public void SetBottomTimerStyle(Styles value) { this.SetDataItemValue(".Bottom.Timer.Style", value); }

        public void SetBottomTimerStartTime(int value) { this.SetDataItemValue(".Bottom.Timer.StartTime", value); }
        public void SetBottomTimerAlarmTime1(int value) { this.SetDataItemValue(".Bottom.Timer.AlarmTime1", value); }
        public void SetBottomTimerAlarmTime2(int value) { this.SetDataItemValue(".Bottom.Timer.AlarmTime2", value); }
        public void SetBottomTimerStopTime(int value) { this.SetDataItemValue(".Bottom.Timer.StopTime", value); }

        public void StartBottomTimer() { this.SetDataItemTrigger(".Bottom.Timer.Start"); }
        public void StopBottomTimer() { this.SetDataItemTrigger(".Bottom.Timer.Stop"); }
        public void ContinueBottomTimer() { this.SetDataItemTrigger(".Bottom.Timer.Continue"); }
        public void ResetBottomTimer() { this.SetDataItemTrigger(".Bottom.Timer.Reset"); }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Top.Timer.OnTimeChanged") this.TopTimerCurrentTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".Top.Timer.RunningChanged") this.TopTimerIsRunning = Convert.ToBoolean(e.Value);
                else if (e.Path == ".Top.Timer.OnAlarm1") this.on_TopTimerAlarm1Fired(this, new EventArgs());
                else if (e.Path == ".Top.Timer.OnAlarm2") this.on_TopTimerAlarm2Fired(this, new EventArgs());
                else if (e.Path == ".Top.Timer.OnStop") this.on_TopTimerStopFired(this, new EventArgs());
                else if (e.Path == ".Bottom.Timer.OnTimeChanged") this.BottomTimerCurrentTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".Bottom.Timer.RunningChanged") this.BottomTimerIsRunning = Convert.ToBoolean(e.Value);
                else if (e.Path == ".Bottom.Timer.OnAlarm1") this.on_BottomTimerAlarm1Fired(this, new EventArgs());
                else if (e.Path == ".Bottom.Timer.OnAlarm2") this.on_BottomTimerAlarm2Fired(this, new EventArgs());
                else if (e.Path == ".Bottom.Timer.OnStop") this.on_BottomTimerStopFired(this, new EventArgs());
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_ValueChanged(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs &&
                e.Value != null) {
                if (e.Path == ".Top.Timer.StartTime") this.TopTimerStartTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".Top.Timer.AlarmTime1") this.TopTimerAlarmTime1 = Convert.ToInt32(e.Value);
                else if (e.Path == ".Top.Timer.AlarmTime2") this.TopTimerAlarmTime2 = Convert.ToInt32(e.Value);
                else if (e.Path == ".Top.Timer.StopTime") this.TopTimerStopTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".Top.Timer.CurrentTime") this.TopTimerCurrentTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".Top.Timer.Running") this.TopTimerIsRunning = Convert.ToBoolean(e.Value);
                else if (e.Path == ".Top.Timer.Style") {
                    Styles style;
                    if (Enum.TryParse(e.Value.ToString(), out style)) this.TopTimerStyle = style;
                }
                else if (e.Path == ".Bottom.Timer.StartTime") this.BottomTimerStartTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".Bottom.Timer.AlarmTime1") this.BottomTimerAlarmTime1 = Convert.ToInt32(e.Value);
                else if (e.Path == ".Bottom.Timer.AlarmTime2") this.BottomTimerAlarmTime2 = Convert.ToInt32(e.Value);
                else if (e.Path == ".Bottom.Timer.StopTime") this.BottomTimerStopTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".Bottom.Timer.CurrentTime") this.BottomTimerCurrentTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".Bottom.Timer.Running") this.BottomTimerIsRunning = Convert.ToBoolean(e.Value);
                else if (e.Path == ".Bottom.Timer.Style") {
                    Styles style;
                    if (Enum.TryParse(e.Value.ToString(), out style)) this.BottomTimerStyle = style;
                }
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

        public event EventHandler TopTimerAlarm1Fired;
        private void on_TopTimerAlarm1Fired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.TopTimerAlarm1Fired, e); }

        public event EventHandler TopTimerAlarm2Fired;
        private void on_TopTimerAlarm2Fired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.TopTimerAlarm2Fired, e); }

        public event EventHandler TopTimerStopFired;
        private void on_TopTimerStopFired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.TopTimerStopFired, e); }

        public event EventHandler BottomTimerAlarm1Fired;
        private void on_BottomTimerAlarm1Fired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.BottomTimerAlarm1Fired, e); }

        public event EventHandler BottomTimerAlarm2Fired;
        private void on_BottomTimerAlarm2Fired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.BottomTimerAlarm2Fired, e); }

        public event EventHandler BottomTimerStopFired;
        private void on_BottomTimerStopFired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.BottomTimerStopFired, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }
}
