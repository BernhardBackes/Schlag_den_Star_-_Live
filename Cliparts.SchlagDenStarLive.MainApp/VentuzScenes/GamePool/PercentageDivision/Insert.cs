using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.PercentageDivision {

    public class Insert : _Base {

        //	[Path]=.Style
        //	[Path]=.MainFader.Reset
        //	[Path]=.MainFader.SetOut
        //	[Path]=.MainFader.ToOut
        //	[Path]=.MainFader.SetIn
        //	[Path]=.MainFader.ToIn
        //	[Path]=.Position.X
        //	[Path]=.Position.Y
        //	[Path]=.LeftPlayer.Score
        //	[Path]=.LeftPlayer.A
        //	[Path]=.RightPlayer.Score
        //	[Path]=.RightPlayer.A
        //	[Path]=.Values.SetOut
        //	[Path]=.Values.ToIn
        //	[Path]=.Winner.Reset
        //	[Path]=.Winner.ToLeft
        //	[Path]=.Winner.ToRight
        //	[Path]=.Winner.ToBoth

        public enum Styles { FourDots, FiveDots, SixDots }

        #region Properties

        private const string sceneID = "project/gamepool/percentagedivision/insert";

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

        public override void Dispose() {
            base.Dispose();
            this.Score.Dispose();
        }

        public void SetStyle(Styles value) { this.SetDataItemValue(".Style", value); }

        public void Reset() { this.SetDataItemTrigger(".MainFader.Reset"); }

        public void SetBorderOut() { this.SetDataItemTrigger(".MainFader.SetOut"); }
        public void BorderToOut() { this.SetDataItemTrigger(".MainFader.ToOut"); }
        public void SetBorderIn() { this.SetDataItemTrigger(".MainFader.SetIn"); }
        public void BorderToIn() { this.SetDataItemTrigger(".MainFader.ToIn"); }

        public void SetBorderPositionX(int value) { this.SetDataItemValue(".Position.X", value); }
        public void SetBorderPositionY(int value) { this.SetDataItemValue(".Position.Y", value); }

        public void SetValuesOut() { this.SetDataItemTrigger(".Values.SetOut"); }
        public void ValuesToIn() { this.SetDataItemTrigger(".Values.ToIn"); }

        public void SetBorderLeftScore(int value) { this.SetDataItemValue(".LeftPlayer.Score", value); }
        public void SetLeftPlayerA(int value) { this.SetDataItemValue(".LeftPlayer.A", value); }

        public void SetBorderRightScore(int value) { this.SetDataItemValue(".RightPlayer.Score", value); }
        public void SetRightPlayerA(int value) { this.SetDataItemValue(".RightPlayer.A", value); }

        public void ResetWinner() { this.SetDataItemTrigger(".Winner.Reset"); }
        public void SetWinnerLeft() { this.SetDataItemTrigger(".Winner.ToLeft"); }
        public void SetWinnerRight() { this.SetDataItemTrigger(".Winner.ToRight"); }
        public void SetWinnerBoth() { this.SetDataItemTrigger(".Winner.ToBoth"); }

        public override void Clear() {
            base.Clear();
            this.Score.Clear();
            this.BorderToOut();
            this.SetValuesOut();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
