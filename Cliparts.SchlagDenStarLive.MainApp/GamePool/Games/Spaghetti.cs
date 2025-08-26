using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Spaghetti : GamePool.Templates.Score.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Spaghetti()
            : base("Spaghetti") {
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.SevenDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
