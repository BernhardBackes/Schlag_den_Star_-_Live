namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Schminken : Templates.AudienceVoting.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Schminken()
            : base("Schminken") {
            this.Vinsert_Timer.Style = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
            this.Vinsert_Timer.PositionX = 0;
            this.Vinsert_Timer.PositionY = 0;
            this.Vinsert_Timer.StartTime = 60;
            this.Vinsert_Timer.StopTime = 0;
            this.Vinsert_Timer.AlarmTime1 = 5;
            this.Vinsert_Timer.AlarmTime2 = -1;
            this.ShowFullscreenTimer = true;
            this.ContentPositionX = 0;
            this.ContentPositionY = 0;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
