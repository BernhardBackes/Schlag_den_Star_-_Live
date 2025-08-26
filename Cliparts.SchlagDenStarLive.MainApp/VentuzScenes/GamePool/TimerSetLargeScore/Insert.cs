using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimerSetLargeScore {

    public class Insert : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/timersetlargescore/insert";

        public _Modules.Timer Timer;
        public _Modules.SetLarge SetLarge;
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
            this.Timer = new _Modules.Timer(syncContext, this.addPort("TimerPort"));
            this.SetLarge = new _Modules.SetLarge(syncContext, this.addPort("SetLargePort"));
            this.Score = new _Modules.Score(syncContext, this.addPort("ScorePort"));
        }


        public override void Dispose() {
            base.Dispose();
            this.Timer.Dispose();
            this.SetLarge.Dispose();
            this.Score.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.Timer.Clear();
            this.SetLarge.Clear();
            this.Score.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
