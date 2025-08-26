using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.CashRegister {

    public class Terminal : VRemote4.HandlerSi.Scene, INotifyPropertyChanged {

        //	[Path]=.Reset
        //	[Path]=.Lock
        //	[Path]=.Unlock
        //	[Path]=.Value
        //	[Path]=.OKPressed
        
        #region Properties

        private const string sceneID = "project/gamepool/cashregister/terminal";

        private double value = 0;
        public double Value {
            get { return this.value; }
            private set {
                if (this.value != value) {
                    this.value = value;
                    this.on_PropertyChanged();
                }
            }
        }
        

        #endregion


        #region Funktionen

        public Terminal(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Business client,
            int pipeIndex)
            : base(syncContext, client, pipeIndex, sceneID) {
            this.init();
        }

        private void init() {
        }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }
        public void Lock() { this.SetDataItemTrigger(".Lock"); }
        public void Unlock() { this.SetDataItemTrigger(".Unlock"); }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Value") this.Value = Convert.ToDouble(e.Value);
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_ValueChanged(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs &&
                e.Value != null) {
                if (e.Path == ".OKPressed") {
                    this.Value = Convert.ToDouble(e.Value) / 100;
                    this.on_OKButtonPressed(this, new EventArgs());
                }
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
