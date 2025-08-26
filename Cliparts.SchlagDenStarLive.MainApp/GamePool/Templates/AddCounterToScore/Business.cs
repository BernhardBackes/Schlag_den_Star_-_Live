using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.AddCounterToScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.AddCounterToScore {

    public class Business : _Base.CounterInOutScore.Business {

        #region Properties

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

            this.ClassInfo = string.Format("'{0}' of 'Templates.AddCounterToScore'", typeIdentifier);
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

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;
        }

        public override void Dispose() {
            base.Dispose();
            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
        }

        public void LeftPlayerCounterTake() {
            this.Vinsert_LeftCounterOut();
            this.LeftPlayerScore += this.LeftPlayerCounter;
            this.LeftPlayerCounter = 0;
            this.Vinsert_SetCounterInOutScore();
        }
        public void LeftPlayerCounterLoose() {
            this.Vinsert_LeftCounterOut();
            this.LeftPlayerCounter = 0;
            this.Vinsert_SetCounterInOutScore();
        }

        public void RightPlayerCounterTake() {
            this.Vinsert_RightCounterOut();
            this.RightPlayerScore += this.RightPlayerCounter;
            this.RightPlayerCounter = 0;
            this.Vinsert_SetCounterInOutScore();
        }
        public void RightPlayerCounterLoose() {
            this.Vinsert_RightCounterOut();
            this.RightPlayerCounter = 0;
            this.Vinsert_SetCounterInOutScore();
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }
        public override void Vinsert_CounterInOutScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_CounterInOutScoreIn(this.insertScene.CounterInOutScore); }
        public override void Vinsert_SetCounterInOutScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetCounterInOutScore(this.insertScene.CounterInOutScore); }
        public override void Vinsert_LeftCounterIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_LeftCounterIn(this.insertScene.CounterInOutScore); }
        public override void Vinsert_LeftCounterOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_LeftCounterOut(this.insertScene.CounterInOutScore); }
        public override void Vinsert_RightCounterIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_RightCounterIn(this.insertScene.CounterInOutScore); }
        public override void Vinsert_RightCounterOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_RightCounterOut(this.insertScene.CounterInOutScore); }
        public override void Vinsert_CounterInOutScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_CounterInOutScoreOut(this.insertScene.CounterInOutScore); }
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
