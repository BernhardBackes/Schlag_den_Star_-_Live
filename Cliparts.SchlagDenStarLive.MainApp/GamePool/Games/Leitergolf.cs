using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Leitergolf : GamePool.Templates.BondedDots.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Leitergolf()
            : base("Leitergolf") {
            this.ShowFullscreenTimer = false;
            this.BondedDotsStyle = VentuzScenes.GamePool._Modules.BondedDots.Styles.FifteenDots;
            this.BondedDotsPositionX = 0;
            this.BondedDotsPositionY = 0;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
