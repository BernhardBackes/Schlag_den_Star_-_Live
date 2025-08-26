using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cliparts.IOnet.IOUnit.IONbase;
using Cliparts.IOnet.IOUnit.IONbuz;

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;
using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;
using Cliparts.Serialization;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimerRandomBuzzerScoreShotclock;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimerRandomBuzzerScoreShotclock {

    public enum ActiveBuzzerStates { NA, Top, Left, Right, Bottom }

    public class Business : _Base.Timer.Business {

        #region Properties

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

        private int scorePositionX = 0;
        public int ScorePositionX {
            get { return this.scorePositionX; }
            set {
                if (this.scorePositionX != value) {
                    this.scorePositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetScore();
                }
            }
        }

        private int scorePositionY = 0;
        public int ScorePositionY {
            get { return this.scorePositionY; }
            set {
                if (this.scorePositionY != value) {
                    this.scorePositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetScore();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.Score.Styles scoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots;
        public VentuzScenes.GamePool._Modules.Score.Styles ScoreStyle {
            get { return this.scoreStyle; }
            set {
                if (this.scoreStyle != value) {
                    this.scoreStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetScore();
                }
            }
        }

        private bool flipPlayers;
        public bool FlipPlayers {
            get { return this.flipPlayers; }
            set {
                if (this.flipPlayers != value) {
                    this.flipPlayers = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetScore();
                }
            }
        }

        private int shotclockStartTime = 15;
        public int ShotclockStartTime {
            get { return this.shotclockStartTime; }
            set {
                if (this.shotclockStartTime != value) {
                    this.shotclockStartTime = value;
                    this.on_PropertyChanged();
                    this.Vfullscreen_SetTimer();
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

        private Color onColor = Color.White;
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

        private Color scoreColor = Color.Green;
        public Color ScoreColor {
            get { return this.scoreColor; }
            set {
                if (this.scoreColor != value) {
                    this.scoreColor = value;
                    this.on_PropertyChanged();
                    this.setDMXValues();
                }
            }
        }

        private Color timeoutColor = Color.Red;
        public Color TimeoutColor {
            get { return this.timeoutColor; }
            set {
                if (this.timeoutColor != value) {
                    this.timeoutColor = value;
                    this.on_PropertyChanged();
                    this.setDMXValues();
                }
            }
        }

        private ActiveBuzzerStates activeBuzzer = ActiveBuzzerStates.NA;
        public ActiveBuzzerStates ActiveBuzzer {
            get { return this.activeBuzzer; }
            set {
                if (this.activeBuzzer != value) {
                    this.activeBuzzer = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private ActiveBuzzerStates startBuzzer = ActiveBuzzerStates.Top;
        public ActiveBuzzerStates StartBuzzer {
            get { return this.startBuzzer; }
            set {
                if (this.startBuzzer != value) {
                    if (value == ActiveBuzzerStates.NA) this.startBuzzer = ActiveBuzzerStates.Top;
                    else this.startBuzzer = value;
                    this.on_PropertyChanged();
                }
            }
        }


        private ActiveBuzzerStates pressedBuzzer = ActiveBuzzerStates.NA;
        public ActiveBuzzerStates PressedBuzzer {
            get { return this.pressedBuzzer; }
            set {
                if (this.pressedBuzzer != value) {
                    this.pressedBuzzer = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private ActiveBuzzerStates timeoutBuzzer = ActiveBuzzerStates.NA;
        public ActiveBuzzerStates TimeoutBuzzer {
            get { return this.timeoutBuzzer; }
            set {
                if (this.timeoutBuzzer != value) {
                    this.timeoutBuzzer = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private Random rnd;

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

            this.rnd = new Random();

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.TimerRandomBuzzerScoreShotclock'", typeIdentifier);
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
            this.insertScene.Timer.PropertyChanged += this.insertTimer_PropertyChanged;

            this.fullscreenMasterScene.Timer.Alarm2Fired += this.shotclock_Alarm2Fired;
            this.fullscreenMasterScene.Timer.StopFired += this.shotclock_StopFired;

            base.Vinsert_Timer.Pose(syncContext, this.insertScene.Timer);
            this.Vinsert_Timer.PropertyChanged += this.on_PropertyChanged;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);
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

            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Timer.PropertyChanged -= this.insertTimer_PropertyChanged;
            this.insertScene.Dispose();

            this.fullscreenMasterScene.Timer.StopFired -= this.shotclock_StopFired;

            this.Vinsert_Timer.PropertyChanged -= this.on_PropertyChanged;
            this.Vinsert_Timer.Dispose();

        }

        public override void Init() {
            base.Init();
            this.LeftPlayerScore = 0;
            this.RightPlayerScore = 0;
            this.SetActiveBuzzer(ActiveBuzzerStates.NA);
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
            this.PressedBuzzer = ActiveBuzzerStates.NA;
            this.TimeoutBuzzer = ActiveBuzzerStates.NA;
            this.SetActiveBuzzer(this.StartBuzzer);
        }

        public void NextBuzzer() {            
            this.PressedBuzzer = ActiveBuzzerStates.NA;
            this.TimeoutBuzzer = ActiveBuzzerStates.NA;
            if (this.Vinsert_Timer.IsRunning) {
                int buzzerIndex = (int)this.ActiveBuzzer;
                if (buzzerIndex < 1) buzzerIndex = 1;
                buzzerIndex = buzzerIndex += this.rnd.Next(1, 3);
                while (buzzerIndex > 4) buzzerIndex -= 4;
                this.SetActiveBuzzer((ActiveBuzzerStates)buzzerIndex);
                this.Vfullscreen_StartTimer();
            }
        }

        public void SetActiveBuzzer(
            ActiveBuzzerStates value) {
            this.ActiveBuzzer = value;
            this.setDMXValues();
            switch (value) {
                case ActiveBuzzerStates.NA:
                    this.LockBuzzer();
                    break;
                case ActiveBuzzerStates.Top:
                case ActiveBuzzerStates.Left:
                case ActiveBuzzerStates.Right:
                case ActiveBuzzerStates.Bottom:
                default:
                    this.ReleaseBuzzer();
                    break;
            }
        }

        public void StopGame() {
            this.LockBuzzer();
            this.Vfullscreen_StopTimer();
            this.PressedBuzzer = ActiveBuzzerStates.NA;
            this.TimeoutBuzzer = ActiveBuzzerStates.NA;
            this.SetActiveBuzzer(ActiveBuzzerStates.NA); 
        }

        private void setDMXValues() {
            if (this.buzzerHandler is BuzzerIO.Business) {
                this.buzzerHandler.SetDMXMode(this.IOUnitName, IOnet.IOUnit.IONbuzDX.DMXBuzzerModes.OFF);
                byte[] offValue = new byte[] { this.OffColor.R, this.OffColor.G, this.OffColor.B };
                byte[] onValue = new byte[] { this.OnColor.R, this.OnColor.G, this.OnColor.B };
                byte[] scoreValue = new byte[] { this.ScoreColor.R, this.ScoreColor.G, this.ScoreColor.B };
                byte[] timeoutValue = new byte[] { this.TimeoutColor.R, this.TimeoutColor.G, this.TimeoutColor.B };
                byte[] valueList = new byte[16];
                Array.Copy(offValue, 0, valueList, 0, offValue.Length);
                Array.Copy(offValue, 0, valueList, 4, offValue.Length);
                Array.Copy(offValue, 0, valueList, 8, offValue.Length);
                Array.Copy(offValue, 0, valueList, 12, offValue.Length);
                switch (this.ActiveBuzzer) {
                    case ActiveBuzzerStates.Top:
                        Array.Copy(onValue, 0, valueList, 0, onValue.Length);
                        break;
                    case ActiveBuzzerStates.Left:
                        Array.Copy(onValue, 0, valueList, 4, onValue.Length);
                        break;
                    case ActiveBuzzerStates.Right:
                        Array.Copy(onValue, 0, valueList, 8, onValue.Length);
                        break;
                    case ActiveBuzzerStates.Bottom:
                        Array.Copy(onValue, 0, valueList, 12, onValue.Length);
                        break;
                    case ActiveBuzzerStates.NA:
                    default:
                        break;
                }
                switch (this.PressedBuzzer) {
                    case ActiveBuzzerStates.Top:
                        Array.Copy(scoreValue, 0, valueList, 0, scoreValue.Length);
                        break;
                    case ActiveBuzzerStates.Left:
                        Array.Copy(scoreValue, 0, valueList, 4, scoreValue.Length);
                        break;
                    case ActiveBuzzerStates.Right:
                        Array.Copy(scoreValue, 0, valueList, 8, scoreValue.Length);
                        break;
                    case ActiveBuzzerStates.Bottom:
                        Array.Copy(scoreValue, 0, valueList, 12, scoreValue.Length);
                        break;
                    case ActiveBuzzerStates.NA:
                    default:
                        break;
                }
                switch (this.TimeoutBuzzer) {
                    case ActiveBuzzerStates.Top:
                        Array.Copy(timeoutValue, 0, valueList, 0, timeoutValue.Length);
                        break;
                    case ActiveBuzzerStates.Left:
                        Array.Copy(timeoutValue, 0, valueList, 4, timeoutValue.Length);
                        break;
                    case ActiveBuzzerStates.Right:
                        Array.Copy(timeoutValue, 0, valueList, 8, timeoutValue.Length);
                        break;
                    case ActiveBuzzerStates.Bottom:
                        Array.Copy(timeoutValue, 0, valueList, 12, timeoutValue.Length);
                        break;
                    case ActiveBuzzerStates.NA:
                    default:
                        break;
                }

                this.buzzerHandler.SetDMXChannel(this.IOUnitName, 1, valueList);
            }
        }

        public virtual void ReleaseBuzzer() {
            bool[] inputMask = new bool[8];
            switch (this.ActiveBuzzer) {
                case ActiveBuzzerStates.Top:
                    if (this.TopBuzzerChannel > 0 &&
                        this.TopBuzzerChannel <= inputMask.Length) inputMask[this.TopBuzzerChannel - 1] = true;
                    break;
                case ActiveBuzzerStates.Left:
                    if (this.LeftBuzzerChannel > 0 &&
                        this.LeftBuzzerChannel <= inputMask.Length) inputMask[this.LeftBuzzerChannel - 1] = true;
                    break;
                case ActiveBuzzerStates.Right:
                    if (this.RightBuzzerChannel > 0 &&
                        this.RightBuzzerChannel <= inputMask.Length) inputMask[this.RightBuzzerChannel - 1] = true;
                    break;
                case ActiveBuzzerStates.Bottom:
                    if (this.BottomBuzzerChannel > 0 &&
                        this.BottomBuzzerChannel <= inputMask.Length) inputMask[this.BottomBuzzerChannel - 1] = true;
                    break;
                case ActiveBuzzerStates.NA:
                default:
                    if (this.TopBuzzerChannel > 0 &&
                        this.TopBuzzerChannel <= inputMask.Length) inputMask[this.TopBuzzerChannel - 1] = true;
                    if (this.LeftBuzzerChannel > 0 &&
                        this.LeftBuzzerChannel <= inputMask.Length) inputMask[this.LeftBuzzerChannel - 1] = true;
                    if (this.RightBuzzerChannel > 0 &&
                        this.RightBuzzerChannel <= inputMask.Length) inputMask[this.RightBuzzerChannel - 1] = true;
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

        public virtual void DoBuzzer() {
            this.LockBuzzer();
            this.Vfullscreen_StopTimer();
            Helper.invokeActionAfterDelay(this.Vfullscreen_ResetTimer, 1000, this.syncContext);
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.PlayJingleHit();
            this.PressedBuzzer = this.ActiveBuzzer;
            this.setDMXValues();
            Helper.invokeActionAfterDelay(this.NextBuzzer, 2000, this.syncContext);
        }

        public virtual void Timeout() {
            this.LockBuzzer();
            this.TimeoutBuzzer = this.ActiveBuzzer;
            this.setDMXValues();
            this.Vfullscreen_StopTimer();
            Helper.invokeActionAfterDelay(this.Vfullscreen_ResetTimer, 1000, this.syncContext);
            Helper.invokeActionAfterDelay(this.NextBuzzer, 2000, this.syncContext);
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }

        public void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public void Vinsert_ScoreIn(
            VentuzScenes.GamePool._Modules.Score scene) {
            if (scene is VentuzScenes.GamePool._Modules.Score) {
                this.Vinsert_SetScore(scene, this.LeftPlayerScore, this.RightPlayerScore);
                scene.ToIn();
            }
        }

        public void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score); }
        public void Vinsert_SetScore(VentuzScenes.GamePool._Modules.Score scene) { this.Vinsert_SetScore(scene, this.LeftPlayerScore, this.RightPlayerScore); }
        public void Vinsert_SetScore(
            VentuzScenes.GamePool._Modules.Score scene,
            int leftPlayerScore,
            int rightPlayerScore) {
            if (scene is VentuzScenes.GamePool._Modules.Score) {
                scene.SetPositionX(this.ScorePositionX);
                scene.SetPositionY(this.ScorePositionY);
                scene.SetFlipPosition(this.FlipPlayers);
                scene.SetStyle(this.ScoreStyle);
                scene.SetLeftTopName(this.LeftPlayerName);
                scene.SetLeftTopScore(leftPlayerScore);
                scene.SetRightBottomName(this.RightPlayerName);
                scene.SetRightBottomScore(rightPlayerScore);
            }
        }

        public void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }
        public void Vinsert_ScoreOut(
            VentuzScenes.GamePool._Modules.Score scene) {
            if (scene is VentuzScenes.GamePool._Modules.Score) scene.ToOut();
        }

        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void Vfullscreen_SetTimer() {
            this.Vfullscreen_SetTimer(this.ShotclockStartTime);
        }
        public void Vfullscreen_SetTimer(int startTime) {
            //base.Vfullscreen_SetTimer();
            if (this.fullscreenMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.fullscreenMasterScene.Timer.SetStyle(VentuzScenes.Fullscreen.Clock.Styles.Sec);
                this.fullscreenMasterScene.Timer.SetStartTime(startTime);
                this.fullscreenMasterScene.Timer.SetStopTime(0);
                this.fullscreenMasterScene.Timer.SetAlarmTime1(3);
                this.fullscreenMasterScene.Timer.SetAlarmTime2(0);
            }
        }

        public override void ClearGraphic() {
            base.ClearGraphic();
            this.insertScene.Clear();
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

        void buzzerHandler_BuzUnit_Buzzered(object sender, BuzzerIDParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_Buzzered);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_BuzUnit_Buzzered(object content) {
            BuzzerIDParam_EventArgs e = content as BuzzerIDParam_EventArgs;
            if (this.isActive &&
                e is BuzzerIDParam_EventArgs &&
                e.Arg.Name == this.IOUnitName) {
                this.DoBuzzer();
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

        protected override void sync_timer_Alarm1Fired(object content) {
        }

        protected override void sync_timer_Alarm2Fired(object content) {
            if (this.insertScene is Insert) this.insertScene.PlayJingleEnd();
            this.StopGame(); 
        }

        private void shotclock_Alarm2Fired(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_shotclock_Alarm2Fired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_shotclock_Alarm2Fired(object content) {
            this.Timeout();
        }

        private void shotclock_StopFired(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_shotclock_StopFired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_shotclock_StopFired(object content) {
            //if (this.fullscreenMasterScene.Timer.CurrentTime == 0) {
            //    this.Timeout();
            //}
        }

        #endregion

    }
}
