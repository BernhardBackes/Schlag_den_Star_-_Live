using System.Threading;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MathematicalFormulaScore {

    public class Insert : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/mathematicalformulascore/insert";

        public _Modules.Score Score;
        public _Modules.TaskCounter TaskCounter;
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
            this.Score = new _Modules.Score(syncContext, this.addPort("ScoreLayer"));
            this.TaskCounter = new _Modules.TaskCounter(syncContext, this.addPort("TaskCounterLayer"));
            this.Game = new Game(syncContext, this.addPort("GameLayer"));
        }

        public override void Dispose() {
            base.Dispose();
            this.Score.Dispose();
            this.TaskCounter.Dispose();
            this.Game.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.Score.Clear();
            this.TaskCounter.Clear();
            this.Game.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
