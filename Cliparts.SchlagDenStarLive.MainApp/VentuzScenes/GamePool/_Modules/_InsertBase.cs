using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules {

    public class _InsertBase : GamePool._Base  {

        //	[Path]=.Reset
        //	[Path]=.SetOut
        //	[Path]=.ToOut
        //	[Path]=.SetIn
        //	[Path]=.ToIn
        //	[Path]=.Position.X
        //	[Path]=.Position.Y

        #region Properties
        #endregion


        #region Funktionen

        public _InsertBase(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            string sceneID)
            : base(syncContext, port, sceneID, Modes.Static) {
        }

        public _InsertBase(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            string sceneID,
            Modes mode = Modes.Static)
            : base(syncContext, port, sceneID, mode) {
        }

        public _InsertBase(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe,
            string sceneID)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetPositionX(int value) { this.SetDataItemValue(".Position.X", value); }
        public void SetPositionY(int value) { this.SetDataItemValue(".Position.Y", value); }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public virtual void ToIn() { this.SetDataItemTrigger(".ToIn"); }

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
