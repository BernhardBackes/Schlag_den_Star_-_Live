using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TwoTimerStartStopScore {

    public class Insert : _Base {

        //	[Path]=.Reset
        //	[Path]=.SetOut
        //	[Path]=.ToOut
        //	[Path]=.SetIn
        //	[Path]=.ToIn
        //	[Path]=.Position.X
        //	[Path]=.Position.Y
        //	[Path]=.Left.Name
        //	[Path]=.Left.Time
        //	[Path]=.Left.Counter
        //	[Path]=.Left.ResetCounter
        //	[Path]=.Right.Name
        //	[Path]=.Right.Time
        //	[Path]=.Right.Counter
        //	[Path]=.Right.ResetCounter

        #region Properties

        private const string sceneID = "project/gamepool/twotimerstartstopscore/insert";

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

        public void Reset() { this.SetDataItemTrigger(".Reset"); }
        public void Setut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void SetPositionX(int value) { this.SetDataItemValue(".Position.X", value); }
        public void SetPositionY(int value) { this.SetDataItemValue(".Position.Y", value); }

        public void SetLeftName(string value) { this.SetDataItemValue(".Left.Name", value); }
        public void SetLeftTime(float value) { this.SetDataItemValue(".Left.Time", value); }
        public void SetLeftCounter(int value) { this.SetDataItemValue(".Left.Counter", value); }
        public void ResetLeftCounter() {
            this.SetDataItemTrigger(".Left.ResetCounter");
            this.SetLeftCounter(0);
        }

        public void SetRightName(string value) { this.SetDataItemValue(".Right.Name", value); }
        public void SetRightTime(float value) { this.SetDataItemValue(".Right.Time", value); }
        public void SetRightCounter(int value) { this.SetDataItemValue(".Right.Counter", value); }
        public void ResetRightCounter() {
            this.SetDataItemTrigger(".Right.ResetCounter");
            this.SetRightCounter(0);
        }
        public override void Dispose() {
            base.Dispose();
            this.Score.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.Score.Clear();
            this.ToOut();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
