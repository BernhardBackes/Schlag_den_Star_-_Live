using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class TretScooter : Templates.TimeToBeatScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public TretScooter()
            : base("Tret-Scooter") {
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots;
            this.TimeToBeatPositionX = 0;
            this.TimeToBeatPositionY = 0;
            this.TimeToBeatStyle = VentuzScenes.GamePool._Modules.TimeToBeat.Styles.Stopwatch;
            this.ShowFullscreenTimer = false;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion


    }
}
