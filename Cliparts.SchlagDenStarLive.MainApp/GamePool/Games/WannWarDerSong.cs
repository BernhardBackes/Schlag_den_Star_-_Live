using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {    

    public class WannWarDerSong : Templates.AudioInsertTeamNumericInputAddDifference.Business
    {

        #region Properties
        #endregion


        #region Funktionen

        public WannWarDerSong()
            : base("Wann war der Song?")
        {
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
            this.GamePositionX = 0;
            this.GamePositionY = 0;
            this.TaskCounterPositionX = 0;
            this.TaskCounterPositionY = 0;
            this.TaskCounterSize = 13;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

    //public class WannWarDerSong : Templates.AudioInsertNumericInputAddDifference.Business
    //{

    //    #region Properties
    //    #endregion


    //    #region Funktionen

    //    public WannWarDerSong()
    //        : base("Wann war der Song?")
    //    {
    //        this.ScorePositionX = 0;
    //        this.ScorePositionY = 0;
    //        this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
    //        this.GamePositionX = 0;
    //        this.GamePositionY = 0;
    //        this.TaskCounterPositionX = 0;
    //        this.TaskCounterPositionY = 0;
    //        this.TaskCounterSize = 13;
    //    }

    //    #endregion


    //    #region Events.Outgoing
    //    #endregion

    //    #region Events.Incoming
    //    #endregion

    //}

}
