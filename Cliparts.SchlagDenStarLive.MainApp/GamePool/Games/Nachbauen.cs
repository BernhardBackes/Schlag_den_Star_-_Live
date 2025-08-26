namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Nachbauen : Templates.TimerSoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Nachbauen()
            : base("Nachbauen") {
            this.TimerStartTime = 60;
            this.TimerExtraTime = 90;
            this.TimerStopTime = 0;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
