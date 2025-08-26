using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.Fullscreen {

    public class Gameboard : VRemote4.HandlerSi.Scene, INotifyPropertyChanged {

        //	[Path]=.Reset
        //	[Path]=.SetToShow
        //	[Path]=.TopGames.Game_01.SetIn
        //	[Path]=.TopGames.Game_01.SetOut
        //	[Path]=.TopGames.Game_02.SetIn
        //	[Path]=.TopGames.Game_02.SetOut
        //	[Path]=.TopGames.Game_03.SetIn
        //	[Path]=.TopGames.Game_03.SetOut
        //	[Path]=.TopGames.Game_04.SetIn
        //	[Path]=.TopGames.Game_04.SetOut
        //	[Path]=.TopGames.Game_05.SetIn
        //	[Path]=.TopGames.Game_05.SetOut
        //	[Path]=.TopGames.Game_06.SetIn
        //	[Path]=.TopGames.Game_06.SetOut
        //	[Path]=.TopGames.Game_07.SetIn
        //	[Path]=.TopGames.Game_07.SetOut
        //	[Path]=.TopGames.Game_08.SetIn
        //	[Path]=.TopGames.Game_08.SetOut
        //	[Path]=.TopGames.Game_09.SetIn
        //	[Path]=.TopGames.Game_09.SetOut
        //	[Path]=.TopGames.Game_10.SetIn
        //	[Path]=.TopGames.Game_10.SetOut
        //	[Path]=.TopGames.Game_11.SetIn
        //	[Path]=.TopGames.Game_11.SetOut
        //	[Path]=.TopGames.Game_12.SetIn
        //	[Path]=.TopGames.Game_12.SetOut
        //	[Path]=.TopGames.Game_13.SetIn
        //	[Path]=.TopGames.Game_13.SetOut
        //	[Path]=.TopGames.Game_14.SetIn
        //	[Path]=.TopGames.Game_14.SetOut
        //	[Path]=.TopGames.Game_15.SetIn
        //	[Path]=.TopGames.Game_15.SetOut
        //	[Path]=.TopGames.All.SetIn
        //	[Path]=.TopGames.All.SetOut
        //	[Path]=.LeftPlayer.Name
        //	[Path]=.LeftPlayer.Score
        //	[Path]=.LeftPlayer.NewScore
        //	[Path]=.LeftPlayer.Games.Game_01.SetIn
        //	[Path]=.LeftPlayer.Games.Game_01.SetOut
        //	[Path]=.LeftPlayer.Games.Game_02.SetIn
        //	[Path]=.LeftPlayer.Games.Game_02.SetOut
        //	[Path]=.LeftPlayer.Games.Game_03.SetIn
        //	[Path]=.LeftPlayer.Games.Game_03.SetOut
        //	[Path]=.LeftPlayer.Games.Game_04.SetIn
        //	[Path]=.LeftPlayer.Games.Game_04.SetOut
        //	[Path]=.LeftPlayer.Games.Game_05.SetIn
        //	[Path]=.LeftPlayer.Games.Game_05.SetOut
        //	[Path]=.LeftPlayer.Games.Game_06.SetIn
        //	[Path]=.LeftPlayer.Games.Game_06.SetOut
        //	[Path]=.LeftPlayer.Games.Game_07.SetIn
        //	[Path]=.LeftPlayer.Games.Game_07.SetOut
        //	[Path]=.LeftPlayer.Games.Game_08.SetIn
        //	[Path]=.LeftPlayer.Games.Game_08.SetOut
        //	[Path]=.LeftPlayer.Games.Game_09.SetIn
        //	[Path]=.LeftPlayer.Games.Game_09.SetOut
        //	[Path]=.LeftPlayer.Games.Game_10.SetIn
        //	[Path]=.LeftPlayer.Games.Game_10.SetOut
        //	[Path]=.LeftPlayer.Games.Game_11.SetIn
        //	[Path]=.LeftPlayer.Games.Game_11.SetOut
        //	[Path]=.LeftPlayer.Games.Game_12.SetIn
        //	[Path]=.LeftPlayer.Games.Game_12.SetOut
        //	[Path]=.LeftPlayer.Games.Game_13.SetIn
        //	[Path]=.LeftPlayer.Games.Game_13.SetOut
        //	[Path]=.LeftPlayer.Games.Game_14.SetIn
        //	[Path]=.LeftPlayer.Games.Game_14.SetOut
        //	[Path]=.LeftPlayer.Games.Game_15.SetIn
        //	[Path]=.LeftPlayer.Games.Game_15.SetOut
        //	[Path]=.LeftPlayer.Games.All.SetIn
        //	[Path]=.LeftPlayer.Games.All.SetOut
        //	[Path]=.RightPlayer.Name
        //	[Path]=.RightPlayer.Score
        //	[Path]=.RightPlayer.NewScore
        //	[Path]=.RightPlayer.Games.Game_01.SetIn
        //	[Path]=.RightPlayer.Games.Game_01.SetOut
        //	[Path]=.RightPlayer.Games.Game_02.SetIn
        //	[Path]=.RightPlayer.Games.Game_02.SetOut
        //	[Path]=.RightPlayer.Games.Game_03.SetIn
        //	[Path]=.RightPlayer.Games.Game_03.SetOut
        //	[Path]=.RightPlayer.Games.Game_04.SetIn
        //	[Path]=.RightPlayer.Games.Game_04.SetOut
        //	[Path]=.RightPlayer.Games.Game_05.SetIn
        //	[Path]=.RightPlayer.Games.Game_05.SetOut
        //	[Path]=.RightPlayer.Games.Game_06.SetIn
        //	[Path]=.RightPlayer.Games.Game_06.SetOut
        //	[Path]=.RightPlayer.Games.Game_07.SetIn
        //	[Path]=.RightPlayer.Games.Game_07.SetOut
        //	[Path]=.RightPlayer.Games.Game_08.SetIn
        //	[Path]=.RightPlayer.Games.Game_08.SetOut
        //	[Path]=.RightPlayer.Games.Game_09.SetIn
        //	[Path]=.RightPlayer.Games.Game_09.SetOut
        //	[Path]=.RightPlayer.Games.Game_10.SetIn
        //	[Path]=.RightPlayer.Games.Game_10.SetOut
        //	[Path]=.RightPlayer.Games.Game_11.SetIn
        //	[Path]=.RightPlayer.Games.Game_11.SetOut
        //	[Path]=.RightPlayer.Games.Game_12.SetIn
        //	[Path]=.RightPlayer.Games.Game_12.SetOut
        //	[Path]=.RightPlayer.Games.Game_13.SetIn
        //	[Path]=.RightPlayer.Games.Game_13.SetOut
        //	[Path]=.RightPlayer.Games.Game_14.SetIn
        //	[Path]=.RightPlayer.Games.Game_14.SetOut
        //	[Path]=.RightPlayer.Games.Game_15.SetIn
        //	[Path]=.RightPlayer.Games.Game_15.SetOut
        //	[Path]=.RightPlayer.Games.All.SetIn
        //	[Path]=.RightPlayer.Games.All.SetOut
        //	[Path]=.SelectedGame.ID
        //	[Path]=.SelectedGame.MoveToLeft
        //	[Path]=.SelectedGame.MoveToRight
        //	[Path]=.SelectedGame.MoveFinished

        #region Properties

        private const string sceneID = "project/fullscreen/gameboard";

        public VRemote4.HandlerSi.Port GamePort;

        #endregion


        #region Funktionen

        public Gameboard(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode = Modes.Static, string scene = sceneID)
            : base(syncContext, port, scene, mode) {
        }

        public Gameboard(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe, string scene = sceneID)
            : base(syncContext, pipe, scene) {
        }

        private void init() {      }

        public void SetAllTopGamesIn() { this.SetDataItemTrigger(".TopGames.All.SetIn"); }
        public void SetAllTopGamesOut() { this.SetDataItemTrigger(".TopGames.All.SetOut"); }
        public void SetTopGameIn(
            int id) {
            string name = string.Format(".TopGames.Game_{0}.SetIn", id.ToString("00"));
            this.SetDataItemTrigger(name);
        }
        public void SetTopGameOut(
            int id) {
            string name = string.Format(".TopGames.Game_{0}.SetOut", id.ToString("00"));
            this.SetDataItemTrigger(name);
        }

        public void SetLeftPlayerName(string value) { this.SetDataItemValue(".LeftPlayer.Name", value); }
        public void SetLeftPlayerScore(ushort value) { this.SetDataItemValue(".LeftPlayer.Score", value); }
        public void SetLeftPlayerNewScore(ushort value) { this.SetDataItemValue(".LeftPlayer.NewScore", value); }
        public void SetAllLeftPlayerGamesIn() { this.SetDataItemTrigger(".LeftPlayer.Games.All.SetIn"); }
        public void SetAllLeftPlayerGamesOut() { this.SetDataItemTrigger(".LeftPlayer.Games.All.SetOut"); }
        public void SetLeftPlayerGameIn(
            int id) {
            string name = string.Format(".LeftPlayer.Games.Game_{0}.SetIn", id.ToString("00"));
            this.SetDataItemTrigger(name);
        }
        public void SetLeftPlayerGameOut(
            int id) {
            string name = string.Format(".LeftPlayer.Games.Game_{0}.SetOut", id.ToString("00"));
            this.SetDataItemTrigger(name);
        }

        public void SetRightPlayerName(string value) { this.SetDataItemValue(".RightPlayer.Name", value); }
        public void SetRightPlayerScore(ushort value) { this.SetDataItemValue(".RightPlayer.Score", value); }
        public void SetRightPlayerNewScore(ushort value) { this.SetDataItemValue(".RightPlayer.NewScore", value); }
        public void SetAllRightPlayerGamesIn() { this.SetDataItemTrigger(".RightPlayer.Games.All.SetIn"); }
        public void SetAllRightPlayerGamesOut() { this.SetDataItemTrigger(".RightPlayer.Games.All.SetOut"); }
        public void SetRightPlayerGameIn(
            int id) {
            string name = string.Format(".RightPlayer.Games.Game_{0}.SetIn", id.ToString("00"));
            this.SetDataItemTrigger(name);
        }
        public void SetRightPlayerGameOut(
            int id) {
            string name = string.Format(".RightPlayer.Games.Game_{0}.SetOut", id.ToString("00"));
            this.SetDataItemTrigger(name);
        }

        public void SetSelectedGameID(int value) { this.SetDataItemValue(".SelectedGame.ID", value); }
        public void MoveSelectedGameToLeft() { this.SetDataItemTrigger(".SelectedGame.MoveToLeft"); }
        public void MoveSelectedGameToRight() { this.SetDataItemTrigger(".SelectedGame.MoveToRight"); }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        //Setzt das Zählwerk wieder auf den Score (nicht den NewScore)
        public void SetToShow() { this.SetDataItemTrigger(".SetToShow"); }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            base.dataItem_TriggerReceived(sender, e);
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".SelectedGame.MoveFinished") this.on_GameMoveFinished(this, new EventArgs());
            }
        }

        #endregion


        #region Events.Outgoing

        public event EventHandler GameMoveFinished;
        protected void on_GameMoveFinished(object sender, EventArgs e) { Helper.raiseEvent(sender, this.GameMoveFinished, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }
}
