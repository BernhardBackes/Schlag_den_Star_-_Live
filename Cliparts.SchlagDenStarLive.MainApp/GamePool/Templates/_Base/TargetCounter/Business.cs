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


namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.TargetCounter {

    public class Business : _Base.Business {

        #region Properties

        private int startValue = 100;
        public int StartValue {
            get { return this.startValue; }
            set {
                if (this.startValue != value) {
                    if (value < 0) this.startValue = 0;
                    else this.startValue = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int targetValue = 0;
        public int TargetValue {
            get { return this.targetValue; }
            set {
                if (this.targetValue != value) {
                    if (value < 0) this.targetValue = 0;
                    else this.targetValue = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int targetCounterPositionX = 0;
        public int TargetCounterPositionX {
            get { return this.targetCounterPositionX; }
            set {
                if (this.targetCounterPositionX != value) {
                    this.targetCounterPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTargetCounter();
                }
            }
        }

        private int targetCounterPositionY = 0;
        public int TargetCounterPositionY {
            get { return this.targetCounterPositionY; }
            set {
                if (this.targetCounterPositionY != value) {
                    this.targetCounterPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTargetCounter();
                }
            }
        }

        private int leftPlayerCounter = 0;
        public int LeftPlayerCounter {
            get { return this.leftPlayerCounter; }
            set {
                if (this.leftPlayerCounter != value) {
                    if (value < 0) this.leftPlayerCounter = 0;
                    else this.leftPlayerCounter = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerCounter = 0;
        public int RightPlayerCounter {
            get { return this.rightPlayerCounter; }
            set {
                if (this.rightPlayerCounter != value) {
                    if (value < 0) this.rightPlayerCounter = 0;
                    else this.rightPlayerCounter = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private Content.Gameboard.PlayerSelection selectedPlayer = Content.Gameboard.PlayerSelection.LeftPlayer;
        public Content.Gameboard.PlayerSelection SelectedPlayer {
            get { return this.selectedPlayer; }
            set {
                if (this.selectedPlayer != value) {
                    if (value == Content.Gameboard.PlayerSelection.NotSelected) value = Content.Gameboard.PlayerSelection.LeftPlayer;
                    this.selectedPlayer = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool flipPlayers;
        public bool FlipPlayers {
            get { return this.flipPlayers; }
            set {
                if (this.flipPlayers != value) {
                    this.flipPlayers = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTargetCounter();
                }
            }
        }

        #endregion


        #region Funktionen

        public Business() { }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {
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
        }

        public override void Dispose() {
            base.Dispose();
        }

        public override void ResetData() {
            base.ResetData();
            this.FlipPlayers = false;
            this.LeftPlayerCounter = this.StartValue;
            this.RightPlayerCounter = this.StartValue;
        }

        internal void VaryValue(int value) {
            int newCounter;
            if (this.StartValue > this.TargetValue) {
                //count down
                if (this.SelectedPlayer == Content.Gameboard.PlayerSelection.LeftPlayer) {
                    newCounter = this.LeftPlayerCounter - value;
                    if (newCounter < this.TargetValue) this.Vinsert_FaultPlayer();
                    else {
                        this.LeftPlayerCounter = newCounter;
                        this.Vinsert_SetTargetCounter();
                    }
                }
                else {
                    newCounter = this.RightPlayerCounter - value;
                    if (newCounter < this.TargetValue) this.Vinsert_FaultPlayer();
                    else {
                        this.RightPlayerCounter = newCounter;
                        this.Vinsert_SetTargetCounter();
                    }
                }            
            }
            else if (this.StartValue < this.TargetValue) {
                //count up
                if (this.SelectedPlayer == Content.Gameboard.PlayerSelection.LeftPlayer) {
                    newCounter = this.LeftPlayerCounter + value;
                    if (newCounter > this.TargetValue) this.Vinsert_FaultPlayer();
                    else {
                        this.LeftPlayerCounter = newCounter;
                        this.Vinsert_SetTargetCounter();
                    }
                }
                else {
                    newCounter = this.RightPlayerCounter + value;
                    if (newCounter > this.TargetValue) this.Vinsert_FaultPlayer();
                    else {
                        this.RightPlayerCounter = newCounter;
                        this.Vinsert_SetTargetCounter();
                    }
                }
            }
            this.FlipSelectedPlayer();
        }

        public void FlipSelectedPlayer() {
            if (this.SelectedPlayer == Content.Gameboard.PlayerSelection.LeftPlayer) this.SelectedPlayer = Content.Gameboard.PlayerSelection.RightPlayer;
            else this.SelectedPlayer = Content.Gameboard.PlayerSelection.LeftPlayer;
        }

        public virtual void Vinsert_TargetCounterIn() { }
        public void Vinsert_TargetCounterIn(
            VentuzScenes.GamePool._Modules.TargetCounter scene) {
            if (scene is VentuzScenes.GamePool._Modules.TargetCounter) {
                this.Vinsert_SetTargetCounter(scene);
                scene.ToIn();
            }
        }

        public virtual void Vinsert_SetTargetCounter() { }
        public void Vinsert_SetTargetCounter(
            VentuzScenes.GamePool._Modules.TargetCounter scene) {
            this.Vinsert_SetTargetCounter(scene, this.LeftPlayerCounter, this.RightPlayerCounter);
        }
        public void Vinsert_SetTargetCounter(
            VentuzScenes.GamePool._Modules.TargetCounter scene,
            int leftPlayerCounter,
            int rightPlayerCounter) {
            if (scene is VentuzScenes.GamePool._Modules.TargetCounter) {
                scene.SetPositionX(this.TargetCounterPositionX);
                scene.SetPositionY(this.TargetCounterPositionY);
                scene.SetLeftTopName(this.LeftPlayerName);
                scene.SetLeftTopCounter(leftPlayerCounter);
                scene.SetRightBottomName(this.RightPlayerName);
                scene.SetRightBottomCounter(rightPlayerCounter);
                scene.SetFlipPosition(this.FlipPlayers);
            }
        }

        public virtual void Vinsert_FaultPlayer() { }
        public void Vinsert_FaultPlayer(
            VentuzScenes.GamePool._Modules.TargetCounter scene) {
            if (scene is VentuzScenes.GamePool._Modules.TargetCounter) {
                scene.SetFlipPosition(this.FlipPlayers);
                if (this.SelectedPlayer == Content.Gameboard.PlayerSelection.LeftPlayer) scene.FaultLeftTop();
                else scene.FaultRightBottom();
            }
        }

        public virtual void Vinsert_TargetCounterOut() { }
        public void Vinsert_TargetCounterOut(VentuzScenes.GamePool._Modules.TargetCounter scene) { if (scene is VentuzScenes.GamePool._Modules.TargetCounter) scene.ToOut(); }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion
    }
}
