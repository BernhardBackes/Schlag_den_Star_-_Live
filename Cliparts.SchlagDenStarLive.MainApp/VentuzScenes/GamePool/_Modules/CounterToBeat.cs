using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules {

    public class CounterToBeat : _InsertBase {

        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        //	[Path]=.Size.Value
        //	[Path]=.Counter.Value
        //	[Path]=.ToBeat.Value
        //	[Path]=.ToBeat.SetOut
        //	[Path]=.ToBeat.ToOut
        //	[Path]=.ToBeat.SetIn
        //	[Path]=.ToBeat.ToIn

        public enum SizeElements { TwoDigits, TwoDigitsLarge, ThreeDigits }

        #region Properties

        private const string sceneID = "project/gamepool/_modules/countertobeat";

        #endregion


        #region Funktionen

        public CounterToBeat(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public CounterToBeat(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetSize(SizeElements value) { this.SetDataItemValue(".Size.Value", value); }
        public void SetCounter(int value) { this.SetDataItemValue(".Counter.Value", value); }
        public void SetToBeatValue(int value) { this.SetDataItemValue(".ToBeat.Value", value); }

        public void SetToBeatOut() { this.SetDataItemTrigger(".ToBeat.SetOut"); }
        public void ToBeatToOut() { this.SetDataItemTrigger(".ToBeat.ToOut"); }
        public void SetToBeatIn() { this.SetDataItemTrigger(".ToBeat.SetIn"); }
        public void ToBeatToIn() { this.SetDataItemTrigger(".ToBeat.ToIn"); }

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
