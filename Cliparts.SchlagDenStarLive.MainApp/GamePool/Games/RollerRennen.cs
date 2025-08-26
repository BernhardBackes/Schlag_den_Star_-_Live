using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class RollerRennen : Templates.CounterScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public RollerRennen()
            : base("Roller-Rennen") {
            this.HidePlayerCounterByDefault = false;
            this.CounterScorePositionX = 0;
            this.CounterScorePositionY = 0;
            this.CounterScoreStyle = VentuzScenes.GamePool._Modules.CounterScore.Styles.FourDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion


    }
}
