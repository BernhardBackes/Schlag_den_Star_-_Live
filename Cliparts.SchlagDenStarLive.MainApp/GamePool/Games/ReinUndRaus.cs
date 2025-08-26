namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class ReinUndRaus : Templates.TimerCounterToBeatScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public ReinUndRaus()
            : base("Rein und raus") {
            this.TimerStartTime = 30;
            this.TimerStopTime = 0;
            this.TimerAlarmTime1 = 5;
            this.TimerAlarmTime2 = -1;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
            this.CounterPositionX = 0;
            this.CounterPositionY = 0;
            this.CounterSize = VentuzScenes.GamePool._Modules.CounterToBeat.SizeElements.TwoDigits;
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
