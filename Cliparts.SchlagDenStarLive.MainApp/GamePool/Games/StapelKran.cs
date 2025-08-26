using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class StapelKran : Templates.Timer.Business {

        #region Properties
        #endregion


        #region Funktionen

        public StapelKran()
            : base("Stapel-Kran") {
            this.Vinsert_Timer.AlarmTime1 = -1;
            this.Vinsert_Timer.AlarmTime2 = -1;
            this.Vinsert_Timer.StopTime = 59999;
            this.Vinsert_Timer.StartTime = 00;
            this.Vinsert_Timer.ExtraTime = 0;
            this.Vinsert_Timer.PositionX = 0;
            this.Vinsert_Timer.PositionY = 0;
            this.Vinsert_Timer.Style = VentuzScenes.GamePool._Modules.Timer.Styles.MinSec;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
