using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.BondedDots {

    public class Insert : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/bondeddots/insert";

        public _Modules.BondedDots BondedDots;

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
            this.BondedDots = new _Modules.BondedDots(syncContext, this.addPort("BondedDotsLayer"));
        }


        public override void Dispose() {
            base.Dispose();
            this.BondedDots.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.BondedDots.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
