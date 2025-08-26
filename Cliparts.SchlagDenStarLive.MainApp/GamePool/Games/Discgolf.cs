using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Discgolf : GamePool.Templates.Sets.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Discgolf() : base("Discgolf") {
            this.SetsStyle = VentuzScenes.GamePool._Modules.Sets.StyleElements.TwoSetsSum;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
