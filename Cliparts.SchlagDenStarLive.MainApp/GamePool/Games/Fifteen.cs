using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Fifteen : GamePool.Templates.DicesCalculation.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Fifteen()
            : base("15") {
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.MinSec;
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerStartTime = 8;
            this.TimerStopTime = 0;
            this.TimerAlarmTime1 = 3;
            this.TimerAlarmTime2 = -1;
            this.ShowFullscreenTimer = false;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.TargetResult = 15;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
