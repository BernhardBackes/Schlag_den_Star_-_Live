using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.AnswerListTimerScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.AnswerListTimerScore {

    public class Data {
        public DatasetContent[] DataList;
        public int SelectedIndex;
    }

    public class DatasetContent : Messaging.Message, INotifyPropertyChanged {

        #region Properties

        private SynchronizationContext syncContext;

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

        private List<string> itemList = new List<string>();
        public string[] ItemList {
            get { return this.itemList.ToArray(); }
            set {
                this.itemList.Clear();
                if (value is string[]) foreach (string item in value) this.itemList.Add(item);
                this.on_PropertyChanged("ItemList");
            }
        }

        public int ItemsCount { get { return this.itemList.Count; } }

        #endregion


        #region Funktionen

        public DatasetContent() {}
        public DatasetContent(
            SynchronizationContext syncContext) {
            this.syncContext = syncContext;
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
                this.ItemList = new string[0];
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
        #endregion

    }

    public class Business : _Base.TimerScore.Business {

        #region Properties

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

        private List<string> idleItems = new List<string>();
        public string[] IdleItems { get { return this.idleItems.ToArray(); } }

        private List<string> selectedItems = new List<string>();
        public string[] SelectedItems { get { return this.selectedItems.ToArray(); } }

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

            this.ClassInfo = string.Format("'{0}' of 'Templates.AnswerListTimerScore'", typeIdentifier);
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
            this.idleItems.Clear();
            if (this.SelectedDataset is DatasetContent) {
                foreach (string item in this.SelectedDataset.ItemList) this.idleItems.Add(item);
            }
            this.selectedItems.Clear();
            this.on_PropertyChanged("IdleItems");
            this.on_PropertyChanged("SelectedItems");
        }

        public void ExecuteSelection(
            string item) {
            if (!string.IsNullOrEmpty(item)) {
                if (this.idleItems.Contains(item)) {
                    this.idleItems.Remove(item);
                    this.on_PropertyChanged("IdleItems");
                }
                if (!this.selectedItems.Contains(item)) {
                    this.selectedItems.Add(item);
                    this.on_PropertyChanged("SelectedItems");
                    this.Vinsert_SetContent();
                    if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.AnswerList.PlayJingle("Select");
                    this.Vstage_SetContent();
                }
            }
        }

        public void TakeSelectionBack(
            string item) {
            if (!string.IsNullOrEmpty(item)) {
                if (this.selectedItems.Contains(item)) {
                    this.selectedItems.Remove(item);
                    this.on_PropertyChanged("SelectedItems");
                    if (!this.idleItems.Contains(item)) {
                        this.idleItems.Add(item);
                        this.on_PropertyChanged("IdleItems");
                    }
                    this.Vinsert_SetContent();
                    this.Vstage_SetContent();
                }
            }
        }

        public void Wrong() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.AnswerList.PlayJingle("Wrong");
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
                System.IO.StreamReader file = null;
                try {
                    string line;
                    int lineCounter = 0;
                    DatasetContent newDataset = null;
                    List<string> itemList = new List<string>();
                    file = new System.IO.StreamReader(filename, Encoding.UTF8);
                    while ((line = file.ReadLine()) != null) {
                        if (string.IsNullOrEmpty(line)) {
                            if (newDataset is DatasetContent) {
                                newDataset.ItemList = itemList.ToArray();
                                this.tryAddDataset(newDataset, -1);
                            }
                            newDataset = null;
                            lineCounter = 0;
                        }
                        else {
                            if (newDataset == null) {
                                newDataset = new DatasetContent();
                                itemList.Clear();
                            }
                            lineCounter++;
                            if (lineCounter == 1) newDataset.HostText = line;
                            else if (lineCounter == 2) newDataset.Name = line;
                            else itemList.Add(Helper.removeTextInBrackets(line));
                        }
                    }
                    if (newDataset is DatasetContent) {
                        newDataset.ItemList = itemList.ToArray();
                        this.tryAddDataset(newDataset, -1);
                    }
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

        private int patchValue(
            int itemsCount,
            int id) {
            int value = id;
            int threshold = (itemsCount + 1) / 2;
            if (id <= threshold) value = id * 2 - 1;
            else value = (id - threshold) * 2;
            return value;
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
        public override void Vinsert_TimerIn() {
            //this.Vinsert_SetTimer();
            //this.Vinsert_ResetTimer();
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
        public void Vinsert_ContentIn() {
            this.Vinsert_SetContent();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.AnswerList.ToIn();
        }
        public void Vinsert_SetContent() {
            this.Vinsert_SetContent(this.insertScene.AnswerList, this.SelectedDataset, this.SelectedItems);
        }
        public void Vinsert_SetContent(
            AnswerList scene,
            DatasetContent content,
            string[] selectedItems)
        {
            if (scene is AnswerList &&
                content is DatasetContent)
            {
                scene.SetHeadline(content.Name);
                scene.SetPanelsCount(content.ItemsCount);
                int counter = 1;
                foreach (string item in content.ItemList)
                {
                    int patchValue = this.patchValue(content.ItemsCount, counter);
                    scene.SetPanelText(patchValue, item);
                    if (selectedItems.Contains(item)) scene.PanelToYellow(patchValue);
                    else scene.SetPanelIn(patchValue);
                    counter++;
                }
            }
        }
        public void Vinsert_ResolveAll() {
            this.Vinsert_TimerOut();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.AnswerList.AllPanelsToGrey();
        }
        public void Vinsert_ContentOut() { 
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.AnswerList.ToOut();
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
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.hostScene.SetIn();
                this.hostScene.SetAllPanelsGrey();
            }
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.ToIn();
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.ToIn();
        }
        public void Vstage_SetContent()
        {
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vstage_SetContent(this.hostScene, this.SelectedDataset, this.SelectedItems, true);
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vstage_SetContent(this.leftPlayerScene, this.SelectedDataset, this.SelectedItems);
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vstage_SetContent(this.rightPlayerScene, this.SelectedDataset, this.SelectedItems);
        }
        public void Vstage_SetContent(
            Stage stageScene,
            DatasetContent content,
            string[] selectedItems,
            bool isHost = false)
        {
            if (content is DatasetContent)
            {
                if (isHost)
                {
                    stageScene.SetHostText(content.HostText);
                }
                else
                {
                    stageScene.SetHeadline(content.Name);
                }
                stageScene.SetPanelsCount(content.ItemsCount);
                int counter = 1;
                foreach (string item in content.ItemList)
                {
                    stageScene.SetPanelText(counter, item);
                    if (selectedItems.Contains(item))
                    {
                        if (isHost) stageScene.SetPanelYellow(counter);
                        else stageScene.PanelToYellow(counter);
                    }
                    else
                    {
                        if (isHost) stageScene.SetPanelGrey(counter);
                        else stageScene.SetPanelIn(counter);
                    }
                    counter++;
                }
            }
        }
        public void Vstage_Select()
        {
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vstage_SetContent(this.hostScene, this.SelectedDataset, this.SelectedItems, true);
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vstage_SetContent(this.leftPlayerScene, this.SelectedDataset, this.SelectedItems);
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vstage_SetContent(this.rightPlayerScene, this.SelectedDataset, this.SelectedItems);
        }
        public void Vstage_ShowResolved() {
            //if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene, true);
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.AllPanelsToGrey();
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.AllPanelsToGrey();
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
