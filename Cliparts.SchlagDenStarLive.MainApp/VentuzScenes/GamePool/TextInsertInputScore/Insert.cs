using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TextInsertInputScore {

    public class Insert : _Base {

        //	[Path]=.Position.X
        //	[Path]=.Position.Y
        //	[Path]=.Task.Reset
        //	[Path]=.Task.SetOut
        //	[Path]=.Task.ToOut
        //	[Path]=.Task.SetIn
        //	[Path]=.Task.ToIn
        //	[Path]=.Task.Text
        //	[Path]=.InputLeft.Reset
        //	[Path]=.InputLeft.SetOut
        //	[Path]=.InputLeft.ToOut
        //	[Path]=.InputLeft.SetIn
        //	[Path]=.InputLeft.ToIn
        //	[Path]=.InputLeft.Text
        //	[Path]=.InputLeft.Status
        //	[Path]=.InputRight.Reset
        //	[Path]=.InputRight.SetOut
        //	[Path]=.InputRight.ToOut
        //	[Path]=.InputRight.SetIn
        //	[Path]=.InputRight.ToIn
        //	[Path]=.InputRight.Text
        //	[Path]=.InputRight.Status

        #region Properties

        private const string sceneID = "project/gamepool/textinsertinputscore/insert";

        public enum InputStates { Idle, True, False }

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
            this.Score = new _Modules.Score(syncContext, this.addPort("ScoreLayer"));
        }

        public void SetPositionX(int value) { this.SetDataItemValue(".Position.X", value); }
        public void SetPositionY(int value) { this.SetDataItemValue(".Position.Y", value); }

        public void ResetTask() { this.SetDataItemTrigger(".Task.Reset"); }
        public void SetTaskOut() { this.SetDataItemTrigger(".Task.SetOut"); }
        public void TaskToOut() { this.SetDataItemTrigger(".Task.ToOut"); }
        public void SetTaskIn() { this.SetDataItemTrigger(".Task.SetIn"); }
        public void TaskToIn() { this.SetDataItemTrigger(".Task.ToIn"); }
        public void SetTaskText(string value) { this.SetDataItemValue(".Task.Text", value); }

        public void ResetInputLeft() { this.SetDataItemTrigger(".InputLeft.Reset"); }
        public void SetInputLeftOut() { this.SetDataItemTrigger(".InputLeft.SetOut"); }
        public void InputLeftToOut() { this.SetDataItemTrigger(".InputLeft.ToOut"); }
        public void SetInputLeftIn() { this.SetDataItemTrigger(".InputLeft.SetIn"); }
        public void InputLeftToIn() { this.SetDataItemTrigger(".InputLeft.ToIn"); }
        public void SetInputLeftText(string value) { this.SetDataItemValue(".InputLeft.Text", value); }
        public void SetInputLeftStatus(InputStates value) { this.SetDataItemValue(".InputLeft.Status", value); }

        public void ResetInputRight() { this.SetDataItemTrigger(".InputRight.Reset"); }
        public void SetInputRightOut() { this.SetDataItemTrigger(".InputRight.SetOut"); }
        public void InputRightToOut() { this.SetDataItemTrigger(".InputRight.ToOut"); }
        public void SetInputRightIn() { this.SetDataItemTrigger(".InputRight.SetIn"); }
        public void InputRightToIn() { this.SetDataItemTrigger(".InputRight.ToIn"); }
        public void SetInputRightText(string value) { this.SetDataItemValue(".InputRight.Text", value); }
        public void SetInputRightStatus(InputStates value) { this.SetDataItemValue(".InputRight.Status", value); }

        public override void Dispose() {
            base.Dispose();
            this.Score.Dispose();
            this.TaskToOut();
            this.InputLeftToOut();
            this.InputRightToOut();
        }

        public override void Clear() {
            base.Clear();
            this.Score.Clear();
            this.TaskToOut();
            this.InputLeftToOut();
            this.InputRightToOut();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
