using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules {

    public class TimeToBeat : _InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        //	[Path]=.Style
        //	[Path]=.Timer.StopTime
        //	[Path]=.Timer.SentenceTime
        //	[Path]=.Timer.Start
        //	[Path]=.Timer.Stop
        //	[Path]=.Timer.Continue
        //	[Path]=.Timer.Reset
        //	[Path]=.Timer.GetPreciseTime
        //	[Path]=.Timer.LAP
        //	[Path]=.Timer.CurrentTime
        //	[Path]=.Timer.OnCurrentTimeChanged
        //	[Path]=.Timer.Running
        //	[Path]=.Timer.OnRunningChanged
        //	[Path]=.Timer.OnStop
        //	[Path]=.Timer.OnGetPreciseTime
        //	[Path]=.Timer.OnLAP
        //	[Path]=.Timer.Invalid
        //	[Path]=.TimeToBeat.Value
        //	[Path]=.TimeToBeat.Reset
        //	[Path]=.TimeToBeat.Show
        //	[Path]=.Offset.Value
        //	[Path]=.Offset.Reset
        //	[Path]=.Offset.Show
        //	[Path]=.Counter
        //	[Path]=.Name

        public enum Styles { Stopwatch, StopwatchName, StopwatchNameCounter1, StopwatchNameCounter2, StopwatchName2Dots }

        #region Properties     
   
        private const string sceneID = "project/gamepool/_modules/timetobeat";

        private Styles? style = null;
        public Styles Style {
            get {
                if (this.style.HasValue) return this.style.Value;
                else return Styles.Stopwatch;
            }
            private set {
                if (!this.style.HasValue ||
                    this.style != value) {
                    this.style = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int stopTime = 600;
        public int StopTime {
            get { return this.stopTime; }
            private set {
                if (this.stopTime != value) {
                    this.stopTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int sentenceTime = 0;
        public int SentenceTime {
            get { return this.sentenceTime; }
            private set {
                if (this.sentenceTime != value) {
                    this.sentenceTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private float? currentTime = null;
        public float CurrentTime {
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

        private float? preciseTime = null;
        public float PreciseTime {
            get {
                if (this.preciseTime.HasValue) return this.preciseTime.Value;
                else return 0;
            }
            private set {
                if (!this.preciseTime.HasValue ||
                    this.preciseTime.Value != value) {
                    this.preciseTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private float? lapTime = null;
        public float LAPTime {
            get {
                if (this.lapTime.HasValue) return this.lapTime.Value;
                else return 0;
            }
            private set {
                if (!this.lapTime.HasValue ||
                    this.lapTime.Value != value) {
                    this.lapTime = value;
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

        public TimeToBeat(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public TimeToBeat(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void ResetData() {
            this.CurrentTime = 0;
            this.PreciseTime = 0;
            this.LAPTime = 0;
        }

        public void SetStopTime(int value) { this.SetDataItemValue(".Timer.StopTime", value); }
        public void SetSentenceTime(int value) { this.SetDataItemValue(".Timer.SentenceTime", value); }
        public void SetInvalid(bool value) { this.SetDataItemValue(".Timer.Invalid", value); }

        public void SetStyle(Styles value) { this.SetDataItemValue(".Style", value); }
        public void SetName(string value) { this.SetDataItemValue(".Name", value); }
        public void SetCounter(int value) { this.SetDataItemValue(".Counter", value); }
        public void SetOffset(float value) { this.SetDataItemValue(".Offset.Value", value); }
        public void SetTimeToBeat(float value) { this.SetDataItemValue(".TimeToBeat.Value", value); }
        public void StartTimer() { this.SetDataItemTrigger(".Timer.Start"); }
        public void StopTimer() { this.SetDataItemTrigger(".Timer.Stop"); }
        public void ContinueTimer() { this.SetDataItemTrigger(".Timer.Continue"); }
        public void GetPreciseTime() { this.SetDataItemTrigger(".Timer.GetPreciseTime"); }
        public void SetLAP() { this.SetDataItemTrigger(".Timer.LAP"); }
        public void SetLAPRestart() { this.SetDataItemTrigger(".Timer.LAPRestart"); }
        public void ResetTimer() { this.SetDataItemTrigger(".Timer.Reset"); }

        public void ResetOffset() { this.SetDataItemTrigger(".Offset.Reset"); }
        public void ShowOffset() { this.SetDataItemTrigger(".Offset.Show"); }

        public void ResetTimeToBeat() { this.SetDataItemTrigger(".TimeToBeat.Reset"); }
        public void ShowTimeToBeat() { this.SetDataItemTrigger(".TimeToBeat.Show"); }

        public void StartFlashing() { this.SetDataItemTrigger(".Flashing.Start"); }
        public void StopFlashing() { this.SetDataItemTrigger(".Flashing.Stop"); }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Timer.OnCurrentTimeChanged") this.CurrentTime = Convert.ToSingle(e.Value);
                else if (e.Path == ".Timer.OnRunningChanged") this.IsRunning = Convert.ToBoolean(e.Value);
                else if (e.Path == ".Timer.OnStop") {
                    float time = Convert.ToSingle(e.Value);
                    time = time / 100;
                    this.CurrentTime = time;
                    this.on_StopFired(this, new EventArgs());
                }
                else if (e.Path == ".Timer.OnGetPreciseTime") {
                    float time = Convert.ToSingle(e.Value);
                    time = time / 100;
                    this.PreciseTime = time;
                    this.on_PreciseTimeReceived(this, new EventArgs());
                }
                else if (e.Path == ".Timer.OnLAP") {
                    float time = Convert.ToSingle(e.Value);
                    time = time / 100;
                    this.LAPTime = time;
                    this.on_LAPTimeReceived(this, new EventArgs());
                }
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_ValueChanged(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Timer.StopTime") this.StopTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".Timer.SentenceTime") this.SentenceTime = Convert.ToInt32(e.Value);
                else if (e.Path == ".Timer.CurrentTime") this.CurrentTime = Convert.ToSingle(e.Value);
                else if (e.Path == ".Timer.Running") this.IsRunning = Convert.ToBoolean(e.Value);
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

        public event EventHandler StopFired;
        private void on_StopFired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.StopFired, e); }

        public event EventHandler PreciseTimeReceived;
        private void on_PreciseTimeReceived(object sender, EventArgs e) { Helper.raiseEvent(sender, this.PreciseTimeReceived, e); }

        public event EventHandler LAPTimeReceived;
        private void on_LAPTimeReceived(object sender, EventArgs e) { Helper.raiseEvent(sender, this.LAPTimeReceived, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }
}
