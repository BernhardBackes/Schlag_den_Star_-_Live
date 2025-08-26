using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class EisPenaltyschiessen : Templates.PenaltyShotclock.Business {

        #region Properties
        #endregion


        #region Funktionen

        public EisPenaltyschiessen()
            : base("Eis-Penaltyschießen") {
            this.TimerStartTime = 20;
            this.TimerStopTime = 0;
            this.TimerAlarmTime1 = 3;
            this.TimerAlarmTime2 = -1;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
            this.ShowFullscreenTimer = true;
            this.PenaltyDotsCount = 20;
            this.PenaltyPositionX = 0;
            this.PenaltyPositionY = 0;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}