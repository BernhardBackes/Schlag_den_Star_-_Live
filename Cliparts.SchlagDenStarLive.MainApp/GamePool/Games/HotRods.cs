using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    //public class HotRods : Templates.TimeToBeatLAPsCountScore.Business {

    //    #region Properties
    //    #endregion


    //    #region Funktionen

    //    public HotRods()
    //        : base("Hot Rods") {

    //        this.ScorePositionX = 0;
    //        this.ScorePositionY = 0;
    //        this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots;

    //        this.TimeToBeatPositionX = 0;
    //        this.TimeToBeatPositionY = 0;
    //        this.TimeToBeatStyle = VentuzScenes.GamePool._Modules.TimeToBeat.Styles.StopwatchNameCounter1;

    //        this.LAPsCount = 3;
    //    }

    //    #endregion


    //    #region Events.Outgoing
    //    #endregion

    //    #region Events.Incoming
    //    #endregion

    //}

    public class HotRods : Templates.TimeToBeatTimer.Business
    {

        #region Properties
        #endregion


        #region Funktionen

        public HotRods()
            : base("Hot Rods")
        {
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerStartTime = 90;
            this.TimerStopTime = 0;
            this.TimerAlarmTime1 = 10;
            this.TimerAlarmTime2 = -1;
            this.ShowFullscreenTimer = true;
            this.TimeToBeatPositionX = 0;
            this.TimeToBeatPositionY = 0;
            this.TimeToBeatStyle = VentuzScenes.GamePool._Modules.TimeToBeat.Styles.Stopwatch;
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
