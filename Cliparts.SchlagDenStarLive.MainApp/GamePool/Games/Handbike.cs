using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Handbike : Templates.TimeToBeatScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Handbike()
            : base("Handbike") {
            this.TimeToBeatPositionX = 0;
            this.TimeToBeatPositionY = 0;
            this.TimeToBeatStopTime = 59999;
            this.TimeToBeatSentenceTime = 0;
            this.TimeToBeatStyle = VentuzScenes.GamePool._Modules.TimeToBeat.Styles.Stopwatch;
            this.ShowFullscreenTimer = false;
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


    //public class Handbike : Templates.TimerForTwoSoloScoreCounter.Business {

    //    #region Properties
    //    #endregion


    //    #region Funktionen

    //    public Handbike()
    //        : base("Handbike") {
    //        this.TimerPositionX = 0;
    //        this.TimerPositionY = 0;
    //        this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
    //        this.ScorePositionX = 0;
    //        this.ScorePositionY = 0;
    //        this.TaskCounterPositionX = 0;
    //        this.TaskCounterPositionY = 0;
    //        this.TaskCounterSize = 10;
    //    }

    //    #endregion


    //    #region Events.Outgoing
    //    #endregion

    //    #region Events.Incoming
    //    #endregion


    //}
}
