namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Ausbalancieren : Templates.TimerSoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Ausbalancieren()
            : base("Ausbalancieren") {
            this.TimerStartTime = 30;
            this.TimerStopTime = 0;
            this.TimerAlarmTime1 = 5;
            this.TimerAlarmTime2 = -1;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FiveDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
