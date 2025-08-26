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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MorveMovieSelectScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.MorveMovieSelectScore {

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

        private string movieFilename = string.Empty;
        public string MovieFilename {
            get { return this.movieFilename; }
            set {
                if (this.movieFilename != value) {
                    if (value == null) value = string.Empty;
                    this.movieFilename = value;
                    this.on_PropertyChanged();
                    this.Movie = Helper.getThumbnailFromMediaFile(value, 2.0f);
                }
            }
        }

        [XmlIgnore]
        public Image Movie { get; private set; }

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

        private string leftText = string.Empty;
        public string LeftText {
            get { return this.leftText; }
            set {
                if (this.leftText != value) {
                    if (value == null) value = string.Empty;
                    this.leftText = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string rightText = string.Empty;
        public string RightText {
            get { return this.rightText; }
            set {
                if (this.rightText != value) {
                    if (value == null) value = string.Empty;
                    this.rightText = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private Player.SelectionValues solution = Player.SelectionValues.Left;
        public Player.SelectionValues Solution {
            get { return this.solution; }
            set {
                if (this.solution != value) {
                    if (value == Player.SelectionValues.Left) this.solution = Player.SelectionValues.Left;
                    else this.solution = Player.SelectionValues.Right;
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
                this.MovieFilename = string.Empty;
            }
            else {
                this.Name = Path.GetFileNameWithoutExtension(pictureFilename);
                this.MovieFilename = pictureFilename;
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

        private Player.SelectionValues leftPlayerSelection = Player.SelectionValues.NotSelected;
        [XmlIgnore]
        public Player.SelectionValues LeftPlayerSelection {
            get { return this.leftPlayerSelection; }
            set {
                if (this.leftPlayerSelection != value) {
                    this.leftPlayerSelection = value;
                    if (this.BuzzerMode && value != Player.SelectionValues.NotSelected) this.doBuzzer(PlayerSelection.LeftPlayer);
                    this.on_PropertyChanged();
                }
            }
        }

        private Player.SelectionValues rightPlayerSelection = Player.SelectionValues.NotSelected;
        [XmlIgnore]
        public Player.SelectionValues RightPlayerSelection {
            get { return this.rightPlayerSelection; }
            set {
                if (this.rightPlayerSelection != value) {
                    this.rightPlayerSelection = value;
                    if (this.BuzzerMode && value != Player.SelectionValues.NotSelected) this.doBuzzer(PlayerSelection.RightPlayer);
                    this.on_PropertyChanged();
                }
            }
        }
        
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.MorveMovieSelectScore'", typeIdentifier);
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
            this.leftPlayerScene.SelectionMade += this.leftPlayerScene_SelectionMade;

            this.rightPlayerScene = new Player(syncContext, rightPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.rightPlayerScene.StatusChanged += this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.SelectionMade += this.rightPlayerScene_SelectionMade;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);
        }

        public override void Dispose() {
            base.Dispose();

            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Dispose();

            this.fullscreenScene.StatusChanged -= this.fullscreenScene_StatusChanged;
            this.fullscreenScene.Dispose();

            this.hostScene.StatusChanged -= this.hostScene_StatusChanged;
            this.hostScene.Dispose();

            this.leftPlayerScene.StatusChanged -= this.leftPlayerScene_StatusChanged;
            this.leftPlayerScene.SelectionMade -= this.leftPlayerScene_SelectionMade;
            this.leftPlayerScene.Dispose();

            this.rightPlayerScene.StatusChanged -= this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.SelectionMade -= this.rightPlayerScene_SelectionMade;
            this.rightPlayerScene.Dispose();
        }

        public override void ResetData() {
            base.ResetData();
            this.TaskCounter = 0;
            this.SelectDataset(0);
            this.LeftPlayerSelection = Player.SelectionValues.NotSelected;
            this.RightPlayerSelection = Player.SelectionValues.NotSelected;
            this.BuzzerMode = false;
        }

        public void Resolve() {
            if (this.SelectedDataset is DatasetContent &&
                this.SelectedDataset.Solution != Player.SelectionValues.NotSelected &&
                (!this.SampleIncluded || (this.SampleIncluded && this.SelectedDatasetIndex > 0))) {
                if (this.LeftPlayerSelection == this.SelectedDataset.Solution) this.LeftPlayerScore++;
                if (this.RightPlayerSelection == this.SelectedDataset.Solution) this.RightPlayerScore++;
            }
        }

        public override void Next() {
            base.Next();
            this.TaskCounter++;
            this.SelectDataset(this.SelectedDatasetIndex + 1);
            this.LeftPlayerSelection = Player.SelectionValues.NotSelected;
            this.RightPlayerSelection = Player.SelectionValues.NotSelected;
        }

        private void doBuzzer(
            PlayerSelection buzzeredPlayer) {
            switch (buzzeredPlayer) {
                case PlayerSelection.LeftPlayer:
                    if (this.RightPlayerSelection == Player.SelectionValues.NotSelected) {
                        this.Vrightplayer_ResetInput();
                    }
                    else {
                        this.Vleftplayer_ResetInput();
                        this.LeftPlayerSelection = Player.SelectionValues.NotSelected;
                    }
                    break;
                case PlayerSelection.RightPlayer:
                    if (this.LeftPlayerSelection == Player.SelectionValues.NotSelected) {
                        this.Vleftplayer_ResetInput();
                    }
                    else {
                        this.Vrightplayer_ResetInput();
                        this.RightPlayerSelection = Player.SelectionValues.NotSelected;
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

        private void checkAges() {
            DateTime current = new DateTime(2018, 06, 09);
            DateTime itemDate;
            DateTime age;
            foreach (DatasetContent item in this.dataList) {
                string[] info = item.HostText.Replace("\r\n", "@").Split('@');
                itemDate = DateTime.Parse(item.LeftText.Substring(1));
                age = new DateTime(current.Ticks - itemDate.Ticks);
                info[0] += string.Format(" ({0})", age.Year.ToString());
                itemDate = DateTime.Parse(item.RightText.Substring(1));
                age = new DateTime(current.Ticks - itemDate.Ticks);
                info[1] += string.Format(" ({0})", age.Year.ToString());
                item.HostText = info[0] + "\r\n" + info[1];
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
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                this.Vinsert_SetBorder(this.insertScene.Border, this.LeftPlayerScore, this.RightPlayerScore);
                this.insertScene.Border.ToIn();
            }
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
            }
        }
        internal void Vinsert_BorderSelectionIn() {
            this.Vinsert_SetBorderSelection(false);
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Border.AddOnToIn();
        }
        internal void Vinsert_ResolveBorderSelection() { this.Vinsert_SetBorderSelection(true); }
        internal void Vinsert_SetBorderSelection(
            bool resolved) {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                bool leftPlayerIsTrue = false;
                bool rightPlayerIsTrue = false;
                if (resolved &&
                    this.SelectedDataset is DatasetContent &&
                    this.SelectedDataset.Solution != Player.SelectionValues.NotSelected) {
                    leftPlayerIsTrue = this.LeftPlayerSelection == this.SelectedDataset.Solution;
                    rightPlayerIsTrue = this.RightPlayerSelection == this.SelectedDataset.Solution;
                }
                this.Vinsert_SetBorderSelection(this.insertScene.Border, this.LeftPlayerSelection, leftPlayerIsTrue, this.RightPlayerSelection, rightPlayerIsTrue);
            }
        }
        internal void Vinsert_SetBorderSelection(
            VentuzScenes.GamePool._Modules.Border scene,
            Player.SelectionValues leftPlayerSelection,
            bool leftPlayerIsTrue,
            Player.SelectionValues rightPlayerSelection,
            bool rightPlayerIsTrue) {
            if (scene is VentuzScenes.GamePool._Modules.Border) {
                switch (leftPlayerSelection) {
                    case Player.SelectionValues.Left:
                        scene.SetLeftAddOnText("A");
                        break;
                    case Player.SelectionValues.Right:
                        scene.SetLeftAddOnText("B");
                        break;
                    case Player.SelectionValues.NotSelected:
                    default:
                        scene.SetLeftAddOnText("");
                        break;
                }
                scene.SetLeftAddOnIsGreen(leftPlayerIsTrue);
                switch (rightPlayerSelection) {
                    case Player.SelectionValues.Left:
                        scene.SetRightAddOnText("A");
                        break;
                    case Player.SelectionValues.Right:
                        scene.SetRightAddOnText("B");
                        break;
                    case Player.SelectionValues.NotSelected:
                    default:
                        scene.SetRightAddOnText("");
                        break;
                }
                scene.SetRightAddOnIsGreen(rightPlayerIsTrue);
            }
        }
        public void Vinsert_BorderOut() { 
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Border.ToOut();
            this.Vinsert_TaskCounterOut();
        }

        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void Vfullscreen_LoadScene() { this.fullscreenScene.Load(); }
        public void Vfullscreen_ContentIn() {
            if (this.fullscreenScene is VRemote4.HandlerSi.Scene &&
                this.SelectedDataset is DatasetContent) {
                this.Vfullscreen_SetContent(this.fullscreenScene, this.SelectedDataset);
                this.fullscreenScene.ToIn();
            }
        }
        public void Vfullscreen_SetContent(
            Fullscreen scene,
            DatasetContent dataset) {
            if (scene is Fullscreen &&
                dataset is DatasetContent) {
                scene.SetMovieFilename(dataset.MovieFilename);
                scene.SetStopFrame(50);
                scene.SetSolutionFrame(75);
            }
        }
        public void Vfullscreen_Start() {
            if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.Start();
            if (this.SelectedDataset is DatasetContent &&
                !string.IsNullOrEmpty(this.SelectedDataset.Credits)) {
                this.fullscreenScene.SetCreditsText(this.SelectedDataset.Credits);
                this.fullscreenScene.ShowCredits();
            }
            else this.fullscreenScene.SetCreditsText(string.Empty);
        }
        public void Vfullscreen_ShowSolution() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.Solution(); }
        public void Vfullscreen_Resolve() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.Resolve(); }
        public void Vfullscreen_ContentOut() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.ToOut(); }
        public override void Vfullscreen_UnloadScene() { this.fullscreenScene.Unload(); }

        public override void Vstage_Init() {
            this.Vstage_GamescoreIn();
            //if (this.hostMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostMasterScene.LiveVideoIn();
            if (this.leftPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerMasterScene.LiveVideoIn();
            if (this.rightPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerMasterScene.LiveVideoIn();
        }
        public void Vstage_ContentIn() {
            this.Vhost_Set();
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.ToIn();
            this.Vleftplayer_Set();
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.ToIn();
            this.Vrightplayer_Set();
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.ToIn();
        }
        public void Vstage_SetContent() {
            this.Vhost_Set();
            this.Vleftplayer_Set();
            this.Vrightplayer_Set();
        }
        public void Vstage_ContentOut() {
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.ToOut();
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.ToOut();
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.ToOut();
        }
        internal void Vstage_InitBuzzer() {
            base.Vstage_Init();
        }

        public override void Vhost_LoadScene() { this.hostScene.Load(); }
        public void Vhost_Set() { if (this.hostScene is VRemote4.HandlerSi.Scene && this.SelectedDataset is DatasetContent) this.Vhost_Set(this.hostScene, this.TaskCounter, this.SelectedDataset, this.LeftPlayerSelection, this.RightPlayerSelection); }
        public void Vhost_Set(
            Host scene,
            int counter,
            DatasetContent dataset,
            Player.SelectionValues leftPlayerSelection,
            Player.SelectionValues rightPlayerSelection) {
            if (scene is Host &&
                dataset is DatasetContent) {
                scene.SetFilename(dataset.MovieFilename);
                string hostText = dataset.HostText;
                if (counter == 0) hostText = string.Format("Beispiel\r\n{0}", hostText);
                else if (counter > 0 && counter <= this.TaskCounterSize) hostText = string.Format("{0}/{1}\r\n{2}", counter.ToString(), this.TaskCounterSize.ToString(), hostText);
                scene.SetHostText(hostText);
                scene.SetLeftText(dataset.LeftText);
                scene.SetRightText(dataset.RightText);
                if (dataset.Solution == Player.SelectionValues.Left) scene.SetBorderPosition(Host.BorderPositions.Left);
                else scene.SetBorderPosition(Host.BorderPositions.Right);
                switch (leftPlayerSelection) {
                    case Player.SelectionValues.Left:
                        switch (rightPlayerSelection) {
                            case Player.SelectionValues.Left:
                                scene.SetLeftSelection(Host.SelectionValues.Both);
                                scene.SetRightSelection(Host.SelectionValues.NotSelected);
                                break;
                            case Player.SelectionValues.Right:
                                scene.SetLeftSelection(Host.SelectionValues.LeftPlayer);
                                scene.SetRightSelection(Host.SelectionValues.RightPlayer);
                                break;
                            case Player.SelectionValues.NotSelected:
                            default:
                                scene.SetLeftSelection(Host.SelectionValues.LeftPlayer);
                                scene.SetRightSelection(Host.SelectionValues.NotSelected);
                                break;
                        }
                        break;
                    case Player.SelectionValues.Right:
                        switch (rightPlayerSelection) {
                            case Player.SelectionValues.Left:
                                scene.SetLeftSelection(Host.SelectionValues.RightPlayer);
                                scene.SetRightSelection(Host.SelectionValues.LeftPlayer);
                                break;
                            case Player.SelectionValues.Right:
                                scene.SetLeftSelection(Host.SelectionValues.NotSelected);
                                scene.SetRightSelection(Host.SelectionValues.Both);
                                break;
                            case Player.SelectionValues.NotSelected:
                            default:
                                scene.SetLeftSelection(Host.SelectionValues.NotSelected);
                                scene.SetRightSelection(Host.SelectionValues.LeftPlayer);
                                break;
                        }
                        break;
                    case Player.SelectionValues.NotSelected:
                    default:
                        switch (rightPlayerSelection) {
                            case Player.SelectionValues.Left:
                                scene.SetLeftSelection(Host.SelectionValues.RightPlayer);
                                scene.SetRightSelection(Host.SelectionValues.NotSelected);
                                break;
                            case Player.SelectionValues.Right:
                                scene.SetLeftSelection(Host.SelectionValues.NotSelected);
                                scene.SetRightSelection(Host.SelectionValues.RightPlayer);
                                break;
                            case Player.SelectionValues.NotSelected:
                            default:
                                scene.SetLeftSelection(Host.SelectionValues.NotSelected);
                                scene.SetRightSelection(Host.SelectionValues.NotSelected);
                                break;
                        }
                        break;
                }
            }
        }
        public override void Vhost_UnloadScene() { this.hostScene.Unload(); }

        public override void Vleftplayer_LoadScene() { this.leftPlayerScene.Load(); }
        public void Vleftplayer_Set() { if (this.leftPlayerScene is VRemote4.HandlerSi.Scene && this.SelectedDataset is DatasetContent) this.Vplayer_Set(this.leftPlayerScene, this.SelectedDataset, this.LeftPlayerSelection); }
        internal void Vleftplayer_ResetInput() { this.Vplayer_ResetInput(this.leftPlayerScene); }
        public void VleftPlayer_EnableTouch() { if (this.leftPlayerScene is VRemote4.HandlerSi.Scene) this.leftPlayerScene.EnableTouch(); }
        public void VleftPlayer_DisableTouch() { if (this.leftPlayerScene is VRemote4.HandlerSi.Scene) this.leftPlayerScene.DisableTouch(); }
        public override void Vleftplayer_UnloadScene() { this.leftPlayerScene.Unload(); }

        public override void Vrightplayer_LoadScene() { this.rightPlayerScene.Load(); }
        public void Vrightplayer_Set() { if (this.rightPlayerScene is VRemote4.HandlerSi.Scene && this.SelectedDataset is DatasetContent) this.Vplayer_Set(this.rightPlayerScene, this.SelectedDataset, this.RightPlayerSelection); }
        internal void Vrightplayer_ResetInput() { this.Vplayer_ResetInput(this.rightPlayerScene); }
        public void VrightPlayer_EnableTouch() { if (this.rightPlayerScene is VRemote4.HandlerSi.Scene) this.rightPlayerScene.EnableTouch(); }
        public void VrightPlayer_DisableTouch() { if (this.rightPlayerScene is VRemote4.HandlerSi.Scene) this.rightPlayerScene.DisableTouch(); }
        public override void Vrightplayer_UnloadScene() { this.rightPlayerScene.Unload(); }

        public void Vplayer_Set(
            Player scene,
            DatasetContent dataset,
            Player.SelectionValues selection) {
            if (scene is Player &&
                dataset is DatasetContent) {
                scene.SetTaskText("Wer ist älter?");
                switch (selection) {
                    case Player.SelectionValues.Left:
                        scene.SelectLeft();
                        break;
                    case Player.SelectionValues.Right:
                        scene.SelectRight();
                        break;
                    case Player.SelectionValues.NotSelected:
                    default:
                        scene.Deselect();
                        break;
                }
            }
        }

        public void Vplayer_ResetInput(Player scene) { if (scene is Player) scene.Deselect(); }

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

        void leftPlayerScene_SelectionMade(object sender, Player.SelectionValues e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftPlayerScene_SelectionMade);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftPlayerScene_SelectionMade(object content) {
            Player.SelectionValues e = (Player.SelectionValues)content;
            this.LeftPlayerSelection = e;
            this.Vhost_Set();
        }

        void rightPlayerScene_SelectionMade(object sender, Player.SelectionValues e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerScene_SelectionMade);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerScene_SelectionMade(object content) {
            Player.SelectionValues e = (Player.SelectionValues)content;
            this.RightPlayerSelection = e;
            this.Vhost_Set();
        }

        #endregion

    }
}
