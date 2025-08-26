namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class WoerterLaufen : Templates.SpeedTypingCounterToBeat.Business {

        #region Properties
        #endregion


        #region Funktionen

        public WoerterLaufen()
            : base("Wörter laufen") {
            this.Vinsert_Timer.AlarmTime1 = 10;
            this.Vinsert_Timer.AlarmTime2 = -1;
            this.Vinsert_Timer.PositionX = 0;
            this.Vinsert_Timer.PositionY = 0;
            this.Vinsert_Timer.StartTime = 240;
            this.Vinsert_Timer.StopTime = 0;
            this.Vinsert_Timer.Style = VentuzScenes.GamePool._Modules.Timer.Styles.MinSec;
            this.CounterPositionX = 0;
            this.CounterPositionY = 0;
            this.TextInsertPositionX = 0;
            this.TextInsertPositionY = 0;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
