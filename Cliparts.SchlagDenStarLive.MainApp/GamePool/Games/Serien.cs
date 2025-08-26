using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Serien : Templates.SixHintsOneSolution.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Serien()
            : base("Abrollen") {
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.SixDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
