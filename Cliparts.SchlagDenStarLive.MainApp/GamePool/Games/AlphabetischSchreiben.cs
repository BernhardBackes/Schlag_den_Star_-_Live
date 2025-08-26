using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class AlphabetischSchreiben : Templates.SortingLettersBuzzerScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public AlphabetischSchreiben()
            : base("Alphabetisch schreiben") {
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
            this.TextInsertPositionX = 0;
            this.TextInsertPositionY = 0;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
