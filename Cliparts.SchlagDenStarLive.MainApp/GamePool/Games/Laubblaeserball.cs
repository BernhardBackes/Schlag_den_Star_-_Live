namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Laubblaeserball : Templates.CounterSoloScoreTimer.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Laubblaeserball()
            : base("Laubbläserball") {
            this.TimerStartTime = 60;
            this.TimerStopTime = 0;
            this.TimerAlarmTime1 = 10;
            this.TimerAlarmTime2 = -1;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
            this.CounterPositionX = 0;
            this.CounterPositionY = 0;
            this.CounterStyle = VentuzScenes.GamePool._Modules.Score.Styles.Sport;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
