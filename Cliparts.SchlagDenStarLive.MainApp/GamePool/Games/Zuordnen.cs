using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Zuordnen : Templates.Correlation.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Zuordnen()
            : base("Zuordnen") {
            this.TimerPositionX = 210;
            this.TimerPositionY = -830;
            this.TimerStartTime = 60;
            this.TimerStopTime = 0;
            this.TimerAlarmTime1 = 5;
            this.TimerAlarmTime2 = 20;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FourDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
