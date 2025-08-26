using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class WerSindDie : Templates.TwoFlipsMovieScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public WerSindDie()
            : base("Wer sind die?") {
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.SixDots;
            this.TimeoutIsVisible = true;
            this.TimeoutPositionX = 115;
            this.TimeoutPositionY = -15;
            this.BorderStyle = VentuzScenes.GamePool._Modules.Border.Styles.SixDots;
            this.BorderPositionX = 0;
            this.BorderPositionY = -90;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
