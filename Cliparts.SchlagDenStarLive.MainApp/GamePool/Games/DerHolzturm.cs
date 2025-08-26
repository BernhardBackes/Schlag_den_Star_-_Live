namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class DerHolzturm : Templates.Timer.Business {

        #region Properties
        #endregion


        #region Funktionen

        public DerHolzturm()
            : base("Der Holzturm") {
            this.Vinsert_Timer.StartTime = 120;
            this.Vinsert_Timer.StopTime = 0;
            this.Vinsert_Timer.AlarmTime1 = 10;
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
