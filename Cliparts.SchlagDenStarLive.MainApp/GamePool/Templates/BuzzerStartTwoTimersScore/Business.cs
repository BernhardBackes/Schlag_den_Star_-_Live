using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;
using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.BuzzerStartTwoTimersScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.BuzzerStartTwoTimersScore
{

    public class Business : _Base.BuzzerStartTimerScore.Business {

        #region Properties

        private int timerPositionX = 0;
        public int RightTimerPositionX
        {
            get { return this.timerPositionX; }
            set
            {
                if (this.timerPositionX != value)
                {
                    this.timerPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_RightSetTimer();
                }
            }
        }

        private int timerPositionY = 0;
        public int RightTimerPositionY
        {
            get { return this.timerPositionY; }
            set
            {
                if (this.timerPositionY != value)
                {
                    this.timerPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_RightSetTimer();
                }
            }
        }

        private int timerCurrentTime = -1;
        public int RightTimerCurrentTime
        {
            get { return this.timerCurrentTime; }
            protected set
            {
                if (this.timerCurrentTime != value)
                {
                    this.timerCurrentTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool timerIsRunning = false;
        public bool RightTimerIsRunning
        {
            get { return this.timerIsRunning; }
            protected set
            {
                if (this.timerIsRunning != value)
                {
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.BuzzerStartTwoTimersScore'", typeIdentifier);
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

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;
            this.insertScene.LeftTimer.Alarm1Fired += this.leftTimer_Alarm1Fired;
            this.insertScene.LeftTimer.Alarm2Fired += this.leftTimer_Alarm2Fired;
            this.insertScene.LeftTimer.StopFired += this.leftTimer_StopFired;
            this.insertScene.LeftTimer.PropertyChanged += this.leftTimer_PropertyChanged;
            this.insertScene.RightTimer.Alarm1Fired += this.rightTimer_Alarm1Fired;
            this.insertScene.RightTimer.Alarm2Fired += this.rightTimer_Alarm2Fired;
            this.insertScene.RightTimer.StopFired += this.rightTimer_StopFired;
            this.insertScene.RightTimer.PropertyChanged += this.rightTimer_PropertyChanged;
        }

        public override void Dispose() {
            base.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.LeftTimer.Alarm1Fired -= this.leftTimer_Alarm1Fired;
            this.insertScene.LeftTimer.Alarm2Fired -= this.leftTimer_Alarm2Fired;
            this.insertScene.LeftTimer.StopFired -= this.leftTimer_StopFired;
            this.insertScene.LeftTimer.PropertyChanged -= this.leftTimer_PropertyChanged;
            this.insertScene.RightTimer.Alarm1Fired -= this.rightTimer_Alarm1Fired;
            this.insertScene.RightTimer.Alarm2Fired -= this.rightTimer_Alarm2Fired;
            this.insertScene.RightTimer.StopFired -= this.rightTimer_StopFired;
            this.insertScene.RightTimer.PropertyChanged -= this.rightTimer_PropertyChanged;
            this.insertScene.Dispose();
        }

        public override void DoBuzzer(
            PlayerSelection buzzeredPlayer)
        {
            if (buzzeredPlayer == PlayerSelection.LeftPlayer)
            {
                if (!this.TimerIsRunning)
                {
                    this.insertMasterScene.Sampler.Play(VentuzScenes.Insert.Sampler.SampleElements.BuzzerLeft);
                    this.Vinsert_TimerIn();
                    this.Vinsert_StartTimer();
                }
            }
            else if (buzzeredPlayer == PlayerSelection.RightPlayer)
            {
                if (!this.RightTimerIsRunning)
                {
                    this.insertMasterScene.Sampler.Play(VentuzScenes.Insert.Sampler.SampleElements.BuzzerRight);
                    this.Vinsert_RightTimerIn();
                    this.Vinsert_RightStartTimer();
                }
            }
            if (this.BuzzeredPlayer == PlayerSelection.NotSelected) this.BuzzeredPlayer = buzzeredPlayer;
            //if (this.BuzzeredPlayer == PlayerSelection.NotSelected &&
            //    this.BuzzeredPlayer != buzzeredPlayer)
            //{
            //    this.BuzzeredPlayer = buzzeredPlayer;
            //    this.Vinsert_Buzzer(buzzeredPlayer);
            //    this.Vinsert_StartTimer();
            //    this.Vfullscreen_StartTimer();
            //}
        }

        public override void ReleaseBuzzer()
        {
            bool[] inputMask = new bool[8];
            if (this.LeftPlayerBuzzerChannel > 0 &&
                this.LeftPlayerBuzzerChannel <= inputMask.Length) inputMask[this.LeftPlayerBuzzerChannel - 1] = true;
            if (this.RightPlayerBuzzerChannel > 0 &&
                this.RightPlayerBuzzerChannel <= inputMask.Length) inputMask[this.RightPlayerBuzzerChannel - 1] = true;
            this.buzzerHandler.SetInputMask(this.IOUnitName, inputMask);
            this.buzzerHandler.ReleaseBuzzer(this.IOUnitName, IOnet.IOUnit.IONbuz.WorkModes.EVENT);
            this.BuzzeredPlayer = PlayerSelection.NotSelected;
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }

        public override void Vinsert_TimerIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_TimerIn(this.insertScene.LeftTimer); }
        public override void Vinsert_SetTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetTimer(this.insertScene.LeftTimer); }
        public override void Vinsert_StartTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.LeftTimer.StartTimer(); }
        public override void Vinsert_StopTimer()
        {
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.LeftTimer.StopTimer();
            this.Vfullscreen_StopTimer();
            this.Vfullscreen_SetTimer(this.TimerCurrentTime);
        }
        public override void Vinsert_ContinueTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.LeftTimer.ContinueTimer(); }
        public override void Vinsert_ResetTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.LeftTimer.ResetTimer(); }
        public override void Vinsert_TimerOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.LeftTimer.ToOut(); }

        public void Vinsert_RightTimerIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_RightTimerIn(this.insertScene.RightTimer); }
        public void Vinsert_RightTimerIn(VentuzScenes.GamePool._Modules.Timer scene)
        {
            if (scene is VentuzScenes.GamePool._Modules.Timer)
            {
                this.Vinsert_RightSetTimer(scene);
                this.Vinsert_ResetTimer(scene);
                scene.ToIn();
            }
        }
        public void Vinsert_RightSetTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_RightSetTimer(this.insertScene.RightTimer); }
        public void Vinsert_RightSetTimer(VentuzScenes.GamePool._Modules.Timer scene)
        {
            if (this.RunExtraTime) this.Vinsert_RightSetTimer(scene, this.TimerExtraTime);
            else this.Vinsert_RightSetTimer(scene, this.TimerStartTime);
        }
        public void Vinsert_RightSetTimer(
            VentuzScenes.GamePool._Modules.Timer scene,
            int startTime)
        {
            if (scene is VentuzScenes.GamePool._Modules.Timer)
            {
                scene.SetPositionX(this.RightTimerPositionX);
                scene.SetPositionY(this.RightTimerPositionY);
                scene.SetStyle(this.TimerStyle);
                scene.SetScaling(100);
                scene.SetStartTime(startTime);
                scene.SetStopTime(this.TimerStopTime);
                scene.SetAlarmTime1(this.TimerAlarmTime1);
                scene.SetAlarmTime2(this.TimerAlarmTime2);
            }
        }

        public void Vinsert_RightStartTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.RightTimer.StartTimer(); }
        public void Vinsert_RightStopTimer()
        {
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.RightTimer.StopTimer();
            this.Vfullscreen_StopTimer();
            this.Vfullscreen_SetTimer(this.TimerCurrentTime);
        }
        public void Vinsert_RightContinueTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.RightTimer.ContinueTimer(); }
        public void Vinsert_RightResetTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.RightTimer.ResetTimer(); }
        public void Vinsert_RightTimerOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.RightTimer.ToOut(); }

        public override void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score); }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }

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

        public event EventHandler RightTimerAlarm1Fired;
        protected void on_RightTimerAlarm1Fired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.RightTimerAlarm1Fired, e); }

        public event EventHandler RightTimerAlarm2Fired;
        protected void on_RightTimerAlarm2Fired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.RightTimerAlarm2Fired, e); }

        public event EventHandler RightTimerStopFired;
        protected void on_RightTimerStopFired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.RightTimerStopFired, e); }

        #endregion

        #region Events.Incoming

        protected override void sync_timer_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "CurrentTime") this.TimerCurrentTime = this.insertScene.LeftTimer.CurrentTime;
                else if (e.PropertyName == "IsRunning") this.TimerIsRunning = this.insertScene.LeftTimer.IsRunning;
            }
        }

        protected void rightTimer_Alarm1Fired(object sender, EventArgs e)
        {
            this.on_RightTimerAlarm1Fired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightTimer_Alarm1Fired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected void sync_rightTimer_Alarm1Fired(object content)
        {
        }

        protected void rightTimer_Alarm2Fired(object sender, EventArgs e)
        {
            this.on_RightTimerAlarm2Fired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightTimer_Alarm2Fired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected void sync_rightTimer_Alarm2Fired(object content)
        {
        }

        protected void rightTimer_StopFired(object sender, EventArgs e)
        {
            this.on_RightTimerStopFired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightTimer_StopFired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected void sync_rightTimer_StopFired(object content)
        {
        }

        protected void rightTimer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightTimer_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected void sync_rightTimer_PropertyChanged(object content)
        {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs)
            {
                if (e.PropertyName == "CurrentTime") this.RightTimerCurrentTime = this.insertScene.RightTimer.CurrentTime;
                else if (e.PropertyName == "IsRunning") this.RightTimerIsRunning = this.insertScene.RightTimer.IsRunning;
            }
        }

        #endregion

    }
}
