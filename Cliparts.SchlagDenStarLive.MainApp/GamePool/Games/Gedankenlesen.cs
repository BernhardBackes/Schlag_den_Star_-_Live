using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Gedankenlesen : Templates.TextInsertTeamInputScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Gedankenlesen()
            : base("Gedankenlesen") {
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

    //public class Gedankenlesen : Templates.TextInsertInputScore.Business {

    //    #region Properties
    //    #endregion


    //    #region Funktionen

    //    public Gedankenlesen()
    //        : base("Gedankenlesen") {
    //        this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
    //    }

    //    #endregion


    //    #region Events.Outgoing
    //    #endregion

    //    #region Events.Incoming
    //    #endregion

    //}
}
