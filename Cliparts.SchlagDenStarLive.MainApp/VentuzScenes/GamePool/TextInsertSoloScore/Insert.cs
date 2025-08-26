using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TextInsertSoloScore {

    public class Insert : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/textinsertsoloscore/insert";

        public _Modules.TextInsert TextInsert;
        public _Modules.Score Score;
        public _Modules.Timeout Timeout;

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
            this.Timeout = new _Modules.Timeout(syncContext, this.addPort("TimeoutLayer"));
        }

        public override void Dispose() {
            base.Dispose();
            this.TextInsert.Dispose();
            this.Score.Dispose();
            this.Timeout.Dispose();
        }

        public void PlayJingleTrue() { this.SetDataItemTrigger(".Jingle.PlayTrue"); }
        public void PlayJingleFalse() { this.SetDataItemTrigger(".Jingle.PlayFalse"); }

        public override void Clear() {
            base.Clear();
            this.TextInsert.Clear();
            this.Score.Clear();
            this.Timeout.Stop();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
