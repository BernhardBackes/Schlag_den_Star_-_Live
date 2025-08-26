using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cliparts.Tools.Base;

using Cliparts.IOnet.IOUnit.IONbase;
using Cliparts.IOnet.IOUnit.IONbuz;

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;
using Cliparts.SchlagDenStarLive.MainApp.Settings;
using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.CounterScore {

    public enum WinnerModes {
        HighestCounter,
        LowestCounter
    }

    public class Business : _Base.Business {

        #region Properties

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

        private int leftPlayerCounter = 0;
        public int LeftPlayerCounter {
            get { return this.leftPlayerCounter; }
            set {
                if (this.leftPlayerCounter != value) {
                    this.leftPlayerCounter = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool leftPlayerCounterIsVisible = true;
        public bool LeftPlayerCounterIsVisible {
            get { return this.leftPlayerCounterIsVisible; }
            set {
                if (this.leftPlayerCounterIsVisible != value) {
                    this.leftPlayerCounterIsVisible = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounterIsVisible();
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

        private int rightPlayerCounter = 0;
        public int RightPlayerCounter {
            get { return this.rightPlayerCounter; }
            set {
                if (this.rightPlayerCounter != value) {
                    this.rightPlayerCounter = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool rightPlayerCounterIsVisible = true;
        public bool RightPlayerCounterIsVisible {
            get { return this.rightPlayerCounterIsVisible; }
            set {
                if (this.rightPlayerCounterIsVisible != value) {
                    this.rightPlayerCounterIsVisible = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounterIsVisible();
                }
            }
        }

        private bool hidePlayerCounterByDefault = true;
        public bool HidePlayerCounterByDefault {
            get { return this.hidePlayerCounterByDefault; }
            set {
                if (this.hidePlayerCounterByDefault != value) {
                    this.hidePlayerCounterByDefault = value;
                    this.on_PropertyChanged();
                    if (!value) {
                        this.LeftPlayerCounterIsVisible = true;
                        this.RightPlayerCounterIsVisible = true;
                    }
                    this.Vinsert_SetCounterIsVisible();
                }
            }
        }

        private WinnerModes winnerMode = WinnerModes.HighestCounter;
        public WinnerModes WinnerMode {
            get { return this.winnerMode; }
            set {
                if (this.winnerMode != value) {
                    this.winnerMode = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int counterScorePositionX = 0;
        public int CounterScorePositionX {
            get { return this.counterScorePositionX; }
            set {
                if (this.counterScorePositionX != value) {
                    this.counterScorePositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounterScore();
                }
            }
        }

        private int counterScorePositionY = 0;
        public int CounterScorePositionY {
            get { return this.counterScorePositionY; }
            set {
                if (this.counterScorePositionY != value) {
                    this.counterScorePositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounterScore();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.CounterScore.Styles counterScoreStyle = VentuzScenes.GamePool._Modules.CounterScore.Styles.ThreeDots;
        public VentuzScenes.GamePool._Modules.CounterScore.Styles CounterScoreStyle {
            get { return this.counterScoreStyle; }
            set {
                if (this.counterScoreStyle != value) {
                    this.counterScoreStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounterScore();
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
                    this.Vinsert_SetCounterScore();
                    this.Vfullscreen_SetCounter();
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

        public override void ResetData() {
            base.ResetData();
            this.LeftPlayerScore = 0;
            this.LeftPlayerCounter = 0;
            this.RightPlayerScore = 0;
            this.RightPlayerCounter = 0;
            if (this.HidePlayerCounterByDefault) {
                this.LeftPlayerCounterIsVisible = false;
                this.RightPlayerCounterIsVisible = false;
            }
            else {
                this.LeftPlayerCounterIsVisible = true;
                this.RightPlayerCounterIsVisible = true;
            }
        }

        public virtual void Resolve() {
            switch (this.WinnerMode) {
                case WinnerModes.LowestCounter:
                    if (this.LeftPlayerCounter < this.RightPlayerCounter) this.LeftPlayerScore++;
                    else if (this.RightPlayerCounter < this.LeftPlayerCounter) this.RightPlayerScore++;
                    break;
                case WinnerModes.HighestCounter:
                default:
                    if (this.LeftPlayerCounter > this.RightPlayerCounter) this.LeftPlayerScore++;
                    else if (this.RightPlayerCounter > this.LeftPlayerCounter) this.RightPlayerScore++;
                    break;
            }
        }

        public override void Next() {
            base.Next();
            this.LeftPlayerCounter = 0;
            this.RightPlayerCounter = 0;
            if (this.HidePlayerCounterByDefault) {
                this.LeftPlayerCounterIsVisible = false;
                this.RightPlayerCounterIsVisible = false;
            }
            else {
                this.LeftPlayerCounterIsVisible = true;
                this.RightPlayerCounterIsVisible = true;
            }
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
            if (buzzerHandler is BuzzerIO.Business) {
                this.buzzerHandler.RequestUnitConnectionStatus(unitName);
                this.buzzerHandler.RequestUnitWorkMode(unitName);
            }
        }

        private void setDMXValues() {
            this.buzzerHandler.SetDMXMode(this.IOUnitName, IOnet.IOUnit.IONbuzDX.DMXBuzzerModes.CHANNEL);
            this.buzzerHandler.SetDMXSettings(this.IOUnitName, 0, 255);
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
            this.Vinsert_SetCounterScore();
            this.Vfullscreen_SetCounter();
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

        public virtual void Vinsert_CounterScoreIn() { }
        public virtual void Vinsert_CounterScoreIn(VentuzScenes.GamePool._Modules.CounterScore scene) {
            this.Vinsert_SetCounterScore(scene);
            if (scene is VentuzScenes.GamePool._Modules.CounterScore) scene.ToIn();
        }
        public virtual void Vinsert_SetCounterScore() { }
        public void Vinsert_SetCounterScore(VentuzScenes.GamePool._Modules.CounterScore scene) {
            this.Vinsert_SetCounterScore(scene, this.LeftPlayerScore, this.LeftPlayerCounter, this.LeftPlayerCounterIsVisible, this.RightPlayerScore, this.RightPlayerCounter, this.RightPlayerCounterIsVisible);
        }
        public void Vinsert_SetCounterScore(
            VentuzScenes.GamePool._Modules.CounterScore scene,
            int leftPlayerScore,
            int leftPlayerCounter,
            bool leftPlayerCounterIsVisible,
            int rightPlayerScore,
            int rightPlayerCounter,
            bool rightPlayerCounterIsVisible) {
            if (scene is VentuzScenes.GamePool._Modules.CounterScore) {
                scene.SetPositionX(this.CounterScorePositionX);
                scene.SetPositionY(this.CounterScorePositionY);
                scene.SetFlipPosition(this.CounterFlipPlayers);
                scene.SetStyle(this.CounterScoreStyle);
                scene.SetTopName(this.LeftPlayerName);
                scene.SetTopScore(leftPlayerScore);
                scene.SetTopCounter(leftPlayerCounter);
                scene.SetTopCounterIsVisible(leftPlayerCounterIsVisible);
                scene.SetBottomName(this.RightPlayerName);
                scene.SetBottomScore(rightPlayerScore);
                scene.SetBottomCounter(rightPlayerCounter);
                scene.SetBottomCounterIsVisible(rightPlayerCounterIsVisible);
            }
        }
        public virtual void Vinsert_SetCounterIsVisible() { }
        public void Vinsert_SetCounterIsVisible(VentuzScenes.GamePool._Modules.CounterScore scene) {
            this.Vinsert_SetCounterIsVisible(scene, this.LeftPlayerCounterIsVisible, this.RightPlayerCounterIsVisible); 
        }
        public void Vinsert_SetCounterIsVisible(
            VentuzScenes.GamePool._Modules.CounterScore scene,
            bool leftPlayerCounterIsVisible,
            bool rightPlayerCounterIsVisible) {
            if (scene is VentuzScenes.GamePool._Modules.CounterScore) {
                scene.SetTopCounterIsVisible(leftPlayerCounterIsVisible);
                scene.SetBottomCounterIsVisible(rightPlayerCounterIsVisible);
            }
        }
        public virtual void Vinsert_CounterScoreOut() { }
        public void Vinsert_CounterScoreOut(VentuzScenes.GamePool._Modules.CounterScore scene) { if (scene is VentuzScenes.GamePool._Modules.CounterScore) scene.ToOut(); }

        public virtual void Vfullscreen_SetCounter() { }
        public void Vfullscreen_SetCounter(VentuzScenes.Fullscreen.Score scene) { this.Vfullscreen_SetCounter(scene, this.LeftPlayerCounter, this.RightPlayerCounter); }
        public void Vfullscreen_SetCounter(
            VentuzScenes.Fullscreen.Score scene,
            int leftPlayerCounter,
            int rightPlayerCounter) {
            if (scene is VentuzScenes.Fullscreen.Score) {
                scene.SetFlipPosition(this.CounterFlipPlayers);
                scene.SetLeftName(this.LeftPlayerName);
                scene.SetLeftScore(leftPlayerCounter);
                scene.SetRightName(this.RightPlayerName);
                scene.SetRightScore(rightPlayerCounter);
            }
        }

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
                if (e.Arg.BuzzerID == this.LeftPlayerBuzzerChannel) {
                    if (this.CounterFlipPlayers) this.DoCounter(PlayerSelection.RightPlayer);
                    else this.DoCounter(PlayerSelection.LeftPlayer);
                }
                else if (e.Arg.BuzzerID == this.RightPlayerBuzzerChannel) {
                    if (this.CounterFlipPlayers) this.DoCounter(PlayerSelection.LeftPlayer);
                    else this.DoCounter(PlayerSelection.RightPlayer);
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

        #endregion

    }
}
