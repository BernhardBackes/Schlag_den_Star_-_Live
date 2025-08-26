using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class ZieselDuell : Templates.TimerForTwo.Business {

        #region Properties
        #endregion


        #region Funktionen

        public ZieselDuell()
            : base("Ziesel-Duell") {
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.ShowFullscreenTimer = false;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion


    }
}
