using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cliparts.Serialization;

using Cliparts.IOnet.IOUnit.IONbase;
using Cliparts.IOnet.IOUnit.IONbuz;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimeToBeatCounterOfScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimeToBeatCounterOfScore
{

    public class Business : _Base.Score.Business {

        #region Properties

        private int timeToBeatPositionX = 0;
        public int TimeToBeatPositionX {
            get { return this.timeToBeatPositionX; }
            set {
                if (this.timeToBeatPositionX != value) {
                    this.timeToBeatPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeToBeat();
                }
            }
        }

        private int timeToBeatPositionY = 0;
        public int TimeToBeatPositionY {
            get { return this.timeToBeatPositionY; }
            set {
                if (this.timeToBeatPositionY != value) {
                    this.timeToBeatPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeToBeat();
                }
            }
        }

        private int timeToBeatStopTime = 5999;
        public int TimeToBeatStopTime {
            get { return this.timeToBeatStopTime; }
            set {
                if (this.timeToBeatStopTime != value) {
                    this.timeToBeatStopTime = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeToBeat();
                }
            }
        }

        private int timeToBeatSentenceTime = 0;
        public int TimeToBeatSentenceTime {
            get { return this.timeToBeatSentenceTime; }
            set {
                if (this.timeToBeatSentenceTime != value) {
                    this.timeToBeatSentenceTime = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeToBeatSentenceTime();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.TimeToBeat.Styles timeToBeatStyle = VentuzScenes.GamePool._Modules.TimeToBeat.Styles.StopwatchName;
        public VentuzScenes.GamePool._Modules.TimeToBeat.Styles TimeToBeatStyle {
            get { return this.timeToBeatStyle; }
            set {
                if (this.timeToBeatStyle != value) {
                    this.timeToBeatStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeToBeat();
                }
            }
        }

        private double timeToBeatCurrentTime = -1;
        public double TimeToBeatCurrentTime {
            get { return this.timeToBeatCurrentTime; }
            protected set {
                if (this.timeToBeatCurrentTime != value) {
                    this.timeToBeatCurrentTime = value;
                    this.on_PropertyChanged();
                }
            }
        }
        
        private double? timeToBeat = null;
        [NotSerialized]
        public string TimeToBeat {
            get {
                if (this.timeToBeat.HasValue) return Helper.convertDoubleToStopwatchTimeText(this.timeToBeat.Value, false, true).Replace(".", ",");
                else return string.Empty; 
            }
            set {
                if (this.TimeToBeat != value) {
                    double result;
                    if (string.IsNullOrEmpty(value) ||
                        !double.TryParse(value, out result)) this.timeToBeat = null;
                    else this.timeToBeat = result;
                    this.on_PropertyChanged();
                }
            }
        }

        private int sentenceTime = 10;
        public int SentenceTime {
            get { return this.sentenceTime; }
            set {
                if (this.sentenceTime != value) {
                    this.sentenceTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int counterPositionX = 0;
        public int CounterPositionX
        {
            get { return this.counterPositionX; }
            set
            {
                if (this.counterPositionX != value)
                {
                    this.counterPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounter();
                }
            }
        }

        private int counterPositionY = 0;
        public int CounterPositionY
        {
            get { return this.counterPositionY; }
            set
            {
                if (this.counterPositionY != value)
                {
                    this.counterPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounter();
                }
            }
        }

        private Content.Gameboard.PlayerSelection selectedPlayer = Content.Gameboard.PlayerSelection.LeftPlayer;
        public Content.Gameboard.PlayerSelection SelectedPlayer
        {
            get { return this.selectedPlayer; }
            set
            {
                if (this.selectedPlayer != value)
                {
                    if (value == Content.Gameboard.PlayerSelection.NotSelected) value = Content.Gameboard.PlayerSelection.LeftPlayer;
                    this.selectedPlayer = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int target = 3;
        public int Target
        {
            get { return this.target; }
            set
            {
                if (this.target != value)
                {
                    if (target < 1) this.target = 1;
                    else this.target = value;
                    this.on_PropertyChanged();
                    this.Counter = this.Counter;
                }
            }
        }

        private int counter = 0;
        public int Counter
        {
            get { return this.counter; }
            set
            {
                if (this.counter != value)
                {
                    if (value < 0) this.counter = 0;
                    else if (value > this.Target) this.counter = this.Target;
                    else this.counter = value;
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

        private bool timeIsRunning = false;

        #endregion


        #region Funktionen

        public Business() { }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.TimeToBeatCounterOfScore'", typeIdentifier);
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
            this.insertScene.TimeToBeat.StopFired += this.timeToBeat_StopFired;
            this.insertScene.TimeToBeat.PropertyChanged += this.timeToBeat_PropertyChanged;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);
        }

        public override void Dispose() {
            base.Dispose();

            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.TimeToBeat.StopFired -= this.timeToBeat_StopFired;
            this.insertScene.TimeToBeat.PropertyChanged -= this.timeToBeat_PropertyChanged;
        }

        public override void ResetData() {
            base.ResetData();
            this.TimeToBeat = null;
            this.timeIsRunning = false;
            this.TimeToBeatSentenceTime = 0;
            this.Counter = 0;
            this.Vfullscreen_SetCounter();
            this.Vinsert_SetCounter();
        }

        public override void Next() {
            this.TimeToBeat = null;
            this.timeIsRunning = false;
            this.TimeToBeatSentenceTime = 0;
            this.Counter = 0;
            this.Vfullscreen_SetCounter();
            this.Vinsert_SetCounter();
        }

        public void NextPlayer() {
            if (this.SelectedPlayer == Content.Gameboard.PlayerSelection.LeftPlayer) this.SelectedPlayer = Content.Gameboard.PlayerSelection.RightPlayer;
            else this.SelectedPlayer = Content.Gameboard.PlayerSelection.LeftPlayer;
            this.TimeToBeatSentenceTime = 0;
            this.Counter = 0;
            this.Vfullscreen_SetCounter();
            this.Vinsert_SetCounter();
        }

        public void AddCounter(
            int value)
        {
            this.Counter += value;
            if (this.Counter >= this.Target)
            {
                this.Vinsert_StopTimeToBeat();
                this.Vinsert_PlaySound_Stop();
            }
            this.Vfullscreen_SetCounter();
            this.Vinsert_SetCounter();
        }

        public void ResetCounter()
        {
            this.Counter = 0;
            this.Vfullscreen_SetCounter();
            this.Vinsert_SetCounter();
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }

        public override void Vinsert_ScoreIn() {
            this.Vinsert_SetScore();
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Score.ToIn();
        }
        public override void Vinsert_SetScore() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
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
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Score.ToOut();
        }

        public void Vinsert_TimeToBeatIn() {
            this.Vinsert_SetTimeToBeat();
            this.Vinsert_ResetTimeToBeat();
            this.Vinsert_ResetOffsetTime();
            this.Vinsert_ResetTimeToBeatTime();
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.TimeToBeat.ToIn();
        }
        public void Vinsert_SetTimeToBeat() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                this.insertScene.TimeToBeat.SetPositionX(this.TimeToBeatPositionX);
                this.insertScene.TimeToBeat.SetPositionY(this.TimeToBeatPositionY);
                this.insertScene.TimeToBeat.SetStopTime(this.TimeToBeatStopTime);
                this.insertScene.TimeToBeat.SetSentenceTime(this.TimeToBeatSentenceTime);
                this.insertScene.TimeToBeat.SetStyle(this.TimeToBeatStyle);
                if (this.SelectedPlayer == Content.Gameboard.PlayerSelection.LeftPlayer) this.insertScene.TimeToBeat.SetName(this.LeftPlayerName);
                else this.insertScene.TimeToBeat.SetName(this.RightPlayerName);
            }
        }
        public void Vinsert_SetTimeToBeatSentenceTime() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.TimeToBeat.SetSentenceTime(this.TimeToBeatSentenceTime);
        }
        public void Vinsert_StartTimeToBeat() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                this.insertScene.TimeToBeat.StartTimer();
                this.timeIsRunning = true;
            }
        }
        public void Vinsert_ShowOffsetTime() { }
        public void Vinsert_ShowOffsetTime(
            float offset) {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                this.insertScene.TimeToBeat.SetOffset(offset);
                this.insertScene.TimeToBeat.ShowOffset();
            }
        }
        public void Vinsert_ResetOffsetTime() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.TimeToBeat.ResetOffset(); }
        public void Vinsert_ShowTimeToBeatTime() {
            if (this.timeToBeat.HasValue) this.Vinsert_ShowTimeToBeatTime(Convert.ToSingle(this.timeToBeat.Value));
        }
        public void Vinsert_ShowTimeToBeatTime(
            float timeToBeat) {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                this.insertScene.TimeToBeat.SetTimeToBeat(timeToBeat);
                this.insertScene.TimeToBeat.ShowTimeToBeat();
            }
        }
        public void Vinsert_ResetTimeToBeatTime() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.TimeToBeat.ResetTimeToBeat(); }
        public void Vinsert_StopTimeToBeat() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.TimeToBeat.StopTimer(); }
        public void Vinsert_ContinueTimeToBeat() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.TimeToBeat.ContinueTimer(); }
        public void Vinsert_ResetTimeToBeat() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.TimeToBeat.ResetTimer(); }
        public void Vinsert_TimeToBeatOut() { 
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.TimeToBeat.ToOut();
            this.Vinsert_ResetOffsetTime();
            this.Vinsert_ResetTimeToBeatTime();
        }

        public void Vinsert_CounterIn()
        {
            if (this.insertScene is VRemote4.HandlerSi.Scene)
            {
                this.Vinsert_SetCounter(this.insertScene.SingleCounterOf);
                this.insertScene.SingleCounterOf.ToIn();
            }
        }
        public void Vinsert_SetCounter() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetCounter(this.insertScene.SingleCounterOf); }
        public void Vinsert_SetCounter(VentuzScenes.GamePool._Modules.SingleCounterOf scene) { this.Vinsert_SetCounter(scene, this.Target, this.Counter); }
        public void Vinsert_SetCounter(
            VentuzScenes.GamePool._Modules.SingleCounterOf scene,
            int target,
            int counter)
        {
            if (scene is VentuzScenes.GamePool._Modules.SingleCounterOf)
            {
                scene.SetPositionX(this.CounterPositionX);
                scene.SetPositionY(this.CounterPositionY);
                scene.SetStyle(VentuzScenes.GamePool._Modules.SingleCounterOf.StyleElements.Untitled);
                scene.SetTarget(target);
                scene.SetCounter(counter);
            }
        }
        public void Vinsert_CounterOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.SingleCounterOf.ToOut(); }

        public void Vinsert_PlaySound_Stop() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.PlaySound_Stop(); }

        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        internal void Vfullscreen_SetCounter()
        {
            this.fullscreenMasterScene.Freetext.SetTextColor(Color.Black);
            if (this.Target >= 10 &&
                this.Counter < 10) 
                this.fullscreenMasterScene.Freetext.SetTextValue($" {this.Counter}/{this.Target}");
            else
                this.fullscreenMasterScene.Freetext.SetTextValue($"{this.Counter}/{this.Target}");
        }

        public override void ClearGraphic() {
            base.ClearGraphic();
            this.insertScene.Clear();
        }

        #endregion


        #region Events.Outgoing

        public event EventHandler TimeToBeatStopFired;
        protected void on_TimeToBeatStopFired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.TimeToBeatStopFired, e); }

        #endregion

        #region Events.Incoming

        protected void timeToBeat_StopFired(object sender, EventArgs e) {
            this.on_TimeToBeatStopFired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timeToBeat_StopFired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timeToBeat_StopFired(object content) {
            if (this.timeIsRunning) {
                if (this.timeToBeat.HasValue) {
                    // zweiter Durchgang, der Offset wird ermittelt
                    double currentTime = this.insertScene.TimeToBeat.CurrentTime;
                    double offset = currentTime - this.timeToBeat.Value;
                    this.Vinsert_ShowOffsetTime(Convert.ToSingle(offset));
                }
                else {
                    // erster Durchgang, die TimeToBeat wird ermittelt
                    if (this.insertScene.TimeToBeat.CurrentTime > 0) {
                        this.timeToBeat = this.insertScene.TimeToBeat.CurrentTime;
                        this.on_PropertyChanged("TimeToBeat");
                    }
                }
            }
            this.timeIsRunning = false;
        }

        protected void timeToBeat_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timeToBeat_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timeToBeat_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "CurrentTime") this.TimeToBeatCurrentTime = Convert.ToDouble(this.insertScene.TimeToBeat.CurrentTime);
            }
        }

        #endregion

    }
}
