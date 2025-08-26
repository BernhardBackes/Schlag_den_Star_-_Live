using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cliparts.Serialization;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.TwoTimersScores {

    public class Business : _Base.Business {

        #region Properties

        private int twoTimersScoresPositionX = 0;
        public int TwoTimersScoresPositionX {
            get { return this.twoTimersScoresPositionX; }
            set {
                if (this.twoTimersScoresPositionX != value) {
                    this.twoTimersScoresPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTwoTimersScores();
                }
            }
        }

        private int twoTimersScoresPositionY = 0;
        public int TwoTimersScoresPositionY {
            get { return this.twoTimersScoresPositionY; }
            set {
                if (this.twoTimersScoresPositionY != value) {
                    this.twoTimersScoresPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTwoTimersScores();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.TwoTimersScores.Styles twoTimersScoresStyle = VentuzScenes.GamePool._Modules.TwoTimersScores.Styles.Sec;
        public VentuzScenes.GamePool._Modules.TwoTimersScores.Styles TwoTimersScoresTimerStyle {
            get { return this.twoTimersScoresStyle; }
            set {
                if (this.twoTimersScoresStyle != value) {
                    this.twoTimersScoresStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimers();
                    this.Vfullscreen_SetTimer();
                }
            }
        }

        private int twoTimersScoresStartTime = 240;
        public int TwoTimersScoresStartTime {
            get { return this.twoTimersScoresStartTime; }
            set {
                if (this.twoTimersScoresStartTime != value) {
                    this.twoTimersScoresStartTime = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimers();
                }
            }
        }

        private int twoTimersScoresAlarmTime1 = -1;
        public int TwoTimersScoresAlarmTime1 {
            get { return this.twoTimersScoresAlarmTime1; }
            set {
                if (this.twoTimersScoresAlarmTime1 != value) {
                    this.twoTimersScoresAlarmTime1 = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimers();
                }
            }
        }

        private int twoTimersScoresAlarmTime2 = -1;
        public int TwoTimersScoresAlarmTime2 {
            get { return this.twoTimersScoresAlarmTime2; }
            set {
                if (this.twoTimersScoresAlarmTime2 != value) {
                    this.twoTimersScoresAlarmTime2 = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimers();
                }
            }
        }

        private int twoTimersScoresStopTime = 0;
        public int TwoTimersScoresStopTime {
            get { return this.twoTimersScoresStopTime; }
            set {
                if (this.twoTimersScoresStopTime != value) {
                    this.twoTimersScoresStopTime = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimers();
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
        }

        public override void Init() {
            base.Init();
            this.LeftPlayerScore = 0;
            this.RightPlayerScore = 0;
        }

        public virtual void Vinsert_TwoTimersScoresIn() { }
        public virtual void Vinsert_TwoTimersScoresIn(VentuzScenes.GamePool._Modules.TwoTimersScores scene) {
            if (scene is VentuzScenes.GamePool._Modules.TwoTimersScores) {
                this.Vinsert_SetTwoTimersScores(scene);
                this.Vinsert_SetTimers(scene);
                this.Vinsert_ResetTopTimer(scene);
                this.Vinsert_ResetBottomTimer(scene);
                this.Vinsert_SetScores(scene);
                scene.ToIn();
            }
        }

        public virtual void Vinsert_SetTwoTimersScores() { }
        public virtual void Vinsert_SetTwoTimersScores(
            VentuzScenes.GamePool._Modules.TwoTimersScores scene) {
            if (scene is VentuzScenes.GamePool._Modules.TwoTimersScores) {
                scene.SetPositionX(this.TwoTimersScoresPositionX);
                scene.SetPositionY(this.TwoTimersScoresPositionY);
            }
        }

        public virtual void Vinsert_SetTimers() { }
        public virtual void Vinsert_SetTimers(VentuzScenes.GamePool._Modules.TwoTimersScores scene) {
            this.Vinsert_SetTimers(scene, this.TwoTimersScoresStartTime);
        }
        public virtual void Vinsert_SetTimers(
            VentuzScenes.GamePool._Modules.TwoTimersScores scene,
            int startTime) {
            if (scene is VentuzScenes.GamePool._Modules.TwoTimersScores) {
                scene.SetTopTimerStyle(this.TwoTimersScoresTimerStyle);
                scene.SetTopTimerStartTime(startTime);
                scene.SetTopTimerStopTime(this.TwoTimersScoresStopTime);
                scene.SetTopTimerAlarmTime1(this.TwoTimersScoresAlarmTime1);
                scene.SetTopTimerAlarmTime2(this.TwoTimersScoresAlarmTime2);
                scene.SetBottomTimerStyle(this.TwoTimersScoresTimerStyle);
                scene.SetBottomTimerStartTime(startTime);
                scene.SetBottomTimerStopTime(this.TwoTimersScoresStopTime);
                scene.SetBottomTimerAlarmTime1(this.TwoTimersScoresAlarmTime1);
                scene.SetBottomTimerAlarmTime2(this.TwoTimersScoresAlarmTime2);
            }
        }
        public virtual void Vinsert_StartTopTimer() { }
        public virtual void Vinsert_StartTopTimer(VentuzScenes.GamePool._Modules.TwoTimersScores scene) { if (scene is VentuzScenes.GamePool._Modules.TwoTimersScores) scene.StartTopTimer(); }
        public virtual void Vinsert_StopTopTimer() { }
        public virtual void Vinsert_StopTopTimer(VentuzScenes.GamePool._Modules.TwoTimersScores scene) { if (scene is VentuzScenes.GamePool._Modules.TwoTimersScores) scene.StopTopTimer(); }
        public virtual void Vinsert_ContinueTopTimer() { }
        public virtual void Vinsert_ContinueTopTimer(VentuzScenes.GamePool._Modules.TwoTimersScores scene) { if (scene is VentuzScenes.GamePool._Modules.TwoTimersScores) scene.ContinueTopTimer(); }
        public virtual void Vinsert_ResetTopTimer() { }
        public virtual void Vinsert_ResetTopTimer(VentuzScenes.GamePool._Modules.TwoTimersScores scene) { if (scene is VentuzScenes.GamePool._Modules.TwoTimersScores) scene.ResetTopTimer(); }

        public virtual void Vinsert_StartBottomTimer() { }
        public virtual void Vinsert_StartBottomTimer(VentuzScenes.GamePool._Modules.TwoTimersScores scene) { if (scene is VentuzScenes.GamePool._Modules.TwoTimersScores) scene.StartBottomTimer(); }
        public virtual void Vinsert_StopBottomTimer() { }
        public virtual void Vinsert_StopBottomTimer(VentuzScenes.GamePool._Modules.TwoTimersScores scene) { if (scene is VentuzScenes.GamePool._Modules.TwoTimersScores) scene.StopBottomTimer(); }
        public virtual void Vinsert_ContinueBottomTimer() { }
        public virtual void Vinsert_ContinueBottomTimer(VentuzScenes.GamePool._Modules.TwoTimersScores scene) { if (scene is VentuzScenes.GamePool._Modules.TwoTimersScores) scene.ContinueBottomTimer(); }
        public virtual void Vinsert_ResetBottomTimer() { }
        public virtual void Vinsert_ResetBottomTimer(VentuzScenes.GamePool._Modules.TwoTimersScores scene) { if (scene is VentuzScenes.GamePool._Modules.TwoTimersScores) scene.ResetBottomTimer(); }

        public virtual void Vinsert_SetScores() { }
        public virtual void Vinsert_SetScores(VentuzScenes.GamePool._Modules.TwoTimersScores scene) { this.Vinsert_SetScores(scene, this.LeftPlayerScore, this.RightPlayerScore); }
        public void Vinsert_SetScores(
            VentuzScenes.GamePool._Modules.TwoTimersScores scene,
            int leftPlayerScore,
            int rightPlayerScore) {
            if (scene is VentuzScenes.GamePool._Modules.TwoTimersScores
                && scene.Status == VRemote4.HandlerSi.Scene.States.Available) {
                scene.SetTopName(this.LeftPlayerName);
                scene.SetTopScore(leftPlayerScore);
                scene.SetBottomName(this.RightPlayerName);
                scene.SetBottomScore(rightPlayerScore);
            }
        }

        public virtual void Vinsert_TwoTimersScoresOut() { }
        public virtual void Vinsert_TwoTimersScoresOut(VentuzScenes.GamePool._Modules.TwoTimersScores scene) { if (scene is VentuzScenes.GamePool._Modules.TwoTimersScores) scene.ToOut(); }

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

        public event EventHandler TopTimerAlarm1Fired;
        protected void on_TopTimerAlarm1Fired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.TopTimerAlarm1Fired, e); }

        public event EventHandler TopTimerAlarm2Fired;
        protected void on_TopTimerAlarm2Fired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.TopTimerAlarm2Fired, e); }

        public event EventHandler TopTimerStopFired;
        protected void on_TopTimerStopFired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.TopTimerStopFired, e); }

        public event EventHandler BottomTimerAlarm1Fired;
        protected void on_BottomTimerAlarm1Fired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.BottomTimerAlarm1Fired, e); }

        public event EventHandler BottomTimerAlarm2Fired;
        protected void on_BottomTimerAlarm2Fired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.BottomTimerAlarm2Fired, e); }

        public event EventHandler BottomTimerStopFired;
        protected void on_BottomTimerStopFired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.BottomTimerStopFired, e); }

        #endregion

        #region Events.Incoming

        protected void timer_TopTimerAlarm1Fired(object sender, EventArgs e) {
            this.on_TopTimerAlarm1Fired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_TopTimerAlarm1Fired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_TopTimerAlarm1Fired(object content) {
        }

        protected void timer_TopTimerAlarm2Fired(object sender, EventArgs e) {
            this.on_TopTimerAlarm2Fired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_TopTimerAlarm2Fired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_TopTimerAlarm2Fired(object content) {
        }

        protected void timer_TopTimerStopFired(object sender, EventArgs e) {
            this.on_TopTimerStopFired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_TopTimerStopFired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_TopTimerStopFired(object content) {
        }

        protected void timer_BottomTimerAlarm1Fired(object sender, EventArgs e) {
            this.on_BottomTimerAlarm1Fired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_BottomTimerAlarm1Fired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_BottomTimerAlarm1Fired(object content) {
        }

        protected void timer_BottomTimerAlarm2Fired(object sender, EventArgs e) {
            this.on_BottomTimerAlarm2Fired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_BottomTimerAlarm2Fired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_BottomTimerAlarm2Fired(object content) {
        }

        protected void timer_BottomTimerStopFired(object sender, EventArgs e) {
            this.on_BottomTimerStopFired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_BottomTimerStopFired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_BottomTimerStopFired(object content) {
        }

        protected void timer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_PropertyChanged(object content) {
        }

        #endregion

    }
}
