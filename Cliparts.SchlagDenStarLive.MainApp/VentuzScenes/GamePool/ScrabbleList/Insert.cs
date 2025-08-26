using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.ScrabbleList {

    public class Insert : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/scrabblelist/insert";

        public Game Game;
        public _Modules.Sets Sets;
        public _Modules.Timer Timer;


        #endregion


        #region Funktionen

        public Insert(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        public Insert(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
            this.init();
        }

        private void init() {
            this.Game = new Game(syncContext, this.addPort("GameLayer"));
            this.Sets = new _Modules.Sets(syncContext, this.addPort("SetsLayer"));
            this.Timer = new _Modules.Timer(syncContext, this.addPort("TimerLayer"));
        }


        public override void Dispose() {
            base.Dispose();
            this.Game.Dispose();
            this.Sets.Dispose();
            this.Timer.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.Game.Clear();
            this.Sets.Clear();
            this.Timer.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
