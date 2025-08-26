using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Songtexte : Templates.TwelveIssuesTimerScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Songtexte() : base("Songtexte") {
            this.TimerAlarmTime1 = 3;
            this.TimerAlarmTime2 = -1;
            this.TimerExtraTime = 0;
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerStartTime = 20;
            this.TimerStopTime = 0;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FourDots;
            this.BorderPositionX = 0;
            this.BorderPositionY = -100;
            this.BorderScaling = 100;
            this.BorderStyle = VentuzScenes.GamePool._Modules.Border.Styles.FourDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
