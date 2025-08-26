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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.ScorePyramid;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.ScorePyramid {

    public class DatasetLevel : INotifyPropertyChanged {

        #region Properties

        public int Value { get; private set; }

        private int leftPlayerHits = 0;
        public int LeftPlayerHits {
            get { return this.leftPlayerHits; }
            set {
                if (this.leftPlayerHits != value) {
                    if (value <= 0) this.leftPlayerHits = 0;
                    else this.leftPlayerHits = value;
                    this.on_PropertyChanged();
                    this.calcPlayerValues();
                }
            }
        }

        private int leftPlayerValue = 0;
        public int LeftPlayerValue {
            get { return this.leftPlayerValue; }
            private set {
                if (this.leftPlayerValue != value) {
                    if (value <= 0) this.leftPlayerValue = 0;
                    else this.leftPlayerValue = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerHits = 0;
        public int RightPlayerHits {
            get { return this.rightPlayerHits; }
            set {
                if (this.rightPlayerHits != value) {
                    if (value <= 0) this.rightPlayerHits = 0;
                    else this.rightPlayerHits = value;
                    this.on_PropertyChanged();
                    this.calcPlayerValues();
                }
            }
        }

        private int rightPlayerValue = 0;
        public int RightPlayerValue {
            get { return this.rightPlayerValue; }
            private set {
                if (this.rightPlayerValue != value) {
                    if (value <= 0) this.rightPlayerValue = 0;
                    else this.rightPlayerValue = value;
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion


        #region Funktionen

        public DatasetLevel(
            int value) {
            this.Value = value;
        }
        public DatasetLevel(
            int value,
            int leftPlayerHits,
            int rightPlayerHits) {
            this.Value = value;
            this.leftPlayerHits = leftPlayerHits;
            this.rightPlayerHits = rightPlayerHits;
            this.calcPlayerValues();
        }

        private void calcPlayerValues() {
            if (this.leftPlayerHits != this.rightPlayerHits) {
                if (this.leftPlayerHits > this.rightPlayerHits) {
                    this.LeftPlayerValue = this.Value;
                    this.RightPlayerValue = 0;
                }
                else {
                    this.LeftPlayerValue = 0;
                    this.RightPlayerValue = this.Value;
                }
            }
            else {
                this.LeftPlayerValue = 0;
                this.RightPlayerValue = 0;
            }
        }

        public void Reset() {
            this.LeftPlayerHits = 0;
            this.RightPlayerHits = 0;
        }

        #endregion


        #region Events.Outgoing

        public event PropertyChangedEventHandler PropertyChanged;
        protected void on_PropertyChanged([CallerMemberName]string callerName = "") { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }
        protected void on_PropertyChanged(PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, e); }
        protected void on_PropertyChanged(object sender, PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(sender, this.PropertyChanged, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }

    public class Business : _Base.Business {

        #region Properties

        private int leftPlayerScore = 0;
        public int LeftPlayerScore {
            get { return this.leftPlayerScore; }
            private set {
                if (this.leftPlayerScore != value) {
                    if (value < 0) value = 0;
                    this.leftPlayerScore = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerScore = 0;
        public int RightPlayerScore {
            get { return this.rightPlayerScore; }
            private set {
                if (this.rightPlayerScore != value) {
                    if (value < 0) value = 0;
                    this.rightPlayerScore = value;
                    this.on_PropertyChanged();
                }
            }
        }

        public const int LevelsCount = 11;

        private List<DatasetLevel> levelList = new List<DatasetLevel>();

        private int gamePositionX = 0;
        public int GamePositionX {
            get { return this.gamePositionX; }
            set {
                if (this.gamePositionX != value) {
                    this.gamePositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetGame();
                }
            }
        }

        private int gamePositionY = 0;
        public int GamePositionY {
            get { return this.gamePositionY; }
            set {
                if (this.gamePositionY != value) {
                    this.gamePositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetGame();
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.ScorePyramid'", typeIdentifier);
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

            while (this.levelList.Count < LevelsCount) {
                DatasetLevel level = new DatasetLevel(this.levelList.Count);
                level.PropertyChanged += this.level_PropertyChanged;
                this.levelList.Add(level);
            }

            this.calcScores();

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);
        }

        public override void Dispose() {
            base.Dispose();

            foreach(DatasetLevel item in this.levelList) item.PropertyChanged -= this.level_PropertyChanged;
            this.levelList.Clear();

            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Dispose();
        }

        public override void ResetData() {
            base.ResetData();
            foreach (DatasetLevel item in this.levelList) item.Reset();
        }

        public bool TryGetLevel(
            int index,
            out DatasetLevel result) {
            if (index >= 0 &&
                index < this.levelList.Count) {
                result = this.levelList[index];
            } 
            else result = null;
            return result is DatasetLevel;
        }

        private void calcScores() {
            int leftPlayerScore = 0;
            int rightPlayerScore = 0;
            foreach (DatasetLevel item in this.levelList) {
                leftPlayerScore += item.LeftPlayerValue;
                rightPlayerScore += item.RightPlayerValue;
            }
            this.LeftPlayerScore = leftPlayerScore;
            this.RightPlayerScore = rightPlayerScore;
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }

        public void Vinsert_GameIn() {
            this.Vinsert_SetGame();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Game.ToIn();
        }
        public void Vinsert_SetGame() {
            if (this.insertScene is Insert) this.Vinsert_SetGame(this.levelList.ToArray(), this.insertScene.Game);
        }
        public void Vinsert_SetGame(
            DatasetLevel[] levelList,
            Game scene) {
            if (scene is Game) {
                scene.SetPositionX(this.GamePositionX);
                scene.SetPositionY(this.GamePositionY);
                scene.SetLeftPlayerName(this.LeftPlayerName);
                scene.SetRightPlayerName(this.RightPlayerName);
                if (levelList is DatasetLevel[]) {
                    foreach(DatasetLevel item in levelList) {
                        if (item is DatasetLevel) {
                            scene.SetLeftPlayerHits(item.Value, item.LeftPlayerHits);
                            scene.SetRightPlayerHits(item.Value, item.RightPlayerHits);
                        }
                    }
                }
            }
        }
        public void Vinsert_GameOut() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Game.ToOut();
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

        private void level_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_level_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_level_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "LeftPlayerValue" ||
                    e.PropertyName == "RightPlayerValue") this.calcScores();
            }
        }

        #endregion

    }
}
