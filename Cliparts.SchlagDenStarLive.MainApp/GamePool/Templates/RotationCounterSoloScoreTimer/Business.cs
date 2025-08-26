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

using Cliparts.Serialization;

using Cliparts.Tools.Base;

using Cliparts.IOnet.IOUnit.IONbase;
using Cliparts.IOnet.IOUnit.IONvelo;

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;
using Cliparts.SchlagDenStarLive.MainApp.Settings;
using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.RotationCounterSoloScoreTimer;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.RotationCounterSoloScoreTimer {

    public class Business : _Base.Score.Business {

        #region Properties

        private BuzzerIO.Business buzzerHandler;

        private List<string> ioUnitNameList = new List<string>();
        public string[] IOUnitNameList { get { return this.ioUnitNameList.ToArray(); } }

        private InfoParamArray_EventArgs ioUnitInfo;

        private string leftPlayerOIUnitName = string.Empty;
        public string LeftPlayerIOUnitName {
            get { return this.leftPlayerOIUnitName; }
            set {
                if (this.leftPlayerOIUnitName != value) {
                    if (value == null)
                        value = string.Empty;
                    this.leftPlayerOIUnitName = value;
                    this.on_PropertyChanged();
                    this.leftPlayerIOUnitConnectionStatus = Tools.NetContact.ClientStates.Missing;
                    this.leftPlayerIOUnitWorkMode = WorkModes.NA;
                    this.checkLeftPlayerIOUnitStatus();
                    this.requestIOUnitStates(value);
                }
            }
        }

        private Tools.NetContact.ClientStates leftPlayerIOUnitConnectionStatus = Tools.NetContact.ClientStates.Missing;

        private WorkModes leftPlayerIOUnitWorkMode = WorkModes.NA;

        public VeloUnitStates LeftPlayerIOUnitStatus { get; private set; }

        private int leftPlayerCounter = 0;
        public int LeftPlayerCounter {
            get { return this.leftPlayerCounter; }
            set {
                if (this.leftPlayerCounter != value) {
                    if (value < 0) this.leftPlayerCounter = 0;
                    else this.leftPlayerCounter = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string rightPlayerOIUnitName = string.Empty;
        public string RightPlayerIOUnitName {
            get { return this.rightPlayerOIUnitName; }
            set {
                if (this.rightPlayerOIUnitName != value) {
                    if (value == null)
                        value = string.Empty;
                    this.rightPlayerOIUnitName = value;
                    this.on_PropertyChanged();
                    this.rightPlayerIOUnitConnectionStatus = Tools.NetContact.ClientStates.Missing;
                    this.rightPlayerIOUnitWorkMode = WorkModes.NA;
                    this.checkRightPlayerIOUnitStatus();
                    this.requestIOUnitStates(value);
                }
            }
        }

        private Tools.NetContact.ClientStates rightPlayerIOUnitConnectionStatus = Tools.NetContact.ClientStates.Missing;

        private WorkModes rightPlayerIOUnitWorkMode = WorkModes.NA;

        public VeloUnitStates RightPlayerIOUnitStatus { get; private set; }

        private int rightPlayerCounter = 0;
        public int RightPlayerCounter {
            get { return this.rightPlayerCounter; }
            set {
                if (this.rightPlayerCounter != value) {
                    if (value < 0) this.rightPlayerCounter = 0;
                    else this.rightPlayerCounter = value;
                    this.on_PropertyChanged();
                }
            }
        }

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

        private bool counterFlipPlayers;
        public bool CounterFlipPlayers {
            get { return this.counterFlipPlayers; }
            set {
                if (this.counterFlipPlayers != value) {
                    this.counterFlipPlayers = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounter();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.Score.Styles setsStyle = VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots;
        public VentuzScenes.GamePool._Modules.Score.Styles CounterStyle {
            get { return this.setsStyle; }
            set {
                if (this.setsStyle != value) {
                    this.setsStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounter();
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

        private VentuzScenes.GamePool._Modules.Timer.Styles timerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
        public VentuzScenes.GamePool._Modules.Timer.Styles TimerStyle {
            get { return this.timerStyle; }
            set {
                if (this.timerStyle != value) {
                    this.timerStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
                    this.Vfullscreen_SetTimer();
                }
            }
        }

        private int timerStartTime = 240;
        public int TimerStartTime {
            get { return this.timerStartTime; }
            set {
                if (this.timerStartTime != value) {
                    this.timerStartTime = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
                    this.Vfullscreen_SetTimer();
                }
            }
        }

        private int timerExtraTime = 60;
        public int TimerExtraTime {
            get { return this.timerExtraTime; }
            set {
                if (this.timerExtraTime != value) {
                    this.timerExtraTime = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
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
                    this.Vinsert_SetTimer();
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
                    this.Vinsert_SetTimer();
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
                    this.Vinsert_SetTimer();
                    this.Vfullscreen_SetTimer();
                }
            }
        }

        private bool runExtraTime = false;
        [NotSerialized]
        public bool RunExtraTime {
            get { return this.runExtraTime; }
            set {
                if (this.runExtraTime != value) {
                    this.runExtraTime = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
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

        private Insert insertScene;
        public override VRemote4.HandlerSi.Scene.States InsertSceneStatus {
            get {
                if (this.insertScene is Insert) return this.insertScene.Status;
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.RotationCounterSoloScoreTimer'", typeIdentifier);
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
            this.buzzerHandler.VeloUnit_PassingsCountABChanged += this.buzzerHandler_VeloUnit_PassingsCountABChanged;
            this.buzzerHandler.VeloUnit_WorkModeChanged += this.buzzerHandler_VeloUnit_WorkModeChanged;
            this.buzzerHandler.VeloUnit_WorkModeRequest += this.buzzerHandler_VeloUnit_WorkModeRequest;
            this.buzzerHandler.Unit_ConnectionStatusChanged += this.buzzerHandler_UnitConnectionStatusChanged;
            this.buzzerHandler.Unit_ConnectionStatusRequest += this.buzzerHandler_UnitConnectionStatusRequest;
            this.buzzerHandler.Unit_InfoListChanged += this.buzzerHandler_UnitInfoListChanged;

            this.fillIOUnitList(this.buzzerHandler.UnitInfoList);
            this.buzzerHandler.RequestUnitConnectionStatus(this.LeftPlayerIOUnitName);
            this.buzzerHandler.RequestUnitWorkMode(this.LeftPlayerIOUnitName);
            this.buzzerHandler.RequestUnitConnectionStatus(this.RightPlayerIOUnitName);
            this.buzzerHandler.RequestUnitWorkMode(this.RightPlayerIOUnitName);

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;
            this.insertScene.Timer.Alarm1Fired += this.timer_Alarm1Fired;
            this.insertScene.Timer.Alarm2Fired += this.timer_Alarm2Fired;
            this.insertScene.Timer.StopFired += this.timer_StopFired;
            this.insertScene.Timer.PropertyChanged += this.timer_PropertyChanged;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);
        }

        public override void Dispose() {
            base.Dispose();

            this.buzzerHandler.VeloUnit_PassingsCountABChanged -= this.buzzerHandler_VeloUnit_PassingsCountABChanged;
            this.buzzerHandler.VeloUnit_WorkModeChanged -= this.buzzerHandler_VeloUnit_WorkModeChanged;
            this.buzzerHandler.VeloUnit_WorkModeRequest -= this.buzzerHandler_VeloUnit_WorkModeRequest;
            this.buzzerHandler.Unit_ConnectionStatusChanged -= this.buzzerHandler_UnitConnectionStatusChanged;
            this.buzzerHandler.Unit_ConnectionStatusRequest -= this.buzzerHandler_UnitConnectionStatusRequest;
            this.buzzerHandler.Unit_InfoListChanged -= this.buzzerHandler_UnitInfoListChanged;

            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Timer.Alarm1Fired -= this.timer_Alarm1Fired;
            this.insertScene.Timer.Alarm2Fired -= this.timer_Alarm2Fired;
            this.insertScene.Timer.StopFired -= this.timer_StopFired;
            this.insertScene.Timer.PropertyChanged -= this.timer_PropertyChanged;
        }

        public override void ResetData() {
            base.ResetData();
            this.LeftPlayerCounter = 0;
            this.RightPlayerCounter = 0;
        }

        public override void Next() {
            base.Next();
            this.LeftPlayerCounter = 0;
            this.RightPlayerCounter = 0;
        }

        private void fillIOUnitList(
            InfoParam[] unitInfoList) {
            this.ioUnitNameList.Clear();
            if (unitInfoList is InfoParam[]) {
                foreach (InfoParam item in unitInfoList) {
                    if (item is InfoParam)
                        this.ioUnitNameList.Add(item.Name);
                }
            }
            this.on_PropertyChanged("IOUnitList");
        }

        private void checkLeftPlayerIOUnitStatus() {
            VeloUnitStates ioUnitStatus = VeloUnitStates.NotAvailable;
            if (this.ioUnitNameList.Contains(this.LeftPlayerIOUnitName)) {
                switch (this.leftPlayerIOUnitConnectionStatus) {
                    case Tools.NetContact.ClientStates.Connected:
                        ioUnitStatus = VeloUnitStates.Connected;
                        break;
                    case Tools.NetContact.ClientStates.Connecting:
                        ioUnitStatus = VeloUnitStates.Connecting;
                        break;
                    case Tools.NetContact.ClientStates.Disconnected:
                        ioUnitStatus = VeloUnitStates.Disconnected;
                        break;
                    case Tools.NetContact.ClientStates.Missing:
                    default:
                        ioUnitStatus = VeloUnitStates.Missing;
                        break;
                }
                if (ioUnitStatus == VeloUnitStates.Connected) {
                    switch (this.leftPlayerIOUnitWorkMode) {
                        case WorkModes.SNG:
                            ioUnitStatus = VeloUnitStates.SingleMode;
                            break;
                        case WorkModes.DBL:
                            ioUnitStatus = VeloUnitStates.DoubleMode;
                            break;
                        case WorkModes.LOCK:
                            ioUnitStatus = VeloUnitStates.Locked;
                            break;
                        case WorkModes.NA:
                        default:
                            break;
                    }
                }
            }
            if (this.LeftPlayerIOUnitStatus != ioUnitStatus) {
                this.LeftPlayerIOUnitStatus = ioUnitStatus;
                this.on_PropertyChanged("LeftPlayerIOUnitStatus");
            }
        }
        private void checkRightPlayerIOUnitStatus() {
            VeloUnitStates ioUnitStatus = VeloUnitStates.NotAvailable;
            if (this.ioUnitNameList.Contains(this.RightPlayerIOUnitName)) {
                switch (this.rightPlayerIOUnitConnectionStatus) {
                    case Tools.NetContact.ClientStates.Connected:
                        ioUnitStatus = VeloUnitStates.Connected;
                        break;
                    case Tools.NetContact.ClientStates.Connecting:
                        ioUnitStatus = VeloUnitStates.Connecting;
                        break;
                    case Tools.NetContact.ClientStates.Disconnected:
                        ioUnitStatus = VeloUnitStates.Disconnected;
                        break;
                    case Tools.NetContact.ClientStates.Missing:
                    default:
                        ioUnitStatus = VeloUnitStates.Missing;
                        break;
                }
                if (ioUnitStatus == VeloUnitStates.Connected) {
                    switch (this.rightPlayerIOUnitWorkMode) {
                        case WorkModes.SNG:
                            ioUnitStatus = VeloUnitStates.SingleMode;
                            break;
                        case WorkModes.DBL:
                            ioUnitStatus = VeloUnitStates.DoubleMode;
                            break;
                        case WorkModes.LOCK:
                            ioUnitStatus = VeloUnitStates.Locked;
                            break;
                        case WorkModes.NA:
                        default:
                            break;
                    }
                }
            }
            if (this.RightPlayerIOUnitStatus != ioUnitStatus) {
                this.RightPlayerIOUnitStatus = ioUnitStatus;
                this.on_PropertyChanged("RightPlayerIOUnitStatus");
            }
        }

        private void requestIOUnitStates(
            string unitName) {
            if (buzzerHandler is BuzzerIO.Business) {
                this.buzzerHandler.RequestUnitConnectionStatus(unitName);
                this.buzzerHandler.RequestUnitWorkMode(unitName);
            }
        }

        public virtual void DoCounter(
            PlayerSelection player) {
            switch (player) {
                case PlayerSelection.LeftPlayer:
                    this.LeftPlayerCounter++;
                    break;
                case PlayerSelection.RightPlayer:
                    this.RightPlayerCounter++;
                    break;
            }
            this.Vinsert_SetCounter();
        }

        public virtual void ReleaseBuzzer() {
            this.buzzerHandler.ReleaseVelo(this.LeftPlayerIOUnitName, WorkModes.SNG);
            this.buzzerHandler.ReleaseVelo(this.RightPlayerIOUnitName, WorkModes.SNG);
        }

        public virtual void LockBuzzer() {
            this.buzzerHandler.LockVelo(this.LeftPlayerIOUnitName);
            this.buzzerHandler.LockVelo(this.RightPlayerIOUnitName);
        }

        public override void Vinsert_LoadScene() { this.insertScene.Load(); }
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
        public override void Vinsert_ScoreOut() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Score.ToOut(); }
        public void Vinsert_CounterIn() {
            this.Vinsert_SetCounter();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Counter.ToIn();
        }
        public void Vinsert_SetCounter() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.Counter.SetPositionX(this.CounterPositionX);
                this.insertScene.Counter.SetPositionY(this.CounterPositionY);
                this.insertScene.Counter.SetStyle(this.CounterStyle);
                this.insertScene.Counter.SetFlipPosition(this.CounterFlipPlayers);
                this.insertScene.Counter.SetLeftTopName(this.LeftPlayerName);
                this.insertScene.Counter.SetLeftTopScore(this.LeftPlayerCounter);
                this.insertScene.Counter.SetRightBottomName(this.RightPlayerName);
                this.insertScene.Counter.SetRightBottomScore(this.RightPlayerCounter);
            }
        }
        public void Vinsert_CounterOut() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Counter.ToOut(); }
        public void Vinsert_TimerIn() {
            this.Vinsert_SetTimer();
            this.Vinsert_ResetTimer();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available)
                this.insertScene.Timer.ToIn();
        }
        public void Vinsert_SetTimer() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.Timer.SetPositionX(this.TimerPositionX);
                this.insertScene.Timer.SetPositionY(this.TimerPositionY);
                this.insertScene.Timer.SetStyle(this.TimerStyle);
                this.insertScene.Timer.SetScaling(100);
                if (this.RunExtraTime)
                    this.insertScene.Timer.SetStartTime(this.TimerExtraTime);
                else
                    this.insertScene.Timer.SetStartTime(this.TimerStartTime);
                this.insertScene.Timer.SetStopTime(this.TimerStopTime);
                this.insertScene.Timer.SetAlarmTime1(this.TimerAlarmTime1);
                this.insertScene.Timer.SetAlarmTime2(this.TimerAlarmTime2);
            }
        }
        public void Vinsert_StartTimer() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.Timer.StartTimer();
                Vinsert_SetCounter();
            }
        }
        public void Vinsert_StopTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.StopTimer(); }
        public void Vinsert_ContinueTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.ContinueTimer(); }
        public void Vinsert_ResetTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.ResetTimer(); }
        public void Vinsert_TimerOut() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.ToOut(); }
        public override void Vinsert_UnloadScene() { this.insertScene.Unload(); }

        public override void Vfullscreen_SetTimer() {
            if (this.RunExtraTime)
                this.Vfullscreen_SetTimer(this.TimerExtraTime);
            else
                this.Vfullscreen_SetTimer(this.TimerStartTime);
        }
        public void Vfullscreen_SetTimer(int startTime) {
            base.Vfullscreen_SetTimer();
            if (this.fullscreenMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                switch (this.TimerStyle) {
                    case Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules.Timer.Styles.Sec:
                        this.fullscreenMasterScene.Timer.SetStyle(VentuzScenes.Fullscreen.Clock.Styles.Sec);
                        break;
                    case Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules.Timer.Styles.MinSec:
                    default:
                        this.fullscreenMasterScene.Timer.SetStyle(VentuzScenes.Fullscreen.Clock.Styles.MinSec);
                        break;
                }
                this.fullscreenMasterScene.Timer.SetStartTime(startTime);
                this.fullscreenMasterScene.Timer.SetStopTime(this.TimerStopTime);
                this.fullscreenMasterScene.Timer.SetAlarmTime1(-1);
                this.fullscreenMasterScene.Timer.SetAlarmTime2(-1);
            }
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

        #endregion

        #region Events.Incoming

        private void buzzerHandler_VeloUnit_PassingsCountABChanged(object sender, CounterParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_VeloUnit_PassingsCountABChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_VeloUnit_PassingsCountABChanged(object content) {
            CounterParam_EventArgs e = content as CounterParam_EventArgs;
            if (e is CounterParam_EventArgs &&
                this.TimerCurrentTime > 0) {
                if (e.Arg.Name == this.LeftPlayerIOUnitName) this.LeftPlayerCounter = e.Arg.Value - 1;
                else if (e.Arg.Name == this.RightPlayerIOUnitName) this.RightPlayerCounter = e.Arg.Value - 1;
                this.Vinsert_SetCounter();
            }
        }

        private void buzzerHandler_VeloUnit_WorkModeChanged(object sender, WorkModeParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_VeloUnit_WorkModeRequest);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void buzzerHandler_VeloUnit_WorkModeRequest(object sender, WorkModeParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_VeloUnit_WorkModeRequest);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_VeloUnit_WorkModeRequest(object content) {
            WorkModeParam_EventArgs e = content as WorkModeParam_EventArgs;
            if (e is WorkModeParam_EventArgs) {
                if (e.Arg.Name == this.LeftPlayerIOUnitName) {
                    this.leftPlayerIOUnitWorkMode = e.Arg.Value;
                    this.checkLeftPlayerIOUnitStatus();
                }
                else if (e.Arg.Name == this.RightPlayerIOUnitName) {
                    this.rightPlayerIOUnitWorkMode = e.Arg.Value;
                    this.checkRightPlayerIOUnitStatus();
                }
            }
        }

        void buzzerHandler_UnitConnectionStatusChanged(object sender, ConnectionStatusParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_UnitConnectionStatusChanged);
            if (this.syncContext != null)
                this.syncContext.Post(callback, e);
        }
        void buzzerHandler_UnitConnectionStatusRequest(object sender, ConnectionStatusParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_UnitConnectionStatusChanged);
            if (this.syncContext != null)
                this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_UnitConnectionStatusChanged(object content) {
            ConnectionStatusParam_EventArgs e = content as ConnectionStatusParam_EventArgs;
            if (e is ConnectionStatusParam_EventArgs) {
                if (e.Arg.Name == this.LeftPlayerIOUnitName) {
                    this.leftPlayerIOUnitConnectionStatus = e.Arg.ConnectionStatus;
                    this.checkLeftPlayerIOUnitStatus();
                }
                else if (e.Arg.Name == this.RightPlayerIOUnitName) {
                    this.rightPlayerIOUnitConnectionStatus = e.Arg.ConnectionStatus;
                    this.checkRightPlayerIOUnitStatus();
                }
            }
        }

        void buzzerHandler_UnitInfoListChanged(object sender, InfoParamArray_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_UnitInfoListChanged);
            if (this.syncContext != null)
                this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_UnitInfoListChanged(object content) {
            InfoParamArray_EventArgs e = content as InfoParamArray_EventArgs;
            if (e is InfoParamArray_EventArgs) {
                this.ioUnitInfo = e;
                this.fillIOUnitList(e.Arg);
            }
        }

        protected void timer_Alarm1Fired(object sender, EventArgs e) {
            this.on_TimerAlarm1Fired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_Alarm1Fired);
            if (this.syncContext != null)
                this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_Alarm1Fired(object content) {
        }

        protected void timer_Alarm2Fired(object sender, EventArgs e) {
            this.on_TimerAlarm2Fired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_Alarm2Fired);
            if (this.syncContext != null)
                this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_Alarm2Fired(object content) {
        }

        protected void timer_StopFired(object sender, EventArgs e) {
            this.on_TimerStopFired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_StopFired);
            if (this.syncContext != null)
                this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_StopFired(object content) { 
            this.LockBuzzer(); 
        }

        protected void timer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_PropertyChanged);
            if (this.syncContext != null)
                this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "CurrentTime")
                    this.TimerCurrentTime = this.insertScene.Timer.CurrentTime;
                else if (e.PropertyName == "IsRunning")
                    this.TimerIsRunning = this.insertScene.Timer.IsRunning;
            }
        }

        #endregion

    }

}
