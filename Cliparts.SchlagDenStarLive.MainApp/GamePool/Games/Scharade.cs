namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Scharade : Templates.TextInsertTimerCounterToBeatScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Scharade()
            : base("Scharade") {
            this.CounterPositionX = 0;
            this.CounterPositionY = 0;
            this.CounterSize = VentuzScenes.GamePool._Modules.CounterToBeat.SizeElements.TwoDigits;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots;
            this.TextInsertPositionX = 0;
            this.TextInsertPositionY = 0;
            this.TextInsertStyle = VentuzScenes.GamePool._Modules.TextInsert.Styles.Small;
            this.TimerAlarmTime1 = 0;
            this.TimerAlarmTime2 = -1;
            this.TimerExtraTime = 60;
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
            this.TimerStartTime = 90;
            this.TimerStopTime = 0; 
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
