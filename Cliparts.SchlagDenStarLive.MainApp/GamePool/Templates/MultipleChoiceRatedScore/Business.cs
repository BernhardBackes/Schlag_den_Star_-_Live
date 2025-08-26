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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MultipleChoiceRatedScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.MultipleChoiceRatedScore {

    public class Data {
        public DatasetContent[] DataList;
        public int SelectedIndex;
    }

    public class DatasetContent : Messaging.Message, INotifyPropertyChanged {


        #region Properties

        private string name = string.Empty;
        public string Name {
            get { return this.name; }
            set {
                if (this.name != value) {
                    if (string.IsNullOrEmpty(value)) this.name = string.Empty;
                    else this.name = Regex.Replace(value, "(?<!\r)\n", "\r\n"); ;
                    this.buildToString();
                    this.on_PropertyChanged();
                    if (string.IsNullOrEmpty(this.HostText)) this.HostText = this.name;
                }
            }
        }

        private string hostText = string.Empty;
        public string HostText {
            get { return this.hostText; }
            set {
                if (this.hostText != value) {
                    if (string.IsNullOrEmpty(value)) this.hostText = string.Empty;
                    else this.hostText = Regex.Replace(value, "(?<!\r)\n", "\r\n"); ;
                    this.on_PropertyChanged();
                    if (string.IsNullOrEmpty(this.Name)) this.Name = this.hostText;
                }
            }
        }

        private string solutionFilename = string.Empty;
        public string SolutionFilename {
            get { return this.solutionFilename; }
            set {
                if (this.solutionFilename != value) {
                    if (string.IsNullOrEmpty(value)) this.solutionFilename = string.Empty;
                    else this.solutionFilename = value;
                    this.SolutionMovie = Helper.getThumbnailFromMediaFile(value, 2.0f);
                    this.on_PropertyChanged();
                }
            }
        }

        [XmlIgnore]
        public Image SolutionMovie { get; private set; }

        private string answerA = string.Empty;
        public string AnswerA {
            get { return this.answerA; }
            set {
                if (this.answerA != value) {
                    if (string.IsNullOrEmpty(value)) this.answerA = string.Empty;
                    else this.answerA = value;
                    this.on_PropertyChanged();
                    if (string.IsNullOrEmpty(this.AnswerAHost)) this.AnswerAHost = value;
                }
            }
        }

        private string answerAHost = string.Empty;
        public string AnswerAHost {
            get { return this.answerAHost; }
            set {
                if (this.answerAHost != value) {
                    if (string.IsNullOrEmpty(value)) this.answerAHost = string.Empty;
                    else this.answerAHost = value;
                    this.on_PropertyChanged();
                    if (string.IsNullOrEmpty(this.AnswerA)) {
                        int index = this.answerAHost.IndexOf('(');
                        if (index > 0) this.AnswerA = this.answerAHost.Substring(0, index).Trim();
                        else this.AnswerA = this.answerAHost;
                    }
                }
            }
        }

        private string answerB = string.Empty;
        public string AnswerB {
            get { return this.answerB; }
            set {
                if (this.answerB != value) {
                    if (string.IsNullOrEmpty(value)) this.answerB = string.Empty;
                    else this.answerB = value;
                    this.on_PropertyChanged();
                    if (string.IsNullOrEmpty(this.AnswerBHost)) this.AnswerBHost = value;
                }
            }
        }

        private string answerBHost = string.Empty;
        public string AnswerBHost {
            get { return this.answerBHost; }
            set {
                if (this.answerBHost != value) {
                    if (string.IsNullOrEmpty(value)) this.answerBHost = string.Empty;
                    else this.answerBHost = value;
                    this.on_PropertyChanged();
                    if (string.IsNullOrEmpty(this.AnswerB)) {
                        int index = this.answerBHost.IndexOf('(');
                        if (index > 0) this.AnswerB = this.answerBHost.Substring(0, index).Trim();
                        else this.AnswerB = this.answerBHost;
                    }
                }
            }
        }

        private string answerC = string.Empty;
        public string AnswerC {
            get { return this.answerC; }
            set {
                if (this.answerC != value) {
                    if (string.IsNullOrEmpty(value)) this.answerC = string.Empty;
                    else this.answerC = value;
                    this.on_PropertyChanged();
                    if (string.IsNullOrEmpty(this.AnswerCHost)) this.AnswerCHost = value;
                }
            }
        }

        private string answerCHost = string.Empty;
        public string AnswerCHost {
            get { return this.answerCHost; }
            set {
                if (this.answerCHost != value) {
                    if (string.IsNullOrEmpty(value)) this.answerCHost = string.Empty;
                    else this.answerCHost = value;
                    this.on_PropertyChanged();
                    if (string.IsNullOrEmpty(this.AnswerC)) {
                        int index = this.answerCHost.IndexOf('(');
                        if (index > 0) this.AnswerC = this.answerCHost.Substring(0, index).Trim();
                        else this.AnswerC = this.answerCHost;
                    }
                }
            }
        }

        private string answerD = string.Empty;
        public string AnswerD {
            get { return this.answerD; }
            set {
                if (this.answerD != value) {
                    if (string.IsNullOrEmpty(value)) this.answerD = string.Empty;
                    else this.answerD = value;
                    this.on_PropertyChanged();
                    if (string.IsNullOrEmpty(this.AnswerDHost)) this.AnswerDHost = value;
                }
            }
        }

        private string answerDHost = string.Empty;
        public string AnswerDHost {
            get { return this.answerDHost; }
            set {
                if (this.answerDHost != value) {
                    if (string.IsNullOrEmpty(value)) this.answerDHost = string.Empty;
                    else this.answerDHost = value;
                    this.on_PropertyChanged();
                    if (string.IsNullOrEmpty(this.AnswerD)) {
                        int index = this.answerDHost.IndexOf('(');
                        if (index > 0) this.AnswerD = this.answerDHost.Substring(0, index).Trim();
                        else this.AnswerD = this.answerDHost;
                    }
                }
            }
        }

        private Game.SolutionItems solution;
        public Game.SolutionItems Solution {
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

        public DatasetContent() { }
        public DatasetContent(
            string movieFilename) {
            if (string.IsNullOrEmpty(movieFilename)) {
                this.Name = "?";
                this.SolutionFilename = string.Empty;
            }
            else {
                this.Name = Path.GetFileNameWithoutExtension(movieFilename);
                this.SolutionFilename = movieFilename;
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
        #endregion

    }

    public class Business : _Base.TimerScore.Business {

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

        private int insertPositionX = 0;
        public int InsertPositionX {
            get { return this.insertPositionX; }
            set {
                if (this.insertPositionX != value) {
                    this.insertPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetContent();
                }
            }
        }

        private int insertPositionY = 0;
        public int InsertPositionY {
            get { return this.insertPositionY; }
            set {
                if (this.insertPositionY != value) {
                    this.insertPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetContent();
                }
            }
        }


        private string filename = string.Empty;
        public string Filename {
            get { return this.filename; }
            set { if (this.filename != value) this.Load(value); }
        }

        private List<DatasetContent> dataList = new List<DatasetContent>();
        [XmlIgnore]
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

        private int? leftPlayerValueA = null;
        [XmlIgnore]
        public int? LeftPlayerValueA {
            get { return this.leftPlayerValueA; }
            set {
                if (this.leftPlayerValueA != value) {
                    this.leftPlayerValueA = value;
                    if (this.LeftPlayerValueA.HasValue && this.LeftPlayerValueA.Value < 0) this.LeftPlayerValueA = null;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? leftPlayerValueB = null;
        [XmlIgnore]
        public int? LeftPlayerValueB {
            get { return this.leftPlayerValueB; }
            set {
                if (this.leftPlayerValueB != value) {
                    this.leftPlayerValueB = value;
                    if (this.leftPlayerValueB.HasValue && this.leftPlayerValueB.Value < 0) this.leftPlayerValueB = null;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? leftPlayerValueC = null;
        [XmlIgnore]
        public int? LeftPlayerValueC {
            get { return this.leftPlayerValueC; }
            set {
                if (this.leftPlayerValueC != value) {
                    this.leftPlayerValueC = value;
                    if (this.leftPlayerValueC.HasValue && this.leftPlayerValueC.Value < 0) this.leftPlayerValueC = null;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? leftPlayerValueD = null;
        [XmlIgnore]
        public int? LeftPlayerValueD {
            get { return this.leftPlayerValueD; }
            set {
                if (this.leftPlayerValueD != value) {
                    this.leftPlayerValueD = value;
                    if (this.leftPlayerValueD.HasValue && this.leftPlayerValueD.Value < 0) this.leftPlayerValueD = null;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool leftPlayerSelectionDone {
            get {
                return this.leftPlayerValueA.HasValue || this.leftPlayerValueB.HasValue || this.leftPlayerValueC.HasValue || this.leftPlayerValueD.HasValue;
            }
        }

        private int? rightPlayerValueA = null;
        [XmlIgnore]
        public int? RightPlayerValueA {
            get { return this.rightPlayerValueA; }
            set {
                if (this.rightPlayerValueA != value) {
                    this.rightPlayerValueA = value;
                    if (this.rightPlayerValueA.HasValue && this.rightPlayerValueA.Value < 0) this.rightPlayerValueA = null;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? rightPlayerValueB = null;
        [XmlIgnore]
        public int? RightPlayerValueB {
            get { return this.rightPlayerValueB; }
            set {
                if (this.rightPlayerValueB != value) {
                    this.rightPlayerValueB = value;
                    if (this.rightPlayerValueB.HasValue && this.rightPlayerValueB.Value < 0) this.rightPlayerValueB = null;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? rightPlayerValueC = null;
        [XmlIgnore]
        public int? RightPlayerValueC {
            get { return this.rightPlayerValueC; }
            set {
                if (this.rightPlayerValueC != value) {
                    this.rightPlayerValueC = value;
                    if (this.rightPlayerValueC.HasValue && this.rightPlayerValueC.Value < 0) this.rightPlayerValueC = null;
                    this.on_PropertyChanged();
                }
            }
        }

        private int? rightPlayerValueD = null;
        [XmlIgnore]
        public int? RightPlayerValueD {
            get { return this.rightPlayerValueD; }
            set {
                if (this.rightPlayerValueD != value) {
                    this.rightPlayerValueD = value;
                    if (this.rightPlayerValueD.HasValue && this.rightPlayerValueD.Value < 0) this.rightPlayerValueD = null;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool rightPlayerSelectionDone {
            get {
                return this.rightPlayerValueA.HasValue || this.rightPlayerValueB.HasValue || this.rightPlayerValueC.HasValue || this.rightPlayerValueD.HasValue;
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

        #endregion


        #region Funktionen

        public Business() { }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.MultipleChoiceRatedScore'", typeIdentifier);
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

            this.hostScene = new Host(syncContext, hostMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.hostScene.StatusChanged += this.hostScene_StatusChanged;

            this.leftPlayerScene = new Player(syncContext, leftPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.leftPlayerScene.StatusChanged += this.leftPlayerScene_StatusChanged;
            this.leftPlayerScene.PropertyChanged += this.leftPlayerScene_PropertyChanged;
            this.leftPlayerScene.OKPressed += this.leftPlayerScene_OKPressed;

            this.rightPlayerScene = new Player(syncContext, rightPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.rightPlayerScene.StatusChanged += this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.PropertyChanged += this.rightPlayerScene_PropertyChanged;
            this.rightPlayerScene.OKPressed += this.rightPlayerScene_OKPressed;

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

            this.leftPlayerScene.StatusChanged -= this.leftPlayerScene_StatusChanged;
            this.leftPlayerScene.PropertyChanged -= this.leftPlayerScene_PropertyChanged;
            this.leftPlayerScene.OKPressed -= this.leftPlayerScene_OKPressed;
            this.leftPlayerScene.Dispose();

            this.rightPlayerScene.StatusChanged -= this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.PropertyChanged -= this.rightPlayerScene_PropertyChanged;
            this.rightPlayerScene.OKPressed -= this.rightPlayerScene_OKPressed;
            this.rightPlayerScene.Dispose();

        }

        public override void ResetData() {
            base.ResetData();
            this.TaskCounter = 0;
            this.SelectDataset(0);
            this.LeftPlayerValueA = null;
            this.LeftPlayerValueB = null;
            this.LeftPlayerValueC = null;
            this.LeftPlayerValueD = null;
            this.RightPlayerValueA = null;
            this.RightPlayerValueB = null;
            this.RightPlayerValueC = null;
            this.RightPlayerValueD = null;
            this.leftPlayerValueA = null;
            this.leftPlayerValueB = null;
            this.leftPlayerValueC = null;
            this.leftPlayerValueD = null;
            this.rightPlayerValueA = null;
            this.rightPlayerValueB = null;
            this.rightPlayerValueC = null;
            this.rightPlayerValueD = null;
        }

        public void Resolve() {
            if (this.SelectedDataset is DatasetContent &&
                (!this.SampleIncluded || (this.SampleIncluded && this.SelectedDatasetIndex > 0))) {
                switch (this.SelectedDataset.Solution) {
                    case Game.SolutionItems.AnswerA:
                        if (this.LeftPlayerValueA.HasValue) this.LeftPlayerScore += this.LeftPlayerValueA.Value;
                        if (this.RightPlayerValueA.HasValue) this.RightPlayerScore += this.RightPlayerValueA.Value;
                        break;
                    case Game.SolutionItems.AnswerB:
                        if (this.LeftPlayerValueB.HasValue) this.LeftPlayerScore += this.LeftPlayerValueB.Value;
                        if (this.RightPlayerValueB.HasValue) this.RightPlayerScore += this.RightPlayerValueB.Value;
                        break;
                    case Game.SolutionItems.AnswerC:
                        if (this.LeftPlayerValueC.HasValue) this.LeftPlayerScore += this.LeftPlayerValueC.Value;
                        if (this.RightPlayerValueC.HasValue) this.RightPlayerScore += this.RightPlayerValueC.Value;
                        break;
                    case Game.SolutionItems.AnswerD:
                        if (this.LeftPlayerValueD.HasValue) this.LeftPlayerScore += this.LeftPlayerValueD.Value;
                        if (this.RightPlayerValueD.HasValue) this.RightPlayerScore += this.RightPlayerValueD.Value;
                        break;
                }
            }
        }

        public override void Next() {
            base.Next();
            this.TaskCounter++;
            this.SelectDataset(this.SelectedDatasetIndex + 1);
            this.LeftPlayerValueA = null;
            this.LeftPlayerValueB = null;
            this.LeftPlayerValueC = null;
            this.LeftPlayerValueD = null;
            this.RightPlayerValueA = null;
            this.RightPlayerValueB = null;
            this.RightPlayerValueC = null;
            this.RightPlayerValueD = null;
            this.leftPlayerValueA = null;
            this.leftPlayerValueB = null;
            this.leftPlayerValueC = null;
            this.leftPlayerValueD = null;
            this.rightPlayerValueA = null;
            this.rightPlayerValueB = null;
            this.rightPlayerValueC = null;
            this.rightPlayerValueD = null;
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
                }
                else {
                    this.dataList.Add(newDataset);
                }
                this.buildNameList();
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
            int index = 0;
            foreach (DatasetContent item in this.dataList) {
                //item.Index = index;
                this.names.Add(item.ToString());
                index++;
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
                    //this.checkAges();
                    this.Save();
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
        public override void Vinsert_StartTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.StartTimer(); }
        public override void Vinsert_StopTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.StopTimer(); }
        public override void Vinsert_ContinueTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.ContinueTimer(); }
        public override void Vinsert_ResetTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.ResetTimer(); }
        public override void Vinsert_TimerOut() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.ToOut(); }

        public override void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score, this.LeftPlayerScore, this.RightPlayerScore); }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }

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

        public void Vinsert_ContentTextIn() {
            this.Vinsert_SetContent();
            if (this.insertScene is Insert &&
                this.insertScene.Game is Game) this.insertScene.Game.ToIn();
            if (this.TaskCounter > 0 && this.TaskCounter <= this.TaskCounterSize) this.Vinsert_TaskCounterIn();
        }
        public void Vinsert_SetContent() {
            if (this.insertScene is Insert) {
                this.Vinsert_SetContent(
                    this.insertScene.Game,
                    this.SelectedDataset,
                    this.LeftPlayerValueA,
                    this.LeftPlayerValueB,
                    this.LeftPlayerValueC,
                    this.LeftPlayerValueD,
                    this.RightPlayerValueA,
                    this.RightPlayerValueB,
                    this.RightPlayerValueC,
                    this.RightPlayerValueD);
            }
        }
        public void Vinsert_SetContent(
            Game scene,
            DatasetContent content,
            int? leftPlayerValueA,
            int? leftPlayerValueB,
            int? leftPlayerValueC,
            int? leftPlayerValueD,
            int? rightPlayerValueA,
            int? rightPlayerValueB,
            int? rightPlayerValueC,
            int? rightPlayerValueD) {
            if (scene is Game) {
                scene.SetPositionX(this.InsertPositionX);
                scene.SetPositionY(this.InsertPositionY);
                if (content is DatasetContent) {
                    scene.SetText(content.Name);
                    scene.SetAnswerAText(content.AnswerA);
                    scene.SetAnswerBText(content.AnswerB);
                    scene.SetAnswerCText(content.AnswerC);
                    scene.SetAnswerDText(content.AnswerD);
                    scene.SetAnswersSolution(content.Solution);
                }
                else {
                    scene.SetText(string.Empty);
                    scene.SetAnswerAText(string.Empty);
                    scene.SetAnswerBText(string.Empty);
                    scene.SetAnswerCText(string.Empty);
                    scene.SetAnswerDText(string.Empty);
                }
                if (leftPlayerValueA.HasValue) scene.SetAnswerAInputLeft(leftPlayerValueA.Value.ToString());
                else scene.SetAnswerAInputLeft(string.Empty);
                if (leftPlayerValueB.HasValue) scene.SetAnswerBInputLeft(leftPlayerValueB.Value.ToString());
                else scene.SetAnswerBInputLeft(string.Empty);
                if (leftPlayerValueC.HasValue) scene.SetAnswerCInputLeft(leftPlayerValueC.Value.ToString());
                else scene.SetAnswerCInputLeft(string.Empty);
                if (leftPlayerValueD.HasValue) scene.SetAnswerDInputLeft(leftPlayerValueD.Value.ToString());
                else scene.SetAnswerDInputLeft(string.Empty);
                if (rightPlayerValueA.HasValue) scene.SetAnswerAInputRight(rightPlayerValueA.Value.ToString());
                else scene.SetAnswerAInputRight(string.Empty);
                if (rightPlayerValueB.HasValue) scene.SetAnswerBInputRight(rightPlayerValueB.Value.ToString());
                else scene.SetAnswerBInputRight(string.Empty);
                if (rightPlayerValueC.HasValue) scene.SetAnswerCInputRight(rightPlayerValueC.Value.ToString());
                else scene.SetAnswerCInputRight(string.Empty);
                if (rightPlayerValueD.HasValue) scene.SetAnswerDInputRight(rightPlayerValueD.Value.ToString());
                else scene.SetAnswerDInputRight(string.Empty);
            }
        }
        public void Vinsert_ContentAnswersIn() {
            this.Vinsert_SetContent();
            if (this.insertScene is Insert &&
                this.insertScene.Game is Game) {
                this.insertScene.Game.SetIn();
                this.insertScene.Game.AnswersToIn();
            }
        }
        public void Vinsert_ContentInputIn() {
            this.Vinsert_SetContent();
            if (this.insertScene is Insert &&
                this.insertScene.Game is Game) {
                this.insertScene.Game.AnswersToInputIn();
            }
        }
        public void Vinsert_ContentResolve() {
            this.Vinsert_SetContent();
            if (this.insertScene is Insert &&
                this.insertScene.Game is Game) {
                this.insertScene.Game.AnswersToSolutionIn();
            }
        }
        public void Vinsert_ContentOut() {
            if (this.insertScene is Insert &&
                this.insertScene.Game is Game) this.insertScene.Game.ToOut();
            this.Vinsert_TaskCounterOut();
        }

        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void Vfullscreen_LoadScene() { this.fullscreenScene.Load(); }
        public void Vfullscreen_ContentIn() {
            if (this.fullscreenScene is VRemote4.HandlerSi.Scene &&
                this.SelectedDataset is DatasetContent) {
                this.Vfullscreen_SetContent(this.fullscreenScene, this.SelectedDataset);
                this.fullscreenScene.ToIn();
            }
        }
        public void Vfullscreen_SetContent(
            Fullscreen scene,
            DatasetContent dataset) {
            if (scene is Fullscreen &&
                dataset is DatasetContent) {
                scene.SetMovieFilename(dataset.SolutionFilename);

            }
        }
        public void Vfullscreen_Start() {
            if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.Start();

        }
        public void Vfullscreen_ContentOut() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene) this.fullscreenScene.ToOut(); }
        public override void Vfullscreen_UnloadScene() { this.fullscreenScene.Unload(); }

        public override void Vstage_Init() {
            base.Vstage_Init();
            //if (this.hostMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostMasterScene.LiveVideoIn();
            //if (this.leftPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerMasterScene.LiveVideoIn();
            //if (this.rightPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerMasterScene.LiveVideoIn();
        }

        public override void Vhost_LoadScene() {
            base.Vhost_LoadScene();
            this.hostScene.Load();
        }

        public void Vstage_ContentIn() {
            this.Vhost_ContentIn();
            this.Vleftplayer_ContentIn();
            this.Vrightplayer_ContentIn();
        }
        public void Vstage_ContentOut() {
            this.Vhost_ContentOut();
            this.Vleftplayer_ContentOut();
            this.Vrightplayer_ContentOut();
        }

        internal void Vhost_ContentIn() {
            this.Vhost_SetContent();
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.GameToIn();
        }
        internal void Vhost_SetContent() {
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vhost_SetContent(
                this.hostScene,
                this.SelectedDataset,
                this.LeftPlayerValueA,
                this.LeftPlayerValueB,
                this.LeftPlayerValueC,
                this.LeftPlayerValueD,
                this.RightPlayerValueA,
                this.RightPlayerValueB,
                this.RightPlayerValueC,
                this.RightPlayerValueD);
        }
        internal void Vhost_SetContent(
            Host scene,
            DatasetContent content,
            int? leftPlayerValueA,
            int? leftPlayerValueB,
            int? leftPlayerValueC,
            int? leftPlayerValueD,
            int? rightPlayerValueA,
            int? rightPlayerValueB,
            int? rightPlayerValueC,
            int? rightPlayerValueD) {
            if (scene is Host) {
                if (content is DatasetContent) {
                    scene.SetText(content.HostText);
                    scene.SetAnswerAText(content.AnswerAHost);
                    scene.SetAnswerBText(content.AnswerBHost);
                    scene.SetAnswerCText(content.AnswerCHost);
                    scene.SetAnswerDText(content.AnswerDHost);
                    switch (content.Solution) {
                        case Game.SolutionItems.AnswerA:
                            scene.SetAnswerAIsSolution(true);
                            scene.SetAnswerBIsSolution(false);
                            scene.SetAnswerCIsSolution(false);
                            scene.SetAnswerDIsSolution(false);
                            break;
                        case Game.SolutionItems.AnswerB:
                            scene.SetAnswerAIsSolution(false);
                            scene.SetAnswerBIsSolution(true);
                            scene.SetAnswerCIsSolution(false);
                            scene.SetAnswerDIsSolution(false);
                            break;
                        case Game.SolutionItems.AnswerC:
                            scene.SetAnswerAIsSolution(false);
                            scene.SetAnswerBIsSolution(false);
                            scene.SetAnswerCIsSolution(true);
                            scene.SetAnswerDIsSolution(false);
                            break;
                        case Game.SolutionItems.AnswerD:
                            scene.SetAnswerAIsSolution(false);
                            scene.SetAnswerBIsSolution(false);
                            scene.SetAnswerCIsSolution(false);
                            scene.SetAnswerDIsSolution(true);
                            break;
                    }
                }
                else {
                    scene.SetText(string.Empty);
                    scene.SetAnswerAText(string.Empty);
                    scene.SetAnswerBText(string.Empty);
                    scene.SetAnswerCText(string.Empty);
                    scene.SetAnswerDText(string.Empty);
                    scene.SetAnswerAIsSolution(false);
                    scene.SetAnswerBIsSolution(false);
                    scene.SetAnswerCIsSolution(false);
                    scene.SetAnswerDIsSolution(false);
                }
                if (leftPlayerValueA.HasValue) scene.SetAnswerAInputLeft(leftPlayerValueA.Value.ToString());
                else scene.SetAnswerAInputLeft(string.Empty);
                if (leftPlayerValueB.HasValue) scene.SetAnswerBInputLeft(leftPlayerValueB.Value.ToString());
                else scene.SetAnswerBInputLeft(string.Empty);
                if (leftPlayerValueC.HasValue) scene.SetAnswerCInputLeft(leftPlayerValueC.Value.ToString());
                else scene.SetAnswerCInputLeft(string.Empty);
                if (leftPlayerValueD.HasValue) scene.SetAnswerDInputLeft(leftPlayerValueD.Value.ToString());
                else scene.SetAnswerDInputLeft(string.Empty);
                if (rightPlayerValueA.HasValue) scene.SetAnswerAInputRight(rightPlayerValueA.Value.ToString());
                else scene.SetAnswerAInputRight(string.Empty);
                if (rightPlayerValueB.HasValue) scene.SetAnswerBInputRight(rightPlayerValueB.Value.ToString());
                else scene.SetAnswerBInputRight(string.Empty);
                if (rightPlayerValueC.HasValue) scene.SetAnswerCInputRight(rightPlayerValueC.Value.ToString());
                else scene.SetAnswerCInputRight(string.Empty);
                if (rightPlayerValueD.HasValue) scene.SetAnswerDInputRight(rightPlayerValueD.Value.ToString());
                else scene.SetAnswerDInputRight(string.Empty);
            }
        }
        internal void Vhost_ContentOut() {
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.GameToOut();
        }

        public override void Vhost_UnloadScene() {
            base.Vhost_UnloadScene();
            this.hostScene.Unload();
        }


        public override void Vleftplayer_LoadScene() {
            base.Vleftplayer_LoadScene();
            this.leftPlayerScene.Load();
        }
        public void Vleftplayer_ContentIn() {
            this.Vleftplayer_SetContent();
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.GameToIn();
        }
        public void Vleftplayer_SetContent() { this.Vplayer_SetContent(this.leftPlayerScene, this.SelectedDataset); }
        public void Vleftplayer_EnableTouch() { if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.UnlockInput(); }
        public void Vleftplayer_DisableTouch() { if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.LockInput(); }
        public void Vleftplayer_ContentOut() { if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.GameToOut(); }
        public override void Vleftplayer_UnloadScene() {
            base.Vleftplayer_UnloadScene();
            this.leftPlayerScene.Unload();
        }

        public override void Vrightplayer_LoadScene() {
            base.Vrightplayer_LoadScene();
            this.rightPlayerScene.Load();
        }
        public void Vrightplayer_ContentIn() {
            this.Vrightplayer_SetContent();
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.GameToIn();
        }
        public void Vrightplayer_SetContent() { this.Vplayer_SetContent(this.rightPlayerScene, this.SelectedDataset); }
        public void Vrightplayer_EnableTouch() { if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.UnlockInput(); }
        public void Vrightplayer_DisableTouch() { if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.LockInput(); }
        public void Vrightplayer_ContentOut() { if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.GameToOut(); }
        public override void Vrightplayer_UnloadScene() {
            base.Vrightplayer_UnloadScene();
            this.rightPlayerScene.Unload();
        }

        public void Vplayer_SetContent(
            Player scene,
            DatasetContent content) {
            if (scene is Player) {
                if (content is DatasetContent) {
                    scene.SetText(content.Name);
                    scene.SetAnswerAText(content.AnswerA);
                    scene.SetAnswerBText(content.AnswerB);
                    scene.SetAnswerCText(content.AnswerC);
                    scene.SetAnswerDText(content.AnswerD);
                }
                else {
                    scene.SetText(string.Empty);
                    scene.SetAnswerAText(string.Empty);
                    scene.SetAnswerBText(string.Empty);
                    scene.SetAnswerCText(string.Empty);
                    scene.SetAnswerDText(string.Empty);
                }
            }
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

        void leftPlayerScene_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftPlayerScene_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftPlayerScene_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "Selection") {
                    //this.LeftTeamInput = this.ventuzLeftPlayerScene.Controller.Selection;
                }
            }
        }

        void leftPlayerScene_OKPressed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftPlayerScene_OKPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftPlayerScene_OKPressed(object content) {
            this.LeftPlayerValueA = this.leftPlayerScene.AnswerAInput;
            this.LeftPlayerValueB = this.leftPlayerScene.AnswerBInput;
            this.LeftPlayerValueC = this.leftPlayerScene.AnswerCInput;
            this.LeftPlayerValueD = this.leftPlayerScene.AnswerDInput;
            if (leftPlayerSelectionDone && rightPlayerSelectionDone) this.Vinsert_StopTimer();
            this.Vhost_SetContent();
        }

        void rightPlayerScene_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerScene_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerScene_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "Selection") {
                    //this.RightTeamInput = this.rightPlayerScene.Controller.Selection;
                }
            }
        }

        void rightPlayerScene_OKPressed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerScene_OKPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerScene_OKPressed(object content) {
            this.RightPlayerValueA = this.rightPlayerScene.AnswerAInput;
            this.RightPlayerValueB = this.rightPlayerScene.AnswerBInput;
            this.RightPlayerValueC = this.rightPlayerScene.AnswerCInput;
            this.RightPlayerValueD = this.rightPlayerScene.AnswerDInput;
            if (leftPlayerSelectionDone && rightPlayerSelectionDone) this.Vinsert_StopTimer();
            this.Vhost_SetContent();
        }

        #endregion

    }
}
