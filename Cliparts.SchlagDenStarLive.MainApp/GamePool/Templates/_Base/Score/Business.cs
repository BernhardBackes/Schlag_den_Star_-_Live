using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.Score {

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

        private int scorePositionX = 0;
        public int ScorePositionX {
            get { return this.scorePositionX; }
            set {
                if (this.scorePositionX != value) {
                    this.scorePositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetScore();
                }
            }
        }

        private int scorePositionY = 0;
        public int ScorePositionY {
            get { return this.scorePositionY; }
            set {
                if (this.scorePositionY != value) {
                    this.scorePositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetScore();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.Score.Styles scoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots;
        public VentuzScenes.GamePool._Modules.Score.Styles ScoreStyle {
            get { return this.scoreStyle; }
            set {
                if (this.scoreStyle != value) {
                    this.scoreStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetScore();
                }
            }
        }

        private bool flipPlayers;
        public bool FlipPlayers {
            get { return this.flipPlayers; }
            set {
                if (this.flipPlayers != value) {
                    this.flipPlayers = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetScore();
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
            this.FlipPlayers = false;
            this.LeftPlayerScore = 0;
            this.RightPlayerScore = 0;
        }

        public virtual void Vinsert_ScoreIn() { }
        public void Vinsert_ScoreIn(
            VentuzScenes.GamePool._Modules.Score scene) {
            if (scene is VentuzScenes.GamePool._Modules.Score) {
                this.Vinsert_SetScore(scene, this.LeftPlayerScore, this.RightPlayerScore);
                scene.ToIn();
            }
        }
        public virtual void Vinsert_SetScore() { }
        public void Vinsert_SetScore(VentuzScenes.GamePool._Modules.Score scene) { this.Vinsert_SetScore(scene, this.LeftPlayerScore, this.RightPlayerScore); }
        public void Vinsert_SetScore(
            VentuzScenes.GamePool._Modules.Score scene,
            int leftPlayerScore,
            int rightPlayerScore) {
            if (scene is VentuzScenes.GamePool._Modules.Score) {
                scene.SetPositionX(this.ScorePositionX);
                scene.SetPositionY(this.ScorePositionY);
                scene.SetFlipPosition(this.FlipPlayers);
                scene.SetStyle(this.ScoreStyle);
                scene.SetLeftTopName(this.LeftPlayerName);
                scene.SetLeftTopScore(leftPlayerScore);
                scene.SetRightBottomName(this.RightPlayerName);
                scene.SetRightBottomScore(rightPlayerScore);
            }
        }
        public virtual void Vinsert_ScoreOut() { }
        public void Vinsert_ScoreOut(
            VentuzScenes.GamePool._Modules.Score scene) {
            if (scene is VentuzScenes.GamePool._Modules.Score) scene.ToOut();
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
