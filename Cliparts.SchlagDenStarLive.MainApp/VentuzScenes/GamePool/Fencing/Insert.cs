using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.Fencing {

    public class Insert : _Base {

        //	[Path]=.Reset
        //	[Path]=.SetOut
        //	[Path]=.ToOut
        //	[Path]=.SetIn
        //	[Path]=.ToIn
        //	[Path]=.Position.X
        //	[Path]=.Position.Y
        //	[Path]=.Left.Name
        //	[Path]=.Left.Score
        //	[Path]=.Left.On
        //	[Path]=.Left.Off
        //	[Path]=.Right.Name
        //	[Path]=.Right.Score
        //	[Path]=.Right.On
        //	[Path]=.Right.Off

        #region Properties     
   
        private const string sceneID = "project/gamepool/fencing/insert";

        #endregion


        #region Funktionen

        public Insert(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
        }

        public Insert(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetPositionX(int value) { this.SetDataItemValue(".Position.X", value); }
        public void SetPositionY(int value) { this.SetDataItemValue(".Position.Y", value); }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void SetLeftName(string value) { this.SetDataItemValue(".Left.Name", value); }
        public void SetLeftScore(int value) { this.SetDataItemValue(".Left.Score", value); }
        public void SetLeftOn() { this.SetDataItemTrigger(".Left.On"); }
        public void SetLeftOff() { this.SetDataItemTrigger(".Left.Off"); }

        public void SetRightName(string value) { this.SetDataItemValue(".Right.Name", value); }
        public void SetRightScore(int value) { this.SetDataItemValue(".Right.Score", value); }
        public void SetRightOn() { this.SetDataItemTrigger(".Right.On"); }
        public void SetRightOff() { this.SetDataItemTrigger(".Right.Off"); }

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
