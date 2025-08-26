using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Drumometer : Templates.BorderScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Drumometer() : base("Drumometer") {
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots;
            this.BorderStyle = VentuzScenes.GamePool._Modules.Border.Styles.Names;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
