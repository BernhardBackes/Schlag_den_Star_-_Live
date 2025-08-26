using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class RovoSchnecke : Templates.TimerForTwo.Business {

        #region Properties
        #endregion


        #region Funktionen

        public RovoSchnecke()
            : base("Rovo-Schnecke") {
            this.TimerStyle = VentuzScenes.GamePool.TimerForTwo.Insert.Styles.FourDots;
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion


    }
}
