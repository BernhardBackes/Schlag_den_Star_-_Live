using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SelectPictureAorBTimerScore {

    public class Player : _Base {

        //	[Path]=.Picture.Filename
        //	[Path]=.Picture.Fader.Reset
        //	[Path]=.Picture.Fader.SetOut
        //	[Path]=.Picture.Fader.ToOut
        //	[Path]=.Picture.Fader.SetIn
        //	[Path]=.Picture.Fader.ToIn
        //	[Path]=.Selection.Disable
        //	[Path]=.Selection.Enable
        //	[Path]=.Selection.ASelected
        //	[Path]=.Selection.BSelected

        #region Properties

        private const string sceneID = "project/gamepool/selectpictureaorbtimerscore/player";

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

        private void init() {}

        public void SetFilename(string value) { this.SetDataItemValue(".Picture.Filename", value); }


        public void Reset() { this.SetDataItemTrigger(".Picture.Fader.Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".Picture.Fader.SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".Picture.Fader.ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".Picture.Fader.SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".Picture.Fader.ToIn"); }

        public void EnableTouch() { this.SetDataItemTrigger(".Selection.Enable"); }
        public void DisableTouch() { this.SetDataItemTrigger(".Selection.Disable"); }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Selection.ASelected") this.on_ASelected(this, new EventArgs());
                else if (e.Path == ".Selection.BSelected") this.on_BSelected(this, new EventArgs());
            }
        }

        public override void Dispose() {
            base.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.ToOut();
        }

        #endregion


        #region Events.Outgoing

        public event EventHandler ASelected;
        private void on_ASelected(object sender, EventArgs e) { Helper.raiseEvent(sender, this.ASelected, e); }

        public event EventHandler BSelected;
        private void on_BSelected(object sender, EventArgs e) { Helper.raiseEvent(sender, this.BSelected, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }
}
