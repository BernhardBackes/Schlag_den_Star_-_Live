namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class WerErkenntMehr : Templates.ListRefersToImageTimerScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public WerErkenntMehr()
            : base("Wer erkennt mehr?") {
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerStartTime = 30;
            this.TimerStopTime = 0;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
            this.TimerAlarmTime1 = 5;
            this.TimerAlarmTime2 = -1;
            this.ShowFullscreenTimer = false;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FiveDots;
            this.BorderPositionX = 0;
            this.BorderPositionY = -120;
            this.BorderStyle = VentuzScenes.GamePool._Modules.Border.Styles.FiveDots;
            this.ItemInsertPositionX = 0;
            this.ItemInsertPositionY = -30;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
