namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Ausliefern : Templates.InsertAndFullscreenStillBuzzerStopTimerScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Ausliefern()
            : base("Ausliefern") {
            this.ContentPositionX = 0;
            this.ContentPositionY = 0;
            this.TimerAlarmTime1 = -1;
            this.TimerAlarmTime2 = -1;
            this.TimerExtraTime = 60;
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerStartTime = 0;
            this.TimerStopTime = 3599;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.MinSec;
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
