using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Klask : Templates.CounterSoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Klask()
            : base("Klask") {
            this.CounterStyle = VentuzScenes.GamePool._Modules.Score.Styles.SixDots;
            this.CounterPositionX = 0;
            this.CounterPositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.TwoDots;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
