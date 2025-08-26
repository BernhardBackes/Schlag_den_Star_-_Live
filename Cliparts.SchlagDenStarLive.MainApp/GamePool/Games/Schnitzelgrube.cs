using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Schnitzelgrube : Templates.TimeToBeatLAPsScore.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Schnitzelgrube()
            : base("Schnitzelgrube") {
            this.TimeToBeatPositionX = 0;
            this.TimeToBeatPositionY = 0;
            this.TimeToBeatStyle = VentuzScenes.GamePool._Modules.TimeToBeat.Styles.Stopwatch;
            this.SentenceTime = 5;
            this.ScorePositionX = 0;
            this.ScorePositionY = 0;
            this.ScoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.FourDots;
            this.LAPsCount = 1;
            this.SentenceTime = 5;
        }

        public override void DoBuzzer() {
            base.DoBuzzer();
            this.Vinsert_PlaySound_Hit();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
