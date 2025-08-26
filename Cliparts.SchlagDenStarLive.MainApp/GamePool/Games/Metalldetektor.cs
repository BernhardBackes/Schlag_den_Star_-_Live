namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Metalldetektor : Templates.TimerScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Metalldetektor()
            : base("Metalldetektor") {
            this.TimerStartTime = 3600;
            this.TimerStopTime = 0;
            this.TimerAlarmTime1 = 10;
            this.TimerAlarmTime2 = -1;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.MinSec;
            this.ShowFullscreenTimer = true;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
