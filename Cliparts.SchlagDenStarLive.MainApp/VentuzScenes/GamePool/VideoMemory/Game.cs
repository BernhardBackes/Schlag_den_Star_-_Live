using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.VideoMemory {

    public class Game : VRemote4.HandlerSi.Scene, INotifyPropertyChanged {

        //	[Path]=.Reset
        //	[Path]=.Selection.ToIn
        //	[Path]=.Selection.ToOut
        //	[Path]=.Touch.Enable
        //	[Path]=.Touch.Disable
        //	[Path]=.Touch.Pressed
        //	[Path]=.Buzzer_01.Buzzer.Reset
        //	[Path]=.Buzzer_01.Buzzer.ToIn
        //	[Path]=.Buzzer_01.Buzzer.SetSelected
        //	[Path]=.Buzzer_01.Buzzer.ToSelected
        //	[Path]=.Buzzer_01.Buzzer.SetOut
        //	[Path]=.Buzzer_01.Buzzer.ToOut
        //	[Path]=.Buzzer_01.Touch.Disable
        //	[Path]=.Buzzer_01.Touch.Enable
        //	[Path]=.Buzzer_01.Touch.Pressed
        //	[Path]=.Buzzer_02.Buzzer.Reset
        //	...
        //	[Path]=.Buzzer_42.Touch.Pressed

        #region Properties

        private const string sceneID = "project/gamepool/videomemory/game";

        #endregion


        #region Funktionen

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID, Modes.Static) {
            this.init();
        }

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Business client,
            int pipeIndex)
            : base(syncContext, client, pipeIndex, sceneID) {
            this.init();
        }

        private void init() {
        }

        public override void Dispose() {
            base.Dispose();
        }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }
        public void EnableTouch() { this.SetDataItemTrigger(".Touch.Enable"); }
        public void DisableTouch() { this.SetDataItemTrigger(".Touch.Disable"); }
        public void SelectionToOut() { this.SetDataItemTrigger(".Selection.ToOut"); }
        public void SelectionToIn() { this.SetDataItemTrigger(".Selection.ToIn"); }

        public void Reset(
            int buzzerID) {
            string dataItemName = this.getBuzzerPrefix(buzzerID) + ".Buzzer.Reset";
            this.SetDataItemTrigger(dataItemName);
        }
        public void ToIn(
            int buzzerID) {
            string dataItemName = this.getBuzzerPrefix(buzzerID) + ".Buzzer.ToIn";
            this.SetDataItemTrigger(dataItemName);
        }
        public void SetSelected(
            int buzzerID) {
            string dataItemName = this.getBuzzerPrefix(buzzerID) + ".Buzzer.SetSelected";
            this.SetDataItemTrigger(dataItemName);
        }
        public void ToSelected(
            int buzzerID) {
            string dataItemName = this.getBuzzerPrefix(buzzerID) + ".Buzzer.ToSelected";
            this.SetDataItemTrigger(dataItemName);
        }
        public void SetOut(
            int buzzerID) {
            string dataItemName = this.getBuzzerPrefix(buzzerID) + ".Buzzer.SetOut";
            this.SetDataItemTrigger(dataItemName);
        }
        public void ToOut(
            int buzzerID) {
            string dataItemName = this.getBuzzerPrefix(buzzerID) + ".Buzzer.ToOut";
            this.SetDataItemTrigger(dataItemName);
        }
        public void EnableTouch(
            int buzzerID) {
            string dataItemName = this.getBuzzerPrefix(buzzerID) + ".Touch.Enable";
            this.SetDataItemTrigger(dataItemName);
        }
        public void DisableTouch(
            int buzzerID) {
            string dataItemName = this.getBuzzerPrefix(buzzerID) + ".Touch.Disable";
            this.SetDataItemTrigger(dataItemName);
        }

        private string getBuzzerPrefix(int buzzerID) { return string.Format(".Buzzer_{0}", buzzerID.ToString("00")); }

        #endregion


        #region Events.Outgoing

        public event EventHandler<int> TouchPressed;
        private void on_TouchPressed(object sender, int e) { Helper.raiseEvent(sender, this.TouchPressed, e); }

        #endregion

        #region Events.Incoming

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Touch.Pressed") { this.on_TouchPressed(sender, Convert.ToInt32(e.Value)); }
            }
        }

        #endregion

    }

}
