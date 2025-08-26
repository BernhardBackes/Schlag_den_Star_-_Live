using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.InsertStillAndMovieBuzzerScore {

    //	[Path]=.Reset
    //	[Path]=.Still.SetOut
    //	[Path]=.Still.ToOut
    //	[Path]=.Still.ToIn
    //	[Path]=.Still.SetIn
    //	[Path]=.Movie.Filename
    //	[Path]=.Movie.Text
    //	[Path]=.Movie.Position.X
    //	[Path]=.Movie.Position.Y
    //	[Path]=.Movie.SetIn
    //	[Path]=.Movie.ToIn
    //	[Path]=.Movie.ToOut
    //	[Path]=.Movie.SetOut
    //	[Path]=.Movie.Resolve
    //	[Path]=.Movie.ToLastFrame
    //	[Path]=.Jingle.PlayResolve

    public class Insert : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/insertstillandmoviebuzzerscore/insert";

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

        public override void Dispose() {
            base.Dispose();
            this.Score.Dispose();
            this.Timeout.Dispose();
        }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        public void SetStillOut() { this.SetDataItemTrigger(".Still.SetOut"); }
        public void StillToOut() { this.SetDataItemTrigger(".Still.ToOut"); }
        public void SetStillIn() { this.SetDataItemTrigger(".Still.SetIn"); }
        public void StillToIn() { this.SetDataItemTrigger(".Still.ToIn"); }

        public void SetMoviePositionX(int value) { this.SetDataItemValue(".Movie.Position.X", value); }
        public void SetMoviePositionY(int value) { this.SetDataItemValue(".Movie.Position.Y", value); }
        public void SetMovieFilename(string value) { this.SetDataItemValue(".Movie.Filename", value); }
        public void SetMovieText(string value) { this.SetDataItemValue(".Movie.Text", value); }
        public void SetMovieOut() { this.SetDataItemTrigger(".Movie.SetOut"); }
        public void MovieToOut() { this.SetDataItemTrigger(".Movie.ToOut"); }
        public void SetMovieIn() { this.SetDataItemTrigger(".Movie.SetIn"); }
        public void MovieToIn() { this.SetDataItemTrigger(".Movie.ToIn"); }
        public void ResolveMovie() { this.SetDataItemTrigger(".Movie.Resolve"); }
        public void MovieToLastFrame() { this.SetDataItemTrigger(".Movie.ToLastFrame"); }

        public void PlayJingleResolve() { this.SetDataItemTrigger(".Jingle.PlayResolve"); }

        public override void Clear() {
            base.Clear();
            this.StillToOut();
            this.MovieToOut();
            this.Score.Clear();
            this.Timeout.Stop();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
