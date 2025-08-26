using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules {

    public class TargetCounter : _InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.FlipPosition
        //	[Path]=.LeftTop.Name
        //	[Path]=.LeftTop.Counter
        //	[Path]=.LeftTop.Fault
        //	[Path]=.RightBottom.Name
        //	[Path]=.RightBottom.Counter
        //	[Path]=.RightBottom.Fault


        #region Properties     

        private const string sceneID = "project/gamepool/_modules/targetcounter";

        #endregion


        #region Funktionen

        public TargetCounter(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public TargetCounter(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetLeftTopName(string value) { this.SetDataItemValue(".LeftTop.Name", value); }
        public void SetLeftTopCounter(int value) { this.SetDataItemValue(".LeftTop.Counter", value); }
        public void FaultLeftTop() { this.SetDataItemTrigger(".LeftTop.Fault"); }

        public void SetRightBottomName(string value) { this.SetDataItemValue(".RightBottom.Name", value); }
        public void SetRightBottomCounter(int value) { this.SetDataItemValue(".RightBottom.Counter", value); }
        public void FaultRightBottom() { this.SetDataItemTrigger(".RightBottom.Fault"); }

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
