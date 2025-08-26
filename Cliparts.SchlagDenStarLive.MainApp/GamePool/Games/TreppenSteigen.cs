using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class TreppenSteigen : Templates.BuzzerStopTimerScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public TreppenSteigen()
            : base("Treppen steigen") {
            this.TimerAlarmTime1 = -1;
            this.TimerAlarmTime2 = -1;
            this.TimerExtraTime = 60;
            this.TimerStartTime = 0;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.MinSec;
            this.TimerStopTime = 5999;
            this.ShowFullscreenTimer = true;
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FiveDots;
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
