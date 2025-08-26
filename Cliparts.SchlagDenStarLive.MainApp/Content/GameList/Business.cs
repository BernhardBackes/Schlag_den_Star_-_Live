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
using Cliparts.Tools.Base;
using Cliparts.SchlagDenStarLive.MainApp.Settings;

namespace Cliparts.SchlagDenStarLive.MainApp.Content.GameList {

    public class Business : Messaging.Message, INotifyPropertyChanged {

        #region Properties

        private SynchronizationContext syncContext;

        private MidiHandler.Business midiHandler;

        private BuzzerIO.Business buzzerHandler;

        private AMB.Business ambHandler;

        private Controller devantechHandler;

        private Gameboard.Business gameboard;

        private VentuzScenes.Insert.Business insertMasterScene;
        private VentuzScenes.Fullscreen.Business fullscreenMasterScene;
        private VentuzScenes.Host.Business hostMasterScene;
        private VentuzScenes.Player.Business leftPlayerMasterScene;
        private VentuzScenes.Player.Business rightPlayerMasterScene;
        private VRemote4.HandlerSi.Client.Pipe.Business previewPipe;

        private string leftPlayerName = string.Empty;
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

        private List<Container> games = new List<Container>();
        public Container[] Games {
            get { return this.games.ToArray(); }
            set {
                this.RemoveAllGames();
                if (value is Container[]) {
                    foreach (Container container in value) this.tryAddGame(container, this.GamesCount);
                }
                this.on_PropertyChanged("Names");
            }
        }

        private List<string> names = new List<string>();
        public string[] Names { get { return this.names.ToArray(); } }

        public int GamesCount { get { return this.games.Count; } }

        public int SelectedGameIndex { get; private set; }

        public GamePool.Templates._Base.Business SelectedGame { get; private set; }

        private bool repressPropertyChanged = false;

        #endregion


        #region Funktionen

        public Business() { }
        public Business(
            SynchronizationContext syncContext,
            MidiHandler.Business midiHandler,
            BuzzerIO.Business buzzerHandler,
            AMB.Business ambHandler,
            Controller devantechHandler,
            Gameboard.Business gameboard,
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

            this.gameboard = gameboard;
            this.LeftPlayerName = gameboard.LeftPlayerName;
            this.RightPlayerName = gameboard.RightPlayerName;

            this.insertMasterScene = insertMasterScene;
            this.fullscreenMasterScene = fullscreenMasterScene;
            this.hostMasterScene = hostMasterScene;
            this.leftPlayerMasterScene = leftPlayerMasterScene;
            this.rightPlayerMasterScene = rightPlayerMasterScene;
            this.previewPipe = previewPipe;

            this.SelectedGame = null;
        }

        public void Dispose() {
        }

        public void SetPreviewPipe(
            VRemote4.HandlerSi.Client.Pipe.Business previewPipe) {
            this.previewPipe = previewPipe;
            foreach (Container item in this.games) item.Business.SetPreviewPipe(previewPipe);
        }

        public void Reset() {
            foreach (Container item in this.games) item.Business.ResetData();
        }

        private Container getContainer(
            int index) {
            if (index >= 0 &&
                index < this.GamesCount) return this.games[index];
            else return null;
        }

        public GamePool.Templates._Base.Business GetGame(
            int index) {
            Container container = this.getContainer(index);
            if (container is Container) return container.Business;
            else return null;
        }

        public int GetGameIndex(
            GamePool.Templates._Base.Business game) {
            int index = -1;
            int gameIndex = 0;
            foreach (GameList.Container item in this.games) {
                if (item.Business == game) {
                    index = gameIndex;
                    break;
                }
                gameIndex++;
            }
            return index;
        }

        public void SelectGame(
            int index) {
            if (index < 0) index = -1;
            if (index >= this.GamesCount) index = this.GamesCount - 1;

            if (this.SelectedGame is GamePool.Templates._Base.Business) this.SelectedGame.Deactivate();

            this.SelectedGameIndex = index;
            this.on_PropertyChanged("SelectedGameIndex");
            this.SelectedGame = this.GetGame(index);
            this.on_PropertyChanged("SelectedGame");

            if (this.SelectedGame is GamePool.Templates._Base.Business) this.SelectedGame.Activate();
        }

        public void AddGame(
            string typeIdentifier,
            string name,
            int insertIndex) {
            if (!string.IsNullOrEmpty(name) &&
                !this.names.Contains(name) &&
                this.tryAddGame(new Container(typeIdentifier, name), insertIndex)) {
                this.on_PropertyChanged("Names");
                this.SelectGame(insertIndex);
            }
        }
        private bool tryAddGame(
            Container container,
            int insertIndex) {
            bool success = false;
            try {
                if (container is Container &&
                    !string.IsNullOrEmpty(container.Business.Name) &&
                    !this.names.Contains(container.Business.Name)) {
                    container.Error += this.on_Error;
                    container.Business.Error += this.on_Error;
                    container.Business.PropertyChanged += this.on_PropertyChanged;
                    container.Business.ClearGraphicFired += this.on_ClearGraphic;
                    container.Business.ClearStageFired += this.on_ClearStage;
                    container.Business.Pose(
                        this.syncContext,
                        this.midiHandler,
                        this.buzzerHandler,
                        this.ambHandler,
                        this.devantechHandler,
                        this.gameboard,
                        this.insertMasterScene,
                        this.fullscreenMasterScene,
                        this.hostMasterScene,
                        this.leftPlayerMasterScene,
                        this.rightPlayerMasterScene,
                        this.previewPipe);
                    if (insertIndex < 0) insertIndex = 0;
                    if (insertIndex >= this.GamesCount) {
                        this.games.Add(container);
                        this.names.Add(container.Business.Name);
                    }
                    else {
                        this.games.Insert(insertIndex, container);
                        this.names.Insert(insertIndex, container.Business.Name);
                    }
                    success = true;
                }
            }
            catch (Exception exc) { 
                this.on_Error("tryAddGame", exc.Message); 
            }
            return success;
        }

        public void RenameGame(
            GamePool.Templates._Base.Business game,
            string name) {
            int index = this.GetGameIndex(game);
            if (!string.IsNullOrEmpty(name) &&
                index >= 0) {
                List<string> names = new List<string>(this.Names);
                names.RemoveAt(index);
                if (!names.Contains(name)) {
                    game.Name = name;
                    this.names.Clear();
                    foreach (Container item in this.games) this.names.Add(item.Business.Name);
                    this.on_PropertyChanged("Names");
                }
            }
        }

        public bool TryMoveGameUp(
            int index) {
            Container container = this.getContainer(index);
            if (container is Container &&
                index > 0 &&
                index < this.GamesCount) {
                this.games.RemoveAt(index);
                this.games.Insert(index - 1, container);
                this.buildNameList();
                this.on_PropertyChanged("Names");
                return true;
            }
            else return false;
        }
        public bool TryMoveGameDown(
            int index) {
            Container container = this.getContainer(index);
            if (container is Container &&
                index >= 0 &&
                index < this.GamesCount - 1) {
                this.games.RemoveAt(index);
                this.games.Insert(index + 1, container);
                this.buildNameList();
                this.on_PropertyChanged("Names");
                return true;
            }
            else return false;
        }

        public void RemoveAllGames() {
            if (this.tryRemoveAllGames()) {
                this.SelectGame(0);
                this.buildNameList();
                this.on_PropertyChanged("Names");
            }
        }
        private bool tryRemoveAllGames() {
            bool gameRemoved = false;
            Container[] games = this.Games;
            foreach (Container item in games) gameRemoved = this.tryRemoveGame(item) | gameRemoved;
            return gameRemoved;
        }

        public void RemoveGame(
            int index) {
            if (this.tryRemoveGame(this.getContainer(index))) {
                this.buildNameList();
                this.on_PropertyChanged("Names");
                if (index <= this.SelectedGameIndex) this.SelectGame(this.SelectedGameIndex);
            }
        }
        private bool tryRemoveGame(
            Container container) {
            if (container is Container &&
                this.games.Contains(container)) {
                container.Error += this.on_Error;
                container.Business.Error += this.on_Error;
                container.Business.PropertyChanged += this.on_PropertyChanged;
                container.Business.ClearGraphicFired += this.on_ClearGraphic;
                container.Business.ClearStageFired += this.on_ClearStage;
                this.games.Remove(container);
                return true;
            }
            else return false;
        }

        private void buildNameList() {
            this.names.Clear();
            this.repressPropertyChanged = true;
            foreach (Container item in this.games) this.names.Add(item.Business.Name);
            this.repressPropertyChanged = false;
        }

        public void ClearAllGamesGraphic() { foreach (Container item in this.games) item.Business.ClearGraphic(); }
        public void ClearAllGamesStage() { foreach (Container item in this.games) item.Business.ClearStage(); }

        private void loadGameHistory(
            string filename,
            out Dictionary<string, Container> historyGames) {
            historyGames = new Dictionary<string, Container>();
            if (File.Exists(filename)) {
                try {
                    XElement xml = XElement.Load(filename);
                    historyGames = (Dictionary<string, Container>)XmlSerializationHelper.getDeserialization(xml, typeof(Dictionary<string, Container>));
                }
                catch (Exception) {
                    historyGames = new Dictionary<string, Container>();
                }
            }
        }
        public void SaveGameHistory() {
            Dictionary<string, Container> historyGames = new Dictionary<string, Container>();
            string filename = Path.Combine(ApplicationAttributes.RootPath, Constants.GameHistoryFilename);
            this.loadGameHistory(filename, out historyGames);
            foreach (Container game in this.Games) {
                Container exportedGame;
                if (historyGames.TryGetValue(game.Business.Name, out exportedGame)) {
                }
                else historyGames.Add(game.Business.Name, game);
            }
            try {
                XElement xml = XmlSerializationHelper.getXmlSerialization(historyGames, "Cliparts.SchlagDenStarLive.MainApp.GameHistory", typeof(Dictionary<string, Container>));
                xml.Save(filename);
            }
            catch (Exception) {
                // Fehler weitergeben
            }

        }
        public bool TryCheckAtGameHistory(
            string gameName,
            out GamePool.Templates._Base.Business game) {
            game = null;
            return false;
        }


        #endregion


        #region Events.Outgoing

        public event PropertyChangedEventHandler PropertyChanged;
        protected void on_PropertyChanged([CallerMemberName]string callerName = "") { if (!this.repressPropertyChanged) Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }
        protected void on_PropertyChanged(PropertyChangedEventArgs e) { if (!this.repressPropertyChanged) Helper.raisePropertyChangedEvent(this, this.PropertyChanged, e); }
        protected void on_PropertyChanged(object sender, PropertyChangedEventArgs e) { if (!this.repressPropertyChanged) Helper.raisePropertyChangedEvent(sender, this.PropertyChanged, e); }

        public event EventHandler ClearGraphicFired;
        protected void on_ClearGraphic(object sender, EventArgs e) { Helper.raiseEvent(sender, this.ClearGraphicFired, e); }

        public event EventHandler ClearStageFired;
        protected void on_ClearStage(object sender, EventArgs e) { Helper.raiseEvent(sender, this.ClearStageFired, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }
}
