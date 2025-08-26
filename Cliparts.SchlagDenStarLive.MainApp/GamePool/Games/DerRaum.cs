using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class DerRaum : GamePool.Templates.ThreeTimerScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public DerRaum()
            : base("Der Raum") {
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.MinSec;
            this.TimerStartTime = 180;
            this.TimerExtraTime = 60;
            this.TimerStopTime = 0;
            this.TimerAlarmTime1 = 10;
            this.TimerAlarmTime2 = -1;
            this.LeftTimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.MinSec;
            this.LeftTimerStartTime = 0;
            this.LeftTimerStopTime = 599;
            this.RightTimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.MinSec;
            this.RightTimerStartTime = 0;
            this.RightTimerStopTime = 599;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
