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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.ListRefersToImageTimerScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.ListRefersToImageTimerScore {

    public class Data {
        public DatasetContent[] DataList;
        public int SelectedIndex;
    }

    public class DatasetItem : INotifyPropertyChanged {

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
                    if (value == null) value = string.Empty;
                    this.text = value;
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
                    this.hostText = Regex.Replace(value, "(?<!\r)\n", "\r\n");
                    this.on_PropertyChanged();
                }
            }
        }

        private bool idle = true;
        public bool Idle {
            get { return this.idle; }
            set {
                if (this.idle != value) {
                    this.idle = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DatasetItem() { }

        public void Reset() { this.Idle = true; }

        public void Clone(
            DatasetItem source) {
            if (source is DatasetItem) {
                this.Text = source.Text;
                this.HostText = source.HostText;
                this.Idle = source.Idle;
            }
            else {
                this.Text = string.Empty;
                this.HostText = string.Empty;
                this.Idle = true;
            }
        }

        private void buildToString() { 
            this.toString = string.Format("{0}. {1}", this.ID.ToString(), this.Text);
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

    public class DatasetContent : Messaging.Message, INotifyPropertyChanged {

        #region Properties

        private SynchronizationContext syncContext;

        private int xCorrection = 0;
        public int XCorrection {
            get { return this.xCorrection; }
            set {
                if (this.xCorrection != value) {
                    this.xCorrection = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int yCorrection = 0;
        public int YCorrection {
            get { return this.yCorrection; }
            set {
                if (this.yCorrection != value) {
                    this.yCorrection = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string name = string.Empty;
        public string Name {
            get { return this.name; }
            set {
                if (this.name != value) {
                    if (value == null) value = string.Empty;
                    this.name = value;
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
                    this.hostText = Regex.Replace(value, "(?<!\r)\n", "\r\n");
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

        private Stage.HostStyles itemHostStyle = Stage.HostStyles.Grid;
        public Stage.HostStyles ItemHostStyle {
            get { return this.itemHostStyle; }
            set {
                if (this.itemHostStyle != value) {
                    this.itemHostStyle = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private InsertList.Styles itemInsertStyle = InsertList.Styles.Columns;
        public InsertList.Styles ItemInsertStyle {
            get { return this.itemInsertStyle; }
            set {
                if (this.itemInsertStyle != value) {
                    this.itemInsertStyle = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private List<DatasetItem> itemList = new List<DatasetItem>();
        public DatasetItem[] ItemList {
            get { return this.itemList.ToArray(); }
            set {
                this.RemoveAllItems();
                if (value is DatasetItem[]) foreach (DatasetItem item in value) this.tryAddItem(item);
                this.on_PropertyChanged("ItemList");
            }
        }

        public int ItemsCount { get { return this.itemList.Count; } }

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

        #endregion


        #region Funktionen

        public DatasetContent() { }
        public DatasetContent(
            SynchronizationContext syncContext) {
            this.syncContext = syncContext;
        }

        public void Reset() {
            foreach (DatasetItem item in this.ItemList) item.Reset();
        }

        public DatasetItem GetItem(
            int index) {
            if (index >= 0 &&
                index < this.itemList.Count) return this.itemList[index];
            else return null;
        }

        public void AddItem(
            DatasetItem newItem) {
            if (this.tryAddItem(newItem)) this.on_PropertyChanged("ItemList");
        }
        private bool tryAddItem(
            DatasetItem newItem) {
            if (newItem is DatasetItem &&
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
            DatasetItem[] itemList = this.itemList.ToArray();
            foreach (DatasetItem item in itemList) itemRemoved = this.removeItem(item) | itemRemoved;
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
            DatasetItem dataset) {
                if (dataset is DatasetItem &&
                this.itemList.Contains(dataset)) {
                dataset.PropertyChanged -= this.on_PropertyChanged;
                this.itemList.Remove(dataset);
                this.setItemIndex();
                return true;
            }
            else return false;
        }

        private void setItemIndex() {
            int index = 0;
            foreach (DatasetItem item in itemList) {
                item.Index = index;
                index++;
            }
        }

        public void Clone(
            DatasetContent source) {
            if (source is DatasetContent) {
                this.Name = source.Name;
                this.HostText = source.HostText;
                this.PictureFilename = source.PictureFilename;
                this.ItemHostStyle = source.ItemHostStyle;
                this.ItemInsertStyle = source.ItemInsertStyle;
                this.ItemList = source.ItemList;
                this.XCorrection = source.XCorrection;
                this.YCorrection = source.YCorrection;
                this.Credits = source.Credits;
            }
            else {
                this.Name = string.Empty;
                this.HostText = string.Empty;
                this.PictureFilename = string.Empty;
                this.ItemHostStyle = Stage.HostStyles.Grid;
                this.ItemInsertStyle = InsertList.Styles.Columns;
                this.ItemList = new DatasetItem[0];
                this.XCorrection = 0;
                this.YCorrection = 0;
                this.Credits = string.Empty;
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

    public class Business : _Base.TimerScore.Business {

        #region Properties

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

        private int borderScaling = 100;
        public int BorderScaling {
            get { return this.borderScaling; }
            set {
                if (this.borderScaling != value) {
                    if (value < 10) this.borderScaling = 10;
                    else this.borderScaling = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetBorder();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.Border.Styles setsStyle = VentuzScenes.GamePool._Modules.Border.Styles.ThreeDotsCounter;
        public VentuzScenes.GamePool._Modules.Border.Styles BorderStyle {
            get { return this.setsStyle; }
            set {
                if (this.setsStyle != value) {
                    this.setsStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetBorder();
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
                    this.Vfullscreen_SetItemList(false);
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
                    this.Vfullscreen_SetItemList(false);
                }
            }
        }

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

        //private ListRefersToImageTimerScore.Stage leftPlayerScene;
        //public override VRemote4.HandlerSi.Scene.States LeftPlayerSceneStatus {
        //    get {
        //        if (this.leftPlayerScene is ListRefersToImageTimerScore.Stage) return this.leftPlayerScene.Status;
        //        else return VRemote4.HandlerSi.Scene.States.Unloaded;
        //    }
        //}

        //private ListRefersToImageTimerScore.Stage rightPlayerScene;
        //public override VRemote4.HandlerSi.Scene.States RightPlayerSceneStatus {
        //    get {
        //        if (this.rightPlayerScene is ListRefersToImageTimerScore.Stage) return this.rightPlayerScene.Status;
        //        else return VRemote4.HandlerSi.Scene.States.Unloaded;
        //    }
        //}

        private bool repressPropertyChanged = false;

        #endregion


        #region Funktionen

        public Business() { }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.ListRefersToImageTimerScore'", typeIdentifier);
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

            this.fullscreenScene = new Fullscreen(syncContext, fullscreenMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.fullscreenScene.StatusChanged += this.fullscreenScene_StatusChanged;

            this.hostScene = new Stage(syncContext, hostMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.hostScene.StatusChanged += this.hostScene_StatusChanged;

            //this.leftPlayerScene = new ListRefersToImageTimerScore.Stage(syncContext, leftPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            //this.leftPlayerScene.StatusChanged += this.leftPlayerScene_StatusChanged;

            //this.rightPlayerScene = new ListRefersToImageTimerScore.Stage(syncContext, rightPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            //this.rightPlayerScene.StatusChanged += this.rightPlayerScene_StatusChanged;

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

            this.fullscreenScene.StatusChanged -= this.fullscreenScene_StatusChanged;
            this.fullscreenScene.Dispose();

            this.hostScene.StatusChanged -= this.hostScene_StatusChanged;
            this.hostScene.Dispose();

            //this.leftPlayerScene.StatusChanged -= this.leftPlayerScene_StatusChanged;
            //this.leftPlayerScene.Dispose();

            //this.rightPlayerScene.StatusChanged -= this.rightPlayerScene_StatusChanged;
            //this.rightPlayerScene.Dispose();

        }

        public override void ResetData() {
            base.ResetData();
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
            if (this.SelectedDataset is DatasetContent) this.SelectedDataset.Reset();
            this.on_PropertyChanged("SelectedDatasetItems");
        }

        public void ExecuteSelection(
            DatasetItem item) {
            if (item is DatasetItem
                && item.Idle) {
                item.Idle = false;
                this.Vfullscreen_SetItemList(false);
                if (this.fullscreenMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.InsertList.PlayJingle("Select");
                this.Vhost_SetContent();
                this.on_PropertyChanged("SelectedDatasetItems");
            }
        }

        public void TakeSelectionBack(
            DatasetItem item) {
            if (item is DatasetItem
                && !item.Idle) {
                item.Idle = true;
                this.Vfullscreen_SetItemList(false);
                this.Vhost_SetContent();
                this.on_PropertyChanged("SelectedDatasetItems");
            }
        }

        public void Wrong() {
            if (this.fullscreenMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.InsertList.PlayJingle("Wrong");
            this.Vinsert_TimerOut();
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
                StreamReader file = null;
                try {
                    string line;
                    DatasetContent newDataset = null;
                    file = new StreamReader(filename, Encoding.UTF7);
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
                                DatasetItem newItem = new DatasetItem();
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
        public void Vinsert_BorderIn() {
            this.Vinsert_SetBorder();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Border.ToIn();
            if (this.TaskCounter > 0 && this.TaskCounter <= this.TaskCounterSize) this.Vinsert_TaskCounterIn();
        }
        public void Vinsert_SetBorder() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                this.Vinsert_SetBorder(this.insertScene.Border, this.LeftPlayerScore, this.RightPlayerScore);
            }
        }
        public void Vinsert_SetBorder(
            VentuzScenes.GamePool._Modules.Border scene,
            int leftPlayerScore,
            int rightPlayerScore) {
            if (scene is VentuzScenes.GamePool._Modules.Border) {
                scene.SetPositionX(this.BorderPositionX);
                scene.SetPositionY(this.BorderPositionY);
                scene.SetScaling(this.BorderScaling);
                scene.SetStyle(this.BorderStyle);
                scene.SetLeftScore(leftPlayerScore);
                scene.SetRightScore(rightPlayerScore);
            }
        }
        public void Vinsert_BorderOut() { 
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Border.ToOut();
            this.Vinsert_TaskCounterOut();
        }

        public override void Vinsert_TimerIn() {
            this.Vinsert_SetTimer();
            this.Vinsert_ResetTimer();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.ToIn();
        }
        public override void Vinsert_SetTimer() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.Timer.SetPositionX(this.TimerPositionX);
                this.insertScene.Timer.SetPositionY(this.TimerPositionY);
                this.insertScene.Timer.SetStyle(this.TimerStyle);
                this.insertScene.Timer.SetScaling(100);
                if (this.RunExtraTime) this.insertScene.Timer.SetStartTime(this.TimerExtraTime);
                else this.insertScene.Timer.SetStartTime(this.TimerStartTime);
                this.insertScene.Timer.SetStopTime(this.TimerStopTime);
                this.insertScene.Timer.SetAlarmTime1(this.TimerAlarmTime1);
                this.insertScene.Timer.SetAlarmTime2(this.TimerAlarmTime2);
            }
        }
        public override void Vinsert_StartTimer() {
            this.Vinsert_SetTimer();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.StartTimer();
        }
        public override void Vinsert_StopTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.StopTimer(); }
        public override void Vinsert_ContinueTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.ContinueTimer(); }
        public override void Vinsert_ResetTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.ResetTimer(); }
        public override void Vinsert_TimerOut() {
            this.Vinsert_StopTimer();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.ToOut();
        }

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

        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void Vfullscreen_LoadScene() { this.fullscreenScene.Load(); }
        public void Vfullscreen_ContentIn() {
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) {
                this.fullscreenScene.SetPictureFilename(this.SelectedDataset.PictureFilename);
                this.fullscreenScene.ToIn();
            }
        }
        public void Vfullscreen_ItemListIn() {
            this.Vfullscreen_SetItemList(false);
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.fullscreenScene.InsertList.ToIn();
                if (this.SelectedDataset is DatasetContent) {
                    if (string.IsNullOrEmpty(this.SelectedDataset.Credits)) this.fullscreenScene.SetCreditsText(string.Empty);
                    else {
                        this.fullscreenScene.SetCreditsText(this.SelectedDataset.Credits);
                        this.fullscreenScene.ShowCredits();
                    }
                }
            }
        }
        public void Vfullscreen_SetItemList(
            bool showAll) {
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vfullscreen_SetItemList(this.fullscreenScene.InsertList, this.SelectedDataset, showAll);
        }
        public void Vfullscreen_SetItemList(
            InsertList scene,
            DatasetContent value,
            bool showAll) {
            if (scene is InsertList) {
                scene.SetPositionX(this.ItemInsertPositionX);
                scene.SetPositionY(this.ItemInsertPositionY);
                if (value is DatasetContent) {
                    scene.SetXCorrection(value.XCorrection);
                    scene.SetYCorrection(value.YCorrection);
                    scene.SetStyle(value.ItemInsertStyle);
                    scene.SetItemsCount(value.ItemsCount);
                    foreach (DatasetItem item in value.ItemList) {
                        scene.SetItemIdle(item.ID, item.Idle & !showAll);
                        scene.SetItemText(item.ID, item.Text);
                    }
                }
            }
        }
        public void Vfullscreen_ResolveItemList() {
            this.Vinsert_TimerOut();
            this.Vfullscreen_SetItemList(true);
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.InsertList.ToIn();
        }
        public void Vfullscreen_ItemListOut() {
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.InsertList.ToOut();
        }
        public void Vfullscreen_ContentOut() {
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.ToOut();
            this.Vfullscreen_ItemListOut();
        }
        public override void Vfullscreen_UnloadScene() { this.fullscreenScene.Unload(); }

        public override void Vstage_Init() { 
            base.Vstage_Init();
            if (this.leftPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerMasterScene.LiveVideoIn();
            if (this.rightPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerMasterScene.LiveVideoIn();
        }

        public override void Vhost_LoadScene() { this.hostScene.Load(); }
        internal void Vhost_ContentIn() {
            this.Vhost_SetContent();
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.ToIn();
        }
        internal void Vhost_SetContent() {
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent) this.Vhost_SetContent(this.hostScene, this.SelectedDataset);
        }
        public void Vhost_SetContent(
            Stage scene,
            DatasetContent value) {
            if (scene is Stage &&
                value is DatasetContent) {
                scene.SetHeadline(value.HostText);
                scene.SetStyle(value.ItemHostStyle);
                if (value.ItemInsertStyle == InsertList.Styles.Columns) scene.SetPictureFilename(value.PictureFilename);
                else scene.SetPictureFilename(string.Empty);
                scene.SetItemsCount(value.ItemsCount);
                foreach (DatasetItem item in value.ItemList) {
                    scene.SetItemText(item.ID, item.HostText);
                    scene.SetItemIdle(item.ID, item.Idle);
                }
            }
        }
        internal void Vhost_ContentOut() { if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.ToOut(); }
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
            if (e is PropertyChangedEventArgs &&
                !this.repressPropertyChanged) {
                if (e.PropertyName == "Name") {
                    this.buildNameList();
                    this.on_PropertyChanged("NameList");
                }
                else if (e.PropertyName == "XCorrection") this.Vfullscreen_SetItemList(false);
                else if (e.PropertyName == "YCorrection") this.Vfullscreen_SetItemList(false);
                else if (e.PropertyName == "ItemInsertStyle") this.Vfullscreen_SetItemList(false);
            }
            this.Save();
        }

        protected override void sync_timer_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "CurrentTime") this.TimerCurrentTime = this.insertScene.Timer.CurrentTime;
                else if (e.PropertyName == "IsRunning") this.TimerIsRunning = this.insertScene.Timer.IsRunning;
            }
        }

        protected override void sync_timer_StopFired(object content) {
            base.sync_timer_StopFired(content);
            this.Vinsert_TimerOut();
        }

        #endregion


    }
}
