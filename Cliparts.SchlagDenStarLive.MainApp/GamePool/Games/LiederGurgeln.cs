using Cliparts.Devantech.Controls;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class LiederGurgeln : Templates.TimerTextInsertAudioScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public LiederGurgeln()
            : base("Lieder gurgeln") {
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;

            this.TaskCounterPositionX = 0;
            this.TaskCounterPositionY = 0;
            this.TaskCounterSize = 7;

            this.TextInsertPositionX = 0;
            this.TextInsertPositionY = 0;   
            this.TextInsertStyle = VentuzScenes.GamePool._Modules.TextInsert.Styles.TwoRowsSmall;

            this.TimerAlarmTime1 = 5;
            this.TimerAlarmTime2 = -1;
            this.TimerExtraTime = 60;
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerScaling = 100;
            this.TimerStartTime = 45;
            this.TimerStopTime = 0;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
            this.ShowFullscreenTimer = true;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
