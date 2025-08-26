using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

using Cliparts.Serialization;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Modules.Timer {
    public class Business : INotifyPropertyChanged {

        #region Properties

        protected SynchronizationContext syncContext;

        private int positionX = 0;
        public int PositionX {
            get { return this.positionX; }
            set {
                if (this.positionX != value) {
                    this.positionX = value;
                    this.on_PropertyChanged();
                    this.Set();
                }
            }
        }

        private int positionY = 0;
        public int PositionY {
            get { return this.positionY; }
            set {
                if (this.positionY != value) {
                    this.positionY = value;
                    this.on_PropertyChanged();
                    this.Set();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.Timer.Styles style = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
        public VentuzScenes.GamePool._Modules.Timer.Styles Style {
            get { return this.style; }
            set {
                if (this.style != value) {
                    this.style = value;
                    this.on_PropertyChanged();
                    this.Set();
                }
            }
        }

        private double scaling = 100;
        public double Scaling {
            get { return this.scaling; }
            set {
                if (this.scaling != value) {
                    this.scaling = value;
                    this.on_PropertyChanged();
                    this.Set();
                }
            }
        }

        private int startTime = 240;
        public int StartTime {
            get { return this.startTime; }
            set {
                if (this.startTime != value) {
                    this.startTime = value;
                    this.on_PropertyChanged();
                    this.Set();
                }
            }
        }

        private int extraTime = 60;
        public int ExtraTime {
            get { return this.extraTime; }
            set {
                if (this.extraTime != value) {
                    this.extraTime = value;
                    this.on_PropertyChanged();
                    this.Set();
                }
            }
        }

        private int alarmTime1 = -1;
        public int AlarmTime1 {
            get { return this.alarmTime1; }
            set {
                if (this.alarmTime1 != value) {
                    this.alarmTime1 = value;
                    this.on_PropertyChanged();
                    this.Set();
                }
            }
        }

        private int alarmTime2 = -1;
        public int AlarmTime2 {
            get { return this.alarmTime2; }
            set {
                if (this.alarmTime2 != value) {
                    this.alarmTime2 = value;
                    this.on_PropertyChanged();
                    this.Set();
                }
            }
        }

        private int stopTime = 0;
        public int StopTime {
            get { return this.stopTime; }
            set {
                if (this.stopTime != value) {
                    this.stopTime = value;
                    this.on_PropertyChanged();
                    this.Set();
                }
            }
        }

        private bool runExtraTime = false;
        [NotSerialized]
        public bool RunExtraTime {
            get { return this.runExtraTime; }
            set {
                if (this.runExtraTime != value) {
                    this.runExtraTime = value;
                    this.on_PropertyChanged();
                    this.Set();
                }
            }
        }

        private int currentTime = -1;
        public int CurrentTime {
            get { return this.currentTime; }
            protected set {
                if (this.currentTime != value) {
                    this.currentTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool isRunning = false;
        public bool IsRunning {
            get { return this.isRunning; }
            protected set {
                if (this.isRunning != value) {
                    this.isRunning = value;
                    this.on_PropertyChanged();
                }
            }
        }

        VentuzScenes.GamePool._Modules.Timer scene;

        #endregion


        #region Funktionen

        public Business() { }

        public void Pose(
            SynchronizationContext syncContext,
            VentuzScenes.GamePool._Modules.Timer scene) {
            this.syncContext = syncContext;
            this.scene = scene;
            this.scene.PropertyChanged += this.scene_PropertyChanged;
            this.scene.Alarm1Fired += this.on_Alarm1Fired;
            this.scene.Alarm2Fired += this.on_Alarm2Fired;
            this.scene.StopFired += this.on_StopFired;
        }

        public void Dispose() {
            this.scene.PropertyChanged -= this.scene_PropertyChanged;
            this.scene.Alarm1Fired -= this.on_Alarm1Fired;
            this.scene.Alarm2Fired -= this.on_Alarm2Fired;
            this.scene.StopFired -= this.on_StopFired;
        }

        public void In() { this.In(this.scene); }
        public void In(VentuzScenes.GamePool._Modules.Timer scene) {
            if (scene is VentuzScenes.GamePool._Modules.Timer) {
                this.Set(scene);
                this.Reset(scene);
                scene.ToIn();
            }
        }
        public void Set() { this.Set(this.scene); }
        public void Set(VentuzScenes.GamePool._Modules.Timer scene) {
            if (this.RunExtraTime) this.Set(scene, this.ExtraTime);
            else this.Set(scene, this.StartTime);
        }
        public void Set(
            VentuzScenes.GamePool._Modules.Timer scene,
            int startTime) {
            if (scene is VentuzScenes.GamePool._Modules.Timer) {
                scene.SetPositionX(this.PositionX);
                scene.SetPositionY(this.PositionY);
                scene.SetStyle(this.Style);
                scene.SetScaling(this.Scaling);
                scene.SetStartTime(startTime);
                scene.SetStopTime(this.StopTime);
                scene.SetAlarmTime1(this.AlarmTime1);
                scene.SetAlarmTime2(this.AlarmTime2);
            }
        }
        public void Start() { this.Start(this.scene); }
        public void Start(VentuzScenes.GamePool._Modules.Timer scene) { if (scene is VentuzScenes.GamePool._Modules.Timer) scene.StartTimer(); }
        public void Stop() { this.Stop(this.scene); }
        public void Stop(VentuzScenes.GamePool._Modules.Timer scene) { if (scene is VentuzScenes.GamePool._Modules.Timer) scene.StopTimer(); }
        public void Continue() { this.Continue(this.scene); }
        public void Continue(VentuzScenes.GamePool._Modules.Timer scene) { if (scene is VentuzScenes.GamePool._Modules.Timer) scene.ContinueTimer(); }
        public void Reset() { this.Reset(this.scene); }
        public void Reset(VentuzScenes.GamePool._Modules.Timer scene) { if (scene is VentuzScenes.GamePool._Modules.Timer) scene.ResetTimer(); }
        public void Out() { this.Out(this.scene); }
        public void Out(VentuzScenes.GamePool._Modules.Timer scene) { if (scene is VentuzScenes.GamePool._Modules.Timer) scene.ToOut(); }

        #endregion


        #region Events.Outgoing

        public event PropertyChangedEventHandler PropertyChanged;
        protected void on_PropertyChanged([CallerMemberName]string callerName = "") { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }
        protected void on_PropertyChanged(PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, e); }
        protected void on_PropertyChanged(object sender, PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(sender, this.PropertyChanged, e); }

        public event EventHandler Alarm1Fired;
        protected void on_Alarm1Fired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.Alarm1Fired, e); }

        public event EventHandler Alarm2Fired;
        protected void on_Alarm2Fired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.Alarm2Fired, e); }

        public event EventHandler StopFired;
        protected void on_StopFired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.StopFired, e); }

        #endregion

        #region Events.Incoming

        private void scene_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (this.syncContext is SynchronizationContext) {
                SendOrPostCallback callback = new SendOrPostCallback(this.sync_scene_PropertyChanged);
                if (this.syncContext != null) this.syncContext.Post(callback, e);
            }
        }
        protected virtual void sync_scene_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "CurrentTime") this.CurrentTime = this.scene.CurrentTime;
                else if (e.PropertyName == "IsRunning") this.IsRunning = this.scene.IsRunning;
            }
        }

        #endregion


    }
}
