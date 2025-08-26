namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Ablenken : Templates.WordByWordTimerScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Ablenken()
            : base("Ablenken") {
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.MinSec;
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerStartTime = 30;
            this.TimerStopTime = 0;
            this.TimerExtraTime = 60;
            this.TimerAlarmTime1 = 5;
            this.TimerAlarmTime2 = -1;
            this.ShowFullscreenTimer = true;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Sport;
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
