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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.Geography;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.Geography {

    public class DatasetContent : Messaging.Message, INotifyPropertyChanged {

        #region Properties

        private string name = string.Empty;
        public string Name {
            get { return this.name; }
            set {
                if (this.name != value) {
                    if (value == null) value = string.Empty;
                    this.name = Regex.Replace(value, "(?<!\r)\n", "\r\n");
                    this.buildToString();
                    this.on_PropertyChanged();
                }
            }
        }

        private Fullscreen.MapLayoutElements mapLayout;
        public Fullscreen.MapLayoutElements MapLayout {
            get { return this.mapLayout; }
            set {
                if (this.mapLayout != value) {
                    this.mapLayout = value;
                    this.on_PropertyChanged();
                }
                if (this.coordinates is CoordinateSet) this.coordinates.MapLayout = value;
            }
        }

        private string insertText = string.Empty;
        public string InsertText {
            get { return this.insertText; }
            set {
                if (this.insertText != value) {
                    if (value == null) value = string.Empty;
                    this.insertText = Regex.Replace(value, "(?<!\r)\n", "\r\n");
                    this.on_PropertyChanged();
                    if (string.IsNullOrEmpty(this.FullText)) this.FullText = value;
                    if (string.IsNullOrEmpty(this.HostText)) this.HostText = value;
                }
            }
        }

        private string fullText = string.Empty;
        public string FullText {
            get { return this.fullText; }
            set {
                if (this.fullText != value) {
                    if (value == null) value = string.Empty;
                    this.fullText = Regex.Replace(value, "(?<!\r)\n", "\r\n");
                    this.on_PropertyChanged();
                    if (string.IsNullOrEmpty(this.InsertText)) this.InsertText = value;
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
                    this.hostText = Regex.Replace(value, "(?<!\r)\n", "\r\n");
                    this.on_PropertyChanged();
                    if (string.IsNullOrEmpty(this.InsertText)) this.InsertText = value;
                    if (string.IsNullOrEmpty(this.FullText)) this.FullText = value;
                }
            }
        }

        private CoordinateSet coordinates = new CoordinateSet(Fullscreen.MapLayoutElements.Africa);
        public CoordinateSet Coordinates {
            get { return this.coordinates; }
            set { 
                if (!(value is CoordinateSet)) value = new CoordinateSet(this.MapLayout);
                if (this.coordinates.Longitude.Text != value.Longitude.Text ||
                    this.coordinates.Latitude.Text != value.Latitude.Text) {
                    this.coordinates.Longitude.Text = value.Longitude.Text;
                    this.coordinates.Latitude.Text = value.Latitude.Text;
                    this.on_PropertyChanged();
                }
            }
        }

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DatasetContent() { this.pose(); }
        public DatasetContent(
            Fullscreen.MapLayoutElements mapLayout) {
            this.MapLayout = mapLayout;
            this.Name = "Köln";
            this.Coordinates = new CoordinateSet(this.MapLayout, new Coordinate("50° 56' 17\" N"), new Coordinate("6° 57' 25\" E"));
            this.pose();
        }
        public DatasetContent(
            string name,
            Fullscreen.MapLayoutElements mapLayout,
            string insertText,
            string fullText,
            string hostInfo,
            CoordinateSet coordinates) {
            this.MapLayout = mapLayout;
            this.name = name;
            this.insertText = insertText;
            this.fullText = fullText;
            this.hostText = hostInfo;
            this.Coordinates = coordinates;
            this.pose();
        }
        private void pose() {
            this.coordinates.PropertyChanged += this.coordinates_PropertyChanged;
        }

        public void Dispose() {
            this.coordinates.PropertyChanged += this.coordinates_PropertyChanged;
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

        void coordinates_PropertyChanged(object sender, PropertyChangedEventArgs e) { this.on_PropertyChanged(sender, e); }

        #endregion
    }

    public class Data {
        public DatasetContent[] DataList;
        public int SelectedIndex;
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

        private CoordinateSet leftPlayerInput = null;
        public String LeftPlayerInput {
            get {
                if (this.leftPlayerInput is CoordinateSet) return this.leftPlayerInput.Text;
                else return string.Empty;
            }
            private set {
                if (this.LeftPlayerInput != value) {
                    if (string.IsNullOrEmpty(value)) this.leftPlayerInput = null;
                    else {
                        if (this.leftPlayerInput is CoordinateSet) this.leftPlayerInput.Text = value;
                        else this.leftPlayerInput = new CoordinateSet(this.mapLayout, value);
                    }
                }
                this.on_PropertyChanged();
                this.calcPlayerDistances();
            }
        }

        private int? leftPlayerDistance = null;
        public String LeftPlayerDistance {
            get {
                if (this.leftPlayerDistance.HasValue) return string.Format("{0} km", this.leftPlayerDistance.Value.ToString("#,##0"));
                else return string.Empty;
            }
        }

        private CoordinateSet rightPlayerInput = null;
        public String RightPlayerInput {
            get {
                if (this.rightPlayerInput is CoordinateSet) return this.rightPlayerInput.Text;
                else return string.Empty;
            }
            private set {
                if (this.RightPlayerInput != value) {
                    if (string.IsNullOrEmpty(value)) this.rightPlayerInput = null;
                    else {
                        if (this.rightPlayerInput is CoordinateSet) this.rightPlayerInput.Text = value;
                        else this.rightPlayerInput = new CoordinateSet(this.mapLayout, value);
                    }
                }
                this.on_PropertyChanged();
                this.calcPlayerDistances();
            }
        }

        private int? rightPlayerDistance = null;
        public String RightPlayerDistance {
            get {
                if (this.rightPlayerDistance.HasValue) return string.Format("{0} km", this.rightPlayerDistance.Value.ToString("#,##0"));
                else return string.Empty;
            }
        }

        private Content.Gameboard.PlayerSelection closerPlayer = Content.Gameboard.PlayerSelection.LeftPlayer;
        public Content.Gameboard.PlayerSelection CloserPlayer {
            get { return this.closerPlayer; }
            private set {
                if (this.closerPlayer != value) {
                    this.closerPlayer = value;
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

        private Fullscreen.MapLayoutElements mapLayout {
            get {
                if (this.SelectedDataset is DatasetContent) return this.SelectedDataset.MapLayout;
                else return Fullscreen.MapLayoutElements.Africa;
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.Geography'", typeIdentifier);
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

            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Dispose();
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
            this.TaskCounter = 0;
            this.SelectDataset(0);
            this.resetInput();
        }

        public override void Next() {
            base.Next();
            this.TaskCounter++;
            this.resetInput();
            this.SelectDataset(this.SelectedDatasetIndex + 1);
        }

        public void Resolve() {
            if (this.leftPlayerDistance.HasValue &&
                this.rightPlayerDistance.HasValue &&
                (!this.SampleIncluded || (this.SampleIncluded && this.SelectedDatasetIndex > 0))) {
                if (this.leftPlayerDistance.Value < this.rightPlayerDistance.Value) this.LeftPlayerScore++;
                else if (this.rightPlayerDistance.Value < this.leftPlayerDistance.Value) this.RightPlayerScore++;
            }
        }

        public void SetLeftPlayerInput(string text) { 
            this.LeftPlayerInput = text;
            this.Vhost_SetMap();
        }
        public void SetRightPlayerInput(string text) { 
            this.RightPlayerInput = text;
            this.Vhost_SetMap();
        }

        private void resetInput() {
            this.LeftPlayerInput = null;
            this.RightPlayerInput = null;
        }

        private void calcPlayerDistances() {
            int? leftPlayerDistance = null;
            int? rightPlayerDistance = null;
            if (this.SelectedDataset is DatasetContent) {
                if (this.leftPlayerInput is CoordinateSet) leftPlayerDistance = Convert.ToInt32(Math.Truncate(Transformation.GetDistance(this.leftPlayerInput, this.SelectedDataset.Coordinates)));
                if (this.rightPlayerInput is CoordinateSet) rightPlayerDistance = Convert.ToInt32(Math.Truncate(Transformation.GetDistance(this.rightPlayerInput, this.SelectedDataset.Coordinates)));
            }
            if (this.leftPlayerDistance != leftPlayerDistance) {
                this.leftPlayerDistance = leftPlayerDistance;
                this.on_PropertyChanged("LeftPlayerDistance");
            }
            if (this.rightPlayerDistance != rightPlayerDistance) {
                this.rightPlayerDistance = rightPlayerDistance;
                this.on_PropertyChanged("RightPlayerDistance");
            }
            if (leftPlayerDistance.HasValue &&
                rightPlayerDistance.HasValue) {
                if (leftPlayerDistance.Value < rightPlayerDistance.Value) this.CloserPlayer = Content.Gameboard.PlayerSelection.LeftPlayer;
                else if (rightPlayerDistance.Value < leftPlayerDistance.Value) this.CloserPlayer = Content.Gameboard.PlayerSelection.RightPlayer;
                else this.CloserPlayer = Content.Gameboard.PlayerSelection.NotSelected;
            }
            else this.CloserPlayer = Content.Gameboard.PlayerSelection.NotSelected;
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
            this.calcPlayerDistances();
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

        public override void Vinsert_LoadScene() {
            this.insertScene.Load();
        }

        public void Vinsert_TextInsertIn() {
            this.Vinsert_SetTextInsert();
            if (this.insertScene is VRemote4.HandlerSi.Scene &&
                this.insertScene.TextInsert is VentuzScenes.GamePool._Modules.TextInsert) this.insertScene.TextInsert.ToIn();
        }
        public void Vinsert_SetTextInsert() { if (this.insertScene is VRemote4.HandlerSi.Scene && this.SelectedDataset is DatasetContent) this.Vinsert_SetTextInsert(this.insertScene.TextInsert, this.SelectedDataset.InsertText); }
        public void Vinsert_SetTextInsert(
            VentuzScenes.GamePool._Modules.TextInsert scene,
            string text) {
            if (scene is VentuzScenes.GamePool._Modules.TextInsert) {
                scene.SetPositionX(this.TextInsertPositionX);
                scene.SetPositionY(this.TextInsertPositionY);
                scene.SetStyle(this.TextInsertStyle);
                scene.SetText(text);
            }
        }
        public void Vinsert_TextInsertOut() {
            if (this.insertScene is VRemote4.HandlerSi.Scene &&
                this.insertScene.TextInsert is VentuzScenes.GamePool._Modules.TextInsert) this.insertScene.TextInsert.ToOut();
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
        public override void Vinsert_UnloadScene() {
            this.insertScene.Unload();
        }

        public override void Vfullscreen_LoadScene() { this.fullscreenScene.Load(); }
        public override void Vfullscreen_ShowGame() {
            this.Vfullscreen_Reset();
            base.Vfullscreen_ShowGame();
        }
        public void Vfullscreen_TextIn() {
            this.Vfullscreen_SetMapLayout();
            this.Vfullscreen_SetText();
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.ShowTask();
        }
        public void Vfullscreen_SetMapLayout() {
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) this.fullscreenScene.SetMapLayout(this.SelectedDataset.MapLayout);
        }
        public void Vfullscreen_SetText() {
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) {
                this.fullscreenScene.SetTaskText(this.SelectedDataset.FullText);
                this.fullscreenScene.SetResultsBlueName(this.RightPlayerName);
                this.fullscreenScene.SetResultsRedName(this.LeftPlayerName);
            }
        }
        public void Vfullscreen_BluePinIn() {
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.rightPlayerInput is CoordinateSet &&
                this.rightPlayerDistance.HasValue) {
                this.fullscreenScene.SetResultsBlueName(this.RightPlayerName);
                this.fullscreenScene.SetResultsBlueDistance(this.rightPlayerDistance.Value);
                this.fullscreenScene.SetBluePinPosition(this.rightPlayerInput.HD_PositionX, this.rightPlayerInput.HD_PositionY);
                this.fullscreenScene.ShowBluePin();
            }
        }
        public void Vfullscreen_RedPinIn() {
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.leftPlayerInput is CoordinateSet &&
                this.leftPlayerDistance.HasValue) {
                this.fullscreenScene.SetResultsRedName(this.LeftPlayerName);
                this.fullscreenScene.SetResultsRedDistance(this.leftPlayerDistance.Value);
                this.fullscreenScene.SetRedPinPosition(this.leftPlayerInput.HD_PositionX, this.leftPlayerInput.HD_PositionY);
                this.fullscreenScene.ShowRedPin();
            }
        }
        public void Vfullscreen_SolutionIn() {
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) {
                this.fullscreenScene.SetResultsSolution(this.SelectedDataset.Name);
                this.fullscreenScene.SetYellowPinPosition(this.SelectedDataset.Coordinates.HD_PositionX, this.SelectedDataset.Coordinates.HD_PositionY);
                this.fullscreenScene.ShowYellowPin();
                this.fullscreenScene.ShowBlueDistance();
                this.fullscreenScene.ShowRedDistance();
                this.fullscreenScene.ShowResults();
            }
        }
        public void Vfullscreen_Reset() {
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.Reset();
        }
        public override void Vfullscreen_UnloadScene() { this.fullscreenScene.Unload(); }

        public override void Vhost_LoadScene() { this.hostScene.Load(); }
        public void Vhost_SetMap() {
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) {
                this.hostScene.SetMapLayout(this.SelectedDataset.MapLayout);
                this.hostScene.SetFullText(this.SelectedDataset.HostText);
                this.hostScene.SetSolutionName(this.SelectedDataset.Name);
                this.hostScene.SetBlueName(this.RightPlayerName);
                this.hostScene.SetRedName(this.LeftPlayerName);
                this.hostScene.SetSolutionPosition(this.SelectedDataset.Coordinates.SXGA_PositionX, this.SelectedDataset.Coordinates.SXGA_PositionY);
                if (this.rightPlayerInput is CoordinateSet &&
                    this.rightPlayerDistance.HasValue) {
                    this.hostScene.SetBlueDistance(this.rightPlayerDistance.Value);
                    this.hostScene.ShowBlueDistance();
                    this.hostScene.SetBluePosition(this.rightPlayerInput.SXGA_PositionX, this.rightPlayerInput.SXGA_PositionY);
                    this.hostScene.ShowBluePin();
                }
                else {
                    this.hostScene.ResetBlueDistance();
                    this.hostScene.HideBluePin();
                }
                if (this.leftPlayerInput is CoordinateSet &&
                    this.leftPlayerDistance.HasValue) {
                    this.hostScene.SetRedDistance(this.leftPlayerDistance.Value);
                    this.hostScene.ShowRedDistance();
                    this.hostScene.SetRedPosition(this.leftPlayerInput.SXGA_PositionX, this.leftPlayerInput.SXGA_PositionY);
                    this.hostScene.ShowRedPin();
                }
                else {
                    this.hostScene.ResetRedDistance();
                    this.hostScene.HideRedPin();
                }
                this.hostScene.ShowSolution();
            }
        }
        public override void Vhost_UnloadScene() { this.hostScene.Unload(); }

        public override void Vleftplayer_LoadScene() { this.leftPlayerScene.Load(); }
        public override void Vleftplayer_UnloadScene() { this.leftPlayerScene.Unload(); }

        public override void Vrightplayer_LoadScene() { this.rightPlayerScene.Load(); }
        public override void Vrightplayer_UnloadScene() { this.rightPlayerScene.Unload(); }

        public override void Vstage_Init() {
            base.Vstage_Init();
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.hostScene.ResetMap();
                this.hostScene.SetFullText(string.Empty);
                this.hostScene.SelectPlayer(Stage.PlayerSelection.NotSelected);
                this.hostScene.ToIn();
            }
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.leftPlayerScene.ResetMap();
                this.leftPlayerScene.SetSampleMap(false);
                this.leftPlayerScene.SetFullText(string.Empty);
                this.leftPlayerScene.SelectPlayer(Stage.PlayerSelection.LeftPlayer);
                this.leftPlayerScene.ToIn();
            }
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.rightPlayerScene.ResetMap();
                this.rightPlayerScene.SetSampleMap(false);
                this.rightPlayerScene.SetFullText(string.Empty);
                this.rightPlayerScene.SelectPlayer(Stage.PlayerSelection.RightPlayer);
                this.rightPlayerScene.ToIn();
            }
        }
        public void Vplayer_UnlockTouch() {
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.UnlockTouch();
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.UnlockTouch();
        }
        public void Vplayer_LockTouch() {
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.LockTouch();
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.LockTouch();
        }
        public void Vstage_MapIn(
            bool showSample) {
            this.Vhost_SetMap();
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                if (showSample) this.hostScene.ToOut();
                else this.hostScene.ToIn();
            }
            if (showSample) {
                if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                    this.leftPlayerScene.ResetMap();
                    this.leftPlayerScene.SetSampleMap(true);
                    this.leftPlayerScene.ToIn();
                }
                if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                    this.rightPlayerScene.ResetMap();
                    this.rightPlayerScene.SetSampleMap(true);
                    this.rightPlayerScene.ToIn();
                }
                this.Vplayer_UnlockTouch();
            }
            else if (this.SelectedDataset is DatasetContent) {
                if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                    this.leftPlayerScene.SetSampleMap(false);
                    this.leftPlayerScene.SetMapLayout(this.SelectedDataset.MapLayout);
                    this.leftPlayerScene.SetFullText(this.SelectedDataset.FullText);
                    this.leftPlayerScene.ToIn();
                }
                if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                    this.rightPlayerScene.SetMapLayout(this.SelectedDataset.MapLayout);
                    this.rightPlayerScene.SetSampleMap(false);
                    this.rightPlayerScene.SetFullText(this.SelectedDataset.FullText);
                    this.rightPlayerScene.ToIn();
                }
            }
        }
        public void Vstage_MapOut() {
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.hostScene.ToOut();
                this.hostScene.ResetMap();
            }
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.leftPlayerScene.ToOut();
                this.leftPlayerScene.ResetMap();
            }
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.rightPlayerScene.ToOut();
                this.rightPlayerScene.ResetMap();
            }
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
            }
            this.Save();
        }

        void leftPlayerScene_OKButtonPressed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftPlayerScene_OKButtonPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftPlayerScene_OKButtonPressed(object content) {
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                CoordinateSet touchCoordinate = new CoordinateSet(this.mapLayout, this.leftPlayerScene.TouchPositionX, this.leftPlayerScene.TouchPositionY);
                this.SetLeftPlayerInput(touchCoordinate.Text);
                //Console.WriteLine(this.LeftPlayerInput);
            }
        }

        void rightPlayerScene_OKButtonPressed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerScene_OKButtonPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerScene_OKButtonPressed(object content) {
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                CoordinateSet touchCoordinate = new CoordinateSet(this.mapLayout, this.rightPlayerScene.TouchPositionX, this.rightPlayerScene.TouchPositionY);
                this.SetRightPlayerInput(touchCoordinate.Text);
                //Console.WriteLine(this.mapLayout.ToString());
                //Console.WriteLine(this.rightPlayerScene.TouchPositionX.ToString());
                //Console.WriteLine(this.rightPlayerScene.TouchPositionY.ToString());
                //Console.WriteLine(touchCoordinate.Text);
                //Console.WriteLine(this.RightPlayerInput);
            }
        }

        #endregion

    }
}
