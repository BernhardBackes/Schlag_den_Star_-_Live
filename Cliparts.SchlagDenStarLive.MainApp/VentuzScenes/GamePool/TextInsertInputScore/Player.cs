using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TextInsertInputScore {

    public class Player : _Base {

        //	[Path]=.Reset
        //	[Path]=.ToOut
        //	[Path]=.ToIn
        //	[Path]=.SetIn
        //	[Path]=.Headline
        //	[Path]=.Input.TextChanged
        //	[Path]=.Input.Text
        //	[Path]=.Input.Enable
        //	[Path]=.Input.Disable
        //	[Path]=.Input.Clear
        //	[Path]=.Input.OKPressed

        #region Properties

        private const string sceneID = "project/gamepool/textinsertinputscore/player";

        private string input = string.Empty;
        public string Input {
            get { return this.input; }
            private set {
                if (this.input != value) {
                    if (string.IsNullOrEmpty(value)) this.input = string.Empty;
                    else this.input = (value);
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

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void EnableInput() { this.SetDataItemTrigger(".Input.Enable"); }
        public void DisableInput() { this.SetDataItemTrigger(".Input.Disable"); }
        public void ClearInput() { this.SetDataItemTrigger(".Input.Clear"); }

        public void SetHeadline(string value) { this.SetDataItemValue(".Headline", value); }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Input.TextChanged") this.syncDataItemValue(".Input.Text");
                else if (e.Path == ".Input.OKPressed") this.on_OKButtonPressed(this, new EventArgs());
                object o = this.GetDataItemValue(".Input.Text");
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_ValueChanged(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Input.Text") {
                    object o = this.GetDataItemValue(".Input.Text");
                    if (o == null) this.Input = string.Empty;
                    else this.Input = o.ToString();
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

        public event EventHandler OKButtonPressed;
        private void on_OKButtonPressed(object sender, EventArgs e) { Helper.raiseEvent(sender, this.OKButtonPressed, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }

}
