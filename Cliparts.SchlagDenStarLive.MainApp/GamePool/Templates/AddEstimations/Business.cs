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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.AddEstimations;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.AddEstimations {

    public class Data {
        public DatasetContent[] DataList;
        public int SelectedIndex;
    }

    public class DatasetContentItem : Cliparts.Messaging.Message, INotifyPropertyChanged {

        #region Properties

        private string keyword = string.Empty;
        public string Keyword {
            get { return this.keyword; }
            set {
                if (this.keyword != value) {
                    if (string.IsNullOrEmpty(value)) this.keyword = string.Empty;
                    else this.keyword = value;
                    this.buildToString();
                    this.on_PropertyChanged();
                }
            }
        }

        private string hostText = string.Empty;
        public string HostText {
            get { return this.hostText; }
            set {
                if (this.hostText != value) {
                    if (string.IsNullOrEmpty(value)) this.hostText = string.Empty;
                    else this.hostText = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int solution = 0;
        public int Solution {
            get { return this.solution; }
            set {
                if (this.solution != value) {
                    this.solution = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DatasetContentItem() { }

        public void Clone(
            DatasetContentItem source) {
            if (source is DatasetContentItem) {
                this.Keyword = source.Keyword;
                this.HostText = source.HostText;
                this.Solution = source.Solution;
            }
            else {
                this.Keyword = string.Empty;
                this.HostText = string.Empty;
                this.Solution = 0;
            }
        }

        private void buildToString() { this.toString = this.Keyword; }

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

    public class DatasetContent : Cliparts.Messaging.Message, INotifyPropertyChanged {

        #region Properties

        private SynchronizationContext syncContext;

        public const int ItemsCount = 3;

        private List<DatasetContentItem> itemList = new List<DatasetContentItem>();
        public DatasetContentItem[] ItemList {
            get { return this.itemList.ToArray(); }
            set {
                this.repressPropertyChanged = true;
                if (value is DatasetContentItem[]) {
                    for (int i = 0; i < this.itemList.Count; i++) {
                        if (value.Length > i) this.itemList[i].Clone(value[i]);
                        else this.itemList[i].Clone(null);
                    }
                }
                this.calcSolution();
                this.repressPropertyChanged = false;
                this.buildToString();
                this.on_PropertyChanged("ToString");
            }
        }

        private int solution = 0;
        [XmlIgnore]
        public int Solution {
            get { return this.solution; }
            protected set {
                if (this.solution != value) {
                    this.solution = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string toString = string.Empty;

        private bool repressPropertyChanged = false;

        #endregion


        #region Funktionen

        public DatasetContent() { this.buildItemList(); }
        public DatasetContent(
            SynchronizationContext syncContext) {
            this.syncContext = syncContext;
            this.buildItemList();
            this.buildToString();
        }

        private void buildItemList() {
            while (this.itemList.Count < ItemsCount) {
                DatasetContentItem item = new DatasetContentItem();
                item.PropertyChanged += this.item_PropertyChanged;
                this.itemList.Add(item);
            }
            this.calcSolution();
        }

        public DatasetContentItem GetItem(
            int index) {
            if (index >= 0 &&
                index < this.itemList.Count) return this.itemList[index];
            else return null;
        }

        public void Clone(
            DatasetContent source) {
            if (source is DatasetContent) {
                this.ItemList = source.ItemList;
            }
            else {
                this.ItemList = new DatasetContentItem[0];
            }
            this.buildToString();
        }

        private void buildToString() {
            this.toString = string.Format("{0} - {1} - {2}", this.ItemList[0].Keyword, this.ItemList[1].Keyword, this.ItemList[2].Keyword);
        }

        private void calcSolution() {
            int solution = this.ItemList[0].Solution + this.ItemList[1].Solution + this.ItemList[2].Solution;
            this.Solution = solution;
        }

        public override string ToString() { return this.toString; }

        #endregion


        #region Events.Outgoing

        public event PropertyChangedEventHandler PropertyChanged;
        protected void on_PropertyChanged([CallerMemberName]string callerName = "") { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }
        protected void on_PropertyChanged(PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, e); }
        protected void on_PropertyChanged(object sender, PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(sender, this.PropertyChanged, e); }

        #endregion

        #region Events.Incoming

        void item_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_item_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_item_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs &&
                !this.repressPropertyChanged) {
                this.on_PropertyChanged(e.PropertyName);
                if (e.PropertyName == "Keyword") {
                    this.buildToString();
                    this.on_PropertyChanged("ToString");
                }
                if (e.PropertyName == "Solution") this.calcSolution();
            }
        }

        #endregion

    }


    public class Business : _Base.Score.Business {

        public enum PlayerSelection { NotSelected, LeftPlayer, RightPlayer, BothPlayers }

        #region Properties

        private int textInsertPositionX = 0;
        public int TextInsertPositionX {
            get { return this.textInsertPositionX; }
            set {
                if (this.textInsertPositionX != value) {
                    this.textInsertPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTextContent();
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
                    this.Vinsert_SetTextContent();
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
                    this.Vinsert_SetTextContent();
                }
            }
        }

        private int leftPlayerEstimation_1 = -1;
        public int LeftPlayerEstimation_1 {
            get { return this.leftPlayerEstimation_1; }
            set {
                if (this.leftPlayerEstimation_1 != value) {
                    this.leftPlayerEstimation_1 = value;
                    this.on_PropertyChanged();
                    this.calcLeftSum();
                }
            }
        }

        private int leftPlayerEstimation_2 = -1;
        public int LeftPlayerEstimation_2 {
            get { return this.leftPlayerEstimation_2; }
            set {
                if (this.leftPlayerEstimation_2 != value) {
                    this.leftPlayerEstimation_2 = value;
                    this.on_PropertyChanged();
                    this.calcLeftSum();
                }
            }
        }

        private int leftPlayerEstimation_3 = -1;
        public int LeftPlayerEstimation_3 {
            get { return this.leftPlayerEstimation_3; }
            set {
                if (this.leftPlayerEstimation_3 != value) {
                    this.leftPlayerEstimation_3 = value;
                    this.on_PropertyChanged();
                    this.calcLeftSum();
                }
            }
        }

        private int leftPlayerSum = 0;
        public int LeftPlayerSum {
            get { return this.leftPlayerSum; }
            private set {
                if (this.leftPlayerSum != value) {
                    this.leftPlayerSum = value;
                    this.on_PropertyChanged();
                    this.calcOffsets();
                }
            }
        }

        private int leftPlayerOffset = 0;
        public int LeftPlayerOffset {
            get { return this.leftPlayerOffset; }
            private set {
                if (this.leftPlayerOffset != value) {
                    this.leftPlayerOffset = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerEstimation_1 = -1;
        public int RightPlayerEstimation_1 {
            get { return this.rightPlayerEstimation_1; }
            set {
                if (this.rightPlayerEstimation_1 != value) {
                    this.rightPlayerEstimation_1 = value;
                    this.on_PropertyChanged();
                    this.calcRightSum();
                }
            }
        }

        private int rightPlayerEstimation_2 = -1;
        public int RightPlayerEstimation_2 {
            get { return this.rightPlayerEstimation_2; }
            set {
                if (this.rightPlayerEstimation_2 != value) {
                    this.rightPlayerEstimation_2 = value;
                    this.on_PropertyChanged();
                    this.calcRightSum();
                }
            }
        }

        private int rightPlayerEstimation_3 = -1;
        public int RightPlayerEstimation_3 {
            get { return this.rightPlayerEstimation_3; }
            set {
                if (this.rightPlayerEstimation_3 != value) {
                    this.rightPlayerEstimation_3 = value;
                    this.on_PropertyChanged();
                    this.calcRightSum();
                }
            }
        }

        private int rightPlayerSum = 0;
        public int RightPlayerSum {
            get { return this.rightPlayerSum; }
            private set {
                if (this.rightPlayerSum != value) {
                    this.rightPlayerSum = value;
                    this.on_PropertyChanged();
                    this.calcOffsets();
                }
            }
        }

        private int rightPlayerOffset = 0;
        public int RightPlayerOffset {
            get { return this.rightPlayerOffset; }
            private set {
                if (this.rightPlayerOffset != value) {
                    this.rightPlayerOffset = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private PlayerSelection closestPlayer = PlayerSelection.NotSelected;
        public PlayerSelection ClosestPlayer {
            get { return this.closestPlayer; }
            private set {
                if (this.closestPlayer != value) {
                    this.closestPlayer = value;
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
                this.repressPropertyChanged = true;
                this.tryRemoveAllDatasets();
                if (value is DatasetContent[]) {
                    foreach (DatasetContent item in value) this.tryAddDataset(item, -1);
                }
                this.repressPropertyChanged = false;
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.AddEstimations'", typeIdentifier);
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

            this.calcOffsets();

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;

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
            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;

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
            this.LeftPlayerEstimation_1 = 0;
            this.LeftPlayerEstimation_2 = 0;
            this.LeftPlayerEstimation_3 = 0;
            this.RightPlayerEstimation_1 = 0;
            this.RightPlayerEstimation_2 = 0;
            this.RightPlayerEstimation_3 = 0;
            this.calcOffsets();
        }

        internal void Resolve() {
            if (this.SelectedDatasetIndex > 0 ||
                !this.SampleIncluded) {
                switch (this.ClosestPlayer) {
                    case PlayerSelection.NotSelected:
                        break;
                    case PlayerSelection.LeftPlayer:
                        this.LeftPlayerScore++;
                        break;
                    case PlayerSelection.RightPlayer:
                        this.RightPlayerScore++;
                        break;
                    case PlayerSelection.BothPlayers:
                        this.LeftPlayerScore++;
                        this.RightPlayerScore++;
                        break;
                }
            }
        }

        public override void Next() {
            base.Next();
            this.TaskCounter++;
            this.SelectDataset(this.SelectedDatasetIndex + 1);
            this.LeftPlayerEstimation_1 = 0;
            this.LeftPlayerEstimation_2 = 0;
            this.LeftPlayerEstimation_3 = 0;
            this.RightPlayerEstimation_1 = 0;
            this.RightPlayerEstimation_2 = 0;
            this.RightPlayerEstimation_3 = 0;
            this.calcOffsets();
        }

        private void calcLeftSum() {
            int sum = 0;
            if (this.LeftPlayerEstimation_1 > 0) sum += this.LeftPlayerEstimation_1;
            if (this.LeftPlayerEstimation_2 > 0) sum += this.LeftPlayerEstimation_2;
            if (this.LeftPlayerEstimation_3 > 0) sum += this.LeftPlayerEstimation_3;
            this.LeftPlayerSum = sum;
        }

        private void calcRightSum() {
            int sum = 0;
            if (this.RightPlayerEstimation_1 > 0) sum += this.RightPlayerEstimation_1;
            if (this.RightPlayerEstimation_2 > 0) sum += this.RightPlayerEstimation_2;
            if (this.RightPlayerEstimation_3 > 0) sum += this.RightPlayerEstimation_3;
            this.RightPlayerSum = sum;
        }

        private void calcOffsets() {
            if (this.SelectedDataset is DatasetContent) {
                this.LeftPlayerOffset = this.LeftPlayerSum - this.SelectedDataset.Solution;
                this.RightPlayerOffset = this.RightPlayerSum - this.SelectedDataset.Solution;
                if (this.LeftPlayerSum > 0 && this.RightPlayerSum > 0) {
                    if (Math.Abs(this.LeftPlayerOffset) < Math.Abs(this.RightPlayerOffset)) this.ClosestPlayer = PlayerSelection.LeftPlayer;
                    else if (Math.Abs(this.LeftPlayerOffset) > Math.Abs(this.RightPlayerOffset)) this.ClosestPlayer = PlayerSelection.RightPlayer;
                    else this.ClosestPlayer = PlayerSelection.BothPlayers;
                }
                else this.ClosestPlayer = PlayerSelection.NotSelected;
            }
            else {
                this.LeftPlayerOffset = 0;
                this.RightPlayerOffset = 0;
                this.ClosestPlayer = PlayerSelection.NotSelected;
            }
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
            this.calcOffsets();
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
                DatasetContent dataset = new DatasetContent(this.syncContext);
                dataset.Clone(newDataset);
                dataset.Error += this.dataset_Error;
                dataset.PropertyChanged += this.dataset_PropertyChanged;
                if (insertIndex >= 0 &&
                    insertIndex < this.DatasetsCount) {
                    this.dataList.Insert(insertIndex, dataset);
                    this.names.Insert(insertIndex, dataset.ToString());
                }
                else {
                    this.dataList.Add(dataset);
                    this.names.Add(dataset.ToString());
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
            this.repressPropertyChanged = true;
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
            this.repressPropertyChanged = false;
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
        public void Import(
            string filename) {
            string subSender = "Import";
            if (File.Exists(filename)) {
                System.IO.StreamReader file = null;
                try {
                    string line;
                    int lineCounter = 0;
                    DatasetContent newDataset = null;
                    file = new System.IO.StreamReader(filename, Encoding.UTF7);
                    while ((line = file.ReadLine()) != null) {
                        if (string.IsNullOrEmpty(line)) {
                            if (newDataset is DatasetContent) {
                                this.tryAddDataset(newDataset, -1);
                            }
                            newDataset = null;
                            lineCounter = 0;
                        }
                        else {
                            if (newDataset == null) newDataset = new DatasetContent();
                            lineCounter++;
                            if (lineCounter == 1) newDataset.ItemList[0].HostText = line.Trim();
                            else if (lineCounter == 2) newDataset.ItemList[0].Solution = Convert.ToInt32(line.Trim());
                            else if (lineCounter == 3) newDataset.ItemList[0].Keyword = line.Trim();
                            else if (lineCounter == 4) newDataset.ItemList[1].HostText = line.Trim();
                            else if (lineCounter == 5) newDataset.ItemList[1].Solution = Convert.ToInt32(line.Trim());
                            else if (lineCounter == 6) newDataset.ItemList[1].Keyword = line.Trim();
                            else if (lineCounter == 7) newDataset.ItemList[2].HostText = line.Trim();
                            else if (lineCounter == 8) newDataset.ItemList[2].Solution = Convert.ToInt32(line.Trim());
                            else if (lineCounter == 9) newDataset.ItemList[2].Keyword = line.Trim();
                        }
                    }
                    if (newDataset is DatasetContent) {
                        this.tryAddDataset(newDataset, -1);
                    }
                    file.Close();
                }
                catch (Exception exc) {
                    if (file != null) file.Close();
                    this.on_Error(subSender, exc.Message);
                }
                this.on_PropertyChanged("NameList");
            }
            else {
                // Fehler weitergeben
                this.on_Error(subSender, "Datei nicht vorhanden - '" + filename + "'");
            }
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }
        internal void Vinsert_ContentIn() {
            this.Vinsert_SetContent();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.ContentToIn();
        }
        internal void Vinsert_SetContent() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                if (this.SelectedDataset is DatasetContent) {
                    this.insertScene.SetContentQuestionText(1, this.SelectedDataset.ItemList[0].Keyword);
                    this.insertScene.SetContentSolutionAmount(1, string.Empty);
                    this.insertScene.SetContentQuestionText(2, this.SelectedDataset.ItemList[1].Keyword);
                    this.insertScene.SetContentSolutionAmount(2, string.Empty);
                    this.insertScene.SetContentQuestionText(3, this.SelectedDataset.ItemList[2].Keyword);
                    this.insertScene.SetContentSolutionAmount(3, string.Empty);
                    this.insertScene.SetContentSolutionSum(string.Empty);
                    this.insertScene.SetContentLeftAnswersName(this.LeftPlayerName);
                    this.insertScene.SetContentLeftAnswersAmount(1, this.LeftPlayerEstimation_1.ToString());
                    this.insertScene.SetContentLeftAnswersAmount(2, this.LeftPlayerEstimation_2.ToString());
                    this.insertScene.SetContentLeftAnswersAmount(3, this.LeftPlayerEstimation_3.ToString());
                    this.insertScene.SetContentLeftAnswersSum(this.LeftPlayerSum.ToString());
                    this.insertScene.SetContentLeftAnswersBorderOut();
                    this.insertScene.SetContentLeftAnswersOffsetOut();
                    this.insertScene.SetContentRightAnswersName(this.RightPlayerName);
                    this.insertScene.SetContentRightAnswersAmount(1, this.RightPlayerEstimation_1.ToString());
                    this.insertScene.SetContentRightAnswersAmount(2, this.RightPlayerEstimation_2.ToString());
                    this.insertScene.SetContentRightAnswersAmount(3, this.RightPlayerEstimation_3.ToString());
                    this.insertScene.SetContentRightAnswersSum(this.RightPlayerSum.ToString());
                    this.insertScene.SetContentRightAnswersBorderOut();
                    this.insertScene.SetContentRightAnswersOffsetOut();
                }
            }
        }
        internal void Vinsert_ShowSolutions() {
            this.Vinsert_SetContent();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.ContentSolutionsToIn();
        }
        internal void Vinsert_ShowSolution(
            int id) {
            int sum = 0;
            if (id >= 1) {
                this.insertScene.SetContentSolutionAmount(1, this.SelectedDataset.ItemList[0].Solution.ToString());
                sum += this.SelectedDataset.ItemList[0].Solution;
            }
            else this.insertScene.SetContentSolutionAmount(1, string.Empty);
            if (id >= 2) {
                this.insertScene.SetContentSolutionAmount(2, this.SelectedDataset.ItemList[1].Solution.ToString());
                sum += this.SelectedDataset.ItemList[1].Solution;
            }
            else this.insertScene.SetContentSolutionAmount(2, string.Empty);
            if (id >= 3) {
                this.insertScene.SetContentSolutionAmount(3, this.SelectedDataset.ItemList[2].Solution.ToString());
                sum += this.SelectedDataset.ItemList[2].Solution;
            }
            else this.insertScene.SetContentSolutionAmount(3, string.Empty);
            if (id >= 1) this.insertScene.SetContentSolutionSum(sum.ToString());
            else this.insertScene.SetContentSolutionSum(string.Empty);
        }
        internal void Vinsert_Resolve() {
            this.calcOffsets();
            this.insertScene.SetContentLeftAnswersOffsetAmount(this.LeftPlayerOffset.ToString());
            this.insertScene.ContentLeftAnswersOffsetToIn();
            this.insertScene.SetContentRightAnswersOffsetAmount(this.RightPlayerOffset.ToString());
            this.insertScene.ContentRightAnswersOffsetToIn();
            switch (this.ClosestPlayer) {
                case PlayerSelection.NotSelected:
                    this.insertScene.ContentLeftAnswersBorderToOut();
                    this.insertScene.ContentRightAnswersBorderToOut();
                    break;
                case PlayerSelection.LeftPlayer:
                    this.insertScene.ContentLeftAnswersBorderToIn();
                    this.insertScene.ContentRightAnswersBorderToOut();
                    break;
                case PlayerSelection.RightPlayer:
                    this.insertScene.ContentLeftAnswersBorderToOut();
                    this.insertScene.ContentRightAnswersBorderToIn();
                    break;
                case PlayerSelection.BothPlayers:
                    this.insertScene.ContentLeftAnswersBorderToIn();
                    this.insertScene.ContentRightAnswersBorderToIn();
                    break;
            }
        }
        internal void Vinsert_ContentOut() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.ContentToOut();
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
        internal void Vinsert_TextContentIn() {
            this.Vinsert_SetTextContent();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.TextInsert.ToIn();
        }
        internal void Vinsert_SetTextContent() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {                
                this.insertScene.TextInsert.SetStyle(this.TextInsertStyle);
                this.insertScene.TextInsert.SetPositionX(this.TextInsertPositionX);
                this.insertScene.TextInsert.SetPositionY(this.TextInsertPositionY);
                if (this.SelectedDataset is DatasetContent) {
                    string text = string.Format("{0} + {1} + {2}",
                        this.SelectedDataset.ItemList[0].Keyword,
                        this.SelectedDataset.ItemList[1].Keyword,
                        this.SelectedDataset.ItemList[2].Keyword);
                    this.insertScene.TextInsert.SetText(text);
                }
            }
        }
        internal void Vinsert_TextContentOut() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.TextInsert.ToOut(); }
        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        internal void Vstage_ContentIn() {
            this.Vhost_ContentIn();
            this.Vleftplayer_ResetInput();
            this.Vrightplayer_ResetInput();
            this.Vleftplayer_ContentIn();
            this.Vrightplayer_ContentIn();
        }
        internal void Vstage_ContentOut() {
            this.Vhost_ContentOut();
            this.Vleftplayer_ContentOut();
            this.Vrightplayer_ContentOut();
        }

        public override void Vhost_LoadScene() { this.hostScene.Load(); }
        internal void Vhost_ContentIn() {
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) {
                this.hostScene.SetHostText(1, this.SelectedDataset.ItemList[0].HostText);
                this.hostScene.SetSolution(1, this.SelectedDataset.ItemList[0].Solution);
                this.hostScene.SetHostText(2, this.SelectedDataset.ItemList[1].HostText);
                this.hostScene.SetSolution(2, this.SelectedDataset.ItemList[1].Solution);
                this.hostScene.SetHostText(3, this.SelectedDataset.ItemList[2].HostText);
                this.hostScene.SetSolution(3, this.SelectedDataset.ItemList[2].Solution);
                this.hostScene.SetLeftPlayer(1, -1);
                this.hostScene.SetLeftPlayer(2, -1);
                this.hostScene.SetLeftPlayer(3, -1);
                this.hostScene.SetRightPlayer(1, -1);
                this.hostScene.SetRightPlayer(2, -1);
                this.hostScene.SetRightPlayer(3, -1);
                this.hostScene.ToIn();
            }
        }
        internal void Vhost_SetLeftPlayerInput() {
            this.hostScene.SetLeftPlayer(1, this.LeftPlayerEstimation_1);
            this.hostScene.SetLeftPlayer(2, this.LeftPlayerEstimation_2);
            this.hostScene.SetLeftPlayer(3, this.LeftPlayerEstimation_3);
        }
        internal void Vhost_SetRightPlayerInput() {
            this.hostScene.SetRightPlayer(1, this.RightPlayerEstimation_1);
            this.hostScene.SetRightPlayer(2, this.RightPlayerEstimation_2);
            this.hostScene.SetRightPlayer(3, this.RightPlayerEstimation_3);
        }
        internal void Vhost_ContentOut() { if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.ToOut(); }
        public override void Vhost_UnloadScene() { this.hostScene.Unload(); }

        public override void Vleftplayer_LoadScene() { this.leftPlayerScene.Load(); }
        internal void Vleftplayer_ContentIn() {
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) {
                this.leftPlayerScene.SetQuestionText(1, this.SelectedDataset.ItemList[0].Keyword);
                this.leftPlayerScene.SetQuestionText(2, this.SelectedDataset.ItemList[1].Keyword);
                this.leftPlayerScene.SetQuestionText(3, this.SelectedDataset.ItemList[2].Keyword);
                this.leftPlayerScene.ToIn();
            }
        }
        internal void Vleftplayer_ContentOut() { if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.ToOut(); }
        internal void Vleftplayer_ResetInput() { if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.ResetQuestion(); }
        internal void Vleftplayer_StartInput() { if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.StartQuestion(); }
        internal void Vleftplayer_ReleaseInput() { if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.ReleaseQuestion(); }
        internal void Vleftplayer_LockInput() { if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.LockQuestion(); }
        public override void Vleftplayer_UnloadScene() { this.leftPlayerScene.Unload(); }

        public override void Vrightplayer_LoadScene() { this.rightPlayerScene.Load(); }
        internal void Vrightplayer_ContentIn() {
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) {
                this.rightPlayerScene.SetQuestionText(1, this.SelectedDataset.ItemList[0].Keyword);
                this.rightPlayerScene.SetQuestionText(2, this.SelectedDataset.ItemList[1].Keyword);
                this.rightPlayerScene.SetQuestionText(3, this.SelectedDataset.ItemList[2].Keyword);
                this.rightPlayerScene.ToIn();
            }
        }
        internal void Vrightplayer_ContentOut() { if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.ToOut(); }
        internal void Vrightplayer_ResetInput() { if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.ResetQuestion(); }
        internal void Vrightplayer_StartInput() { if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.StartQuestion(); }
        internal void Vrightplayer_ReleaseInput() { if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.ReleaseQuestion(); }
        internal void Vrightplayer_LockInput() { if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.LockQuestion(); }
        public override void Vrightplayer_UnloadScene() { this.rightPlayerScene.Unload(); }

        public override void ClearGraphic() {
            base.ClearGraphic();
            this.insertScene.Clear();
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
            if (e is PropertyChangedEventArgs &&
                !this.repressPropertyChanged) {
                if (e.PropertyName == "ToString") {
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
            this.LeftPlayerEstimation_1 = this.leftPlayerScene.AmountQuestion_1;
            this.LeftPlayerEstimation_2 = this.leftPlayerScene.AmountQuestion_2;
            this.LeftPlayerEstimation_3 = this.leftPlayerScene.AmountQuestion_3;
            this.Vhost_SetLeftPlayerInput();
        }

        void rightPlayerScene_OKButtonPressed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerScene_OKButtonPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerScene_OKButtonPressed(object content) {
            this.RightPlayerEstimation_1 = this.rightPlayerScene.AmountQuestion_1;
            this.RightPlayerEstimation_2 = this.rightPlayerScene.AmountQuestion_2;
            this.RightPlayerEstimation_3 = this.rightPlayerScene.AmountQuestion_3;
            this.Vhost_SetRightPlayerInput();
        }

        #endregion
    }
}
