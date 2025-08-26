using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cliparts.IOnet.IOUnit.IONbuz;
using Cliparts.Serialization;

using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;
using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TeamBuzzerScore;
using System.Xml.Serialization;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TeamBuzzerScore
{

    public class Business : _Base.BuzzerScore.Business {

        #region Properties

        private int leftPlayer2ndBuzzerChannel = 3;
        public int LeftPlayer2ndBuzzerChannel
        {
            get { return this.leftPlayer2ndBuzzerChannel; }
            set
            {
                if (this.leftPlayer2ndBuzzerChannel != value)
                {
                    if (value < 1) value = 1;
                    if (value > 8) value = 8;
                    this.leftPlayer2ndBuzzerChannel = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayer2ndBuzzerChannel = 4;
        public int RightPlayer2ndBuzzerChannel
        {
            get { return this.rightPlayer2ndBuzzerChannel; }
            set
            {
                if (this.rightPlayer2ndBuzzerChannel != value)
                {
                    if (value < 1) value = 1;
                    if (value > 8) value = 8;
                    this.rightPlayer2ndBuzzerChannel = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool leftPlayer1stBuzzered = false;
        [NotSerialized]
        public bool LeftPlayer1stBuzzered
        {
            get { return this.leftPlayer1stBuzzered; }
            set
            {
                if (this.leftPlayer1stBuzzered != value)
                {
                    this.leftPlayer1stBuzzered = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool leftPlayer2ndBuzzered = false;
        [NotSerialized]
        public bool LeftPlayer2ndBuzzered
        {
            get { return this.leftPlayer2ndBuzzered; }
            set
            {
                if (this.leftPlayer2ndBuzzered != value)
                {
                    this.leftPlayer2ndBuzzered = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool rightPlayer1stBuzzered = false;
        [NotSerialized]
        public bool RightPlayer1stBuzzered
        {
            get { return this.rightPlayer1stBuzzered; }
            set
            {
                if (this.rightPlayer1stBuzzered != value)
                {
                    this.rightPlayer1stBuzzered = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool rightPlayer2ndBuzzered = false;
        [NotSerialized]
        public bool RightPlayer2ndBuzzered
        {
            get { return this.rightPlayer2ndBuzzered; }
            set
            {
                if (this.rightPlayer2ndBuzzered != value)
                {
                    this.rightPlayer2ndBuzzered = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int leftPlayer2ndDMXStartchannel = 7;
        public int LeftPlayer2ndDMXStartchannel
        {
            get { return this.leftPlayer2ndDMXStartchannel; }
            set
            {
                if (this.leftPlayer2ndDMXStartchannel != value)
                {
                    if (value < 1) this.leftPlayer2ndDMXStartchannel = 1;
                    else if (value > 256) this.leftPlayer2ndDMXStartchannel = 256;
                    else this.leftPlayer2ndDMXStartchannel = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayer2ndDMXStartchannel = 10;
        public int RightPlayer2ndDMXStartchannel
        {
            get { return this.rightPlayer2ndDMXStartchannel; }
            set
            {
                if (this.rightPlayer2ndDMXStartchannel != value)
                {
                    if (value < 1) this.rightPlayer2ndDMXStartchannel = 1;
                    else if (value > 256) this.rightPlayer2ndDMXStartchannel = 256;
                    else this.rightPlayer2ndDMXStartchannel = value;
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

        [XmlIgnore]
        public override PlayerSelection BuzzeredPlayer
        {
            get { return this.buzzeredPlayer; }
            protected set
            {
                if (this.buzzeredPlayer != value)
                {
                    this.buzzeredPlayer = value;
                    this.on_PropertyChanged();
                    switch (buzzeredPlayer)
                    {
                        case PlayerSelection.LeftPlayer:
                            this.LeftPlayer1stBuzzered = true;
                            this.LeftPlayer2ndBuzzered = true;
                            this.SetLeftPlayerOn();
                            this.SetLeftPlayer2ndOn();
                            this.SetRightPlayerOff();
                            this.SetRightPlayer2ndOff();
                            break;
                        case PlayerSelection.RightPlayer:
                            this.RightPlayer1stBuzzered = true;
                            this.RightPlayer2ndBuzzered = true;
                            this.SetLeftPlayerOff();
                            this.SetLeftPlayer2ndOff();
                            this.SetRightPlayerOn();
                            this.SetRightPlayer2ndOn();
                            break;
                        case PlayerSelection.NotSelected:
                        default:
                            this.LeftPlayer1stBuzzered = false;
                            this.LeftPlayer2ndBuzzered = false;
                            this.RightPlayer1stBuzzered = false;
                            this.RightPlayer2ndBuzzered = false;
                            this.SetLeftPlayerOff();
                            this.SetLeftPlayer2ndOff();
                            this.SetRightPlayerOff();
                            this.SetRightPlayer2ndOff();
                            break;
                    }
                }
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.TeamBuzzerScore'", typeIdentifier);
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
        }

        public override void Dispose() {
            base.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Dispose();
        }

        public override void Init()
        {
            base.Init();
            this.SetLeftPlayerOff();
            this.SetLeftPlayer2ndOff();
            this.SetRightPlayerOff();
            this.SetRightPlayer2ndOff();
        }

        public override void ResetData()
        {
            base.ResetData();
            this.LeftPlayer1stBuzzered = false;
            this.LeftPlayer2ndBuzzered = false;
            this.RightPlayer1stBuzzered = false;
            this.RightPlayer2ndBuzzered = false;
        }

        public override void Next()
        {
            base.Next();
            this.LeftPlayer1stBuzzered = false;
            this.LeftPlayer2ndBuzzered = false;
            this.RightPlayer1stBuzzered = false;
            this.RightPlayer2ndBuzzered = false;
        }

        public override void ReleaseBuzzer()
        {
            bool[] inputMask = new bool[8];
            if (this.LeftPlayerBuzzerChannel > 0 &&
                this.LeftPlayerBuzzerChannel <= inputMask.Length) inputMask[this.LeftPlayerBuzzerChannel - 1] = true;
            if (this.LeftPlayer2ndBuzzerChannel > 0 &&
                this.LeftPlayer2ndBuzzerChannel <= inputMask.Length) inputMask[this.LeftPlayer2ndBuzzerChannel - 1] = true;
            if (this.RightPlayerBuzzerChannel > 0 &&
                this.RightPlayerBuzzerChannel <= inputMask.Length) inputMask[this.RightPlayerBuzzerChannel - 1] = true;
            if (this.RightPlayer2ndBuzzerChannel > 0 &&
                this.RightPlayer2ndBuzzerChannel <= inputMask.Length) inputMask[this.RightPlayer2ndBuzzerChannel - 1] = true;
            this.buzzerHandler.SetInputMask(this.IOUnitName, inputMask);
            this.buzzerHandler.ReleaseBuzzer(this.IOUnitName, WorkModes.EVENT);
            this.BuzzeredPlayer = PlayerSelection.NotSelected;
        }

        public override void DoBuzzer(
            PlayerSelection buzzeredPlayer)
        {
            if (this.BuzzeredPlayer == PlayerSelection.NotSelected &&
                this.BuzzeredPlayer != buzzeredPlayer)
            {
                this.BuzzeredPlayer = buzzeredPlayer;
                this.Vinsert_PlayJingleGood();
            }
        }

        public virtual void DoBuzzer(
            PlayerSelection buzzeredPlayer,
            bool secondBuzzer)
        {
            if (this.BuzzeredPlayer == PlayerSelection.NotSelected)
            {
                switch (buzzeredPlayer)
                {
                    case PlayerSelection.LeftPlayer:
                        if (secondBuzzer &&
                            !this.LeftPlayer2ndBuzzered)
                        {
                            if (this.LeftPlayer1stBuzzered) this.DoBuzzer(buzzeredPlayer);
                            else
                            {
                                this.LeftPlayer2ndBuzzered = true;
                                this.Vinsert_PlayJingleBuzzer();
                                this.SetLeftPlayer2ndOn();
                            }
                        }
                        else if (!this.LeftPlayer1stBuzzered)
                        {
                            if (this.LeftPlayer2ndBuzzered) this.DoBuzzer(buzzeredPlayer);
                            else
                            {
                                this.LeftPlayer1stBuzzered = true;
                                this.Vinsert_PlayJingleBuzzer();
                                this.SetLeftPlayerOn();
                            }
                        }
                        break;
                    case PlayerSelection.RightPlayer:
                        if (secondBuzzer &&
                            !this.RightPlayer2ndBuzzered)
                        {
                            if (this.RightPlayer1stBuzzered) this.DoBuzzer(buzzeredPlayer);
                            else
                            {
                                this.RightPlayer2ndBuzzered = true;
                                this.Vinsert_PlayJingleBuzzer();
                                this.SetRightPlayer2ndOn();
                            }
                        }
                        else if (!this.RightPlayer1stBuzzered)
                        {
                            if (this.RightPlayer2ndBuzzered) this.DoBuzzer(buzzeredPlayer);
                            else
                            {
                                this.RightPlayer1stBuzzered = true;
                                this.Vinsert_PlayJingleBuzzer();
                                this.SetRightPlayerOn();
                            }
                        }
                        break;
                }
            }
        }

        internal void SetLeftPlayer2ndOn()
        {
            byte[] valueList = new byte[] { this.LeftOnColor.R, this.LeftOnColor.G, this.LeftOnColor.B };
            this.SetDMXValues(this.LeftPlayer2ndDMXStartchannel, valueList);
        }
        internal void SetLeftPlayer2ndOff()
        {
            byte[] valueList = new byte[] { this.LeftOffColor.R, this.LeftOffColor.G, this.LeftOffColor.B };
            this.SetDMXValues(this.LeftPlayer2ndDMXStartchannel, valueList);
        }

        internal void SetRightPlayer2ndOn()
        {
            byte[] valueList = new byte[] { this.RightOnColor.R, this.RightOnColor.G, this.RightOnColor.B };
            this.SetDMXValues(this.RightPlayer2ndDMXStartchannel, valueList);
        }
        internal void SetRightPlayer2ndOff()
        {
            byte[] valueList = new byte[] {this.RightOffColor.R, this.RightOffColor.G, this.RightOffColor.B };
            this.SetDMXValues(this.RightPlayer2ndDMXStartchannel, valueList);
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }
        public new void Vinsert_Buzzer(
            VentuzScenes.GamePool._Modules.Timeout scene,
            PlayerSelection buzzeredPlayer)
        {
            if (scene is VentuzScenes.GamePool._Modules.Timeout)
            {
                this.Vinsert_SetTimeout(scene);
                switch (buzzeredPlayer)
                {
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
        internal void Vinsert_PlayJingleBuzzer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.PlayJingle(Insert.JingleValues.Buzzer); }
        internal void Vinsert_PlayJingleGood() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.PlayJingle(Insert.JingleValues.True); }
        public override void Vinsert_Buzzer(PlayerSelection buzzeredPlayer) { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_Buzzer(this.insertScene.Timeout, buzzeredPlayer); }
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

        protected override void sync_buzzerHandler_BuzUnit_Buzzered(object content)
        {
            BuzzerIDParam_EventArgs e = content as BuzzerIDParam_EventArgs;
            if (this.isActive &&
                e is BuzzerIDParam_EventArgs &&
                e.Arg.Name == this.IOUnitName)
            {
                if (e.Arg.BuzzerID == this.LeftPlayerBuzzerChannel)
                {
                    if (this.FlipPlayers) this.DoBuzzer(PlayerSelection.RightPlayer, false);
                    else this.DoBuzzer(PlayerSelection.LeftPlayer, false);
                }
                else if (e.Arg.BuzzerID == this.LeftPlayer2ndBuzzerChannel)
                {
                    if (this.FlipPlayers) this.DoBuzzer(PlayerSelection.RightPlayer, true);
                    else this.DoBuzzer(PlayerSelection.LeftPlayer, true);
                }
                else if (e.Arg.BuzzerID == this.RightPlayerBuzzerChannel)
                {
                    if (this.FlipPlayers) this.DoBuzzer(PlayerSelection.LeftPlayer, false);
                    else this.DoBuzzer(PlayerSelection.RightPlayer, false);
                }
                else if (e.Arg.BuzzerID == this.RightPlayer2ndBuzzerChannel)
                {
                    if (this.FlipPlayers) this.DoBuzzer(PlayerSelection.LeftPlayer, true);
                    else this.DoBuzzer(PlayerSelection.RightPlayer, true);
                }
            }
        }

        #endregion

    }
}
