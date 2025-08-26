using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimerCounterToBeatTextInsert
{

    public class Insert : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/timercountertobeattextinsert/insert";

        public _Modules.Timer Timer;
        public _Modules.CounterToBeat CounterToBeat;
        public _Modules.TextInsert TextInsert;

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
            this.Timer = new _Modules.Timer(syncContext, this.addPort("TimerLayer"));
            this.CounterToBeat = new _Modules.CounterToBeat(syncContext, this.addPort("CounterToBeatLayer"));
            this.TextInsert = new _Modules.TextInsert(syncContext, this.addPort("TextInsertLayer"));
        }

        public override void Dispose() {
            base.Dispose();
            this.TextInsert.Dispose();
            this.CounterToBeat.Dispose();
            this.Timer.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.TextInsert.Clear();
            this.CounterToBeat.Clear();
            this.Timer.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
