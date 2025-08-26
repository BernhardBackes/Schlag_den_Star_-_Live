using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TextInsertBuzzerAudioScore {

    public class Preview : _PreviewBase {

        public enum Sources { Insert, Stage };

        #region Properties

        private const string sceneID = "project/gamepool/textinsertbuzzeraudioscore/preview";

        public Insert Insert;
        public Stage Stage;

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
            this.Stage = new Stage(this.syncContext, this.addPort("StageLayer"), Modes.Static);
        }

        public void SetSource(Sources value) { this.SetDataItemValue(".Source", value); }

        public override void Dispose() {
            base.Dispose();
            this.Insert.Dispose();
            this.Stage.Dispose();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
