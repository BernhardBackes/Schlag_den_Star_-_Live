using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules {

    public class CloserToValue : _InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.Players.Left.Name
        //	[Path]=.Players.Left.Value
        //	[Path]=.Players.Left.Offset.Value
        //	[Path]=.Players.Left.Offset.IsWinner
        //	[Path]=.Players.Right.Name
        //	[Path]=.Players.Right.Value
        //	[Path]=.Players.Right.Offset.Value
        //	[Path]=.Players.Right.Offset.IsWinner
        //	[Path]=.Players.Offset.SetOut
        //	[Path]=.Players.Offset.ToOut
        //	[Path]=.Players.Offset.SetIn
        //	[Path]=.Players.Offset.ToIn

        #region Properties

        private const string sceneID = "project/gamepool/_modules/closertovalue";

        #endregion


        #region Funktionen

        public CloserToValue(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public CloserToValue(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetOffsetOut() { this.SetDataItemTrigger(".Players.Offset.SetOut"); }
        public void OffsetToOut() { this.SetDataItemTrigger(".Players.Offset.ToOut"); }
        public void SetOffsetIn() { this.SetDataItemTrigger(".Players.Offset.SetIn"); }
        public void OffsetToIn() { this.SetDataItemTrigger(".Players.Offset.ToIn"); }

        public void SetLeftName(string value) { this.SetDataItemValue(".Players.Left.Name", value); }
        public void SetLeftValue(string value) { this.SetDataItemValue(".Players.Left.Value", value); }
        public void SetLeftOffsetValue(string value) { this.SetDataItemValue(".Players.Left.Offset.Value", value); }
        public void SetLeftOffsetIsWinner(bool value) { this.SetDataItemValue(".Players.Left.Offset.IsWinner", value); }

        public void SetRightName(string value) { this.SetDataItemValue(".Players.Right.Name", value); }
        public void SetRightValue(string value) { this.SetDataItemValue(".Players.Right.Value", value); }
        public void SetRightOffsetValue(string value) { this.SetDataItemValue(".Players.Right.Offset.Value", value); }
        public void SetRightOffsetIsWinner(bool value) { this.SetDataItemValue(".Players.Right.Offset.IsWinner", value); }

        public void SetFlipPosition(bool value) { this.SetDataItemValue(".FlipPosition", value); }

        public override void Dispose() {
            base.Dispose();
        }

        public override void Clear() {
            base.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
