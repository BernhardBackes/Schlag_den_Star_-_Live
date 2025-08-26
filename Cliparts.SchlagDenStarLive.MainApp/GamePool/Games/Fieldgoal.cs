using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Fieldgoal : Templates.PenaltyIcons.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Fieldgoal()
            : base("Fieldgoal") {
            this.PenaltyPositionX = 0;
            this.PenaltyPositionY = 0;
            this.PenaltyStyle = VentuzScenes.GamePool._Modules.PenaltyIcons.Styles.Football_3x3;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
