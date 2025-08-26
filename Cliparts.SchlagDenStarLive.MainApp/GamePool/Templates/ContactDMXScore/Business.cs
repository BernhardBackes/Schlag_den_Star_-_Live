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
using System.Xml.Serialization;

using Cliparts.Tools.Base;

using Cliparts.IOnet.IOUnit.IONbase;
using Cliparts.IOnet.IOUnit.IONbuz;

using Cliparts.Serialization;

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;
using Cliparts.SchlagDenStarLive.MainApp.Settings;
using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.ContactDMXScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.ContactDMXScore {

    public class Business : _Base.Score.Business {

        #region Properties

        private MidiHandler.Business midiHandler;

        private BuzzerIO.Business buzzerHandler;

        private List<string> ioUnitNameList = new List<string>();
        public string[] IOUnitNameList { get { return this.ioUnitNameList.ToArray(); } }

        private Cliparts.IOnet.IOUnit.IONbase.InfoParamArray_EventArgs ioUnitInfo;

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

        private Cliparts.Tools.NetContact.ClientStates ioUnitConnectionStatus = Tools.NetContact.ClientStates.Missing;

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

        private IOnet.IOUnit.IONbuz.InputChannelStates leftPlayerChannelState = InputChannelStates.NA;
        public IOnet.IOUnit.IONbuz.InputChannelStates LeftPlayerChannelState {
            get { return leftPlayerChannelState; }
            set {
                if (this.leftPlayerChannelState != value) {
                    this.leftPlayerChannelState = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int leftPlayerDMXChannel = 1;
        public int LeftPlayerDMXChannel {
            get { return this.leftPlayerDMXChannel; }
            set {
                if (this.leftPlayerDMXChannel != value) {
                    if (value < 1) this.leftPlayerDMXChannel = 1;
                    else if (value > 255) this.leftPlayerDMXChannel = 8;
                    else this.leftPlayerDMXChannel = value;
                    this.on_PropertyChanged();
                    this.setDMXValues();
                }
            }
        }

        private bool leftPlayerDMXChannelClosed = false;
        [NotSerialized]
        public bool LeftPlayerDMXChannelClosed {
            get { return this.leftPlayerDMXChannelClosed; }
            set {
                if (this.leftPlayerDMXChannelClosed != value) {
                    this.leftPlayerDMXChannelClosed = value;
                    this.on_PropertyChanged();
                    if (value) {
                        this.Vinsert_PlayJingleBuzzer(PlayerSelection.RightPlayer);
                        this.sendMidi(PlayerSelection.LeftPlayer);
                    }
                    this.setDMXValues();
                    this.Vinsert_SetMaskBuzzer();
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

        private IOnet.IOUnit.IONbuz.InputChannelStates rightPlayerChannelState = InputChannelStates.NA;
        public IOnet.IOUnit.IONbuz.InputChannelStates RightPlayerChannelState {
            get { return rightPlayerChannelState; }
            set {
                if (this.rightPlayerChannelState != value) {
                    this.rightPlayerChannelState = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerDMXChannel = 2;
        public int RightPlayerDMXChannel {
            get { return this.rightPlayerDMXChannel; }
            set {
                if (this.rightPlayerDMXChannel != value) {
                    if (value < 1) this.rightPlayerDMXChannel = 1;
                    else if (value > 255) this.rightPlayerDMXChannel = 8;
                    else this.rightPlayerDMXChannel = value;
                    this.on_PropertyChanged();
                    this.setDMXValues();
                    this.Vinsert_SetMaskBuzzer();
                }
            }
        }

        private bool rightPlayerDMXChannelClosed = false;
        [NotSerialized]
        public bool RightPlayerDMXChannelClosed {
            get { return this.rightPlayerDMXChannelClosed; }
            set {
                if (this.rightPlayerDMXChannelClosed != value) {
                    this.rightPlayerDMXChannelClosed = value;
                    this.on_PropertyChanged();
                    if (value) {
                        this.Vinsert_PlayJingleBuzzer(PlayerSelection.RightPlayer);
                        this.sendMidi(PlayerSelection.RightPlayer);
                    }
                    this.setDMXValues();
                    this.Vinsert_SetMaskBuzzer();
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.ContactDMXScore'", typeIdentifier);
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

            this.midiHandler = midiHandler;

            this.buzzerHandler = buzzerHandler;
            this.buzzerHandler.BuzUnit_Buzzered += this.buzzerHandler_BuzUnit_Buzzered;
            this.buzzerHandler.BuzUnit_InputChannelChanged += this.buzzerHandler_BuzUnit_InputChannelChanged;
            this.buzzerHandler.BuzUnit_InputChannelRequest += this.buzzerHandler_BuzUnit_InputChannelRequest;
            this.buzzerHandler.Unit_ConnectionStatusChanged += this.buzzerHandler_UnitConnectionStatusChanged;
            this.buzzerHandler.Unit_ConnectionStatusRequest += this.buzzerHandler_UnitConnectionStatusRequest;
            this.buzzerHandler.Unit_InfoListChanged += this.buzzerHandler_UnitInfoListChanged;
            this.buzzerHandler.BuzUnit_WorkmodeChanged += this.buzzerHandler_BuzUnit_WorkmodeChanged;
            this.buzzerHandler.BuzUnit_WorkmodeRequest += this.buzzerHandler_BuzUnit_WorkmodeRequest;
            this.fillIOUnitList(this.buzzerHandler.UnitInfoList);
            this.buzzerHandler.RequestUnitConnectionStatus(this.IOUnitName);
            this.buzzerHandler.RequestUnitWorkMode(this.IOUnitName);

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);
        }

        public override void Init() {
            base.Init();
            this.LeftPlayerChannelState = InputChannelStates.NA;
            this.LeftPlayerDMXChannelClosed = false;
            this.RightPlayerChannelState = InputChannelStates.NA;
            this.RightPlayerDMXChannelClosed = false;
        }

        public override void Dispose() {
            base.Dispose();
            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.buzzerHandler.BuzUnit_Buzzered -= this.buzzerHandler_BuzUnit_Buzzered;
            this.buzzerHandler.BuzUnit_InputChannelChanged -= this.buzzerHandler_BuzUnit_InputChannelChanged;
            this.buzzerHandler.BuzUnit_InputChannelRequest -= this.buzzerHandler_BuzUnit_InputChannelRequest;
            this.buzzerHandler.Unit_ConnectionStatusChanged -= this.buzzerHandler_UnitConnectionStatusChanged;
            this.buzzerHandler.Unit_ConnectionStatusRequest -= this.buzzerHandler_UnitConnectionStatusRequest;
            this.buzzerHandler.Unit_InfoListChanged -= this.buzzerHandler_UnitInfoListChanged;
            this.buzzerHandler.BuzUnit_WorkmodeChanged -= this.buzzerHandler_BuzUnit_WorkmodeChanged;
            this.buzzerHandler.BuzUnit_WorkmodeRequest -= this.buzzerHandler_BuzUnit_WorkmodeRequest;
            this.buzzerHandler = null;
        }

        private void fillIOUnitList(
            Cliparts.IOnet.IOUnit.IONbase.InfoParam[] unitInfoList) {
            this.ioUnitNameList.Clear();
            if (unitInfoList is Cliparts.IOnet.IOUnit.IONbase.InfoParam[]) {
                foreach (Cliparts.IOnet.IOUnit.IONbase.InfoParam item in unitInfoList) {
                    if (item is Cliparts.IOnet.IOUnit.IONbase.InfoParam) this.ioUnitNameList.Add(item.Name);
                }
            }
            this.on_PropertyChanged("IOUnitNameList");
        }

        private void checkIOUnitStatus() {
            BuzzerIO.BuzzerUnitStates ioUnitStatus = BuzzerIO.BuzzerUnitStates.NotAvailable;
            if (this.ioUnitNameList.Contains(this.IOUnitName)) {
                switch (this.ioUnitConnectionStatus) {
                    case Cliparts.Tools.NetContact.ClientStates.Connected:
                        ioUnitStatus = BuzzerIO.BuzzerUnitStates.Connected;
                        break;
                    case Cliparts.Tools.NetContact.ClientStates.Connecting:
                        ioUnitStatus = BuzzerIO.BuzzerUnitStates.Connecting;
                        break;
                    case Cliparts.Tools.NetContact.ClientStates.Disconnected:
                        ioUnitStatus = BuzzerIO.BuzzerUnitStates.Disconnected;
                        break;
                    case Cliparts.Tools.NetContact.ClientStates.Missing:
                    default:
                        ioUnitStatus = BuzzerIO.BuzzerUnitStates.Missing;
                        break;
                }
                if (ioUnitStatus == BuzzerIO.BuzzerUnitStates.Connected) {
                    switch (this.ioUnitWorkMode) {
                        case WorkModes.BUZZER:
                            ioUnitStatus = BuzzerIO.BuzzerUnitStates.BuzzerMode;
                            break;
                        case WorkModes.EVENT:
                            ioUnitStatus = BuzzerIO.BuzzerUnitStates.EventMode;
                            break;
                        case WorkModes.LOCK:
                            ioUnitStatus = BuzzerIO.BuzzerUnitStates.Locked;
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
            if (this.buzzerHandler is BuzzerIO.Business) {
                this.buzzerHandler.RequestUnitConnectionStatus(unitName);
                this.buzzerHandler.RequestUnitWorkMode(unitName);
                this.buzzerHandler.RequestUnitInputChannels(unitName);
            }
        }

        public virtual void ReleaseBuzzer() {
            bool[] inputMask = new bool[8];
            if (this.LeftPlayerBuzzerChannel > 0 &&
                this.LeftPlayerBuzzerChannel <= inputMask.Length) inputMask[this.LeftPlayerBuzzerChannel - 1] = true;
            if (this.RightPlayerBuzzerChannel > 0 &&
                this.RightPlayerBuzzerChannel <= inputMask.Length) inputMask[this.RightPlayerBuzzerChannel - 1] = true;
            this.buzzerHandler.SetInputMask(this.IOUnitName, inputMask);
            this.buzzerHandler.ReleaseBuzzer(this.IOUnitName, WorkModes.BUZZER);
        }

        public virtual void LockBuzzer() {
            this.buzzerHandler.LockBuzzer(this.IOUnitName);
        }

        public void sendMidi(
            PlayerSelection buzzeredPlayer) {
            if (this.midiHandler is MidiHandler.Business) {
                switch (buzzeredPlayer) {
                    case PlayerSelection.LeftPlayer:
                        if (this.FlipPlayers) this.midiHandler.SendEvent("Rechts");
                        else this.midiHandler.SendEvent("Links");
                        break;
                    case PlayerSelection.RightPlayer:
                        if (this.FlipPlayers) this.midiHandler.SendEvent("Links");
                        else this.midiHandler.SendEvent("Rechts");
                        break;
                }
            }
        }

        private void setDMXValues() {
            if (this.buzzerHandler is BuzzerIO.Business) {
                if (this.FlipPlayers) {
                    if (this.LeftPlayerDMXChannelClosed) this.buzzerHandler.SetDMXChannel(this.IOUnitName, this.RightPlayerDMXChannel, new byte[] { 255 });
                    else this.buzzerHandler.SetDMXChannel(this.IOUnitName, this.RightPlayerDMXChannel, new byte[] { 0 });
                    if (this.RightPlayerDMXChannelClosed) this.buzzerHandler.SetDMXChannel(this.IOUnitName, this.LeftPlayerDMXChannel, new byte[] { 255 });
                    else this.buzzerHandler.SetDMXChannel(this.IOUnitName, this.LeftPlayerDMXChannel, new byte[] { 0 });
                }
                else {
                    if (this.LeftPlayerDMXChannelClosed) this.buzzerHandler.SetDMXChannel(this.IOUnitName, this.LeftPlayerDMXChannel, new byte[] { 255 });
                    else this.buzzerHandler.SetDMXChannel(this.IOUnitName, this.LeftPlayerDMXChannel, new byte[] { 0 });
                    if (this.RightPlayerDMXChannelClosed) this.buzzerHandler.SetDMXChannel(this.IOUnitName, this.RightPlayerDMXChannel, new byte[] { 255 });
                    else this.buzzerHandler.SetDMXChannel(this.IOUnitName, this.RightPlayerDMXChannel, new byte[] { 0 });
                }
            }
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }
        public override void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score); }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }

        public void Vinsert_MaskIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.ToIn(); }
        public void Vinsert_SetMaskBuzzer() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                if (this.FlipPlayers) {
                    if (this.LeftPlayerDMXChannelClosed) this.insertScene.SetBuzzerRight();
                    else this.insertScene.ResetRightBuzzer();
                    if (this.RightPlayerDMXChannelClosed) this.insertScene.SetBuzzerLeft();
                    else this.insertScene.ResetLeftBuzzer();
                }
                else {
                    if (this.LeftPlayerDMXChannelClosed) this.insertScene.SetBuzzerLeft();
                    else this.insertScene.ResetLeftBuzzer();
                    if (this.RightPlayerDMXChannelClosed) this.insertScene.SetBuzzerRight();
                    else this.insertScene.ResetRightBuzzer();
                }
            }
        }
        public void Vinsert_MaskOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.ToOut(); }

        public void Vinsert_PlayJingleBuzzer(
            PlayerSelection buzzeredPlayer) {
            switch (buzzeredPlayer) {
                case PlayerSelection.LeftPlayer:
                    if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.PlayJingleBuzzerLeft(); 
                    break;
                case PlayerSelection.RightPlayer:
                    if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.PlayJingleBuzzerRight(); 
                    break;
            }
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

        protected override void on_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.on_PropertyChanged(sender, e);
            if (e.PropertyName == "FlipPlayers") {
                this.setDMXValues();
                this.Vinsert_SetMaskBuzzer();
            }
        }

        #endregion

        #region Events.Incoming

        void buzzerHandler_BuzUnit_Buzzered(object sender, IOnet.IOUnit.IONbuz.BuzzerIDParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_Buzzered);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_BuzUnit_Buzzered(object content) {
            IOnet.IOUnit.IONbuz.BuzzerIDParam_EventArgs e = content as IOnet.IOUnit.IONbuz.BuzzerIDParam_EventArgs;
            if (this.isActive &&
                e is IOnet.IOUnit.IONbuz.BuzzerIDParam_EventArgs &&
                e.Arg.Name == this.IOUnitName) {
                if (e.Arg.BuzzerID == this.LeftPlayerBuzzerChannel) {
                    if (this.FlipPlayers) {
                        this.RightPlayerDMXChannelClosed = true;
                    }
                    else {
                        this.LeftPlayerDMXChannelClosed = true;
                    }
                }
                if (e.Arg.BuzzerID == this.RightPlayerBuzzerChannel) {
                    if (this.FlipPlayers) {
                        this.LeftPlayerDMXChannelClosed = true;
                    }
                    else {
                        this.RightPlayerDMXChannelClosed = true;
                    }
                }
            }
        }

        void buzzerHandler_BuzUnit_InputChannelChanged(object sender, IOnet.IOUnit.IONbuz.InputChannelParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_InputChannelChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        void buzzerHandler_BuzUnit_InputChannelRequest(object sender, IOnet.IOUnit.IONbuz.InputChannelParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_InputChannelChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_BuzUnit_InputChannelChanged(object content) {
            IOnet.IOUnit.IONbuz.InputChannelParam_EventArgs e = content as IOnet.IOUnit.IONbuz.InputChannelParam_EventArgs;
            if (this.isActive &&
                e is IOnet.IOUnit.IONbuz.InputChannelParam_EventArgs &&
                e.Arg.Name == this.IOUnitName) {
                if (e.Arg.InputChannel.Length > this.LeftPlayerBuzzerChannel) {
                    if (this.FlipPlayers) this.RightPlayerChannelState = e.Arg.InputChannel[this.LeftPlayerBuzzerChannel - 1];
                    else this.LeftPlayerChannelState = e.Arg.InputChannel[this.LeftPlayerBuzzerChannel - 1];
                }
                if (e.Arg.InputChannel.Length > this.RightPlayerBuzzerChannel) {
                    if (this.FlipPlayers) this.LeftPlayerChannelState = e.Arg.InputChannel[this.RightPlayerBuzzerChannel - 1];
                    else this.RightPlayerChannelState = e.Arg.InputChannel[this.RightPlayerBuzzerChannel - 1];
                }
            }
        }

        void buzzerHandler_UnitConnectionStatusChanged(object sender, IOnet.IOUnit.IONbase.ConnectionStatusParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_UnitConnectionStatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        void buzzerHandler_UnitConnectionStatusRequest(object sender, IOnet.IOUnit.IONbase.ConnectionStatusParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_UnitConnectionStatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_UnitConnectionStatusChanged(object content) {
            IOnet.IOUnit.IONbase.ConnectionStatusParam_EventArgs e = content as IOnet.IOUnit.IONbase.ConnectionStatusParam_EventArgs;
            if (e is IOnet.IOUnit.IONbase.ConnectionStatusParam_EventArgs &&
                e.Arg.Name == this.IOUnitName) {
                this.ioUnitConnectionStatus = e.Arg.ConnectionStatus;
                this.checkIOUnitStatus();
                if (e.Arg.ConnectionStatus == Tools.NetContact.ClientStates.Connected) this.setDMXValues();
            }
        }

        void buzzerHandler_UnitInfoListChanged(object sender, IOnet.IOUnit.IONbase.InfoParamArray_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_UnitInfoListChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_UnitInfoListChanged(object content) {
            IOnet.IOUnit.IONbase.InfoParamArray_EventArgs e = content as IOnet.IOUnit.IONbase.InfoParamArray_EventArgs;
            if (e is IOnet.IOUnit.IONbase.InfoParamArray_EventArgs) {
                this.ioUnitInfo = e;
                this.fillIOUnitList(e.Arg);
            }
        }

        void buzzerHandler_BuzUnit_WorkmodeChanged(object sender, IOnet.IOUnit.IONbuz.WorkModeParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_WorkmodeChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        void buzzerHandler_BuzUnit_WorkmodeRequest(object sender, IOnet.IOUnit.IONbuz.WorkModeParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_WorkmodeChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_BuzUnit_WorkmodeChanged(object content) {
            IOnet.IOUnit.IONbuz.WorkModeParam_EventArgs e = content as IOnet.IOUnit.IONbuz.WorkModeParam_EventArgs;
            if (e is IOnet.IOUnit.IONbuz.WorkModeParam_EventArgs &&
                e.Arg.Name == this.IOUnitName) {
                this.ioUnitWorkMode = e.Arg.WorkMode;
                this.checkIOUnitStatus();
            }
        }

        #endregion

    }
}
