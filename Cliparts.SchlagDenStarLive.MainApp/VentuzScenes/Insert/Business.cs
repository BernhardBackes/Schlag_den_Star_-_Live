using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.Insert {

    public class Business : VRemote4.HandlerSi.Scene, INotifyPropertyChanged {

        //	[Path]=.SafeArea.IsVisible
        //	[Path]=.SafeArea.IsVisibleChanged
        //	[Path]=.SafeArea.SetOut
        //	[Path]=.SafeArea.ToOut
        //	[Path]=.SafeArea.SetIn
        //	[Path]=.SafeArea.ToIn
        //	[Path]=.CornerLogo.IsVisible
        //	[Path]=.CornerLogo.IsVisibleChanged
        //	[Path]=.CornerLogo.SetOut
        //	[Path]=.CornerLogo.ToOut
        //	[Path]=.CornerLogo.SetIn
        //	[Path]=.CornerLogo.ToIn
        //	[Path]=.Welcome.IsVisible
        //	[Path]=.Welcome.IsVisibleChanged
        //	[Path]=.Welcome.SetOut
        //	[Path]=.Welcome.SetIn
        //	[Path]=.Background.IsVisible
        //	[Path]=.Background.IsVisibleChanged
        //	[Path]=.Background.SetOut
        //	[Path]=.Background.ToOut
        //	[Path]=.Background.SetIn
        //	[Path]=.Background.ToIn
        //	[Path]=.Clear
        //	[Path]=.Reset
        //	[Path]=.SetPreview
        //	[Path]=.Game.Loaded

        #region Properties

        private const string sceneID = "project/insert";

        private bool? safeAreaIsVisible = null;
        public bool SafeAreaIsVisible {
            get {
                if (this.safeAreaIsVisible.HasValue) return this.safeAreaIsVisible.Value;
                else return false;
            }
        }

        private bool? welcomeIsVisible = null;
        public bool WelcomeIsVisible {
            get {
                if (this.welcomeIsVisible.HasValue) return this.welcomeIsVisible.Value;
                else return false;
            }
        }

        private bool? cornerLogoIsVisible = null;
        public bool CornerLogoIsVisible {
            get {
                if (this.cornerLogoIsVisible.HasValue) return this.cornerLogoIsVisible.Value;
                else return false;
            }
        }

        private bool? backgroundIsVisible = null;
        public bool BackgroundIsVisible {
            get {
                if (this.backgroundIsVisible.HasValue) return this.backgroundIsVisible.Value;
                else return false;
            }
        }

        public int TimerPositionX {
            get { return this.settings.TimerPositionX; }
            set {
                if (this.settings.TimerPositionX != value) {
                    this.settings.TimerPositionX = value;
                    this.on_PropertyChanged();
                    this.SetTimer();
                }
            }
        }

        public int TimerPositionY {
            get { return this.settings.TimerPositionY; }
            set {
                if (this.settings.TimerPositionY != value) {
                    this.settings.TimerPositionY = value;
                    this.on_PropertyChanged();
                    this.SetTimer();
                }
            }
        }

        public VentuzScenes.GamePool._Modules.Timer.Styles TimerStyle {
            get { return this.settings.TimerStyle; }
            set {
                if (this.settings.TimerStyle != value) {
                    this.settings.TimerStyle = value;
                    this.on_PropertyChanged();
                    this.SetTimer();
                }
            }
        }

        public int TimerStartTime {
            get { return this.settings.TimerStartTime; }
            set {
                if (this.settings.TimerStartTime != value) {
                    this.settings.TimerStartTime = value;
                    this.on_PropertyChanged();
                    this.SetTimer();
                }
            }
        }

        public int TimerAlarmTime1 {
            get { return this.settings.TimerAlarmTime1; }
            set {
                if (this.settings.TimerAlarmTime1 != value) {
                    this.settings.TimerAlarmTime1 = value;
                    this.on_PropertyChanged();
                    this.SetTimer();
                }
            }
        }

        public int TimerAlarmTime2 {
            get { return this.settings.TimerAlarmTime2; }
            set {
                if (this.settings.TimerAlarmTime2 != value) {
                    this.settings.TimerAlarmTime2 = value;
                    this.on_PropertyChanged();
                    this.SetTimer();
                }
            }
        }

        public int TimerStopTime {
            get { return this.settings.TimerStopTime; }
            set {
                if (this.settings.TimerStopTime != value) {
                    this.settings.TimerStopTime = value;
                    this.on_PropertyChanged();
                    this.SetTimer();
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

        public VRemote4.HandlerSi.Port GamePort;
        public GamePool._Modules.Timer Timer;
        public Gewinner Gewinner;
        public Sampler Sampler;
        public MediaPlayer MediaPlayer;

        private Settings.Business settings;

        #endregion


        #region Funktionen

        public Business(
            SynchronizationContext syncContext,
            Settings.Business settings,
            VRemote4.HandlerSi.Client.Business client,
            int pipeIndex)
            : base(syncContext, client, pipeIndex, sceneID) {
            this.init();
            this.settings = settings;
        }

        private void init() {
            this.GamePort = this.addPort("GameLayer");
            this.Timer = new GamePool._Modules.Timer(syncContext, this.addPort("TimerLayer"), Modes.Static);
            this.Timer.Alarm1Fired += this.timer_Alarm1Fired;
            this.Timer.Alarm2Fired += this.timer_Alarm2Fired;
            this.Timer.StopFired += this.timer_StopFired;
            this.Timer.PropertyChanged += this.timer_PropertyChanged;
            this.Gewinner = new Gewinner(syncContext, this.addPort("GewinnerLayer"), Modes.Static);
            this.Sampler = new Sampler(syncContext, this.addPort("SamplerLayer"), Modes.Static);
            this.MediaPlayer = new MediaPlayer(syncContext, this.addPort("MediaPlayerLayer"), Modes.Static);
        }

        public void SetPreview() { this.SetDataItemTrigger(".SetPreview"); }

        public void SafeAreaSetOut() { this.SetDataItemTrigger(".SafeArea.SetOut"); }
        public void SafeAreaToOut() { this.SetDataItemTrigger(".SafeArea.ToOut"); }
        public void SafeAreaSetIn() { this.SetDataItemTrigger(".SafeArea.SetIn"); }
        public void SafeAreaToIn() { this.SetDataItemTrigger(".SafeArea.ToIn"); }
        private void setSaveArea(
            bool isVisible) {
            if (!this.safeAreaIsVisible.HasValue ||
                this.safeAreaIsVisible.Value != isVisible) {
                this.safeAreaIsVisible = isVisible;
                this.on_PropertyChanged("SafeAreaIsVisible");
            }
        }

        public void WelcomeSetOut() { this.SetDataItemTrigger(".Welcome.SetOut"); }
        public void WelcomeToOut() { this.SetDataItemTrigger(".Welcome.ToOut"); }
        public void WelcomeSetIn() { this.SetDataItemTrigger(".Welcome.SetIn"); }
        public void WelcomeToIn() { this.SetDataItemTrigger(".Welcome.ToIn"); }
        private void setWelcome(
            bool isVisible) {
            if (!this.welcomeIsVisible.HasValue ||
                this.welcomeIsVisible.Value != isVisible) {
                this.welcomeIsVisible = isVisible;
                this.on_PropertyChanged("WelcomeIsVisible");
            }
        }

        public void CornerLogoSetOut() { this.SetDataItemTrigger(".CornerLogo.SetOut"); }
        public void CornerLogoToOut() { this.SetDataItemTrigger(".CornerLogo.ToOut"); }
        public void CornerLogoSetIn() { this.SetDataItemTrigger(".CornerLogo.SetIn"); }
        public void CornerLogoToIn() { this.SetDataItemTrigger(".CornerLogo.ToIn"); }
        private void setCornerLogo(
            bool isVisible) {
            if (!this.cornerLogoIsVisible.HasValue ||
                this.cornerLogoIsVisible.Value != isVisible) {
                this.cornerLogoIsVisible = isVisible;
                this.on_PropertyChanged("CornerLogoIsVisible");
            }
        }

        public void BackgroundSetOut() { this.SetDataItemTrigger(".Background.SetOut"); }
        public void BackgroundToOut() { this.SetDataItemTrigger(".Background.ToOut"); }
        public void BackgroundSetIn() { this.SetDataItemTrigger(".Background.SetIn"); }
        public void BackgroundToIn() { this.SetDataItemTrigger(".Background.ToIn"); }
        private void setBackground(
            bool isVisible) {
            if (!this.backgroundIsVisible.HasValue ||
                this.backgroundIsVisible.Value != isVisible) {
                this.backgroundIsVisible = isVisible;
                this.on_PropertyChanged("BackgroundIsVisible");
            }
        }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".SafeArea.IsVisibleChanged") { this.setSaveArea(Convert.ToBoolean(e.Value)); }
                else if (e.Path == ".Welcome.IsVisibleChanged") { this.setWelcome(Convert.ToBoolean(e.Value)); }
                else if (e.Path == ".CornerLogo.IsVisibleChanged") { this.setCornerLogo(Convert.ToBoolean(e.Value)); }
                else if (e.Path == ".Background.IsVisibleChanged") { this.setBackground(Convert.ToBoolean(e.Value)); }
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".SafeArea.IsVisible") { this.setSaveArea(Convert.ToBoolean(e.Value)); }
                else if (e.Path == ".Welcome.IsVisible") { this.setWelcome(Convert.ToBoolean(e.Value)); }
                else if (e.Path == ".CornerLogo.IsVisible") { this.setCornerLogo(Convert.ToBoolean(e.Value)); }
                else if (e.Path == ".Background.IsVisible") { this.setBackground(Convert.ToBoolean(e.Value)); }
            }
        }

        public void TimerIn() {
            this.SetTimer();
            this.ResetTimer();
            this.Timer.ToIn();
        }
        public void SetTimer() {
            this.Timer.SetPositionX(this.TimerPositionX);
            this.Timer.SetPositionY(this.TimerPositionY);
            this.Timer.SetStyle(this.TimerStyle);
            this.Timer.SetScaling(100);
            this.Timer.SetStartTime(this.TimerStartTime);
            this.Timer.SetStopTime(this.TimerStopTime);
            this.Timer.SetAlarmTime1(this.TimerAlarmTime1);
            this.Timer.SetAlarmTime2(this.TimerAlarmTime2);
        }
        public void StartTimer() { this.Timer.StartTimer(); }
        public void StopTimer() { this.Timer.StopTimer(); }
        public void ContinueTimer() { this.Timer.ContinueTimer(); }
        public void ResetTimer() { this.Timer.ResetTimer(); }
        public void TimerOut() { this.Timer.ToOut(); }

        public void Clear() {
            this.SetDataItemTrigger(".Clear");
            this.TimerOut();
            this.Gewinner.Reset();
            this.MediaPlayer.Clear();
        }

        public void Reset() {
            this.SetDataItemTrigger(".Reset");
            this.TimerOut();
            this.Gewinner.Reset();
            this.MediaPlayer.Clear();
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
                if (e.PropertyName == "CurrentTime") this.TimerCurrentTime = this.Timer.CurrentTime;
                else if (e.PropertyName == "IsRunning") this.TimerIsRunning = this.Timer.IsRunning;
            }
        }

        #endregion

    }

}
