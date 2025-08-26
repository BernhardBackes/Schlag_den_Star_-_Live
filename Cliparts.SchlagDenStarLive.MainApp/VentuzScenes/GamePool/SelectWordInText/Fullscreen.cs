using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SelectWordInText {

    //	[Path]=.Reset
    //	[Path]=.SetOut
    //	[Path]=.ToOut
    //	[Path]=.SetIn
    //	[Path]=.ToIn
    //	[Path]=.Position.X
    //	[Path]=.Position.Y
    //	[Path]=.Cover.Filename
    //	[Path]=.Cover.SetIn
    //	[Path]=.Cover.ToIn
    //	[Path]=.Text.Value
    //	[Path]=.Text.RowTargetID
    //	[Path]=.Text.WordTargetID
    //	[Path]=.Text.SetIn
    //	[Path]=.Text.ToIn
    //	[Path]=.Text.ResetSelection
    //	[Path]=.Text.StartSelection
    //	[Path]=.Text.SetSelection

    public class Fullscreen : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/selectwordintext/fullscreen";

        public _Modules.TextInsert TextInsert;

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
            this.TextInsert = new _Modules.TextInsert(this.syncContext, this.addPort("TextInsertLayer"));
        }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void SetCoverFilename(string value) { this.SetDataItemValue(".Cover.Filename", value); }
        public void SetCoverIn() { this.SetDataItemTrigger(".Cover.SetIn"); }
        public void CoverToIn() { this.SetDataItemTrigger(".Cover.ToIn"); }

        public void SetText(string value) { this.SetDataItemValue(".Text.Value", value); }
        public void SetRowTargetID(int value) { this.SetDataItemValue(".Text.RowTargetID", value); }
        public void SetWordTargetID(int value) { this.SetDataItemValue(".Text.WordTargetID", value); }
        public void SetTextIn() { this.SetDataItemTrigger(".Text.SetIn"); }
        public void TextToIn() { this.SetDataItemTrigger(".Text.ToIn"); }
        public void ResetTextSelection() { this.SetDataItemTrigger(".Text.ResetSelection"); }
        public void StartTextSelection() { this.SetDataItemTrigger(".Text.StartSelection"); }
        public void SetTextSelection() { this.SetDataItemTrigger(".Text.SetSelection"); }

        public override void Clear() {
            base.Clear();
            this.ToOut();
            this.TextInsert.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
