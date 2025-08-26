using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class FlascheWerfen : GamePool.Templates.Penalty.Business {

        #region Properties
        #endregion


        #region Funktionen

        public FlascheWerfen() : base("Flasche werfen") {
            this.PenaltyDotsCount = 5;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}