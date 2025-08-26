using Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.DicesCalculation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules {

    public class SortingLettersInsert : _InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.Task
        //	[Path]=.Solution
        //	[Path]=.ResetTask
        //	[Path]=.SortTask
        //	[Path]=.Input.ToIn
        //	[Path]=.Input.ToOut
        //	[Path]=.Input.Text

        #region Properties

        private const string sceneID = "project/gamepool/_modules/sortinglettersinsert";

        #endregion


        #region Funktionen

        public SortingLettersInsert(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public SortingLettersInsert(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetTask(string value) { this.SetDataItemValue(".Task", value); }
        public void SetSolution(string value) { this.SetDataItemValue(".Solution", value); }
        public void SetInput(string value) { this.SetDataItemValue(".Input.Text", value); }

        public void ResetTask() { this.SetDataItemTrigger(".ResetTask"); }
        public void SortTask() { this.SetDataItemTrigger(".SortTask"); }

        public void InputIn() { this.SetDataItemTrigger(".Input.ToIn"); }
        public void InputOut() { this.SetDataItemTrigger(".Input.ToOut"); }

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
