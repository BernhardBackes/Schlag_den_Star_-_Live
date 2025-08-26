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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SortListTimerScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.SortListTimerScore {

    public class Data {
        public DatasetContent[] DataList;
        public int SelectedIndex;
    }

    public class DatasetContentItemComparer : IComparer<DatasetContentItem> {
        private bool ascending = false;
        private bool asName = false;
        public DatasetContentItemComparer(
            bool ascending,
            bool asName) {
            this.ascending = ascending;
            this.asName = asName; 
        }
        public int Compare(DatasetContentItem a, DatasetContentItem b) {
            if (a is DatasetContentItem && b is DatasetContentItem) {
                string sortItemA = a.Issue;
                string sortItemB = b.Issue;
                if (this.asName) {
                    string[] sortArray = sortItemA.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (sortArray.Length > 0) sortItemA = sortArray[sortArray.Length - 1];
                    sortArray = sortItemB.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (sortArray.Length > 0) sortItemB = sortArray[sortArray.Length - 1];
                }
                if (this.ascending) return string.Compare(sortItemA, sortItemB);
                else return string.Compare(sortItemB, sortItemA);
            }
            else return 0;
        }
    }

    public class DatasetContentItem : Messaging.Message, INotifyPropertyChanged {

        #region Properties

        private int index = 0;
        [XmlIgnore]
        public int Index {
            get { return this.index; }
            private set {
                if (this.index != value) {
                    if (value < 0) value = 0;
                    if (value >= DatasetContent.ItemsCount) value = DatasetContent.ItemsCount - 1;
                    this.index = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string issue = string.Empty;
        public string Issue {
            get { return this.issue; }
            set {
                if (this.issue != value) {
                    if (value == null) value = string.Empty;
                    this.issue = value;
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
                    if (value == null) value = string.Empty;
                    this.hostText = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int choiceIndex = 0;
        public int ChoiceIndex {
            get { return this.choiceIndex; }
            set {
                if (this.choiceIndex != value) {
                    if (value < 0) value = 0;
                    if (value >= DatasetContent.ItemsCount) value = DatasetContent.ItemsCount - 1;
                    this.choiceIndex = value;
                    this.buildToString();
                    this.on_PropertyChanged();
                }
            }
        }

        public enum StatusElements { Idle, Busy }

        private StatusElements status = StatusElements.Idle;
        public StatusElements Status {
            get {
                if (this.ChoiceIndex == 0) return StatusElements.Busy;
                else return this.status;
            }
            set {
                if (this.status != value) {
                    this.status = value;
                }
            }
        }

        [XmlIgnore]
        public int TargetPosition { get; set; }

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DatasetContentItem() {
            this.TargetPosition = 0;
        }
        public DatasetContentItem(
            int index) {
            this.Index = index;
            this.TargetPosition = 0;
        }

        public void Clone(
            DatasetContentItem source) {
            if (source is DatasetContentItem) {
                this.Issue = source.Issue;
                this.HostText = source.HostText;
                this.ChoiceIndex = source.ChoiceIndex;
            }
            else {
                this.Issue = string.Empty;
                this.HostText = string.Empty;
            }
            this.TargetPosition = 0;
        }

        public void Reset() { 
            this.Status = StatusElements.Idle;
            this.TargetPosition = 0;
        }

        private void buildToString() { 
            if (this.ChoiceIndex == 0) this.toString = "* " + this.Issue; 
            else this.toString = this.Issue;
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

    public class DatasetContent : Messaging.Message, INotifyPropertyChanged {

        #region Properties

        private SynchronizationContext syncContext;

        public const int ItemsCount = 9;

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

        private Game.TopTextElements topText = Game.TopTextElements.frueh;
        public Game.TopTextElements TopText {
            get { return this.topText; }
            set {
                if (this.topText != value) {
                    this.topText = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private Game.BottomTextElements bottomText = Game.BottomTextElements.spaet;
        public Game.BottomTextElements BottomText {
            get { return this.bottomText; }
            set {
                if (this.bottomText != value) {
                    this.bottomText = value;
                    this.on_PropertyChanged();
                }
            }
        }

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
                this.repressPropertyChanged = false;
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                this.buildChoiceList();
                this.on_PropertyChanged("ChoiceList");
            }
        }

        private List<string> names = new List<string>();
        public string[] NameList { get { return this.names.ToArray(); } }

        private List<DatasetContentItem> choices = new List<DatasetContentItem>();
        public DatasetContentItem[] ChoiceList { get { return this.choices.ToArray(); } }

        private string toString = string.Empty;

        private bool repressPropertyChanged = false;

        #endregion


        #region Funktionen

        public DatasetContent() { this.buildItemList(); }
        public DatasetContent(
            SynchronizationContext syncContext) {
            this.syncContext = syncContext;
            this.buildItemList();
        }

        private void buildItemList() {
            while (this.itemList.Count < ItemsCount) {
                DatasetContentItem item = new DatasetContentItem(this.itemList.Count);
                item.ChoiceIndex = this.itemList.Count;
                item.PropertyChanged += this.item_PropertyChanged;
                this.itemList.Add(item);
            }
            this.buildNameList();
            this.buildChoiceList();
        }

        private void buildNameList() {
            this.names.Clear();
            foreach (DatasetContentItem item in this.itemList) {
                this.names.Add(item.ToString());
            }
        }

        private void buildChoiceList() {
            this.choices.Clear();
            while (this.choices.Count < ItemsCount) this.choices.Add(null);
            foreach (DatasetContentItem item in this.itemList) {
                int index = item.ChoiceIndex;
                if (index >= 0 &&
                    index < this.choices.Count) this.choices[index] = item;
            }
        }

        public DatasetContentItem GetItem(
            int index) {
            if (index >= 0 &&
                index < this.itemList.Count) return this.itemList[index];
            else return null;
        }

        public bool TryMoveItemUp(
            int index) {
            if (index > 0 &&
                index < this.itemList.Count) {
                DatasetContentItem item = new DatasetContentItem();
                item.Clone(this.itemList[index]);
                this.itemList[index].Clone(this.itemList[index - 1]);
                this.itemList[index - 1].Clone(item);
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                return true;
            }
            else return false;
        }
        public bool TryMoveItemDown(
            int index) {
            if (index >= 0 &&
                index < this.itemList.Count - 1) {
                DatasetContentItem item = new DatasetContentItem();
                item.Clone(this.itemList[index]);
                this.itemList[index].Clone(this.itemList[index + 1]);
                this.itemList[index + 1].Clone(item);
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                return true;
            }
            else return false;
        }

        public bool TryMoveChoiceUp(
            int index) {
            if (index > 0 &&
                index < this.choices.Count) {
                DatasetContentItem sourceItem = null;
                DatasetContentItem targetItem = null;
                foreach(DatasetContentItem item in this.itemList) {
                    if (item.ChoiceIndex == index) sourceItem = item;
                    if (item.ChoiceIndex == index - 1) targetItem = item;
                }
                if (sourceItem is DatasetContentItem) sourceItem.ChoiceIndex = index - 1;
                if (targetItem is DatasetContentItem) targetItem.ChoiceIndex = index;
                this.buildChoiceList();
                this.on_PropertyChanged("ChoiceList");
                return true;
            }
            else return false;
        }
        public bool TryMoveChoiceDown(
            int index) {
            if (index >= 0 &&
                index < this.choices.Count - 1) {
                DatasetContentItem sourceItem = null;
                DatasetContentItem targetItem = null;
                foreach (DatasetContentItem item in this.itemList) {
                    if (item.ChoiceIndex == index) sourceItem = item;
                    if (item.ChoiceIndex == index + 1) targetItem = item;
                }
                if (sourceItem is DatasetContentItem) sourceItem.ChoiceIndex = index + 1;
                if (targetItem is DatasetContentItem) targetItem.ChoiceIndex = index;
                this.buildChoiceList();
                this.on_PropertyChanged("ChoiceList");
                return true;
            }
            else return false;
        }

        public void SortChoicesAscending(
            bool asName) {
            List<DatasetContentItem> sortList = new List<DatasetContentItem>(this.ItemList);
            if (sortList.Count > 1) {
                DatasetContentItemComparer ic = new DatasetContentItemComparer(true, asName);
                sortList.Sort(ic);
            }
            int index = 0;
            foreach (DatasetContentItem item in sortList) {
                item.ChoiceIndex = index;
                index++;
            }
            this.buildChoiceList();
            this.on_PropertyChanged("ChoiceList");
        }

        public void SortChoicesDescending(
            bool asName) {
            List<DatasetContentItem> sortList = new List<DatasetContentItem>(this.ItemList);
            if (sortList.Count > 1) {
                DatasetContentItemComparer ic = new DatasetContentItemComparer(false, asName);
                sortList.Sort(ic);
            }
            int index = 0;
            foreach (DatasetContentItem item in sortList) {
                item.ChoiceIndex = index;
                index++;
            }
            this.buildChoiceList();
            this.on_PropertyChanged("ChoiceList");
        }

        public void Clone(
            DatasetContent source) {
            if (source is DatasetContent) {
                this.Name = source.Name;
                this.TopText = source.TopText;
                this.BottomText = source.BottomText;
                this.ItemList = source.ItemList;
            }
            else {
                this.Name = string.Empty;
                this.ItemList = new DatasetContentItem[0];
            }
        }

        public void ResetItems() {
            foreach (DatasetContentItem item in this.itemList) item.Reset();
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

        void item_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_item_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_item_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs &&
                !this.repressPropertyChanged) {
                if (e.PropertyName == "Issue" ||
                    e.PropertyName == "ChoiceIndex") {
                    this.buildNameList();
                    this.on_PropertyChanged("NameList");
                    this.buildChoiceList();
                    this.on_PropertyChanged("ChoiceList");
                }
            }
        }

        #endregion

    }

    public enum SelectionStates {
        Idle,
        True,
        False
    }

    public class Business : _Base.TimerScore.Business {

        #region Properties

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

        public DatasetContentItem SelectedChoice { get; private set; }

        private int targetPosition = 0;
        public int TargetPosition {
            get { return this.targetPosition; }
            set {
                if (this.targetPosition != value) {
                    if (value < 0) this.targetPosition = 0;
                    else this.targetPosition = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private SelectionStates selectionStatus = SelectionStates.Idle;
        public SelectionStates SelectionStatus {
            get { return this.selectionStatus; }
            private set {
                if (this.selectionStatus != value) {
                    this.selectionStatus = value;
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.SortListTimerScore'", typeIdentifier);
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
            this.insertScene.Timer.Alarm1Fired += this.timer_Alarm1Fired;
            this.insertScene.Timer.Alarm2Fired += this.timer_Alarm2Fired;
            this.insertScene.Timer.StopFired += this.timer_StopFired;
            this.insertScene.Timer.PropertyChanged += this.timer_PropertyChanged;

            this.hostScene = new Stage(syncContext, hostMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.hostScene.StatusChanged += this.hostScene_StatusChanged;

            this.leftPlayerScene = new Stage(syncContext, leftPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.leftPlayerScene.StatusChanged += this.leftPlayerScene_StatusChanged;

            this.rightPlayerScene = new Stage(syncContext, rightPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.rightPlayerScene.StatusChanged += this.rightPlayerScene_StatusChanged;

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
            this.leftPlayerScene.Dispose();

            this.rightPlayerScene.StatusChanged -= this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.Dispose();

        }

        public override void ResetData() {
            base.ResetData();
            this.TaskCounter = 0;
            this.SelectedChoice = null;
            this.TargetPosition = 0;
            this.SelectDataset(0);
        }

        public override void Next() {
            base.Next();
            this.TaskCounter++;
            this.SelectedChoice = null;
            this.TargetPosition = 0;
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
            this.ResetSelectedDataset();
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

        public void ResetSelectedDataset() {
            this.SelectedChoice = null;
            this.TargetPosition = 0;
            this.SelectionStatus = SelectionStates.Idle;
            if (this.SelectedDataset is DatasetContent) {
                this.SelectedDataset.ResetItems();
                this.setTargetPositions();
            }
            this.on_PropertyChanged("Choices");
        }

        private void setTargetPositions() {
            if (this.SelectedDataset is DatasetContent) {
                List<DatasetContentItem> idleItems = new List<DatasetContentItem>();
                List<DatasetContentItem> busyItems = new List<DatasetContentItem>();
                foreach (DatasetContentItem item in this.SelectedDataset.ItemList) {
                    if (item.Status == DatasetContentItem.StatusElements.Busy) busyItems.Add(item);
                    else idleItems.Add(item);
                }
                foreach (DatasetContentItem idle in idleItems) {
                    idle.TargetPosition = 0;
                    int counter = 0;
                    foreach (DatasetContentItem busy in busyItems) {
                        counter++;
                        if (idle.Index < busy.Index) {
                            idle.TargetPosition = counter;
                            break;
                        }
                    }
                    if (idle.TargetPosition == 0) idle.TargetPosition = counter + 1;
                }
            }
        }

        public void SelectChoice(
            DatasetContentItem value) {
            this.setTargetPositions();
            this.SelectedChoice = value;
            this.setSelectionStatus();
        }

        public void SetTargetPosition(
            int value) {
            this.TargetPosition = value;
            this.setSelectionStatus();
        }

        private void setSelectionStatus() {
            if (this.SelectedChoice is DatasetContentItem &&
                this.TargetPosition > 0) {
                if (this.SelectedChoice.TargetPosition == this.TargetPosition) this.SelectionStatus = SelectionStates.True;
                else this.SelectionStatus = SelectionStates.False;
            }
            else this.SelectionStatus = SelectionStates.Idle;
        }

        int targetInterspaceID = 0;
        public void ExecuteSelection() {
            if (this.SelectedDataset is DatasetContent &&
                this.SelectedChoice is DatasetContentItem &&
                this.SelectionStatus != SelectionStates.Idle) {
                if (this.insertScene is VRemote4.HandlerSi.Scene) {
                    this.insertScene.Game.PlayJingle("Morve");
                    this.insertScene.Game.SourceScalingToOut(this.SelectedChoice.ChoiceIndex);
                    for (int i = 0; i <= DatasetContent.ItemsCount; i++) this.insertScene.Game.TargetIndicatorToOut(i + 1);
                    int counter = 0;
                    this.targetInterspaceID = 0;
                    if (this.SelectionStatus == SelectionStates.True) {
                        this.insertScene.Game.SetTargetBackColor(this.SelectedChoice.Index + 1, Game.BackColorElements.Orange);
                        Helper.invokeActionAfterDelay(() => this.insertScene.Game.TargetScalingToIn(this.SelectedChoice.Index + 1), 750, this.syncContext);
                    }
                    else {
                        foreach (DatasetContentItem item in this.SelectedDataset.ItemList) {
                            if (item.Status == DatasetContentItem.StatusElements.Busy) {
                                counter++;
                                if (this.TargetPosition == counter) this.targetInterspaceID = item.Index + 1;
                            }
                        }
                        if (this.targetInterspaceID == 0) this.targetInterspaceID = DatasetContent.ItemsCount + 1;
                        this.insertScene.Game.SetTargetInterspaceText(this.targetInterspaceID, this.SelectedChoice.Issue);
                        this.insertScene.Game.SetTargetInterspaceBackColor(this.targetInterspaceID, Game.BackColorElements.Orange);
                        this.insertScene.Game.TargetInterspaceScalingToIn(this.targetInterspaceID);
                    }
                }
            }
        }
        public void Resolve() {
            if (this.SelectedDataset is DatasetContent && 
                this.SelectedChoice is DatasetContentItem &&
                this.SelectionStatus != SelectionStates.Idle) {
                if (this.SelectionStatus == SelectionStates.True) {
                    this.insertScene.Game.PlayJingle("Pling");
                    this.SelectedChoice.Status = DatasetContentItem.StatusElements.Busy;
                    this.TargetPosition = 0;
                    this.setSelectionStatus();
                    this.on_PropertyChanged("Choices");
                    this.Vinsert_SetContent(true);
                }
                else {
                    this.insertScene.Game.PlayJingle("Wrong");
                    this.insertScene.Game.SetTargetInterspaceBackColor(this.targetInterspaceID, Game.BackColorElements.Red);
                }
            }
        }

        public void ShowSolution() {
            if (this.SelectedDataset is DatasetContent &&
                this.SelectedChoice is DatasetContentItem &&
                this.SelectionStatus != SelectionStates.Idle) {
                this.insertScene.Game.TargetInterspaceScalingToOut(this.targetInterspaceID);
                this.insertScene.Game.SetTargetBackColor(this.SelectedChoice.Index + 1, Game.BackColorElements.Yellow);
                Helper.invokeActionAfterDelay(() => this.insertScene.Game.TargetScalingToIn(this.SelectedChoice.Index + 1), 1000, this.syncContext);
            }
        }

        private void setRepressPropertyChangedFalse() { 
            this.repressPropertyChanged = false;
            this.buildNameList();
            this.on_PropertyChanged("NameList");
            this.Save();
        }
        public void Load(
            string filename) {
            string subSender = "Load";
            if (File.Exists(filename)) {
                this.repressPropertyChanged = true;
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
            Helper.invokeActionAfterDelay(this.setRepressPropertyChangedFalse, 2000, this.syncContext);
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
                                newDataset.SortChoicesAscending(false);
                                this.tryAddDataset(newDataset, -1);
                            }
                            newDataset = null;
                            lineCounter = 0;
                        }
                        else {
                            if (newDataset == null) newDataset = new DatasetContent();
                            lineCounter++;
                            if (lineCounter == 1) newDataset.Name = line;
                            else {
                                DatasetContentItem newItem = newDataset.GetItem(lineCounter - 2);
                                if (newItem is DatasetContentItem) {
                                    newItem.HostText = line.Trim();
                                    int index = newItem.HostText.IndexOf("(");
                                    if (index > 0) newItem.Issue = newItem.HostText.Substring(0, index - 1).Trim();
                                    else newItem.Issue = newItem.HostText;
                                    newItem.ChoiceIndex = lineCounter - 2;
                                }
                            }
                        }
                    }
                    if (newDataset is DatasetContent) {
                        newDataset.SortChoicesAscending(false);
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

        public override void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score); }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }

        public override void Vinsert_TimerIn() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Timer.ToIn();
        }
        public override void Vinsert_SetTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetTimer(this.insertScene.Timer); }
        public override void Vinsert_StartTimer() {
            this.Vinsert_SetTimer();
            this.Vinsert_ResetTimer();
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_StartTimer(this.insertScene.Timer); 
        }
        public override void Vinsert_StopTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_StopTimer(this.insertScene.Timer); }
        public override void Vinsert_ContinueTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ContinueTimer(this.insertScene.Timer); }
        public override void Vinsert_ResetTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ResetTimer(this.insertScene.Timer); }
        public override void Vinsert_TimerOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_TimerOut(this.insertScene.Timer); }

        public void Vinsert_ContentIn() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                this.insertScene.Game.Reset();
                this.Vinsert_SetContent(false);
                this.insertScene.Game.ToIn();
            }
        }
        internal void Vinsert_TargetIn() {
            this.Vinsert_SetContent(false);
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Game.TargetToIn();
        }
        public void Vinsert_SetContent(
            bool moveButtons) {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                this.Vinsert_SetContent(this.insertScene.Game, this.SelectedDataset, moveButtons);
            }
        }
        public void Vinsert_SetContent(
            Game scene,
            DatasetContent dataset,
            bool moveButtons) {
            if (scene is Game &&
                dataset is DatasetContent) {

                scene.SetPositionX(0);
                scene.SetPositionY(0);

                scene.SetTargetTopText(dataset.TopText);
                scene.SetTargetBottomText(dataset.BottomText);                

                int counter = 0;
                foreach (DatasetContentItem item in dataset.ItemList) if (item.Status == DatasetContentItem.StatusElements.Idle) counter++;
                bool resolved =counter == 0;

                counter = 0;
                    foreach (DatasetContentItem item in dataset.ItemList) {

                    if (item.ChoiceIndex > 0) {
                        scene.SetSourceText(item.ChoiceIndex, item.Issue);
                        if (item.Status != DatasetContentItem.StatusElements.Busy) scene.SetSourceScalingIn(item.ChoiceIndex);
                        else scene.SetSourceScalingOut(item.ChoiceIndex);
                    }

                    scene.SetTargetText(item.Index + 1, item.Issue);
                    scene.SetTargetBackColor(item.Index + 1, Game.BackColorElements.Yellow);
                    if (item.Status == DatasetContentItem.StatusElements.Idle) {
                        scene.SetTargetScalingOut(item.Index + 1);
                        scene.SetTargetIndicatorOut(item.Index + 1);
                    }
                    else {
                        scene.SetTargetScalingIn(item.Index + 1);
                        counter++;
                        if (resolved) scene.SetTargetIndicatorOut(item.Index + 1);
                        else {
                            scene.SetTargetIndicatorID(item.Index + 1, counter);
                            if (moveButtons) scene.TargetIndicatorToIn(item.Index + 1);
                            else scene.SetTargetIndicatorIn(item.Index + 1);
                        }
                    }
                }

                if (resolved) scene.SetTargetIndicatorOut(DatasetContent.ItemsCount + 1);
                else {
                    scene.SetTargetIndicatorID(DatasetContent.ItemsCount + 1, counter + 1);
                    if (moveButtons) scene.TargetIndicatorToIn(DatasetContent.ItemsCount + 1);
                    else scene.SetTargetIndicatorIn(DatasetContent.ItemsCount + 1);
                }

            }
        }
        public void Vinsert_ResolveAll() {
            if (this.SelectedDataset is DatasetContent) {
                //this.SelectedChoice.Status = DatasetContentItem.StatusElements.Busy;
                this.TargetPosition = 0;
                this.setSelectionStatus();
                this.on_PropertyChanged("Choices");
                for (int i = 1; i <= DatasetContent.ItemsCount + 1; i++) {
                    this.insertScene.Game.TargetIndicatorToOut(i);
                    this.insertScene.Game.TargetInterspaceScalingToOut(i);
                }
                foreach (DatasetContentItem item in this.SelectedDataset.ItemList) {
                    this.insertScene.Game.SourceScalingToOut(item.ChoiceIndex + 1);
                    if (item.Status == DatasetContentItem.StatusElements.Idle) {
                        this.insertScene.Game.SetTargetBackColor(item.Index + 1, Game.BackColorElements.Yellow);
                        this.insertScene.Game.TargetScalingToIn(item.Index + 1);
                    }
                }
            }
        }
        public void Vinsert_ContentOut() { 
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Game.ToOut(); 
        }
        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void Vhost_LoadScene() { this.hostScene.Load(); }
        public override void Vhost_UnloadScene() { this.hostScene.Unload(); }

        public override void Vleftplayer_LoadScene() { this.leftPlayerScene.Load(); }
        public override void Vleftplayer_UnloadScene() { this.leftPlayerScene.Unload(); }

        public override void Vrightplayer_LoadScene() { this.rightPlayerScene.Load(); }
        public override void Vrightplayer_UnloadScene() { this.rightPlayerScene.Unload(); }

        public override void Vstage_Init() {
            base.Vstage_Init();
        }
        public void Vstage_ContentIn() {
            this.Vstage_SetContent();
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.ToIn();
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.ToIn();
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.ToIn();
        }
        public void Vstage_SetContent() {
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vstage_SetContent(this.hostScene, this.SelectedDataset, this.SelectedChoice, this.TargetPosition, false, true, false, false);
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vstage_SetContent(this.leftPlayerScene, this.SelectedDataset, this.SelectedChoice, this.TargetPosition, false, false, false, false);
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vstage_SetContent(this.rightPlayerScene, this.SelectedDataset, this.SelectedChoice, this.TargetPosition, false, false, false, false);
        }
        public void Vstage_SetContent(
            Stage scene,
            DatasetContent dataset,
            DatasetContentItem selectedChoice,
            int targetPosition,
            bool resolvedPosition,
            bool isHostScene,
            bool resolved,
            bool isPreview) {
            int counter = 0;
            int id = 0;
            if (dataset is DatasetContent) {
                scene.SetTaskText(dataset.Name);
                scene.SetTargetTopText(dataset.TopText);
                scene.SetTargetBottomText(dataset.BottomText);
                if (isPreview) {
                    foreach (DatasetContentItem item in dataset.ItemList) {
                        id = item.Index * 2 + 1;
                        counter = item.Index + 1;
                        scene.SetTargetBackground(id, Stage.BackgroundElements.Number);
                        scene.SetTargetBlocked(id, false);
                        scene.SetTargetText(id, counter.ToString());
                        id++;
                        scene.SetTargetBackground(id, Stage.BackgroundElements.Text);
                        scene.SetTargetBlocked(id, false);
                        scene.SetTargetText(id, item.HostText);
                    }
                    foreach (DatasetContentItem item in dataset.ChoiceList) {
                        id = item.ChoiceIndex;
                        scene.SetSourceBackground(id, Stage.BackgroundElements.Text);
                        scene.SetSourceBlocked(id, false);
                        scene.SetSourceText(id, item.HostText);
                    }
                }
                else if (resolved) {
                    foreach (DatasetContentItem item in dataset.ItemList) {
                        id = item.Index + 1;
                        if (isHostScene) scene.SetTargetText(id, item.HostText);
                        else scene.SetTargetText(id, item.Issue);
                        scene.SetTargetBackground(id, Stage.BackgroundElements.Text);
                        scene.SetTargetBlocked(id, false);
                    }
                    for (int i = id + 1; i <= 18; i++) scene.SetTargetBlocked(i, true);
                    for (int i = 1; i <= 8; i++) scene.SetSourceBlocked(i, true);
                }
                else {
                    foreach (DatasetContentItem item in dataset.ItemList) {
                        if (item.Status == DatasetContentItem.StatusElements.Busy) {
                            counter++;
                            id = (counter - 1) * 2 + 1;
                            scene.SetTargetText(id, counter.ToString());
                            scene.SetTargetBackground(id, Stage.BackgroundElements.Number);
                            id++;
                            scene.SetTargetBackground(id, Stage.BackgroundElements.Text);
                            scene.SetTargetBlocked(id, false);
                            if (isHostScene) scene.SetTargetText(id, item.HostText);
                            else scene.SetTargetText(id, item.Issue);
                        }
                    }

                    counter++;
                    id++;
                    scene.SetTargetText(id, counter.ToString());
                    scene.SetTargetBackground(id, Stage.BackgroundElements.Number);
                    scene.SetTargetBlocked(id, false);

                    for (int i = id + 1; i <= 18; i++) scene.SetTargetBlocked(i, true);

                    if (selectedChoice is DatasetContentItem &&
                        targetPosition > 0) {
                        if (resolvedPosition) {
                            counter = 0;
                            foreach (DatasetContentItem item in dataset.ItemList) {
                                if (item.Index < selectedChoice.Index) {
                                    if (item.Status == DatasetContentItem.StatusElements.Busy) counter++;
                                }
                                else break;
                            }
                            id = counter * 2 + 1;
                        }
                        else id = (targetPosition - 1) * 2 + 1;
                        if (isHostScene) {
                            scene.SetTargetText(id, selectedChoice.HostText);
                            if (selectedChoice.TargetPosition == targetPosition) scene.SetTargetBackground(id, Stage.BackgroundElements.Text);
                            else scene.SetTargetBackground(id, Stage.BackgroundElements.Wrong);
                        }
                        else {
                            scene.SetTargetText(id, selectedChoice.Issue);
                            scene.SetTargetBackground(id, Stage.BackgroundElements.Text);
                        }
                        scene.SetTargetBlocked(id, false);
                    }

                    id = 0;
                    foreach (DatasetContentItem item in dataset.ChoiceList) {
                        if (item.Status == DatasetContentItem.StatusElements.Idle &&
                            (item != selectedChoice ||
                            targetPosition == 0)) {
                            id++;
                            if (isHostScene) scene.SetSourceText(id, item.HostText);
                            else scene.SetSourceText(id, item.Issue);
                            scene.SetSourceBackground(id, Stage.BackgroundElements.Text);
                            scene.SetSourceBlocked(id, false);
                        }
                    }

                    for (int i = id + 1; i <= 8; i++) scene.SetSourceBlocked(i, true);
                }
            }
        }
        public void Vstage_Select() {
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vstage_SetContent(this.hostScene, this.SelectedDataset, this.SelectedChoice, this.TargetPosition, false, true, false, false);
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vstage_SetContent(this.leftPlayerScene, this.SelectedDataset, this.SelectedChoice, this.TargetPosition, false, false, false, false);
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vstage_SetContent(this.rightPlayerScene, this.SelectedDataset, this.SelectedChoice, this.TargetPosition, false, false, false, false);
        }
        public void Vstage_ShowSolution() {
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vstage_SetContent(this.hostScene, this.SelectedDataset, this.SelectedChoice, this.TargetPosition, true, true, false, false);
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vstage_SetContent(this.leftPlayerScene, this.SelectedDataset, this.SelectedChoice, this.TargetPosition, true, false, false, false);
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vstage_SetContent(this.rightPlayerScene, this.SelectedDataset, this.SelectedChoice, this.TargetPosition, true, false, false, false);
        }
        public void Vstage_ShowResolved() {
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vstage_SetContent(this.hostScene, this.SelectedDataset, this.SelectedChoice, this.TargetPosition, false, true, true, false);
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vstage_SetContent(this.leftPlayerScene, this.SelectedDataset, this.SelectedChoice, this.TargetPosition, false, false, true, false);
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vstage_SetContent(this.rightPlayerScene, this.SelectedDataset, this.SelectedChoice, this.TargetPosition, false, false, true, false);
        }
        public void Vstage_ContentOut() {
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.ToOut();
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.ToOut();
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.ToOut();
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
                this.Save();
            }
        }

        protected override void sync_timer_Alarm2Fired(object content) {
            this.Vinsert_TimerIn();
        }

        protected override void sync_timer_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "CurrentTime") this.TimerCurrentTime = this.insertScene.Timer.CurrentTime;
                else if (e.PropertyName == "IsRunning") this.TimerIsRunning = this.insertScene.Timer.IsRunning;
            }
        }

        #endregion

    }
}
