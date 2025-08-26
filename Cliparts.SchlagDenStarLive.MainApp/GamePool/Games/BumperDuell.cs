
namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class BumperDuell : Templates.TimerRandomBuzzerScoreShotclock.Business {

        #region Properties
        #endregion


        #region Funktionen

        public BumperDuell()
            : base("Bumper-Duell") {
            this.Vinsert_Timer.Style = VentuzScenes.GamePool._Modules.Timer.Styles.MinSec;
            this.Vinsert_Timer.PositionX = 496;
            this.Vinsert_Timer.PositionY = 23;
            this.Vinsert_Timer.Scaling = 75.0;
            this.Vinsert_Timer.StartTime = 180;
            this.Vinsert_Timer.ExtraTime = 60;
            this.Vinsert_Timer.StopTime = 0;
            this.Vinsert_Timer.AlarmTime1 = 10;
            this.Vinsert_Timer.AlarmTime2 = -1;
            this.ShowFullscreenTimer = true;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Sport;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
