using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Musikvideos : Templates.BuzzerStopBorderedMovieLoopSolutionScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Musikvideos()
            : base("Musikvideos") {
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
            this.TimeoutPositionY = 20;
            this.TimeoutPositionY = 0;
            this.TimeoutIsVisible = false;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.BorderPositionX = 0;
            this.BorderPositionY = -100;
            this.BorderStyle = VentuzScenes.GamePool._Modules.Border.Styles.Counter;
            this.LoopTask = false;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
