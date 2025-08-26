using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.AudienceVoting;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.AudienceVoting {

    public class Business : _Base.Timer.Business {

        #region Properties

        private int leftPlayerValue = 0;
        public int LeftPlayerValue {
            get { return this.leftPlayerValue; }
            set {
                if (this.leftPlayerValue != value) {
                    if (value < 0) this.leftPlayerValue = 0;
                    else if (value > 100) this.leftPlayerValue = 100;
                    else this.leftPlayerValue = value;
                    this.on_PropertyChanged();
                    this.on_PropertyChanged("RightPlayerScore");
                }
            }
        }

        public int RightPlayerValue {
            get { return 100 - this.LeftPlayerValue; }
        }

        private int contentPositionX = 0;
        public int ContentPositionX {
            get { return this.contentPositionX; }
            set {
                if (this.contentPositionX != value) {
                    this.contentPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetGame();
                }
            }
        }

        private int contentPositionY = 0;
        public int ContentPositionY {
            get { return this.contentPositionY; }
            set {
                if (this.contentPositionY != value) {
                    this.contentPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetGame();
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.AudienceVoting'", typeIdentifier);
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

            this.Vinsert_Timer.Pose(syncContext, this.insertScene.Timer);
            this.Vinsert_Timer.PropertyChanged += this.on_PropertyChanged;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);
        }

        public override void Dispose() {
            base.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Dispose();
            this.Vinsert_Timer.PropertyChanged -= this.on_PropertyChanged;
            this.Vinsert_Timer.Dispose();
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }

        public void Vinsert_SetGame() {
            this.Vinsert_SetGame(this.insertScene, this.LeftPlayerValue);
        }
        public void Vinsert_SetGame(
            Insert scene,
            int leftPlayerValue) {
            if (scene is Insert) {
                scene.Game.SetPositionX(this.ContentPositionX);
                scene.Game.SetPositionY(this.ContentPositionY);
                scene.Game.SetLeftPlayerName(this.LeftPlayerName);
                scene.Game.SetLeftPlayerValue(leftPlayerValue);
                scene.Game.SetRightPlayerName(this.RightPlayerName);
            }
        }
        public void Vinsert_GameIn() {
            this.Vinsert_SetGame();
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Game.ToIn();
        }
        internal void Vinsert_StartBars() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Game.StartBars(); }
        internal void Vinsert_ShowNames() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Game.ShowNames(); }
        public void Vinsert_GameOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Game.ToOut(); }

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
