using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.PenaltyScore {

    public class Insert : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/penaltyscore/insert";

        public _Modules.PenaltyScore PenaltyScore;

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
            this.PenaltyScore = new _Modules.PenaltyScore(syncContext, this.addPort("PenaltyScoreLayer"));
        }


        public override void Dispose() {
            base.Dispose();
            this.PenaltyScore.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.PenaltyScore.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
