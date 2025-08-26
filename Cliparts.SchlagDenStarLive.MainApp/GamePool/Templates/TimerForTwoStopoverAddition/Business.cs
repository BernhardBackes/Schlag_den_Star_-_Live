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

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimerForTwoStopoverAddition;
using System.Xml.Serialization;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimerForTwoStopoverAddition
{

    public class Business : _Base.Buzzer.Business {

        #region Properties

        private MidiHandler.Business midiHandler;

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

        private int leftStopoverChannel = 1;
        public int LeftStopoverChannel
        {
            get { return this.leftStopoverChannel; }
            set
            {
                if (this.leftStopoverChannel != value)
                {
                    if (value < 1) value = 1;
                    if (value > 8) value = 8;
                    this.leftStopoverChannel = value;
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

        private int rightStopoverChannel = 2;
        public int RightStopoverChannel
        {
            get { return this.rightStopoverChannel; }
            set
            {
                if (this.rightStopoverChannel != value)
                {
                    if (value < 1) value = 1;
                    if (value > 8) value = 8;
                    this.rightStopoverChannel = value;
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

        private double leftPlayerStopTime = 0;
        public double LeftPlayerStopTime
        {
            get { return this.leftPlayerStopTime; }
            set
            {
                if (this.leftPlayerStopTime != value)
                {
                    if (value < 0) this.leftPlayerStopTime = 0;
                    else this.leftPlayerStopTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private double leftPlayerAdditionTime = 0;
        public double LeftPlayerAdditionTime
        {
            get { return this.leftPlayerAdditionTime; }
            set
            {
                if (this.leftPlayerAdditionTime != value)
                {
                    if (value < 0) this.leftPlayerAdditionTime = 0;
                    else this.leftPlayerAdditionTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private double rightPlayerStopTime = 0;
        public double RightPlayerStopTime
        {
            get { return this.rightPlayerStopTime; }
            set
            {
                if (this.rightPlayerStopTime != value)
                {
                    if (value < 0) this.rightPlayerStopTime = 0;
                    else this.rightPlayerStopTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private double rightPlayerAdditionTime = 0;
        public double RightPlayerAdditionTime
        {
            get { return this.rightPlayerAdditionTime; }
            set
            {
                if (this.rightPlayerAdditionTime != value)
                {
                    if (value < 0) this.rightPlayerAdditionTime = 0;
                    else this.rightPlayerAdditionTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int roundsCount = 4;
        public int RoundsCount
        {
            get { return this.roundsCount; }
            set
            {
                if (this.roundsCount != value)
                {
                    if (value < 1) this.roundsCount = 1;
                    else this.roundsCount = value;
                    this.on_PropertyChanged();
                    this.RoundCounter = this.roundCounter;
                }
            }
        }

        private int roundCounter = 1;
        public int RoundCounter
        {
            get { return this.roundCounter; }
            set
            {
                if (this.roundCounter != value)
                {
                    if (value < 1) this.roundCounter = 1;
                    else if (value > this.roundsCount) this.roundCounter = this.roundsCount;
                    else this.roundCounter = value;
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.TimerForTwoStopoverAddition'", typeIdentifier);
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

            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.StopFired -= this.insertScene_StopFired;
            this.insertScene.PreciseTimeReceived -= this.insertScene_PreciseTimeReceived;
            this.insertScene.PropertyChanged -= this.insertScene_PropertyChanged;
        }

        public override void ResetData() {
            base.ResetData();
            this.RoundCounter = 0;
            this.BuzzeredPlayer = PlayerSelection.NotSelected;
            this.timeIsRunning = false;
            this.LeftStopoverReached = false;
            this.LeftFinishReached = false;
            this.RightStopoverReached = false;
            this.RightFinishReached = false;
            this.LeftPlayerStopTime = 0;
            this.LeftPlayerAdditionTime = 0;
            this.RightPlayerStopTime = 0;
            this.RightPlayerAdditionTime = 0;
            this.SetLeftPlayerOff();
            this.SetRightPlayerOff();
        }

        public override void Next() {
            this.LockBuzzer();
            this.BuzzeredPlayer = PlayerSelection.NotSelected;
            this.timeIsRunning = false;
            this.LeftStopoverReached = false;
            this.LeftFinishReached = false;
            this.RightStopoverReached = false;
            this.RightFinishReached = false;
            this.LeftPlayerStopTime = 0;
            this.RightPlayerStopTime = 0;
            this.SetLeftPlayerOff();
            this.SetRightPlayerOff();
            this.RoundCounter++;
            this.Vinsert_SetTimer();
        }

        internal void AddTime()
        {
            this.LeftPlayerAdditionTime += this.LeftPlayerStopTime;
            this.RightPlayerAdditionTime += this.RightPlayerStopTime;
        }

        public virtual void SetStopover(
            PlayerSelection buzzeredPlayer)
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

        public virtual void SetFinish(
            PlayerSelection buzzeredPlayer)
        {
            switch (buzzeredPlayer)
            {
                case PlayerSelection.LeftPlayer:
                    this.LeftFinishReached = true;
                    break;
                case PlayerSelection.RightPlayer:
                    this.RightFinishReached = true;
                    break;
            }
        }
        public virtual void PassFinishLine(
            PlayerSelection buzzeredPlayer) {
            if (buzzeredPlayer != PlayerSelection.NotSelected &&
                buzzeredPlayer != this.BuzzeredPlayer) {
                switch (buzzeredPlayer)
                {
                    case PlayerSelection.LeftPlayer:
                        if (this.LeftStopoverReached)
                        {
                            if (!this.LeftFinishReached) this.insertMasterScene.Sampler.Play(VentuzScenes.Insert.Sampler.SampleElements.BuzzerLeft);
                            if (this.BuzzeredPlayer == PlayerSelection.NotSelected)
                            {
                                this.BuzzeredPlayer = PlayerSelection.LeftPlayer;
                                this.Vinsert_GetPreciseTime();
                            }
                            else
                            {
                                this.Vinsert_StopTimer();
                                this.LockBuzzer();
                            }
                            this.LeftFinishReached = true;
                        }
                        break;
                    case PlayerSelection.RightPlayer:
                        if (this.RightStopoverReached)
                        {
                            if (!this.RightFinishReached) this.insertMasterScene.Sampler.Play(VentuzScenes.Insert.Sampler.SampleElements.BuzzerRight);
                            if (this.BuzzeredPlayer == PlayerSelection.NotSelected)
                            {
                                this.BuzzeredPlayer = PlayerSelection.RightPlayer;
                                this.Vinsert_GetPreciseTime();
                            }
                            else
                            {
                                this.Vinsert_StopTimer();
                                this.LockBuzzer();
                            }
                            this.RightFinishReached = true;
                        }
                        break;
                }
            }
        }

        public override void ReleaseBuzzer() {
            base.ReleaseBuzzer();
            bool[] inputMask = new bool[8];
            if (this.LeftStopoverChannel > 0 &&
                this.LeftStopoverChannel <= inputMask.Length) inputMask[this.LeftStopoverChannel - 1] = true;
            if (this.LeftPlayerBuzzerChannel > 0 &&
                this.LeftPlayerBuzzerChannel <= inputMask.Length) inputMask[this.LeftPlayerBuzzerChannel - 1] = true;
            if (this.RightStopoverChannel > 0 &&
                this.RightStopoverChannel <= inputMask.Length) inputMask[this.RightStopoverChannel - 1] = true;
            if (this.RightPlayerBuzzerChannel > 0 &&
                this.RightPlayerBuzzerChannel <= inputMask.Length) inputMask[this.RightPlayerBuzzerChannel - 1] = true;
            this.buzzerHandler.SetInputMask(this.IOUnitName, inputMask);
            this.buzzerHandler.ReleaseBuzzer(this.IOUnitName, WorkModes.EVENT);
            this.LeftStopoverReached = false;
            this.RightStopoverReached = false;
        }
        public void ReleaseBuzzer(
            PlayerSelection track) {
            bool[] inputMask = new bool[8];
            switch (track) {
                case PlayerSelection.LeftPlayer:
                    if (this.LeftStopoverChannel > 0 &&
                        this.LeftStopoverChannel <= inputMask.Length) inputMask[this.LeftStopoverChannel - 1] = true;
                    if (this.LeftPlayerBuzzerChannel > 0 &&
                        this.LeftPlayerBuzzerChannel <= inputMask.Length) inputMask[this.LeftPlayerBuzzerChannel - 1] = true;
                    this.LeftStopoverReached = false;
                    break;
                case PlayerSelection.RightPlayer:
                    if (this.RightStopoverChannel > 0 &&
                        this.RightStopoverChannel <= inputMask.Length) inputMask[this.RightStopoverChannel - 1] = true;
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
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.ToIn();
        }
        public void Vinsert_SetTimer() { 
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetTimer(
                this.insertScene,
                this.RoundCounter,
                this.LeftPlayerStopTime,
                this.LeftPlayerAdditionTime,
                this.RightPlayerStopTime,
                this.RightPlayerAdditionTime); 
        }
        public void Vinsert_SetTimer(
            Insert scene,
            int roundCounter,
            double leftPlayerStopTime,
            double leftPlayerAdditionTime,
            double rightPlayerStopTime,
            double rightPlayerAdditionTime) {
            if (scene is Insert) {
                scene.SetPositionX(this.TimerPositionX);
                scene.SetPositionY(this.TimerPositionY);
                scene.SetCounter(roundCounter);
                scene.SetTopName(this.LeftPlayerName);
                scene.SetTopStopTime(leftPlayerStopTime);
                scene.SetTopAdditionTime(leftPlayerAdditionTime);
                scene.SetBottomName(this.RightPlayerName);
                scene.SetBottomStopTime(rightPlayerStopTime);
                scene.SetBottomAdditionTime(rightPlayerAdditionTime);
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
        public void Vinsert_ResetTimer()
        {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.ResetTimer();
        }
        public void Vinsert_TimerAdditionIn()
        {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.AdditionToIn();
        }
        public void Vinsert_TimerAdditionOut()
        {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.AdditionToOut();
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

        protected override void sync_buzzerHandler_BuzUnit_Buzzered(object content) {
            BuzzerIDParam_EventArgs e = content as BuzzerIDParam_EventArgs;
            if (this.isActive &&
                e is BuzzerIDParam_EventArgs &&
                e.Arg.Name == this.IOUnitName) {
                if (e.Arg.BuzzerID == this.LeftStopoverChannel)
                {
                    this.PassStopover(PlayerSelection.LeftPlayer);
                }
                else if (e.Arg.BuzzerID == this.RightStopoverChannel)
                {
                    this.PassStopover(PlayerSelection.RightPlayer);
                }
                else if (e.Arg.BuzzerID == this.LeftPlayerBuzzerChannel) {
                    this.PassFinishLine(PlayerSelection.LeftPlayer);
                }
                else if (e.Arg.BuzzerID == this.RightPlayerBuzzerChannel) {
                    this.PassFinishLine(PlayerSelection.RightPlayer);
                }
            }
        }

        private void buzzerHandler_BuzUnit_InputChannelChanged(object sender, InputChannelParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_InputChannelChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_BuzUnit_InputChannelChanged(object content) {
            InputChannelParam_EventArgs e = content as InputChannelParam_EventArgs;
            //if (this.isActive &&
            //    e is InputChannelParam_EventArgs &&
            //    e.Arg.Name == this.IOUnitName &&
            //    this.FinishMode == FinishModes.Crossing) {
            //    if (this.LeftFinishReached &&
            //        e.Arg.InputChannel[this.LeftPlayerBuzzerChannel - 1] == InputChannelStates.UP) this.PassFinishLine(PlayerSelection.LeftPlayer);
            //    if (this.RightFinishReached &&
            //        e.Arg.InputChannel[this.RightPlayerBuzzerChannel - 1] == InputChannelStates.UP) this.PassFinishLine(PlayerSelection.RightPlayer);
            //}
        }

        protected void insertScene_StopFired(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_insertScene_StopFired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_insertScene_StopFired(object content) {
            if (this.timeIsRunning) {
                if (this.insertScene.CurrentTime > 0) {
                    this.TimerCurrentTime = Convert.ToDouble(this.insertScene.CurrentTime);
                    switch (this.BuzzeredPlayer)
                    {
                        case PlayerSelection.LeftPlayer:
                            this.RightPlayerStopTime = this.TimerCurrentTime;
                            break;
                        case PlayerSelection.RightPlayer:
                            this.LeftPlayerStopTime = this.TimerCurrentTime;
                            break;
                    }
                    this.Vinsert_SetTimer();

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
                if (this.insertScene.PreciseTime > 0) {
                    switch (this.BuzzeredPlayer)
                    {
                        case PlayerSelection.LeftPlayer:
                            this.LeftPlayerStopTime = this.insertScene.PreciseTime;
                            break;
                        case PlayerSelection.RightPlayer:
                            this.RightPlayerStopTime = this.insertScene.PreciseTime;
                            break;
                    }
                    this.Vinsert_SetTimer();
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
                if (e.PropertyName == "CurrentTime") this.TimerCurrentTime = Convert.ToDouble(this.insertScene.CurrentTime);
            }
        }

        #endregion

    }
}
