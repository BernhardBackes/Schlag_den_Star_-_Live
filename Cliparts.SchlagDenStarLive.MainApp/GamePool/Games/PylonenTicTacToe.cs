namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class PylonenTicTacToe : Templates.TimerSoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public PylonenTicTacToe()
            : base("Pylonen Tic Tac Toe") {
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FiveDots;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.TimerAlarmTime1 = 5;
            this.TimerAlarmTime2 = -1;
            this.TimerExtraTime = 60;
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerStartTime = 60;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}