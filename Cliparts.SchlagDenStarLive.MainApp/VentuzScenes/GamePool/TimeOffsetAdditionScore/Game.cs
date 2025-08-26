using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimeOffsetAdditionScore {

    public class Game : _Modules._InsertBase {


        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.Counter.Value
        //	[Path]=.Timer.Start
        //	[Path]=.Timer.Next
        //	[Path]=.Timer.Stop
        //	[Path]=.Timer.Reset
        //	[Path]=.Timer.GetPreciseTime
        //	[Path]=.Timer.CurrentTime
        //	[Path]=.Timer.OnCurrentTimeChanged
        //	[Path]=.Timer.OnGetPreciseTime
        //	[Path]=.Timer.OnStopFired
        //	[Path]=.Offset.Value
        //	[Path]=.Offset.SetOut
        //	[Path]=.Offset.SetIn
        //	[Path]=.Offset.ToIn
        //	[Path]=.OffsetSum.Value
        //	[Path]=.ToBeat.Value
        //	[Path]=.ToBeat.SetOut
        //	[Path]=.ToBeat.ToOut
        //	[Path]=.ToBeat.SetIn
        //	[Path]=.ToBeat.ToIn
        //	[Path]=.Buzzer.Buzzer
        //	[Path]=.Buzzer.Finish

        #region Properties

        private const string sceneID = "project/gamepool/timeoffsetadditionscore/game";

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

        #endregion


        #region Funktionen

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetCounter(int value) { this.SetDataItemValue(".Counter.Value", value); }

        public void StartTimer() { this.SetDataItemTrigger(".Timer.Start"); }
        public void NextTimer() { this.SetDataItemTrigger(".Timer.Next"); }
        public void StopTimer() { this.SetDataItemTrigger(".Timer.Stop"); }
        public void ResetTimer() { this.SetDataItemTrigger(".Timer.Reset"); }
        public void GetPreciseTime() { this.SetDataItemTrigger(".Timer.GetPreciseTime"); }

        public void SetOffset(double value) { this.SetDataItemValue(".Offset.Value", value); }
        public void SetOffsetOut() { this.SetDataItemTrigger(".Offset.SetOut"); }
        public void OffsetToOut() { this.SetDataItemTrigger(".Offset.ToOut"); }
        public void SetOffsetIn() { this.SetDataItemTrigger(".Offset.SetIn"); }
        public void OffsetToIn() { this.SetDataItemTrigger(".Offset.ToIn"); }

        public void SetOffsetSum(double value) { this.SetDataItemValue(".OffsetSum.Value", value); }

        public void SetToBeat(double value) { this.SetDataItemValue(".ToBeat.Value", value); }
        public void SetToBeatOut() { this.SetDataItemTrigger(".ToBeat.SetOut"); }
        public void ToBeatToOut() { this.SetDataItemTrigger(".ToBeat.ToOut"); }
        public void SetToBeatIn() { this.SetDataItemTrigger(".ToBeat.SetIn"); }
        public void ToBeatToIn() { this.SetDataItemTrigger(".ToBeat.ToIn"); }

        public void PlayBuzzerSound() { this.SetDataItemTrigger(".Buzzer.Buzzer"); }
        public void PlayFinishSound() { this.SetDataItemTrigger(".Buzzer.Finish"); }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Timer.OnCurrentTimeChanged") this.CurrentTime = Convert.ToSingle(e.Value);
                else if (e.Path == ".Timer.OnGetPreciseTime") {
                    float time = Convert.ToSingle(e.Value);
                    time = time / 100;
                    this.PreciseTime = time;
                    this.on_PreciseTimeReceived(this, new EventArgs());
                }
                else if (e.Path == ".Timer.OnStopFired") {
                    float time = Convert.ToSingle(e.Value);
                    time = time / 100;
                    this.PreciseTime = time;
                    this.on_StopFiredReceived(this, new EventArgs());
                }
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_ValueChanged(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Timer.OnCurrentTimeChanged") this.CurrentTime = Convert.ToSingle(e.Value);
                else if (e.Path == ".Timer.CurrentTime") this.CurrentTime = Convert.ToSingle(e.Value);
            }
        }

        public override void Dispose() {
            base.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.ToOut();
        }

        #endregion


        #region Events.Outgoing

        public event EventHandler PreciseTimeReceived;
        private void on_PreciseTimeReceived(object sender, EventArgs e) { Helper.raiseEvent(sender, this.PreciseTimeReceived, e); }

        public event EventHandler StopFiredReceived;
        private void on_StopFiredReceived(object sender, EventArgs e) { Helper.raiseEvent(sender, this.StopFiredReceived, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }
}
