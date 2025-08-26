using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class WoerterFinden : GamePool.Templates.SelectWordInText.Business {

        #region Properties
        #endregion


        #region Funktionen

        public WoerterFinden()
            : base("Wörter finden") {
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.SixDots;
            this.TimeoutPositionY = 20;
            this.TimeoutPositionY = 0;
            this.TimeoutIsVisible = false;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.BorderPositionX = 0;
            this.BorderPositionY = -100;
            this.BorderStyle = VentuzScenes.GamePool._Modules.Border.Styles.SixDots;
            this.TextInsertPositionX = 0;
            this.TextInsertPositionY = -30;
            this.TextInsertStyle = VentuzScenes.GamePool._Modules.TextInsert.Styles.Small;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
