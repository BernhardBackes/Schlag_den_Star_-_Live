using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.CounterInOutScore {

    public class Business : _Base.Business {

        #region Properties

        private int leftPlayerScore = 0;
        public int LeftPlayerScore {
            get { return this.leftPlayerScore; }
            set {
                if (this.leftPlayerScore != value) {
                    if (value < 0) value = 0;
                    this.leftPlayerScore = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int leftPlayerCounter = 0;
        public int LeftPlayerCounter {
            get { return this.leftPlayerCounter; }
            set {
                if (this.leftPlayerCounter != value) {
                    this.leftPlayerCounter = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerScore = 0;
        public int RightPlayerScore {
            get { return this.rightPlayerScore; }
            set {
                if (this.rightPlayerScore != value) {
                    if (value < 0) value = 0;
                    this.rightPlayerScore = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerCounter = 0;
        public int RightPlayerCounter {
            get { return this.rightPlayerCounter; }
            set {
                if (this.rightPlayerCounter != value) {
                    this.rightPlayerCounter = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int counterInOutScorePositionX = 0;
        public int CounterInOutScorePositionX {
            get { return this.counterInOutScorePositionX; }
            set {
                if (this.counterInOutScorePositionX != value) {
                    this.counterInOutScorePositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounterInOutScore();
                }
            }
        }

        private int counterInOutScorePositionY = 0;
        public int CounterInOutScorePositionY {
            get { return this.counterInOutScorePositionY; }
            set {
                if (this.counterInOutScorePositionY != value) {
                    this.counterInOutScorePositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounterInOutScore();
                }
            }
        }

        private bool counterInOutFlipPlayers;
        public bool CounterInOutFlipPlayers {
            get { return this.counterInOutFlipPlayers; }
            set {
                if (this.counterInOutFlipPlayers != value) {
                    this.counterInOutFlipPlayers = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounterInOutScore();
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
            this.LeftPlayerScore = 0;
            this.LeftPlayerCounter = 0;
            this.RightPlayerScore = 0;
            this.RightPlayerCounter = 0;
        }

        public virtual void Vinsert_CounterInOutScoreIn() { }
        public void Vinsert_CounterInOutScoreIn(
            VentuzScenes.GamePool._Modules.CounterInOutScore scene) {
            if (scene is VentuzScenes.GamePool._Modules.CounterInOutScore) {
                this.Vinsert_SetCounterInOutScore(scene);
                scene.ToIn();
            }
        }
        public virtual void Vinsert_SetCounterInOutScore() { }
        public void Vinsert_SetCounterInOutScore(VentuzScenes.GamePool._Modules.CounterInOutScore scene) { 
            this.Vinsert_SetCounterInOutScore(scene, this.LeftPlayerScore, this.LeftPlayerCounter, this.RightPlayerScore, this.RightPlayerCounter); 
        }
        public void Vinsert_SetCounterInOutScore(
            VentuzScenes.GamePool._Modules.CounterInOutScore scene,
            int leftPlayerScore,
            int leftPlayerCounter,
            int rightPlayerScore,
            int rightPlayerCounter) {
            if (scene is VentuzScenes.GamePool._Modules.CounterInOutScore) {
                scene.SetPositionX(this.CounterInOutScorePositionX);
                scene.SetPositionY(this.CounterInOutScorePositionY);
                scene.SetFlipPosition(this.CounterInOutFlipPlayers);
                scene.SetTopName(this.LeftPlayerName);
                scene.SetTopScore(leftPlayerScore);
                scene.SetTopCounter(leftPlayerCounter);
                scene.SetBottomName(this.RightPlayerName);
                scene.SetBottomScore(rightPlayerScore);
                scene.SetBottomCounter(rightPlayerCounter);
            }
        }
        public virtual void Vinsert_LeftCounterIn() { }
        public void Vinsert_LeftCounterIn(VentuzScenes.GamePool._Modules.CounterInOutScore scene) {
            this.Vinsert_SetCounterInOutScore(scene);
            if (scene is VentuzScenes.GamePool._Modules.CounterInOutScore) scene.TopCounterToIn();
        }
        public virtual void Vinsert_LeftCounterOut() { }
        public void Vinsert_LeftCounterOut(VentuzScenes.GamePool._Modules.CounterInOutScore scene) {
            if (scene is VentuzScenes.GamePool._Modules.CounterInOutScore) scene.TopCounterToOut();
        }
        public virtual void Vinsert_RightCounterIn() { }
        public void Vinsert_RightCounterIn(VentuzScenes.GamePool._Modules.CounterInOutScore scene) {
            this.Vinsert_SetCounterInOutScore(scene);
            if (scene is VentuzScenes.GamePool._Modules.CounterInOutScore) scene.BottomCounterToIn();
        }
        public virtual void Vinsert_RightCounterOut() { }
        public void Vinsert_RightCounterOut(VentuzScenes.GamePool._Modules.CounterInOutScore scene) {
            if (scene is VentuzScenes.GamePool._Modules.CounterInOutScore) scene.BottomCounterToOut();
        }
        public virtual void Vinsert_CounterInOutScoreOut() { }
        public void Vinsert_CounterInOutScoreOut(VentuzScenes.GamePool._Modules.CounterInOutScore scene) {
            if (scene is VentuzScenes.GamePool._Modules.CounterInOutScore) scene.ToOut();
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
                this.hostMasterScene.SetLeftPlayerGameScore(this.LeftPlayerScore);
                this.hostMasterScene.SetRightPlayerGameScore(this.RightPlayerScore);
            }
            if (this.leftPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.leftPlayerMasterScene.SetLeftPlayerGameScore(this.LeftPlayerScore);
                this.leftPlayerMasterScene.SetRightPlayerGameScore(this.RightPlayerScore);
            }
            if (this.rightPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.rightPlayerMasterScene.SetLeftPlayerGameScore(this.LeftPlayerScore);
                this.rightPlayerMasterScene.SetRightPlayerGameScore(this.RightPlayerScore);
            }
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
