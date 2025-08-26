using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SingleCounterBuzzerScore {

    public class Insert : _Base {

        //	[Path]=.Counter.Reset
        //	[Path]=.Counter.SetOut
        //	[Path]=.Counter.ToOut
        //	[Path]=.Counter.SetIn
        //	[Path]=.Counter.ToIn
        /*
        [Path]= .Counter.Location (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Location (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= Left (the default value for this DataItem)
            [Elements]= Left,Right (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.Counter.PositionX
        //	[Path]=.Counter.PositionY
        //	[Path]=.Counter.Value

        #region Properties

        public enum Location { Left, Right }

        private const string sceneID = "project/gamepool/singlecounterbuzzerscore/insert";

        public _Modules.Score Score;
        public _Modules.Timeout Timeout;

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
            this.Score = new _Modules.Score(syncContext, this.addPort("ScoreLayer"));
            this.Timeout = new _Modules.Timeout(syncContext, this.addPort("TimeoutLayer"));
        }

        public override void Dispose() {
            base.Dispose();
            this.Score.Dispose();
            this.Timeout.Dispose();
        }

        public void SetCounterPositionX(int value) { this.SetDataItemValue(".Counter.PositionX", value); }
        public void SetCounterPositionY(int value) { this.SetDataItemValue(".Counter.PositionY", value); }

        public void SetCounterLocation(Location value) { this.SetDataItemValue(".Counter.Location", value); }
        public void SetCounterValue(int value) { this.SetDataItemValue(".Counter.Value", value); }

        public void ResetCounter() { this.SetDataItemTrigger(".Counter.Reset"); }

        public void SetCounterOut() { this.SetDataItemTrigger(".Counter.SetOut"); }
        public void CounterToOut() { this.SetDataItemTrigger(".Counter.ToOut"); }
        public void SetCounterIn() { this.SetDataItemTrigger(".Counter.SetIn"); }
        public void CounterToIn() { this.SetDataItemTrigger(".Counter.ToIn"); }


        public override void Clear() {
            base.Clear();
            this.CounterToOut();
            this.Score.Clear();
            this.Timeout.Stop();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
