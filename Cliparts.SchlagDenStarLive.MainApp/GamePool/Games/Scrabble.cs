namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Scrabble : Templates.ScrabbleListCounterScoreTimer.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Scrabble()
            : base("Scrabble") {
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerStartTime = 90;
            this.TimerStopTime = 0;
            this.TimerAlarmTime1 = 5;
            this.TimerAlarmTime2 = -1;
            this.ShowFullscreenTimer = true;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.TwoDots;
            this.CounterPositionX = 0;
            this.CounterPositionY = 0;
            this.CounterStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
            this.GamePositionX = 440;
            this.GamePositionY = -18;
            this.GameScaling = 100;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
