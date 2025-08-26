using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SetsScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.SetsScore {

    public class Business : _Base.Sets.Business {

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

        private Insert insertScene;
        public override VRemote4.HandlerSi.Scene.States InsertSceneStatus {
            get {
                if (this.insertScene is Insert) return this.insertScene.Status;
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.SetsScore'", typeIdentifier);
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

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);
        }

        public override void Dispose() {
            base.Dispose();
            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
        }

        public override void ResetData() {
            base.ResetData();
            this.LeftPlayerScore = 0;
            this.RightPlayerScore = 0;
        }

        public override void Next() {
            foreach (_Base.Sets.SingleSet item in this.leftPlayerSets) item.Reset();
            foreach (_Base.Sets.SingleSet item in this.rightPlayerSets) item.Reset();
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }
        public override void Vinsert_SetsIn() {
            this.Vinsert_SetSets();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vinsert_SetsIn(this.insertScene.Sets);
        }
        public override void Vinsert_SetSets() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vinsert_SetSets(this.insertScene.Sets); }
        public override void Vinsert_SetsOut() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vinsert_SetsOut(this.insertScene.Sets); }

        public void Vinsert_ScoreIn() {
            this.Vinsert_SetScore();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Score.ToIn();
        }
        public void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score); }
        public void Vinsert_SetScore(VentuzScenes.GamePool._Modules.Score scene) { this.Vinsert_SetScore(scene, this.LeftPlayerScore, this.RightPlayerScore); }
        public void Vinsert_SetScore(
            VentuzScenes.GamePool._Modules.Score scene,
            int leftPlayerScore,
            int rightPlayerScore) {
            if (scene is VentuzScenes.GamePool._Modules.Score) {
                scene.SetPositionX(this.ScorePositionX);
                scene.SetPositionY(this.ScorePositionY);
                scene.SetFlipPosition(false);
                scene.SetStyle(this.ScoreStyle);
                scene.SetLeftTopName(this.LeftPlayerName);
                scene.SetLeftTopScore(leftPlayerScore);
                scene.SetRightBottomName(this.RightPlayerName);
                scene.SetRightBottomScore(rightPlayerScore);
            }
        }
        public void Vinsert_ScoreOut() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Score.ToOut();
        }
        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void ClearGraphic() {
            base.ClearGraphic();
            this.insertScene.Clear();
        }


        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
