using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TwoFlipsMovieScore {

    //	[Path]=.Content.PictureFilename
    //	[Path]=.Content.Fader.Reset
    //	[Path]=.Content.Fader.SetOut
    //	[Path]=.Content.Fader.ToOut
    //	[Path]=.Content.Fader.SetIn
    //	[Path]=.Content.Fader.ToIn
    //	[Path]=.Content.Border.Clear
    //	[Path]=.Content.Border.Select_1
    //	[Path]=.Content.Border.Select_2
    //	[Path]=.Content.Border.Select_3
    //	[Path]=.Content.Border.Select_4
    //  [Path]=.XShift
    //	[Path]=.Credits.Text
    //	[Path]=.Credits.Show


    public class Fullscreen : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/twoflipsmoviescore/fullscreen";

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
        public void SetXShift(int value) { this.SetDataItemValue(".XShift", value); }
        public void SetCreditsText(string value) { this.SetDataItemValue(".Credits.Text", value); }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void Start() { this.SetDataItemTrigger(".Start"); }
        public void Next() { this.SetDataItemTrigger(".Next"); }
        internal void SetResolvedIn() { this.SetDataItemTrigger(".SetResolvedIn"); }
        public void Resolve() { this.SetDataItemTrigger(".Resolve"); }
        public void ShowCredits() { this.SetDataItemTrigger(".Credits.Show"); }


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
