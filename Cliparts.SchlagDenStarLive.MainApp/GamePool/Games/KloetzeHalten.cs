namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class KloetzeHalten : Templates.TimerScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public KloetzeHalten()
            : base("Klötze halten") {
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
            this.TimerPositionX = -790;
            this.TimerPositionY = 440;
            this.TimerStartTime = 60;
            this.TimerStopTime = 0;
            this.TimerAlarmTime1 = 10;
            this.TimerAlarmTime2 = -1;
            this.TimerExtraTime = 10;
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
