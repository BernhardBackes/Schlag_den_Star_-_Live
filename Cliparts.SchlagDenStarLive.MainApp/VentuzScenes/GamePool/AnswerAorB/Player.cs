using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.AnswerAorB {

    public class Player : _Base {

        //	[Path]=.Reset
        //	[Path]=.ToOut
        //	[Path]=.ToIn
        //	[Path]=.SetIn
        //	[Path]=.Text
        //	[Path]=.AnswerA
        //	[Path]=.AnswerB
        //	[Path]=.Touch.Enable
        //	[Path]=.Touch.Disable
        //	[Path]=.Selection.Reset
        //	[Path]=.Selection.SetAnswerA
        //	[Path]=.Selection.SetAnswerB
        //	[Path]=.Selection.Index
        //	[Path]=.Selection.IndexChanged

        #region Properties

        private int selectionIndex = 0;
        public int SelectionIndex {
            get { return this.selectionIndex; }
            set {
                if (this.selectionIndex != value) {
                    this.selectionIndex = value;
                    this.on_PropertyChanged();
                }
            }
        }


        private const string sceneID = "project/gamepool/answeraorb/player";

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

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void SetText(string value) { this.SetDataItemValue(".Text", value); }
        public void SetAnswerA(string value) { this.SetDataItemValue(".AnswerA", value); }
        public void SetAnswerB(string value) { this.SetDataItemValue(".AnswerB", value); }

        public void EnableTouch() { this.SetDataItemTrigger(".Touch.Enable"); }
        public void DisableTouch() { this.SetDataItemTrigger(".Touch.Disable"); }

        public void ResetSelection() { this.SetDataItemTrigger(".Selection.Reset"); }
        public void SetSelectionAnswerA() { this.SetDataItemTrigger(".Selection.SetAnswerA"); }
        public void SetSelectionAnswerB() { this.SetDataItemTrigger(".Selection.SetAnswerB"); }

        public override void Dispose() {
            base.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.ToOut();
        }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Selection.Index" &&
                    e.Value is int) this.SelectionIndex = (int)e.Value;
            }
        }
        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_ValueChanged(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Selection.IndexChanged" &&
                    e.Value is int) this.SelectionIndex = (int)e.Value;
            }
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
