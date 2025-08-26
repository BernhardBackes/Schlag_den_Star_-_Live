using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.LettersCount {

    public class Insert : _Modules._InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.SetToEnd
        //	[Path]=.SetBack
        //	[Path]=.Start
        //	[Path]=.Text
        //	[Path]=.Letters
        //	[Path]=.Counter.Reset
        //	[Path]=.Counter.SetOut
        //	[Path]=.Counter.ToOut
        //	[Path]=.Counter.SetIn
        //	[Path]=.Counter.ToIn

        #region Properties

        private const string sceneID = "project/gamepool/letterscount/insert";

        public _Modules.Score Score;
        public _Modules.Timeout Timeout;

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
            this.Timeout = new _Modules.Timeout(syncContext, this.addPort("TimeoutLayer"));
        }

        public void SetText(string value) { this.SetDataItemValue(".Text", value); }
        public void SetLetters(string value) { this.SetDataItemValue(".Letters", value); }

        public void StartTextAnimation() { this.SetDataItemTrigger(".Start"); }
        public void SetTextAnimationToEnd() { this.SetDataItemTrigger(".SetToEnd"); }
        public void SetTextAnimationBack() { this.SetDataItemTrigger(".SetBack"); }

        public void SetCounterOut() { this.SetDataItemTrigger(".Counter.SetOut"); }
        public void CounterToOut() { this.SetDataItemTrigger(".Counter.ToOut"); }
        public void SetCounterIn() { this.SetDataItemTrigger(".Counter.SetIn"); }
        public void CounterToIn() { this.SetDataItemTrigger(".Counter.ToIn"); }

        public override void Dispose() {
            base.Dispose();
            this.Score.Dispose();
            this.Timeout.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.Score.Clear();
            this.Timeout.Stop();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
