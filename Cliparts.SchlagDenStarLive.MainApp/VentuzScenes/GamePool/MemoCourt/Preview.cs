using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MemoCourt {

    public class Preview : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/memocourt/preview";

        public Fullscreen Fullscreen;

        public Insert Insert;

        #endregion


        #region Funktionen

        public Preview(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        public Preview(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
            this.init();
        }

        private void init() {
            this.Fullscreen = new Fullscreen(syncContext, this.addPort("FullscreenLayer"), Modes.Static);
            this.Insert = new Insert(syncContext, this.addPort("InsertLayer"), Modes.Static);
        }

        public void SetFullscreenIsVisible(bool value) { this.SetDataItemValue(".Fullscreen.IsVisible", value); }
        public void SetInsertIsVisible(bool value) { this.SetDataItemValue(".Insert.IsVisible", value); }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion


    }
}
