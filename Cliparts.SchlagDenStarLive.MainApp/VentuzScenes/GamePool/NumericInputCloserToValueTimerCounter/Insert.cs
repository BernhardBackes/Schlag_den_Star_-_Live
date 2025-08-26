using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.NumericInputCloserToValueTimerCounter {

    public class Insert : _Base {

        //	[Path]=.PlayTimeout

        #region Properties

        private const string sceneID = "project/gamepool/numericinputclosertovaluetimercounter/insert";

        public _Modules.Score Score;
        public _Modules.CloserToValue CloserToValue;
        public _Modules.TextInsert TextInsert;
        public _Modules.Timer Timer;

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
            this.Score = new _Modules.Score(syncContext, this.addPort("ScoreLayer"));
            this.CloserToValue = new _Modules.CloserToValue(syncContext, this.addPort("CloserToValueLayer"));
            this.TextInsert = new _Modules.TextInsert(syncContext, this.addPort("TextInsertLayer"));
            this.Timer = new _Modules.Timer(syncContext, this.addPort("TimerLayer"));
        }

        public void PlayTimeout() { this.SetDataItemTrigger(".PlayTimeout"); }

        public override void Dispose() {
            base.Dispose();
            this.Score.Dispose();
            this.CloserToValue.Dispose();
            this.TextInsert.Dispose();
            this.Timer.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.Score.Clear();
            this.CloserToValue.Clear();
            this.TextInsert.Clear();
            this.Timer.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
