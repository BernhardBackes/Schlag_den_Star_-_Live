using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TouchWallTimeToBeat {

    public class Insert : _Base {

        //	[Path]=.Jingles.Play.Hit
        //	[Path]=.Jingles.Play.End

        #region Properties

        private const string sceneID = "project/gamepool/touchwalltimetobeat/insert";

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
            this.TimeToBeat = new _Modules.TimeToBeat(syncContext, this.addPort("TimeToBeatLayer"));
        }


        public void PlayJingleHit() { this.SetDataItemTrigger(".Jingles.Play.Hit"); }
        public void PlayJingleEnd() { this.SetDataItemTrigger(".Jingles.Play.End"); }

        public override void Dispose() {
            base.Dispose();
            this.TimeToBeat.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.TimeToBeat.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
