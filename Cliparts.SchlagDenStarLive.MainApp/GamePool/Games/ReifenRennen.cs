using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class ReifenRennen : GamePool.Templates.TimerSoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public ReifenRennen()
            : base("Reifen-Rennen") {
            this.TimerStartTime = 0;
            this.TimerStopTime = 5999;
            this.TimerExtraTime = 30;
            this.TimerAlarmTime1 = -1;
            this.TimerAlarmTime2 = -1;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.MinSec;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.TwoDots;
            this.ShowFullscreenTimer = true;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
