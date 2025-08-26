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

using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.HitPositionScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.HitPositionScore {

    public class Data {
        public DatasetContent[] DataList;
        public int SelectedIndex;
    }

    public class DatasetContent : Messaging.Message, INotifyPropertyChanged {

        #region Properties

        private int index = 0;
        public int Index {
            get { return this.index; }
            set {
                if (value < 0) this.index = 0;
                else this.index = value; 
            }
        }

        public int ID { get { return this.index + 1; } }

        private string contentFilename = string.Empty;
        public string ContentFilename {
            get { return this.contentFilename; }
            set {
                if (this.contentFilename != value) {
                    if (value == null) value = string.Empty;
                    this.contentFilename = value;
                    this.on_PropertyChanged();
                    this.Content = Helper.getThumbnailFromMediaFile(value, 2.0f);
                }
            }
        }

        [XmlIgnore]
        public Image Content { get; private set; }

        private string name = string.Empty;
        public string Name {
            get { return this.name; }
            set {
                if (this.name != value) {
                    if (value == null) value = string.Empty;
                    this.name = value;
                    this.buildToString();
                    this.on_PropertyChanged();
                    if (string.IsNullOrEmpty(this.HostText)) this.HostText = value;
                }
            }
        }

        private string hostText = string.Empty;
        public string HostText {
            get { return this.hostText; }
            set {
                if (this.hostText != value) {
                    if (value == null) value = string.Empty;
                    this.hostText = Regex.Replace(value, "(?<!\r)\n", "\r\n"); ;
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
                this.ContentFilename = string.Empty;
            }
            else {
                this.Name = Path.GetFileNameWithoutExtension(pictureFilename);
                this.ContentFilename = pictureFilename;
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

    public class Business : _Base.TimerScore.Business {

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

        private Point leftPlayerSelection = new Point();
        [NotSerialized]
        public Point LeftPlayerSelection {
            get { return this.leftPlayerSelection; }
            set {
                if (this.leftPlayerSelection != value) {
                    this.leftPlayerSelection = value;
                    this.on_PropertyChanged();
                }
                this.LeftPlayerSelectionIsValid = this.leftPlayerSelection.X > 0 && this.leftPlayerSelection.Y > 0;
            }
        }
        private bool leftPlayerSelectionIsValid = false;
        [NotSerialized]
        public bool LeftPlayerSelectionIsValid {
            get { return this.leftPlayerSelectionIsValid; }
            set {
                if (this.leftPlayerSelectionIsValid != value) {
                    this.leftPlayerSelectionIsValid = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool leftPlayerSelectionHits = false;
        [NotSerialized]
        public bool LeftPlayerSelectionHits {
            get { return this.leftPlayerSelectionHits; }
            set {
                if (this.leftPlayerSelectionHits != value) {
                    this.leftPlayerSelectionHits = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private Point rightPlayerSelection = new Point();
        [NotSerialized]
        public Point RightPlayerSelection {
            get { return this.rightPlayerSelection; }
            set {
                if (this.rightPlayerSelection != value) {
                    this.rightPlayerSelection = value;
                    this.on_PropertyChanged();
                }
                this.RightPlayerSelectionIsValid = this.rightPlayerSelection.X > 0 && this.rightPlayerSelection.Y > 0;
            }
        }
        private bool rightPlayerSelectionIsValid = false;
        [NotSerialized]
        public bool RightPlayerSelectionIsValid {
            get { return this.rightPlayerSelectionIsValid; }
            set {
                if (this.rightPlayerSelectionIsValid != value) {
                    this.rightPlayerSelectionIsValid = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool rightPlayerSelectionHits = false;
        [NotSerialized]
        public bool RightPlayerSelectionHits {
            get { return this.rightPlayerSelectionHits; }
            set {
                if (this.rightPlayerSelectionHits != value) {
                    this.rightPlayerSelectionHits = value;
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.HitPositionScore'", typeIdentifier);
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
            this.insertScene.Timer.Alarm1Fired += this.timer_Alarm1Fired;
            this.insertScene.Timer.Alarm2Fired += this.timer_Alarm2Fired;
            this.insertScene.Timer.StopFired += this.timer_StopFired;
            this.insertScene.Timer.PropertyChanged += this.timer_PropertyChanged;

            this.fullscreenScene = new Fullscreen(syncContext, fullscreenMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.fullscreenScene.StatusChanged += this.fullscreenScene_StatusChanged;

            this.hostScene = new Host(syncContext, hostMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.hostScene.StatusChanged += this.hostScene_StatusChanged;

            this.leftPlayerScene = new Player(syncContext, leftPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.leftPlayerScene.StatusChanged += this.leftPlayerScene_StatusChanged;
            this.leftPlayerScene.OKPressed += this.leftPlayerScene_OKPressed;

            this.rightPlayerScene = new Player(syncContext, rightPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.rightPlayerScene.StatusChanged += this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.OKPressed += this.rightPlayerScene_OKPressed;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);
        }

        public override void Dispose() {
            base.Dispose();

            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Timer.Alarm1Fired -= this.timer_Alarm1Fired;
            this.insertScene.Timer.Alarm2Fired -= this.timer_Alarm2Fired;
            this.insertScene.Timer.StopFired -= this.timer_StopFired;
            this.insertScene.Timer.PropertyChanged -= this.timer_PropertyChanged;
            this.insertScene.Dispose();

            this.fullscreenScene.StatusChanged -= this.fullscreenScene_StatusChanged;
            this.fullscreenScene.Dispose();

            this.hostScene.StatusChanged -= this.hostScene_StatusChanged;
            this.hostScene.Dispose();

            this.leftPlayerScene.StatusChanged -= this.leftPlayerScene_StatusChanged;
            this.leftPlayerScene.OKPressed -= this.leftPlayerScene_OKPressed;
            this.leftPlayerScene.Dispose();

            this.rightPlayerScene.StatusChanged -= this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.OKPressed -= this.rightPlayerScene_OKPressed;
            this.rightPlayerScene.Dispose();
        }

        public override void ResetData() {
            base.ResetData();
            this.TaskCounter = 0;
            this.SelectDataset(0);
            this.LeftPlayerSelection = new Point();
            this.LeftPlayerSelectionHits = false;
            this.RightPlayerSelection = new Point();
            this.RightPlayerSelectionHits = false;
        }

        public void Resolve() {
            if (!this.SampleIncluded || (this.SampleIncluded && this.SelectedDatasetIndex > 0)) {
                if (this.LeftPlayerSelectionHits) this.LeftPlayerScore++;
                if (this.RightPlayerSelectionHits) this.RightPlayerScore++;
            }
        }

        public override void Next() {
            base.Next();
            this.TaskCounter++;
            this.SelectDataset(this.SelectedDatasetIndex + 1);
            this.LeftPlayerSelection = new Point();
            this.LeftPlayerSelectionHits = false;
            this.RightPlayerSelection = new Point();
            this.RightPlayerSelectionHits = false;
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
                }
                else {
                    this.dataList.Add(newDataset);
                }
                this.buildNameList();
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
            int index = 0;
            foreach (DatasetContent item in this.dataList) {
                item.Index = index;
                this.names.Add(item.ToString());
                index++;
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
                    //this.checkAges();
                    this.Save();
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

        public override void Vinsert_TimerIn() { if (this.insertScene is Insert) this.Vinsert_TimerIn(this.insertScene.Timer); }
        public override void Vinsert_SetTimer() { if (this.insertScene is Insert) this.Vinsert_SetTimer(this.insertScene.Timer); }
        public override void Vinsert_StartTimer() { if (this.insertScene is Insert) this.Vinsert_StartTimer(this.insertScene.Timer); }
        public override void Vinsert_StopTimer() {
            if (this.insertScene is Insert) this.Vinsert_StopTimer(this.insertScene.Timer);
            this.Vfullscreen_StopTimer();
            this.Vfullscreen_SetTimer(this.TimerCurrentTime);
        }
        public override void Vinsert_ContinueTimer() { if (this.insertScene is Insert) this.Vinsert_ContinueTimer(this.insertScene.Timer); }
        public override void Vinsert_ResetTimer() { if (this.insertScene is Insert) this.Vinsert_ResetTimer(this.insertScene.Timer); }
        public override void Vinsert_TimerOut() { if (this.insertScene is Insert) this.Vinsert_TimerOut(this.insertScene.Timer); }

        public override void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score, this.LeftPlayerScore, this.RightPlayerScore); }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }

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

        public void Vinsert_BorderIn() {
            if (this.insertScene is Insert) {
                this.Vinsert_SetBorder(this.insertScene.Border, this.LeftPlayerScore, this.RightPlayerScore);
                this.insertScene.Border.ToIn();
                if (this.TaskCounter > 0 && this.TaskCounter <= this.TaskCounterSize) this.Vinsert_TaskCounterIn();
            }
        }
        public void Vinsert_SetBorder() {
            if (this.insertScene is Insert) {
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
            }
        }
        public void Vinsert_BorderOut() { 
            if (this.insertScene is Insert) this.insertScene.Border.ToOut();
            this.Vinsert_TaskCounterOut();
        }

        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void Vfullscreen_LoadScene() { this.fullscreenScene.Load(); }
        public void Vfullscreen_ContentIn() {
            if (this.fullscreenScene is Fullscreen &&
                this.SelectedDataset is DatasetContent) {
                this.Vfullscreen_SetContent(this.fullscreenScene, this.SelectedDataset.ContentFilename);
                this.fullscreenScene.Game.ContentToIn();
            }
        }
        public void Vfullscreen_SetContent(
            Fullscreen scene,
            string filename) {
            if (scene is Fullscreen) {
                if (string.IsNullOrEmpty(filename)) scene.Game.SetFilename(string.Empty);
                else scene.Game.SetFilename(filename);
            }
        }
        public void Vfullscreen_ContentOut() { if (this.fullscreenScene is Fullscreen) this.fullscreenScene.Game.ContentToOut(); }
        public void Vfullscreen_ShowSelection() {
            this.Vfullscreen_SetPlayerMarker();
            if (this.fullscreenScene is Fullscreen) {
                if (this.LeftPlayerSelectionIsValid) this.fullscreenScene.Game.PlayRedMarker();
                if (this.RightPlayerSelectionIsValid) this.fullscreenScene.Game.PlayBlueMarker();
            }
        }
        public void Vfullscreen_SetPlayerMarker() {
            if (this.fullscreenScene is Fullscreen) this.Vfullscreen_SetPlayerMarker(this.fullscreenScene, this.LeftPlayerSelection, this.LeftPlayerSelectionIsValid, this.RightPlayerSelection, this.RightPlayerSelectionIsValid);
        }
        public void Vfullscreen_SetPlayerMarker(
            Fullscreen scene,
            Point leftPlayerSelection,
            bool leftPlayerSelectionIsValid,
            Point rightPlayerSelection,
            bool rightPlayerSelectionIsValid) {
            if (scene is Fullscreen) {
                if (leftPlayerSelectionIsValid) {
                    scene.Game.SetRedMarkerPositionX(leftPlayerSelection.X);
                    scene.Game.SetRedMarkerPositionY(leftPlayerSelection.Y);
                }
                if (rightPlayerSelectionIsValid) {
                    scene.Game.SetBlueMarkerPositionX(rightPlayerSelection.X);
                    scene.Game.SetBlueMarkerPositionY(rightPlayerSelection.Y);
                }
            }
        }

        internal void Vfullscreen_ShowLeftPlayerMarker() {
            this.Vfullscreen_SetPlayerMarker();
            if (this.fullscreenScene is Fullscreen &&
                this.LeftPlayerSelectionIsValid) this.fullscreenScene.Game.ShowRedMarker();
        }
        internal void Vfullscreen_PlayLeftPlayerMarker() {
            this.Vfullscreen_SetPlayerMarker();
            if (this.fullscreenScene is Fullscreen &&
                this.LeftPlayerSelectionIsValid) this.fullscreenScene.Game.PlayRedMarker();
        }
        internal void Vfullscreen_ZoomToLeftPlayerMarker() {
            this.Vfullscreen_SetPlayerMarker();
            if (this.fullscreenScene is Fullscreen &&
                this.LeftPlayerSelectionIsValid) this.fullscreenScene.Game.ZoomInRedMarker();
        }
        internal void Vfullscreen_HideLeftPlayerMarker() { if (this.fullscreenScene is Fullscreen) this.fullscreenScene.Game.HideRedMarker(); }
        internal void Vfullscreen_BuzzerLeftPlayerMarker() { if (this.fullscreenScene is Fullscreen) this.fullscreenScene.Game.BuzzerRedMarker(); }
        internal void Vfullscreen_ShowRightPlayerMarker() {
            this.Vfullscreen_SetPlayerMarker();
            if (this.fullscreenScene is Fullscreen &&
                this.RightPlayerSelectionIsValid) this.fullscreenScene.Game.ShowBlueMarker();
        }
        internal void Vfullscreen_PlayRightPlayerMarker() {
            this.Vfullscreen_SetPlayerMarker();
            if (this.fullscreenScene is Fullscreen &&
                this.RightPlayerSelectionIsValid) this.fullscreenScene.Game.PlayBlueMarker();
        }
        internal void Vfullscreen_ZoomToRightPlayerMarker() {
            this.Vfullscreen_SetPlayerMarker();
            if (this.fullscreenScene is Fullscreen &&
                this.RightPlayerSelectionIsValid) this.fullscreenScene.Game.ZoomInBlueMarker();
        }
        internal void Vfullscreen_HideRightPlayerMarker() { if (this.fullscreenScene is Fullscreen) this.fullscreenScene.Game.HideBlueMarker(); }
        internal void Vfullscreen_BuzzerRightPlayerMarker() { if (this.fullscreenScene is Fullscreen) this.fullscreenScene.Game.BuzzerBlueMarker(); }
        internal void Vfullscreen_ResetZoom() { if (this.fullscreenScene is Fullscreen) this.fullscreenScene.Game.ResetZoom(); }
        internal void Vfullscreen_ResetContent() { if (this.fullscreenScene is Fullscreen) this.fullscreenScene.Game.Reset(); }
        public override void Vfullscreen_UnloadScene() { this.fullscreenScene.Unload(); }

        public override void Vstage_Init() {
            this.Vstage_GamescoreIn();
        }
        public void Vstage_ContentIn() {
            this.Vhost_Set();
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.hostScene.ToIn();
                this.hostScene.Game.ContentToIn();
            }
            this.Vplayer_ContentIn();
            if (this.BuzzerMode) this.Vstage_SetBuzzer(PlayerSelection.NotSelected);
            else if (this.hostMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostMasterScene.SetDisplaysToLogo();
        }
        public virtual void Vstage_SetBuzzer(
            PlayerSelection buzzeredPlayer) {
            switch (buzzeredPlayer) {
                case PlayerSelection.LeftPlayer:
                    this.hostMasterScene.SetBuzzerLeft();
                    this.leftPlayerMasterScene.SetBuzzerLeft();
                    this.rightPlayerMasterScene.SetBuzzerLeft();
                    break;
                case PlayerSelection.RightPlayer:
                    this.hostMasterScene.SetBuzzerRight();
                    this.leftPlayerMasterScene.SetBuzzerRight();
                    this.rightPlayerMasterScene.SetBuzzerRight();
                    break;
                case PlayerSelection.NotSelected:
                default:
                    this.hostMasterScene.SetBuzzerOut();
                    this.leftPlayerMasterScene.SetBuzzerOut();
                    this.rightPlayerMasterScene.SetBuzzerOut();
                    break;
            }
        }
        public void Vstage_ContentOut() {
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.Game.ContentToOut();
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.ContentToOut();
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.ContentToOut();
        }
        internal void Vstage_ResetContent() {
            this.Vhost_Reset();
            this.VleftPlayer_Reset();
            this.VrightPlayer_Reset();
        }

        public override void Vhost_LoadScene() { this.hostScene.Load(); }
        internal void Vhost_ShowSelection() {
            this.Vhost_Set();
            if (this.hostScene is VRemote4.HandlerSi.Scene) {
                if (this.LeftPlayerSelectionIsValid) this.hostScene.Game.PlayRedMarker();
                else this.hostScene.Game.HideRedMarker();
                if (this.RightPlayerSelectionIsValid) this.hostScene.Game.PlayBlueMarker();
                else this.hostScene.Game.HideBlueMarker();
            }
        }
        public void Vhost_Set() { if (this.hostScene is Host && this.SelectedDataset is DatasetContent) this.Vhost_Set(this.hostScene, this.SelectedDataset.ContentFilename, this.SelectedDataset.HostText, this.TaskCounter, this.LeftPlayerSelection, this.LeftPlayerSelectionIsValid, this.RightPlayerSelection, this.RightPlayerSelectionIsValid); }
        public void Vhost_Set(
            Host scene,
            string filename,
            string hostText,
            int counter,
            Point leftPlayerSelection,
            bool leftPlayerSelectionIsValid,
            Point rightPlayerSelection,
            bool rightPlayerSelectionIsValid) {
            if (scene is Host) {
                if (string.IsNullOrEmpty(filename)) scene.Game.SetFilename(string.Empty);
                else scene.Game.SetFilename(filename);
                if (string.IsNullOrEmpty(hostText)) hostText = string.Empty;
                if (counter == 0) hostText = string.Format("Beispiel: {0}", hostText);
                else if (counter > 0 && counter <= this.TaskCounterSize) hostText = string.Format("{0}/{1}: {2}", counter.ToString(), this.TaskCounterSize.ToString(), hostText);
                scene.SetHostText(hostText);
                if (leftPlayerSelectionIsValid) {
                    scene.Game.SetRedMarkerPositionX(leftPlayerSelection.X);
                    scene.Game.SetRedMarkerPositionY(leftPlayerSelection.Y);
                }
                if (rightPlayerSelectionIsValid) {
                    scene.Game.SetBlueMarkerPositionX(rightPlayerSelection.X);
                    scene.Game.SetBlueMarkerPositionY(rightPlayerSelection.Y);
                }
            }
        }
        public void Vhost_Reset() {
            if (this.hostScene is Host) {
                this.hostScene.Reset();
                this.hostScene.Game.Reset();
            }
        }
        public override void Vhost_UnloadScene() { this.hostScene.Unload(); }

        internal void Vplayer_EnableTouch() {
            this.VleftPlayer_EnableTouch();
            this.VrightPlayer_EnableTouch();
        }

        public override void Vleftplayer_LoadScene() { this.leftPlayerScene.Load(); }
        public void Vleftplayer_Set() { if (this.leftPlayerScene is VRemote4.HandlerSi.Scene && this.SelectedDataset is DatasetContent) this.Vplayer_Set(this.leftPlayerScene, this.SelectedDataset.ContentFilename); }
        public void VleftPlayer_EnableTouch() { if (this.leftPlayerScene is VRemote4.HandlerSi.Scene) this.leftPlayerScene.EnableTouch(); }
        public void VleftPlayer_DisableTouch() { if (this.leftPlayerScene is VRemote4.HandlerSi.Scene) this.leftPlayerScene.DisableTouch(); }
        public void VleftPlayer_Reset() { if (this.leftPlayerScene is Player) this.leftPlayerScene.Reset(); }
        public override void Vleftplayer_UnloadScene() { this.leftPlayerScene.Unload(); }

        public override void Vrightplayer_LoadScene() { this.rightPlayerScene.Load(); }
        public void Vrightplayer_Set() { if (this.rightPlayerScene is VRemote4.HandlerSi.Scene && this.SelectedDataset is DatasetContent) this.Vplayer_Set(this.rightPlayerScene, this.SelectedDataset.ContentFilename); }
        public void VrightPlayer_EnableTouch() { if (this.rightPlayerScene is VRemote4.HandlerSi.Scene) this.rightPlayerScene.EnableTouch(); }
        public void VrightPlayer_DisableTouch() { if (this.rightPlayerScene is VRemote4.HandlerSi.Scene) this.rightPlayerScene.DisableTouch(); }
        public void VrightPlayer_Reset() { if (this.rightPlayerScene is Player) this.rightPlayerScene.Reset(); }
        public override void Vrightplayer_UnloadScene() { this.rightPlayerScene.Unload(); }

        public void Vplayer_ContentIn() {
            this.Vleftplayer_Set();
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.ContentToIn();
            this.Vrightplayer_Set();
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.ContentToIn();
        }
        public void Vplayer_ContentOut() {
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.ContentToOut();
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.ContentToOut();
        }
        public void Vplayer_Set(
            Player scene,
            string filename) {
            if (scene is Player) {
                if (scene == this.leftPlayerScene) scene.SetPlayer(Player.PlayerValues.Red);
                else scene.SetPlayer(Player.PlayerValues.Blue);
                if (string.IsNullOrEmpty(filename)) scene.SetContentFilename(string.Empty);
                else scene.SetContentFilename(filename);
            }
        }

        public override void ClearGraphic() {
            base.ClearGraphic();
            this.insertScene.Clear();
            this.fullscreenScene.Clear();
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
                if (e.PropertyName == "Name") {
                    this.buildNameList();
                    this.on_PropertyChanged("NameList");
                }
            }
            this.Save(); 
        }

        void leftPlayerScene_OKPressed(object sender, Point e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftPlayerScene_OKPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftPlayerScene_OKPressed(object content) {
            Point e = (Point)content;
            if (this.BuzzerMode) {
                if (this.RightPlayerSelectionIsValid) {
                    // Der andere Spieler war schneller
                    this.VleftPlayer_Reset();
                    this.LeftPlayerSelection = new Point();
                }
                else {
                    // Erster!
                    this.Vfullscreen_BuzzerLeftPlayerMarker();
                    this.VrightPlayer_Reset();
                    this.LeftPlayerSelection = e;
                    this.Vstage_SetBuzzer(PlayerSelection.LeftPlayer);
                }
            }
            else this.LeftPlayerSelection = e;
            this.Vhost_ShowSelection();
        }

        void rightPlayerScene_OKPressed(object sender, Point e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerScene_OKPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerScene_OKPressed(object content) {
            Point e = (Point)content;
            if (this.BuzzerMode) {
                if (this.LeftPlayerSelectionIsValid) {
                    // Der andere Spieler war schneller
                    this.VrightPlayer_Reset();
                    this.RightPlayerSelection = new Point();
                }
                else {
                    // Erster!
                    this.Vfullscreen_BuzzerRightPlayerMarker();
                    this.VleftPlayer_Reset();
                    this.RightPlayerSelection = e;
                    this.Vstage_SetBuzzer(PlayerSelection.RightPlayer);
                }
            }
            else this.RightPlayerSelection = e;
            this.Vhost_ShowSelection();
        }

        #endregion

    }
}
