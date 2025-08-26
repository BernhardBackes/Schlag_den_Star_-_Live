using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules {

    public class SetLarge : _InsertBase {

        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        //	[Path]=.Top.Name
        //	[Path]=.Top.Value
        //	[Path]=.Top.Status
        //	[Path]=.Bottom.Name
        //	[Path]=.Bottom.Value
        //	[Path]=.Bottom.Status

        public enum ValueStates { Idle, Valid, Invalid }

        #region Properties     
   
        private const string sceneID = "project/gamepool/_modules/setlarge";

        #endregion


        #region Funktionen

        public SetLarge(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public SetLarge(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetTopName(string value) { this.SetDataItemValue(".Top.Name", value); }
        public void SetTopValue(string value) { this.SetDataItemValue(".Top.Value", value); }
        public void SetTopStatus(ValueStates value) { this.SetDataItemValue(".Top.Status", value); }

        public void SetBottomName(string value) { this.SetDataItemValue(".Bottom.Name", value); }
        public void SetBottomValue(string value) { this.SetDataItemValue(".Bottom.Value", value); }
        public void SetBottomStatus(ValueStates value) { this.SetDataItemValue(".Bottom.Status", value); }

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
