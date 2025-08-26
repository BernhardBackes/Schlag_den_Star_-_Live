using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class KoerbeWerfen : GamePool.Templates.CounterScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public KoerbeWerfen()
            : base("Körbe werfen") {
            this.CounterScoreStyle = VentuzScenes.GamePool._Modules.CounterScore.Styles.FourDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
