using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes {

    public class _PreviewBase : GamePool._Base {

        //	[Path]=.ShowSafeArea

        #region Properties
        #endregion


        #region Funktionen

        public _PreviewBase(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe,
            string sceneID)
            : base(syncContext, pipe, sceneID) {
        }

        public void SetShowSafeArea(bool value) { this.SetDataItemValue(".ShowSafeArea", value); }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion


    }

}
