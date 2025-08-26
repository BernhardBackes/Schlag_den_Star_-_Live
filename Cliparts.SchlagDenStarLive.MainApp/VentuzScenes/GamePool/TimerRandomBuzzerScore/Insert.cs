using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimerRandomBuzzerScore {

    public class Insert : _Base {

        //	[Path]=.Jingles.Play.Hit
        //	[Path]=.Jingles.Play.End

        #region Properties

        private const string sceneID = "project/gamepool/timerrandombuzzerscore/insert";

        public _Modules.Timer Timer;
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
            this.Timer = new _Modules.Timer(syncContext, this.addPort("TimerLayer"));
            this.Score = new _Modules.Score(syncContext, this.addPort("ScoreLayer"));
        }

        public void PlayJingleHit() { this.SetDataItemTrigger(".Jingles.Play.Hit"); }
        public void PlayJingleEnd() { this.SetDataItemTrigger(".Jingles.Play.End"); }

        public override void Dispose() {
            base.Dispose();
            this.Score.Dispose();
            this.Timer.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.Score.Clear();
            this.Timer.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
