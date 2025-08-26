
namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Voelkerball : Templates.TimerSoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Voelkerball()
            : base("Völkerball") {
            this.TimerAlarmTime1 = 3;
            this.TimerAlarmTime2 = -1;
            this.TimerExtraTime = 10;
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerScaling = 100;
            this.TimerStartTime = 20;
            this.TimerStopTime = 0;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
