using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TeamBuzzerScore
{

    public class Insert : _Base
    {

        ///.PlayJingle.Buzzer

        public enum JingleValues { Buzzer, True }

        #region Properties

        private const string sceneID = "project/gamepool/teambuzzerscore/insert";

        public _Modules.Score Score;
        public _Modules.Timeout Timeout;
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
            this.Score = new _Modules.Score(syncContext, this.addPort("ScoreLayer"));
            this.Timeout = new _Modules.Timeout(syncContext, this.addPort("TimeoutLayer"));
            this.TaskCounter = new _Modules.TaskCounter(syncContext, this.addPort("TaskCounterLayer"));
        }

        public void PlayJingle(JingleValues value) { this.SetDataItemTrigger($".PlayJingle.{value}"); }

        public override void Dispose() {
            base.Dispose();
            this.Score.Dispose();
            this.Timeout.Dispose();
            this.TaskCounter.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.Score.Clear();
            this.Timeout.Stop();
            this.TaskCounter.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
