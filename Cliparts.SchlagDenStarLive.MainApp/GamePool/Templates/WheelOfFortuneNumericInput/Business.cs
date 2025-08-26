using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cliparts.Serialization;

using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;
using Cliparts.SchlagDenStarLive.NumericSelectApp;
using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.WheelOfFortuneNumericInput;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.WheelOfFortuneNumericInput {

    public class Business : _Base.Score.Business {

        #region Properties

        private int wheelValue = 0;
        public int WheelValue {
            get { return this.wheelValue; }
            set {
                if (this.wheelValue != value) {
                    if (value < Fullscreen.ValueMinimum) this.wheelValue = Fullscreen.ValueMinimum;
                    else if (value > Fullscreen.ValueMaximum) this.wheelValue = Fullscreen.ValueMaximum;
                    else this.wheelValue = value;
                    this.on_PropertyChanged();
                }
            }
        }

        public PlayerClient LeftPlayerClient;
        private string leftPlayerClientHostname = string.Empty;
        public string LeftPlayerClientHostname {
            get { return leftPlayerClientHostname; }
            set {
                if (this.leftPlayerClientHostname != value) {
                    if (string.IsNullOrEmpty(value)) this.leftPlayerClientHostname = string.Empty;
                    else this.leftPlayerClientHostname = value;
                    if (this.LeftPlayerClient is PlayerClient &&
                        this.LeftPlayerClient.Hostname != this.leftPlayerClientHostname) this.LeftPlayerClient.Hostname = this.leftPlayerClientHostname;
                    this.on_PropertyChanged();
                }
            }
        }

        private int leftPlayerInputValue = 0;
        public int LeftPlayerInputValue {
            get { return this.leftPlayerInputValue; }
            set {
                if (this.leftPlayerInputValue != value) {
                    if (value < FormDisplay.ValueMinimum) this.leftPlayerInputValue = FormDisplay.ValueMinimum;
                    else if (value > FormDisplay.ValueMaximum) this.leftPlayerInputValue = FormDisplay.ValueMaximum;
                    else this.leftPlayerInputValue = value;
                    this.on_PropertyChanged();
                }
            }
        }

        public PlayerClient RightPlayerClient;
        private string rightPlayerClientHostname = string.Empty;
        public string RightPlayerClientHostname {
            get { return rightPlayerClientHostname; }
            set {
                if (this.rightPlayerClientHostname != value) {
                    if (string.IsNullOrEmpty(value)) this.rightPlayerClientHostname = string.Empty;
                    else this.rightPlayerClientHostname = value;
                    if (this.RightPlayerClient is PlayerClient &&
                        this.RightPlayerClient.Hostname != this.rightPlayerClientHostname) this.RightPlayerClient.Hostname = this.rightPlayerClientHostname;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerInputValue = 0;
        public int RightPlayerInputValue {
            get { return this.rightPlayerInputValue; }
            set {
                if (this.rightPlayerInputValue != value) {
                    if (value < FormDisplay.ValueMinimum) this.rightPlayerInputValue = FormDisplay.ValueMinimum;
                    else if (value > FormDisplay.ValueMaximum) this.rightPlayerInputValue = FormDisplay.ValueMaximum;
                    else this.rightPlayerInputValue = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private PlayerSelection buzzeredPlayer = PlayerSelection.NotSelected;
        public PlayerSelection BuzzeredPlayer {
            get { return this.buzzeredPlayer; }
            private set {
                if (this.buzzeredPlayer != value) {
                    this.buzzeredPlayer = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool overrun = false;

        private Insert insertScene;
        public override VRemote4.HandlerSi.Scene.States InsertSceneStatus {
            get {
                if (this.insertScene is Insert) return this.insertScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private Fullscreen fullscreenScene;
        public override VRemote4.HandlerSi.Scene.States FullscreenSceneStatus {
            get {
                if (this.fullscreenScene is Fullscreen) return this.fullscreenScene.Status;
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.WheelOfFortuneNumericInput'", typeIdentifier);
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

            this.LeftPlayerClient = new PlayerClient(syncContext);
            this.LeftPlayerClient.PropertyChanged += this.leftPlayerClient_PropertyChanged;
            this.LeftPlayerClient.PlayerValueReceived += this.leftPlayerClient_PlayerValueReceived;
            this.LeftPlayerClient.Hostname = this.leftPlayerClientHostname;

            this.RightPlayerClient = new PlayerClient(syncContext);
            this.RightPlayerClient.PropertyChanged += this.rightPlayerClient_PropertyChanged;
            this.RightPlayerClient.PlayerValueReceived += this.rightPlayerClient_PlayerValueReceived;
            this.RightPlayerClient.Hostname = this.rightPlayerClientHostname;

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;

            this.fullscreenScene = new Fullscreen(syncContext, fullscreenMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.fullscreenScene.StatusChanged += this.fullscreenScene_StatusChanged;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);
        }

        public override void Dispose() {
            base.Dispose();

            this.LeftPlayerClient.PropertyChanged -= this.leftPlayerClient_PropertyChanged;
            this.LeftPlayerClient.PlayerValueReceived -= this.leftPlayerClient_PlayerValueReceived;
            this.LeftPlayerClient.Dispose();

            this.RightPlayerClient.PropertyChanged -= this.rightPlayerClient_PropertyChanged;
            this.RightPlayerClient.PlayerValueReceived -= this.rightPlayerClient_PlayerValueReceived;
            this.RightPlayerClient.Dispose();

            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Dispose();

            this.fullscreenScene.StatusChanged -= this.fullscreenScene_StatusChanged;
            this.fullscreenScene.Dispose();
        }

        public virtual void DoBuzzer(
            PlayerSelection buzzeredPlayer,
            int value) {
            if (this.BuzzeredPlayer == PlayerSelection.NotSelected &&
                this.BuzzeredPlayer != buzzeredPlayer) {
                this.BuzzeredPlayer = buzzeredPlayer;
                this.overrun = false;
                this.Vfullscreen_StartCountdown();
                switch (buzzeredPlayer) {
                    case PlayerSelection.LeftPlayer:
                        this.LeftPlayerInputValue = value;
                        this.LeftPlayerClient.SetLocked();
                        this.LeftPlayerClient.SetInputValue(this.LeftPlayerInputValue);
                        this.RightPlayerInputValue = 0;
                        this.RightPlayerClient.SetIdle();
                        if (this.WheelValue > this.LeftPlayerInputValue) this.WheelBad();
                        break;
                    case PlayerSelection.RightPlayer:
                        this.LeftPlayerInputValue = 0;
                        this.LeftPlayerClient.SetIdle();
                        this.RightPlayerInputValue = value;
                        this.RightPlayerClient.SetLocked();
                        this.RightPlayerClient.SetInputValue(this.RightPlayerInputValue);
                        if (this.WheelValue > this.RightPlayerInputValue) this.WheelBad();
                        break;
                    case PlayerSelection.NotSelected:
                    default:
                        this.LeftPlayerInputValue = 0;
                        this.RightPlayerInputValue = 0;
                        break;
                }
            }
        }

        public override void ResetData() {
            base.ResetData();
            this.Next();
        }

        public override void Next() {
            base.Next();
            this.LeftPlayerInputValue = 0;
            this.RightPlayerInputValue = 0;
            this.WheelValue = 0;
            this.Vfullscreen_ResetValue();
            this.LeftPlayerClient.SetIdle();
            this.RightPlayerClient.SetIdle();
            this.BuzzeredPlayer = PlayerSelection.NotSelected;
        }

        internal void ConnectClients() {
            this.LeftPlayerClient.Connect();
            this.RightPlayerClient.Connect();
        }
        public void UnlockPlayerClients() {
            this.LeftPlayerClient.SetUnlocked();
            this.RightPlayerClient.SetUnlocked();
        }
        internal void DisconnectClients() {
            this.LeftPlayerClient.Disconnect();
            this.RightPlayerClient.Disconnect();
        }

        internal void AddWheelValue() {
            this.WheelValue++;
            if (this.BuzzeredPlayer != PlayerSelection.NotSelected &&
                !this.overrun) {
                switch (this.BuzzeredPlayer) {
                    case PlayerSelection.LeftPlayer:
                        if (this.WheelValue > this.LeftPlayerInputValue) this.WheelBad();
                        break;
                    case PlayerSelection.RightPlayer:
                        if (this.WheelValue > this.RightPlayerInputValue) this.WheelBad();
                        break;
                }
            }
        }
        internal void WheelGood() {
            this.Vfullscreen_PlayJingleGood();
            this.overrun = true;
        }
        internal void WheelBad() {
            this.Vfullscreen_PlayJingleBad();
            this.overrun = true;
        }

        internal void Resolve() {
            if (this.BuzzeredPlayer != PlayerSelection.NotSelected) {
                switch (this.BuzzeredPlayer) {
                    case PlayerSelection.LeftPlayer:
                        if (this.WheelValue == this.LeftPlayerInputValue) this.LeftPlayerScore++;
                        else this.RightPlayerScore++;
                        break;
                    case PlayerSelection.RightPlayer:
                        if (this.WheelValue == this.RightPlayerInputValue) this.RightPlayerScore++;
                        else this.LeftPlayerScore++;
                        break;
                }
            }
        }

        public override void Vinsert_LoadScene() { this.insertScene.Load(); }
        public override void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score); }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }
        public override void Vinsert_UnloadScene() { this.insertScene.Unload(); }

        public override void Vfullscreen_LoadScene() { this.fullscreenScene.Load(); }
        public void Vfullscreen_ResetValue() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.ResetValue(); }
        public void Vfullscreen_SetValue(int value) { if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.SetValue(value); }
        public void Vfullscreen_StopAudio() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.StopAudio(); }
        public void Vfullscreen_StartCountdown() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.StartCountdown(); }
        public void Vfullscreen_PlayJingleGood() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.PlayJingleGood(); }
        public void Vfullscreen_PlayJingleBad() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.PlayJingleBad(); }
        public override void Vfullscreen_UnloadScene() { this.fullscreenScene.Unload(); }

        public override void ClearGraphic() {
            base.ClearGraphic();
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Clear();
            if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming

        void leftPlayerClient_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftPlayerClient_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftPlayerClient_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "ConnectionStatus" && this.LeftPlayerClient.ConnectionStatus == TCPCom.ConnectionStates.Connected) this.LeftPlayerClient.SetIdle();
            }
        }

        void leftPlayerClient_PlayerValueReceived(object sender, int? e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftPlayerClient_PlayerValueReceived);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftPlayerClient_PlayerValueReceived(object content) {
            int? e = content as int?;
            if (content is int?) {
                if (e.HasValue &&
                    e.Value > 0) {
                    if (this.FlipPlayers) this.DoBuzzer(PlayerSelection.RightPlayer, e.Value);
                    else this.DoBuzzer(PlayerSelection.LeftPlayer, e.Value);
                }
            }
        }

        void rightPlayerClient_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerClient_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerClient_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "ConnectionStatus" && this.RightPlayerClient.ConnectionStatus == TCPCom.ConnectionStates.Connected) this.RightPlayerClient.SetIdle();
            }
        }

        void rightPlayerClient_PlayerValueReceived(object sender, int? e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerClient_PlayerValueReceived);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerClient_PlayerValueReceived(object content) {
            int? e = content as int?;
            if (content is int?) {
                if (e.HasValue &&
                    e.Value > 0) {
                    if (this.FlipPlayers) this.DoBuzzer(PlayerSelection.LeftPlayer, e.Value);
                    else this.DoBuzzer(PlayerSelection.RightPlayer, e.Value);
                }
            }
        }

        #endregion
    }
}
