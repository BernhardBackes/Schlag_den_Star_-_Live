using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TwelveIssuesTimerScore {

    public class Host : _Base {


        //	[Path]=.Reset
        //	[Path]=.SetOut
        //	[Path]=.ToOut
        //	[Path]=.ToIn
        //	[Path]=.SetIn
        //	[Path]=.Issue_01.Reset.Invoke
        //	[Path]=.Issue_01.SetIn.Invoke
        //	[Path]=.Issue_01.SetOut.Invoke
        //	[Path]=.Issue_01.Text.Value
        //	[Path]=.Issue_01.ToIn.Invoke
        //	[Path]=.Issue_01.ToOut.Invoke
        //	[Path]=.Issue_02.Reset.Invoke
        //	...
        //	[Path]=.Issue_12.ToOut.Invoke

        #region Properties

        private const string sceneID = "project/gamepool/twelveissuestimerscore/host";

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

        public void SetIssueText(int id, string value) { this.SetDataItemValue(string.Format(".Issue_{0}.Text.Value", id.ToString("00")), value); }


        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void ResetIssue(int id) { this.SetDataItemTrigger(string.Format(".Issue_{0}.Reset.Invoke", id.ToString("00"))); }
        public void SetIssueOut(int id) { this.SetDataItemTrigger(string.Format(".Issue_{0}.SetOut.Invoke", id.ToString("00"))); }
        public void IssueToOut(int id) { this.SetDataItemTrigger(string.Format(".Issue_{0}.ToOut.Invoke", id.ToString("00"))); }
        public void SetIssueIn(int id) { this.SetDataItemTrigger(string.Format(".Issue_{0}.SetIn.Invoke", id.ToString("00"))); }
        public void IssueToIn(int id) { this.SetDataItemTrigger(string.Format(".Issue_{0}.ToIn.Invoke", id.ToString("00"))); }

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
