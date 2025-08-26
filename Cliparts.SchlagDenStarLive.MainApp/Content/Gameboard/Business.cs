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


namespace Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard {

    public class Business : Cliparts.Messaging.Message, INotifyPropertyChanged {

        #region Properties

        public const int GamesCount = 15;

        private SynchronizationContext syncContext;

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


        private ushort leftPlayerTotalScore = 0;
        public ushort LeftPlayerTotalScore {
            get { return this.leftPlayerTotalScore; }
            private set {
                if (this.leftPlayerTotalScore != value) {
                    this.leftPlayerTotalScore = value;
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

        private ushort rightPlayerTotalScore = 0;
        public ushort RightPlayerTotalScore {
            get { return this.rightPlayerTotalScore; }
            private set {
                if (this.rightPlayerTotalScore != value) {
                    this.rightPlayerTotalScore = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private List<Level> levelList = new List<Level>();
        public Level[] LevelList {
            get { return this.levelList.ToArray(); }
            set {
                if (value is Level[]) {
                    int index = 0;
                    foreach (Level item in this.levelList) {
                        if (value.Length > index &&
                            value[index] is Level) item.Clone(value[index]);
                        else item.Reset();
                        index++;
                    }
                }
            }
        }

        private Level selectedLevel = null;
        public Level SelectedLevel { get { return this.selectedLevel; } }

        private int selectedLevelIndex = -1;
        public int SelectedLevelIndex {
            get { return this.selectedLevelIndex; }
            set {
                if (this.selectedLevelIndex != value) {
                    if (value < 0) value = -1;
                    if (value >= GamesCount) value = GamesCount - 1;
                    this.selectedLevelIndex = value;
                    this.on_PropertyChanged();
                    this.selectLevel(value);
                }
            }
        }

        private bool repressPropertyChangedEvent = false;

        private VentuzScenes.Fullscreen.Business ventuzFullscreenMasterScene;

        #endregion


        #region Funktionen

        public Business(
            SynchronizationContext syncContext,
            VentuzScenes.Fullscreen.Business ventuzFullscreenMasterScene) {
            this.syncContext = syncContext;
            this.ventuzFullscreenMasterScene = ventuzFullscreenMasterScene;
            this.ventuzFullscreenMasterScene.Gameboard.GameMoveFinished += this.gameboard_GameMoveFinished;
            this.fillLevelList();
        }

        public void Dispose() {
            this.ventuzFullscreenMasterScene.Gameboard.GameMoveFinished -= this.gameboard_GameMoveFinished;
            foreach (Level item in this.levelList) {
                item.PropertyChanged -= this.level_PropertyChanged;
                item.Dispose();
            }
            this.levelList.Clear();
        }

        private void fillLevelList() {
            while (this.levelList.Count < GamesCount) {
                ushort id = Convert.ToUInt16(this.levelList.Count + 1);
                ushort value = id;
                Level newLevel = new Level(id, value);
                newLevel.PropertyChanged += this.level_PropertyChanged;
                newLevel.SelectionGotten += this.level_SelectionGotten;
                this.levelList.Add(newLevel);
            }
        }

        private void calcTotalScores() {
            ushort leftPlayerTotalScore = 0;
            ushort rightPlayerTotalScore = 0;
            foreach (Level item in this.levelList) {
                leftPlayerTotalScore += item.LeftPlayerValue;
                rightPlayerTotalScore += item.RightPlayerValue;
            }
            this.LeftPlayerTotalScore = leftPlayerTotalScore;
            this.RightPlayerTotalScore = rightPlayerTotalScore;
        }

        private void selectLevel(
            int index) {
            int levelIndex = 0;
            foreach (Level item in this.levelList) {
                item.Selected = levelIndex == index;
                if (item.Selected) this.selectedLevel = item;
                levelIndex++;
            }
        }

        public void Reset() {
            this.repressPropertyChangedEvent = true;
            foreach (Level item in this.levelList) item.Reset();
            this.repressPropertyChangedEvent = false;
            this.calcTotalScores();
            this.SelectedLevelIndex = 0;
        }

        public void SetGraphic() {
            if (this.ventuzFullscreenMasterScene.Status == VRemote4.HandlerSi.Scene.States.Available) {
                this.ventuzFullscreenMasterScene.Gameboard.SetLeftPlayerName(this.LeftPlayerName);
                this.ventuzFullscreenMasterScene.Gameboard.SetLeftPlayerScore(this.LeftPlayerTotalScore);
                this.ventuzFullscreenMasterScene.Gameboard.SetLeftPlayerNewScore(this.LeftPlayerTotalScore);
                this.ventuzFullscreenMasterScene.Gameboard.SetRightPlayerName(this.RightPlayerName);
                this.ventuzFullscreenMasterScene.Gameboard.SetRightPlayerScore(this.RightPlayerTotalScore);
                this.ventuzFullscreenMasterScene.Gameboard.SetRightPlayerNewScore(this.RightPlayerTotalScore);
                this.ventuzFullscreenMasterScene.Gameboard.SetToShow();
                int id = 0;
                foreach (Level item in this.levelList) {
                    id++;
                    if (item.Enabled) {
                        switch (item.Winner) {
                            case PlayerSelection.LeftPlayer:
                                this.ventuzFullscreenMasterScene.Gameboard.SetTopGameOut(id);
                                this.ventuzFullscreenMasterScene.Gameboard.SetLeftPlayerGameIn(id);
                                this.ventuzFullscreenMasterScene.Gameboard.SetRightPlayerGameOut(id);
                                break;
                            case PlayerSelection.RightPlayer:
                                this.ventuzFullscreenMasterScene.Gameboard.SetTopGameOut(id);
                                this.ventuzFullscreenMasterScene.Gameboard.SetLeftPlayerGameOut(id);
                                this.ventuzFullscreenMasterScene.Gameboard.SetRightPlayerGameIn(id);
                                break;
                            case PlayerSelection.NotSelected:
                            default:
                                this.ventuzFullscreenMasterScene.Gameboard.SetTopGameIn(id);
                                this.ventuzFullscreenMasterScene.Gameboard.SetLeftPlayerGameOut(id);
                                this.ventuzFullscreenMasterScene.Gameboard.SetRightPlayerGameOut(id);
                                break;
                        }
                    }
                    else {
                        this.ventuzFullscreenMasterScene.Gameboard.SetTopGameIn(id);
                        this.ventuzFullscreenMasterScene.Gameboard.SetLeftPlayerGameOut(id);
                        this.ventuzFullscreenMasterScene.Gameboard.SetRightPlayerGameOut(id);
                    }
                }
            }
        }
        public void ShowGraphic() {
            this.SetGraphic();
            if (this.ventuzFullscreenMasterScene.Status == VRemote4.HandlerSi.Scene.States.Available) this.ventuzFullscreenMasterScene.ShowGameboard();
        }
        public void UpdateGraphic() {
            this.ventuzFullscreenMasterScene.Gameboard.SetLeftPlayerNewScore(this.LeftPlayerTotalScore);
            this.ventuzFullscreenMasterScene.Gameboard.SetRightPlayerNewScore(this.RightPlayerTotalScore);
            this.ventuzFullscreenMasterScene.Gameboard.SetSelectedGameID(this.SelectedLevelIndex + 1);
            switch (this.SelectedLevel.Winner) {
                case PlayerSelection.LeftPlayer:
                    this.ventuzFullscreenMasterScene.Gameboard.MoveSelectedGameToLeft();
                    break;
                case PlayerSelection.RightPlayer:
                    this.ventuzFullscreenMasterScene.Gameboard.MoveSelectedGameToRight();
                    break;
            }
        }

        public void ShowPiechart(
            int id) {
            if (this.ventuzFullscreenMasterScene.Status == VRemote4.HandlerSi.Scene.States.Available) {
                this.ventuzFullscreenMasterScene.Piechart.SetGameID(id);
                this.ventuzFullscreenMasterScene.ShowPiechart();
            }
        }

        #endregion


        #region Events.Outgoing

        public event PropertyChangedEventHandler PropertyChanged;
        protected void on_PropertyChanged([CallerMemberName]string callerName = "") { if (!this.repressPropertyChangedEvent) Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }
        protected void on_PropertyChanged(PropertyChangedEventArgs e) { if (!this.repressPropertyChangedEvent) Helper.raisePropertyChangedEvent(this, this.PropertyChanged, e); }
        protected void on_PropertyChanged(object sender, PropertyChangedEventArgs e) { if (!this.repressPropertyChangedEvent) Helper.raisePropertyChangedEvent(sender, this.PropertyChanged, e); }

        #endregion

        #region Events.Incoming

        void level_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_level_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_level_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (!repressPropertyChangedEvent &&
                e is PropertyChangedEventArgs) {
                    if (e.PropertyName == "Winner" ||
                        e.PropertyName == "Enabled") this.calcTotalScores();
            }
        }

        void level_SelectionGotten(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_level_SelectionGotten);
            if (this.syncContext != null) this.syncContext.Post(callback, sender);
        }
        private void sync_level_SelectionGotten(object content) {
            Level sender = content as Level;
            if (sender is Level) this.SelectedLevelIndex = Convert.ToUInt16(sender.ID - 1);
        }

        void gameboard_GameMoveFinished(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_gameboard_GameMoveFinished);
            if (this.syncContext != null) this.syncContext.Post(callback, sender);
        }
        private void sync_gameboard_GameMoveFinished(object content) { this.SetGraphic(); }

        #endregion

    }
}
