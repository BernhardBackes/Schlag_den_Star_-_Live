using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Tretathlon : Templates.ShootingTimerForTwoSoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Tretathlon()
            : base("Tretathlon") {
            this.ShootingPositionX = 0;
            this.ShootingPositionY = 0;
            this.ShootingStyle = VentuzScenes.GamePool._Modules.ShootingTimerForTwo.Styles.ThreeHeats;
            this.HitsCount = 5;

            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots;
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
