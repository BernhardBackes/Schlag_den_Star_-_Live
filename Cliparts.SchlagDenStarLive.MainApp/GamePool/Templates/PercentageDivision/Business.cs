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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.PercentageDivision;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.PercentageDivision {

    public class Data {
        public DatasetContent[] DataList;
        public int SelectedIndex;
    }

    public class DatasetContent : Messaging.Message, INotifyPropertyChanged {

        #region Properties

        private string pictureFilename = string.Empty;
        public string PictureFilename {
            get { return this.pictureFilename; }
            set {
                if (this.pictureFilename != value) {
                    if (value == null) value = string.Empty;
                    this.pictureFilename = value;
                    this.on_PropertyChanged();
                    this.Picture = Helper.getThumbnailFromMediaFile(value);
                }
            }
        }

        [XmlIgnore]
        public Image Picture { get; private set; }

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

    public class Business : _Base.Score.Business {

        #region Properties

        private Insert.Styles borderStyle = Insert.Styles.FourDots;
        public Insert.Styles BorderStyle {
            get { return this.borderStyle; }
            set {
                if (this.borderStyle != value) {
                    this.borderStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetBorder();
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

        private int leftPlayerInputA = -1;
        [NotSerialized]
        public int LeftPlayerInputA {
            get { return this.leftPlayerInputA; }
            set {
                if (this.leftPlayerInputA != value) {
                    if (value < 0) value = -1;
                    if (value > 100) value = 100;
                    this.leftPlayerInputA = value;
                    this.on_PropertyChanged();
                    this.on_PropertyChanged("LeftPlayerInputB");
                    this.setBestPlayer();
                }
            }
        }

        public int LeftPlayerInputB { 
            get {
                if (this.LeftPlayerInputA < 0) return -1;
                else return 100 - this.LeftPlayerInputA; 
            } 
        }

        private int rightPlayerInputA = -1;
        [NotSerialized]
        public int RightPlayerInputA {
            get { return this.rightPlayerInputA; }
            set {
                if (this.rightPlayerInputA != value) {
                    if (value < 0) value = -1;
                    if (value > 100) value = 100;
                    this.rightPlayerInputA = value;
                    this.on_PropertyChanged();
                    this.on_PropertyChanged("RightPlayerInputB");
                    this.setBestPlayer();
                }
            }
        }

        public int RightPlayerInputB {
            get {
                if (this.RightPlayerInputA < 0) return -1;
                else return 100 - this.RightPlayerInputA;
            }
        }

        private int votingA = 50;
        [NotSerialized]
        public int VotingA {
            get { return this.votingA; }
            set {
                if (this.votingA != value) {
                    if (value < 0) value = 0;
                    if (value > 100) value = 100;
                    this.votingA = value;
                    this.on_PropertyChanged();
                    this.on_PropertyChanged("VotingB");
                    this.setBestPlayer();
                }
            }
        }

        public int VotingB { get { return 100 - this.votingA; } }

        private Content.Gameboard.PlayerSelection bestPlayer = Content.Gameboard.PlayerSelection.NotSelected;
        public Content.Gameboard.PlayerSelection BestPlayer {
            get { return this.bestPlayer; }
            set {
                if (this.bestPlayer != value) {
                    this.bestPlayer = value;
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.PercentageDivision'", typeIdentifier);
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
            this.leftPlayerScene.PropertyChanged += this.leftPlayerScene_PropertyChanged;
            this.leftPlayerScene.OKPressed += this.leftPlayerScene_OKPressed;

            this.rightPlayerScene = new Player(syncContext, rightPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.rightPlayerScene.StatusChanged += this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.PropertyChanged += this.rightPlayerScene_PropertyChanged;
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
            this.leftPlayerScene.PropertyChanged -= this.leftPlayerScene_PropertyChanged;
            this.leftPlayerScene.OKPressed -= this.leftPlayerScene_OKPressed;
            this.leftPlayerScene.Dispose();
            this.rightPlayerScene.StatusChanged -= this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.PropertyChanged -= this.rightPlayerScene_PropertyChanged;
            this.rightPlayerScene.OKPressed -= this.rightPlayerScene_OKPressed;
            this.rightPlayerScene.Dispose();
        }

        public override void ResetData() {
            base.ResetData();
            this.LeftPlayerInputA = -1;
            this.RightPlayerInputA = -1;
            this.VotingA = 50;
            this.TaskCounter = 0;
            this.SelectDataset(0);
        }

        private void setBestPlayer() {
            int offsetLeft = Math.Abs(this.VotingA - this.LeftPlayerInputA);
            int offsetRight = Math.Abs(this.VotingA - this.RightPlayerInputA);
            if (offsetLeft < offsetRight) this.BestPlayer = Content.Gameboard.PlayerSelection.LeftPlayer;
            else if (offsetRight < offsetLeft) this.BestPlayer = Content.Gameboard.PlayerSelection.RightPlayer;
            else this.BestPlayer = Content.Gameboard.PlayerSelection.NotSelected;
        }

        public void Resolve() {
            this.setBestPlayer();
            switch (this.BestPlayer) {
                case Content.Gameboard.PlayerSelection.LeftPlayer:
                    this.LeftPlayerScore++;
                    break;
                case Content.Gameboard.PlayerSelection.RightPlayer:
                    this.RightPlayerScore++;
                    break;
                case Content.Gameboard.PlayerSelection.NotSelected:
                default:
                    if (this.LeftPlayerInputA >= 0 &&
                        this.RightPlayerInputA >= 0) {
                        this.LeftPlayerScore++;
                        this.RightPlayerScore++;
                    }
                    break;
            }
        }

        public override void Next() {
            base.Next();
            this.LeftPlayerInputA = -1;
            this.RightPlayerInputA = -1;
            this.VotingA = 50;
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


        public override void Vinsert_LoadScene() { this.insertScene.Load(); }
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
        public override void Vinsert_ScoreOut() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Score.ToOut(); }
        public void Vinsert_BorderIn() {
            this.Vinsert_SetBorder();
            this.Vinsert_ResetWinner();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.SetValuesOut();
                this.insertScene.BorderToIn();
            }
        }
        public void Vinsert_BorderValuesIn() {
            this.Vinsert_SetBorder();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.ValuesToIn();
        }
        public void Vinsert_SetBorder() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.SetStyle(this.BorderStyle);
                this.insertScene.SetBorderPositionX(this.BorderPositionX);
                this.insertScene.SetBorderPositionY(this.BorderPositionY);
                this.insertScene.SetBorderLeftScore(this.LeftPlayerScore);
                this.insertScene.SetBorderRightScore(this.RightPlayerScore);
                this.insertScene.SetLeftPlayerA(this.LeftPlayerInputA);
                this.insertScene.SetRightPlayerA(this.RightPlayerInputA);
            }
        }
        public void Vinsert_ResetWinner() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.ResetWinner(); }
        public void Vinsert_SetBestPlayer() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                switch (this.BestPlayer) {
                    case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.LeftPlayer:
                        this.insertScene.SetWinnerLeft();
                        break;
                    case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.RightPlayer:
                        this.insertScene.SetWinnerRight();
                        break;
                    case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.NotSelected:
                    default:
                        if (this.LeftPlayerInputA >= 0 && this.RightPlayerInputA >= 0) this.insertScene.SetWinnerBoth();
                        break;
                }
            }
        }
        public void Vinsert_BorderOut() { 
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.BorderToOut();
            this.Vinsert_ResetWinner();
        }
        public override void Vinsert_UnloadScene() { this.insertScene.Unload(); }

        public override void Vfullscreen_LoadScene() { this.fullscreenScene.Load(); }
        public void Vfullscreen_ContentIn() {
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) {
                this.fullscreenScene.SetPictureFilename(this.SelectedDataset.PictureFilename);
                this.fullscreenScene.ResetValues();
                this.fullscreenScene.ToIn();
            }
        }
        internal void Vfullscreen_TextIn() {
            this.Vfullscreen_SetText();
            if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.TextInsert.ToIn();
        }
        internal void Vfullscreen_SetText() {
            if (this.fullscreenScene is VRemote4.HandlerSi.Scene && this.SelectedDataset is DatasetContent) this.Vfullscreen_SetText(this.fullscreenScene.TextInsert, this.SelectedDataset);
        }
        internal void Vfullscreen_SetText(
            VentuzScenes.GamePool._Modules.TextInsert scene,
            DatasetContent dataset) {
            if (scene is VentuzScenes.GamePool._Modules.TextInsert &&
                dataset is DatasetContent) {
                scene.SetPositionX(0);
                scene.SetPositionY(100);
                scene.SetStyle(VentuzScenes.GamePool._Modules.TextInsert.Styles.TwoRows);
                scene.SetText(dataset.HostText);
            }
        }
        internal void Vfullscreen_TextOut() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.TextInsert.ToOut(); }
        public void Vfullscreen_StartCountDown() { if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.StartCountDown(); }
        public void Vfullscreen_StopCountDown() { if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.StopCountDown(); }
        public void Vfullscreen_VotingIn() {
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.fullscreenScene.SetValueA(this.VotingA);
                this.fullscreenScene.ValuesToIn();
            }
        }
        public void Vfullscreen_CountVotingUp() {
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.CountValuesUp();
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
            this.Vhost_ShowHostText();
            this.Vhost_HideValues();
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.hostScene.SetBestPlayer(VentuzScenes.GamePool.PercentageDivision.PlayerSelection.NotSelected);
                this.hostScene.ToIn();
            }
        }
        public void Vhost_Set() {
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                if (this.SelectedDataset is DatasetContent) this.hostScene.SetHostText(this.SelectedDataset.HostText);
                this.hostScene.SetLeftPlayerInputA(this.LeftPlayerInputA);
                this.hostScene.SetRightPlayerInputA(this.RightPlayerInputA);
            }
        }
        public void Vhost_ShowHostText() {
            this.Vhost_Set();
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.ShowHostText();
        }
        public void Vhost_HideHostText() { if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.HideHostText(); }
        public void Vhost_ShowLeftPlayerValue() {
            this.Vhost_Set();
            this.Vhost_HideHostText();
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.ShowLeftPlayer();
        }
        public void Vhost_ShowRightPlayerValue() {
            this.Vhost_Set();
            this.Vhost_HideHostText();
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.ShowRightPlayer();
        }
        public void Vhost_HideValues() {
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.hostScene.HideLeftPlayer();
                this.hostScene.HideRightPlayer();
            }
        }
        public void Vhost_Resolve() {
            this.Vhost_Set();
            this.Vhost_HideHostText();
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                switch (this.BestPlayer) {
                    case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.LeftPlayer:
                        this.hostScene.SetBestPlayer(VentuzScenes.GamePool.PercentageDivision.PlayerSelection.LeftPlayer);
                        break;
                    case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.RightPlayer:
                        this.hostScene.SetBestPlayer(VentuzScenes.GamePool.PercentageDivision.PlayerSelection.RightPlayer);
                        break;
                    case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.NotSelected:
                    default:
                        if (this.LeftPlayerInputA >= 0 && this.RightPlayerInputA >= 0) this.hostScene.SetBestPlayer(VentuzScenes.GamePool.PercentageDivision.PlayerSelection.BothPlayers);
                        else this.hostScene.SetBestPlayer(VentuzScenes.GamePool.PercentageDivision.PlayerSelection.NotSelected);
                        break;
                }
            }
        }
        public void Vhost_Out() { 
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.ToOut();
            this.Vhost_HideValues();
        }
        public override void Vhost_UnloadScene() { this.hostScene.Unload(); }

        public override void Vleftplayer_LoadScene() { this.leftPlayerScene.Load(); }
        public override void Vrightplayer_LoadScene() { this.rightPlayerScene.Load(); }
        public void Vplayers_In() {
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.SetIn();
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.SetIn();
        }
        public void Vplayers_EnableInput() {
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.leftPlayerScene.SetSliderLockValue(-1);
                this.leftPlayerScene.ResetSlider();
                this.leftPlayerScene.EnableSlider();
            }
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.rightPlayerScene.SetSliderLockValue(-1);
                this.rightPlayerScene.ResetSlider();
                this.rightPlayerScene.EnableSlider();
            }
        }
        public void Vplayers_SetLockValue(
            int value) {
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.SetSliderLockValue(value);
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.SetSliderLockValue(value);
        }
        public void Vplayers_DisableInput() {
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.leftPlayerScene.SetSliderValueA(this.LeftPlayerInputA);
                this.leftPlayerScene.DisableSlider();
            }
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.rightPlayerScene.SetSliderValueA(this.RightPlayerInputA);
                this.rightPlayerScene.DisableSlider();
            }
        }
        public void Vplayers_Out() {
            this.Vplayers_DisableInput();
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.SetOut();
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.SetOut();
        }
        public override void Vleftplayer_UnloadScene() { this.leftPlayerScene.Unload(); }
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

        void leftPlayerScene_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftPlayerScene_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftPlayerScene_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                //if (e.PropertyName == "ValueA") this.LeftPlayerInputA = this.leftPlayerScene.ValueA;
            }
        }
        void leftPlayerScene_OKPressed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftPlayerScene_OKPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftPlayerScene_OKPressed(object content) {
            this.LeftPlayerInputA = this.leftPlayerScene.ValueA;
            this.Vplayers_SetLockValue(this.LeftPlayerInputA);
            this.Vhost_ShowLeftPlayerValue();
        }

        void rightPlayerScene_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerScene_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerScene_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                //if (e.PropertyName == "ValueA") this.RightPlayerInputA = this.rightPlayerScene.ValueA;
            }
        }
        void rightPlayerScene_OKPressed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerScene_OKPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerScene_OKPressed(object content) {
            this.RightPlayerInputA = this.rightPlayerScene.ValueA;
            this.Vplayers_SetLockValue(this.RightPlayerInputA);
            this.Vhost_ShowRightPlayerValue();
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

        #endregion

    }
}
