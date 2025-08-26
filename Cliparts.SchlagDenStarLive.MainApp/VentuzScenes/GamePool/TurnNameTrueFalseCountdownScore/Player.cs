using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TurnNameTrueFalseCountdownScore {

    public class Player : _Base {

        //	[Path]=.Reset
        //	[Path]=.ToOut
        //	[Path]=.ToIn
        //	[Path]=.SetIn
        //	[Path]=.TopText
        //	[Path]=.Name
        //	[Path]=.Touch.Enable
        //	[Path]=.Touch.Disable
        //	[Path]=.Selection.Reset
        //	[Path]=.Selection.SetTrue
        //	[Path]=.Selection.SetFalse
        //	[Path]=.Selection.Index
        //	[Path]=.Selection.IndexChanged

        #region Properties

        private const string sceneID = "project/gamepool/turnnametruefalsecountdownscore/player";

        private SelectionValues selection = SelectionValues.NotAvailable;
        public SelectionValues Selection {
            get { return this.selection; }
            set {
                if (this.selection != value) {
                    this.selection = value;
                    this.on_PropertyChanged();
                }
            }
        }

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
        }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void EnableTouch() { this.SetDataItemTrigger(".Touch.Enable"); }
        public void DisableTouch() { this.SetDataItemTrigger(".Touch.Disable"); }

        public void ResetSelection() { this.SetDataItemTrigger(".Selection.Reset"); }
        public void SetSelectionTrue() { this.SetDataItemTrigger(".Selection.SetTrue"); }
        public void SetSelectionFalse() { this.SetDataItemTrigger(".Selection.SetFalse"); }

        public void SetTopText(string value) { this.SetDataItemValue(".TopText", value); }
        public void SetName(string value) { this.SetDataItemValue(".Name", value); }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Selection.IndexChanged") {
                    if (e.Value is int) this.Selection = (SelectionValues)e.Value;
                }
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_ValueChanged(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs &&
                e.Value != null) {
                if (e.Path == ".Selection.Index") {
                    if (e.Value is int) this.Selection = (SelectionValues)e.Value;
                }
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
        #endregion

        #region Events.Incoming
        #endregion

    }

}
