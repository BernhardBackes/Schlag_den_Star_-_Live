using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class HauptstadtDuell : Templates.SelectTaskTextInsertTimer.Business {

        #region Properties
        #endregion


        #region Funktionen

        public HauptstadtDuell()
            : base("Hauptstadt-Duell") {
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.TextInsertPositionX = 0;
            this.TextInsertPositionY = -40;
            this.TextInsertStyle = VentuzScenes.GamePool._Modules.TextInsert.Styles.Short;
            this.TimerAlarmTime1 = 3;
            this.TimerAlarmTime2 = -1;
            this.TimerExtraTime = 10;
            this.TimerPositionX = 810;
            this.TimerPositionY = -895;
            this.TimerScaling = 100;
            this.TimerStartTime = 10;
            this.TimerStopTime = 0;
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
