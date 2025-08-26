namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class DoppelSaege : Templates.TimerSoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public DoppelSaege()
            : base("Doppel-Säge") {
            this.TimerStartTime = 0;
            this.TimerStopTime = 5999;
            this.TimerAlarmTime1 = -1;
            this.TimerAlarmTime2 = -1;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.MinSec;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.TwoDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
