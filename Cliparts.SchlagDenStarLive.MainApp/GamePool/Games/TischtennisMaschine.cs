namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {
    public class TischtennisMaschine : Templates.CounterSoloScoreTimer.Business {

        #region Properties
        #endregion


        #region Funktionen

        public TischtennisMaschine()
            : base("Tischtennis-Maschine") {
            this.TimerStartTime = 60;
            this.TimerStopTime = 0;
            this.TimerAlarmTime1 = 5;
            this.TimerAlarmTime2 = -1;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
            this.CounterStyle = VentuzScenes.GamePool._Modules.Score.Styles.CounterLeft;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
