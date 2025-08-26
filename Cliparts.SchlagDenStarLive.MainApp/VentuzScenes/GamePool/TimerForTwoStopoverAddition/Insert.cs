using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimerForTwoStopoverAddition
{

    public class Insert : _Base {

        //	[Path]=.Position.X
        //	[Path]=.Position.Y
        //	[Path]=.SetOut
        //	[Path]=.Reset
        //	[Path]=.ToOut
        //	[Path]=.SetIn
        //	[Path]=.ToIn
        //	[Path]=.Timer.Start
        //	[Path]=.Timer.Stop
        //	[Path]=.Timer.Continue
        //	[Path]=.Timer.Reset
        //	[Path]=.Timer.GetPreciseTime
        //	[Path]=.Timer.CurrentTime
        //	[Path]=.Timer.OnCurrentTimeChanged
        //	[Path]=.Timer.OnStop
        //	[Path]=.Timer.OnGetPreciseTime
        //	[Path]=.Counter.Value
        //	[Path]=.Top.Name
        //	[Path]=.Top.StopTime
        //	[Path]=.Top.AdditionTime
        //	[Path]=.Bottom.Name
        //	[Path]=.Bottom.StopTime
        //	[Path]=.Bottom.AdditionTime
        //	[Path]=.PlayJingle.Moop
        //	[Path]=.Addition.ToOut
        //	[Path]=.Addition.SetIn
        //	[Path]=.Addition.ToIn

        #region Properties

        private const string sceneID = "project/gamepool/timerfortwostopoveraddition/insert";

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

        public void SetPositionX(int value) { this.SetDataItemValue(".Position.X", value); }
        public void SetPositionY(int value) { this.SetDataItemValue(".Position.Y", value); }

        public void SetCounter(int value) { this.SetDataItemValue(".Counter.Value", value); }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void StartTimer() { this.SetDataItemTrigger(".Timer.Start"); }
        public void StopTimer() { this.SetDataItemTrigger(".Timer.Stop"); }
        public void ContinueTimer() { this.SetDataItemTrigger(".Timer.Continue"); }
        public void ResetTimer() { this.SetDataItemTrigger(".Timer.Reset"); }
        public void GetPreciseTime() { this.SetDataItemTrigger(".Timer.GetPreciseTime"); }

        public void SetTopName(string value) { this.SetDataItemValue(".Top.Name", value); }
        public void SetTopStopTime(double value) { this.SetDataItemValue(".Top.StopTime", value); }
        public void SetTopAdditionTime(double value) { this.SetDataItemValue(".Top.AdditionTime", value); }

        public void SetBottomName(string value) { this.SetDataItemValue(".Bottom.Name", value); }
        public void SetBottomStopTime(double value) { this.SetDataItemValue(".Bottom.StopTime", value); }
        public void SetBottomAdditionTime(double value) { this.SetDataItemValue(".Bottom.AdditionTime", value); }

        public void AdditionToOut() { this.SetDataItemTrigger(".Addition.ToOut"); }
        public void SetAdditionIn() { this.SetDataItemTrigger(".Addition.SetIn"); }
        public void AdditionToIn() { this.SetDataItemTrigger(".Addition.ToIn"); }

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
