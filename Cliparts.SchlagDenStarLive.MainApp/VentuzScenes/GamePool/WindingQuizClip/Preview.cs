using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.WindingQuizClip {

    public class Preview : _PreviewBase {

        public enum Sources { Insert, Host };

        #region Properties

        private const string sceneID = "project/gamepool/windingquizclip/preview";

        public Insert Insert;
        public Host Host;

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
            this.Host = new Host(this.syncContext, this.addPort("HostLayer"), Modes.Static);
        }

        public void SetSource(Sources value) { this.SetDataItemValue(".Source", value); }

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
