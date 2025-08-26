using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.ScorePyramid {

    public class Game : _Modules._InsertBase {

        ////	[Path]=.Reset
        ////	[Path]=.SetOut
        ////	[Path]=.ToOut
        ////	[Path]=.SetIn
        ////	[Path]=.ToIn
        ////	[Path]=.Position.X
        ////	[Path]=.Position.Y
        //	[Path]=.LeftPlayer.Name
        //	[Path]=.LeftPlayer.Result
        //	[Path]=.RightPlayer.Name
        //	[Path]=.RightPlayer.Result
        //	[Path]=.Level.Level_00.LeftPlayerHits
        //	[Path]=.Level.Level_00.RightPlayerHits
        //	[Path]=.Level.Level_01.LeftPlayerHits
        //	[Path]=.Level.Level_01.RightPlayerHits
        //	[Path]=.Level.Level_02.LeftPlayerHits
        //	[Path]=.Level.Level_02.RightPlayerHits
        //	[Path]=.Level.Level_03.LeftPlayerHits
        //	[Path]=.Level.Level_03.RightPlayerHits
        //	[Path]=.Level.Level_04.LeftPlayerHits
        //	[Path]=.Level.Level_04.RightPlayerHits
        //	[Path]=.Level.Level_05.LeftPlayerHits
        //	[Path]=.Level.Level_05.RightPlayerHits
        //	[Path]=.Level.Level_06.LeftPlayerHits
        //	[Path]=.Level.Level_06.RightPlayerHits
        //	[Path]=.Level.Level_07.LeftPlayerHits
        //	[Path]=.Level.Level_07.RightPlayerHits
        //	[Path]=.Level.Level_08.LeftPlayerHits
        //	[Path]=.Level.Level_08.RightPlayerHits
        //	[Path]=.Level.Level_09.LeftPlayerHits
        //	[Path]=.Level.Level_09.RightPlayerHits
        //	[Path]=.Level.Level_10.LeftPlayerHits
        //	[Path]=.Level.Level_10.RightPlayerHits

        #region Properties

        private const string sceneID = "project/gamepool/scorepyramid/game";

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

        public void Next() { this.SetDataItemTrigger(".Next"); }
        public void EnableTouch() { this.SetDataItemTrigger(".Touch.Enable"); }
        public void DisableTouch() { this.SetDataItemTrigger(".Touch.Disable"); }

        public void SetLeftPlayerName(string value) { this.SetDataItemValue(".LeftPlayer.Name", value); }
        public void SetLeftPlayerHits(
            int levelIndex,
            int value) {
            string dataItemName = string.Format(".Level.Level_{0}.LeftPlayerHits", levelIndex.ToString("00"));
            this.SetDataItemValue(dataItemName, value);
        }

        public void SetRightPlayerName(string value) { this.SetDataItemValue(".RightPlayer.Name", value); }
        public void SetRightPlayerHits(
            int levelIndex,
            int value) {
            string dataItemName = string.Format(".Level.Level_{0}.RightPlayerHits", levelIndex.ToString("00"));
            this.SetDataItemValue(dataItemName, value);
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
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
