using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.Remembory {

    public class Host : _Base {

        //	[Path]=.SetOut
        //	[Path]=.ToOut
        //	[Path]=.SetIn
        //	[Path]=.ToIn
        //	[Path]=.Title
        //	[Path]=.Thumb_01.Filename
        //	[Path]=.Thumb_01.Text
        //	[Path]=.Thumb_01.Resolved
        //	[Path]=.Thumb_02.Filename
        //	[Path]=.Thumb_02.Text
        //	[Path]=.Thumb_02.Resolved
        //	[Path]=.Thumb_03.Filename
        //	[Path]=.Thumb_03.Text
        //	[Path]=.Thumb_03.Resolved
        //	[Path]=.Thumb_04.Filename
        //	[Path]=.Thumb_04.Text
        //	[Path]=.Thumb_04.Resolved
        //	[Path]=.Thumb_05.Filename
        //	[Path]=.Thumb_05.Text
        //	[Path]=.Thumb_05.Resolved
        //	[Path]=.Thumb_06.Filename
        //	[Path]=.Thumb_06.Text
        //	[Path]=.Thumb_06.Resolved
        //	[Path]=.Thumb_07.Filename
        //	[Path]=.Thumb_07.Text
        //	[Path]=.Thumb_07.Resolved
        //	[Path]=.Thumb_08.Filename
        //	[Path]=.Thumb_08.Text
        //	[Path]=.Thumb_08.Resolved

        #region Properties

        private const string sceneID = "project/gamepool/remembory/host";

        #endregion


        #region Funktionen

        public Host(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        public Host(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
            this.init();
        }

        private void init() {
        }

        public void Reset() { this.SetDataItemTrigger(".Reset"); }

        public void SetOut() { this.SetDataItemTrigger(".SetOut"); }
        public void ToOut() { this.SetDataItemTrigger(".ToOut"); }
        public void SetIn() { this.SetDataItemTrigger(".SetIn"); }
        public void ToIn() { this.SetDataItemTrigger(".ToIn"); }

        public void SetTitle(string value) { this.SetDataItemValue(".Title", value); }
        public void SetImagesFilename(
            int id,
            string value) {
            string name = string.Format(".Thumb_{0}.Filename", id.ToString("00"));
            this.SetDataItemValue(name, value);
        }
        public void SetImagesText(
            int id,
            string value) {
            string name = string.Format(".Thumb_{0}.Text", id.ToString("00"));
            this.SetDataItemValue(name, value);
        }
        public void SetImagesResolved(
            int id,
            bool value) {
            string name = string.Format(".Thumb_{0}.Resolved", id.ToString("00"));
            this.SetDataItemValue(name, value);
        }

        public override void Dispose() {
            base.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.ToOut();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
