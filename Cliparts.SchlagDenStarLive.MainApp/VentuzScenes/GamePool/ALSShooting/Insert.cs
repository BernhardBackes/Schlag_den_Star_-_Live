using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.ALSShooting {

    public class Insert : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/alsshooting/insert";

        public _Modules.Score Score;
        public _Modules.Shooting Shooting;

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
            this.Score = new _Modules.Score(syncContext, this.addPort("CounterScore"));
            this.Shooting = new _Modules.Shooting(syncContext, this.addPort("CounterShooting"));
        }


        public override void Dispose() {
            base.Dispose();
            this.Score.Dispose();
            this.Shooting.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.Score.Clear();
            this.Shooting.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
