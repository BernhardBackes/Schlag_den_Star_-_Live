using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules {

    public class ScoreFaults : _InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.Left.Name
        //	[Path]=.Left.Score
        //	[Path]=.Left.Faults.SetOut
        //	[Path]=.Left.Faults.ToOut
        //	[Path]=.Left.Faults.SetIn
        //	[Path]=.Left.Faults.ToIn
        //	[Path]=.Left.Faults.Count
        //	[Path]=.Right.Name
        //	[Path]=.Right.Score
        //	[Path]=.Right.Faults.SetOut
        //	[Path]=.Right.Faults.ToOut
        //	[Path]=.Right.Faults.SetIn
        //	[Path]=.Right.Faults.ToIn
        //	[Path]=.Right.Faults.Count

        #region Properties     
   
        private const string sceneID = "project/gamepool/_modules/scorefaults";

        #endregion


        #region Funktionen

        public ScoreFaults(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public ScoreFaults(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetLeftFaultsOut() { this.SetDataItemTrigger(".Left.Faults.SetOut"); }
        public void LeftFaultsToOut() { this.SetDataItemTrigger(".Left.Faults.ToOut"); }
        public void SetLeftFaultsIn() { this.SetDataItemTrigger(".Left.Faults.SetIn"); }
        public void LeftFaultsToIn() { this.SetDataItemTrigger(".Left.Faults.ToIn"); }

        public void SetRightFaultsOut() { this.SetDataItemTrigger(".Right.Faults.SetOut"); }
        public void RightFaultsToOut() { this.SetDataItemTrigger(".Right.Faults.ToOut"); }
        public void SetRightFaultsIn() { this.SetDataItemTrigger(".Right.Faults.SetIn"); }
        public void RightFaultsToIn() { this.SetDataItemTrigger(".Right.Faults.ToIn"); }

        public void SetLeftName(string value) { this.SetDataItemValue(".Left.Name", value); }
        public void SetLeftScore(int value) { this.SetDataItemValue(".Left.Score", value); }
        public void SetLeftFaults(int value) { this.SetDataItemValue(".Left.Faults.Count", value); }

        public void SetRightName(string value) { this.SetDataItemValue(".Right.Name", value); }
        public void SetRightScore(int value) { this.SetDataItemValue(".Right.Score", value); }
        public void SetRightFaults(int value) { this.SetDataItemValue(".Right.Faults.Count", value); }

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
