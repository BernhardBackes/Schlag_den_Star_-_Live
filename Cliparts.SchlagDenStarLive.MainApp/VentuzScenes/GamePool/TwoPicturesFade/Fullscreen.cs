using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TwoPicturesFade {

    //	[Path]=.Picture.Filename
    //	[Path]=.Picture.Fader.Reset
    //	[Path]=.Picture.Fader.SetOut
    //	[Path]=.Picture.Fader.ToOut
    //	[Path]=.Picture.Fader.SetIn
    //	[Path]=.Picture.Fader.ToIn
    //	[Path]=.Solution.Filename
    //	[Path]=.Solution.Fader.Reset
    //	[Path]=.Solution.Fader.Show

    public class Fullscreen : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/twopicturesfade/fullscreen";

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

        public void SetPictureFilename(string value) { this.SetDataItemValue(".Picture.Filename", value); }

        public void SetSolutionFilename(string value) { this.SetDataItemValue(".Solution.Filename", value); }

        public void SetOut() { this.SetDataItemTrigger(".Picture.Fader.SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".Picture.Fader.ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".Picture.Fader.SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".Picture.Fader.ToIn"); }

        public void ResetSolution() { this.SetDataItemTrigger(".Solution.Fader.Reset"); }
        public void SolutionToIn() { this.SetDataItemTrigger(".Solution.Fader.Show"); }

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
