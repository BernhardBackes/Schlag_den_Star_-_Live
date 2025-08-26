using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class TrouMadame : GamePool.Templates.CounterScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public TrouMadame()
            : base("Trou Madame") {
            this.CounterScoreStyle = VentuzScenes.GamePool._Modules.CounterScore.Styles.ThreeDots;
            this.WinnerMode = Templates._Base.CounterScore.WinnerModes.HighestCounter;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}

