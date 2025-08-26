using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Cornhole : GamePool.Templates.CounterScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Cornhole()
            : base("Cornhole") {
            this.CounterScoreStyle = VentuzScenes.GamePool._Modules.CounterScore.Styles.ThreeDots;
            this.CounterScorePositionX = 0;
            this.CounterScorePositionY = 0;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion


    }
}
