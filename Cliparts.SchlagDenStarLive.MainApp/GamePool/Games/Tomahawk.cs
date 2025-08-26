using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Tomahawk : GamePool.Templates.Sets.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Tomahawk()
            : base("Tomahawk") {
            this.SetsPositionX = 0;
            this.SetsPositionY = 0;
            this.SetsStyle = VentuzScenes.GamePool._Modules.Sets.StyleElements.EightSetsSum;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
