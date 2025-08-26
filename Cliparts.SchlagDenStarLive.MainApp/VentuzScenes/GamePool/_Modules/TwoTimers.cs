using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules {

    public class TwoTimers : GamePool._Base {


        //	[Path]=.Position.X
        //	[Path]=.Position.Y
        //	[Path]=.Scaling
        //	[Path]=.BothTimers.Start
        //	[Path]=.BothTimers.Stop
        //	[Path]=.BothTimers.Continue
        //	[Path]=.BothTimers.Reset
        //	[Path]=.LeftTimer.StartTime
        //	[Path]=.LeftTimer.StopTime
        //	[Path]=.LeftTimer.AlarmTime1
        //	[Path]=.LeftTimer.AlarmTime2
        //	[Path]=.LeftTimer.Start
        //	[Path]=.LeftTimer.Stop
        //	[Path]=.LeftTimer.Continue
        //	[Path]=.LeftTimer.Reset
        //	[Path]=.LeftTimer.Running
        //	[Path]=.LeftTimer.CurrentTime
        //	[Path]=.LeftTimer.OnRunningChanged
        //	[Path]=.LeftTimer.OnTimeChanged
        //	[Path]=.LeftTimer.OnStop
        //	[Path]=.LeftTimer.OnAlarm1
        //	[Path]=.LeftTimer.OnAlarm2
        //	[Path]=.RightTimer.StartTime
        //	[Path]=.RightTimer.StopTime
        //	[Path]=.RightTimer.AlarmTime1
        //	[Path]=.RightTimer.AlarmTime2
        //	[Path]=.RightTimer.Start
        //	[Path]=.RightTimer.Stop
        //	[Path]=.RightTimer.Continue
        //	[Path]=.RightTimer.Reset
        //	[Path]=.RightTimer.Running
        //	[Path]=.RightTimer.CurrentTime
        //	[Path]=.RightTimer.OnRunningChanged
        //	[Path]=.RightTimer.OnTimeChanged
        //	[Path]=.RightTimer.OnStop
        //	[Path]=.RightTimer.OnAlarm1
        //	[Path]=.RightTimer.OnAlarm2

        #region Properties     
   
        private const string sceneID = "project/gamepool/_modules/twotimers";

        private int? leftStartTime = null;
        public int LeftStartTime {
            get {
                if (this.leftStartTime.HasValue) return this.leftStartTime.Value;
                else return 0;
            }
            private set {
                if (!this.leftStartTime.HasValue ||
                    this.leftStartTime != value) {
                    this.leftStartTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? leftAlarmTime1 = null;
        public int LeftAlarmTime1 {
            get {
                if (this.leftAlarmTime1.HasValue) return this.leftAlarmTime1.Value;
                else return 0;
            }
            private set {
                if (!this.leftAlarmTime1.HasValue ||
                    this.leftAlarmTime1 != value) {
                    this.leftAlarmTime1 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? leftAlarmTime2 = null;
        public int LeftAlarmTime2 {
            get {
                if (this.leftAlarmTime2.HasValue) return this.leftAlarmTime2.Value;
                else return 0;
            }
            private set {
                if (!this.leftAlarmTime2.HasValue ||
                    this.leftAlarmTime2 != value) {
                    this.leftAlarmTime2 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? leftStopTime = null;
        public int LeftStopTime {
            get {
                if (this.leftStopTime.HasValue) return this.leftStopTime.Value;
                else return 0;
            }
            private set {
                if (!this.leftStopTime.HasValue ||
                    this.leftStopTime != value) {
                    this.leftStopTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? leftCurrentTime = null;
        public int LeftCurrentTime {
            get {
                if (this.leftCurrentTime.HasValue) return this.leftCurrentTime.Value;
                else return 0;
            }
            private set {
                if (!this.leftCurrentTime.HasValue ||
                    this.leftCurrentTime.Value != value) {
                    this.leftCurrentTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool? leftIsRunning = null;
        public bool LeftIsRunning {
            get {
                if (this.leftIsRunning.HasValue) return this.leftIsRunning.Value;
                else return false;
            }
            private set {
                if (!this.leftIsRunning.HasValue ||
                    this.leftIsRunning.Value != value) {
                    this.leftIsRunning = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? rightStartTime = null;
        public int RightStartTime {
            get {
                if (this.rightStartTime.HasValue) return this.rightStartTime.Value;
                else return 0;
            }
            private set {
                if (!this.rightStartTime.HasValue ||
                    this.rightStartTime != value) {
                    this.rightStartTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? rightAlarmTime1 = null;
        public int RightAlarmTime1 {
            get {
                if (this.rightAlarmTime1.HasValue) return this.rightAlarmTime1.Value;
                else return 0;
            }
            private set {
                if (!this.rightAlarmTime1.HasValue ||
                    this.rightAlarmTime1 != value) {
                    this.rightAlarmTime1 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? rightAlarmTime2 = null;
        public int RightAlarmTime2 {
            get {
                if (this.rightAlarmTime2.HasValue) return this.rightAlarmTime2.Value;
                else return 0;
            }
            private set {
                if (!this.rightAlarmTime2.HasValue ||
                    this.rightAlarmTime2 != value) {
                    this.rightAlarmTime2 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? rightStopTime = null;
        public int RightStopTime {
            get {
                if (this.rightStopTime.HasValue) return this.rightStopTime.Value;
                else return 0;
            }
            private set {
                if (!this.rightStopTime.HasValue ||
                    this.rightStopTime != value) {
                    this.rightStopTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? rightCurrentTime = null;
        public int RightCurrentTime {
            get {
                if (this.rightCurrentTime.HasValue) return this.rightCurrentTime.Value;
                else return 0;
            }
            private set {
                if (!this.rightCurrentTime.HasValue ||
                    this.rightCurrentTime.Value != value) {
                    this.rightCurrentTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool? rightIsRunning = null;
        public bool RightIsRunning {
            get {
                if (this.rightIsRunning.HasValue) return this.rightIsRunning.Value;
                else return false;
            }
            private set {
                if (!this.rightIsRunning.HasValue ||
                    this.rightIsRunning.Value != value) {
                    this.rightIsRunning = value;
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion


        #region Funktionen

        public TwoTimers(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode = Modes.Static,
            string scene = sceneID)
            : base(syncContext, port, scene, mode) {
        }

        public TwoTimers(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetPositionX(int value) { this.SetDataItemValue(".Position.X", value); }
        public void SetPositionY(int value) { this.SetDataItemValue(".Position.Y", value); }
        public void SetScaling(int value) { this.SetDataItemValue(".Scaling", value); }

        public void SetLeftStartTime(int value) { this.SetDataItemValue(".LeftTimer.StartTime", value); }
        public void SetLeftAlarmTime1(int value) { this.SetDataItemValue(".LeftTimer.AlarmTime1", value); }
        public void SetLeftAlarmTime2(int value) { this.SetDataItemValue(".LeftTimer.AlarmTime2", value); }
        public void SetLeftStopTime(int value) { this.SetDataItemValue(".LeftTimer.StopTime", value); }

        public void SetRightStartTime(int value) { this.SetDataItemValue(".RightTimer.StartTime", value); }
        public void SetRightAlarmTime1(int value) { this.SetDataItemValue(".RightTimer.AlarmTime1", value); }
        public void SetRightAlarmTime2(int value) { this.SetDataItemValue(".RightTimer.AlarmTime2", value); }
        public void SetRightStopTime(int value) { this.SetDataItemValue(".RightTimer.StopTime", value); }

        public void StartBothTimers() { this.SetDataItemTrigger(".BothTimers.Start"); }
        public void StopBothTimers() { this.SetDataItemTrigger(".BothTimers.Stop"); }
        public void ContinueBothTimers() { this.SetDataItemTrigger(".BothTimers.Continue"); }
        public void ResetBothTimers() { this.SetDataItemTrigger(".BothTimers.Reset"); }

        public void StartLeftTimer() { this.SetDataItemTrigger(".LeftTimer.Start"); }
        public void StopLeftTimer() { this.SetDataItemTrigger(".LeftTimer.Stop"); }
        public void ContinueLeftTimer() { this.SetDataItemTrigger(".LeftTimer.Continue"); }
        public void ResetLeftTimer() { this.SetDataItemTrigger(".LeftTimer.Reset"); }

        public void StartRightTimer() { this.SetDataItemTrigger(".RightTimer.Start"); }
        public void StopRightTimer() { this.SetDataItemTrigger(".RightTimer.Stop"); }
        public void ContinueRightTimer() { this.SetDataItemTrigger(".RightTimer.Continue"); }
        public void ResetRightTimer() { this.SetDataItemTrigger(".RightTimer.Reset"); }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".LeftTimer.OnTimeChanged") this.LeftCurrentTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".LeftTimer.OnRunningChanged") this.LeftIsRunning = Convert.ToBoolean(e.Value);
                else if (e.Path == ".LeftTimer.OnAlarm1") this.on_Alarm1Fired(this, new EventArgs());
                else if (e.Path == ".LeftTimer.OnAlarm2") this.on_Alarm2Fired(this, new EventArgs());
                else if (e.Path == ".LeftTimer.OnStop") this.on_StopFired(this, new EventArgs());
                else if (e.Path == ".RightTimer.OnTimeChanged") this.RightCurrentTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".RightTimer.OnRunningChanged") this.RightIsRunning = Convert.ToBoolean(e.Value);
                else if (e.Path == ".RightTimer.OnAlarm1") this.on_Alarm1Fired(this, new EventArgs());
                else if (e.Path == ".RightTimer.OnAlarm2") this.on_Alarm2Fired(this, new EventArgs());
                else if (e.Path == ".RightTimer.OnStop") this.on_StopFired(this, new EventArgs());
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_ValueChanged(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs &&
                e.Value != null) {
                if (e.Path == ".LeftTimer.StartTime") this.LeftStartTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".LeftTimer.AlarmTime1") this.LeftAlarmTime1 = Convert.ToInt32(e.Value);
                else if (e.Path == ".LeftTimer.AlarmTime2") this.LeftAlarmTime2 = Convert.ToInt32(e.Value);
                else if (e.Path == ".LeftTimer.StopTime") this.LeftStopTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".LeftTimer.CurrentTime") this.LeftCurrentTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".LeftTimer.Running") this.LeftIsRunning = Convert.ToBoolean(e.Value);
                else if (e.Path == ".RightTimer.StartTime") this.RightStartTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".RightTimer.AlarmTime1") this.RightAlarmTime1 = Convert.ToInt32(e.Value);
                else if (e.Path == ".RightTimer.AlarmTime2") this.RightAlarmTime2 = Convert.ToInt32(e.Value);
                else if (e.Path == ".RightTimer.StopTime") this.RightStopTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".RightTimer.CurrentTime") this.RightCurrentTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".RightTimer.Running") this.RightIsRunning = Convert.ToBoolean(e.Value);
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
