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

using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.LargeNumbers;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.LargeNumbers {

    public class Data {
        public DatasetContent[] DataList;
        public int SelectedIndex;
    }

    public class DatasetContent : Messaging.Message, INotifyPropertyChanged {

        #region Properties

        private string task = string.Empty;
        public string Task {
            get { return this.task; }
            set {
                if (this.task != value) {
                    if (string.IsNullOrEmpty(value)) this.task = string.Empty;
                    else this.task = value;
                    this.buildToString();
                    this.on_PropertyChanged();
                }
            }
        }

        private string solution = string.Empty;
        public string Solution {
            get { return this.solution; }
            set {
                if (this.solution != value) {
                    if (string.IsNullOrEmpty(value)) this.solution = string.Empty;
                    else this.solution = value.Trim();
                    this.on_PropertyChanged();
                }
            }
        }

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DatasetContent() { }

        public void Clone(
            DatasetContent source) {
            if (source is DatasetContent) {
                this.Task = source.Task;
                this.Solution = source.Solution;
            }
            else {
                this.Task = string.Empty;
                this.Solution = string.Empty;
            }
        }

        private void buildToString() { this.toString = this.Task; }

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

        private int textInsertPositionX = 0;
        public int TextInsertPositionX {
            get { return this.textInsertPositionX; }
            set {
                if (this.textInsertPositionX != value) {
                    this.textInsertPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTextInsert();
                }
            }
        }

        private int textInsertPositionY = 0;
        public int TextInsertPositionY {
            get { return this.textInsertPositionY; }
            set {
                if (this.textInsertPositionY != value) {
                    this.textInsertPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTextInsert();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.TextInsert.Styles textInsertStyle = VentuzScenes.GamePool._Modules.TextInsert.Styles.TwoRows;
        public VentuzScenes.GamePool._Modules.TextInsert.Styles TextInsertStyle {
            get { return this.textInsertStyle; }
            set {
                if (this.textInsertStyle != value) {
                    this.textInsertStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTextInsert();
                }
            }
        }

        private int inputInsertPositionX = 0;
        public int InputInsertPositionX {
            get { return this.inputInsertPositionX; }
            set {
                if (this.inputInsertPositionX != value) {
                    this.inputInsertPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetInputInsert();
                }
            }
        }

        private int inputInsertPositionY = 0;
        public int InputInsertPositionY {
            get { return this.inputInsertPositionY; }
            set {
                if (this.inputInsertPositionY != value) {
                    this.inputInsertPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetInputInsert();
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
                    this.on_PropertyChanged("PlayerInput");
                }
            }
        }

        private string leftPlayerInput = string.Empty;
        [XmlIgnore]
        public string LeftPlayerInput {
            get {
                if (string.IsNullOrEmpty(this.leftPlayerInput)) return "0";
                else return this.leftPlayerInput; 
            }
            set {
                if (this.leftPlayerInput != value) {
                    if (string.IsNullOrEmpty(value)) this.leftPlayerInput = string.Empty;
                    else {
                        long result = 0;
                        long.TryParse(value, out result);
                        this.leftPlayerInput = result.ToString();
                    }
                    this.on_PropertyChanged();
                    this.Vinsert_SetLeftPlayerInput();
                    if (this.BuzzeredPlayer == PlayerSelection.LeftPlayer) this.on_PropertyChanged("PlayerInput");
                }
            }
        }

        private string rightPlayerInput = string.Empty;
        [XmlIgnore]
        public string RightPlayerInput {
            get {
                if (string.IsNullOrEmpty(this.rightPlayerInput)) return "0";
                else return this.rightPlayerInput;
            }
            set {
                if (this.rightPlayerInput != value) {
                    if (string.IsNullOrEmpty(value)) this.rightPlayerInput = "0";
                    else this.rightPlayerInput = value.Trim();
                    this.on_PropertyChanged();
                    this.Vinsert_SetRightPlayerInput();
                    if (this.BuzzeredPlayer == PlayerSelection.RightPlayer) this.on_PropertyChanged("PlayerInput");
                }
            }
        }

        public string PlayerInput {
            get {
                switch (this.BuzzeredPlayer) {
                    case PlayerSelection.LeftPlayer:
                        return this.LeftPlayerInput;
                        break;
                    case PlayerSelection.RightPlayer:
                        return this.RightPlayerInput;
                        break;
                    default:
                        return "0";
                        break;
                }
            }
        }

        private string filename = string.Empty;
        public string Filename {
            get { return this.filename; }
            set { if (this.filename != value) this.Load(value); }
        }

        private List<DatasetContent> dataList = new List<DatasetContent>();
        [Serialization.NotSerialized]
        public DatasetContent[] DataList {
            get { return this.dataList.ToArray(); }
            set {
                this.repressPropertyChanged = true;
                this.tryRemoveAllDatasets();
                if (value is DatasetContent[]) {
                    foreach (DatasetContent item in value) this.tryAddDataset(item, -1);
                }
                this.repressPropertyChanged = false;
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.LargeNumbers'", typeIdentifier);
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
            this.leftPlayerScene.OKButtonPressed += this.leftPlayerScene_OKButtonPressed;
            this.leftPlayerScene.PropertyChanged += this.leftPlayerScene_PropertyChanged;

            this.rightPlayerScene = new Player(syncContext, rightPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.rightPlayerScene.StatusChanged += this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.OKButtonPressed += this.rightPlayerScene_OKButtonPressed;
            this.rightPlayerScene.PropertyChanged += this.rightPlayerScene_PropertyChanged;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);
        }

        public override void Dispose() {
            base.Dispose();
            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;

            this.hostScene.StatusChanged -= this.hostScene_StatusChanged;
            this.hostScene.Dispose();

            this.leftPlayerScene.StatusChanged -= this.leftPlayerScene_StatusChanged;
            this.leftPlayerScene.OKButtonPressed -= this.leftPlayerScene_OKButtonPressed;
            this.leftPlayerScene.Dispose();

            this.rightPlayerScene.StatusChanged -= this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.OKButtonPressed -= this.rightPlayerScene_OKButtonPressed;
            this.rightPlayerScene.Dispose();

        }

        public override void ResetData() {
            base.ResetData();
            this.TaskCounter = 0;
            this.SelectDataset(0);
            this.BuzzeredPlayer = PlayerSelection.NotSelected;
            this.LeftPlayerInput = string.Empty;
            this.RightPlayerInput = string.Empty;
        }

        public virtual void DoBuzzer(
            PlayerSelection buzzeredPlayer) {
            if (this.BuzzeredPlayer == PlayerSelection.NotSelected &&
                this.BuzzeredPlayer != buzzeredPlayer) {
                this.BuzzeredPlayer = buzzeredPlayer;
                //this.Vinsert_InputIn();
                this.Vleftplayer_LockInput();
                this.Vrightplayer_LockInput();
                switch (this.BuzzeredPlayer) {
                    case PlayerSelection.LeftPlayer:
                        if (this.insertScene is Insert) this.insertScene.RightInputToTransparent();
                        break;
                    case PlayerSelection.RightPlayer:
                        if (this.insertScene is Insert) this.insertScene.LeftInputToTransparent();
                        break;
                }
                this.Vhost_SetPlayerInput();
                if (this.insertScene is Insert) this.insertScene.InputBuzzer();
            }
        }

        internal void Resolve() {
            if (this.SelectedDatasetIndex > 0 ||
                !this.SampleIncluded) {
                if (this.SelectedDataset is DatasetContent) {
                    switch (this.BuzzeredPlayer) {
                        case PlayerSelection.LeftPlayer:
                            if (this.PlayerInput == this.SelectedDataset.Solution) this.LeftPlayerScore++;
                            else this.RightPlayerScore++;
                            break;
                        case PlayerSelection.RightPlayer:
                            if (this.PlayerInput == this.SelectedDataset.Solution) this.RightPlayerScore++;
                            else this.LeftPlayerScore++;
                            break;
                    }
                }
            }
        }

        public override void Next() {
            base.Next();
            this.TaskCounter++;
            this.SelectDataset(this.SelectedDatasetIndex + 1);
            this.BuzzeredPlayer = PlayerSelection.NotSelected;
            this.LeftPlayerInput = string.Empty;
            this.RightPlayerInput = string.Empty;
            this.Vleftplayer_ResetInput();
            this.Vrightplayer_ResetInput();
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
        }

        public void AddDataset(
            DatasetContent newDataset,
            int insertIndex) {
            if (this.tryAddDataset(newDataset, insertIndex)) this.on_PropertyChanged("NameList");
            if (this.SelectedDataset == null) this.SelectDataset(0);
        }
        private bool tryAddDataset(
            DatasetContent newDataset,
            int insertIndex) {
            if (newDataset is DatasetContent &&
                !this.dataList.Contains(newDataset)) {
                DatasetContent dataset = new DatasetContent();
                dataset.Clone(newDataset);
                dataset.Error += this.dataset_Error;
                dataset.PropertyChanged += this.dataset_PropertyChanged;
                if (insertIndex >= 0 &&
                    insertIndex < this.DatasetsCount) {
                    this.dataList.Insert(insertIndex, dataset);
                    this.names.Insert(insertIndex, dataset.ToString());
                }
                else {
                    this.dataList.Add(dataset);
                    this.names.Add(dataset.ToString());
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
            }
        }

        public void RemoveAllDatasets() {
            if (this.tryRemoveAllDatasets()) {
                this.SelectDataset(0);
                this.buildNameList();
                this.on_PropertyChanged("NameList");
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
            this.repressPropertyChanged = true;
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
            this.repressPropertyChanged = false;
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
        internal void Vinsert_SetInputInsert(
            Insert scene, 
            DatasetContent dataset,
            PlayerSelection buzzeredPlayer,
            string leftPlayerInput,
            string rightPlayerInput) {
            if (scene is Insert) {
                scene.SetPositionX(this.InputInsertPositionX);
                scene.SetPositionY(this.InputInsertPositionY);
                scene.SetLeftInputValue(leftPlayerInput);
                scene.SetRightInputValue(rightPlayerInput);
                switch (buzzeredPlayer) {
                    case PlayerSelection.LeftPlayer:
                        this.insertScene.LeftInputToIn();
                        this.insertScene.RightInputToTransparent();
                        scene.SetSolutionBuzzer(Insert.BuzzerPositions.Left);
                        break;
                    case PlayerSelection.RightPlayer:
                        this.insertScene.LeftInputToTransparent();
                        this.insertScene.RightInputToIn();
                        scene.SetSolutionBuzzer(Insert.BuzzerPositions.Right);
                        break;
                }
                if (dataset is DatasetContent) scene.SetSolutionValue(dataset.Solution);
            }
        }
        internal void Vinsert_SetInputInsert() { this.Vinsert_SetInputInsert(this.insertScene, this.SelectedDataset, this.BuzzeredPlayer, this.LeftPlayerInput, this.RightPlayerInput); }
        internal void Vinsert_SetLeftPlayerInput(
            Insert scene,
            string text) {
            if (scene is Insert) scene.SetLeftInputValue(text);
        }
        internal void Vinsert_SetLeftPlayerInput() { this.Vinsert_SetLeftPlayerInput(this.insertScene, this.LeftPlayerInput); }
        internal void Vinsert_SetRightPlayerInput(
            Insert scene,
            string text) {
            if (scene is Insert) scene.SetRightInputValue(text);
        }
        internal void Vinsert_SetRightPlayerInput() { this.Vinsert_SetRightPlayerInput(this.insertScene, this.RightPlayerInput); }
        internal void Vinsert_InputIn() {
            this.Vinsert_SetInputInsert();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.InputToIn();
        }
        internal void Vinsert_SolutionIn() {
            this.Vinsert_SetInputInsert();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.SolutionToIn();
        }
        internal void Vinsert_ResolveInput() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) {
                switch (this.BuzzeredPlayer) {
                    case PlayerSelection.LeftPlayer:
                        if (this.PlayerInput == this.SelectedDataset.Solution) {
                            this.insertScene.LeftInputToTrue();
                            this.insertScene.InputTrue();
                        }
                        else {
                            this.insertScene.LeftInputToFalse();
                            this.insertScene.InputFalse();
                        }
                        this.insertScene.RightInputToTransparent();
                        break;
                    case PlayerSelection.RightPlayer:
                        this.insertScene.LeftInputToTransparent();
                        if (this.PlayerInput == this.SelectedDataset.Solution) {
                            this.insertScene.RightInputToTrue();
                            this.insertScene.InputTrue();
                        }
                        else {
                            this.insertScene.RightInputToFalse();
                            this.insertScene.InputFalse();
                        }
                        break;
                    default:
                        this.insertScene.LeftInputToIn();
                        this.insertScene.RightInputToIn();
                        break;
                }
            }
        }
        internal void Vinsert_ContentOut() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.InputToOut();
                this.insertScene.SolutionToOut();
                this.insertScene.TextInsert.ToOut();
            }
        }

        public override void Vinsert_ScoreIn() {
            this.Vinsert_SetScore();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Score.ToIn();
        }
        public override void Vinsert_SetScore() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.Score.SetPositionX(this.ScorePositionX);
                this.insertScene.Score.SetPositionY(this.ScorePositionY);
                this.insertScene.Score.SetFlipPosition(this.FlipPlayers);
                this.insertScene.Score.SetStyle(this.ScoreStyle);
                this.insertScene.Score.SetLeftTopName(this.LeftPlayerName);
                this.insertScene.Score.SetLeftTopScore(this.LeftPlayerScore);
                this.insertScene.Score.SetRightBottomName(this.RightPlayerName);
                this.insertScene.Score.SetRightBottomScore(this.RightPlayerScore);
            }
        }
        public override void Vinsert_ScoreOut() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Score.ToOut();
        }
        internal void Vinsert_TextInsertIn() {
            this.Vinsert_SetTextInsert();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.TextInsert.ToIn();
        }
        internal void Vinsert_SetTextInsert(
            VentuzScenes.GamePool._Modules.TextInsert scene,
            string text) {
            if (scene is VentuzScenes.GamePool._Modules.TextInsert) {
                scene.SetPositionX(this.TextInsertPositionX);
                scene.SetPositionY(this.TextInsertPositionY);
                scene.SetStyle(this.TextInsertStyle);
                scene.SetText(text);
            }
        }
        internal void Vinsert_SetTextInsert() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) this.Vinsert_SetTextInsert(this.insertScene.TextInsert, this.SelectedDataset.Task);
        }
        internal void Vinsert_TextInsertOut() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.TextInsert.ToOut(); }
        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        internal void Vstage_ContentIn() {
            this.Vhost_ContentIn();
            this.Vleftplayer_ResetInput();
            this.Vrightplayer_ResetInput();
            this.Vleftplayer_ContentIn();
            this.Vrightplayer_ContentIn();
        }
        internal void Vstage_ContentOut() {
            this.Vhost_ContentOut();
            this.Vleftplayer_ContentOut();
            this.Vrightplayer_ContentOut();
        }

        public override void Vhost_LoadScene() { this.hostScene.Load(); }
        internal void Vhost_ContentIn() {
            if (this.hostScene is Host &&
                this.SelectedDataset is DatasetContent) {
                this.hostScene.SetTask(this.SelectedDataset.Task);
                this.hostScene.SetSolution(this.SelectedDataset.Solution);
                this.hostScene.ToIn();
            }
        }
        internal void Vhost_SetPlayerInput() {
            if (this.hostScene is Host) {
                switch (this.BuzzeredPlayer) {
                    case PlayerSelection.LeftPlayer:
                        this.hostScene.SetInputValue(this.PlayerInput);
                        this.hostScene.SetInputPosition(Host.InputPositions.Left);
                        this.hostScene.SetInputIn();
                        break;
                    case PlayerSelection.RightPlayer:
                        this.hostScene.SetInputValue(this.PlayerInput);
                        this.hostScene.SetInputPosition(Host.InputPositions.Right);
                        this.hostScene.SetInputIn();
                        break;
                    case PlayerSelection.NotSelected:
                    default:
                        this.hostScene.SetInputOut();
                        break;
                }
            }
        }
        internal void Vhost_ContentOut() { if (this.hostScene is Host) this.hostScene.ToOut(); }
        public override void Vhost_UnloadScene() { this.hostScene.Unload(); }

        public override void Vleftplayer_LoadScene() { this.leftPlayerScene.Load(); }
        internal void Vleftplayer_ContentIn() {
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) {
                this.leftPlayerScene.SetTask(this.SelectedDataset.Task);
                this.leftPlayerScene.ToIn();
            }
        }
        internal void Vleftplayer_ContentOut() { if (this.leftPlayerScene is Player) this.leftPlayerScene.ToOut(); }
        internal void Vleftplayer_ResetInput() { if (this.leftPlayerScene is Player) this.leftPlayerScene.Reset(); }
        internal void Vleftplayer_UnlockInput() { 
            if (this.leftPlayerScene is Player) this.leftPlayerScene.UnlockTouch();
            this.BuzzeredPlayer = PlayerSelection.NotSelected;
        }
        internal void Vleftplayer_ReleaseInput() { if (this.leftPlayerScene is Player) this.leftPlayerScene.UnlockTouch(); }
        internal void Vleftplayer_LockInput() { if (this.leftPlayerScene is Player) this.leftPlayerScene.LockTouch(); }
        public override void Vleftplayer_UnloadScene() { this.leftPlayerScene.Unload(); }

        public override void Vrightplayer_LoadScene() { this.rightPlayerScene.Load(); }
        internal void Vrightplayer_ContentIn() {
            if (this.rightPlayerScene is Player &&
                this.SelectedDataset is DatasetContent) {
                this.rightPlayerScene.SetTask(this.SelectedDataset.Task); 
                this.rightPlayerScene.ToIn();
            }
        }
        internal void Vrightplayer_ContentOut() { if (this.rightPlayerScene is Player) this.rightPlayerScene.ToOut(); }
        internal void Vrightplayer_ResetInput() { if (this.rightPlayerScene is Player) this.rightPlayerScene.Reset(); }
        internal void Vrightplayer_UnlockInput() { 
            if (this.rightPlayerScene is Player) this.rightPlayerScene.UnlockTouch();
            this.BuzzeredPlayer = PlayerSelection.NotSelected;
        }
        internal void Vrightplayer_ReleaseInput() { if (this.rightPlayerScene is Player) this.rightPlayerScene.UnlockTouch(); }
        internal void Vrightplayer_LockInput() { if (this.rightPlayerScene is Player) this.rightPlayerScene.LockTouch(); }
        public override void Vrightplayer_UnloadScene() { this.rightPlayerScene.Unload(); }

        public override void ClearGraphic() {
            base.ClearGraphic();
            this.insertScene.Clear();
            this.hostScene.Clear();
            this.leftPlayerScene.Clear();
            this.rightPlayerScene.Clear();
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
            if (e is PropertyChangedEventArgs &&
                !this.repressPropertyChanged) {
                if (e.PropertyName == "Task") {
                    this.buildNameList();
                    this.on_PropertyChanged("NameList");
                }
            }
            this.Save();
        }

        void leftPlayerScene_OKButtonPressed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftPlayerScene_OKButtonPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftPlayerScene_OKButtonPressed(object content) {
            if (this.BuzzeredPlayer == PlayerSelection.NotSelected) {
                this.DoBuzzer(PlayerSelection.LeftPlayer);
            }
        }

        private void leftPlayerScene_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftPlayerScene_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftPlayerScene_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "Text") this.LeftPlayerInput = this.leftPlayerScene.Text;
            }
        }

        void rightPlayerScene_OKButtonPressed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerScene_OKButtonPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerScene_OKButtonPressed(object content) {
            if (this.BuzzeredPlayer == PlayerSelection.NotSelected) {
                this.DoBuzzer(PlayerSelection.RightPlayer);
            }
        }

        private void rightPlayerScene_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerScene_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerScene_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "Text") this.RightPlayerInput = this.rightPlayerScene.Text;
            }
        }

        #endregion
    }
}
