using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.Remembory {

    public class Insert : _Base {

        //	[Path]=.Images.Position.X
        //	[Path]=.Images.Position.Y
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
        //	[Path]=.Jingle.PlayBuzzerLeft
        //	[Path]=.Jingle.PlayBuzzerRight
        //	[Path]=.Jingle.PlayFalse
        //	[Path]=.Jingle.PlayTrue
        //	[Path]=.Jingle.PlaySet

        #region Properties

        private const string sceneID = "project/gamepool/remembory/insert";

        public _Modules.Timer Timer;
        public _Modules.Score Score;
        public _Modules.Border Border;

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
            this.Timer = new _Modules.Timer(syncContext, this.addPort("TimerLayer"));
            this.Score = new _Modules.Score(syncContext, this.addPort("ScoreLayer"));
            this.Border = new _Modules.Border(syncContext, this.addPort("BorderLayer"));
        }

        public void SetImagesPositionX(int value) { this.SetDataItemValue(".Images.Position.X", value); }
        public void SetImagesPositionY(int value) { this.SetDataItemValue(".Images.Position.Y", value); }
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

        public void PlayJingleSet() { this.SetDataItemTrigger(".Jingle.PlaySet"); }
        public void PlayJingleTrue() { this.SetDataItemTrigger(".Jingle.PlayTrue"); }
        public void PlayJingleFalse() { this.SetDataItemTrigger(".Jingle.PlayFalse"); }
        public void PlayJingleBuzzerLeft() { this.SetDataItemTrigger(".Jingle.PlayBuzzerLeft"); }
        public void PlayJingleBuzzerRight() { this.SetDataItemTrigger(".Jingle.PlayBuzzerRight"); }

        public override void Dispose() {
            base.Dispose();
            this.Timer.Dispose();
            this.Score.Dispose();
            this.Border.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.Timer.Clear();
            this.Score.Clear();
            this.Border.Clear();
            this.ImagesToOut();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
