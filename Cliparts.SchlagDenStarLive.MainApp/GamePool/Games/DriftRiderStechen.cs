using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class DriftRiderStechen : Templates.TimeToBeat.Business {

        #region Properties
        #endregion


        #region Funktionen

        public DriftRiderStechen()
            : base("Drift-Rider Stechen") {
            this.TimeToBeatStyle = VentuzScenes.GamePool._Modules.TimeToBeat.Styles.Stopwatch;
            this.TimeToBeatPositionX = 0;
            this.TimeToBeatPositionY = 0;
            this.ShowFullscreenTimer = false;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion


    }
}
