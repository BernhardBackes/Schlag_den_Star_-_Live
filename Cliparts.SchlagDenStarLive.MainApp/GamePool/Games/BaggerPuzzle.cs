namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class BaggerPuzzle : Templates.Timer.Business {

        #region Properties
        #endregion


        #region Funktionen

        public BaggerPuzzle()
            : base("Bagger-Puzzle") {
            this.Vinsert_Timer.Style = VentuzScenes.GamePool._Modules.Timer.Styles.MinSec;
            this.Vinsert_Timer.PositionX = 0;
            this.Vinsert_Timer.PositionY = 0;
            this.Vinsert_Timer.StartTime = 480;
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
