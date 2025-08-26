using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cliparts.Tools.Base;

using Cliparts.SchlagDenStarLive.MainApp.Settings;
using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.CounterPointerScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.CounterPointerScore {

    public class Business : _Base.Score.Business {

        #region Properties

        private int pointer = 0;
        public int Pointer {
            get { return this.pointer; }
            set {
                if (this.pointer != value) {
                    if (value < 0) value = 0;
                    this.pointer = value;
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

        private int scorePointerPositionX = 0;
        public int ScorePointerPositionX {
            get { return this.scorePointerPositionX; }
            set {
                if (this.scorePointerPositionX != value) {
                    this.scorePointerPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetScorePointer();
                }
            }
        }

        private int scorePointerPositionY = 0;
        public int ScorePointerPositionY {
            get { return this.scorePointerPositionY; }
            set {
                if (this.scorePointerPositionY != value) {
                    this.scorePointerPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetScorePointer();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.ScorePointer.Styles scorePointerStyle = VentuzScenes.GamePool._Modules.ScorePointer.Styles.Points20;
        public VentuzScenes.GamePool._Modules.ScorePointer.Styles ScorePointerStyle {
            get { return this.scorePointerStyle; }
            set {
                if (this.scorePointerStyle != value) {
                    this.scorePointerStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetScorePointer();
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.CounterPointerScore'", typeIdentifier);
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
            this.Pointer = 0;
            this.LeftPlayerCounter = 0;
            this.RightPlayerCounter = 0;
        }

        public override void Next() {
            base.Next();
            this.Pointer = 0;
            this.LeftPlayerCounter = 0;
            this.RightPlayerCounter = 0;
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }

        public override void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score); }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }

        public void Vinsert_ScorePointerIn() {
            if (this.insertScene is Insert) {
                this.Vinsert_SetScorePointer();
                this.insertScene.ScorePointer.ToIn();
            }
        }
        public virtual void Vinsert_SetScorePointer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScorePointer(this.insertScene.ScorePointer, this.Pointer, this.LeftPlayerCounter, this.RightPlayerCounter); }
        public void Vinsert_SetScorePointer(
            VentuzScenes.GamePool._Modules.ScorePointer scene,
            int pointer,
            int leftPlayerScore,
            int rightPlayerScore) {
            if (scene is VentuzScenes.GamePool._Modules.ScorePointer) {
                scene.SetPositionX(this.ScorePointerPositionX);
                scene.SetPositionY(this.ScorePointerPositionY);
                scene.SetFlipPosition(this.FlipPlayers);
                scene.SetStyle(this.ScorePointerStyle);
                scene.SetPointer(pointer);
                scene.SetLeftTopName(this.LeftPlayerName);
                scene.SetLeftTopScore(leftPlayerScore);
                scene.SetRightBottomName(this.RightPlayerName);
                scene.SetRightBottomScore(rightPlayerScore);
            }
        }
        public void Vinsert_ScorePointerOut() { if (this.insertScene is Insert) this.insertScene.ScorePointer.ToOut(); }

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
