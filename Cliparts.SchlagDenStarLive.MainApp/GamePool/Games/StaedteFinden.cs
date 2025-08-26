namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class StaedteFinden : Templates.MapSlidingTimerScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public StaedteFinden()
            : base("Städte finden") {
            this.LeftPlayerBuzzerChannel = 1;
            this.RightPlayerBuzzerChannel = 2;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.MinSec;
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerStartTime = 120;
            this.TimerStopTime = 0;
            this.TimerExtraTime = 60;
            this.TimerAlarmTime1 = 5;
            this.TimerAlarmTime2 = -1;
            this.ShowFullscreenTimer = true;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
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
