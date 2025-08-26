using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cliparts.Serialization;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.ScoreFaults {

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

        private int leftPlayerFaults = 0;
        [NotSerialized]
        public int LeftPlayerFaults {
            get { return this.leftPlayerFaults; }
            set {
                if (this.leftPlayerFaults != value) {
                    this.leftPlayerFaults = value;
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

        private int rightPlayerFaults = 0;
        [NotSerialized]
        public int RightPlayerFaults {
            get { return this.rightPlayerFaults; }
            set {
                if (this.rightPlayerFaults != value) {
                    this.rightPlayerFaults = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int scoreFaultsPositionX = 0;
        public int ScoreFaultsPositionX {
            get { return this.scoreFaultsPositionX; }
            set {
                if (this.scoreFaultsPositionX != value) {
                    this.scoreFaultsPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetScore();
                }
            }
        }

        private int scoreFaultsPositionY = 0;
        public int ScoreFaultsPositionY {
            get { return this.scoreFaultsPositionY; }
            set {
                if (this.scoreFaultsPositionY != value) {
                    this.scoreFaultsPositionY = value;
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
            this.LeftPlayerScore = 0;
            this.LeftPlayerFaults = 0;
            this.RightPlayerScore = 0;
            this.RightPlayerFaults = 0;
            this.Vinsert_LeftFaultsOut();
            this.Vinsert_RightFaultsOut();
        }

        public virtual void Vinsert_ScoreIn() { }
        public void Vinsert_ScoreIn(
            VentuzScenes.GamePool._Modules.ScoreFaults scene) {
            if (scene is VentuzScenes.GamePool._Modules.ScoreFaults) {
                this.Vinsert_SetScore(scene, this.LeftPlayerScore, this.RightPlayerScore);
                scene.ToIn();
            }
        }
        public virtual void Vinsert_SetScore() { }
        public void Vinsert_SetScore(VentuzScenes.GamePool._Modules.ScoreFaults scene) { this.Vinsert_SetScore(scene, this.LeftPlayerScore, this.RightPlayerScore); }
        public void Vinsert_SetScore(
            VentuzScenes.GamePool._Modules.ScoreFaults scene,
            int leftPlayerScore,
            int rightPlayerScore) {
            if (scene is VentuzScenes.GamePool._Modules.ScoreFaults) {
                scene.SetPositionX(this.ScoreFaultsPositionX);
                scene.SetPositionY(this.ScoreFaultsPositionY);
                scene.SetLeftName(this.LeftPlayerName);
                scene.SetLeftScore(leftPlayerScore);
                scene.SetRightName(this.RightPlayerName);
                scene.SetRightScore(rightPlayerScore);
            }
        }
        public virtual void Vinsert_ScoreOut() { }
        public void Vinsert_ScoreOut(
            VentuzScenes.GamePool._Modules.ScoreFaults scene) {
            if (scene is VentuzScenes.GamePool._Modules.ScoreFaults) scene.ToOut();
        }
        public virtual void Vinsert_LeftFaultsIn() {
            this.Vinsert_SetFaults();
        }
        public void Vinsert_LeftFaultsIn(VentuzScenes.GamePool._Modules.ScoreFaults scene) { if (scene is VentuzScenes.GamePool._Modules.ScoreFaults) scene.LeftFaultsToIn(); }
        public virtual void Vinsert_RightFaultsIn() {
            this.Vinsert_SetFaults();
        }
        public void Vinsert_RightFaultsIn(VentuzScenes.GamePool._Modules.ScoreFaults scene) { if (scene is VentuzScenes.GamePool._Modules.ScoreFaults) scene.RightFaultsToIn(); }
        public virtual void Vinsert_SetFaults() { }
        public void Vinsert_SetFaults(VentuzScenes.GamePool._Modules.ScoreFaults scene) { this.Vinsert_SetFaults(scene, this.LeftPlayerFaults, this.RightPlayerFaults); }
        public void Vinsert_SetFaults(
            VentuzScenes.GamePool._Modules.ScoreFaults scene,
            int leftPlayerFaults,
            int rightPlayerFaults) {
            if (scene is VentuzScenes.GamePool._Modules.ScoreFaults) {
                scene.SetLeftFaults(leftPlayerFaults);
                scene.SetRightFaults(rightPlayerFaults);
            }
        }
        public virtual void Vinsert_LeftFaultsOut() { }
        public void Vinsert_LeftFaultsOut(VentuzScenes.GamePool._Modules.ScoreFaults scene) { if (scene is VentuzScenes.GamePool._Modules.ScoreFaults) scene.LeftFaultsToOut(); }
        public virtual void Vinsert_RightFaultsOut() { }
        public void Vinsert_RightFaultsOut(VentuzScenes.GamePool._Modules.ScoreFaults scene) { if (scene is VentuzScenes.GamePool._Modules.ScoreFaults) scene.RightFaultsToOut(); }

        public override void Vfullscreen_SetScore() {
            if (this.fullscreenMasterScene is VRemote4.HandlerSi.Scene) {
                this.fullscreenMasterScene.Score.SetLeftName(this.LeftPlayerName);
                this.fullscreenMasterScene.Score.SetLeftScore(this.LeftPlayerScore);
                this.fullscreenMasterScene.Score.SetRightName(this.RightPlayerName);
                this.fullscreenMasterScene.Score.SetRightScore(this.RightPlayerScore);
            }
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
