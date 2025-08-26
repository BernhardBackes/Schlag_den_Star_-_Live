using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cliparts.Devantech;
using Cliparts.Serialization;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.Timer {

    public class Business : _Base.Business {

        #region Properties

        public _Modules.Timer.Business Vinsert_Timer { get; set; }

        #endregion


        #region Funktionen

        public Business() { }
        public Business(string typeIdentifier) : base(typeIdentifier) {
            this.Vinsert_Timer = new _Modules.Timer.Business();
        }

        public override void Pose(
            SynchronizationContext syncContext,
            MidiHandler.Business midiHandler,
            BuzzerIO.Business buzzerHandler,
            AMB.Business ambHandler,
            Controller devantechHandler,
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

            this.Vinsert_Timer.Alarm1Fired += this.timer_Alarm1Fired;
            this.Vinsert_Timer.Alarm2Fired += this.timer_Alarm2Fired;
            this.Vinsert_Timer.PropertyChanged += this.insertTimer_PropertyChanged;
        }

        public override void Dispose() {
            base.Dispose();
            this.Vinsert_Timer.Alarm1Fired -= this.timer_Alarm1Fired;
            this.Vinsert_Timer.Alarm2Fired -= this.timer_Alarm2Fired;
            this.Vinsert_Timer.PropertyChanged -= this.insertTimer_PropertyChanged;
            this.Vinsert_Timer.Dispose();
        }

        public override void ResetData() {
            base.ResetData();
            this.Vinsert_Timer.RunExtraTime = false;
        }

        public override void Vfullscreen_SetTimer() {
            if (this.fullscreenMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                switch (this.Vinsert_Timer.Style) {
                    case VentuzScenes.GamePool._Modules.Timer.Styles.Sec:
                        this.fullscreenMasterScene.Timer.SetStyle(VentuzScenes.Fullscreen.Clock.Styles.Sec);
                        break;
                    case VentuzScenes.GamePool._Modules.Timer.Styles.MinSec:
                    default:
                        this.fullscreenMasterScene.Timer.SetStyle(VentuzScenes.Fullscreen.Clock.Styles.MinSec);
                        break;
                }
                if (this.Vinsert_Timer.RunExtraTime) this.fullscreenMasterScene.Timer.SetStartTime(this.Vinsert_Timer.ExtraTime);
                else this.fullscreenMasterScene.Timer.SetStartTime(this.Vinsert_Timer.StartTime);
                this.fullscreenMasterScene.Timer.SetStopTime(this.Vinsert_Timer.StopTime);
                this.fullscreenMasterScene.Timer.SetAlarmTime1(-1);
                this.fullscreenMasterScene.Timer.SetAlarmTime2(-1);
            }
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming

        protected void timer_Alarm1Fired(object sender, EventArgs e) {
            //this.on_TimerAlarm1Fired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_Alarm1Fired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_Alarm1Fired(object content) {
        }

        protected void timer_Alarm2Fired(object sender, EventArgs e) {
            //this.on_TimerAlarm2Fired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_Alarm2Fired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_Alarm2Fired(object content) {
        }

        protected void timer_StopFired(object sender, EventArgs e) {
            //this.on_TimerStopFired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_StopFired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_StopFired(object content) {
        }

        protected void insertTimer_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_insertTimer_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_insertTimer_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "Style") this.Vfullscreen_SetTimer();
            }
        }

        #endregion

    }
}
