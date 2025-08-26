using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class StaplerBasketball : GamePool.Templates.TimerScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public StaplerBasketball()
            : base("Stapler-Basketball") {
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.MinSec;
            this.TimerPositionX = 640;
            this.TimerPositionY = 15;
            this.TimerStartTime = 360;
            this.TimerStopTime = 0;
            this.TimerAlarmTime1 = 10;
            this.TimerAlarmTime2 = -1;
            this.ShowFullscreenTimer = true;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Sport;
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
