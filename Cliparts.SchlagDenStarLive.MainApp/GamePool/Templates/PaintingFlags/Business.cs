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
using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.PaintingFlags;
using Cliparts.Serialization;
using Cliparts.VRemote4.HandlerSi;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.PaintingFlags
{

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
                    else this.name = value;
                    this.buildToString();
                    this.on_PropertyChanged();
                }
            }
        }

        private OrientationStates orientation;

        public OrientationStates Orientation
        {
            get { return this.orientation; }
            set 
            { 
                if (this.orientation != value)
                {
                    this.orientation = value;
                    this.on_PropertyChanged();
                }
            }
        }


        private string pictureFilename = string.Empty;
        public string PictureFilename
        {
            get { return this.pictureFilename; }
            set
            {
                if (this.pictureFilename != value)
                {
                    if (string.IsNullOrEmpty(value)) this.pictureFilename = string.Empty;
                    else this.pictureFilename = value;
                    this.Picture = Helper.getThumbnailFromMediaFile(value);
                    this.on_PropertyChanged();
                }
            }
        }

        [XmlIgnore]
        public Image Picture { get; private set; }


        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DatasetContent() { }
        public DatasetContent(
            string text) {
            if (string.IsNullOrEmpty(text)) this.Name = "?";
            else this.Name = text;
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

        public static Color[] ColorValues = new Color[] {
            Color.FromArgb(128, 128, 128),
            Color.FromArgb(37, 81, 143),
            Color.FromArgb(16, 126, 76),
            Color.FromArgb(200, 52, 66),
            Color.FromArgb(130, 90, 75),
            Color.FromArgb(255, 120, 47),
            Color.FromArgb(253, 214, 30),
            Color.FromArgb(255, 255, 255),
            Color.FromArgb(0,0,0)
        };

        #region Properties

        private int contentPositionX = 0;
        public int ContentPositionX
        {
            get { return this.contentPositionX; }
            set
            {
                if (this.contentPositionX != value)
                {
                    this.contentPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetContent();
                }
            }
        }

        private int contentPositionY = 0;
        public int ContentPositionY
        {
            get { return this.contentPositionY; }
            set
            {
                if (this.contentPositionY != value)
                {
                    this.contentPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetContent();
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
                    this.Vinsert_SetContent();
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
                    this.Vinsert_SetContent();
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
                    this.Vinsert_SetContent();
                }
            }
        }

        private int taskCounterPositionX = 0;
        public int TaskCounterPositionX
        {
            get { return this.taskCounterPositionX; }
            set
            {
                if (this.taskCounterPositionX != value)
                {
                    this.taskCounterPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTaskCounter();
                }
            }
        }

        private int taskCounterPositionY = 0;
        public int TaskCounterPositionY
        {
            get { return this.taskCounterPositionY; }
            set
            {
                if (this.taskCounterPositionY != value)
                {
                    this.taskCounterPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTaskCounter();
                }
            }
        }

        public const int TaskCounterPenaltyCountMax = 13;

        private int taskCounterSize = 13;
        public int TaskCounterSize
        {
            get { return this.taskCounterSize; }
            set
            {
                if (this.taskCounterSize != value)
                {
                    if (value < 0) value = 0;
                    this.taskCounterSize = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTaskCounter();
                }
            }
        }

        private ColorStates leftPlayerFlagColor1 = ColorStates.Neutral;
        [NotSerialized]
        public ColorStates LeftPlayerFlagColor1
        {
            get { return this.leftPlayerFlagColor1; }
            set
            {
                if (this.leftPlayerFlagColor1 != value)
                {
                    this.leftPlayerFlagColor1 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private ColorStates leftPlayerFlagColor2 = ColorStates.Neutral;
        [NotSerialized]
        public ColorStates LeftPlayerFlagColor2
        {
            get { return this.leftPlayerFlagColor2; }
            set
            {
                if (this.leftPlayerFlagColor2 != value)
                {
                    this.leftPlayerFlagColor2 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private ColorStates leftPlayerFlagColor3 = ColorStates.Neutral;
        [NotSerialized]
        public ColorStates LeftPlayerFlagColor3
        {
            get { return this.leftPlayerFlagColor3; }
            set
            {
                if (this.leftPlayerFlagColor3 != value)
                {
                    this.leftPlayerFlagColor3 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private ColorStates rightPlayerFlagColor1 = ColorStates.Neutral;
        [NotSerialized]
        public ColorStates RightPlayerFlagColor1
        {
            get { return this.rightPlayerFlagColor1; }
            set
            {
                if (this.rightPlayerFlagColor1 != value)
                {
                    this.rightPlayerFlagColor1 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private ColorStates rightPlayerFlagColor2 = ColorStates.Neutral;
        [NotSerialized]
        public ColorStates RightPlayerFlagColor2
        {
            get { return this.rightPlayerFlagColor2; }
            set
            {
                if (this.rightPlayerFlagColor2 != value)
                {
                    this.rightPlayerFlagColor2 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private ColorStates rightPlayerFlagColor3 = ColorStates.Neutral;
        [NotSerialized]
        public ColorStates RightPlayerFlagColor3
        {
            get { return this.rightPlayerFlagColor3; }
            set
            {
                if (this.rightPlayerFlagColor3 != value)
                {
                    this.rightPlayerFlagColor3 = value;
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
        public bool BuzzerMode
        {
            get { return this.buzzerMode; }
            set
            {
                if (this.buzzerMode != value)
                {
                    this.buzzerMode = value;
                    this.on_PropertyChanged();
                }
                this.BuzzeredPlayer = PlayerSelection.NotSelected;
            }
        }

        internal PlayerSelection buzzeredPlayer = PlayerSelection.NotSelected;
        [NotSerialized]
        public PlayerSelection BuzzeredPlayer
        {
            get { return this.buzzeredPlayer; }
            internal set
            {
                if (this.buzzeredPlayer != value)
                {
                    this.buzzeredPlayer = value;
                    this.Vhost_SetBuzzeredPlayer();
                    this.on_PropertyChanged();
                }
            }
        }

        private Insert insertScene;
        public override Scene.States InsertSceneStatus {
            get {
                if (this.insertScene is Insert) return this.insertScene.Status;
                else return Scene.States.Unloaded;
            }
        }

        private Host hostScene;
        public override Scene.States HostSceneStatus
        {
            get
            {
                if (this.hostScene is Host) return this.hostScene.Status;
                else return Scene.States.Unloaded;
            }
        }

        private Player leftPlayerScene;
        public override Scene.States LeftPlayerSceneStatus
        {
            get
            {
                if (this.leftPlayerScene is Player) return this.leftPlayerScene.Status;
                else return Scene.States.Unloaded;
            }
        }

        private Player rightPlayerScene;
        public override Scene.States RightPlayerSceneStatus
        {
            get
            {
                if (this.rightPlayerScene is Player) return this.rightPlayerScene.Status;
                else return Scene.States.Unloaded;
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.PaintingFlags'", typeIdentifier);
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

            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Timer.Alarm1Fired -= this.timer_Alarm1Fired;
            this.insertScene.Timer.Alarm2Fired -= this.timer_Alarm2Fired;
            this.insertScene.Timer.StopFired -= this.timer_StopFired;
            this.insertScene.Timer.PropertyChanged -= this.timer_PropertyChanged;
            this.insertScene.Dispose();

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
            this.BuzzerMode = false;
            this.BuzzeredPlayer = PlayerSelection.NotSelected;
            this.resetValues();
            this.TaskCounter = 0;
            this.SelectDataset(0);
        }

        public override void Next() {
            base.Next();
            this.BuzzerMode = false;
            this.BuzzeredPlayer = PlayerSelection.NotSelected;
            this.resetValues();
            this.TaskCounter++;
            this.SelectDataset(this.SelectedDatasetIndex + 1);
        }

        private void resetValues()
        {
            this.LeftPlayerFlagColor1 = ColorStates.Neutral;
            this.LeftPlayerFlagColor2 = ColorStates.Neutral;
            this.LeftPlayerFlagColor3 = ColorStates.Neutral;
            this.RightPlayerFlagColor1 = ColorStates.Neutral;
            this.RightPlayerFlagColor2 = ColorStates.Neutral;
            this.RightPlayerFlagColor3 = ColorStates.Neutral;
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

        public override void New() {
            base.New();
            this.Filename = string.Empty;
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

        public override void Vinsert_TimerIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_TimerIn(this.insertScene.Timer); }
        public override void Vinsert_SetTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetTimer(this.insertScene.Timer); }
        public override void Vinsert_StartTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Timer.StartTimer(); }
        public override void Vinsert_StopTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Timer.StopTimer(); }
        public override void Vinsert_ContinueTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Timer.ContinueTimer(); }
        public override void Vinsert_ResetTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Timer.ResetTimer(); }
        public override void Vinsert_TimerOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Timer.ToOut(); }

        public void Vinsert_TaskCounterIn()
        {
            this.Vinsert_SetTaskCounter();
            if (this.insertScene is Insert &&
                this.insertScene.TaskCounter is VentuzScenes.GamePool._Modules.TaskCounter) this.insertScene.TaskCounter.ToIn();
        }
        public void Vinsert_SetTaskCounter() { if (this.insertScene is Insert) this.Vinsert_SetTaskCounter(this.insertScene.TaskCounter, this.TaskCounter); }
        public void Vinsert_SetTaskCounter(
            VentuzScenes.GamePool._Modules.TaskCounter scene,
            int counter)
        {
            if (scene is VentuzScenes.GamePool._Modules.TaskCounter)
            {
                scene.SetPositionX(this.TaskCounterPositionX);
                scene.SetPositionY(this.TaskCounterPositionY);
                scene.SetStyle(VentuzScenes.GamePool._Modules.TaskCounter.Styles.Numeric);
                scene.SetSize(this.TaskCounterSize);
                scene.SetCounter(counter);
            }
        }
        public void Vinsert_TaskCounterOut()
        {
            if (this.insertScene is Insert &&
                this.insertScene.TaskCounter is VentuzScenes.GamePool._Modules.TaskCounter) this.insertScene.TaskCounter.ToOut();
        }

        internal void Vinsert_TextInsertIn()
        {
            this.Vinsert_SetTextInsert();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.TextInsert.ToIn();
            if (this.TaskCounter > 0 && this.TaskCounter <= this.TaskCounterSize) this.Vinsert_TaskCounterIn();
        }
        internal void Vinsert_SetTextInsert()
        {
            if (this.insertScene is VRemote4.HandlerSi.Scene && this.SelectedDataset is DatasetContent) this.Vinsert_SetTextInsert(this.insertScene.TextInsert, this.SelectedDataset);
        }
        internal void Vinsert_SetTextInsert(
            VentuzScenes.GamePool._Modules.TextInsert scene,
            DatasetContent dataset)
        {
            if (scene is VentuzScenes.GamePool._Modules.TextInsert &&
                dataset is DatasetContent)
            {
                scene.SetStyle(this.TextInsertStyle);
                scene.SetPositionX(this.TextInsertPositionX);
                scene.SetPositionY(this.TextInsertPositionY);
                scene.SetText(dataset.Name);
            }
        }
        internal void Vinsert_TextInsertOut() { 
            if (this.InsertSceneStatus == Scene.States.Available) this.insertScene.TextInsert.ToOut();
            this.Vinsert_TaskCounterOut();
        }

        internal void Vinsert_ContentIn()
        {
            this.Vinsert_SetContent();
            if (this.InsertSceneStatus == Scene.States.Available)
            {
                this.insertScene.Game.SetSolutionOut();
                this.insertScene.Game.ToIn();
            }
        }
        internal void Vinsert_SolutionIn()
        {
            this.Vinsert_SetContent();
            if (this.InsertSceneStatus == Scene.States.Available) this.insertScene.Game.SolutionToIn();
        }
        internal void Vinsert_SetContent() {
            if (this.insertScene is Scene && this.SelectedDataset is DatasetContent) this.Vinsert_SetContent(
                this.insertScene.Game, 
                this.SelectedDataset,
                this.LeftPlayerFlagColor1,
                this.LeftPlayerFlagColor2,
                this.LeftPlayerFlagColor3,
                this.RightPlayerFlagColor1,
                this.RightPlayerFlagColor2,
                this.RightPlayerFlagColor3);
        }
        internal void Vinsert_SetContent(
            Game scene,
            DatasetContent dataset,
            ColorStates leftPlayerFlagColor1,
            ColorStates leftPlayerFlagColor2,
            ColorStates leftPlayerFlagColor3,
            ColorStates rightPlayerFlagColor1,
            ColorStates rightPlayerFlagColor2,
            ColorStates rightPlayerFlagColor3) {
            if (scene is Game &&
                dataset is DatasetContent) {
                scene.SetPositionX(this.ContentPositionX);
                scene.SetPositionY(this.ContentPositionY);
                scene.SetOrientation(dataset.Orientation);
                scene.SetSolutionFilename(dataset.PictureFilename);
                scene.SetLeftPlayerFlagColor(1, leftPlayerFlagColor1);
                scene.SetLeftPlayerFlagColor(2, leftPlayerFlagColor2);
                scene.SetLeftPlayerFlagColor(3, leftPlayerFlagColor3);
                scene.SetRightPlayerFlagColor(1, rightPlayerFlagColor1);
                scene.SetRightPlayerFlagColor(2, rightPlayerFlagColor2);
                scene.SetRightPlayerFlagColor(3, rightPlayerFlagColor3);
            }
        }
        internal void Vinsert_ContentOut() { if (this.InsertSceneStatus == Scene.States.Available) this.insertScene.Game.ToOut(); }

        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        internal void Vstage_ContentIn()
        {
            this.Vhost_ContentIn();
            this.VleftPlayer_ContentIn();
            this.VrightPlayer_ContentIn();
        }

        internal void Vstage_ContentOut()
        {
            this.Vhost_ContentOut();
            this.VleftPlayer_ContentOut();
            this.VrightPlayer_ContentOut();
        }

        public override void Vhost_LoadScene() { this.hostScene.Load(); }
        internal void Vhost_ContentIn()
        {
            if (this.hostScene is Host scene &&
                this.SelectedDataset is DatasetContent dataset)
            {
                this.Vhost_SetContent(
                    scene,
                    dataset,
                    this.TaskCounter);
                this.Vhost_SetPlayerInput(
                    scene,
                    this.LeftPlayerFlagColor1,
                    this.LeftPlayerFlagColor2,
                    this.LeftPlayerFlagColor3,
                    this.RightPlayerFlagColor1,
                    this.RightPlayerFlagColor2,
                    this.RightPlayerFlagColor3);
                this.hostScene.ToIn();
            }
        }
        internal void Vhost_SetContent(
            Host scene,
            DatasetContent dataset,
            int taskCounter)
        {
            if (scene is Host &&
                dataset is DatasetContent)
            {
                string hostText = dataset.Name;
                if (taskCounter == 0) hostText = string.Format("Beispiel: {0}", hostText);
                else if (taskCounter > 0 && taskCounter <= this.TaskCounterSize) hostText = string.Format("{0}/{1}: {2}", taskCounter.ToString(), this.TaskCounterSize.ToString(), hostText);
                scene.SetText(hostText);
                scene.SetFilename(dataset.PictureFilename);
                scene.SetOrientation(dataset.Orientation);
            }
        }
        internal void Vhost_SetPlayerInput()
        {
            if (this.hostScene is Host scene)
            {
                this.Vhost_SetPlayerInput(
                    scene,
                    this.LeftPlayerFlagColor1,
                    this.LeftPlayerFlagColor2,
                    this.LeftPlayerFlagColor3,
                    this.RightPlayerFlagColor1,
                    this.RightPlayerFlagColor2,
                    this.RightPlayerFlagColor3);
            }
        }
        internal void Vhost_SetPlayerInput(
            Host scene,
            ColorStates leftPlayerFlagColor1,
            ColorStates leftPlayerFlagColor2,
            ColorStates leftPlayerFlagColor3,
            ColorStates rightPlayerFlagColor1,
            ColorStates rightPlayerFlagColor2,
            ColorStates rightPlayerFlagColor3)
        {
            if (scene is Host)
            {
                scene.SetLeftPlayerFlagColor(1, leftPlayerFlagColor1);
                scene.SetLeftPlayerFlagColor(2, leftPlayerFlagColor2);
                scene.SetLeftPlayerFlagColor(3, leftPlayerFlagColor3);
                scene.SetRightPlayerFlagColor(1, rightPlayerFlagColor1);
                scene.SetRightPlayerFlagColor(2, rightPlayerFlagColor2);
                scene.SetRightPlayerFlagColor(3, rightPlayerFlagColor3);
            }
        }
        internal void Vhost_SetBuzzeredPlayer()
        {
            this.Vhost_SetBuzzeredPlayer(this.hostScene, this.BuzzeredPlayer);
        }
        internal void Vhost_SetBuzzeredPlayer(
            Host scene,
            PlayerSelection buzzeredPlayer)
        {
            if (scene is Host) scene.SetBuzzeredPlayer(buzzeredPlayer);
        }
        internal void Vhost_ContentOut()
        {
            if (this.hostScene is Host) this.hostScene.ToOut();
        }
        public override void Vhost_UnloadScene() { this.hostScene.Unload(); }

        internal void Vplayer_ContentIn(
            Player scene,
            DatasetContent dataset)
        {
            if (scene is Player &&
                dataset is DatasetContent)
            {
                Vplayer_SetContent(scene, dataset);
                scene.ToIn();
            }
        }
        internal void Vplayer_SetContent(
            Player scene,
            DatasetContent dataset)
        {
            if (scene is Player &&
                dataset is DatasetContent)
            {
                scene.SetTask(dataset.Name);
                scene.SetOrientation(dataset.Orientation);
            }
        }
        internal void Vplayer_ContentOut(
            Player scene)
        {
            if (scene is Player)
            {
                scene.ToOut();
            }
        }

        public override void Vleftplayer_LoadScene() { this.leftPlayerScene.Load(); }
        internal void Vleftplayer_Reset() { if (this.leftPlayerScene is Player) this.leftPlayerScene.Reset(); }
        internal void VleftPlayer_ContentIn() { this.Vplayer_ContentIn(this.leftPlayerScene, this.SelectedDataset); }
        internal void Vleftplayer_Unlock() { if (this.leftPlayerScene is Player) this.leftPlayerScene.Unlock(); }
        internal void Vleftplayer_Lock() { if (this.leftPlayerScene is Player) this.leftPlayerScene.Lock(); }
        internal void VleftPlayer_ContentOut() { this.Vplayer_ContentOut(this.leftPlayerScene); }
        public override void Vleftplayer_UnloadScene() { this.leftPlayerScene.Unload(); }

        public override void Vrightplayer_LoadScene() { this.rightPlayerScene.Load(); }
        internal void Vrightplayer_Reset() { if (this.rightPlayerScene is Player) this.rightPlayerScene.Reset(); }
        internal void VrightPlayer_ContentIn() { this.Vplayer_ContentIn(this.rightPlayerScene, this.SelectedDataset); }
        internal void Vrightplayer_Unlock() { if (this.rightPlayerScene is Player) this.rightPlayerScene.Unlock(); }
        internal void Vrightplayer_Lock() { if (this.rightPlayerScene is Player) this.rightPlayerScene.Lock(); }
        internal void VrightPlayer_ContentOut() { this.Vplayer_ContentOut(this.rightPlayerScene); }
        public override void Vrightplayer_UnloadScene() { this.rightPlayerScene.Unload(); }

        public override void ClearGraphic() {
            base.ClearGraphic();
            this.insertScene.Clear();
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

        protected override void sync_timer_PropertyChanged(object content)
        {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs)
            {
                if (e.PropertyName == "CurrentTime") this.TimerCurrentTime = this.insertScene.Timer.CurrentTime;
                else if (e.PropertyName == "IsRunning") this.TimerIsRunning = this.insertScene.Timer.IsRunning;
            }
        }

        protected override void sync_timer_StopFired(object content)
        {
            base.sync_timer_StopFired(content);
            this.Vinsert_TimerOut();
        }

        void leftPlayerScene_OKButtonPressed(object sender, EventArgs e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftPlayerScene_OKButtonPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftPlayerScene_OKButtonPressed(object content)
        {
            try
            {
                this.LeftPlayerFlagColor1 = (ColorStates)Enum.ToObject(typeof(ColorStates), this.leftPlayerScene.FlagColor1);
                this.LeftPlayerFlagColor2 = (ColorStates)Enum.ToObject(typeof(ColorStates), this.leftPlayerScene.FlagColor2);
                this.LeftPlayerFlagColor3 = (ColorStates)Enum.ToObject(typeof(ColorStates), this.leftPlayerScene.FlagColor3);
            }
            catch (Exception)
            {
                this.LeftPlayerFlagColor1 = ColorStates.Neutral;
                this.LeftPlayerFlagColor2 = ColorStates.Neutral;
                this.LeftPlayerFlagColor3 = ColorStates.Neutral;
            }
            if (this.BuzzerMode)
            {
                if (this.BuzzeredPlayer == PlayerSelection.NotSelected) 
                    this.BuzzeredPlayer = PlayerSelection.LeftPlayer;
            }
            this.Vhost_SetPlayerInput();
        }

        void rightPlayerScene_OKButtonPressed(object sender, EventArgs e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerScene_OKButtonPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerScene_OKButtonPressed(object content)
        {
            try
            {
                this.RightPlayerFlagColor1 = (ColorStates)Enum.ToObject(typeof(ColorStates), this.rightPlayerScene.FlagColor1);
                this.RightPlayerFlagColor2 = (ColorStates)Enum.ToObject(typeof(ColorStates), this.rightPlayerScene.FlagColor2);
                this.RightPlayerFlagColor3 = (ColorStates)Enum.ToObject(typeof(ColorStates), this.rightPlayerScene.FlagColor3);
            }
            catch (Exception)
            {
                this.RightPlayerFlagColor1 = ColorStates.Neutral;
                this.RightPlayerFlagColor2 = ColorStates.Neutral;
                this.RightPlayerFlagColor3 = ColorStates.Neutral;
            }
            if (this.BuzzerMode)
            {
                if (this.BuzzeredPlayer == PlayerSelection.NotSelected)
                    this.BuzzeredPlayer = PlayerSelection.RightPlayer;
            }
            this.Vhost_SetPlayerInput();
        }

        #endregion
    }
}
