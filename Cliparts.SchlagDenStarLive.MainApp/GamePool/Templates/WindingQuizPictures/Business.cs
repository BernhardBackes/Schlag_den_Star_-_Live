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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.WindingQuizPictures;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.WindingQuizPictures {

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

        private string pictureFilename = string.Empty;
        public string PictureFilename {
            get { return this.pictureFilename; }
            set {
                if (this.pictureFilename != value) {
                    if (string.IsNullOrEmpty(value)) this.pictureFilename = string.Empty;
                    else this.pictureFilename = value;
                    this.Picture = Helper.getThumbnailFromMediaFile(this.pictureFilename);
                    this.on_PropertyChanged();
                }
            }
        }

        [XmlIgnore]
        public Image Picture { get; private set; }

        private string hostText = string.Empty;
        public string HostText {
            get { return this.hostText; }
            set {
                if (this.hostText != value) {
                    if (value == null) this.hostText = string.Empty;
                    else this.hostText = Regex.Replace(value, "(?<!\r)\n", "\r\n");
                    this.buildToString();
                    this.on_PropertyChanged();
                }
            }
        }

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DataItem() { }
        public DataItem(
            string pictureFilename) {
            if (string.IsNullOrEmpty(pictureFilename)) {
                this.HostText = "?";
                this.PictureFilename = string.Empty;
            }
            else {
                this.HostText = Path.GetFileNameWithoutExtension(pictureFilename);
                this.PictureFilename = pictureFilename;
            }
        }

        public void Clone(
            DataItem source) {
            if (source is DataItem) {
                this.HostText = source.HostText;
                this.PictureFilename = source.PictureFilename;
            }
            else {
                this.HostText = string.Empty;
                this.PictureFilename = string.Empty;
            }
        }

        private void buildToString() {
            this.toString = string.Format("{0}. {1}", this.ID.ToString(), Regex.Replace(this.HostText, "\r\n", " "));
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
                this.ItemList = source.ItemList;
            }
            else {
                this.Name = string.Empty;
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

    public class Business : _Base.BuzzerScore.Business {

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

        private int textInsertPositionX = 0;
        public int TextInsertPositionX {
            get { return this.textInsertPositionX; }
            set {
                if (this.textInsertPositionX != value) {
                    this.textInsertPositionX = value;
                    this.on_PropertyChanged();
                    this.Vfullscreen_SetTextInsert();
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
                    this.Vfullscreen_SetTextInsert();
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
                    this.Vfullscreen_SetTextInsert();
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.WindingQuizPictures'", typeIdentifier);
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

        public override void New() {
            base.New();
            this.Filename = string.Empty;
        }

        public override void ResetData() {
            base.ResetData();
            this.TaskCounter = 0;
            this.SelectDataset(0);
        }

        public override void Next() {
            base.Next();
            this.TaskCounter++;
            this.Vinsert_SetTaskCounter();
            this.SelectDataset(this.SelectedDatasetIndex + 1);
        }

        public override void ReleaseBuzzer() {
            base.ReleaseBuzzer();
            this.Vinsert_SetBorderBuzzer();
        }

        public override void DoBuzzer(PlayerSelection buzzeredPlayer) {
            base.DoBuzzer(buzzeredPlayer);
            this.Vinsert_SetBorderBuzzer();
        }

        public void True() {
            this.Vinsert_StopTimeout();
            switch (this.BuzzeredPlayer) {
                case PlayerSelection.LeftPlayer:
                    this.LeftPlayerScore++;
                    break;
                case PlayerSelection.RightPlayer:
                    this.RightPlayerScore++;
                    break;
            }
            this.Vinsert_SetScore();
            this.Vinsert_SetBorder();
            this.Vstage_SetScore();
        }

        public void False() {
            this.Vinsert_StopTimeout();
            switch (this.BuzzeredPlayer) {
                case PlayerSelection.LeftPlayer:
                    this.RightPlayerScore++;
                    break;
                case PlayerSelection.RightPlayer:
                    this.LeftPlayerScore++;
                    break;
            }
            this.Vinsert_SetScore();
            this.Vinsert_SetBorder();
            this.Vstage_SetScore();
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

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }

        public override void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score, this.LeftPlayerScore, this.RightPlayerScore); }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }

        public override void Vinsert_Buzzer(PlayerSelection buzzeredPlayer) { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_Buzzer(this.insertScene.Timeout, buzzeredPlayer); }
        public override void Vinsert_SetTimeout() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetTimeout(this.insertScene.Timeout); }
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

        public void Vinsert_BorderIn() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                this.Vinsert_SetBorder(this.insertScene.Border, this.LeftPlayerScore, this.RightPlayerScore);
                this.insertScene.Border.ToIn();
            }
            if (this.TaskCounter > 0 && this.TaskCounter <= this.TaskCounterSize) this.Vinsert_TaskCounterIn();
        }
        public void Vinsert_SetBorder() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                this.Vinsert_SetBorder(this.insertScene.Border, this.LeftPlayerScore, this.RightPlayerScore);
                this.Vinsert_SetBorderBuzzer(this.insertScene.Border, this.BuzzeredPlayer);
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
        public override void Vinsert_SetBorderBuzzer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetBorderBuzzer(this.insertScene.Border, this.BuzzeredPlayer); }
        public override void Vinsert_SetBorderBuzzer(PlayerSelection buzzeredPlayer) { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetBorderBuzzer(this.insertScene.Border, buzzeredPlayer); }
        public void Vinsert_BorderOut() {
            if (this.insertScene is VRemote4.HandlerSi.Scene &&
                this.insertScene.Border is VentuzScenes.GamePool._Modules.Border) this.insertScene.Border.ToOut();
            this.Vinsert_TaskCounterOut();
        }
        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }


        public override void Vfullscreen_LoadScene() { this.fullscreenScene.Load(); }
        public void Vfullscreen_ContentIn() {
            this.Vfullscreen_SetItem();
            if (this.fullscreenScene is Fullscreen) this.fullscreenScene.ToIn();
        }
        public void Vfullscreen_SetItem() { this.Vfullscreen_SetItem(this.fullscreenScene, this.SelectedDataItem); }
        public void Vfullscreen_SetItem(
            Fullscreen scene,
            DataItem item) {
            if (scene is Fullscreen) {
                if (item is DataItem) scene.SetPictureFilename(item.PictureFilename);
                else scene.SetPictureFilename(string.Empty);
            }
        }
        internal void Vfullscreen_NextItem() {
            this.Vfullscreen_SetItem();
            if (this.fullscreenScene is Fullscreen) this.fullscreenScene.ToNext();
        }
        public void Vfullscreen_Resolve() {
            this.Vfullscreen_SetTextInsert();
            if (this.fullscreenScene is VRemote4.HandlerSi.Scene &&
                this.SelectedDataset is DatasetContent) {
                this.fullscreenScene.PlayJingleResolve();
                this.fullscreenScene.TextInsert.ToIn();
                //Task.Delay(800).ContinueWith((a) => this.fullscreenScene.PlayJingleResolve(), CancellationToken.None);
            }
        }
        public void Vfullscreen_SetTextInsert() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene && this.SelectedDataset is DatasetContent) this.Vfullscreen_SetTextInsert(this.fullscreenScene.TextInsert, this.SelectedDataset.Name); }
        public void Vfullscreen_SetTextInsert(
            VentuzScenes.GamePool._Modules.TextInsert scene,
            string text) {
            if (scene is VentuzScenes.GamePool._Modules.TextInsert) {
                scene.SetPositionX(this.TextInsertPositionX);
                scene.SetPositionY(this.TextInsertPositionY);
                scene.SetStyle(this.TextInsertStyle);
                scene.SetText(text);
            }
        }
        public void Vfullscreen_TextInsertOut() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.TextInsert.ToOut(); }
        public void Vfullscreen_ContentOut() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.ToOut(); }
        public override void Vfullscreen_UnloadScene() { this.fullscreenScene.Unload(); }


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
            this.Vhost_SetContent();
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.ToIn();
        }
        internal void Vhost_SetContent() {
            if (this.hostScene is VRemote4.HandlerSi.Scene) {
                this.Vhost_SetContent(this.hostScene, this.TaskCounter, this.SelectedDataset);
                this.Vhost_SetItem(this.hostScene, this.SelectedDataset, this.SelectedDataItem);
            }
        }
        internal void Vhost_SetContent(
            Host scene,
            int counter,
            DatasetContent dataset) {
            if (scene is Host &&
                dataset is DatasetContent) {
                string hostText = string.Empty;
                if (counter == 0) hostText = string.Format("Beispiel: {0}", dataset.Name);
                else if (counter > 0 && counter <= this.TaskCounterSize) hostText = string.Format("{0}/{1}: {2}", counter.ToString(), this.TaskCounterSize.ToString(), dataset.Name);
                this.hostScene.SetName(hostText);
            }
        }
        internal void Vhost_NextItem() { this.Vhost_SetItem(); }
        public void Vhost_SetItem() { if (this.hostScene is VRemote4.HandlerSi.Scene) this.Vhost_SetItem(this.hostScene, this.SelectedDataset, this.SelectedDataItem); }
        internal void Vhost_SetItem(
            Host scene,
            DatasetContent dataset,
            DataItem item) {
            if (scene is Host &&
                scene.Status == VRemote4.HandlerSi.Scene.States.Available) {
                if (dataset is DatasetContent &&
                item is DataItem) {
                    string hostText = string.Empty;
                    hostText = string.Format("{0}/{1}: {2}", item.ID.ToString(), dataset.ItemsCount.ToString(), item.HostText);
                    scene.SetItem(item.PictureFilename, hostText);
                }
                else scene.SetItem(string.Empty, string.Empty);
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
