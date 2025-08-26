using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class EinhandTischtennis : GamePool.Templates.Sets.Business {

        #region Properties
        #endregion


        #region Funktionen

        public EinhandTischtennis()
            : base("Einhand-Tischtennis") {
            this.SetsStyle = VentuzScenes.GamePool._Modules.Sets.StyleElements.ThreeSetsSum;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
