namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class DuftPaerchen : Templates.TimerSets.Business {

        #region Properties
        #endregion


        #region Funktionen

        public DuftPaerchen()
            : base("Duft-Pärchen") {
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerStartTime = 120;
            this.TimerStopTime = 0;
            this.TimerAlarmTime1 = 10;
            this.TimerAlarmTime2 = -1;
            this.ShowFullscreenTimer = true;
            this.SetsPositionX = 0;
            this.SetsPositionY = 0;
            this.SetsStyle = VentuzScenes.GamePool._Modules.Sets.StyleElements.FourSetsSum;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
