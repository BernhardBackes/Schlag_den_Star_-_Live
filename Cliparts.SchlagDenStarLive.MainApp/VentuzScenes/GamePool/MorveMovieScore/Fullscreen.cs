using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MorveMovieScore {

    //	[Path]=.MovieFilename
    //	[Path]=.StopFrame
    //	[Path]=.SolutionFilename
    //	[Path]=.Reset
    //	[Path]=.SetIn
    //	[Path]=.ToIn
    //	[Path]=.Start
    //	[Path]=.ToOut
    //	[Path]=.SetOut
    //	[Path]=.SetSolutionOut
    //	[Path]=.SolutionToIn
    //	[Path]=.SetSolutionIn
    //	[Path]=.Jingle.PlayResolve
    //	[Path]=.Credits.Text
    //	[Path]=.Credits.Position.X
    //	[Path]=.Credits.Position.Y
    //	[Path]=.Credits.Rotation
    //	[Path]=.Credits.Show
    //	[Path]=.Credits.SetIn

    public class Fullscreen : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/morvemoviescore/fullscreen";

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

        public void SetMovieFilename(string value) { this.SetDataItemValue(".MovieFilename", value); }
        public void SetStopFrame(int value) { this.SetDataItemValue(".StopFrame", value); }
        public void SetSolutionFilename(string value) { this.SetDataItemValue(".SolutionFilename", value); }
        public void SetCreditsText(string value) { this.SetDataItemValue(".Credits.Text", value); }
        public void SetCreditsPositionX(int value) { this.SetDataItemValue(".Credits.Position.X", value); }
        public void SetCreditsPositionY(int value) { this.SetDataItemValue(".Credits.Position.Y", value); }
        public void SetCreditsRotation(int value) { this.SetDataItemValue(".Credits.Rotation", value); }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void Start(int value) { this.SetDataItemTrigger(".Start", value); }

        public void SetSolutionIn() { this.SetDataItemTrigger(".SetSolutionIn"); }

        public void SetSolutionOut() { this.SetDataItemTrigger(".SetSolutionOut"); }
        public void SolutionToIn() { this.SetDataItemTrigger(".SolutionToIn"); }

        public void ShowCredits() { this.SetDataItemTrigger(".Credits.Show"); }
        public void SetCreditsIn() { this.SetDataItemTrigger(".Credits.SetIn"); }

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
