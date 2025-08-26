namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class ParallelStapler : Templates.TimerSoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public ParallelStapler()
            : base("Parallel-Stapler") {

            this.TimerAlarmTime1 = -1;
            this.TimerAlarmTime2 = -1;
            this.TimerExtraTime = 0;
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerScaling = 1;
            this.TimerStartTime = 0;
            this.TimerStopTime = 5999;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.MinSec;

            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.TwoDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
