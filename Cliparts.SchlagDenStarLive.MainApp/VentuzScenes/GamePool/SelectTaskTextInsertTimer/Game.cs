using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SelectTaskTextInsertTimer
{

    public class Game : _Modules._InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.Task_01.False.Uri
        //	[Path]=.Task_01.Neutral.Uri
        //	[Path]=.Task_01.Selected.Uri
        //	[Path]=.Task_01.True.Uri
        //	[Path]=.Task_01.Task.Reset
        //	[Path]=.Task_01.Task.ToNeutral
        //	[Path]=.Task_01.Task.SetSelected
        //	[Path]=.Task_01.Task.ToSelected
        //	[Path]=.Task_01.Task.SetTrue
        //	[Path]=.Task_01.Task.ToTrue
        //	[Path]=.Task_01.Task.SetFalse
        //	[Path]=.Task_01.Task.ToFalse
        //	[Path]=.Task_01.Task.SetOut
        //	[Path]=.Task_01.Task.ToOut
        //	[Path]=.Task_02.False.Uri
        //	...
        //	[Path]=.Task_20.Task.ToOut

        #region Properties

        private const string sceneID = "project/gamepool/selecttasktextinserttimer/game";

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

        public void SetTaskNeutralFilename(
            int id,
            string value)
        {
            string name = string.Format("{0}.Neutral.Uri", this.taskPrefix(id));
            this.SetDataItemValue(name, value);
        }
        public void SetTaskSelectedFilename(
            int id,
            string value)
        {
            string name = string.Format("{0}.Selected.Uri", this.taskPrefix(id));
            this.SetDataItemValue(name, value);
        }
        public void SetTaskTrueFilename(
            int id,
            string value)
        {
            string name = string.Format("{0}.True.Uri", this.taskPrefix(id));
            this.SetDataItemValue(name, value);
        }
        public void SetTaskFalseFilename(
            int id,
            string value)
        {
            string name = string.Format("{0}.False.Uri", this.taskPrefix(id));
            this.SetDataItemValue(name, value);
        }
        public void ResetTask(
            int id)
        {
            string name = string.Format("{0}.Task.Reset", this.taskPrefix(id));
            this.SetDataItemTrigger(name);
        }
        public void TaskToNeutral(
            int id)
        {
            string name = string.Format("{0}.Task.ToNeutral", this.taskPrefix(id));
            this.SetDataItemTrigger(name);
        }
        public void SetTaskSelected(
            int id)
        {
            string name = string.Format("{0}.Task.SetSelected", this.taskPrefix(id));
            this.SetDataItemTrigger(name);
        }
        public void TaskToSelected(
            int id)
        {
            string name = string.Format("{0}.Task.ToSelected", this.taskPrefix(id));
            this.SetDataItemTrigger(name);
        }
        public void SetTaskTrue(
            int id)
        {
            string name = string.Format("{0}.Task.SetTrue", this.taskPrefix(id));
            this.SetDataItemTrigger(name);
        }
        public void TaskToTrue(
            int id)
        {
            string name = string.Format("{0}.Task.ToTrue", this.taskPrefix(id));
            this.SetDataItemTrigger(name);
        }
        public void SetTaskFalse(
            int id)
        {
            string name = string.Format("{0}.Task.SetFalse", this.taskPrefix(id));
            this.SetDataItemTrigger(name);
        }
        public void TaskToFalse(
            int id)
        {
            string name = string.Format("{0}.Task.ToFalse", this.taskPrefix(id));
            this.SetDataItemTrigger(name);
        }
        public void SetTaskOut(
            int id)
        {
            string name = string.Format("{0}.Task.SetOut", this.taskPrefix(id));
            this.SetDataItemTrigger(name);
        }
        public void TaskToOut(
            int id)
        {
            string name = string.Format("{0}.Task.ToOut", this.taskPrefix(id));
            this.SetDataItemTrigger(name);
        }

        private string taskPrefix(int id) { return string.Format(".Task_{0}", id.ToString("00")); }

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
