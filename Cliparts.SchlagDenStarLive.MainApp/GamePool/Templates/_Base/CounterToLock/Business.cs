using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.CounterToLock {

    public class Business : _Base.Business {

        #region Properties

        private int leftPlayerCounter = 0;
        public int LeftPlayerCounter {
            get { return this.leftPlayerCounter; }
            set {
                if (this.leftPlayerCounter != value) {
                    if (value < 0) this.leftPlayerCounter = 0;
                    else if (value < this.LeftPlayerLocked) this.leftPlayerCounter = this.LeftPlayerLocked;
                    else this.leftPlayerCounter = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int leftPlayerLocked = 0;
        public int LeftPlayerLocked {
            get { return this.leftPlayerLocked; }
            set {
                if (this.leftPlayerLocked != value) {
                    if (value < 0) this.leftPlayerLocked = 0;
                    else this.leftPlayerLocked = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerCounter = 0;
        public int RightPlayerCounter {
            get { return this.rightPlayerCounter; }
            set {
                if (this.rightPlayerCounter != value) {
                    if (value < 0) this.rightPlayerCounter = 0;
                    else if (value < this.RightPlayerLocked) this.rightPlayerCounter = this.RightPlayerLocked;
                    else this.rightPlayerCounter = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerLocked = 0;
        public int RightPlayerLocked {
            get { return this.rightPlayerLocked; }
            set {
                if (this.rightPlayerLocked != value) {
                    if (value < 0) this.rightPlayerLocked = 0;
                    else this.rightPlayerLocked = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int counterToLockPositionX = 0;
        public int CounterToLockPositionX {
            get { return this.counterToLockPositionX; }
            set {
                if (this.counterToLockPositionX != value) {
                    this.counterToLockPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounterToLock();
                }
            }
        }

        private int counterToLockPositionY = 0;
        public int CounterToLockPositionY {
            get { return this.counterToLockPositionY; }
            set {
                if (this.counterToLockPositionY != value) {
                    this.counterToLockPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounterToLock();
                }
            }
        }

        #endregion


        #region Funktionen

        public Business() { }
        public Business(string typeIdentifier) : base(typeIdentifier) { }

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
        }

        public override void ResetData() {
            base.ResetData();
            this.LeftPlayerLocked = 0;
            this.LeftPlayerCounter = 0;
            this.RightPlayerLocked = 0;
            this.RightPlayerCounter = 0;
        }

        internal virtual void FailHot() {
            this.LeftPlayerCounter = this.LeftPlayerLocked;
            this.RightPlayerCounter = this.RightPlayerLocked;
            this.Vinsert_SetCounterToLock();
            this.Vstage_SetScore();
        }

        internal virtual void LockHot() {
            this.LeftPlayerLocked = this.LeftPlayerCounter;
            this.RightPlayerLocked = this.RightPlayerCounter;
            this.Vinsert_SetCounterToLock();
            this.Vstage_SetScore();
        }

        public virtual void Vinsert_CounterToLockIn() { }
        public void Vinsert_CounterToLockIn(
            VentuzScenes.GamePool._Modules.CounterToLock scene) {
            if (scene is VentuzScenes.GamePool._Modules.CounterToLock) {
                this.Vinsert_SetCounterToLock(scene, this.LeftPlayerCounter, this.LeftPlayerLocked, this.RightPlayerCounter, this.RightPlayerLocked);
                scene.ToIn();
            }
        }
        public virtual void Vinsert_SetCounterToLock() { }
        public void Vinsert_SetCounterToLock(VentuzScenes.GamePool._Modules.CounterToLock scene) { this.Vinsert_SetCounterToLock(scene, this.LeftPlayerCounter, this.LeftPlayerLocked, this.RightPlayerCounter, this.RightPlayerLocked); }
        public void Vinsert_SetCounterToLock(
            VentuzScenes.GamePool._Modules.CounterToLock scene,
            int leftPlayerCounter,
            int leftPlayerLocked,
            int rightPlayerCounter,
            int rightPlayerLocked) {
            if (scene is VentuzScenes.GamePool._Modules.CounterToLock) {
                scene.SetPositionX(this.CounterToLockPositionX);
                scene.SetPositionY(this.CounterToLockPositionY);
                scene.SetLeftTopName(this.LeftPlayerName);
                scene.SetLeftTopCounter(leftPlayerCounter);
                scene.SetLeftTopLocked(leftPlayerLocked);
                scene.SetRightBottomName(this.RightPlayerName);
                scene.SetRightBottomCounter(rightPlayerCounter);
                scene.SetRightBottomLocked(rightPlayerLocked);
            }
        }
        public virtual void Vinsert_CounterToLockOut() { }
        public void Vinsert_CounterToLockOut(
            VentuzScenes.GamePool._Modules.CounterToLock scene) {
            if (scene is VentuzScenes.GamePool._Modules.CounterToLock) scene.ToOut();
        }

        public virtual void Vstage_Init() {
            this.Vstage_GamescoreIn();
        }
        public virtual void Vstage_GamescoreIn() {
            this.Vstage_SetScore();
            if (this.hostMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostMasterScene.GameScoreIn();
            if (this.leftPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerMasterScene.GameScoreIn();
            if (this.rightPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerMasterScene.GameScoreIn();
        }
        public virtual void Vstage_SetScore() {
            if (this.hostMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.hostMasterScene.SetLeftPlayerGameScore(this.LeftPlayerLocked);
                this.hostMasterScene.SetRightPlayerGameScore(this.RightPlayerLocked);
            }
            if (this.leftPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.leftPlayerMasterScene.SetLeftPlayerGameScore(this.LeftPlayerLocked);
                this.leftPlayerMasterScene.SetRightPlayerGameScore(this.RightPlayerLocked);
            }
            if (this.rightPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.rightPlayerMasterScene.SetLeftPlayerGameScore(this.LeftPlayerLocked);
                this.rightPlayerMasterScene.SetRightPlayerGameScore(this.RightPlayerLocked);
            }
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
