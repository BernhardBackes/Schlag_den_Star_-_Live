using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.AudioInsertTeamNumericInputAddDifference {

    public class PlayerTablet : VRemote4.HandlerSi.Scene, INotifyPropertyChanged
    {

        //	[Path]=.Task
        //	[Path]=.Reset
        //	[Path]=.Lock
        //	[Path]=.Unlock
        //	[Path]=.OKPressed
        //	[Path]=.Text.Value
        //	[Path]=.Text.Changed

        #region Properties

        private const string sceneID = "project/gamepool/audioinsertteamnumericinputadddifference/playertablet";

        private string text = string.Empty;
        public string Text {
            get { return this.text; }
            protected set {
                if (this.text != value) {
                    if (string.IsNullOrEmpty(value)) this.text = string.Empty;
                    else this.text = value;
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion


        #region Funktionen

        public PlayerTablet(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Business client,
            int pipeIndex)
            : base(syncContext, client, pipeIndex, sceneID)
        {
            this.init();
        }

        private void init() {
        }

        public void SetTask(string value) { this.SetDataItemValue(".Task", value); }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }
        public void LockTouch() { this.SetDataItemTrigger(".Lock"); }
        public void UnlockTouch() { this.SetDataItemTrigger(".Unlock"); }

        public override void Dispose() {
            base.Dispose();
        }

        public void Clear() {
            this.LockTouch();
        }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Text.Changed") this.syncDataItemValue(".Text.Value");
                else if (e.Path == ".OKPressed") {
                    this.Text = e.Value.ToString();
                    this.on_OKButtonPressed(this, new EventArgs());
                }
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_ValueChanged(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs &&
                e.Value != null) {
                if (e.Path == ".Text.Value") this.Text = e.Value.ToString();
            }
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
