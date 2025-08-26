using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.Penalty {

    public class Insert : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/penalty/insert";

        public _Modules.Penalty Penalty;

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
            this.Penalty = new _Modules.Penalty(syncContext, this.addPort("PenaltyLayer"));
        }


        public override void Dispose() {
            base.Dispose();
            this.Penalty.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.Penalty.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
