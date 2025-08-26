using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MemoCourt {

    public class Insert : _Base {

        //	[Path]=.Sequence.Position.X
        //	[Path]=.Sequence.Position.Y
        //	[Path]=.Sequence.SetOut
        //	[Path]=.Sequence.ToOut
        //	[Path]=.Sequence.SetIn
        //	[Path]=.Sequence.ToIn
        //	[Path]=.Sequence.Value
        //	[Path]=.Sequence.HighlightID
        //	[Path]=.Sequence.ResetHighlight
        //	[Path]=.Counter.Position.X
        //	[Path]=.Counter.Position.Y
        //	[Path]=.Counter.SetOut
        //	[Path]=.Counter.ToOut
        //	[Path]=.Counter.SetIn
        //	[Path]=.Counter.ToIn
        //	[Path]=.Counter.Value
        //	[Path]=.Counter.Reference
        //	[Path]=.Counter.ShowReference
        //	[Path]=.Jingles.PlayBad
        //	[Path]=.Jingles.PlayGood
        //	[Path]=.Jingles.PlayScore
        //	[Path]=.Jingles.PlayEnd

        #region Properties

        private const string sceneID = "project/gamepool/memocourt/insert";

        public _Modules.Timer Timer;

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
            this.Timer = new _Modules.Timer(syncContext, this.addPort("TimerLayer"));
        }


        public void SetSequencePositionX(int value) { this.SetDataItemValue(".Sequence.Position.X", value); }
        public void SetSequencePositionY(int value) { this.SetDataItemValue(".Sequence.Position.Y", value); }
        public void SetSequenceValue(string value) { this.SetDataItemValue(".Sequence.Value", value); }
        public void SetSequenceHighlightID(int value) { this.SetDataItemValue(".Sequence.HighlightID", value); }

        public void SetSequenceOut() { this.SetDataItemTrigger(".Sequence.SetOut"); }
        public void SequenceToOut() { this.SetDataItemTrigger(".Sequence.ToOut"); }
        public void SetSequenceIn() { this.SetDataItemTrigger(".Sequence.SetIn"); }
        public void SequenceToIn() { this.SetDataItemTrigger(".Sequence.ToIn"); }
        public void SequenceResetHighlight() { this.SetDataItemTrigger(".Sequence.ResetHighlight"); }

        public void SetCounterPositionX(int value) { this.SetDataItemValue(".Counter.Position.X", value); }
        public void SetCounterPositionY(int value) { this.SetDataItemValue(".Counter.Position.Y", value); }
        public void SetCounterValue(int value) { this.SetDataItemValue(".Counter.Value", value); }
        public void SetCounterReference(int value) { this.SetDataItemValue(".Counter.Reference", value); }
        public void SetCounterShowReference(bool value) { this.SetDataItemValue(".Counter.ShowReference", value); }

        public void SetCounterOut() { this.SetDataItemTrigger(".Counter.SetOut"); }
        public void CounterToOut() { this.SetDataItemTrigger(".Counter.ToOut"); }
        public void SetCounterIn() { this.SetDataItemTrigger(".Counter.SetIn"); }
        public void CounterToIn() { this.SetDataItemTrigger(".Counter.ToIn"); }

        public void PlayJingleBad() { this.SetDataItemTrigger(".Jingles.PlayBad"); }
        public void PlayJingleGood() { this.SetDataItemTrigger(".Jingles.PlayGood"); }
        public void PlayJingleScore() { this.SetDataItemTrigger(".Jingles.PlayScore"); }
        public void PlayJingleEnd() { this.SetDataItemTrigger(".Jingles.PlayEnd"); }

        public override void Dispose() {
            base.Dispose();
            this.Timer.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.Timer.Clear();
            this.CounterToOut();
            this.SequenceToOut();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
