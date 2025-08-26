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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.RotateImageScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.RotateImageScore {

    public class Data {
        public DatasetContent[] DataList;
        public int SelectedIndex;
    }

    public class DatasetContent : Messaging.Message, INotifyPropertyChanged {

        #region Properties

        private string taskMovieFilename = string.Empty;
        public string TaskMovieFilename {
            get { return this.taskMovieFilename; }
            set {
                if (this.taskMovieFilename != value) {
                    if (value == null) value = string.Empty;
                    this.taskMovieFilename = value;
                    this.Task = Helper.getThumbnailFromMediaFile(value);
                    this.on_PropertyChanged();
                }
            }
        }

        [XmlIgnore]
        public Image Task { get; private set; }

        private string solutionImageFilename = string.Empty;
        public string SolutionImageFilename {
            get { return this.solutionImageFilename; }
            set {
                if (this.solutionImageFilename != value) {
                    if (value == null) value = string.Empty;
                    this.solutionImageFilename = value;
                    this.Solution = Helper.getThumbnailFromMediaFile(value);
                    this.on_PropertyChanged();
                }
            }
        }

        [XmlIgnore]
        public Image Solution { get; private set; }

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

        private int taskAngle = 0;
        public int TaskAngle {
            get { return this.taskAngle; }
            set {
                if (this.taskAngle != value) {
                    this.taskAngle = GetValidatetAngle(value);
                    this.on_PropertyChanged();
                }
            }
        }

        public static int GetValidatetAngle(
            int value) {
            int result = 0;
            if (value > 180) result = 180;
            else if (value < -180) result = -180;
            else {
                int offset = Math.Abs(value % 10);
                if (value > 0) {
                    if (offset <= 5) result = value - offset;
                    else result = value + 10 - offset;
                }
                else if (value < 0) {
                    if (offset <= 5) result = value + offset;
                    else result = value - 10 + offset;
                }
                else result = 0;
            }
            return result;
        }

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DatasetContent() { }
        public DatasetContent(
            string taskFilename) {
            if (string.IsNullOrEmpty(taskFilename) &&
                File.Exists(taskFilename)) {
                this.Name = "?";
                this.TaskMovieFilename = string.Empty;
                this.SolutionImageFilename = string.Empty;
            }
            else {
                this.Name = Path.GetFileNameWithoutExtension(taskFilename);
                this.SolutionImageFilename = taskFilename;
                string directory = Path.GetDirectoryName(taskFilename);
                string movieFilename = Path.Combine(Path.GetDirectoryName(taskFilename), this.Name + ".mov");
                if (File.Exists(movieFilename)) this.TaskMovieFilename = movieFilename;
                else this.TaskMovieFilename = string.Empty;
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

        private int textInsertPositionX = 0;
        public int TextInsertPositionX {
            get { return this.textInsertPositionX; }
            set {
                if (this.textInsertPositionX != value) {
                    this.textInsertPositionX = value;
                    this.on_PropertyChanged();
                    this.Vfullscreen_SetTextInsert();
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
                    this.Vfullscreen_SetTextInsert();
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
                    this.Vfullscreen_SetTextInsert();
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
        [Serialization.NotSerialized]
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

        private int? leftPlayerAngle = null;
        [XmlIgnore]
        public int? LeftPlayerAngle {
            get { return this.leftPlayerAngle; }
            set {
                if (this.leftPlayerAngle != value) {
                    if (value.HasValue) this.leftPlayerAngle = DatasetContent.GetValidatetAngle(value.Value);
                    else this.leftPlayerAngle = null;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? rightPlayerAngle = null;
        [XmlIgnore]
        public int? RightPlayerAngle {
            get { return this.rightPlayerAngle; }
            set {
                if (this.rightPlayerAngle != value) {
                    if (value.HasValue) this.rightPlayerAngle = DatasetContent.GetValidatetAngle(value.Value);
                    else this.rightPlayerAngle = null;
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.RotateImageScore'", typeIdentifier);
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
            this.LeftPlayerAngle = null;
            this.RightPlayerAngle = null;
            this.TaskCounter = 0;
            this.SelectDataset(0);
        }

        internal void Resolve() {
            if (this.LeftPlayerAngle.HasValue &&
                this.RightPlayerAngle.HasValue &&
                (this.SampleIncluded || this.TaskCounter > 0)) {
                this.LeftPlayerScore += Math.Abs(this.LeftPlayerAngle.Value / 10);
                this.RightPlayerScore += Math.Abs(this.RightPlayerAngle.Value / 10);
            }
        }

        public override void Next() {
            base.Next();
            this.LeftPlayerAngle = null;
            this.RightPlayerAngle = null;
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
        public void Vinsert_BorderOut() {
            if (this.insertScene is VRemote4.HandlerSi.Scene &&
                this.insertScene.Border is VentuzScenes.GamePool._Modules.Border) this.insertScene.Border.ToOut();
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
                this.Vfullscreen_SetContent(
                    this.fullscreenScene,
                    this.SelectedDataset.TaskMovieFilename,
                    this.SelectedDataset.TaskAngle,
                    this.SelectedDataset.SolutionImageFilename);
                this.Vfullscreen_SetContent(
                    this.fullscreenScene,
                    this.LeftPlayerAngle,
                    this.RightPlayerAngle);
                this.Vfullscreen_SetTextInsert(
                    this.fullscreenScene.TextInsert,
                    this.SelectedDataset.Name);
                this.fullscreenScene.TextInsert.SetIn();
                this.fullscreenScene.ToIn();
            }
        }
        public void Vfullscreen_SetContent(
            Fullscreen scene,
            string taskMovieFilename,
            int taskAngle,
            string solutionImageFilename) {
            if (scene is Fullscreen) {
                scene.SetTaskMovieFilename(taskMovieFilename);
                scene.SetTaskAngle(taskAngle);
                scene.SetSolutionImageFilename(solutionImageFilename);
            }            
        }
        public void Vfullscreen_SetContent(
            Fullscreen scene,
            int? leftPlayerAngle,
            int? rightPlayerAngle) {
            if (scene is Fullscreen) {
                if (leftPlayerAngle.HasValue) scene.SetLeftPlayerAngle(leftPlayerAngle.Value);
                if (rightPlayerAngle.HasValue) scene.SetRightPlayerAngle(rightPlayerAngle.Value);
            }
        }
        public void Vfullscreen_SetTextInsert() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene && this.SelectedDataset is DatasetContent) this.Vfullscreen_SetTextInsert(this.fullscreenScene.TextInsert, this.SelectedDataset.Name); }
        public void Vfullscreen_SetTextInsert(
            VentuzScenes.GamePool._Modules.TextInsert scene,
            string text) {
            if (scene is VentuzScenes.GamePool._Modules.TextInsert) {
                scene.SetPositionX(this.TextInsertPositionX);
                scene.SetPositionY(this.TextInsertPositionY);
                scene.SetStyle(this.TextInsertStyle);
                scene.SetText(text);
            }
        }

        internal void Vfullscreen_ShowPlayerInput() {
            this.Vfullscreen_SetContent(
                this.fullscreenScene,
                this.LeftPlayerAngle,
                this.RightPlayerAngle);
            if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.SolutionToIn();
        }
        public void Vfullscreen_ShowSolution() {
            this.Vfullscreen_SetContent(
                this.fullscreenScene,
                this.LeftPlayerAngle,
                this.RightPlayerAngle);
            if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.PlayerIn();
        }
        public void Vfullscreen_Resolve() {
            if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.StartSolution();
        }
        public void Vfullscreen_ContentOut() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.ToOut(); }
        public override void Vfullscreen_UnloadScene() { this.fullscreenScene.Unload(); }

        public override void Vhost_LoadScene() { this.hostScene.Load(); }
        public void Vhost_In() {
            this.Vhost_Set();
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.ToIn();
        }
        public void Vhost_Set() { 
            if ( this.SelectedDataset is DatasetContent) { 
                this.Vhost_Set(
                    this.hostScene, 
                    this.TaskCounter, 
                    this.SelectedDataset.TaskMovieFilename, 
                    this.SelectedDataset.SolutionImageFilename,
                    this.SelectedDataset.TaskAngle,
                    this.SelectedDataset.HostText, 
                    this.LeftPlayerAngle, 
                    this.RightPlayerAngle); 
            } 
        }
        public void Vhost_Set(
            Host scene,
            int counter,
            string movieFilename,
            string imageFilename,
            int angle,
            string hostText,
            int? leftPlayerAngle,
            int? rightPlayerAngle) {
            if (counter == 0) hostText = string.Format("Beispiel: {0}", hostText);
            else if (counter > 0 && counter <= this.TaskCounterSize) hostText = string.Format("{0}/{1}: {2}", counter.ToString(), this.TaskCounterSize.ToString(), hostText);
            string leftPlayerText = "?";
            if (leftPlayerAngle.HasValue) leftPlayerText = string.Format("{0}°", leftPlayerAngle.ToString());
            string rightPlayerText = "?";
            if (rightPlayerAngle.HasValue) rightPlayerText = string.Format("{0}°", rightPlayerAngle.ToString());
            if (scene is Host) {
                scene.SetMovieFilename(movieFilename);
                scene.SetImageFilename(imageFilename);
                scene.SetAngle(angle);
                scene.SetHostText(hostText);
                scene.SetLeftInput(leftPlayerText);
                scene.SetRightInput(rightPlayerText);
            }
        }
        internal void Vhost_SetInput() {
            if (this.SelectedDataset is DatasetContent) {
                this.Vhost_Set(
                    this.hostScene,
                    this.TaskCounter,
                    this.SelectedDataset.TaskMovieFilename,
                    this.SelectedDataset.SolutionImageFilename,
                    this.SelectedDataset.TaskAngle,
                    this.SelectedDataset.HostText,
                    this.LeftPlayerAngle,
                    this.RightPlayerAngle);
            }
        }
        public void Vhost_Out() { if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.ToOut(); }
        public override void Vhost_UnloadScene() { this.hostScene.Unload(); }

        public void Vplayers_In() {
            this.Vleftplayer_In();
            this.Vrightplayer_In();
        }
        internal void Vplayers_EnableInput() {
            this.Vleftplayer_EnableInput();
            this.Vrightplayer_EnableInput();
        }
        internal void Vplayers_DisableInput() {
            this.Vleftplayer_DisableInput();
            this.Vrightplayer_DisableInput();
        }
        public void Vplayers_Out() {
            this.Vleftplayer_Out();
            this.Vrightplayer_Out();
        }

        public override void Vleftplayer_LoadScene() { this.leftPlayerScene.Load(); }
        public void Vleftplayer_In() {
            this.Vleftplayer_Set();
            if (this.leftPlayerScene is Player) this.leftPlayerScene.ToIn();
        }
        internal void Vleftplayer_Set() { if (this.SelectedDataset is DatasetContent) this.Vplayer_Set(this.leftPlayerScene, this.SelectedDataset.TaskMovieFilename, this.SelectedDataset.Name, this.SelectedDataset.TaskAngle); }
        internal void Vleftplayer_EnableInput() { 
            if (this.leftPlayerScene is Player) this.leftPlayerScene.EnableTouch();
            this.LeftPlayerAngle = null;
        }
        internal void Vleftplayer_DisableInput() { if (this.leftPlayerScene is Player) this.leftPlayerScene.DisableTouch(); }
        public void Vleftplayer_Out() { if (this.leftPlayerScene is Player) this.leftPlayerScene.ToOut(); }
        public override void Vleftplayer_UnloadScene() { this.leftPlayerScene.Unload(); }

        public override void Vrightplayer_LoadScene() { this.rightPlayerScene.Load(); }
        public void Vrightplayer_In() {
            this.Vrightplayer_Set();
            if (this.rightPlayerScene is Player) this.rightPlayerScene.ToIn();
        }
        internal void Vrightplayer_Set() { if (this.SelectedDataset is DatasetContent) this.Vplayer_Set(this.rightPlayerScene, this.SelectedDataset.TaskMovieFilename, this.SelectedDataset.Name, this.SelectedDataset.TaskAngle); }
        internal void Vrightplayer_EnableInput() { 
            if (this.rightPlayerScene is Player) this.rightPlayerScene.EnableTouch();
            this.RightPlayerAngle = null;
        }
        internal void Vrightplayer_DisableInput() { if (this.rightPlayerScene is Player) this.rightPlayerScene.DisableTouch(); }
        public void Vrightplayer_Out() { if (this.rightPlayerScene is Player) this.rightPlayerScene.ToOut(); }
        public override void Vrightplayer_UnloadScene() { this.rightPlayerScene.Unload(); }

        internal void Vplayer_Set(
            Player scene,
            string movieFilename,
            string name,
            int angle) {
            if (scene is Player) {
                scene.SetMovieFilename(movieFilename);
                scene.SetName(name);
                scene.SetAngle(angle);
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

        private void leftPlayerScene_OKPressed(object sender, int e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftPlayerScene_OKPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftPlayerScene_OKPressed(object content) {
            if (content is int) this.LeftPlayerAngle = Convert.ToInt32(content);
            else this.LeftPlayerAngle = null;
            this.Vhost_SetInput();
        }

        private void rightPlayerScene_OKPressed(object sender, int e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerScene_OKPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerScene_OKPressed(object content) {
            if (content is int) this.RightPlayerAngle = Convert.ToInt32(content);
            else this.RightPlayerAngle = null;
            this.Vhost_SetInput();
        }

        #endregion

    }
}
