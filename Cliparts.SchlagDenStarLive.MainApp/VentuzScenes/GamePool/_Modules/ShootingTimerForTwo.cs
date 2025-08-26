using System;
using System.Threading;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules {

    public class ShootingTimerForTwo : _InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        /*
        [Path]= .Style (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Style (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= FourHeats (the default value for this DataItem)
            [Elements]= TwoHeats,ThreeHeats,FourHeats,FiveHeats (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.Timer.Start
        //	[Path]=.Timer.Stop
        //	[Path]=.Timer.Continue
        //	[Path]=.Timer.Reset
        //	[Path]=.Timer.GetPreciseTime
        //	[Path]=.Timer.CurrentTime
        //	[Path]=.Timer.OnCurrentTimeChanged
        //	[Path]=.Timer.OnStop
        //	[Path]=.Timer.OnGetPreciseTime
        //	[Path]=.HitsCountMax
        //	[Path]=.LeftTop.Name
        //	[Path]=.LeftTop.Heats
        //	[Path]=.LeftTop.Hits.SetOut
        //	[Path]=.LeftTop.Hits.ToOut
        //	[Path]=.LeftTop.Hits.SetIn
        //	[Path]=.LeftTop.Hits.ToIn
        //	[Path]=.LeftTop.Hits.Miss
        //	[Path]=.LeftTop.Hits.Count
        //	[Path]=.RightBottom.Name
        //	[Path]=.RightBottom.Heats
        //	[Path]=.RightBottom.Hits.SetOut
        //	[Path]=.RightBottom.Hits.ToOut
        //	[Path]=.RightBottom.Hits.SetIn
        //	[Path]=.RightBottom.Hits.ToIn
        //	[Path]=.RightBottom.Hits.Miss
        //	[Path]=.RightBottom.Hits.Count
        //	[Path]=.Offset.Value
        //	[Path]=.Offset.SetOut
        //	[Path]=.Offset.ToInTop
        //	[Path]=.Offset.ToInBottom
        //	[Path]=.Finish.SetOut
        //	[Path]=.Finish.ToInTop
        //	[Path]=.Finish.ToInBottom

        public enum Styles { TwoHeats, ThreeHeats, FourHeats, FiveHeats }

        #region Properties     

        private const string sceneID = "project/gamepool/_modules/shootingtimerfortwo";

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

        public ShootingTimerForTwo(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public ShootingTimerForTwo(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetStyle(Styles value) { this.SetDataItemValue(".Style", value); }
        public void SetHitsCountMax(int value) { this.SetDataItemValue(".HitsCountMax", value); }

        public void StartTimer() { this.SetDataItemTrigger(".Timer.Start"); }
        public void StopTimer() { this.SetDataItemTrigger(".Timer.Stop"); }
        public void ContinueTimer() { this.SetDataItemTrigger(".Timer.Continue"); }
        public void ResetTimer() { this.SetDataItemTrigger(".Timer.Reset"); }
        public void GetPreciseTime() { this.SetDataItemTrigger(".Timer.GetPreciseTime"); }

        public void SetLeftTopName(string value) { this.SetDataItemValue(".LeftTop.Name", value); }
        public void SetLeftTopHeats(int value) { this.SetDataItemValue(".LeftTop.Heats", value); }
        public void SetLeftTopHits(int value) { this.SetDataItemValue(".LeftTop.Hits.Count", value); }
        public void SetLeftTopHitsOut() { this.SetDataItemTrigger(".LeftTop.Hits.SetOut"); }
        public void LeftTopHitsToOut() { this.SetDataItemTrigger(".LeftTop.Hits.ToOut"); }
        public void SetLeftTopHitsIn() { this.SetDataItemTrigger(".LeftTop.Hits.SetIn"); }
        public void LeftTopHitsToIn() { this.SetDataItemTrigger(".LeftTop.Hits.ToIn"); }
        public void LeftTopHitsMiss() { this.SetDataItemTrigger(".LeftTop.Hits.Miss"); }

        public void SetRightBottomName(string value) { this.SetDataItemValue(".RightBottom.Name", value); }
        public void SetRightBottomHeats(int value) { this.SetDataItemValue(".RightBottom.Heats", value); }
        public void SetRightBottomHits(int value) { this.SetDataItemValue(".RightBottom.Hits.Count", value); }
        public void SetRightBottomHitsOut() { this.SetDataItemTrigger(".RightBottom.Hits.SetOut"); }
        public void RightBottomHitsToOut() { this.SetDataItemTrigger(".RightBottom.Hits.ToOut"); }
        public void SetRightBottomHitsIn() { this.SetDataItemTrigger(".RightBottom.Hits.SetIn"); }
        public void RightBottomHitsToIn() { this.SetDataItemTrigger(".RightBottom.Hits.ToIn"); }
        public void RightBottomHitsMiss() { this.SetDataItemTrigger(".RightBottom.Hits.Miss"); }

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
