using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class KugelBalance : GamePool.Templates.TimerScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public KugelBalance()
            : base("Kugel-Balance") {
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FiveDots;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.MinSec;
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerStartTime = 0;
            this.TimerStopTime = 5999;
            this.TimerAlarmTime1 = -1;
            this.TimerAlarmTime2 = -1;
            this.ShowFullscreenTimer = true;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}