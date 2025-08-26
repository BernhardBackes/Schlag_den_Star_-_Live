using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.ContactDMXScore {

    public class Insert : _Base {

        //	[Path]=.Reset
        //	[Path]=.SetOut
        //	[Path]=.ToOut
        //	[Path]=.SetIn
        //	[Path]=.ToIn
        //	[Path]=.Buzzer.ResetLeft
        //	[Path]=.Buzzer.SetLeft
        //	[Path]=.Buzzer.PlayLeftJingle
        //	[Path]=.Buzzer.ResetRight
        //	[Path]=.Buzzer.SetRight
        //	[Path]=.Buzzer.PlayRightJingle

        #region Properties

        private const string sceneID = "project/gamepool/contactdmxscore/insert";

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

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void ResetLeftBuzzer() { this.SetDataItemTrigger(".Buzzer.ResetLeft"); }
        public void SetBuzzerLeft() { this.SetDataItemTrigger(".Buzzer.SetLeft"); }
        public void ResetRightBuzzer() { this.SetDataItemTrigger(".Buzzer.ResetRight"); }
        public void SetBuzzerRight() { this.SetDataItemTrigger(".Buzzer.SetRight"); }

        public void PlayJingleBuzzerLeft() { this.SetDataItemTrigger(".Buzzer.PlayLeftJingle"); }
        public void PlayJingleBuzzerRight() { this.SetDataItemTrigger(".Buzzer.PlayRightJingle"); }

        public override void Dispose() {
            base.Dispose();
            this.Score.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.ToOut();
            this.Score.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
