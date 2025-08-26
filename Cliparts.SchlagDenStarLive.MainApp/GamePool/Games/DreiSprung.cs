using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class DreiSprung : Templates.DecimalAdditionScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public DreiSprung()
            : base("Drei-Sprung") {

            this.DecimalAdditionPositionX = 0;
            this.DecimalAdditionPositionY = 0;
            this.DecimalAdditionStyle = VentuzScenes.GamePool._Modules.DecimalAddition.Styles.FourSets;
            this.ValidSetsCount = 3;

            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
