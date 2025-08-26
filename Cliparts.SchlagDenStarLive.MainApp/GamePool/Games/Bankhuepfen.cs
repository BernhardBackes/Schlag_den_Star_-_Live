namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Bankhuepfen : Templates.CounterSoloScoreTimer.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Bankhuepfen()
            : base("Bankhüpfen") {
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
            this.TimerStartTime = 45;
            this.TimerExtraTime = 20;
            this.TimerAlarmTime1 = 5;
            this.TimerAlarmTime2 = -1;
            this.TimerStopTime = 0;
            this.CounterPositionX = 0;
            this.CounterPositionY = 0;
            this.CounterStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
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

    //public class Bankhuepfen : Templates.CounterScoreTimer.Business
    //{

    //    #region Properties
    //    #endregion


    //    #region Funktionen

    //    public Bankhuepfen()
    //        : base("Bankhüpfen")
    //    {
    //        this.TimerPositionX = 0;
    //        this.TimerPositionY = 0;
    //        this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
    //        this.TimerStartTime = 60;
    //        this.TimerExtraTime = 20;
    //        this.TimerAlarmTime1 = 5;
    //        this.TimerAlarmTime2 = -1;
    //        this.TimerStopTime = 0;
    //        this.HidePlayerCounterByDefault = false;
    //        this.WinnerMode = Templates._Base.CounterScore.WinnerModes.HighestCounter;
    //        this.CounterScorePositionX = 0; ;
    //        this.CounterScoreStyle = VentuzScenes.GamePool._Modules.CounterScore.Styles.ThreeDots;
    //        this.HidePlayerCounterByDefault = false;
    //    }

    //    #endregion


    //    #region Events.Outgoing
    //    #endregion

    //    #region Events.Incoming
    //    #endregion

    //}

}
