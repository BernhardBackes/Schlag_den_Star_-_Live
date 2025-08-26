using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.PictureInsertAndFullscreenScore {

    ////	[Path]=.Reset
    ////	[Path]=.SetOut
    ////	[Path]=.ToOut
    ////	[Path]=.SetIn
    ////	[Path]=.ToIn
    ////	[Path]=.Position.X
    ////	[Path]=.Position.Y
    //	[Path]=.Scaling
    //	[Path]=.Filename

    public class Game : _Modules._InsertBase {

        #region Properties

        private const string sceneID = "project/gamepool/pictureinsertandfullscreenscore/game";

        #endregion


        #region Funktionen

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetFilename(string value) { this.SetDataItemValue(".Filename", value); }
        public void SetScaling(decimal value) { this.SetDataItemValue(".Scaling", value); }

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
