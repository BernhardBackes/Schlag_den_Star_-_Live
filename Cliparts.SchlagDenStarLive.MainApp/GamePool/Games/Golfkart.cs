using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Golfkart : Templates.TimerForTwoSoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Golfkart()
            : base("Golfkart") {
            this.TimerPositionX = 0;
            this.TimerPositionY = 0;
            this.ShowFullscreenTimer = false;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.BuzzerMode = true;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion


    }
}
