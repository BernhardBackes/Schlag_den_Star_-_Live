using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.Jakkolo {

    public class Business : _Base.Business {

        #region Properties

        private int leftPlayerCoin1 = 0;
        public int LeftPlayerCoin1 {
            get { return this.leftPlayerCoin1; }
            set {
                if (this.leftPlayerCoin1 != value) {
                    if (value < 0) this.leftPlayerCoin1 = 0;
                    else this.leftPlayerCoin1 = value;
                    this.on_PropertyChanged();
                    this.calcResult();
                }
            }
        }

        private int leftPlayerCoin2 = 0;
        public int LeftPlayerCoin2 {
            get { return this.leftPlayerCoin2; }
            set {
                if (this.leftPlayerCoin2 != value) {
                    if (value < 0) this.leftPlayerCoin2 = 0;
                    else this.leftPlayerCoin2 = value;
                    this.on_PropertyChanged();
                    this.calcResult();
                }
            }
        }

        private int leftPlayerCoin3 = 0;
        public int LeftPlayerCoin3 {
            get { return this.leftPlayerCoin3; }
            set {
                if (this.leftPlayerCoin3 != value) {
                    if (value < 0) this.leftPlayerCoin3 = 0;
                    else this.leftPlayerCoin3 = value;
                    this.on_PropertyChanged();
                    this.calcResult();
                }
            }
        }

        private int leftPlayerCoin4 = 0;
        public int LeftPlayerCoin4 {
            get { return this.leftPlayerCoin4; }
            set {
                if (this.leftPlayerCoin4 != value) {
                    if (value < 0) this.leftPlayerCoin4 = 0;
                    else this.leftPlayerCoin4 = value;
                    this.on_PropertyChanged();
                    this.calcResult();
                }
            }
        }

        private int leftPlayerBonus = 0;
        public int LeftPlayerBonus {
            get { return this.leftPlayerBonus; }
            private set {
                if (this.leftPlayerBonus != value) {
                    if (value < 0) this.leftPlayerBonus = 0;
                    else this.leftPlayerBonus = value;
                }
            }
        }

        private int leftPlayerResult = 0;
        public int LeftPlayerResult {
            get { return this.leftPlayerResult; }
            private set {
                if (this.leftPlayerResult != value) {
                    if (value < 0) this.leftPlayerResult = 0;
                    else this.leftPlayerResult = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerCoin1 = 0;
        public int RightPlayerCoin1 {
            get { return this.rightPlayerCoin1; }
            set {
                if (this.rightPlayerCoin1 != value) {
                    if (value < 0) this.rightPlayerCoin1 = 0;
                    else this.rightPlayerCoin1 = value;
                    this.on_PropertyChanged();
                    this.calcResult();
                }
            }
        }

        private int rightPlayerCoin2 = 0;
        public int RightPlayerCoin2 {
            get { return this.rightPlayerCoin2; }
            set {
                if (this.rightPlayerCoin2 != value) {
                    if (value < 0) this.rightPlayerCoin2 = 0;
                    else this.rightPlayerCoin2 = value;
                    this.on_PropertyChanged();
                    this.calcResult();
                }
            }
        }

        private int rightPlayerCoin3 = 0;
        public int RightPlayerCoin3 {
            get { return this.rightPlayerCoin3; }
            set {
                if (this.rightPlayerCoin3 != value) {
                    if (value < 0) this.rightPlayerCoin3 = 0;
                    else this.rightPlayerCoin3 = value;
                    this.on_PropertyChanged();
                    this.calcResult();
                }
            }
        }

        private int rightPlayerCoin4 = 0;
        public int RightPlayerCoin4 {
            get { return this.rightPlayerCoin4; }
            set {
                if (this.rightPlayerCoin4 != value) {
                    if (value < 0) this.rightPlayerCoin4 = 0;
                    else this.rightPlayerCoin4 = value;
                    this.on_PropertyChanged();
                    this.calcResult();
                }
            }
        }

        private int rightPlayerBonus = 0;
        public int RightPlayerBonus {
            get { return this.rightPlayerBonus; }
            private set {
                if (this.rightPlayerBonus != value) {
                    if (value < 0) this.rightPlayerBonus = 0;
                    else this.rightPlayerBonus = value;
                }
            }
        }

        private int rightPlayerResult = 0;
        public int RightPlayerResult {
            get { return this.rightPlayerResult; }
            private set {
                if (this.rightPlayerResult != value) {
                    if (value < 0) this.rightPlayerResult = 0;
                    else this.rightPlayerResult = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int jakkoloPositionX = 0;
        public int JakkoloPositionX {
            get { return this.jakkoloPositionX; }
            set {
                if (this.jakkoloPositionX != value) {
                    this.jakkoloPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetJakkolo();
                }
            }
        }

        private int jakkoloPositionY = 0;
        public int JakkoloPositionY {
            get { return this.jakkoloPositionY; }
            set {
                if (this.jakkoloPositionY != value) {
                    this.jakkoloPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetJakkolo();
                }
            }
        }

        #endregion


        #region Funktionen

        public Business() { }
        public Business(string typeIdentifier) : base(typeIdentifier) { }

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

        public override void ResetData() {
            base.ResetData();
            this.LeftPlayerCoin1 = 0;
            this.LeftPlayerCoin2 = 0;
            this.LeftPlayerCoin3 = 0;
            this.LeftPlayerCoin4 = 0;
            this.RightPlayerCoin1 = 0;
            this.RightPlayerCoin2 = 0;
            this.RightPlayerCoin3 = 0;
            this.RightPlayerCoin4 = 0;
        }

        private void calcResult() {
            int[] coinValues = new int[] { this.LeftPlayerCoin1, this.LeftPlayerCoin2, this.LeftPlayerCoin3, this.LeftPlayerCoin4 };
            this.LeftPlayerBonus = coinValues.Min();
            this.LeftPlayerResult = this.LeftPlayerBonus * 10;
            this.LeftPlayerResult += this.LeftPlayerCoin1 * 1;
            this.LeftPlayerResult += this.LeftPlayerCoin2 * 2;
            this.LeftPlayerResult += this.LeftPlayerCoin3 * 3;
            this.LeftPlayerResult += this.LeftPlayerCoin4 * 4;
            coinValues = new int[] { this.RightPlayerCoin1, this.RightPlayerCoin2, this.RightPlayerCoin3, this.RightPlayerCoin4 };
            this.RightPlayerBonus = coinValues.Min();
            this.RightPlayerResult = this.RightPlayerBonus * 10;
            this.RightPlayerResult += this.RightPlayerCoin1 * 1;
            this.RightPlayerResult += this.RightPlayerCoin2 * 2;
            this.RightPlayerResult += this.RightPlayerCoin3 * 3;
            this.RightPlayerResult += this.RightPlayerCoin4 * 4;
        }

        internal void LeftPlayerCoin1AddHot() {
            this.LeftPlayerCoin1++;
            this.Vinsert_JakkoloToValues();
        }
        internal void LeftPlayerCoin2AddHot() {
            this.LeftPlayerCoin2++;
            this.Vinsert_JakkoloToValues();
        }
        internal void LeftPlayerCoin3AddHot() {
            this.LeftPlayerCoin3++;
            this.Vinsert_JakkoloToValues();
        }
        internal void LeftPlayerCoin4AddHot() {
            this.LeftPlayerCoin4++;
            this.Vinsert_JakkoloToValues();
        }

        internal void RightPlayerCoin1AddHot() {
            this.RightPlayerCoin1++;
            this.Vinsert_JakkoloToValues();
        }
        internal void RightPlayerCoin2AddHot() {
            this.RightPlayerCoin2++;
            this.Vinsert_JakkoloToValues();
        }
        internal void RightPlayerCoin3AddHot() {
            this.RightPlayerCoin3++;
            this.Vinsert_JakkoloToValues();
        }
        internal void RightPlayerCoin4AddHot() {
            this.RightPlayerCoin4++;
            this.Vinsert_JakkoloToValues();
        }


        public virtual void Vinsert_JakkoloIn() { }
        public void Vinsert_JakkoloIn(
            VentuzScenes.GamePool._Modules.Jakkolo scene) {
            if (scene is VentuzScenes.GamePool._Modules.Jakkolo) {
                this.Vinsert_SetJakkolo(scene);
                scene.ToIn();
            }
        }
        public virtual void Vinsert_SetJakkolo() { }
        public void Vinsert_SetJakkolo(
            VentuzScenes.GamePool._Modules.Jakkolo scene) {
            this.Vinsert_SetJakkolo(
                scene,
                this.LeftPlayerCoin1,
                this.LeftPlayerCoin2,
                this.LeftPlayerCoin3,
                this.LeftPlayerCoin4,
                this.LeftPlayerBonus,
                this.LeftPlayerResult,
                this.RightPlayerCoin1,
                this.RightPlayerCoin2,
                this.RightPlayerCoin3,
                this.RightPlayerCoin4,
                this.RightPlayerBonus,
                this.RightPlayerResult);
        }
        public void Vinsert_SetJakkolo(
            VentuzScenes.GamePool._Modules.Jakkolo scene,
            int leftPlayerCoin1,
            int leftPlayerCoin2,
            int leftPlayerCoin3,
            int leftPlayerCoin4,
            int leftPlayerBonus,
            int leftPlayerScore,
            int rightPlayerCoin1,
            int rightPlayerCoin2,
            int rightPlayerCoin3,
            int rightPlayerCoin4,
            int rightPlayerBonus,
            int rightPlayerScore) {
            if (scene is VentuzScenes.GamePool._Modules.Jakkolo) {
                scene.SetPositionX(this.JakkoloPositionX);
                scene.SetPositionY(this.JakkoloPositionY);
                scene.SetTopName(this.LeftPlayerName);
                scene.SetTopCoinValue(1, leftPlayerCoin1);
                scene.SetTopCoinValue(2, leftPlayerCoin2);
                scene.SetTopCoinValue(3, leftPlayerCoin3);
                scene.SetTopCoinValue(4, leftPlayerCoin4);
                scene.SetTopBonus(leftPlayerBonus);
                scene.SetTopScore(leftPlayerScore);
                scene.SetBottomName(this.RightPlayerName);
                scene.SetBottomCoinValue(1, rightPlayerCoin1);
                scene.SetBottomCoinValue(2, rightPlayerCoin2);
                scene.SetBottomCoinValue(3, rightPlayerCoin3);
                scene.SetBottomCoinValue(4, rightPlayerCoin4);
                scene.SetBottomBonus(rightPlayerBonus);
                scene.SetBottomScore(rightPlayerScore);
            }
        }
        public virtual void Vinsert_JakkoloToValues() { }
        public void Vinsert_JakkoloToValues(
            VentuzScenes.GamePool._Modules.Jakkolo scene) {
            this.Vinsert_JakkoloToValues(
                scene,
                this.LeftPlayerCoin1,
                this.LeftPlayerCoin2,
                this.LeftPlayerCoin3,
                this.LeftPlayerCoin4,
                this.LeftPlayerBonus,
                this.LeftPlayerResult,
                this.RightPlayerCoin1,
                this.RightPlayerCoin2,
                this.RightPlayerCoin3,
                this.RightPlayerCoin4,
                this.RightPlayerBonus,
                this.RightPlayerResult);
        }
        public void Vinsert_JakkoloToValues(
            VentuzScenes.GamePool._Modules.Jakkolo scene,
            int leftPlayerCoin1,
            int leftPlayerCoin2,
            int leftPlayerCoin3,
            int leftPlayerCoin4,
            int leftPlayerBonus,
            int leftPlayerScore,
            int rightPlayerCoin1,
            int rightPlayerCoin2,
            int rightPlayerCoin3,
            int rightPlayerCoin4,
            int rightPlayerBonus,
            int rightPlayerScore) {
            if (scene is VentuzScenes.GamePool._Modules.Jakkolo) {
                scene.TopCoinToValue(1, leftPlayerCoin1);
                scene.TopCoinToValue(2, leftPlayerCoin2);
                scene.TopCoinToValue(3, leftPlayerCoin3);
                scene.TopCoinToValue(4, leftPlayerCoin4);
                scene.TopBonusToValue(leftPlayerBonus);
                scene.SetTopScore(leftPlayerScore);
                scene.BottomCoinToValue(1, rightPlayerCoin1);
                scene.BottomCoinToValue(2, rightPlayerCoin2);
                scene.BottomCoinToValue(3, rightPlayerCoin3);
                scene.BottomCoinToValue(4, rightPlayerCoin4);
                scene.BottomBonusToValue(rightPlayerBonus);
                scene.SetBottomScore(rightPlayerScore);
            }
        }
        public virtual void Vinsert_JakkoloOut() { }
        public void Vinsert_JakkoloOut(
            VentuzScenes.GamePool._Modules.Jakkolo scene) {
            if (scene is VentuzScenes.GamePool._Modules.Jakkolo) scene.ToOut();
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
                this.hostMasterScene.SetLeftPlayerGameScore(this.LeftPlayerResult);
                this.hostMasterScene.SetRightPlayerGameScore(this.RightPlayerResult);
            }
            if (this.leftPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.leftPlayerMasterScene.SetLeftPlayerGameScore(this.LeftPlayerResult);
                this.leftPlayerMasterScene.SetRightPlayerGameScore(this.RightPlayerResult);
            }
            if (this.rightPlayerMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.rightPlayerMasterScene.SetLeftPlayerGameScore(this.LeftPlayerResult);
                this.rightPlayerMasterScene.SetRightPlayerGameScore(this.RightPlayerResult);
            }
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming
        #endregion

    }
}
