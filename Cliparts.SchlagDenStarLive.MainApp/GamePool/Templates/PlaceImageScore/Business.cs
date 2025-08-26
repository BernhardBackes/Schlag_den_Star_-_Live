using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.PlaceImageScore;
using Cliparts.VRemote4.HandlerSi;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.PlaceImageScore {

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
                    if (string.IsNullOrEmpty(value)) value = "?";
                    else this.name = value;
                    this.buildToString();
                    this.on_PropertyChanged();
                }
            }
        }

        private double positionX;
        public double PositionX {
            get { return this.positionX; }
            set {
                if (this.positionX != value) {
                    this.positionX = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private double positionY;
        public double PositionY {
            get { return this.positionY; }
            set {
                if (this.positionY != value) {
                    this.positionY = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string filenameOrange = string.Empty;
        public string FilenameOrange {
            get { return this.filenameOrange; }
            set {
                if (this.filenameOrange != value) {
                    if (string.IsNullOrEmpty(value)) this.filenameOrange = string.Empty;
                    else this.filenameOrange = value;
                    this.PictureOrange = Helper.getThumbnailFromMediaFile(value, 1.0f);
                    this.on_PropertyChanged();
                }
            }
        }
        [XmlIgnore]
        public Image PictureOrange { get; private set; }

        private string filenameBlue = string.Empty;
        public string FilenameBlue {
            get { return this.filenameBlue; }
            set {
                if (this.filenameBlue != value) {
                    if (string.IsNullOrEmpty(value)) this.filenameBlue = string.Empty;
                    else this.filenameBlue = value;
                    this.PictureBlue = Helper.getThumbnailFromMediaFile(value, 1.0f);
                    this.on_PropertyChanged();
                }
            }
        }
        [XmlIgnore]
        public Image PictureBlue { get; private set; }

        private string filenameRed = string.Empty;
        public string FilenameRed {
            get { return this.filenameRed; }
            set {
                if (this.filenameRed != value) {
                    if (string.IsNullOrEmpty(value)) this.filenameRed = string.Empty;
                    else this.filenameRed = value;
                    this.PictureRed = Helper.getThumbnailFromMediaFile(value, 1.0f);
                    this.on_PropertyChanged();
                }
            }
        }
        [XmlIgnore]
        public Image PictureRed{ get; private set; }

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DatasetContent() { }
        public DatasetContent(
            string filename) {
            if (string.IsNullOrEmpty(filename) || !File.Exists(filename)) this.Name = "?";
            else {
                this.Name = Path.GetFileNameWithoutExtension(filename);
                int index = this.Name.LastIndexOf('-');
                if (index > 0) this.Name = this.Name.Substring(0, index);
                string extension = Path.GetExtension(filename);
                string directory = Path.GetDirectoryName(filename);
                filename = Path.Combine(directory, this.Name + "-Orange" + extension);
                if (File.Exists(filename)) this.FilenameOrange = filename;
                filename = Path.Combine(directory, this.Name + "-Blau" + extension);
                if (File.Exists(filename)) this.FilenameBlue = filename;
                filename = Path.Combine(directory, this.Name + "-Rot" + extension);
                if (File.Exists(filename)) this.FilenameRed = filename;
            }
            this.buildToString();
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

        private double leftPlayerPositionX = 0;
        [XmlIgnore]
        public double LeftPlayerPositionX {
            get { return this.leftPlayerPositionX; }
            set {
                if (this.leftPlayerPositionX != value) {
                    this.leftPlayerPositionX = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private double leftPlayerPositionY = 0;
        [XmlIgnore]
        public double LeftPlayerPositionY {
            get { return this.leftPlayerPositionY; }
            set {
                if (this.leftPlayerPositionY != value) {
                    this.leftPlayerPositionY = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private double? leftPlayerDistance = null;
        public string LeftPlayerDistance {
            get {
                if (this.leftPlayerDistance.HasValue) return string.Format("{0} km", this.leftPlayerDistance.Value.ToString("#,##0"));
                else return string.Empty;
            }
        }

        private double rightPlayerPositionX = 0;
        [XmlIgnore]
        public double RightPlayerPositionX {
            get { return this.rightPlayerPositionX; }
            set {
                if (this.rightPlayerPositionX != value) {
                    this.rightPlayerPositionX = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private double rightPlayerPositionY = 0;
        [XmlIgnore]
        public double RightPlayerPositionY {
            get { return this.rightPlayerPositionY; }
            set {
                if (this.rightPlayerPositionY != value) {
                    this.rightPlayerPositionY = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private double? rightPlayerDistance = null;
        public string RightPlayerDistance {
            get {
                if (this.rightPlayerDistance.HasValue) return string.Format("{0} km", this.rightPlayerDistance.Value.ToString("#,##0"));
                else return string.Empty;
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

        private int taskCounterSize = 13;
        public int TaskCounterSize {
            get { return this.taskCounterSize; }
            set {
                if (this.taskCounterSize != value) {
                    if (value < 0) value = 0;
                    this.taskCounterSize = value;
                    this.on_PropertyChanged();
                }
            }
        }

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

        private Stage hostScene;
        public override VRemote4.HandlerSi.Scene.States HostSceneStatus {
            get {
                if (this.hostScene is Stage) return this.hostScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private Stage leftPlayerScene;
        public override VRemote4.HandlerSi.Scene.States LeftPlayerSceneStatus {
            get {
                if (this.leftPlayerScene is Stage) return this.leftPlayerScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private Stage rightPlayerScene;
        public override VRemote4.HandlerSi.Scene.States RightPlayerSceneStatus {
            get {
                if (this.rightPlayerScene is Stage) return this.rightPlayerScene.Status;
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.PlaceImageScore'", typeIdentifier);
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

            this.hostScene = new Stage(syncContext, hostMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.hostScene.StatusChanged += this.hostScene_StatusChanged;

            this.leftPlayerScene = new Stage(syncContext, leftPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.leftPlayerScene.StatusChanged += this.leftPlayerScene_StatusChanged;
            this.leftPlayerScene.OKButtonPressed += this.leftPlayerScene_OKButtonPressed;

            this.rightPlayerScene = new Stage(syncContext, rightPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.rightPlayerScene.StatusChanged += this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.OKButtonPressed += this.rightPlayerScene_OKButtonPressed;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);
        }

        public override void Dispose() {
            base.Dispose();
            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.fullscreenScene.StatusChanged -= this.fullscreenScene_StatusChanged;
            this.fullscreenScene.Dispose();
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
            this.resetLeftPlayerValues();
            this.resetRightPlayerValues();
            this.TaskCounter = 0;
            this.SelectDataset(0);
        }

        private void resetLeftPlayerValues() {
            this.LeftPlayerPositionX = 0;
            this.LeftPlayerPositionY = 0;
            this.leftPlayerDistance = null;
            this.on_PropertyChanged("RightPlayerDistance");
        }
        internal void LeftPlayerUnlockTouch() {
            this.resetLeftPlayerValues();
            this.Vleftplayer_UnlockTouch();
        }
        internal void LeftPlayerLockTouch() {
            this.Vleftplayer_LockTouch();
        }
        internal void LeftPlayerResetInput() {
            this.resetLeftPlayerValues();
            this.Vleftplayer_ResetInput();
        }
        internal void CalcLeftPlayerDistance() {
            this.leftPlayerDistance = distance(this.leftPlayerPositionX, this.leftPlayerPositionY);
            this.on_PropertyChanged("LeftPlayerDistance");
        }

        private void resetRightPlayerValues() {
            this.RightPlayerPositionX = 0;
            this.RightPlayerPositionY = 0;
            this.rightPlayerDistance = null;
            this.on_PropertyChanged("RightPlayerDistance");
        }
        internal void RightPlayerUnlockTouch() {
            this.resetRightPlayerValues();
            this.Vrightplayer_UnlockTouch();
        }
        internal void RightPlayerLockTouch() {
            this.Vrightplayer_LockTouch();
        }
        internal void RightPlayerResetInput() {
            this.resetRightPlayerValues();
            this.Vrightplayer_ResetInput();
        }
        internal void CalcRightPlayerDistance() {
            this.rightPlayerDistance = distance(this.rightPlayerPositionX, this.rightPlayerPositionY);
            this.on_PropertyChanged("RightPlayerDistance");
        }

        private double distance(
            double x, double y) {
            if (this.SelectedDataset is DatasetContent) {
                CoordinateSet source = new CoordinateSet(x, y);
                CoordinateSet target = new CoordinateSet(this.SelectedDataset.PositionX, this.SelectedDataset.PositionY);
                return Transformation.GetDistance(source, target);
            }
            else return 0;
        }

        internal void Resolve() {
            if (this.TaskCounter > 0) {
                if (this.leftPlayerDistance.HasValue &&
                    this.rightPlayerDistance.HasValue) {
                    if (this.leftPlayerDistance.Value < this.rightPlayerDistance.Value) this.LeftPlayerScore++;
                    else if (this.leftPlayerDistance.Value > this.rightPlayerDistance.Value) this.RightPlayerScore++;
                    else {
                        this.LeftPlayerScore++;
                        this.RightPlayerScore++;
                    }
                }
                else if (this.leftPlayerDistance.HasValue) this.LeftPlayerScore++;
                else if (this.rightPlayerDistance.HasValue) this.RightPlayerScore++;
            }
        }

        public override void Next() {
            base.Next();
            this.resetLeftPlayerValues();
            this.resetRightPlayerValues();
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
            if (this.tryAddDataset(newDataset, insertIndex)) {
                this.on_PropertyChanged("NameList");
                this.Save();
            }
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
        public override void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score); }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }
        internal void Vinsert_MapIn() {
            if (this.insertScene.Game is Game) {
                this.setMap(this.insertScene.Game, false);
                this.insertScene.Game.SetSelectionColor(Game.ColorElements.Orange);
                this.insertScene.Game.SetMapBlocked(false);
                this.insertScene.Game.SetAudioEnabled(true);
                this.insertScene.Game.SetVisualisation(Game.VisualisationElements.Name);
                this.insertScene.Game.ToIn();
            }
        }
        internal void Vinsert_BlueIn() { this.sceneBlueIn(this.insertScene.Game); }
        internal void Vinsert_RedIn() { this.sceneRedIn(this.insertScene.Game); }
        internal void Vinsert_SolutionIn() { this.sceneSolutionIn(this.insertScene.Game); }
        internal void Vinsert_MapOut() { if (this.insertScene.Game is Game) this.insertScene.Game.ToOut(); }
        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void Vfullscreen_LoadScene() { this.fullscreenScene.Load(); }
        internal void Vfullscreen_MapIn(
            bool asSample) {
            if (this.fullscreenScene.Game is Game) {
                this.setMap(this.fullscreenScene.Game, false);
                this.fullscreenScene.Game.SetSelectionColor(Game.ColorElements.Orange);
                this.fullscreenScene.Game.SetMapBlocked(true);
                this.fullscreenScene.Game.SetAudioEnabled(false);
                if (asSample) this.fullscreenScene.Game.SetVisualisation(Game.VisualisationElements.Out);
                else this.fullscreenScene.Game.SetVisualisation(Game.VisualisationElements.Selection);
                this.fullscreenScene.Game.ToIn();
            }
        }
        internal void Vfullscreen_BlueIn() { this.sceneBlueIn(this.fullscreenScene.Game); }
        internal void Vfullscreen_RedIn() { this.sceneRedIn(this.fullscreenScene.Game); }
        internal void Vfullscreen_SolutionIn() { this.sceneSolutionIn(this.fullscreenScene.Game); }
        internal void Vfullscreen_MapOut() { if (this.fullscreenScene.Game is Game) this.fullscreenScene.Game.ToOut(); }
        public override void Vfullscreen_UnloadScene() { this.fullscreenScene.Unload(); }

        public override void Vhost_LoadScene() { this.hostScene.Load(); }
        internal void Vhost_MapIn(
            bool asSample) {
            if (this.hostScene is Game) {
                this.setMap(this.hostScene, true);
                this.hostScene.SetSelectionColor(Game.ColorElements.Orange);
                this.hostScene.SetMapBlocked(true);
                if (asSample) this.hostScene.SetVisualisation(Game.VisualisationElements.Out);
                else this.hostScene.SetVisualisation(Game.VisualisationElements.Selection);
                this.hostScene.ToIn();
            }
        }
        internal void Vhost_BlueIn() { this.sceneBlueIn(this.hostScene); }
        internal void Vhost_RedIn() { this.sceneRedIn(this.hostScene); }
        internal void Vhost_SolutionIn() { this.sceneSolutionIn(this.hostScene); }
        internal void Vhost_MapOut() { if (this.hostScene is Game) this.hostScene.ToOut(); }
        public override void Vhost_UnloadScene() { this.hostScene.Unload(); }

        public override void Vleftplayer_LoadScene() { this.leftPlayerScene.Load(); }
        internal void Vleftplayer_MapIn(
            bool asSample) {
            if (this.leftPlayerScene is Game) {
                this.setMap(this.leftPlayerScene, false);
                this.leftPlayerScene.SetSelectionColor(Game.ColorElements.Red);
                this.leftPlayerScene.SetMapBlocked(true);
                if (asSample) this.leftPlayerScene.SetVisualisation(Game.VisualisationElements.Out);
                else this.leftPlayerScene.SetVisualisation(Game.VisualisationElements.Selection);
                this.leftPlayerScene.ToIn();
            }
        }
        internal void Vleftplayer_UnlockTouch() { if (this.rightPlayerScene is Game) this.leftPlayerScene.EnableSelection(); }
        internal void Vleftplayer_LockTouch() { if (this.rightPlayerScene is Game) this.leftPlayerScene.DisableSelection(); }
        internal void Vleftplayer_ResetInput() { if (this.rightPlayerScene is Game) this.leftPlayerScene.ResetSelection(); }
        internal void Vleftplayer_MapOut() { if (this.rightPlayerScene is Game) this.leftPlayerScene.ToOut(); }
        public override void Vleftplayer_UnloadScene() { if (this.rightPlayerScene is Game) this.leftPlayerScene.Unload(); }

        public override void Vrightplayer_LoadScene() { this.rightPlayerScene.Load(); }
        internal void Vrightplayer_MapIn(
            bool asSample) {
            if (this.rightPlayerScene is Game) {
                this.setMap(this.rightPlayerScene, false);
                this.rightPlayerScene.SetSelectionColor(Game.ColorElements.Blue);
                this.rightPlayerScene.SetMapBlocked(true);
                if (asSample) this.rightPlayerScene.SetVisualisation(Game.VisualisationElements.Out);
                else this.rightPlayerScene.SetVisualisation(Game.VisualisationElements.Selection);
                this.rightPlayerScene.ToIn();
            }
        }
        internal void Vrightplayer_UnlockTouch() { if (this.rightPlayerScene is Game) this.rightPlayerScene.EnableSelection(); }
        internal void Vrightplayer_LockTouch() { if (this.rightPlayerScene is Game) this.rightPlayerScene.DisableSelection(); }
        internal void Vrightplayer_ResetInput() { if (this.rightPlayerScene is Game) this.rightPlayerScene.ResetSelection(); }
        internal void Vrightplayer_MapOut() { if (this.rightPlayerScene is Game) this.rightPlayerScene.ToOut(); }
        public override void Vrightplayer_UnloadScene() { this.rightPlayerScene.Unload(); }

        private void setMap(
            Game scene,
            bool isHost) {
            if (scene is Game) { 
                if (this.SelectedDataset is DatasetContent) {
                    string name = string.Empty;
                    if (isHost) {
                        if (this.TaskCounter == 0) name = "Bsp: ";
                        else if (this.TaskCounter > 0 && this.TaskCounter <= this.TaskCounterSize) name = string.Format("{0}/{1}: ", this.TaskCounter.ToString(), this.TaskCounterSize.ToString());
                    }
                    name += this.SelectedDataset.Name;
                    scene.SetName(name);
                    scene.SetSolutionFile(this.SelectedDataset.FilenameOrange);
                    scene.SetRedFile(this.SelectedDataset.FilenameRed);
                    scene.SetBlueFile(this.SelectedDataset.FilenameBlue);
                }
                scene.ResetSelection();
                scene.ResetSolution();
                scene.ResetInputRed();
                scene.ResetInputBlue();
                scene.SetResultLeftPlayerName(this.LeftPlayerName);
                scene.SetResultLeftPlayerDistance(0);
                scene.SetResultRightPlayerName(this.RightPlayerName);
                scene.SetResultRightPlayerDistance(0);
            }
        }

        private void sceneBlueIn(
            Game scene) {
            if (scene is Game) {
                scene.SetInputBlueX(this.RightPlayerPositionX);
                scene.SetInputBlueY(this.RightPlayerPositionY);
                scene.InputBlueToIn();
            }
        }

        private void sceneRedIn(
            Game scene) {
            if (scene is Game) {
                scene.SetInputRedX(this.LeftPlayerPositionX);
                scene.SetInputRedY(this.LeftPlayerPositionY);
                scene.InputRedToIn();
            }
        }

        private void sceneSolutionIn(
            Game scene) {
            if (scene is Game) {
                scene.SetSolutionX(this.SelectedDataset.PositionX);
                scene.SetSolutionY(this.SelectedDataset.PositionY);
                scene.SolutionToIn();
            }
            if (this.leftPlayerDistance.HasValue) scene.SetResultLeftPlayerDistance(Convert.ToInt32(this.leftPlayerDistance.Value));
            else scene.SetResultLeftPlayerDistance(0);
            if (this.rightPlayerDistance.HasValue) scene.SetResultRightPlayerDistance(Convert.ToInt32(this.rightPlayerDistance.Value));
            else scene.SetResultRightPlayerDistance(0);
            scene.SetVisualisation(Game.VisualisationElements.Result);
        }

        public override void ClearGraphic() {
            base.ClearGraphic();
            this.insertScene.Clear();
            this.fullscreenScene.Clear();
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
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "Name") {
                    this.buildNameList();
                    this.on_PropertyChanged("NameList");
                }
                else if (e.PropertyName == "PositionX" ||
                    e.PropertyName == "PositionY") this.Vinsert_SolutionIn();
            }
            this.Save();
        }

        void leftPlayerScene_OKButtonPressed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftPlayerScene_OKButtonPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftPlayerScene_OKButtonPressed(object content) {
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.LeftPlayerPositionX = this.leftPlayerScene.ImagePositionX;
                this.LeftPlayerPositionY = this.leftPlayerScene.ImagePositionY;
                this.CalcLeftPlayerDistance();
                this.Vhost_RedIn();
            }
        }

        void rightPlayerScene_OKButtonPressed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerScene_OKButtonPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerScene_OKButtonPressed(object content) {
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.RightPlayerPositionX = this.rightPlayerScene.ImagePositionX;
                this.RightPlayerPositionY = this.rightPlayerScene.ImagePositionY;
                this.CalcRightPlayerDistance();
                this.Vhost_BlueIn();
            }
        }

        #endregion

    }
}
