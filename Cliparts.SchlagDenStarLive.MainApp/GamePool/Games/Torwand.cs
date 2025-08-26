using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Torwand : Templates.Penalty.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Torwand()
            : base("Torwand") {
            this.PenaltyDotsCount = 8;
            this.PenaltyPositionX = 0;
            this.PenaltyPositionY = 0;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
