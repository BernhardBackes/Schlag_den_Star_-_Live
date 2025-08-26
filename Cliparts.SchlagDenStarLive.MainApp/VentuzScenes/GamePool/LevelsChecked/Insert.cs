using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.LevelsChecked {

    public class Insert : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/levelschecked/insert";

        public _Modules.LevelsChecked LevelsChecked;

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
            this.LevelsChecked = new _Modules.LevelsChecked(syncContext, this.addPort("LevelsCheckedLayer"));
        }


        public override void Dispose() {
            base.Dispose();
            this.LevelsChecked.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.LevelsChecked.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
