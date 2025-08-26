using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Buchstaben : GamePool.Templates.LettersCount.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Buchstaben()
            : base("Buchstaben") {
            this.TextInsertPositionX = 0;
            this.TextInsertPositionY = -30;
            this.TimeoutPositionX = 0;
            this.TimeoutPositionY = 0;
            this.TimeoutIsVisible = false;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FiveDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
