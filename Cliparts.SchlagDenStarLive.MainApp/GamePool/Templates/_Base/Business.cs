using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.Devantech;
using Cliparts.Serialization;

using Cliparts.Tools.Base;

using Cliparts.SchlagDenStarLive.MainApp.Settings;
using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base {

    public class Business : Messaging.Message, INotifyPropertyChanged {

        #region Properties

        protected SynchronizationContext syncContext;

        protected Content.Gameboard.Business gameboard;

        protected string typeIdentifier;
        public string TypeIdentifier { get { return this.typeIdentifier; } }

        public UserControlContent ContentControl { get; protected set; }
        public UserControlGame GameControl { get; protected set; }

        public string ClassInfo { get; protected set; }

        protected bool isActive = false;

        private string name;
        public string Name {
            get { return this.name; }
            set {
                if (this.name != value) {
                    if (value == null) value = string.Empty;
                    this.name = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string notepad;
        public string Notepad {
            get { return this.notepad; }
            set {
                if (this.notepad != value) {
                    if (value == null) value = string.Empty;
                    this.notepad = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string leftPlayerName = string.Empty;
        [NotSerialized]
        public string LeftPlayerName {
            get { return this.leftPlayerName; }
            set {
                if (this.leftPlayerName != value) {
                    if (string.IsNullOrEmpty(value)) value = string.Empty;
                    this.leftPlayerName = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string rightPlayerName = string.Empty;
        [NotSerialized]
        public string RightPlayerName {
            get { return this.rightPlayerName; }
            set {
                if (this.rightPlayerName != value) {
                    if (string.IsNullOrEmpty(value)) value = string.Empty;
                    this.rightPlayerName = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private PlayerSelection winner = PlayerSelection.NotSelected;
        public PlayerSelection Winner {
            get { return this.winner; }
            set {
                if (this.winner != value) {
                    this.winner = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool showFullscreenTimer = true;
        public bool ShowFullscreenTimer
        {
            get { return this.showFullscreenTimer; }
            set
            {
                if (this.showFullscreenTimer != value)
                {
                    this.showFullscreenTimer = value;
                    if (value) this.ShowFullscreenScore = false;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool showFullscreenScore = false;
        public bool ShowFullscreenScore
        {
            get { return this.showFullscreenScore; }
            set
            {
                if (this.showFullscreenScore != value)
                {
                    this.showFullscreenScore = value;
                    if (value) this.ShowFullscreenTimer = false;
                    this.on_PropertyChanged();
                }
            }
        }

        protected VentuzScenes.Insert.Business insertMasterScene;
        protected VRemote4.HandlerSi.Scene.States insertMasterSceneStatus { 
            get {
                if (this.insertMasterScene is VentuzScenes.Insert.Business) return this.insertMasterScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            } 
        }

        protected VentuzScenes.Fullscreen.Business fullscreenMasterScene;
        protected VRemote4.HandlerSi.Scene.States fullscreenMasterSceneStatus {
            get {
                if (this.fullscreenMasterScene is VentuzScenes.Fullscreen.Business) return this.fullscreenMasterScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        protected VentuzScenes.Host.Business hostMasterScene;
        protected VRemote4.HandlerSi.Scene.States hostMasterSceneStatus {
            get {
                if (this.hostMasterScene is VentuzScenes.Host.Business) return this.hostMasterScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        protected VentuzScenes.Player.Business leftPlayerMasterScene;
        protected VRemote4.HandlerSi.Scene.States leftPlayerMasterSceneStatus {
            get {
                if (this.leftPlayerMasterScene is VentuzScenes.Player.Business) return this.leftPlayerMasterScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        protected VentuzScenes.Player.Business rightPlayerMasterScene;
        protected VRemote4.HandlerSi.Scene.States rightPlayerMasterSceneStatus {
            get {
                if (this.rightPlayerMasterScene is VentuzScenes.Player.Business) return this.rightPlayerMasterScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        protected VRemote4.HandlerSi.Client.Pipe.Business previewPipe = null;

        public virtual VRemote4.HandlerSi.Scene.States InsertSceneStatus { get { return VRemote4.HandlerSi.Scene.States.Unloaded; } }
        public virtual VRemote4.HandlerSi.Scene.States FullscreenSceneStatus { get { return VRemote4.HandlerSi.Scene.States.Unloaded; } }
        public virtual VRemote4.HandlerSi.Scene.States HostSceneStatus { get { return VRemote4.HandlerSi.Scene.States.Unloaded; } }
        public virtual VRemote4.HandlerSi.Scene.States LeftPlayerSceneStatus { get { return VRemote4.HandlerSi.Scene.States.Unloaded; } }
        public virtual VRemote4.HandlerSi.Scene.States RightPlayerSceneStatus { get { return VRemote4.HandlerSi.Scene.States.Unloaded; } }
        public virtual VRemote4.HandlerSi.Scene.States PreviewSceneStatus { get { return VRemote4.HandlerSi.Scene.States.Unloaded; } }

        #endregion


        #region Funktionen

        public Business() {}
        public Business(string typeIdentifier) { this.typeIdentifier = typeIdentifier; }

        public virtual void New() {
            this.Notepad = string.Empty;
            this.ResetData();
        }

        public virtual void Pose(
            SynchronizationContext syncContext,
            MidiHandler.Business midiHandler,
            BuzzerIO.Business buzzerHandler,
            AMB.Business ambHandler,
            Controller devantechHandler,
            Content.Gameboard.Business gameboard,
            VentuzScenes.Insert.Business insertMasterScene,
            VentuzScenes.Fullscreen.Business fullscreenMasterScene,
            VentuzScenes.Host.Business hostMasterScene,
            VentuzScenes.Player.Business leftPlayerMasterScene,
            VentuzScenes.Player.Business rightPlayerMasterScene,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {

            this.syncContext = syncContext;

            this.gameboard = gameboard;
            this.gameboard.PropertyChanged += this.gameboard_PropertyChanged;
            this.LeftPlayerName = gameboard.LeftPlayerName;
            this.RightPlayerName = gameboard.RightPlayerName;

            this.insertMasterScene = insertMasterScene;
            this.fullscreenMasterScene = fullscreenMasterScene;
            this.hostMasterScene = hostMasterScene;
            this.leftPlayerMasterScene = leftPlayerMasterScene;
            this.rightPlayerMasterScene = rightPlayerMasterScene;

            this.SetPreviewPipe(previewPipe);
        }

        public virtual void Dispose() {
            this.gameboard.PropertyChanged -= this.gameboard_PropertyChanged;
            this.ContentControl.Dispose();
            this.GameControl.Dispose();
        }

        public virtual void SetPreviewPipe(
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            if (previewPipe is VRemote4.HandlerSi.Client.Pipe.Business) {
                this.previewPipe = previewPipe;
                this.ContentControl.SetPreviewPipe(previewPipe);
            }
        }

        public virtual void Activate() { this.isActive = true; }
        public virtual void Deactivate() { this.isActive = false; }

        public virtual void ResetData() {
            this.Winner = PlayerSelection.NotSelected;
        }

        public virtual void Next() { }

        public void ToggleWinner(
            PlayerSelection winner) {
            if (this.Winner == winner) this.Winner = PlayerSelection.NotSelected;
            else this.Winner = winner;
        }

        public virtual void V_ClearGraphic() { this.on_ClearGraphic(this, new EventArgs()); }

        public virtual void Vinsert_LoadScene() { }
        public virtual void Vinsert_UnloadScene() { }

        public virtual void Vfullscreen_LoadScene() { }
        public virtual void Vfullscreen_ShowGame() { if (this.fullscreenMasterScene is VRemote4.HandlerSi.Scene)  this.fullscreenMasterScene.ShowGame(); }
        public virtual void Vfullscreen_ShowTimer() {
            this.Vfullscreen_SetTimer();
            this.Vfullscreen_ResetTimer();
            if (this.fullscreenMasterScene is VRemote4.HandlerSi.Scene && this.ShowFullscreenTimer) this.fullscreenMasterScene.ShowTimer();
        }
        public virtual void Vfullscreen_SetTimer() { }
        public virtual void Vfullscreen_StartTimer() { if (this.fullscreenMasterScene is VRemote4.HandlerSi.Scene) this.fullscreenMasterScene.Timer.StartTimer(); }
        public virtual void Vfullscreen_StopTimer() { if (this.fullscreenMasterScene is VRemote4.HandlerSi.Scene)  this.fullscreenMasterScene.Timer.StopTimer(); }
        public virtual void Vfullscreen_ContinueTimer() { if (this.fullscreenMasterScene is VRemote4.HandlerSi.Scene)  this.fullscreenMasterScene.Timer.ContinueTimer(); }
        public virtual void Vfullscreen_ResetTimer() {
            this.Vfullscreen_SetTimer();
            if (this.fullscreenMasterScene is VRemote4.HandlerSi.Scene)  this.fullscreenMasterScene.Timer.ResetTimer(); 
        }
        public virtual void Vfullscreen_ShowScore() {
            this.Vfullscreen_SetScore();
            if (this.fullscreenMasterScene is VRemote4.HandlerSi.Scene) this.fullscreenMasterScene.ShowScore();
        }
        public virtual void Vfullscreen_SetScore() { }
        public virtual void Vfullscreen_ShowFreetext() { if (this.fullscreenMasterScene is VRemote4.HandlerSi.Scene) this.fullscreenMasterScene.ShowFreetext(); }
        public virtual void Vfullscreen_SetFreetext(string value) { if (this.fullscreenMasterScene is VRemote4.HandlerSi.Scene) this.fullscreenMasterScene.Freetext.SetTextValue(value); }
        public virtual void Vfullscreen_SetFreetextColor(Color value) { if (this.fullscreenMasterScene is VRemote4.HandlerSi.Scene) this.fullscreenMasterScene.Freetext.SetTextColor(value); }
        public virtual void Vfullscreen_UnloadScene() { }
        public virtual void Vfullscreen_ShowGameboard() { this.gameboard.ShowGraphic(); }
        public virtual void Vfullscreen_UpdateGameboard() { this.gameboard.UpdateGraphic(); }

        public virtual void V_ClearStage() { 
            this.on_ClearStage(this, new EventArgs()); 
        }

        public virtual void Vhost_LoadScene() { }
        public virtual void Vhost_UnloadScene() { }

        public virtual void Vleftplayer_LoadScene() { }
        public virtual void Vleftplayer_UnloadScene() { }

        public virtual void Vrightplayer_LoadScene() { }
        public virtual void Vrightplayer_UnloadScene() { }

        public virtual void Init() { this.ResetData(); }

        public void SetWinner() { this.gameboard.SelectedLevel.Winner = this.Winner; }

        public void ParseKey(Keys key) { this.GameControl.ParseKey(key); }

        protected virtual void SetGameControlStep(int stepIndex) { if (this.GameControl is UserControlGame) this.GameControl.SetStep(stepIndex); }
        protected virtual void SetGameControlToNextStep() { if (this.GameControl is UserControlGame) this.GameControl.SetToNextStep(); }

        public virtual void ClearGraphic() { }
        public virtual void ClearStage() { }

        #endregion


        #region Events.Outgoing

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void on_PropertyChanged([CallerMemberName]string callerName = "") { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }
        protected virtual void on_PropertyChanged(PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, e); }
        protected virtual void on_PropertyChanged(object sender, PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(sender, this.PropertyChanged, e); }

        public event EventHandler ClearGraphicFired;
        protected virtual void on_ClearGraphic(object sender, EventArgs e) { Helper.raiseEvent(sender, this.ClearGraphicFired, e); }

        public event EventHandler ClearStageFired;
        protected virtual void on_ClearStage(object sender, EventArgs e) { Helper.raiseEvent(sender, this.ClearStageFired, e); }

        #endregion

        #region Events.Incoming

        void gameboard_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_gameboard_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_gameboard_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "LeftPlayerName") this.LeftPlayerName = this.gameboard.LeftPlayerName;
                else if (e.PropertyName == "RightPlayerName") this.RightPlayerName = this.gameboard.RightPlayerName;
            }
        }

        protected void insertScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_insertScene_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_insertScene_StatusChanged(object content) {
            this.on_PropertyChanged("InsertSceneStatus");
        }

        protected void fullscreenScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_fullscreenScene_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_fullscreenScene_StatusChanged(object content) {
            this.on_PropertyChanged("FullscreenSceneStatus");
        }

        protected void hostScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_hostScene_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_hostScene_StatusChanged(object content) {
            this.on_PropertyChanged("HostSceneStatus");
        }

        protected void leftPlayerScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftPlayerScene_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_leftPlayerScene_StatusChanged(object content) {
            this.on_PropertyChanged("LeftPlayerSceneStatus");
        }

        protected void rightPlayerScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerScene_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_rightPlayerScene_StatusChanged(object content) {
            this.on_PropertyChanged("RightPlayerSceneStatus");
        }

        #endregion

    }

}
