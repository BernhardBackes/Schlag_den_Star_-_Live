using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Laenderkunde : Templates.WindingQuiz.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Laenderkunde()
            : base("Länderkunde") {
            this.TimeoutPositionY = 30;
            this.TimeoutIsVisible = true;
            this.ScorePositionX = 0;
            this.ScorePositionY = 30;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.SixDots;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
