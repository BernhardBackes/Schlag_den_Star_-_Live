using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Luftlinie : Templates.NumericInputCloserToValue.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Luftlinie()
            : base("Luftlinie") {

            this.TextInsertPositionX = 0;
            this.TextInsertPositionY = -12;
            this.TextInsertStyle = VentuzScenes.GamePool._Modules.TextInsert.Styles.OneRow;

            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FiveDots;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;

            this.InputInsertPositionX = 66;
            this.InputInsertPositionY = 8;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
