using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TargetCounter {

    public class Insert : _Base {


        #region Properties

        private const string sceneID = "project/gamepool/targetcounter/insert";

        public _Modules.TargetCounter TargetCounter;

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
            this.TargetCounter = new _Modules.TargetCounter(syncContext, this.addPort("TargetCounter"));
        }


        public override void Dispose() {
            base.Dispose();
            this.TargetCounter.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.TargetCounter.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
