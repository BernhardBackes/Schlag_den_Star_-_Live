using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.ImageDateInputScore {

    //	[Path]=.Reset
    //	[Path]=.SetOut
    //	[Path]=.ToOut
    //	[Path]=.SetIn
    //	[Path]=.ToIn
    //	[Path]=.Border.Position.X
    //	[Path]=.Border.Position.Y
    //	[Path]=.Border.Scaling
    //	[Path]=.Border.SetOut
    //	[Path]=.Border.ToOut
    //	[Path]=.Border.SetInput
    //	[Path]=.Border.ToInput
    //	[Path]=.Border.SetOffset
    //	[Path]=.Border.ToOffset
    //	[Path]=.Border.Left.Score
    //	[Path]=.Border.Left.Input
    //	[Path]=.Border.Left.Offset
    //	[Path]=.Border.Right.Score
    //	[Path]=.Border.Right.Input
    //	[Path]=.Border.Right.Offset
    //	[Path]=.Task.Poistion.X
    //	[Path]=.Task.Poistion.Y
    //	[Path]=.Task.Scaling
    //	[Path]=.Task.Text
    //	[Path]=.Task.Solution
    //	[Path]=.Task.SetSolutionOut
    //	[Path]=.Task.SolutionToOut
    //	[Path]=.Task.SetSolutionIn
    //	[Path]=.Task.SolutionToIn

    public class Game : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/imagedateinputscore/game";

        #endregion


        #region Funktionen

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID, Modes.Static) {
        }

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode = Modes.Static)
            : base(syncContext, port, sceneID, mode) {
        }

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        private void init() {
        }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void SetBorderPositionX(int value) { this.SetDataItemValue(".Border.Position.X", value); }
        public void SetBorderPositionY(int value) { this.SetDataItemValue(".Border.Position.Y", value); }
        public void SetBorderScaling(int value) { this.SetDataItemValue(".Border.Scaling", value); }

        public void SetLeftScore(int value) { this.SetDataItemValue(".Border.Left.Score", value); }
        public void SetLeftInput(string value) { this.SetDataItemValue(".Border.Left.Input", value); }
        public void SetLeftOffset(int value) { this.SetDataItemValue(".Border.Left.Offset", value); }

        public void SetRightScore(int value) { this.SetDataItemValue(".Border.Right.Score", value); }
        public void SetRightInput(string value) { this.SetDataItemValue(".Border.Right.Input", value); }
        public void SetRightOffset(int value) { this.SetDataItemValue(".Border.Right.Offset", value); }

        public void SetAddOnOut() { this.SetDataItemTrigger(".Border.SetOut"); }
        public void AddOnToOut() { this.SetDataItemTrigger(".Border.ToOut"); }
        public void SetInputIn() { this.SetDataItemTrigger(".Border.SetInput"); }
        public void InputToIn() { this.SetDataItemTrigger(".Border.ToInput"); }
        public void SetOffsetIn() { this.SetDataItemTrigger(".Border.SetOffset"); }
        public void OffsetToIn() { this.SetDataItemTrigger(".Border.ToOffset"); }

        public void SetTaskPositionX(int value) { this.SetDataItemValue(".Task.Poistion.X", value); }
        public void SetTaskPositionY(int value) { this.SetDataItemValue(".Task.Poistion.Y", value); }
        public void SetTaskScaling(int value) { this.SetDataItemValue(".Task.Scaling", value); }
        public void SetTaskText(string value) { this.SetDataItemValue(".Task.Text", value); }
        public void SetSolution(string value) { this.SetDataItemValue(".Task.Solution", value); }
        public void SetSolutionOut() { this.SetDataItemTrigger(".Task.SetSolutionOut"); }
        public void SolutionToOut() { this.SetDataItemTrigger(".Task.SolutionToOut"); }
        public void SetSolutionIn() { this.SetDataItemTrigger(".Task.SetSolutionIn"); }
        public void SolutionToIn() { this.SetDataItemTrigger(".Task.SolutionToIn"); }

        public override void Clear() {
            base.Clear();
            this.ToOut();
        }

        public override void Dispose() {
            base.Dispose();
        }


        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
