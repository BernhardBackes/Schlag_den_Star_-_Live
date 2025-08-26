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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimerForTwoSoloScoreStopover;
using System.Xml.Serialization;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimerForTwoSoloScoreStopover {

    public class Business : _Base.BuzzerScore.Business {

        #region Properties

        private AMB.Business ambHandler;

        private MidiHandler.Business midiHandler;

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

        private bool buzzerMode = false;
        public bool BuzzerMode
        {
            get { return this.buzzerMode; }
            set 
            { 
                if (this.buzzerMode != value)
                {
                    this.buzzerMode = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int leftPlayerStopoverChannel = 1;
        public int LeftPlayerStopoverChannel
        {
            get { return this.leftPlayerStopoverChannel; }
            set
            {
                if (this.leftPlayerStopoverChannel != value)
                {
                    if (value < 1) value = 1;
                    if (value > 8) value = 8;
                    this.leftPlayerStopoverChannel = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool leftStopoverReached = false;
        public bool LeftStopoverReached
        {
            get { return this.leftStopoverReached; }
            set
            {
                if (this.leftStopoverReached != value)
                {
                    this.leftStopoverReached = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool leftFinishReached = false;
        public bool LeftFinishReached
        {
            get { return this.leftFinishReached; }
            set
            {
                if (this.leftFinishReached != value)
                {
                    this.leftFinishReached = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerStopoverChannel = 2;
        public int RightPlayerStopoverChannel
        {
            get { return this.rightPlayerStopoverChannel; }
            set
            {
                if (this.rightPlayerStopoverChannel != value)
                {
                    if (value < 1) value = 1;
                    if (value > 8) value = 8;
                    this.rightPlayerStopoverChannel = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool rightStopoverReached = false;
        public bool RightStopoverReached
        {
            get { return this.rightStopoverReached; }
            set
            {
                if (this.rightStopoverReached != value)
                {
                    this.rightStopoverReached = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool rightFinishReached = false;
        public bool RightFinishReached
        {
            get { return this.rightFinishReached; }
            set
            {
                if (this.rightFinishReached != value)
                {
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
        [XmlIgnore]
        public PlayerSelection FirstFinisher {
            get { return this.firstFinisher; }
            set {
                if (this.firstFinisher != value) {
                    this.firstFinisher = value;
                    switch (value)
                    {
                        case PlayerSelection.LeftPlayer:
                            if (this.SwapTracks) this.BuzzeredPlayer = PlayerSelection.RightPlayer;
                            else this.BuzzeredPlayer = value;
                            break;
                        case PlayerSelection.RightPlayer:
                            if (this.SwapTracks) this.BuzzeredPlayer = PlayerSelection.LeftPlayer;
                            else this.BuzzeredPlayer = value;
                            break;
                        case PlayerSelection.NotSelected:
                        default:
                            this.BuzzeredPlayer = value;
                            break;
                    }
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.TimerForTwoSoloScoreStopover'", typeIdentifier);
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

            this.midiHandler = midiHandler;

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;
            this.insertScene.StopFired += this.insertScene_StopFired;
            this.insertScene.PreciseTimeReceived += this.insertScene_PreciseTimeReceived;
            this.insertScene.PropertyChanged += this.insertScene_PropertyChanged;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);

            this.AllLightsBlack();
        }

        public override void Dispose() {
            base.Dispose();

            this.ambHandler.Dispose();
            this.ambHandler.Error -= this.on_Error;
            this.ambHandler.FirstContact -= this.ambHandler_FirstContact;
            this.ambHandler.Passed -= this.ambHandler_Passed;
            this.ambHandler.PropertyChanged -= this.ambHandler_PropertyChanged;

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
            this.FirstFinisher = PlayerSelection.NotSelected;
            this.LeftStopoverReached = false;
            this.LeftFinishReached = false;
            this.RightStopoverReached = false;
            this.RightFinishReached = false;
        }

        public override void Next() {
            this.LockBuzzer();
            this.TimeToBeat = null;
            this.timeIsRunning = false;
            this.FirstFinisher = PlayerSelection.NotSelected;
            this.LeftStopoverReached = false;
            this.LeftFinishReached = false;
            this.RightStopoverReached = false;
            this.RightFinishReached = false;
            this.SetLeftPlayerOff();
            this.SetRightPlayerOff();
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

        private void setDMXValues() {
            this.buzzerHandler.SetDMXMode(this.IOUnitName, IOnet.IOUnit.IONbuzDX.DMXBuzzerModes.CHANNEL);
            this.buzzerHandler.SetDMXSettings(this.IOUnitName, 0, 255);
        }

        public virtual void SetStopover(
            PlayerSelection buzzeredPlayer)
        {
            if (this.SwapTracks)
            {
                switch (buzzeredPlayer)
                {
                    case PlayerSelection.LeftPlayer:
                        this.RightStopoverReached = true;
                        break;
                    case PlayerSelection.RightPlayer:
                        this.LeftStopoverReached = true;
                        break;
                }
            }
            else
            {
                switch (buzzeredPlayer)
                {
                    case PlayerSelection.LeftPlayer:
                        this.LeftStopoverReached = true;
                        break;
                    case PlayerSelection.RightPlayer:
                        this.RightStopoverReached = true;
                        break;
                }
            }
        }

        public virtual void PassStopover(
            PlayerSelection buzzeredPlayer)
        {
            bool changed = false;
            switch (buzzeredPlayer)
            {
                case PlayerSelection.LeftPlayer:
                    changed = !this.LeftStopoverReached;
                    this.LeftStopoverReached = true;
                    break;
                case PlayerSelection.RightPlayer:
                    changed = !this.RightStopoverReached;
                    this.RightStopoverReached = true;
                    break;
            }
            if (changed) this.insertMasterScene.Sampler.Play(VentuzScenes.Insert.Sampler.SampleElements.SpeedcourtRichtig);
        }

        public virtual void PassFinishLine(
            PlayerSelection buzzeredPlayer) {
            if (buzzeredPlayer != PlayerSelection.NotSelected &&
                buzzeredPlayer != this.FirstFinisher) {
                if (this.FirstFinisher == PlayerSelection.NotSelected) {
                    this.FirstFinisher = buzzeredPlayer;
                    if (this.BuzzerMode)
                    {
                        this.Vinsert_StopTimer();
                        this.LockTimeline();
                        this.LockBuzzer();
                    }
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
                    {
                        this.insertScene.FinishToInTop();
                    }
                    else if (this.FirstFinisher == PlayerSelection.RightPlayer)
                    {
                        this.insertScene.FinishToInBottom();
                    }
                    switch (buzzeredPlayer)
                    {
                        case PlayerSelection.LeftPlayer:
                            this.midiHandler.SendEvent("Left");
                            break;
                        case PlayerSelection.RightPlayer:
                            this.midiHandler.SendEvent("Right");
                            break;
                    }
                }
                else {
                    this.Vinsert_StopTimer();
                    this.LockTimeline();
                    this.LockBuzzer();
                }
            }
        }

        public override void ReleaseBuzzer() {
            base.ReleaseBuzzer();
            bool[] inputMask = new bool[8];
            if (this.LeftPlayerStopoverChannel > 0 &&
                this.LeftPlayerStopoverChannel <= inputMask.Length) inputMask[this.LeftPlayerStopoverChannel - 1] = true;
            if (this.LeftPlayerBuzzerChannel > 0 &&
                this.LeftPlayerBuzzerChannel <= inputMask.Length) inputMask[this.LeftPlayerBuzzerChannel - 1] = true;
            if (this.RightPlayerStopoverChannel > 0 &&
                this.RightPlayerStopoverChannel <= inputMask.Length) inputMask[this.RightPlayerStopoverChannel - 1] = true;
            if (this.RightPlayerBuzzerChannel > 0 &&
                this.RightPlayerBuzzerChannel <= inputMask.Length) inputMask[this.RightPlayerBuzzerChannel - 1] = true;
            this.buzzerHandler.SetInputMask(this.IOUnitName, inputMask);
            this.buzzerHandler.ReleaseBuzzer(this.IOUnitName, WorkModes.EVENT);
            this.LeftStopoverReached = false;
            this.RightStopoverReached = false;
            this.FirstFinisher = PlayerSelection.NotSelected;
        }
        public void ReleaseBuzzer(
            PlayerSelection track) {
            bool[] inputMask = new bool[8];
            switch (track) {
                case PlayerSelection.LeftPlayer:
                    if (this.LeftPlayerStopoverChannel > 0 &&
                        this.LeftPlayerStopoverChannel <= inputMask.Length) inputMask[this.LeftPlayerStopoverChannel - 1] = true;
                    if (this.LeftPlayerBuzzerChannel > 0 &&
                        this.LeftPlayerBuzzerChannel <= inputMask.Length) inputMask[this.LeftPlayerBuzzerChannel - 1] = true;
                    this.LeftStopoverReached = false;
                    break;
                case PlayerSelection.RightPlayer:
                    if (this.RightPlayerStopoverChannel > 0 &&
                        this.RightPlayerStopoverChannel <= inputMask.Length) inputMask[this.RightPlayerStopoverChannel - 1] = true;
                    if (this.RightPlayerBuzzerChannel > 0 &&
                        this.RightPlayerBuzzerChannel <= inputMask.Length) inputMask[this.RightPlayerBuzzerChannel - 1] = true;
                    this.RightStopoverReached = false;
                    break;
            }
            this.buzzerHandler.SetDMXMode(this.IOUnitName, IOnet.IOUnit.IONbuzDX.DMXBuzzerModes.CHANNEL);
            this.buzzerHandler.SetInputMask(this.IOUnitName, inputMask);
            this.buzzerHandler.ReleaseBuzzer(this.IOUnitName, WorkModes.EVENT);
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

        protected override void sync_buzzerHandler_BuzUnit_Buzzered(object content) {
            BuzzerIDParam_EventArgs e = content as BuzzerIDParam_EventArgs;
            if (this.isActive &&
                e is BuzzerIDParam_EventArgs &&
                e.Arg.Name == this.IOUnitName) {
                if (e.Arg.BuzzerID == this.LeftPlayerStopoverChannel)
                {
                    this.PassStopover(PlayerSelection.LeftPlayer);
                }
                else if (e.Arg.BuzzerID == this.RightPlayerStopoverChannel)
                {
                    this.PassStopover(PlayerSelection.RightPlayer);
                }
                else if (e.Arg.BuzzerID == this.LeftPlayerBuzzerChannel &&
                    this.LeftStopoverReached) {
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

        protected void insertScene_StopFired(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_insertScene_StopFired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_insertScene_StopFired(object content) {
            if (this.timeIsRunning) {
                if (this.insertScene.CurrentTime > 0 &&
                    this.timeToBeat.HasValue &&
                    !this.BuzzerMode) {
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
