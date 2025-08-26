using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class RoadTennis : GamePool.Templates.Score.Business {

        #region Properties
        #endregion


        #region Funktionen

        public RoadTennis() : base("Road Tennis") {
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Sport;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
