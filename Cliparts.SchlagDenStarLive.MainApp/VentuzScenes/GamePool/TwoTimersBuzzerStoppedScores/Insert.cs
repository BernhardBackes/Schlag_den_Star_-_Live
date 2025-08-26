using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TwoTimersBuzzerStoppedScores {

    ////	[Path]=.Reset
    ////	[Path]=.SetOut
    ////	[Path]=.ToOut
    ////	[Path]=.SetIn
    ////	[Path]=.ToIn
    ////	[Path]=.Position.X
    ////	[Path]=.Position.Y

    public class Insert : _Modules._InsertBase {

        #region Properties

        private const string sceneID = "project/gamepool/twotimersbuzzerstoppedscores/insert";

        public _Modules.TwoTimersScores TwoTimersScores;

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
            this.TwoTimersScores = new _Modules.TwoTimersScores(syncContext, this.addPort("TwoTimersScoresLayer"));
        }

        public override void Dispose() {
            base.Dispose();
            this.TwoTimersScores.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.TwoTimersScores.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
