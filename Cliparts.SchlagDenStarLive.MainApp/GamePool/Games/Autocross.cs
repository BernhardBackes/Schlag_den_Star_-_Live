using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Autocross : Templates.TimeToBeatTimer.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Autocross()
            : base("Autocross") {
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerStartTime = 120;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.MinSec;
            this.TimerStopTime = 0;
            this.TimerAlarmTime1 = 10;
            this.TimerAlarmTime2 = -1;
            this.TimerExtraTime = 0;
            this.TimeToBeatPositionX = 0;
            this.TimeToBeatPositionY = 0;
            this.ShowFullscreenTimer = false;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
