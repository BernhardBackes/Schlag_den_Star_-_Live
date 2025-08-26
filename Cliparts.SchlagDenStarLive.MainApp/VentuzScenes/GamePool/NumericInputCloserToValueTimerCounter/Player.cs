using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.NumericInputCloserToValueTimerCounter {

    public class Player : _Base {

        //	[Path]=.Reset
        //	[Path]=.SetOut
        //	[Path]=.ToOut
        //	[Path]=.SetIn
        //	[Path]=.ToIn
        //	[Path]=.Task
        //	[Path]=.Lock
        //	[Path]=.Unlock
        //	[Path]=.OKPressed
        /*
        [Path]= .Type (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Type (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= Numerary (the default value for this DataItem)
            [Elements]= Numerary,Time (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.Text.Value
        //	[Path]=.Text.Changed

        public enum TypeItems { Numerary, Time }

        #region Properties

        private const string sceneID = "project/gamepool/numericinputclosertovaluetimercounter/player";

        private string text = string.Empty;
        public string InputText {
            get { return this.text; }
            protected set {
                if (this.text != value) {
                    if (string.IsNullOrEmpty(value)) this.text = string.Empty;
                    else this.text = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int inputValue = 0;
        public int InputValue {
            get { return this.inputValue; }
            set {
                if (this.inputValue != value) {
                    this.inputValue = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private TypeItems type = TypeItems.Numerary;

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

        public void SetTask(string value) { this.SetDataItemValue(".Task", value); }
        public void SetType(TypeItems value) {
            this.type = value;
            this.SetDataItemValue(".Type", value); 
        }

        public void LockTouch() { this.SetDataItemTrigger(".Lock"); }
        public void UnlockTouch() { this.SetDataItemTrigger(".Unlock"); }

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
                if (e.Path == ".Text.Changed") this.syncDataItemValue(".Text.Value");
                else if (e.Path == ".OKPressed") {
                    this.InputValue = Convert.ToInt32(e.Value);
                    this.on_OKButtonPressed(this, new EventArgs());
                }
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_ValueChanged(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs &&
                e.Value != null) {
                if (e.Path == ".Type") {
                    Player.TypeItems result;
                    if (Enum.TryParse(e.Value.ToString(), out result)) this.type = result;
                }
                else if (e.Path == ".Text.Value") this.InputText = e.Value.ToString();
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
