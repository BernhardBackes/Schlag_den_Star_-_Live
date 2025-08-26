namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Seilchenspringen : Templates.TimerScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Seilchenspringen()
            : base("Seilchenspringen") {

            this.TimerAlarmTime1 = 5;
            this.TimerAlarmTime2 = -1;
            this.TimerStopTime = 0;
            this.TimerStartTime = 60;
            this.TimerExtraTime = 30;
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
            this.ShowFullscreenTimer = false;

            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots;
            this.ShowFullscreenScore = true;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
