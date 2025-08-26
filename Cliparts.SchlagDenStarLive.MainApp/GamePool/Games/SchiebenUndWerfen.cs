using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class SchiebenUndWerfen : GamePool.Templates.PenaltySoloScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public SchiebenUndWerfen()
            : base("Schieben & Werfen") {
            this.PenaltyPositionX = 0;
            this.PenaltyPositionY = 0;
            this.PenaltyDotsCount = 6;
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
