namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Schuetten : Templates.TimerSoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Schuetten()
            : base("Schütten") {
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerStartTime = 60;
            this.TimerStopTime = 0;
            this.TimerAlarmTime1 = 5;
            this.TimerAlarmTime2 = -1;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.SevenDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
