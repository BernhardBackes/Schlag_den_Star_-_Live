using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Kakerlakak : GamePool.Templates.TournamentClockBuzzerScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Kakerlakak()
            : base("Kakerlakak") {
            this.FullscreenTimerPositionX = 0;
            this.FullscreenTimerPositionY = 0;
            this.FullscreenTimerScaling = 100;
            this.TimerStartTime = 60;
            this.TimerStopTime = 0;
            this.TimerAlarmTime1 = 5;
            this.TimerAlarmTime2 = -1;
            this.ShowFullscreenTimer = true;
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
