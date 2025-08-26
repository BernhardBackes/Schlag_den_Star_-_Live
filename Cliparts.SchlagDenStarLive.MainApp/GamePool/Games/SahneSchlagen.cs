using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class SahneSchlagen : Templates.Timer.Business {

        #region Properties
        #endregion


        #region Funktionen

        public SahneSchlagen()
            : base("Sahne schlagen") {
            this.Vinsert_Timer.PositionX = 0;
            this.Vinsert_Timer.PositionY = 0;
            this.Vinsert_Timer.AlarmTime1 = 10;
            this.Vinsert_Timer.AlarmTime2 = -1;
            this.Vinsert_Timer.StartTime = 10;
            this.Vinsert_Timer.StopTime = 0;
            this.Vinsert_Timer.Style = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
            this.ShowFullscreenTimer = true;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
