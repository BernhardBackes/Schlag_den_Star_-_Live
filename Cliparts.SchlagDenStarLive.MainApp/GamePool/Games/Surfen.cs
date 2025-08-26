namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Surfen : Templates.TimerSoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Surfen()
            : base("Surfen") {
            this.TimerStartTime = 0;
            this.TimerStopTime = 5999;
            this.TimerAlarmTime1 = -1;
            this.TimerAlarmTime2 = -1;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.MinSec;
            this.ShowFullscreenTimer = true;
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
