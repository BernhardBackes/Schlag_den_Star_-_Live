using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.AudienceVoting {

    public class Game : _Modules._InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.Top.Name
        //	[Path]=.Top.Value
        //	[Path]=.Bottom.Name
        //	[Path]=.StartBars
        //	[Path]=.ShowNames

        #region Properties

        private const string sceneID = "project/gamepool/audiencevoting/game";

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

        internal void SetLeftPlayerName(string value) { this.SetDataItemValue(".Top.Name", value); }
        internal void SetLeftPlayerValue(int value) { this.SetDataItemValue(".Top.Value", value); }
        internal void SetRightPlayerName(string value) { this.SetDataItemValue(".Bottom.Name", value); }
        internal void StartBars() { this.SetDataItemTrigger(".StartBars"); }
        internal void ShowNames() { this.SetDataItemTrigger(".ShowNames"); }

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
