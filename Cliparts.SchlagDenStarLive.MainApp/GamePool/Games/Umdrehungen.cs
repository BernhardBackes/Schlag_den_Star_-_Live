
namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Umdrehungen : Templates.RotationCounterSoloScoreTimer.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Umdrehungen()
            : base("Umdrehungen") {
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerStartTime = 30;
            this.TimerStopTime = 0;
            this.TimerAlarmTime1 = 5;
            this.TimerAlarmTime2 = -1;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
            this.CounterPositionX = 0;
            this.CounterPositionY = 0;
            this.CounterStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FiveDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
