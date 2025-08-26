using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.ShootingScore {

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

        private int leftPlayerHeats = 0;
        public int LeftPlayerHeats {
            get { return this.leftPlayerHeats; }
            set {
                if (this.leftPlayerHeats != value) {
                    if (value < 0) value = 0;
                    this.leftPlayerHeats = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerHeats = 0;
        public int RightPlayerHeats {
            get { return this.rightPlayerHeats; }
            set {
                if (this.rightPlayerHeats != value) {
                    if (value < 0) value = 0;
                    this.rightPlayerHeats = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int leftPlayerHits = 0;
        public int LeftPlayerHits {
            get { return this.leftPlayerHits; }
            set {
                if (this.leftPlayerHits != value) {
                    if (value < 0) value = 0;
                    this.leftPlayerHits = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerHits = 0;
        public int RightPlayerHits {
            get { return this.rightPlayerHits; }
            set {
                if (this.rightPlayerHits != value) {
                    if (value < 0) value = 0;
                    this.rightPlayerHits = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int shootingPositionX = 0;
        public int ShootingPositionX {
            get { return this.shootingPositionX; }
            set {
                if (this.shootingPositionX != value) {
                    this.shootingPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetShooting();
                }
            }
        }

        private int shootingPositionY = 0;
        public int ShootingPositionY {
            get { return this.shootingPositionY; }
            set {
                if (this.shootingPositionY != value) {
                    this.shootingPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetShooting();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.Shooting.Styles shootingStyle = VentuzScenes.GamePool._Modules.Shooting.Styles.FourHeats;
        public VentuzScenes.GamePool._Modules.Shooting.Styles ShootingStyle {
            get { return this.shootingStyle; }
            set {
                if (this.shootingStyle != value) {
                    this.shootingStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetShooting();
                }
            }
        }

        private int hitsCount = 3;
        public int HitsCount {
            get { return this.hitsCount; }
            set { 
                if (this.hitsCount != value) {
                    if (value < 2) this.hitsCount = 2;
                    else if (value > 3) this.hitsCount = 3;
                    else this.hitsCount = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetShooting();
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
            this.LeftPlayerHeats = 0;
            this.RightPlayerHeats = 0;
            this.LeftPlayerHits = 0;
            this.RightPlayerHits = 0;
        }

        internal void LeftPlayerNextHeat() {
            this.Vinsert_ShootingLeftPlayerHitsOut();
            this.LeftPlayerHits = 0;
        }

        internal void RightPlayerNextHeat() {
            this.Vinsert_ShootingRightPlayerHitsOut();
            this.RightPlayerHits = 0;
        }

        internal void Resolve() {
            if (this.LeftPlayerHeats > this.RightPlayerHeats) this.LeftPlayerScore++;
            else if (this.LeftPlayerHeats < this.RightPlayerHeats) this.RightPlayerScore++;

        }

        public override void Next() {
            base.Next();
            this.LeftPlayerHeats = 0;
            this.RightPlayerHeats = 0;
            this.LeftPlayerHits = 0;
            this.RightPlayerHits = 0;
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

        public virtual void Vinsert_ShootingIn() { }
        public void Vinsert_ShootingIn(
            VentuzScenes.GamePool._Modules.Shooting scene) {
            if (scene is VentuzScenes.GamePool._Modules.Shooting) {
                this.Vinsert_SetShooting(scene, this.LeftPlayerHeats, this.LeftPlayerHits, this.RightPlayerHeats, this.RightPlayerHits);
                scene.ToIn();
            }
        }
        public virtual void Vinsert_SetShooting() { }
        public void Vinsert_SetShooting(VentuzScenes.GamePool._Modules.Shooting scene) { this.Vinsert_SetShooting(scene, this.LeftPlayerHeats, this.LeftPlayerHits, this.RightPlayerHeats, this.RightPlayerHits); }
        public void Vinsert_SetShooting(
            VentuzScenes.GamePool._Modules.Shooting scene,
            int leftPlayerHeats,
            int leftPlayerHits,
            int rightPlayerHeats,
            int rightPlayerHits) {
            if (scene is VentuzScenes.GamePool._Modules.Shooting) {
                scene.SetPositionX(this.ShootingPositionX);
                scene.SetPositionY(this.ShootingPositionY);
                scene.SetStyle(this.ShootingStyle);
                scene.SetHitsCountMax(this.HitsCount);
                scene.SetLeftTopName(this.LeftPlayerName);
                scene.SetLeftTopHeats(leftPlayerHeats);
                scene.SetLeftTopHits(leftPlayerHits);
                scene.SetRightBottomName(this.RightPlayerName);
                scene.SetRightBottomHeats(rightPlayerHeats);
                scene.SetRightBottomHits(rightPlayerHits);
            }
        }
        public virtual void Vinsert_ShootingLeftPlayerHitsIn() { }
        public void Vinsert_ShootingLeftPlayerHitsIn(
            VentuzScenes.GamePool._Modules.Shooting scene) {
            if (scene is VentuzScenes.GamePool._Modules.Shooting) {
                this.Vinsert_SetShooting(scene, this.LeftPlayerHeats, this.LeftPlayerHits, this.RightPlayerHeats, this.RightPlayerHits);
                scene.LeftTopHitsToIn();
            }
        }
        public virtual void Vinsert_ShootingLeftPlayerHitsOut() { }
        public void Vinsert_ShootingLeftPlayerHitsOut(
            VentuzScenes.GamePool._Modules.Shooting scene) {
            if (scene is VentuzScenes.GamePool._Modules.Shooting) scene.LeftTopHitsToOut();
        }
        public virtual void Vinsert_ShootingLeftPlayerHitMiss() { }
        public void Vinsert_ShootingLeftPlayerHitMiss(
            VentuzScenes.GamePool._Modules.Shooting scene) {
            if (scene is VentuzScenes.GamePool._Modules.Shooting) scene.LeftTopHitsMiss();
        }
        public virtual void Vinsert_ShootingRightPlayerHitsIn() { }
        public void Vinsert_ShootingRightPlayerHitsIn(
            VentuzScenes.GamePool._Modules.Shooting scene) {
            if (scene is VentuzScenes.GamePool._Modules.Shooting) {
                this.Vinsert_SetShooting(scene, this.LeftPlayerHeats, this.LeftPlayerHits, this.RightPlayerHeats, this.RightPlayerHits);
                scene.RightBottomHitsToIn();
            }
        }
        public virtual void Vinsert_ShootingRightPlayerHitsOut() { }
        public void Vinsert_ShootingRightPlayerHitsOut(
            VentuzScenes.GamePool._Modules.Shooting scene) {
            if (scene is VentuzScenes.GamePool._Modules.Shooting) scene.RightBottomHitsToOut();
        }
        public virtual void Vinsert_ShootingRightPlayerHitMiss() { }
        public void Vinsert_ShootingRightPlayerHitMiss(
            VentuzScenes.GamePool._Modules.Shooting scene) {
            if (scene is VentuzScenes.GamePool._Modules.Shooting) scene.RightBottomHitsMiss();
        }
        public virtual void Vinsert_ShootingOut() { }
        public void Vinsert_ShootingOut(
            VentuzScenes.GamePool._Modules.Shooting scene) {
            if (scene is VentuzScenes.GamePool._Modules.Shooting) scene.ToOut();
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
