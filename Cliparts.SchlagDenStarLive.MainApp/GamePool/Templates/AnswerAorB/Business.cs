using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Cliparts.Serialization;

using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.AnswerAorB;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.AnswerAorB {

    public class Data {
        public DatasetContent[] DataList;
        public int SelectedIndex;
    }

    public class DatasetContent : Messaging.Message, INotifyPropertyChanged {

        #region Properties

        private string text = string.Empty;
        public string Text {
            get { return this.text; }
            set {
                if (this.text != value) {
                    if (string.IsNullOrEmpty(value)) this.text = string.Empty;
                    else this.text = Regex.Replace(value, "(?<!\r)\n", "\r\n");
                    this.buildToString();
                    this.on_PropertyChanged();
                    if (string.IsNullOrEmpty(this.HostText)) this.HostText = Text;
                }
            }
        }

        private SolutionStates solution;
        public SolutionStates Solution {
            get { return this.solution; }
            set {
                if (this.solution != value) {
                    this.solution = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string hostText = string.Empty;
        public string HostText {
            get { return this.hostText; }
            set {
                if (this.hostText != value) {
                    if (string.IsNullOrEmpty(value)) this.hostText = string.Empty;
                    else this.hostText = Regex.Replace(value, "(?<!\r)\n", "\r\n");
                    this.on_PropertyChanged();
                }
            }
        }

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DatasetContent() {
            this.Text = "?";
            this.HostText = string.Empty;
        }

        private void buildToString() { this.toString = this.Text; }

        public override string ToString() { return this.toString; }

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

    public class Business : _Base.Score.Business {

        #region Properties

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

        private int taskCounterPositionX = 0;
        public int TaskCounterPositionX {
            get { return this.taskCounterPositionX; }
            set {
                if (this.taskCounterPositionX != value) {
                    this.taskCounterPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTaskCounter();
                }
            }
        }

        private int taskCounterPositionY = 0;
        public int TaskCounterPositionY {
            get { return this.taskCounterPositionY; }
            set {
                if (this.taskCounterPositionY != value) {
                    this.taskCounterPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTaskCounter();
                }
            }
        }

        private int taskCounterSize = 13;
        public int TaskCounterSize {
            get { return this.taskCounterSize; }
            set {
                if (this.taskCounterSize != value) {
                    this.taskCounterSize = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTaskCounter();
                }
            }
        }

        private string answerA = "Answer A";
        public string AnswerA {
            get { return this.answerA; }
            set {
                if (string.IsNullOrEmpty(value)) this.answerA = string.Empty;
                else this.answerA = value;
                this.on_PropertyChanged();
            }
        }

        private string answerB = "Answer B";
        public string AnswerB {
            get { return this.answerB; }
            set {
                if (string.IsNullOrEmpty(value)) this.answerB = string.Empty;
                else this.answerB = value;
                this.on_PropertyChanged();
            }
        }

        private string filename = string.Empty;
        public string Filename {
            get { return this.filename; }
            set { if (this.filename != value) this.Load(value); }
        }

        private List<DatasetContent> dataList = new List<DatasetContent>();
        [XmlIgnore]
        public DatasetContent[] DataList {
            get { return this.dataList.ToArray(); }
            set {
                this.tryRemoveAllDatasets();
                if (value is DatasetContent[]) {
                    foreach (DatasetContent item in value) this.tryAddDataset(item, -1);
                }
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                this.SelectDataset(0);
            }
        }

        private List<string> names = new List<string>();
        public string[] NameList { get { return this.names.ToArray(); } }

        public int DatasetsCount { get { return this.dataList.Count; } }

        public DatasetContent SelectedDataset { get; private set; }

        public int SelectedDatasetIndex { get; private set; }

        private int taskCounter = 0;
        public int TaskCounter {
            get { return this.taskCounter; }
            set {
                if (this.taskCounter != value) {
                    if (value < 0) value = 0;
                    if (!this.SampleIncluded &&
                        value < 1) value = 1;
                    this.taskCounter = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool sampleIncluded = true;
        public bool SampleIncluded {
            get { return this.sampleIncluded; }
            set {
                if (this.sampleIncluded != value) {
                    this.sampleIncluded = value;
                    this.on_PropertyChanged();
                    // TaskCounter validieren
                    int id = this.TaskCounter;
                    this.TaskCounter = -1;
                    this.TaskCounter = id;
                }
            }
        }

        private SelectionStates leftPlayerInput;
        [XmlIgnore]
        public SelectionStates LeftPlayerInput {
            get { return this.leftPlayerInput; }
            set {
                if (this.leftPlayerInput != value) {
                    this.leftPlayerInput = value;
                    if (this.BuzzerMode && value != SelectionStates.NotAvailable) this.doBuzzer(PlayerSelection.LeftPlayer);
                    this.on_PropertyChanged();
                    this.rateLeftPlayerInput();
                }
            }
        }
        private bool leftPlayerCorrectInput = false;
        public bool LeftPlayerCorrectInput {
            get { return this.leftPlayerCorrectInput; }
            private set {
                if (this.leftPlayerCorrectInput != value) {
                    this.leftPlayerCorrectInput = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private SelectionStates rightPlayerInput;
        [XmlIgnore]
        public SelectionStates RightPlayerInput {
            get { return this.rightPlayerInput; }
            set {
                if (this.rightPlayerInput != value) {
                    this.rightPlayerInput = value;
                    if (this.BuzzerMode && value != SelectionStates.NotAvailable) this.doBuzzer(PlayerSelection.RightPlayer);
                    this.on_PropertyChanged();
                    this.rateRightPlayerInput();
                }
            }
        }
        private bool rightPlayerCorrectInput = false;
        public bool RightPlayerCorrectInput {
            get { return this.rightPlayerCorrectInput; }
            private set {
                if (this.rightPlayerCorrectInput != value) {
                    this.rightPlayerCorrectInput = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool buzzerMode = false;
        [XmlIgnore]
        public bool BuzzerMode {
            get { return this.buzzerMode; }
            set {
                if (this.buzzerMode != value) {
                    this.buzzerMode = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.Timeout.Duration timeoutDuration = VentuzScenes.GamePool._Modules.Timeout.Duration.FiveSeconds;
        public VentuzScenes.GamePool._Modules.Timeout.Duration TimeoutDuration {
            get { return this.timeoutDuration; }
            set {
                if (this.timeoutDuration != value) {
                    this.timeoutDuration = value;
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

        private Host hostScene;
        public override VRemote4.HandlerSi.Scene.States HostSceneStatus {
            get {
                if (this.hostScene is Host) return this.hostScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private Player leftPlayerScene;
        public override VRemote4.HandlerSi.Scene.States LeftPlayerSceneStatus {
            get {
                if (this.leftPlayerScene is Player) return this.leftPlayerScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private Player rightPlayerScene;
        public override VRemote4.HandlerSi.Scene.States RightPlayerSceneStatus {
            get {
                if (this.rightPlayerScene is Player) return this.rightPlayerScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private bool repressPropertyChanged = false;

        #endregion


        #region Funktionen

        public Business() { }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.AnswerAorB'", typeIdentifier);
        }

        public override void New() {
            base.New();
            this.Filename = string.Empty;
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

            //wenn die Datasets einen Synccontext benötigen, dann wird er hierdurch zugeordnet
            this.DataList = this.dataList.ToArray();

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;

            this.hostScene = new Host(syncContext, hostMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.hostScene.StatusChanged += this.hostScene_StatusChanged;

            this.leftPlayerScene = new Player(syncContext, leftPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.leftPlayerScene.StatusChanged += this.leftPlayerScene_StatusChanged;
            this.leftPlayerScene.PropertyChanged += this.leftPlayerScene_PropertyChanged;

            this.rightPlayerScene = new Player(syncContext, rightPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.rightPlayerScene.StatusChanged += this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.PropertyChanged += this.rightPlayerScene_PropertyChanged;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);
        }

        public override void Dispose() {
            base.Dispose();

            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Dispose();

            this.hostScene.StatusChanged -= this.hostScene_StatusChanged;
            this.hostScene.Dispose();

            this.leftPlayerScene.StatusChanged -= this.leftPlayerScene_StatusChanged;
            this.leftPlayerScene.PropertyChanged -= this.leftPlayerScene_PropertyChanged;
            this.leftPlayerScene.Dispose();

            this.rightPlayerScene.StatusChanged -= this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.PropertyChanged -= this.rightPlayerScene_PropertyChanged;
            this.rightPlayerScene.Dispose();
        }

        public override void ResetData() {
            base.ResetData();
            this.TaskCounter = 0;
            this.SelectDataset(0);
            this.LeftPlayerInput = SelectionStates.NotAvailable;
            this.RightPlayerInput = SelectionStates.NotAvailable;
            this.BuzzerMode = false;
        }

        public void Resolve() {
            if (this.TaskCounter > 0) {
                if (this.LeftPlayerCorrectInput) this.LeftPlayerScore++;
                if (this.RightPlayerCorrectInput) this.RightPlayerScore++;
            }
        }

        public override void Next() {
            base.Next();
            this.TaskCounter++;
            this.SelectDataset(this.SelectedDatasetIndex + 1);
            this.LeftPlayerInput = SelectionStates.NotAvailable;
            this.RightPlayerInput = SelectionStates.NotAvailable;
        }

        private void rateLeftPlayerInput() {
            if (this.SelectedDataset is DatasetContent) {
                switch (this.SelectedDataset.Solution) {
                    case SolutionStates.AnswerA:
                        this.LeftPlayerCorrectInput = this.LeftPlayerInput == SelectionStates.AnswerA;
                        break;
                    case SolutionStates.AnswerB:
                        this.LeftPlayerCorrectInput = this.LeftPlayerInput == SelectionStates.AnswerB;
                        break;
                    default:
                        this.LeftPlayerCorrectInput = false;
                        break;
                }
            }
            else this.LeftPlayerCorrectInput = false;
        }

        private void rateRightPlayerInput() {
            if (this.SelectedDataset is DatasetContent) {
                switch (this.SelectedDataset.Solution) {
                    case SolutionStates.AnswerA:
                        this.RightPlayerCorrectInput = this.RightPlayerInput == SelectionStates.AnswerA;
                        break;
                    case SolutionStates.AnswerB:
                        this.RightPlayerCorrectInput = this.RightPlayerInput == SelectionStates.AnswerB;
                        break;
                    default:
                        this.RightPlayerCorrectInput = false;
                        break;
                }
            }
            else this.RightPlayerCorrectInput = false;
        }

        private void doBuzzer(
            PlayerSelection buzzeredPlayer) {
            switch (buzzeredPlayer) {
                case PlayerSelection.LeftPlayer:
                    if (this.RightPlayerInput == SelectionStates.NotAvailable) {
                        this.Vrightplayer_ResetInput();
                    }
                    else {
                        this.Vleftplayer_ResetInput();
                        this.LeftPlayerInput = SelectionStates.NotAvailable;
                    }
                    break;
                case PlayerSelection.RightPlayer:
                    if (this.LeftPlayerInput == SelectionStates.NotAvailable) {
                        this.Vleftplayer_ResetInput();
                    }
                    else {
                        this.Vrightplayer_ResetInput();
                        this.RightPlayerInput = SelectionStates.NotAvailable;
                    }
                    break;
            }
            this.Vhost_Set();
        }

        public DatasetContent GetDataset(
            int index) {
            if (index >= 0 &&
                index < this.dataList.Count) return this.dataList[index];
            else return null;
        }

        public int GetDatasetIndex(
            DatasetContent dataset) {
            int index = -1;
            int datasetIndex = 0;
            foreach (DatasetContent item in this.dataList) {
                if (item == dataset) {
                    index = datasetIndex;
                    break;
                }
                datasetIndex++;
            }
            return index;
        }

        public void SelectDataset(
            int index) {
            if (index < 0) index = 0;
            if (index >= this.dataList.Count) index = this.dataList.Count - 1;
            this.SelectedDatasetIndex = index;
            this.on_PropertyChanged("SelectedDatasetIndex");
            this.SelectedDataset = this.GetDataset(index);
            this.on_PropertyChanged("SelectedDataset");
            this.rateLeftPlayerInput();
            this.rateRightPlayerInput();
        }

        public void AddDataset(
            DatasetContent newDataset,
            int insertIndex) {
            if (this.tryAddDataset(newDataset, insertIndex)) this.on_PropertyChanged("NameList");
            if (this.SelectedDataset == null) this.SelectDataset(0);
            this.Save();
        }
        private bool tryAddDataset(
            DatasetContent newDataset,
            int insertIndex) {
            if (newDataset is DatasetContent &&
                !this.dataList.Contains(newDataset)) {
                newDataset.Error += this.dataset_Error;
                newDataset.PropertyChanged += this.dataset_PropertyChanged;
                if (insertIndex >= 0 &&
                    insertIndex < this.DatasetsCount) {
                    this.dataList.Insert(insertIndex, newDataset);
                    this.names.Insert(insertIndex, newDataset.ToString());
                }
                else {
                    this.dataList.Add(newDataset);
                    this.names.Add(newDataset.ToString());
                }
                return true;
            }
            else return false;
        }

        public bool TryMoveDatasetUp(
            int index) {
            if (index > 0 &&
                index < this.DatasetsCount) {
                DatasetContent dataset = this.GetDataset(index);
                this.dataList.RemoveAt(index);
                this.dataList.Insert(index - 1, dataset);
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                this.Save();
                return true;
            }
            else return false;
        }
        public bool TryMoveDatasetDown(
            int index) {
            if (index >= 0 &&
                index < this.DatasetsCount - 1) {
                DatasetContent dataset = this.GetDataset(index);
                this.dataList.RemoveAt(index);
                this.dataList.Insert(index + 1, dataset);
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                this.Save();
                return true;
            }
            else return false;
        }

        public void ResortAllDatasets() {
            if (this.DatasetsCount > 1) {
                List<DatasetContent> dataList = new List<DatasetContent>();
                if (this.SampleIncluded) {
                    dataList.Add(this.dataList[0]);
                    for (int i = this.dataList.Count - 1; i > 0; i--) dataList.Add(this.dataList[i]);
                }
                else {
                    for (int i = this.dataList.Count - 1; i >= 0; i--) dataList.Add(this.dataList[i]);
                }
                this.dataList.Clear();
                foreach (DatasetContent item in dataList) this.dataList.Add(item);
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                this.Save();
            }
        }

        public void RemoveAllDatasets() {
            if (this.tryRemoveAllDatasets()) {
                this.SelectDataset(0);
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                this.Save();
            }
        }
        private bool tryRemoveAllDatasets() {
            bool datasetRemoved = false;
            DatasetContent[] datasetList = this.dataList.ToArray();
            foreach (DatasetContent item in datasetList) datasetRemoved = this.removeDataset(item) | datasetRemoved;
            return datasetRemoved;
        }
        public bool TryRemoveDataset(
            int index) {
            if (this.removeDataset(this.GetDataset(index))) {
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                if (index <= this.SelectedDatasetIndex) this.SelectDataset(this.SelectedDatasetIndex);
                this.Save();
                return true;
            }
            else return false;
        }
        private bool removeDataset(
            DatasetContent dataset) {
            if (dataset is DatasetContent &&
                this.dataList.Contains(dataset)) {
                dataset.Error -= this.dataset_Error;
                dataset.PropertyChanged -= this.dataset_PropertyChanged;
                this.dataList.Remove(dataset);
                return true;
            }
            else return false;
        }

        private void buildNameList() {
            this.names.Clear();
            this.repressPropertyChanged = true;
            foreach (DatasetContent item in this.dataList) {
                this.names.Add(item.ToString());
            }
            this.repressPropertyChanged = false;
        }

        public void Load(
            string filename) {
            string subSender = "Load";
            if (File.Exists(filename)) {
                try {
                    XmlSerializer serializer = new XmlSerializer(typeof(Data));
                    Data data;
                    using (StreamReader reader = new StreamReader(filename)) data = (Data)serializer.Deserialize(reader);
                    this.DataList = data.DataList;
                    this.SelectDataset(data.SelectedIndex);
                    this.filename = filename;
                    this.on_PropertyChanged("Filename");
                }
                catch (Exception exc) {
                    // Fehler weitergeben
                    this.on_Error(subSender, exc.Message);
                }
            }
            else {
                // Fehler weitergeben
                this.on_Error(subSender, "Datei nicht vorhanden - '" + filename + "'");
            }
        }
        public void Save() { if (File.Exists(this.Filename)) this.SaveAs(this.Filename); }
        public void SaveAs(
            string filename) {
            string subSender = "SaveAs";
            try {
                // Dokument speichern
                Data data = new Data();
                data.DataList = this.DataList;
                data.SelectedIndex = this.SelectedDatasetIndex;
                XmlSerializer serializer = new XmlSerializer(typeof(Data));
                using (StreamWriter writer = new StreamWriter(filename)) serializer.Serialize(writer, data);
                this.filename = filename;
                this.on_PropertyChanged("Filename");
            }
            catch (Exception exc) {
                // Fehler weitergeben
                this.on_Error(subSender, exc.Message);
            }
        }


        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }

        public override void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score, this.LeftPlayerScore, this.RightPlayerScore); }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }

        public void Vinsert_StartTimeout() {
            if (!this.BuzzerMode &&
                this.insertScene is VRemote4.HandlerSi.Scene &&
                this.insertScene.Timeout is VentuzScenes.GamePool._Modules.Timeout) this.insertScene.Timeout.StartCenter(this.TimeoutDuration, false);
        }
        public void Vinsert_StopTimeout() {
            if (this.insertScene is VRemote4.HandlerSi.Scene &&
                this.insertScene.Timeout is VentuzScenes.GamePool._Modules.Timeout) this.insertScene.Timeout.Clear();
        }

        public void Vinsert_GameIn() {
            this.Vinsert_SetGame();
            if (this.insertScene is Insert &&
                this.insertScene.Game is Game) {
                this.insertScene.Game.ToIn();
            }
            if (this.TaskCounter > 0 &&
                this.TaskCounter <= this.TaskCounterSize) this.Vinsert_TaskCounterIn();
        }
        public void Vinsert_SetGame() { if (this.insertScene is Insert) this.Vinsert_SetGame(this.insertScene.Game, this.SelectedDataset); }
        public void Vinsert_SetGame(
            Game scene,
            DatasetContent dataset) {
            if (scene is Game) {
                scene.SetPositionX(this.GamePositionX);
                scene.SetPositionY(this.GamePositionY);
                scene.SetLeftPlayerName(this.LeftPlayerName);
                scene.SetLeftPlayerSelection(SelectionStates.NotAvailable);
                scene.SetRightPlayerName(this.RightPlayerName);
                scene.SetRightPlayerSelection(SelectionStates.NotAvailable);
                scene.SetSolution(SelectionStates.NotAvailable);
                if (dataset is DatasetContent) scene.SetText(dataset.Text);
                else scene.SetText(string.Empty);
            }
        }
        public void Vinsert_GameInputIn() {
            this.Vinsert_SetGameInput();
            if (this.insertScene is Insert) this.Vinsert_GameInputIn(this.insertScene.Game);
        }
        public void Vinsert_GameInputIn(
            Game scene) {
            if (scene is Game) scene.SelectionToIn();
        }
        internal void Vinsert_SetGameInput() { if (this.insertScene is Insert) this.Vinsert_SetGameInput(this.insertScene.Game, this.LeftPlayerInput, this.RightPlayerInput); }
        internal void Vinsert_SetGameInput(
            Game scene,
            SelectionStates leftPlayerSelection,
            SelectionStates rightPlayerSelection) {
            if (scene is Game) {
                scene.SetLeftPlayerSelection(leftPlayerSelection);
                scene.SetRightPlayerSelection(rightPlayerSelection);
            }
        }
        public void Vinsert_SetGameSolution() { if (this.insertScene is Insert) this.Vinsert_SetGameSolution(this.insertScene.Game, this.SelectedDataset); }
        public void Vinsert_SetGameSolution(
            Game scene,
            DatasetContent dataset) {
            if (scene is Game) {
                if (dataset is DatasetContent) {
                    switch (dataset.Solution) {
                        case SolutionStates.AnswerA:
                            scene.SetSolution(SelectionStates.AnswerA);
                            break;
                        case SolutionStates.AnswerB:
                            scene.SetSolution(SelectionStates.AnswerB);
                            break;
                    }
                }
                else scene.SetSolution(SelectionStates.NotAvailable);
            }
        }
        public void Vinsert_GameOut() { 
            if (this.insertScene is Insert && this.insertScene.Game is Game) this.insertScene.Game.ToOut();
            this.Vinsert_TaskCounterOut();
        }

        public void Vinsert_TaskCounterIn() {
            this.Vinsert_SetTaskCounter();
            if (this.insertScene is Insert &&
                this.insertScene.TaskCounter is VentuzScenes.GamePool._Modules.TaskCounter) this.insertScene.TaskCounter.ToIn();
        }
        public void Vinsert_SetTaskCounter() { if (this.insertScene is Insert) this.Vinsert_SetTaskCounter(this.insertScene.TaskCounter, this.TaskCounter); }
        public void Vinsert_SetTaskCounter(
            VentuzScenes.GamePool._Modules.TaskCounter scene,
            int counter) {
            if (scene is VentuzScenes.GamePool._Modules.TaskCounter) {
                scene.SetPositionX(this.TaskCounterPositionX);
                scene.SetPositionY(this.TaskCounterPositionY);
                scene.SetStyle(VentuzScenes.GamePool._Modules.TaskCounter.Styles.Numeric);
                scene.SetSize(this.TaskCounterSize);
                scene.SetCounter(counter);
            }
        }
        public void Vinsert_TaskCounterOut() {
            if (this.insertScene is Insert &&
                this.insertScene.TaskCounter is VentuzScenes.GamePool._Modules.TaskCounter) this.insertScene.TaskCounter.ToOut();
        }

        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void Vstage_Init() {
            base.Vstage_Init();
            //if (this.hostMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostMasterScene.LiveVideoIn();
            //if (this.leftPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerMasterScene.LiveVideoIn();
            //if (this.rightPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerMasterScene.LiveVideoIn();
        }

        public override void Vhost_LoadScene() { this.hostScene.Load(); }
        public void Vhost_In() {
            this.Vhost_Set();
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.ToIn();
        }
        public void Vhost_Set() { if (this.hostScene is VRemote4.HandlerSi.Scene && this.SelectedDataset is DatasetContent) this.Vhost_Set(this.hostScene, this.SelectedDataset, this.TaskCounter, this.LeftPlayerInput, this.RightPlayerInput); }
        public void Vhost_Set(
            Host scene,
            DatasetContent dataset,
            int counter,
            SelectionStates leftPlayerSelection,
            SelectionStates rightPlayerSelection) {
            if (scene is Host) {
                scene.SetAnswerA(this.AnswerA);
                scene.SetAnswerB(this.AnswerB);
                scene.SetLeftPlayerName(this.LeftPlayerName);
                scene.SetLeftPlayerSelection(leftPlayerSelection);
                scene.SetRightPlayerName(this.RightPlayerName);
                scene.SetRightPlayerSelection(rightPlayerSelection);
                if (dataset is DatasetContent) {
                    string hostText = dataset.HostText;
                    if (counter == 0) hostText = string.Format("Beispiel: {0}", hostText);
                    else if (counter > 0 && counter <= this.TaskCounterSize) hostText = string.Format("{0}/{1}: {2}", counter.ToString(), this.TaskCounterSize.ToString(), hostText);
                    scene.SetText(hostText);
                    scene.SetSolution(dataset.Solution);
                }
            }
        }
        public void Vhost_Out() { if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.ToOut(); }
        public override void Vhost_UnloadScene() { this.hostScene.Unload(); }

        public override void Vleftplayer_LoadScene() { this.leftPlayerScene.Load(); }
        internal void Vleftplayer_ContentIn() {
            this.Vplayer_SetData(this.leftPlayerScene, this.SelectedDataset);
            this.Vplayer_In(this.leftPlayerScene);
        }
        internal void Vleftplayer_ContentOut() { this.Vplayer_Out(this.leftPlayerScene); }
        internal void Vleftplayer_ResetInput() { this.Vplayer_ResetInput(this.leftPlayerScene); }
        internal void Vleftplayer_SetInput() { this.Vplayer_SetInput(this.leftPlayerScene, this.LeftPlayerInput); }
        internal void Vleftplayer_ReleaseInput() { this.Vplayer_ReleaseInput(this.leftPlayerScene); }
        internal void Vleftplayer_LockInput() { this.Vplayer_LockInput(this.leftPlayerScene); }
        public override void Vleftplayer_UnloadScene() { this.leftPlayerScene.Unload(); }

        public override void Vrightplayer_LoadScene() { this.rightPlayerScene.Load(); }
        internal void Vrightplayer_ContentIn() {
            this.Vplayer_SetData(this.rightPlayerScene, this.SelectedDataset);
            this.Vplayer_In(this.rightPlayerScene);
        }
        internal void Vrightplayer_ContentOut() { this.Vplayer_Out(this.rightPlayerScene); }
        internal void Vrightplayer_ResetInput() { this.Vplayer_ResetInput(this.rightPlayerScene); }
        internal void Vrightplayer_SetInput() { this.Vplayer_SetInput(this.rightPlayerScene, this.RightPlayerInput); }
        internal void Vrightplayer_ReleaseInput() { this.Vplayer_ReleaseInput(this.rightPlayerScene); }
        internal void Vrightplayer_LockInput() { this.Vplayer_LockInput(this.rightPlayerScene); }
        public override void Vrightplayer_UnloadScene() { this.rightPlayerScene.Unload(); }

        public void Vplayer_In(Player scene) { if (scene is Player) scene.ToIn(); }
        public void Vplayer_SetData(
            Player scene,
            DatasetContent dataset) {
            if (scene is Player &&
                dataset is DatasetContent) {
                scene.SetAnswerA(this.AnswerA);
                scene.SetAnswerB(this.AnswerB);
                scene.SetText(dataset.Text);
            }
        }
        public void Vplayer_ResetInput(Player scene) { if (scene is Player) scene.ResetSelection(); }
        public void Vplayer_SetInput(
            Player scene,
            SelectionStates selection) {
            if (scene is Player) {
                switch (selection) {
                    case SelectionStates.NotAvailable:
                        scene.ResetSelection(); 
                        break;
                    case SelectionStates.AnswerA:
                        scene.SetSelectionAnswerA();
                        break;
                    case SelectionStates.AnswerB:
                        scene.SetSelectionAnswerB();
                        break;
                }
            }
        }
        public void Vplayer_ReleaseInput(Player scene) { if (scene is Player) scene.EnableTouch(); }
        public void Vplayer_LockInput(Player scene) { if (scene is Player) scene.DisableTouch(); }
        public void Vplayer_Out(Player scene) { if (scene is Player) scene.ToOut(); }

        public override void ClearGraphic() {
            base.ClearGraphic();
            this.insertScene.Clear();
                                                                                                       }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming

        void dataset_Error(object sender, Messaging.ErrorEventArgs e) { this.on_Error(sender, e); }

        void dataset_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_dataset_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_dataset_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "Text") {
                    this.buildNameList();
                    this.on_PropertyChanged("NameList");
                }
                else if (e.PropertyName == "Solution") {
                    this.rateLeftPlayerInput();
                    this.rateRightPlayerInput();
                }
            }
            this.Save(); 
        }

        void leftPlayerScene_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftPlayerScene_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftPlayerScene_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "SelectionIndex") {
                    int selectionIndex = this.leftPlayerScene.SelectionIndex;
                    if (selectionIndex == 1) {
                        this.LeftPlayerInput = SelectionStates.AnswerA;
                        if (!this.BuzzerMode) this.Vhost_Set();
                    }
                    else if (selectionIndex == 2) {
                        this.LeftPlayerInput = SelectionStates.AnswerB;
                        if (!this.BuzzerMode) this.Vhost_Set();
                    }
                }
            }
        }

        void rightPlayerScene_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerScene_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerScene_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "SelectionIndex") {
                    int selectionIndex = this.rightPlayerScene.SelectionIndex;
                    if (selectionIndex == 1) {
                        this.RightPlayerInput = SelectionStates.AnswerA;
                        if (!this.BuzzerMode) this.Vhost_Set();
                    }
                    else if (selectionIndex == 2) {
                        this.RightPlayerInput = SelectionStates.AnswerB;
                        if (!this.BuzzerMode) this.Vhost_Set();
                    }
                }
            }
        }

        #endregion

    }
}
