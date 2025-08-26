using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.Player {

    public enum SelectedPlayer {
        Left,
        Right
    }

    public class Business : VRemote4.HandlerSi.Scene, INotifyPropertyChanged {

        //	[Path]=.SelectedPlayer
        //	[Path]=.StartIdentification
        //	[Path]=.MainFader.Reset
        //	[Path]=.MainFader.In
        //	[Path]=.MainFader.Out
        //	[Path]=.LeftPlayer.Name
        //	[Path]=.LeftPlayer.TotalScore
        //	[Path]=.LeftPlayer.GameScore
        //	[Path]=.RightPlayer.Name
        //	[Path]=.RightPlayer.TotalScore
        //	[Path]=.RightPlayer.GameScore
        //	[Path]=.Buzzer.Reset
        //	[Path]=.Buzzer.Out
        //	[Path]=.Buzzer.LeftPlayer
        //	[Path]=.Buzzer.RightPlayer
        //	[Path]=.LiveVideo.Reset
        //	[Path]=.LiveVideo.In
        //	[Path]=.LiveVideo.Out
        //	[Path]=.ShowGameScore.Reset
        //	[Path]=.ShowGameScore.In
        //	[Path]=.ShowGameScore.Out
        //	[Path]=.Clear
        //	[Path]=.FlipColor


        #region Properties

        private const string sceneID = "project/player";

        public VRemote4.HandlerSi.Port GamePort;

        #endregion


        #region Funktionen

        public Business(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Business client,
            int pipeIndex)
            : base(syncContext, client, pipeIndex, sceneID) {
            this.init();
        }

        private void init() {
            this.GamePort = this.addPort("GameLayer");
        }

        public void SetSelectedPlayer(SelectedPlayer value) { this.SetDataItemValue(".SelectedPlayer", value); }
        public void SetFlipColor(bool value) { this.SetDataItemValue(".FlipColor", value); }

        public void FadeIn() { this.SetDataItemTrigger(".MainFader.In"); }
        public void FadeOut() { this.SetDataItemTrigger(".MainFader.Out"); }

        public void GameScoreIn() { this.SetDataItemTrigger(".ShowGameScore.In"); }
        public void GameScoreOut() { this.SetDataItemTrigger(".ShowGameScore.Out"); }

        public void LiveVideoIn() { this.SetDataItemTrigger(".LiveVideo.In"); }
        public void LiveVideoOut() { this.SetDataItemTrigger(".LiveVideo.Out"); }

        public void SetLeftPlayerName(string value) { this.SetDataItemValue(".LeftPlayer.Name", value); }
        public void SetLeftPlayerTotalScore(ushort value) { this.SetDataItemValue(".LeftPlayer.TotalScore", value); }
        public void SetLeftPlayerGameScore(int value) { this.SetDataItemValue(".LeftPlayer.GameScore", value); }

        public void SetRightPlayerName(string value) { this.SetDataItemValue(".RightPlayer.Name", value); }
        public void SetRightPlayerTotalScore(ushort value) { this.SetDataItemValue(".RightPlayer.TotalScore", value); }
        public void SetRightPlayerGameScore(int value) { this.SetDataItemValue(".RightPlayer.GameScore", value); }

        public void ResetBuzzer() { this.SetDataItemTrigger(".Buzzer.Reset"); }
        public void SetBuzzerOut() { this.SetDataItemTrigger(".Buzzer.Out"); }
        public void SetBuzzerLeft() { this.SetDataItemTrigger(".Buzzer.LeftPlayer"); }
        public void SetBuzzerRight() { this.SetDataItemTrigger(".Buzzer.RightPlayer"); }

        public void Clear() { this.SetDataItemTrigger(".Clear"); }


        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
