using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class SpitzPassAuf : Templates.CounterSoloScore.Business
    {

        #region Properties
        #endregion


        #region Funktionen

        public SpitzPassAuf() : base("Spitz, pass auf!")
        {
            this.CounterStyle = VentuzScenes.GamePool._Modules.Score.Styles.FourDots;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.TwoDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

    //public class SpitzPassAuf : Templates.Score.Business {

    //    #region Properties
    //    #endregion


    //    #region Funktionen

    //    public SpitzPassAuf() : base("Spitz, pass auf!") {
    //        this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.SevenDots;
    //    }

    //    #endregion


    //    #region Events.Outgoing
    //    #endregion

    //    #region Events.Incoming
    //    #endregion

    //}

}
