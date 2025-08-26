using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Cliparts.Serialization;

using Cliparts.IOnet.IOUnit.IONbase;
using Cliparts.IOnet.IOUnit.IONbuz;

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;
using Cliparts.SchlagDenStarLive.MainApp.Settings;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TimeOffsetAdditionScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TimeOffsetAdditionScore {

    public class Data {
        public DatasetContent[] DataList;
        public int SelectedIndex;
    }

    public class DatasetContent : Messaging.Message, INotifyPropertyChanged {

        public const int DurationMin = 3;

        #region Properties

        private int duration = 0;
        public int Duration {
            get { return this.duration; }
            set {
                if (this.duration != value) {
                    if (value < DurationMin) this.duration = DurationMin;
                    else this.duration = value;
                    this.buildToString();
                    this.on_PropertyChanged();
                }
            }
        }

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DatasetContent() {}

        private void buildToString() { this.toString = this.Duration.ToString(); }

        public override string ToString() { return this.toString; }

        #endregion


        #region Events.Outgoing

        public event PropertyChangedEventHandler PropertyChanged;
        protected void on_PropertyChanged([CallerMemberName] string callerName = "") { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, callerName); }
        protected void on_PropertyChanged(PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(this, this.PropertyChanged, e); }
        protected void on_PropertyChanged(object sender, PropertyChangedEventArgs e) { Helper.raisePropertyChangedEvent(sender, this.PropertyChanged, e); }

        #endregion

        #region Events.Incoming
        #endregion

    }

    public class Business : _Base.Score.Business {

        #region Properties

        MidiHandler.Business midiHandler;

        private int gamePositionX = 0;
        public int GamePositionX {
            get { return this.gamePositionX; }
            set {
                if (this.gamePositionX != value) {
                    this.gamePositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
                }
            }
        }

        private int gamePositionY = 0;
        public int GamePositionY {
            get { return this.gamePositionY; }
            set {
                if (this.gamePositionY != value) {
                    this.gamePositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
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
                    if (value == null)
                        value = string.Empty;
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

        private int pole1BuzzerChannel = 1;
        public int Pole1BuzzerChannel {
            get { return this.pole1BuzzerChannel; }
            set {
                if (this.pole1BuzzerChannel != value) {
                    if (value < 1)
                        value = 1;
                    if (value > 8)
                        value = 8;
                    this.pole1BuzzerChannel = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int pole2BuzzerChannel = 2;
        public int Pole2BuzzerChannel {
            get { return this.pole2BuzzerChannel; }
            set {
                if (this.pole2BuzzerChannel != value) {
                    if (value < 1)
                        value = 1;
                    if (value > 8)
                        value = 8;
                    this.pole2BuzzerChannel = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int pole3BuzzerChannel = 3;
        public int Pole3BuzzerChannel {
            get { return this.pole3BuzzerChannel; }
            set {
                if (this.pole3BuzzerChannel != value) {
                    if (value < 1)
                        value = 1;
                    if (value > 8)
                        value = 8;
                    this.pole3BuzzerChannel = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private Tools.DMX.DMXNet dMX = new Tools.DMX.DMXNet();
        private byte[] universe = new byte[256];

        private Color offColor = Color.Black;
        public Color OffColor {
            get { return this.offColor; }
            set {
                if (this.offColor != value) {
                    this.offColor = value;
                    this.on_PropertyChanged();
                    this.SetDMXValues();
                }
            }
        }

        private Color onColor = Color.Green;
        public Color OnColor {
            get { return this.onColor; }
            set {
                if (this.onColor != value) {
                    this.onColor = value;
                    this.on_PropertyChanged();
                    this.SetDMXValues();
                }
            }
        }

        private int pole1DMXStart = 1;
        public int Pole1DMXStart {
            get { return this.pole1DMXStart; }
            set {
                if (this.pole1DMXStart != value) {
                    if (value < 1) this.pole1DMXStart = 1;
                    else if (value > 254) this.pole1DMXStart = 254;
                    else this.pole1DMXStart = value;
                    this.on_PropertyChanged();
                    this.SetDMXValues();
                }
            }
        }

        private int pole2DMXStart = 4;
        public int Pole2DMXStart {
            get { return this.pole2DMXStart; }
            set {
                if (this.pole2DMXStart != value) {
                    if (value < 1) this.pole2DMXStart = 1;
                    else if (value > 254) this.pole2DMXStart = 254;
                    else this.pole2DMXStart = value;
                    this.on_PropertyChanged();
                    this.SetDMXValues();
                }
            }
        }

        private int pole3DMXStart = 7;
        public int Pole3DMXStart {
            get { return this.pole3DMXStart; }
            set {
                if (this.pole3DMXStart != value) {
                    if (value < 1) this.pole3DMXStart = 1;
                    else if (value > 254) this.pole3DMXStart = 254;
                    else this.pole3DMXStart = value;
                    this.on_PropertyChanged();
                    this.SetDMXValues();
                }
            }
        }


        private string filename = string.Empty;
        public string Filename {
            get { return this.filename; }
            set { if (this.filename != value) this.Load(value); }
        }

        private List<DatasetContent> dataList = new List<DatasetContent>();
        [NotSerialized]
        public DatasetContent[] DataList {
            get { return this.dataList.ToArray(); }
            set {
                this.tryRemoveAllDatasets();
                if (value is DatasetContent[]) {
                    foreach (DatasetContent item in value) this.tryAddDataset(item, -1);
                }
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                this.SelectDataset(0);
            }
        }

        private List<string> names = new List<string>();
        public string[] NameList { get { return this.names.ToArray(); } }

        public int DatasetsCount { get { return this.dataList.Count; } }

        public DatasetContent SelectedDataset { get; private set; }

        public int SelectedDatasetIndex { get; private set; }

        private int taskCounter = 0;
        [NotSerialized]
        public int TaskCounter {
            get { return this.taskCounter; }
            set {
                if (this.taskCounter != value) {
                    if (value < 0) value = 0;
                    if (!this.SampleIncluded &&
                        value < 1) value = 1;
                    this.taskCounter = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool sampleIncluded = true;
        public bool SampleIncluded {
            get { return this.sampleIncluded; }
            set {
                if (this.sampleIncluded != value) {
                    this.sampleIncluded = value;
                    this.on_PropertyChanged();
                    // TaskCounter validieren
                    int id = this.TaskCounter;
                    this.TaskCounter = -1;
                    this.TaskCounter = id;
                }
            }
        }

        public const int PolesToGo = 7;

        private int poleCounter = 0;
        [NotSerialized]
        public int PoleCounter {
            get { return this.poleCounter; }
            set {
                if (this.poleCounter != value) {
                    if (value < 0) this.poleCounter = 0;
                    else if (value > PolesToGo) this.poleCounter = PolesToGo;
                    else this.poleCounter = value;
                    this.SetDMXValues();
                    this.Vinsert_SetTimer();
                    this.on_PropertyChanged();
                }
            }
        }

        private int poleID {
            get {
                if (this.PoleCounter % 3 == 0) return 1;
                else if (this.PoleCounter % 3 == 1) return 2;
                else if (this.PoleCounter % 3 == 2) return 3;
                else return 0;
            }
        }

        private double timerCurrentTime = -1;
        public double TimerCurrentTime {
            get { return this.timerCurrentTime; }
            protected set {
                if (this.timerCurrentTime != value) {
                    this.timerCurrentTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool gameIsRunning = false;
        public bool GameIsRunning {
            get { return this.gameIsRunning; }
            protected set {
                if (this.gameIsRunning != value) {
                    this.gameIsRunning = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private double timerTimeToBeat = 0;
        public double TimerTimeToBeat {
            get { return this.timerTimeToBeat; }
            protected set {
                if (this.timerTimeToBeat != value) {
                    this.timerTimeToBeat = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private List<double> offsetList = new List<double>();

        private double offsetSum = 0;
        [NotSerialized]
        public double OffsetSum {
            get { return this.offsetSum; }
            set {
                if (this.offsetSum != value) {
                    if (value < 0) this.offsetSum = 0;
                    else this.offsetSum = value;
                    this.Vinsert_SetTimer();
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

        private bool repressPropertyChanged = false;

        #endregion


        #region Funktionen

        public Business() { }
        public Business(
            string typeIdentifier)
            : base(typeIdentifier) {

            this.ContentControl = new UserControlContent();
            this.GameControl = new UserControlGame();

            this.ClassInfo = string.Format("'{0}' of 'Templates.TimeOffsetAdditionScore'", typeIdentifier);
        }

        public override void New() {
            base.New();
            this.Filename = string.Empty;
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

            //wenn die Datasets einen Synccontext benötigen, dann wird er hierdurch zugeordnet
            this.DataList = this.dataList.ToArray();

            this.midiHandler = midiHandler;

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
            this.insertScene.Game.PreciseTimeReceived += this.insertScene_PreciseTimeReceived;
            this.insertScene.Game.StopFiredReceived += this.insertScene_StopFiredReceived;
            this.insertScene.Game.PropertyChanged += this.insertScene_PropertyChanged;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);

            this.AllPolesBlack();
        }

        public override void Dispose() {
            base.Dispose();

            this.buzzerHandler.BuzUnit_Buzzered += this.buzzerHandler_BuzUnit_Buzzered;
            this.buzzerHandler.Unit_ConnectionStatusChanged += this.buzzerHandler_UnitConnectionStatusChanged;
            this.buzzerHandler.Unit_ConnectionStatusRequest += this.buzzerHandler_UnitConnectionStatusRequest;
            this.buzzerHandler.Unit_InfoListChanged += this.buzzerHandler_UnitInfoListChanged;
            this.buzzerHandler.BuzUnit_WorkmodeChanged += this.buzzerHandler_BuzUnit_WorkmodeChanged;
            this.buzzerHandler.BuzUnit_WorkmodeRequest += this.buzzerHandler_BuzUnit_WorkmodeRequest;

            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Game.PreciseTimeReceived -= this.insertScene_PreciseTimeReceived;
            this.insertScene.Game.StopFiredReceived -= this.insertScene_StopFiredReceived;
            this.insertScene.Game.PropertyChanged -= this.insertScene_PropertyChanged;
        }

        public override void ResetData() {
            base.ResetData();
            this.GameIsRunning = false;
            this.TaskCounter = 0;
            this.PoleCounter = 0;
            this.offsetList.Clear();
            this.OffsetSum = 0;
            this.TimerTimeToBeat = 0;
            this.SelectDataset(0);
            this.AllPolesBlack();
        }

        public override void Next() {
            base.Next();
            this.GameIsRunning = false;
            this.TaskCounter++;
            this.PoleCounter = 0;
            this.offsetList.Clear();
            this.OffsetSum = 0;
            this.TimerTimeToBeat = 0;
            this.SelectDataset(this.SelectedDatasetIndex + 1);
            this.SetDMXValues();
        }

        public void StartGame() {
            this.GameIsRunning = true;
            this.SetDMXValues();
            //this.midiHandler.SendEvent("AllPoles");
        }

        public void StopGame() {
            this.GameIsRunning = false;
            this.SetDMXValues();
            this.LockBuzzer();
        }

        public void NextPlayer() {
            this.GameIsRunning = false;
            this.TimerTimeToBeat = this.OffsetSum;
            this.PoleCounter = 0;
            this.offsetList.Clear();
            this.OffsetSum = 0;
        }

        public void SetTimeToBeat(
            string value) {
            if (string.IsNullOrEmpty(value)) this.TimerTimeToBeat = 0;
            else {
                value = value.Replace('.', ',');
                double result;
                if (double.TryParse(value, out result)) this.TimerTimeToBeat = result;
                else this.TimerTimeToBeat = 0;
            }
        }

        private void fillBuzzerUnitList(
            InfoParam[] unitInfoList) {
            this.ioUnitNameList.Clear();
            if (unitInfoList is InfoParam[]) {
                foreach (InfoParam item in unitInfoList) {
                    if (item is InfoParam)
                        this.ioUnitNameList.Add(item.Name);
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
            if (this.Pole1BuzzerChannel > 0 &&
                this.Pole1BuzzerChannel <= inputMask.Length)
                inputMask[this.Pole1BuzzerChannel - 1] = true;
            if (this.Pole2BuzzerChannel > 0 &&
                this.Pole2BuzzerChannel <= inputMask.Length)
                inputMask[this.Pole2BuzzerChannel - 1] = true;
            if (this.Pole3BuzzerChannel > 0 &&
                this.Pole3BuzzerChannel <= inputMask.Length)
                inputMask[this.Pole3BuzzerChannel - 1] = true;
            this.buzzerHandler.SetInputMask(this.IOUnitName, inputMask);
            this.buzzerHandler.ReleaseBuzzer(this.IOUnitName, WorkModes.EVENT);
        }

        public virtual void LockBuzzer() {
            this.buzzerHandler.LockBuzzer(this.IOUnitName);
        }

        public void DoBuzzer() {
            if (this.PoleCounter < PolesToGo) {
                if (this.PoleCounter == PolesToGo - 1) {
                    this.LockBuzzer();
                    this.Vinsert_StopTimer();
                    this.PlayFinishSound();
                    this.midiHandler.SendEvent("AllPoles");
                }
                else {
                    this.Vinsert_NextTimer();
                    this.PlayBuzzerSound();
                    if (this.poleID == 1) this.midiHandler.SendEvent("Pole1");
                    else if (this.poleID == 2) this.midiHandler.SendEvent("Pole2");
                    else if (this.poleID == 3) this.midiHandler.SendEvent("Pole3");
                }
                int index = this.poleCounter;
                Helper.invokeActionAfterDelay(() => this.addOffset(index), 1500, this.syncContext);
                this.PoleCounter++;
            }
        }

        private void addOffset(
            int index) {
            double sum = 0;
            int counter = 0;
            foreach (double item in this.offsetList) {
                if (counter <= index) sum += Math.Abs(item);
                else break;
                counter++;
            }
            this.OffsetSum = sum;
        }

        internal void AllPolesBlack() {
            byte[] valueList = new byte[256];
            this.setDMXValues(1, valueList);
        }

        internal void Pole1On(bool value) {
            byte[] onValue = new byte[] { this.OnColor.R, this.OnColor.G, this.OnColor.B };
            byte[] offValue = new byte[] { this.OffColor.R, this.OffColor.G, this.OffColor.B };
            int startChannel = this.Pole1DMXStart;
            if (value) this.setDMXValues(startChannel, onValue);
            else this.setDMXValues(startChannel, offValue);
        }

        internal void Pole2On(bool value) {
            byte[] onValue = new byte[] { this.OnColor.R, this.OnColor.G, this.OnColor.B };
            byte[] offValue = new byte[] { this.OffColor.R, this.OffColor.G, this.OffColor.B };
            int startChannel = this.Pole2DMXStart;
            if (value) this.setDMXValues(startChannel, onValue);
            else this.setDMXValues(startChannel, offValue);
        }

        internal void Pole3On(bool value) {
            byte[] onValue = new byte[] { this.OnColor.R, this.OnColor.G, this.OnColor.B };
            byte[] offValue = new byte[] { this.OffColor.R, this.OffColor.G, this.OffColor.B };
            int startChannel = this.Pole3DMXStart;
            if (value) this.setDMXValues(startChannel, onValue);
            else this.setDMXValues(startChannel, offValue);
        }

        internal void SetDMXValues() {
            byte[] valueList = new byte[256];
            byte[] onValue = new byte[] { this.OnColor.R, this.OnColor.G, this.OnColor.B };
            byte[] offValue = new byte[] { this.OffColor.R, this.OffColor.G, this.OffColor.B };
            if (this.PoleCounter == PolesToGo ||
                !this.GameIsRunning) {
                Array.Copy(offValue, 0, valueList, this.Pole1DMXStart - 1, offValue.Length);
                Array.Copy(offValue, 0, valueList, this.Pole2DMXStart - 1, offValue.Length);
                Array.Copy(offValue, 0, valueList, this.Pole3DMXStart - 1, offValue.Length);
            }
            else {
                if (this.PoleCounter % 3 == 0) Array.Copy(onValue, 0, valueList, this.Pole1DMXStart - 1, onValue.Length);
                else Array.Copy(offValue, 0, valueList, this.Pole1DMXStart - 1, offValue.Length);
                if (this.PoleCounter % 3 == 1) Array.Copy(onValue, 0, valueList, this.Pole2DMXStart - 1, onValue.Length);
                else Array.Copy(offValue, 0, valueList, this.Pole2DMXStart - 1, offValue.Length);
                if (this.PoleCounter % 3 == 2) Array.Copy(onValue, 0, valueList, this.Pole3DMXStart - 1, onValue.Length);
                else Array.Copy(offValue, 0, valueList, this.Pole3DMXStart - 1, offValue.Length);
            }
            this.setDMXValues(1, valueList);
        }

        private void setDMXValues(
            int startChannel,
            byte[] values) {
            if (this.dMX is Tools.DMX.DMXNet) {
                try {
                    byte startIndex = Convert.ToByte(startChannel - 1);
                    if (startIndex + values.Length <= this.universe.Length) Array.Copy(values, 0, this.universe, startIndex, values.Length);
                    this.dMX.SetDmxDataToEspNet(0, 0, this.universe);
                }
                catch (Exception) {
                }
            }
        }

        public DatasetContent GetDataset(
            int index) {
            if (index >= 0 &&
                index < this.dataList.Count) return this.dataList[index];
            else return null;
        }

        public int GetDatasetIndex(
            DatasetContent dataset) {
            int index = -1;
            int datasetIndex = 0;
            foreach (DatasetContent item in this.dataList) {
                if (item == dataset) {
                    index = datasetIndex;
                    break;
                }
                datasetIndex++;
            }
            return index;
        }

        public void SelectDataset(
            int index) {
            if (index < 0) index = 0;
            if (index >= this.dataList.Count) index = this.dataList.Count - 1;
            this.SelectedDatasetIndex = index;
            this.on_PropertyChanged("SelectedDatasetIndex");
            this.SelectedDataset = this.GetDataset(index);
            this.on_PropertyChanged("SelectedDataset");
        }

        public void AddDataset(
            DatasetContent newDataset,
            int insertIndex) {
            if (this.tryAddDataset(newDataset, insertIndex)) this.on_PropertyChanged("NameList");
            if (this.SelectedDataset == null) this.SelectDataset(0);
            this.Save();
        }
        private bool tryAddDataset(
            DatasetContent newDataset,
            int insertIndex) {
            if (newDataset is DatasetContent &&
                !this.dataList.Contains(newDataset)) {
                newDataset.Error += this.dataset_Error;
                newDataset.PropertyChanged += this.dataset_PropertyChanged;
                if (insertIndex >= 0 &&
                    insertIndex < this.DatasetsCount) {
                    this.dataList.Insert(insertIndex, newDataset);
                    this.names.Insert(insertIndex, newDataset.ToString());
                }
                else {
                    this.dataList.Add(newDataset);
                    this.names.Add(newDataset.ToString());
                }
                return true;
            }
            else return false;
        }

        public bool TryMoveDatasetUp(
            int index) {
            if (index > 0 &&
                index < this.DatasetsCount) {
                DatasetContent dataset = this.GetDataset(index);
                this.dataList.RemoveAt(index);
                this.dataList.Insert(index - 1, dataset);
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                this.Save();
                return true;
            }
            else return false;
        }
        public bool TryMoveDatasetDown(
            int index) {
            if (index >= 0 &&
                index < this.DatasetsCount - 1) {
                DatasetContent dataset = this.GetDataset(index);
                this.dataList.RemoveAt(index);
                this.dataList.Insert(index + 1, dataset);
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                this.Save();
                return true;
            }
            else return false;
        }

        public void RemoveAllDatasets() {
            if (this.tryRemoveAllDatasets()) {
                this.SelectDataset(0);
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                this.Save();
            }
        }
        private bool tryRemoveAllDatasets() {
            bool datasetRemoved = false;
            DatasetContent[] datasetList = this.dataList.ToArray();
            foreach (DatasetContent item in datasetList) datasetRemoved = this.removeDataset(item) | datasetRemoved;
            return datasetRemoved;
        }
        public bool TryRemoveDataset(
            int index) {
            if (this.removeDataset(this.GetDataset(index))) {
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                if (index <= this.SelectedDatasetIndex) this.SelectDataset(this.SelectedDatasetIndex);
                this.Save();
                return true;
            }
            else return false;
        }
        private bool removeDataset(
            DatasetContent dataset) {
            if (dataset is DatasetContent &&
                this.dataList.Contains(dataset)) {
                dataset.Error -= this.dataset_Error;
                dataset.PropertyChanged -= this.dataset_PropertyChanged;
                this.dataList.Remove(dataset);
                return true;
            }
            else return false;
        }

        private void buildNameList() {
            this.names.Clear();
            this.repressPropertyChanged = true;
            foreach (DatasetContent item in this.dataList) {
                this.names.Add(item.ToString());
            }
            this.repressPropertyChanged = false;
        }

        public void Load(
            string filename) {
            string subSender = "Load";
            if (File.Exists(filename)) {
                try {
                    XmlSerializer serializer = new XmlSerializer(typeof(Data));
                    Data data;
                    using (StreamReader reader = new StreamReader(filename)) data = (Data)serializer.Deserialize(reader);
                    this.DataList = data.DataList;
                    this.SelectDataset(data.SelectedIndex);
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
                data.DataList = this.DataList;
                data.SelectedIndex = this.SelectedDatasetIndex;
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

        public override void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score); }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }

        public void Vinsert_TimerIn() {
            this.Vinsert_SetTimer();
            if (this.insertScene is Insert &&
                this.insertScene.Game is Game) {
                this.insertScene.Game.ToIn();
            }
        }
        public void Vinsert_SetTimer() { if (this.insertScene is Insert) this.Vinsert_SetTimer(this.insertScene.Game, this.PoleCounter, this.OffsetSum); }
        public void Vinsert_SetTimer(
            Game scene,
            int counter,
            double offsetSum) {
            if (scene is Game) {
                scene.SetPositionX(this.GamePositionX);
                scene.SetPositionY(this.GamePositionY);
                scene.SetCounter(counter);
                scene.SetOffsetSum(offsetSum);
            }
        }
        public void Vinsert_StartTimer() {
            if (this.insertScene is Insert && this.insertScene.Game is Game) {
                this.GameIsRunning = true;
                this.SetDMXValues();
                this.insertScene.Game.StartTimer();
            }
        }
        public void Vinsert_NextTimer() {
            if (this.insertScene is Insert && this.insertScene.Game is Game) this.insertScene.Game.NextTimer();
        }
        public void Vinsert_ShowOffset(
            double offset) {
            if (this.insertScene is Insert && this.insertScene.Game is Game) {
                this.insertScene.Game.SetOffset(offset);
                this.insertScene.Game.OffsetToIn();
            }
        }
        public void Vinsert_StopTimer() {
            if (this.insertScene is Insert && this.insertScene.Game is Game) this.insertScene.Game.StopTimer();
        }
        public void Vinsert_ResetTimer() {
            if (this.insertScene is Insert && this.insertScene.Game is Game) this.insertScene.Game.ResetTimer();
        }
        public void Vinsert_TimeToBeatIn() {
            if (this.insertScene is Insert && 
                this.insertScene.Game is Game &&
                this.TimerTimeToBeat > 0) {
                this.insertScene.Game.SetToBeat(this.TimerTimeToBeat);
                this.insertScene.Game.ToBeatToIn();
            }
        }
        public void Vinsert_TimerOut() {
            if (this.insertScene is Insert && this.insertScene.Game is Game) this.insertScene.Game.ToOut();
        }
        public void PlayBuzzerSound() { if (this.insertScene is Insert && this.insertScene.Game is Game) this.insertScene.Game.PlayBuzzerSound(); }
        public void PlayFinishSound() { if (this.insertScene is Insert && this.insertScene.Game is Game) this.insertScene.Game.PlayFinishSound(); }

        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void Vfullscreen_ShowFreetext() {
            this.Vfullscreen_ClearText();
            base.Vfullscreen_ShowFreetext(); 
        }
        public void Vfullscreen_ClearText() { this.Vfullscreen_SetFreetext(string.Empty); }

        public void Vfullscreen_ShowOffset(
            double value) {
            if (value > 0) {
                this.Vfullscreen_SetFreetextColor(Color.Green);
                this.Vfullscreen_SetFreetext("zu\r\nlangsam");
            }
            else if (value < 0) {
                this.Vfullscreen_SetFreetextColor(Color.Red);
                this.Vfullscreen_SetFreetext("zu\r\nschnell");
            }
            else {
                this.Vfullscreen_SetFreetextColor(Color.Black);
                this.Vfullscreen_SetFreetext("perfekt");
            }
            Helper.invokeActionAfterDelay(this.Vfullscreen_ClearText, 2000, this.syncContext);

        }

        public override void ClearGraphic() {
            base.ClearGraphic();
            this.insertScene.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming

        void buzzerHandler_BuzUnit_Buzzered(object sender, IOnet.IOUnit.IONbuz.BuzzerIDParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_Buzzered);
            if (this.syncContext != null)
                this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_BuzUnit_Buzzered(object content) {
            IOnet.IOUnit.IONbuz.BuzzerIDParam_EventArgs e = content as IOnet.IOUnit.IONbuz.BuzzerIDParam_EventArgs;
            if (this.isActive &&
                e is IOnet.IOUnit.IONbuz.BuzzerIDParam_EventArgs &&
                e.Arg.Name == this.IOUnitName &&
                e.Arg.BuzzerID == this.poleID) {
                this.DoBuzzer();
            }
        }

        void buzzerHandler_UnitConnectionStatusChanged(object sender, IOnet.IOUnit.IONbase.ConnectionStatusParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_UnitConnectionStatusChanged);
            if (this.syncContext != null)
                this.syncContext.Post(callback, e);
        }
        void buzzerHandler_UnitConnectionStatusRequest(object sender, IOnet.IOUnit.IONbase.ConnectionStatusParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_UnitConnectionStatusChanged);
            if (this.syncContext != null)
                this.syncContext.Post(callback, e);
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
            if (this.syncContext != null)
                this.syncContext.Post(callback, e);
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
            if (this.syncContext != null)
                this.syncContext.Post(callback, e);
        }
        void buzzerHandler_BuzUnit_WorkmodeRequest(object sender, IOnet.IOUnit.IONbuz.WorkModeParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_WorkmodeChanged);
            if (this.syncContext != null)
                this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_BuzUnit_WorkmodeChanged(object content) {
            IOnet.IOUnit.IONbuz.WorkModeParam_EventArgs e = content as IOnet.IOUnit.IONbuz.WorkModeParam_EventArgs;
            if (e is IOnet.IOUnit.IONbuz.WorkModeParam_EventArgs &&
                e.Arg.Name == this.IOUnitName) {
                this.ioUnitWorkMode = e.Arg.WorkMode;
                this.checkIOUnitStatus();
            }
        }

        void dataset_Error(object sender, Messaging.ErrorEventArgs e) { this.on_Error(sender, e); }

        void dataset_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_dataset_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_dataset_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "Duration") {
                    this.buildNameList();
                    this.on_PropertyChanged("NameList");
                }
            }
         
            this.Save();
        }

        void insertScene_PreciseTimeReceived(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_insertScene_PreciseTimeReceived);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_insertScene_PreciseTimeReceived(object content) {
            if (this.insertScene.Game.PreciseTime > 0 &&
                this.offsetList.Count < PolesToGo - 2 &&
                this.GameIsRunning) {
                double offset = this.insertScene.Game.PreciseTime;
                if (this.SelectedDataset is DatasetContent) offset -= this.SelectedDataset.Duration;
                this.Vinsert_ShowOffset(offset);
                this.offsetList.Add(offset);
                this.Vfullscreen_ShowOffset(offset);
            }
        }

        void insertScene_StopFiredReceived(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_insertScene_StopFiredReceived);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_insertScene_StopFiredReceived(object content) {
            if (this.insertScene.Game.PreciseTime > 0 &&
                this.offsetList.Count == PolesToGo - 2 &&
                this.GameIsRunning) {
                double offset = this.insertScene.Game.PreciseTime;
                this.TimerCurrentTime = offset;
                if (this.SelectedDataset is DatasetContent) offset -= this.SelectedDataset.Duration;
                this.Vinsert_ShowOffset(offset);
                this.offsetList.Add(offset);
                this.GameIsRunning = false;
                this.Vfullscreen_ShowOffset(offset);
            }
        }

        protected void insertScene_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timeToBeat_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timeToBeat_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "CurrentTime") this.TimerCurrentTime = Convert.ToDouble(this.insertScene.Game.CurrentTime);
            }
        }


        #endregion

    }
}
