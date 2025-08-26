using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.WordByWordTimerScore {

    public class Insert : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/wordbywordtimerscore/insert";

        public Game Game;
        public _Modules.Score Score;
        public _Modules.Timer Timer;
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
            this.Game = new Game(syncContext, this.addPort("GameLayer"));
            this.Score = new _Modules.Score(syncContext, this.addPort("ScoreLayer"));
            this.Timer = new _Modules.Timer(syncContext, this.addPort("TimerLayer"));
            this.TaskCounter = new _Modules.TaskCounter(syncContext, this.addPort("TaskCounterLayer"));
        }

        public override void Dispose() {
            base.Dispose();
            this.Game.Dispose();
            this.Score.Dispose();
            this.Timer.Dispose();
            this.TaskCounter.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.Game.Clear();
            this.Score.Clear();
            this.Timer.Clear();
            this.TaskCounter.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
