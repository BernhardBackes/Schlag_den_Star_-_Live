namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class DerPaerchenZaun : Templates.Timer.Business {

        #region Properties
        #endregion


        #region Funktionen

        public DerPaerchenZaun()
            : base("Der Pärchen-Zaun") {
            this.Vinsert_Timer.StartTime = 0;
            this.Vinsert_Timer.StopTime = 5999;
            this.Vinsert_Timer.AlarmTime1 = -1;
            this.Vinsert_Timer.AlarmTime2 = -1;
            this.Vinsert_Timer.Style = VentuzScenes.GamePool._Modules.Timer.Styles.MinSec;
            this.ShowFullscreenTimer = false;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
