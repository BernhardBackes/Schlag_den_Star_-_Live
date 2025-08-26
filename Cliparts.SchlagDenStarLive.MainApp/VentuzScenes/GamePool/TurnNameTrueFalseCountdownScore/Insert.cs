using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TurnNameTrueFalseCountdownScore {

    public class Insert : _Modules._InsertBase {

        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        //	[Path]=.ShowCounter
        //	[Path]=.Resolve.SetGreen
        //	[Path]=.Resolve.SetRed
        //	[Path]=.Rotate
        //	[Path]=.Counter
        //	[Path]=.TopText
        //	[Path]=.TaskFirstname
        //	[Path]=.SolutionFirstname
        //	[Path]=.Surname
        //	[Path]=.Jingles.PlayPling
        //	[Path]=.Jingles.PlayResolve

        #region Properties

        private const string sceneID = "project/gamepool/turnnametruefalsecountdownscore/insert";

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


        public void ResolveGreen() { this.SetDataItemTrigger(".Resolve.SetGreen"); }
        public void ResolveRed() { this.SetDataItemTrigger(".Resolve.SetRed"); }
        public void Rotate() { this.SetDataItemTrigger(".Rotate"); }
        public void PlayPling() { this.SetDataItemTrigger(".Jingles.PlayPling"); }
        public void PlayResolve() { this.SetDataItemTrigger(".Jingles.PlayResolve"); }

        public void SetCounterValue(int value) { this.SetDataItemValue(".Counter", value); }
        public void SetShowCounter(bool value) { this.SetDataItemValue(".ShowCounter", value); }

        public void SetTopText(string value) { this.SetDataItemValue(".TopText", value); }
        public void SetTaskFirstname(string value) { this.SetDataItemValue(".TaskFirstname", value); }
        public void SetSolutionFirstname(string value) { this.SetDataItemValue(".SolutionFirstname", value); }
        public void SetSurname(string value) { this.SetDataItemValue(".Surname", value); }

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
