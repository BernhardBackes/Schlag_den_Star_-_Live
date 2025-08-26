using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.Memory {

    public class Game : VRemote4.HandlerSi.Scene, INotifyPropertyChanged {

        //	[Path]=.Next
        //	[Path]=.Reset
        /*
        [Path]= .DuellMode (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Value (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= DuellMode (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Boolean (type of the current instance)
            [Default]= False (the default value for this DataItem)
            [PropertyType]= System.Boolean (the underlaying system type of this DataItem)
         */
        //	[Path]=.Touch.Enable
        //	[Path]=.Touch.Disable
        //	[Path]=.Touch.Pressed
        //  [Path]=.Touch.Failure
        //	[Path]=.Card_01.Picture.Filename
        //	[Path]=.Card_01.Picture.Completed
        //	[Path]=.Card_01.Card.Reset
        //	[Path]=.Card_01.Card.ToIn
        //	[Path]=.Card_01.Card.SetSelected
        //	[Path]=.Card_01.Card.ToSelected
        //	[Path]=.Card_01.Card.SetOut
        //	[Path]=.Card_01.Card.ToOut
        //	[Path]=.Card_01.Card.Unlocked
        //	[Path]=.Card_01.Touch.Disable
        //	[Path]=.Card_01.Touch.Enable
        //  [Path]=.Card_01.Touch.FailureInvoke
        //	[Path]=.Card_01.Touch.Pressed
        //	[Path]=.Card_02.Audio.Filename
        //	...
        //	[Path]=.Card_42.Touch.Pressed

        #region Properties

        private const string sceneID = "project/gamepool/memory/game";

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
        public void Next() { this.SetDataItemTrigger(".Next"); }
        public void EnableTouch() { this.SetDataItemTrigger(".Touch.Enable"); }
        public void DisableTouch() { this.SetDataItemTrigger(".Touch.Disable"); }

        public void SetDuellMode(bool value) { this.SetDataItemValue(".DuellMode", value); }

        public void SetPicturefile(
            int cardID,
            string value) {
            string dataItemName = this.getCardPrefix(cardID) + ".Picture.Filename";
            this.SetDataItemValue(dataItemName, value);
        }
        public void Reset(
            int cardID) {
            string dataItemName = this.getCardPrefix(cardID) + ".Card.Reset";
            this.SetDataItemTrigger(dataItemName);
        }
        public void ToIn(
            int cardID) {
            string dataItemName = this.getCardPrefix(cardID) + ".Card.ToIn";
            this.SetDataItemTrigger(dataItemName);
        }
        public void SetSelected(
            int cardID) {
            string dataItemName = this.getCardPrefix(cardID) + ".Card.SetSelected";
            this.SetDataItemTrigger(dataItemName);
        }
        public void ToSelected(
            int cardID) {
            string dataItemName = this.getCardPrefix(cardID) + ".Card.ToSelected";
            this.SetDataItemTrigger(dataItemName);
        }
        public void SetOut(
            int cardID) {
            string dataItemName = this.getCardPrefix(cardID) + ".Card.SetOut";
            this.SetDataItemTrigger(dataItemName);
        }
        public void ToOut(
            int cardID) {
            string dataItemName = this.getCardPrefix(cardID) + ".Card.ToOut";
            this.SetDataItemTrigger(dataItemName);
        }
        public void Unlock(
            int cardID) {
            string dataItemName = this.getCardPrefix(cardID) + ".Card.Unlock";
            this.SetDataItemTrigger(dataItemName);
        }
        public void EnableTouch(
            int cardID) {
            string dataItemName = this.getCardPrefix(cardID) + ".Touch.Enable";
            this.SetDataItemTrigger(dataItemName);
        }
        public void DisableTouch(
            int cardID) {
            string dataItemName = this.getCardPrefix(cardID) + ".Touch.Disable";
            this.SetDataItemTrigger(dataItemName);
        }
        public void InvokeTouchFailure(
            int cardID) {
            string dataItemName = this.getCardPrefix(cardID) + ".Touch.FailureInvoke";
            this.SetDataItemTrigger(dataItemName);
        }

        private string getCardPrefix(int cardID) { return string.Format(".Card_{0}", cardID.ToString("00")); }

        #endregion


        #region Events.Outgoing

        public event EventHandler<int> TouchPressed;
        private void on_TouchPressed(object sender, int e) { Helper.raiseEvent(sender, this.TouchPressed, e); }

        public event EventHandler<int> TouchFailure;
        private void on_TouchFailure(object sender, int e) { Helper.raiseEvent(sender, this.TouchFailure, e); }

        #endregion

        #region Events.Incoming

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Touch.Pressed") { this.on_TouchPressed(sender, Convert.ToInt32(e.Value)); }
                else if (e.Path == ".Touch.Failure") { this.on_TouchFailure(sender, Convert.ToInt32(e.Value)); }
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs &&
                e.Value != null) {
                //if (e.Path == ".Audio.Duration") { this.Duration = Convert.ToInt32(e.Value); }
                //else if (e.Path == ".Audio.Remaining") { this.Remaining = Convert.ToInt32(e.Value); }
            }
        }

        #endregion

    }

}
