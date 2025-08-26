namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class SchaukelWeitsprung : Templates.DecimalSetsTimer.Business {

        #region Properties
        #endregion


        #region Funktionen

        public SchaukelWeitsprung() : base("Schaukel-Weitsprung") {
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerStartTime = 60;
            this.TimerExtraTime = 60;
            this.TimerStopTime = 0;
            this.TimerAlarmTime1 = 5;
            this.TimerAlarmTime2 = -1;
            this.ShowFullscreenTimer = true;

            this.DecimalSetsPositionX = 0;
            this.DecimalSetsPositionY = 0;
            this.DecimalSetsStyle = VentuzScenes.GamePool._Modules.DecimalSets.Styles.SixSets;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
