using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.NumericInputTimerCounterScore
{

    public class Host : _Base {

        //	[Path]=.Reset
        //	[Path]=.SetOut
        //	[Path]=.ToOut
        //	[Path]=.SetIn
        //	[Path]=.ToIn
        //	[Path]=.Text
        //	[Path]=.Input.Left.SetOut
        //	[Path]=.Input.Left.SetIn
        //	[Path]=.Input.Left.Value
        //	[Path]=.Input.Right.SetOut
        //	[Path]=.Input.Right.SetIn
        //	[Path]=.Input.Right.Value
        //	[Path]=.Delivered.Left
        //	[Path]=.Delivered.Right
        //	[Path]=.Difference.Left
        //	[Path]=.Difference.Right
        //	[Path]=.Results.SetIn
        //	[Path]=.Results.SetOut

        #region Properties

        private const string sceneID = "project/gamepool/numericinputtimercounterscore/host";

        #endregion


        #region Funktionen

        public Host(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        public Host(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
            this.init();
        }

        private void init() {
        }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void SetResultsOut() { this.SetDataItemTrigger(".Results.SetOut"); }
        public void SetResultsIn() { this.SetDataItemTrigger(".Results.SetIn"); }

        public void SetText(string value) { this.SetDataItemValue(".Text", value); }

        public void SetLeftValue(string value) { this.SetDataItemValue(".Input.Left.Value", value); }
        public void SetLeftIn() { this.SetDataItemTrigger(".Input.Left.SetIn"); }
        public void SetLeftOut() { this.SetDataItemTrigger(".Input.Left.SetOut"); }

        public void SetRightValue(string value) { this.SetDataItemValue(".Input.Right.Value", value); }
        public void SetRightIn() { this.SetDataItemTrigger(".Input.Right.SetIn"); }
        public void SetRightOut() { this.SetDataItemTrigger(".Input.Right.SetOut"); }

        public void SetLeftDelivered(string value) { this.SetDataItemValue(".Delivered.Left", value); }
        public void SetRightDelivered(string value) { this.SetDataItemValue(".Delivered.Right", value); }

        public void SetLeftDifference(string value) { this.SetDataItemValue(".Difference.Left", value); }
        public void SetRightDifference(string value) { this.SetDataItemValue(".Difference.Right", value); }

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
