using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.RotateImageScore {

    public class Host : _Base {

        //	[Path]=.Reset
        //	[Path]=.SetOut
        //	[Path]=.ToOut
        //	[Path]=.SetIn
        //	[Path]=.ToIn
        //	[Path]=.MovieFilename
        //	[Path]=.ImageFilename
        //	[Path]=.Angle
        //	[Path]=.HostText
        //	[Path]=.Input.Right.Value
        //	[Path]=.Input.Left.Value

        #region Properties

        private const string sceneID = "project/gamepool/rotateimagescore/host";

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

        public void SetMovieFilename(string value) { this.SetDataItemValue(".MovieFilename", value); }
        public void SetImageFilename(string value) { this.SetDataItemValue(".ImageFilename", value); }
        public void SetAngle(int value) { this.SetDataItemValue(".Angle", value); }
        public void SetHostText(string value) { this.SetDataItemValue(".HostText", value); }
        public void SetLeftInput(string value) { this.SetDataItemValue(".Input.Left.Value", value); }
        public void SetRightInput(string value) { this.SetDataItemValue(".Input.Right.Value", value); }

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
