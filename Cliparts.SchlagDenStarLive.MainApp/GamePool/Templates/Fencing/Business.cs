using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cliparts.Tools.AllstarFMA11;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.Fencing;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.Fencing {

    public class Business : _Base.Business {

        #region Properties

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

        private int scorePositionX = 0;
        public int ScorePositionX {
            get { return this.scorePositionX; }
            set {
                if (this.scorePositionX != value) {
                    this.scorePositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetScore();
                }
            }
        }

        private int scorePositionY = 0;
        public int ScorePositionY {
            get { return this.scorePositionY; }
            set {
                if (this.scorePositionY != value) {
                    this.scorePositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetScore();
                }
            }
        }

        private AllstarFMA11 allstarFMA11_hlp;
        private AllstarFMA11 allstarFMA11 {
            get {
                if (this.allstarFMA11_hlp == null) this.allstarFMA11_hlp = new AllstarFMA11();
                return this.allstarFMA11_hlp;
            }
        }

        private bool fma11IsConnected = false;
        public bool FMA11IsConnected {
            get { return this.allstarFMA11.Connected; }
            private set {
                if (this.fma11IsConnected != value) {
                    this.fma11IsConnected = value;
                    this.on_PropertyChanged();
                }
            }
        }

        public string FMA11SerialPortName {
            get { return this.allstarFMA11.SerialPortName; }
            set { this.allstarFMA11.SerialPortName = value; }
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
        public Business(string typeIdentifier) : base(typeIdentifier) {
            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.Fencing'", typeIdentifier);
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

            this.allstarFMA11.ContentChanged += this.allstarFMA11_ContentChanged;
            this.allstarFMA11.LogChanged += this.allstarFMA11_LogChanged;
            this.allstarFMA11.OpenSerialPort();

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);

            this.setFMA11Properties();
        }

        public override void Dispose() {
            base.Dispose();

            this.allstarFMA11.ContentChanged -= this.allstarFMA11_ContentChanged;
            this.allstarFMA11.LogChanged -= this.allstarFMA11_LogChanged;

            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
        }

        public override void ResetData() {
            base.ResetData();
            this.LeftPlayerScore = 0;
            this.RightPlayerScore = 0;
        }

        public void ShowFMA11Form() {
            this.allstarFMA11.ShowForm();
        }

        private void setFMA11Properties() {
            this.FMA11IsConnected = this.allstarFMA11.Connected;
            int score;
            if (int.TryParse(this.allstarFMA11.PointsLeft, out score)) this.LeftPlayerScore = score;
            if (int.TryParse(this.allstarFMA11.PointsRight, out score)) this.RightPlayerScore = score;
            this.Vinsert_SetScore();
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }
        public void Vinsert_ScoreIn() {
            this.Vinsert_SetScore();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.ToIn();
        }
        public void Vinsert_SetScore() {
            this.Vinsert_SetLeftLamp(this.allstarFMA11.RedIndicatorOn);
            this.Vinsert_SetRightLamp(this.allstarFMA11.GreenIndicatorOn);
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.SetPositionX(this.ScorePositionX);
                this.insertScene.SetPositionY(this.ScorePositionY);
                this.insertScene.SetLeftName(this.LeftPlayerName);
                this.insertScene.SetLeftScore(this.LeftPlayerScore);
                this.insertScene.SetRightName(this.RightPlayerName);
                this.insertScene.SetRightScore(this.RightPlayerScore);
            }
        }
        public void Vinsert_SetLeftLamp(
            bool on) {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                if (on) this.insertScene.SetLeftOn();
                else this.insertScene.SetLeftOff();
            }
        }
        public void Vinsert_SetRightLamp(
            bool on) {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                if (on) this.insertScene.SetRightOn();
                else this.insertScene.SetRightOff();
            }
        }
        public void Vinsert_ScoreOut() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.ToOut();
        }
        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void ClearGraphic() {
            base.ClearGraphic();
            this.insertScene.Clear();
        }

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
        #endregion

        #region Events.Incoming

        private void allstarFMA11_ContentChanged() {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_allstarFMA11_ContentChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, (object)this.allstarFMA11);
        }
        private void sync_allstarFMA11_ContentChanged(object content) {
            this.setFMA11Properties();
        }

        void allstarFMA11_LogChanged(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_allstarFMA11_LogChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, sender);
        }
        private void sync_allstarFMA11_LogChanged(object content) {
            this.on_PropertyChanged();
        }


        #endregion

    }
}
