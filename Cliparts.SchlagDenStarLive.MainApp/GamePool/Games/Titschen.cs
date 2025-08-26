using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Titschen : GamePool.Templates.SetsScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Titschen()
            : base("Titschen") {
            this.SetsStyle = VentuzScenes.GamePool._Modules.Sets.StyleElements.FiveSetsSum;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
