
namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class WasserStaffel : Templates.Timer.Business {

        #region Properties
        #endregion


        #region Funktionen

        public WasserStaffel()
            : base("Wasser-Staffel") {
            this.Vinsert_Timer.Style = VentuzScenes.GamePool._Modules.Timer.Styles.MinSec;
            this.Vinsert_Timer.PositionX = 0;
            this.Vinsert_Timer.PositionY = 0;
            this.Vinsert_Timer.StartTime = 120;
            this.Vinsert_Timer.StopTime = 0;
            this.Vinsert_Timer.AlarmTime1 = 10;
            this.Vinsert_Timer.AlarmTime2 = -1;
            this.Vinsert_Timer.ExtraTime = 30;
            this.ShowFullscreenTimer = true;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
