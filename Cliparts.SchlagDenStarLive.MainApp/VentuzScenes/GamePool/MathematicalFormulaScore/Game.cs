using System;
using System.Threading;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MathematicalFormulaScore {

    public class Game : _Modules._InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        /*
        [Path]= .Size (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Size (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= TwoOperations (the default value for this DataItem)
            [Elements]= TwoOperations,ThreeOperations (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.Formula.Number_01
        /*
        [Path]= .Formula.Operation_01 (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Operation_01 (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= Plus (the default value for this DataItem)
            [Elements]= Plus,Minus,Mal,Geteilt (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.Formula.Number_02
        //	[Path]=.Formula.Operation_02
        //	[Path]=.Formula.Number_03
        //	[Path]=.Formula.Operation_03
        //	[Path]=.Formula.Number_04
        //	[Path]=.Formula.Result
        //	[Path]=.Formula.Solution
        //	[Path]=.Formula.IsTrue
        //	[Path]=.Formula.Animation.ToOut
        //	[Path]=.Formula.Animation.SetOut
        //	[Path]=.Formula.Animation.SetIn
        //	[Path]=.Formula.Animation.ToIn
        //	[Path]=.Formula.Animation.SetOperationIn
        //	[Path]=.Formula.Animation.ToOperationIn
        //	[Path]=.Formula.Animation.SetResultIn
        //	[Path]=.Formula.Animation.ToResultIn
        //	[Path]=.Formula.Animation.SetBorderIn
        //	[Path]=.Formula.Animation.ToBorderIn
        //	[Path]=.Buzzer_Left
        //	[Path]=.Buzzer_Right
        //  [Path]=.Jingle_Change

        public enum Sizes { TwoOperations, ThreeOperations };
        public enum Operations { Plus, Minus, Mal, Geteilt };

        #region Properties

        private const string sceneID = "project/gamepool/mathematicalformulascore/game";

        #endregion


        #region Funktionen

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }
  
        public override void Dispose() {
            base.Dispose();
        }

        public void SetSize(Sizes value) { this.SetDataItemValue(".Size", value); }
        public void SetNumber(int id, int value) { this.SetDataItemValue(string.Format(".Formula.Number_{0}", id.ToString("00")), value); }
        public void SetOperation(int id, Operations value) { this.SetDataItemValue(string.Format(".Formula.Operation_{0}", id.ToString("00")), value); }
        public void SetResult(int value) { this.SetDataItemValue(".Formula.Result", value); }
        public void SetSolution(int value) { this.SetDataItemValue(".Formula.Solution", value); }
        public void SetIsTrue(bool value) { this.SetDataItemValue(".Formula.IsTrue", value); }
        public void ToFormulaOut() { this.SetDataItemTrigger(".Formula.Animation.ToOut"); }
        public void SetToFormulaOut() { this.SetDataItemTrigger(".Formula.Animation.SetOut"); }
        public void SetToFormulaIn() { this.SetDataItemTrigger(".Formula.Animation.SetIn"); }
        public void ToFormulaIn() { this.SetDataItemTrigger(".Formula.Animation.ToIn"); }
        public void SetToOperationIn() { this.SetDataItemTrigger(".Formula.Animation.SetOperationIn"); }
        public void ToOperationIn() { this.SetDataItemTrigger(".Formula.Animation.ToOperationIn"); }
        public void SetToResultIn() { this.SetDataItemTrigger(".Formula.Animation.SetResultIn"); }
        public void ToResultIn() { this.SetDataItemTrigger(".Formula.Animation.ToResultIn"); }
        public void SetToBorderIn() { this.SetDataItemTrigger(".Formula.Animation.SetBorderIn"); }
        public void ToBorderIn() { this.SetDataItemTrigger(".Formula.Animation.ToBorderIn"); }

        public void BuzzerLeft() { this.SetDataItemTrigger(".Buzzer_Left"); }
        public void BuzzerRight() { this.SetDataItemTrigger(".Buzzer_Right"); }
        public void JingleChange() { this.SetDataItemTrigger(".Jingle_Change"); }

        public override void Clear() {
            base.Clear();
        }


        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
