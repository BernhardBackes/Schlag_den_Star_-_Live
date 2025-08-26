using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Unsichtball : Templates.CounterSoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Unsichtball()
            : base("Unsichtball") {
            this.CounterStyle = VentuzScenes.GamePool._Modules.Score.Styles.Sport;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.TwoDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}

