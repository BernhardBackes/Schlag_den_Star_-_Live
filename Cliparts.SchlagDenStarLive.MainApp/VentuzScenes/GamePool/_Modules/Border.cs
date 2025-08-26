using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules {

    public class Border : _InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.Scaling
        /*
        [Path]= .Style (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Style (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= FiveDots (the default value for this DataItem)
            [Elements]= Colored,Counter,ColoredCounter,ThreeDots,ThreeDotsCounter,FourDots,FourDotsCounter,FiveDots,FiveDotsCounter,SixDots,SevenDots,Names,NamesColoredTop,NamesColoredBottom (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.Left.Score
        //	[Path]=.Left.Counter
        //	[Path]=.Left.Name
        //	[Path]=.Right.Score
        //	[Path]=.Right.Counter
        //	[Path]=.Right.Name
        //	[Path]=.Buzzer.Reset
        //	[Path]=.Buzzer.Out
        //	[Path]=.Buzzer.Left
        //	[Path]=.Buzzer.Right
        //	[Path]=.AddOn.TextLeft
        //	[Path]=.AddOn.LeftBackgroundIsGreen
        //	[Path]=.AddOn.TextRight
        //	[Path]=.AddOn.RightBackgroundIsGreen
        //	[Path]=.AddOn.SetOut
        //	[Path]=.AddOn.ToOut
        //	[Path]=.AddOn.SetIn
        //	[Path]=.AddOn.ToIn

        public enum Styles { Colored, Counter, ColoredCounter, ThreeDots, ThreeDotsCounter, FourDots, FourDotsCounter, FiveDots, FiveDotsCounter, SixDots, SevenDots, Names, NamesColoredTop, NamesColoredBottom }

        #region Properties

        private const string sceneID = "project/gamepool/_modules/border";

        #endregion


        #region Funktionen

        public Border(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public Border(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetScaling(int value) { this.SetDataItemValue(".Scaling", value); }
        public void SetStyle(Styles value) { this.SetDataItemValue(".Style", value); }

        public void SetLeftScore(int value) { this.SetDataItemValue(".Left.Score", value); }
        public void SetLeftCounter(int value) { this.SetDataItemValue(".Left.Counter", value); }
        public void SetLeftName(string value) { this.SetDataItemValue(".Left.Name", value); }
        public void SetLeftAddOnText(string value) { this.SetDataItemValue(".AddOn.TextLeft", value); }
        public void SetLeftAddOnIsGreen(bool value) { this.SetDataItemValue(".AddOn.LeftBackgroundIsGreen", value); }

        public void SetRightScore(int value) { this.SetDataItemValue(".Right.Score", value); }
        public void SetRightCounter(int value) { this.SetDataItemValue(".Right.Counter", value); }
        public void SetRightName(string value) { this.SetDataItemValue(".Right.Name", value); }
        public void SetRightAddOnText(string value) { this.SetDataItemValue(".AddOn.TextRight", value); }
        public void SetRightAddOnIsGreen(bool value) { this.SetDataItemValue(".AddOn.RightBackgroundIsGreen", value); }

        public void ResetBuzzer() { this.SetDataItemTrigger(".Buzzer.Out"); }
        public void SetBuzzerLeft() { this.SetDataItemTrigger(".Buzzer.Left"); }
        public void SetBuzzerRight() { this.SetDataItemTrigger(".Buzzer.Right"); }

        public void SetAddOnOut() { this.SetDataItemTrigger(".AddOn.SetOut"); }
        public void AddOnToOut() { this.SetDataItemTrigger(".AddOn.ToOut"); }
        public void SetAddOnIn() { this.SetDataItemTrigger(".AddOn.SetIn"); }
        public void AddOnToIn() { this.SetDataItemTrigger(".AddOn.ToIn"); }

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
