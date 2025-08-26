using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Cliparts.IOnet.IOUnit.IONbase;
using Cliparts.IOnet.IOUnit.IONbuz;

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;
using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;
using Cliparts.Serialization;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimeToBeatSpeedKick;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimeToBeatSpeedKick {

    public enum StepPositions { Top, Left, Right, Bottom, NA }

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
                    this.name = value;
                    this.buildToString();
                    this.on_PropertyChanged();
                }
            }
        }

        private List<StepPositions> stepList = new List<StepPositions>();
        public StepPositions[] StepList {
            get { return this.stepList.ToArray(); }
            set {
                this.stepList.Clear();
                if (value is StepPositions[]) this.stepList = new List<StepPositions>(value);
                this.on_PropertyChanged("StepList");
            }
        }


        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DatasetContent() { }
        public DatasetContent(
            string name,
            List<StepPositions> stepList) {
            this.Name = name;
            if (stepList == null) this.stepList = new List<StepPositions>();
            else this.stepList = stepList;
        }

        public void InsertStep(
            int index,
            StepPositions item) {
            if (index < 0) index = 0;
            int maxIndex = this.stepList.Count - 1;
            if (index > maxIndex) this.stepList.Add(item);
            else this.stepList.Insert(index, item);
            this.on_PropertyChanged("StepList");
        }

        public void SetStep(
            int index,
            StepPositions item) {
            int maxIndex = this.stepList.Count - 1;
            if (index >= 0
                && index <= maxIndex)  this.stepList[index] = item;
            this.on_PropertyChanged("StepList");
        }

        public void RemoveStep(
            int index) {
            int maxIndex = this.stepList.Count - 1;
            if (index >= 0
                && index <= maxIndex) this.stepList.RemoveAt(index);
            this.on_PropertyChanged("StepList");
        }

        public void Clear() { 
            this.stepList.Clear();
            this.on_PropertyChanged("StepList");
        }

        public void AddStepsRandom(
            int count) {
            if (count > 0) {
                Random rnd = new Random();
                int value = 0;
                int counter = 0;
                int stepsCount = this.stepList.Count;
                if (stepsCount == 0) {
                    this.InsertStep(0, StepPositions.Top);
                    counter++;
                }
                else value = (int)this.stepList[stepsCount - 1];

                while (counter < count) {
                    value = (value + rnd.Next(1, 3)) % 4;
                    this.stepList.Add((StepPositions)value);
                    counter++;
                }
            }
            this.on_PropertyChanged("StepList");
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

    public class Business : _Base.Score.Business {

        #region Properties

        private MidiHandler.Business midiHandler;

        private int timeToBeatPositionX = 0;
        public int TimeToBeatPositionX {
            get { return this.timeToBeatPositionX; }
            set {
                if (this.timeToBeatPositionX != value) {
                    this.timeToBeatPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeToBeat();
                }
            }
        }

        private int timeToBeatPositionY = 0;
        public int TimeToBeatPositionY {
            get { return this.timeToBeatPositionY; }
            set {
                if (this.timeToBeatPositionY != value) {
                    this.timeToBeatPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeToBeat();
                }
            }
        }

        private int timeToBeatStopTime = 5999;
        public int TimeToBeatStopTime {
            get { return this.timeToBeatStopTime; }
            set {
                if (this.timeToBeatStopTime != value) {
                    this.timeToBeatStopTime = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeToBeat();
                }
            }
        }

        private int timeToBeatSentenceTime = 0;
        public int TimeToBeatSentenceTime {
            get { return this.timeToBeatSentenceTime; }
            set {
                if (this.timeToBeatSentenceTime != value) {
                    this.timeToBeatSentenceTime = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeToBeatSentenceTime();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.TimeToBeat.Styles timeToBeatStyle = VentuzScenes.GamePool._Modules.TimeToBeat.Styles.StopwatchName;
        public VentuzScenes.GamePool._Modules.TimeToBeat.Styles TimeToBeatStyle {
            get { return this.timeToBeatStyle; }
            set {
                if (this.timeToBeatStyle != value) {
                    this.timeToBeatStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeToBeat();
                }
            }
        }

        private double timeToBeatCurrentTime = -1;
        public double TimeToBeatCurrentTime {
            get { return this.timeToBeatCurrentTime; }
            protected set {
                if (this.timeToBeatCurrentTime != value) {
                    this.timeToBeatCurrentTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private BuzzerIO.Business buzzerHandler;

        private List<string> ioUnitNameList = new List<string>();
        public string[] IOUnitNameList { get { return this.ioUnitNameList.ToArray(); } }

        private InfoParamArray_EventArgs ioUnitInfo;

        private string ioUnitName = string.Empty;
        public string IOUnitName {
            get { return this.ioUnitName; }
            set {
                if (this.ioUnitName != value) {
                    if (value == null) value = string.Empty;
                    this.ioUnitName = value;
                    this.on_PropertyChanged();
                    this.ioUnitConnectionStatus = Tools.NetContact.ClientStates.Missing;
                    this.ioUnitWorkMode = WorkModes.NA;
                    this.checkIOUnitStatus();
                    this.requestIOUnitStates(value);
                }
            }
        }

        private Tools.NetContact.ClientStates ioUnitConnectionStatus = Tools.NetContact.ClientStates.Missing;

        private WorkModes ioUnitWorkMode = WorkModes.NA;

        public BuzzerUnitStates IOUnitStatus { get; private set; }

        private int topBuzzerChannel = 1;
        public int TopBuzzerChannel {
            get { return this.topBuzzerChannel; }
            set {
                if (this.topBuzzerChannel != value) {
                    if (value < 1) this.topBuzzerChannel = 1;
                    else if (value > 8) this.topBuzzerChannel = 8;
                    else this.topBuzzerChannel = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int leftBuzzerChannel = 2;
        public int LeftBuzzerChannel {
            get { return this.leftBuzzerChannel; }
            set {
                if (this.leftBuzzerChannel != value) {
                    if (value < 1) this.leftBuzzerChannel = 1;
                    else if (value > 8) this.leftBuzzerChannel = 8;
                    else this.leftBuzzerChannel = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightBuzzerChannel = 3;
        public int RightBuzzerChannel {
            get { return this.rightBuzzerChannel; }
            set {
                if (this.rightBuzzerChannel != value) {
                    if (value < 1) this.rightBuzzerChannel = 1;
                    else if (value > 8) this.rightBuzzerChannel = 8;
                    else this.rightBuzzerChannel = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int bottomBuzzerChannel = 4;
        public int BottomBuzzerChannel {
            get { return this.bottomBuzzerChannel; }
            set {
                if (this.bottomBuzzerChannel != value) {
                    if (value < 1) this.bottomBuzzerChannel = 1;
                    else if (value > 8) this.bottomBuzzerChannel = 8;
                    else this.bottomBuzzerChannel = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private Tools.DMX.DMXNet dMX;
        private byte[] universe = new byte[512];

        private Color offColor = Color.Black;
        public Color OffColor {
            get { return this.offColor; }
            set {
                if (this.offColor != value) {
                    this.offColor = value;
                    this.on_PropertyChanged();
                    this.setDMXValues();
                }
            }
        }

        private Color nextColor = Color.Gold;
        public Color NextColor {
            get { return this.nextColor; }
            set {
                if (this.nextColor != value) {
                    this.nextColor = value;
                    this.on_PropertyChanged();
                    this.setDMXValues();
                }
            }
        }

        private Color onColor = Color.Green;
        public Color OnColor {
            get { return this.onColor; }
            set {
                if (this.onColor != value) {
                    this.onColor = value;
                    this.on_PropertyChanged();
                    this.setDMXValues();
                }
            }
        }

        private Color hitColor = Color.Red;
        public Color HitColor {
            get { return this.hitColor; }
            set {
                if (this.hitColor != value) {
                    this.hitColor = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool showNextPanel = true;
        public bool ShowNextPanel {
            get { return this.showNextPanel; }
            set {
                if (this.showNextPanel != value) {
                    this.showNextPanel = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int topPanelDMXStart = 1;
        public int TopPanelDMXStart {
            get { return this.topPanelDMXStart; }
            set {
                if (this.topPanelDMXStart != value) {
                    if (value < 1) this.topPanelDMXStart = 1;
                    else if (value > 256) this.topPanelDMXStart = 256;
                    else this.topPanelDMXStart = value;
                    this.on_PropertyChanged();
                    this.setDMXValues();
                }
            }
        }

        private int leftPanelDMXStart = 4;
        public int LeftPanelDMXStart {
            get { return this.leftPanelDMXStart; }
            set {
                if (this.leftPanelDMXStart != value) {
                    if (value < 1) this.leftPanelDMXStart = 1;
                    else if (value > 256) this.leftPanelDMXStart = 256;
                    else this.leftPanelDMXStart = value;
                    this.on_PropertyChanged();
                    this.setDMXValues();
                }
            }
        }

        private int rightPanelDMXStart = 7;
        public int RightPanelDMXStart {
            get { return this.rightPanelDMXStart; }
            set {
                if (this.rightPanelDMXStart != value) {
                    if (value < 1) this.rightPanelDMXStart = 1;
                    else if (value > 256) this.rightPanelDMXStart = 256;
                    else this.rightPanelDMXStart = value;
                    this.on_PropertyChanged();
                    this.setDMXValues();
                }
            }
        }

        private int bottomPanelDMXStart = 10;
        public int BottomPanelDMXStart {
            get { return this.bottomPanelDMXStart; }
            set {
                if (this.bottomPanelDMXStart != value) {
                    if (value < 1) this.bottomPanelDMXStart = 1;
                    else if (value > 256) this.bottomPanelDMXStart = 256;
                    else this.bottomPanelDMXStart = value;
                    this.on_PropertyChanged();
                    this.setDMXValues();
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

        private PlayerSelection selectedPlayer = PlayerSelection.LeftPlayer;
        [NotSerialized]
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

        private double? timeToBeat = null;
        [NotSerialized]
        public string TimeToBeat {
            get {
                if (this.timeToBeat.HasValue) return Helper.convertDoubleToStopwatchTimeText(this.timeToBeat.Value, false, true).Replace(".", ",");
                else return string.Empty;
            }
            set {
                if (this.TimeToBeat != value) {
                    double result;
                    if (string.IsNullOrEmpty(value) ||
                        !double.TryParse(value, out result)) this.timeToBeat = null;
                    else this.timeToBeat = result;
                    this.on_PropertyChanged();
                }
            }
        }

        private int buzzerIndex = -1;
        public int BuzzerIndex {
            get { return this.buzzerIndex; }
            private set {
                if (this.buzzerIndex != value) {
                    this.buzzerIndex = value;
                    this.on_PropertyChanged();
                    this.setRemaining();
                }
            }
        }

        public StepPositions ActiveBuzzer {
            get {
                if (this.SelectedDataset is DatasetContent &&
                    this.BuzzerIndex >= 0 &&
                    this.BuzzerIndex < this.SelectedDataset.StepList.Length) return this.SelectedDataset.StepList[this.BuzzerIndex];
                else return StepPositions.NA;
            }
        }
        public StepPositions NextActiveBuzzer {
            get {
                if (this.SelectedDataset is DatasetContent &&
                    this.BuzzerIndex >= 0 &&
                    this.BuzzerIndex < this.SelectedDataset.StepList.Length - 1) return this.SelectedDataset.StepList[this.BuzzerIndex + 1];
                else return StepPositions.NA;
            }
        }

        private int sentenceTime = 10;
        public int SentenceTime {
            get { return this.sentenceTime; }
            set {
                if (this.sentenceTime != value) {
                    this.sentenceTime = value;
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

        private int remaining = 0;
        public int Remaining {
            get { return this.remaining; }
            set {
                if (this.remaining != value) {
                    if (value < 0) this.remaining = 0;
                    else this.remaining = value;
                    this.on_PropertyChanged();
                    this.Vfullscreen_SetTimer(this.remaining);
                }
            }
        }

        private bool timeIsRunning = false;

        private bool toggleOn = true;
        private System.Timers.Timer toggleTimer;

        #endregion


        #region Funktionen

        public Business() { }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.TimeToBeatSpeedKick'", typeIdentifier);
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

            this.midiHandler = midiHandler;

            this.buzzerHandler = buzzerHandler;
            this.buzzerHandler.BuzUnit_Buzzered += this.buzzerHandler_BuzUnit_Buzzered;
            this.buzzerHandler.Unit_ConnectionStatusChanged += this.buzzerHandler_UnitConnectionStatusChanged;
            this.buzzerHandler.Unit_ConnectionStatusRequest += this.buzzerHandler_UnitConnectionStatusRequest;
            this.buzzerHandler.Unit_InfoListChanged += this.buzzerHandler_UnitInfoListChanged;
            this.buzzerHandler.BuzUnit_WorkmodeChanged += this.buzzerHandler_BuzUnit_WorkmodeChanged;
            this.buzzerHandler.BuzUnit_WorkmodeRequest += this.buzzerHandler_BuzUnit_WorkmodeRequest;
            this.fillBuzzerUnitList(this.buzzerHandler.UnitInfoList);
            this.buzzerHandler.RequestUnitConnectionStatus(this.IOUnitName);
            this.buzzerHandler.RequestUnitWorkMode(this.IOUnitName);

            this.dMX = new Tools.DMX.DMXNet();
            this.setDMXValues(0, new byte[0]);

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;
            this.insertScene.TimeToBeat.StopFired += this.timeToBeat_StopFired;
            this.insertScene.TimeToBeat.PropertyChanged += this.timeToBeat_PropertyChanged;

            this.toggleTimer = new System.Timers.Timer();
            this.toggleTimer.AutoReset = false;
            this.toggleTimer.Elapsed += this.toggleTimer_Elapsed;
            this.toggleTimer.Interval = 500;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);

            this.SelectDataset(0);
        }

        public override void Dispose() {
            base.Dispose();

            this.buzzerHandler.BuzUnit_Buzzered -= this.buzzerHandler_BuzUnit_Buzzered;
            this.buzzerHandler.Unit_ConnectionStatusChanged -= this.buzzerHandler_UnitConnectionStatusChanged;
            this.buzzerHandler.Unit_ConnectionStatusRequest -= this.buzzerHandler_UnitConnectionStatusRequest;
            this.buzzerHandler.Unit_InfoListChanged -= this.buzzerHandler_UnitInfoListChanged;
            this.buzzerHandler.BuzUnit_WorkmodeChanged -= this.buzzerHandler_BuzUnit_WorkmodeChanged;
            this.buzzerHandler.BuzUnit_WorkmodeRequest -= this.buzzerHandler_BuzUnit_WorkmodeRequest;
            this.buzzerHandler = null;

            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.TimeToBeat.StopFired -= this.timeToBeat_StopFired;
            this.insertScene.TimeToBeat.PropertyChanged -= this.timeToBeat_PropertyChanged;
        }

        public override void ResetData() {
            this.toggleTimer.Stop();
            base.ResetData();
            this.TimeToBeat = null;
            this.timeIsRunning = false;
            this.TimeToBeatSentenceTime = 0;
            this.BuzzerIndex = -1;
            this.PanelOff(StepPositions.Top);
            this.PanelOff(StepPositions.Left);
            this.PanelOff(StepPositions.Right);
            this.PanelOff(StepPositions.Bottom);
            this.SelectDataset(0);
        }

        public void AddSentence() {
            this.TimeToBeatSentenceTime += this.SentenceTime;
        }

        public void NextPlayer() {
            this.toggleTimer.Stop();
            if (this.SelectedPlayer == PlayerSelection.LeftPlayer) this.SelectedPlayer = PlayerSelection.RightPlayer;
            else this.SelectedPlayer = PlayerSelection.LeftPlayer;
            this.TimeToBeatSentenceTime = 0;
            this.Vinsert_SetTimeToBeat();
            this.BuzzerIndex = -1;
            this.PanelOff(StepPositions.Top);
            this.PanelOff(StepPositions.Left);
            this.PanelOff(StepPositions.Right);
            this.PanelOff(StepPositions.Bottom);
        }

        public override void Next() {
            this.toggleTimer.Stop();
            this.TimeToBeat = null;
            this.timeIsRunning = false;
            this.TimeToBeatSentenceTime = 0;
            this.BuzzerIndex = -1;
            this.PanelOff(StepPositions.Top);
            this.PanelOff(StepPositions.Left);
            this.PanelOff(StepPositions.Right);
            this.PanelOff(StepPositions.Bottom);
            this.SelectDataset(this.SelectedDatasetIndex + 1);
        }

        private void setRemaining() {
            int buzzerIndex = this.BuzzerIndex;
            if (buzzerIndex < 0) buzzerIndex = 0;
            if (this.SelectedDataset is DatasetContent) this.Remaining = this.SelectedDataset.StepList.Length - buzzerIndex;
            else this.Remaining = 0;
        }

        private void fillBuzzerUnitList(
            InfoParam[] unitInfoList) {
            this.ioUnitNameList.Clear();
            if (unitInfoList is InfoParam[]) {
                foreach (InfoParam item in unitInfoList) {
                    if (item is InfoParam) this.ioUnitNameList.Add(item.Name);
                }
            }
            this.on_PropertyChanged("IOUnitList");
        }

        private void checkIOUnitStatus() {
            BuzzerUnitStates ioUnitStatus = BuzzerUnitStates.NotAvailable;
            if (this.ioUnitNameList.Contains(this.IOUnitName)) {
                switch (this.ioUnitConnectionStatus) {
                    case Tools.NetContact.ClientStates.Connected:
                        ioUnitStatus = BuzzerUnitStates.Connected;
                        break;
                    case Tools.NetContact.ClientStates.Connecting:
                        ioUnitStatus = BuzzerUnitStates.Connecting;
                        break;
                    case Tools.NetContact.ClientStates.Disconnected:
                        ioUnitStatus = BuzzerUnitStates.Disconnected;
                        break;
                    case Tools.NetContact.ClientStates.Missing:
                    default:
                        ioUnitStatus = BuzzerUnitStates.Missing;
                        break;
                }
                if (ioUnitStatus == BuzzerUnitStates.Connected) {
                    switch (this.ioUnitWorkMode) {
                        case WorkModes.BUZZER:
                            ioUnitStatus = BuzzerUnitStates.BuzzerMode;
                            break;
                        case WorkModes.EVENT:
                            ioUnitStatus = BuzzerUnitStates.EventMode;
                            break;
                        case WorkModes.LOCK:
                            ioUnitStatus = BuzzerUnitStates.Locked;
                            break;
                        case WorkModes.NA:
                        default:
                            break;
                    }
                }
            }
            if (this.IOUnitStatus != ioUnitStatus) {
                this.IOUnitStatus = ioUnitStatus;
                this.on_PropertyChanged("IOUnitStatus");
            }
        }

        private void requestIOUnitStates(
            string unitName) {
            if (buzzerHandler is BuzzerIO.Business) {
                this.buzzerHandler.RequestUnitConnectionStatus(unitName);
                this.buzzerHandler.RequestUnitWorkMode(unitName);
            }
        }

        public void StartGame() {
            this.toggleTimer.Stop();
            this.BuzzerIndex = -1;
            this.NextBuzzer(); 
        }

        public void NextBuzzer() {
            int count = 0;
            if (this.SelectedDataset is DatasetContent) count = this.SelectedDataset.StepList.Length;
            if (this.BuzzerIndex < count -1) {
                switch (this.ActiveBuzzer) {
                    case StepPositions.Top:
                        if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.PlaySoundHitPanel();
                        this.PanelOff(StepPositions.Top);                        
                        //this.PanelOff(StepPositions.Left);
                        //this.PanelOff(StepPositions.Right);
                        //this.PanelOff(StepPositions.Bottom);                        
                        break;
                    case StepPositions.Left:
                        if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.PlaySoundHitPanel();
                        //this.PanelOff(StepPositions.Top);
                        this.PanelOff(StepPositions.Left);
                        //this.PanelOff(StepPositions.Right);
                        //this.PanelOff(StepPositions.Bottom);
                        break;
                    case StepPositions.Right:
                        if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.PlaySoundHitPanel();
                        //this.PanelOff(StepPositions.Top);
                        //this.PanelOff(StepPositions.Left);
                        this.PanelOff(StepPositions.Right);
                        //this.PanelOff(StepPositions.Bottom);
                        break;
                    case StepPositions.Bottom:
                        if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.PlaySoundHitPanel();
                        //this.PanelOff(StepPositions.Top);
                        //this.PanelOff(StepPositions.Left);
                        //this.PanelOff(StepPositions.Right);
                        this.PanelOff(StepPositions.Bottom);
                        break;
                    case StepPositions.NA:
                    default:
                        //this.PanelOff(StepPositions.Top);
                        //this.PanelOff(StepPositions.Left);
                        //this.PanelOff(StepPositions.Right);
                        //this.PanelOff(StepPositions.Bottom);
                        break;
                }
                this.BuzzerIndex++;
                this.PanelOn(this.ActiveBuzzer);
                //if (this.ShowNextPanel) this.PanelNext(this.NextActiveBuzzer);
                this.ReleaseBuzzer();
                //if (this.NextActiveBuzzer == StepPositions.NA) this.toggleTimer.Start();
            }
            else {
                this.StopGame();        
                if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.PlaySoundFinished();
                Helper.invokeActionAfterDelay(() => this.midiHandler.SendEvent("Finish"), 500, this.syncContext);
                this.Remaining = 0;
            }
        }

        public void StopGame() {
            this.toggleTimer.Stop();
            this.Vinsert_StopTimeToBeat();
            this.LockBuzzer();
            this.PanelOff(StepPositions.Top);
            this.PanelOff(StepPositions.Left);
            this.PanelOff(StepPositions.Right);
            this.PanelOff(StepPositions.Bottom);
        }

        private void setDMXValues() {
            if (this.buzzerHandler is BuzzerIO.Business) {
                this.buzzerHandler.SetDMXMode(this.IOUnitName, IOnet.IOUnit.IONbuzDX.DMXBuzzerModes.OFF);
                byte[] valueList = new byte[12];
                this.setDMXValues(1, valueList);
                byte[] offValue = new byte[] { this.OffColor.R, this.OffColor.G, this.OffColor.B };
                byte[] nextValue = new byte[] { this.NextColor.R, this.NextColor.G, this.NextColor.B };
                byte[] onValue = new byte[] { this.OnColor.R, this.OnColor.G, this.OnColor.B };
                Array.Copy(offValue, 0, valueList, 0, offValue.Length);
                Array.Copy(offValue, 0, valueList, 3, offValue.Length);
                Array.Copy(offValue, 0, valueList, 6, offValue.Length);
                Array.Copy(offValue, 0, valueList, 9, offValue.Length);
                switch (this.ActiveBuzzer) {
                    case StepPositions.Top:
                        Array.Copy(onValue, 0, valueList, 0, onValue.Length);
                        break;
                    case StepPositions.Left:
                        Array.Copy(onValue, 0, valueList, 3, onValue.Length);
                        break;
                    case StepPositions.Right:
                        Array.Copy(onValue, 0, valueList, 6, onValue.Length);
                        break;
                    case StepPositions.Bottom:
                        Array.Copy(onValue, 0, valueList, 9, onValue.Length);
                        break;
                }
                switch (this.NextActiveBuzzer) {
                    case StepPositions.Top:
                        Array.Copy(onValue, 0, valueList, 0, onValue.Length);
                        break;
                    case StepPositions.Left:
                        Array.Copy(onValue, 0, valueList, 3, onValue.Length);
                        break;
                    case StepPositions.Right:
                        Array.Copy(onValue, 0, valueList, 6, onValue.Length);
                        break;
                    case StepPositions.Bottom:
                        Array.Copy(onValue, 0, valueList, 9, onValue.Length);
                        break;
                }
                this.setDMXValues(1, valueList);
                this.buzzerHandler.SetDMXChannel(this.IOUnitName, 1, valueList);
            }
        }

        private void setDMXValues(
            int startChannel,
            byte[] values) {
            try {
                byte startIndex = Convert.ToByte(startChannel - 1);
                if (startIndex + values.Length < this.universe.Length) Array.Copy(values, 0, this.universe, startIndex, values.Length);
                this.dMX.SetDmxDataToEspNet(0, 0, this.universe);
            }
            catch (Exception) {
            }
        }

        public virtual void ReleaseBuzzer() {
            bool[] inputMask = new bool[8];
            switch (this.ActiveBuzzer) {
                case StepPositions.Top:
                    if (this.TopBuzzerChannel > 0 &&
                        this.TopBuzzerChannel <= inputMask.Length) inputMask[this.TopBuzzerChannel - 1] = true;
                    break;
                case StepPositions.Left:
                    if (this.LeftBuzzerChannel > 0 &&
                        this.LeftBuzzerChannel <= inputMask.Length) inputMask[this.LeftBuzzerChannel - 1] = true;
                    break;
                case StepPositions.Right:
                    if (this.RightBuzzerChannel > 0 &&
                        this.RightBuzzerChannel <= inputMask.Length) inputMask[this.RightBuzzerChannel - 1] = true;
                    break;
                case StepPositions.Bottom:
                    if (this.BottomBuzzerChannel > 0 &&
                        this.BottomBuzzerChannel <= inputMask.Length) inputMask[this.BottomBuzzerChannel - 1] = true;
                    break;
            }
            this.buzzerHandler.SetInputMask(this.IOUnitName, inputMask);
            this.buzzerHandler.ReleaseBuzzer(this.IOUnitName, WorkModes.BUZZER);
        }

        public virtual void LockBuzzer() {
            this.buzzerHandler.LockBuzzer(this.IOUnitName);
        }

        internal void PanelOn(StepPositions value) {
            this.buzzerHandler.SetDMXMode(this.IOUnitName, IOnet.IOUnit.IONbuzDX.DMXBuzzerModes.OFF);
            byte[] onValue = new byte[] { this.OnColor.R, this.OnColor.G, this.OnColor.B };
            int startChannel = 0;
            switch (value) {
                case StepPositions.Top:
                    startChannel = this.TopPanelDMXStart;
                    break;
                case StepPositions.Left:
                    startChannel = this.LeftPanelDMXStart;
                    break;
                case StepPositions.Right:
                    startChannel = this.RightPanelDMXStart;
                    break;
                case StepPositions.Bottom:
                    startChannel = this.BottomPanelDMXStart;
                    break;
            }
            this.setDMXValues(startChannel, onValue);
            this.buzzerHandler.SetDMXChannel(this.IOUnitName, startChannel, onValue);
            this.midiHandler?.SendEvent($"{value}On");
        }

        internal void PanelHit(StepPositions value) {
            this.buzzerHandler.SetDMXMode(this.IOUnitName, IOnet.IOUnit.IONbuzDX.DMXBuzzerModes.OFF);
            byte[] hitValue = new byte[] { this.HitColor.R, this.HitColor.G, this.HitColor.B };
            int startChannel = 0;
            switch (value) {
                case StepPositions.Top:
                    startChannel = this.TopPanelDMXStart;
                    break;
                case StepPositions.Left:
                    startChannel = this.LeftPanelDMXStart;
                    break;
                case StepPositions.Right:
                    startChannel = this.RightPanelDMXStart;
                    break;
                case StepPositions.Bottom:
                    startChannel = this.BottomPanelDMXStart;
                    break;
            }
            this.setDMXValues(startChannel, hitValue);
            this.buzzerHandler.SetDMXChannel(this.IOUnitName, startChannel, hitValue);
            Helper.invokeActionAfterDelay(() => this.PanelOff(value), 750, this.syncContext);
            this.midiHandler?.SendEvent($"{value}Hit");
        }

        internal void PanelNext(StepPositions value) {
            this.buzzerHandler.SetDMXMode(this.IOUnitName, IOnet.IOUnit.IONbuzDX.DMXBuzzerModes.OFF);
            byte[] nextValue = new byte[] { this.NextColor.R, this.NextColor.G, this.NextColor.B };
            int startChannel = 0;
            switch (value) {
                case StepPositions.Top:
                    startChannel = this.TopPanelDMXStart;
                    break;
                case StepPositions.Left:
                    startChannel = this.LeftPanelDMXStart;
                    break;
                case StepPositions.Right:
                    startChannel = this.RightPanelDMXStart;
                    break;
                case StepPositions.Bottom:
                    startChannel = this.BottomPanelDMXStart;
                    break;
            }
            this.setDMXValues(startChannel, nextValue);
            this.buzzerHandler.SetDMXChannel(this.IOUnitName, startChannel, nextValue);
            this.midiHandler?.SendEvent($"{value}Next");
        }

        internal void PanelOff(StepPositions value) {
            this.buzzerHandler.SetDMXMode(this.IOUnitName, IOnet.IOUnit.IONbuzDX.DMXBuzzerModes.OFF);
            byte[] offValue = new byte[] { this.OffColor.R, this.OffColor.G, this.OffColor.B };
            int startChannel = 0;
            switch (value) {
                case StepPositions.Top:
                    startChannel = this.TopPanelDMXStart;
                    break;
                case StepPositions.Left:
                    startChannel = this.LeftPanelDMXStart;
                    break;
                case StepPositions.Right:
                    startChannel = this.RightPanelDMXStart;
                    break;
                case StepPositions.Bottom:
                    startChannel = this.BottomPanelDMXStart;
                    break;
            }
            this.setDMXValues(startChannel, offValue);
            this.buzzerHandler.SetDMXChannel(this.IOUnitName, startChannel, offValue);
            this.midiHandler?.SendEvent($"{value}Off");
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
            this.setRemaining();
        }

        public void AddDataset(
            DatasetContent newDataset,
            int insertIndex) {
            if (this.tryAddDataset(newDataset, insertIndex)) this.on_PropertyChanged("NameList");
            if (this.SelectedDataset == null) this.SelectDataset(0);
            this.Save();
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
                this.Save();
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
                this.Save();
                return true;
            }
            else return false;
        }

        public void RemoveAllDatasets() {
            if (this.tryRemoveAllDatasets()) {
                this.SelectDataset(0);
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
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                if (index <= this.SelectedDatasetIndex) this.SelectDataset(this.SelectedDatasetIndex);
                this.Save();
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
            foreach (DatasetContent item in this.dataList) {
                this.names.Add(item.ToString());
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
        public override void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score); }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }

        public void Vinsert_TimeToBeatIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_TimeToBeatIn(this.insertScene.TimeToBeat); }
        public void Vinsert_TimeToBeatIn(
            VentuzScenes.GamePool._Modules.TimeToBeat scene) {
            if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) {
                this.Vinsert_SetTimeToBeat(scene, this.SelectedPlayer);

                scene.ToIn();
            }
            this.Vinsert_ResetTimeToBeat(scene);
            this.Vinsert_ResetOffsetTime(scene);
            this.Vinsert_ResetTimeToBeatTime(scene);
        }
        public void Vinsert_SetTimeToBeat() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetTimeToBeat(this.insertScene.TimeToBeat, this.SelectedPlayer); }
        public void Vinsert_SetTimeToBeat(
            VentuzScenes.GamePool._Modules.TimeToBeat scene,
            Content.Gameboard.PlayerSelection selectedPlayer) {
            if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) {
                scene.SetPositionX(this.TimeToBeatPositionX);
                scene.SetPositionY(this.TimeToBeatPositionY);
                scene.SetStopTime(this.TimeToBeatStopTime);
                scene.SetSentenceTime(this.TimeToBeatSentenceTime);
                scene.SetStyle(this.TimeToBeatStyle);
                if (selectedPlayer == Content.Gameboard.PlayerSelection.LeftPlayer) scene.SetName(this.LeftPlayerName);
                else scene.SetName(this.RightPlayerName);
            }
        }
        public void Vinsert_SetTimeToBeatSentenceTime() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetTimeToBeatSentenceTime(this.insertScene.TimeToBeat); }
        public void Vinsert_SetTimeToBeatSentenceTime(VentuzScenes.GamePool._Modules.TimeToBeat scene) { if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) scene.SetSentenceTime(this.TimeToBeatSentenceTime); }
        public void Vinsert_StartTimeToBeat() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_StartTimeToBeat(this.insertScene.TimeToBeat); }
        public void Vinsert_StartTimeToBeat(VentuzScenes.GamePool._Modules.TimeToBeat scene) {
            if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) {
                scene.StartTimer();
                this.timeIsRunning = true;
            }
        }
        public void Vinsert_ShowOffsetTime(float offset) { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ShowOffsetTime(this.insertScene.TimeToBeat, offset); }
        public void Vinsert_ShowOffsetTime(
            VentuzScenes.GamePool._Modules.TimeToBeat scene,
            float offset) {
            if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) {
                scene.SetOffset(offset);
                scene.ShowOffset();
            }
        }
        public void Vinsert_ResetOffsetTime() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ResetOffsetTime(this.insertScene.TimeToBeat); }
        public void Vinsert_ResetOffsetTime(VentuzScenes.GamePool._Modules.TimeToBeat scene) { if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) scene.ResetOffset(); }
        public void Vinsert_ShowTimeToBeatTime() { if (this.insertScene is VRemote4.HandlerSi.Scene && this.timeToBeat.HasValue) this.Vinsert_ShowTimeToBeatTime(this.insertScene.TimeToBeat, Convert.ToSingle(this.timeToBeat.Value)); }
        public void Vinsert_ShowTimeToBeatTime(float timeToBeat) { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ShowTimeToBeatTime(this.insertScene.TimeToBeat, Convert.ToSingle(timeToBeat)); }
        public void Vinsert_ShowTimeToBeatTime(
            VentuzScenes.GamePool._Modules.TimeToBeat scene,
            float timeToBeat) {
            if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) {
                scene.SetTimeToBeat(timeToBeat);
                scene.ShowTimeToBeat();
            }
        }
        public void Vinsert_ResetTimeToBeatTime() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ResetTimeToBeatTime(this.insertScene.TimeToBeat); }
        public void Vinsert_ResetTimeToBeatTime(VentuzScenes.GamePool._Modules.TimeToBeat scene) { if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) scene.ResetTimeToBeat(); }
        public void Vinsert_StopTimeToBeat() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_StopTimeToBeat(this.insertScene.TimeToBeat); }
        public void Vinsert_StopTimeToBeat(VentuzScenes.GamePool._Modules.TimeToBeat scene) { if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) { scene.StopTimer(); } }
        public void Vinsert_ContinueTimeToBeat() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ContinueTimeToBeat(this.insertScene.TimeToBeat); }
        public void Vinsert_ContinueTimeToBeat(VentuzScenes.GamePool._Modules.TimeToBeat scene) {
            if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) {
                scene.ContinueTimer();
                this.timeIsRunning = true;
            }
        }
        public void Vinsert_ResetTimeToBeat() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ResetTimeToBeat(this.insertScene.TimeToBeat); }
        public void Vinsert_ResetTimeToBeat(VentuzScenes.GamePool._Modules.TimeToBeat scene) { if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) scene.ResetTimer(); }
        public void Vinsert_TimeToBeatOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_TimeToBeatOut(this.insertScene.TimeToBeat); }
        public void Vinsert_TimeToBeatOut(
            VentuzScenes.GamePool._Modules.TimeToBeat scene) {
            if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) {
                scene.ToOut();
                this.Vinsert_ResetOffsetTime(scene);
                this.Vinsert_ResetTimeToBeatTime(scene);
            }
        }

        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public void Vfullscreen_SetTimer(
            int startTime) {
            if (this.fullscreenMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.fullscreenMasterScene.Timer.SetStyle(VentuzScenes.Fullscreen.Clock.Styles.Sec);
                this.fullscreenMasterScene.Timer.SetStartTime(startTime);
            }
        }

        public override void ClearGraphic() {
            base.ClearGraphic();
            this.insertScene.Clear();
        }

        #endregion


        #region Events.Outgoing

        public event EventHandler TimeToBeatStopFired;
        protected void on_TimeToBeatStopFired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.TimeToBeatStopFired, e); }

        #endregion

        #region Events.Incoming

        void buzzerHandler_BuzUnit_Buzzered(object sender, BuzzerIDParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_Buzzered);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_BuzUnit_Buzzered(object content) {
            BuzzerIDParam_EventArgs e = content as BuzzerIDParam_EventArgs;
            if (this.isActive &&
                e is BuzzerIDParam_EventArgs &&
                e.Arg.Name == this.IOUnitName) {
                this.NextBuzzer();
            }
        }

        void buzzerHandler_UnitConnectionStatusChanged(object sender, ConnectionStatusParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_UnitConnectionStatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        void buzzerHandler_UnitConnectionStatusRequest(object sender, ConnectionStatusParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_UnitConnectionStatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_UnitConnectionStatusChanged(object content) {
            ConnectionStatusParam_EventArgs e = content as ConnectionStatusParam_EventArgs;
            if (e is ConnectionStatusParam_EventArgs &&
                e.Arg.Name == this.IOUnitName) {
                this.ioUnitConnectionStatus = e.Arg.ConnectionStatus;
                this.checkIOUnitStatus();
                if (e.Arg.ConnectionStatus == Tools.NetContact.ClientStates.Connected) this.setDMXValues();
            }
        }

        void buzzerHandler_UnitInfoListChanged(object sender, InfoParamArray_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_UnitInfoListChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_UnitInfoListChanged(object content) {
            InfoParamArray_EventArgs e = content as InfoParamArray_EventArgs;
            if (e is InfoParamArray_EventArgs) {
                this.ioUnitInfo = e;
                this.fillBuzzerUnitList(e.Arg);
            }
        }

        void buzzerHandler_BuzUnit_WorkmodeChanged(object sender, WorkModeParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_WorkmodeChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        void buzzerHandler_BuzUnit_WorkmodeRequest(object sender, WorkModeParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_WorkmodeChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_BuzUnit_WorkmodeChanged(object content) {
            WorkModeParam_EventArgs e = content as WorkModeParam_EventArgs;
            if (e is WorkModeParam_EventArgs &&
                e.Arg.Name == this.IOUnitName) {
                this.ioUnitWorkMode = e.Arg.WorkMode;
                this.checkIOUnitStatus();
            }
        }

        protected void timeToBeat_StopFired(object sender, EventArgs e) {
            this.on_TimeToBeatStopFired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timeToBeat_StopFired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timeToBeat_StopFired(object content) {
            if (this.timeIsRunning) {
                if (this.timeToBeat.HasValue) {
                    // zweiter Durchgang, der Offset wird ermittelt
                    double currentTime = this.insertScene.TimeToBeat.CurrentTime;
                    double offset = currentTime - this.timeToBeat.Value;
                    this.Vinsert_ShowOffsetTime(Convert.ToSingle(offset));
                }
                else {
                    // erster Durchgang, die TimeToBeat wird ermittelt
                    if (this.insertScene.TimeToBeat.CurrentTime > 0) {
                        this.timeToBeat = this.insertScene.TimeToBeat.CurrentTime;
                        this.on_PropertyChanged("TimeToBeat");
                    }
                }
            }
            this.timeIsRunning = false;
        }

        protected void timeToBeat_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timeToBeat_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timeToBeat_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "CurrentTime") this.TimeToBeatCurrentTime = Convert.ToDouble(this.insertScene.TimeToBeat.CurrentTime);
            }
        }

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

        private void toggleTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_toggleTimer_Elapsed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_toggleTimer_Elapsed(object content) {
            this.toggleOn = !this.toggleOn;
            if (toggleOn) this.PanelOn(this.ActiveBuzzer);
            else this.PanelOff(this.ActiveBuzzer);
            this.toggleTimer.Start();
        }

        #endregion

    }
}
