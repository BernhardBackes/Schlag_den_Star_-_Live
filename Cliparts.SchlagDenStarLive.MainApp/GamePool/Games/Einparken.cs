using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Einparken : Templates.TimeToBeatAddition.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Einparken()
            : base("Einparken") {
            this.TimeToBeatStyle = VentuzScenes.GamePool.TimeToBeatAddition.Game.StyleElements.TwoRuns;
            this.TimeToBeatPositionX = 0;
            this.TimeToBeatPositionY = 0;
            this.ShowFullscreenTimer = false;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion


    }
}
