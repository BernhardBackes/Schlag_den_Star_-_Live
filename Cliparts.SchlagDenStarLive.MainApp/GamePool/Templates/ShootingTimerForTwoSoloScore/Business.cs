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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.ShootingTimerForTwoSoloScore;
using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.ShootingTimerForTwoSoloScore {

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

        private int leftPlayerHeats = 0;
        public int LeftPlayerHeats {
            get { return this.leftPlayerHeats; }
            set {
                if (this.leftPlayerHeats != value) {
                    if (value < 0) value = 0;
                    this.leftPlayerHeats = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerHeats = 0;
        public int RightPlayerHeats {
            get { return this.rightPlayerHeats; }
            set {
                if (this.rightPlayerHeats != value) {
                    if (value < 0) value = 0;
                    this.rightPlayerHeats = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int leftPlayerHits = 0;
        public int LeftPlayerHits {
            get { return this.leftPlayerHits; }
            set {
                if (this.leftPlayerHits != value) {
                    if (value < 0) value = 0;
                    this.leftPlayerHits = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerHits = 0;
        public int RightPlayerHits {
            get { return this.rightPlayerHits; }
            set {
                if (this.rightPlayerHits != value) {
                    if (value < 0) value = 0;
                    this.rightPlayerHits = value;
                    this.on_PropertyChanged();
                }
            }
        }

        public int HeatsCount {
            get {
                int result = 2;
                switch (this.ShootingStyle) {
                    case ShootingTimerForTwo.Styles.TwoHeats:
                        result = 2;
                        break;
                    case ShootingTimerForTwo.Styles.ThreeHeats:
                        result = 3;
                        break;
                    case ShootingTimerForTwo.Styles.FourHeats:
                        result = 4;
                        break;
                    case ShootingTimerForTwo.Styles.FiveHeats:
                        result = 5;
                        break;
                }
                return result;
            }
        }

        private int shootingPositionX = 0;
        public int ShootingPositionX {
            get { return this.shootingPositionX; }
            set {
                if (this.shootingPositionX != value) {
                    this.shootingPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetShooting();
                }
            }
        }

        private int shootingPositionY = 0;
        public int ShootingPositionY {
            get { return this.shootingPositionY; }
            set {
                if (this.shootingPositionY != value) {
                    this.shootingPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetShooting();
                }
            }
        }

        private ShootingTimerForTwo.Styles shootingStyle = ShootingTimerForTwo.Styles.ThreeHeats;
        public ShootingTimerForTwo.Styles ShootingStyle {
            get { return this.shootingStyle; }
            set {
                if (this.shootingStyle != value) {
                    this.shootingStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetShooting();
                    this.on_PropertyChanged("HeatsCount");
                }
            }
        }

        private int hitsCount = 3;
        public int HitsCount {
            get { return this.hitsCount; }
            set {
                if (this.hitsCount != value) {
                    if (value < 2) this.hitsCount = 2;
                    else if (value > 5) this.hitsCount = 5;
                    else this.hitsCount = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetShooting();
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.ShootingTimerForTwoSoloScore'", typeIdentifier);
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
            this.insertScene.ShootingTimerForTwo.StopFired += this.insertScene_StopFired;
            this.insertScene.ShootingTimerForTwo.PreciseTimeReceived += this.insertScene_PreciseTimeReceived;
            this.insertScene.ShootingTimerForTwo.PropertyChanged += this.insertScene_PropertyChanged;

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
            this.insertScene.ShootingTimerForTwo.StopFired -= this.insertScene_StopFired;
            this.insertScene.ShootingTimerForTwo.PreciseTimeReceived -= this.insertScene_PreciseTimeReceived;
            this.insertScene.ShootingTimerForTwo.PropertyChanged -= this.insertScene_PropertyChanged;
        }

        public override void ResetData() {
            base.ResetData();
            this.TimeToBeat = null;
            this.timeIsRunning = false;
            this.FirstFinisher = PlayerSelection.NotSelected;
            this.LeftFinishReached = false;
            this.RightFinishReached = false;
            this.LeftPlayerHeats = 0;
            this.LeftPlayerHits = 0;
            this.RightPlayerHeats = 0;
            this.RightPlayerHits = 0;
        }

        public override void Next() {
            this.TimeToBeat = null;
            this.timeIsRunning = false;
            this.FirstFinisher = PlayerSelection.NotSelected;
            this.LeftFinishReached = false;
            this.RightFinishReached = false;
            this.LeftPlayerHeats = 0;
            this.LeftPlayerHits = 0;
            this.RightPlayerHeats = 0;
            this.RightPlayerHits = 0;
        }

        internal void LeftPlayerNextHeat() {
            this.Vinsert_ShootingLeftPlayerHitsOut();
            this.LeftPlayerHits = 0;
        }

        internal void RightPlayerNextHeat() {
            this.Vinsert_ShootingRightPlayerHitsOut();
            this.RightPlayerHits = 0;
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
            if (this.buzzerHandler is BuzzerIO.Business) {
                this.buzzerHandler.RequestUnitConnectionStatus(unitName);
                this.buzzerHandler.RequestUnitWorkMode(unitName);
            }
        }

        public virtual void PassFinishLine(
            PlayerSelection finisher) {
            if (finisher != PlayerSelection.NotSelected) {
                int currentLap = this.LeftPlayerHeats;
                if (finisher == PlayerSelection.RightPlayer) currentLap = this.RightPlayerHeats;
                if (currentLap == this.HeatsCount) {
                    if (this.FirstFinisher == PlayerSelection.NotSelected) {
                        this.FirstFinisher = finisher;
                        this.Vinsert_GetPreciseTime();
                        if (this.FirstFinisher == PlayerSelection.LeftPlayer)
                            this.insertScene.ShootingTimerForTwo.FinishToInTop();
                        else if (this.FirstFinisher == PlayerSelection.RightPlayer)
                            this.insertScene.ShootingTimerForTwo.FinishToInBottom();
                    }
                    else {
                        this.Vinsert_StopTimer();
                        this.LockTimeline();
                        this.LockBuzzer();
                    }
                }
                //switch (finisher) {
                //    case PlayerSelection.LeftPlayer:
                //        this.LeftPlayerHeats++;
                //        break;
                //    case PlayerSelection.RightPlayer:
                //        this.RightPlayerHeats++;
                //        break;
                //}
                this.Vinsert_SetShooting();
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
            this.buzzerHandler.SetDMXMode(this.IOUnitName, IOnet.IOUnit.IONbuzDX.DMXBuzzerModes.CHANNEL);
            this.buzzerHandler.SetInputMask(this.IOUnitName, inputMask);
            this.buzzerHandler.ReleaseBuzzer(this.IOUnitName, WorkModes.EVENT);
        }

        public virtual void LockBuzzer() {
            this.buzzerHandler.LockBuzzer(this.IOUnitName);
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }
        public virtual void Vinsert_ShootingIn() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vinsert_ShootingIn(this.insertScene.ShootingTimerForTwo); }
        public void Vinsert_ShootingIn(
            ShootingTimerForTwo scene) {
            this.Vinsert_ResetTimer();
            if (scene is ShootingTimerForTwo) {
                this.Vinsert_SetShooting(scene, this.LeftPlayerHeats, this.LeftPlayerHits, this.RightPlayerHeats, this.RightPlayerHits);
                scene.ToIn();
            }
        }
        public virtual void Vinsert_SetShooting() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vinsert_SetShooting(this.insertScene.ShootingTimerForTwo); }
        public void Vinsert_SetShooting(ShootingTimerForTwo scene) { this.Vinsert_SetShooting(scene, this.LeftPlayerHeats, this.LeftPlayerHits, this.RightPlayerHeats, this.RightPlayerHits); }
        public void Vinsert_SetShooting(
            ShootingTimerForTwo scene,
            int leftPlayerHeats,
            int leftPlayerHits,
            int rightPlayerHeats,
            int rightPlayerHits) {
            if (scene is ShootingTimerForTwo) {
                scene.SetPositionX(this.ShootingPositionX);
                scene.SetPositionY(this.ShootingPositionY);
                scene.SetStyle(this.ShootingStyle);
                scene.SetHitsCountMax(this.HitsCount);
                scene.SetLeftTopName(this.LeftPlayerName);
                scene.SetLeftTopHeats(leftPlayerHeats);
                scene.SetLeftTopHits(leftPlayerHits);
                scene.SetRightBottomName(this.RightPlayerName);
                scene.SetRightBottomHeats(rightPlayerHeats);
                scene.SetRightBottomHits(rightPlayerHits);
            }
        }
        public virtual void Vinsert_ShootingLeftPlayerHitsIn() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vinsert_ShootingLeftPlayerHitsIn(this.insertScene.ShootingTimerForTwo); }
        public void Vinsert_ShootingLeftPlayerHitsIn(
            ShootingTimerForTwo scene) {
            if (scene is ShootingTimerForTwo) {
                this.Vinsert_SetShooting(scene, this.LeftPlayerHeats, this.LeftPlayerHits, this.RightPlayerHeats, this.RightPlayerHits);
                scene.LeftTopHitsToIn();
            }
        }
        public virtual void Vinsert_ShootingLeftPlayerHitsOut() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vinsert_ShootingLeftPlayerHitsOut(this.insertScene.ShootingTimerForTwo); }
        public void Vinsert_ShootingLeftPlayerHitsOut(
            ShootingTimerForTwo scene) {
            if (scene is ShootingTimerForTwo) scene.LeftTopHitsToOut();
        }
        public virtual void Vinsert_ShootingLeftPlayerHitMiss() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vinsert_ShootingLeftPlayerHitMiss(this.insertScene.ShootingTimerForTwo); }
        public void Vinsert_ShootingLeftPlayerHitMiss(
            ShootingTimerForTwo scene) {
            if (scene is ShootingTimerForTwo) scene.LeftTopHitsMiss();
        }
        public virtual void Vinsert_ShootingRightPlayerHitsIn() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vinsert_ShootingRightPlayerHitsIn(this.insertScene.ShootingTimerForTwo); }
        public void Vinsert_ShootingRightPlayerHitsIn(
            ShootingTimerForTwo scene) {
            if (scene is ShootingTimerForTwo) {
                this.Vinsert_SetShooting(scene, this.LeftPlayerHeats, this.LeftPlayerHits, this.RightPlayerHeats, this.RightPlayerHits);
                scene.RightBottomHitsToIn();
            }
        }
        public virtual void Vinsert_ShootingRightPlayerHitsOut() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vinsert_ShootingRightPlayerHitsOut(this.insertScene.ShootingTimerForTwo); }
        public void Vinsert_ShootingRightPlayerHitsOut(
            ShootingTimerForTwo scene) {
            if (scene is ShootingTimerForTwo) scene.RightBottomHitsToOut();
        }
        public virtual void Vinsert_ShootingRightPlayerHitMiss() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vinsert_ShootingRightPlayerHitMiss(this.insertScene.ShootingTimerForTwo); }
        public void Vinsert_ShootingRightPlayerHitMiss(
            ShootingTimerForTwo scene) {
            if (scene is ShootingTimerForTwo) scene.RightBottomHitsMiss();
        }
        public void Vinsert_StartTimer() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.ShootingTimerForTwo.StartTimer();
                this.timeIsRunning = true;
            }
        }
        public void Vinsert_GetPreciseTime() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.ShootingTimerForTwo.GetPreciseTime();
        }
        public void Vinsert_StopTimer() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.ShootingTimerForTwo.StopTimer();
        }
        public void Vinsert_ContinueTimer() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.ShootingTimerForTwo.ContinueTimer(); 
        }
        public void Vinsert_ResetTimer() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.ShootingTimerForTwo.ResetTimer();
        }
        public void Vinsert_SetShowOffsetOut() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.ShootingTimerForTwo.SetOffsetOut();
        }
        public void Vinsert_ShowOffsetTimeTop(
            float offset) {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.ShootingTimerForTwo.SetOffsetValue(offset);
                this.insertScene.ShootingTimerForTwo.OffsetToInTop();
            }
        }
        public void Vinsert_ShowOffsetTimeBottom(
            float offset) {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.ShootingTimerForTwo.SetOffsetValue(offset);
                this.insertScene.ShootingTimerForTwo.OffsetToInBottom();
            }
        }
        public virtual void Vinsert_ShootingOut() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vinsert_ShootingOut(this.insertScene.ShootingTimerForTwo); }
        public void Vinsert_ShootingOut(
            ShootingTimerForTwo scene) {
            if (scene is ShootingTimerForTwo) scene.ToOut();
        }

        public override void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score); }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }
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
                if (this.insertScene.ShootingTimerForTwo.CurrentTime > 0 &&
                    this.timeToBeat.HasValue) {
                    this.TimerCurrentTime = Convert.ToDouble(this.insertScene.ShootingTimerForTwo.CurrentTime);
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
                if (this.insertScene.ShootingTimerForTwo.PreciseTime > 0 &&
                    !this.timeToBeat.HasValue) {
                    this.timeToBeat = this.insertScene.ShootingTimerForTwo.PreciseTime;
                    this.on_PropertyChanged("TimeToBeat");
                }
            }
        }

        protected void insertScene_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timeToBeat_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timeToBeat_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "CurrentTime") this.TimerCurrentTime = Convert.ToDouble(this.insertScene.ShootingTimerForTwo.CurrentTime);
            }
        }

        #endregion

    }
}
