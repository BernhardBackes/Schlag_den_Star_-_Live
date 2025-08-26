using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules {

    public class  NumericValuesInsert : _InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.Top.Name
        //	[Path]=.Top.Value
        //	[Path]=.Top.AddOn.Value
        //	[Path]=.Top.AddOn.SetIn
        //	[Path]=.Top.AddOn.ToIn
        //	[Path]=.Top.AddOn.SetGreen
        //	[Path]=.Top.AddOn.ToGreen
        //	[Path]=.Top.AddOn.SetOut
        //	[Path]=.Top.AddOn.ToOut
        //	[Path]=.Bottom.Name
        //  ...
        /*
        [Path]= .Border (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Border (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= Off (the default value for this DataItem)
            [Elements]= Off,Top,Bottom (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */

        #region Properties     

        private const string sceneID = "project/gamepool/_modules/numericvaluesinsert";

        public enum BorderPosition { Off, Top, Bottom };

        #endregion


        #region Funktionen

        public NumericValuesInsert(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public NumericValuesInsert(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetTopName(string value) { this.SetDataItemValue(".Top.Name", value); }
        public void SetTopValue(string value) { this.SetDataItemValue(".Top.Value", value); }
        public void SetTopAddOnValue(string value) { this.SetDataItemValue(".Top.AddOn.Value", value); }
        public void TopAddOnValueSetOut() { this.SetDataItemTrigger(".Top.AddOn.SetOut"); }
        public void TopAddOnValueToOut() { this.SetDataItemTrigger(".Top.AddOn.ToOut"); }
        public void TopAddOnValueSetIn() { this.SetDataItemTrigger(".Top.AddOn.SetIn"); }
        public void TopAddOnValueToIn() { this.SetDataItemTrigger(".Top.AddOn.ToIn"); }
        public void TopAddOnValueSetGreen() { this.SetDataItemTrigger(".Top.AddOn.SetGreen"); }
        public void TopAddOnValueToGreen() { this.SetDataItemTrigger(".Top.AddOn.ToGreen"); }

        public void SetBottomName(string value) { this.SetDataItemValue(".Bottom.Name", value); }
        public void SetBottomValue(string value) { this.SetDataItemValue(".Bottom.Value", value); }
        public void SetBottomAddOnValue(string value) { this.SetDataItemValue(".Bottom.AddOn.Value", value); }
        public void BottomAddOnValueSetOut() { this.SetDataItemTrigger(".Bottom.AddOn.SetOut"); }
        public void BottomAddOnValueToOut() { this.SetDataItemTrigger(".Bottom.AddOn.ToOut"); }
        public void BottomAddOnValueSetIn() { this.SetDataItemTrigger(".Bottom.AddOn.SetIn"); }
        public void BottomAddOnValueToIn() { this.SetDataItemTrigger(".Bottom.AddOn.ToIn"); }
        public void BottomAddOnValueSetGreen() { this.SetDataItemTrigger(".Bottom.AddOn.SetGreen"); }
        public void BottomAddOnValueToGreen() { this.SetDataItemTrigger(".Bottom.AddOn.ToGreen"); }

        public void SetBorder(BorderPosition value) { this.SetDataItemValue(".Border", value); }

        public override void Dispose() {
            base.Dispose();
            this.Reset();
        }

        public override void Clear() {
            base.Clear();
            this.ToOut();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
