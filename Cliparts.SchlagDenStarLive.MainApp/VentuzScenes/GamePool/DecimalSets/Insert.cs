using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.DecimalSets {

    public class Insert : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/decimalsets/insert";

        public _Modules.DecimalSets DecimalSets;

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
            this.DecimalSets = new _Modules.DecimalSets(syncContext, this.addPort("DecimalSetsLayer"));
        }


        public override void Dispose() {
            base.Dispose();
            this.DecimalSets.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.DecimalSets.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
