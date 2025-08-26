using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class FrissDieHaelfte : Templates.NumDifferenceScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public FrissDieHaelfte()
            : base("Friss die Hälfte") {
            this.NumericValuesPositionX = 0;
            this.NumericValuesPositionY = 0;

            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FourDots;

            this.Show1stHalf = true;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
