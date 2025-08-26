using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.Remembory {

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

    public class Fullscreen : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/remembory/fullscreen";

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

        public void SetPictureFilename(string value) { this.SetDataItemValue(".Content.PictureFilename", value); }

        public void Reset() { this.SetDataItemTrigger(".Content.Fader.Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".Content.Fader.SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".Content.Fader.ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".Content.Fader.SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".Content.Fader.ToIn"); }

        public void Deselect() { this.SetDataItemTrigger(".Content.Border.Clear"); }
        public void Select(int borderID) {
            if (borderID > 0 &&
                borderID <= 4) {
                string name = string.Format(".Content.Border.Select_{0}", borderID.ToString());
                this.SetDataItemTrigger(name);
            }
            else this.Deselect();
        }

        public override void Clear() {
            base.Clear();
            this.ToOut();
            this.Deselect();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
