using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cliparts.Serialization;

using Cliparts.IOnet.IOUnit.IONbase;
using Cliparts.IOnet.IOUnit.IONbuz;

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;
using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.TwoTimersBuzzerStoppedScores {

    public class Business : _Base.Business {

        #region Properties

        private int twoTimersScoresPositionX = 0;
        public int TwoTimersScoresPositionX {
            get { return this.twoTimersScoresPositionX; }
            set {
                if (this.twoTimersScoresPositionX != value) {
                    this.twoTimersScoresPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTwoTimersScores();
                }
            }
        }

        private int twoTimersScoresPositionY = 0;
        public int TwoTimersScoresPositionY {
            get { return this.twoTimersScoresPositionY; }
            set {
                if (this.twoTimersScoresPositionY != value) {
                    this.twoTimersScoresPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTwoTimersScores();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.TwoTimersScores.Styles twoTimersScoresStyle = VentuzScenes.GamePool._Modules.TwoTimersScores.Styles.Sec;
        public VentuzScenes.GamePool._Modules.TwoTimersScores.Styles TwoTimersScoresTimerStyle {
            get { return this.twoTimersScoresStyle; }
            set {
                if (this.twoTimersScoresStyle != value) {
                    this.twoTimersScoresStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimers();
                    this.Vfullscreen_SetTimer();
                }
            }
        }

        private int twoTimersScoresStartTime = 240;
        public int TwoTimersScoresStartTime {
            get { return this.twoTimersScoresStartTime; }
            set {
                if (this.twoTimersScoresStartTime != value) {
                    this.twoTimersScoresStartTime = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimers();
                }
            }
        }

        private int twoTimersScoresAlarmTime1 = -1;
        public int TwoTimersScoresAlarmTime1 {
            get { return this.twoTimersScoresAlarmTime1; }
            set {
                if (this.twoTimersScoresAlarmTime1 != value) {
                    this.twoTimersScoresAlarmTime1 = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimers();
                }
            }
        }

        private int twoTimersScoresAlarmTime2 = -1;
        public int TwoTimersScoresAlarmTime2 {
            get { return this.twoTimersScoresAlarmTime2; }
            set {
                if (this.twoTimersScoresAlarmTime2 != value) {
                    this.twoTimersScoresAlarmTime2 = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimers();
                }
            }
        }

        private int twoTimersScoresStopTime = 0;
        public int TwoTimersScoresStopTime {
            get { return this.twoTimersScoresStopTime; }
            set {
                if (this.twoTimersScoresStopTime != value) {
                    this.twoTimersScoresStopTime = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimers();
                }
            }
        }

        private int leftPlayerScore = 0;
        public int LeftPlayerScore {
            get { return this.leftPlayerScore; }
            set {
                if (this.leftPlayerScore != value) {
                    if (value < 0) value = 0;
                    this.leftPlayerScore = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int leftTimerCurrentTime = -1;
        public int LeftTimerCurrentTime {
            get { return this.leftTimerCurrentTime; }
            protected set {
                if (this.leftTimerCurrentTime != value) {
                    this.leftTimerCurrentTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool leftTimerIsRunning = false;
        public bool LeftTimerIsRunning {
            get { return this.leftTimerIsRunning; }
            protected set {
                if (this.leftTimerIsRunning != value) {
                    this.leftTimerIsRunning = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerScore = 0;
        public int RightPlayerScore {
            get { return this.rightPlayerScore; }
            set {
                if (this.rightPlayerScore != value) {
                    if (value < 0) value = 0;
                    this.rightPlayerScore = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightTimerCurrentTime = -1;
        public int RightTimerCurrentTime {
            get { return this.rightTimerCurrentTime; }
            protected set {
                if (this.rightTimerCurrentTime != value) {
                    this.rightTimerCurrentTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool rightTimerIsRunning = false;
        public bool RightTimerIsRunning {
            get { return this.rightTimerIsRunning; }
            protected set {
                if (this.rightTimerIsRunning != value) {
                    this.rightTimerIsRunning = value;
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

        #endregion


        #region Funktionen

        public Business() { }
        public Business(string typeIdentifier) : base(typeIdentifier) { }

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
            this.buzzerHandler.BuzUnit_Buzzered += this.buzzerHandler_BuzUnit_Buzzered;
            this.buzzerHandler.Unit_ConnectionStatusChanged += this.buzzerHandler_UnitConnectionStatusChanged;
            this.buzzerHandler.Unit_ConnectionStatusRequest += this.buzzerHandler_UnitConnectionStatusRequest;
            this.buzzerHandler.Unit_InfoListChanged += this.buzzerHandler_UnitInfoListChanged;
            this.buzzerHandler.BuzUnit_WorkmodeChanged += this.buzzerHandler_BuzUnit_WorkmodeChanged;
            this.buzzerHandler.BuzUnit_WorkmodeRequest += this.buzzerHandler_BuzUnit_WorkmodeRequest;
            this.fillBuzzerUnitList(this.buzzerHandler.UnitInfoList);
            this.buzzerHandler.RequestUnitConnectionStatus(this.IOUnitName);
            this.buzzerHandler.RequestUnitWorkMode(this.IOUnitName);
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
        }

        public override void Init() {
            base.Init();
            this.LeftPlayerScore = 0;
            this.RightPlayerScore = 0;
        }

        private void fillBuzzerUnitList(
            Cliparts.IOnet.IOUnit.IONbase.InfoParam[] unitInfoList) {
            this.ioUnitNameList.Clear();
            if (unitInfoList is Cliparts.IOnet.IOUnit.IONbase.InfoParam[]) {
                foreach (Cliparts.IOnet.IOUnit.IONbase.InfoParam item in unitInfoList) {
                    if (item is Cliparts.IOnet.IOUnit.IONbase.InfoParam) this.ioUnitNameList.Add(item.Name);
                }
            }
            this.on_PropertyChanged("IOUnitList");
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

        public virtual void DoBuzzer(
            PlayerSelection buzzeredPlayer) {
            switch (buzzeredPlayer) {
                case PlayerSelection.LeftPlayer:
                    this.Vinsert_StopTopTimer();
                    break;
                case PlayerSelection.RightPlayer:
                    this.Vinsert_StopBottomTimer();
                    break;
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


        public virtual void Vinsert_TwoTimersScoresIn() { }
        public virtual void Vinsert_TwoTimersScoresIn(VentuzScenes.GamePool._Modules.TwoTimersScores scene) {
            if (scene is VentuzScenes.GamePool._Modules.TwoTimersScores) {
                this.Vinsert_SetTwoTimersScores(scene);
                this.Vinsert_SetTimers(scene);
                this.Vinsert_ResetTopTimer(scene);
                this.Vinsert_ResetBottomTimer(scene);
                this.Vinsert_SetScores(scene);
                scene.ToIn();
            }
        }

        public virtual void Vinsert_SetTwoTimersScores() { }
        public virtual void Vinsert_SetTwoTimersScores(
            VentuzScenes.GamePool._Modules.TwoTimersScores scene) {
            if (scene is VentuzScenes.GamePool._Modules.TwoTimersScores) {
                scene.SetPositionX(this.TwoTimersScoresPositionX);
                scene.SetPositionY(this.TwoTimersScoresPositionY);
            }
        }

        public virtual void Vinsert_SetTimers() { }
        public virtual void Vinsert_SetTimers(VentuzScenes.GamePool._Modules.TwoTimersScores scene) {
            this.Vinsert_SetTimers(scene, this.TwoTimersScoresStartTime);
        }
        public virtual void Vinsert_SetTimers(
            VentuzScenes.GamePool._Modules.TwoTimersScores scene,
            int startTime) {
            if (scene is VentuzScenes.GamePool._Modules.TwoTimersScores) {
                scene.SetTopTimerStyle(this.TwoTimersScoresTimerStyle);
                scene.SetTopTimerStartTime(startTime);
                scene.SetTopTimerStopTime(this.TwoTimersScoresStopTime);
                scene.SetTopTimerAlarmTime1(this.TwoTimersScoresAlarmTime1);
                scene.SetTopTimerAlarmTime2(this.TwoTimersScoresAlarmTime2);
                scene.SetBottomTimerStyle(this.TwoTimersScoresTimerStyle);
                scene.SetBottomTimerStartTime(startTime);
                scene.SetBottomTimerStopTime(this.TwoTimersScoresStopTime);
                scene.SetBottomTimerAlarmTime1(this.TwoTimersScoresAlarmTime1);
                scene.SetBottomTimerAlarmTime2(this.TwoTimersScoresAlarmTime2);
            }
        }
        public virtual void Vinsert_StartTopTimer() { }
        public virtual void Vinsert_StartTopTimer(VentuzScenes.GamePool._Modules.TwoTimersScores scene) { if (scene is VentuzScenes.GamePool._Modules.TwoTimersScores) scene.StartTopTimer(); }
        public virtual void Vinsert_StopTopTimer() { }
        public virtual void Vinsert_StopTopTimer(VentuzScenes.GamePool._Modules.TwoTimersScores scene) { if (scene is VentuzScenes.GamePool._Modules.TwoTimersScores) scene.StopTopTimer(); }
        public virtual void Vinsert_ContinueTopTimer() { }
        public virtual void Vinsert_ContinueTopTimer(VentuzScenes.GamePool._Modules.TwoTimersScores scene) { if (scene is VentuzScenes.GamePool._Modules.TwoTimersScores) scene.ContinueTopTimer(); }
        public virtual void Vinsert_ResetTopTimer() { }
        public virtual void Vinsert_ResetTopTimer(VentuzScenes.GamePool._Modules.TwoTimersScores scene) { if (scene is VentuzScenes.GamePool._Modules.TwoTimersScores) scene.ResetTopTimer(); }

        public virtual void Vinsert_StartBottomTimer() { }
        public virtual void Vinsert_StartBottomTimer(VentuzScenes.GamePool._Modules.TwoTimersScores scene) { if (scene is VentuzScenes.GamePool._Modules.TwoTimersScores) scene.StartBottomTimer(); }
        public virtual void Vinsert_StopBottomTimer() { }
        public virtual void Vinsert_StopBottomTimer(VentuzScenes.GamePool._Modules.TwoTimersScores scene) { if (scene is VentuzScenes.GamePool._Modules.TwoTimersScores) scene.StopBottomTimer(); }
        public virtual void Vinsert_ContinueBottomTimer() { }
        public virtual void Vinsert_ContinueBottomTimer(VentuzScenes.GamePool._Modules.TwoTimersScores scene) { if (scene is VentuzScenes.GamePool._Modules.TwoTimersScores) scene.ContinueBottomTimer(); }
        public virtual void Vinsert_ResetBottomTimer() { }
        public virtual void Vinsert_ResetBottomTimer(VentuzScenes.GamePool._Modules.TwoTimersScores scene) { if (scene is VentuzScenes.GamePool._Modules.TwoTimersScores) scene.ResetBottomTimer(); }

        public virtual void Vinsert_SetScores() { }
        public virtual void Vinsert_SetScores(VentuzScenes.GamePool._Modules.TwoTimersScores scene) { this.Vinsert_SetScores(scene, this.LeftPlayerScore, this.RightPlayerScore); }
        public void Vinsert_SetScores(
            VentuzScenes.GamePool._Modules.TwoTimersScores scene,
            int leftPlayerScore,
            int rightPlayerScore) {
            if (scene is VentuzScenes.GamePool._Modules.TwoTimersScores
                && scene.Status == VRemote4.HandlerSi.Scene.States.Available) {
                scene.SetTopName(this.LeftPlayerName);
                scene.SetTopScore(leftPlayerScore);
                scene.SetBottomName(this.RightPlayerName);
                scene.SetBottomScore(rightPlayerScore);
            }
        }

        public virtual void Vinsert_TwoTimersScoresOut() { }
        public virtual void Vinsert_TwoTimersScoresOut(VentuzScenes.GamePool._Modules.TwoTimersScores scene) { if (scene is VentuzScenes.GamePool._Modules.TwoTimersScores) scene.ToOut(); }

        public virtual void Vstage_Init() {
            this.Vstage_GamescoreIn();
        }
        public virtual void Vstage_GamescoreIn() {
            this.Vstage_SetScore();
            if (this.hostMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostMasterScene.GameScoreIn();
            if (this.leftPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerMasterScene.GameScoreIn();
            if (this.rightPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerMasterScene.GameScoreIn();
        }
        public virtual void Vstage_SetScore() {
            if (this.hostMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.hostMasterScene.SetLeftPlayerGameScore(this.LeftPlayerScore);
                this.hostMasterScene.SetRightPlayerGameScore(this.RightPlayerScore);
            }
            if (this.leftPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.leftPlayerMasterScene.SetLeftPlayerGameScore(this.LeftPlayerScore);
                this.leftPlayerMasterScene.SetRightPlayerGameScore(this.RightPlayerScore);
            }
            if (this.rightPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.rightPlayerMasterScene.SetLeftPlayerGameScore(this.LeftPlayerScore);
                this.rightPlayerMasterScene.SetRightPlayerGameScore(this.RightPlayerScore);
            }
        }

        #endregion


        #region Events.Outgoing

        public event EventHandler TopTimerAlarm1Fired;
        protected void on_TopTimerAlarm1Fired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.TopTimerAlarm1Fired, e); }

        public event EventHandler TopTimerAlarm2Fired;
        protected void on_TopTimerAlarm2Fired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.TopTimerAlarm2Fired, e); }

        public event EventHandler TopTimerStopFired;
        protected void on_TopTimerStopFired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.TopTimerStopFired, e); }

        public event EventHandler BottomTimerAlarm1Fired;
        protected void on_BottomTimerAlarm1Fired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.BottomTimerAlarm1Fired, e); }

        public event EventHandler BottomTimerAlarm2Fired;
        protected void on_BottomTimerAlarm2Fired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.BottomTimerAlarm2Fired, e); }

        public event EventHandler BottomTimerStopFired;
        protected void on_BottomTimerStopFired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.BottomTimerStopFired, e); }

        #endregion

        #region Events.Incoming

        void buzzerHandler_BuzUnit_Buzzered(object sender, IOnet.IOUnit.IONbuz.BuzzerIDParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_Buzzered);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_BuzUnit_Buzzered(object content) {
            IOnet.IOUnit.IONbuz.BuzzerIDParam_EventArgs e = content as IOnet.IOUnit.IONbuz.BuzzerIDParam_EventArgs;
            if (this.isActive &&
                e is IOnet.IOUnit.IONbuz.BuzzerIDParam_EventArgs &&
                e.Arg.Name == this.IOUnitName) {
                if (e.Arg.BuzzerID == this.LeftPlayerBuzzerChannel) this.DoBuzzer(PlayerSelection.LeftPlayer);
                else if (e.Arg.BuzzerID == this.RightPlayerBuzzerChannel) this.DoBuzzer(PlayerSelection.RightPlayer);
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
                this.fillBuzzerUnitList(e.Arg);
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

        protected void timer_TopTimerAlarm1Fired(object sender, EventArgs e) {
            this.on_TopTimerAlarm1Fired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_TopTimerAlarm1Fired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_TopTimerAlarm1Fired(object content) {
        }

        protected void timer_TopTimerAlarm2Fired(object sender, EventArgs e) {
            this.on_TopTimerAlarm2Fired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_TopTimerAlarm2Fired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_TopTimerAlarm2Fired(object content) {
        }

        protected void timer_TopTimerStopFired(object sender, EventArgs e) {
            this.on_TopTimerStopFired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_TopTimerStopFired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_TopTimerStopFired(object content) {
        }

        protected void timer_BottomTimerAlarm1Fired(object sender, EventArgs e) {
            this.on_BottomTimerAlarm1Fired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_BottomTimerAlarm1Fired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_BottomTimerAlarm1Fired(object content) {
        }

        protected void timer_BottomTimerAlarm2Fired(object sender, EventArgs e) {
            this.on_BottomTimerAlarm2Fired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_BottomTimerAlarm2Fired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_BottomTimerAlarm2Fired(object content) {
        }

        protected void timer_BottomTimerStopFired(object sender, EventArgs e) {
            this.on_BottomTimerStopFired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_BottomTimerStopFired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_BottomTimerStopFired(object content) {
        }

        protected void timer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_PropertyChanged(object content) {
        }

        #endregion

    }
}
