using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.Serialization;

using Cliparts.Tools.Base;

using Cliparts.IOnet.IOUnit.IONbase;
using Cliparts.IOnet.IOUnit.IONbuz;

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;
using Cliparts.SchlagDenStarLive.MainApp.Settings;
using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TwoTimersStartStopContinue;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TwoTimersStartStopContinue {

    public class Business : _Base.Business {

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

        private bool timelineIsConnected = false;
        public bool TimelineIsConnected {
            get { return this.timelineIsConnected; }
            set {
                if (this.timelineIsConnected != value) {
                    this.timelineIsConnected = value;
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

        private double leftPlayerTime = 0;
        public double LeftPlayerTime {
            get { return this.leftPlayerTime; }
            set {
                if (this.leftPlayerTime != value) {
                    if (value < 0) this.leftPlayerTime = 0;
                    else this.leftPlayerTime = value;
                    this.on_PropertyChanged();
                    this.LeftPlayerLAP = this.LeftPlayerTime - this.LeftPlayerPreviousTime;
                }
            }
        }

        private double leftPlayerPreviousTime = 0;
        public double LeftPlayerPreviousTime {
            get { return this.leftPlayerPreviousTime; }
            set {
                if (this.leftPlayerPreviousTime != value) {
                    if (value < 0) this.leftPlayerPreviousTime = 0;
                    else this.leftPlayerPreviousTime = value;
                    this.on_PropertyChanged();
                    this.LeftPlayerLAP = this.LeftPlayerTime - this.LeftPlayerPreviousTime;
                }
            }
        }

        private double leftPlayerLAP = 0;
        public double LeftPlayerLAP {
            get { return this.leftPlayerLAP; }
            private set {
                if (this.leftPlayerLAP != value) {
                    if (value < 0) this.leftPlayerLAP = 0;
                    else this.leftPlayerLAP = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private double rightPlayerTime = 0;
        public double RightPlayerTime {
            get { return this.rightPlayerTime; }
            set {
                if (this.rightPlayerTime != value) {
                    if (value < 0) this.rightPlayerTime = 0;
                    else this.rightPlayerTime = value;
                    this.on_PropertyChanged();
                    this.RightPlayerLAP = this.RightPlayerTime - this.RightPlayerPreviousTime;
                }
            }
        }

        private double rightPlayerPreviousTime = 0;
        public double RightPlayerPreviousTime {
            get { return this.rightPlayerPreviousTime; }
            set {
                if (this.rightPlayerPreviousTime != value) {
                    if (value < 0) this.rightPlayerPreviousTime = 0;
                    else this.rightPlayerPreviousTime = value;
                    this.on_PropertyChanged();
                    this.RightPlayerLAP = this.RightPlayerTime - this.RightPlayerPreviousTime;
                }
            }
        }

        private double rightPlayerLAP = 0;
        public double RightPlayerLAP {
            get { return this.rightPlayerLAP; }
            private set {
                if (this.rightPlayerLAP != value) {
                    if (value < 0) this.rightPlayerLAP = 0;
                    else this.rightPlayerLAP = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int lapCounter = 1;
        public int LAPCounter {
            get { return this.lapCounter; }
            set {
                if (this.lapCounter != value) {
                    if (value < 1) this.rightPlayerLAP = 1;
                    else this.lapCounter = value;
                    this.on_PropertyChanged();
                    if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.SetCounter(this.LAPCounter);
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

        private bool topTimerIsRunning = false;
        private bool bottomTimerIsRunning = false;

        #endregion


        #region Funktionen

        public Business() { }
        public Business(string typeIdentifier) : base(typeIdentifier) {

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.TwoTimersStartStopContinue'", typeIdentifier);
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

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;
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

            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Dispose();
        }

        public override void ResetData() {
            base.ResetData();
            this.LAPCounter = 1;
            this.LeftPlayerTime = 0;
            this.RightPlayerTime = 0;
            this.LeftPlayerPreviousTime = 0;
            this.RightPlayerPreviousTime = 0;
            this.Vinsert_SetTimer();
            this.topTimerIsRunning = false;
            this.bottomTimerIsRunning = false;
        }

        public override void Next() {
            base.Next();
            this.LAPCounter++;
            this.LeftPlayerPreviousTime = this.LeftPlayerTime;
            this.RightPlayerPreviousTime = this.RightPlayerTime;
            this.Vinsert_SetTimer();
        }

        private void checkTimelineStatus() {
            this.TimelineIsConnected = !string.IsNullOrEmpty(this.TimelineName) && this.TimelineNameList.Contains(this.TimelineName);
        }

        public void PassFinishLine(
            PlayerSelection player) {
            switch (player) {
                case PlayerSelection.LeftPlayer:
                    if (this.topTimerIsRunning) {
                        this.Vinsert_StopLeftPlayerTimer();
                        if (this.LeftPlayerPreviousTime > 0) this.Vinsert_LeftPlayerTimerLAPIn();
                    }
                    break;
                case PlayerSelection.RightPlayer:
                    if (this.bottomTimerIsRunning) {
                        this.Vinsert_StopRightPlayerTimer();
                        if (this.RightPlayerPreviousTime > 0) this.Vinsert_RightPlayerTimerLAPIn();
                    }
                    break;
            }
        }

        public override void Vinsert_LoadScene() { this.insertScene.Load(); }
        public void Vinsert_TimerIn() {
            this.Vinsert_SetTimer();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.ResetTopLAPTime();
                this.insertScene.ResetBottomLAPTime();
                this.insertScene.ToIn();
            }
        }
        public void Vinsert_SetTimer() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.SetPositionX(this.TimerPositionX);
                this.insertScene.SetPositionY(this.TimerPositionY);
                this.insertScene.SetCounter(this.LAPCounter);
            }
            this.Vinsert_SetLeftPlayerTimer();
            this.Vinsert_SetRightPlayerTimer();
        }
        internal void Vinsert_StartBothTimer() {
            this.Vinsert_SetTimer();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.StartBothTimers();
                this.topTimerIsRunning = true;
                this.bottomTimerIsRunning = true;
            }
        }
        internal void Vinsert_SetLeftPlayerTimer() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.SetTopName(this.LeftPlayerName);
                this.insertScene.SetTopStartTime(Convert.ToSingle(this.LeftPlayerTime));
                this.insertScene.ResetTopTimer();
            }
        }
        internal void Vinsert_StartLeftPlayerTimer() {
            this.Vinsert_SetLeftPlayerTimer();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.StartTopTimer();
                this.topTimerIsRunning = true;
            }
        }
        internal void Vinsert_StopLeftPlayerTimer() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.StopTopTimer();
            this.topTimerIsRunning = false;
        }
        internal void Vinsert_LeftPlayerTimerLAPIn() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.SetTopLAPTime(Convert.ToSingle(this.LeftPlayerLAP));
                this.insertScene.TopLAPTimeToIn();
            }
        }
        internal void Vinsert_LeftPlayerTimerLAPOut() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.TopLAPTimeToOut();
        }
        internal void Vinsert_SetRightPlayerTimer() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.SetBottomName(this.RightPlayerName);
                this.insertScene.SetBottomStartTime(Convert.ToSingle(this.RightPlayerTime));
                this.insertScene.ResetBottomTimer();
            }
        }
        internal void Vinsert_StartRightPlayerTimer() {
            this.Vinsert_SetRightPlayerTimer();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.StartBottomTimer();
                this.bottomTimerIsRunning = true;
            }
        }
        internal void Vinsert_StopRightPlayerTimer() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.StopBottomTimer();
            this.bottomTimerIsRunning = false;
        }
        internal void Vinsert_RightPlayerTimerLAPIn() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.SetBottomLAPTime(Convert.ToSingle(this.RightPlayerLAP));
                this.insertScene.BottomLAPTimeToIn();
            }
        }
        internal void Vinsert_RightPlayerTimerLAPOut() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.BottomLAPTimeToOut();
        }
        public void Vinsert_TimerOut() { 
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.ToOut();
            this.topTimerIsRunning = false;
            this.bottomTimerIsRunning = false;
        }
        public override void Vinsert_UnloadScene() { this.insertScene.Unload(); }

        public override void V_ClearGraphic() {
            base.V_ClearGraphic();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Clear(); 
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
                e.Timeline == this.TimelineName) {
                //if (e.Data.TransponderCode == this.LeftPlayerTransponderCode) this.PassFinishLine(PlayerSelection.LeftPlayer);
                //else if (e.Data.TransponderCode == this.RightPlayerTransponderCode) this.PassFinishLine(PlayerSelection.RightPlayer);
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

        void insertScene_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_insertScene_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_insertScene_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "TopTime") this.LeftPlayerTime = this.insertScene.TopTime;
                else if (e.PropertyName == "BottomTime") this.RightPlayerTime = this.insertScene.BottomTime;
            }
        }

        #endregion


    }
}
