using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.ImageDateInputScore {

    //	[Path]=.Reset
    //	[Path]=.SetOut
    //	[Path]=.ToOut
    //	[Path]=.SetIn
    //	[Path]=.ToIn
    //	[Path]=.TaskFile
    //	[Path]=.Text
    //	[Path]=.InputLeft    
    //	[Path]=.InputRight

    public class Host : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/imagedateinputscore/host";

        #endregion


        #region Funktionen

        public Host(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        public Host(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
            this.init();
        }

        private void init() {
        }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void SetTaskFilename(string value) { this.SetDataItemValue(".TaskFile", value); }
        public void SetText(string value) { this.SetDataItemValue(".Text", value); }

        public void SetInputLeft(string value) { this.SetDataItemValue(".InputLeft", value); }
        public void SetInputRight(string value) { this.SetDataItemValue(".InputRight", value); }

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
