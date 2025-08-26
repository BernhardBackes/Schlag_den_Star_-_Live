namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Kellen : Templates.TimerScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Kellen()
            : base("Kellen") {
            this.TimerAlarmTime1 = 10;
            this.TimerAlarmTime2 = -1;
            this.TimerExtraTime = 30;
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerStartTime = 90;
            this.TimerStopTime = 0;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
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
