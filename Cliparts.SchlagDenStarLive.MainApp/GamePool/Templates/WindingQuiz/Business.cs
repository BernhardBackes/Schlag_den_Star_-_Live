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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.WindingQuiz;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.WindingQuiz {

    public class Data {
        public DatasetContent[] DataList;
        public int SelectedIndex;
    }

    public class DataItem : INotifyPropertyChanged {

        #region Properties

        private int index;
        public int Index {
            get { return this.index; }
            set {
                if (this.index != value) {
                    this.index = value;
                    this.buildToString();
                }
            }
        }

        public int ID { get { return this.index + 1; } }

        private string text = string.Empty;
        public string Text {
            get { return this.text; }
            set {
                if (this.text != value) {
                    if (value == null) this.text = string.Empty;
                    else this.text = Regex.Replace(value, "(?<!\r)\n", "\r\n");
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
                    if (value == null) this.hostText = string.Empty;
                    else this.hostText = Regex.Replace(value, "(?<!\r)\n", "\r\n");
                    this.on_PropertyChanged();
                }
            }
        }

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DataItem() { }

        public void Clone(
            DataItem source) {
            if (source is DataItem) {
                this.Text = source.Text;
                this.HostText = source.HostText;
            }
            else {
                this.Text = string.Empty;
                this.HostText = string.Empty;
            }
        }

        private void buildToString() {
            this.toString = string.Format("{0}. {1}", this.ID.ToString(), Regex.Replace(this.Text, "\r\n", " "));
            this.on_PropertyChanged("ItemList");
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
        #endregion

    }

    public class DatasetContent : Cliparts.Messaging.Message, INotifyPropertyChanged {

        #region Properties

        private SynchronizationContext syncContext;

        private string name = string.Empty;
        public string Name {
            get { return this.name; }
            set {
                if (this.name != value) {
                    if (value == null) this.name = string.Empty;
                    else this.name = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string hostText = string.Empty;
        public string HostText {
            get { return this.hostText; }
            set {
                if (this.hostText != value) {
                    if (value == null) this.hostText = string.Empty;
                    else this.hostText = Regex.Replace(value, "(?<!\r)\n", "\r\n");
                    this.on_PropertyChanged();
                }
            }
        }

        private List<DataItem> itemList = new List<DataItem>();
        public DataItem[] ItemList {
            get { return this.itemList.ToArray(); }
            set {
                this.RemoveAllItems();
                if (value is DataItem[]) foreach (DataItem item in value) this.tryAddItem(item);
                this.on_PropertyChanged("ItemList");
            }
        }

        public int ItemsCount { get { return this.itemList.Count; } }

        #endregion


        #region Funktionen

        public DatasetContent() { }
        public DatasetContent(
            SynchronizationContext syncContext) {
            this.syncContext = syncContext;
        }

        public DataItem GetItem(
            int index) {
            if (index >= 0 &&
                index < this.itemList.Count) return this.itemList[index];
            else return null;
        }

        public void AddItem(
            DataItem newItem) {
            if (this.tryAddItem(newItem)) this.on_PropertyChanged("ItemList");
        }
        private bool tryAddItem(
            DataItem newItem) {
            if (newItem is DataItem &&
                !this.itemList.Contains(newItem)) {
                newItem.Index = this.ItemsCount;
                newItem.PropertyChanged += this.on_PropertyChanged;
                this.itemList.Add(newItem);
                return true;
            }
            else return false;
        }

        public void RemoveAllItems() {
            if (this.tryRemoveAllItems()) this.on_PropertyChanged("ItemList");
        }
        private bool tryRemoveAllItems() {
            bool itemRemoved = false;
            DataItem[] itemList = this.itemList.ToArray();
            foreach (DataItem item in itemList) itemRemoved = this.removeItem(item) | itemRemoved;
            return itemRemoved;
        }
        public bool TryRemoveItem(
            int index) {
            if (this.removeItem(this.GetItem(index))) {
                this.on_PropertyChanged("ItemList");
                return true;
            }
            else return false;
        }
        private bool removeItem(
            DataItem item) {
            if (item is DataItem &&
            this.itemList.Contains(item)) {
                item.PropertyChanged -= this.on_PropertyChanged;
                this.itemList.Remove(item);
                this.setItemIndex();
                return true;
            }
            else return false;
        }
        public bool TryMoveItemUp(
            int index) {
            if (index > 0 &&
                index < this.ItemsCount) {
                DataItem item = this.GetItem(index);
                this.itemList.RemoveAt(index);
                this.itemList.Insert(index - 1, item);
                this.setItemIndex();
                this.on_PropertyChanged("ItemList");
                return true;
            }
            else return false;
        }
        public bool TryMoveItemDown(
            int index) {
            if (index >= 0 &&
                index < this.ItemsCount - 1) {
                DataItem item = this.GetItem(index);
                this.itemList.RemoveAt(index);
                this.itemList.Insert(index + 1, item);
                this.setItemIndex();
                this.on_PropertyChanged("ItemList");
                return true;
            }
            else return false;
        }

        private void setItemIndex() {
            int index = 0;
            foreach (DataItem item in itemList) {
                item.Index = index;
                index++;
            }
        }

        public void Clone(
            DatasetContent source) {
            if (source is DatasetContent) {
                this.Name = source.Name;
                this.HostText = source.HostText;
                this.ItemList = source.ItemList;
            }
            else {
                this.Name = string.Empty;
                this.HostText = string.Empty;
                this.ItemList = new DataItem[0];
            }
        }

        public override string ToString() { return this.Name; }

        #endregion


        #region Events.Outgoing

        public event PropertyChangedEventHandler PropertyChanged;
        protected void on_PropertyChanged([CallerMemberName]string callerName = "") { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }
        protected void on_PropertyChanged(PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, e); }
        protected void on_PropertyChanged(object sender, PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(sender, this.PropertyChanged, e); }

        #endregion

        #region Events.Incoming

        //void dataset_PropertyChanged(object sender, PropertyChangedEventArgs e) {
        //    thi
        //    SendOrPostCallback callback = new SendOrPostCallback(this.sync_dataset_PropertyChanged);
        //    if (this.syncContext != null) this.syncContext.Post(callback, e);
        //}
        //private void sync_dataset_PropertyChanged(object content) {
        //    PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
        //    if (e is PropertyChangedEventArgs) {
        //        if (e.PropertyName == "Text") this.on_PropertyChanged("ItemList");
        //    }
        //}

        #endregion

    }

    public class SingleDot : INotifyPropertyChanged {

        #region Properties

        private VentuzScenes.GamePool._Modules.TaskCounter.DotStates status = VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Off;
        public VentuzScenes.GamePool._Modules.TaskCounter.DotStates Status {
            get { return this.status; }
            set {
                if (this.status != value) {
                    this.status = value;
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion


        #region Funktionen

        public SingleDot() { }
        public SingleDot(VentuzScenes.GamePool._Modules.TaskCounter.DotStates status) { this.Status = status; }

        public void Reset() { this.Status = VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Off; }

        public void Clone(
            SingleDot dot) {
            if (dot is SingleDot) {
                this.Status = dot.Status;
            }
            else this.Reset();
        }

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

        private int taskCounter = 1;
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

        protected List<SingleDot> taskCounterPenaltyDots = new List<SingleDot>();
        public SingleDot[] TaskCounterPenaltyDots {
            get {
                this.fillPenaltyDots();
                return this.taskCounterPenaltyDots.ToArray();
            }
            set {
                this.fillPenaltyDots();
                for (int i = 0; i < TaskCounterPenaltyCountMax; i++) {
                    if (value is SingleDot[] &&
                        value.Length > i) this.taskCounterPenaltyDots[i].Clone(value[i]);
                    else this.taskCounterPenaltyDots[i].Reset();
                }
            }
        }

        private int itemInsertPositionX = 0;
        public int ItemInsertPositionX {
            get { return this.itemInsertPositionX; }
            set {
                if (this.itemInsertPositionX != value) {
                    this.itemInsertPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetItem();
                }
            }
        }

        private int itemInsertPositionY = 0;
        public int ItemInsertPositionY {
            get { return this.itemInsertPositionY; }
            set {
                if (this.itemInsertPositionY != value) {
                    this.itemInsertPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetItem();
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

        public DataItem SelectedDataItem { get; private set; }

        public int SelectedDataItemIndex { get; private set; }

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

        private bool repressPropertyChanged = false;

        #endregion


        #region Funktionen

        public Business() { }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.WindingQuiz'", typeIdentifier);
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

            this.hostScene = new Host(syncContext, hostMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.hostScene.StatusChanged += this.hostScene_StatusChanged;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);

        }

        public override void Dispose() {
            base.Dispose();
            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.hostScene.StatusChanged -= this.hostScene_StatusChanged;
            this.hostScene.Dispose();
        }

        public override void New() {
            base.New();
            this.Filename = string.Empty;
        }

        public override void ResetData() {
            base.ResetData();
            this.TaskCounter = 0;
            this.ResetPenaltyDots();
            this.SelectDataset(0);
        }

        protected void fillPenaltyDots() {
            while (this.taskCounterPenaltyDots.Count < TaskCounterPenaltyCountMax) {
                SingleDot item = new SingleDot();
                item.PropertyChanged += this.dot_PropertyChanged;
                this.taskCounterPenaltyDots.Add(item);
            }
        }

        public bool TryGetDot(
            int index,
            out SingleDot dot) {
            if (index >= 0 &&
                index < this.TaskCounterPenaltyDots.Length &&
                this.TaskCounterPenaltyDots[index] is SingleDot) {
                dot = this.TaskCounterPenaltyDots[index];
                return true;
            }
            else {
                dot = null;
                return false;
            }
        }

        public override void Next() {
            SingleDot dot;
            if (this.BuzzeredPlayer == Content.Gameboard.PlayerSelection.NotSelected &&
                this.TryGetDot(this.TaskCounter - 1, out dot)) dot.Status = VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Fail;
            base.Next();
            this.TaskCounter++;
            this.Vinsert_SetTaskCounter();
            this.SelectDataset(this.SelectedDatasetIndex + 1);
        }

        public void ResetPenaltyDots() {
            foreach (SingleDot item in this.taskCounterPenaltyDots) item.Reset();
        }

        protected void clearPenalty() {
            foreach (SingleDot item in this.taskCounterPenaltyDots) item.PropertyChanged -= this.dot_PropertyChanged;
        }

        public void True() {
            this.Vinsert_StopTimeout();
            SingleDot dot;
            switch (this.BuzzeredPlayer) {
                case Content.Gameboard.PlayerSelection.LeftPlayer:
                    this.LeftPlayerScore++;
                    if (this.TryGetDot(this.TaskCounter - 1, out dot)) dot.Status = VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Red;
                    break;
                case Content.Gameboard.PlayerSelection.RightPlayer:
                    this.RightPlayerScore++;
                    if (this.TryGetDot(this.TaskCounter - 1, out dot)) dot.Status = VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Blue;
                    break;
            }
            this.Vinsert_SetScore();
            this.Vinsert_SetTaskCounter();
        }

        public void False() {
            this.Vinsert_StopTimeout();
            SingleDot dot;
            switch (this.BuzzeredPlayer) {
                case Content.Gameboard.PlayerSelection.LeftPlayer:
                    this.RightPlayerScore++;
                    if (this.TryGetDot(this.TaskCounter - 1, out dot)) dot.Status = VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Blue;
                    break;
                case Content.Gameboard.PlayerSelection.RightPlayer:
                    this.LeftPlayerScore++;
                    if (this.TryGetDot(this.TaskCounter - 1, out dot)) dot.Status = VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Red;
                    break;
            }
            this.Vinsert_SetScore();
            this.Vinsert_SetTaskCounter();
        }

        internal void NextItem() {
            if (this.SelectedDataset is DatasetContent) this.SelectDataItem(this.SelectedDataItemIndex + 1);
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
            this.SelectDataItem(0);
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

        public void SelectDataItem(
            int index) {
            if (this.SelectedDataset is DatasetContent) {
                if (index < 0) index = 0;
                if (index >= this.SelectedDataset.ItemsCount) index = this.SelectedDataset.ItemsCount - 1;
                this.SelectedDataItemIndex = index;
                this.on_PropertyChanged("SelectedDataItemIndex");
                this.SelectedDataItem = this.SelectedDataset.GetItem(index);
                this.on_PropertyChanged("SelectedDataItem");
            }
            else {
                this.SelectedDataItemIndex = -1;
                this.on_PropertyChanged("SelectedDataItemIndex");
                this.SelectedDataItem = null;
                this.on_PropertyChanged("SelectedDataItem");
            }
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
        public void Import(
            string filename) {
            string subSender = "Import";
            if (File.Exists(filename)) {
                System.IO.StreamReader file = null;
                try {
                    string line;
                    DatasetContent newDataset = null;
                    file = new System.IO.StreamReader(filename, Encoding.UTF7);
                    while ((line = file.ReadLine()) != null) {
                        if (string.IsNullOrEmpty(line)) {
                            if (newDataset is DatasetContent) this.tryAddDataset(newDataset, -1);
                            newDataset = null;
                        }
                        else {
                            if (newDataset == null) {
                                newDataset = new DatasetContent();
                                newDataset.Name = Helper.removeTextInBrackets(line);
                                newDataset.HostText = line;
                            }
                            else {
                                DataItem newItem = new DataItem();
                                newItem.Text = Helper.removeTextInBrackets(line);
                                newItem.HostText = line;
                                newDataset.AddItem(newItem);
                            }
                        }
                    }
                    if (newDataset is DatasetContent) this.tryAddDataset(newDataset, -1);
                    newDataset = null;
                    file.Close();
                }
                catch (Exception exc) {
                    if (file != null) file.Close();
                    this.on_Error(subSender, exc.Message);
                }
                this.on_PropertyChanged("NameList");
                this.Save();
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

        public override void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score, this.LeftPlayerScore, this.RightPlayerScore); }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }

        public override void Vinsert_Buzzer(Content.Gameboard.PlayerSelection buzzeredPlayer) { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_Buzzer(this.insertScene.Timeout, buzzeredPlayer); }
        public override void Vinsert_SetTimeout() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetTimeout(this.insertScene.Timeout); }
        public override void Vinsert_StopTimeout() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_StopTimeout(this.insertScene.Timeout); }

        public void Vinsert_TaskCounterIn() {
            this.Vinsert_SetTaskCounter();
            if (this.insertScene is Insert &&
                this.insertScene.TaskCounter is VentuzScenes.GamePool._Modules.TaskCounter) this.insertScene.TaskCounter.ToIn();
        }
        public void Vinsert_SetTaskCounter() { if (this.insertScene is Insert) this.Vinsert_SetTaskCounter(this.insertScene.TaskCounter, this.TaskCounterPenaltyDots, this.TaskCounter); }
        public void Vinsert_SetTaskCounter(
            VentuzScenes.GamePool._Modules.TaskCounter scene,
            SingleDot[] taskCounterPenaltyDots,
            int counter) {
            if (scene is VentuzScenes.GamePool._Modules.TaskCounter) {
                scene.SetPositionX(this.TaskCounterPositionX);
                scene.SetPositionY(this.TaskCounterPositionY);
                scene.SetStyle(VentuzScenes.GamePool._Modules.TaskCounter.Styles.Numeric);
                scene.SetSize(this.TaskCounterSize);
                int id = 1;
                foreach (SingleDot item in taskCounterPenaltyDots) {
                    scene.SetDot(id, item.Status);
                    id++;
                }
                scene.SetCounter(counter);
            }
        }
        public void Vinsert_TaskCounterOut() {
            if (this.insertScene is Insert &&
                this.insertScene.TaskCounter is VentuzScenes.GamePool._Modules.TaskCounter) this.insertScene.TaskCounter.ToOut();
        }

        internal void Vinsert_ContentIn() {
            this.Vinsert_SetItem();
            if (this.insertScene is VRemote4.HandlerSi.Scene && 
                this.SelectedDataItem is DataItem) {
                this.insertScene.ResetSolution();
                this.insertScene.ToIn();
            }
        }
        internal void Vinsert_NextItem() {
            this.Vinsert_SetItem();
            if (this.insertScene is VRemote4.HandlerSi.Scene && 
                this.SelectedDataItem is DataItem) this.insertScene.ShowNextText();
        }
        internal void Vinsert_ShowSolution() {
            this.Vinsert_SetItem();
            if (this.insertScene is VRemote4.HandlerSi.Scene && 
                this.SelectedDataset is DatasetContent) this.insertScene.SolutionToIn();
        }
        internal void Vinsert_SetItem() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetItem(this.insertScene, this.SelectedDataset, this.SelectedDataItem); }
        internal void Vinsert_SetItem(
            Insert scene,
            DatasetContent dataset,
            DataItem item) {
            if (scene is Insert &&
                scene.Status == VRemote4.HandlerSi.Scene.States.Available) {
                scene.SetPositionX(this.ItemInsertPositionX);
                scene.SetPositionY(this.ItemInsertPositionY);
                if (dataset is DatasetContent) scene.SetSolutionText(dataset.Name);
                if (item is DataItem) scene.SetText(item.Text);
            }
        }
        internal void Vinsert_ContentOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.ToOut(); }

        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void Vstage_Init() {
            base.Vstage_Init();
            if (this.leftPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerMasterScene.LiveVideoIn();
            if (this.rightPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerMasterScene.LiveVideoIn();
        }

        public override void Vhost_LoadScene() {
            base.Vhost_LoadScene();
            this.hostScene.Load(); 
        }
        internal void Vhost_ContentIn() {
            this.Vhost_SetItem();
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) {
                this.hostScene.PassNewItemValues();
                this.hostScene.ToIn();
            }
        }
        internal void Vhost_NextItem() {
            this.Vhost_SetItem();
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) this.hostScene.ShowNextItem();
        }
        public void Vhost_SetItem() { if (this.hostScene is VRemote4.HandlerSi.Scene) this.Vhost_SetItem(this.hostScene, this.TaskCounter, this.SelectedDataset, this.SelectedDataItem); }
        internal void Vhost_SetItem(
            Host scene,
            int counter,
            DatasetContent dataset,
            DataItem item) {
            if (scene is Host &&
                scene.Status == VRemote4.HandlerSi.Scene.States.Available &&
                dataset is DatasetContent) {
                string hostText = string.Empty;
                if (counter == 0) hostText = string.Format("Beispiel: {0}", dataset.HostText);
                else if (counter > 0 && counter <= this.TaskCounterSize) hostText = string.Format("{0}/{1}: {2}", counter.ToString(), this.TaskCounterSize.ToString(), dataset.HostText);
                scene.SetSolution(hostText);
                scene.SetItemsCount(dataset.ItemsCount);
                if (item is DataItem) {
                    int index = item.Index;
                    scene.SetItemsStartID(item.ID);
                    scene.SetItemsHostText(1, item.HostText);
                    for (int i = 2; i <= 5; i++) {
                        index++;
                        item = dataset.GetItem(index);
                        if (item is DataItem) scene.SetItemsHostText(i, item.HostText);
                        else scene.SetItemsHostText(i, string.Empty);
                    }
                }
            }
        }

        internal void Vhost_ContentOut() { if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.ToOut(); }
        public override void Vhost_UnloadScene() {
            base.Vhost_UnloadScene(); 
            this.hostScene.Unload(); 
        }

        public override void ClearGraphic() {
            base.ClearGraphic();
            this.insertScene.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming

        void dataset_Error(object sender, Messaging.ErrorEventArgs e) { this.on_Error(sender, e); }

        void dot_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            this.on_PropertyChanged(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_dot_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_dot_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                //if (e.PropertyName == "Status" &&
                //    !this.repressCalcPenaltySums) this.calcPenaltySums();
            }
        }

        void dataset_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_dataset_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_dataset_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs &&
                !this.repressPropertyChanged) {
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
