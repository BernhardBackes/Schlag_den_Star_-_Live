using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class FahrWippe : GamePool.Templates.TimeToBeatStartStopTimerScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public FahrWippe()
            : base("Fahr-Wippe") {
            this.TimerStartTime = 90;
            this.TimerAlarmTime1 = 10;
            this.TimerAlarmTime2 = -1;
            this.TimerStopTime = 0;
            this.StopwatchStopTime = 5999;
            this.ShowFullscreenTimer = false;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
