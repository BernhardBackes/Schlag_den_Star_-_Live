namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class GravitationsBall : Templates.CounterSoloScoreTimer.Business {

        #region Properties
        #endregion


        #region Funktionen

        public GravitationsBall()
            : base("Gravitations-Ball") {

            this.TimerAlarmTime1 = 5;
            this.TimerAlarmTime2 = -1;
            this.TimerStopTime = 0;
            this.TimerStartTime = 30;
            this.TimerExtraTime = 0;
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;

            this.CounterPositionX = 0;
            this.CounterPositionY = 0;

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
