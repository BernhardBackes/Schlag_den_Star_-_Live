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

using Cliparts.Serialization;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimeToBeatSpeedKickRemote;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimeToBeatSpeedKickRemote {

    public class Business : _Base.Business {

        #region Properties

        private MidiHandler.Business midiHandler;

        public PassenClient SpeedKickServerClient;
        private string speedKickServerClientHostname = string.Empty;
        public string SpeedKickServerClientHostname {
            get { return speedKickServerClientHostname; }
            set {
                if (this.speedKickServerClientHostname != value) {
                    if (string.IsNullOrEmpty(value)) this.speedKickServerClientHostname = string.Empty;
                    else this.speedKickServerClientHostname = value;
                    if (this.SpeedKickServerClient is PassenClient &&
                        this.SpeedKickServerClient.Hostname != this.speedKickServerClientHostname) this.SpeedKickServerClient.Hostname = this.speedKickServerClientHostname;
                    this.on_PropertyChanged();
                }
            }
        }

        private int timeToBeatPositionX = 0;
        public int TimeToBeatPositionX {
            get { return this.timeToBeatPositionX; }
            set {
                if (this.timeToBeatPositionX != value) {
                    this.timeToBeatPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeToBeat();
                }
            }
        }

        private int timeToBeatPositionY = 0;
        public int TimeToBeatPositionY {
            get { return this.timeToBeatPositionY; }
            set {
                if (this.timeToBeatPositionY != value) {
                    this.timeToBeatPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeToBeat();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.TimeToBeat.Styles timeToBeatStyle = VentuzScenes.GamePool._Modules.TimeToBeat.Styles.StopwatchName;
        public VentuzScenes.GamePool._Modules.TimeToBeat.Styles TimeToBeatStyle {
            get { return this.timeToBeatStyle; }
            set {
                if (this.timeToBeatStyle != value) {
                    this.timeToBeatStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeToBeat();
                }
            }
        }

        private int timeToBeatStopTime = 5999;
        public int TimeToBeatStopTime {
            get { return this.timeToBeatStopTime; }
            set {
                if (this.timeToBeatStopTime != value) {
                    this.timeToBeatStopTime = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeToBeat();
                }
            }
        }

        private double timeToBeatCurrentTime = -1;
        public double TimeToBeatCurrentTime {
            get { return this.timeToBeatCurrentTime; }
            protected set {
                if (this.timeToBeatCurrentTime != value) {
                    this.timeToBeatCurrentTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private Content.Gameboard.PlayerSelection selectedPlayer = Content.Gameboard.PlayerSelection.LeftPlayer;
        public Content.Gameboard.PlayerSelection SelectedPlayer {
            get { return this.selectedPlayer; }
            set {
                if (this.selectedPlayer != value) {
                    if (value == Content.Gameboard.PlayerSelection.NotSelected) value = Content.Gameboard.PlayerSelection.LeftPlayer;
                    this.selectedPlayer = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private double? timeToBeat = null;
        [NotSerialized]
        public string TimeToBeat {
            get {
                if (this.timeToBeat.HasValue) return Helper.convertDoubleToStopwatchTimeText(this.timeToBeat.Value, false, true).Replace(".", ",");
                else return string.Empty;
            }
            set {
                if (this.TimeToBeat != value) {
                    double result;
                    if (string.IsNullOrEmpty(value) ||
                        !double.TryParse(value, out result)) this.timeToBeat = null;
                    else this.timeToBeat = result;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool timerEnabled = false;
        public bool TimerEnabled {
            get { return this.timerEnabled; }
            set {
                if (this.timerEnabled != value) {
                    this.timerEnabled = value;
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

        private bool timeIsRunning = false;

        #endregion


        #region Funktionen

        public Business() {}
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.TimeToBeatSpeedKickRemote'", typeIdentifier);
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

            this.midiHandler = midiHandler;

            this.SpeedKickServerClient = new PassenClient(syncContext);
            this.SpeedKickServerClient.PropertyChanged += this.speedKickServerClient_PropertyChanged;
            this.SpeedKickServerClient.StartTimer += this.speedKickServerClient_StartTimer;
            this.SpeedKickServerClient.StopTimer += this.speedKickServerClient_StopTimer;
            this.SpeedKickServerClient.HitPanel += this.speedKickServerClient_HitPanel;
            this.SpeedKickServerClient.Finished += this.speedKickServerClient_Finished;
            this.SpeedKickServerClient.Hostname = this.speedKickServerClientHostname;

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;
            this.insertScene.TimeToBeat.StopFired += this.timeToBeat_StopFired;
            this.insertScene.TimeToBeat.PropertyChanged += this.timeToBeat_PropertyChanged;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);
        }

        public override void Dispose() {
            base.Dispose();

            this.SpeedKickServerClient.PropertyChanged -= this.speedKickServerClient_PropertyChanged;
            this.SpeedKickServerClient.StartTimer -= this.speedKickServerClient_StartTimer;
            this.SpeedKickServerClient.StopTimer -= this.speedKickServerClient_StopTimer;
            this.SpeedKickServerClient.HitPanel -= this.speedKickServerClient_HitPanel;
            this.SpeedKickServerClient.Finished -= this.speedKickServerClient_Finished;

            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.TimeToBeat.StopFired -= this.timeToBeat_StopFired;
            this.insertScene.TimeToBeat.PropertyChanged -= this.timeToBeat_PropertyChanged;
        }

        public override void ResetData() {
            base.ResetData();
            this.TimerEnabled = false;
            this.TimeToBeat = null;
            this.timeIsRunning = false;
        }

        internal void StartCourse() {
            this.SpeedKickServerClient.StartCourse();
        }

        public void NextPlayer() {
            if (this.SelectedPlayer == Content.Gameboard.PlayerSelection.LeftPlayer) this.SelectedPlayer = Content.Gameboard.PlayerSelection.RightPlayer;
            else this.SelectedPlayer = Content.Gameboard.PlayerSelection.LeftPlayer;
            this.Vinsert_SetTimeToBeat();
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }

        public void Vinsert_TimeToBeatIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_TimeToBeatIn(this.insertScene.TimeToBeat); }
        public void Vinsert_TimeToBeatIn(
            VentuzScenes.GamePool._Modules.TimeToBeat scene) {
            if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) {
                this.Vinsert_SetTimeToBeat(scene, this.SelectedPlayer);
                scene.ToIn();
            }
            this.Vinsert_ResetTimeToBeat(scene);
            this.Vinsert_ResetOffsetTime(scene);
            this.Vinsert_ResetTimeToBeatTime(scene);
        }
        public void Vinsert_SetTimeToBeat() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetTimeToBeat(this.insertScene.TimeToBeat, this.SelectedPlayer); }
        public void Vinsert_SetTimeToBeat(
            VentuzScenes.GamePool._Modules.TimeToBeat scene,
            Content.Gameboard.PlayerSelection selectedPlayer) {
            if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) {
                scene.SetPositionX(this.TimeToBeatPositionX);
                scene.SetPositionY(this.TimeToBeatPositionY);
                scene.SetSentenceTime(0);
                scene.SetStopTime(this.TimeToBeatStopTime);
                scene.SetStyle(this.TimeToBeatStyle);
                if (selectedPlayer == Content.Gameboard.PlayerSelection.LeftPlayer) scene.SetName(this.LeftPlayerName);
                else scene.SetName(this.RightPlayerName);
            }
        }
        public void Vinsert_StartTimeToBeat() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_StartTimeToBeat(this.insertScene.TimeToBeat); }
        public void Vinsert_StartTimeToBeat(VentuzScenes.GamePool._Modules.TimeToBeat scene) {
            if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) {
                scene.StartTimer();
                this.timeIsRunning = true;
            }
        }
        public void Vinsert_ShowOffsetTime(float offset) { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ShowOffsetTime(this.insertScene.TimeToBeat, offset); }
        public void Vinsert_ShowOffsetTime(
            VentuzScenes.GamePool._Modules.TimeToBeat scene,
            float offset) {
            if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) {
                scene.SetOffset(offset);
                scene.ShowOffset();
            }
        }
        public void Vinsert_ResetOffsetTime() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ResetOffsetTime(this.insertScene.TimeToBeat); }
        public void Vinsert_ResetOffsetTime(VentuzScenes.GamePool._Modules.TimeToBeat scene) { if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) scene.ResetOffset(); }
        public void Vinsert_ShowTimeToBeatTime() { if (this.insertScene is VRemote4.HandlerSi.Scene && this.timeToBeat.HasValue) this.Vinsert_ShowTimeToBeatTime(this.insertScene.TimeToBeat, Convert.ToSingle(this.timeToBeat.Value)); }
        public void Vinsert_ShowTimeToBeatTime(float timeToBeat) { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ShowTimeToBeatTime(this.insertScene.TimeToBeat, Convert.ToSingle(timeToBeat)); }
        public void Vinsert_ShowTimeToBeatTime(
            VentuzScenes.GamePool._Modules.TimeToBeat scene,
            float timeToBeat) {
            if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) {
                scene.SetTimeToBeat(timeToBeat);
                scene.ShowTimeToBeat();
            }
        }

        public void Vinsert_ResetTimeToBeatTime() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ResetTimeToBeatTime(this.insertScene.TimeToBeat); }
        public void Vinsert_ResetTimeToBeatTime(VentuzScenes.GamePool._Modules.TimeToBeat scene) { if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) scene.ResetTimeToBeat(); }
        public void Vinsert_StopTimeToBeat() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_StopTimeToBeat(this.insertScene.TimeToBeat); }
        public void Vinsert_StopTimeToBeat(VentuzScenes.GamePool._Modules.TimeToBeat scene) { if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) { scene.StopTimer(); } }
        public void Vinsert_ContinueTimeToBeat() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ContinueTimeToBeat(this.insertScene.TimeToBeat); }
        public void Vinsert_ContinueTimeToBeat(VentuzScenes.GamePool._Modules.TimeToBeat scene) {
            if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) {
                scene.ContinueTimer();
                this.timeIsRunning = true;
            }
        }
        public void Vinsert_ResetTimeToBeat() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ResetTimeToBeat(this.insertScene.TimeToBeat); }
        public void Vinsert_ResetTimeToBeat(VentuzScenes.GamePool._Modules.TimeToBeat scene) { if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) scene.ResetTimer(); }
        public void Vinsert_TimeToBeatOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_TimeToBeatOut(this.insertScene.TimeToBeat); }
        public void Vinsert_TimeToBeatOut(
            VentuzScenes.GamePool._Modules.TimeToBeat scene) {
            if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) {
                scene.ToOut();
                this.Vinsert_ResetOffsetTime(scene);
                this.Vinsert_ResetTimeToBeatTime(scene);
            }
        }

        public void Vinsert_PlaySoundHitPanel() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.PlaySoundHitPanel(); }

        public void Vinsert_PlaySoundFinished() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.PlaySoundFinished(); }


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

        public event EventHandler TimeToBeatStopFired;
        protected void on_TimeToBeatStopFired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.TimeToBeatStopFired, e); }

        #endregion

        #region Events.Incoming

        protected void timeToBeat_StopFired(object sender, EventArgs e) {
            this.on_TimeToBeatStopFired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timeToBeat_StopFired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timeToBeat_StopFired(object content) {
            if (this.timeIsRunning) {
                if (this.timeToBeat.HasValue) {
                    // zweiter Durchgang, der Offset wird ermittelt
                    double currentTime = this.insertScene.TimeToBeat.CurrentTime;
                    double offset = currentTime - this.timeToBeat.Value;
                    this.Vinsert_ShowOffsetTime(Convert.ToSingle(offset));
                }
                else {
                    // erster Durchgang, die TimeToBeat wird ermittelt
                    if (this.insertScene.TimeToBeat.CurrentTime > 0) {
                        this.timeToBeat = this.insertScene.TimeToBeat.CurrentTime;
                        this.on_PropertyChanged("TimeToBeat");
                    }
                }                
            }
            this.timeIsRunning = false;
        }

        protected void timeToBeat_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timeToBeat_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timeToBeat_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "CurrentTime") this.TimeToBeatCurrentTime = Convert.ToDouble(this.insertScene.TimeToBeat.CurrentTime);
            }
        }

        private void speedKickServerClient_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_speedKickServerClient_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_speedKickServerClient_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
            }
        }

        private void speedKickServerClient_StartTimer(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_speedKickServerClient_StartTimer);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_speedKickServerClient_StartTimer(object content) { if (this.TimerEnabled) this.Vinsert_StartTimeToBeat(); }
        private void speedKickServerClient_StopTimer(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_speedKickServerClient_StopTimer);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_speedKickServerClient_StopTimer(object content) { if (this.TimerEnabled) this.Vinsert_StopTimeToBeat(); }

        private void speedKickServerClient_HitPanel(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_speedKickServerClient_HitPanel);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }    
        protected virtual void sync_speedKickServerClient_HitPanel(object content) { this.Vinsert_PlaySoundHitPanel(); }

        private void speedKickServerClient_Finished(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_speedKickServerClient_Finished);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_speedKickServerClient_Finished(object content) {
            this.Vinsert_StopTimeToBeat();
            this.Vinsert_PlaySoundFinished();
            this.midiHandler.SendEvent("PassenEnde");
        }

        #endregion
    }
}
