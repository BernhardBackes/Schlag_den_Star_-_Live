using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class RueckenSchreiben : Templates.TextInsertScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public RueckenSchreiben()
            : base("Rücken schreiben") {
            this.TextInsertPositionX = 0;
            this.TextInsertPositionY = -30;
            this.TextInsertStyle = VentuzScenes.GamePool._Modules.TextInsert.Styles.OneRow;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
