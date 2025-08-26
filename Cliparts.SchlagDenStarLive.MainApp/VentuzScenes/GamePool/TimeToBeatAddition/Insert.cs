using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimeToBeatAddition {


    //	[Path]=.Jingles.Play.Buzzer
    //	[Path]=.Jingles.Play.Stop

    public class Insert : _Base {

        #region Properties

        private const string sceneID = "project/gamepool/timetobeataddition/insert";

        public Game Game;

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
        }

        public void PlayBuzzerJingle() { this.SetDataItemTrigger(".Jingles.Play.Buzzer"); }
        public void PlayStopJingle() { this.SetDataItemTrigger(".Jingles.Play.Stop"); }

        public override void Dispose() {
            base.Dispose();
            this.Game.Dispose();
        }

        public override void Clear() {
            base.Clear();
            this.Game.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
