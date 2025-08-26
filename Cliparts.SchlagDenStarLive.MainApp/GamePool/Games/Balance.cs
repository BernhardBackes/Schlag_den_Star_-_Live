
namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Balance : Templates.TimeToBeatStartStopTimerScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Balance()
            : base("Balance") {
            this.TimerStartTime = 30;
            this.TimerAlarmTime1 = 5;
            this.TimerAlarmTime2 = -1;
            this.TimerStopTime = 0;
            this.StopwatchStopTime = 5999;
            this.ShowFullscreenTimer = false;
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
