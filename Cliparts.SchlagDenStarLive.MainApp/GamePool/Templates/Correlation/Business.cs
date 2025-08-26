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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.Correlation;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.Correlation {

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
                string sortItemA = a.Solution;
                string sortItemB = b.Solution;
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

        private string solution = string.Empty;
        public string Solution {
            get { return this.solution; }
            set {
                if (this.solution != value) {
                    if (value == null) value = string.Empty;
                    this.solution = value;
                    this.buildToString();
                    this.on_PropertyChanged();
                }
            }
        }

        private string solutionHostText = string.Empty;
        public string SolutionHostText {
            get { return this.solutionHostText; }
            set {
                if (this.solutionHostText != value) {
                    if (value == null) value = string.Empty;
                    this.solutionHostText = value;
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
                this.Issue = source.Issue;
                this.Solution = source.Solution;
                this.SolutionHostText = source.SolutionHostText;
                this.ChoiceIndex = source.ChoiceIndex;
            }
            else {
                this.Issue = string.Empty;
                this.Solution = string.Empty;
                this.SolutionHostText = string.Empty;
            }
        }

        private void buildToString() { this.toString = this.Issue + " - " + this.Solution; }

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

        private List<string> choices = new List<string>();
        public string[] ChoiceList { get { return this.choices.ToArray(); } }

        private List<string> hostChoices = new List<string>();
        public string[] HostChoiceList { get { return this.hostChoices.ToArray(); } }

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
                DatasetContentItem item = new DatasetContentItem();
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
            this.hostChoices.Clear();
            while (this.choices.Count < ItemsCount) this.choices.Add(string.Empty);
            while (this.hostChoices.Count < ItemsCount) this.hostChoices.Add(string.Empty);
            foreach (DatasetContentItem item in this.itemList) {
                int index = item.ChoiceIndex;
                if (index >= 0 &&
                    index < this.choices.Count) this.choices[index] = item.Solution;
                if (index >= 0 &&
                    index < this.hostChoices.Count) this.hostChoices[index] = item.SolutionHostText;
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
                string text = this.choices[index];
                this.choices[index] = this.choices[index - 1];
                this.choices[index - 1] = text;
                this.on_PropertyChanged("ChoiceList");
                return true;
            }
            else return false;
        }
        public bool TryMoveChoiceDown(
            int index) {
            if (index >= 0 &&
                index < this.choices.Count - 1) {
                string text = this.choices[index];
                this.choices[index] = this.choices[index + 1];
                this.choices[index + 1] = text;
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
                this.ItemList = source.ItemList;
            }
            else {
                this.Name = string.Empty;
                this.ItemList = new DatasetContentItem[0];
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

        void item_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_item_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_item_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs &&
                !this.repressPropertyChanged) {
                if (e.PropertyName == "Issue" ||
                    e.PropertyName == "Solution") {
                    this.buildNameList();
                    this.on_PropertyChanged("NameList");
                    this.buildChoiceList();
                    this.on_PropertyChanged("ChoiceList");
                }
                else if (e.PropertyName == "ChoiceIndex") {
                    this.buildChoiceList();
                    this.on_PropertyChanged("ChoiceList");
                }
            }
        }

        #endregion

    }

    public class ListItemDataset {

        #region Properties

        public int Index { get; private set; }

        private string issue = string.Empty;
        public string Issue {
            get { return this.issue; }
            set {
                if (this.issue != value) {
                    if (value == null) value = string.Empty;
                    this.issue = value;
                }
            }
        }

        private string solution = string.Empty;
        public string Solution {
            get { return this.solution; }
            set {
                if (this.solution != value) {
                    if (value == null) value = string.Empty;
                    this.solution = value;
                }
            }
        }

        private string solutionHostText = string.Empty;
        public string SolutionHostText {
            get { return this.solutionHostText; }
            set {
                if (this.solutionHostText != value) {
                    if (value == null) value = string.Empty;
                    this.solutionHostText = value;
                }
            }
        }

        private string selection = string.Empty;
        public string Selection {
            get { return this.selection; }
            set {
                if (this.selection != value) {
                    if (value == null) value = string.Empty;
                    this.selection = value;
                }
            }
        }

        private string selectionHostText = string.Empty;
        public string SelectionHostText {
            get { return this.selectionHostText; }
            set {
                if (this.selectionHostText != value) {
                    if (value == null) value = string.Empty;
                    this.selectionHostText = value;
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
                }
            }
        }

        public bool IsIdle { get { return string.IsNullOrEmpty(this.Selection); } }

        public bool IsTrue { get { return !this.IsIdle && this.Solution == this.Selection; } }

        #endregion


        #region Funktionen

        public ListItemDataset(
            int index,
            string issue,
            string solution,
            string solutionHostText,
            int choiceIndex) {
            this.Index = index;
            this.Issue = issue;
            this.Solution = solution;
            this.SolutionHostText = solutionHostText;
            this.ChoiceIndex = choiceIndex;
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

        private bool timerEnabled = false;
        [Serialization.NotSerialized]
        public bool TimerEnabled {
            get { return this.timerEnabled; }
            set {
                if (this.timerEnabled != value) {
                    this.timerEnabled = value;
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
        [Serialization.NotSerialized]
        public DatasetContent[] DataList {
            get { return this.dataList.ToArray(); }
            set {
                this.tryRemoveAllDatasets();
                if (value is DatasetContent[]) {
                    foreach (DatasetContent item in value) this.tryAddDataset(item, -1);
                }
                this.SelectDataset(0);
            }
        }

        private List<string> names = new List<string>();
        public string[] NameList { get { return this.names.ToArray(); } }

        public int DatasetsCount { get { return this.dataList.Count; } }

        public DatasetContent SelectedDataset { get; private set; }

        public int SelectedDatasetIndex { get; private set; }

        private List<ListItemDataset> itemList = new List<ListItemDataset>();

        private ListItemDataset selectedItem = null;

        private List<string> idleItems = new List<string>();
        public string[] IdleItems { get { return this.idleItems.ToArray(); } }

        private List<string> choicesList = new List<string>();
        private List<string> hostChoicesList = new List<string>();

        private string selectedChoice = string.Empty;

        private List<string> idleChoices = new List<string>();
        public string[] IdleChoices { get { return this.idleChoices.ToArray(); } }

        private List<string> usedChoices = new List<string>();
        public string[] UsedChoices { get { return this.usedChoices.ToArray(); } }

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

            this.ClassInfo = string.Format("'{0}' of 'Templates.Correlation'", typeIdentifier);
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
            this.TimerEnabled = false;
            this.TaskCounter = 0;
            this.SelectDataset(0);
        }

        public override void Next() {
            base.Next();
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
            this.itemList.Clear();
            this.selectedItem = null;
            this.idleItems.Clear();
            this.choicesList.Clear();
            this.hostChoicesList.Clear();
            this.selectedChoice = string.Empty;
            this.idleChoices.Clear();
            this.usedChoices.Clear();
            this.SelectionStatus = SelectionStates.Idle;
            if (this.SelectedDataset is DatasetContent) {
                int counter = 0;
                foreach (DatasetContentItem item in this.SelectedDataset.ItemList) {
                    counter++;
                    if (item is DatasetContentItem &&
                        counter < DatasetContent.ItemsCount) {
                        ListItemDataset newItem = new ListItemDataset(this.itemList.Count, item.Issue, item.Solution, item.SolutionHostText, item.ChoiceIndex);
                        this.itemList.Add(newItem);
                        this.idleItems.Add(newItem.Issue);
                    }
                }
                foreach (string choice in this.SelectedDataset.ChoiceList) {
                    if (!string.IsNullOrEmpty(choice)) {
                        this.choicesList.Add(choice);
                        this.idleChoices.Add(choice);
                    }
                }
                foreach (string choice in this.SelectedDataset.HostChoiceList) {
                    if (!string.IsNullOrEmpty(choice)) {
                        this.hostChoicesList.Add(choice);
                    }
                }
            }
            this.on_PropertyChanged("IdleItems");
            this.on_PropertyChanged("IdleChoices");
            this.on_PropertyChanged("UsedChoices");
        }

        private void buildItemLists() {
            this.idleItems.Clear();
            foreach (ListItemDataset item in this.itemList) {
                if (item is ListItemDataset &&
                    item.IsIdle) this.idleItems.Add(item.Issue);
            }
        }

        public void SelectItem(
            int index) {
            this.selectedItem = null;
            if (index >= 0 &&
                index < this.idleItems.Count) {
                foreach (ListItemDataset item in this.itemList) {
                    if (item is ListItemDataset &&
                        item.Issue == this.idleItems[index]) {
                        this.selectedItem = item;
                        break;
                    }
                }
            }
            this.setSelectionStatus();
        }

        public void SelectChoice(
            string text) {
            if (this.idleChoices.Contains(text)) this.selectedChoice = text;
            else this.selectedChoice = string.Empty;
            this.setSelectionStatus();
        }

        private void setSelectionStatus() {
            if (this.selectedItem is ListItemDataset &&
                !string.IsNullOrEmpty(this.selectedChoice)) {
                if (this.selectedItem.Solution == this.selectedChoice) this.SelectionStatus = SelectionStates.True;
                else this.SelectionStatus = SelectionStates.False;
            }
            else this.SelectionStatus = SelectionStates.Idle;
        }

        public void ExecuteSelection() {
            if (this.SelectionStatus != SelectionStates.Idle) {
                int choiceIndex = this.choicesList.IndexOf(this.selectedChoice);
                if (choiceIndex >= 0) {
                    this.selectedItem.Selection = this.choicesList[choiceIndex];
                    this.selectedItem.SelectionHostText = this.hostChoicesList[choiceIndex];
                    if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                        this.insertScene.PlayJingle("Morve");
                        this.insertScene.ChoiceButtonOut(choiceIndex + 1);
                        this.insertScene.SetSolutionButtonSelectionText(this.selectedItem.Index + 1, this.selectedItem.Selection);
                        this.insertScene.SelectSolutionButton(this.selectedItem.Index + 1);
                    }
                }
            }
        }

        public void Resolve() {
            if (this.SelectionStatus != SelectionStates.Idle) {
                if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                    if (this.selectedItem.IsTrue) this.insertScene.SetSolutionButtonTrue(this.selectedItem.Index + 1);
                    else this.insertScene.SetSolutionButtonFalse(this.selectedItem.Index + 1);
                }
                if (this.selectedItem.IsTrue) {
                    this.buildItemLists();
                    if (this.idleChoices.Contains(this.selectedChoice)) this.idleChoices.Remove(this.selectedChoice);
                    if (!this.usedChoices.Contains(this.selectedChoice)) this.usedChoices.Add(this.selectedChoice);
                    this.on_PropertyChanged("IdleItems");
                    this.on_PropertyChanged("IdleChoices");
                    this.on_PropertyChanged("UsedChoices");
                    this.selectedChoice = string.Empty;
                    this.selectedItem = null;
                    this.setSelectionStatus();
                }
                else this.insertScene.PlayJingle("Wrong");
            }
        }

        public void ShowSolution() {
            if (this.selectedItem is ListItemDataset &&
                !this.selectedItem.IsIdle &&
                !this.selectedItem.IsTrue) {
                int choiceIndex = this.choicesList.IndexOf(this.selectedItem.Selection);
                this.selectedItem.Selection = this.selectedItem.Solution;
                this.selectedItem.SelectionHostText = this.selectedItem.SolutionHostText;
                if (choiceIndex >= 0 &&
                    this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                    this.insertScene.ChoiceButtonIn(choiceIndex + 1);
                    this.insertScene.SetSolutionButtonTrue(this.selectedItem.Index + 1);
                    this.insertScene.ChoiceButtonOut(this.selectedItem.ChoiceIndex + 1);
                }
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
                Helper.invokeActionAfterDelay(this.setRepressPropertyChangedFalse, 5000, this.syncContext);
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
                StreamReader file = null;
                try {
                    string line;
                    int lineCounter = 0;
                    DatasetContent newDataset = null;
                    file = new StreamReader(filename, Encoding.UTF7);
                    while ((line = file.ReadLine()) != null) {
                        line = line.Trim();
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
                                    string[] lineArray = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                                    if (lineArray.Length >= 1) newItem.Issue = lineArray[0].Trim();
                                    if (lineArray.Length >= 2) {
                                        string solution = lineArray[1].Trim();
                                        newItem.SolutionHostText = solution;
                                        int index = solution.IndexOf("(");
                                        if (index > 0) {
                                            solution = solution.Substring(0, index - 1);
                                        }
                                        newItem.Solution = solution.Trim();
                                    }
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
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Reset();
            this.Vinsert_SetContent(false);
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.ToIn();
        }
        public void Vinsert_SetContent(
            bool moveButtons) {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                this.Vinsert_SetContent(this.insertScene, this.itemList.ToArray(), this.SelectedDataset, moveButtons);
            }
        }
        public void Vinsert_SetContent(
            Insert scene,
            ListItemDataset[] itemList,
            DatasetContent dataset,
            bool moveButtons) {
            if (scene is Insert) {
                int counter = 0;
                foreach (ListItemDataset item in this.itemList) {
                    counter++;
                    if (counter < DatasetContent.ItemsCount) {
                        scene.SetSolutionButtonChoiceIndex(counter, item.ChoiceIndex);
                        scene.SetSolutionButtonSelectionText(counter, item.Selection);
                        scene.SetSolutionButtonSolutionText(counter, item.Solution);
                        scene.SetSolutionButtonTopText(counter, item.Issue);
                        if (moveButtons) {
                            if (item.IsIdle) scene.SetSolutionButtonNeutral(counter);
                            else if (item.IsTrue) scene.SetSolutionButtonTrue(counter);
                            else scene.SetSolutionButtonFalse(counter);
                        }
                    }
                    scene.SetChoiceButtonText(item.ChoiceIndex + 1, item.Solution);
                    if (moveButtons) {
                        if (this.idleChoices.Contains(item.Solution)) scene.ChoiceButtonIn(item.ChoiceIndex + 1);
                        else scene.ChoiceButtonOut(item.ChoiceIndex + 1);
                    }
                }
                if (dataset is DatasetContent) {
                    for (int i = 0; i < 8; i++) {
                        scene.SetSolutionButtonChoiceIndex(i + 1, dataset.ItemList[i].ChoiceIndex);
                        scene.SetSolutionButtonSolutionText(i + 1, dataset.ItemList[i].Solution);
                        scene.SetSolutionButtonTopText(i + 1, dataset.ItemList[i].Issue);
                    }
                    for (int i = 0; i < 9; i++) {
                        scene.SetChoiceButtonText(i + 1, dataset.ChoiceList[i]);
                    }
                }
            }
        }
        public void Vinsert_ResolveAll() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Resolve(); }
        public void Vinsert_ContentOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.ToOut(); }
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
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vstage_SetContent(this.hostScene, this.itemList.ToArray(), this.SelectedDataset, true, false, false);
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vstage_SetContent(this.leftPlayerScene, this.itemList.ToArray(), this.SelectedDataset, false, false, false);
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vstage_SetContent(this.rightPlayerScene, this.itemList.ToArray(), this.SelectedDataset, false, false, false);
        }
        public void Vstage_SetContent(
            VentuzScenes.GamePool.Correlation.Stage stageScene,
            ListItemDataset[] itemList,
            DatasetContent dataset,
            bool isHostScene,
            bool resolved,
            bool isPreview) {
            int counter = 0;
            if (dataset is DatasetContent) {
                stageScene.SetTaskText(dataset.Name);
                foreach (ListItemDataset item in itemList) {
                    counter++;
                    stageScene.SetSolutionTopText(counter, item.Issue);
                    if (resolved) {
                        if (isHostScene) stageScene.SetSolutionBottomText(counter, item.SolutionHostText);
                        else stageScene.SetSolutionBottomText(counter, item.Solution);
                        stageScene.SetSolutionIsTrue(counter, true);
                    }
                    else {
                        if (item.IsIdle) {
                            stageScene.SetSolutionBottomText(counter, string.Empty);
                            stageScene.SetSolutionIsTrue(counter, true);
                        }
                        else {
                            if (isHostScene) {
                                stageScene.SetSolutionBottomText(counter, item.SelectionHostText);
                                stageScene.SetSolutionIsTrue(counter, item.IsTrue);
                            }
                            else {
                                stageScene.SetSolutionBottomText(counter, item.Selection);
                                stageScene.SetSolutionIsTrue(counter, true);
                            }
                        }
                    }
                }
                foreach (DatasetContentItem item in dataset.ItemList) {
                    if (isHostScene) stageScene.SetChoiceText(item.ChoiceIndex + 1, item.SolutionHostText);
                    else stageScene.SetChoiceText(item.ChoiceIndex + 1, item.Solution);
                    if (isPreview) stageScene.ShowChoice(item.ChoiceIndex + 1);
                    else {
                        if (resolved ||
                            this.usedChoices.Contains(item.Solution) ||
                            this.selectedChoice == item.Solution) stageScene.HideChoice(item.ChoiceIndex + 1);
                        else stageScene.ShowChoice(item.ChoiceIndex + 1);
                    }
                }
            }
        }
        public void Vstage_Select() {
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vstage_SetContent(this.hostScene, this.itemList.ToArray(), this.SelectedDataset, true, false, false);
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vstage_SetContent(this.leftPlayerScene, this.itemList.ToArray(), this.SelectedDataset, false, false, false);
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vstage_SetContent(this.rightPlayerScene, this.itemList.ToArray(), this.SelectedDataset, false, false, false);
        }
        public void Vstage_ShowSolution() {
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vstage_SetContent(this.hostScene, this.itemList.ToArray(), this.SelectedDataset, true, false, false);
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vstage_SetContent(this.leftPlayerScene, this.itemList.ToArray(), this.SelectedDataset, false, false, false);
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vstage_SetContent(this.rightPlayerScene, this.itemList.ToArray(), this.SelectedDataset, false, false, false);
        }
        public void Vstage_ShowResolved() {
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vstage_SetContent(this.hostScene, this.itemList.ToArray(), this.SelectedDataset, true, true, false);
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vstage_SetContent(this.leftPlayerScene, this.itemList.ToArray(), this.SelectedDataset, false, true, false);
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vstage_SetContent(this.rightPlayerScene, this.itemList.ToArray(), this.SelectedDataset, false, true, false);
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
            if (this.syncContext != null &&
                !this.repressPropertyChanged) this.syncContext.Post(callback, e);
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
