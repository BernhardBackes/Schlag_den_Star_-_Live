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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TwelveIssuesTimerScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TwelveIssuesTimerScore {

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

        private string name = string.Empty;
        public string Name {
            get { return this.name; }
            set {
                if (this.name != value) {
                    if (string.IsNullOrEmpty(value)) this.name = string.Empty;
                    else this.name = value;
                    this.buildToString();
                    this.on_PropertyChanged();
                }
            }
        }

        private string filename = string.Empty;
        public string Filename {
            get { return this.filename; }
            set {
                if (this.filename != value) {
                    if (value == null) value = string.Empty;
                    this.filename = value;
                    this.on_PropertyChanged();
                    this.FileExists = File.Exists(this.filename);
                }
            }
        }

        private bool fileExists = false;
        [XmlIgnore]
        public bool FileExists {
            get { return this.fileExists; }
            set {
                if (this.fileExists != value) {
                    this.fileExists = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool isIdle = true;
        public bool IsIdle {
            get { return this.isIdle; }
            set {
                if (this.isIdle != value) {
                    this.isIdle = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DataItem() { }
        public DataItem(
            int index) {
            this.Index = index;
        }
        public DataItem(
            string filename) {
            this.Filename = filename;
            if (!string.IsNullOrEmpty(this.Filename)) this.Name = Path.GetFileNameWithoutExtension(this.Filename);
            this.IsIdle = true;
        }

        public void Clone(
            DataItem source) {
            if (source is DataItem) {
                this.Name = source.Name;
                this.Filename = source.Filename;
            }
            else {
                this.Name = string.Empty;
                this.Filename = string.Empty;
            }
            this.IsIdle = true;
        }

        public void Reset() {
            this.IsIdle = true;
        }

        private void buildToString() { this.toString = string.Format("{0}: {1}", this.index.ToString("00"), this.Name); }

        public override string ToString() { return this.toString; }

        #endregion


        #region Events.Outgoing

        public event PropertyChangedEventHandler PropertyChanged;
        protected void on_PropertyChanged([CallerMemberName] string callerName = "") { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }
        protected void on_PropertyChanged(PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, e); }
        protected void on_PropertyChanged(object sender, PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(sender, this.PropertyChanged, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }

    public class DatasetContent : Messaging.Message, INotifyPropertyChanged {

        #region Properties

        private string name = string.Empty;
        public string Name {
            get { return this.name; }
            set {
                if (this.name != value) {
                    if (string.IsNullOrEmpty(value)) this.name = string.Empty;
                    else this.name = value;
                    this.buildToString();
                    this.on_PropertyChanged();
                }
            }
        }

        private string backgroundFilename = string.Empty;
        public string BackgroundFilename {
            get { return this.backgroundFilename; }
            set {
                if (this.backgroundFilename != value) {
                    if (value == null) value = string.Empty;
                    this.backgroundFilename = value;
                    this.Background = Helper.getThumbnailFromMediaFile(value);
                    this.on_PropertyChanged();
                }
            }
        }

        [XmlIgnore]
        public Image Background { get; private set; }

        private List<DataItem> itemList = new List<DataItem>();
        public DataItem[] ItemList {
            get { return this.itemList.ToArray(); }
            set {
                this.buildItemList();
                int index = 0;
                foreach(DataItem item in this.itemList) {
                    if (value is DataItem[] &&
                        value.Length >= index) item.Clone(value[index]);
                    else item.Clone(null);
                    index++;
                }
            }
        }

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DatasetContent() {
            this.buildItemList();
        }
        public DatasetContent(
            string backgroundFilename) {
            if (string.IsNullOrEmpty(backgroundFilename)) {
                this.Name = "?";
                this.BackgroundFilename = string.Empty;
            }
            else {
                this.Name = Path.GetFileNameWithoutExtension(backgroundFilename);
                this.BackgroundFilename = backgroundFilename;
            }
            this.buildItemList();
            string directory = Path.GetDirectoryName(backgroundFilename);
            string extension = Path.GetExtension(backgroundFilename);
            foreach(DataItem item in this.itemList) {
                string filename = Path.Combine(directory, string.Format("{0}_{1}{2}", this.Name, item.ID.ToString("00"), extension));
                if (File.Exists(filename)) item.Clone(new DataItem(filename));
                else item.Clone(null);
            }
        }

        private void buildItemList() {
            while (itemList.Count < Business.ITEMS_COUNT) {
                DataItem item = new DataItem(itemList.Count);
                item.PropertyChanged += this.on_PropertyChanged;
                itemList.Add(item);
            }
        }

        public void Clone(
            DatasetContent source) {
            if (source is DatasetContent) {
                this.Name = source.Name;
                this.BackgroundFilename = source.BackgroundFilename;
                this.ItemList = source.ItemList;
            }
            else {
                this.Name = string.Empty;
                this.BackgroundFilename = string.Empty;
                this.ItemList = null;
            }
        }

        public DataItem GetItem(
            int index) {
            if (index >= 0 &&
                index < this.itemList.Count) return this.itemList[index];
            else return null;
        }

        public void ResetItems() {
            foreach (DataItem item in this.itemList) item.Reset();
        }

        private void buildToString() { this.toString = this.Name; }

        public override string ToString() { return this.toString; }

        #endregion


        #region Events.Outgoing

        public event PropertyChangedEventHandler PropertyChanged;
        protected void on_PropertyChanged([CallerMemberName] string callerName = "") { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }
        protected void on_PropertyChanged(PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, e); }
        protected void on_PropertyChanged(object sender, PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(sender, this.PropertyChanged, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }

    public class Business : _Base.TimerScore.Business {

        public const int ITEMS_COUNT = 12;

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

        private bool repressPropertyChanged = false;

        #endregion


        #region Funktionen

        public Business() { }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.TwelveIssuesTimerScore'", typeIdentifier);
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

        internal void ResetDataset() {
            if (this.SelectedDataset is DatasetContent) this.SelectedDataset.ResetItems();
        }

        public void ToggleItem(
            int index) {
            if (this.SelectedDataset is DatasetContent) {
                DataItem item = this.SelectedDataset.GetItem(index);
                if (item is DataItem) {
                    item.IsIdle = !item.IsIdle;
                    this.Vfullscreen_SetContent();
                    this.Vhost_SetContent();
                }
            }
        }

        public void ItemOut(
            int index,
            bool wrong) {
            if (this.SelectedDataset is DatasetContent) {
                DataItem item = this.SelectedDataset.GetItem(index);
                if (item is DataItem &&
                    item.IsIdle) {
                    this.Vfullscreen_ItemOut(index + 1);
                    if (wrong) this.fullscreenScene.PlayJingleWrong();
                    else this.fullscreenScene.PlayJingleSelect();
                    item.IsIdle = false;
                    this.Vhost_SetContent();
                }
            }
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

        public override void Vinsert_TimerIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_TimerIn(this.insertScene.Timer); }
        public override void Vinsert_StartTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_StartTimer(this.insertScene.Timer); }
        public override void Vinsert_StopTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_StopTimer(this.insertScene.Timer); }
        public override void Vinsert_TimerOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_TimerOut(this.insertScene.Timer); }

        public override void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score); }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }

        public void Vinsert_BorderIn() {
            this.Vinsert_SetBorder();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Border.ToIn();
        }
        public void Vinsert_SetBorder() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.Border.SetPositionX(this.BorderPositionX);
                this.insertScene.Border.SetPositionY(this.BorderPositionY);
                this.insertScene.Border.SetScaling(this.BorderScaling);
                this.insertScene.Border.SetStyle(this.BorderStyle);
                this.insertScene.Border.SetLeftName(this.LeftPlayerName);
                this.insertScene.Border.SetLeftScore(this.LeftPlayerScore);
                this.insertScene.Border.SetRightName(this.RightPlayerName);
                this.insertScene.Border.SetRightScore(this.RightPlayerScore);
            }
        }
        public void Vinsert_BorderOut() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Border.ToOut(); }

        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void Vfullscreen_LoadScene() {
            base.Vfullscreen_LoadScene();
            this.fullscreenScene.Load();
        }
        internal void Vfullscreen_ContentIn() {
            this.Vfullscreen_SetContent();
            if (this.fullscreenScene is VRemote4.HandlerSi.Scene)this.fullscreenScene.ToIn();
        }
        internal void Vfullscreen_SetContent() {
            this.Vfullscreen_SetContent(this.fullscreenScene, this.SelectedDataset);
        }
        internal void Vfullscreen_SetContent(
            Fullscreen scene,
            DatasetContent dataset) {
            if (scene is Fullscreen &&
                dataset is DatasetContent) {
                scene.SetBackgroundFilename(dataset.BackgroundFilename);
                for (int i = 0; i < Business.ITEMS_COUNT; i++) {
                    DataItem item = dataset.GetItem(i);
                    if (item is DataItem &&
                        item.FileExists) {
                        scene.SetIssueFilename(i + 1, item.Filename);
                        if (item.IsIdle) scene.SetIssueIn(i + 1);
                        else scene.SetIssueOut(i + 1);
                    }
                    else {
                        scene.SetIssueFilename(i + 1, string.Empty);
                        scene.SetIssueOut(i + 1);
                    }
                }
            }
        }
        internal void Vfullscreen_ResolveContent() {
            if (this.fullscreenScene is VRemote4.HandlerSi.Scene) {
                for (int i = 1; i <= Business.ITEMS_COUNT; i++) this.fullscreenScene.IssueToOut(i);
            }
        }
        internal void Vfullscreen_ItemOut(
            int id) {
            if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.IssueToOut(id);
        }
        internal void Vfullscreen_ContentOut() {
            if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.ToOut();
        }
        public override void Vfullscreen_UnloadScene() {
            base.Vfullscreen_UnloadScene();
            this.fullscreenScene.Unload();
        }

        public override void Vhost_LoadScene() {
            base.Vhost_LoadScene();
            this.hostScene.Load(); 
        }
        internal void Vhost_ContentIn() {
            this.Vhost_SetContent();
            if (this.hostScene is VRemote4.HandlerSi.Scene) this.hostScene.ToIn();
        }
        internal void Vhost_SetContent() {
            this.Vhost_SetContent(this.hostScene, this.SelectedDataset);
        }
        internal void Vhost_SetContent(
            Host scene,
            DatasetContent dataset) {
            if (scene is Host &&
                dataset is DatasetContent) {
                for (int i = 0; i < Business.ITEMS_COUNT; i++) {
                    DataItem item = dataset.GetItem(i);
                    if (item is DataItem &&
                        item.FileExists) {
                        scene.SetIssueText(i + 1, item.Name);
                        if (item.IsIdle) scene.SetIssueIn(i + 1);
                        else scene.SetIssueOut(i + 1);
                    }
                    else {
                        scene.SetIssueText(i + 1, string.Empty);
                        scene.SetIssueOut(i + 1);
                    }
                }
            }
        }
        internal void Vhost_ContentOut() {
            if (this.hostScene is VRemote4.HandlerSi.Scene) this.hostScene.ToOut();
        }
        public override void Vhost_UnloadScene() {
            base.Vhost_UnloadScene();
            this.hostScene.Unload(); 
        }

        public override void Vstage_Init() {
            base.Vstage_Init();
            if (this.hostMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostMasterScene.LiveVideoIn();
            if (this.leftPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerMasterScene.LiveVideoIn();
            if (this.rightPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerMasterScene.LiveVideoIn();
        }

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
