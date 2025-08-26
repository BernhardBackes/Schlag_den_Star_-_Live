using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class WerWeissMehrTeamMode : Templates.AnswerListTimerCounterToBeatScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public WerWeissMehrTeamMode()
            : base("Wer weiß mehr? (Team-Mode)") {
            this.CounterPositionX = 1600;
            this.CounterPositionY = 0;
            this.CounterSize = VentuzScenes.GamePool._Modules.CounterToBeat.SizeElements.TwoDigits;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FourDots;
            this.TimerAlarmTime1 = 5;
            this.TimerAlarmTime2 = -1;
            this.TimerExtraTime = 60;
            this.TimerPositionX = 1660;
            this.TimerPositionY = -900;
            this.TimerScaling = 100;
            this.TimerStartTime = 45;
            this.TimerStopTime = 0;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
            this.ShowFullscreenTimer = false;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
