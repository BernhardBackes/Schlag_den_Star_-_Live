using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;
using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SelectTaskTextInsertTimer;
using static System.Net.Mime.MediaTypeNames;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.SelectTaskTextInsertTimer {

    public class Data {
        public DatasetContent[] DataList;
        public int SelectedIndex;
    }

    public class DatasetContent : Messaging.Message, INotifyPropertyChanged {

        #region Properties

        public int ID { get; set; }

        private string text = string.Empty;
        public string Text {
            get { return this.text; }
            set {
                if (this.text != value) {
                    if (value == null) value = string.Empty;
                    else this.text = value;
                    this.buildToString();
                    this.on_PropertyChanged();
                }
            }
        }

        private string solution = string.Empty;
        public string Solution
        {
            get { return this.solution; }
            set
            {
                if (this.solution != value)
                {
                    if (string.IsNullOrEmpty(value)) this.solution = string.Empty;
                    else this.solution = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string hostText = string.Empty;
        public string HostText
        {
            get { return this.hostText; }
            set
            {
                if (this.hostText != value)
                {
                    if (string.IsNullOrEmpty(value)) this.hostText = string.Empty;
                    else this.hostText = Regex.Replace(value, "(?<!\r)\n", "\r\n");
                    this.on_PropertyChanged();
                }
            }
        }

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DatasetContent() { }
        public DatasetContent(
            string text) {
            if (string.IsNullOrEmpty(text)) this.Text = "?";
            else this.Text = text;
        }

        private void buildToString() { this.toString = this.Text; }

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

        public const int TasksCount = 20;

        #region Properties

        private MidiHandler.Business midiHandler;

        private int textInsertPositionX = 0;
        public int TextInsertPositionX {
            get { return this.textInsertPositionX; }
            set {
                if (this.textInsertPositionX != value) {
                    this.textInsertPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetSolution();
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
                    this.Vinsert_SetSolution();
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
                    this.Vinsert_SetSolution();
                }
            }
        }

        private int taskInsertPositionX = 0;
        public int TaskInsertPositionX
        {
            get { return this.taskInsertPositionX; }
            set
            {
                if (this.taskInsertPositionX != value)
                {
                    this.taskInsertPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTasks();
                }
            }
        }

        private int taskInsertPositionY = 0;
        public int TaskInsertPositionY
        {
            get { return this.taskInsertPositionY; }
            set
            {
                if (this.taskInsertPositionY != value)
                {
                    this.taskInsertPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTasks();
                }
            }
        }

        private PlayerSelection buzzeredPlayer = PlayerSelection.NotSelected;
        public PlayerSelection BuzzeredPlayer
        {
            get { return this.buzzeredPlayer; }
            private set
            {
                if (this.buzzeredPlayer != value)
                {
                    this.buzzeredPlayer = value;
                    this.Vstage_SetBuzzer(value);
                    this.on_PropertyChanged();
                    switch (this.BuzzeredPlayer)
                    {
                        case PlayerSelection.LeftPlayer:
                            this.buzzerHandler.SetDMXChannel(this.IOUnitName, this.LeftPlayerBuzzerChannel, new byte[] { 255 });
                            break;
                        case PlayerSelection.RightPlayer:
                            this.buzzerHandler.SetDMXChannel(this.IOUnitName, this.RightPlayerBuzzerChannel, new byte[] { 255 });
                            break;
                        case PlayerSelection.NotSelected:
                        default:
                            this.buzzerHandler.SetDMXChannel(this.IOUnitName, IOnet.IOUnit.IONbuzDX.DMXStates.OFF);
                            break;
                    }
                }
            }
        }

        private int timeoutPositionX = 0;
        public int TimeoutPositionX
        {
            get { return this.timeoutPositionX; }
            set
            {
                if (this.timeoutPositionX != value)
                {
                    this.timeoutPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeout();
                }
            }
        }

        private int timeoutPositionY = 0;
        public int TimeoutPositionY
        {
            get { return this.timeoutPositionY; }
            set
            {
                if (this.timeoutPositionY != value)
                {
                    this.timeoutPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeout();
                }
            }
        }

        private bool timeoutIsVisible;
        public bool TimeoutIsVisible
        {
            get { return this.timeoutIsVisible; }
            set
            {
                if (this.timeoutIsVisible != value)
                {
                    this.timeoutIsVisible = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeout();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.Timeout.Duration timeoutDuration = VentuzScenes.GamePool._Modules.Timeout.Duration.FiveSeconds;
        public VentuzScenes.GamePool._Modules.Timeout.Duration TimeoutDuration
        {
            get { return this.timeoutDuration; }
            set
            {
                if (this.timeoutDuration != value)
                {
                    this.timeoutDuration = value;
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
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                this.SelectDataset(-1);
                this.ResetTaskLists();
            }
        }

        [Serialization.NotSerialized]
        public List<DatasetContent> AvailableTasks { get; set; } = new List<DatasetContent>();

        [Serialization.NotSerialized]
        public List<DatasetContent> UsedTasks { get; set; } = new List<DatasetContent>();

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
                    if (value < 1) value = 1;
                    this.taskCounter = value;
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

        private Stage hostScene;
        public override VRemote4.HandlerSi.Scene.States HostSceneStatus
        {
            get
            {
                if (this.hostScene is Stage) return this.hostScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private Stage leftPlayerScene;
        public override VRemote4.HandlerSi.Scene.States LeftPlayerSceneStatus
        {
            get
            {
                if (this.leftPlayerScene is Stage) return this.leftPlayerScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private Stage rightPlayerScene;
        public override VRemote4.HandlerSi.Scene.States RightPlayerSceneStatus
        {
            get
            {
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.SelectTaskTextInsertTimer'", typeIdentifier);
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

            this.midiHandler = midiHandler;

            //wenn die Datasets einen Synccontext benötigen, dann wird er hierdurch zugeordnet
            this.DataList = this.dataList.ToArray();

            this.ResetTaskLists();

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

        public void ResetTaskLists()
        {
            this.AvailableTasks.Clear();
            foreach (DatasetContent item in this.dataList) this.AvailableTasks.Add(item);
            this.on_PropertyChanged(nameof(this.AvailableTasks));
            this.UsedTasks.Clear();
            this.on_PropertyChanged(nameof(this.UsedTasks));
        }

        public override void ResetData() {
            base.ResetData();
            this.TaskCounter = 0;
            this.SelectDataset(-1);
            this.ResetTaskLists();
            this.BuzzeredPlayer = PlayerSelection.NotSelected;
        }

        public override void Next() {
            base.Next();
            //this.TaskCounter++;
            this.TaskOut(this.SelectedDataset);
            this.SelectDataset(-1);
            this.BuzzeredPlayer = PlayerSelection.NotSelected;
        }

        public override void DoBuzzer(
            PlayerSelection buzzeredPlayer)
        {
            if (this.BuzzeredPlayer == PlayerSelection.NotSelected &&
                this.BuzzeredPlayer != buzzeredPlayer)
            {
                this.BuzzeredPlayer = buzzeredPlayer;
                this.Vinsert_Buzzer(buzzeredPlayer);
            }
        }

        public override void ReleaseBuzzer()
        {
            bool[] inputMask = new bool[8];
            if (this.LeftPlayerBuzzerChannel > 0 &&
                this.LeftPlayerBuzzerChannel <= inputMask.Length) inputMask[this.LeftPlayerBuzzerChannel - 1] = true;
            if (this.RightPlayerBuzzerChannel > 0 &&
                this.RightPlayerBuzzerChannel <= inputMask.Length) inputMask[this.RightPlayerBuzzerChannel - 1] = true;
            this.buzzerHandler.SetInputMask(this.IOUnitName, inputMask);
            this.buzzerHandler.ReleaseBuzzer(this.IOUnitName, IOnet.IOUnit.IONbuz.WorkModes.BUZZER);
            this.BuzzeredPlayer = PlayerSelection.NotSelected;
        }

        public void TaskOut(
            DatasetContent value)
        {
            if (value is DatasetContent)
            {
                if (this.AvailableTasks.Contains(value)) this.AvailableTasks.Remove(value);
                if (!this.UsedTasks.Contains(value)) this.UsedTasks.Add(value);
                this.on_PropertyChanged(nameof(this.AvailableTasks));
                this.on_PropertyChanged(nameof(this.UsedTasks));
                this.SelectDataset(-1);
            }
        }

        public void TaskIn(
            DatasetContent value)
        {
            if (value is DatasetContent)
            {
                this.AvailableTasks.Clear();
                if (this.UsedTasks.Contains(value)) this.UsedTasks.Remove(value);
                foreach (var item in this.dataList)
                {
                    if (!this.UsedTasks.Contains(item)) this.AvailableTasks.Add(item);
                }
                this.on_PropertyChanged(nameof(this.AvailableTasks));
                this.on_PropertyChanged(nameof(this.UsedTasks));
                this.SelectDataset(-1);
            }
        }

        public void True() {
            //switch (this.BuzzeredPlayer) {
            //    case Content.Gameboard.PlayerSelection.LeftPlayer:
            //        this.LeftPlayerScore++;
            //        break;
            //    case Content.Gameboard.PlayerSelection.RightPlayer:
            //        this.RightPlayerScore++;
            //        break;
            //}
            this.Vinsert_TaskToTrue();
            this.Vinsert_SetScore();
            this.Vstage_SetScore();
        }

        public void False() {
            //switch (this.BuzzeredPlayer) {
            //    case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.LeftPlayer:
            //        this.RightPlayerScore++;
            //        break;
            //    case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.RightPlayer:
            //        this.LeftPlayerScore++;
            //        break;
            //}
            this.Vinsert_TaskToFalse();
            this.Vinsert_SetScore();
            this.Vstage_SetScore();
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
            if (index >= 0)
            {
                if (index >= this.dataList.Count) index = this.dataList.Count - 1;
                this.SelectedDatasetIndex = index;
                this.SelectedDataset = this.GetDataset(index);
            }
            else
            {
                this.SelectedDatasetIndex = -1;
                this.SelectedDataset = null;
            }
            this.on_PropertyChanged("SelectedDataset");
            this.on_PropertyChanged("SelectedDatasetIndex");
        }

        public void AddDataset(
            DatasetContent newDataset,
            int insertIndex) {
            if (this.tryAddDataset(newDataset, insertIndex))
            {
                this.on_PropertyChanged("NameList");
                this.TaskIn(newDataset);
            }
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
                for (int i = this.dataList.Count - 1; i >= 0; i--) dataList.Add(this.dataList[i]);
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
            this.SetTaskIDs();
        }

        private void SetTaskIDs()
        {
            int id = 1;
            foreach (DatasetContent item in this.dataList)
            {
                item.ID = id;
                id++;
            }
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

        internal void Vinsert_TasksIn()
        {
            this.Vinsert_SetTasks();
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Game.ToIn();
        }
        internal void Vinsert_SelectTask()
        {
            if (this.insertScene is VRemote4.HandlerSi.Scene && 
                this.SelectedDataset is DatasetContent &&
                this.SelectedDataset.ID >= 1 &&
                this.SelectedDataset.ID <= TasksCount)
            {
                this.insertMasterScene.Sampler.Play(VentuzScenes.Insert.Sampler.SampleElements.Select);
                this.insertScene.Game.TaskToSelected(this.SelectedDataset.ID);
            }
        }
        internal void Vinsert_TaskToTrue()
        {
            if (this.insertScene is VRemote4.HandlerSi.Scene && this.SelectedDataset is DatasetContent)
            {
                this.insertScene.Game.TaskToTrue(this.SelectedDataset.ID);
            }
        }
        internal void Vinsert_TaskToFalse()
        {
            if (this.insertScene is VRemote4.HandlerSi.Scene && this.SelectedDataset is DatasetContent)
            {
                this.insertScene.Game.TaskToFalse(this.SelectedDataset.ID);
            }
        }
        internal void Vinsert_SetTasks()
        {
            if (this.insertScene is VRemote4.HandlerSi.Scene) Vinsert_SetTasks(this.insertScene.Game, this.AvailableTasks, this.UsedTasks);
        }
        internal void Vinsert_SetTasks(
            Game scene,
            List<DatasetContent> availableTasks,
            List<DatasetContent> usedTasks)
        {
            if (scene is Game)
            {
                scene.SetPositionX(this.TaskInsertPositionX);
                scene.SetPositionY(this.TaskInsertPositionY);
                foreach (var item in availableTasks) scene.ResetTask(item.ID);
                foreach (var item in usedTasks) scene.TaskToOut(item.ID);
            }
        }
        internal void Vinsert_TasksOut()
        {
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Game.ToOut();
        }

        public override void Vinsert_TimerIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_TimerIn(this.insertScene.Timer); }
        public override void Vinsert_SetTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetTimer(this.insertScene.Timer); }
        public override void Vinsert_StartTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Timer.StartTimer(); }
        public override void Vinsert_StopTimer()
        {
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Timer.StopTimer();
            this.Vfullscreen_StopTimer();
            this.Vfullscreen_SetTimer(this.TimerCurrentTime);
        }
        public override void Vinsert_ContinueTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Timer.ContinueTimer(); }
        public override void Vinsert_ResetTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Timer.ResetTimer(); }
        public override void Vinsert_TimerOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Timer.ToOut(); }

        internal void Vinsert_SolutionIn() {
            this.Vinsert_SetSolution();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) 
            {
                this.insertMasterScene.Sampler.Play(VentuzScenes.Insert.Sampler.SampleElements.Resolve);
                this.insertScene.TextInsert.ToIn(); 
            }
        }
        internal void Vinsert_SetSolution() {
            if (this.insertScene is VRemote4.HandlerSi.Scene && this.SelectedDataset is DatasetContent) this.Vinsert_SetSolution(this.insertScene.TextInsert, this.SelectedDataset);
        }
        internal void Vinsert_SetSolution(
            VentuzScenes.GamePool._Modules.TextInsert scene,
            DatasetContent dataset) {
            if (scene is VentuzScenes.GamePool._Modules.TextInsert &&
            dataset is DatasetContent) {
                scene.SetStyle(this.TextInsertStyle);
                scene.SetPositionX(this.TextInsertPositionX);
                scene.SetPositionY(this.TextInsertPositionY);
                scene.SetText(dataset.Solution);
            }
        }
        internal void Vinsert_SolutionOut() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.TextInsert.ToOut(); 
        }

        public void Vinsert_Buzzer(PlayerSelection buzzeredPlayer) {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vinsert_Buzzer(this.insertScene.Timeout, buzzeredPlayer);
        }
        public void Vinsert_Buzzer(
            VentuzScenes.GamePool._Modules.Timeout scene,
            PlayerSelection buzzeredPlayer)
        {
            if (scene is VentuzScenes.GamePool._Modules.Timeout)
            {
                this.Vinsert_SetTimeout(scene);
                switch (buzzeredPlayer)
                {
                    case PlayerSelection.LeftPlayer:
                        if (this.TimeoutDuration == VentuzScenes.GamePool._Modules.Timeout.Duration.FiveSeconds) scene.BuzzerLeft(this.TimeoutDuration, this.TimeoutIsVisible);
                        else
                        {
                            scene.BuzzerSoundLeft();
                            scene.StartCenter(this.TimeoutDuration, this.TimeoutIsVisible);
                        }
                        break;
                    case PlayerSelection.RightPlayer:
                        if (this.TimeoutDuration == VentuzScenes.GamePool._Modules.Timeout.Duration.FiveSeconds) scene.BuzzerRight(this.TimeoutDuration, this.TimeoutIsVisible);
                        else
                        {
                            scene.BuzzerSoundRight();
                            scene.StartCenter(this.TimeoutDuration, this.TimeoutIsVisible);
                        }
                        break;
                    case PlayerSelection.NotSelected:
                    default:
                        scene.Stop();
                        break;
                }
            }
        }

        public virtual void Vinsert_SetTimeout() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vinsert_SetTimeout(this.insertScene.Timeout);
        }
        public void Vinsert_SetTimeout(
            VentuzScenes.GamePool._Modules.Timeout scene)
        {
            if (scene is VentuzScenes.GamePool._Modules.Timeout)
            {
                scene.SetPositionX(this.TimeoutPositionX);
                scene.SetPositionY(this.TimeoutPositionY);
                scene.SetIsVisible(this.TimeoutIsVisible);
            }
        }
        public void Vinsert_StopTimeout() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vinsert_StopTimeout(this.insertScene.Timeout);
        }
        public void Vinsert_StopTimeout(
            VentuzScenes.GamePool._Modules.Timeout scene)
        {
            if (scene is VentuzScenes.GamePool._Modules.Timeout) scene.Clear();
        }

        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void Vstage_Init() {
            base.Vstage_Init();
            //if (this.hostMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostMasterScene.LiveVideoIn();
            //if (this.leftPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerMasterScene.LiveVideoIn();
            //if (this.rightPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerMasterScene.LiveVideoIn();
        }

        internal void Vstage_TasksIn()
        {
            this.Vstage_SetTasks();
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.ToIn();
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.ToIn();
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.ToIn();
        }

        internal void Vstage_SelectTask(bool empty)
        {
            if (this.SelectedDataset is DatasetContent && !empty)
            {
                if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.SetHeadline(this.SelectedDataset.HostText);
                if (this.SelectedDataset.ID >= 1 &&
                    this.SelectedDataset.ID <= TasksCount)
                {
                    if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.SetHeadline(this.SelectedDataset.Text);
                    if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.SetHeadline(this.SelectedDataset.Text);
                }
                else 
                {
                    if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.SetHeadline(string.Empty);
                    if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.SetHeadline(string.Empty);
                }
            }
            else
            {
                if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.SetHeadline(string.Empty);
                if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.SetHeadline(string.Empty);
                if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.SetHeadline(string.Empty);
            }
        }

        internal void Vstage_SetTasks()
        {
            this.Vstage_SelectTask(true);
            string text = string.Empty;
            foreach (DatasetContent item in this.AvailableTasks)
            {
                if (item.ID <= TasksCount)
                {
                    if (!string.IsNullOrEmpty(text)) text += "\r\n";
                    text += item.Text;
                }
            }
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.SetText(text);
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.SetText(text);
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.SetText(text);
        }

        public void Vstage_SetBuzzer(
            PlayerSelection buzzeredPlayer)
        {
            switch (buzzeredPlayer)
            {
                case PlayerSelection.LeftPlayer:
                    this.hostMasterScene.SetBuzzerLeft();
                    this.leftPlayerMasterScene.SetBuzzerLeft();
                    this.rightPlayerMasterScene.SetBuzzerLeft();
                    break;
                case PlayerSelection.RightPlayer:
                    this.hostMasterScene.SetBuzzerRight();
                    this.leftPlayerMasterScene.SetBuzzerRight();
                    this.rightPlayerMasterScene.SetBuzzerRight();
                    break;
                case PlayerSelection.NotSelected:
                default:
                    this.hostMasterScene.SetBuzzerOut();
                    this.leftPlayerMasterScene.SetBuzzerOut();
                    this.rightPlayerMasterScene.SetBuzzerOut();
                    break;
            }
        }

        public override void Vhost_LoadScene() { this.hostScene.Load(); }
        public override void Vhost_UnloadScene() { this.hostScene.Unload(); }

        public override void Vleftplayer_LoadScene() { this.leftPlayerScene.Load(); }
        public override void Vleftplayer_UnloadScene() { this.leftPlayerScene.Unload(); }

        public override void Vrightplayer_LoadScene() { this.rightPlayerScene.Load(); }
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
                if (e.PropertyName == "Text") {
                    this.buildNameList();
                    this.on_PropertyChanged("NameList");
                }
            }
            this.Save(); 
        }

        #endregion

    }
}
