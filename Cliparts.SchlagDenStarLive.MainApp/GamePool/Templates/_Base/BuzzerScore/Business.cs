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

using Cliparts.Tools.Base;

using Cliparts.IOnet.IOUnit.IONbase;
using Cliparts.IOnet.IOUnit.IONbuz;

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;
using Cliparts.SchlagDenStarLive.MainApp.Settings;
using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;
using System.Xml.Serialization;
using Cliparts.VRemote4.HandlerSi;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.BuzzerScore {

    public class Business : _Base.Business {

        #region Properties

        protected BuzzerIO.Business buzzerHandler;

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

        protected PlayerSelection buzzeredPlayer = PlayerSelection.NotSelected;
        [XmlIgnore]
        public virtual PlayerSelection BuzzeredPlayer {
            get { return this.buzzeredPlayer; }
            protected set {
                if (this.buzzeredPlayer != value) {
                    this.buzzeredPlayer = value;
                    this.Vinsert_SetBorderBuzzer(value);
                    this.Vstage_SetBuzzer(value);
                    this.on_PropertyChanged();
                    switch (buzzeredPlayer)
                    {
                        case PlayerSelection.LeftPlayer:
                            this.SetLeftPlayerOn();
                            this.SetRightPlayerOff();
                            break;
                        case PlayerSelection.RightPlayer:
                            this.SetLeftPlayerOff();
                            this.SetRightPlayerOn();
                            break;
                        case PlayerSelection.NotSelected:
                        default:
                            this.SetLeftPlayerOff();
                            this.SetRightPlayerOff();
                            break;
                    }
                }
            }
        }

        private int timeoutPositionX = 0;
        public int TimeoutPositionX {
            get { return this.timeoutPositionX; }
            set {
                if (this.timeoutPositionX != value) {
                    this.timeoutPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeout();
                }
            }
        }

        private int timeoutPositionY = 0;
        public int TimeoutPositionY {
            get { return this.timeoutPositionY; }
            set {
                if (this.timeoutPositionY != value) {
                    this.timeoutPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeout();
                }
            }
        }

        private bool timeoutIsVisible = true;
        public bool TimeoutIsVisible {
            get { return this.timeoutIsVisible; }
            set {
                if (this.timeoutIsVisible != value) {
                    this.timeoutIsVisible = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeout();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.Timeout.Duration timeoutDuration = VentuzScenes.GamePool._Modules.Timeout.Duration.FiveSeconds;
        public VentuzScenes.GamePool._Modules.Timeout.Duration TimeoutDuration {
            get { return this.timeoutDuration; }
            set {
                if (this.timeoutDuration != value) {
                    this.timeoutDuration = value;
                    this.on_PropertyChanged();
                }
            }
        }

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

        private VentuzScenes.GamePool._Modules.Score.Styles scoreStyle = VentuzScenes.GamePool._Modules.Score.Styles.ThreeDots;
        public VentuzScenes.GamePool._Modules.Score.Styles ScoreStyle {
            get { return this.scoreStyle; }
            set {
                if (this.scoreStyle != value) {
                    this.scoreStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetScore();
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

        private bool flipPlayer;
        public bool FlipPlayers {
            get { return this.flipPlayer; }
            set {
                if (this.flipPlayer != value) {
                    this.flipPlayer = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetScore();
                }
            }
        }

        private bool suppressDMXUpdate = true;

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

            this.suppressDMXUpdate = false;
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
        }

        public override void ResetData() {
            base.ResetData();
            this.LockBuzzer();
            this.LeftPlayerScore = 0;
            this.RightPlayerScore = 0;
            this.Vinsert_SetScore();
            this.Vstage_SetScore();
            this.SetLeftPlayerOff();
            this.SetRightPlayerOff();
        }

        public override void Next() {
            base.Next();
            this.LockBuzzer();
            this.BuzzeredPlayer = PlayerSelection.NotSelected;
        }

        private void fillBuzzerUnitList(
            Cliparts.IOnet.IOUnit.IONbase.InfoParam[] unitInfoList) {
            this.ioUnitNameList.Clear();
            if (unitInfoList is Cliparts.IOnet.IOUnit.IONbase.InfoParam[]) {
                foreach (Cliparts.IOnet.IOUnit.IONbase.InfoParam item in unitInfoList) {
                    if (item is Cliparts.IOnet.IOUnit.IONbase.InfoParam) this.ioUnitNameList.Add(item.Name);
                }
            }
            this.on_PropertyChanged("IOUnitList");
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
            if (buzzerHandler is BuzzerIO.Business) {
                this.buzzerHandler.RequestUnitConnectionStatus(unitName);
                this.buzzerHandler.RequestUnitWorkMode(unitName);
            }
        }

        public virtual void DoBuzzer(
            PlayerSelection buzzeredPlayer) {
            if (this.BuzzeredPlayer == PlayerSelection.NotSelected &&
                this.BuzzeredPlayer != buzzeredPlayer) {
                this.BuzzeredPlayer = buzzeredPlayer;
                this.Vinsert_Buzzer(buzzeredPlayer);
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
            this.BuzzeredPlayer = PlayerSelection.NotSelected;
            this.Vinsert_StopTimeout();
        }

        public virtual void LockBuzzer() {
            this.buzzerHandler.LockBuzzer(this.IOUnitName);
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

        internal void SetDMXValues(
            int startChannel,
            byte[] values)
        {
            try
            {
                if (this.buzzerHandler is BuzzerIO.Business) this.buzzerHandler.SetDMXChannel(this.IOUnitName, startChannel, values);
                if (this.dMX is Tools.DMX.DMXNet)
                {
                    byte startIndex = Convert.ToByte(startChannel - 1);
                    if (startIndex + values.Length <= this.universe.Length) Array.Copy(values, 0, this.universe, startIndex, values.Length);
                    this.dMX.SetDmxDataToEspNet(0, 0, this.universe);
                }
            }
            catch (Exception) { }
        }

        public virtual void Vinsert_ScoreIn() { }
        public void Vinsert_ScoreIn(
            VentuzScenes.GamePool._Modules.Score scene) {
            if (scene is VentuzScenes.GamePool._Modules.Score) {
                this.Vinsert_SetScore(scene);
                scene.ToIn();
            }
        }
        public virtual void Vinsert_SetScore() { }
        public virtual void Vinsert_SetScore(VentuzScenes.GamePool._Modules.Score scene) { this.Vinsert_SetScore(scene, this.LeftPlayerScore, this.RightPlayerScore); }
        public void Vinsert_SetScore(
            VentuzScenes.GamePool._Modules.Score scene,
            int leftPlayerScore,
            int rightPlayerScore) {
            if (scene is VentuzScenes.GamePool._Modules.Score) {
                scene.SetPositionX(this.ScorePositionX);
                scene.SetPositionY(this.ScorePositionY);
                scene.SetFlipPosition(this.FlipPlayers);
                scene.SetStyle(this.ScoreStyle);
                scene.SetLeftTopName(this.LeftPlayerName);
                scene.SetLeftTopScore(leftPlayerScore);
                scene.SetRightBottomName(this.RightPlayerName);
                scene.SetRightBottomScore(rightPlayerScore);
            }
        }
        public virtual void Vinsert_ScoreOut() { }
        public void Vinsert_ScoreOut(
            VentuzScenes.GamePool._Modules.Score scene) {
            if (scene is VentuzScenes.GamePool._Modules.Score) scene.ToOut();
        }

        public virtual void Vinsert_Buzzer(PlayerSelection buzzeredPlayer) { }
        public void Vinsert_Buzzer(
            VentuzScenes.GamePool._Modules.Timeout scene,
            PlayerSelection buzzeredPlayer) {
            if (scene is VentuzScenes.GamePool._Modules.Timeout) {
                this.Vinsert_SetTimeout(scene);
                switch (buzzeredPlayer) {
                    case PlayerSelection.LeftPlayer:
                        if (this.TimeoutDuration == VentuzScenes.GamePool._Modules.Timeout.Duration.FiveSeconds) scene.BuzzerLeft(this.TimeoutDuration, this.TimeoutIsVisible);
                        else {
                            scene.BuzzerSoundLeft();
                            scene.StartCenter(this.TimeoutDuration, this.TimeoutIsVisible);
                        }
                        break;
                    case PlayerSelection.RightPlayer:
                        if (this.TimeoutDuration == VentuzScenes.GamePool._Modules.Timeout.Duration.FiveSeconds) scene.BuzzerRight(this.TimeoutDuration, this.TimeoutIsVisible);
                        else {
                            scene.BuzzerSoundRight();
                            scene.StartCenter(this.TimeoutDuration, this.TimeoutIsVisible);
                        }
                        break;
                    case PlayerSelection.NotSelected:
                    default:
                        scene.Stop();
                        break;
                }
            }
        }
        public virtual void Vinsert_SetTimeout() { }
        public void Vinsert_SetTimeout(
            VentuzScenes.GamePool._Modules.Timeout scene) {
            if (scene is VentuzScenes.GamePool._Modules.Timeout) {
                scene.SetPositionX(this.TimeoutPositionX);
                scene.SetPositionY(this.TimeoutPositionY);
                scene.SetIsVisible(this.TimeoutIsVisible);
            }
        }
        public virtual void Vinsert_StopTimeout() { }
        public void Vinsert_StopTimeout(
            VentuzScenes.GamePool._Modules.Timeout scene) {
            if (scene is VentuzScenes.GamePool._Modules.Timeout) scene.Clear();
        }
        public virtual void Vinsert_SetBorderBuzzer() {}
        public virtual void Vinsert_SetBorderBuzzer(PlayerSelection buzzeredPlayer) { }
        public void Vinsert_SetBorderBuzzer(
            VentuzScenes.GamePool._Modules.Border scene,
            PlayerSelection buzzeredPlayer) {
            if (scene is VentuzScenes.GamePool._Modules.Border) {
                switch (buzzeredPlayer) {
                    case PlayerSelection.LeftPlayer:
                        scene.SetBuzzerLeft();
                        break;
                    case PlayerSelection.RightPlayer:
                        scene.SetBuzzerRight();
                        break;
                    case PlayerSelection.NotSelected:
                    default:
                        scene.ResetBuzzer();
                        break;
                }
            }
        }

        public virtual void Vstage_Init() {
            this.Vstage_GamescoreIn();
            this.Vstage_SetBuzzer(PlayerSelection.NotSelected);
        }
        public virtual void Vstage_GamescoreIn() {
            this.Vstage_SetScore();
            this.hostMasterScene.GameScoreIn();
            this.leftPlayerMasterScene.GameScoreIn();
            this.rightPlayerMasterScene.GameScoreIn();
        }
        public virtual void Vstage_SetScore() {
            this.hostMasterScene.SetLeftPlayerGameScore(this.LeftPlayerScore);
            this.leftPlayerMasterScene.SetLeftPlayerGameScore(this.LeftPlayerScore);
            this.rightPlayerMasterScene.SetLeftPlayerGameScore(this.LeftPlayerScore);
            this.hostMasterScene.SetRightPlayerGameScore(this.RightPlayerScore);
            this.leftPlayerMasterScene.SetRightPlayerGameScore(this.RightPlayerScore);
            this.rightPlayerMasterScene.SetRightPlayerGameScore(this.RightPlayerScore);
        }
        public virtual void Vstage_SetBuzzer(
            PlayerSelection buzzeredPlayer) {
            switch (buzzeredPlayer) {
                case PlayerSelection.LeftPlayer:
                    if (this.hostMasterScene is Scene) this.hostMasterScene.SetBuzzerLeft();
                    if (this.leftPlayerMasterScene is Scene) this.leftPlayerMasterScene.SetBuzzerLeft();
                    if (this.rightPlayerMasterScene is Scene) this.rightPlayerMasterScene.SetBuzzerLeft();
                    break;
                case PlayerSelection.RightPlayer:
                    if (this.hostMasterScene is Scene) this.hostMasterScene.SetBuzzerRight();
                    if (this.leftPlayerMasterScene is Scene) this.leftPlayerMasterScene.SetBuzzerRight();
                    if (this.rightPlayerMasterScene is Scene) this.rightPlayerMasterScene.SetBuzzerRight();
                    break;
                case PlayerSelection.NotSelected:
                default:
                    if (this.hostMasterScene is Scene) this.hostMasterScene.SetBuzzerOut();
                    if (this.leftPlayerMasterScene is Scene) this.leftPlayerMasterScene.SetBuzzerOut();
                    if (this.rightPlayerMasterScene is Scene) this.rightPlayerMasterScene.SetBuzzerOut();
                    break;
            }
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming

        void buzzerHandler_BuzUnit_Buzzered(object sender, IOnet.IOUnit.IONbuz.BuzzerIDParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_Buzzered);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_buzzerHandler_BuzUnit_Buzzered(object content) {
            BuzzerIDParam_EventArgs e = content as IOnet.IOUnit.IONbuz.BuzzerIDParam_EventArgs;
            if (this.isActive &&
                e is IOnet.IOUnit.IONbuz.BuzzerIDParam_EventArgs &&
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
                if (e.Arg.ConnectionStatus == Tools.NetContact.ClientStates.Connected) this.AllLightsBlack();
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
                this.fillBuzzerUnitList(e.Arg);
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
