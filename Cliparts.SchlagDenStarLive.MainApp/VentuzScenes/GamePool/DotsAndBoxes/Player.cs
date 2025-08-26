using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.DotsAndBoxes {

    //	[Path]=.MainFader.Reset
    //	[Path]=.MainFader.SetOut
    //	[Path]=.MainFader.ToOut
    //	[Path]=.MainFader.SetIn
    //	[Path]=.MainFader.ToIn
    //	[Path]=.LeftPlayerCounter
    //	[Path]=.RightPlayerCounter
    //	[Path]=.TimerPosition

    public class Player : _Base {

        public enum Positions { Left, Right }

        #region Properties

        private const string sceneID = "project/gamepool/dotsandboxes/player";

        public Game Game;
        public _Modules.Timer Timer;

        #endregion


        #region Funktionen

        public Player(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Port port,
            Modes mode)
            : base(syncContext, port, sceneID, mode) {
            this.init();
        }

        public Player(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Pipe.Business pipe)
            : base(syncContext, pipe, sceneID) {
            this.init();
        }

        private void init() {
            this.Game = new Game(syncContext, this.addPort("GameLayer"));
            this.Game.PropertyChanged += this.game_PropertyChanged;
            this.Timer = new _Modules.Timer(syncContext, this.addPort("TimerLayer"));
        }

        public void SetLeftPlayerCounter(int value) { this.SetDataItemValue(".LeftPlayerCounter", value); }
        public void SetRightPlayerCounter(int value) { this.SetDataItemValue(".RightPlayerCounter", value); }

        public void SetTimerPosition(Positions value) { this.SetDataItemValue(".TimerPosition", value); }

        public void FadeIn() { this.SetDataItemTrigger(".MainFader.ToIn"); }
        public void FadeOut() { this.SetDataItemTrigger(".MainFader.ToOut"); }

        public override void Dispose() {
            base.Dispose();
            this.Game.PropertyChanged -= this.game_PropertyChanged;
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

        void game_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_game_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_game_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "LeftPlayerCounter") this.SetLeftPlayerCounter(this.Game.LeftPlayerCounter);
                else if (e.PropertyName == "RightPlayerCounter") this.SetRightPlayerCounter(this.Game.RightPlayerCounter);
            }
        }

        #endregion

    }

}
