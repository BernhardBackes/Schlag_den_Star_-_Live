using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.ListRefersToImageTimerScore {

    public class Insert : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/listreferstoimagetimerscore/insert";

        public _Modules.Timer Timer;
        public _Modules.Score Score;
        public _Modules.Border Border;
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
            this.Timer = new _Modules.Timer(syncContext, this.addPort("TimerLayer"));
            this.Score = new _Modules.Score(syncContext, this.addPort("ScoreLayer"));
            this.Border = new _Modules.Border(syncContext, this.addPort("BorderLayer"));
            this.TaskCounter = new _Modules.TaskCounter(syncContext, this.addPort("TaskCounterLayer"));
        }


        public override void Dispose() {
            base.Dispose();
            this.Score.Dispose();
            this.Border.Dispose();
            this.Timer.Dispose();
            this.TaskCounter.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.Score.Clear();
            this.Border.Clear();
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
