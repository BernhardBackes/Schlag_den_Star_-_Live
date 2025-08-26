using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class DoubleDiscCourt : Templates.CounterScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public DoubleDiscCourt()
            : base("Double Disc Court") {
            this.HidePlayerCounterByDefault = false;
            this.CounterScorePositionX = 0;
            this.CounterScorePositionY = 0;
            this.CounterScoreStyle = VentuzScenes.GamePool._Modules.CounterScore.Styles.ThreeDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
