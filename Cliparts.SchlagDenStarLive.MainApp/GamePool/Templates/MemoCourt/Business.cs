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

using Cliparts.Serialization;

using Cliparts.Tools.Base;

using Cliparts.Tools.NetContact;

using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.MemoCourt;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.MemoCourt {

    public class Business : _Base.Business {

        #region Properties

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

        private int sequencePositionX = 0;
        public int SequencePositionX {
            get { return this.sequencePositionX; }
            set {
                if (this.sequencePositionX != value) {
                    this.sequencePositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetSequence();
                }
            }
        }

        private int sequencePositionY = 0;
        public int SequencePositionY {
            get { return this.sequencePositionY; }
            set {
                if (this.sequencePositionY != value) {
                    this.sequencePositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetSequence();
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

        private int timerStartTime = 300;
        public int TimerStartTime {
            get { return this.timerStartTime; }
            set {
                if (this.timerStartTime != value) {
                    this.timerStartTime = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
                    this.Vfullscreen_SetCountdown();
                }
            }
        }

        private int timerAlarmTime1 = 10;
        public int TimerAlarmTime1 {
            get { return this.timerAlarmTime1; }
            set {
                if (this.timerAlarmTime1 != value) {
                    this.timerAlarmTime1 = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
                    this.Vfullscreen_SetCountdown();
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
                    this.Vfullscreen_SetCountdown();
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
                    this.Vfullscreen_SetCountdown();
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

        private PlayerSelection selectedPlayer = PlayerSelection.LeftPlayer;
        public PlayerSelection SelectedPlayer {
            get { return this.selectedPlayer; }
            set {
                if (this.selectedPlayer != value) {
                    if (value == PlayerSelection.NotSelected) value = PlayerSelection.LeftPlayer;
                    this.selectedPlayer = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string sequence = "?????";
        [NotSerialized]
        public string Sequence {
            get { return this.sequence; }
            set {
                if (this.sequence != value) {
                    if (string.IsNullOrEmpty(value)) value = string.Empty;
                    this.sequence = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetSequence();
                    this.Vfullscreen_SetSequence();
                }
            }
        }

        private bool sequenceIsChallenge { get { return this.Sequence.Length > 5; } }

        private int sequenceProgress = 0;
        [NotSerialized]
        public int SequenceProgress {
            get { return this.sequenceProgress; }
            set {
                if (this.sequenceProgress != value) {
                    this.sequenceProgress = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetSequence();
                }
            }
        }

        private bool sequenceIsStopped = false;
        public bool SequenceIsStopped {
            get { return this.sequenceIsStopped; }
            protected set {
                if (this.sequenceIsStopped != value) {
                    this.sequenceIsStopped = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int counter = 0;
        public int Counter {
            get { return this.counter; }
            set {
                if (this.counter != value) {
                    this.counter = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounter();
                }
            }
        }

        private int time = 0;
        public int Time {
            get { return this.time; }
            set {
                if (this.time != value) {
                    this.time = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int referenceCounter = 0;
        public int ReferenceCounter {
            get { return this.referenceCounter; }
            set {
                if (this.referenceCounter != value) {
                    this.referenceCounter = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounter();
                }
            }
        }

        private int referenceTime = 0;
        public int ReferenceTime {
            get { return this.referenceTime; }
            set {
                if (this.referenceTime != value) {
                    this.referenceTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool showReference = false;
        public bool ShowReference {
            get { return this.showReference; }
            set {
                if (this.showReference != value) {
                    this.showReference = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounter();
                }
            }
        }

        private TCPServer speedcourtServer;

        private string speedcourtClientName = string.Empty;
        public string SpeedcourtClientName {
            get { return this.speedcourtClientName; }
            private set {
                if (this.speedcourtClientName != value) {
                    this.speedcourtClientName = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool speedcourtServerIsRunning = false;
        public bool SpeedcourtServerIsRunning {
            get { return this.speedcourtServerIsRunning; }
            private set {
                if (this.speedcourtServerIsRunning != value) {
                    this.speedcourtServerIsRunning = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private List<string> taskNameList = new List<string>();
        public string[] TaskNameList { get { return this.taskNameList.ToArray(); } }

        private string selectedTask = string.Empty;
        public string SelectedTask {
            get { return this.selectedTask; }
            private set {
                if (this.selectedTask != value) {
                    if (string.IsNullOrEmpty(value)) this.selectedTask = string.Empty;
                    else this.selectedTask = value;
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.MemoCourt'", typeIdentifier);
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

            this.speedcourtServer = new TCPServer(61891);
            this.speedcourtServer.DataReceived += this.speedcourtServer_DataReceived;
            this.speedcourtServer.Changed += this.speedcourtServer_Changed;

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;
            this.insertScene.Timer.PropertyChanged += this.insertScene_Timer_PropertyChanged;
            this.insertScene.Timer.Alarm1Fired += this.insertScene_timer_Alarm1Fired;
            this.insertScene.Timer.Alarm2Fired += this.insertScene_timer_Alarm2Fired;
            this.insertScene.Timer.StopFired += this.insertScene_timer_StopFired;

            this.fullscreenScene = new Fullscreen(syncContext, fullscreenMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.fullscreenScene.StatusChanged += this.fullscreenScene_StatusChanged;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);
        }

        public override void Dispose() {
            this.speedcourtServer.DataReceived -= this.speedcourtServer_DataReceived;
            this.speedcourtServer.Changed -= this.speedcourtServer_Changed;
            this.speedcourtServer.StopServer();

            base.Dispose();

            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Timer.PropertyChanged -= this.insertScene_Timer_PropertyChanged;
            this.insertScene.Timer.Alarm1Fired -= this.insertScene_timer_Alarm1Fired;
            this.insertScene.Timer.Alarm2Fired -= this.insertScene_timer_Alarm2Fired;
            this.insertScene.Timer.StopFired -= this.insertScene_timer_StopFired;

            this.fullscreenScene.Dispose();
            this.fullscreenScene.StatusChanged += this.fullscreenScene_StatusChanged;
        }

        public override void ResetData() {
            base.ResetData();
            this.ReferenceCounter = 0;
            this.ReferenceTime = 0;
            this.ShowReference = false;
            this.Counter = 0;
            this.Time = 0;
            this.SequenceProgress = 0;
            this.SequenceIsStopped = false;
        }

        public void NextPlayer() {
            if (this.SelectedPlayer == Content.Gameboard.PlayerSelection.LeftPlayer) this.SelectedPlayer = Content.Gameboard.PlayerSelection.RightPlayer;
            else this.SelectedPlayer = Content.Gameboard.PlayerSelection.LeftPlayer;
            this.ReferenceTime = this.Time;
            this.Time = 0;
            this.ReferenceCounter = this.Counter;
            this.Counter = 0;
            this.ShowReference = true;
            this.SequenceProgress = 0;
            this.SequenceIsStopped = false;
        }

        public void ToggleServerIsRunning() {
            if (this.speedcourtServer.IsRunning) this.speedcourtServer.StopServer();
            else this.speedcourtServer.StartServer();
        }

        public void ShowServer() { this.speedcourtServer.ShowForm(); }

        public void SelectCourse(
            string name) {
            this.sendToClients("SelectTask\t" + name);
        }
        public void StartTask() { this.sendToClients("StartTask"); }
        public void StopTask() { this.sendToClients("StopTask"); }
        public void ResetTask() { this.sendToClients("ResetTask"); }

        private void sendToClients(
            string sendText) {
            sendText = NetControlCharacter.STX.ToString() + sendText + NetControlCharacter.EOT.ToString();
            byte[] data = Encoding.ASCII.GetBytes(sendText);
            List<WorkerSocket> hostList = this.speedcourtServer.HostList;
            foreach (WorkerSocket item in hostList) this.speedcourtServer.SendData(item.Address, data);
        }

        private void parseReceivedData(
            string recText) {
            if (!string.IsNullOrEmpty(recText) &&
                recText.StartsWith(NetControlCharacter.STX.ToString()) &&
                recText.EndsWith(NetControlCharacter.EOT.ToString())) {
                string[] messageArray = recText.Split(new char[] { NetControlCharacter.STX, NetControlCharacter.EOT }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in messageArray) {
                    string[] messageContent = item.Split(new char[] { NetControlCharacter.HT }, StringSplitOptions.RemoveEmptyEntries);
                    if (messageContent.Length > 0) {
                        if (messageContent[0] == "TaskList") {
                            this.taskNameList = new List<string>();
                            for (int i = 1; i < messageContent.Length; i++) {
                                if (!string.IsNullOrEmpty(messageContent[i])) this.taskNameList.Add(messageContent[i]);
                            }
                            this.on_PropertyChanged("TaskNameList");
                        }
                        else if (messageContent[0] == "Task" &&
                            messageContent.Length >= 1 &&
                            !string.IsNullOrEmpty(messageContent[1])) {
                            this.SelectedTask = messageContent[1];
                        }
                        else if (!this.SequenceIsStopped) {
                            if (messageContent[0] == "Richtig") {
                                this.Vinsert_PlayJingleGood();
                                this.Vfullscreen_SequenceOut();
                                this.SequenceProgress++;
                            }
                            else if (messageContent[0] == "Falsch") {
                                this.Vinsert_PlayJingleBad();
                                this.Vfullscreen_SequenceIn();
                                this.SequenceProgress = 0;
                            }
                            else if (messageContent[0] == "Punkt") {
                                this.Vinsert_PlayJingleScore();
                                if (this.sequenceIsChallenge) {
                                    this.Counter++;
                                    this.Time = this.TimerCurrentTime;
                                    this.SequenceProgress = 9;
                                }
                                else this.SequenceProgress = 9;
                            }
                            else if (messageContent[0] == "Sequence" &&
                                messageContent.Length >= 1 &&
                                !string.IsNullOrEmpty(messageContent[1]) &&
                                messageContent[1].Length >= 5) {
                                this.Sequence = messageContent[1];
                                this.Vfullscreen_SequenceIn();
                                this.SequenceProgress = 0;
                                this.Vinsert_ResetSequenceHighlight();
                                this.Vinsert_SequenceIn();
                                if (this.sequenceIsChallenge &&
                                    !this.TimerIsRunning) {
                                    this.Vfullscreen_StartCountdown();
                                    this.Vinsert_StartTimer();
                                }
                            }

                        }
                    }
                }
            }
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }
        public void Vinsert_CounterIn() {
            this.Vinsert_SetCounter();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.CounterToIn();
        }
        public void Vinsert_SetCounter() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.SetCounterPositionX(this.CounterPositionX);
                this.insertScene.SetCounterPositionY(this.CounterPositionY);
                this.insertScene.SetCounterValue(this.Counter);
                this.insertScene.SetCounterReference(this.ReferenceCounter);
                this.insertScene.SetCounterShowReference(this.ShowReference);
            }
        }
        public void Vinsert_CounterOut() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.CounterToOut(); }
        public void Vinsert_SequenceIn() {
            this.Vinsert_SetSequence();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.SequenceToIn();
        }
        public void Vinsert_SetSequence() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.SetSequencePositionX(this.SequencePositionX);
                this.insertScene.SetSequencePositionY(this.SequencePositionY);
                this.insertScene.SetSequenceValue(this.Sequence);
                this.insertScene.SetSequenceHighlightID(this.SequenceProgress);
            }
        }
        public void Vinsert_ResetSequenceHighlight() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.SequenceResetHighlight(); }
        public void Vinsert_SequenceOut() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.SequenceToOut(); }
        public void Vinsert_TimerIn() {
            this.Vinsert_SetTimer();
            this.Vinsert_ResetTimer();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.ToIn();
        }
        public void Vinsert_SetTimer() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.Timer.SetPositionX(this.TimerPositionX);
                this.insertScene.Timer.SetPositionY(this.TimerPositionY);
                this.insertScene.Timer.SetStyle(VentuzScenes.GamePool._Modules.Timer.Styles.MinSec);
                this.insertScene.Timer.SetScaling(100);
                this.insertScene.Timer.SetStartTime(this.TimerStartTime);
                this.insertScene.Timer.SetStopTime(this.TimerStopTime);
                this.insertScene.Timer.SetAlarmTime1(this.TimerAlarmTime1);
                this.insertScene.Timer.SetAlarmTime2(this.TimerAlarmTime2);
            }
        }
        public void Vinsert_StartTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available)   this.insertScene.Timer.StartTimer(); }
        public void Vinsert_StopTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available)                 this.insertScene.Timer.StopTimer(); }
        public void Vinsert_ContinueTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available)                 this.insertScene.Timer.ContinueTimer(); }
        public void Vinsert_ResetTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available)                 this.insertScene.Timer.ResetTimer(); }
        public void Vinsert_TimerOut() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available)                this.insertScene.Timer.ToOut(); }
        internal void Vinsert_PlayJingleBad() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.PlayJingleBad(); }
        internal void Vinsert_PlayJingleEnd() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.PlayJingleEnd(); }
        internal void Vinsert_PlayJingleGood() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.PlayJingleGood(); }
        internal void Vinsert_PlayJingleScore() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.PlayJingleScore(); }
        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void Vfullscreen_LoadScene() {
            base.Vfullscreen_LoadScene();
            this.fullscreenScene.Load();
        }
        public void Vfullscreen_CountdownIn() {
            this.Vfullscreen_SetCountdown();
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.CountdownToIn();
        }
        public void Vfullscreen_SetCountdown() {
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.fullscreenScene.SetCountdownStartTime(this.TimerStartTime);
                this.fullscreenScene.SetCountdownAlarmTime(this.TimerAlarmTime1);
            }
        }
        public void Vfullscreen_StartCountdown() { if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.StartCountdown(); }
        public void Vfullscreen_StopCountdown() { if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.StopCountdown(); }
        public void Vfullscreen_ResetCountdown() { if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.ResetCountdown(); }
        public void Vfullscreen_ContinueCountdown() { if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.ContinueCountdown(); }
        public void Vfullscreen_CountdownOut() { if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.CountdownToOut(); }
        public void Vfullscreen_SequenceIn() {
            this.Vfullscreen_SetSequence();
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.SequenceToIn();
        }
        public void Vfullscreen_SetSequence() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.fullscreenScene.SetSequenceValue(this.Sequence);
            }
        }
        public void Vfullscreen_SequenceOut() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.SequenceToOut(); }
        public override void Vfullscreen_UnloadScene() {
            base.Vfullscreen_UnloadScene();
            this.fullscreenScene.Unload();
        }

        public override void ClearGraphic() {
            base.ClearGraphic();
            this.insertScene.Clear();
            this.fullscreenScene.Clear();
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

        protected void insertScene_Timer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_insertScene_Timer_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_insertScene_Timer_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "CurrentTime") this.TimerCurrentTime = this.insertScene.Timer.CurrentTime;
                else if (e.PropertyName == "IsRunning") this.TimerIsRunning = this.insertScene.Timer.IsRunning;
            }
        }

        protected void insertScene_timer_Alarm1Fired(object sender, EventArgs e) {
            this.on_TimerAlarm1Fired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_insertScene_timer_Alarm1Fired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_insertScene_timer_Alarm1Fired(object content) {
        }

        protected void insertScene_timer_Alarm2Fired(object sender, EventArgs e) {
            this.on_TimerAlarm2Fired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_insertScene_timer_Alarm2Fired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_insertScene_timer_Alarm2Fired(object content) {
            this.SequenceIsStopped = true;
            this.StopTask();
        }

        protected void insertScene_timer_StopFired(object sender, EventArgs e) {
            this.on_TimerStopFired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_insertScene_timer_StopFired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_insertScene_timer_StopFired(object content) {
            this.SequenceIsStopped = true;
            this.StopTask();
        }

        private void speedcourtServer_Changed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_speedcourtServer_Changed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_speedcourtServer_Changed(object content) {
            this.SpeedcourtServerIsRunning = this.speedcourtServer.IsRunning;
            if (this.speedcourtServer.IsRunning &&
                this.speedcourtServer.HostCount > 0) this.SpeedcourtClientName = this.speedcourtServer.HostList[0].Name;
            else this.SpeedcourtClientName = string.Empty;
        }

        private void speedcourtServer_DataReceived(byte[] recBuffer, string recText) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_speedcourzClient_DataReceived);
            if (this.syncContext != null) this.syncContext.Post(callback, recText);
        }
        private void sync_speedcourzClient_DataReceived(object content) {
            string recText = content as string;
            this.parseReceivedData(recText);
        }

        #endregion

    }
}
