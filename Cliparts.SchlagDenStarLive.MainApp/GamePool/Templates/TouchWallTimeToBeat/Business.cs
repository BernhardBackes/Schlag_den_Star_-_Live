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
using System.Xml.Serialization;

using Cliparts.Serialization;

using Cliparts.Tools.Base;

using Cliparts.IOnet.IOUnit.IONbase;
using Cliparts.IOnet.IOUnit.IONbuz;

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;
using Cliparts.SchlagDenStarLive.MainApp.Settings;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TouchWallTimeToBeat;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TouchWallTimeToBeat {

    public enum AvailableIOUnits { UnitA, UnitB }

    public class Tile : INotifyPropertyChanged {

        #region Properties

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

        #endregion


        #region Funktionen

        public Tile() {}
        public Tile(int index) { 
            this.index = index;
            this.dmxStartAddress = 1 + index * 3;
        }

        public void Clone(
            Tile tile) {
            if (tile is Tile) {
                this.SelectedIOUnit = tile.SelectedIOUnit;
                this.BuzzerChannel = tile.BuzzerChannel;
                this.DMXStartAddress = tile.DMXStartAddress;
            }
        }

        public void SetLightOn() { this.on_LightOnTriggered(this, new EventArgs()); }

        public void SetLightOff() { this.on_LightOffTriggered(this, new EventArgs()); }

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
        #endregion

    }

    public class TileCrowd : INotifyPropertyChanged {

        #region Properties

        private int id = 0;
        public int ID {
            get { return this.id; }
            set {
                if (this.id != value) {
                    this.id = value;
                    this.buildToString();
                }
            }
        }

        private string crowd = string.Empty;
        public string Crowd {
            get { return this.crowd; }
            set {
                if (this.crowd != value) {
                    this.crowd = this.validatedSequence(value);
                    this.buildToString();
                    this.on_PropertyChanged();
                }
            }
        }

        private List<int> tileIDList = new List<int>();
        public int[] TileIDList { get { return this.tileIDList.ToArray(); } }

        public int TilesCount { get { return this.tileIDList.Count; } }

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public TileCrowd() {}

        private string validatedSequence(
            string sequence) {
            string newSequence = string.Empty;
            this.tileIDList.Clear();
            if (!string.IsNullOrEmpty(sequence)) {
                string tileLetter;
                int tileID;
                for(int i = 0; i < sequence.Length; i++) {
                    tileLetter = sequence.Substring(i, 1);
                    if (int.TryParse(tileLetter, out tileID) &&
                        tileID > 0 &&
                        tileID <= Business.TilesCount &&
                        !this.tileIDList.Contains(tileID)) this.tileIDList.Add(tileID);
                }
                if (this.tileIDList.Count > 0) {
                    this.tileIDList.Sort();
                    foreach (int id in this.tileIDList) newSequence += id.ToString();
                }
            }
            return newSequence;
        }

        private void buildToString() {
            this.toString = this.id.ToString("00");
            if (!string.IsNullOrEmpty(this.crowd)) this.toString += " - " + this.crowd;
        }

        public override string ToString() { return this.toString; }

        #endregion


        #region Events.Outgoing

        public event PropertyChangedEventHandler PropertyChanged;
        protected void on_PropertyChanged([CallerMemberName]string callerName = "") { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }
        protected void on_PropertyChanged(PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, e); }
        protected void on_PropertyChanged(object sender, PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(sender, this.PropertyChanged, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }

    public class Data {
        public TileCrowd[] Sample;
        public TileCrowd[] Challenge;
    }

    public class Business : _Base.Business {

        public const int TilesCount = 9;

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

        private List<Tile> tilelist = new List<Tile>();
        public Tile[] Tiles {
            get { return this.tilelist.ToArray(); }
            set {
                for (int i = 0; i < this.tilelist.Count; i++) {
                    if (value is Tile[] &&
                        value.Length > i) this.tilelist[i].Clone(value[i]);
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

        private string filename = string.Empty;
        public string Filename {
            get { return this.filename; }
            set { if (this.filename != value) this.Load(value); }
        }

        private List<TileCrowd> sample = new List<TileCrowd>();
        public TileCrowd[] Sample {
            get { return this.sample.ToArray(); }
            private set {
                this.sample.Clear();
                if (value is TileCrowd[]) {
                    foreach (TileCrowd item in value) {
                        if (item is TileCrowd) {
                            item.ID = this.sample.Count + 1;
                            this.sample.Add(item);
                        }
                    }
                }
                this.on_PropertyChanged();
                if (this.selectedTileCrowdList == this.sample) this.on_PropertyChanged("SelectedTileCrowdList");
            }
        }

        private List<TileCrowd> challenge = new List<TileCrowd>();
        public TileCrowd[] Challenge {
            get { return this.challenge.ToArray(); }
            private set {
                this.challenge.Clear();
                if (value is TileCrowd[]) {
                    foreach (TileCrowd item in value) {
                        if (item is TileCrowd) {
                            item.ID = this.challenge.Count + 1;
                            this.challenge.Add(item);
                        }
                    }
                }
                this.on_PropertyChanged();
                if (this.selectedTileCrowdList == this.challenge) this.on_PropertyChanged("SelectedTileCrowdList");
            }
        }

        private List<TileCrowd> selectedTileCrowdList = new List<TileCrowd>();
        public TileCrowd[] SelectedTileCrowdList { get { return this.selectedTileCrowdList.ToArray(); } }

        private int selectedTileCrowdIndex = -1;
        public int SelectedTileCrowdIndex {
            get { return this.selectedTileCrowdIndex; }
            set {
                if (value < 0) value = 0;
                if (value >= this.selectedTileCrowdList.Count) value = this.selectedTileCrowdList.Count - 1;
                if (this.selectedTileCrowdIndex != value) {
                    this.selectedTileCrowdIndex = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int remainingTilesCounter = 0;
        public int RemainingTilesCounter {
            get { return this.remainingTilesCounter; }
            protected set {
                if (this.remainingTilesCounter != value) {
                    if (value < 0) value = 0;
                    this.remainingTilesCounter = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimeToBeatCounter(value);
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

        private Dictionary<int, Tile> tileToTouchList = new Dictionary<int, Tile>();

        private bool timeIsRunning = false;

        private System.Timers.Timer buzzerLockTimer;

        private bool buzzerIsLocked = false;

        #endregion


        #region Funktionen

        public Business() { this.fillTileList(); }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.fillTileList();

            this.buzzerLockTimer = new System.Timers.Timer(400);
            this.buzzerLockTimer.AutoReset = false;
            this.buzzerLockTimer.Elapsed += this.buzzerLockTimer_Elapsed;

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.TouchWallTimeToBeat'", typeIdentifier);
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

            this.SelectSample();

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);
        }

        public override void Dispose() {
            base.Dispose();

            this.clearTileList();

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

            this.buzzerLockTimer.Elapsed -= this.buzzerLockTimer_Elapsed;
        }

        protected void fillTileList() {
            while (this.tilelist.Count < TilesCount) {
                Tile tile = new Tile(this.tilelist.Count);
                tile.PropertyChanged += this.tile_PropertyChanged;
                tile.LightOnTriggered += this.tile_LightOnTriggered;
                tile.LightOffTriggered += this.tile_LightOffTriggered;
                this.tilelist.Add(tile);
            }
        }

        protected void clearTileList() {
            foreach (Tile tile in this.tilelist) {
                tile.PropertyChanged -= this.tile_PropertyChanged;
                tile.LightOnTriggered -= this.tile_LightOnTriggered;
                tile.LightOffTriggered -= this.tile_LightOffTriggered;
            }
            this.tilelist.Clear();
        }

        public override void ResetData() {
            base.ResetData();
            this.TimeToBeat = null;
            this.timeIsRunning = false;
            this.SelectSample();
        }

        public void NextPlayer() {
            if (this.SelectedPlayer == Content.Gameboard.PlayerSelection.LeftPlayer) this.SelectedPlayer = Content.Gameboard.PlayerSelection.RightPlayer;
            else this.SelectedPlayer = Content.Gameboard.PlayerSelection.LeftPlayer;
            this.Vinsert_SetTimeToBeat();
        }

        public void FillTileCrowdListRandomized(
            List<TileCrowd> tileCrowdList,
            int length) {
            Random rnd = new Random();
            int lastValue = 0;
            for (int i = 0; i < length; i++) {
                int value = lastValue;
                while(value == lastValue) value = rnd.Next(1, TilesCount + 1);
                lastValue = value;
                TileCrowd newCrowd = new TileCrowd();
                newCrowd.Crowd = value.ToString();
                tileCrowdList.Add(newCrowd);
            }
            this.on_PropertyChanged("SelectedTileCrowdList");
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

        public virtual void DoBuzzer(
            int tileIndex) {
            int id = tileIndex + 1;
            if (this.tileToTouchList.ContainsKey(id)) this.doBuzzer(this.tileToTouchList[id]);
        }

        private void doBuzzer(
            Tile tile) {
            if (tile is Tile &&
                this.tileToTouchList.ContainsValue(tile)) {
                if (this.selectedTileCrowdList == this.challenge && !this.timeIsRunning) this.Vinsert_StartTimeToBeat();
                this.tileToTouchList.Remove(tile.ID);
                if (this.tileToTouchList.Count == 0) {
                    if (this.SelectedTileCrowdIndex < this.selectedTileCrowdList.Count - 1) {
                        this.Vinsert_PlayJingleHit();
                        this.setTileLightOff(tile);
                        this.SelectedTileCrowdIndex++;
                        this.enqueueTile();
                        this.calcRemainingTilesCounter();
                    }
                    else {
                        this.Stop();
                        this.Vinsert_PlayJingleEnd();
                        this.RemainingTilesCounter = 0;
                    }
                }
                else {
                    this.Vinsert_PlayJingleHit();
                    this.setTileLightOff(tile);
                    this.RemainingTilesCounter--;
                }
                //this.buzzerIsLocked = true;
                //this.buzzerLockTimer.Start();
            }
        }

        public virtual void ReleaseBuzzer() {
            bool[] inputMask = new bool[] { true, true, true, true, true, true, true, true };
            this.buzzerHandler.SetInputMask(this.IOUnitAName, inputMask);
            this.buzzerHandler.ReleaseBuzzer(this.IOUnitAName, IOnet.IOUnit.IONbuz.WorkModes.BUZZER);
            this.buzzerHandler.SetInputMask(this.IOUnitBName, inputMask);
            this.buzzerHandler.ReleaseBuzzer(this.IOUnitBName, IOnet.IOUnit.IONbuz.WorkModes.BUZZER);
        }

        public virtual void LockBuzzer() {
            this.buzzerHandler.LockBuzzer(this.IOUnitAName);
            this.buzzerHandler.LockBuzzer(this.IOUnitBName);
        }


        private void setTileLightOn(
            Tile tile) {
            if (tile is Tile) this.buzzerHandler.SetDMXChannel(this.IOUnitBName, tile.DMXStartAddress, new byte[] { 255, 255, 255 });
        }

        private void setTileLightOff() {
            foreach (Tile item in this.tilelist) this.setTileLightOff(item);
        }
        private void setTileLightOff(
            Tile tile) {
            if (tile is Tile) this.buzzerHandler.SetDMXChannel(this.IOUnitBName, tile.DMXStartAddress, new byte[] { 0, 0, 0 });
        }

        public void SelectSample() {
            this.Stop();
            if (this.selectedTileCrowdList != this.sample) {
                this.selectedTileCrowdList = this.sample;
                this.on_PropertyChanged("SelectedTileCrowdList");
            }
            this.SelectedTileCrowdIndex = 0;
        }

        public void SelectChallenge() {
            this.Stop(); 
            if (this.selectedTileCrowdList != this.challenge) {
                this.selectedTileCrowdList = this.challenge;
                this.on_PropertyChanged("SelectedTileCrowdList");
            }
            this.SelectedTileCrowdIndex = 0;
        }

        public void Start() {
            if (this.SelectedTileCrowdIndex < this.selectedTileCrowdList.Count) {
                this.enqueueTile();
                this.calcRemainingTilesCounter();
            }
        }
        public void Stop() {
            this.Vinsert_StopTimeToBeat();
            this.LockBuzzer();
            this.setTileLightOff();
            this.tileToTouchList.Clear();
        }

        private void enqueueTile() {
            this.tileToTouchList.Clear();
            if (this.SelectedTileCrowdIndex >= 0 &&
                this.SelectedTileCrowdIndex < this.selectedTileCrowdList.Count) {
                TileCrowd nextTileCrowd = this.SelectedTileCrowdList[this.SelectedTileCrowdIndex];
                if (nextTileCrowd is TileCrowd) {
                    foreach (int tileID in nextTileCrowd.TileIDList) {
                        if (tileID > 0 &&
                            tileID <= this.tilelist.Count) {
                            Tile tile = this.tilelist[tileID - 1];
                            this.setTileLightOn(tile);
                            this.tileToTouchList.Add(tileID, tile);
                        }
                    }
                }
            }
        }

        private void calcRemainingTilesCounter() {
            int counter = 0;
            for (int i = this.SelectedTileCrowdIndex; i < this.selectedTileCrowdList.Count; i++) {
                counter += this.selectedTileCrowdList[i].TilesCount;
            }
            this.RemainingTilesCounter = counter;
        }

        public void SetSample(TileCrowd[] sample) { this.Sample = sample; }

        public void SetChallenge(TileCrowd[] challenge) { this.Challenge = challenge; }

        public void Load(
            string filename) {
            string subSender = "Load";
            if (File.Exists(filename)) {
                try {
                    XmlSerializer serializer = new XmlSerializer(typeof(Data));
                    Data data;
                    using (StreamReader reader = new StreamReader(filename)) data = (Data)serializer.Deserialize(reader);
                    this.Sample = data.Sample;
                    this.Challenge = data.Challenge;
                    this.filename = filename;
                    this.on_PropertyChanged("Filename");
                }
                catch (Exception exc) {
                    // Fehler weitergeben
                    this.on_Error(subSender, exc.Message);
                }
            }
            else {
                // Fehler weitergeben
                this.on_Error(subSender, "Datei nicht vorhanden - '" + filename + "'");
            }
        }
        public void Save() { if (File.Exists(this.Filename)) this.SaveAs(this.Filename); }
        public void SaveAs(
            string filename) {
            string subSender = "SaveAs";
            try {
                // Dokument speichern
                Data data = new Data();
                data.Sample = this.Sample;
                data.Challenge = this.Challenge;
                XmlSerializer serializer = new XmlSerializer(typeof(Data));
                using (StreamWriter writer = new StreamWriter(filename)) serializer.Serialize(writer, data);
                this.filename = filename;
                this.on_PropertyChanged("Filename");
            }
            catch (Exception exc) {
                // Fehler weitergeben
                this.on_Error(subSender, exc.Message);
            }
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
            this.Vinsert_SetTimeToBeatCounter(this.RemainingTilesCounter);
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
        public void Vinsert_SetTimeToBeatCounter(int value) { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.TimeToBeat.SetCounter(value); }
        public void Vinsert_TimeToBeatOut() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.TimeToBeat.ToOut();
            this.Vinsert_ResetOffsetTime();
            this.Vinsert_ResetTimeToBeatTime();
        }
        public void Vinsert_PlayJingleHit() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.PlayJingleHit(); }
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

        void tile_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_tile_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_tile_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {}
            this.on_PropertyChanged("Tile");
        }

        void tile_LightOnTriggered(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_tile_LightOnTriggered);
            if (this.syncContext != null) this.syncContext.Post(callback, sender);
        }
        private void sync_tile_LightOnTriggered(object content) { this.setTileLightOn(content as Tile); }

        void tile_LightOffTriggered(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_tile_LightOffTriggered);
            if (this.syncContext != null) this.syncContext.Post(callback, sender);
        }
        private void sync_tile_LightOffTriggered(object content) { this.setTileLightOff(content as Tile); }

        void buzzerHandler_BuzUnit_Buzzered(object sender, IOnet.IOUnit.IONbuz.BuzzerIDParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_Buzzered);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_BuzUnit_Buzzered(object content) {
            IOnet.IOUnit.IONbuz.BuzzerIDParam_EventArgs e = content as IOnet.IOUnit.IONbuz.BuzzerIDParam_EventArgs;
            if (this.isActive &&
                !this.buzzerIsLocked &&
                e is IOnet.IOUnit.IONbuz.BuzzerIDParam_EventArgs) {
                foreach (Tile tile in this.tilelist) {
                    if (tile.BuzzerChannel == e.Arg.BuzzerID) {
                        if ((tile.SelectedIOUnit == AvailableIOUnits.UnitA && this.IOUnitAName == e.Arg.Name) ||
                            (tile.SelectedIOUnit == AvailableIOUnits.UnitB && this.IOUnitBName == e.Arg.Name)) {
                            this.doBuzzer(tile);
                            break;
                        }
                    }
                }
                this.buzzerLockTimer.Start();
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

        void buzzerLockTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerLockTimer_Elapsed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_buzzerLockTimer_Elapsed(object content) {
            this.buzzerIsLocked = false;
            this.ReleaseBuzzer();
        }

        #endregion
    }
}
