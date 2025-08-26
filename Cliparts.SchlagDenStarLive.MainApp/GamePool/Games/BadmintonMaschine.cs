using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class BadmintonMaschine : GamePool.Templates.CounterPointerScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public BadmintonMaschine()
            : base("Badminton-Maschine") {
            this.ScorePointerPositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots;
            this.ScorePointerPositionX = 0;
            this.ScorePointerPositionY = 0;
            this.ScorePointerStyle = VentuzScenes.GamePool._Modules.ScorePointer.Styles.Points20;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
