namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Huerdenlauf : Templates.TimerSoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Huerdenlauf()
            : base("Hürdenlauf") {
            this.TimerStartTime = 75;
            this.TimerStopTime = 0;
            this.TimerAlarmTime1 = 10;
            this.TimerAlarmTime2 = -1;
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FourDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
