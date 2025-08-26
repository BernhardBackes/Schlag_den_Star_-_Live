using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TargetValueCounterScore {

    public class Insert : GamePool._Modules._InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.Target
        //	[Path]=.Top.Name
        //	[Path]=.Top.Value
        //	[Path]=.Top.Counter
        //	[Path]=.Bottom.Name
        //	[Path]=.Bottom.Value
        //	[Path]=.Bottom.Counter

        #region Properties

        private const string sceneID = "project/gamepool/targetvaluecounterscore/insert";

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

        public void SetTarget(int value) { this.SetDataItemValue(".Target", value); }

        public void SetTopName(string value) { this.SetDataItemValue(".Top.Name", value); }
        public void SetTopValue(int value) { this.SetDataItemValue(".Top.Value", value); }
        public void SetTopCounter(int value) { this.SetDataItemValue(".Top.Counter", value); }

        public void SetBottomName(string value) { this.SetDataItemValue(".Bottom.Name", value); }
        public void SetBottomValue(int value) { this.SetDataItemValue(".Bottom.Value", value); }
        public void SetBottomCounter(int value) { this.SetDataItemValue(".Bottom.Counter", value); }

        public override void Dispose() {
            base.Dispose();
            this.Score.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.Score.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
