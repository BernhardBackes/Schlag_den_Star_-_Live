using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class AugenZaehlen : GamePool.Templates.SingleCounterBuzzerScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public AugenZaehlen()
            : base("Augen zählen") {

            this.TimeoutPositionY = 30;
            this.TimeoutIsVisible = true;
            this.ScorePositionY = 30;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FiveDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
