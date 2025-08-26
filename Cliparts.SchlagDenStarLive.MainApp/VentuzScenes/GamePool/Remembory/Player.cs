using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.Remembory {

    public class Player : _Base {

        //	[Path]=.Images.SetOut
        //	[Path]=.Images.ToOut
        //	[Path]=.Images.SetIn
        //	[Path]=.Images.ToIn
        //	[Path]=.Images.Count
        //	[Path]=.Images.Filename1
        //	[Path]=.Images.Filename2
        //	[Path]=.Images.Filename3
        //	[Path]=.Images.Filename4
        //	[Path]=.Images.Filename5
        //	[Path]=.Images.Filename6
        //	[Path]=.Images.Filename7
        //	[Path]=.Images.Filename8

        #region Properties

        private const string sceneID = "project/gamepool/remembory/player";

        public _Modules.Timer Timer;

        #endregion


        #region Funktionen

        public Player(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        public Player(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
            this.init();
        }

        private void init() {
            this.Timer = new _Modules.Timer(syncContext, this.addPort("TimerLayer"));
        }

        public void SetImagesCount(int value) { this.SetDataItemValue(".Images.Count", value); }
        public void SetImagesFilename(
            int id,
            string value) {
            string name = string.Format(".Images.Filename{0}", id.ToString());
            this.SetDataItemValue(name, value);
        }
        public void SetImagesFilename(
            List<string> values) {
            for (int i = 1; i <= 8; i++) {
                if (values is List<string> &&
                    values.Count >= i) this.SetImagesFilename(i, values[i - 1]);
                else this.SetImagesFilename(i, string.Empty);
            }
        }

        public void SetImagesOut() { this.SetDataItemTrigger(".Images.SetOut"); }
        public void ImagesToOut() { this.SetDataItemTrigger(".Images.ToOut"); }
        public void SetImagesIn() { this.SetDataItemTrigger(".Images.SetIn"); }
        public void ImagesToIn() { this.SetDataItemTrigger(".Images.ToIn"); }

        public override void Dispose() {
            base.Dispose();
            this.Timer.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.ImagesToOut();
            this.Timer.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
