using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class DosenOeffnen : GamePool.Templates.Score.Business {

        #region Properties
        #endregion


        #region Funktionen

        public DosenOeffnen() : base("Dosen öffnen") {
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
            this.ScorePositionX = 90;
            this.ScorePositionY = -40;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}