using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.PenaltyIcons {

    public class Insert : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/penaltyicons/insert";

        public _Modules.PenaltyIcons PenaltyIcons;

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
            this.PenaltyIcons = new _Modules.PenaltyIcons(syncContext, this.addPort("PenaltyIconsLayer"));
        }


        public override void Dispose() {
            base.Dispose();
            this.PenaltyIcons.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.PenaltyIcons.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
