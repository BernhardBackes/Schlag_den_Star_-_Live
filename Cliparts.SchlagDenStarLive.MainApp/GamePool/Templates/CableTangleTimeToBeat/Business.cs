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

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;
using Cliparts.SchlagDenStarLive.MainApp.Settings;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.CableTangleTimeToBeat;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.CableTangleTimeToBeat {

    public enum AvailableIOUnits { UnitA, UnitB }

    public class Cable : INotifyPropertyChanged {

        #region Properties

        protected SynchronizationContext syncContext;

        private int index;
        public int Index { get { return this.index;} }
	
        public int ID { get { return this.index + 1;} }

        private AvailableIOUnits selectedIOUnit = AvailableIOUnits.UnitA;
        public AvailableIOUnits SelectedIOUnit {
            get { return this.selectedIOUnit; }
            set {
                if (this.selectedIOUnit != value) {
                    this.selectedIOUnit = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int buzzerChannel = 1;
        public int BuzzerChannel {
            get { return this.buzzerChannel; }
            set {
                if (this.buzzerChannel != value) {
                    if (value < 1) value = 1;
                    if (value > 8) value = 8;
                    this.buzzerChannel = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int dmxStartAddress = 1;
        public int DMXStartAddress {
            get { return this.dmxStartAddress; }
            set {
                if (this.dmxStartAddress != value) {
                    if (value < 1) value = 1;
                    if (value > 512) value = 512;
                    this.dmxStartAddress = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool lightIsOn = false;

        private System.Timers.Timer onDelayTimer;

        private System.Timers.Timer offDelayTimer;

        #endregion


        #region Funktionen

        public Cable() {}
        public Cable(
            int index) {
            this.index = index;
            this.dmxStartAddress = 1;

            this.onDelayTimer = new System.Timers.Timer(750);
            this.onDelayTimer.AutoReset = false;
            this.onDelayTimer.Elapsed += this.onDelayTimer_Elapsed;

            this.offDelayTimer = new System.Timers.Timer(750);
            this.offDelayTimer.AutoReset = false;
            this.offDelayTimer.Elapsed += this.offDelayTimer_Elapsed;
        }

        public void Dispose() {
            this.onDelayTimer.Elapsed -= this.onDelayTimer_Elapsed;
            this.offDelayTimer.Elapsed -= this.offDelayTimer_Elapsed;
        }

        public void Clone(
            SynchronizationContext syncContext,
            Cable cable) {
            this.syncContext = syncContext;
            if (cable is Cable) {
                this.SelectedIOUnit = cable.SelectedIOUnit;
                this.BuzzerChannel = cable.BuzzerChannel;
                this.DMXStartAddress = cable.DMXStartAddress;
            }
        }

        public void SetLightOn(
            bool delayed) {
            this.lightIsOn = true;
            if (delayed) this.onDelayTimer.Start();
            else this.on_LightOnTriggered(this, new EventArgs()); 
        }

        public void SetLightOff(
            bool delayed) {
            this.lightIsOn = false;
            if (delayed) this.offDelayTimer.Start();
            else this.on_LightOffTriggered(this, new EventArgs());
        }

        public void ToggleLight(
            bool delayed) {
            if (this.lightIsOn) this.SetLightOff(delayed);
            else this.SetLightOn(delayed);
        }

        #endregion


        #region Events.Outgoing

        public event PropertyChangedEventHandler PropertyChanged;
        protected void on_PropertyChanged([CallerMemberName]string callerName = "") { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }
        protected void on_PropertyChanged(PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, e); }
        protected void on_PropertyChanged(object sender, PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(sender, this.PropertyChanged, e); }

        public event EventHandler LightOnTriggered;
        private void on_LightOnTriggered(object sender, EventArgs e) { Helper.raiseEvent(sender, this.LightOnTriggered, e); }

        public event EventHandler LightOffTriggered;
        private void on_LightOffTriggered(object sender, EventArgs e) { Helper.raiseEvent(sender, this.LightOffTriggered, e); }

        #endregion

        #region Events.Incoming

        void onDelayTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_onDelayTimer_Elapsed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_onDelayTimer_Elapsed(object content) {
            this.on_LightOnTriggered(this, new EventArgs());
        }

        void offDelayTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_offDelayTimer_Elapsed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_offDelayTimer_Elapsed(object content) {
            this.on_LightOffTriggered(this, new EventArgs());
        }

        #endregion

    }

    public class Business : _Base.Business {

        public const int CablesCount = 15;

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

        private int timeToBeatStopTime = 600;
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

        private BuzzerIO.Business buzzerHandler;

        private List<string> ioUnitNameList = new List<string>();
        public string[] IOUnitNameList { get { return this.ioUnitNameList.ToArray(); } }

        private Cliparts.IOnet.IOUnit.IONbase.InfoParamArray_EventArgs ioUnitInfo;

        private string ioUnitAName = string.Empty;
        public string IOUnitAName {
            get { return this.ioUnitAName; }
            set {
                if (this.ioUnitAName != value) {
                    if (value == null) value = string.Empty;
                    this.ioUnitAName = value;
                    this.on_PropertyChanged();
                    this.ioUnitAConnectionStatus = Tools.NetContact.ClientStates.Missing;
                    this.ioUnitAWorkMode = WorkModes.NA;
                    this.checkIOUnitAStatus();
                    this.requestIOUnitStates(value);
                }
            }
        }

        private Cliparts.Tools.NetContact.ClientStates ioUnitAConnectionStatus = Tools.NetContact.ClientStates.Missing;

        private WorkModes ioUnitAWorkMode = WorkModes.NA;

        public BuzzerUnitStates IOUnitAStatus { get; private set; }

        private string ioUnitBName = string.Empty;
        public string IOUnitBName {
            get { return this.ioUnitBName; }
            set {
                if (this.ioUnitBName != value) {
                    if (value == null) value = string.Empty;
                    this.ioUnitBName = value;
                    this.on_PropertyChanged();
                    this.ioUnitBConnectionStatus = Tools.NetContact.ClientStates.Missing;
                    this.ioUnitBWorkMode = WorkModes.NA;
                    this.checkIOUnitBStatus();
                    this.requestIOUnitStates(value);
                }
            }
        }

        private Cliparts.Tools.NetContact.ClientStates ioUnitBConnectionStatus = Tools.NetContact.ClientStates.Missing;

        private WorkModes ioUnitBWorkMode = WorkModes.NA;

        public BuzzerUnitStates IOUnitBStatus { get; private set; }

        private List<Cable> cablelist = new List<Cable>();
        public Cable[] Cables {
            get { return this.cablelist.ToArray(); }
            set {
                for (int i = 0; i < this.cablelist.Count; i++) {
                    if (value is Cable[] &&
                        value.Length > i) this.cablelist[i].Clone( this.syncContext, value[i]);
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

        private Insert insertScene;
        public override VRemote4.HandlerSi.Scene.States InsertSceneStatus {
            get {
                if (this.insertScene is Insert) return this.insertScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private bool timeIsRunning = false;

        private int cableIndex = 0;

        private System.Timers.Timer faultDelayTimer;

        #endregion


        #region Funktionen

        public Business() { this.fillCableList(); }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.faultDelayTimer = new System.Timers.Timer(1500);
            this.faultDelayTimer.AutoReset = false;
            this.faultDelayTimer.Elapsed += this.faultDelayTimer_Elapsed;

            this.fillCableList();

            this.ClassInfo = string.Format("'{0}' of 'Templates.CableTangleTimeToBeat'", typeIdentifier);
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
            this.buzzerHandler.RequestUnitConnectionStatus(this.IOUnitAName);
            this.buzzerHandler.RequestUnitWorkMode(this.IOUnitAName);

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;
            this.insertScene.TimeToBeat.StopFired += this.timeToBeat_StopFired;
            this.insertScene.TimeToBeat.PropertyChanged += this.timeToBeat_PropertyChanged;

            foreach (Cable cable in this.cablelist) cable.Clone(this.syncContext, cable);

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);
        }

        public override void Dispose() {
            base.Dispose();

            this.faultDelayTimer.Elapsed -= this.faultDelayTimer_Elapsed;

            this.clearCableList();

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

        protected void fillCableList() {
            while (this.cablelist.Count < CablesCount) {
                Cable cable = new Cable(this.cablelist.Count);
                cable.PropertyChanged += this.cable_PropertyChanged;
                cable.LightOnTriggered += this.cable_LightOnTriggered;
                cable.LightOffTriggered += this.cable_LightOffTriggered;
                cable.SetLightOff(false);
                this.cablelist.Add(cable);
            }
        }

        protected void clearCableList() {
            foreach (Cable cable in this.cablelist) {
                cable.PropertyChanged -= this.cable_PropertyChanged;
                cable.LightOnTriggered -= this.cable_LightOnTriggered;
                cable.LightOffTriggered -= this.cable_LightOffTriggered;
                cable.Dispose();
            }
            this.cablelist.Clear();
        }

        public override void ResetData() {
            base.ResetData();
            this.TimeToBeat = null;
            this.timeIsRunning = false;
            this.cableIndex = 0;
            this.SetCableLightOff();
        }

        public void NextPlayer() {
            if (this.SelectedPlayer == Content.Gameboard.PlayerSelection.LeftPlayer) this.SelectedPlayer = Content.Gameboard.PlayerSelection.RightPlayer;
            else this.SelectedPlayer = Content.Gameboard.PlayerSelection.LeftPlayer;
            this.Vinsert_SetTimeToBeat();
            this.SetCableLightOff();
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

        private void checkIOUnitAStatus() {
            BuzzerIO.BuzzerUnitStates ioUnitStatus = BuzzerIO.BuzzerUnitStates.NotAvailable;
            if (this.ioUnitNameList.Contains(this.IOUnitAName)) {
                switch (this.ioUnitAConnectionStatus) {
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
                    switch (this.ioUnitAWorkMode) {
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
            if (this.IOUnitAStatus != ioUnitStatus) {
                this.IOUnitAStatus = ioUnitStatus;
                this.on_PropertyChanged("IOUnitAStatus");
            }
        }

        private void checkIOUnitBStatus() {
            BuzzerIO.BuzzerUnitStates ioUnitStatus = BuzzerIO.BuzzerUnitStates.NotAvailable;
            if (this.ioUnitNameList.Contains(this.IOUnitBName)) {
                switch (this.ioUnitBConnectionStatus) {
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
                    switch (this.ioUnitBWorkMode) {
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
            if (this.IOUnitBStatus != ioUnitStatus) {
                this.IOUnitBStatus = ioUnitStatus;
                this.on_PropertyChanged("IOUnitBStatus");
            }
        }

        private void requestIOUnitStates(
            string unitName) {
            if (this.buzzerHandler is BuzzerIO.Business) {
                this.buzzerHandler.RequestUnitConnectionStatus(unitName);
                this.buzzerHandler.RequestUnitWorkMode(unitName);
            }
        }

        public Cable GetCable(
            int index) {
            if (index >= 0 && index < this.cablelist.Count) return this.cablelist[index];
            else return null;
        }

        public virtual void DoBuzzer(
            int cableIndex) {
            this.DoBuzzer(this.GetCable(cableIndex));
        }

        public void DoBuzzer(
            Cable cable) {
            if (cable is Cable) {
                if (cable.Index == this.cableIndex) {
                    // richtige Lampe angezündet
                    cable.SetLightOn(true);
                    this.cableIndex++;
                    if (this.cableIndex == CablesCount) {
                        this.Vinsert_StopTimeToBeat();
                        this.Vinsert_PlayJingleEnd();
                        this.LockBuzzer();
                    }
                }
                else {
                    // falsche Lampe angezündet
                    cable.ToggleLight(true);
                    this.Vinsert_PlayJingleBad();
                    this.LockBuzzer();
                    this.faultDelayTimer.Start();
                }
            }
        }

        public virtual void ReleaseBuzzer() {
            bool[] inputMask = new bool[] { true, true, true, true, true, true, true, true };
            this.buzzerHandler.SetInputMask(this.IOUnitAName, inputMask);
            this.buzzerHandler.ReleaseBuzzer(this.IOUnitAName, IOnet.IOUnit.IONbuz.WorkModes.EVENT);
            this.buzzerHandler.SetInputMask(this.IOUnitBName, inputMask);
            this.buzzerHandler.ReleaseBuzzer(this.IOUnitBName, IOnet.IOUnit.IONbuz.WorkModes.EVENT);
        }

        public virtual void LockBuzzer() {
            this.buzzerHandler.LockBuzzer(this.IOUnitAName);
            this.buzzerHandler.LockBuzzer(this.IOUnitBName);
        }


        public void SetCableLightOn() {
            foreach (Cable item in this.cablelist) item.SetLightOn(false);
        }
        private void setCableLightOn(
            Cable cable) {
            if (cable is Cable) this.buzzerHandler.SetDMXChannel(this.IOUnitAName, cable.DMXStartAddress, new byte[] { 100 });
        }

        public void SetCableLightOff() {
            foreach (Cable item in this.cablelist) item.SetLightOff(false);
        }
        private void setCableLightOff(
            Cable cable) {
            if (cable is Cable) this.buzzerHandler.SetDMXChannel(this.IOUnitAName, cable.DMXStartAddress, new byte[] { 0 });
        }

        public void Start() {
            this.start();
            this.Vinsert_StartTimeToBeat();
        }
        private void start() {
            this.SetCableLightOff();
            this.cableIndex = 0;
        }
        public void Stop() {
            this.Vinsert_StopTimeToBeat();
            this.LockBuzzer();
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }
        public void Vinsert_TimeToBeatIn() {
            this.Vinsert_SetTimeToBeat();
            this.Vinsert_ResetTimeToBeat();
            this.Vinsert_ResetOffsetTime();
            this.Vinsert_ResetTimeToBeatTime();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.TimeToBeat.ToIn();
        }
        public void Vinsert_SetTimeToBeat() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.TimeToBeat.SetPositionX(this.TimeToBeatPositionX);
                this.insertScene.TimeToBeat.SetPositionY(this.TimeToBeatPositionY);
                this.insertScene.TimeToBeat.SetStopTime(this.TimeToBeatStopTime);
                this.insertScene.TimeToBeat.SetStyle(this.TimeToBeatStyle);
                if (this.SelectedPlayer == Content.Gameboard.PlayerSelection.LeftPlayer) this.insertScene.TimeToBeat.SetName(this.LeftPlayerName);
                else this.insertScene.TimeToBeat.SetName(this.RightPlayerName);
            }
        }
        public void Vinsert_StartTimeToBeat() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.TimeToBeat.StartTimer();
                this.timeIsRunning = true;
            }
        }
        public void Vinsert_ShowOffsetTime() { }
        public void Vinsert_ShowOffsetTime(
            float offset) {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.TimeToBeat.SetOffset(offset);
                this.insertScene.TimeToBeat.ShowOffset();
            }
        }
        public void Vinsert_ResetOffsetTime() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.TimeToBeat.ResetOffset(); }
        public void Vinsert_ShowTimeToBeatTime() {
            if (this.timeToBeat.HasValue) this.Vinsert_ShowTimeToBeatTime(Convert.ToSingle(this.timeToBeat.Value));
        }
        public void Vinsert_ShowTimeToBeatTime(
            float timeToBeat) {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                this.insertScene.TimeToBeat.SetTimeToBeat(timeToBeat);
                this.insertScene.TimeToBeat.ShowTimeToBeat();
            }
        }
        public void Vinsert_ResetTimeToBeatTime() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.TimeToBeat.ResetTimeToBeat(); }
        public void Vinsert_StopTimeToBeat() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.TimeToBeat.StopTimer(); }
        public void Vinsert_ContinueTimeToBeat() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.TimeToBeat.ContinueTimer(); }
        public void Vinsert_ResetTimeToBeat() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.TimeToBeat.ResetTimer(); }
        public void Vinsert_TimeToBeatOut() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.TimeToBeat.ToOut();
            this.Vinsert_ResetOffsetTime();
            this.Vinsert_ResetTimeToBeatTime();
        }
        public void Vinsert_PlayJingleBad() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.PlayJingleBad(); }
        public void Vinsert_PlayJingleEnd() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.PlayJingleEnd(); }
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

        void cable_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_cable_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_cable_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {}
            this.on_PropertyChanged("Cable");
        }

        void cable_LightOnTriggered(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_cable_LightOnTriggered);
            if (this.syncContext != null) this.syncContext.Post(callback, sender);
        }
        private void sync_cable_LightOnTriggered(object content) { 
            this.setCableLightOn(content as Cable); 
        }

        void cable_LightOffTriggered(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_cable_LightOffTriggered);
            if (this.syncContext != null) this.syncContext.Post(callback, sender);
        }
        private void sync_cable_LightOffTriggered(object content) { this.setCableLightOff(content as Cable); }

        void buzzerHandler_BuzUnit_Buzzered(object sender, IOnet.IOUnit.IONbuz.BuzzerIDParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_Buzzered);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_BuzUnit_Buzzered(object content) {
            IOnet.IOUnit.IONbuz.BuzzerIDParam_EventArgs e = content as IOnet.IOUnit.IONbuz.BuzzerIDParam_EventArgs;
            if (this.isActive &&
                e is IOnet.IOUnit.IONbuz.BuzzerIDParam_EventArgs) {
                foreach (Cable cable in this.cablelist) {
                    if (cable.BuzzerChannel == e.Arg.BuzzerID) {
                        if ((cable.SelectedIOUnit == AvailableIOUnits.UnitA && this.IOUnitAName == e.Arg.Name) ||
                            (cable.SelectedIOUnit == AvailableIOUnits.UnitB && this.IOUnitBName == e.Arg.Name)) {
                            this.DoBuzzer(cable);
                            break;
                        }
                    }
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
            if (e is IOnet.IOUnit.IONbase.ConnectionStatusParam_EventArgs) {
                if (e.Arg.Name == this.IOUnitAName) {
                    this.ioUnitAConnectionStatus = e.Arg.ConnectionStatus;
                    this.checkIOUnitAStatus();
                }
                else if (e.Arg.Name == this.IOUnitBName) {
                    this.ioUnitBConnectionStatus = e.Arg.ConnectionStatus;
                    this.checkIOUnitBStatus();
                }
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
            if (e is IOnet.IOUnit.IONbuz.WorkModeParam_EventArgs) {
                if (e.Arg.Name == this.IOUnitAName) {
                    this.ioUnitAWorkMode = e.Arg.WorkMode;
                    this.checkIOUnitAStatus();
                }
                else if (e.Arg.Name == this.IOUnitBName) {
                    this.ioUnitBWorkMode = e.Arg.WorkMode;
                    this.checkIOUnitBStatus();
                }
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

        void faultDelayTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_faultDelayTimer_Elapsed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_faultDelayTimer_Elapsed(object content) {
            this.ReleaseBuzzer();
            this.start();
        }

        #endregion
    }
}
