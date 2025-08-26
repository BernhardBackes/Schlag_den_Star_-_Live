using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class HoerDuell : Templates.TextInsertTimerScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public HoerDuell()
            : base("Hör-Duell") {
            this.TimerAlarmTime1 = 230;
            this.TimerAlarmTime2 = -1;
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.TimerStartTime = 0;
            this.TimerStopTime = 240;
            this.TimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.MinSec;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
            this.TextInsertPositionX = 0;
            this.TextInsertPositionY = 0;
            this.TextInsertStyle = VentuzScenes.GamePool._Modules.TextInsert.Styles.OneRow;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
