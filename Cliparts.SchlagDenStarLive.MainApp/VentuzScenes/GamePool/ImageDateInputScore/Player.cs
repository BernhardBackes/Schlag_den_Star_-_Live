using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.ImageDateInputScore {

    //	[Path]=.SetOut
    //	[Path]=.ToOut
    //	[Path]=.SetIn
    //	[Path]=.ToIn
    //	[Path]=.Task
    //	[Path]=.Reset
    //	[Path]=.Lock
    //	[Path]=.Release
    //	[Path]=.OKPressed

    public class Player : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/imagedateinputscore/player";

        private DateTime input = DateTime.MinValue;
        public DateTime Input {
            get { return this.input; }
            set {
                if (this.input != value) {
                    this.input = value;
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
        public void Lock() { this.SetDataItemTrigger(".Lock"); }
        public void Unlock() { this.SetDataItemTrigger(".Release"); }

        public void SetTask(string value) { this.SetDataItemValue(".Task", value); }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".OKPressed") {
                    int value = Convert.ToInt32(e.Value);
                    if (value <= 0) this.Input = DateTime.MinValue;
                    else {
                        int month = value / 100;
                        int day = value % 100;
                        if (month > 0 &&
                            day > 0) this.Input = new DateTime(DateTime.Now.Year, month, day);
                        else this.Input = DateTime.MinValue;
                    }
                    this.on_OKPressed(this, this.Input);
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

        public event EventHandler<DateTime> OKPressed;
        private void on_OKPressed(object sender, DateTime e) { Helper.raiseEvent(sender, this.OKPressed, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }
}
