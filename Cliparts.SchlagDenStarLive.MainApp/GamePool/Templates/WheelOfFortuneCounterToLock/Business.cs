using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cliparts.Serialization;

using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;
using Cliparts.SchlagDenStarLive.NumericSelectApp;
using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.WheelOfFortuneCounterToLock;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.WheelOfFortuneCounterToLock {

    public class Business : _Base.CounterToLock.Business {

        #region Properties

        private PlayerSelection selectedPlayer = PlayerSelection.LeftPlayer;
        public PlayerSelection SelectedPlayer {
            get { return this.selectedPlayer; }
            set {
                if (this.selectedPlayer != value) {
                    if (value == PlayerSelection.NotSelected) value = PlayerSelection.LeftPlayer;
                    this.selectedPlayer = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int wheelValue = 0;
        public int WheelValue {
            get { return this.wheelValue; }
            set {
                if (this.wheelValue != value) {
                    if (value < Fullscreen.ValueMinimum) this.wheelValue = Fullscreen.ValueMinimum;
                    else if (value > Fullscreen.ValueMaximum) this.wheelValue = Fullscreen.ValueMaximum;
                    else this.wheelValue = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool overrun = false;

        private Insert insertScene;
        public override VRemote4.HandlerSi.Scene.States InsertSceneStatus {
            get {
                if (this.insertScene is Insert) return this.insertScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private Fullscreen fullscreenScene;
        public override VRemote4.HandlerSi.Scene.States FullscreenSceneStatus {
            get {
                if (this.fullscreenScene is Fullscreen) return this.fullscreenScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        #endregion


        #region Funktionen

        public Business() { }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.WheelOfFortuneCounterToLock'", typeIdentifier);
        }

        public override void Pose(
            SynchronizationContext syncContext,
            MidiHandler.Business midiHandler,
            BuzzerIO.Business buzzerHandler,
            AMB.Business ambHandler,
            Devantech.Controller devantechHandler,
            Content.Gameboard.Business gameboard,
            VentuzScenes.Insert.Business insertMasterScene,
            VentuzScenes.Fullscreen.Business fullscreenMasterScene,
            VentuzScenes.Host.Business hostMasterScene,
            VentuzScenes.Player.Business leftPlayerMasterScene,
            VentuzScenes.Player.Business rightPlayerMasterScene,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {

            base.Pose(syncContext,
                midiHandler, buzzerHandler, ambHandler, devantechHandler, gameboard,
                insertMasterScene, fullscreenMasterScene, hostMasterScene, leftPlayerMasterScene, rightPlayerMasterScene, previewPipe);

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;

            this.fullscreenScene = new Fullscreen(syncContext, fullscreenMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.fullscreenScene.StatusChanged += this.fullscreenScene_StatusChanged;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);
        }

        public override void Dispose() {
            base.Dispose();

            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Dispose();

            this.fullscreenScene.StatusChanged -= this.fullscreenScene_StatusChanged;
            this.fullscreenScene.Dispose();
        }

        public override void ResetData() {
            base.ResetData();
            this.overrun = false;
            this.Next();
        }

        public override void Next() {
            base.Next();
            this.WheelValue = 0;
            this.Vfullscreen_ResetValue();
            if (this.overrun) this.SwitchPlayer();
            this.overrun = false;
        }

        internal override void FailHot() {
            base.FailHot();
            this.overrun = true;
            this.WheelBad();
        }

        internal override void LockHot() {
            base.LockHot();
            this.overrun = true;
            this.WheelGood();
        }

        internal void AddWheelValue() {
            this.WheelValue++;
            if (!this.overrun) {
                int targetValue = int.MaxValue;
                switch (this.SelectedPlayer) {
                    case PlayerSelection.LeftPlayer:
                        targetValue = this.LeftPlayerCounter + 1;
                        break;
                    case PlayerSelection.RightPlayer:
                        targetValue = this.RightPlayerCounter + 1;                        
                        break;
                }
                if (this.WheelValue > targetValue) this.FailHot();
            }
        }
        internal void WheelGood() {
            this.Vfullscreen_PlayJingleGood();
        }
        internal void WheelBad() {
            this.Vfullscreen_PlayJingleBad();
        }

        internal void SwitchPlayer() {
            if (this.SelectedPlayer == PlayerSelection.LeftPlayer) this.SelectedPlayer = PlayerSelection.RightPlayer;
            else this.SelectedPlayer = PlayerSelection.LeftPlayer;
        }

        public override void Vinsert_LoadScene() { this.insertScene.Load(); }
        public override void Vinsert_CounterToLockIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_CounterToLockIn(this.insertScene.CounterToLock); }
        public override void Vinsert_SetCounterToLock() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetCounterToLock(this.insertScene.CounterToLock); }
        public override void Vinsert_CounterToLockOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_CounterToLockOut(this.insertScene.CounterToLock); }
        public override void Vinsert_UnloadScene() { this.insertScene.Unload(); }

        public override void Vfullscreen_LoadScene() { this.fullscreenScene.Load(); }
        public void Vfullscreen_ResetValue() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.ResetValue(); }
        public void Vfullscreen_SetValue(int value) { if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.SetValue(value); }
        public void Vfullscreen_StopAudio() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.StopAudio(); }
        public void Vfullscreen_StartCountdown() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.StartCountdown(); }
        public void Vfullscreen_PlayJingleGood() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.PlayJingleGood(); }
        public void Vfullscreen_PlayJingleBad() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.PlayJingleBad(); }
        public override void Vfullscreen_UnloadScene() { this.fullscreenScene.Unload(); }

        public override void ClearGraphic() {
            base.ClearGraphic();
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Clear();
            if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming

        #endregion
    }
}
