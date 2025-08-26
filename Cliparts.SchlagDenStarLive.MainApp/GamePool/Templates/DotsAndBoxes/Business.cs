using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.DotsAndBoxes;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.DotsAndBoxes {

    public class Business : _Base.TimerScore.Business {

        #region Properties

        private int borderPositionX = 0;
        public int BorderPositionX {
            get { return this.borderPositionX; }
            set {
                if (this.borderPositionX != value) {
                    this.borderPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetBorder();
                }
            }
        }

        private int borderPositionY = 0;
        public int BorderPositionY {
            get { return this.borderPositionY; }
            set {
                if (this.borderPositionY != value) {
                    this.borderPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetBorder();
                }
            }
        }

        private int borderScaling = 100;
        public int BorderScaling {
            get { return this.borderScaling; }
            set {
                if (this.borderScaling != value) {
                    if (value < 10) this.borderScaling = 10;
                    else this.borderScaling = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetBorder();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.Border.Styles setsStyle = VentuzScenes.GamePool._Modules.Border.Styles.ThreeDotsCounter;
        public VentuzScenes.GamePool._Modules.Border.Styles BorderStyle {
            get { return this.setsStyle; }
            set {
                if (this.setsStyle != value) {
                    this.setsStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetBorder();
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

        private bool selectedPlayerStays;
        public bool SelectedPlayerStays {
            get { return this.selectedPlayerStays; }
            private set {
                if (this.selectedPlayerStays != value) {
                    selectedPlayerStays = value;
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

        private Player leftPlayerScene;
        public override VRemote4.HandlerSi.Scene.States LeftPlayerSceneStatus {
            get {
                if (this.leftPlayerScene is Player) return this.leftPlayerScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private Player rightPlayerScene;
        public override VRemote4.HandlerSi.Scene.States RightPlayerSceneStatus {
            get {
                if (this.rightPlayerScene is Player) return this.rightPlayerScene.Status;
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.DotsAndBoxes'", typeIdentifier);
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

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;
            this.insertScene.Timer.Alarm1Fired += this.timer_Alarm1Fired;
            this.insertScene.Timer.Alarm2Fired += this.timer_Alarm2Fired;
            this.insertScene.Timer.StopFired += this.timer_StopFired;
            this.insertScene.Timer.PropertyChanged += this.timer_PropertyChanged;

            this.fullscreenScene = new Fullscreen(syncContext, fullscreenMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.fullscreenScene.StatusChanged += this.fullscreenScene_StatusChanged;

            this.leftPlayerScene = new Player(syncContext, leftPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.leftPlayerScene.StatusChanged += this.leftPlayerScene_StatusChanged;
            this.leftPlayerScene.Game.BoxAdded += this.leftPlayerGame_BoxAdded;
            this.leftPlayerScene.Game.PropertyChanged += this.leftPlayerGame_PropertyChanged;

            this.rightPlayerScene = new Player(syncContext, rightPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.rightPlayerScene.StatusChanged += this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.Game.BoxAdded += this.rightPlayerGame_BoxAdded;
            this.rightPlayerScene.Game.PropertyChanged += this.rightPlayerGame_PropertyChanged;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(
                this,
                this.insertScene,
                this.fullscreenScene,
                this.leftPlayerScene,
                this.rightPlayerScene);
        }

        public override void Dispose() {
            base.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Timer.Alarm1Fired -= this.timer_Alarm1Fired;
            this.insertScene.Timer.Alarm2Fired -= this.timer_Alarm2Fired;
            this.insertScene.Timer.StopFired -= this.timer_StopFired;
            this.insertScene.Timer.PropertyChanged -= this.timer_PropertyChanged;
            this.insertScene.Dispose();

            this.fullscreenScene.StatusChanged -= this.fullscreenScene_StatusChanged;
            this.fullscreenScene.Dispose();

            this.leftPlayerScene.StatusChanged -= this.leftPlayerScene_StatusChanged;
            this.leftPlayerScene.Game.BoxAdded -= this.leftPlayerGame_BoxAdded;
            this.leftPlayerScene.Game.PropertyChanged -= this.leftPlayerGame_PropertyChanged;
            this.leftPlayerScene.Dispose();

            this.rightPlayerScene.StatusChanged -= this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.Game.BoxAdded -= this.rightPlayerGame_BoxAdded;
            this.rightPlayerScene.Game.PropertyChanged -= this.rightPlayerGame_PropertyChanged;
            this.rightPlayerScene.Dispose();
        }

        public override void Deactivate() {
            base.Deactivate();
            ((UserControlGame)this.GameControl).CloseFormBoardControl();
        }

        public override void ResetData() {
            base.ResetData();
            this.ResetGame();
        }

        private void ResetGame() {
            frames.Clear();
            resetBase();
        }
        private void resetBase() {
            this.vfullscreen_ResetGame();
            this.Vplayer_ResetGame();
            this.LeftPlayerCounter = 0;
            this.RightPlayerCounter = 0;
            this.SelectedPlayerStays = false;
            setCounters();
        }

        public override void Next() {
            this.ResetGame();
        }

        public void NextPlayer() {
            if (this.SelectedPlayer == Content.Gameboard.PlayerSelection.LeftPlayer) this.SelectedPlayer = Content.Gameboard.PlayerSelection.RightPlayer;
            else this.SelectedPlayer = Content.Gameboard.PlayerSelection.LeftPlayer;
        }

        private void distributeSelectedFrame(
            string name) {
            if (!string.IsNullOrEmpty(name)) {
                this.Vinsert_StopTimer();
                this.Vplayer_OnlyStopClock();
                this.Vplayer_DisableInput();
                this.GameControl.SetToNextStep();
                string[] nameArray = name.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                int firstIndex;
                int secondIndex;
                if (nameArray.Length == 3 &&
                    int.TryParse(nameArray[1], out firstIndex) &&
                    int.TryParse(nameArray[2], out secondIndex)) {
                    if(!replaying)
                        frames.Add(new FrameData {
                            Name = name,
                            Player = this.SelectedPlayer
                        });
                    if (nameArray[0].ToUpper() == "H") {
                        if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.Game.SetHorizontalFrameIn(firstIndex, secondIndex);
                        if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.Game.SetHorizontalFrameIn(firstIndex, secondIndex);
                        if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.Game.SetHorizontalFrameIn(firstIndex, secondIndex);
                    }
                    else if (nameArray[0].ToUpper() == "V") {
                        if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.Game.SetVerticalFrameIn(firstIndex, secondIndex);
                        if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.Game.SetVerticalFrameIn(firstIndex, secondIndex);
                        if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.Game.SetVerticalFrameIn(firstIndex, secondIndex);
                    }
                }
            }
        }
        private List<FrameData> frames = new List<FrameData>();
        private bool replaying;
        public void ReplayFrames() {
            replaying = true;
            resetBase();
            Task.Factory.StartNew(async () => {
                
            });
            replaying = false;
        }

       


        public override void Vinsert_LoadScene() { this.insertScene.Load(); }
        public override void Vinsert_TimerIn() {
            this.Vinsert_SetTimer();
            this.Vinsert_ResetTimer();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.ToIn();
        }
        public override void Vinsert_SetTimer() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.Timer.SetPositionX(this.TimerPositionX);
                this.insertScene.Timer.SetPositionY(this.TimerPositionY);
                this.insertScene.Timer.SetStyle(this.TimerStyle);
                this.insertScene.Timer.SetScaling(100);
                if (this.RunExtraTime) this.insertScene.Timer.SetStartTime(this.TimerExtraTime);
                else this.insertScene.Timer.SetStartTime(this.TimerStartTime);
                this.insertScene.Timer.SetStopTime(this.TimerStopTime);
                this.insertScene.Timer.SetAlarmTime1(this.TimerAlarmTime1);
                this.insertScene.Timer.SetAlarmTime2(this.TimerAlarmTime2);
            }
        }
        public override void Vinsert_StartTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.StartTimer(); }
        public override void Vinsert_StopTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.StopTimer(); }
        public override void Vinsert_ContinueTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.ContinueTimer(); }
        public override void Vinsert_ResetTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.ResetTimer(); }
        public override void Vinsert_TimerOut() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.ToOut(); }
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
        public void Vinsert_BorderIn() {
            this.Vinsert_SetBorder();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Border.ToIn();
        }
        public void Vinsert_SetBorder() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.Border.SetPositionX(this.BorderPositionX);
                this.insertScene.Border.SetPositionY(this.BorderPositionY);
                this.insertScene.Border.SetScaling(this.BorderScaling);
                this.insertScene.Border.SetStyle(this.BorderStyle);
                this.insertScene.Border.SetLeftScore(this.LeftPlayerScore);
                this.insertScene.Border.SetLeftCounter(this.LeftPlayerCounter);
                this.insertScene.Border.SetRightScore(this.RightPlayerScore);
                this.insertScene.Border.SetRightCounter(this.RightPlayerCounter);
                this.insertScene.Border.ResetBuzzer();
            }
        }
        public void Vinsert_BorderOut() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Border.ToOut(); }
        public override void Vinsert_UnloadScene() { this.insertScene.Unload(); }

        public override void Vfullscreen_LoadScene() { this.fullscreenScene.Load(); }
        public void vfullscreen_SelectGamePlayer() {
            if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                if (this.SelectedPlayer == Content.Gameboard.PlayerSelection.LeftPlayer) this.fullscreenScene.Game.SelectLeftPlayer();
                else this.fullscreenScene.Game.SelectRightPlayer();
            }
        }
        public void vfullscreen_ResetGame() { if (this.FullscreenSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.fullscreenScene.Game.Reset(); }
        public override void Vfullscreen_UnloadScene() { this.fullscreenScene.Unload(); }

        public override void Vstage_Init() {
            base.Vstage_Init();
            this.Vplayer_GameOut();
            if (this.hostMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostMasterScene.LiveVideoIn();
        }

        public override void Vleftplayer_LoadScene() { this.leftPlayerScene.Load(); }
        public override void Vleftplayer_UnloadScene() { this.leftPlayerScene.Unload(); }

        public override void Vrightplayer_LoadScene() { this.rightPlayerScene.Load(); }
        public override void Vrightplayer_UnloadScene() { this.rightPlayerScene.Unload(); }

        public void Vplayer_GameIn() {
            this.ResetGame();
            this.SelectedPlayerStays = false;
            this.Vplayer_DisableInput();
            this.Vplayer_StopTimer();
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.leftPlayerScene.Game.SetAutoLock(true);
                this.leftPlayerScene.FadeIn();
            }
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.rightPlayerScene.Game.SetAutoLock(true);
                this.rightPlayerScene.FadeIn();
            }
        }
        public void Vplayer_EnableInput() {
            this.SelectedPlayerStays = false;
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.leftPlayerScene.Game.SetTouch(this.SelectedPlayer == Content.Gameboard.PlayerSelection.LeftPlayer);
                if (this.SelectedPlayer == Content.Gameboard.PlayerSelection.LeftPlayer) this.leftPlayerScene.Game.SelectLeftPlayer();
                else this.leftPlayerScene.Game.SelectRightPlayer();
            }
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.rightPlayerScene.Game.SetTouch(this.SelectedPlayer == Content.Gameboard.PlayerSelection.RightPlayer);
                if (this.SelectedPlayer == Content.Gameboard.PlayerSelection.LeftPlayer) this.rightPlayerScene.Game.SelectLeftPlayer();
                else this.rightPlayerScene.Game.SelectRightPlayer();
            }
        }
        public void Vplayer_DisableInput() {
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.Game.SetTouch(false);
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.Game.SetTouch(false);
        }
        internal void Vplayer_SetCounter() {
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.leftPlayerScene.SetLeftPlayerCounter(this.LeftPlayerCounter);
                this.leftPlayerScene.SetRightPlayerCounter(this.RightPlayerCounter);
            }
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.rightPlayerScene.SetLeftPlayerCounter(this.LeftPlayerCounter);
                this.rightPlayerScene.SetRightPlayerCounter(this.RightPlayerCounter);
            }
        }
        internal void Vplayer_StartTimer() {
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                if (this.SelectedPlayer == Content.Gameboard.PlayerSelection.LeftPlayer) {
                    this.leftPlayerScene.SetTimerPosition(VentuzScenes.GamePool.DotsAndBoxes.Player.Positions.Left);
                    this.leftPlayerScene.Timer.SetPositionX(0);
                    this.leftPlayerScene.Timer.SetPositionY(0);
                    this.leftPlayerScene.Timer.SetStyle(this.TimerStyle);
                    this.leftPlayerScene.Timer.SetScaling(100);
                    if (this.RunExtraTime) this.leftPlayerScene.Timer.SetStartTime(this.TimerExtraTime);
                    else this.leftPlayerScene.Timer.SetStartTime(this.TimerStartTime);
                    this.leftPlayerScene.Timer.SetStopTime(this.TimerStopTime);
                    this.leftPlayerScene.Timer.SetAlarmTime1(-1);
                    this.leftPlayerScene.Timer.SetAlarmTime2(-1);
                    this.leftPlayerScene.Timer.StartTimer();
                    this.leftPlayerScene.Timer.ToIn();
                }
                else {
                    this.leftPlayerScene.Timer.StopTimer();
                    this.leftPlayerScene.Timer.ToOut();
                    Helper.invokeActionAfterDelay(this.leftPlayerScene.Timer.ResetTimer, 500, this.syncContext);
                }
            }
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                if (this.SelectedPlayer == Content.Gameboard.PlayerSelection.RightPlayer) {
                    this.rightPlayerScene.SetTimerPosition(VentuzScenes.GamePool.DotsAndBoxes.Player.Positions.Right);
                    this.rightPlayerScene.Timer.SetPositionX(0);
                    this.rightPlayerScene.Timer.SetPositionY(0);
                    this.rightPlayerScene.Timer.SetStyle(this.TimerStyle);
                    this.rightPlayerScene.Timer.SetScaling(100);
                    if (this.RunExtraTime) this.rightPlayerScene.Timer.SetStartTime(this.TimerExtraTime);
                    else this.rightPlayerScene.Timer.SetStartTime(this.TimerStartTime);
                    this.rightPlayerScene.Timer.SetStopTime(this.TimerStopTime);
                    this.rightPlayerScene.Timer.SetAlarmTime1(-1);
                    this.rightPlayerScene.Timer.SetAlarmTime2(-1);
                    this.rightPlayerScene.Timer.StartTimer();
                    this.rightPlayerScene.Timer.ToIn();
                }
                else {
                    this.rightPlayerScene.Timer.StopTimer();
                    this.rightPlayerScene.Timer.ToOut();
                    Helper.invokeActionAfterDelay(this.rightPlayerScene.Timer.ResetTimer, 500, this.syncContext);
                }
            }
        }
        internal void Vplayer_ResetTimerAndOut() {
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                Helper.invokeActionAfterDelay(this.leftPlayerScene.Timer.ResetTimer, 500, this.syncContext);
                this.leftPlayerScene.Timer.ToOut();
            }
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                Helper.invokeActionAfterDelay(this.rightPlayerScene.Timer.ResetTimer, 500, this.syncContext);
                this.rightPlayerScene.Timer.ToOut();
            }
        }
        public void Vplayer_OnlyStopClock() {
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.leftPlayerScene.Timer.StopTimer();
            }
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.rightPlayerScene.Timer.StopTimer();
            }
        }


        internal void Vplayer_StopTimer() {
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.leftPlayerScene.Timer.StopTimer();
                this.leftPlayerScene.Timer.ToOut();
                Helper.invokeActionAfterDelay(this.leftPlayerScene.Timer.ResetTimer, 500, this.syncContext);
            }
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.rightPlayerScene.Timer.StopTimer();
                this.rightPlayerScene.Timer.ToOut();
                Helper.invokeActionAfterDelay(this.rightPlayerScene.Timer.ResetTimer, 500, this.syncContext);
            }
        }
        public void Vplayer_GameOut() {
            this.SelectedPlayerStays = false;
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.FadeOut();
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.FadeOut();
        }
        public void Vplayer_ResetGame() {
            this.SelectedPlayerStays = false;
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.Game.Reset();
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.Game.Reset();
        }

        public override void ClearGraphic() {
            base.ClearGraphic();
            this.insertScene.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming

        protected override void sync_timer_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "CurrentTime") this.TimerCurrentTime = Convert.ToInt32(this.insertScene.Timer.CurrentTime);
            }
        }

        void leftPlayerGame_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftPlayerGame_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftPlayerGame_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs &&
                this.SelectedPlayer == Content.Gameboard.PlayerSelection.LeftPlayer) {
                if (e.PropertyName == "LeftPlayerCounter") {
                    //this.LeftPlayerCounter = this.leftPlayerScene.Game.LeftPlayerCounter;
                    //this.Vplayer_SetCounter();
                }
                else if (e.PropertyName == "LastSelectedFrame") this.distributeSelectedFrame(this.leftPlayerScene.Game.LastSelectedFrame);
            }
        }

        void leftPlayerGame_BoxAdded(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftPlayerGame_BoxAdded);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftPlayerGame_BoxAdded(object content) {
            this.SelectedPlayerStays = true;
            if (this.SelectedPlayer == Content.Gameboard.PlayerSelection.LeftPlayer) this.LeftPlayerCounter++;
            setCounters();
        }

        void rightPlayerGame_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerGame_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerGame_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs &&
                this.SelectedPlayer == Content.Gameboard.PlayerSelection.RightPlayer) {
                if (e.PropertyName == "RightPlayerCounter") {
                    //this.RightPlayerCounter = this.rightPlayerScene.Game.RightPlayerCounter;
                    //this.Vplayer_SetCounter();
                }
                else if (e.PropertyName == "LastSelectedFrame") this.distributeSelectedFrame(this.rightPlayerScene.Game.LastSelectedFrame);
            }
        }

        void rightPlayerGame_BoxAdded(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerGame_BoxAdded);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerGame_BoxAdded(object content) {
            this.SelectedPlayerStays = true;
            if (this.SelectedPlayer == Content.Gameboard.PlayerSelection.RightPlayer) this.RightPlayerCounter++;

            setCounters();
        }

        private void setCounters() {
            this.Vplayer_SetCounter();
            this.Vinsert_SetBorder();
        }

        #endregion

    }
}
