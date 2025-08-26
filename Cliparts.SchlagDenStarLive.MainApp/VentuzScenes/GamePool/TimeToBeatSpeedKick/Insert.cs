using System.Threading;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimeToBeatSpeedKick {

    public class Insert : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/timetobeatspeedkick/insert";

        public _Modules.Score Score;
        public _Modules.TimeToBeat TimeToBeat;

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
            this.TimeToBeat = new _Modules.TimeToBeat(syncContext, this.addPort("TimeToBeatLayer"));
        }

        public void PlaySoundHitPanel() { this.SetDataItemTrigger(".PlaySound.Hit"); }

        public void PlaySoundFinished() { this.SetDataItemTrigger(".PlaySound.Finished"); }

        public override void Dispose() {
            base.Dispose();
            this.Score.Dispose();
            this.TimeToBeat.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.Score.Clear();
            this.TimeToBeat.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
