
namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Boxen : Templates.PunchCounter.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Boxen()
            : base("Boxen") {
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerStartTime = 60;
            this.TimerStopTime = 0;
            this.TimerExtraTime = 60;
            this.TimerAlarmTime1 = 10;
            this.TimerAlarmTime2 = -1;
            this.CounterPositionX = 0;
            this.CounterPositionY = 0;
            this.CounterStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.SixDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion


    }
}
