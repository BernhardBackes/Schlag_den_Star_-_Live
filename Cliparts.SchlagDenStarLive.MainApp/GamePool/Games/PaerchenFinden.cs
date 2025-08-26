using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class PaerchenFinden : GamePool.Templates.TwoPicturesFade.Business {

        #region Properties
        #endregion


        #region Funktionen

        public PaerchenFinden()
            : base("Pärchen finden") {
            this.TimeoutPositionX = 17;
            this.TimeoutPositionY = 30;
            this.TimeoutIsVisible = true;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.SixDots;
            this.BorderPositionX = 0;
            this.BorderPositionY = -80;
            this.BorderStyle = VentuzScenes.GamePool._Modules.Border.Styles.SixDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
