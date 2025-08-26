using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.VideoMemory {

    public class Preview : _PreviewBase {

        /*
        [Path]= .Source (full path of the DataItem)
        [Description]=  (the description of the DataItem)
        [Label]= Input (the display label of the DataItem)
        [Mode]= RW (read/write mode of the DataItem)
        [Name]= Source (the name of the DataItem)
        [UserData]=  (user-defined information of the DataItem)
        [Type]= Ventuz.Remoting4.SceneModel.Enum (type of the current instance)
            [Default]= Insert (the default value for this DataItem)
            [Elements]= Insert,Fullscreen (an array of strings containing the choice of enum values)
            [PropertyType]= System.String (the underlaying system type of this DataItem)
         */
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

        public enum Sources { Insert, Fullscreen };

        #region Properties

        private const string sceneID = "project/gamepool/videomemory/preview";

        public Insert Insert;
        public Fullscreen Fullscreen;

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
            this.Fullscreen = new Fullscreen(this.syncContext, this.addPort("FullscreenLayer"), Modes.Static);
        }

        public void SetSource(Sources value) { this.SetDataItemValue(".Source", value); }

        public override void Dispose() {
            base.Dispose();
            this.Insert.Dispose();
            this.Fullscreen.Dispose();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
