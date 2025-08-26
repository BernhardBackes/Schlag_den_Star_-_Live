using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cliparts.Serialization;

using Cliparts.IOnet.IOUnit.IONbuz;

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;
using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimerForTwoSoloScoreCounter;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimerForTwoSoloScoreCounter {

    public class SingleDot : INotifyPropertyChanged {

        #region Properties

        private VentuzScenes.GamePool._Modules.TaskCounter.DotStates status = VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Off;
        public VentuzScenes.GamePool._Modules.TaskCounter.DotStates Status {
            get { return this.status; }
            set {
                if (this.status != value) {
                    this.status = value;
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion


        #region Funktionen

        public SingleDot() { }
        public SingleDot(VentuzScenes.GamePool._Modules.TaskCounter.DotStates status) { this.Status = status; }

        public void Reset() { this.Status = VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Off; }

        public void Clone(
            SingleDot dot) {
            if (dot is SingleDot) {
                this.Status = dot.Status;
            }
            else this.Reset();
        }

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

    public class Business : _Base.Score.Business {

        #region Properties

        private AMB.Business ambHandler;

        public string[] TimelineNameList {
            get {
                if (this.ambHandler is AMB.Business) return this.ambHandler.DecoderList;
                else return new string[] { };
            }
        }

        private string timelineName = string.Empty;
        public string TimelineName {
            get { return this.timelineName; }
            set {
                if (this.timelineName != value) {
                    if (value == null) value = string.Empty;
                    this.timelineName = value;
                    this.on_PropertyChanged();
                    this.checkTimelineStatus();
                }
            }
        }

        private AMB.TimelineStates timelineStatus = AMB.TimelineStates.Offline;
        public AMB.TimelineStates TimelineStatus {
            get { return this.timelineStatus; }
            set {
                if (this.timelineStatus != value) {
                    this.timelineStatus = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string leftPlayerTransponderCode = string.Empty;
        public string LeftPlayerTransponderCode {
            get { return this.leftPlayerTransponderCode; }
            set {
                if (this.leftPlayerTransponderCode != value) {
                    if (string.IsNullOrEmpty(value)) this.leftPlayerTransponderCode = string.Empty;
                    else this.leftPlayerTransponderCode = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string rightPlayerTransponderCode = string.Empty;
        public string RightPlayerTransponderCode {
            get { return this.rightPlayerTransponderCode; }
            set {
                if (this.rightPlayerTransponderCode != value) {
                    if (string.IsNullOrEmpty(value)) this.rightPlayerTransponderCode = string.Empty;
                    else this.rightPlayerTransponderCode = value;
                    this.on_PropertyChanged();
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

        private double timerCurrentTime = -1;
        public double TimerCurrentTime {
            get { return this.timerCurrentTime; }
            protected set {
                if (this.timerCurrentTime != value) {
                    this.timerCurrentTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private double timerTimeToBeat = -1;
        public double TimerTimeToBeat {
            get { return this.timerTimeToBeat; }
            protected set {
                if (this.timerTimeToBeat != value) {
                    this.timerTimeToBeat = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private BuzzerIO.Business buzzerHandler;

        private List<string> ioUnitNameList = new List<string>();
        public string[] IOUnitNameList { get { return this.ioUnitNameList.ToArray(); } }

        private IOnet.IOUnit.IONbase.InfoParamArray_EventArgs ioUnitInfo;

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

        private int leftPlayerBuzzerChannel = 1;
        public int LeftPlayerBuzzerChannel {
            get { return this.leftPlayerBuzzerChannel; }
            set {
                if (this.leftPlayerBuzzerChannel != value) {
                    if (value < 1) value = 1;
                    if (value > 8) value = 8;
                    this.leftPlayerBuzzerChannel = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerBuzzerChannel = 2;
        public int RightPlayerBuzzerChannel {
            get { return this.rightPlayerBuzzerChannel; }
            set {
                if (this.rightPlayerBuzzerChannel != value) {
                    if (value < 1) value = 1;
                    if (value > 8) value = 8;
                    this.rightPlayerBuzzerChannel = value;
                    this.on_PropertyChanged();
                }
            }
        }

        public enum FinishModes { Reaching, Crossing }
        private FinishModes finishMode = FinishModes.Reaching;
        public FinishModes FinishMode {
            get { return this.finishMode; }
            set {
                if (this.finishMode != value) {
                    this.finishMode = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool leftFinishReached = false;
        public bool LeftFinishReached {
            get { return this.leftFinishReached; }
            set {
                if (this.leftFinishReached != value) {
                    this.leftFinishReached = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool rightFinishReached = false;
        public bool RightFinishReached {
            get { return this.rightFinishReached; }
            set {
                if (this.rightFinishReached != value) {
                    this.rightFinishReached = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool swapTracks;
        public bool SwapTracks {
            get { return this.swapTracks; }
            set {
                if (this.swapTracks != value) {
                    this.swapTracks = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private PlayerSelection buzzeredPlayer = PlayerSelection.NotSelected;

        private PlayerSelection firstFinisher = PlayerSelection.NotSelected;
        public PlayerSelection FirstFinisher {
            get { return this.firstFinisher; }
            private set {
                if (this.firstFinisher != value) {
                    this.firstFinisher = value;
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
                    if (value < 1) value = 1;
                    this.taskCounter = value;
                    this.on_PropertyChanged();
                }
            }
        }

        protected List<SingleDot> taskCounterPenaltyDots = new List<SingleDot>();
        public SingleDot[] TaskCounterPenaltyDots {
            get {
                this.fillPenaltyDots();
                return this.taskCounterPenaltyDots.ToArray();
            }
            set {
                this.fillPenaltyDots();
                for (int i = 0; i < TaskCounterPenaltyCountMax; i++) {
                    if (value is SingleDot[] &&
                        value.Length > i) this.taskCounterPenaltyDots[i].Clone(value[i]);
                    else this.taskCounterPenaltyDots[i].Reset();
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

        private bool timeIsRunning = false;

        #endregion


        #region Funktionen

        public Business() { }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.TimerForTwoSoloScoreCounter'", typeIdentifier);
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

            this.ambHandler = ambHandler;
            this.ambHandler.Error += this.on_Error;
            this.ambHandler.FirstContact += this.ambHandler_FirstContact;
            this.ambHandler.Passed += this.ambHandler_Passed;
            this.ambHandler.PropertyChanged += this.ambHandler_PropertyChanged;

            this.buzzerHandler = buzzerHandler;
            this.buzzerHandler.BuzUnit_Buzzered += this.buzzerHandler_BuzUnit_Buzzered;
            this.buzzerHandler.BuzUnit_InputChannelChanged += this.buzzerHandler_BuzUnit_InputChannelChanged;
            this.buzzerHandler.Unit_ConnectionStatusChanged += this.buzzerHandler_UnitConnectionStatusChanged;
            this.buzzerHandler.Unit_ConnectionStatusRequest += this.buzzerHandler_UnitConnectionStatusRequest;
            this.buzzerHandler.Unit_InfoListChanged += this.buzzerHandler_UnitInfoListChanged;
            this.buzzerHandler.BuzUnit_WorkmodeChanged += this.buzzerHandler_BuzUnit_WorkmodeChanged;
            this.buzzerHandler.BuzUnit_WorkmodeRequest += this.buzzerHandler_BuzUnit_WorkmodeRequest;
            this.fillIOUnitList(this.buzzerHandler.UnitInfoList);
            this.buzzerHandler.RequestUnitConnectionStatus(this.IOUnitName);
            this.buzzerHandler.RequestUnitWorkMode(this.IOUnitName);

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;
            this.insertScene.StopFired += this.insertScene_StopFired;
            this.insertScene.PreciseTimeReceived += this.insertScene_PreciseTimeReceived;
            this.insertScene.PropertyChanged += this.insertScene_PropertyChanged;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);
        }

        public override void Dispose() {
            base.Dispose();

            this.ambHandler.Dispose();
            this.ambHandler.Error -= this.on_Error;
            this.ambHandler.FirstContact -= this.ambHandler_FirstContact;
            this.ambHandler.Passed -= this.ambHandler_Passed;
            this.ambHandler.PropertyChanged -= this.ambHandler_PropertyChanged;

            this.buzzerHandler.BuzUnit_Buzzered -= this.buzzerHandler_BuzUnit_Buzzered;
            this.buzzerHandler.Unit_ConnectionStatusChanged -= this.buzzerHandler_UnitConnectionStatusChanged;
            this.buzzerHandler.Unit_ConnectionStatusRequest -= this.buzzerHandler_UnitConnectionStatusRequest;
            this.buzzerHandler.Unit_InfoListChanged -= this.buzzerHandler_UnitInfoListChanged;
            this.buzzerHandler.BuzUnit_WorkmodeChanged -= this.buzzerHandler_BuzUnit_WorkmodeChanged;
            this.buzzerHandler.BuzUnit_WorkmodeRequest -= this.buzzerHandler_BuzUnit_WorkmodeRequest;
            this.buzzerHandler = null;

            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.StopFired -= this.insertScene_StopFired;
            this.insertScene.PreciseTimeReceived -= this.insertScene_PreciseTimeReceived;
            this.insertScene.PropertyChanged -= this.insertScene_PropertyChanged;
        }

        public override void ResetData() {
            base.ResetData();
            this.TimeToBeat = null;
            this.timeIsRunning = false;
            this.buzzeredPlayer = PlayerSelection.NotSelected;
            this.FirstFinisher = PlayerSelection.NotSelected;
            this.LeftFinishReached = false;
            this.RightFinishReached = false;
            this.TaskCounter = 1;
            this.ResetPenaltyDots();
        }

        protected void fillPenaltyDots() {
            while (this.taskCounterPenaltyDots.Count < TaskCounterPenaltyCountMax) {
                SingleDot item = new SingleDot();
                item.PropertyChanged += this.dot_PropertyChanged;
                this.taskCounterPenaltyDots.Add(item);
            }
        }

        public bool TryGetDot(
            int index,
            out SingleDot dot) {
            if (index >= 0 &&
                index < this.TaskCounterPenaltyDots.Length &&
                this.TaskCounterPenaltyDots[index] is SingleDot) {
                dot = this.TaskCounterPenaltyDots[index];
                return true;
            }
            else {
                dot = null;
                return false;
            }
        }

        public override void Next() {
            this.TimeToBeat = null;
            this.timeIsRunning = false;
            this.buzzeredPlayer = PlayerSelection.NotSelected;
            this.FirstFinisher = PlayerSelection.NotSelected;
            this.LeftFinishReached = false;
            this.RightFinishReached = false;
            this.TaskCounter++;
            this.Vinsert_SetTaskCounter();
        }

        public void ResetPenaltyDots() {
            foreach (SingleDot item in this.taskCounterPenaltyDots) item.Reset();
        }

        protected void clearPenalty() {
            foreach (SingleDot item in this.taskCounterPenaltyDots) item.PropertyChanged -= this.dot_PropertyChanged;
        }

        private void checkTimelineStatus() {
            if (!string.IsNullOrEmpty(this.TimelineName) &&
                this.TimelineNameList.Contains(this.TimelineName)) {
                if (this.TimelineStatus == AMB.TimelineStates.Offline) this.TimelineStatus = AMB.TimelineStates.Locked;
            }
            else this.TimelineStatus = AMB.TimelineStates.Offline;
        }

        public void LockTimeline() {
            if (this.TimelineStatus == AMB.TimelineStates.Unlocked) this.TimelineStatus = AMB.TimelineStates.Locked;
        }

        public void ReleaseTimeline() {
            if (this.TimelineStatus == AMB.TimelineStates.Locked) this.TimelineStatus = AMB.TimelineStates.Unlocked;
        }

        private void fillIOUnitList(
            Cliparts.IOnet.IOUnit.IONbase.InfoParam[] unitInfoList) {
            this.ioUnitNameList.Clear();
            if (unitInfoList is IOnet.IOUnit.IONbase.InfoParam[]) {
                foreach (IOnet.IOUnit.IONbase.InfoParam item in unitInfoList) {
                    if (item is IOnet.IOUnit.IONbase.InfoParam) this.ioUnitNameList.Add(item.Name);
                }
            }
            this.on_PropertyChanged("IOUnitNameList");
        }

        private void checkIOUnitStatus() {
            BuzzerIO.BuzzerUnitStates ioUnitStatus = BuzzerUnitStates.NotAvailable;
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
            if (this.buzzerHandler is BuzzerIO.Business) {
                this.buzzerHandler.RequestUnitConnectionStatus(unitName);
                this.buzzerHandler.RequestUnitWorkMode(unitName);
            }
        }

        private void setDMXValues() {
            this.buzzerHandler.SetDMXMode(this.IOUnitName, IOnet.IOUnit.IONbuzDX.DMXBuzzerModes.CHANNEL);
            this.buzzerHandler.SetDMXSettings(this.IOUnitName, 0, 255);
        }

        public virtual void PassFinishLine(
            PlayerSelection buzzeredPlayer) {
            if (buzzeredPlayer != PlayerSelection.NotSelected &&
                buzzeredPlayer != this.buzzeredPlayer) {
                if (this.buzzeredPlayer == PlayerSelection.NotSelected) {
                    this.buzzeredPlayer = buzzeredPlayer;
                    if (this.SwapTracks) {
                        switch (buzzeredPlayer) {
                            case PlayerSelection.LeftPlayer:
                                this.FirstFinisher = PlayerSelection.RightPlayer;
                                break;
                            case PlayerSelection.RightPlayer:
                                this.FirstFinisher = PlayerSelection.LeftPlayer;
                                break;
                        }
                    }
                    else this.FirstFinisher = buzzeredPlayer;
                    this.Vinsert_GetPreciseTime();
                    if (this.FirstFinisher == PlayerSelection.LeftPlayer)
                        this.insertScene.FinishToInTop();
                    else if (this.FirstFinisher == PlayerSelection.RightPlayer)
                        this.insertScene.FinishToInBottom();
                }
                else {
                    this.Vinsert_StopTimer();
                    this.LockTimeline();
                    this.LockBuzzer();
                }
            }
        }

        public virtual void ReleaseBuzzer() {
            bool[] inputMask = new bool[8];
            if (this.LeftPlayerBuzzerChannel > 0 &&
                this.LeftPlayerBuzzerChannel <= inputMask.Length) inputMask[this.LeftPlayerBuzzerChannel - 1] = true;
            if (this.RightPlayerBuzzerChannel > 0 &&
                this.RightPlayerBuzzerChannel <= inputMask.Length) inputMask[this.RightPlayerBuzzerChannel - 1] = true;
            this.buzzerHandler.SetInputMask(this.IOUnitName, inputMask);
            this.buzzerHandler.ReleaseBuzzer(this.IOUnitName, IOnet.IOUnit.IONbuz.WorkModes.EVENT);
        }
        public virtual void ReleaseBuzzer(
            PlayerSelection track) {
            bool[] inputMask = new bool[8];
            switch (track) {
                case PlayerSelection.LeftPlayer:
                    if (this.LeftPlayerBuzzerChannel > 0 &&
                        this.LeftPlayerBuzzerChannel <= inputMask.Length) inputMask[this.LeftPlayerBuzzerChannel - 1] = true;
                    break;
                case PlayerSelection.RightPlayer:
                    if (this.RightPlayerBuzzerChannel > 0 &&
                        this.RightPlayerBuzzerChannel <= inputMask.Length) inputMask[this.RightPlayerBuzzerChannel - 1] = true;
                    break;
            }
            this.buzzerHandler.SetInputMask(this.IOUnitName, inputMask);
            this.buzzerHandler.ReleaseBuzzer(this.IOUnitName, IOnet.IOUnit.IONbuz.WorkModes.EVENT);
        }

        public virtual void LockBuzzer() {
            this.buzzerHandler.LockBuzzer(this.IOUnitName);
        }

        internal void Resolve() {
            SingleDot dot;
            switch (this.FirstFinisher) {
                case PlayerSelection.LeftPlayer:
                    this.LeftPlayerScore++;
                    if (this.TryGetDot(this.TaskCounter - 1, out dot)) dot.Status = VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Red;
                    break;
                case PlayerSelection.RightPlayer:
                    this.RightPlayerScore++;
                    if (this.TryGetDot(this.TaskCounter - 1, out dot)) dot.Status = VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Blue;
                    break;
            }
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }
        public void Vinsert_TimerIn() {
            this.Vinsert_SetTimer();
            this.Vinsert_ResetTimer();
            this.Vinsert_SetShowOffsetOut();
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.ToIn();
        }
        public void Vinsert_SetTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetTimer(this.insertScene); }
        public void Vinsert_SetTimer(
            Insert scene) {
            if (scene is Insert) {
                scene.SetPositionX(this.TimerPositionX);
                scene.SetPositionY(this.TimerPositionY);
                scene.SetTopName(this.LeftPlayerName);
                scene.SetBottomName(this.RightPlayerName);
            }
        }
        public void Vinsert_StartTimer() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.StartTimer();
                this.timeIsRunning = true;
            }
        }
        public void Vinsert_GetPreciseTime() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.GetPreciseTime();
        }
        public void Vinsert_StopTimer() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.StopTimer();
        }
        public void Vinsert_ContinueTimer() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.ContinueTimer(); 
        }
        public void Vinsert_ResetTimer() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.ResetTimer();
        }
        public void Vinsert_SetShowOffsetOut() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.SetOffsetOut();
        }
        public void Vinsert_ShowOffsetTimeTop(
            float offset) {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.SetOffsetValue(offset);
                this.insertScene.OffsetToInTop();
            }
        }
        public void Vinsert_ShowOffsetTimeBottom(
            float offset) {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.SetOffsetValue(offset);
                this.insertScene.OffsetToInBottom();
            }
        }
        public void Vinsert_TimerOut() { 
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.ToOut();
        }
        public override void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score); }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }

        public void Vinsert_TaskCounterIn() {
            this.Vinsert_SetTaskCounter();
            if (this.insertScene is Insert &&
                this.insertScene.TaskCounter is VentuzScenes.GamePool._Modules.TaskCounter) this.insertScene.TaskCounter.ToIn();
        }
        public void Vinsert_SetTaskCounter() { if (this.insertScene is Insert) this.Vinsert_SetTaskCounter(this.insertScene.TaskCounter, this.TaskCounterPenaltyDots, this.TaskCounter); }
        public void Vinsert_SetTaskCounter(
            VentuzScenes.GamePool._Modules.TaskCounter scene,
            SingleDot[] taskCounterPenaltyDots,
            int counter) {
            if (scene is VentuzScenes.GamePool._Modules.TaskCounter) {
                scene.SetPositionX(this.TaskCounterPositionX);
                scene.SetPositionY(this.TaskCounterPositionY);
                scene.SetStyle(VentuzScenes.GamePool._Modules.TaskCounter.Styles.Penalty);
                scene.SetSize(this.TaskCounterSize);
                int id = 1;
                foreach (SingleDot item in taskCounterPenaltyDots) {
                    scene.SetDot(id, item.Status);
                    id++;
                }
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

        void ambHandler_FirstContact(object sender, AMB.FirstContactArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ambHandler_FirstContact);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ambHandler_FirstContact(object content) {
            AMB.FirstContactArgs e = content as AMB.FirstContactArgs;
            if (this.isActive &&
                e is AMB.FirstContactArgs &&
                e.Timeline == this.TimelineName) {
                if (e.Data.Transponder.ToString() == this.LeftPlayerTransponderCode) this.PassFinishLine(PlayerSelection.LeftPlayer);
                else if (e.Data.Transponder.ToString() == this.RightPlayerTransponderCode) this.PassFinishLine(PlayerSelection.RightPlayer);
            }
        }

        void ambHandler_Passed(object sender, AMB.PassingArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ambHandler_Passed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ambHandler_Passed(object content) {
            AMB.PassingArgs e = content as AMB.PassingArgs;
            if (this.isActive &&
                e is AMB.PassingArgs &&
                e.Timeline == this.TimelineName &&
                this.TimelineStatus == AMB.TimelineStates.Unlocked) {
                if (e.Data.TransponderCode == this.LeftPlayerTransponderCode) this.PassFinishLine(PlayerSelection.LeftPlayer);
                else if (e.Data.TransponderCode == this.RightPlayerTransponderCode) this.PassFinishLine(PlayerSelection.RightPlayer);
            }
        }

        void ambHandler_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ambHandler_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ambHandler_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "DecoderList") this.checkTimelineStatus();
            }
        }

        void buzzerHandler_BuzUnit_Buzzered(object sender, BuzzerIDParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_Buzzered);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_BuzUnit_Buzzered(object content) {
            BuzzerIDParam_EventArgs e = content as BuzzerIDParam_EventArgs;
            if (this.isActive &&
                e is BuzzerIDParam_EventArgs &&
                e.Arg.Name == this.IOUnitName) {
                if (e.Arg.BuzzerID == this.LeftPlayerBuzzerChannel) {
                    if (this.FinishMode == FinishModes.Reaching) this.PassFinishLine(PlayerSelection.LeftPlayer);
                    this.LeftFinishReached = true;
                }
                else if (e.Arg.BuzzerID == this.RightPlayerBuzzerChannel) {
                    if (this.FinishMode == FinishModes.Reaching) this.PassFinishLine(PlayerSelection.RightPlayer);
                    this.RightFinishReached = true;
                }
            }
        }

        private void buzzerHandler_BuzUnit_InputChannelChanged(object sender, InputChannelParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_InputChannelChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_BuzUnit_InputChannelChanged(object content) {
            InputChannelParam_EventArgs e = content as InputChannelParam_EventArgs;
            if (this.isActive &&
                e is InputChannelParam_EventArgs &&
                e.Arg.Name == this.IOUnitName &&
                this.FinishMode == FinishModes.Crossing) {
                if (this.LeftFinishReached &&
                    e.Arg.InputChannel[this.LeftPlayerBuzzerChannel - 1] == InputChannelStates.UP) this.PassFinishLine(PlayerSelection.LeftPlayer);
                if (this.RightFinishReached &&
                    e.Arg.InputChannel[this.RightPlayerBuzzerChannel - 1] == InputChannelStates.UP) this.PassFinishLine(PlayerSelection.RightPlayer);
            }
        }


        void buzzerHandler_UnitConnectionStatusChanged(object sender, IOnet.IOUnit.IONbase.ConnectionStatusParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_UnitConnectionStatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        void buzzerHandler_UnitConnectionStatusRequest(object sender, IOnet.IOUnit.IONbase.ConnectionStatusParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_UnitConnectionStatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_UnitConnectionStatusChanged(object content) {
            IOnet.IOUnit.IONbase.ConnectionStatusParam_EventArgs e = content as IOnet.IOUnit.IONbase.ConnectionStatusParam_EventArgs;
            if (e is IOnet.IOUnit.IONbase.ConnectionStatusParam_EventArgs &&
                e.Arg.Name == this.IOUnitName) {
                this.ioUnitConnectionStatus = e.Arg.ConnectionStatus;
                this.checkIOUnitStatus();
                if (e.Arg.ConnectionStatus == Tools.NetContact.ClientStates.Connected) this.setDMXValues();
            }
        }

        void buzzerHandler_UnitInfoListChanged(object sender, IOnet.IOUnit.IONbase.InfoParamArray_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_UnitInfoListChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_UnitInfoListChanged(object content) {
            IOnet.IOUnit.IONbase.InfoParamArray_EventArgs e = content as IOnet.IOUnit.IONbase.InfoParamArray_EventArgs;
            if (e is IOnet.IOUnit.IONbase.InfoParamArray_EventArgs) {
                this.ioUnitInfo = e;
                this.fillIOUnitList(e.Arg);
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

        protected void insertScene_StopFired(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_insertScene_StopFired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_insertScene_StopFired(object content) {
            if (this.timeIsRunning) {
                if (this.insertScene.CurrentTime > 0 &&
                    this.timeToBeat.HasValue) {
                    this.TimerCurrentTime = Convert.ToDouble(this.insertScene.CurrentTime);
                    double offset = this.TimerCurrentTime - this.timeToBeat.Value;
                    switch (this.FirstFinisher) {
                        case PlayerSelection.LeftPlayer:
                            this.Vinsert_ShowOffsetTimeBottom(Convert.ToSingle(offset));
                            break;
                        case PlayerSelection.RightPlayer:
                            this.Vinsert_ShowOffsetTimeTop(Convert.ToSingle(offset));
                            break;
                    }
                }
            }
            this.timeIsRunning = false;
        }

        void insertScene_PreciseTimeReceived(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_insertScene_PreciseTimeReceived);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_insertScene_PreciseTimeReceived(object content) {
            if (this.timeIsRunning) {
                if (this.insertScene.PreciseTime > 0 &&
                    !this.timeToBeat.HasValue) {
                    this.timeToBeat = this.insertScene.PreciseTime;
                    this.on_PropertyChanged("TimeToBeat");
                }
            }
        }

        protected void insertScene_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timeToBeat_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timeToBeat_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "CurrentTime") this.TimerCurrentTime = Convert.ToDouble(this.insertScene.CurrentTime);
            }
        }

        void dot_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            this.on_PropertyChanged(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_dot_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_dot_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                //if (e.PropertyName == "Status" &&
                //    !this.repressCalcPenaltySums) this.calcPenaltySums();
            }
        }

        #endregion

    }
}
