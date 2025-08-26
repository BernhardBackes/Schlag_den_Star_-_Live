using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.PictureScore {

    public class Preview : _PreviewBase {

        /*
        [Path]= .ShowSafeArea (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Value (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= ShowSafeArea (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Boolean (type of the current instance)
            [Default]= False (the default value for this DataItem)
            [PropertyType]= System.Boolean (the underlaying system type of this DataItem)
         */

        #region Properties

        private const string sceneID = "project/gamepool/picturescore/preview";

        public Insert Insert;

        #endregion


        #region Funktionen

        public Preview(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
            this.init();
        }

        private void init() {
            this.Insert = new Insert(this.syncContext, this.addPort("InsertLayer"), Modes.Static);
        }

        public override void Dispose() {
            base.Dispose();
            this.Insert.Dispose();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
