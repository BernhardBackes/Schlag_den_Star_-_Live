using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MovieQuestionsBuzzerScore {

    public class TextInsert : _Modules._InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.SetSolutionIn
        //	[Path]=.ToSolutionIn
        //	[Path]=.Task
        //	[Path]=.Solution

        #region Properties     

        private const string sceneID = "project/gamepool/moviequestionsbuzzerscore/textinsert";

        #endregion


        #region Funktionen

        public TextInsert(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public TextInsert(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetText(string value) { this.SetDataItemValue(".Task", value); }
        public void SetSolution(string value) { this.SetDataItemValue(".Solution", value); }

        public void SetSolutionIn() { this.SetDataItemTrigger(".SetSolutionIn"); }
        public void SolutionToIn() { this.SetDataItemTrigger(".ToSolutionIn"); }

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
