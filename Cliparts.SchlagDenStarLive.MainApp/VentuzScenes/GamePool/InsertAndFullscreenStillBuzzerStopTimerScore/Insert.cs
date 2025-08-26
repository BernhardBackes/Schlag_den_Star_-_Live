using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.InsertAndFullscreenStillBuzzerStopTimerScore {

    //	[Path]=.Reset
    //	[Path]=.SetOut
    //	[Path]=.ToOut
    //	[Path]=.ToIn
    //	[Path]=.SetIn
    //	[Path]=.Position.X
    //	[Path]=.Position.Y
    //	[Path]=.Filename

    public class Insert : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/insertandfullscreenstillbuzzerstoptimerscore/insert";

        public _Modules.Score Score;
        public _Modules.Timer Timer;
        public _Modules.Timeout Timeout;

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
            this.Timer = new _Modules.Timer(syncContext, this.addPort("TimerLayer"));
            this.Timeout = new _Modules.Timeout(syncContext, this.addPort("TimeoutLayer"));
        }

        public override void Dispose() {
            base.Dispose();
            this.Score.Dispose();
            this.Timeout.Dispose();
            this.Timer.Dispose();
        }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void SetPositionX(int value) { this.SetDataItemValue(".Position.X", value); }
        public void SetPositionY(int value) { this.SetDataItemValue(".Position.Y", value); }

        public void SetFilename(string value) { this.SetDataItemValue(".Filename", value); }

        public override void Clear() {
            base.Clear();
            this.ToOut();
            this.Score.Clear();
            this.Timeout.Stop();
            this.Timer.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
