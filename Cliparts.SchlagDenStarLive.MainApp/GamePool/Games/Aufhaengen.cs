namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Aufhaengen : Templates.Timer.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Aufhaengen()
            : base("Aufhängen") {
            this.Vinsert_Timer.StartTime = 60;
            this.Vinsert_Timer.StopTime = 0;
            this.Vinsert_Timer.AlarmTime1 = 5;
            this.Vinsert_Timer.AlarmTime2 = -1;
            this.Vinsert_Timer.Style = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
            this.ShowFullscreenTimer = true;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
