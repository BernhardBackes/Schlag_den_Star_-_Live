using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TwoTimersBuzzerStoppedScores;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TwoTimersBuzzerStoppedScores {

    public class Business : _Base.TwoTimersBuzzerStoppedScores.Business {

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

            this.ClassInfo = string.Format("'{0}' of 'Templates.TwoTimersBuzzerStoppedScores'", typeIdentifier);
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
            this.insertScene.TwoTimersScores.PropertyChanged += this.twoTimersScores_PropertyChanged;
            this.insertScene.TwoTimersScores.BottomTimerAlarm1Fired += this.on_BottomTimerAlarm1Fired;
            this.insertScene.TwoTimersScores.BottomTimerAlarm2Fired += this.on_BottomTimerAlarm2Fired;
            this.insertScene.TwoTimersScores.BottomTimerStopFired += this.on_BottomTimerStopFired;
            this.insertScene.TwoTimersScores.TopTimerAlarm1Fired += this.on_TopTimerAlarm1Fired;
            this.insertScene.TwoTimersScores.TopTimerAlarm2Fired += this.on_TopTimerAlarm2Fired;
            this.insertScene.TwoTimersScores.TopTimerStopFired += this.on_TopTimerStopFired;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);
        }

        public override void Dispose() {
            base.Dispose();

            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.TwoTimersScores.PropertyChanged -= this.twoTimersScores_PropertyChanged;
            this.insertScene.TwoTimersScores.BottomTimerAlarm1Fired -= this.on_BottomTimerAlarm1Fired;
            this.insertScene.TwoTimersScores.BottomTimerAlarm2Fired -= this.on_BottomTimerAlarm2Fired;
            this.insertScene.TwoTimersScores.BottomTimerStopFired -= this.on_BottomTimerStopFired;
            this.insertScene.TwoTimersScores.TopTimerAlarm1Fired -= this.on_TopTimerAlarm1Fired;
            this.insertScene.TwoTimersScores.TopTimerAlarm2Fired -= this.on_TopTimerAlarm2Fired;
            this.insertScene.TwoTimersScores.TopTimerStopFired -= this.on_TopTimerStopFired;
            this.insertScene.Dispose();
        }


        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }

        public override void Vinsert_TwoTimersScoresIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_TwoTimersScoresIn(this.insertScene.TwoTimersScores); }

        public override void Vinsert_SetTwoTimersScores() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetTwoTimersScores(this.insertScene.TwoTimersScores); }

        public override void Vinsert_StartTopTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_StartTopTimer(this.insertScene.TwoTimersScores); }
        public override void Vinsert_StopTopTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_StopTopTimer(this.insertScene.TwoTimersScores); }
        public override void Vinsert_ContinueTopTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ContinueTopTimer(this.insertScene.TwoTimersScores); }
        public override void Vinsert_ResetTopTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ResetTopTimer(this.insertScene.TwoTimersScores); }

        public override void Vinsert_StartBottomTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_StartBottomTimer(this.insertScene.TwoTimersScores); }
        public override void Vinsert_StopBottomTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_StopBottomTimer(this.insertScene.TwoTimersScores); }
        public override void Vinsert_ContinueBottomTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ContinueBottomTimer(this.insertScene.TwoTimersScores); }
        public override void Vinsert_ResetBottomTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ResetBottomTimer(this.insertScene.TwoTimersScores); }

        public override void Vinsert_SetScores() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScores(this.insertScene.TwoTimersScores); }
        
        public override void Vinsert_TwoTimersScoresOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_TwoTimersScoresOut(this.insertScene.TwoTimersScores); }

        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void Vstage_Init() {
            base.Vstage_Init();
            if (this.hostMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostMasterScene.LiveVideoIn();
            if (this.leftPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerMasterScene.LiveVideoIn();
            if (this.rightPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerMasterScene.LiveVideoIn();
        }

        public override void ClearGraphic() {
            base.ClearGraphic();
            this.insertScene.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming

        void twoTimersScores_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_twoTimersScores_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_twoTimersScores_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "TopTimerCurrentTime") this.LeftTimerCurrentTime = this.insertScene.TwoTimersScores.TopTimerCurrentTime;
                else if (e.PropertyName == "TopTimerIsRunning") this.LeftTimerIsRunning = this.insertScene.TwoTimersScores.TopTimerIsRunning;
                else if (e.PropertyName == "BottomTimerCurrentTime") this.RightTimerCurrentTime = this.insertScene.TwoTimersScores.BottomTimerCurrentTime;
                else if (e.PropertyName == "BottomTimerIsRunning") this.RightTimerIsRunning = this.insertScene.TwoTimersScores.BottomTimerIsRunning;
            }
        }

        #endregion

    }
}
