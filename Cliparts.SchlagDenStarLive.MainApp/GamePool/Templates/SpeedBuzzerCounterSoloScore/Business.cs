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

using Cliparts.IOnet.IOUnit.IONbase;
using Cliparts.IOnet.IOUnit.IONbuz;

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;
using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;
using Cliparts.Serialization;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.SpeedBuzzerCounterSoloScore;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.SpeedBuzzerCounterSoloScore {

    public enum StepPositions { Position_1, Position_2, Position_3, Position_4 }

    public class Data {
        public DatasetContent[] DataList;
        public int SelectedIndex;
    }

    public class DatasetContent : Messaging.Message, INotifyPropertyChanged {

        #region Properties

        private string name = string.Empty;
        public string Name {
            get { return this.name; }
            set {
                if (this.name != value) {
                    this.name = value;
                    this.buildToString();
                    this.on_PropertyChanged();
                }
            }
        }

        private int score = 5;
        public int Score {
            get { return this.score; }
            set {
                if (this.score != value) {
                    if (value < 1) this.score = 1;
                    else this.score = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private List<StepPositions> stepList = new List<StepPositions>();
        public StepPositions[] StepList {
            get { return this.stepList.ToArray(); }
            set {
                this.stepList.Clear();
                if (value is StepPositions[]) this.stepList = new List<StepPositions>(value);
                this.on_PropertyChanged("StepList");
            }
        }


        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DatasetContent() { }
        public DatasetContent(
            string name,
            List<StepPositions> stepList) {
            this.Name = name;
            if (stepList == null) this.stepList = new List<StepPositions>();
            else this.stepList = stepList;
        }

        public void InsertStep(
            int index,
            StepPositions item) {
            if (index < 0) index = 0;
            int maxIndex = this.stepList.Count - 1;
            if (index > maxIndex) this.stepList.Add(item);
            else this.stepList.Insert(index, item);
            this.on_PropertyChanged("StepList");
        }

        public void SetStep(
            int index,
            StepPositions item) {
            int maxIndex = this.stepList.Count - 1;
            if (index >= 0
                && index <= maxIndex)  this.stepList[index] = item;
            this.on_PropertyChanged("StepList");
        }

        public void RemoveStep(
            int index) {
            int maxIndex = this.stepList.Count - 1;
            if (index >= 0
                && index <= maxIndex) this.stepList.RemoveAt(index);
            this.on_PropertyChanged("StepList");
        }

        public void Clear() { 
            this.stepList.Clear();
            this.on_PropertyChanged("StepList");
        }

        public void AddStepsRandom(
            int count) {
            if (count > 0) {
                Random rnd = new Random();
                int value = 0;
                int counter = 0;
                int stepsCount = this.stepList.Count;
                if (stepsCount == 0) {
                    this.InsertStep(0, StepPositions.Position_1);
                    counter++;
                }
                else value = (int)this.stepList[stepsCount - 1];

                while (counter < count) {
                    value = (value + rnd.Next(1, 3)) % 4;
                    this.stepList.Add((StepPositions)value);
                    counter++;
                }
            }
            this.on_PropertyChanged("StepList");
        }

        private void buildToString() { this.toString = this.Name; }

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

    public class Business : _Base.Score.Business {

        #region Properties

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

        private int counterPositionX = 0;
        public int CounterPositionX {
            get { return this.counterPositionX; }
            set {
                if (this.counterPositionX != value) {
                    this.counterPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounter();
                }
            }
        }

        private int counterPositionY = 0;
        public int CounterPositionY {
            get { return this.counterPositionY; }
            set {
                if (this.counterPositionY != value) {
                    this.counterPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounter();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.Score.Styles setsStyle = VentuzScenes.GamePool._Modules.Score.Styles.CounterLeft;
        public VentuzScenes.GamePool._Modules.Score.Styles CounterStyle {
            get { return this.setsStyle; }
            set {
                if (this.setsStyle != value) {
                    this.setsStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounter();
                }
            }
        }

        private int maxCounter {
            get {
                if (this.SelectedDataset is DatasetContent) return this.SelectedDataset.Score;
                else return 5;
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

        private int leftPlayerBuzzerChannel_1 = 1;
        public int LeftPlayerBuzzerChannel_1 {
            get { return this.leftPlayerBuzzerChannel_1; }
            set {
                if (this.leftPlayerBuzzerChannel_1 != value) {
                    if (value < 1)
                        value = 1;
                    if (value > 8)
                        value = 8;
                    this.leftPlayerBuzzerChannel_1 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int leftPlayerBuzzerChannel_2 = 3;
        public int LeftPlayerBuzzerChannel_2 {
            get { return this.leftPlayerBuzzerChannel_2; }
            set {
                if (this.leftPlayerBuzzerChannel_2 != value) {
                    if (value < 1)
                        value = 1;
                    if (value > 8)
                        value = 8;
                    this.leftPlayerBuzzerChannel_2 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int leftPlayerBuzzerChannel_3 = 5;
        public int LeftPlayerBuzzerChannel_3 {
            get { return this.leftPlayerBuzzerChannel_3; }
            set {
                if (this.leftPlayerBuzzerChannel_3 != value) {
                    if (value < 1)
                        value = 1;
                    if (value > 8)
                        value = 8;
                    this.leftPlayerBuzzerChannel_3 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int leftPlayerBuzzerChannel_4 = 7;
        public int LeftPlayerBuzzerChannel_4 {
            get { return this.leftPlayerBuzzerChannel_4; }
            set {
                if (this.leftPlayerBuzzerChannel_4 != value) {
                    if (value < 1)
                        value = 1;
                    if (value > 8)
                        value = 8;
                    this.leftPlayerBuzzerChannel_4 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerBuzzerChannel_1 = 2;
        public int RightPlayerBuzzerChannel_1 {
            get { return this.rightPlayerBuzzerChannel_1; }
            set {
                if (this.rightPlayerBuzzerChannel_1 != value) {
                    if (value < 1)
                        value = 1;
                    if (value > 8)
                        value = 8;
                    this.rightPlayerBuzzerChannel_1 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerBuzzerChannel_2 = 4;
        public int RightPlayerBuzzerChannel_2 {
            get { return this.rightPlayerBuzzerChannel_2; }
            set {
                if (this.rightPlayerBuzzerChannel_2 != value) {
                    if (value < 1)
                        value = 1;
                    if (value > 8)
                        value = 8;
                    this.rightPlayerBuzzerChannel_2 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerBuzzerChannel_3 = 6;
        public int RightPlayerBuzzerChannel_3 {
            get { return this.rightPlayerBuzzerChannel_3; }
            set {
                if (this.rightPlayerBuzzerChannel_3 != value) {
                    if (value < 1)
                        value = 1;
                    if (value > 8)
                        value = 8;
                    this.rightPlayerBuzzerChannel_3 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerBuzzerChannel_4 = 8;
        public int RightPlayerBuzzerChannel_4 {
            get { return this.rightPlayerBuzzerChannel_4; }
            set {
                if (this.rightPlayerBuzzerChannel_4 != value) {
                    if (value < 1)
                        value = 1;
                    if (value > 8)
                        value = 8;
                    this.rightPlayerBuzzerChannel_4 = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private Tools.DMX.DMXNet dMX;
        private byte[] universe = new byte[512];

        private Color offColor = Color.Black;
        public Color OffColor {
            get { return this.offColor; }
            set {
                if (this.offColor != value) {
                    this.offColor = value;
                    this.on_PropertyChanged();
                    this.setDMXValues();
                }
            }
        }

        private Color onColor = Color.White;
        public Color OnColor {
            get { return this.onColor; }
            set {
                if (this.onColor != value) {
                    this.onColor = value;
                    this.on_PropertyChanged();
                    this.setDMXValues();
                }
            }
        }

        private Color leftColor = Color.Red;
        public Color LeftColor {
            get { return this.leftColor; }
            set {
                if (this.leftColor != value) {
                    this.leftColor = value;
                    this.on_PropertyChanged();
                    this.setDMXValues();
                }
            }
        }

        private Color rightColor = Color.Blue;
        public Color RightColor {
            get { return this.rightColor; }
            set {
                if (this.rightColor != value) {
                    this.rightColor = value;
                    this.on_PropertyChanged();
                    this.setDMXValues();
                }
            }
        }

        private int dmxStartchannel_1 = 1;
        public int DMXStartchannel_1 {
            get { return this.dmxStartchannel_1; }
            set {
                if (this.dmxStartchannel_1 != value) {
                    if (value < 1) this.dmxStartchannel_1 = 1;
                    else if (value > 256) this.dmxStartchannel_1 = 256;
                    else this.dmxStartchannel_1 = value;
                    this.on_PropertyChanged();
                    this.setDMXValues();
                }
            }
        }

        private int dmxStartchannel_2 = 4;
        public int DMXStartchannel_2 {
            get { return this.dmxStartchannel_2; }
            set {
                if (this.dmxStartchannel_2 != value) {
                    if (value < 1) this.dmxStartchannel_2 = 1;
                    else if (value > 256) this.dmxStartchannel_2 = 256;
                    else this.dmxStartchannel_2 = value;
                    this.on_PropertyChanged();
                    this.setDMXValues();
                }
            }
        }

        private int dmxStartchannel_3 = 7;
        public int DMXStartchannel_3 {
            get { return this.dmxStartchannel_3; }
            set {
                if (this.dmxStartchannel_3 != value) {
                    if (value < 1) this.dmxStartchannel_3 = 1;
                    else if (value > 256) this.dmxStartchannel_3 = 256;
                    else this.dmxStartchannel_3 = value;
                    this.on_PropertyChanged();
                    this.setDMXValues();
                }
            }
        }

        private int dmxStartchannel_4 = 10;
        public int DMXStartchannel_4 {
            get { return this.dmxStartchannel_4; }
            set {
                if (this.dmxStartchannel_4 != value) {
                    if (value < 1) this.dmxStartchannel_4 = 1;
                    else if (value > 256) this.dmxStartchannel_4 = 256;
                    else this.dmxStartchannel_4 = value;
                    this.on_PropertyChanged();
                    this.setDMXValues();
                }
            }
        }

        private System.Timers.Timer nextBuzzerTimer;

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

        private int buzzerIndex = -1;
        public int BuzzerIndex {
            get { return this.buzzerIndex; }
            private set {
                if (this.buzzerIndex != value) {
                    this.buzzerIndex = value;
                    this.on_PropertyChanged();
                }
            }
        }

        public StepPositions ActiveBuzzer {
            get {
                if (this.SelectedDataset is DatasetContent &&
                    this.BuzzerIndex >= 0 &&
                    this.BuzzerIndex < this.SelectedDataset.StepList.Length) return this.SelectedDataset.StepList[this.BuzzerIndex];
                else return StepPositions.Position_1;
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.SpeedBuzzerCounterSoloScore'", typeIdentifier);
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

            this.dMX = new Tools.DMX.DMXNet();
            this.setDMXValues(1, new byte[512]);

            this.nextBuzzerTimer = new System.Timers.Timer(2000);
            this.nextBuzzerTimer.AutoReset = false;
            this.nextBuzzerTimer.Elapsed += this.nextBuzzerTimer_Elapsed;

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this);

            this.SelectDataset(0);
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

            this.nextBuzzerTimer.Elapsed -= this.nextBuzzerTimer_Elapsed;

            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
        }

        public override void ResetData() {
            base.ResetData();
            this.BuzzerIndex = -1;
            this.LeftPlayerCounter = 0;
            this.RightPlayerCounter = 0;
            this.AllPanelsOff();
            this.SelectDataset(0);
        }


        public override void Next() {
            this.BuzzerIndex = -1;
            this.LeftPlayerCounter = 0;
            this.RightPlayerCounter = 0;
            this.FlipPlayers = !this.FlipPlayers;
            this.AllPanelsOff();
            this.SelectDataset(this.SelectedDatasetIndex + 1);
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

        public void StartGame() {
            this.BuzzerIndex = -1;
            this.NextBuzzer();
        }

        public void NextBuzzer() {
            int count = 0;
            if (this.SelectedDataset is DatasetContent) count = this.SelectedDataset.StepList.Length;
            if (this.BuzzerIndex < count - 1) {
                Console.WriteLine(">>>>>>>> NextBuzzer");
                this.BuzzerIndex++;
                if (this.ActiveBuzzer == StepPositions.Position_1) this.PanelOn(StepPositions.Position_1);
                else this.PanelOff(StepPositions.Position_1);
                if (this.ActiveBuzzer == StepPositions.Position_2) this.PanelOn(StepPositions.Position_2);
                else this.PanelOff(StepPositions.Position_2);
                if (this.ActiveBuzzer == StepPositions.Position_3) this.PanelOn(StepPositions.Position_3);
                else this.PanelOff(StepPositions.Position_3);
                if (this.ActiveBuzzer == StepPositions.Position_4) this.PanelOn(StepPositions.Position_4);
                else this.PanelOff(StepPositions.Position_4);
                this.ReleaseBuzzer();
            }
            else {
                this.StopGame();
            }
        }

        public void StopGame() {
            this.LockBuzzer();
            this.AllPanelsOff();
        }

        private void setDMXValues() {
            //if (this.buzzerHandler is BuzzerIO.Business) {
            //    this.buzzerHandler.SetDMXMode(this.IOUnitName, IOnet.IOUnit.IONbuzDX.DMXBuzzerModes.OFF);
            //    byte[] valueList = new byte[12];
            //    this.setDMXValues(1, valueList);
            //    byte[] offValue = new byte[] { this.OffColor.R, this.OffColor.G, this.OffColor.B };
            //    byte[] nextValue = new byte[] { this.LeftColor.R, this.LeftColor.G, this.LeftColor.B };
            //    byte[] onValue = new byte[] { this.OnColor.R, this.OnColor.G, this.OnColor.B };
            //    Array.Copy(offValue, 0, valueList, 0, offValue.Length);
            //    Array.Copy(offValue, 0, valueList, 3, offValue.Length);
            //    Array.Copy(offValue, 0, valueList, 6, offValue.Length);
            //    Array.Copy(offValue, 0, valueList, 9, offValue.Length);
            //    switch (this.ActiveBuzzer) {
            //        case StepPositions.Position_1:
            //            Array.Copy(onValue, 0, valueList, 0, onValue.Length);
            //            break;
            //        case StepPositions.Position_2:
            //            Array.Copy(onValue, 0, valueList, 3, onValue.Length);
            //            break;
            //        case StepPositions.Position_3:
            //            Array.Copy(onValue, 0, valueList, 6, onValue.Length);
            //            break;
            //        case StepPositions.Position_4:
            //            Array.Copy(onValue, 0, valueList, 9, onValue.Length);
            //            break;
            //    }
            //    this.setDMXValues(1, valueList);
            //    this.buzzerHandler.SetDMXChannel(this.IOUnitName, 1, valueList);
            //}
        }

        private void setDMXValues(
            int startChannel,
            byte[] values) {
            try {
                byte startIndex = Convert.ToByte(startChannel - 1);
                if (startIndex + values.Length < this.universe.Length) Array.Copy(values, 0, this.universe, startIndex, values.Length);
                this.dMX.SetDmxDataToEspNet(0, 0, this.universe);
            }
            catch (Exception) {
            }
        }

        public virtual void ReleaseBuzzer() {
            bool[] inputMask = new bool[8];
            switch (this.ActiveBuzzer) {
                case StepPositions.Position_1:
                    if (this.LeftPlayerBuzzerChannel_1 > 0 &&
                        this.LeftPlayerBuzzerChannel_1 <= inputMask.Length) inputMask[this.LeftPlayerBuzzerChannel_1 - 1] = true;
                    if (this.RightPlayerBuzzerChannel_1 > 0 &&
                        this.RightPlayerBuzzerChannel_1 <= inputMask.Length) inputMask[this.RightPlayerBuzzerChannel_1 - 1] = true;
                    break;
                case StepPositions.Position_2:
                    if (this.LeftPlayerBuzzerChannel_2 > 0 &&
                        this.LeftPlayerBuzzerChannel_2 <= inputMask.Length) inputMask[this.LeftPlayerBuzzerChannel_2 - 1] = true;
                    if (this.RightPlayerBuzzerChannel_2 > 0 &&
                        this.RightPlayerBuzzerChannel_2 <= inputMask.Length) inputMask[this.RightPlayerBuzzerChannel_2 - 1] = true;
                    break;
                case StepPositions.Position_3:
                    if (this.LeftPlayerBuzzerChannel_3 > 0 &&
                        this.LeftPlayerBuzzerChannel_3 <= inputMask.Length) inputMask[this.LeftPlayerBuzzerChannel_3 - 1] = true;
                    if (this.RightPlayerBuzzerChannel_3 > 0 &&
                        this.RightPlayerBuzzerChannel_3 <= inputMask.Length) inputMask[this.RightPlayerBuzzerChannel_3 - 1] = true;
                    break;
                case StepPositions.Position_4:
                    if (this.LeftPlayerBuzzerChannel_4 > 0 &&
                        this.LeftPlayerBuzzerChannel_4 <= inputMask.Length) inputMask[this.LeftPlayerBuzzerChannel_4 - 1] = true;
                    if (this.RightPlayerBuzzerChannel_4 > 0 &&
                        this.RightPlayerBuzzerChannel_4 <= inputMask.Length) inputMask[this.RightPlayerBuzzerChannel_4 - 1] = true;
                    break;
            }
            this.buzzerHandler.SetInputMask(this.IOUnitName, inputMask);
            this.buzzerHandler.ReleaseBuzzer(this.IOUnitName, WorkModes.BUZZER);
        }

        public virtual void LockBuzzer() {
            this.buzzerHandler.LockBuzzer(this.IOUnitName);
        }

        internal void AllPanelsOn() {
            this.PanelOn(StepPositions.Position_1);
            this.PanelOn(StepPositions.Position_2);
            this.PanelOn(StepPositions.Position_3);
            this.PanelOn(StepPositions.Position_4);
        }

        internal void PanelOn(StepPositions value) {
            this.buzzerHandler.SetDMXMode(this.IOUnitName, IOnet.IOUnit.IONbuzDX.DMXBuzzerModes.OFF);
            byte[] onValue = new byte[] { this.OnColor.R, this.OnColor.G, this.OnColor.B };
            int startChannel = 0;
            switch (value) {
                case StepPositions.Position_1:
                    startChannel = this.DMXStartchannel_1;
                    break;
                case StepPositions.Position_2:
                    startChannel = this.DMXStartchannel_2;
                    break;
                case StepPositions.Position_3:
                    startChannel = this.DMXStartchannel_3;
                    break;
                case StepPositions.Position_4:
                    startChannel = this.DMXStartchannel_4;
                    break;
            }
            this.setDMXValues(startChannel, onValue);
            this.buzzerHandler.SetDMXChannel(this.IOUnitName, startChannel, onValue);
            Console.WriteLine(">>> PanelOn: " + value.ToString());
        }

        internal void AllPanelsLeft() {
            this.PanelLeft(StepPositions.Position_1);
            this.PanelLeft(StepPositions.Position_2);
            this.PanelLeft(StepPositions.Position_3);
            this.PanelLeft(StepPositions.Position_4);
        }

        internal void PanelLeft(StepPositions value) {
            this.buzzerHandler.SetDMXMode(this.IOUnitName, IOnet.IOUnit.IONbuzDX.DMXBuzzerModes.OFF);
            byte[] leftValue = new byte[] { this.LeftColor.R, this.LeftColor.G, this.LeftColor.B };
            int startChannel = 1;
            switch (value) {
                case StepPositions.Position_1:
                    startChannel = this.DMXStartchannel_1;
                    break;
                case StepPositions.Position_2:
                    startChannel = this.DMXStartchannel_2;
                    break;
                case StepPositions.Position_3:
                    startChannel = this.DMXStartchannel_3;
                    break;
                case StepPositions.Position_4:
                    startChannel = this.DMXStartchannel_4;
                    break;
            }
            this.setDMXValues(startChannel, leftValue);
            this.buzzerHandler.SetDMXChannel(this.IOUnitName, startChannel, leftValue);
            Console.WriteLine(">>> PanelLeft: " + value.ToString());
        }

        internal void AllPanelsRight() {
            this.PanelRight(StepPositions.Position_1);
            this.PanelRight(StepPositions.Position_2);
            this.PanelRight(StepPositions.Position_3);
            this.PanelRight(StepPositions.Position_4);
        }

        internal void PanelRight(StepPositions value) {
            this.buzzerHandler.SetDMXMode(this.IOUnitName, IOnet.IOUnit.IONbuzDX.DMXBuzzerModes.OFF);
            byte[] rightValue = new byte[] { this.RightColor.R, this.RightColor.G, this.RightColor.B };
            int startChannel = 1;
            switch (value) {
                case StepPositions.Position_1:
                    startChannel = this.DMXStartchannel_1;
                    break;
                case StepPositions.Position_2:
                    startChannel = this.DMXStartchannel_2;
                    break;
                case StepPositions.Position_3:
                    startChannel = this.DMXStartchannel_3;
                    break;
                case StepPositions.Position_4:
                    startChannel = this.DMXStartchannel_4;
                    break;
            }
            this.setDMXValues(startChannel, rightValue);
            this.buzzerHandler.SetDMXChannel(this.IOUnitName, startChannel, rightValue);
            Console.WriteLine(">>> PanelRight: " + value.ToString());
        }

        internal void AllPanelsOff() {
            this.PanelOff(StepPositions.Position_1);
            this.PanelOff(StepPositions.Position_2);
            this.PanelOff(StepPositions.Position_3);
            this.PanelOff(StepPositions.Position_4);
        }

        internal void PanelOff(StepPositions value) {
            this.buzzerHandler.SetDMXMode(this.IOUnitName, IOnet.IOUnit.IONbuzDX.DMXBuzzerModes.OFF);
            byte[] offValue = new byte[] { this.OffColor.R, this.OffColor.G, this.OffColor.B };
            int startChannel = 1;
            switch (value) {
                case StepPositions.Position_1:
                    startChannel = this.DMXStartchannel_1;
                    break;
                case StepPositions.Position_2:
                    startChannel = this.DMXStartchannel_2;
                    break;
                case StepPositions.Position_3:
                    startChannel = this.DMXStartchannel_3;
                    break;
                case StepPositions.Position_4:
                    startChannel = this.DMXStartchannel_4;
                    break;
            }
            this.setDMXValues(startChannel, offValue);
            this.buzzerHandler.SetDMXChannel(this.IOUnitName, startChannel, offValue);
            Console.WriteLine(">>> PanelOff: " + value.ToString());
        }

        public void DoBuzzer(
            int buzzerID) {
            this.LockBuzzer();
            if (buzzerID == this.LeftPlayerBuzzerChannel_1 ||
                buzzerID == this.LeftPlayerBuzzerChannel_2 ||
                buzzerID == this.LeftPlayerBuzzerChannel_3 ||
                buzzerID == this.LeftPlayerBuzzerChannel_4) {
                if (this.FlipPlayers) {
                    if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.PlaySound_BuzzerRight();
                    this.PanelRight(this.ActiveBuzzer);
                    this.RightPlayerCounter++;
                    if (this.RightPlayerCounter >= this.maxCounter) {
                        if (this.insertScene is VRemote4.HandlerSi.Scene) Helper.invokeActionAfterDelay(this.insertScene.PlaySound_BuzzerFinished, 750, this.syncContext);
                        this.AllPanelsRight();
                    }
                    else this.nextBuzzerTimer.Start(); // Helper.invokeActionAfterDelay(this.NextBuzzer, 2000, this.syncContext);
                }
                else {
                    if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.PlaySound_BuzzerLeft();
                    this.PanelLeft(this.ActiveBuzzer);
                    this.LeftPlayerCounter++;
                    if (this.LeftPlayerCounter >= this.maxCounter) {
                        if (this.insertScene is VRemote4.HandlerSi.Scene) Helper.invokeActionAfterDelay(this.insertScene.PlaySound_BuzzerFinished, 750, this.syncContext);
                        this.AllPanelsLeft();
                    }
                    else Helper.invokeActionAfterDelay(this.NextBuzzer, 2000, this.syncContext);
                }
                this.Vinsert_SetCounter();
            }
            else if (buzzerID == this.RightPlayerBuzzerChannel_1 ||
                buzzerID == this.RightPlayerBuzzerChannel_2 ||
                buzzerID == this.RightPlayerBuzzerChannel_3 ||
                buzzerID == this.RightPlayerBuzzerChannel_4) {
                if (this.FlipPlayers) {
                    if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.PlaySound_BuzzerLeft();
                    this.PanelLeft(this.ActiveBuzzer);
                    this.LeftPlayerCounter++;
                    if (this.LeftPlayerCounter >= this.maxCounter) {
                        if (this.insertScene is VRemote4.HandlerSi.Scene) Helper.invokeActionAfterDelay(this.insertScene.PlaySound_BuzzerFinished, 750, this.syncContext);
                        this.AllPanelsLeft();
                    }
                    else Helper.invokeActionAfterDelay(this.NextBuzzer, 2000, this.syncContext);
                }
                else {
                    if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.PlaySound_BuzzerRight();
                    this.PanelRight(this.ActiveBuzzer);
                    this.RightPlayerCounter++;
                    if (this.RightPlayerCounter >= this.maxCounter) {
                        if (this.insertScene is VRemote4.HandlerSi.Scene) Helper.invokeActionAfterDelay(this.insertScene.PlaySound_BuzzerFinished, 750, this.syncContext);
                        this.AllPanelsRight();
                    }
                    else Helper.invokeActionAfterDelay(this.NextBuzzer, 2000, this.syncContext);
                }
                this.Vinsert_SetCounter();
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
            foreach (DatasetContent item in this.dataList) {
                this.names.Add(item.ToString());
            }
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


        public void Vinsert_CounterIn() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                this.Vinsert_SetCounter(this.insertScene.Counter);
                this.insertScene.Counter.ToIn();
            }
        }
        public void Vinsert_SetCounter() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetCounter(this.insertScene.Counter); }
        public void Vinsert_SetCounter(VentuzScenes.GamePool._Modules.Score scene) { this.Vinsert_SetCounter(scene, this.LeftPlayerCounter, this.RightPlayerCounter); }
        public void Vinsert_SetCounter(
            VentuzScenes.GamePool._Modules.Score scene,
            int leftPlayerCounter,
            int rightPlayerCounter) {
            if (scene is VentuzScenes.GamePool._Modules.Score) {
                scene.SetPositionX(this.CounterPositionX);
                scene.SetPositionY(this.CounterPositionY);
                scene.SetStyle(this.CounterStyle);
                scene.SetFlipPosition(this.FlipPlayers);
                scene.SetLeftTopName(this.LeftPlayerName);
                scene.SetLeftTopScore(leftPlayerCounter);
                scene.SetRightBottomName(this.RightPlayerName);
                scene.SetRightBottomScore(rightPlayerCounter);
            }
        }

        public void Vinsert_CounterOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Counter.ToOut(); }

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
                e.Arg.Name == this.IOUnitName) this.DoBuzzer(e.Arg.BuzzerID);
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
                if (e.Arg.ConnectionStatus == Tools.NetContact.ClientStates.Connected) this.AllPanelsOff();
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

        private void nextBuzzerTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_nextBuzzerTimer_Elapsed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        protected virtual void sync_nextBuzzerTimer_Elapsed(object content) {
            this.NextBuzzer();
        }

        void dataset_Error(object sender, Messaging.ErrorEventArgs e) { this.on_Error(sender, e); }

        void dataset_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_dataset_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_dataset_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "Name") {
                    this.buildNameList();
                    this.on_PropertyChanged("NameList");
                }
            }
            this.Save();
        }

        #endregion

    }
}
