using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.WindingQuiz {

    public class Insert : _Modules._InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.Text
        //	[Path]=.ToNewText
        //	[Path]=.Solution.Reset
        //	[Path]=.Solution.SetOut
        //	[Path]=.Solution.SetIn
        //	[Path]=.Solution.ToIn
        //	[Path]=.Solution.Text

        #region Properties

        private const string sceneID = "project/gamepool/windingquiz/insert";

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
            this.Score = new _Modules.Score(this.syncContext, this.addPort("ScoreLayer"));
            this.Timeout = new _Modules.Timeout(this.syncContext, this.addPort("TimeoutLayer"));
            this.TaskCounter = new _Modules.TaskCounter(syncContext, this.addPort("TaskCounterLayer"));
        }

        public void ShowNextText() { this.SetDataItemTrigger(".ToNewText"); }
        public void SetText(string value) { this.SetDataItemValue(".Text", value); }

        public void ResetSolution() { this.SetDataItemTrigger(".Solution.Reset"); }
        public void SetSolutionOut() { this.SetDataItemTrigger(".Solution.SetOut"); }
        public void SetSolutionIn() { this.SetDataItemTrigger(".Solution.SetIn"); }
        public void SolutionToIn() { this.SetDataItemTrigger(".Solution.ToIn"); }
        public void SetSolutionText(string value) { this.SetDataItemValue(".Solution.Text", value); }

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
