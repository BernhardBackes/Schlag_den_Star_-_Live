using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Vornamen : GamePool.Templates.TurnNameTrueFalseCountdownScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Vornamen()
            : base("Vornamen") {
            this.TextInsertPositionX = 0;
            this.TextInsertPositionY = 0;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FiveDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
