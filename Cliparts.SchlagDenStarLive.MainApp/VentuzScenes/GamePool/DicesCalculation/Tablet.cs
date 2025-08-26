using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.DicesCalculation {

    public class Tablet : VRemote4.HandlerSi.Scene, INotifyPropertyChanged {

        //	[Path]=.SelectedPlayer
        //	[Path]=.Name
        //	[Path]=.Start
        //	[Path]=.Stop
        //	[Path]=.Reset
        //	[Path]=.Dice_1_Value
        //	[Path]=.Dice_2_Value
        //	[Path]=.Dice_3_Value
        //	[Path]=.StartTimeout
        //	[Path]=.Input.Dice_1
        /*
        [Path]= .Input.Operation_1 (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Operation_1 (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Operation_1 (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= Subtract (the default value for this DataItem)
            [Elements]= Clear,Add,Subtract,Multiply,Divide (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */

        //	[Path]=.Input.Dice_2
        //	[Path]=.Input.Operation_2
        //	[Path]=.Input.Dice_3
        //	[Path]=.Input.Changed
        //	[Path]=.Input.Result
        //	[Path]=.Input.ResolvePressed

        public enum Players { Left, Right }
        public enum Operations { Clear, Add, Subtract, Multiply, Divide }

        #region Properties

        private const string sceneID = "project/gamepool/dicescalculation/tablet";

        private int dice_1 = 0;
        public int Dice_1 {
            get { return this.dice_1; }
            private set {
                if (this.dice_1 != value) {
                    this.dice_1 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int dice_2 = 0;
        public int Dice_2 {
            get { return this.dice_2; }
            private set {
                if (this.dice_2 != value) {
                    this.dice_2 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int dice_3 = 0;
        public int Dice_3 {
            get { return this.dice_3; }
            private set {
                if (this.dice_3 != value) {
                    this.dice_3 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private Operations operation_1 = Operations.Clear;
        public Operations Operation_1 {
            get { return this.operation_1; }
            private set {
                if (this.operation_1 != value) {
                    this.operation_1 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private Operations operation_2 = Operations.Clear;
        public Operations Operation_2 {
            get { return this.operation_2; }
            private set {
                if (this.operation_2 != value) {
                    this.operation_2 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion


        #region Funktionen

        public Tablet(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Business client,
            int pipeIndex)
            : base(syncContext, client, pipeIndex, sceneID) {
            this.init();
        }

        private void init() {
        }

        public void SetSelectedPlayer(Players value) { this.SetDataItemValue(".SelectedPlayer", value); }
        
        public void SetPlayerName(string value) { this.SetDataItemValue(".Name", value); }

        public void SetDiceValue(
            int id,
            int value) {
            string name = string.Format(".Dice_{0}_Value", id.ToString());
            this.SetDataItemValue(name, value);
        }

        public void Start() { this.SetDataItemTrigger(".Start"); }
        public void StartTimeout(int duration) { this.SetDataItemTrigger(".StartTimeout", duration); }
        public void Stop() { this.SetDataItemTrigger(".Stop"); }
        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Input.Changed") this.syncAllDataItems();
                else if (e.Path == ".Input.ResolvePressed") this.on_ResolvePressed(this, new EventArgs());
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_ValueChanged(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs &&
                e.Value != null) {
                if (e.Path == ".Input.Dice_1") this.Dice_1 = Convert.ToInt32(e.Value);
                else if (e.Path == ".Input.Operation_1") {
                    Operations operation;
                    if (Enum.TryParse(e.Value.ToString(), out operation)) this.Operation_1 = operation;
                }
                else if (e.Path == ".Input.Dice_2") this.Dice_2 = Convert.ToInt32(e.Value);
                else if (e.Path == ".Input.Operation_2") {
                    Operations operation;
                    if (Enum.TryParse(e.Value.ToString(), out operation)) this.Operation_2 = operation;
                }
                else if (e.Path == ".Input.Dice_3") this.Dice_3 = Convert.ToInt32(e.Value);
            }
        }

        #endregion


        #region Events.Outgoing

        public event EventHandler ResolvePressed;
        private void on_ResolvePressed(object sender, EventArgs e) { Helper.raiseEvent(sender, this.ResolvePressed, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }

}
