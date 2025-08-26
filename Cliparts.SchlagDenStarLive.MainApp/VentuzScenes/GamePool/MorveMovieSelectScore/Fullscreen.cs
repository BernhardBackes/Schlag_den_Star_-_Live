using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MorveMovieSelectScore {


    //	[Path]=.MovieFilename
    //	[Path]=.StopFrame
    //	[Path]=.SolutionFrame
    //	[Path]=.Reset
    //	[Path]=.SetIn
    //	[Path]=.ToIn
    //	[Path]=.Start
    //	[Path]=.Solution
    //	[Path]=.Resolve
    //	[Path]=.LastFrame
    //	[Path]=.ToOut
    //	[Path]=.SetOut
    //	[Path]=.Jingle.PlayResolve
    //	[Path]=.Credits.Text
    //	[Path]=.Credits.Show

    public class Fullscreen : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/morvemovieselectscore/fullscreen";

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
        }

        public void SetMovieFilename(string value) { this.SetDataItemValue(".MovieFilename", value); }
        public void SetStopFrame(int value) { this.SetDataItemValue(".StopFrame", value); }
        public void SetSolutionFrame(int value) { this.SetDataItemValue(".SolutionFrame", value); }
        public void SetCreditsText(string value) { this.SetDataItemValue(".Credits.Text", value); }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void Start() { this.SetDataItemTrigger(".Start"); }

        public void Solution() { this.SetDataItemTrigger(".Solution"); }

        public void Resolve() { this.SetDataItemTrigger(".Resolve"); }

        public void LastFrame() { this.SetDataItemTrigger(".LastFrame"); }

        public void ShowCredits() { this.SetDataItemTrigger(".Credits.Show"); }

        public void PlayJingleResolve() { this.SetDataItemTrigger(".Jingle.PlayResolve"); }

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


    //	[Path]=.MovieFilename
    //	[Path]=.Reset
    //	[Path]=.SetIn
    //	[Path]=.ToIn
    //	[Path]=.Start
    //	[Path]=.ToLastFrame
    //	[Path]=.ToOut
    //	[Path]=.SetOut
    //	[Path]=.Credits.Text
    //	[Path]=.Credits.Show
    //	[Path]=.Solution.TextLeft
    //	[Path]=.Solution.TextRight
    //	[Path]=.Solution.Reset
    //	[Path]=.Solution.SetIn
    //	[Path]=.Solution.ToIn
    //	[Path]=.Border.Reset
    //	[Path]=.Border.SetLeft
    //	[Path]=.Border.ToLeft
    //	[Path]=.Border.SetRight
    //	[Path]=.Border.ToRight

    public class Fullscreen_old : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/morvemovieselectscore/fullscreen";

        #endregion


        #region Funktionen

        public Fullscreen_old(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        public Fullscreen_old(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
            this.init();
        }

        private void init() {
        }

        public void SetMovieFilename(string value) { this.SetDataItemValue(".MovieFilename", value); }
        public void SetSolutionTextLeft(string value) { this.SetDataItemValue(".Solution.TextLeft", value); }
        public void SetSolutionTextRight(string value) { this.SetDataItemValue(".Solution.TextRight", value); }
        public void SetCreditsText(string value) { this.SetDataItemValue(".Credits.Text", value); }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void Start() { this.SetDataItemTrigger(".Start"); }

        public void ToLastFrame() { this.SetDataItemTrigger(".ToLastFrame"); }

        public void ResetSolution() { this.SetDataItemTrigger(".Solution.Reset"); }
        public void SetSolutionIn() { this.SetDataItemTrigger(".Solution.SetIn"); }
        public void SolutionToIn() { this.SetDataItemTrigger(".Solution.ToIn"); }

        public void ResetBorder() { this.SetDataItemTrigger(".Border.Reset"); }
        public void SetLeftBorder() { this.SetDataItemTrigger(".Border.SetLeft"); }
        public void ToLeftBorder() { this.SetDataItemTrigger(".Border.ToLeft"); }
        public void SetRightBorder() { this.SetDataItemTrigger(".Border.SetRight"); }
        public void ToRightBorder() { this.SetDataItemTrigger(".Border.ToRight"); }

        public void ShowCredits() { this.SetDataItemTrigger(".Credits.Show"); }

        public void PlayJingleResolve() { this.SetDataItemTrigger(".Jingle.PlayResolve"); }

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
