using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Schreiben : Templates.TimeToBeatReachCounterTextInsertScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Schreiben()
            : base("Schreiben") {
            this.TimeToBeatPositionX = 0;
            this.TimeToBeatPositionY = 0;
            this.TimeToBeatStopTime = 59999;
            this.TimeToBeatSentenceTime = 0;
            this.TimeToBeatStyle = VentuzScenes.GamePool._Modules.TimeToBeat.Styles.StopwatchNameCounter1;
            this.ShowFullscreenTimer = false;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots;
            this.TextInsertPositionX = 0;
            this.TextInsertPositionY = 0;
            this.TextInsertStyle = VentuzScenes.GamePool._Modules.TextInsert.Styles.OneRow;
            this.ToReachValue = 3;
            this.SentenceTime = 10;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
