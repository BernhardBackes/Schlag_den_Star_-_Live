using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Zahlenwoerter : Templates.InsertMovieBuzzerScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Zahlenwoerter()
            : base("Zahlenwörter") {
            this.ContentPositionX = 0;
            this.ContentPositionY = -130;
            this.ScorePositionX = 0;
            this.ScorePositionY = -12;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FiveDots;
            this.TaskCounterPositionX = 0;
            this.TaskCounterPositionY = -200;
            this.TaskCounterSize = 13;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
