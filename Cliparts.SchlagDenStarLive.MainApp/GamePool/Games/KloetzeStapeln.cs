namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class KloetzeStapeln : Templates.Timer.Business {

        #region Properties
        #endregion


        #region Funktionen

        public KloetzeStapeln()
            : base("Klötze stapeln") {
            this.Vinsert_Timer.Style = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
            this.Vinsert_Timer.PositionX = -790;
            this.Vinsert_Timer.PositionY = 440;
            this.Vinsert_Timer.StartTime = 90;
            this.Vinsert_Timer.StopTime = 0;
            this.Vinsert_Timer.AlarmTime1 = 10;
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
