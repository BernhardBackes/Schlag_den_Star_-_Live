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

using Cliparts.Serialization;

using Cliparts.Tools.Base;

using Cliparts.IOnet.IOUnit.IONbase;
using Cliparts.IOnet.IOUnit.IONbuz;

using Cliparts.SchlagDenStarLive.MainApp.BuzzerIO;
using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;

using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.NumericInputTimerCounterScore;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.NumericInputTimerCounterScore {

    public class Data
    {
        public DatasetContent[] DataList;
        public int SelectedIndex;
    }

    public class DatasetContent : Messaging.Message, INotifyPropertyChanged
    {

        #region Properties

        private string name = string.Empty;
        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
                {
                    if (string.IsNullOrEmpty(value)) this.name = string.Empty;
                    else this.name = value;
                    this.buildToString();
                    this.on_PropertyChanged();
                }
            }
        }

        private string hostText = string.Empty;
        public string HostText
        {
            get { return this.hostText; }
            set
            {
                if (this.hostText != value)
                {
                    if (string.IsNullOrEmpty(value)) this.hostText = string.Empty;
                    else this.hostText = Regex.Replace(value, "(?<!\r)\n", "\r\n");
                    this.on_PropertyChanged();
                }
            }
        }

        private string playerText = string.Empty;
        public string PlayerText
        {
            get { return this.playerText; }
            set
            {
                if (this.playerText != value)
                {
                    if (string.IsNullOrEmpty(value)) this.playerText = string.Empty;
                    else this.playerText = Regex.Replace(value, "(?<!\r)\n", "\r\n");
                    this.on_PropertyChanged();
                }
            }
        }

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DatasetContent() { }

        public void Clone(
            DatasetContent source)
        {
            if (source is DatasetContent)
            {
                this.Name = source.Name;
                this.HostText = source.HostText;
                this.PlayerText = source.PlayerText;
            }
            else
            {
                this.Name = string.Empty;
                this.HostText = string.Empty;
                this.PlayerText = string.Empty;
            }
        }

        private void buildToString() { this.toString = this.Name; }

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

    public class ListItem
    {
        public int ID { get; private set; }
        public string Text { get; private set; }
        public ListItem(
            int id,
            string text) {
            this.ID = id;
            this.Text = text;
        }
        public override string ToString()
        {
            return $"{this.ID.ToString()}. {this.Text}";
        }
    }

    public class Business : _Base.Score.Business {

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

        private int leftPlayerBuzzerChannel = 1;
        public int LeftPlayerBuzzerChannel {
            get { return this.leftPlayerBuzzerChannel; }
            set {
                if (this.leftPlayerBuzzerChannel != value) {
                    if (value < 1)
                        value = 1;
                    if (value > 8)
                        value = 8;
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
                    if (value < 1)
                        value = 1;
                    if (value > 8)
                        value = 8;
                    this.rightPlayerBuzzerChannel = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool femalesSelected = false;
        [NotSerialized]
        public bool FemalesSelected
        {
            get { return this.femalesSelected; }
            set { 
                if (this.femalesSelected != value )
                {
                    this.femalesSelected = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private PlayerSelection selectedPlayer = PlayerSelection.LeftPlayer;
        [NotSerialized]
        public PlayerSelection SelectedPlayer
        {
            get { return this.selectedPlayer; }
            set
            {
                if (this.selectedPlayer != value)
                {
                    if (value == PlayerSelection.NotSelected) value = PlayerSelection.LeftPlayer;
                    this.selectedPlayer = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string leftTeamFemale = string.Empty;
        public string LeftTeamFemale
        {
            get { return this.leftTeamFemale; }
            set { 
                if (this.leftTeamFemale != value)
                {
                    if (string.IsNullOrEmpty(value)) this.leftTeamFemale = string.Empty;
                    else this.leftTeamFemale = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string leftTeamMale = string.Empty;
        public string LeftTeamMale
        {
            get { return this.leftTeamMale; }
            set
            {
                if (this.leftTeamMale != value)
                {
                    if (string.IsNullOrEmpty(value)) this.leftTeamMale = string.Empty;
                    else this.leftTeamMale = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int leftPlayerValue = 0;
        public int LeftPlayerValue
        {
            get { return this.leftPlayerValue; }
            set
            {
                if (this.leftPlayerValue != value)
                {
                    this.leftPlayerValue = value;
                    this.on_PropertyChanged();
                }
            }
        }

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

        private bool leftPlayerBestEstimation = false;
        public bool LeftPlayerBestEstimation
        {
            get { return this.leftPlayerBestEstimation; }
            set
            {
                if (this.leftPlayerBestEstimation != value)
                {
                    this.leftPlayerBestEstimation = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool leftPlayerHighestCounter = false;
        public bool LeftPlayerHighestCounter
        {
            get { return this.leftPlayerHighestCounter; }
            set
            {
                if (this.leftPlayerHighestCounter != value)
                {
                    this.leftPlayerHighestCounter = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string rightTeamFemale = string.Empty;
        public string RightTeamFemale
        {
            get { return this.rightTeamFemale; }
            set
            {
                if (this.rightTeamFemale != value)
                {
                    if (string.IsNullOrEmpty(value)) this.rightTeamFemale = string.Empty;
                    else this.rightTeamFemale = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string rightTeamMale = string.Empty;
        public string RightTeamMale
        {
            get { return this.rightTeamMale; }
            set
            {
                if (this.rightTeamMale != value)
                {
                    if (string.IsNullOrEmpty(value)) this.rightTeamMale = string.Empty;
                    else this.rightTeamMale = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private int rightPlayerValue = 0;
        public int RightPlayerValue
        {
            get { return this.rightPlayerValue; }
            set
            {
                if (this.rightPlayerValue != value)
                {
                    this.rightPlayerValue = value;
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

        private bool rightPlayerBestEstimation = false;
        public bool RightPlayerBestEstimation
        {
            get { return this.rightPlayerBestEstimation; }
            set
            {
                if (this.rightPlayerBestEstimation != value)
                {
                    this.rightPlayerBestEstimation = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool rightPlayerHighestCounter = false;
        public bool RightPlayerHighestCounter
        {
            get { return this.rightPlayerHighestCounter; }
            set
            {
                if (this.rightPlayerHighestCounter != value)
                {
                    this.rightPlayerHighestCounter = value;
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

        private bool counterFlipPlayers;
        public bool CounterFlipPlayers {
            get { return this.counterFlipPlayers; }
            set {
                if (this.counterFlipPlayers != value) {
                    this.counterFlipPlayers = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetCounter();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.Score.Styles setsStyle = VentuzScenes.GamePool._Modules.Score.Styles.Counter;
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

        private int timerPositionX = 0;
        public int TimerPositionX {
            get { return this.timerPositionX; }
            set {
                if (this.timerPositionX != value) {
                    this.timerPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
                }
            }
        }

        private int timerPositionY = 0;
        public int TimerPositionY {
            get { return this.timerPositionY; }
            set {
                if (this.timerPositionY != value) {
                    this.timerPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
                }
            }
        }

        private VentuzScenes.GamePool._Modules.Timer.Styles timerStyle = VentuzScenes.GamePool._Modules.Timer.Styles.Sec;
        public VentuzScenes.GamePool._Modules.Timer.Styles TimerStyle {
            get { return this.timerStyle; }
            set {
                if (this.timerStyle != value) {
                    this.timerStyle = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
                    this.Vfullscreen_SetTimer();
                }
            }
        }

        private int timerStartTime = 240;
        public int TimerStartTime {
            get { return this.timerStartTime; }
            set {
                if (this.timerStartTime != value) {
                    this.timerStartTime = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
                    this.Vfullscreen_SetTimer();
                }
            }
        }

        private int timerExtraTime = 60;
        public int TimerExtraTime {
            get { return this.timerExtraTime; }
            set {
                if (this.timerExtraTime != value) {
                    this.timerExtraTime = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
                    this.Vfullscreen_SetTimer();
                }
            }
        }

        private int timerAlarmTime1 = -1;
        public int TimerAlarmTime1 {
            get { return this.timerAlarmTime1; }
            set {
                if (this.timerAlarmTime1 != value) {
                    this.timerAlarmTime1 = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
                    this.Vfullscreen_SetTimer();
                }
            }
        }

        private int timerAlarmTime2 = -1;
        public int TimerAlarmTime2 {
            get { return this.timerAlarmTime2; }
            set {
                if (this.timerAlarmTime2 != value) {
                    this.timerAlarmTime2 = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
                    this.Vfullscreen_SetTimer();
                }
            }
        }

        private int timerStopTime = 0;
        public int TimerStopTime {
            get { return this.timerStopTime; }
            set {
                if (this.timerStopTime != value) {
                    this.timerStopTime = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
                    this.Vfullscreen_SetTimer();
                }
            }
        }

        private bool runExtraTime = false;
        [NotSerialized]
        public bool RunExtraTime {
            get { return this.runExtraTime; }
            set {
                if (this.runExtraTime != value) {
                    this.runExtraTime = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTimer();
                    this.Vfullscreen_SetTimer();
                }
            }
        }

        private int timerCurrentTime = -1;
        public int TimerCurrentTime {
            get { return this.timerCurrentTime; }
            protected set {
                if (this.timerCurrentTime != value) {
                    this.timerCurrentTime = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private bool timerIsRunning = false;
        public bool TimerIsRunning {
            get { return this.timerIsRunning; }
            protected set {
                if (this.timerIsRunning != value) {
                    this.timerIsRunning = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string filename = string.Empty;
        public string Filename
        {
            get { return this.filename; }
            set { if (this.filename != value) this.Load(value); }
        }

        private List<DatasetContent> dataList = new List<DatasetContent>();
        [NotSerialized]
        public DatasetContent[] DataList
        {
            get { return this.dataList.ToArray(); }
            set
            {
                this.repressPropertyChanged = true;
                this.tryRemoveAllDatasets();
                if (value is DatasetContent[])
                {
                    foreach (DatasetContent item in value) this.tryAddDataset(item, -1);
                }
                this.repressPropertyChanged = false;
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

        private List<ListItem> items = new List<ListItem>();
        public List<ListItem> AvailableItems = new List<ListItem>();
        public List<ListItem> SelectedItems = new List<ListItem>();

        private Insert insertScene;
        public override VRemote4.HandlerSi.Scene.States InsertSceneStatus {
            get {
                if (this.insertScene is Insert) return this.insertScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private Host hostScene;
        public override VRemote4.HandlerSi.Scene.States HostSceneStatus
        {
            get
            {
                if (this.hostScene is Host) return this.hostScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private Player leftPlayerScene;
        public override VRemote4.HandlerSi.Scene.States LeftPlayerSceneStatus
        {
            get
            {
                if (this.leftPlayerScene is Player) return this.leftPlayerScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private Player rightPlayerScene;
        public override VRemote4.HandlerSi.Scene.States RightPlayerSceneStatus
        {
            get
            {
                if (this.rightPlayerScene is Player) return this.rightPlayerScene.Status;
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.NumericInputTimerCounterScore'", typeIdentifier);
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

            this.insertScene = new Insert(syncContext, insertMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.insertScene.StatusChanged += this.insertScene_StatusChanged;
            this.insertScene.Timer.Alarm1Fired += this.timer_Alarm1Fired;
            this.insertScene.Timer.Alarm2Fired += this.timer_Alarm2Fired;
            this.insertScene.Timer.StopFired += this.timer_StopFired;
            this.insertScene.Timer.PropertyChanged += this.timer_PropertyChanged;

            this.hostScene = new Host(syncContext, hostMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.hostScene.StatusChanged += this.hostScene_StatusChanged;

            this.leftPlayerScene = new Player(syncContext, leftPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.leftPlayerScene.StatusChanged += this.leftPlayerScene_StatusChanged;
            this.leftPlayerScene.OKButtonPressed += this.leftPlayerScene_OKButtonPressed;

            this.rightPlayerScene = new Player(syncContext, rightPlayerMasterScene.GamePort, VRemote4.HandlerSi.Scene.Modes.Dynamic);
            this.rightPlayerScene.StatusChanged += this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.OKButtonPressed += this.rightPlayerScene_OKButtonPressed;

            this.items.Add(new ListItem(this.items.Count + 1, "Bagdad"));
            this.items.Add(new ListItem(this.items.Count + 1, "Baku"));
            this.items.Add(new ListItem(this.items.Count + 1, "Bamako"));
            this.items.Add(new ListItem(this.items.Count + 1, "Bandar Seri Begawan"));
            this.items.Add(new ListItem(this.items.Count + 1, "Bangkok"));
            this.items.Add(new ListItem(this.items.Count + 1, "Bangui"));
            this.items.Add(new ListItem(this.items.Count + 1, "Banjul"));
            this.items.Add(new ListItem(this.items.Count + 1, "Basseterre"));
            this.items.Add(new ListItem(this.items.Count + 1, "Beirut"));
            this.items.Add(new ListItem(this.items.Count + 1, "Belgrad"));
            this.items.Add(new ListItem(this.items.Count + 1, "Belmopan"));
            this.items.Add(new ListItem(this.items.Count + 1, "Berlin"));
            this.items.Add(new ListItem(this.items.Count + 1, "Bern"));
            this.items.Add(new ListItem(this.items.Count + 1, "Bischkek"));
            this.items.Add(new ListItem(this.items.Count + 1, "Bissau"));
            this.items.Add(new ListItem(this.items.Count + 1, "Bogotá"));
            this.items.Add(new ListItem(this.items.Count + 1, "Brasília"));
            this.items.Add(new ListItem(this.items.Count + 1, "Bratislava"));
            this.items.Add(new ListItem(this.items.Count + 1, "Brazzaville"));
            this.items.Add(new ListItem(this.items.Count + 1, "Bridgetown"));
            this.items.Add(new ListItem(this.items.Count + 1, "Brüssel"));
            this.items.Add(new ListItem(this.items.Count + 1, "Budapest"));
            this.items.Add(new ListItem(this.items.Count + 1, "Buenos Aires"));
            this.items.Add(new ListItem(this.items.Count + 1, "Bukarest"));

            foreach(var item in this.items) this.AvailableItems.Add(item);

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

            this.insertScene.Dispose();
            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Timer.Alarm1Fired -= this.timer_Alarm1Fired;
            this.insertScene.Timer.Alarm2Fired -= this.timer_Alarm2Fired;
            this.insertScene.Timer.StopFired -= this.timer_StopFired;
            this.insertScene.Timer.PropertyChanged -= this.timer_PropertyChanged;

            this.hostScene.StatusChanged -= this.hostScene_StatusChanged;
            this.hostScene.Dispose();

            this.leftPlayerScene.StatusChanged -= this.leftPlayerScene_StatusChanged;
            this.leftPlayerScene.OKButtonPressed -= this.leftPlayerScene_OKButtonPressed;
            this.leftPlayerScene.Dispose();

            this.rightPlayerScene.StatusChanged -= this.rightPlayerScene_StatusChanged;
            this.rightPlayerScene.OKButtonPressed -= this.rightPlayerScene_OKButtonPressed;
            this.rightPlayerScene.Dispose();

        }

        public override void ResetData() {
            base.ResetData();
            this.FemalesSelected = false;
            this.LeftPlayerCounter = 0;
            this.LeftPlayerValue = 0;
            this.LeftPlayerBestEstimation = false;
            this.LeftPlayerHighestCounter = false;
            this.RightPlayerCounter = 0;
            this.RightPlayerValue = 0;
            this.RightPlayerBestEstimation = false;
            this.RightPlayerHighestCounter = false;
            this.ResetItems();
        }

        public override void Next() {
            base.Next();
            this.FemalesSelected = !this.FemalesSelected;
            this.LeftPlayerCounter = 0;
            this.LeftPlayerValue = 0;
            this.LeftPlayerBestEstimation = false;
            this.LeftPlayerHighestCounter = false;
            this.RightPlayerCounter = 0;
            this.RightPlayerValue = 0;
            this.RightPlayerBestEstimation = false;
            this.RightPlayerHighestCounter = false;
            this.SelectDataset(this.SelectedDatasetIndex + 1);
        }

        internal void SetResults()
        {
            int offsetLeft = Math.Abs(this.LeftPlayerCounter - this.LeftPlayerValue);
            int offsetRight = Math.Abs(this.RightPlayerCounter - this.RightPlayerValue);
            if (offsetLeft < offsetRight)
            {
                this.LeftPlayerBestEstimation = true;
                this.RightPlayerBestEstimation = false;
            }
            else if (offsetLeft > offsetRight)
            {
                this.LeftPlayerBestEstimation = false;
                this.RightPlayerBestEstimation = true;
            }
            else
            {
                this.LeftPlayerBestEstimation = true;
                this.RightPlayerBestEstimation = true;
            }
            if (this.LeftPlayerCounter > this.RightPlayerCounter)
            {
                this.LeftPlayerHighestCounter = true;
                this.RightPlayerHighestCounter = false;
            }
            else if (this.LeftPlayerCounter < this.RightPlayerCounter)
            {
                this.LeftPlayerHighestCounter = false;
                this.RightPlayerHighestCounter = true;
            }
            else
            {
                this.LeftPlayerHighestCounter = true;
                this.RightPlayerHighestCounter = true;
            }
            this.Vhost_SetResults(
                this.hostScene,
                this.LeftPlayerCounter,
                offsetLeft,
                this.RightPlayerCounter,
                offsetRight);
        }

        internal void ResolveBestEstimation()
        {
            if (this.LeftPlayerBestEstimation) this.LeftPlayerScore++;
            if (this.RightPlayerBestEstimation) this.RightPlayerScore++;
            this.Vinsert_PlayJinglePling();
        }

        internal void ResolveHighestCounter()
        {
            if (this.LeftPlayerHighestCounter) this.LeftPlayerScore++;
            if (this.RightPlayerHighestCounter) this.RightPlayerScore++;
            this.Vinsert_PlayJinglePling();
        }

        private void fillBuzzerUnitList(
            Cliparts.IOnet.IOUnit.IONbase.InfoParam[] unitInfoList) {
            this.ioUnitNameList.Clear();
            if (unitInfoList is Cliparts.IOnet.IOUnit.IONbase.InfoParam[]) {
                foreach (Cliparts.IOnet.IOUnit.IONbase.InfoParam item in unitInfoList) {
                    if (item is Cliparts.IOnet.IOUnit.IONbase.InfoParam)
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

        public virtual void DoCounter(
            PlayerSelection player) {
            switch (player) {
                case PlayerSelection.LeftPlayer:
                    this.LeftPlayerCounter++;
                    break;
                case PlayerSelection.RightPlayer:
                    this.RightPlayerCounter++;
                    break;
            }
            this.Vinsert_SetCounter();
        }

        public virtual void ReleaseBuzzer() {
            bool[] inputMask = new bool[8];
            if (this.LeftPlayerBuzzerChannel > 0 &&
                this.LeftPlayerBuzzerChannel <= inputMask.Length)
                inputMask[this.LeftPlayerBuzzerChannel - 1] = true;
            if (this.RightPlayerBuzzerChannel > 0 &&
                this.RightPlayerBuzzerChannel <= inputMask.Length)
                inputMask[this.RightPlayerBuzzerChannel - 1] = true;
            this.buzzerHandler.SetInputMask(this.IOUnitName, inputMask);
            this.buzzerHandler.ReleaseBuzzer(this.IOUnitName, IOnet.IOUnit.IONbuz.WorkModes.EVENT);
        }

        public virtual void LockBuzzer() {
            this.buzzerHandler.LockBuzzer(this.IOUnitName);
        }

        public DatasetContent GetDataset(
            int index)
        {
            if (index >= 0 &&
                index < this.dataList.Count) return this.dataList[index];
            else return null;
        }

        public int GetDatasetIndex(
            DatasetContent dataset)
        {
            int index = -1;
            int datasetIndex = 0;
            foreach (DatasetContent item in this.dataList)
            {
                if (item == dataset)
                {
                    index = datasetIndex;
                    break;
                }
                datasetIndex++;
            }
            return index;
        }

        public void SelectDataset(
            int index)
        {
            if (index < 0) index = 0;
            if (index >= this.dataList.Count) index = this.dataList.Count - 1;
            this.SelectedDatasetIndex = index;
            this.on_PropertyChanged("SelectedDatasetIndex");
            this.SelectedDataset = this.GetDataset(index);
            this.on_PropertyChanged("SelectedDataset");
        }

        public void AddDataset(
            DatasetContent newDataset,
            int insertIndex)
        {
            if (this.tryAddDataset(newDataset, insertIndex)) this.on_PropertyChanged("NameList");
            if (this.SelectedDataset == null) this.SelectDataset(0);
        }
        private bool tryAddDataset(
            DatasetContent newDataset,
            int insertIndex)
        {
            if (newDataset is DatasetContent &&
                !this.dataList.Contains(newDataset))
            {
                DatasetContent dataset = new DatasetContent();
                dataset.Clone(newDataset);
                dataset.Error += this.dataset_Error;
                dataset.PropertyChanged += this.dataset_PropertyChanged;
                if (insertIndex >= 0 &&
                    insertIndex < this.DatasetsCount)
                {
                    this.dataList.Insert(insertIndex, dataset);
                    this.names.Insert(insertIndex, dataset.ToString());
                }
                else
                {
                    this.dataList.Add(dataset);
                    this.names.Add(dataset.ToString());
                }
                return true;
            }
            else return false;
        }

        public bool TryMoveDatasetUp(
            int index)
        {
            if (index > 0 &&
                index < this.DatasetsCount)
            {
                DatasetContent dataset = this.GetDataset(index);
                this.dataList.RemoveAt(index);
                this.dataList.Insert(index - 1, dataset);
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                return true;
            }
            else return false;
        }
        public bool TryMoveDatasetDown(
            int index)
        {
            if (index >= 0 &&
                index < this.DatasetsCount - 1)
            {
                DatasetContent dataset = this.GetDataset(index);
                this.dataList.RemoveAt(index);
                this.dataList.Insert(index + 1, dataset);
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                return true;
            }
            else return false;
        }

        public void ResortAllDatasets()
        {
            if (this.DatasetsCount > 1)
            {
                List<DatasetContent> dataList = new List<DatasetContent>();
                for (int i = this.dataList.Count - 1; i >= 0; i--) dataList.Add(this.dataList[i]);
                this.dataList.Clear();
                foreach (DatasetContent item in dataList) this.dataList.Add(item);
                this.buildNameList();
                this.on_PropertyChanged("NameList");
            }
        }

        public void RemoveAllDatasets()
        {
            if (this.tryRemoveAllDatasets())
            {
                this.SelectDataset(0);
                this.buildNameList();
                this.on_PropertyChanged("NameList");
            }
        }
        private bool tryRemoveAllDatasets()
        {
            bool datasetRemoved = false;
            DatasetContent[] datasetList = this.dataList.ToArray();
            foreach (DatasetContent item in datasetList) datasetRemoved = this.removeDataset(item) | datasetRemoved;
            return datasetRemoved;
        }
        public bool TryRemoveDataset(
            int index)
        {
            if (this.removeDataset(this.GetDataset(index)))
            {
                this.buildNameList();
                this.on_PropertyChanged("NameList");
                if (index <= this.SelectedDatasetIndex) this.SelectDataset(this.SelectedDatasetIndex);
                return true;
            }
            else return false;
        }
        private bool removeDataset(
            DatasetContent dataset)
        {
            if (dataset is DatasetContent &&
                this.dataList.Contains(dataset))
            {
                dataset.Error -= this.dataset_Error;
                dataset.PropertyChanged -= this.dataset_PropertyChanged;
                this.dataList.Remove(dataset);
                return true;
            }
            else return false;
        }

        private void buildNameList()
        {
            this.names.Clear();
            this.repressPropertyChanged = true;
            foreach (DatasetContent item in this.dataList)
            {
                this.names.Add(item.ToString());
            }
            this.repressPropertyChanged = false;
        }

        public void Load(
            string filename)
        {
            string subSender = "Load";
            this.repressPropertyChanged = true;
            if (File.Exists(filename))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Data));
                    Data data;
                    using (StreamReader reader = new StreamReader(filename)) data = (Data)serializer.Deserialize(reader);
                    this.DataList = data.DataList;
                    this.SelectDataset(data.SelectedIndex);
                    this.filename = filename;
                    this.on_PropertyChanged("Filename");
                }
                catch (Exception exc)
                {
                    // Fehler weitergeben
                    this.on_Error(subSender, exc.Message);
                }
            }
            else
            {
                // Fehler weitergeben
                this.on_Error(subSender, "Datei nicht vorhanden - '" + filename + "'");
            }
            this.repressPropertyChanged = false;
        }
        public void Save() { if (File.Exists(this.Filename)) this.SaveAs(this.Filename); }
        public void SaveAs(
            string filename)
        {
            string subSender = "SaveAs";
            try
            {
                // Dokument speichern
                Data data = new Data();
                data.DataList = this.DataList;
                data.SelectedIndex = this.SelectedDatasetIndex;
                XmlSerializer serializer = new XmlSerializer(typeof(Data));
                using (StreamWriter writer = new StreamWriter(filename)) serializer.Serialize(writer, data);
                this.filename = filename;
                this.on_PropertyChanged("Filename");
            }
            catch (Exception exc)
            {
                // Fehler weitergeben
                this.on_Error(subSender, exc.Message);
            }
        }

        public void SelectItem(ListItem item)
        {
            if (item == null) return;
            if (this.AvailableItems.Contains(item))
            {
                this.AvailableItems.Remove(item);
                this.SelectedItems.Add(item);   
            }
            this.Vinsert_SetList();
            this.on_PropertyChanged("Items");
            switch (this.SelectedPlayer)
            {
                case PlayerSelection.LeftPlayer:
                    this.LeftPlayerCounter = this.SelectedItems.Count;
                    break;
                case PlayerSelection.RightPlayer:
                    this.RightPlayerCounter = this.SelectedItems.Count;
                    break;
            }
            this.Vinsert_SetCounter();
        }

        public void DeselectItem(ListItem item)
        {
            if (item == null) return;
            if (this.SelectedItems.Contains(item))
            {
                this.SelectedItems.Remove(item);
                this.AvailableItems.Add(item);
            }
            this.Vinsert_SetList();
            this.on_PropertyChanged("Items");
            switch (this.SelectedPlayer)
            {
                case PlayerSelection.LeftPlayer:
                    this.LeftPlayerCounter = this.SelectedItems.Count;
                    break;
                case PlayerSelection.RightPlayer:
                    this.RightPlayerCounter = this.SelectedItems.Count;
                    break;
            }
            this.Vinsert_SetCounter();
        }

        public void ResetItems()
        {
            this.AvailableItems = new List<ListItem>(this.items.ToArray());
            this.SelectedItems.Clear();
            this.on_PropertyChanged("Items");
        }

        public override void Vinsert_LoadScene() {
            base.Vinsert_LoadScene();
            this.insertScene.Load();
        }

        public override void Vinsert_ScoreIn() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreIn(this.insertScene.Score); }
        public override void Vinsert_SetScore() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetScore(this.insertScene.Score); }
        public override void Vinsert_ScoreOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_ScoreOut(this.insertScene.Score); }

        //public override void Vinsert_ScoreIn()
        //{
        //    this.Vinsert_SetScore();
        //    if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Game.ToIn();
        //}
        //public override void Vinsert_SetScore()
        //{
        //    if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available)
        //    {
        //        this.insertScene.Game.SetPositionX(this.ScorePositionX);
        //        this.insertScene.Game.SetPositionY(this.ScorePositionY);
        //        this.insertScene.Game.SetTopName(this.LeftPlayerName);
        //        if (this.LeftPlayerBestEstimation) this.insertScene.Game.SetTopEstimated(1);
        //        else this.insertScene.Game.SetTopEstimated(0);
        //        if (this.LeftPlayerHighestCounter) this.insertScene.Game.SetTopDelivered(1);
        //        else this.insertScene.Game.SetTopDelivered(0);
        //        this.insertScene.Game.SetTopScore(this.LeftPlayerScore);
        //        this.insertScene.Game.SetBottomName(this.RightPlayerName);
        //        if (this.RightPlayerBestEstimation) this.insertScene.Game.SetBottomEstimated(1);
        //        else this.insertScene.Game.SetBottomEstimated(0);
        //        if (this.RightPlayerHighestCounter) this.insertScene.Game.SetBottomDelivered(1);
        //        else this.insertScene.Game.SetBottomDelivered(0);
        //        this.insertScene.Game.SetBottomScore(this.RightPlayerScore);
        //    }
        //}
        //public override void Vinsert_ScoreOut()
        //{
        //    if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Game.ToOut();
        //}

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
                if (this.FemalesSelected) scene.SetLeftTopName(this.LeftTeamFemale);
                else scene.SetLeftTopName(this.LeftTeamMale);
                scene.SetLeftTopScore(leftPlayerCounter);
                if (this.FemalesSelected) scene.SetRightBottomName(this.RightTeamFemale);
                else scene.SetRightBottomName(this.RightTeamMale);
                scene.SetRightBottomScore(rightPlayerCounter);
            }
        }
        public void Vinsert_CounterOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Counter.ToOut(); }

        public void Vinsert_TimerIn() {
            if (this.insertScene is VRemote4.HandlerSi.Scene) {
                this.Vinsert_SetTimer();
                this.Vinsert_ResetTimer();
                this.insertScene.Timer.ToIn();
            }
        }
        public void Vinsert_SetTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.Vinsert_SetTimer(this.insertScene.Timer); }
        public void Vinsert_SetTimer(VentuzScenes.GamePool._Modules.Timer scene) {
            int startTime = this.TimerStartTime;
            if (this.RunExtraTime) startTime = this.TimerExtraTime;
            this.Vinsert_SetTimer(scene, startTime);
        }
        public void Vinsert_SetTimer(
            VentuzScenes.GamePool._Modules.Timer scene,
            int startTime) {
            if (scene is VentuzScenes.GamePool._Modules.Timer) {
                scene.SetPositionX(this.TimerPositionX);
                scene.SetPositionY(this.TimerPositionY);
                scene.SetStyle(this.TimerStyle);
                scene.SetScaling(100);
                scene.SetStartTime(startTime);
                scene.SetStopTime(this.TimerStopTime);
                scene.SetAlarmTime1(this.TimerAlarmTime1);
                scene.SetAlarmTime2(this.TimerAlarmTime2);
            }
        }
        public void Vinsert_StartTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Timer.StartTimer(); }
        public void Vinsert_StopTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Timer.StopTimer(); }
        public void Vinsert_ContinueTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Timer.ContinueTimer(); }
        public void Vinsert_ResetTimer() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Timer.ResetTimer(); }
        public void Vinsert_TimerOut() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.Timer.ToOut(); }

        public void Vinsert_ListIn()
        {
            this.ResetItems();
            if (this.insertScene is VRemote4.HandlerSi.Scene)
            {
                this.insertScene.List.SetPositionX(60);
                this.insertScene.List.ToIn();
            }
        }
        public void Vinsert_SetList()
        {
            if (this.insertScene is VRemote4.HandlerSi.Scene)
            {
                foreach (var item in this.AvailableItems) this.insertScene.List.HideItem(item.ID);
                foreach (var item in this.SelectedItems) this.insertScene.List.ShowItem(item.ID);
            }
        }
        public void Vinsert_ListOut()
        {
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.List.ToOut();
        }
        public void Vinsert_FillList()
        {
            if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.List.ShowAllItems();
        }

        public void Vinsert_PlayJinglePling() { if (this.insertScene is VRemote4.HandlerSi.Scene) this.insertScene.PlayPlingJingle(); }

        public override void Vinsert_UnloadScene() {
            base.Vinsert_UnloadScene();
            this.insertScene.Unload();
        }

        public override void Vfullscreen_SetTimer() {
            if (this.RunExtraTime)
                this.Vfullscreen_SetTimer(this.TimerExtraTime);
            else
                this.Vfullscreen_SetTimer(this.TimerStartTime);
        }
        public void Vfullscreen_SetTimer(int startTime) {
            base.Vfullscreen_SetTimer();
            if (this.fullscreenMasterSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
                switch (this.TimerStyle) {
                    case Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules.Timer.Styles.Sec:
                        this.fullscreenMasterScene.Timer.SetStyle(VentuzScenes.Fullscreen.Clock.Styles.Sec);
                        break;
                    case Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool._Modules.Timer.Styles.MinSec:
                    default:
                        this.fullscreenMasterScene.Timer.SetStyle(VentuzScenes.Fullscreen.Clock.Styles.MinSec);
                        break;
                }
                this.fullscreenMasterScene.Timer.SetStartTime(startTime);
                this.fullscreenMasterScene.Timer.SetStopTime(this.TimerStopTime);
                this.fullscreenMasterScene.Timer.SetAlarmTime1(-1);
                this.fullscreenMasterScene.Timer.SetAlarmTime2(-1);
            }
        }

        public override void Vstage_Init()
        {
            base.Vstage_Init();
            this.hostMasterScene.SetDisplaysToNeutral();
        }
        internal void Vstage_ContentIn()
        {
            this.Vhost_ContentIn();
            this.Vleftplayer_ResetInput();
            this.Vrightplayer_ResetInput();
            this.Vleftplayer_ContentIn();
            this.Vrightplayer_ContentIn();
        }
        internal void Vstage_ContentOut()
        {
            this.Vhost_ContentOut();
            this.Vleftplayer_ContentOut();
            this.Vrightplayer_ContentOut();
            this.hostMasterScene.SetDisplaysToNeutral();
        }

        internal void Vplayers_ContentOut()
        {
            this.Vleftplayer_ContentOut();
            this.Vrightplayer_ContentOut();
        }
        internal void Vplayers_ShowInput()
        {
            this.hostMasterScene.SetLeftDisplayText(this.LeftPlayerValue.ToString());
            this.hostMasterScene.SetRightDisplayText(this.RightPlayerValue.ToString());
            this.hostMasterScene.LeftDisplayTextIn();
            this.hostMasterScene.RightDisplayTextIn();
        }

        public override void Vhost_LoadScene() { this.hostScene.Load(); }
        internal void Vhost_ContentIn()
        {
            if (this.hostScene is Host &&
                this.SelectedDataset is DatasetContent)
            {
                this.hostScene.SetText(this.SelectedDataset.HostText);
                this.hostScene.ToIn();
            }
        }
        internal void Vhost_SetPlayerInput()
        {
            string unit = string.Empty;
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vhost_SetPlayerInput(
                    this.hostScene,
                    this.LeftPlayerValue,
                    this.RightPlayerValue);
        }
        internal void Vhost_SetPlayerInput(
            Host scene,
            int leftPlayerValue,
            int rightPlayerValue)
        {
            if (scene is Host)
            {
                string text;
                if (leftPlayerValue > 0)
                {
                    text = leftPlayerValue.ToString();
                    scene.SetLeftValue(text);
                    scene.SetLeftIn();
                }
                else scene.SetLeftOut();

                if (rightPlayerValue > 0)
                {
                    text = rightPlayerValue.ToString();
                    scene.SetRightValue(text);
                    scene.SetRightIn();
                }
                else scene.SetRightOut();

            }
        }
        internal void Vhost_SetResults(
            Host scene,
            int leftPlayerCounter,
            int leftPlayerOffset,
            int rightPlayerCounter,
            int rightPlayerOffset)
        {
            if (scene is Host)
            {
                scene.SetLeftDelivered(leftPlayerCounter.ToString());
                scene.SetLeftDifference(leftPlayerOffset.ToString());
                scene.SetRightDelivered(rightPlayerCounter.ToString());
                scene.SetRightDifference(rightPlayerOffset.ToString());
            }
        }
        internal void Vhost_ShowResults()
        {
            if (this.hostScene is Host) this.hostScene.SetResultsIn();
        }
        internal void Vhost_ContentOut()
        {
            if (this.hostScene is Host) this.hostScene.ToOut();
        }
        public override void Vhost_UnloadScene() { this.hostScene.Unload(); }

        public override void Vleftplayer_LoadScene() { this.leftPlayerScene.Load(); }
        internal void Vleftplayer_ContentIn()
        {
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available &&
                this.SelectedDataset is DatasetContent)
            {
                this.leftPlayerScene.SetTask(this.SelectedDataset.PlayerText);
                this.leftPlayerScene.ToIn();
            }
        }
        internal void Vleftplayer_ContentOut() { if (this.leftPlayerScene is Player) this.leftPlayerScene.ToOut(); }
        internal void Vleftplayer_ResetInput() { if (this.leftPlayerScene is Player) this.leftPlayerScene.Reset(); }
        internal void Vleftplayer_UnlockInput() { if (this.leftPlayerScene is Player) this.leftPlayerScene.UnlockTouch(); }
        internal void Vleftplayer_ReleaseInput() { if (this.leftPlayerScene is Player) this.leftPlayerScene.UnlockTouch(); }
        internal void Vleftplayer_LockInput() { if (this.leftPlayerScene is Player) this.leftPlayerScene.LockTouch(); }
        public override void Vleftplayer_UnloadScene() { this.leftPlayerScene.Unload(); }

        public override void Vrightplayer_LoadScene() { this.rightPlayerScene.Load(); }
        internal void Vrightplayer_ContentIn()
        {
            if (this.rightPlayerScene is Player &&
                this.SelectedDataset is DatasetContent)
            {
                this.rightPlayerScene.SetTask(this.SelectedDataset.PlayerText);
                this.rightPlayerScene.ToIn();
            }
        }
        internal void Vrightplayer_ContentOut() { if (this.rightPlayerScene is Player) this.rightPlayerScene.ToOut(); }
        internal void Vrightplayer_ResetInput() { if (this.rightPlayerScene is Player) this.rightPlayerScene.Reset(); }
        internal void Vrightplayer_UnlockInput() { if (this.rightPlayerScene is Player) this.rightPlayerScene.UnlockTouch(); }
        internal void Vrightplayer_ReleaseInput() { if (this.rightPlayerScene is Player) this.rightPlayerScene.UnlockTouch(); }
        internal void Vrightplayer_LockInput() { if (this.rightPlayerScene is Player) this.rightPlayerScene.LockTouch(); }
        public override void Vrightplayer_UnloadScene() { this.rightPlayerScene.Unload(); }


        public override void ClearGraphic() {
            base.ClearGraphic();
            this.insertScene.Clear();
            this.hostScene.Clear();
            this.leftPlayerScene.Clear();
            this.rightPlayerScene.Clear();
        }

        #endregion


        #region Events.Outgoing

        public event EventHandler TimerAlarm1Fired;
        protected void on_TimerAlarm1Fired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.TimerAlarm1Fired, e); }

        public event EventHandler TimerAlarm2Fired;
        protected void on_TimerAlarm2Fired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.TimerAlarm2Fired, e); }

        public event EventHandler TimerStopFired;
        protected void on_TimerStopFired(object sender, EventArgs e) { Helper.raiseEvent(sender, this.TimerStopFired, e); }

        #endregion

        #region Events.Incoming

        void buzzerHandler_BuzUnit_Buzzered(object sender, BuzzerIDParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_Buzzered);
            if (this.syncContext != null)
                this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_BuzUnit_Buzzered(object content) {
            BuzzerIDParam_EventArgs e = content as BuzzerIDParam_EventArgs;
            if (this.isActive &&
                e is BuzzerIDParam_EventArgs &&
                e.Arg.Name == this.IOUnitName) {
                if (e.Arg.BuzzerID == this.LeftPlayerBuzzerChannel) {
                    if (this.CounterFlipPlayers)
                        this.DoCounter(PlayerSelection.RightPlayer);
                    else
                        this.DoCounter(PlayerSelection.LeftPlayer);
                }
                else if (e.Arg.BuzzerID == this.RightPlayerBuzzerChannel) {
                    if (this.CounterFlipPlayers)
                        this.DoCounter(PlayerSelection.LeftPlayer);
                    else
                        this.DoCounter(PlayerSelection.RightPlayer);
                }
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

        void buzzerHandler_BuzUnit_WorkmodeChanged(object sender, WorkModeParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_WorkmodeChanged);
            if (this.syncContext != null)
                this.syncContext.Post(callback, e);
        }
        void buzzerHandler_BuzUnit_WorkmodeRequest(object sender, WorkModeParam_EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_buzzerHandler_BuzUnit_WorkmodeChanged);
            if (this.syncContext != null)
                this.syncContext.Post(callback, e);
        }
        private void sync_buzzerHandler_BuzUnit_WorkmodeChanged(object content) {
            WorkModeParam_EventArgs e = content as WorkModeParam_EventArgs;
            if (e is WorkModeParam_EventArgs &&
                e.Arg.Name == this.IOUnitName) {
                this.ioUnitWorkMode = e.Arg.WorkMode;
                this.checkIOUnitStatus();
            }
        }

        protected void timer_Alarm1Fired(object sender, EventArgs e) {
            this.on_TimerAlarm1Fired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_Alarm1Fired);
            if (this.syncContext != null)
                this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_Alarm1Fired(object content) {
        }

        protected void timer_Alarm2Fired(object sender, EventArgs e) {
            this.on_TimerAlarm2Fired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_Alarm2Fired);
            if (this.syncContext != null)
                this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_Alarm2Fired(object content) {
        }

        protected void timer_StopFired(object sender, EventArgs e) {
            this.on_TimerStopFired(sender, e);
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_StopFired);
            if (this.syncContext != null)
                this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_StopFired(object content) {
        }

        protected void timer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_timer_PropertyChanged);
            if (this.syncContext != null)
                this.syncContext.Post(callback, e);
        }
        protected virtual void sync_timer_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "CurrentTime")
                    this.TimerCurrentTime = this.insertScene.Timer.CurrentTime;
                else if (e.PropertyName == "IsRunning")
                    this.TimerIsRunning = this.insertScene.Timer.IsRunning;
            }
        }

        void dataset_Error(object sender, Messaging.ErrorEventArgs e) { this.on_Error(sender, e); }

        void dataset_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_dataset_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_dataset_PropertyChanged(object content)
        {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs &&
                !this.repressPropertyChanged)
            {
                if (e.PropertyName == "Name")
                {
                    this.buildNameList();
                    this.on_PropertyChanged("NameList");
                }
            }
            this.Save();
        }

        void leftPlayerScene_OKButtonPressed(object sender, EventArgs e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftPlayerScene_OKButtonPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftPlayerScene_OKButtonPressed(object content)
        {
            this.LeftPlayerValue = Convert.ToInt32(this.leftPlayerScene.Text);
            this.Vhost_SetPlayerInput();
        }

        void rightPlayerScene_OKButtonPressed(object sender, EventArgs e)
        {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerScene_OKButtonPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerScene_OKButtonPressed(object content)
        {
            this.RightPlayerValue = Convert.ToInt32(this.rightPlayerScene.Text);
            this.Vhost_SetPlayerInput();
        }

        #endregion

    }

}
