using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules {

    public class BondedDots : _InsertBase {

        /*
        [Path]= .Style (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Style (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
	        [Default]= TenDots (the default value for this DataItem)
	        [Elements]= TenDots,ElevenDots,ThirteenDots,FifteenDots (an array of strings containing the choice of enum values)
	        [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        //	[Path]=.ResetAllDots
        //	[Path]=.Top.Name
        //	[Path]=.Top.Reset
        //	[Path]=.Top.ResetPointer
        //	[Path]=.Top.SetPointer
        //	[Path]=.Top.SetValue
        //	[Path]=.Top.SetIn
        //	[Path]=.Top.ToIn
        //	[Path]=.Bottom.Name        
        
        public enum Styles { TenDots,ElevenDots,ThirteenDots,FifteenDots }

        #region Properties     
   
        private const string sceneID = "project/gamepool/_modules/bondeddots";

        #endregion


        #region Funktionen

        public BondedDots(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public BondedDots(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetStyle(Styles value) { this.SetDataItemValue(".Style", value); }

        public void SetTopName(string value) { this.SetDataItemValue(".Top.Name", value); }
        public void ResetTopPointer() { this.SetDataItemTrigger(".Top.ResetPointer"); }
        public void SetTopPointer(int index) { this.SetDataItemTrigger(".Top.SetPointer", index); }
        public void SetTopValue(int value) { this.SetDataItemTrigger(".Top.SetValue", value); }
        public void ResetTopDot(int index) { this.SetDataItemTrigger(".Top.Reset", index); }
        public void SetTopDotIn(int index) { this.SetDataItemTrigger(".Top.SetIn", index); }
        public void TopDotToIn(int index) { this.SetDataItemTrigger(".Top.ToIn", index); }

        public void SetBottomName(string value) { this.SetDataItemValue(".Bottom.Name", value); }
        public void ResetBottomPointer() { this.SetDataItemTrigger(".Bottom.ResetPointer"); }
        public void SetBottomPointer(int index) { this.SetDataItemTrigger(".Bottom.SetPointer", index); }
        public void SetBottomValue(int value) { this.SetDataItemTrigger(".Bottom.SetValue", value); }
        public void ResetBottomDot(int index) { this.SetDataItemTrigger(".Bottom.Reset", index); }
        public void SetBottomDotIn(int index) { this.SetDataItemTrigger(".Bottom.SetIn", index); }
        public void BottomDotToIn(int index) { this.SetDataItemTrigger(".Bottom.ToIn", index); }

        public void ResetAllDots() { this.SetDataItemTrigger(".ResetAllDots"); }

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
