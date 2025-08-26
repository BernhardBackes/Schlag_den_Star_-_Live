using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules {

    public class CounterToLock : _InsertBase {

        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        //	[Path]=.LeftTop.Name
        //	[Path]=.LeftTop.Counter
        //	[Path]=.LeftTop.Locked
        //	[Path]=.RightBottom.Name
        //	[Path]=.RightBottom.Counter
        //	[Path]=.RightBottom.Locked

        #region Properties

        private const string sceneID = "project/gamepool/_modules/countertolock";

        #endregion


        #region Funktionen

        public CounterToLock(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public CounterToLock(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetLeftTopName(string value) { this.SetDataItemValue(".LeftTop.Name", value); }
        public void SetLeftTopCounter(int value) { this.SetDataItemValue(".LeftTop.Counter", value); }
        public void SetLeftTopLocked(int value) { this.SetDataItemValue(".LeftTop.Locked", value); }

        public void SetRightBottomName(string value) { this.SetDataItemValue(".RightBottom.Name", value); }
        public void SetRightBottomCounter(int value) { this.SetDataItemValue(".RightBottom.Counter", value); }
        public void SetRightBottomLocked(int value) { this.SetDataItemValue(".RightBottom.Locked", value); }

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
