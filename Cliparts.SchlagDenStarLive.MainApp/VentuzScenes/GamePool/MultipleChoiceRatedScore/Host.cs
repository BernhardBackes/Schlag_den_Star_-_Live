using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MultipleChoiceRatedScore {

    public class Host : VRemote4.HandlerSi.Scene, INotifyPropertyChanged {

        //	[Path]=.LeftPlayer.Name
        //	[Path]=.LeftPlayer.TotalScore
        //	[Path]=.LeftPlayer.GameScore
        //	[Path]=.RightPlayer.Name
        //	[Path]=.RightPlayer.TotalScore
        //	[Path]=.RightPlayer.GameScore
        //	[Path]=.Clear
        //	[Path]=.FlipColor
        //	[Path]=.Game.Reset
        //	[Path]=.Game.In
        //	[Path]=.Game.Out
        //	[Path]=.Game.Text
        //	[Path]=.Game.Answers.Answer_A.Text
        //	[Path]=.Game.Answers.Answer_A.IsSolution
        //	[Path]=.Game.Answers.Answer_A.InputLeft
        //	[Path]=.Game.Answers.Answer_A.InputRight
        //	[Path]=.Game.Answers.Answer_B.Text
        //	[Path]=.Game.Answers.Answer_B.IsSolution
        //	[Path]=.Game.Answers.Answer_B.InputLeft
        //	[Path]=.Game.Answers.Answer_B.InputRight
        //	[Path]=.Game.Answers.Answer_C.Text
        //	[Path]=.Game.Answers.Answer_C.IsSolution
        //	[Path]=.Game.Answers.Answer_C.InputLeft
        //	[Path]=.Game.Answers.Answer_C.InputRight
        //	[Path]=.Game.Answers.Answer_D.Text
        //	[Path]=.Game.Answers.Answer_D.IsSolution
        //	[Path]=.Game.Answers.Answer_D.InputLeft
        //	[Path]=.Game.Answers.Answer_D.InputRight

        #region Properties

        private const string sceneID = "project/gamepool/multiplechoiceratedscore/host";

        #endregion


        #region Funktionen

        public Host(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        private void init() {
        }

        public void SetLeftPlayerName(string value) { this.SetDataItemValue(".LeftPlayer.Name", value); }
        public void SetLeftPlayerTotalScore(ushort value) { this.SetDataItemValue(".LeftPlayer.TotalScore", value); }
        public void SetLeftPlayerGameScore(int value) { this.SetDataItemValue(".LeftPlayer.GameScore", value); }

        public void SetRightPlayerName(string value) { this.SetDataItemValue(".RightPlayer.Name", value); }
        public void SetRightPlayerTotalScore(ushort value) { this.SetDataItemValue(".RightPlayer.TotalScore", value); }
        public void SetRightPlayerGameScore(int value) { this.SetDataItemValue(".RightPlayer.GameScore", value); }

        public void SetFlipColor(bool value) { this.SetDataItemValue(".FlipColor", value); }

        public void ResetGame() { this.SetDataItemTrigger(".Game.Reset"); }
        public void GameToIn() { this.SetDataItemTrigger(".Game.In"); }
        public void GameToOut() { this.SetDataItemTrigger(".Game.Out"); }

        public void SetText(string value) { this.SetDataItemValue(".Game.Text", value); }

        public void SetAnswerAText(string value) { this.SetDataItemValue(".Game.Answers.Answer_A.Text", value); }
        public void SetAnswerAIsSolution(bool value) { this.SetDataItemValue(".Game.Answers.Answer_A.IsSolution", value); }
        public void SetAnswerAInputLeft(string value) { this.SetDataItemValue(".Game.Answers.Answer_A.InputLeft", value); }
        public void SetAnswerAInputRight(string value) { this.SetDataItemValue(".Game.Answers.Answer_A.InputRight", value); }

        public void SetAnswerBText(string value) { this.SetDataItemValue(".Game.Answers.Answer_B.Text", value); }
        public void SetAnswerBIsSolution(bool value) { this.SetDataItemValue(".Game.Answers.Answer_B.IsSolution", value); }
        public void SetAnswerBInputLeft(string value) { this.SetDataItemValue(".Game.Answers.Answer_B.InputLeft", value); }
        public void SetAnswerBInputRight(string value) { this.SetDataItemValue(".Game.Answers.Answer_B.InputRight", value); }

        public void SetAnswerCText(string value) { this.SetDataItemValue(".Game.Answers.Answer_C.Text", value); }
        public void SetAnswerCIsSolution(bool value) { this.SetDataItemValue(".Game.Answers.Answer_C.IsSolution", value); }
        public void SetAnswerCInputLeft(string value) { this.SetDataItemValue(".Game.Answers.Answer_C.InputLeft", value); }
        public void SetAnswerCInputRight(string value) { this.SetDataItemValue(".Game.Answers.Answer_C.InputRight", value); }

        public void SetAnswerDText(string value) { this.SetDataItemValue(".Game.Answers.Answer_D.Text", value); }
        public void SetAnswerDIsSolution(bool value) { this.SetDataItemValue(".Game.Answers.Answer_D.IsSolution", value); }
        public void SetAnswerDInputLeft(string value) { this.SetDataItemValue(".Game.Answers.Answer_D.InputLeft", value); }
        public void SetAnswerDInputRight(string value) { this.SetDataItemValue(".Game.Answers.Answer_D.InputRight", value); }


        public override void Dispose() {
            base.Dispose();
        }

        public void Clear() {
            this.SetDataItemTrigger(".Clear");
            this.GameToOut();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
