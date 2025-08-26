using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class SchnitzelgrubenDuell : Templates.TimerForTwoStopoverAddition.Business {

        #region Properties
        #endregion


        #region Funktionen

        public SchnitzelgrubenDuell()
            : base("Schnitzelgruben-Duell") {
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
