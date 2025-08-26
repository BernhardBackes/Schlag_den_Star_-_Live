using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using Cliparts.Serialization;

using Cliparts.Tools.Base;

using Cliparts.IOnet.IOUnit.IONbase;
using Cliparts.IOnet.IOUnit.IONbuz;

using Cliparts.Devantech;

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;
using Cliparts.SchlagDenStarLive.MainApp.Settings;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimerTimeToBeat;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimerTimeToBeat {

    public class Business : _Base.Timer.Business {

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

        private int rightPlayerBuzzerChannel = 1;
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

        public DevantechIO.Controls.RelaysLeftRight DevantechIO { get; set; }

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

        public Business() {}
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.DevantechIO = new DevantechIO.Controls.RelaysLeftRight();

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.TimerTimeToBeat'", typeIdentifier);
        }

        public override void Pose(
            SynchronizationContext syncContext,
            MidiHandler.Business midiHandler,
            BuzzerIO.Business buzzerHandler,
            AMB.Business ambHandler,
            Controller devantechHandler,
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

            this.DevantechIO.Pose(syncContext, devantechHandler);
            this.DevantechIO.PropertyChanged += this.on_PropertyChanged;

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;
            this.insertScene.TimeToBeat.StopFired += this.timeToBeat_StopFired;
            this.insertScene.TimeToBeat.PropertyChanged += this.timeToBeat_PropertyChanged;
            this.insertScene.Timer.PropertyChanged += this.insertTimer_PropertyChanged;

            this.Vinsert_Timer.Pose(syncContext, this.insertScene.Timer);
            this.Vinsert_Timer.PropertyChanged += this.on_PropertyChanged;

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

            this.DevantechIO.PropertyChanged -= this.on_PropertyChanged;
            this.DevantechIO.Dispose();
            this.DevantechIO = null;

            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.TimeToBeat.StopFired -= this.timeToBeat_StopFired;
            this.insertScene.TimeToBeat.PropertyChanged -= this.timeToBeat_PropertyChanged;
            this.insertScene.Timer.PropertyChanged -= this.insertTimer_PropertyChanged;
            this.insertScene.Dispose();

            this.Vinsert_Timer.PropertyChanged -= this.on_PropertyChanged;
            this.Vinsert_Timer.Dispose();

        }

        public override void ResetData() {
            base.ResetData();
            this.TimeToBeat = null;
            this.timeIsRunning = false;
        }


        public void NextPlayer() {
            if (this.SelectedPlayer == Content.Gameboard.PlayerSelection.LeftPlayer) this.SelectedPlayer = Content.Gameboard.PlayerSelection.RightPlayer;
            else this.SelectedPlayer = Content.Gameboard.PlayerSelection.LeftPlayer;
            this.Vinsert_SetTimeToBeat();
        }

        public void CloseRelais() {
            if (this.SelectedPlayer == Content.Gameboard.PlayerSelection.LeftPlayer) this.DevantechIO.CloseLeftRelay();
            else this.DevantechIO.CloseRightRelay();
        }

        public void OpenRelais() {
            if (this.SelectedPlayer == Content.Gameboard.PlayerSelection.LeftPlayer) this.DevantechIO.OpenLeftRelay();
            else this.DevantechIO.OpenRightRelay();
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

        public virtual void PassFinishLine() {
            if (this.timeIsRunning) this.Vinsert_StopTimeToBeat();
            else this.Vinsert_StartTimeToBeat();
            this.LockBuzzer();
        }

        public virtual void ReleaseBuzzer() {
            bool[] inputMask = new bool[8];
            int channel = 0;
            if (this.SelectedPlayer == Content.Gameboard.PlayerSelection.LeftPlayer) channel = this.LeftPlayerBuzzerChannel;
            else channel = this.RightPlayerBuzzerChannel;
            if (channel > 0 &&
                channel <= inputMask.Length) inputMask[channel - 1] = true;
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

        public void Vinsert_TimeToBeatIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_TimeToBeatIn(this.insertScene.TimeToBeat); }
        public void Vinsert_TimeToBeatIn(
            VentuzScenes.GamePool._Modules.TimeToBeat scene) {
            if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) {
                this.Vinsert_SetTimeToBeat(scene, this.SelectedPlayer);

                scene.ToIn();
            }
            this.Vinsert_ResetTimeToBeat(scene);
            this.Vinsert_ResetOffsetTime(scene);
            this.Vinsert_ResetTimeToBeatTime(scene);
        }
        public void Vinsert_SetTimeToBeat() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetTimeToBeat(this.insertScene.TimeToBeat, this.SelectedPlayer); }
        public void Vinsert_SetTimeToBeat(
            VentuzScenes.GamePool._Modules.TimeToBeat scene,
            Content.Gameboard.PlayerSelection selectedPlayer) {
            if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) {
                scene.SetPositionX(this.TimeToBeatPositionX);
                scene.SetPositionY(this.TimeToBeatPositionY);
                scene.SetStopTime(this.TimeToBeatStopTime);
                scene.SetSentenceTime(0);
                scene.SetStyle(this.TimeToBeatStyle);
                if (selectedPlayer == Content.Gameboard.PlayerSelection.LeftPlayer) scene.SetName(this.LeftPlayerName);
                else scene.SetName(this.RightPlayerName);
            }
        }
        public void Vinsert_StartTimeToBeat() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_StartTimeToBeat(this.insertScene.TimeToBeat); }
        public void Vinsert_StartTimeToBeat(VentuzScenes.GamePool._Modules.TimeToBeat scene) {
            if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) {
                scene.StartTimer();
                this.timeIsRunning = true;
            }
        }
        public void Vinsert_ShowOffsetTime(float offset) { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ShowOffsetTime(this.insertScene.TimeToBeat, offset); }
        public void Vinsert_ShowOffsetTime(
            VentuzScenes.GamePool._Modules.TimeToBeat scene,
            float offset) {
            if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) {
                scene.SetOffset(offset);
                scene.ShowOffset();
            }
        }
        public void Vinsert_ResetOffsetTime() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ResetOffsetTime(this.insertScene.TimeToBeat); }
        public void Vinsert_ResetOffsetTime(VentuzScenes.GamePool._Modules.TimeToBeat scene) { if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) scene.ResetOffset(); }
        public void Vinsert_ShowTimeToBeatTime() { if (this.insertScene is VRemote4.HandlerSi.Scene && this.timeToBeat.HasValue) this.Vinsert_ShowTimeToBeatTime(this.insertScene.TimeToBeat, Convert.ToSingle(this.timeToBeat.Value)); }
        public void Vinsert_ShowTimeToBeatTime(float timeToBeat) { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ShowTimeToBeatTime(this.insertScene.TimeToBeat, Convert.ToSingle(timeToBeat)); }
        public void Vinsert_ShowTimeToBeatTime(
            VentuzScenes.GamePool._Modules.TimeToBeat scene,
            float timeToBeat) {
            if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) {
                scene.SetTimeToBeat(timeToBeat);
                scene.ShowTimeToBeat();
            }
        }
        public void Vinsert_ResetTimeToBeatTime() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ResetTimeToBeatTime(this.insertScene.TimeToBeat); }
        public void Vinsert_ResetTimeToBeatTime(VentuzScenes.GamePool._Modules.TimeToBeat scene) { if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) scene.ResetTimeToBeat(); }
        public void Vinsert_StopTimeToBeat() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_StopTimeToBeat(this.insertScene.TimeToBeat); }
        public void Vinsert_StopTimeToBeat(VentuzScenes.GamePool._Modules.TimeToBeat scene) { if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) { scene.StopTimer(); } }
        public void Vinsert_ContinueTimeToBeat() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ContinueTimeToBeat(this.insertScene.TimeToBeat); }
        public void Vinsert_ContinueTimeToBeat(VentuzScenes.GamePool._Modules.TimeToBeat scene) {
            if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) {
                scene.ContinueTimer();
                this.timeIsRunning = true;
            }
        }
        public void Vinsert_ResetTimeToBeat() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ResetTimeToBeat(this.insertScene.TimeToBeat); }
        public void Vinsert_ResetTimeToBeat(VentuzScenes.GamePool._Modules.TimeToBeat scene) { if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) scene.ResetTimer(); }
        public void Vinsert_TimeToBeatOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_TimeToBeatOut(this.insertScene.TimeToBeat); }
        public void Vinsert_TimeToBeatOut(
            VentuzScenes.GamePool._Modules.TimeToBeat scene) {
            if (scene is VentuzScenes.GamePool._Modules.TimeToBeat) {
                scene.ToOut();
                this.Vinsert_ResetOffsetTime(scene);
                this.Vinsert_ResetTimeToBeatTime(scene);
            }
        }

        public void Vinsert_PlayBuzzerSound() {}

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

        void buzzerHandler_BuzUnit_Buzzered(object sender, BuzzerIDParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_Buzzered);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_BuzUnit_Buzzered(object content) {
            BuzzerIDParam_EventArgs e = content as BuzzerIDParam_EventArgs;
            if (this.isActive &&
                e is BuzzerIDParam_EventArgs &&
                e.Arg.Name == this.IOUnitName) {
                this.PassFinishLine();
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
                if (e.Arg.ConnectionStatus == Tools.NetContact.ClientStates.Connected) this.setDMXValues();
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
        }

        protected void timeToBeat_PropertyChanged(object sender, PropertyChangedEventArgs e) {
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
