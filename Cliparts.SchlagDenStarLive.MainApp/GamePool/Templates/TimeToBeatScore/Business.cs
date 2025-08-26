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

using Cliparts.IOnet.IOUnit.IONbase;
using Cliparts.IOnet.IOUnit.IONbuz;

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimeToBeatScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimeToBeatScore {

    public class Business : _Base.Score.Business {

        #region Properties

        private int timeToBeatPositionX = 0;
        public int TimeToBeatPositionX {
            get { return this.timeToBeatPositionX; }
            set {
                if (this.timeToBeatPositionX != value) {
                    this.timeToBeatPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeToBeat();
                }
            }
        }

        private int timeToBeatPositionY = 0;
        public int TimeToBeatPositionY {
            get { return this.timeToBeatPositionY; }
            set {
                if (this.timeToBeatPositionY != value) {
                    this.timeToBeatPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeToBeat();
                }
            }
        }

        private int timeToBeatStopTime = 5999;
        public int TimeToBeatStopTime {
            get { return this.timeToBeatStopTime; }
            set {
                if (this.timeToBeatStopTime != value) {
                    this.timeToBeatStopTime = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeToBeat();
                }
            }
        }

        private int timeToBeatSentenceTime = 0;
        public int TimeToBeatSentenceTime {
            get { return this.timeToBeatSentenceTime; }
            set {
                if (this.timeToBeatSentenceTime != value) {
                    this.timeToBeatSentenceTime = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeToBeatSentenceTime();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.TimeToBeat.Styles timeToBeatStyle = VentuzScenes.GamePool._Modules.TimeToBeat.Styles.StopwatchName;
        public VentuzScenes.GamePool._Modules.TimeToBeat.Styles TimeToBeatStyle {
            get { return this.timeToBeatStyle; }
            set {
                if (this.timeToBeatStyle != value) {
                    this.timeToBeatStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeToBeat();
                }
            }
        }

        private double timeToBeatCurrentTime = -1;
        public double TimeToBeatCurrentTime {
            get { return this.timeToBeatCurrentTime; }
            protected set {
                if (this.timeToBeatCurrentTime != value) {
                    this.timeToBeatCurrentTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int sportIconsPositionX = 0;
        public int SportIconsPositionX {
            get { return this.sportIconsPositionX; }
            set {
                if (this.sportIconsPositionX != value) {
                    this.sportIconsPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetSportIcons();
                }
            }
        }

        private int sportIconsPositionY = 0;
        public int SportIconsPositionY {
            get { return this.sportIconsPositionY; }
            set {
                if (this.sportIconsPositionY != value) {
                    this.sportIconsPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetSportIcons();
                }
            }
        }

        private bool useSportIcons = false;
        public bool UseSportIcons {
            get { return this.useSportIcons; }
            set {
                if (this.useSportIcons != value) {
                    this.useSportIcons = value;
                    this.on_PropertyChanged();
                    if (!value) this.Vinsert_SportIconsOut();
                }
            }
        }
        
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

        private int buzzerChannelStart = 1;
        public int BuzzerChannelStart {
            get { return this.buzzerChannelStart; }
            set {
                if (this.buzzerChannelStart != value) {
                    if (value < 1) value = 1;
                    if (value > 8) value = 8;
                    this.buzzerChannelStart = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int buzzerChannelFinish = 2;
        public int BuzzerChannelFinish {
            get { return this.buzzerChannelFinish; }
            set {
                if (this.buzzerChannelFinish != value) {
                    if (value < 1) value = 1;
                    if (value > 8) value = 8;
                    this.buzzerChannelFinish = value;
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

        private double? timeToBeat = null;
        [NotSerialized]
        public string TimeToBeat {
            get {
                if (this.timeToBeat.HasValue) return Helper.convertDoubleToStopwatchTimeText(this.timeToBeat.Value, false, true).Replace(".", ",");
                else return string.Empty; 
            }
            set {
                if (this.TimeToBeat != value) {
                    double result;
                    if (string.IsNullOrEmpty(value) ||
                        !double.TryParse(value, out result)) this.timeToBeat = null;
                    else this.timeToBeat = result;
                    this.on_PropertyChanged();
                }
            }
        }

        private int sentenceTime = 10;
        public int SentenceTime {
            get { return this.sentenceTime; }
            set {
                if (this.sentenceTime != value) {
                    this.sentenceTime = value;
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

        private bool timeIsRunning = false;

        #endregion


        #region Funktionen

        public Business() { }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.TimeToBeatScore'", typeIdentifier);
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
            this.fillIOUnitList(this.buzzerHandler.UnitInfoList);
            this.buzzerHandler.RequestUnitConnectionStatus(this.IOUnitName);
            this.buzzerHandler.RequestUnitWorkMode(this.IOUnitName);

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;
            this.insertScene.TimeToBeat.StopFired += this.timeToBeat_StopFired;
            this.insertScene.TimeToBeat.PropertyChanged += this.timeToBeat_PropertyChanged;

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
            this.insertScene.TimeToBeat.StopFired -= this.timeToBeat_StopFired;
            this.insertScene.TimeToBeat.PropertyChanged -= this.timeToBeat_PropertyChanged;
        }

        public override void ResetData() {
            base.ResetData();
            this.TimeToBeat = null;
            this.timeIsRunning = false;
            this.TimeToBeatSentenceTime = 0;
        }

        public override void Next() {
            this.TimeToBeat = null;
            this.timeIsRunning = false;
            this.TimeToBeatSentenceTime = 0;
        }

        public void AddSentence() {
            this.TimeToBeatSentenceTime += this.SentenceTime;
        }

        public void NextPlayer() {
            if (this.SelectedPlayer == Content.Gameboard.PlayerSelection.LeftPlayer) this.SelectedPlayer = Content.Gameboard.PlayerSelection.RightPlayer;
            else this.SelectedPlayer = Content.Gameboard.PlayerSelection.LeftPlayer;
            this.TimeToBeatSentenceTime = 0;
            this.Vinsert_SetTimeToBeat();
        }

        private void fillIOUnitList(
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
            if (this.buzzerHandler is BuzzerIO.Business) {
                this.buzzerHandler.RequestUnitConnectionStatus(unitName);
                this.buzzerHandler.RequestUnitWorkMode(unitName);
            }
        }

        private void setDMXValues() {
            this.buzzerHandler.SetDMXMode(this.IOUnitName, IOnet.IOUnit.IONbuzDX.DMXBuzzerModes.CHANNEL);
            this.buzzerHandler.SetDMXSettings(this.IOUnitName, 0, 255);
        }

        public virtual void DoBuzzer() {
            if (this.timeIsRunning) this.Vinsert_StopTimeToBeat();
            else
            {
                this.insertMasterScene.Sampler.Play(VentuzScenes.Insert.Sampler.SampleElements.SpeedcourtRichtig);
                this.Vinsert_StartTimeToBeat();
            }
            this.LockBuzzer();
        }

        public virtual void ReleaseBuzzer() {
            bool[] inputMask = new bool[8];
            //if (this.timeIsRunning) {
                if (this.BuzzerChannelFinish > 0 &&
                    this.BuzzerChannelFinish <= inputMask.Length) inputMask[this.BuzzerChannelFinish - 1] = true;
            //}
            //else {
                if (this.BuzzerChannelStart > 0 &&
                    this.BuzzerChannelStart <= inputMask.Length) inputMask[this.BuzzerChannelStart - 1] = true;
            //}
            this.buzzerHandler.SetInputMask(this.IOUnitName, inputMask);
            this.buzzerHandler.ReleaseBuzzer(this.IOUnitName, WorkModes.BUZZER);
        }

        public virtual void LockBuzzer() {
            this.buzzerHandler.LockBuzzer(this.IOUnitName);
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }

        public override void Vinsert_ScoreIn() {
            this.Vinsert_SetScore();
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Score.ToIn();
        }
        public override void Vinsert_SetScore() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                this.insertScene.Score.SetPositionX(this.ScorePositionX);
                this.insertScene.Score.SetPositionY(this.ScorePositionY);
                this.insertScene.Score.SetFlipPosition(this.FlipPlayers);
                this.insertScene.Score.SetStyle(this.ScoreStyle);
                this.insertScene.Score.SetLeftTopName(this.LeftPlayerName);
                this.insertScene.Score.SetLeftTopScore(this.LeftPlayerScore);
                this.insertScene.Score.SetRightBottomName(this.RightPlayerName);
                this.insertScene.Score.SetRightBottomScore(this.RightPlayerScore);
            }
        }
        public override void Vinsert_ScoreOut() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Score.ToOut();
        }

        public void Vinsert_TimeToBeatIn() {
            this.Vinsert_SetTimeToBeat();
            this.Vinsert_ResetTimeToBeat();
            this.Vinsert_ResetOffsetTime();
            this.Vinsert_ResetTimeToBeatTime();
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.TimeToBeat.ToIn();
        }
        public void Vinsert_SetTimeToBeat() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                this.insertScene.TimeToBeat.SetPositionX(this.TimeToBeatPositionX);
                this.insertScene.TimeToBeat.SetPositionY(this.TimeToBeatPositionY);
                this.insertScene.TimeToBeat.SetStopTime(this.TimeToBeatStopTime);
                this.insertScene.TimeToBeat.SetSentenceTime(this.TimeToBeatSentenceTime);
                this.insertScene.TimeToBeat.SetStyle(this.TimeToBeatStyle);
                if (this.SelectedPlayer == Content.Gameboard.PlayerSelection.LeftPlayer) this.insertScene.TimeToBeat.SetName(this.LeftPlayerName);
                else this.insertScene.TimeToBeat.SetName(this.RightPlayerName);
            }
        }
        public void Vinsert_SetTimeToBeatSentenceTime() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.TimeToBeat.SetSentenceTime(this.TimeToBeatSentenceTime);
        }
        public void Vinsert_StartTimeToBeat() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                this.insertScene.TimeToBeat.StartTimer();
                this.timeIsRunning = true;
            }
        }
        public void Vinsert_ShowOffsetTime() { }
        public void Vinsert_ShowOffsetTime(
            float offset) {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                this.insertScene.TimeToBeat.SetOffset(offset);
                this.insertScene.TimeToBeat.ShowOffset();
            }
        }
        public void Vinsert_ResetOffsetTime() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.TimeToBeat.ResetOffset(); }
        public void Vinsert_ShowTimeToBeatTime() {
            if (this.timeToBeat.HasValue) this.Vinsert_ShowTimeToBeatTime(Convert.ToSingle(this.timeToBeat.Value));
        }
        public void Vinsert_ShowTimeToBeatTime(
            float timeToBeat) {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                this.insertScene.TimeToBeat.SetTimeToBeat(timeToBeat);
                this.insertScene.TimeToBeat.ShowTimeToBeat();
            }
        }
        public void Vinsert_ResetTimeToBeatTime() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.TimeToBeat.ResetTimeToBeat(); }
        public void Vinsert_StopTimeToBeat() { 
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.TimeToBeat.StopTimer();
            this.Vinsert_PlaySound_Stop();
        }
        public void Vinsert_ContinueTimeToBeat() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.TimeToBeat.ContinueTimer(); }
        public void Vinsert_ResetTimeToBeat() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.TimeToBeat.ResetTimer(); }
        public void Vinsert_TimeToBeatOut() { 
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.TimeToBeat.ToOut();
            this.Vinsert_ResetOffsetTime();
            this.Vinsert_ResetTimeToBeatTime();
        }

        public void Vinsert_SportIconsIn() {
            this.Vinsert_SetSportIcons();
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.SportIcons.ToIn();
        }
        public void Vinsert_SetSportIcons() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                this.insertScene.SportIcons.SetPositionX(this.SportIconsPositionX);
                this.insertScene.SportIcons.SetPositionY(this.SportIconsPositionY);
            }
        }
        public void Vinsert_SportIconsBasketball(VentuzScenes.GamePool._Modules.SportIcons.States status) { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.SportIcons.BasketballTo(status); }
        public void Vinsert_SportIconsHockey(VentuzScenes.GamePool._Modules.SportIcons.States status) { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.SportIcons.HockeyTo(status); }
        public void Vinsert_SportIconsCan(VentuzScenes.GamePool._Modules.SportIcons.States status) { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.SportIcons.CanTo(status); }
        public void Vinsert_SportIconsSoccer(VentuzScenes.GamePool._Modules.SportIcons.States status) { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.SportIcons.SoccerTo(status); }
        public void Vinsert_SportIconsOut() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.SportIcons.ToOut();
        }

        public void Vinsert_PlaySound_Stop() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.PlaySound_Stop(); }

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

        public event EventHandler TimeToBeatStopFired;
        protected void on_TimeToBeatStopFired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.TimeToBeatStopFired, e); }

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
                if (e.Arg.BuzzerID == this.BuzzerChannelStart ||
                    e.Arg.BuzzerID == this.BuzzerChannelFinish) this.DoBuzzer();
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

        protected void timeToBeat_StopFired(object sender, EventArgs e) {
            this.on_TimeToBeatStopFired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timeToBeat_StopFired);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timeToBeat_StopFired(object content) {
            if (this.timeIsRunning) {
                if (this.timeToBeat.HasValue) {
                    // zweiter Durchgang, der Offset wird ermittelt
                    double currentTime = this.insertScene.TimeToBeat.CurrentTime;
                    double offset = currentTime - this.timeToBeat.Value;
                    this.Vinsert_ShowOffsetTime(Convert.ToSingle(offset));
                }
                else {
                    // erster Durchgang, die TimeToBeat wird ermittelt
                    if (this.insertScene.TimeToBeat.CurrentTime > 0) {
                        this.timeToBeat = this.insertScene.TimeToBeat.CurrentTime;
                        this.on_PropertyChanged("TimeToBeat");
                    }
                }
            }
            this.timeIsRunning = false;
            this.LockBuzzer();
        }

        protected void timeToBeat_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timeToBeat_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timeToBeat_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "CurrentTime") this.TimeToBeatCurrentTime = Convert.ToDouble(this.insertScene.TimeToBeat.CurrentTime);
            }
        }

        #endregion

    }
}
