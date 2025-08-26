using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TextInsertNumericSelectionScore {

    public class Insert : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/textinsertnumericselectionscore/insert";

        public _Modules.TextInsert TextInsert;
        public _Modules.Score Score;
        public _Modules.Counter Counter;
        public _Modules.TaskCounter TaskCounter;

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
            this.TextInsert = new _Modules.TextInsert(syncContext, this.addPort("TextInsertLayer"));
            this.Score = new _Modules.Score(syncContext, this.addPort("ScoreLayer"));
            this.Counter = new _Modules.Counter(syncContext, this.addPort("CounterLayer"));
            this.TaskCounter = new _Modules.TaskCounter(syncContext, this.addPort("TaskCounterLayer"));
        }

        public void PlayJingleInput() { this.SetDataItemTrigger(".Jingle.PlayInput"); }
        public void PlayJingleNext() { this.SetDataItemTrigger(".Jingle.PlayNext"); }
        public void PlayJingleResult() { this.SetDataItemTrigger(".Jingle.PlayResult"); }

        public override void Clear() {
            base.Clear();
            this.TextInsert.Clear();
            this.Score.Clear();
            this.Counter.Clear();
            this.TaskCounter.Clear();
        }

        public override void Dispose() {
            base.Dispose();
            this.TextInsert.Dispose();
            this.Score.Dispose();
            this.Counter.Dispose();
            this.TaskCounter.Dispose();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
