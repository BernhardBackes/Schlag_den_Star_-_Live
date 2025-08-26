using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.DicesCalculation {

    public class Insert : _Modules._InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.Dices.Reset
        //	[Path]=.Dices.SetOut
        //	[Path]=.Dices.ToOut
        //	[Path]=.Dices.SetIn
        //	[Path]=.Dices.ToIn
        //	[Path]=.Dices.Dice_1
        //	[Path]=.Dices.Dice_2
        //	[Path]=.Dices.Dice_3
        //	[Path]=.Left.Reset
        //	[Path]=.Left.SetOut
        //	[Path]=.Left.ToOut
        //	[Path]=.Left.SetIn
        //	[Path]=.Left.ToIn
        //	[Path]=.Left.Input.Dice_1
        /*
        [Path]= .Left.Input.Operation_1 (full path of the DataItem)
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
        //	[Path]=.Left.Input.Dice_2
        //	[Path]=.Left.Input.Operation_2
        //	[Path]=.Left.Input.Dice_3
        //	[Path]=.Left.Input.Result
        //	[Path]=.Left.Buzzer
        //	[Path]=.Left.Border.Reset
        //	[Path]=.Left.Border.ToIn
        //	[Path]=.Right.Reset
        //	[Path]=.Right.SetOut
        //	[Path]=.Right.ToOut
        //	[Path]=.Right.SetIn
        //	[Path]=.Right.ToIn
        //	[Path]=.Right.Input.Dice_1
        //	[Path]=.Right.Input.Operation_1
        //	[Path]=.Right.Input.Dice_2
        //	[Path]=.Right.Input.Operation_2
        //	[Path]=.Right.Input.Dice_3
        //	[Path]=.Right.Input.Result
        //	[Path]=.Right.Buzzer
        //	[Path]=.Right.Border.Reset
        //	[Path]=.Right.Border.ToIn
        //	[Path]=.Solution.Reset
        //	[Path]=.Solution.SetOut
        //	[Path]=.Solution.ToOut
        //	[Path]=.Solution.SetIn
        //	[Path]=.Solution.ToIn
        //	[Path]=.Solution.Input.Dice_1
        //	[Path]=.Solution.Input.Operation_1
        //	[Path]=.Solution.Input.Dice_2
        //	[Path]=.Solution.Input.Operation_2
        //	[Path]=.Solution.Input.Dice_3
        //	[Path]=.Solution.Input.Result

        #region Properties

        private const string sceneID = "project/gamepool/dicescalculation/insert";

        public _Modules.Timer Timer;
        public _Modules.Score Score;

        #endregion


        #region Funktionen

        public Insert(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        public Insert(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
            this.init();
        }

        private void init() {
            this.Timer = new _Modules.Timer(syncContext, this.addPort("TimerLayer"));
            this.Score = new _Modules.Score(syncContext, this.addPort("ScoreLayer"));
        }

        public void ResetDices() { this.SetDataItemTrigger(".Dices.Reset"); }
        public void SetDicesOut() { this.SetDataItemTrigger(".Dices.SetOut"); }
        public void DicesToOut() { this.SetDataItemTrigger(".Dices.ToOut"); }
        public void SetDicesIn() { this.SetDataItemTrigger(".Dices.SetIn"); }
        public void DicesToIn() { this.SetDataItemTrigger(".Dices.ToIn"); }
        public void SetDice(
            int id,
            int value) {
            string name = string.Format(".Dices.Dice_{0}", id.ToString());
            this.SetDataItemValue(name, value);
        }

        public void ResetLeft() { this.SetDataItemTrigger(".Left.Reset"); }
        public void SetLeftOut() { this.SetDataItemTrigger(".Left.SetOut"); }
        public void LeftToOut() { this.SetDataItemTrigger(".Left.ToOut"); }
        public void SetLeftIn() { this.SetDataItemTrigger(".Left.SetIn"); }
        public void LeftToIn() { this.SetDataItemTrigger(".Left.ToIn"); }
        public void SetLeftDice(
            int id,
            int value) {
            string name = string.Format(".Left.Input.Dice_{0}", id.ToString());
            this.SetDataItemValue(name, value);
        }
        public void SetLeftOperation(
            int id,
            Tablet.Operations value) {
            string name = string.Format(".Left.Input.Operation_{0}", id.ToString());
            this.SetDataItemValue(name, value);
        }
        public void SetLeftResult(int value) { this.SetDataItemValue(".Left.Input.Result", value); }
        public void LeftBuzzer() { this.SetDataItemTrigger(".Left.Buzzer"); }
        public void ResetLeftBorder() { this.SetDataItemTrigger(".Left.Border.Reset"); }
        public void LeftBorderToIn() { this.SetDataItemTrigger(".Left.Border.ToIn"); }

        public void ResetRight() { this.SetDataItemTrigger(".Right.Reset"); }
        public void SetRightOut() { this.SetDataItemTrigger(".Right.SetOut"); }
        public void RightToOut() { this.SetDataItemTrigger(".Right.ToOut"); }
        public void SetRightIn() { this.SetDataItemTrigger(".Right.SetIn"); }
        public void RightToIn() { this.SetDataItemTrigger(".Right.ToIn"); }
        public void SetRightDice(
            int id,
            int value) {
            string name = string.Format(".Right.Input.Dice_{0}", id.ToString());
            this.SetDataItemValue(name, value);
        }
        public void SetRightOperation(
            int id,
            Tablet.Operations value) {
            string name = string.Format(".Right.Input.Operation_{0}", id.ToString());
            this.SetDataItemValue(name, value);
        }
        public void SetRightResult(int value) { this.SetDataItemValue(".Right.Input.Result", value); }
        public void RightBuzzer() { this.SetDataItemTrigger(".Right.Buzzer"); }

        public void ResetSolution() { this.SetDataItemTrigger(".Solution.Reset"); }
        public void SetSolutionOut() { this.SetDataItemTrigger(".Solution.SetOut"); }
        public void SolutionToOut() { this.SetDataItemTrigger(".Solution.ToOut"); }
        public void SetSolutionIn() { this.SetDataItemTrigger(".Solution.SetIn"); }
        public void SolutionToIn() { this.SetDataItemTrigger(".Solution.ToIn"); }
        public void SetSolutionDice(
            int id,
            int value) {
            string name = string.Format(".Solution.Input.Dice_{0}", id.ToString());
            this.SetDataItemValue(name, value);
        }
        public void SetSolutionOperation(
            int id,
            Tablet.Operations value) {
            string name = string.Format(".Solution.Input.Operation_{0}", id.ToString());
            this.SetDataItemValue(name, value);
        }
        public void SetSolutionResult(int value) { this.SetDataItemValue(".Solution.Input.Result", value); }
        public void ResetRightBorder() { this.SetDataItemTrigger(".Right.Border.Reset"); }
        public void RightBorderToIn() { this.SetDataItemTrigger(".Right.Border.ToIn"); }

        public override void Dispose() {
            base.Dispose();
            this.Timer.Dispose();
            this.Score.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.Timer.Clear();
            this.Score.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
