using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.BuzzerStopMovieLoopSolutionScore {

    //	[Path]=.Reset
    //	[Path]=.SetIn
    //	[Path]=.ToIn
    //	[Path]=.ToOut
    //	[Path]=.SetOut
    //	[Path]=.Task.Filename
    //	[Path]=.Task.Loop
    //	[Path]=.Task.Show
    //	[Path]=.Task.Start
    //	[Path]=.Task.Pause
    //	[Path]=.Task.ToLastFrame
    //	[Path]=.Solution.Filename
    //	[Path]=.Solution.Show
    //	[Path]=.Solution.Start
    //	[Path]=.Solution.ToLastFrame

    public class Fullscreen : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/buzzerstopmovieloopsolutionscore/fullscreen";

        public _Modules.TextInsert TextInsert;

        #endregion


        #region Funktionen

        public Fullscreen(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        public Fullscreen(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
            this.init();
        }

        private void init() {
            this.TextInsert = new _Modules.TextInsert(this.syncContext, this.addPort("TextInsertLayer"));
        }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void SetTaskFilename(string value) { this.SetDataItemValue(".Task.Filename", value); }
        public void SetTaskLoop(bool value) { this.SetDataItemValue(".Task.Loop", value); }
        public void ShowTask() { this.SetDataItemTrigger(".Task.Show"); }
        public void StartTask() { this.SetDataItemTrigger(".Task.Start"); }
        public void PauseTask() { this.SetDataItemTrigger(".Task.Pause"); }
        public void TaskToLastFrame() { this.SetDataItemTrigger(".Task.ToLastFrame"); }

        public void SetSolutionFilename(string value) { this.SetDataItemValue(".Solution.Filename", value); }
        public void ShowSolution() { this.SetDataItemTrigger(".Solution.Show"); }
        public void StartSolution() { this.SetDataItemTrigger(".Solution.Start"); }
        public void PauseSolution() { this.SetDataItemTrigger(".Solution.Pause"); }
        public void SolutionToLastFrame() { this.SetDataItemTrigger(".Solution.ToLastFrame"); }

        public void PlayJingleResolve() { this.SetDataItemTrigger(".Jingle.PlayResolve"); }

        public override void Clear() {
            base.Clear();
            this.ToOut();
            this.TextInsert.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
