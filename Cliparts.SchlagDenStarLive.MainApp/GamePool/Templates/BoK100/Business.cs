using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.BoK;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.BoK100 {

    public class Business : _Base.BuzzerScore.Business {

        #region Properties

        //BoK Änderung
        private MidiHandler.Business midiHandler;

        private int taskCounterPositionX = 0;
        public int TaskCounterPositionX {
            get { return this.taskCounterPositionX; }
            set {
                if (this.taskCounterPositionX != value) {
                    this.taskCounterPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTaskCounter();
                }
            }
        }

        private int taskCounterPositionY = 0;
        public int TaskCounterPositionY {
            get { return this.taskCounterPositionY; }
            set {
                if (this.taskCounterPositionY != value) {
                    this.taskCounterPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTaskCounter();
                }
            }
        }

        public const int TaskCounterPenaltyCountMax = 13;

        private int taskCounterSize = 13;
        public int TaskCounterSize {
            get { return this.taskCounterSize; }
            set {
                if (this.taskCounterSize != value) {
                    if (value < 0) value = 0;
                    this.taskCounterSize = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTaskCounter();
                }
            }
        }

        private int taskCounter = 1;
        public int TaskCounter {
            get { return this.taskCounter; }
            set {
                if (this.taskCounter != value) {
                    if (value < 1) value = 1;
                    this.taskCounter = value;
                    this.on_PropertyChanged();
                }
            }
        }

        protected List<BoK.SingleDot> taskCounterPenaltyDots = new List<BoK.SingleDot>();
        public BoK.SingleDot[] TaskCounterPenaltyDots {
            get {
                this.fillPenaltyDots();
                return this.taskCounterPenaltyDots.ToArray();
            }
            set {
                this.fillPenaltyDots();
                for (int i = 0; i < TaskCounterPenaltyCountMax; i++) {
                    if (value is BoK.SingleDot[] &&
                        value.Length > i) this.taskCounterPenaltyDots[i].Clone(value[i]);
                    else this.taskCounterPenaltyDots[i].Reset();
                }
            }
        }


        private bool negativeMode = false;
        public bool NegativeMode {
            get { return this.negativeMode; }
            set {
                if (this.negativeMode != value) {
                    this.negativeMode = value;
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.BoK'", typeIdentifier);
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

            //BoK Änderung
            this.midiHandler = midiHandler;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;
        }

        public override void Dispose() {
            base.Dispose();
            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
        }

        public override void ResetData() {
            base.ResetData();
            this.TaskCounter = 1;
            this.ResetPenaltyDots();
        }

        protected void fillPenaltyDots() {
            while (this.taskCounterPenaltyDots.Count < TaskCounterPenaltyCountMax) {
                BoK.SingleDot item = new BoK.SingleDot();
                item.PropertyChanged += this.dot_PropertyChanged;
                this.taskCounterPenaltyDots.Add(item);
            }
        }

        public bool TryGetDot(
            int index,
            out BoK.SingleDot dot) {
            if (index >= 0 &&
                index < this.TaskCounterPenaltyDots.Length &&
                this.TaskCounterPenaltyDots[index] is BoK.SingleDot) {
                dot = this.TaskCounterPenaltyDots[index];
                return true;
            }
            else {
                dot = null;
                return false;
            }
        }

        public override void Next() {
            BoK.SingleDot dot;
            if (this.BuzzeredPlayer == Content.Gameboard.PlayerSelection.NotSelected &&
                this.TryGetDot(this.TaskCounter -1, out dot)) dot.Status = VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Fail;
            base.Next();
            this.TaskCounter++;
            this.Vinsert_SetTaskCounter();
        }

        public void ResetPenaltyDots() {
            foreach (BoK.SingleDot item in this.taskCounterPenaltyDots) item.Reset();
        }

        protected void clearPenalty() {
            foreach (BoK.SingleDot item in this.taskCounterPenaltyDots) item.PropertyChanged -= this.dot_PropertyChanged;
        }

        public override void DoBuzzer(PlayerSelection buzzeredPlayer) {
            //BoK Änderung
            base.DoBuzzer(buzzeredPlayer);
            switch (buzzeredPlayer) {
                case PlayerSelection.LeftPlayer:
                    this.midiHandler.SendEvent("Links");
                    break;
                case PlayerSelection.RightPlayer:
                    this.midiHandler.SendEvent("Rechts");
                    break;
            }
        }

        public void True() {
            this.Vinsert_StopTimeout();
            BoK.SingleDot dot;
            switch (this.BuzzeredPlayer) {
                case Content.Gameboard.PlayerSelection.LeftPlayer:
                    //BoK Änderung
                    this.LeftPlayerScore += 100;
                    if (this.TryGetDot(this.TaskCounter - 1, out dot)) dot.Status = VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Red;
                    break;
                case Content.Gameboard.PlayerSelection.RightPlayer:
                    //BoK Änderung
                    this.RightPlayerScore += 100;
                    if (this.TryGetDot(this.TaskCounter - 1, out dot)) dot.Status = VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Blue;
                    break;
            }
            if (this.BuzzeredPlayer != Content.Gameboard.PlayerSelection.NotSelected &&
                this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.PlayJingleTrue();
            this.Vinsert_SetScore();
            this.Vinsert_SetTaskCounter();
            //BoK Änderung
            this.midiHandler.SendEvent("Richtig");
            this.GameControl.SetStep(5);
            this.Vstage_SetScore();
        }

        public void False() {
            this.Vinsert_StopTimeout();
            BoK.SingleDot dot;
            switch (this.BuzzeredPlayer) {
                case Content.Gameboard.PlayerSelection.LeftPlayer:
                    //BoK Änderung
                    if (this.negativeMode) this.LeftPlayerScore -= 100;
                    else this.RightPlayerScore += 100;
                    if (this.TryGetDot(this.TaskCounter - 1, out dot)) dot.Status = VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Blue;
                    break;
                case Content.Gameboard.PlayerSelection.RightPlayer:
                    //BoK Änderung
                    if (this.negativeMode) this.RightPlayerScore -= 100;
                    else this.LeftPlayerScore += 100;
                    if (this.TryGetDot(this.TaskCounter - 1, out dot)) dot.Status = VentuzScenes.GamePool._Modules.TaskCounter.DotStates.Red;
                    break;
            }
            if (this.BuzzeredPlayer != Content.Gameboard.PlayerSelection.NotSelected &&
                this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.PlayJingleFalse();
            this.Vinsert_SetScore();
            this.Vinsert_SetTaskCounter();
            //BoK Änderung
            this.midiHandler.SendEvent("Falsch");
            this.GameControl.SetStep(5);
            this.Vstage_SetScore();
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }

        public override void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score, this.LeftPlayerScore, this.RightPlayerScore); }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }

        public override void Vinsert_Buzzer(PlayerSelection buzzeredPlayer) { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_Buzzer(this.insertScene.Timeout, buzzeredPlayer); }
        public new void Vinsert_Buzzer(
            VentuzScenes.GamePool._Modules.Timeout scene,
            PlayerSelection buzzeredPlayer) {
            if (scene is VentuzScenes.GamePool._Modules.Timeout) {
                this.Vinsert_SetTimeout(scene);
                switch (buzzeredPlayer) {
                    case PlayerSelection.LeftPlayer:
                        scene.BuzzerSoundLeft();
                        break;
                    case PlayerSelection.RightPlayer:
                        scene.BuzzerSoundRight();
                        break;
                    case PlayerSelection.NotSelected:
                    default:
                        scene.Stop();
                        break;
                }
            }
        }

        public override void Vinsert_SetTimeout() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetTimeout(this.insertScene.Timeout); }
        public override void Vinsert_StopTimeout() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_StopTimeout(this.insertScene.Timeout); }

        public void Vinsert_TaskCounterIn() {
            this.Vinsert_SetTaskCounter();
            if (this.insertScene is Insert &&
                this.insertScene.TaskCounter is VentuzScenes.GamePool._Modules.TaskCounter) this.insertScene.TaskCounter.ToIn();
        }
        public void Vinsert_SetTaskCounter() { if (this.insertScene is Insert) this.Vinsert_SetTaskCounter(this.insertScene.TaskCounter, this.TaskCounterPenaltyDots, this.TaskCounter); }
        public void Vinsert_SetTaskCounter(
            VentuzScenes.GamePool._Modules.TaskCounter scene,
            BoK.SingleDot[] taskCounterPenaltyDots,
            int counter) {
            if (scene is VentuzScenes.GamePool._Modules.TaskCounter) {
                scene.SetPositionX(this.TaskCounterPositionX);
                scene.SetPositionY(this.TaskCounterPositionY);
                //BoK Änderung
                scene.SetStyle(VentuzScenes.GamePool._Modules.TaskCounter.Styles.Numeric);
                scene.SetSize(this.TaskCounterSize);
                int id = 1;
                foreach (BoK.SingleDot item in taskCounterPenaltyDots) {
                    scene.SetDot(id, item.Status);
                    id++;
                }
                scene.SetCounter(counter);
            }
        }
        public void Vinsert_TaskCounterOut() {
            if (this.insertScene is Insert &&
                this.insertScene.TaskCounter is VentuzScenes.GamePool._Modules.TaskCounter) this.insertScene.TaskCounter.ToOut();
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

        void dot_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            this.on_PropertyChanged(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_dot_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_dot_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                //if (e.PropertyName == "Status" &&
                //    !this.repressCalcPenaltySums) this.calcPenaltySums();
            }
        }

        #endregion

    }
}
