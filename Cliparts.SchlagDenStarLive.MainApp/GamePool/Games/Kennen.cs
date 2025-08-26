using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {    

    public class Kennen : Templates.TeamCorrelation.Business
    {

        #region Properties
        #endregion


        #region Funktionen

        public Kennen()
            : base("Kennen")
        {
            this.TimerAlarmTime1 = 10;
            this.TimerAlarmTime2 = -1;
            this.TimerExtraTime = 60;
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerScaling = 100;
            this.TimerStartTime = 45;
            this.TimerStopTime = 0;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
            this.GamePositionX = 0;
            this.GamePositionY = 0;
            this.TaskCounterPositionX = 0;
            this.TaskCounterPositionY = 0;
            this.TaskCounterSize = 6;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
