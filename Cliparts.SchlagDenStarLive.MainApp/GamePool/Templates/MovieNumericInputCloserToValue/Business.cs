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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MovieNumericInputCloserToValue;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.MovieNumericInputCloserToValue {

    public class Data {
        public DatasetContent[] DataList;
        public int SelectedIndex;
    }

    public class DatasetContent : Messaging.Message, INotifyPropertyChanged {

        #region Properties

        private string name = string.Empty;
        public string Name {
            get { return this.name; }
            set {
                if (this.name != value) {
                    if (string.IsNullOrEmpty(value)) this.name = string.Empty;
                    else this.name = value;
                    this.buildToString();
                    this.on_PropertyChanged();
                }
            }
        }

        private Single solution = 0;
        public Single Solution {
            get { return this.solution; }
            set {
                if (this.solution != value) {
                    if (value < 0) this.solution = 0;
                    else this.solution = value;
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

        private string movieFilename = string.Empty;
        public string MovieFilename {
            get { return this.movieFilename; }
            set {
                if (this.movieFilename != value) {
                    if (value == null) value = string.Empty;
                    this.movieFilename = value;
                    this.on_PropertyChanged();
                    this.Movie = Helper.getThumbnailFromMediaFile(value, 0f);
                }
            }
        }

        private Single firstStop = 0;
        public Single FirstStop {
            get { return this.firstStop; }
            set {
                if (this.firstStop != value) {
                    if (value < 0) this.firstStop = 0;
                    else this.firstStop = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private Single secondStop = 0;
        public Single SecondStop {
            get { return this.secondStop; }
            set {
                if (this.secondStop != value) {
                    if (value < 0) this.secondStop = 0;
                    else this.secondStop = value;
                    this.on_PropertyChanged();
                }
            }
        }

        [XmlIgnore]
        public Image Movie { get; private set; }

        private string toString = string.Empty;

        private string unit = "kg";
        public string Unit {
            get { return this.unit; }
            set {
                if (this.unit != value) {
                    if (string.IsNullOrEmpty(value)) this.unit = string.Empty;
                    else this.unit = value;
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion


        #region Funktionen

        public DatasetContent() { }
        public DatasetContent(
            string movieFilename) {
            if (string.IsNullOrEmpty(movieFilename)) {
                this.Name = "?";
                this.MovieFilename = string.Empty;
                this.HostText = string.Empty;
            }
            else {
                this.Name = Path.GetFileNameWithoutExtension(movieFilename);
                this.MovieFilename = movieFilename;
                this.HostText = this.Name;
            }
        }

        public void Clone(
            DatasetContent source) {
            if (source is DatasetContent) {
                this.Name = source.Name;
                this.Solution = source.Solution;
                this.HostText = source.HostText;
                this.MovieFilename = source.MovieFilename;
                this.FirstStop = source.FirstStop;
                this.SecondStop = source.SecondStop;
            }
            else {
                this.Name = string.Empty;
                this.Solution = 0;
                this.HostText = string.Empty;
                this.MovieFilename = string.Empty;
                this.FirstStop = 0;
                this.SecondStop = 0;
            }
        }

        private void buildToString() { this.toString = this.Name; }

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

        private VentuzScenes.GamePool._Modules.TextInsert.Styles textInsertStyle = VentuzScenes.GamePool._Modules.TextInsert.Styles.OneRow;
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

        public const int TaskCounterPenaltyCountMax = 13;

        private int taskCounterSize = 13;
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

        private float leftPlayerValue = 0;
        public float LeftPlayerValue {
            get { return this.leftPlayerValue; }
            set {
                if (this.leftPlayerValue != value) {
                    this.leftPlayerValue = value;
                    this.on_PropertyChanged();
                    this.calcOffsets();
                }
            }
        }

        private float leftPlayerOffset = 0;
        public float LeftPlayerOffset {
            get { return this.leftPlayerOffset; }
            private set {
                if (this.leftPlayerOffset != value) {
                    this.leftPlayerOffset = value;
                    this.on_PropertyChanged();
                }
            }
        }
        public string LeftPlayerOffsetText { get; private set; }

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

        private float rightPlayerValue = 0;
        public float RightPlayerValue {
            get { return this.rightPlayerValue; }
            set {
                if (this.rightPlayerValue != value) {
                    this.rightPlayerValue = value;
                    this.on_PropertyChanged();
                    this.calcOffsets();
                }
            }
        }

        private float rightPlayerOffset = 0;
        public float RightPlayerOffset {
            get { return this.rightPlayerOffset; }
            private set {
                if (this.rightPlayerOffset != value) {
                    this.rightPlayerOffset = value;
                    this.on_PropertyChanged();
                }
            }
        }
        public string RightPlayerOffsetText { get; private set; }

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

        private Fullscreen fullscreenScene;
        public override VRemote4.HandlerSi.Scene.States FullscreenSceneStatus {
            get {
                if (this.fullscreenScene is Fullscreen) return this.fullscreenScene.Status;
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.MovieNumericInputCloserToValue'", typeIdentifier);
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

            this.fullscreenScene = new Fullscreen(syncContext, fullscreenMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.fullscreenScene.StatusChanged += this.fullscreenScene_StatusChanged;

            this.hostScene = new Host(syncContext, hostMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.hostScene.StatusChanged += this.hostScene_StatusChanged;

            this.leftPlayerScene = new Player(syncContext, leftPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.leftPlayerScene.StatusChanged += this.leftPlayerScene_StatusChanged;
            this.leftPlayerScene.OKButtonPressed += this.leftPlayerScene_OKButtonPressed;

            this.rightPlayerScene = new Player(syncContext, rightPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.rightPlayerScene.StatusChanged += this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.OKButtonPressed += this.rightPlayerScene_OKButtonPressed;

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
            this.LeftPlayerValue = 0;
            this.RightPlayerValue = 0;
            this.SelectDataset(0);
        }

        internal void Resolve() {
            if (this.SelectedDatasetIndex > 0 ||
                !this.SampleIncluded) {
                if (this.LeftPlayerIsCloser) this.LeftPlayerScore++;
                else if (this.RightPlayerIsCloser) this.RightPlayerScore++;
                else {
                    this.LeftPlayerScore++;
                    this.RightPlayerScore++;
                }
            }
        }

        public override void Next() {
            base.Next();
            this.TaskCounter++;
            this.LeftPlayerValue = 0;
            this.RightPlayerValue = 0;
            this.SelectDataset(this.SelectedDatasetIndex + 1);
        }

        private void calcOffsets() {
            if (this.SelectedDataset is DatasetContent &&
                this.LeftPlayerValue > 0) {
                this.LeftPlayerOffset = this.LeftPlayerValue - this.SelectedDataset.Solution;
                if (this.LeftPlayerOffset > 0) this.LeftPlayerOffsetText = "+" + this.LeftPlayerOffset.ToString("0.0") + this.SelectedDataset.Unit;
                else if (this.LeftPlayerOffset < 0) this.LeftPlayerOffsetText = this.LeftPlayerOffset.ToString("0.0") + this.SelectedDataset.Unit;
                else this.LeftPlayerOffsetText = this.LeftPlayerOffset.ToString() + this.SelectedDataset.Unit;
            }
            else {
                this.LeftPlayerOffset = 0;
                this.LeftPlayerOffsetText = string.Empty;
            }

            if (this.SelectedDataset is DatasetContent &&
                this.RightPlayerValue > 0) {
                this.RightPlayerOffset = this.RightPlayerValue - this.SelectedDataset.Solution;
                if (this.RightPlayerOffset > 0) this.RightPlayerOffsetText = "+" + this.RightPlayerOffset.ToString("0.0") + this.SelectedDataset.Unit;
                else if (this.RightPlayerOffset < 0) this.RightPlayerOffsetText = this.RightPlayerOffset.ToString("0.0") + this.SelectedDataset.Unit;
                else this.RightPlayerOffsetText = this.RightPlayerOffset.ToString() + this.SelectedDataset.Unit;
            }
            else {
                this.RightPlayerOffset = 0;
                this.RightPlayerOffsetText = string.Empty;
            }

            if (this.LeftPlayerValue > 0 &&
                this.RightPlayerValue > 0 &&
                this.SelectedDataset is DatasetContent) {
                this.LeftPlayerIsCloser = Math.Abs(this.LeftPlayerOffset) < Math.Abs(this.RightPlayerOffset);
                this.RightPlayerIsCloser = Math.Abs(this.LeftPlayerOffset) > Math.Abs(this.RightPlayerOffset);
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

        internal void Vinsert_TextInsertIn() {
            this.Vinsert_SetTextInsert();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.TextInsert.ToIn();
        }
        internal void Vinsert_SetTextInsert(
            Insert scene,
            string text) {
            if (scene is Insert) {
                scene.TextInsert.SetPositionX(this.TextInsertPositionX);
                scene.TextInsert.SetPositionY(this.TextInsertPositionY);
                scene.TextInsert.SetStyle(this.TextInsertStyle);
                scene.TextInsert.SetText(text);
            }
        }
        internal void Vinsert_SetTextInsert() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) this.Vinsert_SetTextInsert(this.insertScene, string.Format("{0}{1}", this.SelectedDataset.Solution.ToString("0.0"), this.SelectedDataset.Unit));
        }
        internal void Vinsert_TextInsertOut() { 
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.TextInsert.ToOut(); 
        }
        internal void Vinsert_InputInsertIn() {
            this.Vinsert_SetInputInsert();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.CloserToValue.ToIn();
            if (this.TaskCounter > 0 && this.TaskCounter <= this.TaskCounterSize) this.Vinsert_TaskCounterIn();
        }
        internal void Vinsert_SetInputInsert(
            Insert scene,
            string unit,
            float leftPlayerValue,
            string leftPlayerOffsetText,
            bool leftPlayerIsCloser,
            float rightPlayerValue,
            string rightPlayerOffsetText,
            bool rightPlayerIsCloser) {
            if (scene is Insert) {
                scene.CloserToValue.SetPositionX(this.InputInsertPositionX);
                scene.CloserToValue.SetPositionY(this.InputInsertPositionY);
                scene.CloserToValue.SetLeftName(this.LeftPlayerName);
                string value = string.Format("{0}{1}", leftPlayerValue.ToString("0.0"), unit);
                this.insertScene.CloserToValue.SetLeftValue(value);
                scene.CloserToValue.SetLeftOffsetValue(leftPlayerOffsetText);
                scene.CloserToValue.SetLeftOffsetIsWinner(leftPlayerIsCloser);
                this.insertScene.CloserToValue.SetRightName(this.RightPlayerName);
                value = string.Format("{0}{1}", rightPlayerValue.ToString("0.0"), unit);
                this.insertScene.CloserToValue.SetRightValue(value);
                scene.CloserToValue.SetRightOffsetValue(rightPlayerOffsetText);
                scene.CloserToValue.SetRightOffsetIsWinner(rightPlayerIsCloser);
                this.insertScene.CloserToValue.SetFlipPosition(this.FlipPlayers);
            }
        }
        internal void Vinsert_SetInputInsert() {
            string unit = string.Empty;
            if (this.SelectedDataset is DatasetContent) unit = this.SelectedDataset.Unit;
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vinsert_SetInputInsert(                    
                this.insertScene, 
                unit,
                this.LeftPlayerValue, 
                this.LeftPlayerOffsetText,
                this.LeftPlayerIsCloser,
                this.RightPlayerValue,
                this.RightPlayerOffsetText,
                this.RightPlayerIsCloser);
        }
        internal void Vinsert_ResolveInputInsert() {
            this.Vinsert_SetInputInsert();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.CloserToValue.OffsetToIn();
        }
        internal void Vinsert_InputInsertOut() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.CloserToValue.ToOut();
            this.Vinsert_TaskCounterOut();
        }
        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void Vfullscreen_LoadScene() {
            base.Vfullscreen_LoadScene();
            this.fullscreenScene.Load();
        }
        internal void Vfullscreen_ContentIn() {
            if (this.fullscreenScene is VRemote4.HandlerSi.Scene &&
                this.SelectedDataset is DatasetContent) {
                this.Vfullscreen_SetContent(this.fullscreenScene, this.SelectedDataset.MovieFilename, this.SelectedDataset.FirstStop, this.SelectedDataset.SecondStop);
                this.fullscreenScene.ToIn();
            }
        }
        public void Vfullscreen_SetContent(
            Fullscreen scene,
            string filename,
            Single firstStop,
            Single secondStop) {
            if (scene is Fullscreen) {
                scene.SetMovieFilename(filename);
                scene.SetFirstStop(firstStop);
                scene.SetSecondStop(secondStop);
            }
        }
        internal void Vfullscreen_PlayMovie() {
            if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.Start();
        }
        internal void Vfullscreen_ContinueMovie() {
            if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.Continue();
        }
        public void Vfullscreen_ContentOut() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.ToOut(); }
        public override void Vfullscreen_UnloadScene() {
            base.Vfullscreen_UnloadScene();
            this.fullscreenScene.Unload();
        }

        public override void Vstage_Init() {
            base.Vstage_Init();
            if (this.hostMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostMasterScene.LiveVideoIn();
            if (this.leftPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerMasterScene.LiveVideoIn();
            if (this.rightPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerMasterScene.LiveVideoIn();
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
                string hostText = this.SelectedDataset.HostText;
                if (this.TaskCounter == 0) hostText = string.Format("Beispiel: {0}", hostText);
                else if (this.TaskCounter > 0 && this.TaskCounter <= this.TaskCounterSize) hostText = string.Format("{0}/{1}: {2}", this.TaskCounter.ToString(), this.TaskCounterSize.ToString(), hostText);
                this.hostScene.SetText(hostText);
                this.hostScene.ToIn();
            }
        }
        internal void Vhost_SetPlayerInput() {
            string unit = string.Empty;
            if (this.SelectedDataset is DatasetContent) unit = this.SelectedDataset.Unit;
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vhost_SetPlayerInput(
                    this.hostScene,
                    unit,
                    this.LeftPlayerValue,
                    this.LeftPlayerOffsetText,
                    this.LeftPlayerIsCloser,
                    this.RightPlayerValue,
                    this.RightPlayerOffsetText,
                    this.RightPlayerIsCloser);
        }
        internal void Vhost_SetPlayerInput(
            Host scene,
            string unit,
            float leftPlayerValue,
            string leftPlayerOffsetText,
            bool leftPlayerIsCloser,
            float rightPlayerValue,
            string rightPlayerOffsetText,
            bool rightPlayerIsCloser) {
            if (this.hostScene is Host) {
                string text;
                if (leftPlayerValue > 0) {
                    text = leftPlayerValue.ToString() + unit;
                    if (!string.IsNullOrEmpty(leftPlayerOffsetText)) text += " / " + leftPlayerOffsetText;
                    this.hostScene.SetLeftValue(text);
                    this.hostScene.SetLeftIn();
                }
                else this.hostScene.SetLeftOut();
                
                if (rightPlayerValue > 0) {
                    text = rightPlayerValue.ToString() + unit;
                    if (!string.IsNullOrEmpty(rightPlayerOffsetText)) text += " / " + rightPlayerOffsetText;
                    this.hostScene.SetRightValue(text);
                    this.hostScene.SetRightIn();
                }
                else this.hostScene.SetRightOut();

                if (leftPlayerIsCloser) this.hostScene.SetLeftCloser();
                else if (rightPlayerIsCloser) this.hostScene.SetRightCloser();
                else hostScene.ResetCloserPlayer();
            }
        }
        internal void Vhost_ContentOut() {
            if (this.hostScene is Host) this.hostScene.ToOut();
        }
        public override void Vhost_UnloadScene() { this.hostScene.Unload(); }

        internal void Vplayers_ContentOut() {
            this.Vleftplayer_ContentOut();
            this.Vrightplayer_ContentOut();
        }

        public override void Vleftplayer_LoadScene() { this.leftPlayerScene.Load(); }
        internal void Vleftplayer_ContentIn() {
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) {
                this.leftPlayerScene.SetTask(this.SelectedDataset.Name);
                this.leftPlayerScene.ToIn();
            }
        }
        internal void Vleftplayer_ContentOut() { if (this.leftPlayerScene is Player) this.leftPlayerScene.ToOut(); }
        internal void Vleftplayer_ResetInput() { if (this.leftPlayerScene is Player) this.leftPlayerScene.Reset(); }
        internal void Vleftplayer_UnlockInput() { if (this.leftPlayerScene is Player) this.leftPlayerScene.UnlockTouch(); }
        internal void Vleftplayer_ReleaseInput() { if (this.leftPlayerScene is Player) this.leftPlayerScene.UnlockTouch(); }
        internal void Vleftplayer_LockInput() { if (this.leftPlayerScene is Player) this.leftPlayerScene.LockTouch(); }
        public override void Vleftplayer_UnloadScene() { this.leftPlayerScene.Unload(); }

        public override void Vrightplayer_LoadScene() { this.rightPlayerScene.Load(); }
        internal void Vrightplayer_ContentIn() {
            if (this.rightPlayerScene is Player &&
                this.SelectedDataset is DatasetContent) {
                this.rightPlayerScene.SetTask(this.SelectedDataset.Name); 
                this.rightPlayerScene.ToIn();
            }
        }
        internal void Vrightplayer_ContentOut() { if (this.rightPlayerScene is Player) this.rightPlayerScene.ToOut(); }
        internal void Vrightplayer_ResetInput() { if (this.rightPlayerScene is Player) this.rightPlayerScene.Reset(); }
        internal void Vrightplayer_UnlockInput() { if (this.rightPlayerScene is Player) this.rightPlayerScene.UnlockTouch(); }
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
                if (e.PropertyName == "Name") {
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
            this.LeftPlayerValue = Convert.ToSingle(this.leftPlayerScene.Text) / 10.0f;
            this.Vhost_SetPlayerInput();
        }

        void rightPlayerScene_OKButtonPressed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerScene_OKButtonPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerScene_OKButtonPressed(object content) {
            this.RightPlayerValue = Convert.ToSingle(this.rightPlayerScene.Text) / 10.0f;
            this.Vhost_SetPlayerInput();
        }

        #endregion
    }
}
