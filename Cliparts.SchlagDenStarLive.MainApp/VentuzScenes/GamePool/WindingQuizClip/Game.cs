using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.WindingQuizClip {

    public class Game : _Modules._InsertBase {


        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.Task.Filename
        //	[Path]=.Task.Reset
        //	[Path]=.Task.ToIn
        //	[Path]=.Task.Next
        //	[Path]=.Task.SetStop
        //	[Path]=.Task.SetEnd
        //	[Path]=.Solution.Filename
        //	[Path]=.Solution.SetIn
        //	[Path]=.Solution.SetOut
        //	[Path]=.Solution.ToIn

        #region Properties

        private const string sceneID = "project/gamepool/windingquizclip/game";

        #endregion


        #region Funktionen

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetTaskFilename(string value) { this.SetDataItemValue(".Task.Filename", value); }
        public void ResetTask() { this.SetDataItemTrigger(".Task.Reset"); }
        public void TaskToIn() { this.SetDataItemTrigger(".Task.ToIn"); }
        public void TaskNext() { this.SetDataItemTrigger(".Task.Next"); }
        public void SetTaskStop(int value) { this.SetDataItemTrigger(".Task.SetStop", value); }
        public void SetTaskEnd() { this.SetDataItemTrigger(".Task.SetEnd"); }

        public void SetSolutionFilename(string value) { this.SetDataItemValue(".Solution.Filename", value); }
        public void SetSolutionOut() { this.SetDataItemTrigger(".Solution.SetOut"); }
        public void SetSolutionIn() { this.SetDataItemTrigger(".Solution.SetIn"); }
        public void SolutionToIn() { this.SetDataItemTrigger(".Solution.ToIn"); }

        public override void Dispose() {
            base.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.ToOut();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
