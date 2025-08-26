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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimeToBeatStartStopTimerScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimeToBeatStartStopTimerScore {

    public enum ContactModes { Open, Closed }

    public class Business : _Base.Score.Business {

        #region Properties

        private int timerStartTime = 90;
        public int TimerStartTime {
            get { return this.timerStartTime; }
            set {
                if (this.timerStartTime != value) {
                    this.timerStartTime = value;
                    this.on_PropertyChanged();
                    this.Vfullscreen_SetTimer();
                }
            }
        }

        private int timerAlarmTime1 = -1;
        public int TimerAlarmTime1 {
            get { return this.timerAlarmTime1; }
            set {
                if (this.timerAlarmTime1 != value) {
                    this.timerAlarmTime1 = value;
                    this.on_PropertyChanged();
                    this.Vfullscreen_SetTimer();
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
                    this.Vfullscreen_SetTimer();
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
                    this.Vfullscreen_SetTimer();
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

        private bool timerAutoStart = false;
        [NotSerialized]
        public bool TimerAutoStart {
            get { return this.timerAutoStart; }
            set {
                if (this.timerAutoStart != value) {
                    if (this.TimerIsRunning) {
                        if (value == false) this.timerAutoStart = value;
                    }
                    else {
                        this.timerAutoStart = value;
                    }
                    this.on_PropertyChanged();
                }
            }
        }

        private int stopwatchStopTime = 5999;
        public int StopwatchStopTime {
            get { return this.stopwatchStopTime; }
            set {
                if (this.stopwatchStopTime != value) {
                    this.stopwatchStopTime = value;
                    this.on_PropertyChanged();
                    this.Vfullscreen_SetTimeToBeat();
                }
            }
        }

        private double stopwatchCurrentTime = -1;
        public double StopwatchCurrentTime {
            get { return this.stopwatchCurrentTime; }
            protected set {
                if (this.stopwatchCurrentTime != value) {
                    this.stopwatchCurrentTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool stopwatchIsRunning = false;
        public bool StopwatchIsRunning {
            get { return this.stopwatchIsRunning; }
            protected set {
                if (this.stopwatchIsRunning != value) {
                    this.stopwatchIsRunning = value;
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

        private Tools.NetContact.ClientStates ioUnitConnectionStatus = Tools.NetContact.ClientStates.Missing;

        private WorkModes ioUnitWorkMode = WorkModes.NA;

        public BuzzerUnitStates IOUnitStatus { get; private set; }

        private int buzzerChannel = 1;
        public int BuzzerChannel {
            get { return this.buzzerChannel; }
            set {
                if (this.buzzerChannel != value) {
                    if (value < 1) value = 1;
                    if (value > 8) value = 8;
                    this.buzzerChannel = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private ContactModes contactMode = ContactModes.Open;
        public ContactModes ContactMode {
            get { return this.contactMode; }
            set {
                if (this.contactMode != value) {
                    this.contactMode = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private Content.Gameboard.PlayerSelection selectedPlayer = Content.Gameboard.PlayerSelection.LeftPlayer;
        public Content.Gameboard.PlayerSelection SelectedPlayer {
            get { return this.selectedPlayer; }
            set {
                if (this.selectedPlayer != value) {
                    if (value == Content.Gameboard.PlayerSelection.NotSelected) value = Content.Gameboard.PlayerSelection.LeftPlayer;
                    this.selectedPlayer = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool boardIsValid = false;
        public bool BoardIsValid {
            get { return this.boardIsValid; }
            set {
                if (this.boardIsValid != value) {
                    this.boardIsValid = value;
                    if (value &&
                        this.TimerAutoStart) {
                        this.TimerAutoStart = false;
                        this.Vfullscreen_StartTimer();
                    }
                    this.Vfullscreen_SetBoardStatus();
                    this.on_PropertyChanged();
                }
            }
        }
        
        private double? timeToBeat = null;
        [NotSerialized]
        public string TimeToBeat {
            get {
                if (this.timeToBeat.HasValue) return Helper.convertDoubleToStopwatchTimeText(this.timeToBeat.Value, false, true);
                else return string.Empty; 
            }
            set {
                if (this.TimeToBeat != value) {
                    double result;
                    if (!string.IsNullOrEmpty(value)) value = value.Replace(".", ",");
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

        private Fullscreen fullscreenScene;
        public override VRemote4.HandlerSi.Scene.States FullscreenSceneStatus {
            get {
                if (this.fullscreenScene is Fullscreen) return this.fullscreenScene.Status;
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.TimeToBeatStartStopTimerScore'", typeIdentifier);
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

            this.buzzerHandler = buzzerHandler;
            this.buzzerHandler.BuzUnit_InputChannelChanged += this.buzzerHandler_BuzUnit_InputChannelChanged;
            this.buzzerHandler.BuzUnit_InputChannelRequest += this.buzzerHandler_BuzUnit_InputChannelRequest;
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

            this.fullscreenScene = new Fullscreen(syncContext, fullscreenMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.fullscreenScene.StatusChanged += this.fullscreenScene_StatusChanged;
            this.fullscreenScene.PropertyChanged += this.fullscreenScene_PropertyChanged;
            this.fullscreenScene.TimerAlarm1Fired += this.timer_Alarm1Fired;
            this.fullscreenScene.TimerAlarm2Fired += this.timer_Alarm2Fired;
            this.fullscreenScene.TimerStopFired += this.timer_StopFired;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);
        }

        public override void Dispose() {
            base.Dispose();

            this.buzzerHandler.BuzUnit_InputChannelChanged -= this.buzzerHandler_BuzUnit_InputChannelChanged;
            this.buzzerHandler.BuzUnit_InputChannelRequest -= this.buzzerHandler_BuzUnit_InputChannelRequest;
            this.buzzerHandler.Unit_ConnectionStatusChanged -= this.buzzerHandler_UnitConnectionStatusChanged;
            this.buzzerHandler.Unit_ConnectionStatusRequest -= this.buzzerHandler_UnitConnectionStatusRequest;
            this.buzzerHandler.Unit_InfoListChanged -= this.buzzerHandler_UnitInfoListChanged;
            this.buzzerHandler.BuzUnit_WorkmodeChanged -= this.buzzerHandler_BuzUnit_WorkmodeChanged;
            this.buzzerHandler.BuzUnit_WorkmodeRequest -= this.buzzerHandler_BuzUnit_WorkmodeRequest;
            this.buzzerHandler = null;

            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;

            this.fullscreenScene.Dispose();
            this.fullscreenScene.StatusChanged -= this.fullscreenScene_StatusChanged;
            this.fullscreenScene.PropertyChanged -= this.fullscreenScene_PropertyChanged;
            this.fullscreenScene.TimerAlarm1Fired -= this.timer_Alarm1Fired;
            this.fullscreenScene.TimerAlarm2Fired -= this.timer_Alarm2Fired;
            this.fullscreenScene.TimerStopFired -= this.timer_StopFired;

        }

        public override void ResetData() {
            base.ResetData();
            this.TimeToBeat = null;
            this.timeIsRunning = false;
            this.TimerAutoStart = false;
        }

        public override void Next() {
            this.TimeToBeat = null;
            this.timeIsRunning = false;
            this.TimerAutoStart = false;
        }

        public void NextPlayer() {
            if (this.SelectedPlayer == Content.Gameboard.PlayerSelection.LeftPlayer) this.SelectedPlayer = Content.Gameboard.PlayerSelection.RightPlayer;
            else this.SelectedPlayer = Content.Gameboard.PlayerSelection.LeftPlayer;
            this.TimeToBeat = Helper.convertDoubleToStopwatchTimeText(this.StopwatchCurrentTime, false, true);
        }

        private void fillIOUnitList(
            InfoParam[] unitInfoList) {
            this.ioUnitNameList.Clear();
            if (unitInfoList is InfoParam[]) {
                foreach (InfoParam item in unitInfoList) {
                    if (item is InfoParam) this.ioUnitNameList.Add(item.Name);
                }
            }
            this.on_PropertyChanged("IOUnitNameList");
        }

        private void checkIOUnitStatus() {
            BuzzerIO.BuzzerUnitStates ioUnitStatus = BuzzerIO.BuzzerUnitStates.NotAvailable;
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

        public virtual void ReleaseIOUnit() {
            bool[] inputMask = new bool[8];
            if (this.BuzzerChannel > 0 &&
                this.BuzzerChannel <= inputMask.Length) inputMask[this.BuzzerChannel - 1] = true;
            this.buzzerHandler.SetInputMask(this.IOUnitName, inputMask);
            this.buzzerHandler.ReleaseBuzzer(this.IOUnitName, WorkModes.EVENT);
        }

        public virtual void LockIOUnit() {
            this.buzzerHandler.LockBuzzer(this.IOUnitName);
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }
        public override void Vinsert_ScoreIn() { if (this.insertScene is Insert) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() { if (this.insertScene is Insert) this.Vinsert_SetScore(this.insertScene.Score); }
        public override void Vinsert_ScoreOut() { if (this.insertScene is Insert) this.Vinsert_ScoreOut(this.insertScene.Score); }
        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void Vfullscreen_LoadScene() {
            base.Vfullscreen_LoadScene();
            this.fullscreenScene.Load(); 
        }
        public void Vfullscreen_SetBoardStatus() { if (this.fullscreenScene is Fullscreen) this.Vfullscreen_SetBoardStatus(this.fullscreenScene, this.BoardIsValid); }
        public void Vfullscreen_SetBoardStatus(
            Fullscreen scene,
            bool isValid) {
            if (scene is Fullscreen) scene.SetBackgroundIsValid(isValid);
        }
        public override void Vfullscreen_SetTimer() { if (this.fullscreenScene is Fullscreen) this.Vfullscreen_SetTimer(this.fullscreenScene, this.TimerStartTime); }
        public void Vfullscreen_SetTimer(
            Fullscreen scene,
            int startTime) {
            if (scene is Fullscreen) {
                scene.SetStartTime(startTime);
                scene.SetAlarmTime1(this.TimerAlarmTime1);
                scene.SetAlarmTime2(this.TimerAlarmTime2);
                scene.SetStopTime(this.TimerStopTime);
            }
        }
        public override void Vfullscreen_StartTimer() {
            this.Vfullscreen_SetTimer();
            if (this.fullscreenScene is Fullscreen) this.fullscreenScene.StartTimer(); 
        }
        public override void Vfullscreen_StopTimer() { if (this.fullscreenScene is Fullscreen) this.fullscreenScene.StopTimer(); }
        public override void Vfullscreen_ContinueTimer() { if (this.fullscreenScene is Fullscreen) this.fullscreenScene.ContinueTimer(); }
        public override void Vfullscreen_ResetTimer() {
            this.Vfullscreen_SetTimer();
            if (this.fullscreenScene is Fullscreen) this.fullscreenScene.ResetTimer(); 
        }
        public void Vfullscreen_SetStopwatch() { if (this.fullscreenScene is Fullscreen) this.Vfullscreen_SetStopwatch(this.fullscreenScene); }
        public void Vfullscreen_SetStopwatch(
            Fullscreen scene) {
            if (scene is Fullscreen) {
            }
        }
        public void Vfullscreen_StartStopwatch() {
            this.Vfullscreen_SetStopwatch();
            if (this.fullscreenScene is Fullscreen) this.fullscreenScene.StartStopwatch();
        }
        public void Vfullscreen_StopStopwatch() { if (this.fullscreenScene is Fullscreen) this.fullscreenScene.StopStopwatch(); }
        public void Vfullscreen_ResetStopwatch() {
            this.Vfullscreen_SetStopwatch();
            if (this.fullscreenScene is Fullscreen) this.fullscreenScene.ResetStopwatch();
        }
        public void Vfullscreen_SetTimeToBeat() { if (this.fullscreenScene is Fullscreen) this.Vfullscreen_SetTimeToBeat(this.fullscreenScene, this.TimeToBeat); }
        public void Vfullscreen_SetTimeToBeat(
            Fullscreen scene,
            string timeToBeat) {
            if (scene is Fullscreen) scene.SetTimeToBeatTime(timeToBeat);
        }
        public void Vfullscreen_TimeToBeatIn() {
            this.Vfullscreen_SetTimeToBeat();
            if (this.fullscreenScene is Fullscreen) this.fullscreenScene.TimeToBeatToIn(); 
        }
        public void Vfullscreen_TimeToBeatOut() { if (this.fullscreenScene is Fullscreen) this.fullscreenScene.TimeToBeatToOut(); }
        public override void Vfullscreen_UnloadScene() {
            base.Vfullscreen_UnloadScene();
            this.fullscreenScene.Unload(); 
        }

        public override void ClearGraphic() {
            base.ClearGraphic();
            this.insertScene.Clear();
        }

        #endregion


        #region Events.Outgoing

        public event EventHandler TimerAlarm1Fired;
        protected void on_TimerAlarm1Fired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.TimerAlarm1Fired, e); }

        public event EventHandler TimerAlarm2Fired;
        protected void on_TimerAlarm2Fired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.TimerAlarm2Fired, e); }

        public event EventHandler TimerStopFired;
        protected void on_TimerStopFired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.TimerStopFired, e); }

        public event EventHandler TimeToBeatStopFired;
        protected void on_TimeToBeatStopFired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.TimeToBeatStopFired, e); }

        #endregion

        #region Events.Incoming

        protected override void sync_fullscreenScene_StatusChanged(object content) {
            base.sync_fullscreenScene_StatusChanged(content);
            VRemote4.HandlerSi.Scene.StatusArgs e = content as VRemote4.HandlerSi.Scene.StatusArgs;
            if (e is VRemote4.HandlerSi.Scene.StatusArgs &&
                e.Status == VRemote4.HandlerSi.Scene.States.Available) {
                this.Vfullscreen_SetBoardStatus();
                this.Vfullscreen_ResetTimer();
                this.Vfullscreen_ResetStopwatch();
                if (this.fullscreenScene is Fullscreen) this.fullscreenScene.SetTimeToBeatOut();
            }
        }

        protected void fullscreenScene_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_fullscreenScene_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_fullscreenScene_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "TimerCurrentTime") this.TimerCurrentTime = this.fullscreenScene.TimerCurrentTime;
                else if (e.PropertyName == "TimerIsRunning") this.TimerIsRunning = this.fullscreenScene.TimerIsRunning;
                else if (e.PropertyName == "StopwatchCurrentTime") this.StopwatchCurrentTime = this.fullscreenScene.StopwatchCurrentTime;
                else if (e.PropertyName == "StopwatchIsRunning") this.StopwatchIsRunning = this.fullscreenScene.StopwatchIsRunning;
            }
        }

        protected void timer_Alarm1Fired(object sender, EventArgs e) {
            this.on_TimerAlarm1Fired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_Alarm1Fired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_Alarm1Fired(object content) {
        }

        protected void timer_Alarm2Fired(object sender, EventArgs e) {
            this.on_TimerAlarm2Fired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_Alarm2Fired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_Alarm2Fired(object content) {
        }

        protected void timer_StopFired(object sender, EventArgs e) {
            this.on_TimerStopFired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_StopFired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_StopFired(object content) {
        }

        void buzzerHandler_BuzUnit_InputChannelChanged(object sender, InputChannelParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_InputChannelChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        void buzzerHandler_BuzUnit_InputChannelRequest(object sender, InputChannelParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_InputChannelChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_BuzUnit_InputChannelChanged(object content) {
            InputChannelParam_EventArgs e = content as InputChannelParam_EventArgs;
            if (this.isActive &&
                e is InputChannelParam_EventArgs &&
                e.Arg.Name == this.IOUnitName) {
                switch (this.ContactMode) {
                    case ContactModes.Open:
                        this.BoardIsValid = e.Arg.InputChannel[this.BuzzerChannel - 1] == InputChannelStates.UP;
                        break;
                    case ContactModes.Closed:
                    default:
                        this.BoardIsValid = e.Arg.InputChannel[this.BuzzerChannel - 1] == InputChannelStates.DOWN;
                        break;
                }
                this.Vfullscreen_SetBoardStatus();
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
                if (e.Arg.ConnectionStatus == Tools.NetContact.ClientStates.Connected) this.buzzerHandler.RequestUnitInputChannels(this.IOUnitName);
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

        protected void timeToBeat_StopFired(object sender, EventArgs e) {
            this.on_TimeToBeatStopFired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timeToBeat_StopFired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timeToBeat_StopFired(object content) {
            if (this.timeIsRunning) {
                if (this.timeToBeat.HasValue) {
                    // zweiter Durchgang, der Offset wird ermittelt
                    //double currentTime = this.insertScene.TimeToBeat.CurrentTime;
                    //double offset = currentTime - this.timeToBeat.Value;
                    //this.Vinsert_ShowOffsetTime(Convert.ToSingle(offset));
                }
                else {
                    // erster Durchgang, die TimeToBeat wird ermittelt
                    //if (this.insertScene.TimeToBeat.CurrentTime > 0) {
                    //    this.timeToBeat = this.insertScene.TimeToBeat.CurrentTime;
                    //    this.on_PropertyChanged("TimeToBeat");
                    //}
                }
            }
            this.timeIsRunning = false;
            this.LockIOUnit();
        }

        protected void timeToBeat_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timeToBeat_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timeToBeat_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                //if (e.PropertyName == "CurrentTime") this.TimeToBeatCurrentTime = Convert.ToDouble(this.insertScene.TimeToBeat.CurrentTime);
            }
        }

        #endregion

    }
}
