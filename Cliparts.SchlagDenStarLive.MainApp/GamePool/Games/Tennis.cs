using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Tennis : Templates.Score.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Tennis()
            : base("Tennis") {
            this.ScorePositionX = 542;
            this.ScorePositionY = -900;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Sport;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
