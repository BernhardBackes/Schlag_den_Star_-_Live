using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.BuzzerStartTwoTimersScore
{

    public class Insert : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/buzzerstarttwotimersscore/insert";

        public _Modules.Timer LeftTimer;
        public _Modules.Timer RightTimer;
        public _Modules.Score Score;

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
            this.LeftTimer = new _Modules.Timer(syncContext, this.addPort("LeftTimerLayer"));
            this.RightTimer = new _Modules.Timer(syncContext, this.addPort("RightTimerLayer"));
            this.Score = new _Modules.Score(syncContext, this.addPort("ScoreLayer"));
        }


        public override void Dispose() {
            base.Dispose();
            this.Score.Dispose();
            this.LeftTimer.Dispose();
            this.RightTimer.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.Score.Clear();
            this.LeftTimer.Clear();
            this.RightTimer.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
