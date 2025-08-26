using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.AddCounterToScore {

    public class Insert : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/addcountertoscore/insert";

        public _Modules.CounterInOutScore CounterInOutScore;

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
            this.CounterInOutScore = new _Modules.CounterInOutScore(syncContext, this.addPort("CounterInOutScoreLayer"));
        }


        public override void Dispose() {
            base.Dispose();
            this.CounterInOutScore.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.CounterInOutScore.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
