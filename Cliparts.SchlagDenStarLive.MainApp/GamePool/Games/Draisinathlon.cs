using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Draisinathlon : Templates.ALSShooting.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Draisinathlon()
            : base("Draisinathlon") {
            ScorePositionX = 0;
            ScorePositionY = 0;
            ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.TwoDots;
            ShootingPositionX = 0;
            ShootingPositionY = 0;
            ShootingStyle = VentuzScenes.GamePool._Modules.Shooting.Styles.FourHeats;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
