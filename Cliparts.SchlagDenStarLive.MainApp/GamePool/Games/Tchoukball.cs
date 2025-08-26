using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Tchoukball : GamePool.Templates.Score.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Tchoukball()
            : base("Tchoukball") {
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.CounterLeft;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
