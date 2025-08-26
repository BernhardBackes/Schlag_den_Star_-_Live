using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class DerMuehlstein : Templates.TargetCounter.Business {

        #region Properties
        #endregion


        #region Funktionen

        public DerMuehlstein()
            : base("Der Mühlstein") {
            this.TargetCounterPositionX = 0;
            this.TargetCounterPositionY = 0;
            this.StartValue = 460;
            this.TargetValue = 0;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

    //public class DerMuehlstein : Templates.SetsScore.Business {

    //    #region Properties
    //    #endregion


    //    #region Funktionen

    //    public DerMuehlstein()
    //        : base("Der Mühlstein") {
    //        this.SetsStyle = VentuzScenes.GamePool._Modules.Sets.Styles.ThreeSetsSum;
    //        this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FiveDots;
    //    }

    //    #endregion


    //    #region Events.Outgoing
    //    #endregion

    //    #region Events.Incoming
    //    #endregion

    //}

}
