using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SelectAnswerScore {

    //	[Path]=.MovieFilename
    //	[Path]=.AudioFilename
    //	[Path]=.IsAudio
    //	[Path]=.Reset
    //	[Path]=.SetIn
    //	[Path]=.ToIn
    //	[Path]=.ToLastFrame
    //	[Path]=.ToOut
    //	[Path]=.SetOut
    //	[Path]=.SetSolutionOut
    //	[Path]=.SolutionToIn
    //	[Path]=.PlayAudio
    //	[Path]=.Jingle.PlayResolve
    //	[Path]=.Credits.Text
    //	[Path]=.Credits.PositionY
    //	[Path]=.Credits.Show

    public class Fullscreen : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/selectanswerscore/fullscreen";

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
        public void SetAudioFilename(string value) { this.SetDataItemValue(".AudioFilename", value); }
        public void SetIsAudio(bool value) { this.SetDataItemValue(".IsAudio", value); }
        public void SetCreditsText(string value) { this.SetDataItemValue(".Credits.Text", value); }
        public void SetCreditsPositionY(int value) { this.SetDataItemValue(".Credits.PositionY", value); }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void Start() { this.SetDataItemTrigger(".Start"); }

        public void ToLastFrame() { this.SetDataItemTrigger(".ToLastFrame"); }

        public void SetSolutionOut() { this.SetDataItemTrigger(".SetSolutionOut"); }
        public void SolutionToIn() { this.SetDataItemTrigger(".SolutionToIn"); }

        public void ShowCredits() { this.SetDataItemTrigger(".Credits.Show"); }

        public void PlayAudioFile() { this.SetDataItemTrigger(".PlayAudio"); }

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
