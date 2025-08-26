namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class PusteHuegel : Templates.TimerSoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public PusteHuegel()
            : base("Puste-Hügel") {
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerStartTime = 60;
            this.TimerStopTime = 0;
            this.TimerExtraTime = 60;
            this.TimerAlarmTime1 = 5;
            this.TimerAlarmTime2 = -1;
            this.ShowFullscreenTimer = true;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FiveDots;
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
