using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.BuzzerStopMovieScore {

    //	[Path]=.MovieFilename
    //	[Path]=.Position.X
    //	[Path]=.Position.Y
    //	[Path]=.Reset
    //	[Path]=.SetIn
    //	[Path]=.ToIn
    //	[Path]=.Start
    //	[Path]=.ToLastFrame
    //	[Path]=.ToOut
    //	[Path]=.SetOut
    //	[Path]=.SetSolutionOut
    //	[Path]=.SolutionToIn
    //	[Path]=.Jingle.PlayResolve
    //	[Path]=.Credits.Text
    //	[Path]=.Credits.Show

    public class Fullscreen : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/buzzerstopmoviescore/fullscreen";

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
        public void SetPositionX(int value) { this.SetDataItemValue(".Position.X", value); }
        public void SetPositionY(int value) { this.SetDataItemValue(".Position.Y", value); }
        public void SetCreditsText(string value) { this.SetDataItemValue(".Credits.Text", value); }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void Start() { this.SetDataItemTrigger(".Start"); }
        public void Pause() { this.SetDataItemTrigger(".Pause"); }
        public void Continue() { this.SetDataItemTrigger(".Continue"); }
        public void ToLastFrame() { this.SetDataItemTrigger(".ToLastFrame"); }

        public void ShowCredits() { this.SetDataItemTrigger(".Credits.Show"); }

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
