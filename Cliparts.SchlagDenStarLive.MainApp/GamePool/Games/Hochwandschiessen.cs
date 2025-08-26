using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class Hochwandschiessen : Templates.Sets.Business {

        #region Properties
        #endregion


        #region Funktionen

        public Hochwandschiessen()
            : base("Hochwandschießen") {
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
