using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TeamCorrelation
{

    public class Insert : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/teamcorrelation/insert";

        public _Modules.Score Score;
        public _Modules.TaskCounter TaskCounter;
        public _Modules.Timer Timer;
        public _Modules.TextInsert TextInsert;
        public Game Game;

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
            this.TextInsert = new _Modules.TextInsert(syncContext, this.addPort("TextInsertLayer"));
            this.Score = new _Modules.Score(syncContext, this.addPort("ScoreLayer"));
            this.TaskCounter = new _Modules.TaskCounter(syncContext, this.addPort("TaskCounterLayer"));
            this.Timer = new _Modules.Timer(syncContext, this.addPort("TimerLayer"));
            this.Game = new Game(syncContext, this.addPort("GameLayer"));
        }

        public override void Dispose() {
            base.Dispose();
            this.TextInsert.Dispose();
            this.Score.Dispose();
            this.TaskCounter.Dispose();
            this.Timer.Dispose();
            this.Game.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.TextInsert.Clear();
            this.Score.Clear();
            this.TaskCounter.Clear();
            this.Timer.Clear();
            this.Game.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
