using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.AddEstimations {

    //	[Path]=.Reset
    //	[Path]=.SetOut
    //	[Path]=.ToOut
    //	[Path]=.SetIn
    //	[Path]=.ToIn
    //	[Path]=.Question_1.HostText
    //	[Path]=.Question_1.Solution
    //	[Path]=.Question_1.LeftPlayer
    //	[Path]=.Question_1.RightPlayer
    //	[Path]=.Question_2.HostText
    //	[Path]=.Question_2.Solution
    //	[Path]=.Question_2.LeftPlayer
    //	[Path]=.Question_2.RightPlayer
    //	[Path]=.Question_3.HostText
    //	[Path]=.Question_3.Solution
    //	[Path]=.Question_3.LeftPlayer
    //	[Path]=.Question_3.RightPlayer

    public class Host : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/addestimations/host";

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

        public void SetHostText(int questionID, string value) { this.SetDataItemValue(string.Format(".Question_{0}.HostText", questionID.ToString()), value); }
        public void SetSolution(int questionID, int value) { this.SetDataItemValue(string.Format(".Question_{0}.Solution", questionID.ToString()), value); }
        public void SetLeftPlayer(int questionID, int value) { this.SetDataItemValue(string.Format(".Question_{0}.LeftPlayer", questionID.ToString()), value); }
        public void SetRightPlayer(int questionID, int value) { this.SetDataItemValue(string.Format(".Question_{0}.RightPlayer", questionID.ToString()), value); }

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
