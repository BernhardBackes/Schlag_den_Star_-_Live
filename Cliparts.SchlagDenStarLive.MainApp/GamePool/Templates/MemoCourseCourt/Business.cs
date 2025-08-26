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

using Cliparts.Serialization;

using Cliparts.Tools.Base;

using Cliparts.Tools.NetContact;

using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MemoCourseCourt;
using System.Xml.Serialization;
using System.Reflection;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.MemoCourseCourt {

    public class Data {
        public DatasetContent[] DataList;
        public int SelectedIndex;
    }

    public class DatasetItem : INotifyPropertyChanged {

        #region Properties

        private string text = string.Empty;
        public string Text {
            get { return this.text; }
            set {
                if (this.text != value) {
                    if (string.IsNullOrEmpty(value)) value = "?";
                    else this.text = value;
                    this.buildToString();
                }
            }
        }

        private string filename = string.Empty;
        public string Filename {
            get { return this.filename; }
            set {
                if (this.filename != value) {
                    if (string.IsNullOrEmpty(value)) this.filename = string.Empty;
                    else this.filename = value;
                    this.Image = Helper.getThumbnailFromMediaFile(value);
                    this.on_PropertyChanged();
                }
            }
        }

        [XmlIgnore]
        public Image Image { get; private set; }


        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DatasetItem() { }
        public DatasetItem(
            string filename) {
            this.Text = Path.GetFileNameWithoutExtension(filename);
            this.Filename = filename;
        }

        public void Clone(
            DatasetItem source) {
            if (source is DatasetItem) {
                this.Text = source.Text;
                this.Filename = source.Filename;
            }
            else {
                this.Text = string.Empty;
                this.Filename = string.Empty;
            }
        }

        private void buildToString() { this.toString = this.Text; }

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
                    if (string.IsNullOrEmpty(value)) value = "?";
                    else this.name = value;
                    this.buildToString();
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

        public DatasetItem LastItem {
            get {
                if (this.itemList.Count > 0) return this.itemList.Last();
                else return null;
            }
        }

        public int ItemsCount { get { return this.itemList.Count; } }

        public bool InvalidSize { get { return this.itemList.Count != this.Name.Length; } }

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DatasetContent() { }
        public DatasetContent(
            string folder) {
            if (string.IsNullOrEmpty(folder) ||
                !Directory.Exists(folder)) this.Name = "?";
            else {
                DirectoryInfo folderInfo = new DirectoryInfo(folder);
                this.Name = folderInfo.Name;
                List<string> files = new List<string>(Directory.GetFiles(folder, "*.png"));
                foreach (string item in files) this.tryAddItem(new DatasetItem(item));
            }
            this.buildToString();
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
                newItem.PropertyChanged += this.on_PropertyChanged;
                this.itemList.Add(newItem);
                return true;
            }
            else return false;
        }

        public bool TryMoveItemUp(
            int index) {
            if (index > 0 &&
                index < this.ItemsCount) {
                DatasetItem item = this.GetItem(index);
                this.itemList.RemoveAt(index);
                this.itemList.Insert(index - 1, item);
                this.on_PropertyChanged("ItemList");
                return true;
            }
            else return false;
        }
        public bool TryMoveItemDown(
            int index) {
            if (index >= 0 &&
                index < this.ItemsCount - 1) {
                DatasetItem item = this.GetItem(index);
                this.itemList.RemoveAt(index);
                this.itemList.Insert(index + 1, item);
                this.on_PropertyChanged("ItemList");
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
                return true;
            }
            else return false;
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

    public class Business : _Base.Business {

        #region Properties

        private int counterPositionX = 0;
        public int CounterPositionX {
            get { return this.counterPositionX; }
            set {
                if (this.counterPositionX != value) {
                    this.counterPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounter();
                }
            }
        }

        private int counterPositionY = 0;
        public int CounterPositionY {
            get { return this.counterPositionY; }
            set {
                if (this.counterPositionY != value) {
                    this.counterPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounter();
                }
            }
        }

        private int coursePositionX = 0;
        public int CoursePositionX {
            get { return this.coursePositionX; }
            set {
                if (this.coursePositionX != value) {
                    this.coursePositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCourse();
                }
            }
        }

        private int coursePositionY = -375;
        public int CoursePositionY {
            get { return this.coursePositionY; }
            set {
                if (this.coursePositionY != value) {
                    this.coursePositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCourse();
                }
            }
        }

        private double courseScaling = 20;
        public double CourseScaling {
            get { return this.courseScaling; }
            set {
                if (this.courseScaling != value) {
                    if (value < 10) this.courseScaling = 10;
                    else if (value > 100) this.courseScaling = 100;
                    else this.courseScaling = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCourse();
                }
            }
        }

        private int timerPositionX = 0;
        public int TimerPositionX {
            get { return this.timerPositionX; }
            set {
                if (this.timerPositionX != value) {
                    this.timerPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
                }
            }
        }

        private int timerPositionY = 0;
        public int TimerPositionY {
            get { return this.timerPositionY; }
            set {
                if (this.timerPositionY != value) {
                    this.timerPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
                }
            }
        }

        private int timerStartTime = 300;
        public int TimerStartTime {
            get { return this.timerStartTime; }
            set {
                if (this.timerStartTime != value) {
                    this.timerStartTime = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
                    this.Vfullscreen_SetCountdown();
                }
            }
        }

        private int timerAlarmTime1 = 10;
        public int TimerAlarmTime1 {
            get { return this.timerAlarmTime1; }
            set {
                if (this.timerAlarmTime1 != value) {
                    this.timerAlarmTime1 = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
                    this.Vfullscreen_SetCountdown();
                }
            }
        }

        private int timerAlarmTime2 = -1;
        public int TimerAlarmTime2 {
            get { return this.timerAlarmTime2; }
            set {
                if (this.timerAlarmTime2 != value) {
                    this.timerAlarmTime2 = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
                    this.Vfullscreen_SetCountdown();
                }
            }
        }

        private int timerStopTime = 0;
        public int TimerStopTime {
            get { return this.timerStopTime; }
            set {
                if (this.timerStopTime != value) {
                    this.timerStopTime = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
                    this.Vfullscreen_SetCountdown();
                }
            }
        }

        private int timerCurrentTime = -1;
        public int TimerCurrentTime {
            get { return this.timerCurrentTime; }
            protected set {
                if (this.timerCurrentTime != value) {
                    this.timerCurrentTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool timerIsRunning = false;
        public bool TimerIsRunning {
            get { return this.timerIsRunning; }
            protected set {
                if (this.timerIsRunning != value) {
                    this.timerIsRunning = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private PlayerSelection selectedPlayer = PlayerSelection.LeftPlayer;
        public PlayerSelection SelectedPlayer {
            get { return this.selectedPlayer; }
            set {
                if (this.selectedPlayer != value) {
                    if (value == PlayerSelection.NotSelected) value = PlayerSelection.LeftPlayer;
                    this.selectedPlayer = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string sequence = "?????";
        [NotSerialized]
        public string Sequence {
            get { return this.sequence; }
            set {
                if (this.sequence != value) {
                    if (string.IsNullOrEmpty(value)) value = string.Empty;
                    this.sequence = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCourse();
                    this.Vfullscreen_SetCourse();
                }
            }
        }

        private bool sequenceIsChallenge { get { return this.Sequence.Length >= 5; } }

        private int sequenceProgress = 0;
        [NotSerialized]
        public int SequenceProgress {
            get { return this.sequenceProgress; }
            set {
                if (this.sequenceProgress != value) {
                    this.sequenceProgress = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCourse();
                }
            }
        }

        private bool sequenceIsStopped = false;
        public bool SequenceIsStopped {
            get { return this.sequenceIsStopped; }
            protected set {
                if (this.sequenceIsStopped != value) {
                    this.sequenceIsStopped = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int counter = 0;
        public int Counter {
            get { return this.counter; }
            set {
                if (this.counter != value) {
                    this.counter = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounter();
                }
            }
        }

        private int time = 0;
        public int Time {
            get { return this.time; }
            set {
                if (this.time != value) {
                    this.time = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int referenceCounter = 0;
        public int ReferenceCounter {
            get { return this.referenceCounter; }
            set {
                if (this.referenceCounter != value) {
                    this.referenceCounter = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounter();
                }
            }
        }

        private int referenceTime = 0;
        public int ReferenceTime {
            get { return this.referenceTime; }
            set {
                if (this.referenceTime != value) {
                    this.referenceTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool showReference = false;
        public bool ShowReference {
            get { return this.showReference; }
            set {
                if (this.showReference != value) {
                    this.showReference = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounter();
                }
            }
        }

        private TCPServer speedcourtServer;

        private string speedcourtClientName = string.Empty;
        public string SpeedcourtClientName {
            get { return this.speedcourtClientName; }
            private set {
                if (this.speedcourtClientName != value) {
                    this.speedcourtClientName = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool speedcourtServerIsRunning = false;
        public bool SpeedcourtServerIsRunning {
            get { return this.speedcourtServerIsRunning; }
            private set {
                if (this.speedcourtServerIsRunning != value) {
                    this.speedcourtServerIsRunning = value;
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
            }
        }

        private List<string> names = new List<string>();
        public string[] NameList { get { return this.names.ToArray(); } }

        public int DatasetsCount { get { return this.dataList.Count; } }

        private List<string> taskNameList = new List<string>();
        public string[] TaskNameList { get { return this.taskNameList.ToArray(); } }

        private string selectedTask = string.Empty;
        public string SelectedTask {
            get { return this.selectedTask; }
            private set {
                if (this.selectedTask != value) {
                    if (string.IsNullOrEmpty(value)) this.selectedTask = string.Empty;
                    else this.selectedTask = value;
                    this.on_PropertyChanged();
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

        private bool repressPropertyChanged = false;

        #endregion


        #region Funktionen

        public Business() { }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.MemoCourseCourt'", typeIdentifier);
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

            this.speedcourtServer = new TCPServer(61891);
            this.speedcourtServer.DataReceived += this.speedcourtServer_DataReceived;
            this.speedcourtServer.Changed += this.speedcourtServer_Changed;

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;
            this.insertScene.Timer.PropertyChanged += this.insertScene_Timer_PropertyChanged;
            this.insertScene.Timer.Alarm1Fired += this.insertScene_timer_Alarm1Fired;
            this.insertScene.Timer.Alarm2Fired += this.insertScene_timer_Alarm2Fired;
            this.insertScene.Timer.StopFired += this.insertScene_timer_StopFired;

            this.fullscreenScene = new Fullscreen(syncContext, fullscreenMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.fullscreenScene.StatusChanged += this.fullscreenScene_StatusChanged;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);
        }

        public override void Dispose() {
            this.speedcourtServer.DataReceived -= this.speedcourtServer_DataReceived;
            this.speedcourtServer.Changed -= this.speedcourtServer_Changed;
            this.speedcourtServer.StopServer();

            base.Dispose();

            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Timer.PropertyChanged -= this.insertScene_Timer_PropertyChanged;
            this.insertScene.Timer.Alarm1Fired -= this.insertScene_timer_Alarm1Fired;
            this.insertScene.Timer.Alarm2Fired -= this.insertScene_timer_Alarm2Fired;
            this.insertScene.Timer.StopFired -= this.insertScene_timer_StopFired;

            this.fullscreenScene.Dispose();
            this.fullscreenScene.StatusChanged += this.fullscreenScene_StatusChanged;
        }

        public override void ResetData() {
            base.ResetData();
            this.ReferenceCounter = 0;
            this.ReferenceTime = 0;
            this.ShowReference = false;
            this.Counter = 0;
            this.Time = 0;
            this.SequenceProgress = 0;
            this.SequenceIsStopped = false;
        }

        public void NextPlayer() {
            if (this.SelectedPlayer == PlayerSelection.LeftPlayer) this.SelectedPlayer = PlayerSelection.RightPlayer;
            else this.SelectedPlayer = PlayerSelection.LeftPlayer;
            this.ReferenceTime = this.Time;
            this.Time = 0;
            this.ReferenceCounter = this.Counter;
            this.Counter = 0;
            this.ShowReference = true;
            this.SequenceProgress = 0;
            this.SequenceIsStopped = false;
        }

        public void ToggleServerIsRunning() {
            if (this.speedcourtServer.IsRunning) this.speedcourtServer.StopServer();
            else this.speedcourtServer.StartServer();
        }

        public void ShowServer() { this.speedcourtServer.ShowForm(); }

        public void SelectTask(
            string name) {
            this.sendToClients("SelectTask\t" + name);
        }
        public void StartTask() { this.sendToClients("StartTask"); }
        public void StopTask() { this.sendToClients("StopTask"); }
        public void ResetTask() { this.sendToClients("ResetTask"); }

        private void sendToClients(
            string sendText) {
            sendText = NetControlCharacter.STX.ToString() + sendText + NetControlCharacter.EOT.ToString();
            byte[] data = Encoding.ASCII.GetBytes(sendText);
            List<WorkerSocket> hostList = this.speedcourtServer.HostList;
            foreach (WorkerSocket item in hostList) this.speedcourtServer.SendData(item.Address, data);
        }

        private void parseReceivedData(
            string recText) {
            if (!string.IsNullOrEmpty(recText) &&
                recText.StartsWith(NetControlCharacter.STX.ToString()) &&
                recText.EndsWith(NetControlCharacter.EOT.ToString())) {
                string[] messageArray = recText.Split(new char[] { NetControlCharacter.STX, NetControlCharacter.EOT }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in messageArray) {
                    string[] messageContent = item.Split(new char[] { NetControlCharacter.HT }, StringSplitOptions.RemoveEmptyEntries);
                    if (messageContent.Length > 0) {
                        if (messageContent[0] == "TaskList") {
                            this.taskNameList = new List<string>();
                            for (int i = 1; i < messageContent.Length; i++) {
                                if (!string.IsNullOrEmpty(messageContent[i])) this.taskNameList.Add(messageContent[i]);
                            }
                            this.on_PropertyChanged("TaskNameList");
                        }
                        else if (messageContent[0] == "Task" &&
                            messageContent.Length >= 1 &&
                            !string.IsNullOrEmpty(messageContent[1])) {
                            this.SelectedTask = messageContent[1];
                        }
                        else if (!this.SequenceIsStopped) {
                            if (messageContent[0] == "Richtig") {
                                this.Vinsert_PlayJingleGood();
                                this.Vfullscreen_CourseOut();
                                this.SequenceProgress++;
                            }
                            else if (messageContent[0] == "Falsch") {
                                this.Vinsert_PlayJingleBad();
                                this.Vfullscreen_CourseIn();
                                this.SequenceProgress = 0;
                            }
                            else if (messageContent[0] == "Punkt") {
                                this.Vinsert_PlayJingleScore();
                                if (this.sequenceIsChallenge) {
                                    this.Counter++;
                                    this.Time = this.TimerCurrentTime;
                                    this.SequenceProgress++;
                                }
                                else this.SequenceProgress++;
                            }
                            else if (messageContent[0] == "Sequence" &&
                                messageContent.Length >= 1 &&
                                !string.IsNullOrEmpty(messageContent[1]) &&
                                messageContent[1].Length >= 3) {
                                this.Sequence = messageContent[1];
                                this.Vfullscreen_CourseIn();
                                this.SequenceProgress = 0;
                                this.Vinsert_CourseIn();
                                if (this.sequenceIsChallenge &&
                                    !this.TimerIsRunning) {
                                    this.Vfullscreen_StartCountdown();
                                    this.Vinsert_StartTimer();
                                }
                            }

                        }
                    }
                }
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

        private DatasetContent getDataset(
            string name) {
            int index = this.names.IndexOf(name);
            if (index >= 0) return this.dataList[index];
            else return null;
        }

        public void AddDataset(
            DatasetContent newDataset,
            int insertIndex) {
            if (this.tryAddDataset(newDataset, insertIndex)) {
                this.Save();
                this.on_PropertyChanged("NameList");
            }
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
                this.Save();
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
                this.Save();
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                return true;
            }
            else return false;
        }

        public void RemoveAllDatasets() {
            if (this.tryRemoveAllDatasets()) {
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                this.Save();
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
                this.Save();
                this.buildNameList();
                this.on_PropertyChanged("NameList");
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
        public void Vinsert_CounterIn() {
            this.Vinsert_SetCounter();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.CounterToIn();
        }
        public void Vinsert_SetCounter() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.SetCounterPositionX(this.CounterPositionX);
                this.insertScene.SetCounterPositionY(this.CounterPositionY);
                this.insertScene.SetCounterValue(this.Counter);
                this.insertScene.SetCounterReference(this.ReferenceCounter);
                this.insertScene.SetCounterShowReference(this.ShowReference);
            }
        }
        public void Vinsert_CounterOut() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.CounterToOut(); }
        public void Vinsert_CourseIn() {
            this.Vinsert_SetCourse();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.CourseToIn();
        }
        public void Vinsert_SetCourse() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.SetCoursePositionX(this.CoursePositionX);
                this.insertScene.SetCoursePositionY(this.CoursePositionY);
                this.insertScene.SetCourseScaling(this.CourseScaling);
                DatasetContent content = this.getDataset(this.Sequence);
                if (content is DatasetContent) {
                    //DatasetItem item = null;
                    //if (this.sequenceProgress == 0) item = content.LastItem;
                    //else item = content.GetItem(this.sequenceProgress - 1);
                    //if (item is DatasetItem) this.insertScene.SetCourseFilename(item.Filename);
                    //else this.insertScene.SetCourseFilename(string.Empty);
                    this.insertScene.SetCourseFilename(content.LastItem.Filename);
                }
                else this.insertScene.SetCourseFilename(string.Empty);
            }
        }
        public void Vinsert_CourseOut() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.CourseToOut(); }
        public void Vinsert_TimerIn() {
            this.Vinsert_SetTimer();
            this.Vinsert_ResetTimer();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.ToIn();
        }
        public void Vinsert_SetTimer() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.Timer.SetPositionX(this.TimerPositionX);
                this.insertScene.Timer.SetPositionY(this.TimerPositionY);
                this.insertScene.Timer.SetStyle(VentuzScenes.GamePool._Modules.Timer.Styles.MinSec);
                this.insertScene.Timer.SetScaling(100);
                this.insertScene.Timer.SetStartTime(this.TimerStartTime);
                this.insertScene.Timer.SetStopTime(this.TimerStopTime);
                this.insertScene.Timer.SetAlarmTime1(this.TimerAlarmTime1);
                this.insertScene.Timer.SetAlarmTime2(this.TimerAlarmTime2);
            }
        }
        public void Vinsert_StartTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available)   this.insertScene.Timer.StartTimer(); }
        public void Vinsert_StopTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available)                 this.insertScene.Timer.StopTimer(); }
        public void Vinsert_ContinueTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available)                 this.insertScene.Timer.ContinueTimer(); }
        public void Vinsert_ResetTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available)                 this.insertScene.Timer.ResetTimer(); }
        public void Vinsert_TimerOut() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available)                this.insertScene.Timer.ToOut(); }
        internal void Vinsert_PlayJingleBad() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.PlayJingleBad(); }
        internal void Vinsert_PlayJingleEnd() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.PlayJingleEnd(); }
        internal void Vinsert_PlayJingleGood() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.PlayJingleGood(); }
        internal void Vinsert_PlayJingleScore() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.PlayJingleScore(); }
        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void Vfullscreen_LoadScene() {
            base.Vfullscreen_LoadScene();
            this.fullscreenScene.Load();
        }
        public void Vfullscreen_CountdownIn() {
            this.Vfullscreen_SetCountdown();
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.CountdownToIn();
        }
        public void Vfullscreen_SetCountdown() {
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.fullscreenScene.SetCountdownStartTime(this.TimerStartTime);
                this.fullscreenScene.SetCountdownAlarmTime(this.TimerAlarmTime1);
            }
        }
        public void Vfullscreen_StartCountdown() { if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.StartCountdown(); }
        public void Vfullscreen_StopCountdown() { if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.StopCountdown(); }
        public void Vfullscreen_ResetCountdown() { if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.ResetCountdown(); }
        public void Vfullscreen_ContinueCountdown() { if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.ContinueCountdown(); }
        public void Vfullscreen_CountdownOut() { if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.CountdownToOut(); }
        public void Vfullscreen_CourseIn() {
            this.Vfullscreen_SetCourse();
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.CourseToIn();
        }
        public void Vfullscreen_SetCourse() {
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                DatasetContent content = this.getDataset(this.Sequence);
                if (content is DatasetContent) this.fullscreenScene.SetCourseFilename(content.LastItem.Filename);
                else this.fullscreenScene.SetCourseFilename(string.Empty);
            }
        }
        public void Vfullscreen_CourseOut() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.CourseToOut(); }
        public override void Vfullscreen_UnloadScene() {
            base.Vfullscreen_UnloadScene();
            this.fullscreenScene.Unload();
        }

        public override void ClearGraphic() {
            base.ClearGraphic();
            this.insertScene.Clear();
            this.fullscreenScene.Clear();
        }

        #endregion


        #region Events.Outgoing

        public event EventHandler TimerAlarm1Fired;
        protected void on_TimerAlarm1Fired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.TimerAlarm1Fired, e); }

        public event EventHandler TimerAlarm2Fired;
        protected void on_TimerAlarm2Fired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.TimerAlarm2Fired, e); }

        public event EventHandler TimerStopFired;
        protected void on_TimerStopFired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.TimerStopFired, e); }

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

        protected void insertScene_Timer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_insertScene_Timer_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_insertScene_Timer_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "CurrentTime") this.TimerCurrentTime = this.insertScene.Timer.CurrentTime;
                else if (e.PropertyName == "IsRunning") this.TimerIsRunning = this.insertScene.Timer.IsRunning;
            }
        }

        protected void insertScene_timer_Alarm1Fired(object sender, EventArgs e) {
            this.on_TimerAlarm1Fired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_insertScene_timer_Alarm1Fired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_insertScene_timer_Alarm1Fired(object content) {
        }

        protected void insertScene_timer_Alarm2Fired(object sender, EventArgs e) {
            this.on_TimerAlarm2Fired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_insertScene_timer_Alarm2Fired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_insertScene_timer_Alarm2Fired(object content) {
            this.SequenceIsStopped = true;
            this.StopTask();
        }

        protected void insertScene_timer_StopFired(object sender, EventArgs e) {
            this.on_TimerStopFired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_insertScene_timer_StopFired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_insertScene_timer_StopFired(object content) {
            this.SequenceIsStopped = true;
            this.StopTask();
        }

        private void speedcourtServer_Changed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_speedcourtServer_Changed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_speedcourtServer_Changed(object content) {
            this.SpeedcourtServerIsRunning = this.speedcourtServer.IsRunning;
            if (this.speedcourtServer.IsRunning &&
                this.speedcourtServer.HostCount > 0) this.SpeedcourtClientName = this.speedcourtServer.HostList[0].Name;
            else this.SpeedcourtClientName = string.Empty;
        }

        private void speedcourtServer_DataReceived(byte[] recBuffer, string recText) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_speedcourzClient_DataReceived);
            if (this.syncContext != null) this.syncContext.Post(callback, recText);
        }
        private void sync_speedcourzClient_DataReceived(object content) {
            string recText = content as string;
            this.parseReceivedData(recText);
        }

        #endregion

    }
}
