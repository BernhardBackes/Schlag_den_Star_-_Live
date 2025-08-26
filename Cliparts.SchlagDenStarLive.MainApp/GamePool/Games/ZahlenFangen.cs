using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class ZahlenFangen : GamePool.Templates.TimeToBeat.Business {

        #region Properties
        #endregion


        #region Funktionen

        public ZahlenFangen()
            : base("Zahlen fangen") {

                this.TimeToBeatStopTime = 300;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
