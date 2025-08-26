using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Kleben : Templates.BuzzerStartTwoTimersScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Kleben()
            : base("Kleben") {
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.RightTimerPositionX = 1700;
            this.RightTimerPositionY = 0;
            this.TimerAlarmTime1 = 3;
            this.TimerAlarmTime2 = -1;
            this.TimerStartTime = 10;
            this.TimerStopTime = 0;
            this.TimerExtraTime = 10;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
            this.ShowFullscreenTimer = true;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
