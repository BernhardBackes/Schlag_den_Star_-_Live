using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MapSlidingTimerScore {

    public class Insert : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/mapslidingtimerscore/insert";

        public _Modules.Timer Timer;
        public _Modules.TaskCounter TaskCounter;
        public _Modules.Score Score;
        public _Modules.Border Border;
        public _Modules.Timeout Timeout;
        public _Modules.TextInsert TextInsert;

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
            this.TaskCounter = new _Modules.TaskCounter(syncContext, this.addPort("TaskCounterLayer"));
            this.Score = new _Modules.Score(syncContext, this.addPort("ScoreLayer"));
            this.Border = new _Modules.Border(syncContext, this.addPort("BorderLayer"));
            this.Timeout = new _Modules.Timeout(syncContext, this.addPort("TimeoutLayer"));
            this.TextInsert = new _Modules.TextInsert(syncContext, this.addPort("TextInsertLayer"));
            
        }

        public override void Dispose() {
            base.Dispose();
            this.Timer.Dispose();
            this.TaskCounter.Dispose();
            this.Score.Dispose();
            this.Border.Dispose();
            this.Timeout.Dispose();
            this.TextInsert.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.Timer.Clear();
            this.TaskCounter.Clear();
            this.Score.Clear();
            this.Border.Clear();
            this.Timeout.Clear();
            this.TextInsert.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
