using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SelectPictureAorBTimerScore {

    public class Host : _Base {

        //	[Path]=.Picture.Filename
        //	[Path]=.Picture.Text
        //	[Path]=.Picture.Fader.Reset
        //	[Path]=.Picture.Fader.SetOut
        //	[Path]=.Picture.Fader.ToOut
        //	[Path]=.Picture.Fader.SetIn
        //	[Path]=.Picture.Fader.ToIn
        //	[Path]=.Selection.LeftPlayer
        //	[Path]=.Selection.RightPlayer

        #region Properties

        private const string sceneID = "project/gamepool/selectpictureaorbtimerscore/host";

        #endregion


        #region Funktionen

        public Host(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        public Host(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
            this.init();
        }

        private void init() {
        }

        public void Reset() { this.SetDataItemTrigger(".Picture.Fader.Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".Picture.Fader.SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".Picture.Fader.ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".Picture.Fader.SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".Picture.Fader.ToIn"); }

        public void SetFilename(string value) { this.SetDataItemValue(".Picture.Filename", value); }
        public void SetText(string value) { this.SetDataItemValue(".Picture.Text", value); }
        public void SetLeftPlayerSelection(Fullscreen.SelectionElements value) { this.SetDataItemValue(".Selection.LeftPlayer", value); }
        public void SetRightPlayerSelection(Fullscreen.SelectionElements value) { this.SetDataItemValue(".Selection.RightPlayer", value); }

        public override void Dispose() {
            base.Dispose();
        }

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
