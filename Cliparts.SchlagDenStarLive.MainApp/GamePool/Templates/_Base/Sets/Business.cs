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

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates._Base.Sets {

    public class SingleSet : INotifyPropertyChanged {

        #region Properties

        private int value = 0;
        public int Value {
            get { return this.value; }
            set {
                if (this.value != value) {
                    if (value < 0) value = 0;
                    this.value = value;
                    this.on_PropertyChanged();
                    this.setValidValue();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.Sets.ValueStatusElements status = VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Idle;
        public VentuzScenes.GamePool._Modules.Sets.ValueStatusElements Status {
            get { return this.status; }
            set {
                if (this.status != value) {
                    this.status = value;
                    this.on_PropertyChanged();
                    this.setValidValue();
                }
            }
        }

        private int validValue;
        public int ValidValue {
            get { return this.validValue; }
            private set {
                if (this.validValue != value) {
                    this.validValue = value;
                    this.on_PropertyChanged();
                }
            }
        }

        #endregion


        #region Funktionen

        public SingleSet() { }

        public SingleSet(
            int value,
            VentuzScenes.GamePool._Modules.Sets.ValueStatusElements status) {
            this.Value = value;
            this.Status = status;
        }

        public void Reset() {
            this.Value = 0;
            this.Status = VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Idle;
        }

        private void setValidValue() {
            if (this.Status == VentuzScenes.GamePool._Modules.Sets.ValueStatusElements.Valid) this.ValidValue = this.Value;
            else this.ValidValue = 0;
        }

        public void Clone(
            SingleSet set) {
            if (set is SingleSet) {
                this.Value = set.Value;
                this.Status = set.Status;
            }
            else this.Reset();
        }

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

    public class Business : _Base.Business {

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

        private Cliparts.Tools.NetContact.ClientStates ioUnitConnectionStatus = Tools.NetContact.ClientStates.Missing;

        private WorkModes ioUnitWorkMode = WorkModes.NA;

        public BuzzerUnitStates IOUnitStatus { get; private set; }

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

        private int selectedSet = 0;
        public int SelectedSet {
            get { return this.selectedSet; }
            set {
                if (this.selectedSet != value) {
                    if (value < 0) this.SelectedSet = 0;
                    else if (value >= this.SetsCount) this.SelectedSet = this.SetsCount - 1;
                    else this.selectedSet = value;
                    this.on_PropertyChanged();
                }
            }
        }


        private int setsPositionX = 0;
        public int SetsPositionX {
            get { return this.setsPositionX; }
            set {
                if (this.setsPositionX != value) {
                    this.setsPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetSets();
                }
            }
        }

        private int setsPositionY = 0;
        public int SetsPositionY {
            get { return this.setsPositionY; }
            set {
                if (this.setsPositionY != value) {
                    this.setsPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetSets();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.Sets.StyleElements setsStyle = VentuzScenes.GamePool._Modules.Sets.StyleElements.ThreeSets;
        public VentuzScenes.GamePool._Modules.Sets.StyleElements SetsStyle {
            get { return this.setsStyle; }
            set {
                if (this.setsStyle != value) {
                    this.setsStyle = value;
                    this.on_PropertyChanged();
                    this.on_PropertyChanged("SetsCount");
                    this.Vinsert_SetSets();
                    this.calcSetSums();
                    this.SelectedSet = this.SelectedSet;
                }
            }
        }

        public int SetsCount {
            get {
                int count = 0;
                switch (this.SetsStyle) {
                    case VentuzScenes.GamePool._Modules.Sets.StyleElements.TwoSets:
                    case VentuzScenes.GamePool._Modules.Sets.StyleElements.TwoSetsSum:
                        count = 2;
                        break;
                    case VentuzScenes.GamePool._Modules.Sets.StyleElements.ThreeSets:
                    case VentuzScenes.GamePool._Modules.Sets.StyleElements.ThreeSetsSum:
                        count = 3;
                        break;
                    case VentuzScenes.GamePool._Modules.Sets.StyleElements.FourSetsSum:
                        count = 4;
                        break;
                    case VentuzScenes.GamePool._Modules.Sets.StyleElements.FiveSetsSum:
                        count = 5;
                        break;
                    case VentuzScenes.GamePool._Modules.Sets.StyleElements.SixSetsSum:
                        count = 6;
                        break;
                    case VentuzScenes.GamePool._Modules.Sets.StyleElements.SevenSetsSum:
                        count = 7;
                        break;
                    case VentuzScenes.GamePool._Modules.Sets.StyleElements.EightSetsSum:
                        count = 8;
                        break;
                    case VentuzScenes.GamePool._Modules.Sets.StyleElements.NineSetsSum:
                        count = 9;
                        break;
                    case VentuzScenes.GamePool._Modules.Sets.StyleElements.TenSetsSum:
                    case VentuzScenes.GamePool._Modules.Sets.StyleElements.TenSetsSumSmall:
                        count = 10;
                        break;
                    case VentuzScenes.GamePool._Modules.Sets.StyleElements.OneSet:
                    default:
                        count = 1;
                        break;
                }
                return count;
            }
        }

        public const int SetsCountMax = 10;

        protected List<SingleSet> leftPlayerSets = new List<SingleSet>();
        public SingleSet[] LeftPlayerSets {
            get {
                this.fillSets();
                return this.leftPlayerSets.ToArray(); 
            }
            set {
                this.fillSets();
                for (int i = 0; i < SetsCountMax; i++) {
                    if (value is SingleSet[] &&
                        value.Length > i) this.leftPlayerSets[i].Clone(value[i]);
                    else this.leftPlayerSets[i].Reset();
                }
                this.calcSetSums();
            }
        }

        protected int leftPlayerSetSum = 0;
        public int LeftPlayerSetSum {
            get { return this.leftPlayerSetSum; }
            private set {
                if (this.leftPlayerSetSum != value) {
                    if (value < 0) this.leftPlayerSetSum = 0;
                    else this.leftPlayerSetSum = value;
                    this.on_PropertyChanged();
                }
            }
        }

        protected int leftPlayerScoreOffset = 0;
        public int LeftPlayerScoreOffset {
            get { return this.leftPlayerScoreOffset; }
            set {
                if (this.leftPlayerScoreOffset != value) {
                    if (value < 0) leftPlayerScoreOffset = 0;
                    else this.leftPlayerScoreOffset = value;
                    this.on_PropertyChanged();
                }
            }
        }

        protected List<SingleSet> rightPlayerSets = new List<SingleSet>();
        public SingleSet[] RightPlayerSets {
            get {
                this.fillSets();
                return this.rightPlayerSets.ToArray();
            }
            set {
                this.fillSets();
                for (int i = 0; i < SetsCountMax; i++) {
                    if (value is SingleSet[] &&
                        value.Length > i) this.rightPlayerSets[i].Clone(value[i]);
                    else this.rightPlayerSets[i].Reset();
                }
                this.calcSetSums();
            }
        }

        protected int rightPlayerSetSum = 0;
        public int RightPlayerSetSum {
            get { return this.rightPlayerSetSum; }
            private set {
                if (this.rightPlayerSetSum != value) {
                    if (value < 0) rightPlayerSetSum = 0;
                    else this.rightPlayerSetSum = value;
                    this.on_PropertyChanged();
                }
            }
        }

        protected int rightPlayerScoreOffset = 0;
        public int RightPlayerScoreOffset {
            get { return this.rightPlayerScoreOffset; }
            set {
                if (this.rightPlayerScoreOffset != value) {
                    if (value < 0) rightPlayerScoreOffset = 0;
                    else this.rightPlayerScoreOffset = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool repressCalcSetSums = false;

        #endregion


        #region Funktionen

        public Business() { }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {
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
        }

        public override void Dispose() {
            base.Dispose();
            this.clearSets();
        }

        public override void ResetData() {
            base.ResetData();
            this.SelectedPlayer = Content.Gameboard.PlayerSelection.LeftPlayer;
            this.SelectedSet = 0;
            this.LeftPlayerScoreOffset = 0;
            this.RightPlayerScoreOffset = 0;
            foreach (SingleSet item in this.leftPlayerSets) item.Reset();
            foreach (SingleSet item in this.rightPlayerSets) item.Reset();
        }

        private void fillBuzzerUnitList(
            InfoParam[] unitInfoList) {
            this.ioUnitNameList.Clear();
            if (unitInfoList is InfoParam[]) {
                foreach (InfoParam item in unitInfoList) {
                    if (item is InfoParam) this.ioUnitNameList.Add(item.Name);
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

        public virtual void ReleaseBuzzer() {
            bool[] inputMask = new bool[8];
            if (this.BuzzerChannel > 0 &&
                this.BuzzerChannel <= inputMask.Length) inputMask[this.BuzzerChannel - 1] = true;
            this.buzzerHandler.SetInputMask(this.IOUnitName, inputMask);
            this.buzzerHandler.ReleaseBuzzer(this.IOUnitName, IOnet.IOUnit.IONbuz.WorkModes.EVENT);
        }

        public virtual void LockBuzzer() {
            this.buzzerHandler.LockBuzzer(this.IOUnitName);
        }

        public virtual void DoBuzzer() {
            switch (this.SelectedPlayer) {
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.LeftPlayer:
                    if (this.SelectedSet >= 0 && this.SelectedSet < this.LeftPlayerSets.Length) this.LeftPlayerSets[this.SelectedSet].Value++;
                    break;
                case Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard.PlayerSelection.RightPlayer:
                    if (this.SelectedSet >= 0 && this.SelectedSet < this.RightPlayerSets.Length) this.RightPlayerSets[this.SelectedSet].Value++;
                    break;
            }
            this.Vinsert_SetSets();
        }

        protected void fillSets() {
            while (this.leftPlayerSets.Count < SetsCountMax) {
                SingleSet item = new SingleSet();
                item.PropertyChanged += this.set_PropertyChanged;
                this.leftPlayerSets.Add(item);
            }
            while (this.rightPlayerSets.Count < SetsCountMax) {
                SingleSet item = new SingleSet();
                item.PropertyChanged += this.set_PropertyChanged;
                this.rightPlayerSets.Add(item);
            }
        }

        protected void clearSets() {
            foreach (SingleSet item in this.leftPlayerSets) item.PropertyChanged -= this.set_PropertyChanged;
            foreach (SingleSet item in this.rightPlayerSets) item.PropertyChanged -= this.set_PropertyChanged;
        }

        protected void calcSetSums() {
            this.repressCalcSetSums = true;
            int index = 0;
            int setSum = 0;
            foreach (SingleSet item in this.leftPlayerSets) {
                if (index < this.SetsCount) setSum += item.ValidValue;
                index++;
            }
            this.LeftPlayerSetSum = setSum + this.LeftPlayerScoreOffset;
            index = 0;
            setSum = 0;
            foreach (SingleSet item in this.rightPlayerSets) {
                if (index < this.SetsCount) setSum += item.ValidValue;
                index++;
            }
            this.RightPlayerSetSum = setSum + this.RightPlayerScoreOffset;
            this.repressCalcSetSums = false;
        }

        public virtual void Vinsert_SetsIn() { }
        public void Vinsert_SetsIn(
            VentuzScenes.GamePool._Modules.Sets scene) {
            if (scene is VentuzScenes.GamePool._Modules.Sets) {
                this.Vinsert_SetSets(scene);
                scene.ToIn();
            }
        }
        public virtual void Vinsert_SetSets() { }
        public void Vinsert_SetSets(VentuzScenes.GamePool._Modules.Sets scene) {
            this.Vinsert_SetSets(scene, this.LeftPlayerScoreOffset, this.RightPlayerScoreOffset, this.LeftPlayerSets, this.RightPlayerSets);
        }
        public void Vinsert_SetSets(
            VentuzScenes.GamePool._Modules.Sets scene,
            int leftPlayerScoreOffset,
            int rightPlayerScoreOffset,
            SingleSet[] leftPlayerSets,
            SingleSet[] rightPlayerSets) {
            if (scene is VentuzScenes.GamePool._Modules.Sets) {
                scene.SetPositionX(this.SetsPositionX);
                scene.SetPositionY(this.SetsPositionY);
                scene.SetStyle(this.SetsStyle);
                scene.SetTopName(this.LeftPlayerName);
                scene.SetBottomName(this.RightPlayerName);
                scene.SetTopScoreOffset(leftPlayerScoreOffset);
                scene.SetBottomScoreOffset(rightPlayerScoreOffset);
                int id = 1;
                foreach (SingleSet set in leftPlayerSets) {
                    scene.SetTopStatus(id, set.Status);
                    scene.SetTopValue(id, set.Value);
                    id++;
                }
                id = 1;
                foreach (SingleSet set in rightPlayerSets) {
                    scene.SetBottomStatus(id, set.Status);
                    scene.SetBottomValue(id, set.Value);
                    id++;
                }
            }
        }
        public virtual void Vinsert_SetsOut() { }
        public void Vinsert_SetsOut(VentuzScenes.GamePool._Modules.Sets scene) { if (scene is VentuzScenes.GamePool._Modules.Sets) scene.ToOut(); }

        #endregion


        #region Events.Outgoing
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
                e.Arg.Name == this.IOUnitName &&
                e.Arg.BuzzerID == this.BuzzerChannel) this.DoBuzzer();
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

        void set_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_set_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_set_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "ValidValue" &&
                    !this.repressCalcSetSums) this.calcSetSums();
            }
        }

        #endregion

    }
}
