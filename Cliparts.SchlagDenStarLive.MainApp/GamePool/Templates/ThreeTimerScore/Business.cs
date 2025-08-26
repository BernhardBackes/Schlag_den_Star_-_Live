using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.ThreeTimerScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.ThreeTimerScore {

    public class Business : _Base.TimerScore.Business {

        #region Properties

        private int leftTimerPositionX = 0;
        public int LeftTimerPositionX {
            get { return this.leftTimerPositionX; }
            set {
                if (this.leftTimerPositionX != value) {
                    this.leftTimerPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetLeftTimer();
                }
            }
        }

        private int leftTimerPositionY = 0;
        public int LeftTimerPositionY {
            get { return this.leftTimerPositionY; }
            set {
                if (this.leftTimerPositionY != value) {
                    this.leftTimerPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetLeftTimer();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.Timer.Styles leftTimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
        public VentuzScenes.GamePool._Modules.Timer.Styles LeftTimerStyle {
            get { return this.leftTimerStyle; }
            set {
                if (this.leftTimerStyle != value) {
                    this.leftTimerStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetLeftTimer();
                }
            }
        }

        private int leftTimerStartTime = 240;
        public int LeftTimerStartTime {
            get { return this.leftTimerStartTime; }
            set {
                if (this.leftTimerStartTime != value) {
                    this.leftTimerStartTime = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetLeftTimer();
                }
            }
        }

        private int leftTimerStopTime = 0;
        public int LeftTimerStopTime {
            get { return this.leftTimerStopTime; }
            set {
                if (this.leftTimerStopTime != value) {
                    this.leftTimerStopTime = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetLeftTimer();
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

        private int rightTimerPositionX = 0;
        public int RightTimerPositionX {
            get { return this.rightTimerPositionX; }
            set {
                if (this.rightTimerPositionX != value) {
                    this.rightTimerPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetRightTimer();
                }
            }
        }

        private int rightTimerPositionY = 0;
        public int RightTimerPositionY {
            get { return this.rightTimerPositionY; }
            set {
                if (this.rightTimerPositionY != value) {
                    this.rightTimerPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetRightTimer();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.Timer.Styles rightTimerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
        public VentuzScenes.GamePool._Modules.Timer.Styles RightTimerStyle {
            get { return this.rightTimerStyle; }
            set {
                if (this.rightTimerStyle != value) {
                    this.rightTimerStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetRightTimer();
                }
            }
        }

        private int rightTimerStartTime = 240;
        public int RightTimerStartTime {
            get { return this.rightTimerStartTime; }
            set {
                if (this.rightTimerStartTime != value) {
                    this.rightTimerStartTime = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetRightTimer();
                }
            }
        }

        private int rightTimerStopTime = 0;
        public int RightTimerStopTime {
            get { return this.rightTimerStopTime; }
            set {
                if (this.rightTimerStopTime != value) {
                    this.rightTimerStopTime = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetRightTimer();
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

        private int leftRightTimerPositionX = 0;
        public int LeftRightTimerPositionX {
            get { return this.leftRightTimerPositionX; }
            set {
                if (this.leftRightTimerPositionX != value) {
                    this.leftRightTimerPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetLeftRightTimer();
                }
            }
        }

        private int leftRightTimerPositionY = 0;
        public int LeftRightTimerPositionY {
            get { return this.leftRightTimerPositionY; }
            set {
                if (this.leftRightTimerPositionY != value) {
                    this.leftRightTimerPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetLeftRightTimer();
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

        #endregion


        #region Funktionen

        public Business() { }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.ThreeTimerScore'", typeIdentifier);
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
            this.insertScene.LeftTimer.PropertyChanged += this.leftTimer_PropertyChanged;
            this.insertScene.RightTimer.PropertyChanged += this.rightTimer_PropertyChanged;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);
        }

        public override void Dispose() {
            base.Dispose();
            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Timer.Alarm1Fired -= this.timer_Alarm1Fired;
            this.insertScene.Timer.Alarm2Fired -= this.timer_Alarm2Fired;
            this.insertScene.Timer.StopFired -= this.timer_StopFired;
            this.insertScene.Timer.PropertyChanged -= this.timer_PropertyChanged;
            this.insertScene.LeftTimer.PropertyChanged -= this.leftTimer_PropertyChanged;
            this.insertScene.RightTimer.PropertyChanged -= this.rightTimer_PropertyChanged;
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }
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
        public override void Vinsert_ScoreOut() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Score.ToOut();
        }

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

        public void Vinsert_LeftTimerIn() {
            this.Vinsert_SetLeftTimer();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.LeftTimer.ToIn();
        }
        public void Vinsert_SetLeftTimer() {
            this.Vinsert_SetLeftRightTimer();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.LeftTimer.SetPositionX(this.LeftTimerPositionX);
                this.insertScene.LeftTimer.SetPositionY(this.LeftTimerPositionY);
                this.insertScene.LeftTimer.SetScaling(100);
                this.insertScene.LeftTimer.SetStyle(this.LeftTimerStyle);
                this.insertScene.LeftTimer.SetStartTime(this.LeftTimerStartTime);
                this.insertScene.LeftTimer.SetStopTime(this.LeftTimerStopTime);
                this.insertScene.LeftTimer.SetAlarmTime1(-1);
                this.insertScene.LeftTimer.SetAlarmTime2(-1);
            }
        }
        public void Vinsert_StartLeftTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.LeftTimer.StartTimer(); }
        public void Vinsert_StopLeftTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.LeftTimer.StopTimer(); }
        public void Vinsert_ContinueLeftTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.LeftTimer.ContinueTimer(); }
        public void Vinsert_ResetLeftTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.LeftTimer.ResetTimer(); }
        public void Vinsert_LeftTimerOut() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.LeftTimer.ToOut(); }

        public void Vinsert_RightTimerIn() {
            this.Vinsert_SetRightTimer();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.RightTimer.ToIn();
        }
        public void Vinsert_SetRightTimer() {
            this.Vinsert_SetLeftRightTimer();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.RightTimer.SetPositionX(this.RightTimerPositionX);
                this.insertScene.RightTimer.SetPositionY(this.RightTimerPositionY);
                this.insertScene.RightTimer.SetStyle(this.RightTimerStyle);
                this.insertScene.RightTimer.SetScaling(100);
                this.insertScene.RightTimer.SetStartTime(this.RightTimerStartTime);
                this.insertScene.RightTimer.SetStopTime(this.RightTimerStopTime);
                this.insertScene.RightTimer.SetAlarmTime1(-1);
                this.insertScene.RightTimer.SetAlarmTime2(-1);
            }
        }
        public void Vinsert_StartRightTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.RightTimer.StartTimer(); }
        public void Vinsert_StopRightTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.RightTimer.StopTimer(); }
        public void Vinsert_ContinueRightTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.RightTimer.ContinueTimer(); }
        public void Vinsert_ResetRightTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.RightTimer.ResetTimer(); }
        public void Vinsert_RightTimerOut() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.RightTimer.ToOut(); }

        public void Vinsert_SetLeftRightTimer() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.SetPositionX(this.LeftRightTimerPositionX);
                this.insertScene.SetPositionY(this.LeftRightTimerPositionY);
            }
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
        #endregion

        #region Events.Incoming

        protected override void sync_timer_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "CurrentTime") this.TimerCurrentTime = Convert.ToInt32(this.insertScene.Timer.CurrentTime);
            }
        }

        void leftTimer_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftTimer_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_leftTimer_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "CurrentTime") this.LeftTimerCurrentTime = Convert.ToInt32(this.insertScene.LeftTimer.CurrentTime);
            }
        }

        void rightTimer_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightTimer_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_rightTimer_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "CurrentTime") this.RightTimerCurrentTime = Convert.ToInt32(this.insertScene.RightTimer.CurrentTime);
            }
        }

        #endregion

    }
}
