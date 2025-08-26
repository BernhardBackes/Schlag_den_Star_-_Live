using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.PercentageDivision {

    public class Player : _Base {

        //	[Path]=.MainFader.Reset
        //	[Path]=.MainFader.SetOut
        //	[Path]=.MainFader.ToOut
        //	[Path]=.MainFader.SetIn
        //	[Path]=.MainFader.ToIn
        //	[Path]=.Slider.Enable
        //	[Path]=.Slider.Disable
        //	[Path]=.Slider.A
        //	[Path]=.Slider.Reset
        //	[Path]=.Slider.LockValue
        //	[Path]=.Slider.OKPressed

        #region Properties

        private const string sceneID = "project/gamepool/percentagedivision/player";

        private int valueA = 0;
        public int ValueA {
            get { return this.valueA; }
            private set {
                if (this.valueA != value) {
                    this.valueA = value;
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

        public void Reset() { this.SetDataItemTrigger(".MainFader.Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".MainFader.SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".MainFader.ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".MainFader.SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".MainFader.ToIn"); }

        public void ResetSlider() { this.SetDataItemTrigger(".Slider.Reset"); }
        public void EnableSlider() { this.SetDataItemTrigger(".Slider.Enable"); }
        public void DisableSlider() { this.SetDataItemTrigger(".Slider.Disable"); }

        public void SetSliderValueA(int value) { this.SetDataItemValue(".Slider.A", value); }
        public void SetSliderLockValue(int value) { this.SetDataItemValue(".Slider.LockValue", value); }

        public override void Dispose() {
            base.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.ToOut();
        }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Slider.OKPressed") {
                    this.ValueA = Convert.ToInt32(e.Value);
                    this.on_OKPressed(this, new EventArgs());
                }
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Slider.A") this.ValueA = Convert.ToInt32(e.Value);
            }
        }

        #endregion


        #region Events.Outgoing

        public event EventHandler OKPressed;
        private void on_OKPressed(object sender, EventArgs e) { Helper.raiseEvent(sender, this.OKPressed, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }
}
