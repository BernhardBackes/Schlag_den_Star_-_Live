using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TripleBuzzerScore {

    public class Insert : _Base {

        //	[Path]=.Jingle.PlayBuzzerLeft
        //	[Path]=.Jingle.PlayBuzzerRight

        #region Properties

        private const string sceneID = "project/gamepool/triplebuzzerscore/insert";

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
            this.Score = new _Modules.Score(syncContext, this.addPort("ScoreLayer"));
        }

        public void PlayJingleBuzzerLeft() { this.SetDataItemTrigger(".Jingle.PlayBuzzerLeft"); }
        public void PlayJingleBuzzerRight() { this.SetDataItemTrigger(".Jingle.PlayBuzzerRight"); }

        public override void Dispose() {
            base.Dispose();
            this.Score.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.Score.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
