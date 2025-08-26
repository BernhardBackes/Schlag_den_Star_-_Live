namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Tanzen : Templates.TimerCounterToBeatScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Tanzen()
            : base("Tanzen") {
            this.TimerAlarmTime1 = 10;
            this.TimerAlarmTime2 = -1;
            this.TimerExtraTime = 30;
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerStartTime = 60;
            this.TimerStopTime = 0;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.MinSec;

            this.CounterPositionX = 0;
            this.CounterPositionY = 0;

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
