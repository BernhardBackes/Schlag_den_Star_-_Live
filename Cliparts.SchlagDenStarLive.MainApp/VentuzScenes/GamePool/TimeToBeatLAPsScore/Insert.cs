using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimeToBeatLAPsScore {

    public class Insert : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/timetobeatlapsscore/insert";

        public _Modules.TimeToBeat TimeToBeat;
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
            this.TimeToBeat = new _Modules.TimeToBeat(syncContext, this.addPort("TimeToBeatLayer"));
            this.Score = new _Modules.Score(syncContext, this.addPort("ScoreLayer"));
        }

        public void PlaySound_Hit() { this.SetDataItemTrigger(".PlaySound.Hit"); }

        public override void Dispose() {
            base.Dispose();
            this.TimeToBeat.Dispose();
            this.Score.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.TimeToBeat.Clear();
            this.Score.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
