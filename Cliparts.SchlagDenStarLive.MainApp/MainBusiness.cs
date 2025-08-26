using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cliparts.Devantech;
using Cliparts.Tools.Base;

using Cliparts.SchlagDenStarLive.MainApp.Settings;

//#region Properties
//#endregion

//#region Funktionen
//#endregion

//#region Events.Outgoing
//#endregion

//#region Events.Incoming
//#endregion

namespace Cliparts.SchlagDenStarLive.MainApp {

    public class MainBusiness : INotifyPropertyChanged {

        #region Properties

        private SynchronizationContext syncContext;

        public string Car {
            get { return this.settingsHandler.Car; }
            set {
                if (this.settingsHandler.Car != value) {
                    if (value == null) value = string.Empty;
                    this.settingsHandler.Car = value;
                    this.on_PropertyChanged();
                }
            }
        }

        public string Gainer {
            get { return this.settingsHandler.Gainer; }
            set {
                if (this.settingsHandler.Gainer != value) {
                    if (value == null) value = string.Empty;
                    this.settingsHandler.Gainer = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private Messaging.Business messageHandler;
        public Messaging.ErrorStatus MessagingStatus { get { return this.messageHandler.Status; } }
        public string MessagingLatestMessage { get { return this.messageHandler.LatestMessage; } }

        private Settings.Business settingsHandler;

        private Controller devantechHandler;

        private Content.Business contentHandler;

        private VRemote4.HandlerSi.Business ventuzHandler;

        private VRemote4.HandlerSi.Client.Business ventuzInsertClient;
        private VentuzScenes.Insert.Business ventuzInsertScene;
        public VRemote4.HandlerSi.Scene.States VentuzInsertSceneStatus {
            get {
                if (this.ventuzInsertScene is VentuzScenes.Insert.Business) return this.ventuzInsertScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private VRemote4.HandlerSi.Client.Business ventuzFullscreenClient;
        private VentuzScenes.Fullscreen.Business ventuzFullscreenScene;
        public VRemote4.HandlerSi.Scene.States VentuzFullscreenSceneStatus {
            get {
                if (this.ventuzFullscreenScene is VentuzScenes.Fullscreen.Business) return this.ventuzFullscreenScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private VRemote4.HandlerSi.Client.Business ventuzHostClient;
        private VentuzScenes.Host.Business ventuzHostScene;
        public VRemote4.HandlerSi.Scene.States VentuzHostSceneStatus {
            get {
                if (this.ventuzHostScene is VentuzScenes.Host.Business) return this.ventuzHostScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private VRemote4.HandlerSi.Client.Business ventuzLeftPlayerClient;
        private VentuzScenes.Player.Business ventuzLeftPlayerScene;
        public VRemote4.HandlerSi.Scene.States VentuzLeftPlayerSceneStatus {
            get {
                if (this.ventuzLeftPlayerScene is VentuzScenes.Player.Business) return this.ventuzLeftPlayerScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private VRemote4.HandlerSi.Client.Business ventuzRightPlayerClient;
        private VentuzScenes.Player.Business ventuzRightPlayerScene;
        public VRemote4.HandlerSi.Scene.States VentuzRightPlayerSceneStatus {
            get {
                if (this.ventuzRightPlayerScene is VentuzScenes.Player.Business) return this.ventuzRightPlayerScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private MidiHandler.Business midiHandler;

        private BuzzerIO.Business buzzerHandler;

        private AMB.Business ambHandler;

        private System.Timers.Timer buzzerConnectDelay;

        #endregion


        #region Funktionen

        public MainBusiness(
            SynchronizationContext syncContext,
            out Settings.Business settingsHandler,
            out Controller devantechHandler,
            out Content.Business contentHandler,
            out VRemote4.HandlerSi.Business ventuzHandler,
            out VentuzScenes.Insert.Business ventuzInsertScene,
            out VentuzScenes.Fullscreen.Business ventuzFullscreenScene,
            out VentuzScenes.Host.Business ventuzHostScene,
            out VentuzScenes.Player.Business ventuzLeftPlayerScene,
            out VentuzScenes.Player.Business ventuzRightPlayerScene) {

            this.syncContext = syncContext;

            this.messageHandler = new Messaging.Business();
            this.messageHandler.StatusChanged += this.messageHandler_StatusChanged;

            this.settingsHandler = new Settings.Business();
            this.settingsHandler.Error += this.error;
            settingsHandler = this.settingsHandler;
            this.settingsHandler.Load();

            this.devantechHandler = new Controller();
            this.devantechHandler.PropertyChanged += this.devantechHandler_PropertyChanged;
            this.devantechHandler.ExeptionRaised += this.devantechHandler_ExeptionRaised;
            devantechHandler = this.devantechHandler;
            this.devantechHandler.Load(this.settingsHandler.DevantechFilename);

            this.ventuzHandler = new VRemote4.HandlerSi.Business(syncContext);
            this.ventuzHandler.Error += this.ventuzHandler_Error;
            this.ventuzHandler.PreviewPipeChanged += this.ventuzHandler_PreviewPipeChanged;
            ventuzHandler = this.ventuzHandler;

            if (this.ventuzHandler.TryAddClient("Insert", false, out this.ventuzInsertClient)) {
                this.ventuzInsertClient.HostnameChanged += this.insertClient_ServerChanged;
                this.ventuzInsertClient.StatusChanged += this.insertClient_StatusChanged;
                this.ventuzInsertScene = new VentuzScenes.Insert.Business(syncContext, this.settingsHandler, this.ventuzInsertClient, 0);
                this.ventuzInsertScene.StatusChanged += this.ventuzInsertScene_StatusChanged;
            }
            ventuzInsertScene = this.ventuzInsertScene;

            if (this.ventuzHandler.TryAddClient("Fullscreen", false, out this.ventuzFullscreenClient)) {
                this.ventuzFullscreenClient.HostnameChanged += this.fullscreenClient_ServerChanged;
                this.ventuzFullscreenClient.StatusChanged += this.fullscreenClient_StatusChanged;
                this.ventuzFullscreenScene = new VentuzScenes.Fullscreen.Business(syncContext, this.ventuzFullscreenClient, 0);
                this.ventuzFullscreenScene.StatusChanged += this.ventuzFullscreenScene_StatusChanged;
            }
            ventuzFullscreenScene = this.ventuzFullscreenScene;

            if (this.ventuzHandler.TryAddClient("Host", false, out this.ventuzHostClient)) {
                this.ventuzHostClient.HostnameChanged += this.hostClient_ServerChanged;
                this.ventuzHostClient.StatusChanged += this.hostClient_StatusChanged;
                this.ventuzHostScene = new VentuzScenes.Host.Business(syncContext, this.ventuzHostClient, 0);
                this.ventuzHostScene.StatusChanged += this.ventuzHostScene_StatusChanged;
            }
            ventuzHostScene = this.ventuzHostScene;

            if (this.ventuzHandler.TryAddClient("Left Player", false, out this.ventuzLeftPlayerClient)) {
                this.ventuzLeftPlayerClient.HostnameChanged += this.leftPlayerClient_ServerChanged;
                this.ventuzLeftPlayerClient.StatusChanged += this.leftPlayerClient_StatusChanged;
                this.ventuzLeftPlayerScene = new VentuzScenes.Player.Business(syncContext, this.ventuzLeftPlayerClient, 0);
                this.ventuzLeftPlayerScene.StatusChanged += this.ventuzLeftPlayerScene_StatusChanged;
            }
            ventuzLeftPlayerScene = this.ventuzLeftPlayerScene;

            if (this.ventuzHandler.TryAddClient("Right Player", false, out this.ventuzRightPlayerClient)) {
                this.ventuzRightPlayerClient.HostnameChanged += this.rightPlayerClient_ServerChanged;
                this.ventuzRightPlayerClient.StatusChanged += this.rightPlayerClient_StatusChanged;
                this.ventuzRightPlayerScene = new VentuzScenes.Player.Business(syncContext, this.ventuzRightPlayerClient, 0);
                this.ventuzRightPlayerScene.StatusChanged += this.ventuzRightPlayerScene_StatusChanged;
            }
            ventuzRightPlayerScene = this.ventuzRightPlayerScene;

            this.midiHandler = new MidiHandler.Business(
                syncContext,
                this.settingsHandler.MidiFilename);
            this.midiHandler.Error += this.error;

            this.buzzerHandler = new BuzzerIO.Business(
                syncContext,
                this.settingsHandler);
            this.buzzerHandler.Error += this.error;

            this.ambHandler = new AMB.Business(
                syncContext,
                this.settingsHandler);
            this.ambHandler.Error += this.error;

            this.buzzerConnectDelay = new System.Timers.Timer(5000);
            this.buzzerConnectDelay.AutoReset = false;
            this.buzzerConnectDelay.Elapsed += this.buzzerConnectDelay_Elapsed;

            this.contentHandler = new Content.Business(
                syncContext,
                this.midiHandler,
                this.buzzerHandler,
                this.ambHandler,
                this.devantechHandler,
                this.settingsHandler,
                this.ventuzInsertScene,
                this.ventuzFullscreenScene,
                this.ventuzHostScene,
                this.ventuzLeftPlayerScene,
                this.ventuzRightPlayerScene,
                this.ventuzHandler.PreviewPipe);
            this.contentHandler.ClearGraphicFired += this.contentHandler_ClearGraphicFired;
            this.contentHandler.ClearStageFired += this.contentHandler_ClearStageFired;
            this.contentHandler.Error += this.error;
            contentHandler = this.contentHandler;

            if (this.ventuzInsertClient is VRemote4.HandlerSi.Client.Business) this.ventuzInsertClient.Start(this.settingsHandler.VentuzInsertServer);
            if (this.ventuzFullscreenClient is VRemote4.HandlerSi.Client.Business) this.ventuzFullscreenClient.Start(this.settingsHandler.VentuzFullscreenServer);
            if (this.ventuzHostClient is VRemote4.HandlerSi.Client.Business) this.ventuzHostClient.Start(this.settingsHandler.VentuzHostServer);
            if (this.ventuzLeftPlayerClient is VRemote4.HandlerSi.Client.Business) this.ventuzLeftPlayerClient.Start(this.settingsHandler.VentuzLeftPlayerServer);
            if (this.ventuzRightPlayerClient is VRemote4.HandlerSi.Client.Business) this.ventuzRightPlayerClient.Start(this.settingsHandler.VentuzRightPlayerServer);

            this.contentHandler.Load(this.settingsHandler.ContentFilename);

            this.buzzerHandler.LoadUnitList();

            this.buzzerConnectDelay.Start();

        }

        public void Dispose() {

            this.ambHandler.Dispose();
            this.ambHandler.Error -= this.error;

            this.buzzerHandler.Dispose();
            this.buzzerHandler.Error -= this.error;

            this.midiHandler.Dispose();
            this.midiHandler.Error -= this.error;

            this.devantechHandler.PropertyChanged -= this.devantechHandler_PropertyChanged;
            this.devantechHandler.ExeptionRaised -= this.devantechHandler_ExeptionRaised;
            this.devantechHandler.Dispose();

            this.contentHandler.Dispose();
            this.contentHandler.ClearGraphicFired -= this.contentHandler_ClearGraphicFired;
            this.contentHandler.ClearStageFired -= this.contentHandler_ClearStageFired;
            this.contentHandler.Error -= this.error;

            this.ventuzHandler.Dispose();
            this.ventuzHandler.Error -= this.ventuzHandler_Error;

            if (this.ventuzInsertClient is VRemote4.HandlerSi.Client.Business) {
                this.ventuzInsertClient.Dispose();
                this.ventuzInsertClient.HostnameChanged -= this.insertClient_ServerChanged;
                this.ventuzInsertClient.StatusChanged -= this.insertClient_StatusChanged;
            }
            this.ventuzInsertScene.Dispose();
            this.ventuzInsertScene.StatusChanged -= this.ventuzInsertScene_StatusChanged;

            if (this.ventuzFullscreenClient is VRemote4.HandlerSi.Client.Business) {
                this.ventuzFullscreenClient.Dispose();
                this.ventuzFullscreenClient.HostnameChanged -= this.fullscreenClient_ServerChanged;
                this.ventuzFullscreenClient.StatusChanged -= this.fullscreenClient_StatusChanged;
            }
            this.ventuzFullscreenScene.Dispose();
            this.ventuzFullscreenScene.StatusChanged -= this.ventuzFullscreenScene_StatusChanged;

            if (this.ventuzHostClient is VRemote4.HandlerSi.Client.Business) {
                this.ventuzHostClient.Dispose();
                this.ventuzHostClient.HostnameChanged -= this.hostClient_ServerChanged;
                this.ventuzHostClient.StatusChanged -= this.hostClient_StatusChanged;
            }
            this.ventuzHostScene.Dispose();
            this.ventuzHostScene.StatusChanged -= this.ventuzHostScene_StatusChanged;

            if (this.ventuzLeftPlayerClient is VRemote4.HandlerSi.Client.Business) {
                this.ventuzLeftPlayerClient.Dispose();
                this.ventuzLeftPlayerClient.HostnameChanged -= this.leftPlayerClient_ServerChanged;
                this.ventuzLeftPlayerClient.StatusChanged -= this.leftPlayerClient_StatusChanged;
            }
            this.ventuzLeftPlayerScene.Dispose();
            this.ventuzLeftPlayerScene.StatusChanged -= this.ventuzLeftPlayerScene_StatusChanged;

            if (this.ventuzRightPlayerClient is VRemote4.HandlerSi.Client.Business) {
                this.ventuzRightPlayerClient.Dispose();
                this.ventuzRightPlayerClient.HostnameChanged -= this.rightPlayerClient_ServerChanged;
                this.ventuzRightPlayerClient.StatusChanged -= this.rightPlayerClient_StatusChanged;
            }
            this.ventuzRightPlayerScene.Dispose();
            this.ventuzRightPlayerScene.StatusChanged -= this.ventuzRightPlayerScene_StatusChanged;

            this.settingsHandler.MidiFilename = this.midiHandler.Filename;
            this.settingsHandler.Error -= this.error;
            this.settingsHandler.Save();
        }

        public void ResetAll() {
            this.contentHandler.ResetAll();
        }

        public void SyncAll() {
            this.SyncVentuzInsertScene();
            this.SyncVentuzFullscreenScene();
            this.SyncVentuzHostScene();
            this.SyncVentuzLeftPlayerScene();
            this.SyncVentuzRightPlayerScene();
        }

        public void ClearGraphic() {
            this.ventuzInsertScene.Clear();
            this.ventuzFullscreenScene.Clear();
            this.contentHandler.GameList.ClearAllGamesGraphic();
        }

        public void ClearStage() {
            this.ventuzHostScene.Clear();
            this.ventuzLeftPlayerScene.Clear();
            this.ventuzRightPlayerScene.Clear();
            this.contentHandler.GameList.ClearAllGamesStage();
        }

        public void FadeStageOut() {
            this.ventuzHostScene.FadeOut();
            this.ventuzLeftPlayerScene.FadeOut();
            this.ventuzRightPlayerScene.FadeOut();
        }
        public void FadeStageIn() {
            this.ventuzHostScene.FadeIn();
            this.ventuzLeftPlayerScene.FadeIn();
            this.ventuzRightPlayerScene.FadeIn();
        }

        public void LiveVideoIn() {
            this.ventuzHostScene.LiveVideoIn();
            this.ventuzLeftPlayerScene.LiveVideoIn();
            this.ventuzRightPlayerScene.LiveVideoIn();
        }
        public void LiveVideoOut() {
            this.ventuzHostScene.LiveVideoOut();
            this.ventuzLeftPlayerScene.LiveVideoOut();
            this.ventuzRightPlayerScene.LiveVideoOut();
        }

        public void ShowMessagingForm() { this.messageHandler.ShowForm(); }

        public void ShowIOnet() { this.buzzerHandler.ShowForm(); }

        public void ShowAMBForm() { this.ambHandler.ShowForm(); }

        public void ShowMidiForm() { this.midiHandler.ShowForm(); }
        public void UpdateMidiEventList() { this.midiHandler.UpdateEventList(); }
        public void ToggleMidiOutStatus() { this.midiHandler.ToggleOutStatus(); }

        public void LoadVentuzInsertScene() { this.ventuzInsertScene.Load(); }
        public void SyncVentuzInsertScene() {
            if (this.ventuzInsertScene.Status == VRemote4.HandlerSi.Scene.States.Available) {
            }
        }
        public void StartGainerInsert() {
            this.ventuzInsertScene.Gewinner.SetAuto(this.Car);
            this.ventuzInsertScene.Gewinner.SetName(this.Gainer);
            this.ventuzInsertScene.Gewinner.Start();
        }
        public void ResetGainerInsert() { this.ventuzInsertScene.Gewinner.Reset(); }

        public void LoadVentuzFullscreenScene() { this.ventuzFullscreenScene.Load(); }
        public void SyncVentuzFullscreenScene() {
            this.contentHandler.Gameboard.SetGraphic();
        }

        public void LoadVentuzHostScene() { this.ventuzHostScene.Load(); }
        public void SyncVentuzHostScene() {
            if (this.ventuzHostScene.Status == VRemote4.HandlerSi.Scene.States.Available) {
                this.ventuzHostScene.SetFlipColor(Constants.FlipPlayerColor);
                this.ventuzHostScene.SetLeftPlayerTotalScore(this.contentHandler.Gameboard.LeftPlayerTotalScore);
                this.ventuzHostScene.SetLeftPlayerName(this.contentHandler.Gameboard.LeftPlayerName);
                this.ventuzHostScene.SetRightPlayerTotalScore(this.contentHandler.Gameboard.RightPlayerTotalScore);
                this.ventuzHostScene.SetRightPlayerName(this.contentHandler.Gameboard.RightPlayerName);
            }
        }

        public void LoadVentuzLeftPlayerScene() { this.ventuzLeftPlayerScene.Load(); }
        public void SyncVentuzLeftPlayerScene() {
            if (this.ventuzLeftPlayerScene.Status == VRemote4.HandlerSi.Scene.States.Available) {
                this.ventuzLeftPlayerScene.SetFlipColor(Constants.FlipPlayerColor);
                this.ventuzLeftPlayerScene.SetSelectedPlayer(VentuzScenes.Player.SelectedPlayer.Left);
                this.ventuzLeftPlayerScene.SetLeftPlayerTotalScore(this.contentHandler.Gameboard.LeftPlayerTotalScore);
                this.ventuzLeftPlayerScene.SetLeftPlayerName(this.contentHandler.Gameboard.LeftPlayerName);
                this.ventuzLeftPlayerScene.SetRightPlayerTotalScore(this.contentHandler.Gameboard.RightPlayerTotalScore);
                this.ventuzLeftPlayerScene.SetRightPlayerName(this.contentHandler.Gameboard.RightPlayerName);
            }
        }

        public void LoadVentuzRightPlayerScene() { this.ventuzRightPlayerScene.Load(); }
        public void SyncVentuzRightPlayerScene() {
            if (this.ventuzRightPlayerScene.Status == VRemote4.HandlerSi.Scene.States.Available) {
                this.ventuzRightPlayerScene.SetFlipColor(Constants.FlipPlayerColor);
                this.ventuzRightPlayerScene.SetSelectedPlayer(VentuzScenes.Player.SelectedPlayer.Right);
                this.ventuzRightPlayerScene.SetLeftPlayerTotalScore(this.contentHandler.Gameboard.LeftPlayerTotalScore);
                this.ventuzRightPlayerScene.SetLeftPlayerName(this.contentHandler.Gameboard.LeftPlayerName);
                this.ventuzRightPlayerScene.SetRightPlayerTotalScore(this.contentHandler.Gameboard.RightPlayerTotalScore);
                this.ventuzRightPlayerScene.SetRightPlayerName(this.contentHandler.Gameboard.RightPlayerName);
            }
        }

        //public void ToggleVentuzMuteStatus() {
        //    VRemote4.HandlerSi.Client.Pipe.Business pipe;
        //    if (this.ventuzInsertClient.TryGetPipe(0, out pipe)) {
        //        if (pipe.AudioOutputStatus == VRemote4.HandlerSi.Client.Pipe.AudioOutputStates.Muted)
        //            pipe.ActivateAudioOutput();
        //        else if (pipe.AudioOutputStatus == VRemote4.HandlerSi.Client.Pipe.AudioOutputStates.Active)
        //            pipe.MuteAudioOutput();
        //    }
        //}

        #endregion


        #region Events.Outgoing

        public event PropertyChangedEventHandler PropertyChanged;
        protected void on_PropertyChanged([CallerMemberName]string callerName = "") { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }
        protected void on_PropertyChanged(PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, e); }
        protected void on_PropertyChanged(object sender, PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(sender, this.PropertyChanged, e); }

        public event EventHandler<Cliparts.Messaging.MessageStatusChangedEventArgs> MessagingStatusChanged;
        private void on_MessagingStatusChanged(object sender, Cliparts.Messaging.MessageStatusChangedEventArgs e) { if (this.MessagingStatusChanged != null) this.MessagingStatusChanged(sender, e); }

        public event EventHandler<MidiHandler.StatusChangedArgs> MidiOutStatusChanged;
        private void on_MidiOutStatusChanged(object sender, MidiHandler.StatusChangedArgs e) { if (this.MidiOutStatusChanged != null) this.MidiOutStatusChanged(sender, e); }

        public event EventHandler<FilenameArgs> ContentFilenameChanged;
        private void on_ContentFilenameChanged(object sender, FilenameArgs e) { if (this.ContentFilenameChanged != null) this.ContentFilenameChanged(sender, e); }

        #endregion

        #region Events.Incoming

        private void messageHandler_StatusChanged(object sender, Messaging.MessageStatusChangedEventArgs e) {
            this.on_MessagingStatusChanged(sender, e);
        }

        private void error(object sender, Messaging.ErrorEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_error);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_error(object content) {
            Messaging.ErrorEventArgs error = content as Messaging.ErrorEventArgs;
            if (error is Messaging.ErrorEventArgs) this.messageHandler.AddErrorMessage(error);
        }

        private void resetGraphic(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_resetGraphic);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_resetGraphic(object content) {
            this.ClearGraphic();
        }


        void ventuzHandler_Error(object sender, System.IO.ErrorEventArgs e) {
            Exception exc = e.GetException();
            this.error(this, new Messaging.ErrorEventArgs(this, sender.ToString(), DateTime.Now, exc.ToString()));
        }
        void ventuzHandler_PreviewPipeChanged(object sender, VRemote4.HandlerSi.PipeArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzHandler_PreviewPipeChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzHandler_PreviewPipeChanged(object content) {
            VRemote4.HandlerSi.PipeArgs e = content as VRemote4.HandlerSi.PipeArgs;
            if (e is VRemote4.HandlerSi.PipeArgs) this.contentHandler.GameList.SetPreviewPipe(e.Pipe);
        }

        void insertClient_ServerChanged(object sender, VRemote4.HandlerSi.Client.HostnameArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_insertClient_ServerChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_insertClient_ServerChanged(object content) {
            VRemote4.HandlerSi.Client.HostnameArgs e = content as VRemote4.HandlerSi.Client.HostnameArgs;
            if (e is VRemote4.HandlerSi.Client.HostnameArgs) this.settingsHandler.VentuzInsertServer = e.Name;
        }

        void insertClient_StatusChanged(object sender, VRemote4.HandlerSi.Client.ClientStateArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_insertClient_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_insertClient_StatusChanged(object content) {
            VRemote4.HandlerSi.Client.ClientStateArgs e = content as VRemote4.HandlerSi.Client.ClientStateArgs;
            if (e is VRemote4.HandlerSi.Client.ClientStateArgs &&
                e.Status.HasValue &&
                e.Status.Value == VRemote4.HandlerSi.Client.ClientStates.Active) this.LoadVentuzInsertScene();
        }

        void ventuzInsertScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzInsertScene_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzInsertScene_StatusChanged(object content) {
            this.on_PropertyChanged("VentuzInsertSceneStatus");
        }

        void fullscreenClient_ServerChanged(object sender, VRemote4.HandlerSi.Client.HostnameArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_fullscreenClient_ServerChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_fullscreenClient_ServerChanged(object content) {
            VRemote4.HandlerSi.Client.HostnameArgs e = content as VRemote4.HandlerSi.Client.HostnameArgs;
            if (e is VRemote4.HandlerSi.Client.HostnameArgs) this.settingsHandler.VentuzFullscreenServer = e.Name;
        }

        void fullscreenClient_StatusChanged(object sender, VRemote4.HandlerSi.Client.ClientStateArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_fullscreenClient_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_fullscreenClient_StatusChanged(object content) {
            VRemote4.HandlerSi.Client.ClientStateArgs e = content as VRemote4.HandlerSi.Client.ClientStateArgs;
            if (e is VRemote4.HandlerSi.Client.ClientStateArgs &&
                e.Status.HasValue &&
                e.Status.Value == VRemote4.HandlerSi.Client.ClientStates.Active) this.LoadVentuzFullscreenScene();
        }

        void ventuzFullscreenScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzFullscreenScene_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzFullscreenScene_StatusChanged(object content) {
            this.on_PropertyChanged("VentuzFullscreenSceneStatus");
            VRemote4.HandlerSi.Scene.StatusArgs e = content as VRemote4.HandlerSi.Scene.StatusArgs;
            if (e is VRemote4.HandlerSi.Scene.StatusArgs &&
                e.Status == VRemote4.HandlerSi.Scene.States.Available) this.SyncVentuzFullscreenScene();
        }

        void hostClient_ServerChanged(object sender, VRemote4.HandlerSi.Client.HostnameArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_hostClient_ServerChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_hostClient_ServerChanged(object content) {
            VRemote4.HandlerSi.Client.HostnameArgs e = content as VRemote4.HandlerSi.Client.HostnameArgs;
            if (e is VRemote4.HandlerSi.Client.HostnameArgs) this.settingsHandler.VentuzHostServer = e.Name;
        }

        void hostClient_StatusChanged(object sender, VRemote4.HandlerSi.Client.ClientStateArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_hostClient_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_hostClient_StatusChanged(object content) {
            VRemote4.HandlerSi.Client.ClientStateArgs e = content as VRemote4.HandlerSi.Client.ClientStateArgs;
            if (e is VRemote4.HandlerSi.Client.ClientStateArgs &&
                e.Status.HasValue &&
                e.Status.Value == VRemote4.HandlerSi.Client.ClientStates.Active) this.LoadVentuzHostScene();
        }

        void ventuzHostScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzHostScene_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzHostScene_StatusChanged(object content) {
            this.on_PropertyChanged("VentuzHostSceneStatus");
            VRemote4.HandlerSi.Scene.StatusArgs e = content as VRemote4.HandlerSi.Scene.StatusArgs;
            if (e is VRemote4.HandlerSi.Scene.StatusArgs &&
                e.Status == VRemote4.HandlerSi.Scene.States.Available) this.SyncVentuzHostScene();
        }

        void leftPlayerClient_ServerChanged(object sender, VRemote4.HandlerSi.Client.HostnameArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftPlayerClient_ServerChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftPlayerClient_ServerChanged(object content) {
            VRemote4.HandlerSi.Client.HostnameArgs e = content as VRemote4.HandlerSi.Client.HostnameArgs;
            if (e is VRemote4.HandlerSi.Client.HostnameArgs) this.settingsHandler.VentuzLeftPlayerServer = e.Name;
        }

        void leftPlayerClient_StatusChanged(object sender, VRemote4.HandlerSi.Client.ClientStateArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftPlayerClient_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftPlayerClient_StatusChanged(object content) {
            VRemote4.HandlerSi.Client.ClientStateArgs e = content as VRemote4.HandlerSi.Client.ClientStateArgs;
            if (e is VRemote4.HandlerSi.Client.ClientStateArgs &&
                e.Status.HasValue &&
                e.Status.Value == VRemote4.HandlerSi.Client.ClientStates.Active) this.LoadVentuzLeftPlayerScene();
        }

        void ventuzLeftPlayerScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzLeftPlayerScene_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzLeftPlayerScene_StatusChanged(object content) {
            this.on_PropertyChanged("VentuzLeftPlayerSceneStatus");
            VRemote4.HandlerSi.Scene.StatusArgs e = content as VRemote4.HandlerSi.Scene.StatusArgs;
            if (e is VRemote4.HandlerSi.Scene.StatusArgs &&
                e.Status == VRemote4.HandlerSi.Scene.States.Available) this.SyncVentuzLeftPlayerScene();
        }

        void rightPlayerClient_ServerChanged(object sender, VRemote4.HandlerSi.Client.HostnameArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerClient_ServerChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerClient_ServerChanged(object content) {
            VRemote4.HandlerSi.Client.HostnameArgs e = content as VRemote4.HandlerSi.Client.HostnameArgs;
            if (e is VRemote4.HandlerSi.Client.HostnameArgs) this.settingsHandler.VentuzRightPlayerServer = e.Name;
        }

        void rightPlayerClient_StatusChanged(object sender, VRemote4.HandlerSi.Client.ClientStateArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerClient_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerClient_StatusChanged(object content) {
            VRemote4.HandlerSi.Client.ClientStateArgs e = content as VRemote4.HandlerSi.Client.ClientStateArgs;
            if (e is VRemote4.HandlerSi.Client.ClientStateArgs &&
                e.Status.HasValue &&
                e.Status.Value == VRemote4.HandlerSi.Client.ClientStates.Active) this.LoadVentuzRightPlayerScene();
        }

        void ventuzRightPlayerScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzRightPlayerScene_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzRightPlayerScene_StatusChanged(object content) {
            this.on_PropertyChanged("VentuzRightPlayerSceneStatus");
            VRemote4.HandlerSi.Scene.StatusArgs e = content as VRemote4.HandlerSi.Scene.StatusArgs;
            if (e is VRemote4.HandlerSi.Scene.StatusArgs &&
                e.Status == VRemote4.HandlerSi.Scene.States.Available) this.SyncVentuzRightPlayerScene();
        }

        void contentHandler_ClearGraphicFired(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_contentHandler_ClearGraphicFired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_contentHandler_ClearGraphicFired(object content) { this.ClearGraphic(); }

        void contentHandler_ClearStageFired(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_contentHandler_ClearStageFired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_contentHandler_ClearStageFired(object content) { this.ClearStage(); }

        void buzzerConnectDelay_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerConnectDelay_Elapsed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerConnectDelay_Elapsed(object content) { this.buzzerHandler.ConnectAllUnits(); }

        private void devantechHandler_ExeptionRaised(object sender, Exception e) {
            this.error(sender, new Messaging.ErrorEventArgs(sender, "", DateTime.Now, e.Message));            
        }

        private void devantechHandler_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_devantechHandler_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_devantechHandler_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "Filename") this.settingsHandler.DevantechFilename = this.devantechHandler.Filename;
            }
        }

        #endregion

    }
}
