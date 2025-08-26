using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimerForTwo;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class TriScooter : Templates.TimerForTwo.Business {

        #region Properties
        #endregion


        #region Funktionen

        public TriScooter()
            : base("Tri-Scooter") {
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.ShowFullscreenTimer = false;
            this.TimerStyle = Insert.Styles.ThreeDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion


    }
}
