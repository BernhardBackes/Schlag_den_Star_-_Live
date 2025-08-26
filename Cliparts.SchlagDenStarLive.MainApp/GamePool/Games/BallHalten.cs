namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class BallHalten : Templates.Timer.Business {

        #region Properties
        #endregion


        #region Funktionen

        public BallHalten()
            : base("Ball halten") {
            this.Vinsert_Timer.StartTime = 0;
            this.Vinsert_Timer.StopTime = 5999;
            this.Vinsert_Timer.AlarmTime1 = -1;
            this.Vinsert_Timer.AlarmTime2 = -1;
            this.Vinsert_Timer.Style = VentuzScenes.GamePool._Modules.Timer.Styles.MinSec;
            this.ShowFullscreenTimer = true;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
