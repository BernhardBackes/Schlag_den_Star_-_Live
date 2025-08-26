using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules {

    public class CounterInOutScore : _InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.FlipPosition
        //	[Path]=.Top.Name
        //	[Path]=.Top.Score
        //	[Path]=.Top.Counter.Value
        //	[Path]=.Top.Counter.ToIn
        //	[Path]=.Top.Counter.ToOut
        //	[Path]=.Bottom.Name
        //	[Path]=.Bottom.Score
        //	[Path]=.Bottom.Counter.Value
        //	[Path]=.Bottom.Counter.ToIn
        //	[Path]=.Bottom.Counter.ToOut
        #region Properties

        private const string sceneID = "project/gamepool/_modules/counterinoutscore";

        #endregion


        #region Funktionen

        public CounterInOutScore(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public CounterInOutScore(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetTopName(string value) { this.SetDataItemValue(".Top.Name", value); }
        public void SetTopScore(int value) { this.SetDataItemValue(".Top.Score", value); }
        public void SetTopCounter(int value) { this.SetDataItemValue(".Top.Counter.Value", value); }
        public void TopCounterToIn() { this.SetDataItemTrigger(".Top.Counter.ToIn"); }
        public void TopCounterToOut() { this.SetDataItemTrigger(".Top.Counter.ToOut"); }

        public void SetBottomName(string value) { this.SetDataItemValue(".Bottom.Name", value); }
        public void SetBottomScore(int value) { this.SetDataItemValue(".Bottom.Score", value); }
        public void SetBottomCounter(int value) { this.SetDataItemValue(".Bottom.Counter.Value", value); }
        public void BottomCounterToIn() { this.SetDataItemTrigger(".Bottom.Counter.ToIn"); }
        public void BottomCounterToOut() { this.SetDataItemTrigger(".Bottom.Counter.ToOut"); }

        public void SetFlipPosition(bool value) { this.SetDataItemValue(".FlipPosition", value); }

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
        #endregion

        #region Events.Incoming
        #endregion


    }
}
