namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Ballerburg : Templates.TimerSoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Ballerburg()
            : base("Ballerburg") {
            this.TimerStartTime = 120;
            this.TimerStopTime = 0;
            this.TimerAlarmTime1 = 10;
            this.TimerAlarmTime2 = -1;
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
