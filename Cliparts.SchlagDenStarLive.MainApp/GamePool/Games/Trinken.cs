namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Trinken : Templates.TimerCounterToBeatTextInsert.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Trinken()
            : base("Trinken") {
            this.Vinsert_Timer.StartTime = 0;
            this.Vinsert_Timer.StopTime = 5999;
            this.Vinsert_Timer.Style = VentuzScenes.GamePool._Modules.Timer.Styles.MinSec;
        }
        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
