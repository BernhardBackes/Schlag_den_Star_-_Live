using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class StimmenErkennen : Templates.BuzzerScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public StimmenErkennen()
            : base("Stimmen erkennen") {
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.TimeoutPositionX = 0;
            this.TimeoutPositionY = 0;
            this.TimeoutDuration = VentuzScenes.GamePool._Modules.Timeout.Duration.FiveSeconds;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
