
namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Setzen10 : Templates.MultipleChoiceRatedScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Setzen10() : base("Setzen, 10!") {
            this.InsertPositionX = 0;
            this.InsertPositionY = 0;

            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;

            this.TaskCounterPositionX = 0;
            this.TaskCounterPositionY = 0;
            this.TaskCounterSize = 7;

            this.TimerAlarmTime1 = 5;
            this.TimerAlarmTime2 = -1;
            this.TimerExtraTime = 90;
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerStartTime = 45;
            this.TimerStopTime = 0;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
