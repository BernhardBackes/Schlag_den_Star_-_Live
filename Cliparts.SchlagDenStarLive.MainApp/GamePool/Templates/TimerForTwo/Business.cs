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

using Cliparts.Tools.Base;

using Cliparts.IOnet.IOUnit.IONbase;
using Cliparts.IOnet.IOUnit.IONbuz;

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;
using Cliparts.SchlagDenStarLive.MainApp.Settings;
using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimerForTwo;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimerForTwo {

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

        private Insert.Styles timerStyle = Insert.Styles.ThreeDots;
        public Insert.Styles TimerStyle {
            get { return this.timerStyle; }
            set {
                if (this.timerStyle != value) {
                    this.timerStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
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

        private int timerLimit = -1;
        public int TimerLimit {
            get { return this.timerLimit; }
            protected set {
                if (this.timerLimit != value) {
                    this.timerLimit = value;
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

        private Cliparts.IOnet.IOUnit.IONbase.InfoParamArray_EventArgs ioUnitInfo;

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

        private Cliparts.Tools.NetContact.ClientStates ioUnitConnectionStatus = Tools.NetContact.ClientStates.Missing;

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

        private bool onePlayerIsOut;
        public bool OnePlayerIsOut {
            get { return this.onePlayerIsOut; }
            set {
                if (this.onePlayerIsOut != value) {
                    this.onePlayerIsOut = value;
                    this.on_PropertyChanged();
                }
            }
        }

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

            this.ClassInfo = string.Format("'{0}' of 'Templates.TimerForTwo'", typeIdentifier);
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
            this.OnePlayerIsOut = false;
            this.FirstFinisher = PlayerSelection.NotSelected;
        }

        public override void Next() {
            this.TimeToBeat = null;
            this.timeIsRunning = false;
            this.OnePlayerIsOut = false;
            this.FirstFinisher = PlayerSelection.NotSelected;
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
            if (unitInfoList is Cliparts.IOnet.IOUnit.IONbase.InfoParam[]) {
                foreach (Cliparts.IOnet.IOUnit.IONbase.InfoParam item in unitInfoList) {
                    if (item is Cliparts.IOnet.IOUnit.IONbase.InfoParam) this.ioUnitNameList.Add(item.Name);
                }
            }
            this.on_PropertyChanged("IOUnitNameList");
        }

        private void checkIOUnitStatus() {
            BuzzerIO.BuzzerUnitStates ioUnitStatus = BuzzerIO.BuzzerUnitStates.NotAvailable;
            if (this.ioUnitNameList.Contains(this.IOUnitName)) {
                switch (this.ioUnitConnectionStatus) {
                    case Cliparts.Tools.NetContact.ClientStates.Connected:
                        ioUnitStatus = BuzzerIO.BuzzerUnitStates.Connected;
                        break;
                    case Cliparts.Tools.NetContact.ClientStates.Connecting:
                        ioUnitStatus = BuzzerIO.BuzzerUnitStates.Connecting;
                        break;
                    case Cliparts.Tools.NetContact.ClientStates.Disconnected:
                        ioUnitStatus = BuzzerIO.BuzzerUnitStates.Disconnected;
                        break;
                    case Cliparts.Tools.NetContact.ClientStates.Missing:
                    default:
                        ioUnitStatus = BuzzerIO.BuzzerUnitStates.Missing;
                        break;
                }
                if (ioUnitStatus == BuzzerIO.BuzzerUnitStates.Connected) {
                    switch (this.ioUnitWorkMode) {
                        case WorkModes.BUZZER:
                            ioUnitStatus = BuzzerIO.BuzzerUnitStates.BuzzerMode;
                            break;
                        case WorkModes.EVENT:
                            ioUnitStatus = BuzzerIO.BuzzerUnitStates.EventMode;
                            break;
                        case WorkModes.LOCK:
                            ioUnitStatus = BuzzerIO.BuzzerUnitStates.Locked;
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
            PlayerSelection finisher) {
            if (this.OnePlayerIsOut) {
                this.Vinsert_StopTimer();
                this.LockTimeline();
                this.LockBuzzer();
            }
            else if (finisher != PlayerSelection.NotSelected &&
                finisher != this.FirstFinisher) {
                if (this.FirstFinisher == PlayerSelection.NotSelected) {
                    this.FirstFinisher = finisher;
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

        public virtual void LockBuzzer() {
            this.buzzerHandler.LockBuzzer(this.IOUnitName);
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }
        public void Vinsert_TimerIn() {
            this.Vinsert_SetTimer();
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.ToIn();
        }
        public void Vinsert_SetTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetTimer(this.insertScene, this.LeftPlayerScore, this.RightPlayerScore); }
        public void Vinsert_SetTimer(
            Insert scene,
            int leftPlayerScore,
            int rightPlayerScore) {
            if (scene is Insert) {
                scene.SetStyle(this.TimerStyle);
                scene.SetTimerLimit(this.TimerLimit);
                scene.SetPositionX(this.TimerPositionX);
                scene.SetPositionY(this.TimerPositionY);
                scene.SetTopName(this.LeftPlayerName);
                scene.SetTopScore(leftPlayerScore);
                scene.SetBottomName(this.RightPlayerName);
                scene.SetBottomScore(rightPlayerScore);
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
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.SetOffsetOut();
                this.insertScene.SetFinishOut();
            }
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
                if (e.Data.Transponder.ToString() == this.LeftPlayerTransponderCode) {
                    if (this.SwapTracks) this.PassFinishLine(PlayerSelection.RightPlayer);
                    else this.PassFinishLine(PlayerSelection.LeftPlayer);
                }
                else if (e.Data.Transponder.ToString() == this.RightPlayerTransponderCode) {
                    if (this.SwapTracks) this.PassFinishLine(PlayerSelection.LeftPlayer);
                    else this.PassFinishLine(PlayerSelection.RightPlayer);
                }
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

        void buzzerHandler_BuzUnit_Buzzered(object sender, IOnet.IOUnit.IONbuz.BuzzerIDParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_Buzzered);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_BuzUnit_Buzzered(object content) {
            IOnet.IOUnit.IONbuz.BuzzerIDParam_EventArgs e = content as IOnet.IOUnit.IONbuz.BuzzerIDParam_EventArgs;
            if (this.isActive &&
                e is IOnet.IOUnit.IONbuz.BuzzerIDParam_EventArgs &&
                e.Arg.Name == this.IOUnitName) {
                if (e.Arg.BuzzerID == this.LeftPlayerBuzzerChannel) {
                    if (this.SwapTracks) this.PassFinishLine(PlayerSelection.RightPlayer);
                    else this.PassFinishLine(PlayerSelection.LeftPlayer);
                }
                else if (e.Arg.BuzzerID == this.RightPlayerBuzzerChannel) {
                    if (this.SwapTracks) this.PassFinishLine(PlayerSelection.LeftPlayer);
                    else this.PassFinishLine(PlayerSelection.RightPlayer);
                }
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

        void buzzerHandler_BuzUnit_WorkmodeChanged(object sender, IOnet.IOUnit.IONbuz.WorkModeParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_WorkmodeChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        void buzzerHandler_BuzUnit_WorkmodeRequest(object sender, IOnet.IOUnit.IONbuz.WorkModeParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_WorkmodeChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_BuzUnit_WorkmodeChanged(object content) {
            IOnet.IOUnit.IONbuz.WorkModeParam_EventArgs e = content as IOnet.IOUnit.IONbuz.WorkModeParam_EventArgs;
            if (e is IOnet.IOUnit.IONbuz.WorkModeParam_EventArgs &&
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
            if (this.timeIsRunning && 
                !this.OnePlayerIsOut) {
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

        #endregion

    }
}
