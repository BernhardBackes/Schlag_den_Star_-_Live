using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class EisRadRennen : Templates.SingleCounterOfSoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public EisRadRennen()
            : base("Eis-Rad-Rennen") {
            this.CounterPositionX = 0;
            this.CounterPositionY = 0;
            this.Title = "Runde";
            this.Target = 3;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
