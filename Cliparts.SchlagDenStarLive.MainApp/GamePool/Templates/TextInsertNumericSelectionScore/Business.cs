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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TextInsertNumericSelectionScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TextInsertNumericSelectionScore {

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
                    if (value == null) value = string.Empty;
                    else this.text = value;
                    this.buildToString();
                    this.on_PropertyChanged();
                }
            }
        }

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DatasetContent() { }
        public DatasetContent(
            string text) {
            if (string.IsNullOrEmpty(text)) this.Text = "?";
            else this.Text = text;
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

        private int textInsertPositionX = 0;
        public int TextInsertPositionX {
            get { return this.textInsertPositionX; }
            set {
                if (this.textInsertPositionX != value) {
                    this.textInsertPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetContent();
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
                    this.Vinsert_SetContent();
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
                    this.Vinsert_SetContent();
                }
            }
        }

        private int counterPositionX = 0;
        public int CounterPositionX {
            get { return this.counterPositionX; }
            set {
                if (this.counterPositionX != value) {
                    this.counterPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounter();
                }
            }
        }

        private int counterPositionY = 0;
        public int CounterPositionY {
            get { return this.counterPositionY; }
            set {
                if (this.counterPositionY != value) {
                    this.counterPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounter();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.Counter.SizeElements counterStyle = VentuzScenes.GamePool._Modules.Counter.SizeElements.TwoDigitsLarge;
        public VentuzScenes.GamePool._Modules.Counter.SizeElements CounterSize {
            get { return this.counterStyle; }
            set {
                if (this.counterStyle != value) {
                    this.counterStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounter();
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

        private int taskCounterSize = 11;
        public int TaskCounterSize {
            get { return this.taskCounterSize; }
            set {
                if (this.taskCounterSize != value) {
                    if (value < 0) value = 0;
                    this.taskCounterSize = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTaskCounter();
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

        private bool leftPlayerHasValue = false;
        public bool LeftPlayerHasValue {
            get { return this.leftPlayerHasValue; }
            set {
                if (this.leftPlayerHasValue != value) {
                    this.leftPlayerHasValue = value;
                    this.on_PropertyChanged();
                    this.calcOffsets();
                }
                if (value) this.LeftPlayerValueMatch = this.RightPlayerHasValue && this.LeftPlayerValue == this.RightPlayerValue;
                else {
                    this.LeftPlayerValueMatch = false;
                    this.RightPlayerValueMatch = false;
                }
            }
        }

        private bool leftPlayerValueMatch = false;
        public bool LeftPlayerValueMatch {
            get { return this.leftPlayerValueMatch; }
            private set {
                if (this.leftPlayerValueMatch != value) {
                    this.leftPlayerValueMatch = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int leftPlayerValue = 0;
        public int LeftPlayerValue {
            get { return this.leftPlayerValue; }
            set {
                if (this.leftPlayerValue != value) {
                    this.leftPlayerValue = value;
                    this.on_PropertyChanged();
                    this.calcOffsets();
                }
            }
        }

        private int leftPlayerOffset = 0;
        public int LeftPlayerOffset {
            get { return this.leftPlayerOffset; }
            private set {
                if (this.leftPlayerOffset != value) {
                    this.leftPlayerOffset = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool leftPlayerIsCloser = false;
        public bool LeftPlayerIsCloser {
            get { return this.leftPlayerIsCloser; }
            private set {
                if (this.leftPlayerIsCloser != value) {
                    this.leftPlayerIsCloser = value;
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

        private bool rightPlayerHasValue = false;
        public bool RightPlayerHasValue {
            get { return this.rightPlayerHasValue; }
            set {
                if (this.rightPlayerHasValue != value) {
                    this.rightPlayerHasValue = value;
                    this.on_PropertyChanged();
                    this.calcOffsets();
                }
                if (value) this.RightPlayerValueMatch = this.LeftPlayerHasValue && this.LeftPlayerValue == this.RightPlayerValue;
                else {
                    this.LeftPlayerValueMatch = false;
                    this.RightPlayerValueMatch = false;
                }
            }
        }

        private bool rightPlayerValueMatch = false;
        public bool RightPlayerValueMatch {
            get { return this.rightPlayerValueMatch; }
            private set {
                if (this.rightPlayerValueMatch != value) {
                    this.rightPlayerValueMatch = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerValue = 0;
        public int RightPlayerValue {
            get { return this.rightPlayerValue; }
            set {
                if (this.rightPlayerValue != value) {
                    this.rightPlayerValue = value;
                    this.on_PropertyChanged();
                    this.calcOffsets();
                }
            }
        }

        private int rightPlayerOffset = 0;
        public int RightPlayerOffset {
            get { return this.rightPlayerOffset; }
            private set {
                if (this.rightPlayerOffset != value) {
                    this.rightPlayerOffset = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool rightPlayerIsCloser = false;
        public bool RightPlayerIsCloser {
            get { return this.rightPlayerIsCloser; }
            private set {
                if (this.rightPlayerIsCloser != value) {
                    this.rightPlayerIsCloser = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string filename = string.Empty;
        public string Filename {
            get { return this.filename; }
            set { if (this.filename != value) this.Load(value); }
        }

        private List<DatasetContent> dataList = new List<DatasetContent>();
        [Cliparts.Serialization.NotSerialized]
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

        private int counter = 0;
        public int Counter {
            get { return this.counter; }
            set {
                if (this.counter != value) {
                    if (value < 0) value = 0;
                    this.counter = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int solution = 0;
        public int Solution {
            get { return this.solution; }
            set {
                if (this.solution != value) {
                    if (value < 0) this.solution = 0;
                    else if (value > 10) this.solution = 10;
                    else this.solution = value;
                    this.on_PropertyChanged();
                    this.calcOffsets();
                }
            }
        }

        private System.Timers.Timer solutionTimer;

        private Insert insertScene;
        public override VRemote4.HandlerSi.Scene.States InsertSceneStatus {
            get {
                if (this.insertScene is Insert) return this.insertScene.Status;
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.TextInsertNumericSelectionScore'", typeIdentifier);
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

            this.LeftPlayerClient = new PlayerClient(syncContext);
            this.LeftPlayerClient.PropertyChanged += this.leftPlayerClient_PropertyChanged;
            this.LeftPlayerClient.PlayerValueReceived += this.leftPlayerClient_PlayerValueReceived;
            this.LeftPlayerClient.Hostname = this.leftPlayerClientHostname;

            this.RightPlayerClient = new PlayerClient(syncContext);
            this.RightPlayerClient.PropertyChanged += this.rightPlayerClient_PropertyChanged;
            this.RightPlayerClient.PlayerValueReceived += this.rightPlayerClient_PlayerValueReceived;
            this.RightPlayerClient.Hostname = this.rightPlayerClientHostname;

            this.solutionTimer = new System.Timers.Timer(500);
            this.solutionTimer.Elapsed += this.solutionTimer_Elapsed;
            this.solutionTimer.AutoReset = false;

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;

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
        }

        public override void ResetData() {
            base.ResetData();
            this.Solution = 0;
            this.LeftPlayerHasValue = false;
            this.LeftPlayerValue = 0;
            this.LeftPlayerClient.SetIdle();
            this.RightPlayerHasValue = false;
            this.RightPlayerValue = 0;
            this.RightPlayerClient.SetIdle();
            this.TaskCounter = 0;
            this.SelectDataset(0);
        }

        internal void Resolve() {
            if (this.TaskCounter > 0) {
                if (this.LeftPlayerIsCloser) this.LeftPlayerScore++;
                if (this.RightPlayerIsCloser) this.RightPlayerScore++;
            }
        }

        public override void Next() {
            base.Next();
            this.Solution = 0;
            this.LeftPlayerHasValue = false;
            this.LeftPlayerValue = 0;
            this.LeftPlayerClient.SetIdle();
            this.RightPlayerHasValue = false;
            this.RightPlayerValue = 0;
            this.RightPlayerClient.SetIdle();
            this.TaskCounter++;
            this.SelectDataset(this.SelectedDatasetIndex + 1);
        }

        internal void ConnectClients() {
            this.LeftPlayerClient.Connect();
            this.RightPlayerClient.Connect();
        }
        public void UnlockPlayerClients() {
            this.LeftPlayerClient.SetUnlocked();
            this.RightPlayerClient.SetUnlocked();
        }
        public void ShowInput() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.PlayJingleInput();
                this.LeftPlayerClient.SetInputValue(this.LeftPlayerValue);
            this.RightPlayerClient.SetInputValue(this.RightPlayerValue);
        }
        internal void DisconnectClients() {
            this.LeftPlayerClient.Disconnect();
            this.RightPlayerClient.Disconnect();
        }

        private void calcOffsets() {
            if (this.SelectedDataset is DatasetContent &&
                this.LeftPlayerHasValue) {
                this.LeftPlayerOffset = this.LeftPlayerValue - this.Solution;
            }
            else {
                this.LeftPlayerOffset = 0;
            }

            if (this.SelectedDataset is DatasetContent &&
                this.RightPlayerHasValue) {
                this.RightPlayerOffset = this.RightPlayerValue - this.Solution;
            }
            else {
                this.RightPlayerOffset = 0;
            }

            if (this.LeftPlayerHasValue &&
                this.RightPlayerHasValue &&
                this.SelectedDataset is DatasetContent) {
                this.LeftPlayerIsCloser = Math.Abs(this.LeftPlayerOffset) <= Math.Abs(this.RightPlayerOffset);
                this.RightPlayerIsCloser = Math.Abs(this.LeftPlayerOffset) >= Math.Abs(this.RightPlayerOffset);
            }
            else {
                this.LeftPlayerIsCloser = false;
                this.RightPlayerIsCloser = false;
            }
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

        public override void New() {
            base.New();
            this.Filename = string.Empty;
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

        internal void Vinsert_ContentIn() {
            this.Vinsert_CounterOut();
            this.Vinsert_SetContent();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.TextInsert.ToIn();
            if (this.TaskCounter > 0 && this.TaskCounter <= this.TaskCounterSize) this.Vinsert_TaskCounterIn();
        }
        internal void Vinsert_SetContent() {
            if (this.insertScene is VRemote4.HandlerSi.Scene && this.SelectedDataset is DatasetContent) this.Vinsert_SetContent(this.insertScene.TextInsert, this.SelectedDataset);
        }
        internal void Vinsert_SetContent(
            VentuzScenes.GamePool._Modules.TextInsert scene,
            DatasetContent dataset) {
            if (scene is VentuzScenes.GamePool._Modules.TextInsert &&
            dataset is DatasetContent) {
                scene.SetStyle(this.TextInsertStyle);
                scene.SetPositionX(this.TextInsertPositionX);
                scene.SetPositionY(this.TextInsertPositionY);
                scene.SetText(dataset.Text);
            }
        }
        internal void Vinsert_ContentOut() {
            this.Vinsert_CounterOut();
            this.Vinsert_TaskCounterOut();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.TextInsert.ToOut(); 
        }

        internal void Vinsert_ShowSolution() {
            this.Counter = 0;
            this.Vinsert_CounterIn();
            this.solutionTimer.Start();
        }

        internal void Vinsert_CounterIn() {
            this.Vinsert_SetCounter();
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                this.insertScene.Counter.ToIn();
                this.insertScene.PlayJingleNext();
            }
        }
        internal void Vinsert_SetCounter() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetCounter(this.insertScene.Counter, this.Counter);
        }
        internal void Vinsert_SetCounter(
            VentuzScenes.GamePool._Modules.Counter scene,
            int value) {
            if (scene is VentuzScenes.GamePool._Modules.Counter) {
                scene.SetSize(this.CounterSize);
                scene.SetPositionX(this.CounterPositionX);
                scene.SetPositionY(this.CounterPositionY);
                scene.SetCounter(value);
            }
        }
        internal void Vinsert_CounterOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Counter.ToOut(); }

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
            if (this.hostMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostMasterScene.LiveVideoIn();
            if (this.leftPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerMasterScene.LiveVideoIn();
            if (this.rightPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerMasterScene.LiveVideoIn();
        }

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
            }
            this.Save(); 
        }

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
                if (e.HasValue) {
                    this.LeftPlayerValue = e.Value;
                    this.LeftPlayerHasValue = true;
                }
                else {
                    this.LeftPlayerValue = 0;
                    this.LeftPlayerHasValue = false;
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
                if (e.HasValue) {
                    this.RightPlayerValue = e.Value;
                    this.RightPlayerHasValue = true;
                }
                else {
                    this.RightPlayerValue = 0;
                    this.RightPlayerHasValue = false;
                }
            }
        }

        private void solutionTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_solutionTimer_Elapsed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_solutionTimer_Elapsed(object content) {
            this.solutionTimer.Stop();
            if (this.Counter < this.Solution) {
                this.Counter++;
                this.Vinsert_SetCounter();
                if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.PlayJingleNext();
                this.solutionTimer.Start();
            }
            else {
                if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.PlayJingleResult();
            }
        }


        #endregion


    }
}
