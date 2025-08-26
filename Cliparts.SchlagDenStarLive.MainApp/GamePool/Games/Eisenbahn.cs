namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Eisenbahn : Templates.RGBIndicatorsScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Eisenbahn()
            : base("Eisenbahn") {
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerStartTime = 60;
            this.TimerStopTime = 0;
            this.TimerExtraTime = 60;
            this.TimerAlarmTime1 = 5;
            this.TimerAlarmTime2 = -1;
            this.ShowFullscreenTimer = true;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.TwoDots;
            this.IndicatorsCount = 5;
            this.OffColor = System.Drawing.Color.White;
            this.LeftColor = System.Drawing.Color.Red;
            this.RightColor = System.Drawing.Color.Blue;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
