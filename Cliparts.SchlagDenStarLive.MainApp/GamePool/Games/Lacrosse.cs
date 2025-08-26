using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Lacrosse : Templates.SetsPenalty.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Lacrosse()
            : base("Lacrosse") {
            this.SetsPositionX = 0;
            this.SetsPositionY = 0;
            this.SetsStyle = VentuzScenes.GamePool.SetsPenalty.Insert.Styles.FiveSets;
            this.PenaltyShots = VentuzScenes.GamePool.SetsPenalty.Insert.PenaltyShots.FiveShots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
