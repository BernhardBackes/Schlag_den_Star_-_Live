using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.WordByWordTimerScore {

    public class Game : _Modules._InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.Text.Value
        //	[Path]=.Text.Reset
        //	[Path]=.Text.Start
        //	[Path]=.Text.NextWord
        //	[Path]=.Text.Fail
        //	[Path]=.Text.Pass
        //	[Path]=.Text.Completed

        #region Properties

        private const string sceneID = "project/gamepool/wordbywordtimerscore/game";

        #endregion


        #region Funktionen

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port)
            : base(syncContext, port, sceneID, Modes.Static) {
        }

        public Game(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
        }

        public override void Dispose() {
            base.Dispose();
        }

        public void ResetText() { this.SetDataItemTrigger(".Text.Reset"); }
        public void StartText() { this.SetDataItemTrigger(".Text.Start"); }
        public void NextWord() { this.SetDataItemTrigger(".Text.NextWord"); }
        public void Fail() { this.SetDataItemTrigger(".Text.Fail"); }
        public void Pass() { this.SetDataItemTrigger(".Text.Pass"); }
        public void SetText(string value) { this.SetDataItemValue(".Text.Value", value); }

        #endregion


        #region Events.Outgoing

        public event EventHandler Completed;

        private void on_Completed(object sender, EventArgs e) { Helper.raiseEvent(sender, this.Completed, e); }

        #endregion

        #region Events.Incoming

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Text.Completed") this.on_Completed(this, new EventArgs());
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs &&
                e.Value != null) {
                //if (e.Path == ".Audio.Duration") { this.Duration = Convert.ToInt32(e.Value); }
                //else if (e.Path == ".Audio.Remaining") { this.Remaining = Convert.ToInt32(e.Value); }
            }
        }

        #endregion

    }
}
