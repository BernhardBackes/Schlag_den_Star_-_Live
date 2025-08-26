using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TwoTimersStartStopContinue {

    public class Insert : _Base {

        //	[Path]=.Reset
        //	[Path]=.SetOut
        //	[Path]=.ToOut
        //	[Path]=.SetIn
        //	[Path]=.ToIn
        //	[Path]=.Position.X
        //	[Path]=.Position.Y
        //	[Path]=.Counter
        //	[Path]=.StartBothTimers
        //	[Path]=.Top.Name
        //	[Path]=.Top.Timer.StartTime
        //	[Path]=.Top.Timer.Reset
        //	[Path]=.Top.Timer.Start
        //	[Path]=.Top.Timer.Stop
        //	[Path]=.Top.Timer.CentiSeconds
        //	[Path]=.Top.Timer.CentiSecondsChanged
        //	[Path]=.Top.LAP.Time
        //	[Path]=.Top.LAP.Reset
        //	[Path]=.Top.LAP.SetOut
        //	[Path]=.Top.LAP.ToOut
        //	[Path]=.Top.LAP.SetIn
        //	[Path]=.Top.LAP.ToIn
        //	[Path]=.Bottom.Name
        //	[Path]=.Bottom.Timer.StartTime
        //	[Path]=.Bottom.Timer.Reset
        //	[Path]=.Bottom.Timer.Start
        //	[Path]=.Bottom.Timer.Stop
        //	[Path]=.Bottom.Timer.CentiSeconds
        //	[Path]=.Bottom.Timer.CentiSecondsChanged
        //	[Path]=.Bottom.LAP.Time
        //	[Path]=.Bottom.LAP.Reset
        //	[Path]=.Bottom.LAP.SetOut
        //	[Path]=.Bottom.LAP.ToOut
        //	[Path]=.Bottom.LAP.SetIn
        //	[Path]=.Bottom.LAP.ToIn

        #region Properties

        private const string sceneID = "project/gamepool/twotimersstartstopcontinue/insert";

        private float topTime = 0;
        public float TopTime {
            get { return this.topTime; }
            private set {
                if (this.topTime != value) {
                    this.topTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private float bottomTime = 0;
        public float BottomTime {
            get { return this.bottomTime; }
            private set {
                if (this.bottomTime != value) {
                    this.bottomTime = value;
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

        public void Reset() { this.SetDataItemTrigger(".Reset"); }
        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void SetPositionX(int value) { this.SetDataItemValue(".Position.X", value); }
        public void SetPositionY(int value) { this.SetDataItemValue(".Position.Y", value); }

        public void SetCounter(int value) { this.SetDataItemValue(".Counter", value); }

        public void StartBothTimers() { this.SetDataItemTrigger(".StartBothTimers"); }

        public void SetTopName(string value) { this.SetDataItemValue(".Top.Name", value); }
        public void SetTopStartTime(float value) { this.SetDataItemValue(".Top.Timer.StartTime", value); }
        public void ResetTopTimer() { this.SetDataItemTrigger(".Top.Timer.Reset"); }
        public void StartTopTimer() { this.SetDataItemTrigger(".Top.Timer.Start"); }
        public void StopTopTimer() { this.SetDataItemTrigger(".Top.Timer.Stop"); }
        public void SetTopLAPTime(float value) { this.SetDataItemValue(".Top.LAP.Time", value); }
        public void ResetTopLAPTime() { this.SetDataItemTrigger(".Top.LAP.Reset"); }
        public void SetTopLAPTimeOut() { this.SetDataItemTrigger(".Top.LAP.SetOut"); }
        public void TopLAPTimeToOut() { this.SetDataItemTrigger(".Top.LAP.ToOut"); }
        public void SetTopLAPTimeIn() { this.SetDataItemTrigger(".Top.LAP.SetIn"); }
        public void TopLAPTimeToIn() { this.SetDataItemTrigger(".Top.LAP.ToIn"); }

        public void SetBottomName(string value) { this.SetDataItemValue(".Bottom.Name", value); }
        public void SetBottomStartTime(float value) { this.SetDataItemValue(".Bottom.Timer.StartTime", value); }
        public void ResetBottomTimer() { this.SetDataItemTrigger(".Bottom.Timer.Reset"); }
        public void StartBottomTimer() { this.SetDataItemTrigger(".Bottom.Timer.Start"); }
        public void StopBottomTimer() { this.SetDataItemTrigger(".Bottom.Timer.Stop"); }
        public void SetBottomLAPTime(float value) { this.SetDataItemValue(".Bottom.LAP.Time", value); }
        public void ResetBottomLAPTime() { this.SetDataItemTrigger(".Bottom.LAP.Reset"); }
        public void SetBottomLAPTimeOut() { this.SetDataItemTrigger(".Bottom.LAP.SetOut"); }
        public void BottomLAPTimeToOut() { this.SetDataItemTrigger(".Bottom.LAP.ToOut"); }
        public void SetBottomLAPTimeIn() { this.SetDataItemTrigger(".Bottom.LAP.SetIn"); }
        public void BottomLAPTimeToIn() { this.SetDataItemTrigger(".Bottom.LAP.ToIn"); }

        public override void Dispose() {
            base.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.ToOut();
        }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs &&
                e.Value != null) {
                if (e.Path == ".Top.Timer.CentiSecondsChanged") this.TopTime = Convert.ToSingle(e.Value) / 100;
                else if (e.Path == ".Bottom.Timer.CentiSecondsChanged") this.BottomTime = Convert.ToSingle(e.Value) / 100;
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_ValueChanged(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs &&
                e.Value != null) {
                if (e.Path == ".Top.Timer.CentiSeconds") this.TopTime = Convert.ToSingle(e.Value) / 100;
                else if (e.Path == ".Bottom.Timer.CentiSeconds") this.BottomTime = Convert.ToSingle(e.Value) / 100;
            }
        }


        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
