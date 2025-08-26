using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cliparts.IOnet.IOUnit.IONbase;
using Cliparts.IOnet.IOUnit.IONbuz;

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;
using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.NextLightScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.NextLightScore {

    public class Business : _Base.Score.Business {

        #region Properties

        private BuzzerIO.Business buzzerHandler;

        private List<string> ioUnitNameList = new List<string>();
        public string[] IOUnitNameList { get { return this.ioUnitNameList.ToArray(); } }

        private InfoParamArray_EventArgs ioUnitInfo;

        private string ioUnitName = string.Empty;
        public string IOUnitName {
            get { return this.ioUnitName; }
            set {
                if (this.ioUnitName != value) {
                    if (value == null) value = string.Empty;
                    this.ioUnitName = value;
                    this.on_PropertyChanged();
                    this.ioUnitConnectionStatus = Tools.NetContact.ClientStates.Missing;
                    this.ioUnitWorkMode = WorkModes.NA;
                    this.checkIOUnitStatus();
                    this.requestIOUnitStates(value);
                }
            }
        }

        private Tools.NetContact.ClientStates ioUnitConnectionStatus = Tools.NetContact.ClientStates.Missing;

        private WorkModes ioUnitWorkMode = WorkModes.NA;

        public BuzzerUnitStates IOUnitStatus { get; private set; }

        private int leftPlayerBuzzerChannel = 1;
        public int LeftPlayerBuzzerChannel {
            get { return this.leftPlayerBuzzerChannel; }
            set {
                if (this.leftPlayerBuzzerChannel != value) {
                    if (value < 1) value = 1;
                    if (value > 8) value = 8;
                    this.leftPlayerBuzzerChannel = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerBuzzerChannel = 2;
        public int RightPlayerBuzzerChannel {
            get { return this.rightPlayerBuzzerChannel; }
            set {
                if (this.rightPlayerBuzzerChannel != value) {
                    if (value < 1) value = 1;
                    if (value > 8) value = 8;
                    this.rightPlayerBuzzerChannel = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private Color leftPlayerOffColor = Color.Red;
        public Color LeftPlayerOffColor {
            get { return this.leftPlayerOffColor; }
            set {
                if (this.leftPlayerOffColor != value) {
                    this.leftPlayerOffColor = value;
                    this.on_PropertyChanged();
                    this.SetDMXValues();
                }
            }
        }

        private Color leftPlayerOnColor = Color.Black;
        public Color LeftPlayerOnColor {
            get { return this.leftPlayerOnColor; }
            set {
                if (this.leftPlayerOnColor != value) {
                    this.leftPlayerOnColor = value;
                    this.on_PropertyChanged();
                    this.SetDMXValues();
                }
            }
        }


        private Color rightPlayerOffColor = Color.Blue;
        public Color RightPlayerOffColor {
            get { return this.rightPlayerOffColor; }
            set {
                if (this.rightPlayerOffColor != value) {
                    this.rightPlayerOffColor = value;
                    this.on_PropertyChanged();
                    this.SetDMXValues();
                }
            }
        }

        private Color rightPlayerOnColor = Color.Black;
        public Color RightPlayerOnColor {
            get { return this.rightPlayerOnColor; }
            set {
                if (this.rightPlayerOnColor != value) {
                    this.rightPlayerOnColor = value;
                    this.on_PropertyChanged();
                    this.SetDMXValues();
                }
            }
        }

        private int leftPlayerCounter = 0;
        public int LeftPlayerCounter {
            get { return this.leftPlayerCounter; }
            set {
                if (this.leftPlayerCounter != value) {
                    this.leftPlayerCounter = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private byte dmxStartAddressLeft_0 = 1;
        public byte DMXStartAddressLeft_0 {
            get { return this.dmxStartAddressLeft_0; }
            set {
                if (this.dmxStartAddressLeft_0 != value) {
                    this.dmxStartAddressLeft_0 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private byte dmxStartAddressLeft_1 = 4;
        public byte DMXStartAddressLeft_1 {
            get { return this.dmxStartAddressLeft_1; }
            set {
                if (this.dmxStartAddressLeft_1 != value) {
                    this.dmxStartAddressLeft_1 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private byte dmxStartAddressLeft_2 = 7;
        public byte DMXStartAddressLeft_2 {
            get { return this.dmxStartAddressLeft_2; }
            set {
                if (this.dmxStartAddressLeft_2 != value) {
                    this.dmxStartAddressLeft_2 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private byte dmxStartAddressLeft_3 = 10;
        public byte DMXStartAddressLeft_3 {
            get { return this.dmxStartAddressLeft_3; }
            set {
                if (this.dmxStartAddressLeft_3 != value) {
                    this.dmxStartAddressLeft_3 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private byte dmxStartAddressLeft_4 = 13;
        public byte DMXStartAddressLeft_4 {
            get { return this.dmxStartAddressLeft_4; }
            set {
                if (this.dmxStartAddressLeft_4 != value) {
                    this.dmxStartAddressLeft_4 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerCounter = 0;
        public int RightPlayerCounter {
            get { return this.rightPlayerCounter; }
            set {
                if (this.rightPlayerCounter != value) {
                    this.rightPlayerCounter = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private byte dmxStartAddressRight_0 = 16;
        public byte DMXStartAddressRight_0 {
            get { return this.dmxStartAddressRight_0; }
            set {
                if (this.dmxStartAddressRight_0 != value) {
                    this.dmxStartAddressRight_0 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private byte dmxStartAddressRight_1 = 19;
        public byte DMXStartAddressRight_1 {
            get { return this.dmxStartAddressRight_1; }
            set {
                if (this.dmxStartAddressRight_1 != value) {
                    this.dmxStartAddressRight_1 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private byte dmxStartAddressRight_2 = 22;
        public byte DMXStartAddressRight_2 {
            get { return this.dmxStartAddressRight_2; }
            set {
                if (this.dmxStartAddressRight_2 != value) {
                    this.dmxStartAddressRight_2 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private byte dmxStartAddressRight_3 = 25;
        public byte DMXStartAddressRight_3 {
            get { return this.dmxStartAddressRight_3; }
            set {
                if (this.dmxStartAddressRight_3 != value) {
                    this.dmxStartAddressRight_3 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private byte dmxStartAddressRight_4 = 28;
        public byte DMXStartAddressRight_4 {
            get { return this.dmxStartAddressRight_4; }
            set {
                if (this.dmxStartAddressRight_4 != value) {
                    this.dmxStartAddressRight_4 = value;
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.NextLightScore'", typeIdentifier);
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

            this.buzzerHandler = buzzerHandler;
            this.buzzerHandler.BuzUnit_Buzzered += this.buzzerHandler_BuzUnit_Buzzered;
            this.buzzerHandler.Unit_ConnectionStatusChanged += this.buzzerHandler_UnitConnectionStatusChanged;
            this.buzzerHandler.Unit_ConnectionStatusRequest += this.buzzerHandler_UnitConnectionStatusRequest;
            this.buzzerHandler.Unit_InfoListChanged += this.buzzerHandler_UnitInfoListChanged;
            this.buzzerHandler.BuzUnit_WorkmodeChanged += this.buzzerHandler_BuzUnit_WorkmodeChanged;
            this.buzzerHandler.BuzUnit_WorkmodeRequest += this.buzzerHandler_BuzUnit_WorkmodeRequest;
            this.fillBuzzerUnitList(this.buzzerHandler.UnitInfoList);
            this.buzzerHandler.RequestUnitConnectionStatus(this.IOUnitName);
            this.buzzerHandler.RequestUnitWorkMode(this.IOUnitName);

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);
        }

        public override void Dispose() {
            base.Dispose();

            this.buzzerHandler.BuzUnit_Buzzered -= this.buzzerHandler_BuzUnit_Buzzered;
            this.buzzerHandler.Unit_ConnectionStatusChanged -= this.buzzerHandler_UnitConnectionStatusChanged;
            this.buzzerHandler.Unit_ConnectionStatusRequest -= this.buzzerHandler_UnitConnectionStatusRequest;
            this.buzzerHandler.Unit_InfoListChanged -= this.buzzerHandler_UnitInfoListChanged;
            this.buzzerHandler.BuzUnit_WorkmodeChanged -= this.buzzerHandler_BuzUnit_WorkmodeChanged;
            this.buzzerHandler.BuzUnit_WorkmodeRequest -= this.buzzerHandler_BuzUnit_WorkmodeRequest;
            this.buzzerHandler = null;

            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
        }

        public override void ResetData() {
            base.ResetData();
            this.LockBuzzer();
            this.LeftPlayerCounter = 0;
            this.RightPlayerCounter = 0;
            this.SetDMXValues();
        }

        internal void Resolve() {
            if (this.LeftPlayerCounter >= 5 &&
                this.RightPlayerCounter < 5) this.LeftPlayerScore++;
            else if (this.LeftPlayerCounter < 5 &&
                this.RightPlayerCounter >= 5) this.RightPlayerScore++;
        }

        public override void Next() {
            base.Next();
            this.LockBuzzer();
            this.LeftPlayerCounter = 0;
            this.RightPlayerCounter = 0;
            this.SetDMXValues();
        }

        private void fillBuzzerUnitList(
            InfoParam[] unitInfoList) {
            this.ioUnitNameList.Clear();
            if (unitInfoList is InfoParam[]) {
                foreach (InfoParam item in unitInfoList) {
                    if (item is InfoParam) this.ioUnitNameList.Add(item.Name);
                }
            }
            this.on_PropertyChanged("IOUnitNameList");
        }

        private void checkIOUnitStatus() {
            BuzzerUnitStates ioUnitStatus = BuzzerUnitStates.NotAvailable;
            if (this.ioUnitNameList.Contains(this.IOUnitName)) {
                switch (this.ioUnitConnectionStatus) {
                    case Tools.NetContact.ClientStates.Connected:
                        ioUnitStatus = BuzzerUnitStates.Connected;
                        break;
                    case Tools.NetContact.ClientStates.Connecting:
                        ioUnitStatus = BuzzerUnitStates.Connecting;
                        break;
                    case Tools.NetContact.ClientStates.Disconnected:
                        ioUnitStatus = BuzzerUnitStates.Disconnected;
                        break;
                    case Tools.NetContact.ClientStates.Missing:
                    default:
                        ioUnitStatus = BuzzerUnitStates.Missing;
                        break;
                }
                if (ioUnitStatus == BuzzerUnitStates.Connected) {
                    switch (this.ioUnitWorkMode) {
                        case WorkModes.BUZZER:
                            ioUnitStatus = BuzzerUnitStates.BuzzerMode;
                            break;
                        case WorkModes.EVENT:
                            ioUnitStatus = BuzzerUnitStates.EventMode;
                            break;
                        case WorkModes.LOCK:
                            ioUnitStatus = BuzzerUnitStates.Locked;
                            break;
                        case WorkModes.NA:
                        default:
                            break;
                    }
                }
            }
            if (this.IOUnitStatus != ioUnitStatus) {
                this.IOUnitStatus = ioUnitStatus;
                this.on_PropertyChanged("IOUnitStatus");
            }
        }

        private void requestIOUnitStates(
            string unitName) {
            if (buzzerHandler is BuzzerIO.Business) {
                this.buzzerHandler.RequestUnitConnectionStatus(unitName);
                this.buzzerHandler.RequestUnitWorkMode(unitName);
            }
        }

        public void SetDMXValues() {
            if (this.buzzerHandler is BuzzerIO.Business) {
                this.buzzerHandler.SetDMXMode(this.IOUnitName, IOnet.IOUnit.IONbuzDX.DMXBuzzerModes.OFF);

                byte[] offValue = new byte[] { this.LeftPlayerOffColor.R, this.LeftPlayerOffColor.G, this.LeftPlayerOffColor.B };
                byte[] onValue = new byte[] { this.LeftPlayerOnColor.R, this.LeftPlayerOnColor.G, this.LeftPlayerOnColor.B };
                if (this.LeftPlayerCounter >= 1) this.buzzerHandler.SetDMXChannel(this.IOUnitName, this.DMXStartAddressLeft_0, onValue);
                else this.buzzerHandler.SetDMXChannel(this.IOUnitName, this.DMXStartAddressLeft_0, offValue);
                if (this.LeftPlayerCounter >= 2) this.buzzerHandler.SetDMXChannel(this.IOUnitName, this.DMXStartAddressLeft_1, onValue);
                else this.buzzerHandler.SetDMXChannel(this.IOUnitName, this.DMXStartAddressLeft_1, offValue);
                if (this.LeftPlayerCounter >= 3) this.buzzerHandler.SetDMXChannel(this.IOUnitName, this.DMXStartAddressLeft_2, onValue);
                else this.buzzerHandler.SetDMXChannel(this.IOUnitName, this.DMXStartAddressLeft_2, offValue);
                if (this.LeftPlayerCounter >= 4) this.buzzerHandler.SetDMXChannel(this.IOUnitName, this.DMXStartAddressLeft_3, onValue);
                else this.buzzerHandler.SetDMXChannel(this.IOUnitName, this.DMXStartAddressLeft_3, offValue);
                if (this.LeftPlayerCounter >= 5) this.buzzerHandler.SetDMXChannel(this.IOUnitName, this.DMXStartAddressLeft_4, onValue);
                else this.buzzerHandler.SetDMXChannel(this.IOUnitName, this.DMXStartAddressLeft_4, offValue);

                offValue = new byte[] { this.RightPlayerOffColor.R, this.RightPlayerOffColor.G, this.RightPlayerOffColor.B };
                onValue = new byte[] { this.RightPlayerOnColor.R, this.RightPlayerOnColor.G, this.RightPlayerOnColor.B };
                if (this.RightPlayerCounter >= 1) this.buzzerHandler.SetDMXChannel(this.IOUnitName, this.DMXStartAddressRight_0, onValue);
                else this.buzzerHandler.SetDMXChannel(this.IOUnitName, this.DMXStartAddressRight_0, offValue);
                if (this.RightPlayerCounter >= 2) this.buzzerHandler.SetDMXChannel(this.IOUnitName, this.DMXStartAddressRight_1, onValue);
                else this.buzzerHandler.SetDMXChannel(this.IOUnitName, this.DMXStartAddressRight_1, offValue);
                if (this.RightPlayerCounter >= 3) this.buzzerHandler.SetDMXChannel(this.IOUnitName, this.DMXStartAddressRight_2, onValue);
                else this.buzzerHandler.SetDMXChannel(this.IOUnitName, this.DMXStartAddressRight_2, offValue);
                if (this.RightPlayerCounter >= 4) this.buzzerHandler.SetDMXChannel(this.IOUnitName, this.DMXStartAddressRight_3, onValue);
                else this.buzzerHandler.SetDMXChannel(this.IOUnitName, this.DMXStartAddressRight_3, offValue);
                if (this.RightPlayerCounter >= 5) this.buzzerHandler.SetDMXChannel(this.IOUnitName, this.DMXStartAddressRight_4, onValue);
                else this.buzzerHandler.SetDMXChannel(this.IOUnitName, this.DMXStartAddressRight_4, offValue);
            }
        }

        public virtual void DoBuzzer(
            PlayerSelection buzzeredPlayer) {
            switch (buzzeredPlayer) {
                case PlayerSelection.LeftPlayer:
                    this.LeftPlayerCounter++;
                    break;
                case PlayerSelection.RightPlayer:
                    this.RightPlayerCounter++;
                    break;
            }
            this.SetDMXValues();
        }

        public virtual void ReleaseBuzzer() {
            bool[] inputMask = new bool[8];
            if (this.LeftPlayerBuzzerChannel > 0 &&
                this.LeftPlayerBuzzerChannel <= inputMask.Length) inputMask[this.LeftPlayerBuzzerChannel - 1] = true;
            if (this.RightPlayerBuzzerChannel > 0 &&
                this.RightPlayerBuzzerChannel <= inputMask.Length) inputMask[this.RightPlayerBuzzerChannel - 1] = true;
            this.buzzerHandler.SetInputMask(this.IOUnitName, inputMask);
            this.buzzerHandler.ReleaseBuzzer(this.IOUnitName, WorkModes.EVENT);
        }

        public virtual void LockBuzzer() {
            this.buzzerHandler.LockBuzzer(this.IOUnitName);
        }
        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }
        public override void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score); }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }
        public void Vinsert_PlayJingleStart() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.PlayJingleStart(); }
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

        void buzzerHandler_BuzUnit_Buzzered(object sender, BuzzerIDParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_Buzzered);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_BuzUnit_Buzzered(object content) {
            BuzzerIDParam_EventArgs e = content as BuzzerIDParam_EventArgs;
            if (this.isActive &&
                e is BuzzerIDParam_EventArgs &&
                e.Arg.Name == this.IOUnitName) {
                if (e.Arg.BuzzerID == this.LeftPlayerBuzzerChannel) {
                    if (this.FlipPlayers) this.DoBuzzer(PlayerSelection.RightPlayer);
                    else this.DoBuzzer(PlayerSelection.LeftPlayer);
                }
                else if (e.Arg.BuzzerID == this.RightPlayerBuzzerChannel) {
                    if (this.FlipPlayers) this.DoBuzzer(PlayerSelection.LeftPlayer);
                    else this.DoBuzzer(PlayerSelection.RightPlayer);
                }
            }
        }

        void buzzerHandler_UnitConnectionStatusChanged(object sender, ConnectionStatusParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_UnitConnectionStatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        void buzzerHandler_UnitConnectionStatusRequest(object sender, ConnectionStatusParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_UnitConnectionStatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_UnitConnectionStatusChanged(object content) {
            ConnectionStatusParam_EventArgs e = content as ConnectionStatusParam_EventArgs;
            if (e is ConnectionStatusParam_EventArgs &&
                e.Arg.Name == this.IOUnitName) {
                this.ioUnitConnectionStatus = e.Arg.ConnectionStatus;
                this.checkIOUnitStatus();
                if (e.Arg.ConnectionStatus == Tools.NetContact.ClientStates.Connected) this.SetDMXValues();
            }
        }

        void buzzerHandler_UnitInfoListChanged(object sender, InfoParamArray_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_UnitInfoListChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_UnitInfoListChanged(object content) {
            InfoParamArray_EventArgs e = content as InfoParamArray_EventArgs;
            if (e is InfoParamArray_EventArgs) {
                this.ioUnitInfo = e;
                this.fillBuzzerUnitList(e.Arg);
            }
        }

        void buzzerHandler_BuzUnit_WorkmodeChanged(object sender, WorkModeParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_WorkmodeChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        void buzzerHandler_BuzUnit_WorkmodeRequest(object sender, WorkModeParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_WorkmodeChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_BuzUnit_WorkmodeChanged(object content) {
            WorkModeParam_EventArgs e = content as WorkModeParam_EventArgs;
            if (e is WorkModeParam_EventArgs &&
                e.Arg.Name == this.IOUnitName) {
                this.ioUnitWorkMode = e.Arg.WorkMode;
                this.checkIOUnitStatus();
            }
        }


        #endregion

    }
}
