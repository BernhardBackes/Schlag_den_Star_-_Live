using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimerScoreDartCounter;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimerScoreDartCounter {

    public class Business : _Base.TimerScore.Business {

        #region Properties

        private int counterPositionX = 0;
        public int CounterPositionX {
            get { return this.counterPositionX; }
            set {
                if (this.counterPositionX != value) {
                    this.counterPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounter();
                }
            }
        }

        private int counterPositionY = 0;
        public int CounterPositionY {
            get { return this.counterPositionY; }
            set {
                if (this.counterPositionY != value) {
                    this.counterPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounter();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.CounterToBeat.SizeElements size = VentuzScenes.GamePool._Modules.CounterToBeat.SizeElements.ThreeDigits;
        public VentuzScenes.GamePool._Modules.CounterToBeat.SizeElements CounterSize {
            get { return this.size; }
            set {
                if (this.size != value) {
                    this.size = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounter();
                }
            }
        }

        private int counterValue = 0;
        public int CounterValue {
            get { return this.counterValue; }
            set {
                if (this.counterValue != value) {
                    this.counterValue = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int toBeatValue = 0;
        public int ToBeatValue {
            get { return this.toBeatValue; }
            set {
                if (this.toBeatValue != value) {
                    this.toBeatValue = value;
                    this.on_PropertyChanged();
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.TimerScoreDartCounter'", typeIdentifier);
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
            this.insertScene.Timer.Alarm1Fired += this.timer_Alarm1Fired;
            this.insertScene.Timer.Alarm2Fired += this.timer_Alarm2Fired;
            this.insertScene.Timer.StopFired += this.timer_StopFired;
            this.insertScene.Timer.PropertyChanged += this.timer_PropertyChanged;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);
        }

        public override void Dispose() {
            base.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Timer.Alarm1Fired -= this.timer_Alarm1Fired;
            this.insertScene.Timer.Alarm2Fired -= this.timer_Alarm2Fired;
            this.insertScene.Timer.StopFired -= this.timer_StopFired;
            this.insertScene.Timer.PropertyChanged -= this.timer_PropertyChanged;
            this.insertScene.Dispose();
        }

        public override void ResetData() {
            base.ResetData();
            this.CounterValue = 0;
            this.ToBeatValue = 0;
        }

        public override void Next() {
            base.Next();
            this.CounterValue = 0;
            this.ToBeatValue = 0;
        }

        public void AddCounterHot(
            int value) {
            this.CounterValue += value;
            this.Vinsert_SetCounter();
        }
        internal void NextCounter() {
            this.ToBeatValue = this.CounterValue;
            this.CounterValue = 0;
            this.Vinsert_SetCounter();
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }
        public override void Vinsert_TimerIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_TimerIn(this.insertScene.Timer); }
        public override void Vinsert_SetTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetTimer(this.insertScene.Timer); }
        public override void Vinsert_StartTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Timer.StartTimer(); }
        public override void Vinsert_StopTimer() { 
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Timer.StopTimer();
            this.Vfullscreen_StopTimer();
            this.Vfullscreen_SetTimer(this.TimerCurrentTime);
        }
        public override void Vinsert_ContinueTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Timer.ContinueTimer(); }
        public override void Vinsert_ResetTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Timer.ResetTimer(); }
        public override void Vinsert_TimerOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Timer.ToOut(); }
        public override void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score); }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }

        internal void Vinsert_CounterIn() {
            this.Vinsert_SetCounter();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.CounterToBeat.ToIn();
        }
        public void Vinsert_SetCounter() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetCounter(this.insertScene.CounterToBeat); }
        public void Vinsert_SetCounter(VentuzScenes.GamePool._Modules.CounterToBeat scene) { this.Vinsert_SetCounter(scene, this.CounterValue, this.ToBeatValue); }

        public void Vinsert_SetCounter(
            VentuzScenes.GamePool._Modules.CounterToBeat scene,
            int counterValue,
            int toBeatValue) {
            if (scene is VentuzScenes.GamePool._Modules.CounterToBeat
                && scene.Status == VRemote4.HandlerSi.Scene.States.Available) {
                scene.SetPositionX(this.CounterPositionX);
                scene.SetPositionY(this.CounterPositionY);
                scene.SetSize(this.CounterSize);
                scene.SetCounter(counterValue);
                if (toBeatValue > 0) {
                    scene.SetToBeatValue(toBeatValue);
                    scene.ToBeatToIn();
                }
                else scene.SetToBeatOut();
            }
        }
        internal void Vinsert_CounterOut() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.CounterToBeat.ToOut();
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

        protected override void sync_timer_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "CurrentTime") this.TimerCurrentTime = this.insertScene.Timer.CurrentTime;
                else if (e.PropertyName == "IsRunning") this.TimerIsRunning = this.insertScene.Timer.IsRunning;
            }
        }

        #endregion

    }
}
