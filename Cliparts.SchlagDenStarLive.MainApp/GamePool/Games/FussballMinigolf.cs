using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class FussballMinigolf : GamePool.Templates.Sets.Business {

        #region Properties
        #endregion


        #region Funktionen

        public FussballMinigolf()
            : base("Fußball-Minigolf") {
            this.SetsStyle = VentuzScenes.GamePool._Modules.Sets.StyleElements.FourSetsSum;
            this.SetsPositionX = 0;
            this.SetsPositionY = 0;
        }
        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}