using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.DoubleStopClockScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.DoubleStopClockScore {

    public class Business : _Base.Score.Business {

        #region Properties

        private VRemote4.HandlerSi.Business localVentuzHandler;

        private VRemote4.HandlerSi.Client.Business doubleStopClockClient;
        private DoubleStopClock ventuzDoubleStopClockScene;
        public VRemote4.HandlerSi.Scene.States VentuzDoubleStopClockSceneStatus {
            get {
                if (this.ventuzDoubleStopClockScene is DoubleStopClock) return this.ventuzDoubleStopClockScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }
        private string doubleStopClockClientHostname = string.Empty;
        public string DoubleStopClockClientHostname {
            get { return this.doubleStopClockClientHostname; }
            set {
                if (this.doubleStopClockClientHostname != value) {
                    if (value == null) value = string.Empty;
                    this.doubleStopClockClientHostname = value;
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.DoubleStopClockScore'", typeIdentifier);
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

            this.localVentuzHandler = new VRemote4.HandlerSi.Business(syncContext);
            this.localVentuzHandler.Error += this.localVentuzHandler_Error;

            if (this.localVentuzHandler.TryAddClient("DoubleStopClock", false, out this.doubleStopClockClient)) {
                this.doubleStopClockClient.HostnameChanged += this.doubleStopClockClient_HostnameChanged;
                this.doubleStopClockClient.StatusChanged += this.doubleStopClockClient_StatusChanged;
                this.ventuzDoubleStopClockScene = new DoubleStopClock(syncContext, this.doubleStopClockClient, 0);
                this.ventuzDoubleStopClockScene.StatusChanged += this.ventuzDoubleStopClockScene_StatusChanged;
            }

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this, this.localVentuzHandler, this.ventuzDoubleStopClockScene);
        }

        public override void Dispose() {
            base.Dispose();
            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            
            this.localVentuzHandler.Error -= this.localVentuzHandler_Error;

            this.doubleStopClockClient.HostnameChanged -= this.doubleStopClockClient_HostnameChanged;
            this.doubleStopClockClient.StatusChanged -= this.doubleStopClockClient_StatusChanged;
            this.ventuzDoubleStopClockScene.StatusChanged -= this.ventuzDoubleStopClockScene_StatusChanged;
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }
        public override void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score); }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }
        public void Vinsert_PlayJingleStart() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.PlayJingleStart(); }
        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public void VdoubleStopClock_Start() { this.doubleStopClockClient.Start(this.DoubleStopClockClientHostname); }
        public void VdoubleStopClocks_Init() { if (this.VentuzDoubleStopClockSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.VdoubleStopClock_Reset(); }
        internal void VdoubleStopClock_Reset() { this.ventuzDoubleStopClockScene.Reset(); }
        internal void VdoubleStopClock_Stop() { this.ventuzDoubleStopClockScene.Stop(); }
        internal void VdoubleStopClock_ResetLeftClock() { this.ventuzDoubleStopClockScene.ResetLeftClock(); }
        internal void VdoubleStopClock_EnableLeftClock() { this.ventuzDoubleStopClockScene.EnableLeftClock(); }
        internal void VdoubleStopClock_StopLeftClock() { this.ventuzDoubleStopClockScene.StopLeftClock(); }
        internal void VdoubleStopClock_ResetRightClock() { this.ventuzDoubleStopClockScene.ResetRightClock(); }
        internal void VdoubleStopClock_EnableRightClock() { this.ventuzDoubleStopClockScene.EnableRightClock(); }
        internal void VdoubleStopClock_StopRightClock_() { this.ventuzDoubleStopClockScene.StopRightClock(); }
        internal void VdoubleStopClock_ShutDown() { this.doubleStopClockClient.Shutdown(); }

        public override void ClearGraphic() {
            base.ClearGraphic();
            this.insertScene.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming

        void localVentuzHandler_Error(object sender, System.IO.ErrorEventArgs e) {
            Exception exc = e.GetException();
            this.on_Error(sender, new Messaging.ErrorEventArgs(this, sender.ToString(), DateTime.Now, exc.ToString()));
        }

        void doubleStopClockClient_HostnameChanged(object sender, VRemote4.HandlerSi.Client.HostnameArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_doubleStopClockClient_HostnameChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }

        private void sync_doubleStopClockClient_HostnameChanged(object content) {
            VRemote4.HandlerSi.Client.HostnameArgs e = content as VRemote4.HandlerSi.Client.HostnameArgs;
            if (e is VRemote4.HandlerSi.Client.HostnameArgs) this.DoubleStopClockClientHostname = e.Name;
        }

        void doubleStopClockClient_StatusChanged(object sender, VRemote4.HandlerSi.Client.ClientStateArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_doubleStopClockClient_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_doubleStopClockClient_StatusChanged(object content) {
            VRemote4.HandlerSi.Client.ClientStateArgs e = content as VRemote4.HandlerSi.Client.ClientStateArgs;
            if (e is VRemote4.HandlerSi.Client.ClientStateArgs &&
                e.Status.HasValue &&
                e.Status.Value == VRemote4.HandlerSi.Client.ClientStates.Active &&
                this.ventuzDoubleStopClockScene is VRemote4.HandlerSi.Scene) this.ventuzDoubleStopClockScene.Load();
        }

        void ventuzDoubleStopClockScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzDoubleStopClockScene_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzDoubleStopClockScene_StatusChanged(object content) {
            VRemote4.HandlerSi.Scene.StatusArgs e = content as VRemote4.HandlerSi.Scene.StatusArgs;
            if (e is VRemote4.HandlerSi.Scene.StatusArgs) {
                if (e.Status == VRemote4.HandlerSi.Scene.States.Unloaded) this.doubleStopClockClient.Shutdown();
                else if (e.Status == VRemote4.HandlerSi.Scene.States.Available) this.VdoubleStopClocks_Init();
            }
            this.on_PropertyChanged("VentuzDoubleStopClockSceneStatus");
        }

        #endregion

    }
}
