using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SortItemsScore {

    //	[Path]=.Reset
    //	[Path]=.ToOut
    //	[Path]=.ToIn
    //	[Path]=.SetIn
    //	[Path]=.Touch.Enable
    //	[Path]=.Touch.Disable
    //	[Path]=.Items.Item_01.Value
    //	[Path]=.Items.Item_02.Value
    //	[Path]=.Items.Item_03.Value
    //	[Path]=.Items.Item_04.Value
    //	[Path]=.Items.Item_05.Value
    /*
    [Path]= .Result.Array (full path of the DataItem)
    [Description]=  (the description of the DataItem)
    [Label]= Array (the display label of the DataItem)
    [Mode]= R (read/write mode of the DataItem)
    [Name]= Array (the name of the DataItem)
    [UserData]=  (user-defined information of the DataItem)
    [Type]= Ventuz.Remoting4.SceneModel.IntegerArray (type of the current instance)
     */
    //	[Path]=.Result.Changed.Event.Fired
    //	[Path]=.Result.OK.Event.Fired

    public class Player : _Base {

        #region Properties

        private int[] resultArray = new int[0];
        public int[] ResultArray {
            get { return this.resultArray; }
            set {
                if (this.resultArray != value) {
                    this.resultArray = value;
                    this.on_PropertyChanged();
                }
            }
        }


        private const string sceneID = "project/gamepool/sortitemsscore/player";

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

        public void SetItemText(
            int itemID,
            string value) {
            this.SetDataItemValue(string.Format(".Items.Item_{0}.Value", itemID.ToString("00")), value);
        }

        public void EnableTouch() { this.SetDataItemTrigger(".Touch.Enable"); }
        public void DisableTouch() { this.SetDataItemTrigger(".Touch.Disable"); }

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
                if (e.Path == ".Result.Changed.Event.Fired") this.syncDataItemValue(".Result.Array");
                else if (e.Path == ".Result.Ok.Event.Fired") this.on_OKPressed(this, new EventArgs());
            }
        }
        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_ValueChanged(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Result.Array" &&
                    e.Value is int[]) this.ResultArray = (int[])e.Value;
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
