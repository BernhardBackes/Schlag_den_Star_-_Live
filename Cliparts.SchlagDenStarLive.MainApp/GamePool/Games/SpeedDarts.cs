
namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class SpeedDarts : Templates.TimerScoreDartCounter.Business {

        #region Properties
        #endregion


        #region Funktionen

        public SpeedDarts()
            : base("Speed-Darts") {
            this.ShowFullscreenTimer = true;
            this.TimerAlarmTime1 = 5;
            this.TimerAlarmTime2 = -1;
            this.TimerExtraTime = 0;
            this.TimerStartTime = 60;
            this.TimerStopTime = 0;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
            this.CounterPositionX = 0;
            this.CounterPositionY = 0;
            this.CounterSize = VentuzScenes.GamePool._Modules.CounterToBeat.SizeElements.ThreeDigits;
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
    //public class SpeedDarts : Templates.TimerSets.Business {

    //    #region Properties
    //    #endregion


    //    #region Funktionen

    //    public SpeedDarts()
    //        : base("Speed-Darts") {
    //        this.TimerPositionX = 0;
    //        this.TimerPositionY = 0;
    //        this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
    //        this.TimerStartTime = 60;
    //        this.TimerStopTime = 0;
    //        this.TimerAlarmTime1 = 5;
    //        this.TimerAlarmTime2 = -1;
    //        this.ShowFullscreenTimer = true;

    //        this.SetsPositionX = 0;
    //        this.SetsPositionY = 0;
    //        this.SetsStyle = VentuzScenes.GamePool._Modules.Sets.Styles.FiveSetsSum;
    //    }

    //    #endregion


    //    #region Events.Outgoing
    //    #endregion

    //    #region Events.Incoming
    //    #endregion

    //}

}