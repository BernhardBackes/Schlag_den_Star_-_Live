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

using Cliparts.Serialization;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SelectPictureAorBTimerScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.SelectPictureAorBTimerScore {

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
                    if (value == null) value = string.Empty;
                    this.name = value;
                    this.buildToString();
                    this.on_PropertyChanged();
                }
            }
        }

        private string pictureFilename = string.Empty;
        public string PictureFilename {
            get { return this.pictureFilename; }
            set {
                if (this.pictureFilename != value) {
                    if (value == null) value = string.Empty;
                    this.pictureFilename = value;
                    this.Picture = Helper.getThumbnailFromMediaFile(value);
                    this.on_PropertyChanged();
                }
            }
        }

        [XmlIgnore]
        public Image Picture { get; private set; }

        private string solutionFilename = string.Empty;
        public string SolutionFilename {
            get { return this.solutionFilename; }
            set {
                if (this.solutionFilename != value) {
                    if (value == null) value = string.Empty;
                    this.solutionFilename = value;
                    this.Solution = Helper.getThumbnailFromMediaFile(value);
                    this.on_PropertyChanged();
                }
            }
        }

        [XmlIgnore]
        public Image Solution { get; private set; }

        private string hostText = string.Empty;
        public string HostText {
            get { return this.hostText; }
            set {
                if (this.hostText != value) {
                    if (value == null) value = string.Empty;
                    this.hostText = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private Fullscreen.SelectionElements correctPicture = Fullscreen.SelectionElements.PictureA;
        public Fullscreen.SelectionElements CorrectPicture {
            get { return this.correctPicture; }
            set {
                if (this.correctPicture != value) {
                    if (value == Fullscreen.SelectionElements.NA) this.correctPicture = Fullscreen.SelectionElements.PictureA;
                    else this.correctPicture = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string credits = string.Empty;
        public string Credits {
            get { return this.credits; }
            set {
                if (this.credits != value) {
                    if (value == null) value = string.Empty;
                    this.credits = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DatasetContent() { }
        public DatasetContent(
            string pictureFilename) {
            if (string.IsNullOrEmpty(pictureFilename)) {
                this.Name = "?";
                this.PictureFilename = string.Empty;
                this.SolutionFilename = string.Empty;
            }
            else {
                this.Name = Path.GetFileNameWithoutExtension(pictureFilename);
                this.PictureFilename = pictureFilename;
                this.SolutionFilename = string.Empty;
            }
        }

        private void buildToString() { this.toString = this.Name; }

        public override string ToString() { return this.toString; }

        #endregion


        #region Events.Outgoing

        public event PropertyChangedEventHandler PropertyChanged;
        protected void on_PropertyChanged([CallerMemberName] string callerName = "") { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }
        protected void on_PropertyChanged(PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, e); }
        protected void on_PropertyChanged(object sender, PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(sender, this.PropertyChanged, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }

    public class Business : _Base.Score.Business {

        #region Properties

        private int borderPositionX = 0;
        public int BorderPositionX {
            get { return this.borderPositionX; }
            set {
                if (this.borderPositionX != value) {
                    this.borderPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetBorder();
                }
            }
        }

        private int borderPositionY = 0;
        public int BorderPositionY {
            get { return this.borderPositionY; }
            set {
                if (this.borderPositionY != value) {
                    this.borderPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetBorder();
                }
            }
        }

        private int borderScaling = 100;
        public int BorderScaling {
            get { return this.borderScaling; }
            set {
                if (this.borderScaling != value) {
                    if (value < 10) this.borderScaling = 10;
                    else this.borderScaling = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetBorder();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.Border.Styles setsStyle = VentuzScenes.GamePool._Modules.Border.Styles.ThreeDotsCounter;
        public VentuzScenes.GamePool._Modules.Border.Styles BorderStyle {
            get { return this.setsStyle; }
            set {
                if (this.setsStyle != value) {
                    this.setsStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetBorder();
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

        private Fullscreen.SelectionElements leftPlayerInput = Fullscreen.SelectionElements.NA;
        [NotSerialized]
        public Fullscreen.SelectionElements LeftPlayerInput {
            get { return this.leftPlayerInput; }
            set {
                if (this.leftPlayerInput != value) {
                    this.leftPlayerInput = value;
                    this.on_PropertyChanged();
                    this.checkCorrectSelection();
                }
            }
        }

        private bool leftPlayerCorrect = false;
        public bool LeftPlayerCorrect {
            get { return this.leftPlayerCorrect; }
            private set {
                this.leftPlayerCorrect = value;
                this.on_PropertyChanged();
            }
        }

        private Fullscreen.SelectionElements rightPlayerInput = Fullscreen.SelectionElements.NA;
        [NotSerialized]
        public Fullscreen.SelectionElements RightPlayerInput {
            get { return this.rightPlayerInput; }
            set {
                if (this.rightPlayerInput != value) {
                    this.rightPlayerInput = value;
                    this.on_PropertyChanged();
                    this.checkCorrectSelection();
                }
            }
        }

        private bool rightPlayerCorrect = false;
        public bool RightPlayerCorrect {
            get { return this.rightPlayerCorrect; }
            private set {
                this.rightPlayerCorrect = value;
                this.on_PropertyChanged();
            }
        }

        private string filename = string.Empty;
        public string Filename {
            get { return this.filename; }
            set { if (this.filename != value) this.Load(value); }
        }

        private List<DatasetContent> dataList = new List<DatasetContent>();
        [NotSerialized]
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

        private bool buzzerMode = false;
        [NotSerialized]
        public bool BuzzerMode {
            get { return this.buzzerMode; }
            set {
                if (this.buzzerMode != value) {
                    this.buzzerMode = value;
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

        public _Modules.Timer.Business Vinsert_Timer { get; set; }

        private bool useTimer = false;
        [NotSerialized]
        public bool UseTimer {
            get { return this.useTimer; }
            set {
                if (this.useTimer != value) {
                    this.useTimer = value;
                    this.on_PropertyChanged();
                }
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

            this.Vinsert_Timer = new _Modules.Timer.Business();
            this.Vinsert_Timer.PropertyChanged += this.insertTimer_PropertyChanged;

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.SelectPictureAorBTimerScore'", typeIdentifier);
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
            this.insertScene.Timer.PropertyChanged += this.insertTimer_PropertyChanged;

            this.Vinsert_Timer.Pose(syncContext, this.insertScene.Timer);
            this.Vinsert_Timer.PropertyChanged += this.on_PropertyChanged;

            this.fullscreenScene = new Fullscreen(syncContext, fullscreenMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.fullscreenScene.StatusChanged += this.fullscreenScene_StatusChanged;

            this.hostScene = new Host(syncContext, hostMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.hostScene.StatusChanged += this.hostScene_StatusChanged;

            this.leftPlayerScene = new Player(syncContext, leftPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.leftPlayerScene.StatusChanged += this.leftPlayerScene_StatusChanged;
            this.leftPlayerScene.ASelected += this.leftPlayerScene_ASelected;
            this.leftPlayerScene.BSelected += this.leftPlayerScene_BSelected;

            this.rightPlayerScene = new Player(syncContext, rightPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.rightPlayerScene.StatusChanged += this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.ASelected += this.rightPlayerScene_ASelected;
            this.rightPlayerScene.BSelected += this.rightPlayerScene_BSelected;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);
        }

        public override void Dispose() {
            base.Dispose();

            this.Vinsert_Timer.Dispose();

            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Timer.PropertyChanged -= this.insertTimer_PropertyChanged;
            this.insertScene.Dispose();
            this.Vinsert_Timer.PropertyChanged -= this.on_PropertyChanged;
            this.Vinsert_Timer.Dispose();
            this.fullscreenScene.StatusChanged -= this.fullscreenScene_StatusChanged;
            this.fullscreenScene.Dispose();
            this.hostScene.StatusChanged -= this.hostScene_StatusChanged;
            this.hostScene.Dispose();
            this.leftPlayerScene.StatusChanged -= this.leftPlayerScene_StatusChanged;
            this.leftPlayerScene.ASelected -= this.leftPlayerScene_ASelected;
            this.leftPlayerScene.BSelected -= this.leftPlayerScene_BSelected;
            this.leftPlayerScene.Dispose();
            this.rightPlayerScene.StatusChanged -= this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.ASelected -= this.rightPlayerScene_ASelected;
            this.rightPlayerScene.BSelected -= this.rightPlayerScene_BSelected;
            this.rightPlayerScene.Dispose();

        }

        public override void ResetData() {
            base.ResetData();
            this.BuzzerMode = false;
            this.Vinsert_Timer.RunExtraTime = false;
            this.LeftPlayerInput = Fullscreen.SelectionElements.NA;
            this.RightPlayerInput = Fullscreen.SelectionElements.NA;
            this.TaskCounter = 0;
            this.SelectDataset(0);
        }

        internal void Resolve() {
            if (this.TaskCounter > 0) {
                if (this.LeftPlayerCorrect) this.LeftPlayerScore++;
                if (this.RightPlayerCorrect) this.RightPlayerScore++;
            }
        }

        public override void Next() {
            base.Next();
            this.LeftPlayerInput = Fullscreen.SelectionElements.NA;
            this.RightPlayerInput = Fullscreen.SelectionElements.NA;
            this.TaskCounter++;
            this.SelectDataset(this.SelectedDatasetIndex + 1);
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
            this.checkCorrectSelection();
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

        private void checkCorrectSelection() {
            if (this.SelectedDataset is DatasetContent) {
                this.LeftPlayerCorrect = this.SelectedDataset.CorrectPicture == this.LeftPlayerInput;
                this.RightPlayerCorrect = this.SelectedDataset.CorrectPicture == this.RightPlayerInput;
            }
            else {
                this.LeftPlayerCorrect = false;
                this.RightPlayerCorrect = false;
            }
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }
        public void Vinsert_BorderIn() {
            this.Vinsert_SetBorder();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Border.ToIn();
            if (this.TaskCounter > 0 && this.TaskCounter <= this.TaskCounterSize) this.Vinsert_TaskCounterIn();
        }
        public void Vinsert_SetBorder() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                this.Vinsert_SetBorder(this.insertScene.Border, this.LeftPlayerScore, this.RightPlayerScore);
            }
        }
        public void Vinsert_SetBorder(
            VentuzScenes.GamePool._Modules.Border scene,
            int leftPlayerScore,
            int rightPlayerScore) {
            if (scene is VentuzScenes.GamePool._Modules.Border) {
                scene.SetPositionX(this.BorderPositionX);
                scene.SetPositionY(this.BorderPositionY);
                scene.SetScaling(this.BorderScaling);
                scene.SetStyle(this.BorderStyle);
                scene.SetLeftScore(leftPlayerScore);
                scene.SetRightScore(rightPlayerScore);
                scene.SetLeftName(this.LeftPlayerName);
                scene.SetRightName(this.RightPlayerName);
                scene.ResetBuzzer();
            }
        }
        public void Vinsert_BuzzerBorderLeft() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.Border.SetBuzzerLeft();
                this.insertScene.PlaySoundBuzzerLeft();
            }
        }
        public void Vinsert_BuzzerBorderRight() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.Border.SetBuzzerRight();
                this.insertScene.PlaySoundBuzzerRight();
            }
        }
        public void Vinsert_BorderOut() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Border.ToOut();
            this.Vinsert_TaskCounterOut();
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

        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void Vfullscreen_LoadScene() { this.fullscreenScene.Load(); }
        public void Vfullscreen_ContentIn() {
            if (this.fullscreenScene is VRemote4.HandlerSi.Scene) {
                this.Vfullscreen_SetContent();
                this.fullscreenScene.ResetSolution();
                this.fullscreenScene.ResetSelection();
                this.fullscreenScene.ToIn();
            }
        }
        public void Vfullscreen_ShowInput() {
            if (this.fullscreenScene is VRemote4.HandlerSi.Scene) {
                this.Vfullscreen_SetContent();
                this.fullscreenScene.SelectionToIn();
            }
        }
        public void Vfullscreen_SetContent() {
            if (this.fullscreenScene is VRemote4.HandlerSi.Scene) {
                this.Vfullscreen_SetContent(this.fullscreenScene, this.SelectedDataset, this.LeftPlayerInput, this.RightPlayerInput);
            }
        }
        public void Vfullscreen_SetContent(
            Fullscreen scene,
            DatasetContent dataset,
            Fullscreen.SelectionElements leftPlayerInput,
            Fullscreen.SelectionElements rightPlayerInput) {
            if (scene is Fullscreen) {
                if (dataset is DatasetContent) {
                    scene.SetPictureFilename(dataset.PictureFilename);
                    scene.SetSolutionFilename(dataset.SolutionFilename);
                }
                scene.SetLeftPlayerSelection(leftPlayerInput);
                scene.SetRightPlayerSelection(rightPlayerInput);
            }
        }
        public void Vfullscreen_Resolve() {
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.fullscreenScene.SolutionToIn();
                if (this.SelectedDataset is DatasetContent && !string.IsNullOrEmpty(this.SelectedDataset.Credits)) {
                    this.fullscreenScene.SetCreditsText(this.SelectedDataset.Credits);
                    this.fullscreenScene.ShowCredits();
                }
            }
        }
        public void Vfullscreen_ContentOut() { if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.ToOut(); }
        public override void Vfullscreen_UnloadScene() { this.fullscreenScene.Unload(); }

        public override void Vstage_Init() {
            base.Vstage_Init();
        }

        public override void Vhost_LoadScene() { this.hostScene.Load(); }
        public void Vhost_In() {
            if (this.hostScene is VRemote4.HandlerSi.Scene) {
                this.Vhost_Set();
                this.hostScene.ToIn();
            }
        }
        public void Vhost_Set() { 
            if (this.hostScene is VRemote4.HandlerSi.Scene && 
                this.SelectedDataset is DatasetContent) this.Vhost_Set(this.hostScene, this.SelectedDataset, this.TaskCounter, this.LeftPlayerInput, this.RightPlayerInput); 
        }
        public void Vhost_Set(
            Host scene,
            DatasetContent dataset,
            int counter,
            Fullscreen.SelectionElements leftPlayerInput,
            Fullscreen.SelectionElements rightPlayerInput) {
            if (scene is Host) {
                if (dataset is DatasetContent) {
                    scene.SetFilename(dataset.SolutionFilename);
                    string hostText = dataset.HostText;
                    if (counter == 0) hostText = string.Format("Beispiel: {0}", hostText);
                    else if (counter > 0 && counter <= this.TaskCounterSize) hostText = string.Format("{0}/{1}: {2}", counter.ToString(), this.TaskCounterSize.ToString(), hostText);
                    scene.SetText(dataset.HostText);
                }
                scene.SetLeftPlayerSelection(leftPlayerInput);
                scene.SetRightPlayerSelection(rightPlayerInput);
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
        internal void Vleftplayer_ReleaseInput() { this.Vplayer_ReleaseInput(this.leftPlayerScene); }
        internal void Vleftplayer_LockInput() { this.Vplayer_LockInput(this.leftPlayerScene); }
        public override void Vleftplayer_UnloadScene() { this.leftPlayerScene.Unload(); }

        public override void Vrightplayer_LoadScene() { this.rightPlayerScene.Load(); }
        internal void Vrightplayer_ContentIn() {
            this.Vplayer_SetData(this.rightPlayerScene, this.SelectedDataset);
            this.Vplayer_In(this.rightPlayerScene);
        }
        internal void Vrightplayer_ContentOut() { this.Vplayer_Out(this.rightPlayerScene); }
        internal void Vrightplayer_ReleaseInput() { this.Vplayer_ReleaseInput(this.rightPlayerScene); }
        internal void Vrightplayer_LockInput() { this.Vplayer_LockInput(this.rightPlayerScene); }
        public override void Vrightplayer_UnloadScene() { this.rightPlayerScene.Unload(); }

        public void Vplayer_In(Player scene) {
            if (scene is Player) {
                scene.DisableTouch();
                scene.ToIn();
            }
        }
        public void Vplayer_SetData(
            Player scene,
            DatasetContent dataset) {
            if (scene is Player &&
                dataset is DatasetContent) {
                scene.SetFilename(dataset.PictureFilename);
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

        protected void insertTimer_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_insertTimer_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_insertTimer_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
            }
        }

        void dataset_Error(object sender, Messaging.ErrorEventArgs e) { this.on_Error(sender, e); }

        void dataset_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_dataset_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_dataset_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "Name") {
                    this.buildNameList();
                    this.on_PropertyChanged("NameList");
                }
            }
            this.Save();
        }

        private void leftPlayerScene_ASelected(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftPlayerScene_ASelected);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftPlayerScene_ASelected(object content) { 
            this.LeftPlayerInput = Fullscreen.SelectionElements.PictureA;
            if (this.BuzzerMode) {
                this.Vrightplayer_ContentIn();
                this.RightPlayerInput = Fullscreen.SelectionElements.NA;
                this.Vinsert_BuzzerBorderLeft();
            }
            this.Vhost_Set();
        }

        private void leftPlayerScene_BSelected(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftPlayerScene_BSelected);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftPlayerScene_BSelected(object content) { 
            this.LeftPlayerInput = Fullscreen.SelectionElements.PictureB;
            if (this.BuzzerMode) {
                this.Vrightplayer_ContentIn();
                this.RightPlayerInput = Fullscreen.SelectionElements.NA;
                this.Vinsert_BuzzerBorderLeft();
            }
            this.Vhost_Set();
        }

        private void rightPlayerScene_ASelected(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerScene_ASelected);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerScene_ASelected(object content) { 
            this.RightPlayerInput = Fullscreen.SelectionElements.PictureA;
            if (this.BuzzerMode) {
                this.Vleftplayer_ContentIn();
                this.LeftPlayerInput = Fullscreen.SelectionElements.NA;
                this.Vinsert_BuzzerBorderRight();
            }
            this.Vhost_Set();
        }

        private void rightPlayerScene_BSelected(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerScene_BSelected);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerScene_BSelected(object content) { 
            this.RightPlayerInput = Fullscreen.SelectionElements.PictureB;
            if (this.BuzzerMode) {
                this.Vleftplayer_ContentIn();
                this.LeftPlayerInput = Fullscreen.SelectionElements.NA;
                this.Vinsert_BuzzerBorderRight();
            }
            this.Vhost_Set();
        }

        #endregion

    }
}
