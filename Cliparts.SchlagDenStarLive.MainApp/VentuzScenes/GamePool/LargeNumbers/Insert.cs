using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.LargeNumbers {

    public class Insert : _Base {

        //	[Path]=.Reset
        //	[Path]=.Position.X
        //	[Path]=.Position.Y
        //	[Path]=.Input.SetOut
        //	[Path]=.Input.ToOut
        //	[Path]=.Input.SetIn
        //	[Path]=.Input.ToIn
        //	[Path]=.Input.Buzzer
        //	[Path]=.Input.Ture
        //	[Path]=.Input.False
        //	[Path]=.Input.Left.Value
        //	[Path]=.Input.Left.SetOut
        //	[Path]=.Input.Left.ToOut
        //	[Path]=.Input.Left.SetIn
        //	[Path]=.Input.Left.ToIn
        //	[Path]=.Input.Left.SetTransparent
        //	[Path]=.Input.Left.ToTransparent
        //	[Path]=.Input.Left.SetTrue
        //	[Path]=.Input.Left.ToTrue
        //	[Path]=.Input.Left.SetFalse
        //	[Path]=.Input.Left.ToFalse
        //	[Path]=.Input.Right.Value
        //	[Path]=.Input.Right.SetOut
        //	[Path]=.Input.Right.ToOut
        //	[Path]=.Input.Right.SetIn
        //	[Path]=.Input.Right.ToIn
        //	[Path]=.Input.Right.SetTransparent
        //	[Path]=.Input.Right.ToTransparent
        //	[Path]=.Input.Right.SetTrue
        //	[Path]=.Input.Right.ToTrue
        //	[Path]=.Input.Right.SetFalse
        //	[Path]=.Input.Right.ToFalse
        /*
        [Path]= .Solution.Buzzer (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Input (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= Left (the default value for this DataItem)
            [Elements]= Left,Right (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
        //	[Path]=.Solution.Value
        //	[Path]=.Solution.SetOut
        //	[Path]=.Solution.ToOut
        //	[Path]=.Solution.SetIn
        //	[Path]=.Solution.ToIn

        public enum BuzzerPositions { Left, Right };

        #region Properties

        private const string sceneID = "project/gamepool/largenumbers/insert";

        public _Modules.Score Score;
        public _Modules.TextInsert TextInsert;

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
            this.Score = new _Modules.Score(syncContext, this.addPort("ScoreLayer"));
            this.TextInsert = new _Modules.TextInsert(syncContext, this.addPort("TextInsertLayer"));
        }

        public void Reset() { 
            this.SetDataItemTrigger(".Reset");
            this.SetInputOut();
            this.SetSolutiontOut();
        }

        public void SetPositionX(int value) { this.SetDataItemValue(".Position.X", value); }
        public void SetPositionY(int value) { this.SetDataItemValue(".Position.Y", value); }

        public void SetInputOut() { this.SetDataItemTrigger(".Input.SetOut"); }
        public void InputToOut() { this.SetDataItemTrigger(".Input.ToOut"); }
        public void SetInputIn() { this.SetDataItemTrigger(".Input.SetIn"); }
        public void InputToIn() { this.SetDataItemTrigger(".Input.ToIn"); }
        public void InputBuzzer() { this.SetDataItemTrigger(".Input.Buzzer"); }
        public void InputTrue() { this.SetDataItemTrigger(".Input.True"); }
        public void InputFalse() { this.SetDataItemTrigger(".Input.False"); }

        public void SetLeftInputValue(string value) { this.SetDataItemValue(".Input.Left.Value", value); }
        public void SetLeftInputOut() { this.SetDataItemTrigger(".Input.Left.SetOut"); }
        public void LeftInputToOut() { this.SetDataItemTrigger(".Input.Left.ToOut"); }
        public void SetLeftInputIn() { this.SetDataItemTrigger(".Input.Left.SetIn"); }
        public void LeftInputToIn() { this.SetDataItemTrigger(".Input.Left.ToIn"); }
        public void SetLeftInputTransparent() { this.SetDataItemTrigger(".Input.Left.SetTransparent"); }
        public void LeftInputToTransparent() { this.SetDataItemTrigger(".Input.Left.ToTransparent"); }
        public void SetLeftInputTrue() { this.SetDataItemTrigger(".Input.Left.SetTrue"); }
        public void LeftInputToTrue() { this.SetDataItemTrigger(".Input.Left.ToTrue"); }
        public void SetLeftInputFalse() { this.SetDataItemTrigger(".Input.Left.SetFalse"); }
        public void LeftInputToFalse() { this.SetDataItemTrigger(".Input.Left.ToFalse"); }

        public void SetRightInputValue(string value) { this.SetDataItemValue(".Input.Right.Value", value); }
        public void SetRightInputOut() { this.SetDataItemTrigger(".Input.Right.SetOut"); }
        public void RightInputToOut() { this.SetDataItemTrigger(".Input.Right.ToOut"); }
        public void SetRightInputIn() { this.SetDataItemTrigger(".Input.Right.SetIn"); }
        public void RightInputToIn() { this.SetDataItemTrigger(".Input.Right.ToIn"); }
        public void SetRightInputTransparent() { this.SetDataItemTrigger(".Input.Right.SetTransparent"); }
        public void RightInputToTransparent() { this.SetDataItemTrigger(".Input.Right.ToTransparent"); }
        public void SetRightInputTrue() { this.SetDataItemTrigger(".Input.Right.SetTrue"); }
        public void RightInputToTrue() { this.SetDataItemTrigger(".Input.Right.ToTrue"); }
        public void SetRightInputFalse() { this.SetDataItemTrigger(".Input.Right.SetFalse"); }
        public void RightInputToFalse() { this.SetDataItemTrigger(".Input.Right.ToFalse"); }

        public void SetSolutionBuzzer(BuzzerPositions value) { this.SetDataItemValue(".Solution.Buzzer", value); }
        public void SetSolutionValue(string value) { this.SetDataItemValue(".Solution.Value", value); }
        public void SetSolutiontOut() { this.SetDataItemTrigger(".Solution.SetOut"); }
        public void SolutionToOut() { this.SetDataItemTrigger(".Solution.ToOut"); }
        public void SetSolutionIn() { this.SetDataItemTrigger(".Solution.SetIn"); }
        public void SolutionToIn() { this.SetDataItemTrigger(".Solution.ToIn"); }

        public override void Dispose() {
            base.Dispose();
            this.Score.Dispose();
            this.TextInsert.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.Score.Clear();
            this.TextInsert.Clear();
            this.InputToOut();
            this.SolutionToOut();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
