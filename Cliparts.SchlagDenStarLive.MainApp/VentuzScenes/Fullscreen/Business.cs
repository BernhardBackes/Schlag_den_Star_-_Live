using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.Fullscreen {

    public enum LoopStates {
        Idle,
        Loaded,
        Playing
    }

    public class Business : VRemote4.HandlerSi.Scene, INotifyPropertyChanged {

        #region Properties

        private const string sceneID = "project/fullscreen";

        private bool? backloopIsLoaded = null;
        public bool BackloopIsLoaded {
            get {
                if (this.backloopIsLoaded.HasValue) return this.backloopIsLoaded.Value;
                else return false;
            }
        }

        private bool? backloopIsPlaying = null;
        public bool BackloopIsPlaying {
            get {
                if (this.backloopIsPlaying.HasValue) return this.backloopIsPlaying.Value;
                else return false;
            }
        }

        private LoopStates backloopStatus = LoopStates.Idle;
        public LoopStates BackloopStatus {
            get { return this.backloopStatus; }
            set {
                if (this.backloopStatus != value) {
                    this.backloopStatus = value;
                    this.on_PropertyChanged();
                }
            }
        }

        public VRemote4.HandlerSi.Port GamePort;
        public MediaPlayer MediaPlayer;
        public Gameboard Gameboard;
        public Clock Timer;
        public Score Score;
        public Piechart Piechart;
        public Freetext Freetext;

        #endregion


        #region Funktionen

        public Business(
            SynchronizationContext syncContext,
            VRemote4.HandlerSi.Client.Business client,
            int pipeIndex)
            : base(syncContext, client, pipeIndex, sceneID) {
            this.init();
        }

        private void init() {
            this.GamePort = this.addPort("GameLayer");
            this.MediaPlayer = new MediaPlayer(syncContext, this.addPort("MediaPlayerLayer"), Modes.Static);
            this.Gameboard = new Gameboard(syncContext, this.addPort("GameboardLayer"), Modes.Static);
            this.Timer = new Clock(syncContext, this.addPort("ClockLayer"), Modes.Static);
            this.Score = new Score(syncContext, this.addPort("ScoreLayer"), Modes.Static);
            this.Piechart = new Piechart(syncContext, this.addPort("PiechartLayer"), Modes.Static);
            this.Freetext = new Freetext(syncContext, this.addPort("FreetextLayer"), Modes.Static);
        }

        public void RestartBackloop() { this.SetDataItemTrigger(".Backloop.Restart"); }
        private void setBackloopIsLoaded(
            bool isTrue) {
            if (!this.backloopIsLoaded.HasValue ||
            this.backloopIsLoaded.Value != isTrue) {
                this.backloopIsLoaded = isTrue;
                this.on_PropertyChanged("BackloopIsLoaded");
                this.setBackloopStatus(this.BackloopIsLoaded, this.BackloopIsPlaying);
            }
        }
        private void setBackloopIsPlaying(
            bool isTrue) {
            if (!this.backloopIsPlaying.HasValue ||
            this.backloopIsPlaying.Value != isTrue) {
                this.backloopIsPlaying = isTrue;
                this.on_PropertyChanged("BackloopIsPlaying");
                this.setBackloopStatus(this.BackloopIsLoaded, this.BackloopIsPlaying);
            }
        }
        private void setBackloopStatus(
            bool backloopIsLoaded,
            bool backloopIsPlaying) {
            LoopStates status = LoopStates.Idle;
            if (backloopIsLoaded) {
                if (backloopIsPlaying) status = LoopStates.Playing;
                else status = LoopStates.Loaded;
            }
            this.BackloopStatus = status;
        }

        public void FadeBackloopIn() { this.SetDataItemTrigger(".Backloop.FadeIn"); }
        public void FadeBackloopOut() { this.SetDataItemTrigger(".Backloop.FadeOut"); }

        public void ShowGameboard() {
            this.Timer.ResetTimer();
            this.Gameboard.SetToShow();
            this.SetDataItemTrigger(".ShowGameboard");
        }

        public void ShowGame() {
            this.Timer.ResetTimer();
            this.SetDataItemTrigger(".ShowGame"); 
        }

        public void ShowTimer() {
            this.Timer.ResetTimer();
            this.SetDataItemTrigger(".ShowClock");
        }

        public void ShowScore() {
            this.Timer.ResetTimer();
            this.SetDataItemTrigger(".ShowScore");
        }

        public void ShowPiechart() {
            this.Timer.ResetTimer();
            this.SetDataItemTrigger(".ShowPiechart");
        }

        public void ShowVoting() {
            this.Timer.ResetTimer();
            this.SetDataItemTrigger(".ShowVoting");
        }

        public void ShowFreetext() {
            this.Timer.ResetTimer();
            this.SetDataItemTrigger(".ShowFreetext");
        }

        public void Clear() {
            this.Timer.ResetTimer();
            this.ShowGameboard();
            this.MediaPlayer.Clear();
        }

        protected override void dataItem_TriggerReceived(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Backloop.IsLoadedChanged") { this.setBackloopIsLoaded(Convert.ToBoolean(e.Value)); }
                else if (e.Path == ".Backloop.IsPlayingChanged") { this.setBackloopIsPlaying(Convert.ToBoolean(e.Value)); }
            }
        }

        protected override void dataItem_ValueChanged(object sender, VRemote4.HandlerSi.DataItem.ValueArgs e) {
            if (e is VRemote4.HandlerSi.DataItem.ValueArgs) {
                if (e.Path == ".Backloop.IsLoaded") { this.setBackloopIsLoaded(Convert.ToBoolean(e.Value)); }
                else if (e.Path == ".Backloop.IsPlaying") { this.setBackloopIsPlaying(Convert.ToBoolean(e.Value)); }
            }
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }

}
