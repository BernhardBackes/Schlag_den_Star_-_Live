using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class DerStrohhalm : Templates.Penalty.Business {

        #region Properties
        #endregion


        #region Funktionen

        public DerStrohhalm()
            : base("Der Strohhalm") {
            this.PenaltyPositionX = 0;
            this.PenaltyPositionY = 0;
            this.PenaltyDotsCount = 10;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
