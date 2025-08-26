using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.CounterScoreTimer {

    public class Insert : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/counterscoretimer/insert";

        public _Modules.CounterScore CounterScore;
        public _Modules.Timer Timer;

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
            this.CounterScore = new _Modules.CounterScore(syncContext, this.addPort("CounterScoreLayer"));
            this.Timer = new _Modules.Timer(syncContext, this.addPort("TimerLayer"));
        }


        public override void Dispose() {
            base.Dispose();
            this.CounterScore.Dispose();
            this.Timer.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.CounterScore.Clear();
            this.Timer.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
