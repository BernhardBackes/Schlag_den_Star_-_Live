using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.PercentageDivision {

    public enum PlayerSelection { NotSelected, LeftPlayer, RightPlayer, BothPlayers }

    public class Host : _Base {

        //	[Path]=.MainFader.Reset
        //	[Path]=.MainFader.SetOut
        //	[Path]=.MainFader.ToOut
        //	[Path]=.MainFader.SetIn
        //	[Path]=.MainFader.ToIn
        //	[Path]=.LeftPlayer.A
        //	[Path]=.LeftPlayer.Show
        //	[Path]=.LeftPlayer.Hide
        //	[Path]=.RightPlayer.A
        //	[Path]=.RightPlayer.Show
        //	[Path]=.RightPlayer.Hide
        //	[Path]=.BestPlayer

        #region Properties

        private const string sceneID = "project/gamepool/percentagedivision/host";

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

        public void Reset() { this.SetDataItemTrigger(".MainFader.Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".MainFader.SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".MainFader.ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".MainFader.SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".MainFader.ToIn"); }

        public void SetHostText(string value) { this.SetDataItemValue(".Host.Text", value); }
        public void ShowHostText() { this.SetDataItemTrigger(".Host.Show"); }
        public void HideHostText() { this.SetDataItemTrigger(".Host.Hide"); }

        public void SetLeftPlayerInputA(int value) { this.SetDataItemValue(".LeftPlayer.A", value); }
        public void ShowLeftPlayer() { this.SetDataItemTrigger(".LeftPlayer.Show"); }
        public void HideLeftPlayer() { this.SetDataItemTrigger(".LeftPlayer.Hide"); }

        public void SetRightPlayerInputA(int value) { this.SetDataItemValue(".RightPlayer.A", value); }
        public void ShowRightPlayer() { this.SetDataItemTrigger(".RightPlayer.Show"); }
        public void HideRightPlayer() { this.SetDataItemTrigger(".RightPlayer.Hide"); }

        public void SetBestPlayer(PlayerSelection value) { this.SetDataItemValue(".BestPlayer", value); }

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
