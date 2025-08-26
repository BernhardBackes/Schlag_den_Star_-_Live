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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SelectDifferences;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.SelectDifferences {

    public class Data {
        public DatasetContent[] DataList;
        public int SelectedIndex;
    }

    public class DatasetContent : Cliparts.Messaging.Message, INotifyPropertyChanged {

        public const int FramesCount = 20;

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

        private List<int> solutionFrameList = new List<int>();
        public int[] SolutionFrameList {
            get { return this.solutionFrameList.ToArray(); }
            set {
                if (value == null) this.solutionFrameList.Clear();
                else solutionFrameList = new List<int>(value);
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

        public void ToggleSolutionFrame(
            int index) {
            if (this.solutionFrameList.Contains(index)) this.solutionFrameList.Remove(index);
            else this.solutionFrameList.Add(index);
            this.on_PropertyChanged("SolutionFrameList");
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

        private Insert.Style backgroundStyle = Insert.Style.FiveDots;
        public VentuzScenes.GamePool.SelectDifferences.Insert.Style BackgroundStyle {
            get { return this.backgroundStyle; }
            set {
                if (this.backgroundStyle != value) {
                    this.backgroundStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetContent();
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

        private PlayerSelection buzzeredPlayer = PlayerSelection.NotSelected;
        public PlayerSelection BuzzeredPlayer {
            get { return this.buzzeredPlayer; }
            private set {
                if (this.buzzeredPlayer != value) {
                    this.buzzeredPlayer = value;
                    this.Vstage_SetBuzzer(value);
                    this.on_PropertyChanged();
                    if (value == PlayerSelection.NotSelected) this.Vinsert_Buzzer(value);
                }
            }
        }

        public bool[] FrameIsIdleList {
            get {
                if (this.buzzeredPlayer == PlayerSelection.LeftPlayer && this.leftPlayerScene is Player) return this.leftPlayerScene.FrameIsIdleList;
                else if (this.buzzeredPlayer == PlayerSelection.RightPlayer && this.rightPlayerScene is Player) return this.rightPlayerScene.FrameIsIdleList;
                else return new bool[0];
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.SelectDifferences'", typeIdentifier);
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

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;

            this.fullscreenScene = new Fullscreen(syncContext, fullscreenMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.fullscreenScene.StatusChanged += this.fullscreenScene_StatusChanged;

            this.leftPlayerScene = new Player(syncContext, leftPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.leftPlayerScene.StatusChanged += this.leftPlayerScene_StatusChanged;
            this.leftPlayerScene.PropertyChanged += this.leftPlayerGame_PropertyChanged;
            this.leftPlayerScene.OKPressed += this.leftPlayerScene_OKPressed;

            this.rightPlayerScene = new Player(syncContext, rightPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.rightPlayerScene.StatusChanged += this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.PropertyChanged += this.rightPlayerGame_PropertyChanged;
            this.rightPlayerScene.OKPressed += this.rightPlayerScene_OKPressed;

            ((SelectDifferences.UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((SelectDifferences.UserControlGame)this.GameControl).Pose(this);
        }

        public override void Dispose() {
            base.Dispose();

            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Dispose();
            this.fullscreenScene.StatusChanged -= this.fullscreenScene_StatusChanged;
            this.fullscreenScene.Dispose();
            this.leftPlayerScene.StatusChanged -= this.leftPlayerScene_StatusChanged;
            this.leftPlayerScene.PropertyChanged -= this.leftPlayerGame_PropertyChanged;
            this.leftPlayerScene.OKPressed -= this.leftPlayerScene_OKPressed;
            this.leftPlayerScene.Dispose();
            this.rightPlayerScene.StatusChanged -= this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.PropertyChanged -= this.rightPlayerGame_PropertyChanged;
            this.rightPlayerScene.OKPressed -= this.rightPlayerScene_OKPressed;
            this.rightPlayerScene.Dispose();
        }

        public override void ResetData() {
            base.ResetData();
            this.BuzzeredPlayer = PlayerSelection.NotSelected;
            this.TaskCounter = 0;
            this.SelectDataset(0);
        }

        public virtual void DoBuzzer(
            PlayerSelection buzzeredPlayer) {
            if (this.BuzzeredPlayer == PlayerSelection.NotSelected &&
                this.BuzzeredPlayer != buzzeredPlayer) {
                this.Vinsert_Buzzer(buzzeredPlayer);
                this.BuzzeredPlayer = buzzeredPlayer;
            }
        }

        public override void Next() {
            base.Next();
            this.BuzzeredPlayer = PlayerSelection.NotSelected;
            this.TaskCounter++;
            this.SelectDataset(this.SelectedDatasetIndex + 1);
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
            this.Vinsert_SetScore();
            this.Vinsert_SetContent();
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
            this.Vinsert_SetScore();
            this.Vinsert_SetContent();
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

        private List<int> missedFrameIDList {
            get {
                List<int> result = new List<int>();
                bool[] frameIsIdleList = this.FrameIsIdleList;
                if (this.SelectedDataset is DatasetContent &&
                    frameIsIdleList.Length == DatasetContent.FramesCount) {
                    for (int i = 0; i < DatasetContent.FramesCount; i++) {
                        if (frameIsIdleList[i] && this.SelectedDataset.SolutionFrameList.Contains(i)) result.Add(i + 1);
                    }
                }
                return result;
            }
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }

        public override void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score, this.LeftPlayerScore, this.RightPlayerScore); }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }

        public void Vinsert_ContentIn() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                this.Vinsert_SetContent();
                this.insertScene.ResetSolution();
                this.insertScene.ToIn();
            }
        }
        public void Vinsert_SetContent() {
            if (this.SelectedDataset is DatasetContent) this.Vinsert_SetContent(this.insertScene, this.BackgroundStyle, this.LeftPlayerScore, this.RightPlayerScore, this.SelectedDataset.PictureFilename, this.SelectedDataset.SolutionFilename);
            else this.Vinsert_SetContent(this.insertScene, this.BackgroundStyle, this.LeftPlayerScore, this.RightPlayerScore, string.Empty, string.Empty);
            this.Vinsert_SetBuzzer(this.insertScene, this.BuzzeredPlayer);
        }
        public void Vinsert_SetContent(
            Insert scene,
            Insert.Style backgroundStyle,
            int leftScore,
            int rightScore,
            string pictureFilename,
            string solutionFilename) {
            if (scene is Insert) {
                scene.SetStyle(this.BackgroundStyle);
                scene.SetLeftScore(leftScore);
                scene.SetRightScore(rightScore);
                scene.SetFilename(pictureFilename);
                scene.SetSolutionFilename(solutionFilename);
            }
        }
        public void Vinsert_Buzzer(
            PlayerSelection buzzeredPlayer) {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                switch (buzzeredPlayer) {
                    case PlayerSelection.NotSelected:
                        this.insertScene.ResetBuzzer();
                        break;
                    case PlayerSelection.LeftPlayer:
                        this.insertScene.BuzzerLeft();
                        break;
                    case PlayerSelection.RightPlayer:
                        this.insertScene.BuzzerRight();
                        break;
                }
            }
        }
        public void Vinsert_SetBuzzer(
            Insert scene,
            PlayerSelection buzzeredPlayer) {
            if (scene is Insert) {
                switch (buzzeredPlayer) {
                    case PlayerSelection.NotSelected:
                        scene.ResetBuzzer();
                        break;
                    case PlayerSelection.LeftPlayer:
                        scene.SetBuzzerLeft();
                        break;
                    case PlayerSelection.RightPlayer:
                        scene.SetBuzzerRight();
                        break;
                }
            }
        }
        internal void Vinsert_ShowSelection() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                int id = 1;
                foreach (bool item in this.FrameIsIdleList) {
                    if (item) this.insertScene.ResetSelection(id);
                    else this.insertScene.SetSelection(id);
                    id++;
                }
            }
        }
        public void Vinsert_Resolve() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.ShowSolution(); }
        internal void Vinsert_ShowMissed() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                foreach (int id in this.missedFrameIDList) this.insertScene.SetSelectionMissing(id);
            }
        }
        public void Vinsert_ContentOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.ToOut(); }
        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void Vfullscreen_LoadScene() { this.fullscreenScene.Load(); }
        public void Vfullscreen_ContentIn() {
            if (this.fullscreenScene is VRemote4.HandlerSi.Scene) {
                if (this.SelectedDataset is DatasetContent) this.Vfullscreen_SetContent(this.fullscreenScene, this.SelectedDataset.PictureFilename, this.SelectedDataset.SolutionFilename);
                else this.Vfullscreen_SetContent(this.fullscreenScene, string.Empty, string.Empty);
                this.fullscreenScene.ResetSolution();
                this.fullscreenScene.ToIn();
            }
        }
        public void Vfullscreen_SetContent(
            VentuzScenes.GamePool.SelectDifferences.Fullscreen scene,
            string pictureFilename,
            string solutionFilename) {
            if (scene is Fullscreen) {
                scene.SetFilename(pictureFilename);
                scene.SetSolutionFilename(solutionFilename);
            }
        }
        internal void Vfullscreen_ShowSelection() {
            if (this.fullscreenScene is VRemote4.HandlerSi.Scene) {
                int id = 1;
                foreach (bool item in this.FrameIsIdleList) {
                    if (item) this.fullscreenScene.ResetSelection(id);
                    else this.fullscreenScene.SetSelection(id);
                    id++;
                }
            }
        }
        public void Vfullscreen_Resolve() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.ShowSolution(); }
        internal void Vfullscreen_ShowMissed() {
            if (this.fullscreenScene is VRemote4.HandlerSi.Scene) {
                foreach (int id in this.missedFrameIDList) this.fullscreenScene.SetSelectionMissing(id);
            }
        }
        public void Vfullscreen_ContentOut() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.ToOut(); }
        public override void Vfullscreen_UnloadScene() { this.fullscreenScene.Unload(); }

        public override void Vstage_Init() {
            base.Vstage_Init();
            if (this.hostMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostMasterScene.LiveVideoIn();
            this.Vstage_SetBuzzer(this.BuzzeredPlayer);

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

        internal void Vplayers_ContentIn() {
            if (this.SelectedDataset is DatasetContent) {
                this.Vplayers_SetContent(this.leftPlayerScene, this.SelectedDataset.PictureFilename, this.SelectedDataset.SolutionFilename);
                this.Vplayers_SetContent(this.rightPlayerScene, this.SelectedDataset.PictureFilename, this.SelectedDataset.SolutionFilename);
            }
            else {
                this.Vplayers_SetContent(this.leftPlayerScene, string.Empty, string.Empty);
                this.Vplayers_SetContent(this.rightPlayerScene, string.Empty, string.Empty);
            }
            if (this.leftPlayerScene is VRemote4.HandlerSi.Scene) this.leftPlayerScene.ToIn();
            if (this.rightPlayerScene is VRemote4.HandlerSi.Scene) this.rightPlayerScene.ToIn();
        }

        internal void Vplayers_SetContent(
            VentuzScenes.GamePool.SelectDifferences.Player scene,
            string pictureFilename,
            string solutionFilename) {
            if (scene is Player) {
                scene.SetFilename(pictureFilename);
                scene.SetSolutionFilename(solutionFilename);
            }
        }

        internal void Vplayers_ShowSelection() {
            if (this.leftPlayerScene is VRemote4.HandlerSi.Scene) {
                int id = 1;
                foreach (bool item in this.FrameIsIdleList) {
                    if (item) this.leftPlayerScene.ResetSelection(id);
                    else this.leftPlayerScene.SetSelection(id);
                    id++;
                }
            }
            if (this.rightPlayerScene is VRemote4.HandlerSi.Scene) {
                int id = 1;
                foreach (bool item in this.FrameIsIdleList) {
                    if (item) this.rightPlayerScene.ResetSelection(id);
                    else this.rightPlayerScene.SetSelection(id);
                    id++;
                }
            }

        }
        internal void Vplayers_Resolve() {
            if (this.leftPlayerScene is VRemote4.HandlerSi.Scene) this.leftPlayerScene.ShowSolution();
            if (this.rightPlayerScene is VRemote4.HandlerSi.Scene) this.rightPlayerScene.ShowSolution();
        }
        internal void Vplayers_ShowMissed() {
            if (this.leftPlayerScene is VRemote4.HandlerSi.Scene) {
                foreach (int id in this.missedFrameIDList) this.leftPlayerScene.SetSelectionMissing(id);
            }
            if (this.rightPlayerScene is VRemote4.HandlerSi.Scene) {
                foreach (int id in this.missedFrameIDList) this.rightPlayerScene.SetSelectionMissing(id);
            }
        }
        internal void Vplayers_ContentOut() {
            if (this.leftPlayerScene is VRemote4.HandlerSi.Scene) this.leftPlayerScene.ToOut();
            if (this.rightPlayerScene is VRemote4.HandlerSi.Scene) this.rightPlayerScene.ToOut();
        }

        public override void Vleftplayer_LoadScene() { this.leftPlayerScene.Load(); }
        internal void Vleftplayer_UnlockTouch() { if (this.leftPlayerScene is VRemote4.HandlerSi.Scene) this.leftPlayerScene.UnlockTouch(); }
        internal void Vleftplayer_LockTouch() { if (this.leftPlayerScene is VRemote4.HandlerSi.Scene) this.leftPlayerScene.LockTouch(); }
        public override void Vleftplayer_UnloadScene() { this.leftPlayerScene.Unload(); }

        public override void Vrightplayer_LoadScene() { this.rightPlayerScene.Load(); }
        internal void Vrightplayer_UnlockTouch() { if (this.rightPlayerScene is VRemote4.HandlerSi.Scene) this.rightPlayerScene.UnlockTouch(); }
        internal void Vrightplayer_LockTouch() { if (this.rightPlayerScene is VRemote4.HandlerSi.Scene) this.rightPlayerScene.LockTouch(); }
        public override void Vrightplayer_UnloadScene() { this.rightPlayerScene.Unload(); }

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

        void leftPlayerGame_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftPlayerGame_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftPlayerGame_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
        }
        void leftPlayerScene_OKPressed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftPlayerScene_OKPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftPlayerScene_OKPressed(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            this.Vrightplayer_LockTouch();
            this.DoBuzzer(PlayerSelection.LeftPlayer);
        }

        void rightPlayerGame_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerGame_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerGame_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            //if (e is PropertyChangedEventArgs &&
            //    this.SelectedPlayer == Content.Gameboard.PlayerSelection.RightPlayer) {
            //    if (e.PropertyName == "RightPlayerCounter") {
            //        //this.RightPlayerCounter = this.rightPlayerScene.Game.RightPlayerCounter;
            //        //this.Vplayer_SetCounter();
            //    }
            //    else if (e.PropertyName == "LastSelectedFrame") this.distributeSelectedFrame(this.rightPlayerScene.Game.LastSelectedFrame);
            //}
        }
        void rightPlayerScene_OKPressed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerScene_OKPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerScene_OKPressed(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            this.Vleftplayer_LockTouch();
            this.DoBuzzer(PlayerSelection.RightPlayer);
        }

        #endregion

    }
}
