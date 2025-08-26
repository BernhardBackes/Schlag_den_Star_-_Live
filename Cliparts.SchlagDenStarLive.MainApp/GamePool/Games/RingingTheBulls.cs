using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class RingingTheBulls : Templates.TimerScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public RingingTheBulls()
            : base("Ringing The Bulls") {
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.MinSec;
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerScaling = 100;
            this.TimerStartTime = 600;
            this.TimerExtraTime = 60;
            this.TimerStopTime = 0;
            this.TimerAlarmTime1 = 10;
            this.TimerAlarmTime2 = -1;
            this.ShowFullscreenTimer = true;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.CounterLeft;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
