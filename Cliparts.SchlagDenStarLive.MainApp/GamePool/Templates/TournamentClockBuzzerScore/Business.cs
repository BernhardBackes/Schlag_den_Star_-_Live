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

using Cliparts.Tools.Base;

using Cliparts.IOnet.IOUnit.IONbase;
using Cliparts.IOnet.IOUnit.IONbuz;

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;
using Cliparts.SchlagDenStarLive.MainApp.Settings;
using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TournamentClockBuzzerScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TournamentClockBuzzerScore {

    public class Business : _Base.Score.Business {

        #region Properties

        private int insertTimerPositionX = 0;
        public int InsertTimerPositionX {
            get { return this.insertTimerPositionX; }
            set {
                if (this.insertTimerPositionX != value) {
                    this.insertTimerPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
                }
            }
        }

        private int insertTimerPositionY = 0;
        public int InsertTimerPositionY {
            get { return this.insertTimerPositionY; }
            set {
                if (this.insertTimerPositionY != value) {
                    this.insertTimerPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
                }
            }
        }

        private int fullscreenTimerPositionX = 0;
        public int FullscreenTimerPositionX {
            get { return this.fullscreenTimerPositionX; }
            set {
                if (this.fullscreenTimerPositionX != value) {
                    this.fullscreenTimerPositionX = value;
                    this.on_PropertyChanged();
                    this.Vfullscreen_SetTimer();
                }
            }
        }

        private int fullscreenTimerPositionY = 0;
        public int FullscreenTimerPositionY {
            get { return this.fullscreenTimerPositionY; }
            set {
                if (this.fullscreenTimerPositionY != value) {
                    this.fullscreenTimerPositionY = value;
                    this.on_PropertyChanged();
                    this.Vfullscreen_SetTimer();
                }
            }
        }

        private int fullscreenTimerScaling = 100;
        public int FullscreenTimerScaling {
            get { return this.fullscreenTimerScaling; }
            set {
                if (this.fullscreenTimerScaling != value) {
                    if (value < 25) this.FullscreenTimerScaling = 25;
                    else this.fullscreenTimerScaling = value;
                    this.on_PropertyChanged();
                    this.Vfullscreen_SetTimer();
                }
            }
        }

        private int timerStartTime = 60;
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

        private int leftTimerCurrentTime = -1;
        public int LeftTimerCurrentTime {
            get { return this.leftTimerCurrentTime; }
            protected set {
                if (this.leftTimerCurrentTime != value) {
                    this.leftTimerCurrentTime = value;
                    this.Vinsert_SetTimer();
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

        private int rightTimerCurrentTime = -1;
        public int RightTimerCurrentTime {
            get { return this.rightTimerCurrentTime; }
            protected set {
                if (this.rightTimerCurrentTime != value) {
                    this.rightTimerCurrentTime = value;
                    this.Vinsert_SetTimer();
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
                    if (value == null)
                        value = string.Empty;
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
                    if (value < 1)
                        value = 1;
                    if (value > 8)
                        value = 8;
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
                    if (value < 1)
                        value = 1;
                    if (value > 8)
                        value = 8;
                    this.rightPlayerBuzzerChannel = value;
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

        #endregion


        #region Funktionen

        public Business() { }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.TournamentClockBuzzerScore'", typeIdentifier);
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
            this.buzzerHandler.BuzUnit_Buzzered += this.buzzerHandler_BuzUnit_Buzzered;
            this.buzzerHandler.Unit_ConnectionStatusChanged += this.buzzerHandler_UnitConnectionStatusChanged;
            this.buzzerHandler.Unit_ConnectionStatusRequest += this.buzzerHandler_UnitConnectionStatusRequest;
            this.buzzerHandler.Unit_InfoListChanged += this.buzzerHandler_UnitInfoListChanged;
            this.buzzerHandler.BuzUnit_WorkmodeChanged += this.buzzerHandler_BuzUnit_WorkmodeChanged;
            this.buzzerHandler.BuzUnit_WorkmodeRequest += this.buzzerHandler_BuzUnit_WorkmodeRequest;
            this.fillBuzzerUnitList(this.buzzerHandler.UnitInfoList);
            this.buzzerHandler.RequestUnitConnectionStatus(this.IOUnitName);
            this.buzzerHandler.RequestUnitWorkMode(this.IOUnitName);

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;

            this.fullscreenScene = new Fullscreen(syncContext, fullscreenMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.fullscreenScene.StatusChanged += this.fullscreenScene_StatusChanged;
            this.fullscreenScene.PropertyChanged += this.fullscreenScene_PropertyChanged;
            this.fullscreenScene.TwoTimers.PropertyChanged += this.twoTimers_PropertyChanged;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);
        }

        public override void Dispose() {
            base.Dispose();

            this.buzzerHandler.BuzUnit_Buzzered += this.buzzerHandler_BuzUnit_Buzzered;
            this.buzzerHandler.Unit_ConnectionStatusChanged += this.buzzerHandler_UnitConnectionStatusChanged;
            this.buzzerHandler.Unit_ConnectionStatusRequest += this.buzzerHandler_UnitConnectionStatusRequest;
            this.buzzerHandler.Unit_InfoListChanged += this.buzzerHandler_UnitInfoListChanged;
            this.buzzerHandler.BuzUnit_WorkmodeChanged += this.buzzerHandler_BuzUnit_WorkmodeChanged;
            this.buzzerHandler.BuzUnit_WorkmodeRequest += this.buzzerHandler_BuzUnit_WorkmodeRequest;

            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;

            this.fullscreenScene.StatusChanged -= this.fullscreenScene_StatusChanged;
            this.fullscreenScene.PropertyChanged -= this.fullscreenScene_PropertyChanged;
            this.fullscreenScene.TwoTimers.PropertyChanged -= this.twoTimers_PropertyChanged;
            this.fullscreenScene.Dispose();
        }

        private void fillBuzzerUnitList(
            Cliparts.IOnet.IOUnit.IONbase.InfoParam[] unitInfoList) {
            this.ioUnitNameList.Clear();
            if (unitInfoList is Cliparts.IOnet.IOUnit.IONbase.InfoParam[]) {
                foreach (Cliparts.IOnet.IOUnit.IONbase.InfoParam item in unitInfoList) {
                    if (item is Cliparts.IOnet.IOUnit.IONbase.InfoParam)
                        this.ioUnitNameList.Add(item.Name);
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
            if (buzzerHandler is BuzzerIO.Business) {
                this.buzzerHandler.RequestUnitConnectionStatus(unitName);
                this.buzzerHandler.RequestUnitWorkMode(unitName);
            }
        }

        public override void Init() {
            base.Init();
        }

        public void ResetTimer() {
            this.Vfullscreen_StopBothTimers();
            this.Vfullscreen_ResetBothTimers();
        }

        public override void Next() {
            if (this.SelectedPlayer == Content.Gameboard.PlayerSelection.LeftPlayer) this.SelectedPlayer = Content.Gameboard.PlayerSelection.RightPlayer;
            else this.SelectedPlayer = Content.Gameboard.PlayerSelection.LeftPlayer;
            this.Vfullscreen_StopBothTimers();
            this.Vfullscreen_ResetBothTimers();
        }

        public virtual void StartTimer() { this.StartTimer(this.SelectedPlayer); }
        public virtual void StartTimer(
            PlayerSelection player) {
            switch (player) {
                case PlayerSelection.LeftPlayer:
                    this.Vfullscreen_StopRightTimer();
                    this.Vfullscreen_ContinueLeftTimer();
                    break;
                case PlayerSelection.RightPlayer:
                    this.Vfullscreen_StopLeftTimer();
                    this.Vfullscreen_ContinueRightTimer();
                    break;
            }
        }

        public virtual void StopTimer() { this.Vfullscreen_StopBothTimers(); }

        public virtual void ReleaseBuzzer() {
            bool[] inputMask = new bool[8];
            if (this.LeftPlayerBuzzerChannel > 0 &&
                this.LeftPlayerBuzzerChannel <= inputMask.Length)
                inputMask[this.LeftPlayerBuzzerChannel - 1] = true;
            if (this.RightPlayerBuzzerChannel > 0 &&
                this.RightPlayerBuzzerChannel <= inputMask.Length)
                inputMask[this.RightPlayerBuzzerChannel - 1] = true;
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
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.TwoTimersPassiv.ToIn(); 
        }
        public void Vinsert_SetTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetTimer(this.insertScene.TwoTimersPassiv); }
        public void Vinsert_SetTimer(
            VentuzScenes.GamePool._Modules.TwoTimersPassiv scene) {
            if (scene is VentuzScenes.GamePool._Modules.TwoTimersPassiv) {
                scene.SetPositionX(this.InsertTimerPositionX);
                scene.SetPositionY(this.InsertTimerPositionY);
                scene.SetLeftTimer(this.LeftTimerCurrentTime);
                scene.SetRightTimer(this.RightTimerCurrentTime);
            }
        }
        public void Vinsert_TimerOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.TwoTimersPassiv.ToOut(); }
        public override void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score); }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }
        public void Vinsert_PlayJingleStart() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.PlayJingleStart(); }
        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void Vfullscreen_ShowGame() {
            base.Vfullscreen_ShowGame();
            this.Vfullscreen_SetTimer();
        }
        public override void Vfullscreen_LoadScene() { this.fullscreenScene.Load(); }
        public override void Vfullscreen_SetTimer() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene)  this.Vfullscreen_SetTimer(this.fullscreenScene.TwoTimers); }
        public void Vfullscreen_SetTimer(
            VentuzScenes.GamePool._Modules.TwoTimers scene) {
            if (scene is VentuzScenes.GamePool._Modules.TwoTimers) {
                scene.SetPositionX(this.FullscreenTimerPositionX);
                scene.SetPositionY(this.FullscreenTimerPositionY);
                scene.SetScaling(this.FullscreenTimerScaling);
                scene.SetLeftStartTime(this.TimerStartTime);
                scene.SetLeftStopTime(this.TimerStopTime);
                scene.SetLeftAlarmTime1(this.TimerAlarmTime1);
                scene.SetLeftAlarmTime2(this.TimerAlarmTime2);
                scene.SetRightStartTime(this.TimerStartTime);
                scene.SetRightStopTime(this.TimerStopTime);
                scene.SetRightAlarmTime1(this.TimerAlarmTime1);
                scene.SetRightAlarmTime2(this.TimerAlarmTime2);
            }
        }
        internal void Vfullscreen_StartLeftTimer() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene)  this.fullscreenScene.TwoTimers.StartLeftTimer(); }
        internal void Vfullscreen_StopLeftTimer() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene)  this.fullscreenScene.TwoTimers.StopLeftTimer(); }
        internal void Vfullscreen_ContinueLeftTimer() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene)  this.fullscreenScene.TwoTimers.ContinueLeftTimer(); }
        internal void Vfullscreen_ResetLeftTimer() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene)  this.fullscreenScene.TwoTimers.ResetLeftTimer(); }
        internal void Vfullscreen_StartBothTimers() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene)  this.fullscreenScene.TwoTimers.StartBothTimers(); }
        internal void Vfullscreen_StopBothTimers() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene)  this.fullscreenScene.TwoTimers.StopBothTimers(); }
        internal void Vfullscreen_ContinueBothTimers() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene)  this.fullscreenScene.TwoTimers.ContinueBothTimers(); }
        internal void Vfullscreen_ResetBothTimers() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene)  this.fullscreenScene.TwoTimers.ResetBothTimers(); }
        internal void Vfullscreen_StartRightTimer() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene)  this.fullscreenScene.TwoTimers.StartRightTimer(); }
        internal void Vfullscreen_StopRightTimer() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene)  this.fullscreenScene.TwoTimers.StopRightTimer(); }
        internal void Vfullscreen_ContinueRightTimer() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene)  this.fullscreenScene.TwoTimers.ContinueRightTimer(); }
        internal void Vfullscreen_ResetRightTimer() { if (this.fullscreenScene is VRemote4.HandlerSi.Scene)  this.fullscreenScene.TwoTimers.ResetRightTimer(); }
        public override void Vfullscreen_UnloadScene() { this.fullscreenScene.Unload(); }

        public override void ClearGraphic() {
            base.ClearGraphic();
            this.insertScene.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming

        void buzzerHandler_BuzUnit_Buzzered(object sender, IOnet.IOUnit.IONbuz.BuzzerIDParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_Buzzered);
            if (this.syncContext != null)
                this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_BuzUnit_Buzzered(object content) {
            IOnet.IOUnit.IONbuz.BuzzerIDParam_EventArgs e = content as IOnet.IOUnit.IONbuz.BuzzerIDParam_EventArgs;
            if (this.isActive &&
                e is IOnet.IOUnit.IONbuz.BuzzerIDParam_EventArgs &&
                e.Arg.Name == this.IOUnitName) {
                if (e.Arg.BuzzerID == this.LeftPlayerBuzzerChannel) this.StartTimer(PlayerSelection.RightPlayer);
                else if (e.Arg.BuzzerID == this.RightPlayerBuzzerChannel) this.StartTimer(PlayerSelection.LeftPlayer);
            }
        }

        void buzzerHandler_UnitConnectionStatusChanged(object sender, IOnet.IOUnit.IONbase.ConnectionStatusParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_UnitConnectionStatusChanged);
            if (this.syncContext != null)
                this.syncContext.Post(callback, e);
        }
        void buzzerHandler_UnitConnectionStatusRequest(object sender, IOnet.IOUnit.IONbase.ConnectionStatusParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_UnitConnectionStatusChanged);
            if (this.syncContext != null)
                this.syncContext.Post(callback, e);
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
            if (this.syncContext != null)
                this.syncContext.Post(callback, e);
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
            if (this.syncContext != null)
                this.syncContext.Post(callback, e);
        }
        void buzzerHandler_BuzUnit_WorkmodeRequest(object sender, IOnet.IOUnit.IONbuz.WorkModeParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_WorkmodeChanged);
            if (this.syncContext != null)
                this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_BuzUnit_WorkmodeChanged(object content) {
            IOnet.IOUnit.IONbuz.WorkModeParam_EventArgs e = content as IOnet.IOUnit.IONbuz.WorkModeParam_EventArgs;
            if (e is IOnet.IOUnit.IONbuz.WorkModeParam_EventArgs &&
                e.Arg.Name == this.IOUnitName) {
                this.ioUnitWorkMode = e.Arg.WorkMode;
                this.checkIOUnitStatus();
            }
        }

        void fullscreenScene_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_fullscreenScene_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_fullscreenScene_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
            }
        }

        void twoTimers_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_twoTimers_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_twoTimers_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "LeftCurrentTime") this.LeftTimerCurrentTime = this.fullscreenScene.TwoTimers.LeftCurrentTime;
                else if (e.PropertyName == "LeftIsRunning") this.LeftTimerIsRunning = this.fullscreenScene.TwoTimers.LeftIsRunning;
                else if (e.PropertyName == "RightCurrentTime") this.RightTimerCurrentTime = this.fullscreenScene.TwoTimers.RightCurrentTime;
                else if (e.PropertyName == "RightIsRunning") this.RightTimerIsRunning = this.fullscreenScene.TwoTimers.RightIsRunning;
            }
        }


        #endregion

    }
}
