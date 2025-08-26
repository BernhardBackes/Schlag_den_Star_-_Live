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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.Select1PictureOf4;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.Select1PictureOf4 {

    public class Data {
        public DatasetContent[] DataList;
        public int SelectedIndex;
    }

    public class DatasetContent : Cliparts.Messaging.Message, INotifyPropertyChanged {

        #region Properties

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

        private int solutionID = 1;
        public int SolutionID {
            get { return this.solutionID; }
            set {
                if (this.solutionID != value) {
                    if (value < 1) value = 1;
                    if (value > 4) value = 4;
                    this.solutionID = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string description1 = string.Empty;
        public string Description1 {
            get { return this.description1; }
            set {
                if (this.description1 != value) {
                    if (value == null) value = string.Empty;
                    this.description1 = Regex.Replace(value, "(?<!\r)\n", "\r\n"); ;
                    this.on_PropertyChanged();
                }
            }
        }

        private string description2 = string.Empty;
        public string Description2 {
            get { return this.description2; }
            set {
                if (this.description2 != value) {
                    if (value == null) value = string.Empty;
                    this.description2 = Regex.Replace(value, "(?<!\r)\n", "\r\n"); ;
                    this.on_PropertyChanged();
                }
            }
        }

        private string description3 = string.Empty;
        public string Description3 {
            get { return this.description3; }
            set {
                if (this.description3 != value) {
                    if (value == null) value = string.Empty;
                    this.description3 = Regex.Replace(value, "(?<!\r)\n", "\r\n"); ;
                    this.on_PropertyChanged();
                }
            }
        }

        private string description4 = string.Empty;
        public string Description4 {
            get { return this.description4; }
            set {
                if (this.description4 != value) {
                    if (value == null) value = string.Empty;
                    this.description4 = Regex.Replace(value, "(?<!\r)\n", "\r\n"); ;
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

        // © Verlagsgruppe Oetinger

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DatasetContent() { }
        public DatasetContent(
            string pictureFilename) {
            if (string.IsNullOrEmpty(pictureFilename)) {
                this.Name = "?";
                this.PictureFilename = string.Empty;
            }
            else {
                this.Name = Path.GetFileNameWithoutExtension(pictureFilename);
                this.PictureFilename = pictureFilename;
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

    public class Business : _Base.BuzzerScore.Business {

        #region Properties

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

        private bool repressPropertyChanged = false;

        #endregion


        #region Funktionen

        public Business() { }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.Select1PictureOf4'", typeIdentifier);
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
        }

        public override void ResetData() {
            base.ResetData();
            this.TaskCounter = 0;
            this.SelectDataset(0);
        }

        public override void Next() {
            base.Next();
            this.Vinsert_SetBuzzer();
            this.TaskCounter++;
            this.SelectDataset(this.SelectedDatasetIndex + 1);
        }

        public override void ReleaseBuzzer() {
            base.ReleaseBuzzer();
            this.Vinsert_SetBuzzer();
        }

        public override void DoBuzzer(Content.Gameboard.PlayerSelection buzzeredPlayer) {
            base.DoBuzzer(buzzeredPlayer);
            this.Vinsert_SetBuzzer();
        }

        public void True() {
            switch (this.BuzzeredPlayer) {
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.LeftPlayer:
                    this.LeftPlayerScore++;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.RightPlayer:
                    this.RightPlayerScore++;
                    break;
            }
            //if (this.BuzzeredPlayer != Content.Gameboard.PlayerSelection.NotSelected &&
            //    this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.PlayJingleTrue();
            this.Vinsert_SetScore();
            this.Vstage_SetScore();
        }

        public void False() {
            switch (this.BuzzeredPlayer) {
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.LeftPlayer:
                    this.RightPlayerScore++;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.RightPlayer:
                    this.LeftPlayerScore++;
                    break;
            }
            //if (this.BuzzeredPlayer != Content.Gameboard.PlayerSelection.NotSelected &&
            //    this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.PlayJingleFalse();
            this.Vinsert_SetScore();
            this.Vstage_SetScore();
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
        public override void Vinsert_SetScore() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                switch (this.ScoreStyle) {
                    case VentuzScenes.GamePool._Modules.Score.Styles.FourDots:
                        this.insertScene.SetScoreStyle(Insert.Styles.FourDots);
                        break;
                    case VentuzScenes.GamePool._Modules.Score.Styles.FiveDots:
                        this.insertScene.SetScoreStyle(Insert.Styles.FiveDots);
                        break;
                    case VentuzScenes.GamePool._Modules.Score.Styles.SixDots:
                        this.insertScene.SetScoreStyle(Insert.Styles.SixDots);
                        break;
                    case VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots:
                    case VentuzScenes.GamePool._Modules.Score.Styles.SevenDots:
                    case VentuzScenes.GamePool._Modules.Score.Styles.Counter:
                    case VentuzScenes.GamePool._Modules.Score.Styles.CounterLeft:
                    case VentuzScenes.GamePool._Modules.Score.Styles.Sport:
                    default:
                        this.insertScene.SetScoreStyle(Insert.Styles.Counter);
                        break;
                }
                this.insertScene.SetLeftScore(this.LeftPlayerScore);
                this.insertScene.SetRightScore(this.RightPlayerScore);

                this.Vinsert_SetScore(this.insertScene.Score, this.LeftPlayerScore, this.RightPlayerScore);
            }
        }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }

        public override void Vinsert_Buzzer(Content.Gameboard.PlayerSelection buzzeredPlayer) { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_Buzzer(this.insertScene.Timeout, buzzeredPlayer); }
        public override void Vinsert_SetTimeout() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetTimeout(this.insertScene.Timeout); }
        public void Vinsert_SetBuzzer() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                switch (this.BuzzeredPlayer) {
                    case Content.Gameboard.PlayerSelection.LeftPlayer:
                        this.insertScene.BuzzerLeft();
                        break;
                    case Content.Gameboard.PlayerSelection.RightPlayer:
                        this.insertScene.BuzzerRight();
                        break;
                    case Content.Gameboard.PlayerSelection.NotSelected:
                    default:
                        this.insertScene.SetBuzzerOut();
                        break;
                }
            }
        }
        public override void Vinsert_StopTimeout() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_StopTimeout(this.insertScene.Timeout); }

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

        public void Vinsert_ContentIn() {
            this.Vinsert_SetScore();
            this.Vinsert_SetBuzzer();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) {
                this.insertScene.SetPictureFilename(this.SelectedDataset.PictureFilename);
                this.insertScene.SetSolutionFilename(this.SelectedDataset.SolutionFilename);
                this.insertScene.SetSolutionOut();
                this.insertScene.Deselect();
                this.insertScene.ToIn();
            }
            if (this.TaskCounter > 0 && this.TaskCounter <= this.TaskCounterSize) this.Vinsert_TaskCounterIn();
        }
        public void Vinsert_Resolve() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) {
                this.insertScene.SolutionToIn();
                this.insertScene.Select(this.SelectedDataset.SolutionID);
                if (string.IsNullOrEmpty(this.SelectedDataset.Credits)) this.insertScene.SetCreditsText(string.Empty);
                else {
                    this.insertScene.SetCreditsText(this.SelectedDataset.Credits);
                    this.insertScene.ShowCredits();
                }
            }
        }
        public void Vinsert_ContentOut() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.ToOut();
            this.Vinsert_TaskCounterOut();
        }

        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void Vfullscreen_LoadScene() { this.fullscreenScene.Load(); }
        public void Vfullscreen_ContentIn() {
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) {
                this.fullscreenScene.SetPictureFilename(this.SelectedDataset.PictureFilename);
                this.fullscreenScene.SetSolutionFilename(this.SelectedDataset.SolutionFilename);
                this.fullscreenScene.SetSolutionOut();
                this.fullscreenScene.Deselect();
                this.fullscreenScene.ToIn();
            }
        }
        public void Vfullscreen_Resolve() {
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) {
                this.fullscreenScene.SolutionToIn();
                this.fullscreenScene.Select(this.SelectedDataset.SolutionID);
            }
        }
        public void Vfullscreen_ContentOut() {
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.ToOut();
        }
        public override void Vfullscreen_UnloadScene() { this.fullscreenScene.Unload(); }

        public override void Vstage_Init() {
            base.Vstage_Init();
            if (this.hostMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostMasterScene.LiveVideoIn();
            if (this.leftPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerMasterScene.LiveVideoIn();
            if (this.rightPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerMasterScene.LiveVideoIn();
        }

        public override void Vhost_LoadScene() { this.hostScene.Load(); }
        public void Vhost_In() {
            this.Vhost_Set();
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.ToIn();
        }
        public void Vhost_Set() {
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) {
                int counter = this.TaskCounter;
                string hostText = this.SelectedDataset.Name;
                if (counter == 0) hostText = string.Format("Beispiel: {0}", hostText);
                else if (counter > 0 && counter <= this.TaskCounterSize) hostText = string.Format("{0}/{1}: {2}", counter.ToString(), this.TaskCounterSize.ToString(), hostText);
                this.hostScene.SetTitle(hostText);
                this.hostScene.SetDescription1(this.SelectedDataset.Description1);
                this.hostScene.SetDescription2(this.SelectedDataset.Description2);
                this.hostScene.SetDescription3(this.SelectedDataset.Description3);
                this.hostScene.SetDescription4(this.SelectedDataset.Description4);
            }
        }

        public void Vhost_Out() { if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.ToOut(); }
        public override void Vhost_UnloadScene() { this.hostScene.Unload(); }

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

        #endregion

    }
}
