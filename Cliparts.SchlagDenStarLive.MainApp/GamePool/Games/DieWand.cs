namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class DieWand : Templates.Timer.Business {

        #region Properties
        #endregion


        #region Funktionen

        public DieWand()
            : base("Die Wand") {
            this.Vinsert_Timer.Style = VentuzScenes.GamePool._Modules.Timer.Styles.MinSec;
            this.Vinsert_Timer.PositionX = 0;
            this.Vinsert_Timer.PositionY = 0;
            this.Vinsert_Timer.StartTime = 0;
            this.Vinsert_Timer.StopTime = 5999;
            this.Vinsert_Timer.AlarmTime1 = -1;
            this.Vinsert_Timer.AlarmTime2 = -1;
            this.ShowFullscreenTimer = true;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
