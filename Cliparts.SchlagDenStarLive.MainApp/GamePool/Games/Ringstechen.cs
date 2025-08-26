
namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Ringstechen : Templates.TimerSets.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Ringstechen()
            : base("Ringstechen") {
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
            this.TimerStartTime = 15;
            this.TimerStopTime = 0;
            this.TimerAlarmTime1 = 3;
            this.TimerAlarmTime2 = -1;
            this.ShowFullscreenTimer = true;

            this.SetsPositionX = 0;
            this.SetsPositionY = 0;
            this.SetsStyle = VentuzScenes.GamePool._Modules.Sets.StyleElements.TenSetsSum;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}