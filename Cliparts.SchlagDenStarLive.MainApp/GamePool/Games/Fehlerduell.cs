using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Fehlerduell : Templates.SelectDifferences4x4.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Fehlerduell()
            : base("Fehlerduell") {
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FiveDots;
            this.BackgroundStyle = VentuzScenes.GamePool.SelectDifferences4x4.Insert.Style.FiveDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
