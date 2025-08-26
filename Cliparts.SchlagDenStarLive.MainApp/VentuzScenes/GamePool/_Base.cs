using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool {

    public class _Base : VRemote4.HandlerSi.Scene, INotifyPropertyChanged {

        #region Properties
        #endregion


        #region Funktionen

        public _Base(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            string sceneID,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
        }

        public _Base(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe,
            string sceneID)
            : base(syncContext, pipe, sceneID) {
        }

        public _Base(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Business client,
            int pipeIndex,
            string sceneID)
            : base(syncContext, client, pipeIndex, sceneID) {
        }

        public override void Dispose() {
            base.Dispose();
        }

        public virtual void Clear() {}

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

    public class Dummy : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/dummy";

        #endregion


        #region Funktionen

        public Dummy(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        public Dummy(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
            this.init();
        }

        private void init() {
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
