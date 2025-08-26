using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool {

    public class WannWarDasFoto : Templates.FullscreenPictureNumericInputAddDifference.Business {

        #region Properties
        #endregion


        #region Funktionen

        public WannWarDasFoto()
            : base("Wann war das Foto?") {
            this.BorderPositionX = 0;
            this.BorderPositionY = 0;
            this.BorderScaling = 100;
            this.BorderStyle = VentuzScenes.GamePool._Modules.Border.Styles.Colored;
            this.GamePositionX = 0;
            this.GamePositionY = 0;
            this.TextInsertPositionX = 0;
            this.TextInsertPositionY = 0;
            this.TextInsertStyle = VentuzScenes.GamePool._Modules.TextInsert.Styles.Short;
            this.TaskCounterPositionX = 0;
            this.TaskCounterPositionY = 0;
            this.TaskCounterSize = 10;
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
