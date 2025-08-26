using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.AudioPictureBuzzerScore {

    public class Insert : _Base {

        //	[Path]=.Image.SetOut
        //	[Path]=.Image.ToOut
        //	[Path]=.Image.SetIn
        //	[Path]=.Image.ToIn
        //	[Path]=.Image.Position.X
        //	[Path]=.Image.Position.Y
        //	[Path]=.Image.Filename

        #region Properties

        private const string sceneID = "project/gamepool/audiopicturebuzzerscore/insert";

        public _Modules.Score Score;
        public _Modules.Timeout Timeout;
        public _Modules.TaskCounter TaskCounter;

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
            this.Score = new _Modules.Score(syncContext, this.addPort("ScoreLayer"));
            this.Timeout = new _Modules.Timeout(syncContext, this.addPort("TimeoutLayer"));
            this.TaskCounter = new _Modules.TaskCounter(syncContext, this.addPort("TaskCounterLayer"));
        }

        public override void Dispose() {
            base.Dispose();
            this.Score.Dispose();
            this.Timeout.Dispose();
            this.TaskCounter.Dispose();
        }

        public void SetImagePositionX(int value) { this.SetDataItemValue(".Image.Position.X", value); }
        public void SetImagePositionY(int value) { this.SetDataItemValue(".Image.Position.Y", value); }
        public void SetImageFilename(string value) { this.SetDataItemValue(".Image.Filename", value); }
        public void SetImageOut() { this.SetDataItemTrigger(".Image.SetOut"); }
        public void ImageToOut() { this.SetDataItemTrigger(".Image.ToOut"); }
        public void SetImageIn() { this.SetDataItemTrigger(".Image.SetIn"); }
        public void ImageToIn() { this.SetDataItemTrigger(".Image.ToIn"); }

        public override void Clear() {
            base.Clear();
            this.ImageToOut();
            this.Score.Clear();
            this.Timeout.Stop();
            this.TaskCounter.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
