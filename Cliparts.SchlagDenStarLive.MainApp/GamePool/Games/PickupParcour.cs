namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    //public class PickupParcour : Templates.LAPTimeToBeatScore.Business {

    //    #region Properties
    //    #endregion


    //    #region Funktionen

    //    public PickupParcour()
    //        : base("Pickup-Parcour") {
    //        this.ScorePositionX = 0;
    //        this.ScorePositionY = 0;
    //        this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots;
    //        this.TimeToBeatPositionX = 0;
    //        this.TimeToBeatPositionY = 0;
    //        this.TimeToBeatStyle = VentuzScenes.GamePool._Modules.TimeToBeat.Styles.StopwatchNameCounter1;
    //        this.ShowFullscreenTimer = false;
    //        this.LAPsCount = 5;
    //    }

    //    #endregion


    //    #region Events.Outgoing
    //    #endregion

    //    #region Events.Incoming
    //    #endregion

    //}
    public class PickupParcour : Templates.TimeToBeatTimer.Business {

        #region Properties
        #endregion


        #region Funktionen

        public PickupParcour()
            : base("Pickup-Parcour") {
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerStartTime = 180;
            this.TimerStopTime = 0;
            this.TimerAlarmTime1 = 10;
            this.TimerAlarmTime2 = -1;
            this.ShowFullscreenTimer = true;
            this.TimeToBeatPositionX = 0;
            this.TimeToBeatPositionY = 0;
            this.TimeToBeatStyle = VentuzScenes.GamePool._Modules.TimeToBeat.Styles.Stopwatch;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.TwoDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
