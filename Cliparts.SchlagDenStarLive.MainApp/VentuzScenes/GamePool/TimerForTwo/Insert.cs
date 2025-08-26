using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimerForTwo {

    public class Insert : _Base {

        //	[Path]=.Style
        //	[Path]=.Reset
        //	[Path]=.Position.X
        //	[Path]=.Position.Y
        //	[Path]=.SetOut
        //	[Path]=.ToOut
        //	[Path]=.SetIn
        //	[Path]=.ToIn
        //	[Path]=.Timer.Limit
        //	[Path]=.Timer.Start
        //	[Path]=.Timer.Stop
        //	[Path]=.Timer.Continue
        //	[Path]=.Timer.Reset
        //	[Path]=.Timer.GetPreciseTime
        //	[Path]=.Timer.CurrentTime
        //	[Path]=.Timer.OnCurrentTimeChanged
        //	[Path]=.Timer.OnStop
        //	[Path]=.Timer.OnGetPreciseTime
        //	[Path]=.Top.Name
        //	[Path]=.Top.Score
        //	[Path]=.Bottom.Name
        //	[Path]=.Bottom.Score
        //	[Path]=.Offset.Value
        //	[Path]=.Offset.SetOut
        //	[Path]=.Offset.ToInTop
        //	[Path]=.Offset.ToInBottom
        //	[Path]=.Finish.SetOut
        //	[Path]=.Finish.ToInTop
        //	[Path]=.Finish.ToInBottom

        public enum Styles { ThreeDots, FourDots, FiveDots, SixDots }

        #region Properties

        private const string sceneID = "project/gamepool/timerfortwo/insert";

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

        public Insert(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        public Insert(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
            this.init();
        }

        private void init() {
        }

        public void SetStyle(Styles value) { this.SetDataItemValue(".Style", value); }

        public void SetPositionX(int value) { this.SetDataItemValue(".Position.X", value); }
        public void SetPositionY(int value) { this.SetDataItemValue(".Position.Y", value); }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void SetTimerLimit(int value) { this.SetDataItemValue(".Timer.Limit", value); }
        public void StartTimer() { this.SetDataItemTrigger(".Timer.Start"); }
        public void StopTimer() { this.SetDataItemTrigger(".Timer.Stop"); }
        public void ContinueTimer() { this.SetDataItemTrigger(".Timer.Continue"); }
        public void ResetTimer() { this.SetDataItemTrigger(".Timer.Reset"); }
        public void GetPreciseTime() { this.SetDataItemTrigger(".Timer.GetPreciseTime"); }

        public void SetTopName(string value) { this.SetDataItemValue(".Top.Name", value); }
        public void SetTopScore(int value) { this.SetDataItemValue(".Top.Score", value); }

        public void SetBottomName(string value) { this.SetDataItemValue(".Bottom.Name", value); }
        public void SetBottomScore(int value) { this.SetDataItemValue(".Bottom.Score", value); }

        public void SetOffsetValue(float value) { this.SetDataItemValue(".Offset.Value", value); }
        public void SetOffsetOut() { this.SetDataItemTrigger(".Offset.SetOut"); }
        public void OffsetToInTop() { this.SetDataItemTrigger(".Offset.ToInTop"); }
        public void OffsetToInBottom() { this.SetDataItemTrigger(".Offset.ToInBottom"); }

        public void SetFinishOut() { this.SetDataItemTrigger(".Finish.SetOut"); }
        public void FinishToInTop() { this.SetDataItemTrigger(".Finish.ToInTop"); }
        public void FinishToInBottom() { this.SetDataItemTrigger(".Finish.ToInBottom"); }


        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Timer.OnCurrentTimeChanged") this.CurrentTime = Convert.ToSingle(e.Value);
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
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_ValueChanged(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Timer.OnCurrentTimeChanged") this.CurrentTime = Convert.ToSingle(e.Value);
                else if (e.Path == ".Timer.CurrentTime") {
                    this.CurrentTime = Convert.ToSingle(e.Value);
                }
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

        public event EventHandler StopFired;
        private void on_StopFired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.StopFired, e); }

        public event EventHandler PreciseTimeReceived;
        private void on_PreciseTimeReceived(object sender, EventArgs e) { Helper.raiseEvent(sender, this.PreciseTimeReceived, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }

}
