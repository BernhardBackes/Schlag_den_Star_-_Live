using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.CounterPointerScore {

    public class Insert : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/counterpointerscore/insert";

        public _Modules.ScorePointer ScorePointer;
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
            this.ScorePointer = new _Modules.ScorePointer(syncContext, this.addPort("ScorePointerLayer"));
            this.Score = new _Modules.Score(syncContext, this.addPort("ScoreLayer"));
        }


        public override void Dispose() {
            base.Dispose();
            this.ScorePointer.Dispose();
            this.Score.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.ScorePointer.Clear();
            this.Score.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
