using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class QuadFatBike : Templates.LAPTimeToBeatScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public QuadFatBike()
            : base("Quad Fat Bike") {
            this.TimeToBeatPositionX = 0;
            this.TimeToBeatPositionY = 0;
            this.TimeToBeatStyle = VentuzScenes.GamePool._Modules.TimeToBeat.Styles.StopwatchNameCounter1;
            this.SentenceTime = 5;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots;
            this.LAPsCount = 3;
            this.SentenceTime = 5;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
