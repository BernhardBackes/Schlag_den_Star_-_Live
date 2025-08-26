using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.ListRefersToImageTimerScore {

    //	[Path]=.Content.PictureFilename
    //	[Path]=.Content.Fader.Reset
    //	[Path]=.Content.Fader.SetOut
    //	[Path]=.Content.Fader.ToOut
    //	[Path]=.Content.Fader.SetIn
    //	[Path]=.Content.Fader.ToIn

    public class Fullscreen : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/listreferstoimagetimerscore/fullscreen";

        public InsertList InsertList;

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
            this.InsertList = new InsertList(syncContext, this.addPort("InsertListLayer"));
        }

        public void SetPictureFilename(string value) { this.SetDataItemValue(".Content.PictureFilename", value); }

        public void SetCreditsText(string value) { this.SetDataItemValue(".Content.Credits.Text", value); }

        public void Reset() { this.SetDataItemTrigger(".Content.Fader.Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".Content.Fader.SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".Content.Fader.ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".Content.Fader.SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".Content.Fader.ToIn"); }

        public void ShowCredits() { this.SetDataItemTrigger(".Content.Credits.Show"); }

        public override void Clear() {
            base.Clear();
            this.ToOut();
            this.InsertList.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
