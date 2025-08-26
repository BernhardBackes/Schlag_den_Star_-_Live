namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class UltimateDodgeball : Templates.CounterSoloScoreTimerShotclock.Business {

        #region Properties
        #endregion


        #region Funktionen

        public UltimateDodgeball()
            : base("Ultimate Dodgeball") {
            this.TimerAlarmTime1 = 10;
            this.TimerAlarmTime2 = -1;
            this.TimerExtraTime = 60;
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerStartTime = 180;
            this.TimerStopTime = 0;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.MinSec;
            this.CounterStyle = VentuzScenes.GamePool._Modules.Score.Styles.Sport;
            this.CounterPositionX = 0;
            this.CounterPositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.TwoDots;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}