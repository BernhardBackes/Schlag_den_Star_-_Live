using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

using Cliparts.Devantech;

using Cliparts.Serialization;

namespace Cliparts.SchlagDenStarLive.MainApp.Content {

    public class Data {
        public string LeftPlayerName { get; set; }
        public string RightPlayerName { get; set; }
        public Gameboard.Level[] GameboardLevelList { get; set; }
        public int SelectedLevelIndex { get; set; }
        public GameList.Container[] GameList { get; set; }
        public int SelectedGameIndex { get; set; }
        public Playlist.DatasetContent[] Playlist { get; set; }
    }

    public class Business : Cliparts.Messaging.Message, INotifyPropertyChanged {

        #region Properties

        private SynchronizationContext syncContext;

        private MidiHandler.Business midiHandler;

        private BuzzerIO.Business buzzerHandler;

        private AMB.Business ambHandler;

        private Controller devantechHandler;

        private Settings.Business settings;

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

        //private VRemote4.HandlerSi.Client.Pipe.Business previewPipe;

        public Gameboard.Business Gameboard { get; set; }

        public GameList.Business GameList { get; set; }

        public Playlist.Business Playlist { get; set; }

        public string Filename {
            get { return this.settings.ContentFilename; }
            private set {
                if (this.settings.ContentFilename != value) {
                    if (string.IsNullOrEmpty(value)) value = string.Empty;
                    this.settings.ContentFilename = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool isLoading = false;

        #endregion


        #region Funktionen

        public Business(
            SynchronizationContext syncContext,
            MidiHandler.Business midiHandler,
            BuzzerIO.Business buzzerHandler,
            AMB.Business ambHandler,
            Controller devantechHandler,
            Settings.Business settings,
            VentuzScenes.Insert.Business insertMasterScene,
            VentuzScenes.Fullscreen.Business fullscreenMasterScene,
            VentuzScenes.Host.Business hostMasterScene,
            VentuzScenes.Player.Business leftPlayerMasterScene,
            VentuzScenes.Player.Business rightPlayerMasterScene,
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {

            this.syncContext = syncContext;
            this.midiHandler = midiHandler;
            this.buzzerHandler = buzzerHandler;
            this.ambHandler = ambHandler;
            this.devantechHandler = devantechHandler;
            this.settings = settings;
            this.insertMasterScene = insertMasterScene;
            this.fullscreenMasterScene = fullscreenMasterScene;
            this.hostMasterScene = hostMasterScene;
            this.leftPlayerMasterScene = leftPlayerMasterScene;
            this.rightPlayerMasterScene = rightPlayerMasterScene;
            //this.previewPipe = previewPipe;

            this.Gameboard = new Gameboard.Business(syncContext, fullscreenMasterScene);
            this.Gameboard.Error += this.on_Error;
            this.Gameboard.PropertyChanged += this.gameboard_PropertyChanged;

            this.GameList = new GameList.Business(
                syncContext,
                midiHandler,
                buzzerHandler,
                ambHandler,
                devantechHandler,
                this.Gameboard,
                insertMasterScene,
                fullscreenMasterScene,
                hostMasterScene,
                leftPlayerMasterScene,
                rightPlayerMasterScene,
                previewPipe);
            this.GameList.Error += this.on_Error;
            this.GameList.PropertyChanged += this.gamelist_PropertyChanged;
            this.GameList.ClearGraphicFired += this.on_ClearGraphicFired;
            this.GameList.ClearStageFired += this.on_ClearStageFired;

            this.Playlist = new Playlist.Business(
                syncContext,
                insertMasterScene,
                fullscreenMasterScene);
            this.Playlist.Error += this.on_Error;
            this.Playlist.PropertyChanged += this.playlist_PropertyChanged;
        }

        public void Dispose() {
            this.Gameboard.Error -= this.on_Error;
            this.Gameboard.PropertyChanged -= this.gameboard_PropertyChanged;
            this.Gameboard.Dispose();
            this.GameList.Error -= this.on_Error;
            this.GameList.PropertyChanged -= this.gamelist_PropertyChanged;
            this.GameList.ClearGraphicFired -= this.on_ClearGraphicFired;
            this.GameList.ClearStageFired -= this.on_ClearStageFired;
            this.GameList.Dispose();
        }

        public void ResetAll() {
            this.Gameboard.Reset();
            this.GameList.Reset();
        }

        public void New(
            string filename) {
            this.SaveAs(filename);
        }

        public void Load(
            string filename) {
            string subSender = "Load";
            if (File.Exists(filename)) {
                try {
                    this.isLoading = true;
                    //XmlSerializer serializer = new XmlSerializer(typeof(Data));
                    //using (StreamReader reader = new StreamReader(filename)) data = (Data)serializer.Deserialize(reader);
                    XElement xml = XElement.Load(filename);
                    Data data = (Data)XmlSerializationHelper.getDeserialization(xml, typeof(Data));
                    this.GameList.Games = data.GameList;
                    this.Gameboard.LeftPlayerName = data.LeftPlayerName;
                    this.Gameboard.RightPlayerName = data.RightPlayerName;
                    this.Gameboard.LevelList = data.GameboardLevelList;
                    this.Gameboard.SelectedLevelIndex = data.SelectedLevelIndex;
                    this.GameList.SelectGame(data.SelectedGameIndex);
                    this.Playlist.DataList = data.Playlist;
                    this.Filename = filename;
                }
                catch (Exception exc) {
                    // Fehler weitergeben
                    this.on_Error(subSender, exc.Message);
                }
                finally {
                    this.isLoading = false;
                }
            }
            else {
                // Fehler weitergeben
                this.on_Error(subSender, "Datei nicht vorhanden - '" + filename + "'");
            }
        }

        public void Save() {
            string subSender = "Save";
            if (File.Exists(this.Filename)) this.SaveAs(this.Filename);
            else this.on_Error(subSender, "Datei nicht vorhanden - '" + this.Filename + "'");
        }

        public void SaveAs(
            string filename) {
            string subSender = "SaveAs";
            if (!this.isLoading) {
                try {
                    // Dokument speichern
                    Data data = new Data();
                    data.LeftPlayerName = this.Gameboard.LeftPlayerName;
                    data.RightPlayerName = this.Gameboard.RightPlayerName;
                    data.GameboardLevelList = this.Gameboard.LevelList;
                    data.SelectedLevelIndex = this.Gameboard.SelectedLevelIndex;
                    data.GameList = this.GameList.Games;
                    data.SelectedGameIndex = this.GameList.SelectedGameIndex;
                    data.Playlist = this.Playlist.DataList;
                    //XmlSerializer serializer = new XmlSerializer(typeof(Data));
                    //using (StreamWriter writer = new StreamWriter(filename)) serializer.Serialize(writer, data);
                    XElement xml = XmlSerializationHelper.getXmlSerialization(data, "Cliparts.SchlagDenStarLive.MainApp.Content.Data", typeof(Cliparts.SchlagDenStarLive.MainApp.Content.Data));
                    xml.Save(filename);
                    this.Filename = filename;
                }
                catch (Exception exc) {
                    // Fehler weitergeben
                    this.on_Error(subSender, exc.Message);
                }
            }
        }

        private void syncLeftName() {
            if (this.fullscreenMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenMasterScene.Gameboard.SetLeftPlayerName(this.Gameboard.LeftPlayerName);
            if (this.hostMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostMasterScene.SetLeftPlayerName(this.Gameboard.LeftPlayerName);
            if (this.leftPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerMasterScene.SetLeftPlayerName(this.Gameboard.LeftPlayerName);
            if (this.rightPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerMasterScene.SetLeftPlayerName(this.Gameboard.LeftPlayerName);
        }

        private void syncLeftTotalScore() {
            //if (this.fullscreenMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenMasterScene.Gameboard.SetLeftPlayerScore(this.Gameboard.LeftPlayerTotalScore);
            if (this.hostMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostMasterScene.SetLeftPlayerTotalScore(this.Gameboard.LeftPlayerTotalScore);
            if (this.leftPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerMasterScene.SetLeftPlayerTotalScore(this.Gameboard.LeftPlayerTotalScore);
            if (this.rightPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerMasterScene.SetLeftPlayerTotalScore(this.Gameboard.LeftPlayerTotalScore);
        }

        private void syncRightName() {
            if (this.fullscreenMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenMasterScene.Gameboard.SetRightPlayerName(this.Gameboard.RightPlayerName);
            if (this.hostMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostMasterScene.SetRightPlayerName(this.Gameboard.RightPlayerName);
            if (this.leftPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerMasterScene.SetRightPlayerName(this.Gameboard.RightPlayerName);
            if (this.rightPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerMasterScene.SetRightPlayerName(this.Gameboard.RightPlayerName);
        }

        private void syncRightTotalScore() {
            //if (this.fullscreenMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenMasterScene.Gameboard.SetRightPlayerScore(this.Gameboard.RightPlayerTotalScore);
            if (this.hostMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostMasterScene.SetRightPlayerTotalScore(this.Gameboard.RightPlayerTotalScore);
            if (this.leftPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerMasterScene.SetRightPlayerTotalScore(this.Gameboard.RightPlayerTotalScore);
            if (this.rightPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerMasterScene.SetRightPlayerTotalScore(this.Gameboard.RightPlayerTotalScore);
        }

        #endregion


        #region Events.Outgoing

        public event PropertyChangedEventHandler PropertyChanged;
        protected void on_PropertyChanged([CallerMemberName]string callerName = "") { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }
        protected void on_PropertyChanged(PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, e); }
        protected void on_PropertyChanged(object sender, PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(sender, this.PropertyChanged, e); }

        public event EventHandler ClearGraphicFired;
        protected void on_ClearGraphicFired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.ClearGraphicFired, e); }

        public event EventHandler ClearStageFired;
        protected void on_ClearStageFired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.ClearStageFired, e); }

        #endregion

        #region Events.Incoming

        void gameboard_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_gameboard_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_gameboard_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "SelectedLevelIndex") this.GameList.SelectGame(this.Gameboard.SelectedLevelIndex);
                else if (e.PropertyName == "LeftPlayerName") this.syncLeftName();
                else if (e.PropertyName == "LeftPlayerTotalScore") this.syncLeftTotalScore();
                else if (e.PropertyName == "RightPlayerName") this.syncRightName();
                else if (e.PropertyName == "RightPlayerTotalScore") this.syncRightTotalScore();
            }
            this.Save();
        }

        void gamelist_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_gamelist_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_gamelist_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) { }
            this.Save();
        }

        void playlist_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_playlist_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_playlist_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) { }
            this.Save();
        }

        #endregion

    }
}
