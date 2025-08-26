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

using Cliparts.IOnet.IOUnit.IONbuz;

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;
using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.FinishBuzzerRGBScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.FinishBuzzerRGBScore {

    public class Business : _Base.Score.Business {

        #region Properties

        private BuzzerIO.Business buzzerHandler;

        private List<string> ioUnitNameList = new List<string>();
        public string[] IOUnitNameList { get { return this.ioUnitNameList.ToArray(); } }

        private IOnet.IOUnit.IONbase.InfoParamArray_EventArgs ioUnitInfo;

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

        public enum FinishModes { Reaching, Crossing }
        private FinishModes finishMode = FinishModes.Reaching;
        public FinishModes FinishMode {
            get { return this.finishMode; }
            set {
                if (this.finishMode != value) {
                    this.finishMode = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool leftFinishReached = false;
        public bool LeftFinishReached {
            get { return this.leftFinishReached; }
            set {
                if (this.leftFinishReached != value) {
                    this.leftFinishReached = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool rightFinishReached = false;
        public bool RightFinishReached {
            get { return this.rightFinishReached; }
            set {
                if (this.rightFinishReached != value) {
                    this.rightFinishReached = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool swapTracks;
        public bool SwapTracks {
            get { return this.swapTracks; }
            set {
                if (this.swapTracks != value) {
                    this.swapTracks = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private PlayerSelection buzzeredPlayer = PlayerSelection.NotSelected;

        private PlayerSelection firstFinisher = PlayerSelection.NotSelected;
        public PlayerSelection FirstFinisher
        {
            get { return this.firstFinisher; }
            private set
            {
                if (this.firstFinisher != value)
                {
                    this.firstFinisher = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private Tools.DMX.DMXNet dMX = new Tools.DMX.DMXNet();
        private byte[] universe = new byte[512];

        private int leftPlayerDMXStartchannel = 250;
        public int LeftPlayerDMXStartchannel
        {
            get { return this.leftPlayerDMXStartchannel; }
            set
            {
                if (this.leftPlayerDMXStartchannel != value)
                {
                    if (value < 1) this.leftPlayerDMXStartchannel = 1;
                    else if (value > 256) this.leftPlayerDMXStartchannel = 256;
                    else this.leftPlayerDMXStartchannel = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerDMXStartchannel = 250;
        public int RightPlayerDMXStartchannel
        {
            get { return this.rightPlayerDMXStartchannel; }
            set
            {
                if (this.rightPlayerDMXStartchannel != value)
                {
                    if (value < 1) this.rightPlayerDMXStartchannel = 1;
                    else if (value > 256) this.rightPlayerDMXStartchannel = 256;
                    else this.rightPlayerDMXStartchannel = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private Color leftOffColor = Color.Black;
        public Color LeftOffColor
        {
            get { return this.leftOffColor; }
            set
            {
                if (this.leftOffColor != value)
                {
                    this.leftOffColor = value;
                    this.on_PropertyChanged();
                    if (!suppressDMXUpdate)
                    {
                        this.SetLeftPlayerOff();
                    }
                }
            }
        }

        private Color leftOnColor = Color.Black;
        public Color LeftOnColor
        {
            get { return this.leftOnColor; }
            set
            {
                if (this.leftOnColor != value)
                {
                    this.leftOnColor = value;
                    this.on_PropertyChanged();
                    if (!suppressDMXUpdate)
                    {
                        this.SetLeftPlayerOn();
                    }
                }
            }
        }

        private Color rightOffColor = Color.Black;
        public Color RightOffColor
        {
            get { return this.rightOffColor; }
            set
            {
                if (this.rightOffColor != value)
                {
                    this.rightOffColor = value;
                    this.on_PropertyChanged();
                    if (!suppressDMXUpdate)
                    {
                        this.SetRightPlayerOff();
                    }
                }
            }
        }


        private Color rightOnColor = Color.Black;
        public Color RightOnColor
        {
            get { return this.rightOnColor; }
            set
            {
                if (this.rightOnColor != value)
                {
                    this.rightOnColor = value;
                    this.on_PropertyChanged();
                    if (!suppressDMXUpdate)
                    {
                        this.SetRightPlayerOn();
                    }
                }
            }
        }

        private bool suppressDMXUpdate = true;

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

            this.ClassInfo = string.Format("'{0}' of 'Templates.FinishBuzzerRGBScore'", typeIdentifier);
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
            this.buzzerHandler.BuzUnit_InputChannelChanged += this.buzzerHandler_BuzUnit_InputChannelChanged;
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
            this.insertScene.PropertyChanged += this.insertScene_PropertyChanged;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);

            this.AllLightsBlack();
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
            this.insertScene.PropertyChanged -= this.insertScene_PropertyChanged;
        }

        public override void ResetData() {
            base.ResetData();
            this.buzzeredPlayer = PlayerSelection.NotSelected;
            this.FirstFinisher = PlayerSelection.NotSelected;
            this.LeftFinishReached = false;
            this.RightFinishReached = false;
            this.SetDMXValues();
        }

        public override void Next() {
            this.buzzeredPlayer = PlayerSelection.NotSelected;
            this.FirstFinisher = PlayerSelection.NotSelected;
            this.LeftFinishReached = false;
            this.RightFinishReached = false;
            this.SetDMXValues();
        }

        private void fillIOUnitList(
            IOnet.IOUnit.IONbase.InfoParam[] unitInfoList) {
            this.ioUnitNameList.Clear();
            if (unitInfoList is IOnet.IOUnit.IONbase.InfoParam[]) {
                foreach (IOnet.IOUnit.IONbase.InfoParam item in unitInfoList) {
                    if (item is IOnet.IOUnit.IONbase.InfoParam) this.ioUnitNameList.Add(item.Name);
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
            if (this.buzzerHandler is BuzzerIO.Business) {
                this.buzzerHandler.RequestUnitConnectionStatus(unitName);
                this.buzzerHandler.RequestUnitWorkMode(unitName);
            }
        }

        private void setDMXValues() {
            this.buzzerHandler.SetDMXMode(this.IOUnitName, IOnet.IOUnit.IONbuzDX.DMXBuzzerModes.CHANNEL);
            this.buzzerHandler.SetDMXSettings(this.IOUnitName, 0, 255);
        }

        public virtual void PassFinishLine(
            PlayerSelection buzzeredPlayer) {
            if (buzzeredPlayer != PlayerSelection.NotSelected &&
                buzzeredPlayer != this.buzzeredPlayer) {
                if (this.buzzeredPlayer == PlayerSelection.NotSelected) {
                    this.buzzeredPlayer = buzzeredPlayer;
                    if (this.SwapTracks) {
                        switch (buzzeredPlayer) {
                            case PlayerSelection.LeftPlayer:
                                this.FirstFinisher = PlayerSelection.RightPlayer;
                                break;
                            case PlayerSelection.RightPlayer:
                                this.FirstFinisher = PlayerSelection.LeftPlayer;
                                break;
                        }
                    }
                    else this.FirstFinisher = buzzeredPlayer;
                    if (this.FirstFinisher == PlayerSelection.LeftPlayer)
                        this.insertScene.PlayJingle(Insert.Jingles.Buzzer);
                    else if (this.FirstFinisher == PlayerSelection.RightPlayer)
                        this.insertScene.PlayJingle(Insert.Jingles.Buzzer);
                    this.SetDMXValues();
                }
                else {
                    this.LockBuzzer();
                }
            }
        }

        public virtual void ReleaseBuzzer() {
            bool[] inputMask = new bool[8];
            if (this.LeftPlayerBuzzerChannel > 0 &&
                this.LeftPlayerBuzzerChannel <= inputMask.Length) inputMask[this.LeftPlayerBuzzerChannel - 1] = true;
            if (this.RightPlayerBuzzerChannel > 0 &&
                this.RightPlayerBuzzerChannel <= inputMask.Length) inputMask[this.RightPlayerBuzzerChannel - 1] = true;
            this.buzzerHandler.SetInputMask(this.IOUnitName, inputMask);
            this.buzzerHandler.ReleaseBuzzer(this.IOUnitName, IOnet.IOUnit.IONbuz.WorkModes.BUZZER);
            this.SetDMXValues();
        }
        public virtual void ReleaseBuzzer(
            PlayerSelection track) {
            bool[] inputMask = new bool[8];
            switch (track) {
                case PlayerSelection.LeftPlayer:
                    if (this.LeftPlayerBuzzerChannel > 0 &&
                        this.LeftPlayerBuzzerChannel <= inputMask.Length) inputMask[this.LeftPlayerBuzzerChannel - 1] = true;
                    break;
                case PlayerSelection.RightPlayer:
                    if (this.RightPlayerBuzzerChannel > 0 &&
                        this.RightPlayerBuzzerChannel <= inputMask.Length) inputMask[this.RightPlayerBuzzerChannel - 1] = true;
                    break;
            }
            this.buzzerHandler.SetDMXMode(this.IOUnitName, IOnet.IOUnit.IONbuzDX.DMXBuzzerModes.CHANNEL);
            this.buzzerHandler.SetInputMask(this.IOUnitName, inputMask);
            this.buzzerHandler.ReleaseBuzzer(this.IOUnitName, WorkModes.EVENT);
        }

        public virtual void LockBuzzer() {
            this.buzzerHandler.LockBuzzer(this.IOUnitName);
        }

        internal void SetDMXValues() {
            switch (this.FirstFinisher) {
                case PlayerSelection.LeftPlayer:
                    if (this.SwapTracks) {
                        this.SetLeftPlayerOff();
                        this.SetRightPlayerOn();
                    }
                    else {
                        this.SetLeftPlayerOn();
                        this.SetRightPlayerOff();
                    }
                    break;
                case PlayerSelection.RightPlayer:
                    if (this.SwapTracks) {
                        this.SetLeftPlayerOn();
                        this.SetRightPlayerOff();
                    }
                    else {
                        this.SetLeftPlayerOff();
                        this.SetRightPlayerOn();
                    }
                    break;
                default:
                    this.SetLeftPlayerOff();
                    this.SetRightPlayerOff();
                    break;
            }
        }

        internal void AllLightsBlack()
        {
            byte[] valueList = new byte[512];
            this.SetDMXValues(1, valueList);
        }

        internal void SetLeftPlayerOn()
        {
            byte[] valueList = new byte[] { this.LeftOnColor.R, this.LeftOnColor.G, this.LeftOnColor.B };
            this.SetDMXValues(this.LeftPlayerDMXStartchannel, valueList);
        }
        internal void SetLeftPlayerOff()
        {
            byte[] valueList = new byte[] { this.LeftOffColor.R, this.LeftOffColor.G, this.LeftOffColor.B };
            this.SetDMXValues(this.LeftPlayerDMXStartchannel, valueList);
        }

        internal void SetRightPlayerOn()
        {
            byte[] valueList = new byte[] { this.RightOnColor.R, this.RightOnColor.G, this.RightOnColor.B };
            this.SetDMXValues(this.RightPlayerDMXStartchannel, valueList);
        }
        internal void SetRightPlayerOff()
        {
            byte[] valueList = new byte[] { this.RightOffColor.R, this.RightOffColor.G, this.RightOffColor.B };
            this.SetDMXValues(this.RightPlayerDMXStartchannel, valueList);
        }

        private void SetDMXValues(
            int startChannel,
            byte[] values) {
            try {
                if (this.buzzerHandler is BuzzerIO.Business) this.buzzerHandler.SetDMXChannel(this.IOUnitName, startChannel, values);
                if (this.dMX is Tools.DMX.DMXNet) {
                    byte startIndex = Convert.ToByte(startChannel - 1);
                    if (startIndex + values.Length <= this.universe.Length) Array.Copy(values, 0, this.universe, startIndex, values.Length);
                    this.dMX.SetDmxDataToEspNet(0, 0, this.universe);
                }
            }
            catch (Exception) {}
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
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
                    if (this.FinishMode == FinishModes.Reaching) this.PassFinishLine(PlayerSelection.LeftPlayer);
                    this.LeftFinishReached = true;
                }
                else if (e.Arg.BuzzerID == this.RightPlayerBuzzerChannel) {
                    if (this.FinishMode == FinishModes.Reaching) this.PassFinishLine(PlayerSelection.RightPlayer);
                    this.RightFinishReached = true;
                }
            }
        }

        private void buzzerHandler_BuzUnit_InputChannelChanged(object sender, InputChannelParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_InputChannelChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_BuzUnit_InputChannelChanged(object content) {
            InputChannelParam_EventArgs e = content as InputChannelParam_EventArgs;
            if (this.isActive &&
                e is InputChannelParam_EventArgs &&
                e.Arg.Name == this.IOUnitName &&
                this.FinishMode == FinishModes.Crossing) {
                if (this.LeftFinishReached &&
                    e.Arg.InputChannel[this.LeftPlayerBuzzerChannel - 1] == InputChannelStates.UP) this.PassFinishLine(PlayerSelection.LeftPlayer);
                if (this.RightFinishReached &&
                    e.Arg.InputChannel[this.RightPlayerBuzzerChannel - 1] == InputChannelStates.UP) this.PassFinishLine(PlayerSelection.RightPlayer);
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

        protected void insertScene_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timeToBeat_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timeToBeat_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
            }
        }

        #endregion

    }
}
