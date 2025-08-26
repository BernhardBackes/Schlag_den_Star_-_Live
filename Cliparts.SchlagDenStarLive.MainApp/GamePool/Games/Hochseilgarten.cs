using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Hochseilgarten : GamePool.Templates.TimeToBeatLAPs.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Hochseilgarten()
            : base("Hochseilgarten") {
            this.LAPsCount = 3;
            this.TimeToBeatStyle = VentuzScenes.GamePool._Modules.TimeToBeat.Styles.StopwatchName;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}

