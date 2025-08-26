using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.PenaltyShotclock;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.PenaltyShotclock {

    public class Business : _Base.Penalty.Business {

        #region Properties

        private VentuzScenes.GamePool._Modules.Timer.Styles timerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
        public VentuzScenes.GamePool._Modules.Timer.Styles TimerStyle {
            get { return this.timerStyle; }
            set {
                if (this.timerStyle != value) {
                    this.timerStyle = value;
                    this.on_PropertyChanged();
                    this.Vfullscreen_SetTimer();
                }
            }
        }

        private int timerStartTime = 10;
        public int TimerStartTime {
            get { return this.timerStartTime; }
            set {
                if (this.timerStartTime != value) {
                    this.timerStartTime = value;
                    this.on_PropertyChanged();
                    this.Vfullscreen_SetTimer();
                }
            }
        }

        private int timerAlarmTime1 = -1;
        public int TimerAlarmTime1 {
            get { return this.timerAlarmTime1; }
            set {
                if (this.timerAlarmTime1 != value) {
                    this.timerAlarmTime1 = value;
                    this.on_PropertyChanged();
                    this.Vfullscreen_SetTimer();
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
                    this.Vfullscreen_SetTimer();
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
                    this.Vfullscreen_SetTimer();
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.PenaltyShotclock'", typeIdentifier);
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

            this.ShowFullscreenTimer = true;

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;

            this.fullscreenMasterScene.Timer.Alarm1Fired += this.timer_Alarm1Fired;
            this.fullscreenMasterScene.Timer.Alarm2Fired += this.timer_Alarm2Fired;
            this.fullscreenMasterScene.Timer.StopFired += this.timer_StopFired;
            this.fullscreenMasterScene.Timer.PropertyChanged += this.timer_PropertyChanged;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);
        }

        public override void Dispose() {
            base.Dispose();
            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }
        public override void Vinsert_PenaltyIn() {
            this.Vinsert_SetPenalty();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Penalty.ToIn();
        }
        public override void Vinsert_SetPenalty() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.Penalty.SetPositionX(this.PenaltyPositionX);
                this.insertScene.Penalty.SetPositionY(this.PenaltyPositionY);
                this.insertScene.Penalty.SetSize(this.PenaltyDotsCount);
                this.insertScene.Penalty.SetTopName(this.LeftPlayerName);
                this.insertScene.Penalty.SetBottomName(this.RightPlayerName);
                int id = 1;
                foreach (_Base.Penalty.SingleDot dot in this.leftPlayerPenaltyDots) {
                    this.insertScene.Penalty.SetTopDot(id, dot.Status);
                    id++;
                }
                id = 1;
                foreach (_Base.Penalty.SingleDot dot in this.RightPlayerPenaltyDots) {
                    this.insertScene.Penalty.SetBottomDot(id, dot.Status); id++;
                }
            }
        }
        public override void Vinsert_PenaltyOut() { this.Vinsert_PenaltyOut(this.insertScene.Penalty); }
        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void Vfullscreen_SetTimer() {
            base.Vfullscreen_SetTimer();
            if (this.fullscreenMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                switch (this.TimerStyle) {
                    case Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules.Timer.Styles.Sec:
                        this.fullscreenMasterScene.Timer.SetStyle(VentuzScenes.Fullscreen.Clock.Styles.Sec);
                        break;
                    case Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules.Timer.Styles.MinSec:
                    default:
                        this.fullscreenMasterScene.Timer.SetStyle(VentuzScenes.Fullscreen.Clock.Styles.MinSec);
                        break;
                }
                this.fullscreenMasterScene.Timer.SetStartTime(this.TimerStartTime);
                this.fullscreenMasterScene.Timer.SetStopTime(this.TimerStopTime);
                this.fullscreenMasterScene.Timer.SetAlarmTime1(this.TimerAlarmTime1);
                this.fullscreenMasterScene.Timer.SetAlarmTime2(this.TimerAlarmTime2);
            }
        }

        public override void ClearGraphic() {
            base.ClearGraphic();
            this.insertScene.Clear();
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

        protected void timer_Alarm1Fired(object sender, EventArgs e) {
            this.on_TimerAlarm1Fired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_Alarm1Fired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_Alarm1Fired(object content) {
        }

        protected void timer_Alarm2Fired(object sender, EventArgs e) {
            this.on_TimerAlarm2Fired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_Alarm2Fired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_Alarm2Fired(object content) {
        }

        protected void timer_StopFired(object sender, EventArgs e) {
            this.on_TimerStopFired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_StopFired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_StopFired(object content) {
        }

        protected void timer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "CurrentTime") this.TimerCurrentTime = this.fullscreenMasterScene.Timer.CurrentTime;
                //else if (e.PropertyName == "IsRunning") this.TimerIsRunning = this.fullscreenMasterScene.Timer.IsRunning;
            }
        }

        #endregion

    }
}
