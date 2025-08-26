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
using Cliparts.SchlagDenStarLive.MainApp.VentuzScenes.GamePool.TeamCorrelation;
using Cliparts.Serialization;
using Cliparts.SchlagDenStarLive.MainApp.Content.Gameboard;
using NReco.VideoConverter;

namespace Cliparts.SchlagDenStarLive.MainApp.GamePool.Templates.TeamCorrelation {

    public class Data {
        public DatasetContent[] DataList;
        public int SelectedIndex;
    }

    public class DatasetContent : Messaging.Message, INotifyPropertyChanged {

        #region Properties

        private string text = string.Empty;
        public string Text {
            get { return this.text; }
            set {
                if (this.text != value) {
                    if (value == null) this.text = string.Empty;
                    else this.text = value;
                    this.buildToString();
                    this.on_PropertyChanged();
                }
            }
        }

        private string hostText = string.Empty;
        public string HostText {
            get { return this.hostText; }
            set {
                if (this.hostText != value) {
                    if (value == null) value = string.Empty;
                    else this.hostText = Regex.Replace(value, "(?<!\r)\n", "\r\n");
                    this.on_PropertyChanged();
                }
            }
        }

        private PositionValues position = PositionValues.Desk;
        public PositionValues Position
        {
            get { return this.position; }
            set
            {
                if (this.position != value)
                {
                    this.position = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private string item1Text = string.Empty;
        public string Item1Text
        {
            get { return this.item1Text; }
            set
            {
                if (this.item1Text != value)
                {
                    if (value == null) this.item1Text = string.Empty;
                    else this.item1Text = Regex.Replace(value, "(?<!\r)\n", "\r\n");
                    this.on_PropertyChanged();
                }
            }
        }

        private string item1Filename = string.Empty;
        public string Item1Filename
        {
            get { return this.item1Filename; }
            set
            {
                if (this.item1Filename != value)
                {
                    if (value == null) this.item1Filename = string.Empty;
                    else this.item1Filename = value;
                    this.Item1Image = Helper.getThumbnailFromMediaFile(this.item1Filename);
                    this.on_PropertyChanged();
                }
            }
        }

        [XmlIgnore]
        public Image Item1Image { get; private set; }

        private string item2Text = string.Empty;
        public string Item2Text
        {
            get { return this.item2Text; }
            set
            {
                if (this.item2Text != value)
                {
                    if (value == null) this.item2Text = string.Empty;
                    else this.item2Text = Regex.Replace(value, "(?<!\r)\n", "\r\n");
                    this.on_PropertyChanged();
                }
            }
        }

        private string item2Filename = string.Empty;
        public string Item2Filename
        {
            get { return this.item2Filename; }
            set
            {
                if (this.item2Filename != value)
                {
                    if (value == null) this.item2Filename = string.Empty;
                    else this.item2Filename = value;
                    this.Item2Image = Helper.getThumbnailFromMediaFile(this.item2Filename);
                    this.on_PropertyChanged();
                }
            }
        }

        [XmlIgnore]
        public Image Item2Image { get; private set; }

        private string item3Text = string.Empty;
        public string Item3Text
        {
            get { return this.item3Text; }
            set
            {
                if (this.item3Text != value)
                {
                    if (value == null) this.item3Text = string.Empty;
                    else this.item3Text = Regex.Replace(value, "(?<!\r)\n", "\r\n");
                    this.on_PropertyChanged();
                }
            }
        }

        private string item3Filename = string.Empty;
        public string Item3Filename
        {
            get { return this.item3Filename; }
            set
            {
                if (this.item3Filename != value)
                {
                    if (value == null) this.item3Filename = string.Empty;
                    else this.item3Filename = value;
                    this.Item3Image = Helper.getThumbnailFromMediaFile(this.item3Filename);
                    this.on_PropertyChanged();
                }
            }
        }

        [XmlIgnore]
        public Image Item3Image { get; private set; }

        private string item4Text = string.Empty;
        public string Item4Text
        {
            get { return this.item4Text; }
            set
            {
                if (this.item4Text != value)
                {
                    if (value == null) this.item4Text = string.Empty;
                    else this.item4Text = Regex.Replace(value, "(?<!\r)\n", "\r\n");
                    this.on_PropertyChanged();
                }
            }
        }

        private string item4Filename = string.Empty;
        public string Item4Filename
        {
            get { return this.item4Filename; }
            set
            {
                if (this.item4Filename != value)
                {
                    if (value == null) this.item4Filename = string.Empty;
                    else this.item4Filename = value;
                    this.Item4Image = Helper.getThumbnailFromMediaFile(this.item4Filename);
                    this.on_PropertyChanged();
                }
            }
        }

        [XmlIgnore]
        public Image Item4Image { get; private set; }

        public List<string> ItemTextsByInput(string input)
        {
            List<string> result = new List<string>();
            if (!string.IsNullOrEmpty(input)
                && input.Length == 4)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    string letter = input.Substring(i, 1);
                    if (letter == "1") result.Add(this.Item1Text);
                    else if (letter == "2") result.Add(this.Item2Text);
                    else if (letter == "3") result.Add(this.Item3Text);
                    else if (letter == "4") result.Add(this.Item4Text);
                }
            }
            return result;
        }

        public List<string> ItemFilenamesByInput(string input)
        {
            List<string> result = new List<string>();
            if (!string.IsNullOrEmpty(input)
                && input.Length == 4)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    string letter = input.Substring(i, 1);
                    if (letter == "1") result.Add(this.Item1Filename);
                    else if (letter == "2") result.Add(this.Item2Filename);
                    else if (letter == "3") result.Add(this.Item3Filename);
                    else if (letter == "4") result.Add(this.Item4Filename);
                }
            }
            return result;
        }

        private string toString = string.Empty;

        #endregion


        #region Funktionen

        public DatasetContent() { }
        public DatasetContent(
            string text) {
            if (string.IsNullOrEmpty(text)) this.Text = "?";
            else this.Text = text;
            this.HostText = text;
        }

        private void buildToString() { this.toString = this.Text; }

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

    public class Business : _Base.TimerScore.Business {

        #region Properties

        private int gamePositionX = 0;
        public int GamePositionX
        {
            get { return this.gamePositionX; }
            set
            {
                if (this.gamePositionX != value)
                {
                    this.gamePositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetGame();
                }
            }
        }

        private int gamePositionY = 0;
        public int GamePositionY
        {
            get { return this.gamePositionY; }
            set
            {
                if (this.gamePositionY != value)
                {
                    this.gamePositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetGame();
                }
            }
        }

        private int gameScaling = 100;
        public int GameScaling
        {
            get { return this.gameScaling; }
            set
            {
                if (this.gameScaling != value)
                {
                    if (value < 10) this.gameScaling = 10;
                    else this.gameScaling = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetGame();
                }
            }
        }

        private int textInsertPositionX = 0;
        public int TextInsertPositionX {
            get { return this.textInsertPositionX; }
            set {
                if (this.textInsertPositionX != value) {
                    this.textInsertPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetGame();
                }
            }
        }

        private int textInsertPositionY = 0;
        public int TextInsertPositionY {
            get { return this.textInsertPositionY; }
            set {
                if (this.textInsertPositionY != value) {
                    this.textInsertPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetGame();
                }
            }
        }

        private int taskCounterPositionX = 0;
        public int TaskCounterPositionX {
            get { return this.taskCounterPositionX; }
            set {
                if (this.taskCounterPositionX != value) {
                    this.taskCounterPositionX = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTaskCounter();
                }
            }
        }

        private int taskCounterPositionY = 0;
        public int TaskCounterPositionY {
            get { return this.taskCounterPositionY; }
            set {
                if (this.taskCounterPositionY != value) {
                    this.taskCounterPositionY = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTaskCounter();
                }
            }
        }

        public const int TaskCounterPenaltyCountMax = 13;

        private int taskCounterSize = 13;
        public int TaskCounterSize {
            get { return this.taskCounterSize; }
            set {
                if (this.taskCounterSize != value) {
                    if (value < 0) value = 0;
                    this.taskCounterSize = value;
                    this.on_PropertyChanged();
                    this.Vinsert_SetTaskCounter();
                }
            }
        }

        private string leftTeamInputDesk = string.Empty;
        public string LeftTeamInputDesk {
            get { return this.leftTeamInputDesk; }
            set {
                if (this.leftTeamInputDesk != value) {
                    if (string.IsNullOrEmpty(value)) this.leftTeamInputDesk = string.Empty;
                    else this.leftTeamInputDesk = (value);
                    this.on_PropertyChanged();
                }
            }
        }

        private string leftTeamInputTablet = string.Empty;
        public string LeftTeamInputTablet {
            get { return this.leftTeamInputTablet; }
            set {
                if (this.leftTeamInputTablet != value) {
                    if (string.IsNullOrEmpty(value)) this.leftTeamInputTablet = string.Empty;
                    else this.leftTeamInputTablet = (value);
                    this.on_PropertyChanged();
                }
            }
        }

        private string rightTeamInputDesk = string.Empty;
        public string RightTeamInputDesk {
            get { return this.rightTeamInputDesk; }
            set {
                if (this.rightTeamInputDesk != value) {
                    if (string.IsNullOrEmpty(value)) this.rightTeamInputDesk = string.Empty;
                    else this.rightTeamInputDesk = (value);
                    this.on_PropertyChanged();
                }
            }
        }

        private string rightTeamInputTablet = string.Empty;
        public string RightTeamInputTablet {
            get { return this.rightTeamInputTablet; }
            set {
                if (this.rightTeamInputTablet != value) {
                    if (string.IsNullOrEmpty(value)) this.rightTeamInputTablet = string.Empty;
                    else this.rightTeamInputTablet = (value);
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

        private string filename = string.Empty;
        public string Filename {
            get { return this.filename; }
            set { if (this.filename != value) this.Load(value); }
        }

        private List<DatasetContent> dataList = new List<DatasetContent>();
        [Serialization.NotSerialized]
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

        private Insert insertScene;
        public override VRemote4.HandlerSi.Scene.States InsertSceneStatus {
            get {
                if (this.insertScene is Insert) return this.insertScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private Host hostScene;
        public override VRemote4.HandlerSi.Scene.States HostSceneStatus {
            get {
                if (this.hostScene is Host) return this.hostScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private Player leftPlayerScene;
        public override VRemote4.HandlerSi.Scene.States LeftPlayerSceneStatus {
            get {
                if (this.leftPlayerScene is Player) return this.leftPlayerScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private Player rightPlayerScene;
        public override VRemote4.HandlerSi.Scene.States RightPlayerSceneStatus {
            get {
                if (this.rightPlayerScene is Player) return this.rightPlayerScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }

        private VRemote4.HandlerSi.Business localVentuzHandler;

        private VRemote4.HandlerSi.Client.Business leftTeamTabletClient;
        private PlayerTablet ventuzLeftTeamTabletScene;
        public VRemote4.HandlerSi.Scene.States VentuzLeftTeamTabletSceneStatus {
            get {
                if (this.ventuzLeftTeamTabletScene is PlayerTablet ) return this.ventuzLeftTeamTabletScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }
        private string leftTeamTabletHostname = string.Empty;
        public string LeftTeamTabletHostname {
            get { return this.leftTeamTabletHostname; }
            set {
                if (this.leftTeamTabletHostname != value) {
                    if (value == null) value = string.Empty;
                    this.leftTeamTabletHostname = value;
                    this.on_PropertyChanged();
                }
            }
        }

        private VRemote4.HandlerSi.Client.Business rightTeamTabletClient;
        private PlayerTablet ventuzRightTeamTabletScene;
        public VRemote4.HandlerSi.Scene.States VentuzRightTeamTabletSceneStatus {
            get {
                if (this.ventuzRightTeamTabletScene is PlayerTablet) return this.ventuzRightTeamTabletScene.Status;
                else return VRemote4.HandlerSi.Scene.States.Unloaded;
            }
        }
        private string rightTeamTabletHostname = string.Empty;
        public string RightTeamTabletHostname {
            get { return this.rightTeamTabletHostname; }
            set {
                if (this.rightTeamTabletHostname != value) {
                    if (value == null) value = string.Empty;
                    this.rightTeamTabletHostname = value;
                    this.on_PropertyChanged();
                }
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

            this.ClassInfo = string.Format("'{0}' of 'Templates.TeamCorrelation'", typeIdentifier);
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

            this.localVentuzHandler = new VRemote4.HandlerSi.Business(syncContext);
            this.localVentuzHandler.Error += this.localVentuzHandler_Error;

            if (this.localVentuzHandler.TryAddClient("Left Tablet", false, out this.leftTeamTabletClient)) {
                this.leftTeamTabletClient.HostnameChanged += this.leftTeamTabletClient_HostnameChanged;
                this.leftTeamTabletClient.StatusChanged += this.leftTeamTabletClient_StatusChanged;
                this.ventuzLeftTeamTabletScene = new PlayerTablet(syncContext, this.leftTeamTabletClient, 0);
                this.ventuzLeftTeamTabletScene.StatusChanged += this.ventuzLeftTeamTabletScene_StatusChanged;
                this.ventuzLeftTeamTabletScene.PropertyChanged += this.ventuzLeftTeamTabletScene_PropertyChanged;
                this.ventuzLeftTeamTabletScene.OKButtonPressed += this.ventuzLeftTeamTabletScene_OKButtonPressed;
            }

            if (this.localVentuzHandler.TryAddClient("Right Tablet", false, out this.rightTeamTabletClient)) {
                this.rightTeamTabletClient.HostnameChanged += this.rightTeamTabletClient_HostnameChanged;
                this.rightTeamTabletClient.StatusChanged += this.rightTeamTabletClient_StatusChanged;
                this.ventuzRightTeamTabletScene = new PlayerTablet(syncContext, this.rightTeamTabletClient, 0);
                this.ventuzRightTeamTabletScene.StatusChanged += this.ventuzRightTeamTabletScene_StatusChanged;
                this.ventuzRightTeamTabletScene.PropertyChanged += this.ventuzRightTeamTabletScene_PropertyChanged;
                this.ventuzRightTeamTabletScene.OKButtonPressed += this.ventuzRightTeamTabletScene_OKButtonPressed;
            }

            ((UserControlContent)this.ContentControl).Pose(this, previewPipe);
            ((UserControlGame)this.GameControl).Pose(this, this.localVentuzHandler);
        }

        public override void Dispose() {
            base.Dispose();

            this.insertScene.StatusChanged -= this.insertScene_StatusChanged;
            this.insertScene.Dispose();
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

            this.localVentuzHandler.Error -= this.localVentuzHandler_Error;
            this.localVentuzHandler.Dispose();

            this.leftTeamTabletClient.HostnameChanged -= this.leftTeamTabletClient_HostnameChanged;
            this.leftTeamTabletClient.StatusChanged -= this.leftTeamTabletClient_StatusChanged;
            this.ventuzLeftTeamTabletScene.StatusChanged -= this.ventuzLeftTeamTabletScene_StatusChanged;
            this.ventuzLeftTeamTabletScene.PropertyChanged -= this.ventuzLeftTeamTabletScene_PropertyChanged;
            this.ventuzLeftTeamTabletScene.OKButtonPressed -= this.ventuzLeftTeamTabletScene_OKButtonPressed;
            this.ventuzLeftTeamTabletScene.Dispose();

            this.rightTeamTabletClient.HostnameChanged -= this.rightTeamTabletClient_HostnameChanged;
            this.rightTeamTabletClient.StatusChanged -= this.rightTeamTabletClient_StatusChanged;
            this.ventuzRightTeamTabletScene.StatusChanged -= this.ventuzRightTeamTabletScene_StatusChanged;
            this.ventuzRightTeamTabletScene.PropertyChanged -= this.ventuzRightTeamTabletScene_PropertyChanged;
            this.ventuzRightTeamTabletScene.OKButtonPressed -= this.ventuzRightTeamTabletScene_OKButtonPressed;
            this.ventuzRightTeamTabletScene.Dispose();
        }

        public override void ResetData() {
            base.ResetData();
            this.TaskCounter = 0;
            this.SelectDataset(0);
            this.resetInput();
        }

        internal void NextTeam()
        {
            if (this.SelectedPlayer == PlayerSelection.LeftPlayer) this.SelectedPlayer = PlayerSelection.RightPlayer;
            else this.SelectedPlayer = PlayerSelection.LeftPlayer;
        }

        public override void Next() {
            base.Next();
            this.TaskCounter++;
            this.SelectDataset(this.SelectedDatasetIndex + 1);
            this.resetInput();
        }

        private void resetInput() {
            this.LeftTeamInputDesk = string.Empty;
            this.LeftTeamInputTablet = string.Empty;
            this.RightTeamInputDesk = string.Empty;
            this.RightTeamInputTablet = string.Empty;
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
                return true;
            }
            else return false;
        }

        public void ResortAllDatasets() {
            if (this.DatasetsCount > 1) {
                List<DatasetContent> dataList = new List<DatasetContent>();
                if (this.SampleIncluded) {
                    dataList.Add(this.dataList[0]);
                    for (int i = this.dataList.Count - 1; i > 0; i--) dataList.Add(this.dataList[i]);
                }
                else {
                    for (int i = this.dataList.Count - 1; i >= 0; i--) dataList.Add(this.dataList[i]);
                }
                this.dataList.Clear();
                foreach (DatasetContent item in dataList) this.dataList.Add(item);
                this.buildNameList();
                this.on_PropertyChanged("NameList");
            }
        }

        public void RemoveAllDatasets() {
            if (this.tryRemoveAllDatasets()) {
                this.SelectDataset(0);
                this.buildNameList();
                this.on_PropertyChanged("NameList");
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

        public override void New() {
            base.New();
            this.Filename = string.Empty;
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
        public void Import(
            string filename) {
            string subSender = "Import";
            if (File.Exists(filename)) {
                System.IO.StreamReader file = null;
                try {
                    string line;
                    file = new System.IO.StreamReader(filename, Encoding.UTF8);
                    while ((line = file.ReadLine()) != null) {
                        if (!string.IsNullOrEmpty(line)) this.tryAddDataset(new DatasetContent(line), -1);
                    }
                    file.Close();
                }
                catch (Exception exc) {
                    if (file != null) file.Close();
                    this.on_Error(subSender, exc.Message);
                }
                this.on_PropertyChanged("NameList");
                this.Save();
            }
            else {
                // Fehler weitergeben
                this.on_Error(subSender, "Datei nicht vorhanden - '" + filename + "'");
            }
        }

        public override void Vinsert_LoadScene() { this.insertScene.Load(); }
        public override void Vinsert_ScoreIn() {
            this.Vinsert_SetScore();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Score.ToIn();
        }
        public override void Vinsert_SetScore() {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) {
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
        public override void Vinsert_ScoreOut() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Score.ToOut(); }

        public override void Vinsert_TimerIn()
        {
            this.Vinsert_SetTimer();
            this.Vinsert_ResetTimer();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.ToIn();
        }
        public override void Vinsert_SetTimer()
        {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available)
            {
                this.insertScene.Timer.SetPositionX(this.TimerPositionX);
                this.insertScene.Timer.SetPositionY(this.TimerPositionY);
                this.insertScene.Timer.SetStyle(this.TimerStyle);
                this.insertScene.Timer.SetScaling(100);
                if (this.RunExtraTime) this.insertScene.Timer.SetStartTime(this.TimerExtraTime);
                else this.insertScene.Timer.SetStartTime(this.TimerStartTime);
                this.insertScene.Timer.SetStopTime(this.TimerStopTime);
                this.insertScene.Timer.SetAlarmTime1(this.TimerAlarmTime1);
                this.insertScene.Timer.SetAlarmTime2(this.TimerAlarmTime2);
            }
        }
        public override void Vinsert_StartTimer()
        {
            this.Vinsert_SetTimer();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.StartTimer();
        }
        public override void Vinsert_StopTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.StopTimer(); }
        public override void Vinsert_ContinueTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.ContinueTimer(); }
        public override void Vinsert_ResetTimer() { if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.ResetTimer(); }
        public override void Vinsert_TimerOut()
        {
            this.Vinsert_StopTimer();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Timer.ToOut();
        }

        internal void Vinsert_GameIn() {
            this.Vinsert_SetGame();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Game.ToIn();
            if (this.TaskCounter > 0 && this.TaskCounter <= this.TaskCounterSize) this.Vinsert_TaskCounterIn();
        }
        internal void Vinsert_ToNextItem()
        {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Game.NextItem();
        }
        internal void Vinsert_AllItemsOut()
        {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Game.ToAllItemsOut();
        }
        internal void Vinsert_FirstItemIn()
        {
            this.Vinsert_SetGame();
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Game.ToTopItem1();
        }
        internal void Vinsert_GameOut()
        {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.insertScene.Game.ToOut();
            this.Vinsert_TaskCounterOut();
        }
        internal void Vinsert_SetGame()
        {
            if (this.InsertSceneStatus == VRemote4.HandlerSi.Scene.States.Available)
            {
                string inputTop = string.Empty;
                string inputBottom = string.Empty;
                switch (this.SelectedPlayer)
                {
                    case PlayerSelection.LeftPlayer:
                        switch (this.SelectedDataset.Position)
                        {
                            case PositionValues.Tablet:
                                inputTop = this.LeftTeamInputTablet;
                                inputBottom = this.LeftTeamInputDesk;
                                break;
                            case PositionValues.Desk:
                                inputTop = this.LeftTeamInputDesk;
                                inputBottom = this.LeftTeamInputTablet;
                                break;
                        }
                        break;
                    case PlayerSelection.RightPlayer:
                        switch (this.SelectedDataset.Position)
                        {
                            case PositionValues.Tablet:
                                inputTop = this.RightTeamInputTablet;
                                inputBottom = this.RightTeamInputDesk;
                                break;
                            case PositionValues.Desk:
                                inputTop = this.RightTeamInputDesk;
                                inputBottom = this.RightTeamInputTablet;
                                break;
                        }
                        break;
                }
                this.Vinsert_SetGame(this.insertScene.Game, this.SelectedDataset, inputTop, inputBottom);
            }
        }
        internal void Vinsert_SetGame(
            Game scene,
            DatasetContent content,
            string inputTop,
            string inputBottom)
        {
            if (scene is Game &&
                content is DatasetContent)
            {
                scene.SetPositionX(this.GamePositionX);
                scene.SetPositionY(this.GamePositionY);
                scene.SetScaling(this.GameScaling);
                scene.SetTaskFile(1, content.Item1Filename);
                scene.SetTaskFile(2, content.Item2Filename);
                scene.SetTaskFile(3, content.Item3Filename);
                scene.SetTaskFile(4, content.Item4Filename);
                switch (content.Position)
                {
                    case PositionValues.Desk:
                        scene.SetTopRowPosition(PositionValues.Desk);
                        scene.SetBottomRowPosition(PositionValues.Tablet);
                        break;
                    case PositionValues.Tablet:
                        scene.SetTopRowPosition(PositionValues.Tablet);
                        scene.SetBottomRowPosition(PositionValues.Desk);
                        break;
                }
                List<string> filenames = content.ItemFilenamesByInput(inputTop);
                if (filenames.Count == 4)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        scene.SetTopRowTargetFile(i + 1, filenames[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        scene.SetTopRowTargetFile(i + 1, string.Empty);
                    }
                }
                filenames = content.ItemFilenamesByInput(inputBottom);
                if (filenames.Count == 4)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        scene.SetBottomRowTargetFile(i + 1, filenames[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        scene.SetBottomRowTargetFile(i + 1, string.Empty);
                    }
                }
            }
        }

        public void Vinsert_TaskCounterIn() {
            this.Vinsert_SetTaskCounter();
            if (this.insertScene is Insert &&
                this.insertScene.TaskCounter is VentuzScenes.GamePool._Modules.TaskCounter) this.insertScene.TaskCounter.ToIn();
        }
        public void Vinsert_SetTaskCounter() { if (this.insertScene is Insert) this.Vinsert_SetTaskCounter(this.insertScene.TaskCounter, this.TaskCounter); }
        public void Vinsert_SetTaskCounter(
            VentuzScenes.GamePool._Modules.TaskCounter scene,
            int counter) {
            if (scene is VentuzScenes.GamePool._Modules.TaskCounter) {
                scene.SetPositionX(this.TaskCounterPositionX);
                scene.SetPositionY(this.TaskCounterPositionY);
                scene.SetStyle(VentuzScenes.GamePool._Modules.TaskCounter.Styles.Numeric);
                scene.SetSize(this.TaskCounterSize);
                scene.SetCounter(counter);
            }
        }
        public void Vinsert_TaskCounterOut() {
            if (this.insertScene is Insert &&
                this.insertScene.TaskCounter is VentuzScenes.GamePool._Modules.TaskCounter) this.insertScene.TaskCounter.ToOut();
        }

        public override void Vinsert_UnloadScene() { this.insertScene.Unload(); }

        public override void Vhost_LoadScene() { this.hostScene.Load(); }
        public void Vhost_SetContent() {
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) Vhost_SetContent(
                this.hostScene,
                this.SelectedDataset,
                this.LeftTeamInputDesk,
                this.LeftTeamInputTablet,
                this.RightTeamInputDesk,
                this.RightTeamInputTablet);
        }
        internal void Vhost_SetContent(
            Host scene,
            DatasetContent content,
            string inputLeftDesk,
            string inputLeftTablet,
            string inputRightDesk,
            string inputRightTablet)
        {
            if (scene is Host &&
                content is DatasetContent)
            {
                scene.SetHeadline(content.HostText);
                scene.SetTask(1, content.Item1Text);
                scene.SetTask(2, content.Item2Text);
                scene.SetTask(3, content.Item3Text);
                scene.SetTask(4, content.Item4Text);

                scene.SetLeftTeamInsideName("SHANIA");
                scene.SetLeftTeamOutsideName("DAVINA");
                scene.SetRightTeamInsideName("LILLI");
                scene.SetRightTeamOutsideName("LUNA");

                List<string> text = content.ItemTextsByInput(inputLeftDesk);
                if (text.Count == 4)
                {
                    for (int i = 0; i < 4; i++) scene.SetLeftTeamInsideInput(i + 1, text[i]);
                }
                else
                {
                    for (int i = 0; i < 4; i++) scene.SetLeftTeamInsideInput(i + 1, string.Empty);
                }
                text = content.ItemTextsByInput(inputLeftTablet);
                if (text.Count == 4)
                {
                    for (int i = 0; i < 4; i++) scene.SetLeftTeamOutsideInput(i + 1, text[i]);
                }
                else
                {
                    for (int i = 0; i < 4; i++) scene.SetLeftTeamOutsideInput(i + 1, string.Empty);
                }
                text = content.ItemTextsByInput(inputRightDesk);
                if (text.Count == 4)
                {
                    for (int i = 0; i < 4; i++) scene.SetRightTeamInsideInput(i + 1, text[i]);
                }
                else
                {
                    for (int i = 0; i < 4; i++) scene.SetRightTeamInsideInput(i + 1, string.Empty);
                }
                text = content.ItemTextsByInput(inputRightTablet);
                if (text.Count == 4)
                {
                    for (int i = 0; i < 4; i++) scene.SetRightTeamOutsideInput(i + 1, text[i]);
                }
                else
                {
                    for (int i = 0; i < 4; i++) scene.SetRightTeamOutsideInput(i + 1, string.Empty);
                }
            }
        }
        public override void Vhost_UnloadScene() { this.hostScene.Unload(); }

        public override void Vleftplayer_LoadScene() { this.leftPlayerScene.Load(); }
        internal void Vleftplayer_SetContent() {
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vplayer_SetContent(this.leftPlayerScene, this.SelectedDataset);
        }
        internal void Vleftplayer_EnableInput() { if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.Unlock(); }
        internal void Vleftplayer_DisableInput() { if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.Lock(); }
        public override void Vleftplayer_UnloadScene() { this.leftPlayerScene.Unload(); }

        public override void Vrightplayer_LoadScene() { this.rightPlayerScene.Load(); }
        internal void Vrightplayer_SetContent() {
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vplayer_SetContent(this.rightPlayerScene, this.SelectedDataset);
        }
        internal void Vrightplayer_EnableInput() { if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.Unlock(); }
        internal void Vrightplayer_DisableInput() { if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.Lock(); }
        public override void Vrightplayer_UnloadScene() { this.rightPlayerScene.Unload(); }
        internal void Vplayer_SetContent(
            Player scene,
            DatasetContent content)
        {
            if (scene is Player &&
                content is DatasetContent)
            {
                scene.SetHeadline(content.Text);
                scene.SetTask(1, content.Item1Text);
                scene.SetTask(2, content.Item2Text);
                scene.SetTask(3, content.Item3Text);
                scene.SetTask(4, content.Item4Text);
            }
        }

        internal void Vplayers_EnableInput() {
            this.Vleftplayer_EnableInput();
            this.Vrightplayer_EnableInput();
            this.Vlefttablet_EnableInput();
            this.Vrighttablet_EnableInput();
        }

        internal void Vstage_ContentIn() {
            this.Vhost_SetContent();
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.ToIn();
            this.Vleftplayer_SetContent();
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.ToIn();
            this.Vrightplayer_SetContent();
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.ToIn();
            this.Vlefttablet_SetContent();
            if (this.VentuzLeftTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzLeftTeamTabletScene.ToIn();
            this.Vrighttablet_SetContent();
            if (this.VentuzRightTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzRightTeamTabletScene.ToIn();
        }

        internal void Vstage_ContentOut() {
            if (this.HostSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.hostScene.ToOut();
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.ToOut();
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.ToOut();
            if (this.VentuzLeftTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzLeftTeamTabletScene.ToOut();
            if (this.VentuzRightTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzRightTeamTabletScene.ToOut();
        }

        public override void Vstage_Init() {
            base.Vstage_Init();
            this.Vlefttablet_Init();
            this.Vrighttablet_Init();
        }

        public void Vlefttablet_Start() { this.leftTeamTabletClient.Start(this.LeftTeamTabletHostname); }
        public void Vlefttablet_Init() {
            if (this.VentuzLeftTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzLeftTeamTabletScene.Reset();
        }
        internal void Vlefttablet_SetContent() {
            if (this.VentuzLeftTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vtablet_SetContent(this.ventuzLeftTeamTabletScene, this.SelectedDataset);
        }
        internal void Vlefttablet_EnableInput() {
            if (this.VentuzLeftTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzLeftTeamTabletScene.Unlock();
        }
        internal void Vlefttablet_DisableInput() {
            if (this.VentuzLeftTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzLeftTeamTabletScene.Lock();
        }
        public void Vlefttablet_ShutDown() { this.leftTeamTabletClient.Shutdown(); }

        public void Vrighttablet_Start() { this.rightTeamTabletClient.Start(this.RightTeamTabletHostname); }
        public void Vrighttablet_Init() {
            if (this.VentuzRightTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzRightTeamTabletScene.Reset();
        }
        internal void Vrighttablet_SetContent() {
            if (this.VentuzRightTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.Vtablet_SetContent(this.ventuzRightTeamTabletScene, this.SelectedDataset);
        }
        internal void Vrighttablet_EnableInput() {
            if (this.VentuzRightTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzRightTeamTabletScene.Unlock();
        }
        internal void Vrighttablet_DisableInput() {
            if (this.VentuzRightTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzRightTeamTabletScene.Lock();
        }
        public void Vrighttablet_ShutDown() { this.rightTeamTabletClient.Shutdown(); }
        internal void Vtablet_SetContent(
            PlayerTablet scene,
            DatasetContent content)
        {
            if (scene is PlayerTablet &&
                content is DatasetContent)
            {
                scene.SetHeadline(content.Text);
                scene.SetTask(1, content.Item1Text);
                scene.SetTask(2, content.Item2Text);
                scene.SetTask(3, content.Item3Text);
                scene.SetTask(4, content.Item4Text);
            }
        }

        public override void V_ClearStage()
        {
            base.V_ClearStage();
            if (this.LeftPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.leftPlayerScene.ToOut();
            if (this.RightPlayerSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.rightPlayerScene.ToOut();
            if (this.VentuzLeftTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzLeftTeamTabletScene.ToOut();
            if (this.VentuzRightTeamTabletSceneStatus == VRemote4.HandlerSi.Scene.States.Available) this.ventuzRightTeamTabletScene.ToOut();
        }

        public override void ClearGraphic() {
            base.ClearGraphic();
            this.insertScene.Clear();
            this.hostScene.Clear();
        }

        #endregion


        #region Events.Outgoing
        #endregion

        #region Events.Incoming

        void dataset_Error(object sender, Messaging.ErrorEventArgs e) { this.on_Error(sender, e); }

        void dataset_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_dataset_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_dataset_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "Text") {
                    this.buildNameList();
                    this.on_PropertyChanged("NameList");
                }
            }
            this.Save();
        }

        protected override void sync_timer_PropertyChanged(object content)
        {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs)
            {
                if (e.PropertyName == "CurrentTime")
                    this.TimerCurrentTime = this.insertScene.Timer.CurrentTime;
                else if (e.PropertyName == "IsRunning")
                    this.TimerIsRunning = this.insertScene.Timer.IsRunning;
            }
        }

        void localVentuzHandler_Error(object sender, ErrorEventArgs e) {
            Exception exc = e.GetException();
            this.on_Error(sender, new Messaging.ErrorEventArgs(this, sender.ToString(), DateTime.Now, exc.ToString()));
        }

        void leftPlayerScene_OKButtonPressed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftPlayerScene_OKButtonPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftPlayerScene_OKButtonPressed(object content) {
            this.LeftTeamInputDesk = this.leftPlayerScene.Input;
            this.Vhost_SetContent();
        }

        void rightPlayerScene_OKButtonPressed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightPlayerScene_OKButtonPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightPlayerScene_OKButtonPressed(object content) {
            this.RightTeamInputDesk = this.rightPlayerScene.Input;
            this.Vhost_SetContent();
        }

        void leftTeamTabletClient_HostnameChanged(object sender, VRemote4.HandlerSi.Client.HostnameArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftTeamTabletClient_HostnameChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftTeamTabletClient_HostnameChanged(object content) {
            VRemote4.HandlerSi.Client.HostnameArgs e = content as VRemote4.HandlerSi.Client.HostnameArgs;
            if (e is VRemote4.HandlerSi.Client.HostnameArgs) this.LeftTeamTabletHostname = e.Name;
        }

        void leftTeamTabletClient_StatusChanged(object sender, VRemote4.HandlerSi.Client.ClientStateArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_leftTeamTabletClient_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_leftTeamTabletClient_StatusChanged(object content) {
            VRemote4.HandlerSi.Client.ClientStateArgs e = content as VRemote4.HandlerSi.Client.ClientStateArgs;
            if (e is VRemote4.HandlerSi.Client.ClientStateArgs &&
                e.Status.HasValue &&
                e.Status.Value == VRemote4.HandlerSi.Client.ClientStates.Active &&
                this.ventuzLeftTeamTabletScene is VRemote4.HandlerSi.Scene) this.ventuzLeftTeamTabletScene.Load();
        }

        void ventuzLeftTeamTabletScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzLeftTeamTabletScene_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzLeftTeamTabletScene_StatusChanged(object content) {
            VRemote4.HandlerSi.Scene.StatusArgs e = content as VRemote4.HandlerSi.Scene.StatusArgs;
            if (e is VRemote4.HandlerSi.Scene.StatusArgs) {
                if (e.Status == VRemote4.HandlerSi.Scene.States.Unloaded) this.leftTeamTabletClient.Shutdown();
                else if (e.Status == VRemote4.HandlerSi.Scene.States.Available) this.Vlefttablet_Init();
            }
            this.on_PropertyChanged("VentuzLeftTeamTabletSceneStatus");
        }

        void ventuzLeftTeamTabletScene_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzLeftTeamTabletScene_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzLeftTeamTabletScene_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "Selection") {
                    //this.LeftTeamInput = this.ventuzTerminalLeftScene.Controller.Selection;
                }
            }
        }

        void ventuzLeftTeamTabletScene_OKButtonPressed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzLeftTeamTabletScene_OKButtonPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzLeftTeamTabletScene_OKButtonPressed(object content) {
            this.LeftTeamInputTablet = this.ventuzLeftTeamTabletScene.Input;
            this.Vhost_SetContent();
        }

        void rightTeamTabletClient_HostnameChanged(object sender, VRemote4.HandlerSi.Client.HostnameArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightTeamTabletClient_HostnameChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightTeamTabletClient_HostnameChanged(object content) {
            VRemote4.HandlerSi.Client.HostnameArgs e = content as VRemote4.HandlerSi.Client.HostnameArgs;
            if (e is VRemote4.HandlerSi.Client.HostnameArgs) this.RightTeamTabletHostname = e.Name;
        }

        void rightTeamTabletClient_StatusChanged(object sender, VRemote4.HandlerSi.Client.ClientStateArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_rightTeamTabletClient_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_rightTeamTabletClient_StatusChanged(object content) {
            VRemote4.HandlerSi.Client.ClientStateArgs e = content as VRemote4.HandlerSi.Client.ClientStateArgs;
            if (e is VRemote4.HandlerSi.Client.ClientStateArgs &&
                e.Status.HasValue &&
                e.Status.Value == VRemote4.HandlerSi.Client.ClientStates.Active &&
                this.ventuzRightTeamTabletScene is VRemote4.HandlerSi.Scene) this.ventuzRightTeamTabletScene.Load();
        }

        void ventuzRightTeamTabletScene_StatusChanged(object sender, VRemote4.HandlerSi.Scene.StatusArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzRightTeamTabletScene_StatusChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzRightTeamTabletScene_StatusChanged(object content) {
            VRemote4.HandlerSi.Scene.StatusArgs e = content as VRemote4.HandlerSi.Scene.StatusArgs;
            if (e is VRemote4.HandlerSi.Scene.StatusArgs) {
                if (e.Status == VRemote4.HandlerSi.Scene.States.Unloaded) this.rightTeamTabletClient.Shutdown();
                else if (e.Status == VRemote4.HandlerSi.Scene.States.Available) this.Vrighttablet_Init();
            }
            this.on_PropertyChanged("VentuzRightTeamTabletSceneStatus");
        }

        void ventuzRightTeamTabletScene_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzRightTeamTabletScene_PropertyChanged);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzRightTeamTabletScene_PropertyChanged(object content) {
            PropertyChangedEventArgs e = content as PropertyChangedEventArgs;
            if (e is PropertyChangedEventArgs) {
                if (e.PropertyName == "Selection") {
                    //this.RightTeamInput = this.ventuzTerminalRightScene.Controller.Selection;
                }
            }
        }

        void ventuzRightTeamTabletScene_OKButtonPressed(object sender, EventArgs e) {
            SendOrPostCallback callback = new SendOrPostCallback(this.sync_ventuzRightTeamTabletScene_OKButtonPressed);
            if (this.syncContext != null) this.syncContext.Post(callback, e);
        }
        private void sync_ventuzRightTeamTabletScene_OKButtonPressed(object content) {
            this.RightTeamInputTablet = this.ventuzRightTeamTabletScene.Input;
            this.Vhost_SetContent();
        }

        #endregion

    }
}
