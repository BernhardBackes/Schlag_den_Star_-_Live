using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules {

    public class CounterScore : _InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.Style
        //	[Path]=.FlipPosition
        //	[Path]=.Top.Name
        //	[Path]=.Top.Score
        //	[Path]=.Top.Counter
        //	[Path]=.Bottom.Name
        //	[Path]=.Bottom.Score
        //	[Path]=.Bottom.Counter

        public enum Styles { ZeroDots, ThreeDots, FourDots, FiveDots }

        #region Properties     
   
        private const string sceneID = "project/gamepool/_modules/counterscore";

        #endregion


        #region Funktionen

        public CounterScore(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID) {
        }

        public CounterScore(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetStyle(Styles value) { this.SetDataItemValue(".Style", value); }

        public void SetTopName(string value) { this.SetDataItemValue(".Top.Name", value); }
        public void SetTopScore(int value) { this.SetDataItemValue(".Top.Score", value); }
        public void SetTopCounter(int value) { this.SetDataItemValue(".Top.Counter", value); }
        public void SetTopCounterIsVisible(bool value) { this.SetDataItemValue(".Top.CounterIsVisible", value); }

        public void SetBottomName(string value) { this.SetDataItemValue(".Bottom.Name", value); }
        public void SetBottomScore(int value) { this.SetDataItemValue(".Bottom.Score", value); }
        public void SetBottomCounter(int value) { this.SetDataItemValue(".Bottom.Counter", value); }
        public void SetBottomCounterIsVisible(bool value) { this.SetDataItemValue(".Bottom.CounterIsVisible", value); }

        public void SetFlipPosition(bool value) { this.SetDataItemValue(".FlipPosition", value); }

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
