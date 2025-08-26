using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.CloserToValue {

    public class Insert : GamePool._Base {

        //	[Path]=.Reset
        //	[Path]=.Position.X
        //	[Path]=.Position.Y
        //	[Path]=.Players.SetOut
        //	[Path]=.Players.ToOut
        //	[Path]=.Players.SetIn
        //	[Path]=.Players.ToIn
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
        //	[Path]=.Target.Value
        //	[Path]=.Target.SetOut
        //	[Path]=.Target.ToOut
        //	[Path]=.Target.SetIn
        //	[Path]=.Target.ToIn

        #region Properties

        private const string sceneID = "project/gamepool/closertovalue/insert";

        public _Modules.Timer Timer;
        public _Modules.Score Score;

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
            this.Timer = new _Modules.Timer(syncContext, this.addPort("TimerLayer"));
            this.Score = new _Modules.Score(syncContext, this.addPort("ScoreLayer"));
        }

        public void SetPositionX(int value) { this.SetDataItemValue(".Position.X", value); }
        public void SetPositionY(int value) { this.SetDataItemValue(".Position.Y", value); }

        public void ResetPlayer() { this.SetDataItemTrigger(".Reset"); }

        public void SetPlayerOut() { this.SetDataItemTrigger(".Players.SetOut"); }
        public void PlayerToOut() { this.SetDataItemTrigger(".Players.ToOut"); }
        public void SetPlayerIn() { this.SetDataItemTrigger(".Players.SetIn"); }
        public void PlayerToIn() { this.SetDataItemTrigger(".Players.ToIn"); }

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

        public void SetTargetValue(string value) { this.SetDataItemValue(".Target.Value", value); }
        public void SetTargetOut() { this.SetDataItemTrigger(".Target.SetOut"); }
        public void TargetToOut() { this.SetDataItemTrigger(".Target.ToOut"); }
        public void SetTargetIn() { this.SetDataItemTrigger(".Target.SetIn"); }
        public void TargetToIn() { this.SetDataItemTrigger(".Target.ToIn"); }

        public void SetFlipPosition(bool value) { this.SetDataItemValue(".FlipPosition", value); }

        public override void Dispose() {
            base.Dispose();
            this.Score.Dispose();
            this.Timer.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.Score.Clear();
            this.Timer.Clear();
            this.PlayerToOut();
            this.TargetToOut();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
