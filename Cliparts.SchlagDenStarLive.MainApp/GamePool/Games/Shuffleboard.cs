using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Shuffleboard : GamePool.Templates.CounterScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Shuffleboard()
            : base("Shuffleboard") {
            this.CounterScoreStyle = VentuzScenes.GamePool._Modules.CounterScore.Styles.ZeroDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
