using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.NumDifferenceScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.NumDifferenceScore {

    public class Business : _Base.Score.Business {

        #region Properties

        private int numericValuesPositionX = 0;
        public int NumericValuesPositionX {
            get { return this.numericValuesPositionX; }
            set {
                if (this.numericValuesPositionX != value) {
                    this.numericValuesPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetNumericValues();
                }
            }
        }

        private int numericValuesPositionY = 0;
        public int NumericValuesPositionY {
            get { return this.numericValuesPositionY; }
            set {
                if (this.numericValuesPositionY != value) {
                    this.numericValuesPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetNumericValues();
                }
            }
        }

        private double leftPlayer1stHalf = 0;
        public double LeftPlayer1stHalf {
            get { return this.leftPlayer1stHalf; }
            set {
                if (this.leftPlayer1stHalf != value) {
                    if (value < 0) this.leftPlayer1stHalf = 0;
                    else this.leftPlayer1stHalf = value;
                    this.setDifferences();
                    this.on_PropertyChanged();
                }
            }
        }

        private double leftPlayer2ndHalf = 0;
        public double LeftPlayer2ndHalf {
            get { return this.leftPlayer2ndHalf; }
            set {
                if (this.leftPlayer2ndHalf != value) {
                    if (value < 0) this.leftPlayer2ndHalf = 0;
                    else this.leftPlayer2ndHalf = value;
                    this.setDifferences();
                    this.on_PropertyChanged();
                }
            }
        }

        public string LeftPlayerDifference { get; private set; }

        private double rightPlayer1stHalf = 0;
        public double RightPlayer1stHalf {
            get { return this.rightPlayer1stHalf; }
            set {
                if (this.rightPlayer1stHalf != value) {
                    if (value < 0) this.rightPlayer1stHalf = 0;
                    else this.rightPlayer1stHalf = value;
                    this.setDifferences();
                    this.on_PropertyChanged();
                }
            }
        }

        private double rightPlayer2ndHalf = 0;
        public double RightPlayer2ndHalf {
            get { return this.rightPlayer2ndHalf; }
            set {
                if (this.rightPlayer2ndHalf != value) {
                    if (value < 0) this.rightPlayer2ndHalf = 0;
                    else this.rightPlayer2ndHalf = value;
                    this.setDifferences();
                    this.on_PropertyChanged();
                }
            }
        }

        public string RightPlayerDifference { get; private set; }

        private bool show1stHalf = false;
        public bool Show1stHalf {
            get { return this.show1stHalf; }
            set {
                if (this.show1stHalf != value) {
                    this.show1stHalf = value;
                    this.setDifferences();
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

            this.LeftPlayerDifference = string.Empty;
            this.RightPlayerDifference = string.Empty;

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.NumDifferenceScore'", typeIdentifier);
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

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);

            this.setDifferences();
        }

        public override void Dispose() {
            base.Dispose();
            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
        }

        public override void ResetData() {
            base.ResetData();
            this.Next();
        }

        public override void Next() {
            base.Next();
            this.LeftPlayer1stHalf = 0;
            this.LeftPlayer2ndHalf = 0;
            this.RightPlayer1stHalf = 0;
            this.RightPlayer2ndHalf = 0;
            this.setDifferences();
        }

        internal void LeftPlayerCalc1stHalf() { 
            this.LeftPlayer1stHalf = this.LeftPlayer1stHalf / 2;
            this.setDifferences();
        }

        internal void RightPlayerCalc1stHalf() { 
            this.RightPlayer1stHalf = this.RightPlayer1stHalf / 2;
            this.setDifferences();
        }

        private void setDifferences() {
            double difference;
            string text;

            if (this.LeftPlayer1stHalf > 0 &&
                this.LeftPlayer2ndHalf > 0) {
                difference = Math.Abs(this.LeftPlayer1stHalf - this.LeftPlayer2ndHalf);
                text = difference.ToString("0.000");
            }
            else text = string.Empty;
            if (this.LeftPlayerDifference != text) {
                this.LeftPlayerDifference = text;
                this.on_PropertyChanged("LeftPlayerDifference");
            }

            if (this.RightPlayer1stHalf > 0 &&
                this.RightPlayer2ndHalf > 0) {
                difference = Math.Abs(this.RightPlayer1stHalf - this.RightPlayer2ndHalf);
                text = difference.ToString("0.000");
            }
            else text = string.Empty;
            if (this.RightPlayerDifference != text) {
                this.RightPlayerDifference = text;
                this.on_PropertyChanged("RightPlayerDifference");
            }
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }

        public void Vinsert_NumericValuesIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_NumericValuesIn(this.insertScene.NumericValuesInsert); }
        public void Vinsert_NumericValuesIn(
            VentuzScenes.GamePool._Modules.NumericValuesInsert scene) {
            if (scene is VentuzScenes.GamePool._Modules.NumericValuesInsert) {
                this.Vinsert_SetNumericValues(scene);
                scene.ToIn();
            }
        }

        public void Vinsert_SetNumericValues() { this.Vinsert_SetNumericValues(this.insertScene.NumericValuesInsert); }
        public void Vinsert_SetNumericValues(VentuzScenes.GamePool._Modules.NumericValuesInsert scene) {
            VentuzScenes.GamePool._Modules.NumericValuesInsert.BorderPosition border = VentuzScenes.GamePool._Modules.NumericValuesInsert.BorderPosition.Off;
            if (this.Show1stHalf) {
                string leftPlayerValue = string.Empty;
                double leftPlayerDifference = double.MinValue;
                if (this.LeftPlayer1stHalf > 0) {
                    leftPlayerValue = this.LeftPlayer1stHalf.ToString("0.000");
                    if (this.LeftPlayer2ndHalf > 0) leftPlayerDifference = Math.Abs(this.LeftPlayer1stHalf - this.LeftPlayer2ndHalf);
                }
                string rightPlayerValue = string.Empty;
                double rightPlayerDifference = double.MinValue;
                if (this.RightPlayer1stHalf > 0) {
                    rightPlayerValue = this.RightPlayer1stHalf.ToString("0.000");
                    if (this.RightPlayer2ndHalf > 0) rightPlayerDifference = Math.Abs(this.RightPlayer1stHalf - this.RightPlayer2ndHalf);
                }
                this.Vinsert_SetNumericValues(scene, leftPlayerValue, this.LeftPlayerDifference, rightPlayerValue, this.RightPlayerDifference, border);
                if (leftPlayerDifference >= 0) scene.TopAddOnValueToIn();
                else scene.TopAddOnValueSetOut();
                if (rightPlayerDifference >= 0) scene.BottomAddOnValueToIn();
                else scene.BottomAddOnValueSetOut();
                if (leftPlayerDifference >= 0 &&
                    rightPlayerDifference >= 0) {
                    if (leftPlayerDifference < rightPlayerDifference) Helper.invokeActionAfterDelay(scene.TopAddOnValueToGreen, 1500, this.syncContext);
                    else if (leftPlayerDifference > rightPlayerDifference) Helper.invokeActionAfterDelay(scene.BottomAddOnValueToGreen, 1500, this.syncContext);
                }
            }
            else { 
                if (this.LeftPlayer1stHalf > 0 &&
                    this.LeftPlayer2ndHalf > 0 &&
                    this.RightPlayer1stHalf > 0 &&
                    this.RightPlayer2ndHalf > 0) {
                    double leftPlayerDifference = Math.Abs(this.LeftPlayer1stHalf - this.LeftPlayer2ndHalf);
                    double rightPlayerDifference = Math.Abs(this.RightPlayer1stHalf - this.RightPlayer2ndHalf);
                    if (leftPlayerDifference < rightPlayerDifference) border = VentuzScenes.GamePool._Modules.NumericValuesInsert.BorderPosition.Top;
                    else if (leftPlayerDifference > rightPlayerDifference) border = VentuzScenes.GamePool._Modules.NumericValuesInsert.BorderPosition.Bottom;
                    else border = VentuzScenes.GamePool._Modules.NumericValuesInsert.BorderPosition.Off;
                }
                this.Vinsert_SetNumericValues(scene, this.LeftPlayerDifference, string.Empty, this.RightPlayerDifference, string.Empty, border);
            }
        }

        public void Vinsert_SetNumericValues(
            VentuzScenes.GamePool._Modules.NumericValuesInsert scene,
            string leftPlayerValue,
            string leftPlayerAddOnValue,
            string rightPlayerValue,
            string rightPlayerAddOnValue,
            VentuzScenes.GamePool._Modules.NumericValuesInsert.BorderPosition border) {
            if (scene is VentuzScenes.GamePool._Modules.NumericValuesInsert) {
                scene.SetPositionX(this.NumericValuesPositionX);
                scene.SetPositionY(this.NumericValuesPositionY);
                scene.SetTopName(this.LeftPlayerName);
                scene.SetTopValue(leftPlayerValue);
                scene.SetTopAddOnValue(leftPlayerAddOnValue);
                scene.SetBottomName(this.RightPlayerName);
                scene.SetBottomValue(rightPlayerValue);
                scene.SetBottomAddOnValue(rightPlayerAddOnValue);
                scene.SetBorder(border);
            }
        }
        public void Vinsert_NumericValuesOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_NumericValuesOut(this.insertScene.NumericValuesInsert); }
        public void Vinsert_NumericValuesOut(
            VentuzScenes.GamePool._Modules.NumericValuesInsert scene) {
            if (scene is VentuzScenes.GamePool._Modules.NumericValuesInsert) scene.ToOut();
        }

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
        #endregion

        #region Events.Incoming
        #endregion

    }
}
