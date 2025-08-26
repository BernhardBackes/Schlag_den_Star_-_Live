using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.AudioInsertNumericInputAddDifference {

    public class Game : _Modules._InsertBase {


        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.Audio.Start
        //	[Path]=.Audio.Pause
        //	[Path]=.Audio.Play
        //	[Path]=.Audio.Stop
        //	[Path]=.Audio.Filename
        //	[Path]=.Audio.MaxVolume
        //	[Path]=.Audio.Duration
        //	[Path]=.Audio.DurationChanged
        //	[Path]=.Audio.Remaining
        //	[Path]=.Audio.RemainingChanged
        //	[Path]=.Left.Name
        //	[Path]=.Left.Score
        //	[Path]=.Left.Input
        //	[Path]=.Left.Difference
        //	[Path]=.Right.Name
        //	[Path]=.Right.Score
        //	[Path]=.Right.Input
        //	[Path]=.Right.Difference
        //	[Path]=.Task.Text
        //	[Path]=.Task.Solution
        //	[Path]=.Task.SetOut
        //	[Path]=.Task.ToOut
        //	[Path]=.Task.SetIn
        //	[Path]=.Task.ToIn
        //	[Path]=.Task.ToSolution
        //	[Path]=.Task.SetSolution
        //	[Path]=.Input.SetOut
        //	[Path]=.Input.ToOut
        //	[Path]=.Input.SetIn
        //	[Path]=.Input.ToIn
        //	[Path]=.Input.SetResolved
        //	[Path]=.Input.ToResolved

        #region Properties

        private const string sceneID = "project/gamepool/audioinsertnumericinputadddifference/game";

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

        public void SetAudioFilename(string value) { this.SetDataItemValue(".Audio.Filename", value); }
        public void PlayAudio() { this.SetDataItemTrigger(".Audio.Start"); }

        public void SetLeftPlayerName(string value) { this.SetDataItemValue(".Left.Name", value); }
        public void SetLeftPlayerScore(int value) { this.SetDataItemValue(".Left.Score", value); }
        public void SetLeftPlayerInput(int value) { this.SetDataItemValue(".Left.Input", value); }
        public void SetLeftPlayerDifference(int value) { this.SetDataItemValue(".Left.Difference", value); }

        public void SetRightPlayerName(string value) { this.SetDataItemValue(".Right.Name", value); }
        public void SetRightPlayerScore(int value) { this.SetDataItemValue(".Right.Score", value); }
        public void SetRightPlayerInput(int value) { this.SetDataItemValue(".Right.Input", value); }
        public void SetRightPlayerDifference(int value) { this.SetDataItemValue(".Right.Difference", value); }

        public void SetTaskText(string value) { this.SetDataItemValue(".Task.Text", value); }
        public void SetTaskSolution(int value) { this.SetDataItemValue(".Task.Solution", value); }
        public void SetTaskOut() { this.SetDataItemTrigger(".Task.SetOut"); }
        public void TaskToOut() { this.SetDataItemTrigger(".Task.ToOut"); }
        public void SetTaskIn() { this.SetDataItemTrigger(".Task.SetIn"); }
        public void TaskToIn() { this.SetDataItemTrigger(".Task.ToIn"); }
        public void SetSolutionIn() { this.SetDataItemTrigger(".Task.SetSolution"); }
        public void SolutionToIn() { this.SetDataItemTrigger(".Task.ToSolution"); }

        public void SetInputOut() { this.SetDataItemTrigger(".Input.SetOut"); }
        public void InputToOut() { this.SetDataItemTrigger(".Input.ToOut"); }
        public void SetInputIn() { this.SetDataItemTrigger(".Input.SetIn"); }
        public void InputToIn() { this.SetDataItemTrigger(".Input.ToIn"); }
        public void SetResolved() { this.SetDataItemTrigger(".Input.SetResolved"); }
        public void ResolvedToIn() { this.SetDataItemTrigger(".Input.ToResolved"); }

        public override void Dispose() {
            base.Dispose();
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
