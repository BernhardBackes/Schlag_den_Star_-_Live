using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class BlamierenOderKassieren : GamePool.Templates.BoK.Business {

        #region Properties
        #endregion


        #region Funktionen

        public BlamierenOderKassieren() : base("Blamieren oder Kassieren") {
            this.TimeoutPositionX = 0;
            this.TimeoutPositionY = 30;
            this.TimeoutIsVisible = true;
            this.ScorePositionY = 0;
            this.ScorePositionY = 30;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.SevenDots;
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

}
