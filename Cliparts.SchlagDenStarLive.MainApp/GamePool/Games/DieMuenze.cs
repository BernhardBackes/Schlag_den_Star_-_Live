using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class DieMuenze : GamePool.Templates.Penalty.Business {

        #region Properties
        #endregion


        #region Funktionen

        public DieMuenze()
            : base("Die Münze") {
            this.PenaltyDotsCount = 10;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
