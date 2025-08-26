using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.FullscreenPictureNumericInputAddDifference {

    public class Game : _Modules._InsertBase {


        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.Left.Input
        //	[Path]=.Left.Difference
        //	[Path]=.Right.Input
        //	[Path]=.Right.Difference
        //	[Path]=.Input.SetOut
        //	[Path]=.Input.ToOut
        //	[Path]=.Input.SetIn
        //	[Path]=.Input.ToIn
        //	[Path]=.Input.SetResolved
        //	[Path]=.Input.ToResolved

        #region Properties

        private const string sceneID = "project/gamepool/fullscreenpicturenumericinputadddifference/game";

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

        public void SetLeftPlayerInput(int value) { this.SetDataItemValue(".Left.Input", value); }
        public void SetLeftPlayerDifference(int value) { this.SetDataItemValue(".Left.Difference", value); }

        public void SetRightPlayerInput(int value) { this.SetDataItemValue(".Right.Input", value); }
        public void SetRightPlayerDifference(int value) { this.SetDataItemValue(".Right.Difference", value); }

        public void SetInputOut() { this.SetDataItemTrigger(".Input.SetOut"); }
        public void InputToOut() { this.SetDataItemTrigger(".Input.ToOut"); }
        public void SetInputIn() { this.SetDataItemTrigger(".Input.SetIn"); }
        public void InputToIn() { this.SetDataItemTrigger(".Input.ToIn"); }
        public void SetResolved() { this.SetDataItemTrigger(".Input.SetResolved"); }
        public void ResolvedToIn() { this.SetDataItemTrigger(".Input.ToResolved"); }

        public override void Dispose() {
            base.Dispose();
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
