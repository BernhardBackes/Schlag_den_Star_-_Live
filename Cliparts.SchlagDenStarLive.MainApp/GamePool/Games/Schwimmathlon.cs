using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Schwimmathlon : GamePool.Templates.CounterSoloScoreTimer.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Schwimmathlon()
            : base("Schwimmathlon") {
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerStartTime = 0;
            this.TimerStopTime = 5999;
            this.ShowFullscreenTimer = false;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.MinSec;
            this.CounterPositionX = 0;
            this.CounterPositionY = 0;
            this.CounterStyle = VentuzScenes.GamePool._Modules.Score.Styles.CounterLeft;
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
